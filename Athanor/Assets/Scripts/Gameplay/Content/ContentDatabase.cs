using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>
    /// The game's content: every definition asset in the order the rules see it (the order matters: offers, draws and
    /// "the order of the file" tie-breaks follow it). ToContentData builds the plain data ContentDb validates.
    /// </summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Content Database", fileName = "ContentDatabase")]
    public sealed class ContentDatabase : ScriptableObject
    {
        public GameRules rules;
        public List<MaterialDefinition> materials = new List<MaterialDefinition>();
        public List<RecipeDefinition> recipes = new List<RecipeDefinition>();
        public List<CatalystDefinition> catalysts = new List<CatalystDefinition>();
        public List<AspectDefinition> aspects = new List<AspectDefinition>();
        public List<EventDefinition> events = new List<EventDefinition>();
        public List<GoalDefinition> goals = new List<GoalDefinition>();

        /// <summary>A fresh copy of the content as the rules read it. Throws when rules is not set.</summary>
        public ContentData ToContentData()
        {
            if (rules == null) throw new System.InvalidOperationException(name + ": rules (GameRules) is not set");
            return new ContentData
            {
                rules = rules.ToData(),
                materials = materials.Select(m => m.ToData()).ToArray(),
                recipes = recipes.Select(r => r.ToData()).ToArray(),
                catalysts = catalysts.Select(c => c.ToData()).ToArray(),
                aspects = aspects.Select(a => a.ToData()).ToArray(),
                events = events.Select(e => e.ToData()).ToArray(),
                goals = goals.Select(g => g.ToData()).ToArray(),
                depthTargets = (int[])rules.depthTargets.Clone(),
                depthNames = (string[])rules.depthNames.Clone(),
                ultimate = rules.ultimate != null ? rules.ultimate.id : "",
                noEventWeight = rules.noEventWeight,
                rarityWeights = rules.rarityWeights != null && rules.rarityWeights.Length > 0 ? (int[])rules.rarityWeights.Clone() : null,
            };
        }
    }
}
