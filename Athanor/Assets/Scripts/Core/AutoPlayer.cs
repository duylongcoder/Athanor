using System.Collections.Generic;
using System.Linq;

namespace Athanor.Core
{
    /// <summary>
    /// Plays a run knowing every recipe and how a bench resolves: each trial it builds a layout one
    /// cluster (a recipe laid round a hub) or one card at a time, asking the chain planner what the
    /// bench would do, and keeps the best. Used to prove the content is winnable and as a debug autoplay.
    /// </summary>
    public enum AutoMove { None, ChoseCatalyst, Redrew, ReadyToIgnite, ChoseAspect, ChoseEvent }

    public sealed class AutoPlayer
    {
        static readonly string[] CatalystOrder =
        {
            Effects.AllScale, Effects.ExtraHand, Effects.ChainPlus, Effects.FirstTriple, Effects.DepthScale, Effects.CentreTwice, Effects.ExtraTrial,
            Effects.SecondChance, Effects.PairBonus, Effects.TierDouble, Effects.ComboBonus, Effects.ChainFrom, Effects.KindDouble,
            Effects.StarterDouble, Effects.Diagonals, Effects.HeatPlus, Effects.FlatPerReaction, Effects.EmptyTileBonus, Effects.ExtraRedraw,
            Effects.FreeDiscovery, Effects.Salvage, Effects.DiscoveryTriple,
        };
        static readonly string[] AspectOrder =
        {
            AspectEffects.Kiln, AspectEffects.Heated, AspectEffects.Doubled, AspectEffects.Echo, AspectEffects.Heavy, AspectEffects.Anchor,
            AspectEffects.Greedy, AspectEffects.Attuned, AspectEffects.Seed, AspectEffects.Fallow, AspectEffects.Crossroads, AspectEffects.Bridge,
            AspectEffects.OpenBench, AspectEffects.Pivot, AspectEffects.Volatile, AspectEffects.Cold,
        };
        /// <summary>What a hint is worth to this player when weighing an event: nothing to one who knows every recipe.</summary>
        public double HintValue = 0;
        /// <summary>Redraw when no layout makes anything this player wants. Off for a player with no route to want yet.</summary>
        public bool RedrawWhenIdle = true;
        /// <summary>For a player who does not know the whole tree: with no route to the last substance, work toward the highest materials it can make.</summary>
        public bool SeekFrontier = false;
        /// <summary>Materials this player is working toward while it knows no route to the last substance (material, how many).</summary>
        public Dictionary<string, int> Wanted;
        /// <summary>This player knows how to make the material from starting materials (or it is one).</summary>
        public bool CanMake(string id) => tier.TryGetValue(id, out int t) && (t == 0 || route.ContainsKey(id));
        /// <summary>Reactions it takes to make one from starting materials.</summary>
        public int CostOf(string id) => cost.TryGetValue(id, out int c) ? c : 0;
        public bool KnowsTheWay => route.ContainsKey(db.Ultimate);
        /// <summary>How many of a material the last plan wanted in all, as an end or as an ingredient.</summary>
        public int Demand(string id) => Count(demand, id);

        readonly ContentDb db;
        readonly Dictionary<string, int> cost = new Dictionary<string, int>();      // reactions needed to make one from starters
        readonly Dictionary<string, RecipeData> route = new Dictionary<string, RecipeData>();   // the recipe this player uses for each material
        readonly Dictionary<string, int> tier = new Dictionary<string, int>();
        readonly List<string> byTierDown;

        public AutoPlayer(ContentDb db)
        {
            this.db = db;
            foreach (var m in db.Materials) { tier[m.id] = m.tier; cost[m.id] = 0; }
            foreach (var m in db.Materials.OrderBy(m => m.tier))
                foreach (var r in db.Recipes.Where(r => r.result == m.id))
                {
                    int c = 1 + r.items.Sum(i => cost[i]);
                    if (!route.ContainsKey(m.id) || c < cost[m.id]) { route[m.id] = r; cost[m.id] = c; }
                }
            byTierDown = db.Materials.OrderByDescending(m => m.tier).Select(m => m.id).ToList();
        }

