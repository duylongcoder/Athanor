using System;
using UnityEngine;
using Athanor.Data;
using Athanor.Core;

namespace Athanor.Editor.TempImport
{
    /// <summary>TEMPORARY IMPORT TOOL — will be deleted after migration.
    /// Reads content.json from Assets/Resources and creates a GameConfigSO instance.
    /// Run once via Unity Menu: Tools → Athanor → Import Content JSON
    /// </summary>
    [UnityEditor.InitializeOnLoad]
    public static class ContentJsonImporter
    {
        const string JSON_PATH = "Assets/Resources/content.json";
        const string ASSET_PATH = "Assets/Data/GameConfig.asset";

        static ContentJsonImporter() { }

        [UnityEditor.MenuItem("Tools/Athanor/Import Content JSON")]
        public static void Import()
        {
            var jsonText = System.IO.File.ReadAllText(JSON_PATH);
            var root = JsonUtility.FromJson<JsonRoot>(jsonText);

            var config = ScriptableObject.CreateInstance<GameConfigSO>();
            config.name = "GameConfig";

            // Rules
            var rules = ScriptableObject.CreateInstance<RulesSO>();
            rules.handMin = root.rules.handMin;
            rules.handBase = root.rules.handBase;
            rules.trials = root.rules.trials;
            rules.trialsFinalDepth = root.rules.trialsFinalDepth;
            rules.redraws = root.rules.redraws;
            rules.starterCopies = root.rules.starterCopies;
            rules.offerSize = root.rules.offerSize;
            rules.rackSize = root.rules.rackSize;
            rules.maxRi = root.rules.maxRi;
            rules.pouchCards = root.rules.pouchCards;
            rules.firstReactionMult = root.rules.firstReactionMult;
            rules.aspectDepths = root.rules.aspectDepths ?? Array.Empty<int>();
            rules.potencyByTier = root.rules.potencyByTier ?? new long[10];
            config.SetRules(rules);

            // Depth settings
            config.depthTargets = root.depthTargets ?? Array.Empty<int>();
            config.depthNames = root.depthNames ?? Array.Empty<string>();
            config.ultimateMaterialId = root.ultimate ?? "worldseed";
            config.noEventWeight = root.noEventWeight;

            // Materials
            var mats = new MaterialSO[root.materials.Length];
            for (int i = 0; i < root.materials.Length; i++)
            {
                var src = root.materials[i];
                mats[i] = new MaterialSO
                {
                    id = src.id,
                    name = src.name,
                    kind = ParseEnum<Kind>(src.kind),
                    tier = src.tier,
                    reactivity = src.reactivity,
                    glow = src.glow,
                    rarity = ParseEnum<Rarity>(src.rarity),
                    locked = src.locked
                };
            }
            config.SetMaterials(mats);

            // Recipes
            var recipies = new RecipeSO[root.recipes.Length];
            for (int i = 0; i < root.recipes.Length; i++)
            {
                var src = root.recipes[i];
                var items = src.items?.Length > 0 ? src.items : new[] { src.a, src.b };
                recipies[i] = new RecipeSO
                {
                    items = items,
                    minReactivity = src.minReactivity,
                    result = src.result
                };
            }
            config.SetRecipes(recipies);

            // Catalysts
            var cats = new CatalystSO[root.catalysts.Length];
            for (int i = 0; i < root.catalysts.Length; i++)
            {
                var src = root.catalysts[i];
                cats[i] = new CatalystSO
                {
                    id = src.id,
                    name = src.name,
                    text = src.text,
                    effect = src.effect,
                    kind = src.kind,
                    amount = src.amount,
                    rarity = ParseEnum<Rarity>(src.rarity),
                    locked = src.locked
                };
            }
            config.SetCatalysts(cats);

            // Goals
            var goals = new GoalSO[root.goals.Length];
            for (int i = 0; i < root.goals.Length; i++)
            {
                var src = root.goals[i];
                goals[i] = new GoalSO
                {
                    id = src.id,
                    text = src.text,
                    stat = ParseEnum<GoalStatId>(src.stat),
                    target = src.target,
                    rewardKind = ParseEnum<RewardKind>(src.rewardKind),
                    rewardRarity = ParseEnum<Rarity>(src.rewardRarity)
                };
            }
            config.SetGoals(goals);

            // Aspects
            var aspects = new AspectSO[root.aspects.Length];
            for (int i = 0; i < root.aspects.Length; i++)
            {
                var src = root.aspects[i];
                aspects[i] = new AspectSO
                {
                    id = src.id,
                    name = src.name,
                    text = src.text,
                    effect = src.effect,
                    scope = ParseEnum<AspectScope>(src.scope),
                    group = ParseEnum<AspectGroup>(src.group),
                    amount = src.amount,
                    tradeOff = src.tradeOff
                };
            }
            config.SetAspects(aspects);

            // Events
            var events = new EventDataSO[root.events.Length];
            for (int i = 0; i < root.events.Length; i++)
            {
                var src = root.events[i];
                var options = new EventOptionSO[src.options.Length];
                for (int j = 0; j < src.options.Length; j++)
                {
                    var oSrc = src.options[j];
                    var outcomes = new EventOutcomeSO[oSrc.outcomes.Length];
                    for (int k = 0; k < oSrc.outcomes.Length; k++)
                    {
                        var oc = oSrc.outcomes[k];
                        var effectEntries = new EffectEntrySO[oc.gain.Length];
                        for (int l = 0; l < oc.gain.Length; l++)
                        {
                            effectEntries[l] = new EffectEntrySO
                            {
                                op = ParseEnum<EffectOp>(oc.gain[l].op),
                                arg = oc.gain[l].arg ?? "",
                                amount = oc.gain[l].amount,
                                amount2 = oc.gain[l].amount2
                            };
                        }
                        outcomes[k] = new StandardEffectOutcomeSO
                        {
                            weight = oc.weight,
                            text = oc.text,
                            gain = effectEntries
                        };
                    }
                    options[j] = new EventOptionSO
                    {
                        label = oSrc.label,
                        requires = string.IsNullOrEmpty(oSrc.requires) ? EventNeedType.None : ParseEnum<EventNeedType>(oSrc.requires),
                        cost = Array.Empty<EffectEntrySO>(), // todo: parse costs
                        outcomes = outcomes
                    };
                }
                events[i] = new EventDataSO
                {
                    id = src.id,
                    name = src.name,
                    text = src.text,
                    type = ParseEnum<Athanor.Core.EventType>(src.type),
                    minDepth = src.minDepth,
                    maxDepth = src.maxDepth,
                    weight = src.weight,
                    options = options
                };
            }
            config.SetEvents(events);

            if (root.rarityWeights != null)
                config.rarityWeights = root.rarityWeights;

            // Save asset
            UnityEditor.AssetDatabase.CreateAsset(config, ASSET_PATH);
            UnityEditor.AssetDatabase.SaveAssets();

            Debug.Log("[TempImport] Imported content.json → " + ASSET_PATH);
        }

