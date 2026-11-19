using System;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>One serialized spark burst: what <see cref="Sparks.Burst"/> takes besides the position.</summary>
    [Serializable]
    public struct BurstSpec
    {
        public int count;
        public Color color;
        public float speed, life;
        public int order;
        public float gravity;

        public BurstSpec(int count, Color color, float speed, float life, int order, float gravity = 0)
        {
            this.count = count; this.color = color; this.speed = speed; this.life = life; this.order = order; this.gravity = gravity;
        }

        /// <summary>Throws the burst at pos.</summary>
        public void Play(Sparks sparks, Vector2 pos) => sparks.Burst(pos, count, color, speed, life, order, gravity);
    }
}
