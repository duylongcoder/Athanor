using System;
using System.Collections.Generic;
using System.Linq;

namespace Athanor.Gameplay
{
    /// <summary>An aspect as held by a run (or as offered): where it sits and, for an attuned tile, which kind it favours.</summary>
    public sealed class AspectInstance
    {
        public AspectData Data;
        public int[] Tiles = new int[0];   // none for a bench-wide aspect, two for a bridge
        public string Kind;
    }

    /// <summary>What taking an event option did, for the screen.</summary>
    public sealed class EventResult
    {
        public string Text;
        public readonly List<EventNote> Notes = new List<EventNote>();
    }

    /// <summary>One thing an event option did, as a lang key (note.*) and its arguments; the Game layer words it.</summary>
    [Serializable]
    public sealed class EventNote
    {
        public string key;
        public int n;
        public string item, other, kind;
        public bool plural;   // the wording has a one/other form for n
    }

    // The start of a depth: aspects (run-long bench modifiers) and events (one-depth happenings).
    public sealed partial class RunState
    {
        readonly RunOptions options;
        readonly Rng aspectRng, eventRng;
        readonly List<AspectInstance> aspects = new List<AspectInstance>();
        List<AspectInstance> aspectOffer = new List<AspectInstance>();
        List<EventOption> eventOptions = new List<EventOption>();
        readonly List<int> depthBlocked = new List<int>();
        readonly List<(int tile, int bonus)> depthHeated = new List<(int, int)>();
        int handDelta, riShift, fallowPending;
        bool tripleFirst, freeDiscoveryUsed, secondChanceUsed, aspectPending, eventPending;
        List<string> reachable;
        string lastLore;

        /// <summary>The last Lore event seen: this run's latest, or the one the run started with.</summary>
        public string LastLore => lastLore ?? options.LastLore;

        public IReadOnlyList<AspectInstance> Aspects => aspects;
        public IReadOnlyList<AspectInstance> AspectOffer => aspectOffer;
        public EventData CurrentEvent { get; private set; }
        /// <summary>The options of the current event that can be taken now.</summary>
        public IReadOnlyList<EventOption> EventOptions => eventOptions;
        public BenchRules Rules { get; private set; } = BenchRules.None;
        /// <summary>This depth only: how much every material's Reaction Index is shifted, and whether the next first reaction triples.</summary>
        public int RiShift => riShift;
        public bool TripleFirst => tripleFirst;
        /// <summary>This depth only: how many cards more or fewer each hand holds.</summary>
        public int HandDelta => handDelta;

        bool HasAspect(string effect) => aspects.Any(a => a.Data.effect == effect);
        bool HasAspectOn(string effect, int tile) => aspects.Any(a => a.Data.effect == effect && a.Tiles.Length > 0 && a.Tiles[0] == tile);

        void RebuildRules()
        {
            var r = new BenchRules { Diagonals = Has(Effects.Diagonals), HeatPlus = Has(Effects.HeatPlus) ? Sum(Effects.HeatPlus) : 0, RiShift = riShift };
            foreach (var a in aspects)
            {
                int t = a.Tiles.Length > 0 ? a.Tiles[0] : -1;
                switch (a.Data.effect)
                {
                    case AspectEffects.Doubled: r.HubMult[t] *= 2; break;
                    case AspectEffects.Volatile: r.HubMult[t] *= 3; break;
                    case AspectEffects.Heated: r.RiBonus[t] += (int)a.Data.amount; break;
                    case AspectEffects.Attuned: r.AttunedKind[t] = a.Kind; break;
                    case AspectEffects.Seed: r.SeedBonus[t] = (int)a.Data.amount; break;
                    case AspectEffects.Heavy: r.Heavy[t] = true; break;
                    case AspectEffects.Cold: r.Blocked[t] = true; break;
                    case AspectEffects.Bridge: r.Links.Add(new LinkPair(a.Tiles[0], a.Tiles[1])); break;
                    case AspectEffects.OpenBench: r.Links.Add(new LinkPair(t, Bench.Centre)); break;
                    case AspectEffects.Kiln: r.Kiln[t] = true; break;
                    case AspectEffects.Crossroads: r.Crossroads[t] = true; break;
                    case AspectEffects.Pivot: r.Pivot = t; break;
                }
            }
            foreach (int t in depthBlocked) r.Blocked[t] = true;
            foreach (var (t, bonus) in depthHeated) r.RiBonus[t] += bonus;
            Rules = r;
        }

        // ---- aspects ------------------------------------------------------------------------------

        bool TileTaken(int t) => aspects.Any(a => a.Tiles.Contains(t)) || depthBlocked.Contains(t);

        /// <summary>Shape: never shut more tiles than this, so the bench stays playable.</summary>
        const int MaxBlocked = 2;
        /// <summary>Shape: an option that costs a trial needs one to spend and one left to play.</summary>
        const int MinTrialsToSpend = 2;

