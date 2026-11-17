using System.IO;
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
        LayoutData layout;

        [SetUp]
        public void SetUp()
        {
            layout = JsonUtility.FromJson<LayoutData>(File.ReadAllText("Assets/Resources/layout.json"));
            root = new GameObject("main depth test");
            var cam = new GameObject("world camera").AddComponent<Camera>();
            cam.transform.SetParent(root.transform, false);
            rig = root.AddComponent<DepthRig>();
            rig.worldCamera = cam;
            rig.Apply();
            tiles = new TileView[layout.tiles.Length];
            var tileRoot = Make.Root("Tiles", root.transform);
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = new GameObject("Tile " + i).AddComponent<TileView>();
                tiles[i].Build(i, layout.tiles[i], tileRoot);
                foreach (var t in tiles[i].GetComponentsInChildren<Transform>(true)) t.gameObject.layer = DepthRig.WorldLayer;
                DepthLayer.AddGroup(tiles[i].gameObject, DepthMath.TableZ(layout.tiles[i].y) + 0.2f).Apply(rig.distance);
            }
        }

        [TearDown]
        public void TearDown() => Object.DestroyImmediate(root);

        [Test]
        public void MakeChildren_OfAWorldLayerParent_DrawOnTheWorldLayer()
        {
            var sprite = Make.Sprite(tiles[0].transform, "dot", Vector3.zero, 1);
            var text = Make.Root("later", tiles[0].transform);
            Assert.AreEqual(DepthRig.WorldLayer, sprite.gameObject.layer);
            Assert.AreEqual(DepthRig.WorldLayer, text.gameObject.layer);
            Assert.AreEqual(0, Make.Root("loose").gameObject.layer, "no parent: the UI camera's layer");
            Object.DestroyImmediate(sprite.gameObject);
        }

        [Test]
        public void TileGroup_AtRest_KeepsTheFaceOnItsArtPixels()
        {
            var cam = rig.worldCamera;
            for (int i = 0; i < tiles.Length; i++)
            {
                var f = layout.tiles[i].face;
                Vector2 flat = Px.Mid(f.x, f.y, f.w, f.h);
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
            PointerInput.Rig = rig;
            try
            {
                foreach (var sway in sways)
                    foreach (var shake in new[] { Vector2.zero, new Vector2(3, -2), new Vector2(-3, 2) })
                    {
                        rig.SetView(sway, shake);
                        for (int i = 0; i < tiles.Length; i++)
                        {
                            var f = layout.tiles[i].face;
                            Vector2 flat = Px.Mid(f.x, f.y, f.w, f.h);
                            var world = DepthMath.Place(flat, DepthMath.TableZ(Px.H / 2f - flat.y));
                            Vector3 vp = rig.worldCamera.WorldToViewportPoint(world);
                            PointerInput.Resolve(new Vector2((vp.x - 0.5f) * Px.W, (vp.y - 0.5f) * Px.H));
                            var hits = Enumerable.Range(0, tiles.Length).Where(k => tiles[k].Contains(PointerInput.Bench)).ToArray();
                            CollectionAssert.AreEqual(new[] { i }, hits, $"tile {i}, sway {sway}, shake {shake}");
                        }
                    }
            }
            finally { PointerInput.Rig = old; rig.SetView(Vector2.zero, Vector2.zero); }
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
    }
}
