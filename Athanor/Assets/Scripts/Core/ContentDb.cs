using System;
using System.Collections.Generic;
using System.Linq;

namespace Athanor.Core
{
    /// <summary>Validated, indexed view of the game's content.</summary>
    public sealed class ContentDb
    {
        readonly Dictionary<string, MaterialData> materials = new Dictionary<string, MaterialData>();
        readonly Dictionary<string, Kind> kinds = new Dictionary<string, Kind>();
        readonly Dictionary<string, RecipeData> recipes = new Dictionary<string, RecipeData>();
        readonly Dictionary<string, List<string>> partners = new Dictionary<string, List<string>>();
        readonly Dictionary<string, List<RecipeData>> containing = new Dictionary<string, List<RecipeData>>();
        readonly Dictionary<RecipeData, int> totals = new Dictionary<RecipeData, int>();

        public const int MinItems = 2, MaxItems = 5, MaxCopies = 2, MinReactivity = 1, MaxReactivity = 10;
        readonly List<MaterialData> starters = new List<MaterialData>();
        readonly ContentData data;

        public ContentDb(ContentData data)
        {
            if (data == null || data.materials == null || data.recipes == null) throw new ArgumentException("content is empty");
            this.data = data;
            foreach (var m in data.materials)
            {
                if (materials.ContainsKey(m.id)) throw new ArgumentException("duplicate material " + m.id);
                if (!Enum.TryParse(m.kind, out Kind k)) throw new ArgumentException("unknown kind " + m.kind + " on " + m.id);
                if (m.reactivity < MinReactivity || m.reactivity > MaxReactivity)
                    throw new ArgumentException("reactivity of " + m.id + " must be " + MinReactivity + " to " + MaxReactivity);
                materials[m.id] = m; kinds[m.id] = k; partners[m.id] = new List<string>(); containing[m.id] = new List<RecipeData>();
                if (m.tier == 0) starters.Add(m);
            }
            foreach (var r in data.recipes)
            {
                if (r.items == null || r.items.Length == 0) r.items = new[] { r.a, r.b };   // the older two-material form
                if (r.items.Length < MinItems || r.items.Length > MaxItems)
                    throw new ArgumentException("a recipe takes " + MinItems + " to " + MaxItems + " materials: " + r.result);
                foreach (var id in r.items.Concat(new[] { r.result }))
                    if (id == null || !materials.ContainsKey(id)) throw new ArgumentException("recipe names unknown material " + id);
                var key = ComboKey(r.items);
                if (r.items.GroupBy(i => i).Any(g => g.Count() > MaxCopies)) throw new ArgumentException("more than " + MaxCopies + " of one material in " + key);
                if (r.minReactivity < 0) throw new ArgumentException("negative minReactivity on " + key);
                if (recipes.ContainsKey(key)) throw new ArgumentException("duplicate recipe " + key);
                recipes[key] = r;
                totals[r] = r.items.Sum(i => materials[i].reactivity);
                // the hub has four sides: whatever the recipe does not fill is all the room company has
                if (r.minReactivity > totals[r] + (MaxItems - r.items.Length) * MaxReactivity)
                    throw new ArgumentException("minReactivity on " + key + " can never be met");
                foreach (var id in r.items.Distinct())
                {
                    containing[id].Add(r);
                    foreach (var other in r.items.Distinct())
                        if (other != id && !partners[id].Contains(other)) partners[id].Add(other);
                }
            }
            // largest first, then the liveliest, then the order of the file (OrderBy is stable)
            foreach (var id in containing.Keys.ToList())
                containing[id] = containing[id].OrderByDescending(r => r.items.Length).ThenByDescending(TotalReactivity).ToList();
            if (!string.IsNullOrEmpty(data.ultimate) && !materials.ContainsKey(data.ultimate))
                throw new ArgumentException("unknown ultimate " + data.ultimate);
            Catalysts = data.catalysts ?? new CatalystData[0];
        }

        public IReadOnlyList<MaterialData> Starters => starters;
        public IReadOnlyList<MaterialData> Materials => data.materials;
        public IReadOnlyList<RecipeData> Recipes => data.recipes;
        public IReadOnlyList<CatalystData> Catalysts { get; }
        public string Ultimate => data.ultimate;

        public MaterialData Material(string id) => materials[id];
        public bool Has(string id) => id != null && materials.ContainsKey(id);
        public Kind KindOf(string id) => kinds[id];
        public IEnumerable<string> PartnersOf(string id) => partners[id];

        public int Reactivity(string id) => materials[id].reactivity;
        public int TotalReactivity(RecipeData r) => totals.TryGetValue(r, out int t) ? t : r.items.Sum(i => materials[i].reactivity);
        public string KeyOf(RecipeData r) => ComboKey(r.items);

        /// <summary>How much Reaction Index a recipe needs from materials beside its hub that are not part of it.</summary>
        public int Company(RecipeData r) => Math.Max(0, r.minReactivity - TotalReactivity(r));

        /// <summary>Recipes this material is an ingredient of: largest first, then highest total reactivity.</summary>
        public IReadOnlyList<RecipeData> RecipesContaining(string id) => containing[id];

        /// <summary>The recipe for exactly these materials, in any order. Does not look at minReactivity.</summary>
        public bool TryCombo(IEnumerable<string> items, out RecipeData recipe) => recipes.TryGetValue(ComboKey(items), out recipe);

        public bool TryReact(string a, string b, out string result)
        {
            bool found = recipes.TryGetValue(PairKey(a, b), out var r);
            result = found ? r.result : null;
            return found;
        }

        /// <summary>Potency needed to clear a depth; 0 where there is none (depth 5).</summary>
        public int TargetFor(int depth)
        {
            var t = data.depthTargets;
            return t != null && depth >= 1 && depth <= t.Length ? t[depth - 1] : 0;
        }

        public string DepthName(int depth)
        {
            var n = data.depthNames;
            return n != null && depth >= 1 && depth <= n.Length ? n[depth - 1] : "Depth " + depth;
        }

        static readonly long[] Base = { 0, 20, 60, 180, 500, 1500 };
        public static long BasePotency(int tier) => Base[Math.Max(0, Math.Min(tier, Base.Length - 1))];

        public static string PairKey(string a, string b) => string.CompareOrdinal(a, b) <= 0 ? a + "+" + b : b + "+" + a;

        /// <summary>The journal's name for a recipe: its materials sorted and joined with '+'.</summary>
        public static string ComboKey(IEnumerable<string> items) => string.Join("+", items.OrderBy(i => i, StringComparer.Ordinal));
    }
}
