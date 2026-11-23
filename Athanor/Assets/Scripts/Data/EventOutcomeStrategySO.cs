using System;
using Athanor.Core;

namespace Athanor.Data
{
    // ── Base class for polymorphic event outcomes ─────────────────────────────
    /// <summary>Base type for all event outcome strategy classes.</summary>
    [Serializable]
    public abstract class EventOutcomeSO
    {
        /// <summary>Draw weight relative to other outcomes in this option.</summary>
        public int weight = 1;
        /// <summary>Flavor text shown when this outcome triggers.</summary>
        public string text = "";
    }

    // ── Standard outcome: applies effect gains ────────────────────────────────
    [Serializable]
    public class StandardEffectOutcomeSO : EventOutcomeSO
    {
        /// <summary>List of effects applied when this outcome fires.</summary>
        public EffectEntrySO[] gain = Array.Empty<EffectEntrySO>();
    }

    // ── No-op outcome placeholder ─────────────────────────────────────────────
    [Serializable]
    public class NothingOutcomeSO : EventOutcomeSO
    {
        // intentionally empty — used when an outcome has no effects
    }
}