        // ---- one decision -------------------------------------------------------------------------

        /// <summary>One whole decision, including pressing Ignite.</summary>
        public void Step(RunState run)
        {
            if (Decide(run) == AutoMove.ReadyToIgnite) run.Ignite();
        }

        /// <summary>Picks a catalyst, or redraws, or lays the bench out and leaves the Ignite to the caller.</summary>
        public AutoMove Decide(RunState run)
        {
            if (run.Phase == RunPhase.DepthCleared)
            {
                var pick = run.CatalystOffer.OrderBy(c => System.Array.IndexOf(CatalystOrder, c.effect)).FirstOrDefault();
                run.ChooseCatalyst(pick?.id);
                return AutoMove.ChoseCatalyst;
            }
            if (run.Phase == RunPhase.AspectOffer)
            {
                var pick = run.AspectOffer.OrderBy(a => System.Array.IndexOf(AspectOrder, a.Data.effect)).First();
                run.ChooseAspect(pick.Data.id);
                return AutoMove.ChoseAspect;
            }
            if (run.Phase == RunPhase.EventOffer)
            {
                int bestOption = 0; double bestWorth = double.NegativeInfinity;
                for (int i = 0; i < run.EventOptions.Count; i++)
                {
                    double w = Worth(run.EventOptions[i]);
                    if (w > bestWorth) { bestWorth = w; bestOption = i; }
                }
                run.ChooseEventOption(bestOption);
                return AutoMove.ChoseEvent;
            }
            if (run.Phase != RunPhase.Playing) return AutoMove.None;
            for (int t = 0; t < Bench.TileCount; t++) run.Unplace(t);

            Prepare(run);
            Search();
            if ((best == null || bestUseful == 0) && RedrawWhenIdle && run.Redraw()) return AutoMove.Redrew;
            if (best == null) return Force(run);

            for (int t = 0; t < Bench.TileCount; t++)
                if (best[t] != null) run.Place(run.Hand.ToList().IndexOf(best[t]), t);
            return run.CanIgnite ? AutoMove.ReadyToIgnite : Force(run);
        }

        /// <summary>A rough worth of an event option: what it gives less what it costs, averaged over its outcomes.</summary>
        double Worth(EventOption o)
        {
            double w = 0;
            foreach (var c in o.cost ?? new EffectData[0]) w += Worth(c);
            double total = o.outcomes.Sum(x => x.weight), gain = 0;
            foreach (var x in o.outcomes)
                foreach (var g in x.gain ?? new EffectData[0]) gain += Worth(g) * x.weight / total;
            return w + gain;
        }

        double Worth(EffectData fx)
        {
            switch (fx.op)
            {
                case Ops.Trials: return 3.0 * fx.amount;
                case Ops.Hand: return 1.5 * fx.amount;
                case Ops.Redraws: return 0.5 * fx.amount;
                case Ops.RiShift: return 2.0 * fx.amount;
                case Ops.GiveDiscovery: return 2;
                case Ops.LoseRack: return -1.5 * System.Math.Max(1, fx.amount);
                case Ops.LoseCard: return -0.5 * System.Math.Max(1, fx.amount);
                case Ops.FirstTriple: return 2;
                case Ops.Hint: return HintValue;
                case Ops.BlockTile: return -2;
                case Ops.HeatTile: return 1;
                case Ops.CatalystUpgrade: return 3;
                case Ops.CatalystGift: return 4;
                default: return 0;
            }
        }

        /// <summary>Nothing worth doing: spend the trial on two starting materials, never on a discovery.</summary>
        AutoMove Force(RunState run)
        {
            for (int t = 0; t < Bench.TileCount; t++) run.Unplace(t);
            // two tiles that touch and are not shut
            int first = -1, second = -1;
            for (int a = 0; a < Bench.TileCount && first < 0; a++)
                for (int b = a + 1; b < Bench.TileCount; b++)
                    if (!run.Rules.Blocked[a] && !run.Rules.Blocked[b] && run.Rules.Touch(a, b)) { first = a; second = b; break; }
            foreach (int tile in new[] { first, second })
            {
                int i = run.Hand.ToList().FindIndex(h => tier[h] == 0);
                if (i < 0 || tile < 0) return run.Redraw() ? AutoMove.Redrew : AutoMove.None;
                run.Place(i, tile);
            }
            return run.CanIgnite ? AutoMove.ReadyToIgnite : AutoMove.None;
        }

