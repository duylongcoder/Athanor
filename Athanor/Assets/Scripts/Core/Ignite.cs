using System.Collections.Generic;
using System.Linq;

namespace Athanor.Core
{
    /// <summary>The 3x3 bench: tile index = row * 3 + column, null where empty.</summary>
    public sealed class Bench
    {
        public const int Size = 3, TileCount = 9, Centre = 4;
        public readonly string[] Tiles = new string[TileCount];
        public int Count => Tiles.Count(t => t != null);
        public void Clear() { for (int i = 0; i < TileCount; i++) Tiles[i] = null; }
    }

    /// <summary>
    /// One thing that happened during an Ignite, in the order it should be shown.
    /// A reaction: Tiles[0] is the hub, where the result now stands; the other tiles were consumed.
    /// A sputter: a combo that stood ready and was spoiled by the reaction before it.
    /// A fizzle (no Result, not a Sputter): two neighbours left on the bench that do nothing together.
    /// </summary>
    public sealed class ReactionEvent
    {
        public int Hub;
        public int[] Tiles;
        public string[] Items;     // the materials on Tiles, in the same order
        public string Result;      // null: a fizzle or a sputter
        public RecipeData Recipe;  // null on a fizzle
        public bool Discovery;     // first time this recipe entered the journal
        public bool FirstThisRun;
        public bool Sputter;
        public int Chain;          // 1 for the first reaction of the Ignite, 2 for the second...
        public long Potency;
        public HintData Hint;      // on a fizzle, when there was something new to hint at
    }

    public sealed class ResolveResult
    {
        public List<ReactionEvent> Events = new List<ReactionEvent>();
        public string[] Final;     // the bench after the chain
        public bool[] Made;        // true where a material made during this Ignite still stands
    }

    public static class IgniteResolver
    {
        /// <summary>Occupied neighbouring pairs in reading order (a &lt; b).</summary>
        public static List<(int a, int b)> Pairs(Bench bench, bool diagonals) => Pairs(bench, BenchRules.Plain(diagonals));

        public static List<(int a, int b)> Pairs(Bench bench, BenchRules rules)
        {
            rules = rules ?? BenchRules.None;
            var pairs = new List<(int, int)>();
            for (int a = 0; a < Bench.TileCount; a++)
            {
                if (bench.Tiles[a] == null) continue;
                for (int b = a + 1; b < Bench.TileCount; b++)
                    if (bench.Tiles[b] != null && rules.Touch(a, b)) pairs.Add((a, b));
            }
            return pairs;
        }

        public static bool HasAnyPair(Bench bench, bool diagonals) => Pairs(bench, diagonals).Count > 0;
        public static bool HasAnyPair(Bench bench, BenchRules rules) => Pairs(bench, rules).Count > 0;

        public static bool Has(IReadOnlyList<CatalystData> catalysts, string effect) =>
            catalysts != null && catalysts.Any(c => c.effect == effect);

        /// <summary>
        /// Plays the bench out: the chain of reactions, then a fizzle for every pair of neighbours left over.
        /// Updates the journal and the set of recipes fired this run; the bench itself is not changed.
        /// </summary>
        public static ResolveResult Resolve(Bench bench, ContentDb db, Journal journal, ISet<string> firedThisRun,
            IReadOnlyList<CatalystData> catalysts, ScoreContext context = null)
        {
            catalysts = catalysts ?? new CatalystData[0];
            var ctx = context ?? new ScoreContext { Rules = BenchRules.Plain(Has(catalysts, Effects.Diagonals)) };
            var diagonals = ctx.Rules;
            ctx.EmptyTiles = Bench.TileCount - bench.Count;
            ctx.EmptyAtIgnite = new bool[Bench.TileCount];
            for (int t = 0; t < Bench.TileCount; t++) ctx.EmptyAtIgnite[t] = bench.Tiles[t] == null;
            int chain = 0;
            var plan = ChainPlanner.Plan(bench.Tiles, db, diagonals);
            var result = new ResolveResult { Final = plan.Final, Made = plan.Made };

            foreach (var step in plan.Steps)
            {
                var e = new ReactionEvent { Hub = step.Hub, Tiles = step.Tiles, Items = step.Items, Recipe = step.Recipe, Sputter = step.Sputter };
                if (!step.Sputter)
                {
                    e.Result = step.Recipe.result;
                    e.Chain = ++chain;
                    e.Discovery = journal.Learn(step.Items);
                    e.FirstThisRun = firedThisRun.Add(db.KeyOf(step.Recipe));
                    e.Potency = Score(db, step.Recipe, step.Tiles, e.FirstThisRun, e.Discovery, chain, catalysts, ctx);
                }
                result.Events.Add(e);
            }

            var after = new Bench();
            for (int t = 0; t < Bench.TileCount; t++) after.Tiles[t] = plan.Final[t];
            foreach (var (ta, tb) in Pairs(after, diagonals))
            {
                string a = after.Tiles[ta], b = after.Tiles[tb];
                result.Events.Add(new ReactionEvent
                {
                    Hub = ta, Tiles = new[] { ta, tb }, Items = new[] { a, b },
                    Hint = HeatHint(db, journal, after.Tiles, ta, tb, diagonals) ?? NextHint(db, journal, a, b),
                });
            }
            return result;
        }

