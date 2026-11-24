using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>A run-long bench modifier offered at GameRules.aspectDepths.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Aspect", fileName = "new_aspect")]
    public sealed class AspectDefinition : ScriptableObject
    {
        [Tooltip("Save id; icon asp_<id> in tools/art/icons.mjs.")] public string id = "";
        public string displayName = "";
        [TextArea(2, 4)] public string text = "";
        public AspectEffect effect;
        public AspectScope scope;
        public AspectGroup group;
        [Tooltip("Must be above 0 for Heated and Seed.")] public float amount;
        [Tooltip("Cold, Volatile and Greedy are the costly ones.")] public bool tradeOff;

        /// <summary>The plain data the rules read.</summary>
        public AspectData ToData() => new AspectData
        {
            id = id, name = displayName, text = text, effect = effect.ToString(), scope = scope.ToString(),
            group = group.ToString(), amount = amount, tradeOff = tradeOff,
        };
    }
}
