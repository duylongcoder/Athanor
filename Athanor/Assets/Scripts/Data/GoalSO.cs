using System;

namespace Athanor.Data
{
    /// <summary>A goal definition that rewards a locked item when reached.</summary>
    [Serializable]
    public class GoalSO
    {
        /// <summary>Unique identifier (e.g. "reach_depth_3").</summary>
        public string id = "";

        /// <summary>Localized description of the goal.</summary>
        public string text = "";

        /// <summary>The stat ID this goal tracks.</summary>
        public GoalStatId stat = GoalStatId.RunsStarted;

        /// <summary>Target value to reach on the tracked stat.</summary>
        public int target = 0;

        /// <summary>Kind of item rewarded (starter material or catalyst).</summary>
        public RewardKind rewardKind = RewardKind.Starter;

        /// <summary>Rarity tier of the rewarded item.</summary>
        public Rarity rewardRarity = Rarity.Common;
    }
}
