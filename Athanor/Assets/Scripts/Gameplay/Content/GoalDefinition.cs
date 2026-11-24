using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>A long-term goal: reach target on a stat, win one random locked item of rewardKind and rewardRarity.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Goal", fileName = "new_goal")]
    public sealed class GoalDefinition : ScriptableObject
    {
        public string id = "";
        [TextArea(1, 3)] public string text = "";
        public GoalStat stat;
        [Min(1)] public int target = 1;
        public RewardKind rewardKind;
        [Tooltip("Uncommon or above; a starter reward is at most Rare.")] public RarityId rewardRarity = RarityId.Uncommon;

        /// <summary>The plain data the rules read.</summary>
        public GoalData ToData() => new GoalData
        {
            id = id, text = text, stat = ContentNames.Stat(stat), target = target,
            rewardKind = ContentNames.Reward(rewardKind), rewardRarity = ContentNames.Rarity(rewardRarity),
        };
    }
}
