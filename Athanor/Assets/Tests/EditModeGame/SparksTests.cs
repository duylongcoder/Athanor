using Athanor.Game;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>Sparks on batched meshes: same motion and fade as the old SpriteRenderer pool, whole-pixel centres, HDR kept, no garbage.</summary>
    public class SparksTests
    {
        ArtLibrary libBefore;
        GameObject go;

        [SetUp] public void Keep()
        {
            libBefore = Make.Lib;
            Make.Lib = AssetDatabase.LoadAssetAtPath<ArtLibrary>("Assets/Art/ArtLibrary.asset");
            go = new GameObject("sparks test");
        }

        [TearDown] public void Restore()
        {
            Object.DestroyImmediate(go);
            Make.Lib = libBefore;
        }

        // the integrator of the SpriteRenderer pool, copied as it was before the change
        static void OldStep(ref Vector2 pos, ref Vector2 vel, float gravity, float drag, float dt)
        {
            vel.y -= gravity * dt;
            if (drag > 0) vel *= Mathf.Max(0, 1 - drag * dt);
            pos += vel * dt;
        }

        [Test]
        public void Step_MatchesTheOldPool_PositionAndFade()
        {
            var p = new Sparks.Particle { pos = new Vector2(3, 4), vel = new Vector2(40, 90), color = new Color(2, 1, 0.5f, 0.8f), life = 1, maxLife = 1, gravity = 40, drag = 1.5f };
            Vector2 pos = p.pos, vel = p.vel;
            float life = 1;
            for (int f = 0; f < 30; f++)
            {
                Assert.IsTrue(Sparks.Step(ref p, 1 / 60f));
                OldStep(ref pos, ref vel, 40, 1.5f, 1 / 60f); life -= 1 / 60f;
            }
            Assert.AreEqual(Mathf.Round(pos.x), Mathf.Round(p.pos.x));
            Assert.AreEqual(Mathf.Round(pos.y), Mathf.Round(p.pos.y));
            Assert.AreEqual(0.8f * Mathf.Clamp01(life * 2), Sparks.Shade(p).a, 2 / 255f);
            Assert.AreEqual(2, Sparks.Shade(p).r, 1e-6f, "the colour itself is not touched, only alpha");
        }

        [Test]
        public void Step_Dies_WhenItsLifeIsOver()
        {
            var p = new Sparks.Particle { life = 0.05f, maxLife = 0.05f, color = Color.white };
            Assert.IsTrue(Sparks.Step(ref p, 0.03f));
            Assert.IsFalse(Sparks.Step(ref p, 0.03f));
        }

        [TestCase(30), TestCase(60), TestCase(144), TestCase(240)]
        public void Trail_EmitsTheSameDensity_AtAnyFrameRate(int hz)
        {
            int n = 0, frames = hz * 20;
            for (int f = 0; f < frames; f++) n += Sparks.TrailCount(1f / hz, (f * 0.6180339887f) % 1f);   // evenly spread r: deterministic
            Assert.AreEqual(Sparks.TrailRate * 20, n, Sparks.TrailRate * 20 * 0.03f);
        }

        [Test]
        public void Trail_IsOnePerFrame_At60Hz()
        {
            for (float r = 0.001f; r < 1; r += 0.05f) Assert.AreEqual(1, Sparks.TrailCount(1 / 60f + 1e-6f, r));
        }

        [Test]
        public void SparkMesh_SnapsCentres_ToWholePixels_AndKeepsHdrColours()
        {
            var m = SparkMesh.Create(go.transform, "test", 150, Make.Lib.Sprite("dot"), Make.Lib.Sprite("sparkle"));
            m.Begin();
            m.Add(new Vector2(10.4f, -3.6f), new Color(2.5f, 1.2f, 0.4f, 0.7f), false);
            m.Add(new Vector2(0.6f, 0), new Color(3, 3, 3, 1), true);
            m.End();
            var v = m.Mesh.vertices;
            var c = m.Mesh.colors;
            var dot = Make.Lib.Sprite("dot").bounds;
            Assert.AreEqual(1, (dot.max.x - dot.min.x) * SparkMesh.DotScale, 1e-5f, "a dot is 1 px");
            Assert.AreEqual(10 + dot.min.x * SparkMesh.DotScale, v[0].x, 1e-5f, "around the rounded centre (10, -4)");
            Assert.AreEqual(-4 + dot.min.y * SparkMesh.DotScale, v[0].y, 1e-5f);
            Assert.AreEqual(10 + dot.max.x * SparkMesh.DotScale, v[2].x, 1e-5f);
            var sparkle = Make.Lib.Sprite("sparkle").bounds;
            Assert.AreEqual(1 + sparkle.min.x, v[4].x, 1e-5f, "a sparkle is centred on the rounded centre (1, 0) at full size");
            Assert.AreEqual(1 + sparkle.max.x, v[6].x, 1e-5f);
            Assert.AreEqual(2.5f, c[0].r, 1e-5f, "colours above 1 survive, so the glow still crosses the bloom threshold");
            Assert.AreEqual(3f, c[4].g, 1e-5f);
            Assert.AreEqual(0.7f, c[1].a, 1e-5f);
            Assert.AreEqual(150, m.GetComponent<MeshRenderer>().sortingOrder);
            Assert.AreEqual(6, m.Mesh.GetSubMesh(0).indexCount);
            Assert.AreEqual(6, m.Mesh.GetSubMesh(1).indexCount);
        }

        [Test]
        public void Sparks_UsesOneLanePerSortingOrder_AndClearEmptiesThem()
        {
            var s = go.AddComponent<Sparks>();
            s.Burst(Vector2.zero, 90, new Color(2.2f, 1.7f, 0.6f), 220, 1.4f, 426, 30);
            for (int i = 0; i < 20; i++) s.Emit(new Vector2(i, 0), Vector2.up, Color.white, 0.5f, 151, 0, i % 2 == 0);
            Assert.AreEqual(110, s.LiveCount);
            Assert.AreEqual(2, s.LaneCount);
            s.Draw();
            Assert.AreEqual(110 * 4, s.LaneMesh(426).Count * 4 + s.LaneMesh(151).Count * 4);
            s.Advance(0.6f);
            Assert.AreEqual(90, s.LiveCount, "the 0.5 s ones are gone, the burst (0.84 s or more) is not");
            s.Clear();
            Assert.AreEqual(0, s.LiveCount);
            s.Draw();
            Assert.IsFalse(s.LaneMesh(426).GetComponent<MeshRenderer>().enabled, "an empty lane draws nothing");
        }

        [Test]
        public void Sparks_SteadyState_AllocatesNothing()
        {
            var s = go.AddComponent<Sparks>();
            for (int f = 0; f < 120; f++) { s.Burst(Vector2.zero, 20, Color.white, 100, 0.5f, 150, 40); s.Advance(1 / 60f); s.Draw(); }  // warm up: lanes grown
            long before = System.GC.GetAllocatedBytesForCurrentThread();
            for (int f = 0; f < 120; f++) { s.Burst(Vector2.zero, 20, Color.white, 100, 0.5f, 150, 40); s.Advance(1 / 60f); s.Draw(); }
            Assert.AreEqual(0, System.GC.GetAllocatedBytesForCurrentThread() - before);
        }
    }
}