        void BuildAspectOffer()
        {
            aspectOffer = new List<AspectInstance>();
            var pool = db.Aspects.Where(a => aspects.All(h => h.Data.id != a.id)).ToList();
            aspectRng.Shuffle(pool);
            foreach (var data in pool)
            {
                var placed = PlaceAspect(data);
                if (placed != null) aspectOffer.Add(placed);
                if (aspectOffer.Count == db.Rules.offerSize) break;
            }
        }

        /// <summary>Finds room for an aspect on the bench, or null. The tile is fixed here so the offer can show it.</summary>
        AspectInstance PlaceAspect(AspectData data)
        {
            var inst = new AspectInstance { Data = data };
            if (data.scope == "Bench") return inst;
            var free = Enumerable.Range(0, Bench.TileCount).Where(t => !TileTaken(t)).ToList();
            if (data.effect == AspectEffects.Bridge)
            {
                var pairs = new List<(int, int)>();
                foreach (int a in free)
                    foreach (int b in free)
                        if (a < b && !BenchRules.None.Touch(a, b)) pairs.Add((a, b));
                if (pairs.Count == 0) return null;
                var (x, y) = pairs[aspectRng.Range(pairs.Count)];
                inst.Tiles = new[] { x, y };
                return inst;
            }
            if (data.effect == AspectEffects.OpenBench) free = free.Where(t => t == 0 || t == 2 || t == 6 || t == 8).ToList();
            if (data.effect == AspectEffects.Cold && CountBlocked() >= MaxBlocked) return null;   // never shut the bench down
            if (free.Count == 0) return null;
            inst.Tiles = new[] { free[aspectRng.Range(free.Count)] };
            if (data.effect == AspectEffects.Attuned)
            {
                var kinds = (Kind[])Enum.GetValues(typeof(Kind));
                inst.Kind = kinds[aspectRng.Range(kinds.Length)].ToString();
            }
            return inst;
        }

        int CountBlocked() => aspects.Count(a => a.Data.effect == AspectEffects.Cold) + depthBlocked.Count;

        /// <summary>Takes one of the offered aspects for the rest of the run and moves on to the event or the bench.</summary>
        public void ChooseAspect(string id)
        {
            if (Phase != RunPhase.AspectOffer) throw new InvalidOperationException("no aspect choice is open");
            var pick = aspectOffer.FirstOrDefault(a => a.Data.id == id);
            if (pick == null) throw new ArgumentException("not on offer: " + id);
            aspects.Add(pick);
            aspectOffer = new List<AspectInstance>();
            if (pick.Data.effect == AspectEffects.Cold) TrialsLeft++;
            if (pick.Data.effect == AspectEffects.Greedy) RedrawsLeft = Math.Max(0, RedrawsLeft - 1);
            RebuildRules();
            Advance();
        }

        // ---- events -------------------------------------------------------------------------------

        static bool IsWalkOn(EventOption o) =>
            string.IsNullOrEmpty(o.requires) && (o.cost == null || o.cost.Length == 0)
            && o.outcomes.All(x => x.gain == null || x.gain.All(g => g.op == Ops.Nothing));

        bool CanTake(EventOption o)
        {
            int lose = 0, trials = 0, redraws = 0;
            foreach (var c in o.cost ?? new EffectData[0])
            {
                if (c.op == Ops.LoseRack) lose += Math.Max(1, c.amount);
                if (c.op == Ops.Trials) trials += c.amount;
                if (c.op == Ops.Redraws) redraws += c.amount;
            }
            if (hand.Count(h => !IsStarter(h)) < lose) return false;
            if (TrialsLeft + trials < 1 || RedrawsLeft + redraws < 0) return false;
            switch (o.requires)
            {
                case Ops.NeedRack: return hand.Any(h => !IsStarter(h));
                case Ops.NeedCatalyst: return Upgrades().Count > 0;
                case Ops.NeedTrial: return TrialsLeft >= MinTrialsToSpend;
                case Ops.NeedRedraw: return RedrawsLeft >= 1;
                default: return true;
            }
        }

