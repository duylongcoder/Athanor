using System.Linq;
using Athanor.Game;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>The HD-2D bench in the main scene (5b-07a): layers, the tiles' depth group, clicks through the world ray, the shake.</summary>
    public class MainDepthTests
    {
        GameObject root;
        DepthRig rig;
        TileView[] tiles;

        [SetUp]
        public void SetUp()
        {
            root = new GameObject("main depth test");
            var cam = new GameObject("world camera").AddComponent<Camera>();
            cam.transform.SetParent(root.transform, false);
            rig = root.AddComponent<DepthRig>();
            rig.worldCamera = cam;
            rig.Apply();
            var bench = ((GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Views/Bench.prefab"), root.transform)).GetComponent<BenchView>();
            tiles = new TileView[bench.TileCount];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = bench.Tile(i);
                foreach (var t in tiles[i].GetComponentsInChildren<Transform>(true)) t.gameObject.layer = DepthRig.WorldLayer;
                DepthLayer.AddGroup(tiles[i].gameObject, DepthMath.TableZ(Px.H / 2f - tiles[i].centre.y) + 0.2f).Apply(rig.distance);
            }
        }

        [TearDown]
        public void TearDown() => Object.DestroyImmediate(root);

        [Test]
        public void SpawnChildren_OfAWorldLayerParent_DrawOnTheWorldLayer()
        {
            var sprite = Spawn.Sprite(tiles[0].transform, "dot", Vector3.zero, 1);
            var text = Spawn.Root("later", tiles[0].transform);
            Assert.AreEqual(DepthRig.WorldLayer, sprite.gameObject.layer);
            Assert.AreEqual(DepthRig.WorldLayer, text.gameObject.layer);
            var loose = Spawn.Root("loose");
            Assert.AreEqual(0, loose.gameObject.layer, "no parent: the UI camera's layer");
            Object.DestroyImmediate(sprite.gameObject);
            Object.DestroyImmediate(loose.gameObject);
        }

        [Test]
        public void TileGroup_AtRest_KeepsTheFaceOnItsArtPixels()
        {
            var cam = rig.worldCamera;
            for (int i = 0; i < tiles.Length; i++)
            {
                Vector2 flat = tiles[i].FaceCentre;
                var child = tiles[i].transform.GetChild(0);
                var shown = tiles[i].transform.position;      // group at (0,0,z): children keep flat local coordinates
                Assert.AreEqual(0f, shown.x, 1e-4f);
                var world = tiles[i].transform.TransformPoint(new Vector3(flat.x, flat.y, 0));
                var vp = cam.WorldToViewportPoint(world);
                Assert.AreEqual(flat.x, (vp.x - 0.5f) * Px.W, 0.02f, "tile " + i + " x");
                Assert.AreEqual(flat.y, (vp.y - 0.5f) * Px.H, 0.02f, "tile " + i + " y");
                Assert.NotNull(child);
            }
        }

        [Test]
        public void TileIcon_MovedByLocalPosition_StaysOnTheTile()
        {
            // TileView.Update writes localPosition: below the group the icon stays in the tile's depth, not at z = 0
            var icon = tiles[4].Icon;
            icon.transform.localPosition = tiles[4].centre + new Vector3(0, 10, 0);
            Assert.AreNotEqual(0f, icon.transform.position.z, "the icon sits at the tile's depth");
        }

        [Test]
        public void ShakeFromScreenFx_MovesTheWorldPicture_AndFlatModeIgnoresIt()
        {
            var sway = root.AddComponent<CameraSway>();
            sway.external = new Vector2(3, -2);
            sway.Pose(0);
            Assert.AreEqual(3f, rig.ImageShift.x, 1e-4f);
            Assert.AreEqual(-2f, rig.ImageShift.y, 1e-4f);
            rig.Flat = true;
            sway.Pose(0);
            Assert.AreEqual(Vector2.zero, rig.ImageShift);
            Assert.AreEqual(Vector2.zero, rig.CamOffset);
            rig.Flat = false;
        }

        [Test]
        public void PointerBench_AllNineTiles_HitAtRestAtSwayExtremesAndUnderShake()
        {
            var sways = new[] { Vector2.zero, new Vector2(4, 2), new Vector2(-4, 2), new Vector2(4, -2), new Vector2(-4, -2) };
            var old = PointerInput.Rig;
            PointerInput.Use(rig);
            try
            {
                foreach (var sway in sways)
                    foreach (var shake in new[] { Vector2.zero, new Vector2(3, -2), new Vector2(-3, 2) })
                    {
                        rig.SetView(sway, shake);
                        for (int i = 0; i < tiles.Length; i++)
                        {
                            Vector2 flat = tiles[i].FaceCentre;
                            var world = DepthMath.Place(flat, DepthMath.TableZ(Px.H / 2f - flat.y));
                            Vector3 vp = rig.worldCamera.WorldToViewportPoint(world);
                            PointerInput.Resolve(new Vector2((vp.x - 0.5f) * Px.W, (vp.y - 0.5f) * Px.H));
                            var hits = Enumerable.Range(0, tiles.Length).Where(k => tiles[k].Contains(PointerInput.Bench)).ToArray();
                            CollectionAssert.AreEqual(new[] { i }, hits, $"tile {i}, sway {sway}, shake {shake}");
                        }
                    }
            }
            finally { PointerInput.Use(old); rig.SetView(Vector2.zero, Vector2.zero); }
        }

        [Test]
        public void MainScene_HasTheRigWorldAndUiCameras_AndTheStageIsWired()
        {
            var scene = EditorSceneManager.OpenScene("Assets/Scenes/Main.unity", OpenSceneMode.Additive);
            try
            {
                var flow = scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<GameFlow>(true)).Single();
                Assert.NotNull(flow.depthStage);
                var stage = flow.depthStage;
                Assert.AreEqual(1 << DepthRig.WorldLayer, stage.rig.worldCamera.cullingMask);
                Assert.AreEqual(0, stage.rig.uiCamera.cullingMask & (1 << DepthRig.WorldLayer));
                Assert.AreSame(stage.rig.uiCamera, flow.mainCamera);
                Assert.AreEqual(DepthRig.WorldLayer, flow.workshop.layer);
                Assert.AreEqual(0, flow.cutsceneRoom.layer, "cutscenes are flat, on the UI camera");
                Assert.IsTrue(stage.sway.swayOn);
            }
            finally { EditorSceneManager.CloseScene(scene, true); }
        }

        [Test]
        public void DepthMoodPresets_MainScene_MatchTheOldGameFlowLooks()
        {
            var ambient = new[] { new Color(0.52f, 0.52f, 1f), new Color(0.40f, 0.62f, 0.86f), new Color(0.62f, 0.42f, 0.95f), new Color(0.86f, 0.40f, 0.46f), new Color(0.78f, 0.74f, 0.58f) };
            var moon = new[] { 0.62f, 0.5f, 0.4f, 0.3f, 0.5f };
            // bloom threshold, bloom intensity, vignette, back fog, front fog, fog mix: DepthMood's old SPEC conversions
            var rest = new[]
            {
                new[] { 1f + (0.80f - 0.80f), 1.1f * 0.55f / 0.55f, 0.34f * 0.30f / 0.30f, 0.40f, 0.14f, 0.45f },
                new[] { 1f + (0.78f - 0.80f), 1.1f * 0.60f / 0.55f, 0.34f * 0.36f / 0.30f, 0.50f, 0.15f, 0.50f },
                new[] { 1f + (0.74f - 0.80f), 1.1f * 0.75f / 0.55f, 0.34f * 0.42f / 0.30f, 0.55f, 0.15f, 0.55f },
                new[] { 1f + (0.70f - 0.80f), 1.1f * 0.90f / 0.55f, 0.34f * 0.48f / 0.30f, 0.45f, 0.13f, 0.60f },
                new[] { 1f + (0.76f - 0.80f), 1.1f * 0.65f / 0.55f, 0.34f * 0.55f / 0.30f, 0.62f, 0.16f, 0.50f },
            };
            var accent = new[] { "#7cc0f8", "#6ee6c8", "#c486f4", "#ee6a3c", "#fff6b8" };
            using (var main = new MainSceneFixture())
            {
                var mood = Object.FindObjectsByType<DepthMood>(FindObjectsInactive.Include).Single();
                Assert.AreEqual(5, mood.presets.Length);
                const float tol = 1f / 255f + 1e-4f;
                for (int d = 0; d < 5; d++)
                {
                    var p = mood.presets[d];
                    var a = ambient[d]; var c = Px.Hex(accent[d]);
                    Assert.AreEqual(a.r, p.ambient.r, tol, "ambient r depth " + (d + 1)); Assert.AreEqual(a.g, p.ambient.g, tol, "ambient g depth " + (d + 1)); Assert.AreEqual(a.b, p.ambient.b, tol, "ambient b depth " + (d + 1));
                    Assert.AreEqual(moon[d], p.moon, 1e-4f, "moon depth " + (d + 1));
                    var got = new[] { p.bloomThreshold, p.bloomIntensity, p.vignette, p.backFog, p.frontFog, p.fogMix };
                    for (int k = 0; k < got.Length; k++) Assert.AreEqual(rest[d][k], got[k], 1e-4f, "post/fog value " + k + " depth " + (d + 1));
                    Assert.AreEqual(c.r, p.accent.r, tol, "accent r depth " + (d + 1)); Assert.AreEqual(c.g, p.accent.g, tol, "accent g depth " + (d + 1)); Assert.AreEqual(c.b, p.accent.b, tol, "accent b depth " + (d + 1));
                }
            }
        }
    }
}
