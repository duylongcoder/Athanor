using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Athanor.Core;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>
    /// The tutorial steps are scene objects of Main.unity. The baseline (tutorial_lit_baseline.json) is what each step lit when the rects were
    /// numbers in TutorialTargets and lists in Drive (art pixels, before the hole snap); the steps must still light exactly that.
    /// </summary>
    public class TutorialStepsBakeTests
    {
        public const string BaselinePath = "Assets/Tests/EditModeGame/Baselines/tutorial_lit_baseline.json";

        /// <summary>Every step id Drive, AfterEvent, AfterIgnite and OnOffer ask for.</summary>
        public static readonly string[] AllStepIds =
        {
            "1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.6b", "1.7", "2.1", "2.2", "2.3", "2.4", "2.5", "2.6", "2.7", "3.0", "3.1", "3.2", "3.3", "3.3e",
            "3.4", "3.5", "3.6", "5.1", "5.2", "5.3", "5.4", "5.4i", "5.4r", "5.5",
        };

        [Serializable] public class StepRects { public string id; public Rect[] lit; }
        [Serializable] public class Baseline { public List<StepRects> steps = new List<StepRects>(); }

        MainSceneFixture main;
        TutorialController tutorial;

        [SetUp] public void Open() { main = new MainSceneFixture(); tutorial = main.FlowField<TutorialController>("tutorial"); tutorial.Bind(main.Flow);
            main.Flow.JournalView.Bind(new ContentDb(JsonUtility.FromJson<ContentData>(Resources.Load<TextAsset>("content").text)));   // the cells the tutorial points at
        }
        [TearDown] public void Close() => main?.Dispose();

        [Test]
        public void EveryStepId_UsedByDrive_ExistsInTheScene()
        {
            foreach (var id in AllStepIds) Assert.DoesNotThrow(() => tutorial.Step(id), "missing step " + id);
        }

        [Test]
        public void EveryStep_TextKey_HasEnglishAndVietnamese()
        {
            string[] Keys(string file) => Regex.Matches(File.ReadAllText("Assets/Resources/lang/" + file + ".json"), "\"key\":\\s*\"([^\"]+)\"").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
            var en = Keys("en"); var vi = Keys("vi");
            foreach (var id in AllStepIds)
            {
                var key = tutorial.Step(id).textKey;
                Assert.IsNotEmpty(key, id);
                CollectionAssert.Contains(en, key, "en " + id); CollectionAssert.Contains(vi, key, "vi " + id);
            }
        }

        [Test]
        public void FixedAreas_OfEveryStep_HavePositiveSize()
        {
            foreach (var id in AllStepIds)
                foreach (var a in tutorial.Step(id).Areas())
                    if (a.kind == AreaKind.Fixed) Assert.IsTrue(a.size.x > 0 && a.size.y > 0, id + " " + a.name);
        }

        [Test]
        public void EveryStep_Rects_EqualTheBaseline()
        {
            var baseline = JsonUtility.FromJson<Baseline>(File.ReadAllText(BaselinePath));
            Assert.GreaterOrEqual(baseline.steps.Count, 20);
            int hub = TutorialScript.Chain.Lay[1].Tile;   // the baseline stands the chain hub in for "the hub of the event that just played"
            foreach (var want in baseline.steps)
            {
                var got = tutorial.Holes(tutorial.Step(want.id), hub);
                Assert.AreEqual(want.lit.Length, got.Length, "areas of " + want.id);
                for (int i = 0; i < got.Length; i++)
                {
                    Assert.AreEqual(want.lit[i].x, got[i].x, 0.5f, want.id + "[" + i + "].x"); Assert.AreEqual(want.lit[i].y, got[i].y, 0.5f, want.id + "[" + i + "].y");
                    Assert.AreEqual(want.lit[i].width, got[i].width, 0.5f, want.id + "[" + i + "].w"); Assert.AreEqual(want.lit[i].height, got[i].height, 0.5f, want.id + "[" + i + "].h");
                }
            }
        }

        [Test]
        public void ClickRules_PassFlags_MatchTheOldBehaviour()
        {
            // awaited steps let clicks through their areas; read steps keep them (a click advances)
            foreach (var id in new[] { "1.2", "1.3", "1.5", "2.1", "2.2", "2.4", "3.1", "3.3", "5.3", "5.4", "5.4i" })
                foreach (var a in tutorial.Step(id).Areas()) Assert.IsTrue(a.clicksPass, id + " " + a.name);
            foreach (var id in new[] { "1.1", "1.4", "1.6", "1.7", "2.5", "2.6", "3.0", "3.2", "3.5", "3.6" })
                foreach (var a in tutorial.Step(id).Areas()) Assert.IsFalse(a.clicksPass, id + " " + a.name);
            var last = tutorial.Step("2.7"); Assert.IsFalse(last.dark); Assert.IsFalse(last.lockPad);
            var offer = tutorial.Step("5.3"); Assert.IsFalse(offer.dark); Assert.IsTrue(offer.lockPad);
            Assert.IsTrue(tutorial.Step("2.5").useBoxPos); Assert.AreEqual(262f, tutorial.Step("2.5").boxPos.y);
        }
    }
}
