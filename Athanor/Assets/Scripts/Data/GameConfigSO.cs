using System;
using UnityEngine;
using Athanor.Core;

namespace Athanor.Data
{
    /// <summary>Master ScriptableObject aggregating all game content for Inspector editing.</summary>
    [CreateAssetMenu(fileName = "GameConfigSO.asset", menuName = "Athanor/Game Config")]
    public class GameConfigSO : ScriptableObject
    {
        // ── Tuning ──────────────────────────────────────────────────────────────
        [Header("Rules & Tuning")]
        [SerializeField] private RulesSO rules;

        [Tooltip("Depths (1-based) at which difficulty targets are checked. e.g. [300, 1500, 7500, 37500].")]
        public int[] depthTargets = new int[] { 300, 1500, 7500, 37500 };

        [Tooltip("Display names for each depth tier (must match depthTargets length).")]
        public string[] depthNames = Array.Empty<string>();

        [Tooltip("The ultimate material id to be crafted at the final depth.")]
        public string ultimateMaterialId = "worldseed";

        [Tooltip("Base draw weight for events when no event is triggered (null = omit).")]
        public int? noEventWeight;

        [Tooltip("Optional rarity draw weights for catalyst offers: Common, Uncommon, Rare, Legendary. Empty = uniform.")]
        public int[] rarityWeights = Array.Empty<int>();

        // ── Content Arrays ──────────────────────────────────────────────────────
        [Header("Content Arrays")]
        [SerializeField] private MaterialSO[] materials;
        [SerializeField] private RecipeSO[] recipes;
        [SerializeField] private CatalystSO[] catalysts;
        [SerializeField] private GoalSO[] goals;
        [SerializeField] private AspectSO[] aspects;
        [SerializeField] private EventDataSO[] events;

        // ── Accessors ───────────────────────────────────────────────────────────
        public RulesSO Rules => rules;
        public MaterialSO[] Materials => materials;
        public RecipeSO[] Recipes => recipes;
        public CatalystSO[] Catalysts => catalysts;
        public GoalSO[] Goals => goals;
        public AspectSO[] Aspects => aspects;
        public EventDataSO[] Events => events;

        /// <summary>Setters for the temporary content.json import tool.</summary>
        public void SetRules(RulesSO v)       { rules = v; }
        public void SetMaterials(MaterialSO[] v) { materials = v; }
        public void SetRecipes(RecipeSO[] v)   { recipes = v; }
        public void SetCatalysts(CatalystSO[] v) { catalysts = v; }
        public void SetGoals(GoalSO[] v)       { goals = v; }
        public void SetAspects(AspectSO[] v)   { aspects = v; }
        public void SetEvents(EventDataSO[] v) { events = v; }

        /// <summary>Check whether a material ID exists in the collection.</summary>
        public bool HasMaterial(string id) => Array.Exists(materials, m => m.id == id);

        /// <summary>Check whether a recipe with the given items exists.</summary>
        public bool HasRecipe(string[] items) => Array.Exists(recipes, r => IsSameItems(r.items, items));

        /// <summary>Check whether a catalyst ID exists.</summary>
        public bool HasCatalyst(string id) => Array.Exists(catalysts, c => c.id == id);

        /// <summary>Check whether a goal ID exists.</summary>
        public bool HasGoal(string id) => Array.Exists(goals, g => g.id == id);

        /// <summary>Check whether an event ID exists.</summary>
        public bool HasEvent(string id) => Array.Exists(events, e => e.id == id);

        // ── Validation ──────────────────────────────────────────────────────────
        private void OnValidate()
        {
            ValidateArrays();
            ValidateReferences();
            ValidateUniqueness();
        }

        private void ValidateArrays()
        {
            if (materials == null || materials.Length == 0)
                Debug.LogWarning("[GameConfigSO] No materials defined.", this);
            if (recipes == null || recipes.Length == 0)
                Debug.LogWarning("[GameConfigSO] No recipes defined.", this);
            if (catalysts == null || catalysts.Length == 0)
                Debug.LogWarning("[GameConfigSO] No catalysts defined.", this);
            if (goals == null || goals.Length == 0)
                Debug.LogWarning("[GameConfigSO] No goals defined.", this);
            if (aspects == null || aspects.Length == 0)
                Debug.LogWarning("[GameConfigSO] No aspects defined.", this);
            if (events == null || events.Length == 0)
                Debug.LogWarning("[GameConfigSO] No events defined.", this);

            if (depthTargets != null && depthNames != null)
            {
                if (depthTargets.Length != depthNames.Length)
                    Debug.LogError("[GameConfigSO] depthTargets length (" + depthTargets.Length +
                        ") does not match depthNames length (" + depthNames.Length + ").", this);
            }
        }

        private void ValidateReferences()
        {
            var knownMaterials = new System.Collections.Generic.HashSet<string>();
            foreach (var m in materials ?? Array.Empty<MaterialSO>())
                if (!string.IsNullOrEmpty(m.id)) knownMaterials.Add(m.id);

            foreach (var r in recipes ?? Array.Empty<RecipeSO>())
            {
                foreach (var item in r.items ?? Array.Empty<string>())
                    if (!knownMaterials.Contains(item))
                        Debug.LogWarning("[GameConfigSO] Recipe '" + r.result + "' references unknown material '" + item + "'.", this);
            }

            foreach (var c in catalysts ?? Array.Empty<CatalystSO>())
            {
                if (!string.IsNullOrEmpty(c.kind) && !Enum.TryParse<Kind>(c.kind, true, out _))
                    Debug.LogWarning("[GameConfigSO] Catalyst '" + c.id + "' has invalid kind: " + c.kind + ".", this);
            }
        }

        private void ValidateUniqueness()
        {
            CheckDuplicateIds(materials ?? Array.Empty<MaterialSO>(), "Material");
            CheckDuplicateIds(recipes ?? Array.Empty<RecipeSO>(), "Recipe");
            CheckDuplicateIds(catalysts ?? Array.Empty<CatalystSO>(), "Catalyst");
            CheckDuplicateIds(goals ?? Array.Empty<GoalSO>(), "Goal");
            CheckDuplicateIds(aspects ?? Array.Empty<AspectSO>(), "Aspect");
            CheckDuplicateIds(events ?? Array.Empty<EventDataSO>(), "Event");
        }

        private void CheckDuplicateIds<T>(T[] arr, string type) where T : class
        {
            var ids = new System.Collections.Generic.HashSet<string>();
            foreach (var item in arr)
            {
                var id = GetIdFromItem(item);
                if (ids.Contains(id))
                    Debug.LogError("[GameConfigSO] Duplicate " + type + " ID: '" + id + "'.", this);
                ids.Add(id);
            }
        }

        private static string GetIdFromItem<T>(T item) where T : class
        {
            return item switch
            {
                MaterialSO m => m.id,
                RecipeSO r => r.result,
                CatalystSO c => c.id,
                GoalSO g => g.id,
                AspectSO a => a.id,
                EventDataSO e => e.id,
                _ => ""
            };
        }

        // ── Helpers ─────────────────────────────────────────────────────────────
        private static bool IsSameItems(string[] a, string[] b)
        {
            if (a.Length != b.Length) return false;
            var sa = new System.Collections.Generic.SortedSet<string>(a);
            var sb = new System.Collections.Generic.SortedSet<string>(b);
            return sa.SetEquals(sb);
        }
    }
}
