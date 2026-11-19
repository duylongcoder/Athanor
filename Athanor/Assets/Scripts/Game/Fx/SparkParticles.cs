#if ATHANOR_SPARK_PARTICLES
using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// Sparks drawn by ParticleSystems (docs/ATHANOR.md §2), only compiled with
    /// ATHANOR_SPARK_PARTICLES. Not shipped: it pulls the ParticleSystem engine module into the wasm, which was the
    /// thing measured. One system per (order, gravity, drag, sparkle). Colours are divided by K and multiplied back
    /// by the material, because particle colours are 8 bit.
    /// </summary>
    sealed class SparkParticles
    {
        public const float K = 3;

        readonly Dictionary<(int, float, float, bool), ParticleSystem> lanes = new Dictionary<(int, float, float, bool), ParticleSystem>();
        readonly List<ParticleSystem> all = new List<ParticleSystem>();
        static readonly ParticleSystemVertexStream[] Streams =
        {
            ParticleSystemVertexStream.Position, ParticleSystemVertexStream.Color, ParticleSystemVertexStream.UV, ParticleSystemVertexStream.Center,
        };

        public int LiveCount { get { int n = 0; foreach (var p in all) n += p.particleCount; return n; } }

        public void Clear() { foreach (var p in all) p.Clear(); }

        public void Emit(Sparks owner, Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity, bool sparkle, float drag)
        {
            var key = (order, gravity, drag, sparkle);
            if (!lanes.TryGetValue(key, out var ps)) { ps = Make(owner.transform, key); lanes[key] = ps; all.Add(ps); }
            var c = new Color(color.r / K, color.g / K, color.b / K, color.a);
            ps.Emit(new ParticleSystem.EmitParams
            {
                position = pos, velocity = vel, startLifetime = life, startColor = c, startSize = sparkle ? 5 : 1, applyShapeToPosition = false,
            }, 1);
        }

        static ParticleSystem Make(Transform parent, (int order, float gravity, float drag, bool sparkle) k)
        {
            var go = new GameObject("spark lane " + k.order);
            go.transform.SetParent(parent, false);
            var ps = go.AddComponent<ParticleSystem>();
            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            var main = ps.main;
            main.loop = false; main.playOnAwake = false; main.simulationSpace = ParticleSystemSimulationSpace.World;
            main.startSpeed = 0; main.gravityModifier = 0; main.maxParticles = 1024; main.startRotation = 0;
            main.scalingMode = ParticleSystemScalingMode.Hierarchy; main.duration = 1;
            var emission = ps.emission; emission.enabled = false;
            var shape = ps.shape; shape.enabled = false;
            if (k.gravity != 0)
            {
                var f = ps.forceOverLifetime; f.enabled = true; f.space = ParticleSystemSimulationSpace.World;
                f.x = new ParticleSystem.MinMaxCurve(0); f.y = new ParticleSystem.MinMaxCurve(-k.gravity); f.z = new ParticleSystem.MinMaxCurve(0);
            }
            if (k.drag > 0)
            {
                var lv = ps.limitVelocityOverLifetime; lv.enabled = true; lv.limit = 1e6f; lv.drag = k.drag;
                lv.multiplyDragByParticleSize = false; lv.multiplyDragByParticleVelocity = false;
            }
            var col = ps.colorOverLifetime; col.enabled = true;
            var g = new Gradient();
            g.SetKeys(new[] { new GradientColorKey(Color.white, 0), new GradientColorKey(Color.white, 1) },
                new[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(1, 0.5f), new GradientAlphaKey(0, 1) });
            col.color = g;
            var r = go.GetComponent<ParticleSystemRenderer>();
            r.renderMode = ParticleSystemRenderMode.Billboard; r.alignment = ParticleSystemRenderSpace.View;
            r.sortingOrder = k.order;
            var sprite = Athanor.Game.Art.Lib.Sprite(k.sparkle ? "sparkle" : "dot");
            var m = new Material(SparkMesh.MaterialFor(sprite.texture));
            m.SetFloat("_Intensity", K); m.EnableKeyword("CENTER_SNAP");
            r.sharedMaterial = m;
            r.SetActiveVertexStreams(new List<ParticleSystemVertexStream>(Streams));
            ps.Play();
            return ps;
        }
    }
}
#endif
