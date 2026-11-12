using System;
using System.Collections.Generic;
using System.Linq;

namespace Athanor.Core
{
    public enum RunPhase { Playing, DepthCleared, Lost, Won, AspectOffer, EventOffer }

    public sealed class IgniteOutcome
    {
        public List<ReactionEvent> Events;
        public long Gained;
        public List<string> Dropped = new List<string>();   // discoveries pushed off a full rack
        public string[] Final;                              // the bench as the chain left it
        public bool[] Made;                                 // true where a material made this Ignite stood at the end
        public RunPhase PhaseAfter;
    }

    /// <summary>One run: descending through depths, spending trials, collecting catalysts.</summary>
    public sealed partial class RunState
    {
        public const int MinHand = 3, BaseHand = 5, BaseTrials = 4, BaseRedraws = 2, FinalDepth = 5, FinalTrials = 6, StarterCopies = 2, OfferSize = 3;
        /// <summary>How many discoveries the hand can keep; a new one pushes the weakest old one out.</summary>
        public const int RackSize = 12;

        readonly ContentDb db;
        readonly Unlocks unlocks;
        readonly Journal journal;
        readonly Satchel satchel, pouch;
        readonly Rng rng;
        readonly List<string> hand = new List<string>();
        readonly List<CatalystData> catalysts = new List<CatalystData>();
        readonly HashSet<string> fired = new HashSet<string>();
        List<CatalystData> offer = new List<CatalystData>();
        int carried;   // trials left unspent at the last depth; they are added to the next

        public RunState(ContentDb db, Journal journal, int seed, int startDepth = 1, RunOptions options = null)
        {
            this.db = db; this.journal = journal;
            this.options = options ?? RunOptions.Legacy;
            unlocks = this.options.Unlocks ?? Unlocks.Fresh(db);   // no profile: what the content leaves open
            rng = new Rng(seed ^ 0x5bd1e995);
            aspectRng = new Rng(seed ^ 0x2545F491);
            eventRng = new Rng(seed ^ 0x7F4A7C15);
            // Starters won from goals do not thin the satchel: they wait in a pouch of their own, and each hand
            // takes PouchCards of them on top of the usual draw. (Mode 0 and 1 put them in the satchel; kept for the balance report.)
            var own = db.Starters.Where(m => IsOpen(m.id) && (PouchMode < 2 || !m.locked));
            satchel = new Satchel(own.SelectMany(m => Enumerable.Repeat(m.id, m.locked && PouchMode == 1 ? 1 : StarterCopies)), seed);
            pouch = new Satchel(db.Starters.Where(m => IsOpen(m.id) && m.locked && PouchMode == 2).SelectMany(m => Enumerable.Repeat(m.id, StarterCopies)), seed ^ 0x3C6EF372);
            Depth = Math.Max(1, Math.Min(FinalDepth, startDepth));
            BeginDepth();
        }

        public int Depth { get; private set; }
        public long Potency { get; private set; }
        public int Target => db.TargetFor(Depth);
        public int TrialsLeft { get; private set; }
        public int RedrawsLeft { get; private set; }
        public int DiscoveriesThisRun { get; private set; }
        public RunPhase Phase { get; private set; }
        public Bench Bench { get; } = new Bench();
        public IReadOnlyList<string> Hand => hand;
        public IReadOnlyList<CatalystData> Catalysts => catalysts;
        public IReadOnlyList<CatalystData> CatalystOffer => offer;
        public int HandSize => Math.Max(MinHand, BaseHand + Sum(Effects.ExtraHand) + (HasAspect(AspectEffects.Greedy) ? 1 : 0) + handDelta);
        public bool Diagonals => Has(Effects.Diagonals);
        public bool CanIgnite => Phase == RunPhase.Playing && IgniteResolver.HasAnyPair(Bench, Rules);
        /// <summary>How many times the hand was redrawn this run.</summary>
        public int RedrawsUsed { get; private set; }
        public RunOptions Options => options;
        public bool IsOpen(string id) => unlocks.IsOpen(id);
        /// <summary>Copies of a material in the hand or on the bench.</summary>
        public int Owned(string id) => hand.Count(h => h == id) + Bench.Tiles.Count(t => t == id);
        bool IsStarter(string id) => db.Material(id).tier == 0;
        /// <summary>A starting material that was won from a goal: it is drawn from the pouch, not the satchel.</summary>
        bool IsPouch(string id) => PouchMode == 2 && db.Material(id).tier == 0 && db.Material(id).locked;
        bool IsMain(string id) => IsStarter(id) && !IsPouch(id);
        Satchel DeckOf(string id) => IsPouch(id) ? pouch : satchel;
        /// <summary>How unlocked starters reach the hand. 2 (the game): from their own pouch, PouchCards a hand. 0: two copies in the satchel. 1: one copy in the satchel.</summary>
        public static int PouchMode = 2;
        public const int PouchCards = 1;
        public bool HasFired(string a, string b) => fired.Contains(ContentDb.PairKey(a, b));
        public bool HasFired(RecipeData recipe) => fired.Contains(db.KeyOf(recipe));
        public int SatchelSize => satchel.Total + pouch.Total;

