using UnityEngine;

namespace Athanor.Game
{
    /// <summary>One exported effect: its frames in order and how long each is shown.</summary>
    public sealed class FlipbookSheet : ScriptableObject
    {
        public Sprite[] frames;
        public float[] durations;   // seconds per frame
        public bool loop;
    }
}
