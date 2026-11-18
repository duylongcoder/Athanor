using Athanor.Core;
using Athanor.Game;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>The pause menu: time stops and comes back on every exit, Esc toggles, nothing opens in the tutorial, Quit leaves the stored run data alone.</summary>
    public class PauseTests
    {
        PrefabFixture fixture;
        GameObject go;
        PauseMenu menu;
        readonly string[] keys = { Save.Key, Save.ProfileKey };
        readonly System.Collections.Generic.Dictionary<string, string> kept = new System.Collections.Generic.Dictionary<string, string>();

        [SetUp] public void Build()
        {
            foreach (var k in keys) kept[k] = PlayerPrefs.GetString(k, null);
            Haptics.ForceSupported = null;
            fixture = new PrefabFixture("Assets/Prefabs/Views/Pause.prefab");
            go = fixture.Root;
            menu = fixture.Get<PauseMenu>();
            menu.Bind(new Settings());
            Time.timeScale = 1f;
        }

        [TearDown] public void Restore()
        {
            Time.timeScale = 1f;
            fixture.Destroy();
            Haptics.ForceSupported = null;
            foreach (var k in keys) { if (kept[k] == null) PlayerPrefs.DeleteKey(k); else PlayerPrefs.SetString(k, kept[k]); }
        }

        [Test]
        public void Open_StopsTime_AndEveryExitRestoresIt()
        {
            Assert.IsTrue(menu.Open());
            Assert.AreEqual(0f, Time.timeScale);
            menu.Close();
            Assert.AreEqual(1f, Time.timeScale);

            menu.Open(); menu.Toggle();                                   // Esc
            Assert.AreEqual(1f, Time.timeScale); Assert.IsFalse(menu.IsOpen);

            menu.Open(); menu.Close();
            Assert.AreEqual(1f, Time.timeScale);
        }

        [Test]
        public void Quit_RestoresTime_BeforeTheOwnerHearsOfIt()
        {
            float seen = -1; menu.QuitConfirmed += () => seen = Time.timeScale;
            menu.Open();
            menu.Toggle(); menu.Open();                                   // closed, reopened
            // the confirm is reached through the Quit button; drive it as the button does
            var quit = go.transform.Find("Pause menu").Find("Button ui.pause.quit").GetComponent<Clickable>();
            quit.onClick();
            Assert.IsTrue(menu.Confirming); Assert.AreEqual(0f, Time.timeScale);
            go.transform.Find("Quit confirm").Find("Button ui.pause.quit_confirm").GetComponent<Clickable>().onClick();
            Assert.AreEqual(1f, seen); Assert.AreEqual(1f, Time.timeScale); Assert.IsFalse(menu.IsOpen);
        }

        [Test]
        public void Esc_Toggles_AndBacksOutOfTheQuitQuestion()
        {
            Assert.IsFalse(menu.IsOpen);
            menu.Toggle(); Assert.IsTrue(menu.IsOpen);
            go.transform.Find("Pause menu").Find("Button ui.pause.quit").GetComponent<Clickable>().onClick();
            Assert.IsTrue(menu.Confirming);
            menu.Toggle(); Assert.IsTrue(menu.IsOpen); Assert.IsFalse(menu.Confirming); Assert.AreEqual(0f, Time.timeScale);
            menu.Toggle(); Assert.IsFalse(menu.IsOpen); Assert.AreEqual(1f, Time.timeScale);
        }

        [Test]
        public void Abort_TutorialRun_DoesNotMarkItDone()
        {
            Save.TutorialDone = false;
            var t = go.AddComponent<TutorialController>(); t.Build();
            t.Abort();
            Assert.IsFalse(Save.TutorialDone);
        }

        [Test]
        public void Open_NotAllowed_IsRefused()
        {
            menu.CanOpen = () => false;
            Assert.IsFalse(menu.Open()); menu.Toggle();
            Assert.IsFalse(menu.IsOpen); Assert.AreEqual(1f, Time.timeScale);
        }

        [Test]
        public void Allowed_OnlyOnTheBenchWithInput_NotInCutscene_TutorialIsPausable()
        {
            Assert.IsTrue(PauseMenu.Allowed(FlowState.Bench, true, false), "a tutorial run is on the Bench state: pausable (SPEC 20)");
            Assert.IsFalse(PauseMenu.Allowed(FlowState.Bench, true, true), "cutscene");
            Assert.IsFalse(PauseMenu.Allowed(FlowState.Bench, false, false), "offer or resolving: no bench input");
            foreach (var s in new[] { FlowState.Title, FlowState.Intro, FlowState.Resolving, FlowState.Journal, FlowState.Descent, FlowState.CatalystChoice, FlowState.Ending, FlowState.RunOver, FlowState.AspectChoice, FlowState.Event, FlowState.Goals })
                Assert.IsFalse(PauseMenu.Allowed(s, true, false), s.ToString());
        }

        [Test]
        public void VibrationButton_NoHaptics_IsHidden()
        {
            var b = go.transform.Find("Pause menu").Find("Button ui.pause.vibration").gameObject;
            Haptics.ForceSupported = false; menu.Open(); Assert.IsFalse(b.activeSelf); menu.Close();
            Haptics.ForceSupported = true; menu.Open(); Assert.IsTrue(b.activeSelf); menu.Close();
        }

        [Test]
        public void AfterQuit_ProfileAndJournalAreWhatIsStored_AndStorageIsNotWritten()
        {
            var db = new ContentDb(JsonUtility.FromJson<ContentData>(Resources.Load<TextAsset>("content").text));
            var profile = new Profile(1); profile.RunStarted();
            Save.StoreProfile(profile);
            string before = PlayerPrefs.GetString(Save.ProfileKey), beforeJ = PlayerPrefs.GetString(Save.Key, "");
            // a run in memory that earned things the page would never have saved
            var live = Save.LoadProfile(db); live.RunStarted(); live.DepthBegan(4);
            var after = GameFlow.ProfileAfterQuit(false, live, db);
            Assert.AreEqual(1, after.RunsStarted);
            Assert.AreEqual(before, JsonUtility.ToJson(after.ToData()));
            Assert.AreEqual(before, PlayerPrefs.GetString(Save.ProfileKey));
            Assert.AreEqual(beforeJ, PlayerPrefs.GetString(Save.Key, ""));
            Assert.AreSame(live, GameFlow.ProfileAfterQuit(true, live, db), "a How-to-play sandbox keeps its throwaway");
        }

        [Test]
        public void Quit_AfterADiscovery_JournalInStorageHasIt()
        {
            var db = new ContentDb(JsonUtility.FromJson<ContentData>(Resources.Load<TextAsset>("content").text));
            Save.StoreJournal(new Journal());
            var journal = new Journal();
            var run = new RunState(db, journal, TutorialScript.Seed, 1, TutorialScript.Options(Unlocks.Fresh(db)));
            var trial = TutorialScript.Depth1[0];
            Assert.IsTrue(run.Deal(trial.Hand));
            foreach (var l in trial.Lay) Assert.IsTrue(run.Place(System.Linq.Enumerable.ToList(run.Hand).IndexOf(l.Id), l.Tile));
            // the same call Resolve makes: the Ignite and the store are one step, before the chain plays or Quit can be pressed
            GameFlow.SettleIgnite(run, journal, false);
            Assert.Greater(journal.KnownPairs.Count, 0, "the Ignite discovered something");
            // Quit to title: the page-close view of the world is what is stored
            var after = GameFlow.JournalAfterQuit(false, journal);
            Assert.AreNotSame(journal, after, "read back from storage, not the live object");
            CollectionAssert.AreEquivalent(journal.KnownPairs, after.KnownPairs);
        }

        [Test]
        public void SettleIgnite_Sandbox_StoresNothing()
        {
            var db = new ContentDb(JsonUtility.FromJson<ContentData>(Resources.Load<TextAsset>("content").text));
            Save.StoreJournal(new Journal());
            string before = PlayerPrefs.GetString(Save.Key, "");
            var journal = new Journal();
            var run = new RunState(db, journal, TutorialScript.Seed, 1, TutorialScript.Options(Unlocks.Fresh(db)));
            var trial = TutorialScript.Depth1[0];
            Assert.IsTrue(run.Deal(trial.Hand));
            foreach (var l in trial.Lay) Assert.IsTrue(run.Place(System.Linq.Enumerable.ToList(run.Hand).IndexOf(l.Id), l.Tile));
            GameFlow.SettleIgnite(run, journal, true);
            Assert.Greater(journal.KnownPairs.Count, 0);
            Assert.AreEqual(before, PlayerPrefs.GetString(Save.Key, ""), "a How-to-play replay writes no journal");
        }
    }
}