        void DrawEvent()
        {
            CurrentEvent = null; eventOptions = new List<EventOption>();
            var pool = new List<EventData>();
            foreach (var e in db.Events)
            {
                if (Depth < e.minDepth || Depth > e.maxDepth) continue;
                // worth showing only when there is something to do besides walking on (or it is all there is)
                if (!e.options.Any(CanTake)) continue;
                if (e.options.Length == 1 || e.options.Any(o => !IsWalkOn(o) && CanTake(o))) pool.Add(e);
            }
            if (options.ForceEvent != null)
            {
                // a balance check asked for this event: it comes wherever it may, and nothing else does
                if (options.ForceEventAt == 0 || options.ForceEventAt == Depth) CurrentEvent = pool.FirstOrDefault(e => e.id == options.ForceEvent);
                if (CurrentEvent != null) eventOptions = CurrentEvent.options.Where(CanTake).ToList();
                return;
            }
            int total = Math.Max(0, db.NoEventWeight) + pool.Sum(e => e.weight);
            if (total <= 0) return;
            int roll = eventRng.Range(total) - Math.Max(0, db.NoEventWeight);
            if (roll < 0) return;
            foreach (var e in pool)
            {
                if (roll < e.weight) { CurrentEvent = e; break; }
                roll -= e.weight;
            }
            if (CurrentEvent == null) return;
            if (CurrentEvent.type == "Lore" && CurrentEvent.id == LastLore)
            {
                // seen last time: no event. Roll the outcome dice as walking past it would have, so every later draw is
                // the same as if it had been shown (balance-neutral; a Lore event does nothing).
                var walk = CurrentEvent.options.FirstOrDefault(CanTake);
                if (walk != null) eventRng.Range(walk.outcomes.Sum(x => x.weight));
                CurrentEvent = null;
                return;
            }
            if (CurrentEvent.type == "Lore") lastLore = CurrentEvent.id;
            eventOptions = CurrentEvent.options.Where(CanTake).ToList();
        }

        /// <summary>Takes an option of the current event (an index into EventOptions), applies it and opens the bench.</summary>
        public EventResult ChooseEventOption(int index)
        {
            if (Phase != RunPhase.EventOffer) throw new InvalidOperationException("no event is open");
            if (index < 0 || index >= eventOptions.Count) throw new ArgumentException("no such option: " + index);
            var option = eventOptions[index];
            var result = new EventResult();
            foreach (var c in option.cost ?? new EffectData[0]) Apply(c, result);
            int roll = eventRng.Range(option.outcomes.Sum(x => x.weight));
            var outcome = option.outcomes[option.outcomes.Length - 1];
            foreach (var x in option.outcomes)
            {
                if (roll < x.weight) { outcome = x; break; }
                roll -= x.weight;
            }
            result.Text = outcome.text;
            foreach (var g in outcome.gain ?? new EffectData[0]) Apply(g, result);
            CurrentEvent = null; eventOptions = new List<EventOption>();
            TrimHand();
            RebuildRules();
            Advance();
            return result;
        }

        /// <summary>The hand was dealt before the event: when the event made it smaller, the last cards drawn go back.</summary>
        void TrimHand()
        {
            for (int extra = hand.Count(IsMain) - HandSize; extra > 0; extra--)
            {
                int i = hand.FindLastIndex(IsMain);
                satchel.Discard(new[] { hand[i] });
                hand.RemoveAt(i);
            }
        }

        /// <summary>Discoveries that can be made from the starting materials this run has.</summary>
        List<string> Reachable()
        {
            if (reachable != null) return reachable;
            var have = new HashSet<string>(db.Starters.Where(m => IsOpen(m.id)).Select(m => m.id));
            for (bool grew = true; grew;)
            {
                grew = false;
                foreach (var r in db.Recipes)
                    if (!have.Contains(r.result) && r.items.All(have.Contains)) { have.Add(r.result); grew = true; }
            }
            return reachable = db.Materials.Where(m => m.tier > 0 && m.id != db.Ultimate && have.Contains(m.id)).Select(m => m.id).ToList();
        }

        /// <summary>Held catalysts that could be traded up, each with the open, unheld catalysts one rarity above it.</summary>
        List<(CatalystData held, List<CatalystData> better)> Upgrades()
        {
            var found = new List<(CatalystData, List<CatalystData>)>();
            foreach (var c in catalysts)
            {
                int next = Rarities.Rank(c.rarity) + 1;
                var better = db.Catalysts.Where(x => IsOpen(x.id) && Rarities.Rank(x.rarity) == next && catalysts.All(h => h.id != x.id)).ToList();
                if (better.Count > 0) found.Add((c, better));
            }
            return found;
        }