        // ---- search state -------------------------------------------------------------------------

        readonly Dictionary<string, int> inHand = new Dictionary<string, int>();     // unplaced cards by material
        readonly Dictionary<string, int> held = new Dictionary<string, int>();       // the whole hand by material
        readonly Dictionary<string, int> need = new Dictionary<string, int>(), demand = new Dictionary<string, int>();
        readonly string[] tiles = new string[Bench.TileCount];
        readonly List<string> usedNeed = new List<string>();
        readonly HashSet<string> firedLocal = new HashSet<string>();
        readonly List<int> freeAround = new List<int>();
        RunState run;
        IReadOnlyList<CatalystData> cats;
        BenchRules rules;
        ScoreContext ctx;
        long remaining;
        int trialsLeft, discoveriesHeld, bestUseful, lastUseful;
        string[] best;
        double bestScore;

        void Prepare(RunState r)
        {
            run = r; cats = r.Catalysts; rules = r.Rules; ctx = r.NewContext(); ctx.EmptyAtIgnite = new bool[Bench.TileCount];
            inHand.Clear(); held.Clear();
            foreach (var c in r.Hand) { inHand[c] = Count(inHand, c) + 1; held[c] = Count(held, c) + 1; }
            discoveriesHeld = r.Hand.Count(c => tier[c] > 0);
            ComputeNeeds();
            trialsLeft = r.TrialsLeft;
            remaining = r.Depth < db.FinalDepth ? System.Math.Max(0, r.Target - r.Potency) : 0;
            for (int t = 0; t < tiles.Length; t++) tiles[t] = null;
            best = null; bestScore = double.NegativeInfinity; bestUseful = 0;
        }

        static int Count(Dictionary<string, int> d, string k) => d.TryGetValue(k, out int v) ? v : 0;

        /// <summary>need: how many more of each material must be made; demand: how many are wanted in all.</summary>
        void ComputeNeeds()
        {
            need.Clear(); demand.Clear();
            if (Wanted != null && Wanted.Count > 0 && !route.ContainsKey(db.Ultimate))
            {
                foreach (var w in Wanted) demand[w.Key] = w.Value;
            }
            else if (SeekFrontier && !route.ContainsKey(db.Ultimate))
            {
                // no known way to the last substance yet: want one of each of the highest things this player can make
                int top = 0;
                foreach (var m in route.Keys) if (tier[m] > top) top = tier[m];
                foreach (var m in route.Keys) if (tier[m] >= top - 1) demand[m] = 1;
            }
            else demand[db.Ultimate] = 1;
            foreach (var m in byTierDown)
            {
                int want = Count(demand, m);
                if (want <= 0 || !route.TryGetValue(m, out var r)) continue;
                int make = want - Count(held, m);
                if (make <= 0) continue;
                need[m] = make;
                foreach (var i in r.items) demand[i] = Count(demand, i) + make;
            }
        }

        void Put(string id, int t) { tiles[t] = id; inHand[id]--; }
        void Take(int t) { inHand[tiles[t]]++; tiles[t] = null; }

        struct Move { public string[] ids; public int[] at; public double score; }

