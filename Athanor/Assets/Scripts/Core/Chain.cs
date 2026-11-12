using System.Collections.Generic;
using System.Linq;

namespace Athanor.Core
{
    /// <summary>
    /// One thing the bench does during an Ignite. A reaction: the hub and the neighbours it consumed
    /// (Tiles[0] is the hub, the rest ascending). A sputter: a combo of three or more whose layout was
    /// standing ready and was broken by the reaction before it.
    /// </summary>
    public sealed class ChainStep
    {
        public int Hub;
        public int[] Tiles;
        public string[] Items;   // what stood on Tiles when the step was found
        public RecipeData Recipe;
        public bool Sputter;
    }

    public sealed class ChainPlan
    {
        public readonly List<ChainStep> Steps = new List<ChainStep>();
        public string[] Final;   // the bench after the chain
        public bool[] Made;      // true where a material made during the chain still stands
    }

    /// <summary>
    /// Works out what a bench does, with no side effects. Each round the liveliest tile (highest Reaction
    /// Index, earlier tile on a tie) that has a recipe around it becomes the hub; the largest recipe it
    /// can complete with its neighbours fires; the result takes the hub's tile and joins the next round.
    /// </summary>
    public static class ChainPlanner
    {
        /// <summary>Sputters are only reported for combos of this many materials or more.</summary>
        public const int SputterSize = 3;

        public static IEnumerable<int> Neighbours(int tile, bool diagonals) => BenchRules.Plain(diagonals).Around(tile);
        public static int[] Neighbours(int tile, BenchRules rules) => (rules ?? BenchRules.None).Around(tile);

        /// <summary>
        /// The Reaction Index of the material on a tile plus that of everything touching it. A recipe fires
        /// only when the heat round its hub reaches the recipe's required total, so materials that are not
        /// part of the recipe (company) can lift it over, and dull company can leave it short.
        /// </summary>
        public static int Heat(string[] tiles, int hub, ContentDb db, bool diagonals) => Heat(tiles, hub, db, BenchRules.Plain(diagonals));

        public static int Heat(string[] tiles, int hub, ContentDb db, BenchRules rules)
        {
            if (hub < 0 || hub >= tiles.Length || !db.Has(tiles[hub])) return 0;
            int heat = rules.Ri(db, hub, tiles[hub]) + rules.HeatPlus;
            foreach (int n in rules.Around(hub)) if (db.Has(tiles[n])) heat += rules.Ri(db, n, tiles[n]) + rules.HeatPlus;
            return heat;
        }

        /// <summary>The best recipe the material on this tile can complete as a hub, or null.</summary>
        public static ChainStep MatchAt(string[] tiles, int hub, ContentDb db, bool diagonals) => Find(tiles, hub, db, BenchRules.Plain(diagonals), false);
        public static ChainStep MatchAt(string[] tiles, int hub, ContentDb db, BenchRules rules) => Find(tiles, hub, db, rules ?? BenchRules.None, false);

        /// <summary>
        /// The largest recipe laid out round this tile that does not fire only because the heat there is
        /// below its required total, or null. Used to tell the player what a dull bench was missing.
        /// </summary>
        public static RecipeData ShortAt(string[] tiles, int hub, ContentDb db, bool diagonals) => Find(tiles, hub, db, BenchRules.Plain(diagonals), true)?.Recipe;
        public static RecipeData ShortAt(string[] tiles, int hub, ContentDb db, BenchRules rules) => Find(tiles, hub, db, rules ?? BenchRules.None, true)?.Recipe;

