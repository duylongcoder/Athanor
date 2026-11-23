using System;

namespace Athanor.Data
{
    /// <summary>A single catalyst definition for the game.</summary>
    [Serializable]
    public class CatalystSO
    {
        /// <summary>Unique identifier (e.g. "bellows").</summary>
        public string id = "";

        /// <summary>Display name shown to the player (localized).</summary>
        public string name = "";

        /// <summary>Flavor text describing what the catalyst does.</summary>
        public string text = "";

        /// <summary>Catalyst effect type.</summary>
        public string effect = "";

        /// <summary>The kind this catalyst modifies (empty = all kinds). e.g. "Ore".</summary>
        public string kind = "";

        /// <summary>Potency multiplier (amount > 0 means potency-based scaling).</summary>
        public float amount = 0f;

        /// <summary>Rarity of this catalyst.</summary>
        public Rarity rarity = Rarity.Common;

        /// <summary>Whether this catalyst is locked behind a goal reward.</summary>
        public bool locked = false;
    }
}
