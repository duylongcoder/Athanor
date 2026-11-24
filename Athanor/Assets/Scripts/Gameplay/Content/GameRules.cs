using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>The run's tuning numbers (was the "rules" block of the content assets) and the depth table.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Game Rules", fileName = "GameRules")]
    public sealed class GameRules : ScriptableObject
    {
        [Header("Hand and trials")]
        [Tooltip("Smallest hand size.")] public int handMin;
        [Tooltip("Hand size a run starts with.")] public int handBase;
        [Tooltip("Trials per depth.")] public int trials;
        [Tooltip("Trials at the final depth.")] public int trialsFinalDepth;
        [Tooltip("Redraws per run.")] public int redraws;
        [Header("Deck, rack, offers")]
        [Tooltip("Copies of each starter material in the bench deck.")] public int starterCopies;
        [Tooltip("Catalyst cards offered after a depth.")] public int offerSize;
        [Tooltip("Bench rack slots.")] public int rackSize;
        [Tooltip("Cards the pouch adds to the hand.")] public int pouchCards;
        [Header("Scoring")]
        [Tooltip("Ceiling of a material's Reaction Index on a tile.")] public int maxRi;
        [Tooltip("Multiplier on the first reaction of a trial.")] public int firstReactionMult;
        [Tooltip("Base potency by material tier; index is the tier.")] public long[] potencyByTier = new long[0];
        [Tooltip("Depths (1-based) at which the run offers an aspect.")] public int[] aspectDepths = new int[0];
        [Header("Depths")]
        [Tooltip("Potency to clear each depth but the last (one fewer than depthNames).")] public int[] depthTargets = new int[0];
        [Tooltip("One name per depth; the count is the final depth.")] public string[] depthNames = new string[0];
        [Tooltip("Making this material wins the run.")] public MaterialDefinition ultimate;
        [Header("Draws")]
        [Tooltip("Weight of 'no event' against the events' own weights.")] public int noEventWeight;
        [Tooltip("Empty = every rarity draws equally (owner decision 2026-11-15). Otherwise one weight per rarity, Common first.")]
        public int[] rarityWeights = new int[0];

        /// <summary>The plain rules block, with its own copies of the arrays.</summary>
        public RulesData ToData() => new RulesData
        {
            handMin = handMin, handBase = handBase, trials = trials, trialsFinalDepth = trialsFinalDepth, redraws = redraws,
            starterCopies = starterCopies, offerSize = offerSize, rackSize = rackSize, pouchCards = pouchCards, maxRi = maxRi,
            firstReactionMult = firstReactionMult, aspectDepths = (int[])aspectDepths.Clone(), potencyByTier = (long[])potencyByTier.Clone(),
        };
    }
}
