using System.Linq;
using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>A combo of 2 to 5 materials (unordered, at most 2 of one) that makes result.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Recipe", fileName = "new_recipe")]
    public sealed class RecipeDefinition : ScriptableObject
    {
        public MaterialDefinition[] items = new MaterialDefinition[0];
        [Tooltip("Total Reaction Index the hub and its company must reach.")]
        [Min(0)] public int minReactivity;
        public MaterialDefinition result;

        /// <summary>The plain data the rules read.</summary>
        public RecipeData ToData() => new RecipeData
        {
            items = items.Select(i => i != null ? i.id : null).ToArray(),
            minReactivity = minReactivity,
            result = result != null ? result.id : null,
        };
    }
}
