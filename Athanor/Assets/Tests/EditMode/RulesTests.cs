using System;
using NUnit.Framework;
using Athanor.Core;

namespace Athanor.Tests
{
    public class RulesTests
    {
        [Test]
        public void Rules_ShippedContent_HasTodaysValues()
        {
            var r = new ContentDb(TestContent.Load()).Rules;
            Assert.AreEqual(3, r.handMin);
            Assert.AreEqual(5, r.handBase);
            Assert.AreEqual(4, r.trials);
            Assert.AreEqual(6, r.trialsFinalDepth);
            Assert.AreEqual(2, r.redraws);
            Assert.AreEqual(2, r.starterCopies);
            Assert.AreEqual(3, r.offerSize);
            Assert.AreEqual(12, r.rackSize);
            Assert.AreEqual(13, r.maxRi);
            Assert.AreEqual(1, r.pouchCards);
            Assert.AreEqual(2, r.firstReactionMult);
            CollectionAssert.AreEqual(new[] { 1, 3, 5 }, r.aspectDepths);
            CollectionAssert.AreEqual(new long[] { 0, 20, 60, 180, 500, 1500 }, r.potencyByTier);
        }

        [Test]
        public void FinalDepth_ShippedContent_IsFive() => Assert.AreEqual(5, new ContentDb(TestContent.Load()).FinalDepth);

        static void Throws(Action<ContentData> change)
        {
            var data = TestContent.Load();
            change(data);
            Assert.Throws<ArgumentException>(() => new ContentDb(data));
        }

        [Test] public void Constructor_NoRulesBlock_Throws() => Throws(d => d.rules = null);
        [Test] public void Constructor_PotencyTableShorterThanTiers_Throws() => Throws(d => d.rules.potencyByTier = new long[] { 0, 20 });
        [Test] public void Constructor_AspectDepthOutsideTheRun_Throws() => Throws(d => d.rules.aspectDepths = new[] { 6 });
        [Test] public void Constructor_HandBaseBelowHandMin_Throws() => Throws(d => { d.rules.handMin = 5; d.rules.handBase = 4; });
        [Test] public void Constructor_DepthTargetsNotOneFewerThanDepths_Throws() => Throws(d => d.depthTargets = new[] { 300, 1500, 7500 });

        static ContentDb Db(Action<RulesData> change)
        {
            var data = TestContent.Load();
            change(data.rules);
            return new ContentDb(data);
        }

        [Test] public void HandSize_HandBaseSeven_IsSeven() => Assert.AreEqual(7, new RunState(Db(r => r.handBase = 7), new Journal(), 1).HandSize);
        [Test] public void TrialsLeft_TrialsSix_StartsDepthOneWithSix() => Assert.AreEqual(6, new RunState(Db(r => r.trials = 6), new Journal(), 1).TrialsLeft);
        [Test] public void BasePotency_TableDoubled_IsDoubled() =>
            Assert.AreEqual(120, Db(r => r.potencyByTier = new long[] { 0, 40, 120, 360, 1000, 3000 }).BasePotency(2));

        [Test]
        public void Phase_AspectDepthsTwo_OffersAspectsAtDepthTwoOnly()
        {
            var db = Db(r => r.aspectDepths = new[] { 2 });
            var run = new RunState(db, new Journal(), 1, 1, RunOptions.Full(Unlocks.All(db)));
            Assert.AreNotEqual(RunPhase.AspectOffer, run.Phase);
            var run2 = new RunState(db, new Journal(), 1, 2, RunOptions.Full(Unlocks.All(db)));
            Assert.AreEqual(RunPhase.AspectOffer, run2.Phase);
        }
    }
}
