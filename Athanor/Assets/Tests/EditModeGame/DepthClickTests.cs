using System.IO;
using System.Linq;
using Athanor.Game;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>
    /// Clicks on the HD-2D bench: a point of the UI camera's world goes through the world texture and the perspective world
    /// camera to the table's tilted top, and the flat point found there is what TileView.Contains tests (5b-05 step 5).
    /// </summary>
    public class DepthClickTests
    {
        // rest, and the sway's four extremes (amplitude 4: x +-4, y +-2)
        static readonly Vector2[] Sways = { Vector2.zero, new Vector2(4, 2), new Vector2(-4, 2), new Vector2(4, -2), new Vector2(-4, -2) };

        GameObject root;
        DepthRig rig;
        TileView[] tiles;
        LayoutData layout;

        [SetUp]
        public void SetUp()
        {
            Make.Lib = AssetDatabase.LoadAssetAtPath<ArtLibrary>("Assets/Art/ArtLibrary.asset");
            layout = JsonUtility.FromJson<LayoutData>(File.ReadAllText("Assets/Resources/layout.json"));
            root = new GameObject("depth click test");
            var cam = new GameObject("world camera").AddComponent<Camera>();
            cam.transform.SetParent(root.transform, false);
            rig = root.AddComponent<DepthRig>();
            rig.worldCamera = cam;
            rig.Apply();
            tiles = new TileView[layout.tiles.Length];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new GameObject("Tile " + i).AddComponent<TileView>();
                tiles[i].Build(i, layout.tiles[i], root.transform);
            }
        }

        [TearDown]
        public void TearDown() => Object.DestroyImmediate(root);

        Vector2 FaceCentre(int i) { var f = layout.tiles[i].face; return Px.Mid(f.x, f.y, f.w, f.h); }

        int[] Hits(Vector2 flat) => Enumerable.Range(0, tiles.Length).Where(i => tiles[i].Contains(flat)).ToArray();

        /// <summary>Where the world camera shows a flat table point, in the UI camera's world.</summary>
        Vector2 OnScreen(Vector2 flat)
        {
            var f = Px.H / 2f - flat.y;   // art y of the point
            var world = DepthMath.Place(flat, DepthMath.TableZ(f));
            Vector3 vp = rig.worldCamera.WorldToViewportPoint(world);
            return new Vector2((vp.x - 0.5f) * Px.W, (vp.y - 0.5f) * Px.H);
        }

        [Test]
        public void ClickOnTileCentre_AtRestAndUnderMaximumSway_HitsThatTile()
        {
            Assert.AreEqual(9, tiles.Length);
            foreach (var sway in Sways)
            {
                rig.SetView(sway, Vector2.zero);
                for (int i = 0; i < tiles.Length; i++)
                {
                    Assert.IsTrue(rig.BenchPoint(FaceCentre(i), out var flat), $"tile {i}, sway {sway}: the ray misses the table");
                    CollectionAssert.AreEqual(new[] { i }, Hits(flat), $"tile {i}, sway {sway}");
                }
            }
        }

        [Test]
        public void ClickWhereTheCameraShowsTheTileCentre_UnderSwayAndShake_FindsThatPoint()
        {
            foreach (var sway in Sways)
                foreach (var shake in new[] { Vector2.zero, new Vector2(3, -2) })
                {
                    rig.SetView(sway, shake);
                    for (int i = 0; i < tiles.Length; i++)
                    {
                        Assert.IsTrue(rig.BenchPoint(OnScreen(FaceCentre(i)), out var flat));
                        Assert.AreEqual(FaceCentre(i).x, flat.x, 0.02f, $"tile {i}, sway {sway}, shake {shake}");
                        Assert.AreEqual(FaceCentre(i).y, flat.y, 0.02f, $"tile {i}, sway {sway}, shake {shake}");
                        CollectionAssert.AreEqual(new[] { i }, Hits(flat));
                    }
                }
        }

        [Test]
        public void WorldRay_UnderSwayAndShake_IsTheWorldCamerasOwnRay()
        {
            rig.SetView(new Vector2(-4, 2), new Vector2(2, 1));
            foreach (var ui in new[] { Vector2.zero, new Vector2(-300, 170), new Vector2(250, -150) })
            {
                Ray mine = rig.WorldRay(ui), unity = rig.worldCamera.ViewportPointToRay(DepthRig.ViewportOf(ui));
                Assert.Less(Vector3.Angle(mine.direction, unity.direction), 0.01f, "ray at " + ui);
                Assert.Less(Vector3.Distance(mine.origin, unity.origin + unity.direction * (mine.origin.z - unity.origin.z) / unity.direction.z), 0.01f);
            }
        }

        [Test]
        public void Sway_KeepsTheAnchorRowStill_AndMovesTheWallLessThanAnArtPixel()
        {
            foreach (var sway in Sways)
            {
                rig.SetView(sway, Vector2.zero);
                for (float x = 100; x <= 540; x += 110)
                {
                    var p = Px.At(x, DepthMath.AnchorY);
                    var seen = OnScreen(p);
                    Assert.AreEqual(p.x, seen.x, 1e-2f, $"anchor x {x}, sway {sway}"); Assert.AreEqual(p.y, seen.y, 1e-2f);
                }
                var wall = DepthMath.Place(Px.At(60, 40), -400f);
                Vector3 vp = rig.worldCamera.WorldToViewportPoint(wall);
                var moved = new Vector2((vp.x - 0.5f) * Px.W, (vp.y - 0.5f) * Px.H) - (Vector2)Px.At(60, 40);
                float par = DepthMath.Parallax(-400f, rig.AnchorZ);
                Assert.AreEqual(sway.x * par, moved.x, 1e-2f, "the wall moves by cam * par(z)");
                Assert.AreEqual(sway.y * par, moved.y, 1e-2f);
            }
        }

        [Test]
        public void SetView_ShakeOnly_MovesEveryDepthByTheSameWholeArtPixels()
        {
            rig.SetView(Vector2.zero, new Vector2(3, -2));
            foreach (float z in new[] { -400f, -145.77f, 40f, 180f })
            {
                Vector3 vp = rig.worldCamera.WorldToViewportPoint(DepthMath.Place(Px.At(320, 180), z));
                Assert.AreEqual(3f, (vp.x - 0.5f) * Px.W - Px.At(320, 180).x, 1e-3f, "z " + z);
                Assert.AreEqual(-2f, (vp.y - 0.5f) * Px.H - Px.At(320, 180).y, 1e-3f, "z " + z);
            }
        }

        [Test]
        public void TiltShiftSigma_IsZeroInTheBand_AndRisesToTheMaximum()
        {
            Assert.AreEqual(0f, TiltShift.Sigma(145, 145, 235, 110, 1.5f));
            Assert.AreEqual(0f, TiltShift.Sigma(235, 145, 235, 110, 1.5f));
            Assert.AreEqual(1.5f, TiltShift.Sigma(20, 145, 235, 110, 1.5f), 1e-5f);
            Assert.AreEqual(1.5f, TiltShift.Sigma(350, 145, 235, 110, 1.5f), 1e-5f);
            Assert.AreEqual(0.75f, TiltShift.Sigma(145 - 55, 145, 235, 110, 1.5f), 1e-5f, "half the feather is half strength");
        }

        [Test]
        public void Show_TileReloadedFromAScene_ShowsTheMaterialAndTakesClicks()
        {
            // Arrange: Instantiate copies only serialized state, as saving DepthTest.unity and opening it again does
            var copy = Object.Instantiate(tiles[4], root.transform);

            // Act
            copy.Show(new Athanor.Core.MaterialData { id = "moondust", glow = "#7cc0f8" }, false);

            // Assert
            Assert.AreEqual("moondust", copy.Material);
            Assert.IsTrue(copy.Icon.enabled, "the material's icon shows");
            Assert.IsTrue(copy.Contains(FaceCentre(4)), "the face still takes clicks at its centre");
            Assert.IsFalse(copy.Contains(FaceCentre(0)));
        }

        [Test]
        public void FogOffset_StepsInWholeTexels_AndWraps()
        {
            Assert.AreEqual(8, FogSheet.Offset(2.1f, 4, 320));
            Assert.AreEqual(320 - 12, FogSheet.Offset(2.1f, -6, 320));
            Assert.AreEqual(0, FogSheet.Offset(80f, 4, 320));
        }
    }
}
