using System;
using System.Collections.Generic;
using System.Linq;

namespace Athanor.Core
{
    public static class RewardKinds { public const string Starter = "starter", Catalyst = "catalyst"; }

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
            Goals = data.goals ?? new GoalData[0];
            Aspects = data.aspects ?? new AspectData[0];
            Events = data.events ?? new EventData[0];
            ValidatePhase3();
        }

        /// <summary>Rarity, locks, goals, aspects and events. Content with none of them (Phase 2) passes untouched.</summary>
        void ValidatePhase3()
        {
            if (data.rarityWeights != null)
            {
                if (data.rarityWeights.Length != Rarities.All.Length) throw new ArgumentException("rarityWeights needs " + Rarities.All.Length + " numbers: Common, Uncommon, Rare, Legendary");
                if (data.rarityWeights.Any(w => w < 0)) throw new ArgumentException("a rarity weight cannot be negative");
                if (data.rarityWeights.All(w => w == 0)) throw new ArgumentException("rarityWeights cannot all be zero");
            }
            foreach (var m in data.materials)
            {
                if (m.tier != 0)
                {
                    if (!string.IsNullOrEmpty(m.rarity) || m.locked) throw new ArgumentException("only a starting material has a rarity or a lock: " + m.id);
                    continue;
                }
                int rank = Array.IndexOf(Rarities.All, Rarities.Of(m.rarity));
                if (rank < 0 || rank > 2) throw new ArgumentException("starter rarity must be Common, Uncommon or Rare: " + m.id);
                if (m.locked && rank == 0) throw new ArgumentException("a Common item cannot be locked: " + m.id);
            }
            var ids = new HashSet<string>();
            foreach (var c in Catalysts)
            {
                if (!ids.Add(c.id)) throw new ArgumentException("duplicate catalyst " + c.id);
                if (Array.IndexOf(Effects.All, c.effect) < 0) throw new ArgumentException("unknown catalyst effect " + c.effect + " on " + c.id);
                if (Array.IndexOf(Rarities.All, Rarities.Of(c.rarity)) < 0) throw new ArgumentException("unknown rarity on " + c.id);
                if (c.locked && Rarities.Rank(c.rarity) == 0) throw new ArgumentException("a Common item cannot be locked: " + c.id);
            }
            ids.Clear();
            var budget = new Dictionary<string, int>();
            foreach (var g in Goals)
            {
                if (string.IsNullOrEmpty(g.id) || !ids.Add(g.id)) throw new ArgumentException("duplicate or empty goal id " + g.id);
                if (Array.IndexOf(StatIds.All, g.stat) < 0) throw new ArgumentException("unknown stat " + g.stat + " on goal " + g.id);
                if (g.target < 1) throw new ArgumentException("goal target must be 1 or more: " + g.id);
                if (g.rewardKind != RewardKinds.Starter && g.rewardKind != RewardKinds.Catalyst) throw new ArgumentException("unknown reward kind on goal " + g.id);
                int rank = Array.IndexOf(Rarities.All, g.rewardRarity);
                if (rank < 1 || (g.rewardKind == RewardKinds.Starter && rank > 2)) throw new ArgumentException("no such reward bucket for goal " + g.id);
                string bucket = g.rewardKind + "/" + g.rewardRarity;
                budget[bucket] = (budget.TryGetValue(bucket, out int n) ? n : 0) + 1;
            }
            // the owner's rule: a goal must never complete onto an empty pool
            foreach (var b in budget)
            {
                var parts = b.Key.Split('/');
                int locked = LockedIn(parts[0], parts[1]).Count;
                if (b.Value > locked) throw new ArgumentException(b.Value + " goals pay out " + b.Key + " but only " + locked + " such items are locked");
            }
            ids.Clear();
            foreach (var a in Aspects)
            {
                if (string.IsNullOrEmpty(a.id) || !ids.Add(a.id)) throw new ArgumentException("duplicate or empty aspect id " + a.id);
                if (Array.IndexOf(AspectEffects.All, a.effect) < 0) throw new ArgumentException("unknown aspect effect " + a.effect + " on " + a.id);
                if ((a.effect == AspectEffects.Heated || a.effect == AspectEffects.Seed) && a.amount <= 0) throw new ArgumentException("aspect " + a.id + " needs an amount above 0");
            }
            ids.Clear();
            foreach (var e in Events)
            {
                if (string.IsNullOrEmpty(e.id) || !ids.Add(e.id)) throw new ArgumentException("duplicate or empty event id " + e.id);
                if (Array.IndexOf(Ops.Types, e.type) < 0) throw new ArgumentException("unknown event type " + e.type + " on " + e.id);
                if (e.weight < 1 || e.minDepth < 1 || e.maxDepth < e.minDepth) throw new ArgumentException("bad weight or depths on event " + e.id);
                if (e.options == null || e.options.Length == 0) throw new ArgumentException("event has no option: " + e.id);
                bool free = false;
                foreach (var o in e.options)
                {
                    if (!string.IsNullOrEmpty(o.requires) && Array.IndexOf(Ops.Needs, o.requires) < 0) throw new ArgumentException("unknown requirement " + o.requires + " on " + e.id);
                    if (o.outcomes == null || o.outcomes.Length == 0) throw new ArgumentException("option with no outcome on " + e.id);
                    foreach (var fx in (o.cost ?? new EffectData[0]).Concat(o.outcomes.SelectMany(x => x.gain ?? new EffectData[0])))
                        if (Array.IndexOf(Ops.All, fx.op) < 0) throw new ArgumentException("unknown effect " + fx.op + " on " + e.id);
                    foreach (var fx in (o.cost ?? new EffectData[0]).Concat(o.outcomes.SelectMany(x => x.gain ?? new EffectData[0])))
                        if (fx.op == Ops.HeatTile && fx.amount <= 0) throw new ArgumentException("HeatTile needs an amount above 0 on " + e.id);
                    if (o.outcomes.Any(x => x.weight < 1)) throw new ArgumentException("outcome weight must be 1 or more on " + e.id);
                    if (string.IsNullOrEmpty(o.requires) && (o.cost == null || o.cost.Length == 0)) free = true;
                }
                if (!free) throw new ArgumentException("event cannot be left: no option without a cost on " + e.id);
            }
            ValidateRules(data);
        }

        /// <summary>The tuning numbers from content.json.</summary>
        public RulesData Rules => data.rules;

        /// <summary>The last depth of a run: the number of named depths.</summary>
        public int FinalDepth => data.depthNames.Length;

        static void ValidateRules(ContentData data)
        {
            var r = data.rules;
            if (r == null) throw new ArgumentException("content has no rules block");
            if (data.depthNames == null || data.depthNames.Length < 1) throw new ArgumentException("depthNames must name at least one depth");
            if (data.depthTargets == null || data.depthTargets.Length != data.depthNames.Length - 1)
                throw new ArgumentException("depthTargets must be one fewer than depthNames");
            if (r.handMin < 1) throw new ArgumentException("rules.handMin must be 1 or more");
            if (r.handBase < r.handMin) throw new ArgumentException("rules.handBase must be at least handMin");
            if (r.trials < 1 || r.trialsFinalDepth < 1) throw new ArgumentException("rules.trials and trialsFinalDepth must be 1 or more");
            if (r.redraws < 0) throw new ArgumentException("rules.redraws must be 0 or more");
            if (r.starterCopies < 1 || r.offerSize < 1 || r.rackSize < 1) throw new ArgumentException("rules.starterCopies, offerSize and rackSize must be 1 or more");
            if (r.pouchCards < 0) throw new ArgumentException("rules.pouchCards must be 0 or more");
            if (r.maxRi < 1) throw new ArgumentException("rules.maxRi must be 1 or more");
            if (r.firstReactionMult < 1) throw new ArgumentException("rules.firstReactionMult must be 1 or more");
            if (r.aspectDepths == null) throw new ArgumentException("rules.aspectDepths must be listed");
            var final = data.depthNames.Length;
            if (r.aspectDepths.Any(d => d < 1 || d > final)) throw new ArgumentException("rules.aspectDepths must be 1 to " + final);
            var top = data.materials.Length == 0 ? 0 : data.materials.Max(m => m.tier);
            if (r.potencyByTier == null || r.potencyByTier.Length < top + 1)
                throw new ArgumentException("rules.potencyByTier must have an entry for every tier up to " + top);
        }

        /// <summary>The locked items of one reward bucket, by id.</summary>
        public List<string> LockedIn(string rewardKind, string rarity)
        {
            var found = rewardKind == RewardKinds.Starter
                ? starters.Where(m => m.locked && Rarities.Of(m.rarity) == rarity).Select(m => m.id)
                : Catalysts.Where(c => c.locked && Rarities.Of(c.rarity) == rarity).Select(c => c.id);
            return found.OrderBy(i => i, StringComparer.Ordinal).ToList();
        }

        public IReadOnlyList<MaterialData> Starters => starters;
        public IReadOnlyList<MaterialData> Materials => data.materials;
        public IReadOnlyList<RecipeData> Recipes => data.recipes;
        public IReadOnlyList<CatalystData> Catalysts { get; }
        public IReadOnlyList<GoalData> Goals { get; private set; }
        public IReadOnlyList<AspectData> Aspects { get; private set; }
        public IReadOnlyList<EventData> Events { get; private set; }
        public int NoEventWeight => data.noEventWeight;

        /// <summary>The draw weight of a rarity (rarityWeights, or 1 when the field is missing).</summary>
        public int RarityWeight(string rarity) => data.rarityWeights == null ? 1 : data.rarityWeights[Rarities.Rank(rarity)];

        /// <summary>True when the weights are all equal (or missing): draws then keep the plain uniform shuffle.</summary>
        public bool UniformRarity => data.rarityWeights == null || data.rarityWeights.All(w => w == data.rarityWeights[0]);

        /// <summary>Removes and returns up to n catalysts from pool by rarity weight, without replacement. Uniform weights shuffle exactly as before.</summary>
        public List<CatalystData> DrawCatalysts(List<CatalystData> pool, int n, Rng rng)
        {
            if (UniformRarity) { rng.Shuffle(pool); var all = pool.Take(n).ToList(); return all; }
            var left = new List<CatalystData>(pool); var got = new List<CatalystData>();
            while (got.Count < n && left.Count > 0) { var c = PickCatalyst(left, rng); left.Remove(c); got.Add(c); }
            return got;
        }

        /// <summary>One catalyst from pool by rarity weight (uniform when the weights are missing or all zero in the pool).</summary>
        public CatalystData PickCatalyst(IList<CatalystData> pool, Rng rng)
        {
            if (UniformRarity) return pool[rng.Range(pool.Count)];
            int total = pool.Sum(c => RarityWeight(c.rarity));
            if (total <= 0) return pool[rng.Range(pool.Count)];
            int roll = rng.Range(total);
            foreach (var c in pool) { roll -= RarityWeight(c.rarity); if (roll < 0) return c; }
            return pool[pool.Count - 1];
        }
        public CatalystData Catalyst(string id) => Catalysts.FirstOrDefault(c => c.id == id);
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

        /// <summary>Base potency of a material tier, from rules.potencyByTier; a tier past the table uses its last entry.</summary>
        public long BasePotency(int tier) { var t = data.rules.potencyByTier; return t[Math.Max(0, Math.Min(tier, t.Length - 1))]; }

        public static string PairKey(string a, string b) => string.CompareOrdinal(a, b) <= 0 ? a + "+" + b : b + "+" + a;

        /// <summary>The journal's name for a recipe: its materials sorted and joined with '+'.</summary>
        public static string ComboKey(IEnumerable<string> items) => string.Join("+", items.OrderBy(i => i, StringComparer.Ordinal));
    }
}
