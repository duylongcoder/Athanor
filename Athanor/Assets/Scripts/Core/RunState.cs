using System;
using System.Collections.Generic;
using System.Linq;

namespace Athanor.Core
{
    public enum RunPhase { Playing, DepthCleared, Lost, Won }

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
    public sealed class RunState
    {
        public const int BaseHand = 5, BaseTrials = 4, BaseRedraws = 2, FinalDepth = 5, FinalTrials = 6, StarterCopies = 2, OfferSize = 3;
        /// <summary>How many discoveries the hand can keep; a new one pushes the weakest old one out.</summary>
        public const int RackSize = 12;

        readonly ContentDb db;
        readonly Journal journal;
        readonly Satchel satchel;
        readonly Rng rng;
        readonly List<string> hand = new List<string>();
        readonly List<CatalystData> catalysts = new List<CatalystData>();
        readonly HashSet<string> fired = new HashSet<string>();
        List<CatalystData> offer = new List<CatalystData>();
        int carried;   // trials left unspent at the last depth; they are added to the next

        public RunState(ContentDb db, Journal journal, int seed, int startDepth = 1)
        {
            this.db = db; this.journal = journal;
            rng = new Rng(seed ^ 0x5bd1e995);
            satchel = new Satchel(db.Starters.SelectMany(m => Enumerable.Repeat(m.id, StarterCopies)), seed);
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
        public int HandSize => BaseHand + (Has(Effects.ExtraHand) ? 1 : 0);
        public bool Diagonals => Has(Effects.Diagonals);
        public bool CanIgnite => Phase == RunPhase.Playing && IgniteResolver.HasAnyPair(Bench, Diagonals);
        /// <summary>Copies of a material in the hand or on the bench.</summary>
        public int Owned(string id) => hand.Count(h => h == id) + Bench.Tiles.Count(t => t == id);
        bool IsStarter(string id) => db.Material(id).tier == 0;
        public bool HasFired(string a, string b) => fired.Contains(ContentDb.PairKey(a, b));
        public bool HasFired(RecipeData recipe) => fired.Contains(db.KeyOf(recipe));
        public int SatchelSize => satchel.Total;

        bool Has(string effect) => IgniteResolver.Has(catalysts, effect);

        void BeginDepth()
        {
            Potency = 0;
            TrialsLeft = (Depth == FinalDepth ? FinalTrials : BaseTrials) + (Has(Effects.ExtraTrial) ? 1 : 0) + carried;
            carried = 0;
            RedrawsLeft = BaseRedraws;
            Phase = RunPhase.Playing;
            FillHand();
        }

        /// <summary>Draws starting materials until the hand holds its full count of them. Discoveries do not count.</summary>
        void FillHand()
        {
            int want = HandSize - hand.Count(IsStarter) - Bench.Tiles.Count(t => t != null && IsStarter(t));
            if (want > 0) hand.AddRange(satchel.Draw(want));
        }

        public bool Place(int handIndex, int tile)
        {
            if (Phase != RunPhase.Playing || handIndex < 0 || handIndex >= hand.Count) return false;
            if (tile < 0 || tile >= Bench.TileCount || Bench.Tiles[tile] != null) return false;
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
            RedrawsLeft--;
            satchel.Discard(hand.Where(IsStarter));
            hand.RemoveAll(IsStarter);
            FillHand();
            return true;
        }

        public IgniteOutcome Ignite()
        {
            if (!CanIgnite) throw new InvalidOperationException("nothing on the bench can react");
            var resolved = IgniteResolver.Resolve(Bench, db, journal, fired, catalysts);
            var events = resolved.Events;
            long gained = 0; bool madeUltimate = false;
            foreach (var e in events)
            {
                gained += e.Potency;
                if (e.Discovery) DiscoveriesThisRun++;
                if (e.Result != null && e.Result == db.Ultimate) madeUltimate = true;
            }
            // everything placed is spent: starting materials go back to the satchel, discoveries are used up.
            // Only what the chain made, and left standing, is kept.
            for (int t = 0; t < Bench.TileCount; t++)
                if (Bench.Tiles[t] != null && IsStarter(Bench.Tiles[t])) satchel.Discard(new[] { Bench.Tiles[t] });
            var made = new List<string>();
            for (int t = 0; t < Bench.TileCount; t++)
                if (resolved.Made[t] && resolved.Final[t] != null) made.Add(resolved.Final[t]);
            var dropped = new List<string>();
            foreach (var id in made)
            {
                if (hand.Count(h => !IsStarter(h)) >= RackSize && id != db.Ultimate)
                {
                    // the weakest discovery already held makes room (oldest first among equals)
                    var weakest = hand.Where(h => !IsStarter(h)).OrderBy(h => db.Material(h).tier).First();
                    hand.Remove(weakest); dropped.Add(weakest);
                }
                hand.Add(id);
            }
            Bench.Clear();
            Potency += gained;
            TrialsLeft--;

            if (madeUltimate) Phase = RunPhase.Won;
            else if (Depth < FinalDepth && Potency >= Target) { Phase = RunPhase.DepthCleared; BuildOffer(); }
            else if (TrialsLeft <= 0) Phase = RunPhase.Lost;
            else FillHand();
            return new IgniteOutcome { Events = events, Gained = gained, PhaseAfter = Phase, Dropped = dropped, Final = resolved.Final, Made = resolved.Made };
        }

        void BuildOffer()
        {
            var pool = db.Catalysts.Where(c => catalysts.All(o => o.id != c.id)).ToList();
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
