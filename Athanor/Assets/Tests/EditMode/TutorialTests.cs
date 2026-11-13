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
    }
}
