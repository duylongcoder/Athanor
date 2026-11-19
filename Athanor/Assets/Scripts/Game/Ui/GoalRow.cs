using UnityEngine;

namespace Athanor.Game
{
    /// <summary>One row of the Goals page: the goal and its count, then the reward and its progress bar.</summary>
    public sealed class GoalRow : MonoBehaviour
    {
        public Label text, count, reward;
        public SpriteRenderer track, fill, icon;
    }
}