        /// <summary>Grows the layout greedily: each round, the single best cluster or card to add, until nothing helps.</summary>
        void Search()
        {
            double current = double.NegativeInfinity;
            while (true)
            {
                var next = new Move { score = current };
                // a recipe laid round a hub
                foreach (var recipe in db.Recipes)
                {
                    if (!Available(recipe)) continue;
                    foreach (var hubItem in recipe.items.Distinct())
                    {
                        var others = Without(recipe.items, hubItem);
                        for (int hub = 0; hub < tiles.Length; hub++)
                        {
                            if (tiles[hub] != null || rules.Blocked[hub]) continue;
                            freeAround.Clear();
                            foreach (int n in rules.Around(hub)) if (tiles[n] == null) freeAround.Add(n);
                            if (freeAround.Count < others.Length) continue;
                            for (int variant = 0; variant < 2; variant++)
                            {
                                int skip = variant == 0 ? 0 : freeAround.Count - others.Length;
                                if (variant == 1 && skip == 0) break;
                                var ids = new string[recipe.items.Length]; var at = new int[recipe.items.Length];
                                ids[0] = hubItem; at[0] = hub;
                                for (int k = 0; k < others.Length; k++) { ids[k + 1] = others[k]; at[k + 1] = freeAround[skip + k]; }
                                Try(ids, at, ref next);
                            }
                            // the same cluster with starting materials beside the hub, where the recipe needs company
                            int company = db.Company(recipe), room = freeAround.Count - others.Length;
                            if (company <= 0 || room <= 0) continue;
                            foreach (var extra in CompanyFor(recipe, company, room))
                            {
                                var ids = new string[recipe.items.Length + extra.Length]; var at = new int[ids.Length];
                                ids[0] = hubItem; at[0] = hub;
                                for (int k = 0; k < others.Length; k++) { ids[k + 1] = others[k]; at[k + 1] = freeAround[k]; }
                                for (int k = 0; k < extra.Length; k++) { ids[others.Length + 1 + k] = extra[k]; at[others.Length + 1 + k] = freeAround[others.Length + k]; }
                                Try(ids, at, ref next);
                            }
                        }
                    }
                }
                // one more card anywhere
                foreach (var id in inHand.Keys.ToList())
                {
                    if (inHand[id] <= 0) continue;
                    for (int t = 0; t < tiles.Length; t++)
                        if (tiles[t] == null && !rules.Blocked[t]) Try(new[] { id }, new[] { t }, ref next);
                }
                if (next.ids == null) break;
                for (int k = 0; k < next.ids.Length; k++) Put(next.ids[k], next.at[k]);
                current = next.score;
            }
            if (!double.IsNegativeInfinity(current))
            {
                bestScore = current; Evaluate(); bestUseful = lastUseful; best = (string[])tiles.Clone();
            }
            for (int t = 0; t < tiles.Length; t++) if (tiles[t] != null) Take(t);
        }

        void Try(string[] ids, int[] at, ref Move next)
        {
            for (int k = 0; k < ids.Length; k++) Put(ids[k], at[k]);
            double s = Evaluate();
            for (int k = 0; k < ids.Length; k++) Take(at[k]);
            if (s > next.score) next = new Move { ids = ids, at = at, score = s };
        }

        readonly List<string> spare = new List<string>();

        /// <summary>
        /// Every handful of starting materials still in the hand, beyond what the recipe itself takes, that
        /// fits in the room beside the hub and brings enough Reaction Index, with no card more than needed.
        /// </summary>
        List<string[]> CompanyFor(RecipeData recipe, int company, int room)
        {
            spare.Clear();
            foreach (var pair in inHand)
            {
                if (tier[pair.Key] != 0) continue;
                int free = pair.Value;
                foreach (var i in recipe.items) if (i == pair.Key) free--;
                for (int k = 0; k < free; k++) spare.Add(pair.Key);
            }
            spare.Sort((x, y) => db.Reactivity(y).CompareTo(db.Reactivity(x)));
            var found = new List<string[]>();
            var picked = new List<string>();
            Pick(0, company, room, picked, found);
            return found;
        }

        void Pick(int from, int lacking, int room, List<string> picked, List<string[]> found)
        {
            if (lacking <= 0) { found.Add(picked.ToArray()); return; }   // enough: one card more would be waste
            if (room == 0) return;
            for (int k = from; k < spare.Count; k++)
            {
                if (k > from && spare[k] == spare[k - 1]) continue;   // the same handful again
                picked.Add(spare[k]);
                Pick(k + 1, lacking - db.Reactivity(spare[k]), room - 1, picked, found);
                picked.RemoveAt(picked.Count - 1);
            }
        }