        bool Has(string effect) => IgniteResolver.Has(catalysts, effect);

        /// <summary>The amounts of every held catalyst with this effect, added up; one that names no amount counts 1.</summary>
        int Sum(string effect)
        {
            float sum = 0;
            foreach (var c in catalysts) if (c.effect == effect) sum += c.amount > 0 ? c.amount : 1;
            return (int)sum;
        }

        void BeginDepth()
        {
            Potency = 0;
            handDelta = 0; riShift = 0; tripleFirst = false; freeDiscoveryUsed = false;
            depthBlocked.Clear(); depthHeated.Clear();
            TrialsLeft = (Depth == FinalDepth ? FinalTrials : BaseTrials) + Sum(Effects.ExtraTrial) + carried + aspects.Count(a => a.Data.effect == AspectEffects.Cold);
            carried = 0;
            RedrawsLeft = Math.Max(0, BaseRedraws + Sum(Effects.ExtraRedraw) - (HasAspect(AspectEffects.Greedy) ? 1 : 0));
            RebuildRules();
            aspectPending = options.Aspects && (Depth == 1 || Depth == 3 || Depth == 5);
            eventPending = options.Events;
            Advance();
        }

        /// <summary>The start of a depth: the aspect offer where there is one, then the event, then the bench.</summary>
        void Advance()
        {
            if (aspectPending)
            {
                aspectPending = false;
                BuildAspectOffer();
                if (aspectOffer.Count > 0) { Phase = RunPhase.AspectOffer; return; }
            }
            if (eventPending)
            {
                eventPending = false;
                FillHand();
                DrawEvent();
                if (CurrentEvent != null) { Phase = RunPhase.EventOffer; return; }
            }
            Phase = RunPhase.Playing;
            FillHand();
        }

        /// <summary>Draws starting materials until the hand holds its full count of them. Discoveries do not count.</summary>
        void FillHand()
        {
            int want = HandSize + fallowPending - hand.Count(IsMain) - Bench.Tiles.Count(t => t != null && IsMain(t));
            fallowPending = 0;
            if (want > 0) hand.AddRange(satchel.Draw(want));
            int more = (pouch.Total > 0 ? PouchCards : 0) - hand.Count(IsPouch) - Bench.Tiles.Count(t => t != null && IsPouch(t));
            if (more > 0) hand.AddRange(pouch.Draw(more));
        }

        public bool Place(int handIndex, int tile)
        {
            if (Phase != RunPhase.Playing || handIndex < 0 || handIndex >= hand.Count) return false;
            if (tile < 0 || tile >= Bench.TileCount || Bench.Tiles[tile] != null || Rules.Blocked[tile]) return false;
            Bench.Tiles[tile] = hand[handIndex];
            hand.RemoveAt(handIndex);
            return true;
        }

        public bool Unplace(int tile)
        {
            if (Phase != RunPhase.Playing || tile < 0 || tile >= Bench.TileCount || Bench.Tiles[tile] == null) return false;
            hand.Add(Bench.Tiles[tile]);
            Bench.Tiles[tile] = null;
            return true;
        }

        /// <summary>Throws away the starting materials in hand (not the bench, not discoveries) and draws again.</summary>
        public bool Redraw()
        {
            if (Phase != RunPhase.Playing || RedrawsLeft <= 0) return false;
            RedrawsLeft--; RedrawsUsed++;
            satchel.Discard(hand.Where(IsMain));
            pouch.Discard(hand.Where(IsPouch));
            hand.RemoveAll(IsStarter);
            FillHand();
            return true;
        }

        /// <summary>What a reaction's score depends on here and now (also used by the scripted player to plan).</summary>
        public ScoreContext NewContext() => new ScoreContext { Depth = Depth, Rules = Rules, TripleFirst = tripleFirst };

