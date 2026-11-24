using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>A catalyst card: a run-long scoring effect chosen after a depth.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Catalyst", fileName = "new_catalyst")]
    public sealed class CatalystDefinition : ScriptableObject
    {
        [Tooltip("Save id; icon cat_<id> in tools/art/icons.mjs.")] public string id = "";
        public string displayName = "";
        [TextArea(2, 4)] public string text = "";
        public CatalystEffect effect;
        [Tooltip("KindDouble only: the kind it doubles.")] public KindOrNone kind;
        public float amount;
        public RarityId rarity = RarityId.Common;
        [Tooltip("Must be won from a goal (not Common).")] public bool locked;

        /// <summary>The plain data the rules read.</summary>
        public CatalystData ToData() => new CatalystData
        {
            id = id, name = displayName, text = text, effect = effect.ToString(), kind = ContentNames.Kind(kind),
            amount = amount, rarity = ContentNames.Rarity(rarity), locked = locked,
        };
    }
}
