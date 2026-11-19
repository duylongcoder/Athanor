using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>Phase 4: the hooks the tutorial needs, and the tutorial script replayed with every number it shows.</summary>
    public class TutorialTests
    {
        ContentDb db;
        [SetUp] public void Load() => db = new ContentDb(TestContent.Load());

        // ---- satchel ------------------------------------------------------------------------------

        [Test]
        public void Satchel_Take_PullsANamedCard_FromDrawOrDiscard()
        {
            var s = new Satchel(new[] { "a", "b", "b" }, 1);
            Assert.AreEqual(2, s.Available("b"));
            Assert.IsTrue(s.Take("b"));
            Assert.AreEqual(1, s.Available("b"));
            Assert.AreEqual(3, s.Total);                 // a card that is out still belongs to the satchel
            s.Discard(new[] { "b" });
            Assert.AreEqual(2, s.Available("b"));        // on the discard pile it can be taken again
            Assert.IsTrue(s.Take("b"));
            Assert.IsTrue(s.Take("b"));
            Assert.IsFalse(s.Take("b"));
            Assert.IsFalse(s.Take("zzz"));
        }

        // ---- scripted hands -----------------------------------------------------------------------

        static readonly string[] FirstHand = { "ember_coal", "deep_tear", "iron_ore", "moondust", "bone_ash" };

        /// <summary>Lays each (id, tile) from the hand and ignites.</summary>
        static IgniteOutcome LayAndIgnite(RunState run, params (string id, int tile)[] lay)
        {
            foreach (var (id, tile) in lay)
                Assert.IsTrue(run.Place(run.Hand.ToList().IndexOf(id), tile), "place " + id + " on " + tile);
            return run.Ignite();
        }

        [Test]
        public void Deal_ReplacesTheStartingHand_AndKeepsTheSatchelWhole()
        {
            var run = new RunState(db, new Journal(), 7);
            int size = run.SatchelSize;
            Assert.IsTrue(run.Deal(FirstHand));
            CollectionAssert.AreEquivalent(FirstHand, run.Hand);
            Assert.AreEqual(size, run.SatchelSize);
        }

        [Test]
        public void Deal_AfterMakingADiscovery_KeepsTheRack()
        {
            var run = new RunState(db, new Journal(), 7);
            run.Deal(FirstHand);
            LayAndIgnite(run, ("ember_coal", 4), ("deep_tear", 5));
            Assert.IsTrue(run.Deal(FirstHand));
            CollectionAssert.AreEquivalent(FirstHand.Concat(new[] { "steam_wisp" }), run.Hand);
        }

        [Test]
        public void Deal_Refuses_TooManyCopies_NonStarters_LockedStarters_AndABusyBench()
        {
            var run = new RunState(db, new Journal(), 7);
            var before = run.Hand.ToList();
            Assert.IsFalse(run.Deal(new[] { "moondust", "moondust", "moondust" }));   // the satchel holds two
            Assert.IsFalse(run.Deal(new[] { "steam_wisp" }));                         // a discovery
            Assert.IsFalse(run.Deal(new[] { "salt_crystal" }));                       // locked: not in a fresh satchel
            Assert.IsFalse(run.Deal(new[] { "no_such_thing" }));
            CollectionAssert.AreEqual(before, run.Hand);
            run.Place(0, 4);
            Assert.IsFalse(run.Deal(new[] { "moondust" }));
        }

        // ---- run options --------------------------------------------------------------------------

        [Test]
        public void QuietBefore_HoldsAspectsAndEventsBack_UntilThatDepth()
        {
            var opts = new RunOptions { Unlocks = Unlocks.Fresh(db), Aspects = true, Events = true, QuietBefore = 3 };
            Assert.AreEqual(RunPhase.AspectOffer, new RunState(db, new Journal(), 7, 1, RunOptions.Full(Unlocks.Fresh(db))).Phase, "control");
            for (int seed = 1; seed <= 40; seed++)
            {
                foreach (int depth in new[] { 1, 2 })
                {
                    var run = new RunState(db, new Journal(), seed, depth, opts);
                    Assert.AreEqual(RunPhase.Playing, run.Phase, "seed " + seed + " depth " + depth);
                    Assert.IsNull(run.CurrentEvent);
                    Assert.AreEqual(0, run.Aspects.Count);
                }
            }
            Assert.AreEqual(RunPhase.AspectOffer, new RunState(db, new Journal(), 7, 3, opts).Phase);
        }

        /// <summary>Two starters, one recipe, targets of 1, four catalysts: every reaction clears a depth.</summary>
        static ContentDb MiniWithCatalysts() => new ContentDb(new ContentData
        {
            rules = TestContent.Rules(),
            materials = new[] { TestContent.Mat("a", "Ore", 0, 6), TestContent.Mat("b", "Potion", 0, 4), TestContent.Mat("x", "Metal", 1, 5) },
            recipes = new[] { TestContent.Rec(new[] { "a", "b" }, 0, "x") },
            catalysts = new[] { TestContent.Cat(Effects.ChainPlus), TestContent.Cat(Effects.ExtraTrial), TestContent.Cat(Effects.ExtraRedraw), TestContent.Cat(Effects.PairBonus, "", 2) },
            depthTargets = new[] { 1, 1, 1, 1 }, depthNames = new[] { "1", "2", "3", "4", "5" }, ultimate = "",
        });

        [Test]
        public void FirstOffer_IsFixed_ForTheFirstOfferOnly()
        {
            var mini = MiniWithCatalysts();
            var run = new RunState(mini, new Journal(), 7, 1, new RunOptions { FirstOffer = new[] { Effects.ChainPlus, Effects.ExtraTrial, "unknown" } });
            LayAndIgnite(run, ("a", 4), ("b", 5));
            Assert.AreEqual(RunPhase.DepthCleared, run.Phase);
            CollectionAssert.AreEqual(new[] { Effects.ChainPlus, Effects.ExtraTrial }, run.CatalystOffer.Select(c => c.id).ToArray());
            run.ChooseCatalyst(Effects.ChainPlus);
            LayAndIgnite(run, ("a", 4), ("b", 5));
            Assert.AreEqual(3, run.CatalystOffer.Count, "the second offer is drawn as usual");
        }

        [Test]
        public void FirstOffer_WithALockedCatalystNamed_LeavesOutLockedCatalysts()
        {
            var opts = new RunOptions { Unlocks = Unlocks.Fresh(db), FirstOffer = new[] { "crooked_mirror", "bellows" } };   // crooked_mirror is locked
            var run = new RunState(db, new Journal(), 7, 1, opts);
            run.Deal(FirstHand); LayAndIgnite(run, ("ember_coal", 4), ("deep_tear", 5));
            run.Deal(new[] { "iron_ore", "ember_coal", "moondust", "bone_ash", "sunbloom" });
            LayAndIgnite(run, ("iron_ore", 3), ("ember_coal", 4), ("moondust", 5));
            Assert.AreEqual(RunPhase.DepthCleared, run.Phase);
            CollectionAssert.AreEqual(new[] { "bellows" }, run.CatalystOffer.Select(c => c.id).ToArray());
        }

        // ---- preview and score parts --------------------------------------------------------------

        static string[] Row(string left, string centre, string right)
        {
            var t = new string[Bench.TileCount]; t[3] = left; t[4] = centre; t[5] = right; return t;
        }

        [Test]
        public void Preview_ScoresALayout_AndChangesNothing()
        {
            var journal = new Journal();
            var run = new RunState(db, journal, 7);
            var hand = run.Hand.ToList();
            var events = run.Preview(Row("iron_ore", "ember_coal", "moondust")).Where(e => e.Result != null).ToList();
            CollectionAssert.AreEqual(new[] { "sky_iron", "starmetal_ore" }, events.Select(e => e.Result).ToArray());
            CollectionAssert.AreEqual(new long[] { 40, 240 }, events.Select(e => e.Potency).ToArray());
            CollectionAssert.AreEqual(new[] { 4, 5 }, events.Select(e => e.Hub).ToArray());   // step 2's hub is Moondust
            Assert.AreEqual(0, journal.KnownPairs.Count);
            Assert.IsFalse(run.HasFired("ember_coal", "iron_ore"));
            Assert.AreEqual(0, run.Potency);
            CollectionAssert.AreEqual(hand, run.Hand);
        }

        [Test]
        public void ScoreParts_SplitsAScore_IntoItsFactors()
        {
            var run = new RunState(db, new Journal(), 7);
            var step2 = run.Preview(Row("iron_ore", "ember_coal", "moondust")).Where(e => e.Result != null).ElementAt(1);
            var p = ScoreParts.Of(db, step2);
            Assert.AreEqual(60, p.Base); Assert.AreEqual(1, p.Used); Assert.AreEqual(2, p.First); Assert.AreEqual(2, p.Step);
            Assert.AreEqual(1.0, p.Bonus, 1e-9); Assert.AreEqual(240, p.Total);
            Assert.IsNull(ScoreParts.Of(db, new ReactionEvent()));   // a fizzle has no score
        }

        // ---- the script ---------------------------------------------------------------------------

        RunState StartTutorial(int seed = TutorialScript.Seed) =>
            new RunState(db, new Journal(), seed, 1, TutorialScript.Options(Unlocks.Fresh(db)));

        static IgniteOutcome Play(RunState run, TutorialTrial trial)
        {
            Assert.IsTrue(run.Deal(trial.Hand), "deal");
            foreach (var lay in trial.Lay)
                Assert.IsTrue(run.Place(run.Hand.ToList().IndexOf(lay.Id), lay.Tile), "place " + lay.Id + " on " + lay.Tile);
            return run.Ignite();
        }

        static List<ReactionEvent> Reactions(IEnumerable<ReactionEvent> events) => events.Where(e => e.Result != null).ToList();

        [Test]
        public void Trial1_EmberCoalAndDeepTear_MakeSteamWisp_For40()
        {
            var run = StartTutorial();
            Assert.AreEqual(RunPhase.Playing, run.Phase);
            Assert.AreEqual(4, run.TrialsLeft);
            var r = Reactions(Play(run, TutorialScript.Reaction).Events);
            CollectionAssert.AreEqual(TutorialScript.Reaction.Makes, r.Select(e => e.Result).ToArray());
            var p = ScoreParts.Of(db, r[0]);
            Assert.AreEqual(20, p.Base); Assert.AreEqual(1, p.Used); Assert.AreEqual(2, p.First); Assert.AreEqual(1, p.Step);
            Assert.AreEqual(40, p.Total);
            Assert.AreEqual(3, run.TrialsLeft);
            Assert.Contains("steam_wisp", run.Hand.ToList());   // kept on the rack
        }

        [Test]
        public void Trial2_GlowmossAndIronOre_Fizzle_SpendATrial_AndHintPotionOnGlowmoss()
        {
            var run = StartTutorial();
            Play(run, TutorialScript.Reaction);
            var o = Play(run, TutorialScript.Miss);
            Assert.IsEmpty(Reactions(o.Events));
            Assert.AreEqual(2, run.TrialsLeft);
            Assert.AreEqual(40, run.Potency);
            var hint = o.Events.Single(e => e.Hint != null).Hint;
            Assert.AreEqual("glowmoss", hint.material);
            Assert.AreEqual("Potion", hint.partnerKind);
        }

        [Test]
        public void Trial3_Chain_SkyIronThenStarmetalOre_40And240_ClearsDepth1()
        {
            var run = StartTutorial();
            Play(run, TutorialScript.Reaction);
            Play(run, TutorialScript.Miss);
            var broken = Reactions(run.Preview(TutorialScript.BrokenChain()));
            CollectionAssert.AreEqual(new long[] { 40 }, broken.Select(e => e.Potency).ToArray());
            var r = Reactions(Play(run, TutorialScript.Chain).Events);
            CollectionAssert.AreEqual(TutorialScript.Chain.Makes, r.Select(e => e.Result).ToArray());
            CollectionAssert.AreEqual(new long[] { 40, 240 }, r.Select(e => e.Potency).ToArray());
            CollectionAssert.AreEqual(new[] { 4, 5 }, r.Select(e => e.Hub).ToArray());
            CollectionAssert.AreEqual(new[] { 1, 2 }, r.Select(e => e.Chain).ToArray());
            Assert.AreEqual(320, run.Potency);
            Assert.AreEqual(RunPhase.DepthCleared, run.Phase);
            Assert.AreEqual(1, run.TrialsLeft);
            CollectionAssert.AreEqual(TutorialScript.Offer, run.CatalystOffer.Select(c => c.id).ToArray());
        }

        [TestCase("bellows", 80, 2.0)]
        [TestCase("heart_tile", 80, 2.0)]
        [TestCase("first_spark", 120, 3.0)]
        public void Depth2_CatalystTrial_ShowsTheCatalyst(string catalyst, long score, double bonus)
        {
            var run = StartTutorial();
            foreach (var t in TutorialScript.Depth1) Play(run, t);
            run.ChooseCatalyst(catalyst);
            Assert.AreEqual(2, run.Depth);
            Assert.AreEqual(RunPhase.Playing, run.Phase, "no aspect or event before depth 3");
            Assert.AreEqual(5, run.TrialsLeft, "4 plus the 1 left at depth 1");
            var trial = TutorialScript.CatalystTrial(catalyst);
            var r = Reactions(Play(run, trial).Events);
            CollectionAssert.AreEqual(trial.Makes, r.Select(e => e.Result).ToArray());
            Assert.AreEqual(score, r[0].Potency);
            Assert.AreEqual(bonus, ScoreParts.Of(db, r[0]).Bonus, 1e-9);
        }

        [Test]
        public void Script_IsTheSame_WhateverTheSeed()
        {
            for (int seed = 1; seed <= 25; seed++)
            {
                var run = StartTutorial(seed);
                foreach (var t in TutorialScript.Depth1) Play(run, t);
                Assert.AreEqual(320, run.Potency, "seed " + seed);
            }
        }

        [Test]
        public void CatalystTrial_CoversTheOffer_AndNothingElse()
        {
            foreach (var id in TutorialScript.Offer) Assert.IsNotNull(TutorialScript.CatalystTrial(id), id);
            Assert.IsNull(TutorialScript.CatalystTrial("hourglass"));
        }

        [TestCase(false, true)]
        [TestCase(true, false)]
        public void ShouldRun_TutorialDoneFlag_RunsOnlyUntilDone(bool done, bool expected) =>
            Assert.AreEqual(expected, TutorialScript.ShouldRun(done));

        [TestCase(false, 0, TutorialPlan.Run)]
        [TestCase(false, 1, TutorialPlan.AskSkip)]
        [TestCase(false, 12, TutorialPlan.AskSkip)]
        [TestCase(true, 0, TutorialPlan.None)]
        [TestCase(true, 12, TutorialPlan.None)]
        public void Plan_DoneFlagAndJournal_PicksRunAskSkipOrNone(bool done, int journalEntries, TutorialPlan expected) =>
            Assert.AreEqual(expected, TutorialScript.Plan(done, journalEntries));

        [TestCase(0)] [TestCase(5)]
        public void Plan_NotDone_RunsExactlyWhenShouldRunAndTheJournalIsEmpty(int journalEntries)
        {
            bool runs = TutorialScript.Plan(false, journalEntries) == TutorialPlan.Run;
            Assert.AreEqual(TutorialScript.ShouldRun(false) && journalEntries == 0, runs);
        }
    }
}
