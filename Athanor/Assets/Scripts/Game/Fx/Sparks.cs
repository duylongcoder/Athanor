using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>A pool of glowing pixel particles: bursts, trails and rising embers.</summary>
    public sealed class Sparks : MonoBehaviour
    {
        public static Sparks I { get; private set; }

        sealed class Particle
        {
            public SpriteRenderer r; public Vector2 pos, vel; public float life, maxLife, gravity, drag; public Color color;
        }

        readonly List<Particle> live = new List<Particle>();
        readonly Stack<Particle> spare = new Stack<Particle>();

        void Awake() => I = this;

        /// <summary>One particle. gravity pulls down (negative floats up); sparkle uses the cross-shaped sprite.</summary>
        public void Emit(Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity = 0, bool sparkle = false, float drag = 0)
        {
            Particle p;
            if (spare.Count > 0) { p = spare.Pop(); p.r.gameObject.SetActive(true); }
            else
            {
                p = new Particle { r = Make.Sprite(transform, "dot", Vector3.zero, order, Make.Lib.glow) };
                p.r.name = "spark";
            }
            p.r.sprite = Make.Lib.Sprite(sparkle ? "sparkle" : "dot");
            p.r.transform.localScale = sparkle ? Vector3.one : new Vector3(0.25f, 0.25f, 1);
            p.r.sortingOrder = order;
            p.pos = pos; p.vel = vel; p.color = color; p.life = p.maxLife = life; p.gravity = gravity; p.drag = drag;
            live.Add(p);
        }

        /// <summary>A ring of particles flying outward.</summary>
        public void Burst(Vector2 pos, int count, Color color, float speed, float life, int order, float gravity = 0, float squash = 1)
        {
            for (int i = 0; i < count; i++)
            {
                float a = Random.value * Mathf.PI * 2, s = speed * (0.35f + Random.value * 0.65f);
                Emit(pos, new Vector2(Mathf.Cos(a) * s, Mathf.Sin(a) * s * squash), color, life * (0.6f + Random.value * 0.4f), order, gravity, i % 4 == 0, 1.5f);
            }
        }

        public void Clear()
        {
            foreach (var p in live) { p.r.gameObject.SetActive(false); spare.Push(p); }
            live.Clear();
        }

        void Update()
        {
            float dt = Time.deltaTime;
            for (int i = live.Count - 1; i >= 0; i--)
            {
                var p = live[i];
                p.life -= dt;
                if (p.life <= 0) { p.r.gameObject.SetActive(false); spare.Push(p); live.RemoveAt(i); continue; }
                p.vel.y -= p.gravity * dt;
                if (p.drag > 0) p.vel *= Mathf.Max(0, 1 - p.drag * dt);
                p.pos += p.vel * dt;
                p.r.transform.position = new Vector3(Mathf.Round(p.pos.x), Mathf.Round(p.pos.y), 0);
                p.r.color = p.color.Alpha(p.color.a * Mathf.Clamp01(p.life / p.maxLife * 2));
            }
        }
    }
}