        void Apply(EffectData fx, EventResult result)
        {
            int n = fx.amount;
            switch (fx.op)
            {
                case Ops.Trials:
                    TrialsLeft = Math.Max(1, TrialsLeft + n);
                    result.Notes.Add(new EventNote { key = "note.trials", n = n, plural = true });
                    break;
                case Ops.Hand:
                    handDelta += n;
                    result.Notes.Add(new EventNote { key = "note.hand", n = n, plural = true });
                    break;
                case Ops.Redraws:
                    RedrawsLeft = Math.Max(0, RedrawsLeft + n);
                    result.Notes.Add(new EventNote { key = "note.redraws", n = n, plural = true });
                    break;
                case Ops.RiShift:
                    riShift += n;
                    result.Notes.Add(new EventNote { key = "note.ri_shift", n = n });
                    break;
                case Ops.GiveDiscovery:
                {
                    var pool = Reachable().Where(m => db.Material(m).tier >= n && db.Material(m).tier <= Math.Max(n, fx.amount2)).ToList();
                    if (pool.Count == 0) break;
                    string id = pool[eventRng.Range(pool.Count)];
                    AddToRack(id, null);
                    result.Notes.Add(new EventNote { key = "note.found", item = db.Material(id).name });
                    break;
                }
                case Ops.LoseRack:
                    for (int k = 0; k < Math.Max(1, n); k++)
                    {
                        var rack = hand.Where(h => !IsStarter(h)).ToList();
                        if (rack.Count == 0) break;
                        // the offering is the least of what is held
                        string id = rack.OrderBy(h => db.Material(h).tier).First();
                        hand.Remove(id);
                        result.Notes.Add(new EventNote { key = "note.gave_up", item = db.Material(id).name });
                    }
                    break;
                case Ops.LoseCard:
                    for (int k = 0; k < Math.Max(1, n); k++)
                    {
                        var cards = hand.Where(IsMain).ToList();
                        if (cards.Count == 0 || satchel.Total <= MinSatchel) break;   // never thin the satchel below a hand
                        string id = cards[eventRng.Range(cards.Count)];
                        hand.Remove(id); satchel.Lose(id);
                        result.Notes.Add(new EventNote { key = "note.lost", item = db.Material(id).name });
                    }
                    break;
                case Ops.FirstTriple:
                    tripleFirst = true;
                    result.Notes.Add(new EventNote { key = "note.first_triple" });
                    break;
                case Ops.Hint:
                {
                    var open = Reachable().Concat(db.Starters.Where(m => IsOpen(m.id)).Select(m => m.id))
                        .Where(m => !journal.IsComplete(m, db)).ToList();
                    eventRng.Shuffle(open);
                    foreach (var m in open)
                    {
                        bool told = false;
                        foreach (var recipe in db.RecipesContaining(m).Reverse())
                        {
                            if (journal.IsKnown(recipe.items)) continue;
                            var partner = recipe.items.FirstOrDefault(i => i != m) ?? m;
                            if (!journal.AddHint(m, db.KindOf(partner))) continue;
                            result.Notes.Add(new EventNote { key = "note.hint", item = db.Material(m).name, kind = db.KindOf(partner).ToString() });
                            told = true; break;
                        }
                        if (told) break;
                    }
                    break;
                }
                case Ops.BlockTile:
                {
                    if (CountBlocked() >= MaxBlocked) break;
                    // Never the centre: it is the one tile with four neighbours, and the deep recipes cannot be laid without it.
                    // A corner is twice as likely as an edge: an edge shut takes a neighbour from the centre, and at depth 4
                    // that alone cost the informed player a third of its wins (EventReport).
                    var free = Enumerable.Range(0, Bench.TileCount).Where(t => t != Bench.Centre && !TileTaken(t))
                        .SelectMany(t => Enumerable.Repeat(t, t % 2 == 0 ? 2 : 1)).ToList();
                    if (free.Count == 0) break;
                    int t = free[eventRng.Range(free.Count)];
                    depthBlocked.Add(t);
                    result.Notes.Add(new EventNote { key = "note.block_tile" });
                    break;
                }
                case Ops.HeatTile:
                {
                    var free = Enumerable.Range(0, Bench.TileCount).Where(t => !TileTaken(t) && depthHeated.All(h => h.tile != t)).ToList();
                    if (free.Count == 0) break;
                    int t = free[eventRng.Range(free.Count)];
                    depthHeated.Add((t, n));
                    result.Notes.Add(new EventNote { key = "note.heat_tile", n = n });
                    break;
                }
                case Ops.CatalystUpgrade:
                {
                    var ups = Upgrades();
                    if (ups.Count == 0) break;
                    var (held, better) = ups[eventRng.Range(ups.Count)];
                    var got = better[eventRng.Range(better.Count)];
                    catalysts[catalysts.FindIndex(c => c.id == held.id)] = got;
                    result.Notes.Add(new EventNote { key = "note.became", item = held.name, other = got.name });
                    break;
                }
                case Ops.CatalystGift:
                {
                    var pool = db.Catalysts.Where(c => IsOpen(c.id) && catalysts.All(h => h.id != c.id)).ToList();
                    if (pool.Count == 0) break;
                    var got = db.PickCatalyst(pool, eventRng);
                    catalysts.Add(got);
                    result.Notes.Add(new EventNote { key = "note.received", item = got.name });
                    break;
                }
            }
        }

        /// <summary>Tiles shut or warmed by this depth's event, for the screen.</summary>
        public IReadOnlyList<int> DepthBlocked => depthBlocked;
        public IEnumerable<int> DepthHeated => depthHeated.Select(h => h.tile);
    }
}