        bool Available(RecipeData recipe)
        {
            foreach (var i in recipe.items)
            {
                int wanted = 0;
                foreach (var j in recipe.items) if (j == i) wanted++;
                if (Count(inHand, i) < wanted) return false;
            }
            return true;
        }

        static string[] Without(string[] items, string one)
        {
            var list = items.ToList();
            list.Remove(one);
            return list.ToArray();
        }

        bool AnyPair()
        {
            for (int t = 0; t < tiles.Length; t++)
            {
                if (tiles[t] == null) continue;
                foreach (int n in rules.Around(t)) if (tiles[n] != null) return true;
            }
            return false;
        }

        /// <summary>How good the bench as laid out is; negative infinity when it could not be ignited.</summary>
        double Evaluate()
        {
            lastUseful = 0;
            if (!AnyPair()) return double.NegativeInfinity;
            int empty = 0, chain = 0, useful = 0;
            for (int t = 0; t < tiles.Length; t++) { ctx.EmptyAtIgnite[t] = tiles[t] == null; if (tiles[t] == null) empty++; }
            ctx.EmptyTiles = empty;
            var plan = ChainPlanner.Plan(tiles, db, rules, false);
            long gain = 0; bool won = false;
            double score = 0;
            firedLocal.Clear(); usedNeed.Clear();
            foreach (var step in plan.Steps)
            {
                chain++;
                string key = db.KeyOf(step.Recipe);
                bool first = !run.HasFired(step.Recipe) && firedLocal.Add(key);
                gain += IgniteResolver.Score(db, step.Recipe, step.Tiles, first, false, chain, cats, ctx);
                if (step.Recipe.result == db.Ultimate) won = true;
            }
            // what the chain leaves standing is kept: worth the reactions it took, if it is wanted
            int kept = 0;
            for (int t = 0; t < tiles.Length; t++)
            {
                if (!plan.Made[t]) continue;
                string m = plan.Final[t];
                kept++;
                if (Count(need, m) > 0)
                {
                    need[m]--; usedNeed.Add(m); useful++;
                    score += 2000.0 * cost[m] + 300 * tier[m];
                }
            }
            foreach (var m in usedNeed) need[m]++;
            // every discovery laid down is spent, whatever happens to it
            int spent = 0;
            foreach (var pair in held)
            {
                string c = pair.Key;
                if (tier[c] == 0) continue;
                int placed = pair.Value - Count(inHand, c);
                if (placed <= 0) continue;
                spent += placed;
                int short_ = System.Math.Min(placed, System.Math.Max(0, Count(demand, c) - Count(inHand, c)));
                score -= short_ * (2000.0 * cost[c] + 300 * tier[c]);
            }
            lastUseful = useful;
            if (won) score += 1e9;
            // a full rack pushes the weakest out
            int over = discoveriesHeld - spent + kept - db.Rules.rackSize;
            if (over > 0) score -= 3000.0 * over;
            // potency toward the depth target
            if (remaining > 0)
            {
                if (gain >= remaining) score += 6000;
                else if (trialsLeft <= 1) score += gain - 1e6;   // this would end the run
                else score += 2000.0 * gain / ((double)remaining / trialsLeft);
            }
            // starting materials left in hand: worth keeping only with a partner to react with
            foreach (var pair in inHand)
                if (pair.Value > 0 && tier[pair.Key] == 0) score += pair.Value * (HasPartnerInHand(pair.Key) ? 30 : -160);
            return score;
        }

        /// <summary>Some wanted recipe uses this card together with another card still in the hand.</summary>
        bool HasPartnerInHand(string id)
        {
            foreach (var r in db.RecipesContaining(id))
            {
                if (Count(need, r.result) <= 0) continue;
                foreach (var other in r.items)
                    if (other != id ? Count(inHand, other) > 0 : Count(inHand, other) > 1) return true;
            }
            return false;
        }
    }
}