        public IgniteOutcome Ignite()
        {
            if (!CanIgnite) throw new InvalidOperationException("nothing on the bench can react");
            var placed = (string[])Bench.Tiles.Clone();
            var resolved = IgniteResolver.Resolve(Bench, db, journal, fired, catalysts, NewContext());
            var events = resolved.Events;
            long gained = 0; bool madeUltimate = false, discovered = false, reacted = false;
            var made = new List<string>();
            foreach (var e in events)
            {
                gained += e.Potency;
                if (e.Discovery) { DiscoveriesThisRun++; discovered = true; }
                if (e.Result == null) continue;
                reacted = true;
                if (e.Result == db.Ultimate) madeUltimate = true;
                else if (HasAspectOn(AspectEffects.Echo, e.Hub)) made.Add(e.Result);      // an echo keeps a copy
            }
            if (reacted) tripleFirst = false;
            // everything placed is spent: starting materials go back to the satchel, discoveries are used up.
            // Only what the chain made, and left standing, is kept.
            for (int t = 0; t < Bench.TileCount; t++)
            {
                string id = placed[t];
                if (id == null) continue;
                if (IsStarter(id))
                {
                    if (HasAspectOn(AspectEffects.Volatile, t)) DeckOf(id).Lose(id);     // burnt up for the rest of the run
                    else DeckOf(id).Discard(new[] { id });
                }
                else if (HasAspectOn(AspectEffects.Anchor, t)) made.Add(id);          // an anchored discovery comes back
                else if (Has(Effects.Salvage) && !resolved.Made[t] && resolved.Final[t] == id) made.Add(id);   // left standing, untouched
            }
            for (int t = 0; t < Bench.TileCount; t++)
                if (resolved.Made[t] && resolved.Final[t] != null) made.Add(resolved.Final[t]);
            foreach (var a in aspects)
                if (a.Data.effect == AspectEffects.Fallow && placed[a.Tiles[0]] == null) fallowPending++;
            var dropped = new List<string>();
            foreach (var id in made) AddToRack(id, dropped);
            Bench.Clear();
            Potency += gained;
            if (discovered && Has(Effects.FreeDiscovery) && !freeDiscoveryUsed) freeDiscoveryUsed = true;   // this one costs no trial
            else TrialsLeft--;

            if (madeUltimate) Phase = RunPhase.Won;
            else if (Depth < FinalDepth && Potency >= Target) { Phase = RunPhase.DepthCleared; BuildOffer(); }
            else if (TrialsLeft <= 0)
            {
                if (Has(Effects.SecondChance) && !secondChanceUsed) { secondChanceUsed = true; TrialsLeft = Sum(Effects.SecondChance); FillHand(); }
                else Phase = RunPhase.Lost;
            }
            else FillHand();
            return new IgniteOutcome { Events = events, Gained = gained, PhaseAfter = Phase, Dropped = dropped, Final = resolved.Final, Made = resolved.Made };
        }

        /// <summary>Puts a discovery on the rack; when the rack is full the weakest one already there makes room.</summary>
        void AddToRack(string id, List<string> dropped)
        {
            if (hand.Count(h => !IsStarter(h)) >= RackSize && id != db.Ultimate)
            {
                // the weakest discovery already held makes room (oldest first among equals)
                var weakest = hand.Where(h => !IsStarter(h)).OrderBy(h => db.Material(h).tier).First();
                hand.Remove(weakest); dropped?.Add(weakest);
            }
            hand.Add(id);
        }

        void BuildOffer()
        {
            var pool = db.Catalysts.Where(c => IsOpen(c.id) && catalysts.All(o => o.id != c.id)).ToList();
            rng.Shuffle(pool);
            offer = pool.Take(OfferSize).ToList();
        }

        /// <summary>Takes one of the offered catalysts (or null when none were left to offer) and descends.</summary>
        public void ChooseCatalyst(string id)
        {
            if (Phase != RunPhase.DepthCleared) throw new InvalidOperationException("no catalyst choice is open");
            var pick = offer.FirstOrDefault(c => c.id == id);
            if (pick == null && offer.Count > 0) throw new ArgumentException("not on offer: " + id);
            if (pick != null) catalysts.Add(pick);
            offer = new List<CatalystData>();
            carried = TrialsLeft;
            Depth++;
            BeginDepth();
        }
    }
}
