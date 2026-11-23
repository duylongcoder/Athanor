using System;
using UnityEngine;

namespace Athanor.Data
{
    /// <summary>Master ScriptableObject holding all rules/tuning numbers for a run.</summary>
    [CreateAssetMenu(fileName = "GameRulesSO.asset", menuName = "Athanor/Rules Config")]
    public class RulesSO : ScriptableObject
    {
        [Header("Hand Settings")]
        [Tooltip("Smallest hand size allowed during a trial.")]
        [Range(1, 9)] public int handMin = 4;

        [Tooltip("Starting hand size at the beginning of each trial.")]
        [Range(4, 12)] public int handBase = 6;

        [Header("Trial Settings")]
        [Tooltip("Number of trials per depth level.")]
        [Range(1, 20)] public int trials = 5;

        [Tooltip("Number of trials at the final depth (overrides trials if higher).")]
        [Range(1, 20)] public int trialsFinalDepth = 10;

        [Header("Resource Settings")]
        [Tooltip("Number of redraws available per run.")]
        [Range(0, 10)] public int redraws = 3;

        [Tooltip("Copies of each starter material placed in the bench deck.")]
        [Range(1, 10)] public int starterCopies = 2;

        [Tooltip("Number of catalyst cards offered after completing a depth.")]
        [Range(1, 10)] public int offerSize = 3;

        [Tooltip("Maximum number of catalyst cards held on the bench rack simultaneously.")]
        [Range(1, 10)] public int rackSize = 5;

        [Tooltip("Number of cards added to hand from the pouch in PouchMode > 0.")]
        [Range(0, 5)] public int pouchCards = 2;

        [Header("Scoring Settings")]
        [Tooltip("Ceiling of a material's Reaction Index on any tile.")]
        [Range(5, 30)] public int maxRi = 15;

        [Tooltip("Multiplier applied to the first reaction score of each trial.")]
        [Range(1, 5)] public int firstReactionMult = 2;

        [Header("Aspect Settings")]
        [Tooltip("1-based depths at which aspects become available (empty = never).")]
        public int[] aspectDepths = Array.Empty<int>();

        [Header("Potency Scaling")]
        [Tooltip("Base potency multiplier indexed by material tier (index 0 = tier 0).")]
        public long[] potencyByTier = new long[10];

        // ── Validation ──────────────────────────────────────────────────────────
        private void OnValidate()
        {
            // Ensure arrays are non-null
            potencyByTier ??= new long[10];
            aspectDepths ??= Array.Empty<int>();

            // Clamp ranges
            handMin = Mathf.Clamp(handMin, 1, 9);
            handBase = Mathf.Clamp(handBase, 4, 12);
            trials = Mathf.Clamp(trials, 1, 20);
            trialsFinalDepth = Mathf.Clamp(trialsFinalDepth, 1, 20);
            redraws = Mathf.Clamp(redraws, 0, 10);
            starterCopies = Mathf.Clamp(starterCopies, 1, 10);
            offerSize = Mathf.Clamp(offerSize, 1, 10);
            rackSize = Mathf.Clamp(rackSize, 1, 10);
            pouchCards = Mathf.Clamp(pouchCards, 0, 5);
            maxRi = Mathf.Clamp(maxRi, 5, 30);
            firstReactionMult = Mathf.Clamp(firstReactionMult, 1, 5);

            // Sanity checks
            if (handMin > handBase)
                Debug.LogWarning("[RulesSO] handMin (" + handMin + ") exceeds handBase (" + handBase + ").", this);
            if (trials > trialsFinalDepth)
                Debug.LogWarning("[RulesSO] trials (" + trials + ") exceeds trialsFinalDepth (" + trialsFinalDepth + ").", this);
        }
    }
}
