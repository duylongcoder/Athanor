using System;
using Athanor.Core;

namespace Athanor.Data
{
    /// <summary>A single material definition for the game.</summary>
    [Serializable]
    public class MaterialSO
    {
        /// <summary>Unique identifier (e.g. "ember_coal").</summary>
        public string id = "";

        /// <summary>Display name shown to the player (localized).</summary>
        public string name = "";

        /// <summary>Material kind.</summary>
        public Kind kind;

        /// <summary>Tier: 0 = starter material, higher = deeper.</summary>
        public int tier = 0;

        /// <summary>Base reactivity value used in scoring.</summary>
        public int reactivity = 0;

        /// <summary>Hue/saturation override as hex color (e.g. "#ff6a1e"). Empty = default.</summary>
        public string glow = "";

        /// <summary>Rarity of this material — applies mainly to starting materials.</summary>
        public Rarity rarity = Rarity.Common;

        /// <summary>Whether this material is locked behind a goal reward.</summary>
        public bool locked = false;
    }
}
