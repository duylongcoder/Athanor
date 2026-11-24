using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>A material. The asset's file name is its id; the id is what saves store, so never change it after release.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Material", fileName = "new_material")]
    public sealed class MaterialDefinition : ScriptableObject
    {
        [Tooltip("Save id (journal, profile, icons in tools/art/icons.mjs). Never change it after release.")]
        public string id = "";
        [Tooltip("English name; Vietnamese keeps item names in English.")]
        public string displayName = "";
        public Kind kind;
        [Tooltip("0 = starter. Base potency is GameRules.potencyByTier[tier].")]
        [Min(0)] public int tier;
        [Range(ContentDb.MinReactivity, ContentDb.MaxReactivity)] public int reactivity = ContentDb.MinReactivity;
        [Tooltip("The glow of the material's tile and sparks.")]
        public Color glow = Color.white;
        [Tooltip("Off writes no glow at all (an empty glow): the tile and sparks show none. On uses the colour above.")]
        public bool hasGlow = true;
        [Tooltip("Starters only (tier 0): Common, Uncommon or Rare. Leave Unset for every other material.")]
        public RarityId rarity;
        [Tooltip("Starters only: must be won from a goal.")]
        public bool locked;

        /// <summary>The plain data the rules read.</summary>
        public MaterialData ToData() => new MaterialData
        {
            id = id, name = displayName, kind = kind.ToString(), tier = tier, reactivity = reactivity,
            glow = hasGlow ? "#" + ColorUtility.ToHtmlStringRGB(glow).ToLowerInvariant() : "", rarity = ContentNames.Rarity(rarity), locked = locked,
        };
    }
}
