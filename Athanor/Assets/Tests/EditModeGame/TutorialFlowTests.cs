using System.Collections.Generic;
using System.Linq;
using Athanor.Gameplay;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>The tutorial's decisions, its sandbox and the scores it shows (the numbers TutorialTests pins in Core).</summary>
    public class TutorialFlowTests
    {
        static readonly TutorialSequence Seq = UnityEditor.AssetDatabase.LoadAssetAtPath<TutorialSequence>("Assets/Content/Tutorial/Tutorial.asset");
        string[] keys = { Save.Key, Save.ProfileKey };
        readonly Dictionary<string, string> kept = new Dictionary<string, string>();
        int doneKept;

        [SetUp] public void Keep()
        {
            foreach (var k in keys) kept[k] = PlayerPrefs.GetString(k, null);
            doneKept = PlayerPrefs.GetInt("athanor.tutorial.v1", -1);
        }

        [TearDown] public void Restore()
        {
            foreach (var k in keys) { if (kept[k] == null) PlayerPrefs.DeleteKey(k); else PlayerPrefs.SetString(k, kept[k]); }
            Save.SetTutorialDone(false);
            if (doneKept < 0) PlayerPrefs.DeleteKey("athanor.tutorial.v1"); else PlayerPrefs.SetInt("athanor.tutorial.v1", doneKept);
        }

        static ContentDb Db() => new ContentDb(ContentAssets.Data());

        [Test]
        public void Union_WithNoHoles_IsTheWholeCanvas_SoATextOnlyStepDimsEverythingAndDrawsNoRim()
        {
            Assert.AreEqual(new Rect(0, 0, Px.W, Px.H), TutorialManager.Union(new Rect[0]));
            Assert.AreEqual(new Rect(0, 0, Px.W, Px.H), TutorialManager.Union(null));
        }

        [Test]
        public void Union_OfHoles_IsTheirBoundingBox()
        {
            var u = TutorialManager.Union(new[] { new Rect(10, 20, 30, 40), new Rect(100, 5, 10, 10) });
            Assert.AreEqual(new Rect(10, 5, 100, 55), u);
        }

        [Test]
        public void Blocks_ADarkStep_EvenWithoutLockPad_SoTheDimNeverLetsAClickThrough()
        {
            Assert.IsTrue(TutorialManager.Blocks(new TutorialStepDef { dark = true, lockPad = false }));
            Assert.IsTrue(TutorialManager.Blocks(new TutorialStepDef { dark = false, lockPad = true }));
            Assert.IsFalse(TutorialManager.Blocks(new TutorialStepDef { dark = false, lockPad = false }));
        }

        [Test]
        public void Blocks_ForEveryDarkStepOfTheSequence_IsTrue()
        {
            foreach (var s in Seq.steps.Where(s => s.dark)) Assert.IsTrue(TutorialManager.Blocks(s), s.id);
        }

        [Test]
        public void Plan_FreshSave_RunsTheTutorial()
        {
            Assert.AreEqual(TutorialPlan.Run, TutorialGate.Plan(false, 0));
        }

        [Test]
        public void Plan_JournalWithEntries_AsksOnce()
        {
            Assert.AreEqual(TutorialPlan.AskSkip, TutorialGate.Plan(false, 12));
            Assert.AreEqual(TutorialPlan.None, TutorialGate.Plan(true, 12));
            Assert.AreEqual(TutorialPlan.None, TutorialGate.Plan(true, 0));
        }

        [Test]
        public void MarkDone_AfterSkip_SetsTutorialDone()
        {
            Save.SetTutorialDone(false);
            TutorialRules.MarkDone();
            Assert.IsTrue(Save.TutorialDone);
            Assert.AreEqual(TutorialPlan.None, TutorialGate.Plan(Save.TutorialDone, 0));
        }

        [Test]
        public void AnswerSkipQuestion_Skip_MarksDoneAndStartsTheRunWithoutAReplay()
        {
            Save.SetTutorialDone(false);
            Assert.AreEqual(TutorialPlan.AskSkip, TutorialGate.Plan(Save.TutorialDone, 12));
            Assert.IsFalse(TutorialRules.AnswerSkipQuestion(false), "Skip: no sandbox replay, Main goes straight to the run");
            Assert.IsTrue(Save.TutorialDone);
            Assert.AreEqual(TutorialPlan.None, TutorialGate.Plan(Save.TutorialDone, 12), "the run that starts is a normal one, and Begin is not asked again");
        }

        [Test]
        public void AnswerSkipQuestion_PlayIt_ReplaysInTheSandboxAndLeavesTheSaveAlone()
        {
            Save.SetTutorialDone(false);
            Assert.IsTrue(TutorialRules.AnswerSkipQuestion(true));
            Assert.IsFalse(Save.TutorialDone, "the real save is unchanged (owner Q4)");
        }

        /// <summary>Plays the whole script on a RunState exactly as the controller lays it, collecting every reaction's potency.</summary>
        static List<long> PlayScript(ContentDb db, Journal journal, out RunState run)
        {
            run = new RunState(db, journal, Seq.seed, 1, Seq.Options(Unlocks.Fresh(db)));
            var shown = new List<long>();
            foreach (var t in new[] { Seq.Trial(0), Seq.Trial(1), Seq.Trial(2) })
            {
                Assert.IsTrue(run.Deal(t.Hand));
                foreach (var l in t.Lay) Assert.IsTrue(run.Place(run.Hand.ToList().IndexOf(l.Id), l.Tile));
                var o = run.Ignite();
                shown.AddRange(o.Events.Select(e => e.Result == null ? 0 : e.Potency));
                for (int i = 0; i < Gameplay.Bench.TileCount; i++) run.Unplace(i);
            }
            return shown;
        }

        [Test]
        public void ScriptedTrials_Shown_Equal40_0_40_240()
        {
            var shown = PlayScript(Db(), new Journal(), out var run);
            CollectionAssert.AreEqual(new long[] { 40, 0, 40, 240 }, shown);
            Assert.AreEqual(RunPhase.DepthCleared, run.Phase);
            Assert.AreEqual(320, run.Potency);
        }

        [Test]
        public void HowToPlay_ThrowawayJournalAndProfile_LeavesStorageByteIdentical()
        {
            var db = Db();
            var realJ = new Journal(); realJ.Learn("ember_coal", "deep_tear");
            Save.StoreJournal(realJ); Save.StoreProfile(new Profile(7));
            string j0 = PlayerPrefs.GetString(Save.Key), p0 = PlayerPrefs.GetString(Save.ProfileKey);
            // what the replay does: its own journal and profile, never stored
            var throwaway = new Journal(); var profile = new Profile(1);
            PlayScript(db, throwaway, out var run);
            profile.Evaluate(db, throwaway);
            Assert.AreEqual(j0, PlayerPrefs.GetString(Save.Key));
            Assert.AreEqual(p0, PlayerPrefs.GetString(Save.ProfileKey));
            Assert.Greater(throwaway.KnownPairs.Count, 0);
        }

        /// <summary>
        /// Regression (webtest section C): EventView read run.EventOptions[picked] after ChooseEventOption, which empties the list,
        /// and threw ArgumentOutOfRangeException. The view must keep the option; this pins the Core behaviour it depends on.
        /// </summary>
        [Test]
        public void ChooseEventOption_EmptiesEventOptions_SoViewsMustKeepTheChosenOption()
        {
            var db = new ContentDb(ContentAssets.Data());
            for (int seed = 1; seed <= 60; seed++)
            {
                var r = new RunState(db, new Journal(), seed, 1, RunOptions.Full(Unlocks.Fresh(db)));
                var auto = new AutoPlayer(db);
                for (int n = 0; n < 600 && r.Phase != RunPhase.EventOffer && r.Phase != RunPhase.Lost && r.Phase != RunPhase.Won; n++) auto.Step(r);
                if (r.Phase != RunPhase.EventOffer) continue;
                Assert.Greater(r.EventOptions.Count, 0);
                r.ChooseEventOption(0);
                Assert.AreEqual(0, r.EventOptions.Count);
                return;
            }
            Assert.Inconclusive("no event reached in 60 seeds");
        }
    }
}
