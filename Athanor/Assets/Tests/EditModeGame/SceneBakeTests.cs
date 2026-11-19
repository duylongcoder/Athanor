using System.Collections.Generic;
using System.Reflection;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Athanor.Tests
{
    /// <summary>The scene objects of Main.unity that are not prefabs (Task 15): their serialized parts are all there, and Bind finishes them.</summary>
    public class SceneBakeTests
    {
        MainSceneFixture main;

        [SetUp] public void Open() => main = new MainSceneFixture();
        [TearDown] public void Close() => main?.Dispose();

        static T F<T>(object o, string n) => MainSceneFixture.Field<T>(o, n);

        [Test]
        public void TutorialTargets_RedrawAndJournalSteps_MatchTheViews()
        {
            // the Redraw step lights the button's rectangle (556,88,76,20) plus the TutorialTargets margin (2)
            var tutorial = main.FlowField<TutorialController>("tutorial");
            var fField = typeof(TutorialController).GetField("f", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(fField, "TutorialController.f");
            fField.SetValue(tutorial, main.Flow);
            var redraw = typeof(TutorialController).GetProperty("RedrawRect", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(redraw, "TutorialController.RedrawRect");
            Assert.AreEqual(new Rect(554, 86, 80, 24), (Rect)redraw.GetValue(tutorial));

            // the Journal steps light the page's recipe lines, the index line and the hints, whether those are statics or the view's own
            var jv = main.Flow.JournalView;
            Rect Of(string name)
            {
                const BindingFlags all = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public;
                var prop = typeof(JournalView).GetProperty(name, all);
                Assert.IsNotNull(prop, "JournalView." + name);
                return (Rect)prop.GetValue(prop.GetMethod.IsStatic ? null : jv);
            }
            Assert.AreEqual(new Rect(356, 156, 236, 60), Of("RecipesRect"));
            Assert.AreEqual(new Rect(356, 136, 236, 22), Of("IndexRect"));
            Assert.AreEqual(new Rect(51, 253, 272, 36), Of("HintsRect"));
        }

        [Test]
        public void GameFlow_Baked_HasSparksField()
        {
            Assert.IsNotNull(main.FlowField<Sparks>("sparks"), "GameFlow.sparks");
        }

        [Test]
        public void ScreenFx_Baked_HasFlashAndFadeSprites()
        {
            var fx = main.FlowField<ScreenFx>("screenFx");
            Assert.IsNotNull(fx, "GameFlow.screenFx");
            Assert.AreNotSame(main.Flow.gameObject, fx.gameObject, "its own scene object, not GameFlow's");
            var flash = F<SpriteRenderer>(fx, "flash"); var black = F<SpriteRenderer>(fx, "black");
            Assert.IsNotNull(flash); Assert.IsNotNull(black);
            Assert.AreEqual(Color.clear, flash.color); Assert.AreEqual(Color.clear, black.color);

            var cam = new GameObject("test camera").AddComponent<Camera>();
            try
            {
                fx.Bind(cam);
                fx.SetBlack(1);
                Assert.AreEqual(1f, black.color.a);
            }
            finally { Object.DestroyImmediate(cam.gameObject); }
        }

        [Test]
        public void Window_Baked_HasPaneAndEveryEffectSprite_AndShowStartsAnEffect()
        {
            var w = main.FlowField<WindowView>("window");
            Assert.IsNotNull(w, "GameFlow.window");
            Assert.AreNotSame(main.Flow.gameObject, w.gameObject, "its own scene object, not GameFlow's");
            Assert.IsNotNull(F<SpriteRenderer>(w, "pane"), "the workshop's window sprite");
            Assert.IsNotNull(F<SpriteRenderer>(w, "glow")); Assert.IsNotNull(F<SpriteRenderer>(w, "point"));
            var motes = F<SpriteRenderer[]>(w, "motes"); var drips = F<SpriteRenderer[]>(w, "drips");
            Assert.AreEqual(6, motes.Length); Assert.AreEqual(2, drips.Length);
            foreach (var m in motes) { Assert.IsNotNull(m); Assert.IsFalse(m.enabled, "hidden until a depth shows"); }
            foreach (var d in drips) { Assert.IsNotNull(d); Assert.IsFalse(d.enabled); }

            w.Bind();
            w.Show(1);
            typeof(WindowView).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(w, null);
            Assert.IsTrue(motes[0].enabled, "depth 1: dust motes");
            w.ShowNight();
            Assert.IsFalse(motes[0].enabled, "the night sky has no effect");
        }

        [Test]
        public void Tutorial_Baked_HasItsBoxArrowAndCellRoot_ClosedUntilAStepShows()
        {
            var t = main.FlowField<TutorialController>("tutorial");
            Assert.IsNotNull(t, "GameFlow.tutorial");
            Assert.AreNotSame(main.Flow.gameObject, t.gameObject, "its own scene object, not GameFlow's");
            var root = F<Transform>(t, "root");
            Assert.IsNotNull(root); Assert.IsNotNull(F<Transform>(t, "extras"));
            Assert.IsNotNull(F<Label>(t, "line"));
            Assert.IsNotNull(F<SpriteRenderer>(t, "boxFill")); Assert.IsNotNull(F<SpriteRenderer>(t, "boxEdge")); Assert.IsNotNull(F<SpriteRenderer>(t, "arrow"));
            Assert.IsFalse(root.gameObject.activeSelf);
            Assert.AreEqual(0, F<Transform>(t, "extras").childCount);
            var dim = F<SpriteRenderer>(t, "dim");
            Assert.IsNotNull(dim, "the dim"); Assert.AreEqual(SpriteMaskInteraction.VisibleOutsideMask, dim.maskInteraction, "the holes are an inverse mask");
            Assert.IsNotNull(F<Clickable>(t, "lockPad")); Assert.IsNotNull(F<Clickable>(t, "readPad"));
            var holes = F<List<TutorialHole>>(t, "holes");
            Assert.GreaterOrEqual(holes.Count, 4, "a pool for the widest step (hand and three tiles)");
            foreach (var h in holes) Assert.IsNotNull(h);

            t.Bind(main.Flow);
            Assert.AreSame(main.Flow, F<GameFlow>(t, "f"));
        }

        static void AllSet<T>(T[] items, int length, string what) where T : Object
        {
            Assert.AreEqual(length, items.Length, what);
            for (int i = 0; i < items.Length; i++) Assert.IsNotNull(items[i], what + "[" + i + "]");
        }

        [Test]
        public void Reaction_Baked_HasEveryPart_AndBindHidesTheStageAndSwitchesTheOthers()
        {
            var r = main.FlowField<ReactionCutscene>("reaction");
            Assert.IsNotNull(r, "GameFlow.reaction");
            Assert.AreNotSame(main.Flow.gameObject, r.gameObject, "its own scene object, not GameFlow's");
            var room = F<GameObject>(r, "room");
            Assert.AreSame(main.Flow.cutsceneRoom, room);
            var stage = F<Transform>(r, "stage"); var ui = F<Transform>(r, "ui");
            Assert.IsNotNull(stage); Assert.IsNotNull(ui);
            Assert.AreSame(room.transform, stage.parent); Assert.AreSame(room.transform, ui.parent);
            foreach (var n in new[] { "rune", "liquid", "liquidGlow", "pillar", "result", "resultGlow" }) Assert.IsNotNull(F<SpriteRenderer>(r, n), n);
            foreach (var n in new[] { "runeLight", "liquidLight", "resultLight" }) Assert.IsNotNull(F<Light2D>(r, n), n);
            foreach (var n in new[] { "headerLeft", "headerRight", "skipHint", "banner", "title", "plus", "times", "total" }) Assert.IsNotNull(F<Label>(r, n), n);
            int mats = Athanor.Core.ContentDb.MaxItems;
            AllSet(F<SpriteRenderer[]>(r, "mats"), mats, "mats"); AllSet(F<SpriteRenderer[]>(r, "matGlow"), mats, "matGlow");
            AllSet(F<Light2D[]>(r, "matLight"), mats, "matLight"); AllSet(F<SpriteRenderer[]>(r, "rings"), 3, "rings");
            var rays = F<Transform[]>(r, "rays");
            AllSet(rays, 14, "rays");
            foreach (var ray in rays) Assert.IsNotNull(ray.GetChild(0).GetComponent<SpriteRenderer>(), "each ray has its sprite");
            AllSet(F<Sprite[]>(r, "liquidFrames"), 4, "liquidFrames");
            Assert.IsFalse(stage.gameObject.activeSelf); Assert.IsFalse(ui.gameObject.activeSelf);

            var other = new GameObject("other"); other.SetActive(false); room.SetActive(true);
            var rx = TestFx.Make(null);
            try
            {
                r.Bind(new[] { other }, rx);
                Assert.IsTrue(other.activeSelf, "what the cutscene hides is shown again");
                Assert.IsFalse(room.activeSelf, "the room is hidden");
            }
            finally { Object.DestroyImmediate(other); Object.DestroyImmediate(rx.Screen.gameObject); }
        }

        [Test]
        public void Story_Baked_HasEveryPart_AndTheRayFacesAreTheRaySprites()
        {
            var s = main.FlowField<SimpleCutscene>("story");
            Assert.IsNotNull(s, "GameFlow.story");
            Assert.AreNotSame(main.Flow.gameObject, s.gameObject, "its own scene object, not GameFlow's");
            Assert.AreSame(main.Flow.cutsceneRoom, F<GameObject>(s, "room"));
            Assert.AreSame(main.Flow.workshop, F<GameObject>(s, "workshop"));
            Assert.AreSame(main.Flow.Bench.gameObject, F<GameObject>(s, "bench"));
            var root = F<Transform>(s, "root");
            Assert.IsNotNull(root); Assert.AreSame(s.transform, root.parent);
            Assert.IsFalse(root.gameObject.activeSelf, "hidden until a story plays");
            foreach (var n in new[] { "shade", "glow", "ring", "rune", "icon", "iconGlow" }) Assert.IsNotNull(F<SpriteRenderer>(s, n), n);
            foreach (var n in new[] { "small", "big", "banner", "prompt", "intro" }) Assert.IsNotNull(F<Label>(s, n), n);
            Assert.IsNotNull(F<Light2D>(s, "iconLight"));
            AllSet(F<SpriteRenderer[]>(s, "bars"), 4, "bars"); AllSet(F<Label[]>(s, "lines"), 3, "lines");
            var rays = F<Transform[]>(s, "rays"); var faces = F<SpriteRenderer[]>(s, "rayFaces");
            AllSet(rays, 12, "rays"); AllSet(faces, 12, "rayFaces");
            for (int k = 0; k < rays.Length; k++) Assert.AreSame(rays[k].GetChild(0).GetComponent<SpriteRenderer>(), faces[k], "ray " + k);
            Assert.IsTrue(F<Label>(s, "big").LineBox, "the depth name keeps its line box");
        }
    }
}
