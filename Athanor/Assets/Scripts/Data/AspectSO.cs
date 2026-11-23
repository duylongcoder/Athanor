using System;

namespace Athanor.Data
{
    /// <summary>An aspect definition that modifies bench behaviour.</summary>
    [Serializable]
    public class AspectSO
    {
        /// <summary>Unique identifier (e.g. "doubled").</summary>
        public string id = "";

        /// <summary>Display name shown to the player (localized).</summary>
        public string name = "";

        /// <summary>Flavor text describing the aspect's effect.</summary>
        public string text = "";

        /// <summary>The effect type applied to the bench.</summary>
        public string effect = "";

        /// <summary>Scope of the effect: Tile, Link, or Bench.</summary>
        public AspectScope scope = AspectScope.Tile;

        /// <summary>Flavour group label.</summary>
        public AspectGroup group = AspectGroup.Cheap;

        /// <summary>Potency multiplier (positive for scaling effects like Heated/Seed).</summary>
        public float amount = 0f;

        /// <summary>Whether this aspect has a trade-off (negative side effect).</summary>
        public bool tradeOff = false;
    }
}
