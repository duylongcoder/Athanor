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
        public void Deal_KeepsTheRack()
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
        public void FirstOffer_LeavesOutLockedCatalysts()
        {
            var opts = new RunOptions { Unlocks = Unlocks.Fresh(db), FirstOffer = new[] { "crooked_mirror", "bellows" } };   // crooked_mirror is locked
            var run = new RunState(db, new Journal(), 7, 1, opts);
            run.Deal(FirstHand); LayAndIgnite(run, ("ember_coal", 4), ("deep_tear", 5));
            run.Deal(new[] { "iron_ore", "ember_coal", "moondust", "bone_ash", "sunbloom" });
            LayAndIgnite(run, ("iron_ore", 3), ("ember_coal", 4), ("moondust", 5));
            Assert.AreEqual(RunPhase.DepthCleared, run.Phase);
            CollectionAssert.AreEqual(new[] { "bellows" }, run.CatalystOffer.Select(c => c.id).ToArray());
        }
    }
}
