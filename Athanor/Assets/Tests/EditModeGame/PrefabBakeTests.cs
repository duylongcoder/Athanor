using System.Collections.Generic;
using System.Reflection;
using Athanor.Gameplay;
using Athanor.Game;
using NUnit.Framework;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>The shipped prefabs keep what the views need: a label keeps its key, a button still clicks, the tooltip makes its panel.</summary>
    public class PrefabBakeTests
    {
        const string RunOverPrefab = "Assets/Prefabs/Views/RunOver.prefab";
        PrefabFixture fixture;

        [TearDown] public void Clean()
        {
            fixture?.Destroy();
            Loc.Set(Loc.English);
        }

        PrefabFixture Open(string path) => fixture = new PrefabFixture(path);

        /// <summary>The "Descend again" button of the RunOver prefab: a UiButton with a keyed Label and a Clickable.</summary>
        UiButton AgainButton() => Open(RunOverPrefab).Root.GetComponentsInChildren<UiButton>(true)[0];

        static string KeyOf(Label l) => (string)typeof(Label).GetField("key", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(l);

        static void Relabel(Label l) => typeof(Label).GetMethod("Relabel", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(l, null);

        [Test]
        public void Relabel_KeyedLabelInPrefab_FollowsTheLanguage()
        {
            var label = AgainButton().Text;
            Assert.AreEqual("ui.runover.again", KeyOf(label), "the key is kept in the prefab");
            Loc.Set(Loc.Vietnamese);
            Relabel(label);
            Assert.AreEqual(Loc.T("ui.runover.again"), label.Text);
        }

        [Test]
        public void Relabel_PlainLabelInPrefab_KeepsItsText()
        {
            Open(RunOverPrefab);
            var label = System.Array.Find(fixture.Root.GetComponentsInChildren<Label>(true), l => string.IsNullOrEmpty(KeyOf(l)));
            Assert.IsNotNull(label, "the prefab has a label without a key");
            label.Text = "abc";
            Relabel(label);
            Assert.AreEqual("abc", label.Text);
        }

        [Test]
        public void Click_ButtonInPrefab_RaisesClicked()
        {
            var button = AgainButton();
            button.Bind();
            int clicks = 0;
            button.Clicked += () => clicks++;
            button.GetComponent<Clickable>().onClick();
            Assert.AreEqual(1, clicks);
        }

        [Test]
        public void Click_DisabledButtonInPrefab_DoesNothing()
        {
            var button = AgainButton();
            button.Bind();
            button.Disabled = true;
            int clicks = 0;
            button.Clicked += () => clicks++;
            button.GetComponent<Clickable>().onClick();
            Assert.AreEqual(0, clicks);
        }

        [Test]
        public void Text_ButtonInPrefab_HasLabelAndBody()
        {
            var button = AgainButton();
            Assert.IsNotNull(button.Text, "label reference lost in the prefab");
            var body = typeof(UiButton).GetField("body", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(button) as SpriteRenderer;
            Assert.IsTrue(body != null, "body reference lost in the prefab");
        }

        [Test]
        public void Disabled_SetOnAButton_IsKeptInTheSerializedState()
        {
            var button = AgainButton();
            button.Disabled = true;
            Assert.IsTrue(new SerializedObject(button).FindProperty("disabled").boolValue, "a saved prefab would lose the flag");
            Assert.IsTrue(button.Disabled);
        }

        [Test]
        public void ShowDepth_RunOverPrefab_WritesTheDepthRow()
        {
            var f = new PrefabFixture("Assets/Prefabs/Views/RunOver.prefab");
            try
            {
                var view = f.Get<RunOverView>();
                view.Bind();
                view.ShowDepth(true, 4, 5);
                Assert.AreEqual("Planted at depth 4", view.DepthName.Text);
            }
            finally { f.Destroy(); }
        }

        // the two buttons in hierarchy order: "Descend again" (index 0), then "Title" (index 1)
        [TestCase(0, true)]
        [TestCase(1, false)]
        public void Click_RunOverPrefabButton_PicksItsChoice(int index, bool again)
        {
            var f = new PrefabFixture("Assets/Prefabs/Views/RunOver.prefab");
            try
            {
                var view = f.Get<RunOverView>();
                view.Bind();
                view.gameObject.SetActive(true);            // Show opens it; Pick ignores a hidden summary
                var buttons = view.GetComponentsInChildren<UiButton>(true);
                Assert.AreEqual(2, buttons.Length);

                buttons[index].GetComponent<Clickable>().onClick();
                view.Pick(!again);                          // ignored once the click has chosen; counts if the click did nothing

                Assert.AreEqual(again, view.Again);
            }
            finally { f.Destroy(); }
        }

        // a fresh run that opens on an event: the first one in the content a fresh run can meet at depth 1 (no aspect offer before it)
        static RunState EventRun()
        {
            var db = new ContentDb(ContentAssets.Data());
            foreach (var e in db.Events)
            {
                var options = new RunOptions { Unlocks = Unlocks.Fresh(db), Events = true, ForceEvent = e.id, ForceEventAt = 1 };
                var run = new RunState(db, new Journal(), 1, 1, options);
                if (run.Phase == RunPhase.EventOffer) return run;
            }
            Assert.Fail("no event in the content opens a fresh run at depth 1");
            return null;
        }

        [Test]
        public void Bind_EventPrefabWithoutFx_Throws()
        {
            var f = new PrefabFixture("Assets/Prefabs/Views/Event.prefab");
            try
            {
                var view = f.Get<EventView>();
                Assert.Throws<System.ArgumentNullException>(() => view.Bind(null));
            }
            finally { DOTween.KillAll(); f.Destroy(); }
        }

        [Test]
        public void Show_EventPrefab_ListsTheOptions()
        {
            var f = new PrefabFixture("Assets/Prefabs/Views/Event.prefab");
            try
            {
                var view = f.Get<EventView>();
                view.Bind(TestFx.Make(f.Root.transform));
                var run = EventRun();

                view.Show(run, null).MoveNext();            // up to the slide-in: heading and rows are written

                Assert.AreEqual(Wording.OptionLabel(run.CurrentEvent, run.EventOptions[0]), view.OptionLabel(0).Text);
                Assert.IsNotEmpty(view.OptionLabel(0).Text);
            }
            finally { DOTween.KillAll(); f.Destroy(); }
        }

        [Test]
        public void Click_EventPrefabLastOptionThenContinue_ShowsTheOutcomeAndCloses()
        {
            var f = new PrefabFixture("Assets/Prefabs/Views/Event.prefab");
            try
            {
                var view = f.Get<EventView>();
                view.Bind(TestFx.Make(f.Root.transform));
                var run = EventRun();
                int last = run.EventOptions.Count - 1;          // the walk-on option: no gain, so no screen flash
                var show = view.Show(run, null);
                show.MoveNext(); show.MoveNext();               // slide-in done, waiting for a pick

                view.OptionLabel(last).GetComponentInParent<Clickable>().onClick();
                show.MoveNext();                                // the pick is taken: outcome shown, waiting for Continue
                Assert.IsNotEmpty(view.Outcome.Text);

                view.GetComponentInChildren<UiButton>().GetComponent<Clickable>().onClick();
                for (int i = 0; i < 4 && show.MoveNext(); i++) { }   // fade out, then hide

                Assert.IsFalse(view.gameObject.activeSelf);
            }
            finally { DOTween.KillAll(); f.Destroy(); }
        }

        // a goal with no reward item: the toast needs no content database for it
        static GoalReward[] OneGoal() => new[] { new GoalReward { Goal = new GoalData { id = "g_depth2", text = "Reach depth 2" } } };

        // edit mode runs no Update; the toast takes the next item off its queue in Update
        static void Tick(GoalToast t) => typeof(GoalToast).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(t, null);

        [Test]
        public void Add_GoalToastPrefab_ShowsTheGoalName()
        {
            var f = new PrefabFixture("Assets/Prefabs/Views/GoalToast.prefab");
            try
            {
                var toast = f.Get<GoalToast>();
                toast.Bind(TestFx.Make(f.Root.transform));

                toast.Add(OneGoal(), null);
                Tick(toast);

                Assert.AreEqual("GOAL COMPLETE: REACH DEPTH 2", toast.Title.Text);
                Assert.AreEqual("Nothing was left to unlock", toast.Line.Text);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Click_ShowingGoalToastPrefab_SkipsToTheSlideOut()
        {
            var f = new PrefabFixture("Assets/Prefabs/Views/GoalToast.prefab");
            try
            {
                var toast = f.Get<GoalToast>();
                toast.Bind(TestFx.Make(f.Root.transform));
                toast.Add(OneGoal(), null);
                Tick(toast);

                toast.Title.GetComponentInParent<Clickable>(true).onClick();

                float age = (float)typeof(GoalToast).GetField("age", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(toast);
                Assert.GreaterOrEqual(age, toast.SlideOutAt);
            }
            finally { f.Destroy(); }
        }

        const string ChoicePrefab = "Assets/Prefabs/Views/CatalystChoice.prefab";

        // three cards; the middle one is a Rare catalyst (stars and tier strip), the others plain
        static List<ChoiceCard> ThreeCards() => new List<ChoiceCard>
        {
            new ChoiceCard { id = "a", sprite = "dot", name = "Alpha", text = "first" },
            new ChoiceCard { id = "b", sprite = "dot", name = "Beta", text = "second", rarity = Rarities.Rare },
            new ChoiceCard { id = "c", sprite = "dot", name = "Gamma", text = "third" },
        };

        [Test]
        public void Show_CatalystChoicePrefab_FillsThreeCards()
        {
            var f = new PrefabFixture(ChoicePrefab);
            try
            {
                var view = f.Get<CatalystChoiceView>();
                view.Bind(TestFx.Make(f.Root.transform));

                view.Choose("title", "line", ThreeCards(), _ => { }).MoveNext();     // up to the deal: the cards are written

                Assert.AreEqual("Alpha", view.CardName(0).Text);
                Assert.AreEqual("Beta", view.CardName(1).Text);
                Assert.AreEqual("Gamma", view.CardName(2).Text);
            }
            finally { DOTween.KillAll(); f.Destroy(); }
        }

        [Test]
        public void Show_CatalystChoicePrefabRareCard_DrawsThreeStars()
        {
            var f = new PrefabFixture(ChoicePrefab);
            try
            {
                var view = f.Get<CatalystChoiceView>();
                view.Bind(TestFx.Make(f.Root.transform));

                view.Choose("title", "line", ThreeCards(), _ => { }).MoveNext();

                // each star is one fill sprite above four outline copies: 3 stars = 15 drawn sprites, every one the corner-pivot glyph
                // (made at runtime, so not in the prefab; the baked centre-pivot "star" would sit on half pixels)
                var drawn = new List<SpriteRenderer>();
                foreach (var r in view.CornerStars(1).Root.GetComponentsInChildren<SpriteRenderer>(true)) if (r.enabled) drawn.Add(r);
                Assert.AreEqual(3 * 5, drawn.Count);
                foreach (var r in drawn)
                {
                    Assert.IsTrue(r.sprite != null, "a shown star has no sprite after the prefab round trip");
                    Assert.AreEqual("star_corner", r.sprite.name);
                }
            }
            finally { DOTween.KillAll(); f.Destroy(); }
        }

        [Test]
        public void Click_CatalystChoicePrefabCardAfterTheDeal_PicksItsId()
        {
            var f = new PrefabFixture(ChoicePrefab);
            try
            {
                var view = f.Get<CatalystChoiceView>();
                view.Bind(TestFx.Make(f.Root.transform));
                var choose = view.Choose("title", "line", ThreeCards(), _ => { });
                choose.MoveNext(); choose.MoveNext();             // the deal is over: the cards take a click

                view.CardName(2).GetComponentInParent<Clickable>().onClick();

                Assert.AreEqual("c", typeof(CatalystChoiceView).GetField("picked", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(view));
            }
            finally { DOTween.KillAll(); f.Destroy(); }
        }

        const string PausePrefab = "Assets/Prefabs/Views/Pause.prefab";

        static Clickable PauseClick(GameObject root, string dialog, string button) =>
            root.transform.Find(dialog).Find("Button " + button).GetComponent<Clickable>();

        [Test]
        public void Open_PausePrefab_ShowsTheMenu()
        {
            var f = new PrefabFixture(PausePrefab);
            try
            {
                var menu = f.Get<PauseMenu>();
                menu.Bind(new Settings());

                Assert.IsTrue(menu.Open());

                Assert.IsTrue(f.Root.transform.Find("Pause menu").gameObject.activeSelf);
                Assert.IsTrue(menu.IsOpen);
                Assert.AreEqual(0f, Time.timeScale);
            }
            finally { Time.timeScale = 1f; f.Destroy(); }
        }

        [Test]
        public void Click_PausePrefabResume_ClosesAndRestoresTime()
        {
            var f = new PrefabFixture(PausePrefab);
            try
            {
                var menu = f.Get<PauseMenu>();
                menu.Bind(new Settings());
                menu.Open();

                PauseClick(f.Root, "Pause menu", "ui.pause.resume").onClick();

                Assert.IsFalse(menu.IsOpen);
                Assert.AreEqual(1f, Time.timeScale);
            }
            finally { Time.timeScale = 1f; f.Destroy(); }
        }

        [Test]
        public void Click_PausePrefabCancelOnTheQuitQuestion_BacksOutToTheMenu()
        {
            var f = new PrefabFixture(PausePrefab);
            try
            {
                var menu = f.Get<PauseMenu>();
                menu.Bind(new Settings());
                menu.Open();
                PauseClick(f.Root, "Pause menu", "ui.pause.quit").onClick();

                PauseClick(f.Root, "Quit confirm", "ui.common.cancel").onClick();

                Assert.IsFalse(menu.Confirming);
                Assert.IsTrue(menu.IsOpen);
                Assert.AreEqual(0f, Time.timeScale);
            }
            finally { Time.timeScale = 1f; f.Destroy(); }
        }

        [Test]
        public void Click_PausePrefabOutsideTheDialog_Resumes()
        {
            var f = new PrefabFixture(PausePrefab);
            try
            {
                var menu = f.Get<PauseMenu>();
                menu.Bind(new Settings());
                menu.Open();

                // the full-screen shade is the first child of the menu; its click is "a click outside"
                f.Root.transform.Find("Pause menu").GetChild(0).GetComponent<Clickable>().onClick();

                Assert.IsFalse(menu.IsOpen);
                Assert.AreEqual(1f, Time.timeScale);
            }
            finally { Time.timeScale = 1f; f.Destroy(); }
        }

        const string GoalsPrefab = "Assets/Prefabs/Views/Goals.prefab";

        static ContentDb Content() =>
            new ContentDb(ContentAssets.Data());

        [Test]
        public void Bind_GoalsPrefab_MakesOneRowPerGoal()
        {
            var f = new PrefabFixture(GoalsPrefab);
            try
            {
                var db = Content();
                var view = f.Get<GoalsView>();

                view.Bind(db);

                Assert.AreEqual(db.Goals.Count, view.GetComponentsInChildren<GoalRow>(true).Length);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Open_GoalsPrefabFreshProfile_WritesEveryGoal()
        {
            var f = new PrefabFixture(GoalsPrefab);
            try
            {
                var db = Content();
                var view = f.Get<GoalsView>();
                view.Bind(db);

                view.Open(new Profile(), new Journal());

                var shown = new List<string>();
                foreach (var r in view.GetComponentsInChildren<GoalRow>(false)) shown.Add(r.text.Text);
                var expected = new List<string>();
                foreach (var g in db.Goals) expected.Add(Wording.GoalText(g));
                CollectionAssert.AreEquivalent(expected, shown);       // every goal is in one of the three sections
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Click_GoalsPrefabClose_ClosesAndTellsTheOwner()
        {
            var f = new PrefabFixture(GoalsPrefab);
            try
            {
                var view = f.Get<GoalsView>();
                view.Bind(Content());
                view.Open(new Profile(), new Journal());
                int closed = 0;
                view.Closed += () => closed++;

                view.CloseClick.onClick();

                Assert.IsFalse(view.IsOpen);
                Assert.AreEqual(1, closed);
            }
            finally { f.Destroy(); }
        }

        const string JournalPrefab = "Assets/Prefabs/Views/Journal.prefab";

        [Test]
        public void Bind_JournalPrefab_MakesOneCellPerMaterial()
        {
            var f = new PrefabFixture(JournalPrefab);
            try
            {
                var db = Content();
                var view = f.Get<JournalView>();

                view.Bind(db);

                Assert.AreEqual(db.Materials.Count, view.GetComponentsInChildren<JournalCell>(true).Length);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Click_JournalPrefabCell_ShowsThatMaterial()
        {
            var f = new PrefabFixture(JournalPrefab);
            try
            {
                var db = Content();
                var view = f.Get<JournalView>();
                view.Bind(db);
                view.Open(new Journal());                       // every starting material is open to a fresh book
                var starter = db.Starters[1];
                int index = -1;
                for (int i = 0; i < db.Materials.Count; i++) if (db.Materials[i].id == starter.id) index = i;
                Assert.GreaterOrEqual(index, 0);

                view.GetComponentsInChildren<JournalCell>(true)[index].click.onClick();

                Assert.AreEqual(starter.name, view.Title.Text);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Click_JournalPrefabPageLabel_TurnsTheGridPage()
        {
            var f = new PrefabFixture(JournalPrefab);
            try
            {
                var view = f.Get<JournalView>();
                view.Bind(Content());
                view.Open(new Journal());
                var cells = view.GetComponentsInChildren<JournalCell>(true);
                Assume.That(cells.Length, Is.GreaterThan(JournalView.PerPage), "content has a second page of materials");
                Assert.IsTrue(cells[0].icon.gameObject.activeSelf);
                Assert.IsFalse(cells[JournalView.PerPage].icon.gameObject.activeSelf);

                view.PageClick.onClick();

                Assert.IsFalse(cells[0].icon.gameObject.activeSelf);
                Assert.IsTrue(cells[JournalView.PerPage].icon.gameObject.activeSelf);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Click_JournalPrefabClose_ClosesAndTellsTheOwner()
        {
            var f = new PrefabFixture(JournalPrefab);
            try
            {
                var view = f.Get<JournalView>();
                view.Bind(Content());
                view.Open(new Journal());
                int closed = 0;
                view.Closed += () => closed++;

                view.CloseClick.onClick();

                Assert.IsFalse(view.IsOpen);
                Assert.AreEqual(1, closed);
            }
            finally { f.Destroy(); }
        }

        // the tooltip's 9-slice panel sprite is made by Sprite.Create: a prefab cannot keep it, Bind makes it again
        [Test]
        public void Show_TooltipInBenchPrefab_HasItsSlicedPanel()
        {
            var tip = Open("Assets/Prefabs/Views/Bench.prefab").Root.GetComponentInChildren<Tooltip>(true);
            Assert.IsNotNull(tip, "the Bench prefab holds the tooltip");

            tip.Bind();
            tip.Show("Name", "text", Vector2.zero);

            var panel = typeof(Tooltip).GetField("panel", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(tip) as SpriteRenderer;
            Assert.AreEqual(SpriteDrawMode.Sliced, panel.drawMode);
            Assert.AreEqual(new Vector4(4, 4, 4, 4), panel.sprite.border);
            Assert.AreEqual(new Vector2(176, 58), panel.size);
        }

        const string TitlePrefab = "Assets/Prefabs/Views/Title.prefab";

        static Clickable TitleClick(GameObject root, string button) => root.transform.Find("Button " + button).GetComponent<Clickable>();

        static T TitleField<T>(TitleView view, string name) =>
            (T)typeof(TitleView).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(view);

        [Test]
        public void Bind_TitlePrefab_ShowsTheLogoText()
        {
            var f = new PrefabFixture(TitlePrefab);
            try
            {
                var view = f.Get<TitleView>();
                view.Bind(new Settings(), TestFx.Make(f.Root.transform));

                view.Open(0, 0, 0, 0);

                Assert.IsTrue(f.Root.activeSelf);
                Assert.AreEqual(Loc.T("ui.title.logo"), TitleField<Label>(view, "logo").Text);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Click_TitlePrefabBegin_SetsBeginPressed()
        {
            var f = new PrefabFixture(TitlePrefab);
            try
            {
                var view = f.Get<TitleView>();
                view.Bind(new Settings(), TestFx.Make(f.Root.transform));
                view.Open(0, 0, 0, 0);

                TitleClick(f.Root, "ui.title.begin").onClick();

                Assert.IsTrue(view.BeginPressed);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Click_TitlePrefabSettingsThenLanguage_SwitchesTheLanguageAndBackCloses()
        {
            var f = new PrefabFixture(TitlePrefab);
            try
            {
                var settings = new Settings();
                var view = f.Get<TitleView>();
                view.Bind(settings, TestFx.Make(f.Root.transform));
                view.Open(0, 0, 0, 0);
                int closed = 0;
                view.SettingsClosed += () => closed++;

                TitleClick(f.Root, "ui.title.settings").onClick();
                Assert.IsTrue(view.Asking);
                f.Root.transform.Find("Settings").Find("cell language").Find("hit").GetComponent<Clickable>().onClick();

                Assert.IsTrue(Loc.Vi);
                Assert.AreEqual(Loc.Vietnamese, settings.language);

                f.Root.transform.Find("Settings").Find("Button ui.common.back").GetComponent<Clickable>().onClick();

                Assert.IsFalse(view.Asking);
                Assert.AreEqual(1, closed);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Click_TitlePrefabEraseLink_AsksThenConfirmingRaisesResetConfirmed()
        {
            var f = new PrefabFixture(TitlePrefab);
            try
            {
                var view = f.Get<TitleView>();
                view.Bind(new Settings(), TestFx.Make(f.Root.transform));
                view.Open(0, 0, 0, 0);
                int reset = 0;
                view.ResetConfirmed += () => reset++;
                TitleClick(f.Root, "ui.title.settings").onClick();

                TitleField<Label>(view, "resetLabel").GetComponent<Clickable>().onClick();
                Assert.IsTrue(f.Root.transform.Find("Confirm").gameObject.activeSelf);
                f.Root.transform.Find("Confirm").Find("Button ui.title.erase").GetComponent<Clickable>().onClick();

                Assert.AreEqual(1, reset);
                Assert.IsFalse(f.Root.transform.Find("Confirm").gameObject.activeSelf);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void AskSkip_TitlePrefab_AnswersPlayFromTheSkipDialog()
        {
            var f = new PrefabFixture(TitlePrefab);
            try
            {
                var view = f.Get<TitleView>();
                view.Bind(new Settings(), TestFx.Make(f.Root.transform));
                view.Open(0, 0, 0, 0);
                bool? answer = null;
                view.AskSkip(play => answer = play);

                f.Root.transform.Find("Skip").Find("Button ui.title.skip_prompt_play").GetComponent<Clickable>().onClick();

                Assert.AreEqual(true, answer);
                Assert.IsFalse(f.Root.transform.Find("Skip").gameObject.activeSelf);
            }
            finally { f.Destroy(); }
        }

        const string HudPrefab = "Assets/Prefabs/Parts/Hud.prefab";

        static T HudField<T>(HudView view, string name) =>
            (T)typeof(HudView).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(view);

        static ContentDb ContentFromResources() => new ContentDb(ContentAssets.Data());

        [Test]
        public void Bind_HudPrefab_ShowsTheDepthNumber()
        {
            var f = new PrefabFixture(HudPrefab);
            try
            {
                var db = ContentFromResources();
                var run = new RunState(db, new Journal(), 5);
                var view = f.Get<HudView>();
                view.Bind();

                view.Show(run, db, new Journal(), true);

                Assert.AreEqual(Loc.T("ui.hud.depth", ("n", run.Depth)), HudField<Label>(view, "depth").Text);
                Assert.AreEqual(run.TrialsLeft.ToString(), HudField<Label>(view, "trials").Text);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Hover_HudPrefabSocketAndAspect_SetsAndClearsTheHoveredIndex()
        {
            var f = new PrefabFixture(HudPrefab);
            try
            {
                var view = f.Get<HudView>();
                view.Bind();
                var socket = HudField<Clickable[]>(view, "socketHit");
                var aspect = HudField<Clickable[]>(view, "aspectHit");

                socket[2].onEnter();
                aspect[1].onEnter();
                Assert.AreEqual(2, view.HoveredCatalyst);
                Assert.AreEqual(1, view.HoveredAspect);

                socket[0].onExit();                       // leaving another socket leaves the hover alone
                Assert.AreEqual(2, view.HoveredCatalyst);
                socket[2].onExit();
                aspect[1].onExit();
                Assert.AreEqual(-1, view.HoveredCatalyst);
                Assert.AreEqual(-1, view.HoveredAspect);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Click_HudPrefabRedraw_RaisesRedrawClickedOnlyWhileARedrawIsLeft()
        {
            var f = new PrefabFixture(HudPrefab);
            try
            {
                var db = ContentFromResources();
                var run = new RunState(db, new Journal(), 5);
                var view = f.Get<HudView>();
                view.Bind();
                var hit = HudField<Clickable>(view, "redrawHit");
                int raised = 0;
                view.RedrawClicked += () => raised++;

                hit.onClick();                            // nothing shown yet: the button is not live
                Assert.AreEqual(0, raised);
                view.Show(run, db, new Journal(), false);
                hit.onEnter();
                hit.onClick();

                Assert.AreEqual(1, raised);
                Assert.IsTrue(view.RedrawHovered);
                hit.onExit();
                Assert.IsFalse(view.RedrawHovered);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Click_HudPrefabPauseJournalAndIgnite_RaiseTheirEvents()
        {
            var f = new PrefabFixture(HudPrefab);
            try
            {
                var db = ContentFromResources();
                var run = new RunState(db, new Journal(), 5);
                var view = f.Get<HudView>();
                view.Bind();
                int pause = 0, journal = 0, ignite = 0;
                view.PauseClicked += () => pause++;
                view.JournalClicked += () => journal++;
                view.IgniteClicked += () => ignite++;

                HudField<Clickable>(view, "pauseHit").onEnter();
                HudField<Clickable>(view, "pauseHit").onClick();
                HudField<Clickable>(view, "journalHit").onClick();
                HudField<Clickable>(view, "igniteHit").onClick();          // nothing to ignite: swallowed

                Assert.AreEqual(1, pause);
                Assert.IsTrue(view.PauseHovered);
                Assert.AreEqual(1, journal);
                Assert.AreEqual(0, ignite);
                view.Show(run, db, new Journal(), true);
                Assert.AreEqual(run.CanIgnite, HudField<bool>(view, "canIgnite"));
            }
            finally { f.Destroy(); }
        }

        const string BenchPrefab = "Assets/Prefabs/Views/Bench.prefab";

        static T Field<T>(object owner, string name) =>
            (T)owner.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(owner);

        [Test]
        public void Refresh_BenchPrefab_HasNineTiles()
        {
            var f = new PrefabFixture(BenchPrefab);
            try
            {
                var db = ContentFromResources();
                var run = new RunState(db, new Journal(), 5);
                var view = f.Get<BenchView>();
                view.Bind(db, TestFx.Make(f.Root.transform));
                view.Bind(run, new Journal());

                view.Refresh();

                Assert.AreEqual(Athanor.Gameplay.Bench.TileCount, view.TileCount);
                for (int i = 0; i < view.TileCount; i++)
                {
                    Assert.IsNotNull(view.Tile(i), "tile " + i);
                    Assert.AreEqual(i, view.Tile(i).index);
                    Assert.Greater(view.Tile(i).FaceSize.x, 0, "tile " + i + " keeps its face");
                }
                Assert.IsNotNull(view.Hud, "the Hud prefab is nested in the Bench");
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Refresh_BenchPrefab_DealsOneCardPerStartingMaterialFromTheCardPrefab()
        {
            var f = new PrefabFixture(BenchPrefab);
            try
            {
                var db = ContentFromResources();
                var run = new RunState(db, new Journal(), 5);
                var view = f.Get<BenchView>();
                view.Bind(db, TestFx.Make(f.Root.transform));
                view.Bind(run, new Journal());

                view.Refresh();

                int starters = 0;
                foreach (var id in run.Hand) if (db.Material(id).tier == 0) starters++;
                var cards = f.Root.GetComponentsInChildren<CardView>(true);
                Assert.AreEqual(starters, cards.Length);
                Assert.Greater(starters, 0);
                var first = db.Material(run.Hand[cards[0].handIndex]);
                Assert.AreEqual(first.id, cards[0].id);
                Assert.AreEqual(first.name.Split(' ')[0], Field<Label>(cards[0], "line1").Text);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Show_CardPrefab_OneWordNameHidesTheSecondLine()
        {
            var f = new PrefabFixture("Assets/Prefabs/Parts/Card.prefab");
            try
            {
                var db = ContentFromResources();
                MaterialData one = null, two = null;
                foreach (var m in db.Starters)
                {
                    if (m.tier != 0) continue;
                    if (one == null && !m.name.Contains(" ")) one = m;
                    if (two == null && m.name.Contains(" ")) two = m;
                }
                var card = f.Get<CardView>();

                card.Show(two ?? one, 110);
                bool shownFor2 = Field<Label>(card, "line2").gameObject.activeSelf;
                card.Show(one ?? two, 110);

                Assert.AreEqual(two != null, shownFor2);
                Assert.AreEqual(one == null, Field<Label>(card, "line2").gameObject.activeSelf);
            }
            finally { f.Destroy(); }
        }
    }
}