        /// <summary>
        /// Potency of one reaction: the result's base, times one less than the number of materials used,
        /// doubled the first time this run, times its place in the chain, then the catalysts and the hub's tile.
        /// </summary>
        public static long Score(ContentDb db, RecipeData recipe, int[] tiles, bool firstThisRun, bool discovery, int chain,
            int emptyTiles, IReadOnlyList<CatalystData> catalysts) =>
            Score(db, recipe, tiles, firstThisRun, discovery, chain, catalysts, new ScoreContext { EmptyTiles = emptyTiles });

        public static long Score(ContentDb db, RecipeData recipe, int[] tiles, bool firstThisRun, bool discovery, int chain,
            IReadOnlyList<CatalystData> catalysts, ScoreContext ctx)
        {
            var rules = ctx.Rules ?? BenchRules.None;
            int hub = tiles[0], tier = db.Material(recipe.result).tier, items = recipe.items.Length;
            int baseTier = tier;
            if (rules.AttunedKind[hub] != null && recipe.items.Any(i => db.KindOf(i).ToString() == rules.AttunedKind[hub])) baseTier++;
            double p = db.BasePotency(baseTier) * (items - 1);
            if (firstThisRun) p *= db.Rules.firstReactionMult;
            int step = chain + (rules.Heavy[hub] ? 1 : 0);
            p *= Has(catalysts, Effects.ChainPlus) ? step + 1 : step;
            long bonus = 0;
            foreach (var c in catalysts)
            {
                switch (c.effect)
                {
                    case Effects.KindDouble: if (recipe.items.Any(i => db.KindOf(i).ToString() == c.kind)) p *= 2; break;
                    case Effects.CentreTwice: if (System.Array.IndexOf(tiles, Bench.Centre) >= 0) p *= 2; break;
                    case Effects.FirstTriple: if (chain == 1) p *= 3; break;
                    case Effects.DiscoveryTriple: if (discovery) p *= 3; break;
                    case Effects.EmptyTileBonus: bonus += (long)c.amount * ctx.EmptyTiles; break;
                    case Effects.ComboBonus: if (items >= 3) p *= c.amount; break;
                    case Effects.PairBonus: if (items == 2) p *= c.amount; break;
                    case Effects.FlatPerReaction: bonus += (long)c.amount; break;
                    case Effects.TierDouble: if (tier >= (int)c.amount) p *= 2; break;
                    case Effects.StarterDouble: if (recipe.items.Count(i => db.Material(i).tier == 0) >= (int)c.amount) p *= 2; break;
                    case Effects.ChainFrom: if (chain >= (int)c.amount) p *= 2; break;
                    case Effects.DepthScale: p *= 1 + c.amount * (ctx.Depth - 1); break;
                    case Effects.AllScale: p *= c.amount; break;
                }
            }
            p *= rules.HubMult[hub];
            if (ctx.TripleFirst && chain == 1) p *= 3;
            if (ctx.EmptyAtIgnite != null)
                for (int t = 0; t < Bench.TileCount; t++)
                    if (rules.SeedBonus[t] > 0 && ctx.EmptyAtIgnite[t] && rules.Touch(hub, t)) bonus += rules.SeedBonus[t];
            return (long)System.Math.Round(p) + bonus;
        }

        /// <summary>
        /// For a pair that did nothing only because the bench was too dull: the heat that a recipe not yet
        /// found, laid out round one of the two, is waiting for. Given once for each material and number.
        /// </summary>
        static HintData HeatHint(ContentDb db, Journal journal, string[] tiles, int ta, int tb, BenchRules diagonals)
        {
            RecipeData told = null;   // one recipe is hinted at from one of its materials, not from each
            foreach (int t in new[] { ta, tb })
            {
                var recipe = ChainPlanner.ShortAt(tiles, t, db, diagonals);
                if (recipe == null || recipe == told || journal.IsKnown(recipe.items)) continue;
                if (journal.AddHeatHint(tiles[t], recipe.minReactivity)) return new HintData { material = tiles[t], heat = recipe.minReactivity };
                told = recipe;
            }
            return null;
        }

        /// <summary>
        /// For a pair that did nothing: the kind of some material that one of the two does react with,
        /// in a recipe not yet found and not yet hinted at.
        /// </summary>
        static HintData NextHint(ContentDb db, Journal journal, string a, string b)
        {
            foreach (var m in a == b ? new[] { a } : new[] { a, b })
                foreach (var recipe in db.RecipesContaining(m).Reverse())   // the simplest recipes first
                {
                    if (journal.IsKnown(recipe.items)) continue;
                    bool self = false;
                    foreach (var partner in recipe.items)
                    {
                        if (partner == m && !self) { self = true; continue; }
                        var kind = db.KindOf(partner);
                        if (journal.AddHint(m, kind)) return new HintData { material = m, partnerKind = kind.ToString() };
                    }
                }
            return null;
        }
    }
}
