using System.IO;
using System.Linq;
using Athanor.Game;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Athanor.Tests
{
    /// <summary>The HD-2D projection maths (HD-2D mockup SPEC, "Layer stack and projection"; removed in P6-22, see git history) and the rules that keep the depth art out of the shipped game.</summary>
    public class DepthLayerTests
    {
        const float D = DepthFixture.D;
        const string DepthDir = "Assets/Art/Depth";

        // SPEC table: layer depth -> scale (D - z) / D
        static readonly (float z, float scale)[] SpecScales =
        {
            (-400f, 1.667f), (-398f, 1.663f), (-392f, 1.653f), (-300f, 1.500f), (-238.2f, 1.397f), (-194.3f, 1.324f),
            (-145.8f, 1.243f), (-102.9f, 1.172f), (-60f, 1.100f), (40f, 0.933f), (180f, 0.700f),
        };

        // SPEC table: art row of the table -> depth
        static readonly (float y, float z)[] SpecTableRows = { (132f, -238.2f), (157f, -194.3f), (188f, -145.8f), (219f, -102.9f), (254f, -60f) };

        // SPEC table: layer depth -> parallax factor against the anchor row
        static readonly (float z, float par)[] SpecParallax =
        {
            (-400f, 0.2045f), (-398f, 0.2033f), (-392f, 0.1997f), (-300f, 0.1379f), (-238.2f, 0.0887f), (-194.3f, 0.0491f),
            (-102.9f, -0.0491f), (-60f, -0.1046f), (40f, -0.2669f), (180f, -0.6240f),
        };

        [Test]
        public void Fov_AtRest_Shows180UnitsAboveAndBelowTheCentre()
        {
            Assert.AreEqual(33.40f, DepthMath.FovDegrees(D), 0.005f);
            Assert.AreEqual(Px.H / 2f, D * Mathf.Tan(DepthMath.FovDegrees(D) * Mathf.Deg2Rad / 2f), 1e-3f);
        }

        [Test]
        public void Scale_SpecDepths_MatchTheSpecTable()
        {
            foreach (var (z, scale) in SpecScales) Assert.AreEqual(scale, DepthMath.Scale(z, D), 0.0006f, "z = " + z);
            Assert.AreEqual(1f, DepthMath.Scale(0, D), 1e-6f, "the art plane is not scaled");
        }

        [Test]
        public void Place_AtAnyDepth_IsSeenWhereItWasDrawn()
        {
            var points = new[] { new Vector2(-320, 180), new Vector2(319.5f, -179.5f), new Vector2(12.5f, -44f), Vector2.zero };
            foreach (var (z, _) in SpecScales)
                foreach (var p in points)
                {
                    var world = DepthMath.Place(p, z, D);
                    Assert.AreEqual(-z, world.z, 1e-4f);
                    var seen = DepthMath.Seen(world, D);
                    Assert.AreEqual(p.x, seen.x, 1e-3f, "x at z = " + z);
                    Assert.AreEqual(p.y, seen.y, 1e-3f, "y at z = " + z);
                }
        }

        [Test]
        public void TableZ_MatchesTheSpecRows_AndClampsOutsideTheTop()
        {
            foreach (var (y, z) in SpecTableRows) Assert.AreEqual(z, DepthMath.TableZ(y, D, DepthFixture.Table), 0.06f, "art y = " + y);
            Assert.AreEqual(-145.77f, DepthMath.TableZ(DepthFixture.AnchorY, D, DepthFixture.Table), 0.01f, "the anchor row");
            Assert.AreEqual(DepthMath.TableZ(132, D, DepthFixture.Table), DepthMath.TableZ(100, D, DepthFixture.Table), 1e-4f, "above the back edge");
            Assert.AreEqual(DepthMath.TableZ(254, D, DepthFixture.Table), DepthMath.TableZ(277, D, DepthFixture.Table), 1e-4f, "the front face shares the front edge's depth");
        }

        [Test]
        public void TableZ_EveryArtRow_LiesOnOnePlane()
        {
            // 1 / distance linear in the screen row means the rows lie on one plane: world y is linear in world z
            Vector3 a = DepthMath.Place(Px.At(320, 132), DepthMath.TableZ(132, D, DepthFixture.Table), D), b = DepthMath.Place(Px.At(320, 254), DepthMath.TableZ(254, D, DepthFixture.Table), D);
            for (int y = 133; y < 254; y += 7)
            {
                var p = DepthMath.Place(Px.At(320, y), DepthMath.TableZ(y, D, DepthFixture.Table), D);
                float t = (p.z - a.z) / (b.z - a.z);
                Assert.AreEqual(Mathf.Lerp(a.y, b.y, t), p.y, 2e-3f, "art y = " + y);
            }
        }

        [Test]
        public void Parallax_MatchesTheSpecTable_AndIsZeroAtTheAnchor()
        {
            float anchor = DepthMath.TableZ(DepthFixture.AnchorY, D, DepthFixture.Table);
            foreach (var (z, par) in SpecParallax) Assert.AreEqual(par, DepthMath.Parallax(z, anchor, D), 0.0006f, "z = " + z);
            Assert.AreEqual(0f, DepthMath.Parallax(anchor, anchor, D), 1e-6f);
        }

        GameObject go;

        [TearDown]
        public void TearDown() { if (go != null) Object.DestroyImmediate(go); }

        [Test]
        public void DepthLayer_Apply_PlacesAndScalesTheObjectAndItsPointLight()
        {
            go = new GameObject("layer");
            go.transform.position = new Vector3(-94f, 86f, 0); go.transform.localScale = new Vector3(1, 0.8f, 1);
            var light = go.AddComponent<Light2D>();
            light.lightType = Light2D.LightType.Point; light.pointLightInnerRadius = 0; light.pointLightOuterRadius = 170;
            var layer = DepthLayer.Add(go, -392f);
            layer.Apply(D);
            float s = DepthMath.Scale(-392f, D);
            Assert.AreEqual(392f, go.transform.position.z, 1e-3f);
            Assert.AreEqual(-94f * s, go.transform.position.x, 1e-3f);
            Assert.AreEqual(0.8f * s, go.transform.localScale.y, 1e-4f);
            Assert.AreEqual(170f * s, light.pointLightOuterRadius, 1e-3f, "URP ignores the transform scale of a point light: the radius is scaled");
            layer.Apply(D);
            Assert.AreEqual(170f * s, light.pointLightOuterRadius, 1e-3f, "applying twice does not compound");
            var seen = DepthMath.Seen(go.transform.position, D);
            Assert.AreEqual(-94f, seen.x, 1e-3f); Assert.AreEqual(86f, seen.y, 1e-3f);
        }

        [Test]
        public void ArtLibrary_MainScene_HasEveryDepthArtSpriteOnce()
        {
            var lib = AssetDatabase.LoadAssetAtPath<ArtLibrary>("Assets/Art/ArtLibrary.asset");
            Assert.NotNull(lib);
            var names = lib.sprites.Where(s => s != null).Select(s => s.name).ToArray();
            Assert.AreEqual(names.Length, names.Distinct().Count(), "no sprite name twice in the library");
            var depth = AssetDatabase.FindAssets("t:Sprite", new[] { DepthDir }).Select(AssetDatabase.GUIDToAssetPath)
                .Select(p => System.IO.Path.GetFileNameWithoutExtension(p)).ToArray();
            Assert.IsNotEmpty(depth);
            CollectionAssert.IsSubsetOf(depth, names);
        }

        [Test]
        public void DepthArt_ImportSettings_KeepTheBlurSmoothAndTheFogTiling()
        {
            Assert.IsTrue(AssetDatabase.IsValidFolder(DepthDir), "the depth art is committed: " + DepthDir);
            var paths = AssetDatabase.FindAssets("t:Texture2D", new[] { DepthDir }).Select(AssetDatabase.GUIDToAssetPath).ToArray();
            Assert.IsNotEmpty(paths);
            foreach (string path in paths)
            {
                var ti = (TextureImporter)AssetImporter.GetAtPath(path);
                string name = Path.GetFileNameWithoutExtension(path);
                bool fog = name.StartsWith("fog_");
                Assert.AreEqual(fog ? FilterMode.Point : FilterMode.Bilinear, ti.filterMode, path);
                Assert.AreEqual(fog ? TextureWrapMode.Repeat : TextureWrapMode.Clamp, ti.wrapMode, path);
                Assert.AreEqual(1f, ti.spritePixelsPerUnit, path);
                Assert.IsFalse(ti.mipmapEnabled, path);
                Assert.AreEqual(TextureImporterCompression.Uncompressed, ti.textureCompression, path);
            }
        }
    }
}