        static T ParseEnum<T>(string s) where T : struct
        {
            if (Enum.TryParse(s, ignoreCase: true, out T result)) return result;
            return default;
        }

        // ── Temporary JSON DTOs (matched to content.json structure) ─────────────
        [Serializable]
        private class JsonRoot
        {
            public JsonRules rules;
            public JsonMaterial[] materials;
            public JsonRecipe[] recipes;
            public JsonCatalyst[] catalysts;
            public int[] depthTargets;
            public string[] depthNames;
            public string ultimate;
            public JsonGoal[] goals;
            public JsonAspect[] aspects;
            public JsonEvent[] events;
            public int? noEventWeight;
            public int[] rarityWeights;
        }

        [Serializable]
        private class JsonRules
        {
            public int handMin, handBase, trials, trialsFinalDepth, redraws;
            public int starterCopies, offerSize, rackSize, maxRi, pouchCards, firstReactionMult;
            public int[] aspectDepths;
            public long[] potencyByTier;
        }

        [Serializable]
        private class JsonMaterial
        {
            public string id, name, kind, glow, rarity;
            public int tier, reactivity;
            public bool locked;
        }

        [Serializable]
        private class JsonRecipe
        {
            public string[] items;
            public string a, b;
            public int minReactivity;
            public string result;
        }

        [Serializable]
        private class JsonCatalyst
        {
            public string id, name, text, effect, kind, rarity;
            public float amount;
            public bool locked;
        }

        [Serializable]
        private class JsonGoal
        {
            public string id, text, stat, rewardKind, rewardRarity;
            public int target;
        }

        [Serializable]
        private class JsonAspect
        {
            public string id, name, text, effect, scope, group;
            public float amount;
            public bool tradeOff;
        }

        [Serializable]
        private class JsonEvent
        {
            public string id, name, text, type;
            public int minDepth, maxDepth, weight;
            public JsonEventOption[] options;
        }

        [Serializable]
        private class JsonEventOption
        {
            public string label, requires;
            public JsonEffect[] cost;
            public JsonOutcome[] outcomes;
        }

        [Serializable]
        private class JsonOutcome
        {
            public int weight;
            public string text;
            public JsonEffect[] gain;
        }

        [Serializable]
        private class JsonEffect
        {
            public string op, arg;
            public int amount, amount2;
        }
    }
}
