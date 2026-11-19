using System;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>One colour per rarity, for code that switches on the rarity.</summary>
    [Serializable]
    public struct RarityColors
    {
        public Color common, uncommon, rare, legendary;
    }

    /// <summary>
    /// The colours two or more files share, by role (Assets/Data/Palette.asset). A colour only one file uses stays on that file's view.
    /// GameFlow publishes it as <see cref="Art.Palette"/>.
    /// </summary>
    [CreateAssetMenu(menuName = "Athanor/Palette")]
    public sealed class Palette : ScriptableObject
    {
        [Header("Rarity")]
        /// <summary>Rarity word and stars on a dark panel.</summary>
        public RarityColors rarityText;
        /// <summary>Rarity word on parchment, and the catalyst strip.</summary>
        public RarityColors rarityTone;

        [Header("Aspects")]
        public Color gold;
        public Color anchor, warm, echo, attuned, seed, fallow, heavy, cold, volatileTile, kiln, pivot;

        [Header("Shared")]
        /// <summary>Glow of a material that has none.</summary>
        public Color defaultGlow;
        /// <summary>Ink, faded ink and teal of the parchment pages (Journal, Goals, choice cards).</summary>
        public Color ink, faded, teal;
        /// <summary>Resting border of the pause button and the title's cells.</summary>
        public Color edge;
        /// <summary>Reactivity pips and choice tiles: lit and unlit.</summary>
        public Color pipOn, pipOff;
        /// <summary>Label of an enabled button.</summary>
        public Color buttonText;
        /// <summary>The dark outline around the rarity stars.</summary>
        public Color starOutline;
    }
}
