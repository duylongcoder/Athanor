using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// Glowing pixel particles: bursts, trails and rising embers. Particles are plain structs integrated here; each
    /// sorting order is one lane drawn by one SparkMesh (one renderer, at most two draws), so a burst of 90 costs one
    /// object instead of 90. Allocates only when a lane grows past its most particles so far.
    /// Building with ATHANOR_SPARK_PARTICLES draws through ParticleSystems instead (kept to measure the engine
    /// module's size; see docs/devlogs/phase-4-assets.md).
    /// </summary>
    public sealed class Sparks : MonoBehaviour
    {
        public static Sparks I { get; private set; }

        /// <summary>Trail emitters call Trail once per frame; it emits this many particles per second, whatever the frame rate.</summary>
        public const float TrailRate = 60;

        public struct Particle
        {
            public Vector2 pos, vel; public float life, maxLife, gravity, drag; public Color color; public bool sparkle;
        }

        sealed class Lane
        {
            public int order; public Particle[] ps = new Particle[16]; public int count; public SparkMesh mesh;
        }

        readonly Dictionary<int, Lane> laneByOrder = new Dictionary<int, Lane>();
        readonly List<Lane> lanes = new List<Lane>();

        void Awake() => I = this;

        /// <summary>Particles alive now, over all lanes.</summary>
        public int LiveCount
        {
            get
            {
#if ATHANOR_SPARK_PARTICLES
                return particleLanes.LiveCount;
#else
                int n = 0;
                foreach (var l in lanes) n += l.count;
                return n;
#endif
            }
        }

        /// <summary>Objects drawing sparks (one per sorting order used so far).</summary>
        public int LaneCount => lanes.Count;

        /// <summary>The mesh drawing one sorting order, or null if nothing was emitted at it yet (for tests).</summary>
        public SparkMesh LaneMesh(int order) => laneByOrder.TryGetValue(order, out var l) ? l.mesh : null;

        /// <summary>One particle. gravity pulls down (negative floats up); sparkle uses the cross-shaped sprite.</summary>
        public void Emit(Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity = 0, bool sparkle = false, float drag = 0)
        {
            if (life <= 0) return;
#if ATHANOR_SPARK_PARTICLES
            particleLanes.Emit(this, pos, vel, color, life, order, gravity, sparkle, drag);
#else
            var l = LaneFor(order);
            if (l.count == l.ps.Length) System.Array.Resize(ref l.ps, l.ps.Length * 2);
            l.ps[l.count++] = new Particle { pos = pos, vel = vel, color = color, life = life, maxLife = life, gravity = gravity, drag = drag, sparkle = sparkle };
#endif
        }

        /// <summary>
        /// A trail behind something moving, called once per frame: the same density at 30, 60 or 144 Hz
        /// (TrailRate per second; exactly one per frame at 60 Hz). Extra particles of a long frame start part-way
        /// through their life, so they spread out as if emitted during the frame.
        /// </summary>
        public void Trail(Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity = 0, bool sparkle = false, float drag = 0)
        {
            float dt = Time.deltaTime;
            int n = TrailCount(dt, Random.value);
            for (int k = 0; k < n; k++)
            {
                float age = k == 0 ? 0 : Random.value * dt;
                Emit(pos + vel * age, vel, color, life - age, order, gravity, sparkle, drag);
            }
        }

        /// <summary>How many trail particles a frame of dt seconds emits; r is uniform in [0, 1).</summary>
        public static int TrailCount(float dt, float r) => Mathf.FloorToInt(dt * TrailRate + r);

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
#if ATHANOR_SPARK_PARTICLES
            particleLanes.Clear();
#endif
            foreach (var l in lanes) l.count = 0;
        }

        /// <summary>Advances one particle by dt; false once it has died. The integrator the SpriteRenderer pool used.</summary>
        public static bool Step(ref Particle p, float dt)
        {
            p.life -= dt;
            if (p.life <= 0) return false;
            p.vel.y -= p.gravity * dt;
            if (p.drag > 0) p.vel *= Mathf.Max(0, 1 - p.drag * dt);
            p.pos += p.vel * dt;
            return true;
        }

        /// <summary>Its colour this frame: full until half its life is gone, then fading out.</summary>
        public static Color Shade(in Particle p) => p.color.Alpha(p.color.a * Mathf.Clamp01(p.life / p.maxLife * 2));

        Lane LaneFor(int order)
        {
            if (laneByOrder.TryGetValue(order, out var l)) return l;
            l = new Lane { order = order, mesh = SparkMesh.Create(transform, "sparks " + order, order, Make.Lib.Sprite("dot"), Make.Lib.Sprite("sparkle")) };
            laneByOrder[order] = l;
            lanes.Add(l);
            return l;
        }

        void Update() => Advance(Time.deltaTime);

        // drawn after the coroutines, so a particle emitted this frame shows at once where it was emitted
        void LateUpdate() => Draw();

        /// <summary>Moves every particle on by dt and drops the dead (Update calls it; public for tests).</summary>
        public void Advance(float dt)
        {
            foreach (var l in lanes)
            {
                var ps = l.ps;
                for (int i = l.count - 1; i >= 0; i--)
                    if (!Step(ref ps[i], dt)) ps[i] = ps[--l.count];     // order inside a lane does not matter: the blend is additive
            }
        }

        /// <summary>Uploads every lane's mesh (LateUpdate calls it; public for tests).</summary>
        public void Draw()
        {
            foreach (var l in lanes)
            {
                var m = l.mesh;
                m.Begin();
                for (int i = 0; i < l.count; i++) m.Add(l.ps[i].pos, Shade(l.ps[i]), l.ps[i].sparkle);
                m.End();
            }
        }

#if ATHANOR_SPARK_PARTICLES
        readonly SparkParticles particleLanes = new SparkParticles();
#endif
    }
}
