using System;
using Athanor.Core;

namespace Athanor.Data
{
    /// <summary>A single option within an event.</summary>
    [Serializable]
    public class EventOptionSO
    {
        /// <summary>Display label for the option (localized).</summary>
        public string label = "";

        /// <summary>Requirement needed to select this option (e.g. "Rack", "Catalyst", empty = no requirement).</summary>
        public EventNeedType requires = EventNeedType.None;

        /// <summary>Cost effects applied when selecting this option.</summary>
        public EffectEntrySO[] cost = Array.Empty<EffectEntrySO>();

        /// <summary>Possible outcomes drawn by weight when this option is selected.</summary>
        public EventOutcomeSO[] outcomes = Array.Empty<EventOutcomeSO>();
    }
}