        /// <summary>lacking: false finds what fires here; true finds what would, given more heat.</summary>
        static ChainStep Find(string[] tiles, int hub, ContentDb db, BenchRules rules, bool lacking)
        {
            if (hub < 0 || hub >= tiles.Length) return null;
            string centre = tiles[hub];
            if (centre == null || !db.Has(centre)) return null;
            var around = rules.Around(hub);
            var recipes = db.RecipesContaining(centre);
            int heat = rules.Kiln[hub] ? int.MaxValue : Heat(tiles, hub, db, rules);   // a kiln asks for no heat
            var used = new int[ContentDb.MaxItems - 1];
            for (int i = 0; i < recipes.Count; i++)
            {
                var recipe = recipes[i];
                if ((heat < recipe.minReactivity) != lacking) continue;   // too dull round the hub: as if there were no recipe
                int count = 0;
                bool hubTaken = false, fits = true;
                foreach (var item in recipe.items)
                {
                    if (!hubTaken && item == centre) { hubTaken = true; continue; }
                    int found = -1;
                    foreach (int n in around)
                    {
                        if (tiles[n] != item) continue;
                        bool taken = false;
                        for (int k = 0; k < count; k++) if (used[k] == n) taken = true;
                        if (!taken) { found = n; break; }
                    }
                    if (found < 0) { fits = false; break; }
                    used[count++] = found;
                }
                if (!fits) continue;
                var all = new int[count + 1];
                all[0] = hub;
                System.Array.Copy(used, 0, all, 1, count);
                System.Array.Sort(all, 1, count);
                var items = new string[all.Length];
                for (int k = 0; k < all.Length; k++) items[k] = tiles[all[k]];
                return new ChainStep { Hub = hub, Tiles = all, Items = items, Recipe = recipe };
            }
            return null;
        }

        /// <summary>sputters: false skips looking for spoiled combos (the scripted player does not need them).</summary>
        public static ChainPlan Plan(string[] tiles, ContentDb db, bool diagonals, bool sputters = true) =>
            Plan(tiles, db, BenchRules.Plain(diagonals), sputters);

        public static ChainPlan Plan(string[] tiles, ContentDb db, BenchRules rules, bool sputters = true)
        {
            rules = rules ?? BenchRules.None;
            var now = (string[])tiles.Clone();
            var plan = new ChainPlan { Final = now, Made = new bool[now.Length] };
            var order = new int[now.Length];
            while (true)
            {
                // occupied tiles, liveliest first, earlier tile on a tie
                int n = 0;
                for (int t = 0; t < now.Length; t++)
                {
                    if (now[t] == null || !db.Has(now[t])) continue;
                    int ri = rules.Ri(db, t, now[t]), at = n++;
                    while (at > 0 && rules.Ri(db, order[at - 1], now[order[at - 1]]) < ri) { order[at] = order[at - 1]; at--; }
                    order[at] = t;
                }
                // a pivot tile that can react goes first, whatever the others' Reaction Index
                ChainStep step = rules.Pivot >= 0 ? Find(now, rules.Pivot, db, rules, false) : null;
                for (int k = 0; k < n && step == null; k++) step = Find(now, order[k], db, rules, false);
                if (step == null) break;

                // combos that were standing ready and are about to lose a material
                List<ChainStep> spoiled = null;
                for (int t = 0; sputters && t < now.Length; t++)
                {
                    if (t == step.Hub || now[t] == null) continue;
                    var other = Find(now, t, db, rules, false);
                    if (other == null || other.Tiles.Length < SputterSize || !other.Tiles.Intersect(step.Tiles).Any()) continue;
                    if (SameTiles(other, step) || (spoiled != null && spoiled.Any(s => SameTiles(s, other)))) continue;
                    other.Sputter = true;
                    (spoiled ?? (spoiled = new List<ChainStep>())).Add(other);
                }

                for (int k = 1; k < step.Tiles.Length; k++) { now[step.Tiles[k]] = null; plan.Made[step.Tiles[k]] = false; }
                now[step.Hub] = step.Recipe.result;
                plan.Made[step.Hub] = true;
                plan.Steps.Add(step);
                if (spoiled != null) plan.Steps.AddRange(spoiled);
            }
            return plan;
        }

        static bool SameTiles(ChainStep a, ChainStep b) => a.Tiles.Length == b.Tiles.Length && !a.Tiles.Except(b.Tiles).Any();
    }
}
