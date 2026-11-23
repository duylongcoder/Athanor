using System;
using Athanor.Core;

namespace Athanor.Data
{
    /// <summary>A single effect entry inside an event outcome or cost line.</summary>
    [Serializable]
    public class EffectEntrySO
    {
        /// <summary>The operation performed (e.g., Trials, Hand, GiveDiscovery).</summary>
        public EffectOp op;

        /// <summary>Target argument — usually null for simple ops; holds the argument string where needed.</summary>
        public string arg = "";

        /// <summary>Primary amount for the effect.</summary>
        public int amount = 0;

        /// <summary>Secondary amount reserved for future use.</summary>
        public int amount2 = 0;
    }
}
