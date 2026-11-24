using System;
using System.Linq;
using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>One effect of an event option's cost or outcome.</summary>
    [Serializable]
    public sealed class EventEffectDef
    {
        public EventOp op;
        [Tooltip("Unused by every effect today; kept for the data shape.")] public string arg = "";
        public int amount, amount2;
        /// <summary>The plain data the rules read.</summary>
        public EffectData ToData() => new EffectData { op = op.ToString(), arg = arg ?? "", amount = amount, amount2 = amount2 };
    }

    /// <summary>One weighted outcome of an event option.</summary>
    [Serializable]
    public sealed class EventOutcomeDef
    {
        [Min(0)] public int weight = 1;
        [Tooltip("At most 52 characters.")] public string text = "";
        public EventEffectDef[] gain = new EventEffectDef[0];
        /// <summary>The plain data the rules read.</summary>
        public EventOutcome ToData() => new EventOutcome { weight = weight, text = text, gain = gain.Select(g => g.ToData()).ToArray() };
    }

    /// <summary>One choice of an event: its requirement, cost and weighted outcomes.</summary>
    [Serializable]
    public sealed class EventOptionDef
    {
        public string label = "";
        public EventNeed requires;
        public EventEffectDef[] cost = new EventEffectDef[0];
        public EventOutcomeDef[] outcomes = new EventOutcomeDef[0];
        /// <summary>The plain data the rules read.</summary>
        public EventOption ToData() => new EventOption
        {
            label = label, requires = ContentNames.Need(requires),
            cost = cost.Select(c => c.ToData()).ToArray(), outcomes = outcomes.Select(o => o.ToData()).ToArray(),
        };
    }

    /// <summary>An event drawn at the start of a depth.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Event", fileName = "new_event")]
    public sealed class EventDefinition : ScriptableObject
    {
        public string id = "";
        [Tooltip("At most 22 characters.")] public string displayName = "";
        [Tooltip("At most 56 characters.")] public string text = "";
        public EventKind type;
        [Min(1)] public int minDepth = 1;
        [Min(1)] public int maxDepth = 1;
        [Min(0)] public int weight = 1;
        public EventOptionDef[] options = new EventOptionDef[0];

        /// <summary>The plain data the rules read.</summary>
        public EventData ToData() => new EventData
        {
            id = id, name = displayName, text = text, type = type.ToString(), minDepth = minDepth, maxDepth = maxDepth, weight = weight,
            options = options.Select(o => o.ToData()).ToArray(),
        };
    }
}
