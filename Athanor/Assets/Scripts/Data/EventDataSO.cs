using System;
using Athanor.Core;

namespace Athanor.Data
{
    /// <summary>An event definition in the game.</summary>
    [Serializable]
    public class EventDataSO
    {
        /// <summary>Unique identifier (e.g. "cracked_altar").</summary>
        public string id = "";

        /// <summary>Display name shown in UI (localized).</summary>
        public string name = "";

        /// <summary>Flavor text describing the event context.</summary>
        public string text = "";

        /// <summary>Type of event.</summary>
        public EventType type = EventType.Choice;

        /// <summary>Minimum depth at which this event can appear (1-based).</summary>
        public int minDepth = 1;

        /// <summary>Maximum depth at which this event can appear (0 = unlimited).</summary>
        public int maxDepth = 0;

        /// <summary>Base draw weight for this event (higher = more likely).</summary>
        public int weight = 1;

        /// <summary>The available options presented to the player.</summary>
        public EventOptionSO[] options = Array.Empty<EventOptionSO>();
    }
}
