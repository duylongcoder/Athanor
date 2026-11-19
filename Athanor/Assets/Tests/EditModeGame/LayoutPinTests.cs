using System.Collections.Generic;
using System.Linq;
using Athanor.Game;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>Phase 7: numbers that moved from code into Main.unity or a prefab equal the literals the code had.</summary>
    public class LayoutPinTests
    {
        static void Floats(SerializedObject so, string root, Dictionary<string, float> want)
        {
            foreach (var kv in want)
            {
                var p = so.FindProperty(root + "." + kv.Key);
                Assert.IsNotNull(p, root + "." + kv.Key);
                float got = p.propertyType == SerializedPropertyType.Integer ? p.intValue : p.floatValue;
                Assert.AreEqual(kv.Value, got, 1e-4f, root + "." + kv.Key);
            }
        }

        static void Colour(SerializedObject so, string path, int rgb)
        {
            var c = so.FindProperty(path).colorValue;
            Assert.AreEqual(((rgb >> 16) & 255) / 255f, c.r, 1e-4f, path); Assert.AreEqual(((rgb >> 8) & 255) / 255f, c.g, 1e-4f, path); Assert.AreEqual((rgb & 255) / 255f, c.b, 1e-4f, path);
        }

        static void Burst(SerializedObject so, string path, int count, float speed, float life, int order, float gravity, float r, float g, float b, float a)
        {
            Floats(so, path, new Dictionary<string, float> { { "count", count }, { "speed", speed }, { "life", life }, { "order", order }, { "gravity", gravity } });
            var c = so.FindProperty(path + ".color").colorValue;
            Assert.AreEqual(new[] { r, g, b, a }, new[] { c.r, c.g, c.b, c.a }.Select((v, i) => v).ToArray(), "burst colour " + path);
        }

        [Test]
        public void Palette_Asset_HasTodaysColours()
        {
            var p = AssetDatabase.LoadAssetAtPath<Palette>("Assets/Data/Palette.asset");
            Assert.IsNotNull(p, "Assets/Data/Palette.asset");
            void Is(string hex, Color got, string name) => Assert.AreEqual(Px.Hex(hex), got, name);
            Is("#a8a0a8", p.rarityText.common, "rarityText.common"); Is("#8ad04a", p.rarityText.uncommon, "rarityText.uncommon");
            Is("#7cc0f8", p.rarityText.rare, "rarityText.rare"); Is("#ffa63a", p.rarityText.legendary, "rarityText.legendary");
            Is("#5a5058", p.rarityTone.common, "rarityTone.common"); Is("#3a6a14", p.rarityTone.uncommon, "rarityTone.uncommon");
            Is("#1c5a96", p.rarityTone.rare, "rarityTone.rare"); Is("#8a4a08", p.rarityTone.legendary, "rarityTone.legendary");
            Is("#f8d250", p.gold, "gold"); Is("#a8b8c4", p.anchor, "anchor"); Is("#ffa63a", p.warm, "warm");
            Is("#6ee6c8", p.echo, "echo"); Is("#c486f4", p.attuned, "attuned"); Is("#8ad04a", p.seed, "seed");
            Is("#d49a5c", p.fallow, "fallow"); Is("#cfc8c4", p.heavy, "heavy"); Is("#7cc0f8", p.cold, "cold");
            Is("#ee6a3c", p.volatileTile, "volatileTile"); Is("#e06a1c", p.kiln, "kiln"); Is("#f890b8", p.pivot, "pivot");
            Is("#f0dcc0", p.defaultGlow, "defaultGlow");
            Is("#3a2212", p.ink, "ink"); Is("#7a5430", p.faded, "faded"); Is("#11606a", p.teal, "teal");
            Is("#8a5a1e", p.edge, "edge"); Is("#c8501e", p.pipOn, "pipOn"); Is("#b89860", p.pipOff, "pipOff");
            Is("#fff6b8", p.buttonText, "buttonText");
        }

        [Test]
        public void IgniteSequence_MainScene_KeepsTodaysTiming()
        {
            using (var main = new MainSceneFixture())
            {
                var so = new SerializedObject(MainSceneFixture.Field<IgniteSequence>(main.Flow, "ignite"));
                Floats(so, "timing", new Dictionary<string, float>
                {
                    { "paceHurry", 0.4f }, { "paceFloor", 0.55f }, { "paceDecay", 0.07f }, { "endPaceHurry", 0.4f }, { "endPace", 0.8f },
                    { "discoverHurryBase", 1 }, { "discoverHurryStep", 0.4f }, { "discoverHurryCap", 3 }, { "discoverBeat", 0.4f },
                    { "fizzleHintWait", 0.55f }, { "fizzleWait", 0.24f }, { "fizzleHintText", 1.1f }, { "sputterWait", 0.6f }, { "sputterText", 1.0f },
                    { "drawSeconds", 0.22f }, { "drawWait", 0.2f }, { "reactWait", 0.5f }, { "pointsSeconds", 0.5f }, { "reactShake", 0.16f }, { "reactFlash", 0.2f },
                    { "pointsText", 0.5f }, { "pointsTextPace", 0.5f }, { "comboText", 0.9f }, { "discoverShake", 0.35f },
                    { "rackPop", 0.3f }, { "rackFly", 0.4f }, { "rackHold", 0.12f }, { "rackStagger", 0.08f }, { "rackWaitKept", 0.85f }, { "rackWaitNone", 0.3f },
                    { "lostText", 1.6f }, { "rackTrailChance", 0.6f },
                });
                var run = new SerializedObject(main.Flow);
                Floats(run, "runTiming", new Dictionary<string, float> { { "descendWait", 0.6f }, { "endingWait", 0.6f }, { "lossShake", 3 }, { "lossShakeSeconds", 0.5f }, { "lossWait", 0.9f } });
            }
        }

        [Test]
        public void IgniteSequence_MainScene_KeepsTodaysLook()
        {
            using (var main = new MainSceneFixture())
            {
                var so = new SerializedObject(MainSceneFixture.Field<IgniteSequence>(main.Flow, "ignite"));
                Burst(so, "look.fizzleBurst", 10, 26, 0.7f, 150, -18, 0.62f, 0.62f, 0.7f, 0.8f);
                Burst(so, "look.sputterBurst", 5, 22, 0.6f, 150, -14, 0.55f, 0.4f, 0.75f, 0.8f);
                Burst(so, "look.discoverBurst", 26, 95, 0.5f, 150, 0, 2.2f, 2.1f, 1.8f, 1);
                Burst(so, "look.reactBurst", 14, 60, 0.55f, 150, 40, 2.2f, 2.2f, 2.2f, 1);
                Burst(so, "look.pointsLand", 8, 45, 0.3f, 172, 0, 2.4f, 1.9f, 0.7f, 1);
                Burst(so, "look.rackLand", 8, 40, 0.3f, 163, 0, 2, 2, 2, 1);
                Colour(so, "look.defaultGlow", 0xf0dcc0); Colour(so, "look.pointsText", 0xf8d250); Colour(so, "look.comboText", 0xee6a3c);
                Colour(so, "look.chainText", 0x6ee6c8); Colour(so, "look.hintText", 0xc486f4); Colour(so, "look.spoiledText", 0xc486f4); Colour(so, "look.lostText", 0xee6a3c);
                Assert.AreEqual(new Vector3(0, 12, 0), so.FindProperty("look.hubLift").vector3Value);
                Assert.AreEqual(new Vector3(0, 30, 0), so.FindProperty("look.comboTextLift").vector3Value);
                Assert.AreEqual(new Vector2(320, 232), so.FindProperty("look.lostAt").vector2Value);
                Floats(so, "look", new Dictionary<string, float>
                {
                    { "burstPerChain", 5 }, { "burstChainCap", 6 }, { "burstPerExtra", 8 }, { "speedPerChain", 10 },
                    { "shakeBase", 1 }, { "shakePerChain", 0.5f }, { "shakePerExtra", 0.6f }, { "shakeMax", 5 }, { "discoverShake", 2 },
                    { "flashChain", 2 }, { "flashSize", 4 }, { "flashPerStep", 0.05f }, { "flashMax", 0.22f },
                    { "pointsHump", 34 }, { "haloSize", 26 }, { "haloAlpha", 0.6f }, { "popScale", 2 }, { "arcHeight", 20 }, { "haloOrder", 164 }, { "iconOrder", 165 },
                    { "drawOrder", 151 }, { "pointsOrder", 172 }, { "pointsOrder2", 171 }, { "rackTrailOrder", 163 },
                });
            }
        }
    }
}
