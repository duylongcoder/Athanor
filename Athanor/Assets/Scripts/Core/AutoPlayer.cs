using System.Collections.Generic;
using System.Linq;

namespace Athanor.Core
{
    /// <summary>
    /// Plays a run knowing every recipe and how a bench resolves: each trial it builds a layout one
    /// cluster (a recipe laid round a hub) or one card at a time, asking the chain planner what the
    /// bench would do, and keeps the best. Used to prove the content is winnable and as a debug autoplay.
    /// </summary>
    public enum AutoMove { None, ChoseCatalyst, Redrew, ReadyToIgnite }

    public sealed class AutoPlayer
    {
        static readonly string[] CatalystOrder =
        {
            Effects.ExtraHand, Effects.ChainPlus, Effects.FirstTriple, Effects.CentreTwice, Effects.ExtraTrial,
            Effects.KindDouble, Effects.Diagonals, Effects.EmptyTileBonus, Effects.DiscoveryTriple,
        };

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
            if (run.Phase != RunPhase.Playing) return AutoMove.None;
            for (int t = 0; t < Bench.TileCount; t++) run.Unplace(t);

            Prepare(run);
            Search();
            if ((best == null || bestUseful == 0) && run.Redraw()) return AutoMove.Redrew;
            if (best == null) return Force(run);

            for (int t = 0; t < Bench.TileCount; t++)
                if (best[t] != null) run.Place(run.Hand.ToList().IndexOf(best[t]), t);
            return run.CanIgnite ? AutoMove.ReadyToIgnite : Force(run);
        }

        /// <summary>Nothing worth doing: spend the trial on two starting materials, never on a discovery.</summary>
        AutoMove Force(RunState run)
        {
            for (int t = 0; t < Bench.TileCount; t++) run.Unplace(t);
            for (int tile = 0; tile < 2; tile++)
            {
                int i = run.Hand.ToList().FindIndex(h => tier[h] == 0);
                if (i < 0) return run.Redraw() ? AutoMove.Redrew : AutoMove.None;
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
        bool diagonals;
        long remaining;
        int trialsLeft, discoveriesHeld, bestUseful, lastUseful;
        string[] best;
        double bestScore;

        void Prepare(RunState r)
        {
            run = r; cats = r.Catalysts; diagonals = r.Diagonals;
            inHand.Clear(); held.Clear();
            foreach (var c in r.Hand) { inHand[c] = Count(inHand, c) + 1; held[c] = Count(held, c) + 1; }
            discoveriesHeld = r.Hand.Count(c => tier[c] > 0);
            ComputeNeeds();
            trialsLeft = r.TrialsLeft;
            remaining = r.Depth < RunState.FinalDepth ? System.Math.Max(0, r.Target - r.Potency) : 0;
            for (int t = 0; t < tiles.Length; t++) tiles[t] = null;
            best = null; bestScore = double.NegativeInfinity; bestUseful = 0;
        }

        static int Count(Dictionary<string, int> d, string k) => d.TryGetValue(k, out int v) ? v : 0;

        /// <summary>need: how many more of each material must be made; demand: how many are wanted in all.</summary>
        void ComputeNeeds()
        {
            need.Clear(); demand.Clear();
            demand[db.Ultimate] = 1;
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
                            if (tiles[hub] != null) continue;
                            freeAround.Clear();
                            foreach (int n in ChainPlanner.Neighbours(hub, diagonals)) if (tiles[n] == null) freeAround.Add(n);
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
                        if (tiles[t] == null) Try(new[] { id }, new[] { t }, ref next);
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
                foreach (int n in ChainPlanner.Neighbours(t, diagonals)) if (tiles[n] != null) return true;
            }
            return false;
        }

        /// <summary>How good the bench as laid out is; negative infinity when it could not be ignited.</summary>
        double Evaluate()
        {
            lastUseful = 0;
            if (!AnyPair()) return double.NegativeInfinity;
            int empty = 0, chain = 0, useful = 0;
            foreach (var t in tiles) if (t == null) empty++;
            var plan = ChainPlanner.Plan(tiles, db, diagonals, false);
            long gain = 0; bool won = false;
            double score = 0;
            firedLocal.Clear(); usedNeed.Clear();
            foreach (var step in plan.Steps)
            {
                chain++;
                string key = db.KeyOf(step.Recipe);
                bool first = !run.HasFired(step.Recipe) && firedLocal.Add(key);
                gain += IgniteResolver.Score(db, step.Recipe, step.Tiles, first, false, chain, empty, cats);
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
            int over = discoveriesHeld - spent + kept - RunState.RackSize;
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
