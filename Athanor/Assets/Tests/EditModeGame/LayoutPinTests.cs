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
        static void AssertFloats(SerializedObject so, string root, Dictionary<string, float> want)
        {
            foreach (var kv in want)
            {
                string path = root.Length == 0 ? kv.Key : root + "." + kv.Key;
                var p = so.FindProperty(path);
                Assert.IsNotNull(p, path);
                float got = p.propertyType == SerializedPropertyType.Integer ? p.intValue : p.floatValue;
                Assert.AreEqual(kv.Value, got, 1e-4f, path);
            }
        }

        static void AssertColour(SerializedObject so, string path, int rgb)
        {
            var c = so.FindProperty(path).colorValue;
            Assert.AreEqual(((rgb >> 16) & 255) / 255f, c.r, 1e-4f, path); Assert.AreEqual(((rgb >> 8) & 255) / 255f, c.g, 1e-4f, path); Assert.AreEqual((rgb & 255) / 255f, c.b, 1e-4f, path);
        }

        static void AssertBurst(SerializedObject so, string path, int count, float speed, float life, int order, float gravity, float r, float g, float b, float a)
        {
            AssertFloats(so, path, new Dictionary<string, float> { { "count", count }, { "speed", speed }, { "life", life }, { "order", order }, { "gravity", gravity } });
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
                AssertFloats(so, "timing", new Dictionary<string, float>
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
                AssertFloats(run, "runTiming", new Dictionary<string, float> { { "descendWait", 0.6f }, { "endingWait", 0.6f }, { "lossShake", 3 }, { "lossShakeSeconds", 0.5f }, { "lossWait", 0.9f } });
            }
        }

        [Test]
        public void IgniteSequence_MainScene_KeepsTodaysLook()
        {
            using (var main = new MainSceneFixture())
            {
                var so = new SerializedObject(MainSceneFixture.Field<IgniteSequence>(main.Flow, "ignite"));
                AssertBurst(so, "look.fizzleBurst", 10, 26, 0.7f, 150, -18, 0.62f, 0.62f, 0.7f, 0.8f);
                AssertBurst(so, "look.sputterBurst", 5, 22, 0.6f, 150, -14, 0.55f, 0.4f, 0.75f, 0.8f);
                AssertBurst(so, "look.discoverBurst", 26, 95, 0.5f, 150, 0, 2.2f, 2.1f, 1.8f, 1);
                AssertBurst(so, "look.reactBurst", 14, 60, 0.55f, 150, 40, 2.2f, 2.2f, 2.2f, 1);
                AssertBurst(so, "look.pointsLand", 8, 45, 0.3f, 172, 0, 2.4f, 1.9f, 0.7f, 1);
                AssertBurst(so, "look.rackLand", 8, 40, 0.3f, 163, 0, 2, 2, 2, 1);
                AssertColour(so, "look.defaultGlow", 0xf0dcc0); AssertColour(so, "look.pointsText", 0xf8d250); AssertColour(so, "look.comboText", 0xee6a3c);
                AssertColour(so, "look.chainText", 0x6ee6c8); AssertColour(so, "look.hintText", 0xc486f4); AssertColour(so, "look.spoiledText", 0xc486f4); AssertColour(so, "look.lostText", 0xee6a3c);
                Assert.AreEqual(new Vector3(0, 12, 0), so.FindProperty("look.hubLift").vector3Value);
                Assert.AreEqual(new Vector3(0, 30, 0), so.FindProperty("look.comboTextLift").vector3Value);
                Assert.AreEqual(new Vector2(320, 232), so.FindProperty("look.lostAt").vector2Value);
                AssertFloats(so, "look", new Dictionary<string, float>
                {
                    { "burstPerChain", 5 }, { "burstChainCap", 6 }, { "burstPerExtra", 8 }, { "speedPerChain", 10 },
                    { "shakeBase", 1 }, { "shakePerChain", 0.5f }, { "shakePerExtra", 0.6f }, { "shakeMax", 5 }, { "discoverShake", 2 },
                    { "flashChain", 2 }, { "flashSize", 4 }, { "flashPerStep", 0.05f }, { "flashMax", 0.22f },
                    { "pointsHump", 34 }, { "haloSize", 26 }, { "haloAlpha", 0.6f }, { "popScale", 2 }, { "arcHeight", 20 }, { "haloOrder", 164 }, { "iconOrder", 165 },
                    { "drawOrder", 151 }, { "pointsOrder", 172 }, { "pointsOrder2", 171 }, { "rackTrailOrder", 163 },
                });
            }
        }
        static T Part<T>(string prefab) where T : Component
        {
            var go = AssetDatabase.LoadAssetAtPath<GameObject>(prefab);
            Assert.IsNotNull(go, prefab);
            var c = go.GetComponentInChildren<T>(true);
            Assert.IsNotNull(c, typeof(T).Name + " in " + prefab);
            return c;
        }

        static void AssertRect(SerializedObject so, string path, float x, float y, float w, float h) =>
            Assert.AreEqual(new Rect(x, y, w, h), so.FindProperty(path).rectValue, path);

        static void AssertV2(SerializedObject so, string path, float x, float y) => Assert.AreEqual(new Vector2(x, y), so.FindProperty(path).vector2Value, path);

        [Test]
        public void HudView_Prefab_KeepsTodaysLayout()
        {
            var so = new SerializedObject(Part<HudView>("Assets/Prefabs/Parts/Hud.prefab"));
            AssertFloats(so, "layout", new Dictionary<string, float>
            {
                { "rowEn", 10 }, { "rowVi", 12 }, { "noteCharsEn", 19 }, { "noteCharsVi", 17 }, { "redrawLabelY", 3 }, { "pressedDy", 2 },
                { "catalystPitch", 35 }, { "catalystSpread", 105 }, { "catalystSideBySide", 4 }, { "aspectPitch", 29 },
                { "noteX", 10 }, { "noteTopAspects", 130 }, { "noteTop", 88 }, { "potencyX", 70 }, { "potencyTightX", 17 }, { "potencyY", 49 },
                { "barWidth", 132 }, { "barHeight", 5 }, { "tightFrom", 70 }, { "tightGap", 4 }, { "tightTo", 153 },
            });
            AssertRect(so, "layout.redrawRect", 556, 88, 76, 20); AssertRect(so, "layout.pauseRect", 12, 152, 28, 20);
            AssertV2(so, "layout.socketOrigin", 488, 24); AssertV2(so, "layout.socketSize", 30, 30); AssertV2(so, "layout.iconOffset", 3, 3); AssertV2(so, "layout.iconSize", 24, 24);
            AssertV2(so, "layout.aspectAnchor", 22, 111); AssertV2(so, "layout.potencyAnchor", 96, 56); AssertV2(so, "layout.barOrigin", 18, 70); AssertV2(so, "layout.pulseAlpha", 0.1f, 0.2f);
            Assert.AreEqual(new Vector3(0.25f, 1.25f, 1), so.FindProperty("layout.barTipScale").vector3Value);
            AssertColour(so, "redrawUp", 0x1d5a52); AssertColour(so, "redrawOver", 0x26706a); AssertColour(so, "redrawPressed", 0x143f3a); AssertColour(so, "redrawOff", 0x3a2e28);
            var pulse = so.FindProperty("layout.pulseColor").colorValue;
            Assert.AreEqual(new Color(1, 0.75f, 0.35f, 1), pulse);
            Assert.AreEqual(new Color(0.55f, 0.5f, 0.75f, 1), so.FindProperty("noTargetBar").colorValue);
        }

        [Test]
        public void HudView_IgniteLabel_SitsWhereTheCodePutIt()
        {
            var hud = Part<HudView>("Assets/Prefabs/Parts/Hud.prefab");
            var label = MainSceneFixture.Field<Label>(hud, "igniteText");
            Assert.AreEqual(Px.At(575, 306), label.transform.position);
        }

        [Test]
        public void BenchParts_Prefabs_KeepTodaysLayout()
        {
            var bench = new SerializedObject(Part<BenchView>("Assets/Prefabs/Views/Bench.prefab"));
            Assert.AreEqual(new Vector3(0, -8, 0), bench.FindProperty("layout.joinLift").vector3Value);
            Assert.AreEqual(new Vector3(0, 6, 0), bench.FindProperty("layout.floatLiftFrom").vector3Value);
            Assert.AreEqual(new Vector3(0, 28, 0), bench.FindProperty("layout.floatLiftTo").vector3Value);
            Assert.AreEqual(new Vector3(0, 12, 0), bench.FindProperty("layout.tileTipLift").vector3Value);
            Assert.AreEqual(new Vector3(0, CardView.H / 2f, 0), bench.FindProperty("layout.cardTipLift").vector3Value);
            AssertFloats(bench, "layout", new Dictionary<string, float> { { "ghostLift", 6 } });
            AssertColour(bench, "layout.floatShadow", 0x0c0706);

            var tile = new SerializedObject(Part<TileView>("Assets/Prefabs/Views/Bench.prefab"));
            var lid = tile.FindProperty("layout.shutLid").colorValue;
            Assert.AreEqual(new Color(0.03f, 0.02f, 0.05f, 0.78f), lid);
            Assert.AreEqual(new Color(1, 0.85f, 0.45f, 1), tile.FindProperty("layout.hoverFace").colorValue);
            Assert.AreEqual(new Vector3(0, 8, 0), tile.FindProperty("layout.consumeLift").vector3Value);
            AssertFloats(tile, "layout", new Dictionary<string, float> { { "iconLift", 10 }, { "popRise", 8 } });

            var card = new SerializedObject(Part<CardView>("Assets/Prefabs/Parts/Card.prefab"));
            AssertFloats(card, "layout", new Dictionary<string, float>
            {
                { "nameOneLine", -3 }, { "nameTwoLines", 2 }, { "pictureSlideMax", 10 }, { "dealDrop", 90 },
                { "selectedLift", 14 }, { "liftedLift", 9 }, { "liftSpeed", 90 }, { "bobAmp", 1.5f }, { "bobSpeed", 3 },
            });

            var link = new SerializedObject(Part<LinkView>("Assets/Prefabs/Views/Bench.prefab"));
            Assert.AreEqual(new Color(2.2f, 1.7f, 0.8f, 1), link.FindProperty("color").colorValue);
            AssertV2(link, "moteScale", 0.5f, 0.5f);
            Assert.AreEqual(9, link.FindProperty("lift").floatValue);
        }

        /// <summary>A serialized colour equals want in all four channels.</summary>
        static void AssertRgba(SerializedObject so, string path, Color want)
        {
            var p = so.FindProperty(path);
            Assert.IsNotNull(p, path);
            var c = p.colorValue;
            Assert.AreEqual(want.r, c.r, 1e-4f, path + ".r"); Assert.AreEqual(want.g, c.g, 1e-4f, path + ".g");
            Assert.AreEqual(want.b, c.b, 1e-4f, path + ".b"); Assert.AreEqual(want.a, c.a, 1e-4f, path + ".a");
        }

        static void AssertHex(SerializedObject so, string path, string hex) => AssertRgba(so, path, Px.Hex(hex));

        static void AssertVec2(SerializedObject so, string path, float x, float y)
        {
            var p = so.FindProperty(path);
            Assert.IsNotNull(p, path);
            Assert.AreEqual(x, p.vector2Value.x, 1e-4f, path + ".x"); Assert.AreEqual(y, p.vector2Value.y, 1e-4f, path + ".y");
        }

        static SerializedObject View<T>(string name) where T : Component => new SerializedObject(Part<T>("Assets/Prefabs/Views/" + name + ".prefab"));

        [Test]
        public void JournalView_Prefab_KeepsTodaysLayout()
        {
            // the old statics: X 20, Y 15, PageX 324, PageY 30, PageW 260, GridX 33, HintsY 240, Cols 10, Cell 27
            var so = View<JournalView>("Journal");
            AssertRect(so, "layout.indexRect", 20 + 324 + 12, 15 + 30 + 91, 260 - 24, 22);
            AssertRect(so, "layout.recipesRect", 20 + 324 + 12, 15 + 30 + 111, 260 - 24, 60);
            AssertRect(so, "layout.hintsRect", 20 + 33 - 2, 15 + 240 - 2, 10 * 27 + 2, 14 + 2 * 11);
            AssertFloats(so, "layout", new Dictionary<string, float>
            {
                { "titleTop", 62 }, { "titleTopLong", 72 }, { "titleLongChars", 13 }, { "titleSize", 32 }, { "titleSizeLong", 16 },
            });
            AssertHex(so, "layout.hint", "#4a1f7a");
            AssertRgba(so, "layout.unknown", new Color(0.16f, 0.1f, 0.05f, 0.4f));
        }

        [Test]
        public void GoalsView_Prefab_KeepsTodaysLayout()
        {
            var so = View<GoalsView>("Goals");
            AssertFloats(so, "layout", new Dictionary<string, float> { { "barHeight", 3 }, { "barDy", -14.5f } });
        }

        [Test]
        public void CatalystChoiceView_Prefab_KeepsTodaysLayout()
        {
            var so = View<CatalystChoiceView>("CatalystChoice");
            AssertFloats(so, "timing", new Dictionary<string, float>
            {
                { "dealBase", 0.5f }, { "dealPerCard", 0.1f }, { "dealFly", 0.5f }, { "shadeDiv", 2.5f }, { "stagger", 0.1f }, { "away", 0.55f }, { "hopShare", 0.45f },
            });
            AssertVec2(so, "timing.pickFlash", 0.22f, 0.35f);
            AssertFloats(so, "layout", new Dictionary<string, float>
            {
                { "dim", 0.8f }, { "startDrop", -300 }, { "fallDrop", -330 }, { "hop", 10 }, { "hoverLift", 8 }, { "liftSpeed", 80 },
            });
            AssertHex(so, "layout.kicker", "#a8854e"); AssertHex(so, "layout.kickerCost", "#9a3414");
            AssertBurst(so, "layout.pickBurst", 40, 150, 0.8f, 440 + 8, 60, 2.2f, 1.7f, 0.6f, 1);
        }

        [Test]
        public void EventView_Prefab_KeepsTodaysLayout()
        {
            var so = View<EventView>("Event");
            AssertFloats(so, "timing", new Dictionary<string, float> { { "fadeIn", 0.35f }, { "fadeOut", 0.2f }, { "slide", 0.35f }, { "shadeAlpha", 0.62f }, { "slideFrom", -40 } });
            AssertVec2(so, "timing.noteFlash", 0.12f, 0.25f);
            AssertRgba(so, "layout.overTint", new Color(1.25f, 1.15f, 1.05f, 1));
            AssertFloats(so, "layout", new Dictionary<string, float> { { "labelUp", 9 }, { "labelDown", 7 } });
        }

        [Test]
        public void RunOverView_Prefab_KeepsTodaysTiming()
        {
            var so = View<RunOverView>("RunOver");
            AssertFloats(so, "timing", new Dictionary<string, float> { { "fadeIn", 0.35f }, { "slide", 0.35f }, { "shadeAlpha", 0.62f }, { "slideFrom", -40 } });
        }

        [Test]
        public void PauseMenu_Prefab_KeepsTodaysLayout()
        {
            var so = View<PauseMenu>("Pause");
            AssertFloats(so, "layout", new Dictionary<string, float>
            {
                { "buttonX", 260 }, { "howToDy", 110 }, { "quitDy", 152 }, { "hintX", 320 }, { "hintDy", 137 }, { "noVibrationUp", 30 },
            });
        }

        [Test]
        public void TitleView_Prefab_KeepsTodaysLayout()
        {
            var so = View<TitleView>("Title");
            AssertFloats(so, "layout", new Dictionary<string, float>
            {
                { "centerX", 320 }, { "logoY", 96 }, { "logoRise", 10 }, { "outlineSpread", 2 },
                { "progressY", 334 }, { "progressYVi", 332 }, { "creditY", 348 }, { "creditYVi", 346 }, { "cellInset", 1 },
                { "pitch", 28 }, { "pitchNoVib", 32 }, { "firstRow", 54 }, { "firstRowNoVib", 58 },
                { "dividerX", 24 }, { "dividerW", 272 }, { "dividerH", 1 }, { "dividerY", 140 }, { "dividerYNoVib", 126 }, { "resetY", 144 }, { "resetYNoVib", 134 },
                { "labelX", 188 }, { "labelDy", 4 }, { "valueDy", 3 }, { "valueDx", 26 }, { "boxInset", 8 }, { "boxSize", 10 }, { "fillInset", 9 }, { "fillSize", 8 },
            });
            AssertHex(so, "layout.resetRest", "#7a5a3a"); AssertHex(so, "layout.resetOver", "#ee6a3c");
            AssertHex(so, "layout.cellRest", "#2a1810"); AssertHex(so, "layout.cellOver", "#4a2c14");
            AssertFloats(so, "timing", new Dictionary<string, float>
            {
                { "fadeIn", 1.2f }, { "subDelay", 0.6f }, { "subFade", 0.8f }, { "glowBase", 0.28f }, { "glowSwing", 0.12f }, { "glowSpeed", 1.7f },
                { "emberRate", 9 }, { "emberY", 300 }, { "emberSize", 4 }, { "emberFall", 0.3f },
            });
            // the glow's alpha is computed each frame; only its rgb was a literal
            var glow = so.FindProperty("timing.glow").colorValue;
            Assert.AreEqual(1f, glow.r, 1e-4f, "timing.glow.r"); Assert.AreEqual(0.55f, glow.g, 1e-4f, "timing.glow.g"); Assert.AreEqual(0.2f, glow.b, 1e-4f, "timing.glow.b");
            AssertVec2(so, "timing.emberX", 120, 520); AssertVec2(so, "timing.emberVx", -6, 6); AssertVec2(so, "timing.emberVy", 18, 42);
            AssertRgba(so, "timing.ember", new Color(2.2f, 1.2f, 0.4f, 0.9f));
        }

        [Test]
        public void GoalToast_Prefab_KeepsTodaysLayout()
        {
            var so = View<GoalToast>("GoalToast");
            AssertBurst(so, "layout.burst", 18, 90, 0.6f, 490 + 3, 20, 2.2f, 1.7f, 0.6f, 1);
            AssertFloats(so, "layout", new Dictionary<string, float> { { "hideGap", 2 } });
        }

        [Test]
        public void Tooltip_BenchPrefab_KeepsTodaysLayout()
        {
            var so = View<Tooltip>("Bench");
            AssertFloats(so, "layout", new Dictionary<string, float>
            {
                { "pad", 8 }, { "titleDy", 4 }, { "bodyDy", 21 }, { "starsDy", 22 }, { "tierDy", 19 }, { "tierGap", 4 }, { "edgeMargin", 2 },
                { "border", 4 }, { "longText", 62 }, { "tightSpacing", -30 },
            });
            AssertVec2(so, "layout.pivot", 0.5f, 0.5f);
        }

        [Test]
        public void UiButton_EveryPrefabInstance_KeepsTodaysLook()
        {
            var want = new Dictionary<string, int> { { "Pause", 6 }, { "RunOver", 2 }, { "Title", 11 }, { "Event", 1 } };
            foreach (var kv in want)
            {
                var path = "Assets/Prefabs/Views/" + kv.Key + ".prefab";
                var go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                Assert.IsNotNull(go, path);
                var buttons = go.GetComponentsInChildren<UiButton>(true);
                Assert.AreEqual(kv.Value, buttons.Length, "UiButtons in " + path);
                foreach (var b in buttons)
                {
                    var so = new SerializedObject(b);
                    string at = path + " " + b.name + " ";
                    AssertRgba(so, "look.disabledTint", new Color(0.45f, 0.42f, 0.42f, 1));
                    AssertRgba(so, "look.overTint", new Color(1.25f, 1.15f, 1.05f, 1));
                    AssertHex(so, "look.disabledText", "#8a7a6a");
                    Assert.AreEqual(10f, so.FindProperty("look.labelUp").floatValue, 1e-4f, at + "labelUp");
                    Assert.AreEqual(8f, so.FindProperty("look.labelDown").floatValue, 1e-4f, at + "labelDown");
                }
            }
        }

        [Test]
        public void Palette_Asset_KeepsTheStarOutline()
        {
            var p = AssetDatabase.LoadAssetAtPath<Palette>("Assets/Data/Palette.asset");
            Assert.IsNotNull(p, "Assets/Data/Palette.asset");
            AssertHex(new SerializedObject(p), "starOutline", "#1a0e08");
        }

        [Test]
        public void Dust_MainScene_KeepsTodaysLook()
        {
            using (var main = new MainSceneFixture())
            {
                var dust = main.Flow.gameObject.scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<Dust>(true)).Single();
                var so = new SerializedObject(dust);
                Assert.AreEqual(new Color(0.7f, 0.85f, 1, 1), so.FindProperty("tint").colorValue);
                Assert.AreEqual(0.3f, so.FindProperty("alphaBase").floatValue, 1e-4f); Assert.AreEqual(0.4f, so.FindProperty("alphaSwing").floatValue, 1e-4f);
            }
        }

        /// <summary>Pins name/value pairs (a number each) under a serialized struct.</summary>
        static void Pin(SerializedObject so, string root, params object[] pairs)
        {
            var want = new Dictionary<string, float>();
            for (int i = 0; i < pairs.Length; i += 2) want[(string)pairs[i]] = System.Convert.ToSingle(pairs[i + 1]);
            AssertFloats(so, root, want);
        }

        [Test]
        public void SimpleCutscene_MainScene_KeepsTodaysBeats()
        {
            using (var main = new MainSceneFixture())
            {
                var so = new SerializedObject(main.FlowField<SimpleCutscene>("story"));
                Pin(so, "introPage", "fadeIn", 0.9f, "lineFade", 0.35f, "fadeOut", 0.4f, "hold", 0.8f, "lastHold", 0.3f, "finalHold", 1.6f, "promptHold", 8);
                Pin(so, "descentBanner", "flash", 0.3f, "flashSeconds", 0.5f, "shake", 2, "shakeSeconds", 0.3f, "rise", 0.3f, "fromY", 102, "toY", 88, "hold", 0.9f, "burstY", 170, "burstSquash", 0.6f);
                AssertBurst(so, "descentBanner.burst", 70, 210, 1.1f, 431, 50, 2.2f, 1.7f, 0.6f, 1);
                Pin(so, "descentLeave", "fade", 0.3f, "seconds", 0.9f, "scroll", 400);
                Pin(so, "descentName", "fade", 0.45f, "rise", 0.45f, "fromY", 136, "toY", 128, "introFade", 0.3f, "hold", 2.4f);
                Pin(so, "descentArrive", "streaks", 0.4f, "fade", 0.36f, "runeFade", 0.45f, "seconds", 0.9f);
                Pin(so, "depthLine", "fade", 0.3f, "hold", 1.6f);
                AssertRgba(so, "depthLine.shade", new Color(0, 0, 0, 0.35f));
                Pin(so, "ending", "fadeOut", 0.5f, "fadeIn", 1.1f, "hold", 0.9f, "flash", 0.5f, "flashSeconds", 0.8f, "promptHold", 10, "burstY", 150, "bannerY", 44);
                AssertBurst(so, "ending.burst", 90, 220, 1.4f, 426, 30, 2.4f, 2.4f, 2.4f, 1);
                Pin(so, "look", "prizeY", 122, "bobSpeed", 2.2f, "bobAmplitude", 3, "glowAlpha", 0.45f, "lightIntensity", 1.2f, "rayStep", 30, "raySpin", 14, "shortRay", 0.75f,
                    "runeBright", 1.5f, "pulseBase", 0.7f, "pulseAmplitude", 0.3f, "pulseSpeed", 4, "runeGlow", 0.16f,
                    "ringSpeed", 0.5f, "ringWidth", 60, "ringGrowW", 320, "ringHeight", 14, "ringGrowH", 76, "ringAlpha", 0.8f,
                    "streakBright", 1.6f, "streakAlpha", 0.75f, "moteBright", 2);
                AssertRgba(so, "look.rayColor", new Color(1, 0.95f, 0.75f, 0.3f)); AssertRgba(so, "look.emberColor", new Color(2.4f, 1.2f, 0.4f, 1));
                Pin(so, "emberSpec", "order", 310, "rate", 22, "life", 1.5f, "x0", 264, "x1", 376, "y0", 286, "y1", 286, "vx0", -10, "vx1", 10, "vy0", 22, "vy1", 60, "gravity", -6, "sparkle", 0.25f);
                Pin(so, "streakSpec", "order", 421, "rate", 70, "life", 1.1f, "x0", 0, "x1", 640, "y0", 364, "y1", 364, "vx0", 0, "vx1", 0, "vy0", 380, "vy1", 620, "gravity", 0, "sparkle", 0);
                Pin(so, "moteSpec", "order", 423, "rate", 26, "life", 1.3f, "x0", 180, "x1", 460, "y0", 248, "y1", 276, "vx0", 0, "vx1", 0, "vy0", 20, "vy1", 60, "gravity", -4, "sparkle", 0.3f);
                Assert.AreEqual(42f, so.FindProperty("typeRate").floatValue, 1e-4f);
            }
        }

        [Test]
        public void ReactionCutscene_MainScene_KeepsTodaysBeats()
        {
            using (var main = new MainSceneFixture())
            {
                var so = new SerializedObject(main.FlowField<ReactionCutscene>("reaction"));
                Pin(so, "opening", "fadeOut", 0.12f, "fadeIn", 0.15f);
                Pin(so, "gathering", "end", 2.05f, "ramp", 2);
                Pin(so, "bursting", "flash", 1, "flashSeconds", 0.9f, "shake", 5, "shakeSeconds", 0.9f, "implosionOrder", 312);
                AssertBurst(so, "bursting.burst", 60, 190, 1.1f, 312, 40, 2.5f, 2.5f, 2.5f, 1);
                Pin(so, "revealing", "riseAt", 2.3f, "rise", 1.2f, "resultFade", 1.6f, "wordsAt", 2.9f, "wordsFade", 0.25f);
                Pin(so, "tallying", "at", 3.4f, "seconds", 0.9f);
                Pin(so, "closing", "settled", 4.5f, "autoEnd", 7.5f, "fadeOut", 0.15f, "fadeIn", 0.18f);
                Pin(so, "brew", "runeBright", 1.5f, "runePulseBase", 0.75f, "runePulseAmplitude", 0.25f, "runePulseSpeed", 4, "runeLight", 0.6f, "frameRate", 6, "brewBright", 1.4f,
                    "alphaBase", 0.35f, "alphaAfter", 0.5f, "alphaGather", 0.25f, "glowBase", 0.2f, "glowAfter", 0.55f, "glowGain", 0.25f,
                    "lightGain", 0.5f, "lightBase", 0.2f, "lightAfter", 1.3f, "matLight", 1.1f, "matGlow", 0.75f);
                AssertRgba(so, "brew.runeStart", new Color(0.75f, 0.5f, 1, 1)); AssertRgba(so, "brew.brewStart", new Color(0.35f, 0.15f, 0.6f, 1)); AssertRgba(so, "brew.brewGain", new Color(0.4f, 0.25f, 0, 0));
                Pin(so, "prize", "glowAlpha", 0.4f, "lightStrength", 1.25f, "pillarBright", 1.2f, "pillarAlpha", 0.6f, "pillarPulseBase", 0.85f, "pillarPulseAmplitude", 0.15f, "pillarPulseSpeed", 9,
                    "bobSpeed", 2.2f, "bobAmplitude", 3, "raySpin", 20, "shortRay", 0.7f, "ringSpeed", 0.55f, "ringWidth", 60, "ringGrowW", 300, "ringHeight", 14, "ringGrowH", 70, "ringBright", 1.5f, "ringAlpha", 0.8f);
                AssertRgba(so, "prize.rayColor", new Color(1, 0.95f, 0.75f, 0.28f));
                Pin(so, "layout", "rimY", 161, "resultLift", 20, "resultRise", 62, "spiralRadius", 150, "spiralPower", 1.4f, "spiralMin", 4, "spiralSpin", 9, "spiralFlat", 0.3f,
                    "spiralY", 70, "spiralDrop", 84, "bannerY", 38, "titleY", 180, "wordSlide", 6, "plusY", 230, "plusShift", 84);
                Pin(so, "sparkle", "trailJitter", 5, "trailSpeed", 10, "trailLife", 0.45f, "trailBright", 2, "trailTwinkle", 0.25f, "rate", 45, "spread", 60, "speedX", 45,
                    "speedYMin", 50, "speedYMax", 120, "life", 1.6f, "gravity", -8, "bright", 2);
                AssertRgba(so, "sparkle.white", new Color(2, 1.8f, 1.2f, 1));
            }
        }

        [Test]
        public void WindowView_MainScene_KeepsTodaysEffects()
        {
            using (var main = new MainSceneFixture())
            {
                var so = new SerializedObject(main.FlowField<WindowView>("window"));
                Pin(so, "timing", "dustPeriod", 9, "dripPeriod", 3.2f, "dripFall", 0.3f, "breathePeriod", 5, "flickerPeriod", 7, "flickerDip", 0.28f, "flickerRate", 22, "rootStep", 0.45f);
                AssertVec2(so, "layout.moteX", 4, 14); AssertVec2(so, "layout.moteY", -22, 40); AssertVec2(so, "layout.dripX", -16, 16);
                AssertVec2(so, "layout.glowAt", 22, -12); AssertVec2(so, "layout.pointAt", 12, -30);
                Pin(so, "layout", "dripTop", 34, "dripDrop", 66);
                AssertRgba(so, "look.mote", new Color(0.85f, 0.85f, 0.8f, 0.55f)); AssertRgba(so, "look.glow", new Color(1, 0.68f, 0.28f, 0.08f));
                AssertRgba(so, "look.point", new Color(1, 0.82f, 0.4f, 0.5f));
                Pin(so, "look", "glowSwing", 0.2f, "dipBase", 0.1f, "dipSwing", 0.5f);
                Color32 bg = so.FindProperty("look.rootBackground").colorValue;
                Assert.AreEqual(new Color32(23, 19, 28, 255), bg, "the roots' background");
            }
        }

        [Test]
        public void Tutorial_MainScene_KeepsTodaysTargetsAndStyle()
        {
            using (var main = new MainSceneFixture())
            {
                var tutorial = main.FlowField<TutorialController>("tutorial");
                var so = new SerializedObject(tutorial);
                AssertRect(so, "targets.hand", 126, 284, 392, 80); AssertRect(so, "targets.ignite", 515, 304, 122, 42);
                AssertRect(so, "targets.journal", 516, 284, 122, 24); AssertRect(so, "targets.rack", 126, 252, 392, 32); AssertRect(so, "targets.offer", 0, 60, 640, 240);
                AssertVec2(so, "targets.tileSize", 72, 44); AssertVec2(so, "targets.tileShift", 36, 24);
                Pin(so, "targets", "redrawMargin", 2);
                Pin(so, "style", "baseOrder", 600, "popupOrderOffset", 12, "brokenOrderOffset", 6, "boxWidth", 304, "boxBorder", 1, "textWrapWidth", 288, "boxPadding", 12, "readExtra", 12,
                    "boxMaxHeight", 86, "linePitch", 12, "maxLines", 4, "linePitchVi", 16, "maxLinesVi", 3, "journalBoxDrop", 248, "popupPitch", 20, "popupFontSize", 16,
                    "holeSnap", 2, "clickGuard", 0.25f, "rimBase", 0.55f, "rimAmplitude", 0.3f, "rimSpeed", 5, "bumpDecay", 3,
                    "arrowAbove", 10, "arrowBelow", 8, "arrowFlipY", 80, "arrowBobSpeed", 6, "arrowBobAmplitude", 2);
                AssertVec2(so, "style.boxPos", 168, 14); AssertVec2(so, "style.textInset", 8, 7); AssertVec2(so, "style.popupPos", 320, 70);
                AssertHex(so, "style.popupColor", "#f0dcc0"); AssertHex(so, "style.popupTotalColor", "#fff6b8"); AssertHex(so, "style.popupShadow", "#0c0706");
                AssertRgba(so, "style.brokenGhost", new Color(0.45f, 0.45f, 0.45f, 0.35f));
                var holes = MainSceneFixture.Field<List<TutorialHole>>(tutorial, "holes");
                Assert.IsNotEmpty(holes);
                foreach (var h in holes) Assert.AreEqual(2f, new SerializedObject(h).FindProperty("rimThickness").floatValue, "rim thickness of " + h.name);
            }
        }

        [Test]
        public void Depth_MainScene_KeepsTodaysRigSwayAndTable()
        {
            using (var main = new MainSceneFixture())
            {
                var rigComponent = main.All<DepthRig>().Single();
                var rig = new SerializedObject(rigComponent);
                AssertFloats(rig, "", new Dictionary<string, float> { { "distance", 600 }, { "near", 50 }, { "far", 2000 }, { "anchorY", 188 } });
                Assert.AreSame(main.All<DepthTable>().Single(), rigComponent.table, "the rig's table is the scene's table");
                var sway = new SerializedObject(main.All<CameraSway>().Single());
                Assert.AreEqual(4f, sway.FindProperty("amplitude").floatValue, 1e-4f, "amplitude");
                Pin(sway, "timing", "periodX", 6, "periodY", 9, "shakeRateX", 53.1f, "shakeRateY", 41.7f);
                var table = new SerializedObject(main.All<DepthTable>().Single());
                AssertFloats(table, "", new Dictionary<string, float>
                {
                    { "distance", 600 }, { "front", 660 }, { "back", 838.2f }, { "topY", 132 }, { "rows", 122 }, { "artX", 102 }, { "artY", 132 },
                });
            }
        }

        [Test]
        public void Effects_MainScene_KeepsTodaysTiltShiftFogAndShake()
        {
            using (var main = new MainSceneFixture())
            {
                var tilt = new SerializedObject(main.All<TiltShift>().Single());
                AssertFloats(tilt, "", new Dictionary<string, float> { { "bandTop", 145 }, { "bandBottom", 235 }, { "feather", 110 }, { "maxSigma", 1.5f } });
                var fogs = main.All<FogSheet>();
                Assert.AreEqual(2, fogs.Length);
                CollectionAssert.AreEquivalent(new[] { 4f, -6f }, fogs.Select(f => new SerializedObject(f).FindProperty("speed").floatValue).ToArray());
                foreach (var f in fogs) Assert.AreEqual(320, new SerializedObject(f).FindProperty("width").intValue);
                var fx = new SerializedObject(main.All<ScreenFx>().Single());
                Pin(fx, "shake", "rateX", 97, "rateY", 71, "yScale", 0.8f);
            }
        }
    }
}
