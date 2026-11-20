using Athanor.Core;
using Athanor.Game;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Constraints;
using Is = UnityEngine.TestTools.Constraints.Is;

namespace Athanor.Tests
{
    /// <summary>Idle frames allocate nothing: a component that has nothing to change this frame builds no string, looks up no sprite and makes no closure.</summary>
    public class HotPathTests
    {
        readonly System.Collections.Generic.List<GameObject> made = new System.Collections.Generic.List<GameObject>();

        GameObject Make(string name, params System.Type[] parts) { var g = new GameObject(name, parts); made.Add(g); return g; }

        [TearDown]
        public void DestroyMade()
        {
            foreach (var g in made) if (g != null) Object.DestroyImmediate(g);
            made.Clear();
        }

        const string HudPrefab = "Assets/Prefabs/Parts/Hud.prefab";

        static ContentDb Db() => new ContentDb(JsonUtility.FromJson<ContentData>(Resources.Load<TextAsset>("content").text));

        static Label PotencyLabel(HudView hud) =>
            (Label)typeof(HudView).GetField("potency", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(hud);

        static HudView ShownHud(PrefabFixture f)
        {
            var db = Db();
            var hud = f.Get<HudView>();
            hud.Bind();
            hud.Show(new RunState(db, new Journal(), 5), db, new Journal(), true);
            hud.SetPotency(120, instant: true);
            return hud;
        }

        [Test]
        public void Tick_IdleHud_AllocatesNothing()
        {
            var f = new PrefabFixture(HudPrefab);
            try
            {
                var hud = ShownHud(f);
                hud.Tick(0.016f);                                    // first frame may lay out
                Assert.That(() => hud.Tick(0.016f), Is.Not.AllocatingGCMemory());
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Tick_PotencyUnchanged_KeepsTheSameText()
        {
            var f = new PrefabFixture(HudPrefab);
            try
            {
                var hud = ShownHud(f);
                hud.Tick(0.016f);
                string first = PotencyLabel(hud).Text;
                hud.Tick(0.016f);
                Assert.AreSame(first, PotencyLabel(hud).Text);
                Assert.AreEqual(Loc.Number(120), first);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Tick_PotencyChanged_RewritesText()
        {
            var f = new PrefabFixture(HudPrefab);
            try
            {
                var hud = ShownHud(f);
                hud.Tick(0.016f);
                hud.SetPotency(5000, instant: true);
                hud.Tick(0.016f);
                Assert.AreEqual(Loc.Number(5000), PotencyLabel(hud).Text, "an instant jump is drawn");
                hud.SetPotency(9000);                                // counts up: not there after one frame, there after enough of them
                hud.Tick(0.016f);
                Assert.AreNotEqual(Loc.Number(9000), PotencyLabel(hud).Text, "still counting");
                for (int i = 0; i < 600; i++) hud.Tick(0.016f);
                Assert.AreEqual(Loc.Number(9000), PotencyLabel(hud).Text, "the count arrived and was drawn");
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Tick_PotencyRunsIntoTarget_SwapsTheWordAwayAndBack()
        {
            var f = new PrefabFixture(HudPrefab);
            try
            {
                var hud = ShownHud(f);
                var word = (Label)typeof(HudView).GetField("potencyWord", NonPublic).GetValue(hud);
                hud.Tick(0.016f);
                Assert.IsTrue(word.gameObject.activeSelf, "a short number keeps the word");
                hud.SetPotency(100000000000L, instant: true);
                hud.Tick(0.016f);
                Assert.IsFalse(word.gameObject.activeSelf, "a number long enough to run into the target takes the word's place");
                hud.SetPotency(120, instant: true);
                hud.Tick(0.016f);
                Assert.IsTrue(word.gameObject.activeSelf, "and the word comes back");
            }
            finally { f.Destroy(); }
        }

        // ---- the rest of the idle updates ----------------------------------------------------------

        const string BenchPrefab = "Assets/Prefabs/Views/Bench.prefab", CardPrefab = "Assets/Prefabs/Parts/Card.prefab", EventPrefab = "Assets/Prefabs/Views/Event.prefab";
        const BindingFlags NonPublic = BindingFlags.Instance | BindingFlags.NonPublic;

        static T Field<T>(object owner, string name) => (T)owner.GetType().GetField(name, NonPublic).GetValue(owner);

        /// <summary>A bench on a fresh run with one card placed on tile 4, input on and the pointer far from everything.</summary>
        static BenchView ReadyBench(PrefabFixture f)
        {
            var db = Db();
            var run = new RunState(db, new Journal(), 5);
            var journal = new Journal();
            var view = f.Get<BenchView>();
            view.Bind(db, TestFx.Make(f.Root.transform));
            view.Bind(run, journal);
            run.Place(0, 4);
            view.Refresh();
            view.InputEnabled = true;
            PointerInput.Resolve(new Vector2(-10000, -10000));
            return view;
        }

        [Test]
        public void Tick_IdleBench_AllocatesNothing()
        {
            var f = new PrefabFixture(BenchPrefab);
            try
            {
                var view = ReadyBench(f);
                view.Tick(0.016f);
                Assert.That(() => view.Tick(0.016f), Is.Not.AllocatingGCMemory());
            }
            finally { PointerInput.ResetStatics(); f.Destroy(); }
        }

        [Test]
        public void Tick_BenchWithTooltipUp_AllocatesNothing()
        {
            var f = new PrefabFixture(BenchPrefab);
            try
            {
                var view = ReadyBench(f);
                var tip = Field<Tooltip>(view, "tip");
                Assert.IsFalse(tip.gameObject.activeSelf, "no tip before the pointer rests");
                Field<Clickable>(view.Hud, "redrawHit").onEnter();           // the pointer rests on Redraw
                view.Tick(0.2f); view.Tick(0.2f);                            // past the tip delay: the tip is built once
                Assert.IsTrue(tip.gameObject.activeSelf, "the tip is up");
                Assert.IsNotEmpty(Field<Label>(tip, "title").Text, "and it says something");
                Assert.That(() => view.Tick(0.016f), Is.Not.AllocatingGCMemory());
            }
            finally { PointerInput.ResetStatics(); f.Destroy(); }
        }

        [Test]
        public void Tick_TooltipUp_RebuildsAfterRedrawsChange()
        {
            var f = new PrefabFixture(BenchPrefab);
            try
            {
                var db = Db();
                var run = new RunState(db, new Journal(), 5);
                var view = f.Get<BenchView>();
                view.Bind(db, TestFx.Make(f.Root.transform));
                view.Bind(run, new Journal());
                view.Refresh();
                view.InputEnabled = true;
                PointerInput.Resolve(new Vector2(-10000, -10000));
                var body = Field<Label>(Field<Tooltip>(view, "tip"), "body");
                Field<Clickable>(view.Hud, "redrawHit").onEnter();
                view.Tick(0.2f); view.Tick(0.2f);
                string before = body.Text;
                Assert.IsNotEmpty(before, "the Redraw tip says how many are left");

                while (run.RedrawsLeft > 0) Assert.IsTrue(run.Redraw());     // no Refresh: only the count behind the tip changed
                view.Tick(0.016f);

                Assert.AreNotEqual(before, body.Text, "the tip was built again for the new count");
            }
            finally { PointerInput.ResetStatics(); f.Destroy(); }
        }

        [Test]
        public void Tick_TileHoverStartsAndStops_DrawsAndClearsTheFace()
        {
            var f = new PrefabFixture(BenchPrefab);
            try
            {
                var view = ReadyBench(f);
                var tile = view.Tile(0);
                var face = Field<SpriteRenderer>(tile, "face");
                tile.Tick(0.016f);
                Assert.AreEqual(0f, face.color.a, "an idle tile shows no face glow");
                tile.Hovered = true;
                tile.Tick(0.016f);
                Assert.Greater(face.color.a, 0f, "pointed at: the face glows");
                tile.Hovered = false;
                tile.Tick(0.016f);
                Assert.AreEqual(Color.clear, face.color, "no longer pointed at: the glow is cleared");
            }
            finally { PointerInput.ResetStatics(); f.Destroy(); }
        }

        [Test]
        public void Tick_TileAndTileWithMaterial_AllocateNothing()
        {
            var f = new PrefabFixture(BenchPrefab);
            try
            {
                var view = ReadyBench(f);
                foreach (int i in new[] { 0, 4 })
                {
                    var tile = view.Tile(i);
                    tile.Tick(0.016f);
                    Assert.That(() => tile.Tick(0.016f), Is.Not.AllocatingGCMemory(), "tile " + i);
                }
            }
            finally { PointerInput.ResetStatics(); f.Destroy(); }
        }

        [Test]
        public void Tick_IdleCard_AllocatesNothing()
        {
            var f = new PrefabFixture(CardPrefab);
            try
            {
                var db = Db();
                var card = f.Get<CardView>();
                card.Show(db.Material(new RunState(db, new Journal(), 5).Hand[0]), 110);
                card.Fan(74, 130);
                card.home = card.transform.position;
                card.Tick(0.016f);
                Assert.That(() => card.Tick(0.016f), Is.Not.AllocatingGCMemory());
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Tick_IdleEventView_AllocatesNothing()
        {
            var f = new PrefabFixture(EventPrefab);
            try
            {
                var view = f.Get<EventView>();
                typeof(EventView).GetField("count", NonPublic).SetValue(view, 2);
                view.Tick(0.016f);
                Assert.That(() => view.Tick(0.016f), Is.Not.AllocatingGCMemory());
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Tick_IdleUiButton_AllocatesNothing()
        {
            var f = new PrefabFixture(EventPrefab);
            try
            {
                var button = Field<UiButton>(f.Get<EventView>(), "proceed");
                button.Tick(0.016f);
                Assert.That(() => button.Tick(0.016f), Is.Not.AllocatingGCMemory());
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Tick_FogSheet_AllocatesNothingWithinATexelAndWhenOneIsCrossed()
        {
            var go = Make("fog", typeof(SpriteRenderer), typeof(FogSheet));
            try
            {
                var fog = go.GetComponent<FogSheet>();
                fog.speed = 4; fog.width = 320;
                fog.Tick(10f);
                Assert.That(() => fog.Tick(10.01f), Is.Not.AllocatingGCMemory());       // same texel
                Assert.That(() => fog.Tick(10.5f), Is.Not.AllocatingGCMemory());        // a new texel: still no garbage (the block is reused)
            }
            finally { Object.DestroyImmediate(go); }
        }

        [Test]
        public void Tick_FogSheet_WritesTheNewTexelOffsetAndOnlyThen()
        {
            var go = Make("fog", typeof(SpriteRenderer), typeof(FogSheet));
            try
            {
                var fog = go.GetComponent<FogSheet>();
                fog.speed = 4; fog.width = 320;
                var sheet = go.GetComponent<SpriteRenderer>();
                var block = new MaterialPropertyBlock();
                float Drawn() { sheet.GetPropertyBlock(block); return block.GetFloat("_ScrollTexels"); }
                fog.Tick(10f);
                Assert.AreEqual(40f, Drawn(), "t = 10 s at 4 texels a second");
                fog.Tick(10.01f);
                Assert.AreEqual(40f, Drawn(), "within the texel nothing moves");
                fog.Tick(10.5f);
                Assert.AreEqual(42f, Drawn(), "a new texel is drawn");
            }
            finally { Object.DestroyImmediate(go); }
        }

        [Test]
        public void Tick_ScreenFxFlashEnds_ClearsTheFlashSprite()
        {
            var go = Make("fx", typeof(ScreenFx));
            var camGo = Make("cam", typeof(Camera));
            var flash = Make("flash", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>();
            try
            {
                var fx = go.GetComponent<ScreenFx>();
                typeof(ScreenFx).GetField("flash", NonPublic).SetValue(fx, flash);
                fx.Bind(camGo.GetComponent<Camera>());
                fx.Tick(0.016f);
                fx.Flash(1f, 0.2f);
                fx.Tick(0.05f);
                Assert.Greater(flash.color.a, 0f, "the flash is showing");
                fx.Tick(0.2f); fx.Tick(0.016f);
                Assert.AreEqual(Color.clear, flash.color, "it ended and the sprite was cleared once");
            }
            finally { Object.DestroyImmediate(go); Object.DestroyImmediate(camGo); Object.DestroyImmediate(flash.gameObject); }
        }

        [Test]
        public void Tick_IdleScreenFx_AllocatesNothing()
        {
            var go = Make("fx", typeof(ScreenFx));
            var camGo = Make("cam", typeof(Camera));
            var flash = Make("flash", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>();
            try
            {
                var fx = go.GetComponent<ScreenFx>();
                typeof(ScreenFx).GetField("flash", NonPublic).SetValue(fx, flash);
                fx.Bind(camGo.GetComponent<Camera>());
                fx.Tick(0.016f);
                Assert.That(() => fx.Tick(0.016f), Is.Not.AllocatingGCMemory());
            }
            finally { Object.DestroyImmediate(go); Object.DestroyImmediate(camGo); Object.DestroyImmediate(flash.gameObject); }
        }

        // ---- the figure for docs/ATHANOR.md section 6 -------------------------------------------------

        const int IdleFrames = 600;     // 10 s at 60 fps

        [Test]
        public void IdleBench_TenSecondsOfEveryUpdate_AllocatesZeroBytes()
        {
            var f = new PrefabFixture(BenchPrefab);
            try
            {
                var view = ReadyBench(f);
                var hud = view.Hud;
                var tiles = new TileView[view.TileCount];
                for (int i = 0; i < tiles.Length; i++) tiles[i] = view.Tile(i);
                var cards = f.Root.GetComponentsInChildren<CardView>(true);
                var links = f.Root.GetComponentsInChildren<LinkView>(true);
                var flames = f.Root.GetComponentsInChildren<Flame>(true);
                // Update is private; a delegate calls it without the garbage MethodInfo.Invoke makes
                var linkUpdates = System.Array.ConvertAll(links, l => (System.Action)System.Delegate.CreateDelegate(typeof(System.Action), l, typeof(LinkView).GetMethod("Update", NonPublic)));
                var flameUpdates = System.Array.ConvertAll(flames, l => (System.Action)System.Delegate.CreateDelegate(typeof(System.Action), l, typeof(Flame).GetMethod("Update", NonPublic)));
                System.Action frame = () =>
                {
                    view.Tick(0.016f); hud.Tick(0.016f);
                    foreach (var t in tiles) t.Tick(0.016f);
                    foreach (var c in cards) c.Tick(0.016f);
                    foreach (var u in linkUpdates) u();
                    foreach (var u in flameUpdates) u();
                };
                frame(); frame();                                                // the first frames may lay out
                Assert.That(() => { for (int n = 0; n < 60; n++) view.Tick(0.016f); }, Is.Not.AllocatingGCMemory(), "bench");
                Assert.That(() => { for (int n = 0; n < 60; n++) hud.Tick(0.016f); }, Is.Not.AllocatingGCMemory(), "hud");
                Assert.That(() => { for (int n = 0; n < 60; n++) foreach (var t in tiles) t.Tick(0.016f); }, Is.Not.AllocatingGCMemory(), "tiles");
                Assert.That(() => { for (int n = 0; n < 60; n++) foreach (var c in cards) c.Tick(0.016f); }, Is.Not.AllocatingGCMemory(), "cards");
                Assert.That(() => { for (int n = 0; n < 60; n++) foreach (var u in linkUpdates) u(); }, Is.Not.AllocatingGCMemory(), "links");
                Assert.That(() => { for (int n = 0; n < 60; n++) foreach (var u in flameUpdates) u(); }, Is.Not.AllocatingGCMemory(), "flames");
                // the engine's own GC.Alloc counter (the one the Profiler's GC Alloc column shows) over 10 s of frames: no sample at all
                Assert.That(() => { for (int n = 0; n < IdleFrames; n++) frame(); }, Is.Not.AllocatingGCMemory());
                Debug.Log("[hotpath] idle bench: " + IdleFrames + " frames, " + tiles.Length + " tiles, " + cards.Length + " cards, " + links.Length + " link layers, " + flames.Length + " flames: 0 B allocated");
            }
            finally { PointerInput.ResetStatics(); f.Destroy(); }
        }

        [Test]
        public void Continuous_DustSparksAndFlame_AllocateNothingWhileRunning()
        {
            var dustGo = Make("dust", typeof(Dust));
            var flameGo = Make("flame", typeof(Flame));
            var fxGo = Make("fx");
            try
            {
                var dust = dustGo.GetComponent<Dust>();
                dust.sprite = Art.Lib.Sprite("dot"); dust.tint = Color.white; dust.alphaBase = 0.2f; dust.alphaSwing = 0.3f;
                typeof(Dust).GetMethod("Start", NonPublic).Invoke(dust, null);
                var dustLate = (System.Action)System.Delegate.CreateDelegate(typeof(System.Action), dust, typeof(Dust).GetMethod("LateUpdate", NonPublic));

                var flame = flameGo.GetComponent<Flame>();
                flame.body = flameGo.AddComponent<SpriteRenderer>(); flame.halo = Make("halo", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>();
                flame.frames = new[] { Art.Lib.Sprite("dot"), Art.Lib.Sprite("sparkle") };
                var flameUpdate = (System.Action)System.Delegate.CreateDelegate(typeof(System.Action), flame, typeof(Flame).GetMethod("Update", NonPublic));

                var sparks = TestFx.Make(fxGo.transform).Sparks;
                for (int i = 0; i < 40; i++) sparks.Emit(new Vector2(i, 0), Vector2.up, Color.white, 1000, 20 + i % 3, 0, i % 4 == 0);

                System.Action frame = () => { dustLate(); flameUpdate(); sparks.Advance(0.016f); sparks.Draw(); };
                frame(); frame();
                Assert.That(() => { for (int n = 0; n < IdleFrames; n++) frame(); }, Is.Not.AllocatingGCMemory());
            }
            finally { Object.DestroyImmediate(dustGo); Object.DestroyImmediate(flameGo); Object.DestroyImmediate(fxGo); }
        }
    }
}
