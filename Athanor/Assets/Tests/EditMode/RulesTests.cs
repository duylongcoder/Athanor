using System;
using System.Linq;
using NUnit.Framework;
using Athanor.Core;

namespace Athanor.Tests
{
    public class RulesTests
    {
        [Test]
        public void Rules_ShippedContent_HasTodaysValues()
        {
            // Arrange
            var data = TestContent.Load();

            // Act
            var r = new ContentDb(data).Rules;

            // Assert
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
        public void FinalDepth_ShippedContent_IsFive()
        {
            // Arrange
            var data = TestContent.Load();

            // Act
            var finalDepth = new ContentDb(data).FinalDepth;

            // Assert
            Assert.AreEqual(5, finalDepth);
        }

        static void Throws(Action<ContentData> change)
        {
            var data = TestContent.Load();
            change(data);
            Assert.Throws<ArgumentException>(() => new ContentDb(data));
        }

        [Test] public void Constructor_HeatedAspectWithoutAmount_Throws()
        {
            // Arrange
            // Act & Assert
            Throws(d => d.aspects.First(a => a.effect == AspectEffects.Heated).amount = 0);
        }

        [Test]
        public void Constructor_SeedAspectWithoutAmount_Throws()
        {
            // Arrange
            // Act & Assert
            Throws(d => d.aspects.First(a => a.effect == AspectEffects.Seed).amount = 0);
        }

        [Test]
        public void Constructor_HeatTileEffectWithoutAmount_Throws()
        {
            // Arrange
            // Act & Assert
            Throws(d =>
            {
                foreach (var e in d.events) foreach (var o in e.options)
                    foreach (var fx in (o.cost ?? new EffectData[0]).Concat(o.outcomes.SelectMany(x => x.gain ?? new EffectData[0])))
                        if (fx.op == Ops.HeatTile) fx.amount = 0;
            });
        }

        [Test] public void Satchel_PouchModeZero_PutsWonStartersInTheSatchel()
        {
            // Arrange
            var db = new ContentDb(TestContent.Load());
            var p = Profile.FromData(new ProfileData { unlockedStarters = new[] { "salt_crystal", "hollow_bone" } }, db);

            // Act
            var two = new RunState(db, new Journal(), 1, 1, new RunOptions { Unlocks = Unlocks.For(db, p) });
            var zero = new RunState(db, new Journal(), 1, 1, new RunOptions { Unlocks = Unlocks.For(db, p), PouchMode = 0 });
            var defaultPouchMode = new RunOptions().PouchMode;

            // Assert
            Assert.AreEqual(2, defaultPouchMode);
            Assert.AreEqual(6, two.Hand.Count);   // mode 2: the pouch card comes on top of the five
            Assert.AreEqual(5, zero.Hand.Count);  // mode 0: no pouch, the starters share the satchel
            Assert.AreEqual(two.SatchelSize, zero.SatchelSize);
        }

        [Test] public void Constructor_NoRulesBlock_Throws()
        {
            // Arrange
            // Act & Assert
            Throws(d => d.rules = null);
        }

        [Test] public void Constructor_PotencyTableShorterThanTiers_Throws()
        {
            // Arrange
            // Act & Assert
            Throws(d => d.rules.potencyByTier = new long[] { 0, 20 });
        }

        [Test]
        public void Constructor_AspectDepthOutsideTheRun_Throws()
        {
            // Arrange
            // Act & Assert
            Throws(d => d.rules.aspectDepths = new[] { 6 });
        }

        [Test]
        public void Constructor_HandBaseBelowHandMin_Throws()
        {
            // Arrange
            // Act & Assert
            Throws(d => { d.rules.handMin = 5; d.rules.handBase = 4; });
        }

        [Test]
        public void Constructor_DepthTargetsNotOneFewerThanDepths_Throws()
        {
            // Arrange
            // Act & Assert
            Throws(d => d.depthTargets = new[] { 300, 1500, 7500 });
        }

        static ContentDb Db(Action<RulesData> change)
        {
            var data = TestContent.Load();
            change(data.rules);
            return new ContentDb(data);
        }

        [Test] public void HandSize_HandBaseSeven_IsSeven()
        {
            // Arrange
            var db = Db(r => r.handBase = 7);

            // Act
            var handSize = new RunState(db, new Journal(), 1).HandSize;

            // Assert
            Assert.AreEqual(7, handSize);
        }

        [Test]
        public void TrialsLeft_TrialsSix_StartsDepthOneWithSix()
        {
            // Arrange
            var db = Db(r => r.trials = 6);

            // Act
            var trialsLeft = new RunState(db, new Journal(), 1).TrialsLeft;

            // Assert
            Assert.AreEqual(6, trialsLeft);
        }

        [Test]
        public void BasePotency_TableDoubled_IsDoubled()
        {
            // Arrange
            var db = Db(r => r.potencyByTier = new long[] { 0, 40, 120, 360, 1000, 3000 });

            // Act
            var potency = db.BasePotency(2);

            // Assert
            Assert.AreEqual(120, potency);
        }

        [Test]
        public void Phase_AspectDepthsTwo_OffersAspectsAtDepthTwoOnly()
        {
            // Arrange
            var db = Db(r => r.aspectDepths = new[] { 2 });

            // Act
            var run1 = new RunState(db, new Journal(), 1, 1, RunOptions.Full(Unlocks.All(db)));
            var run2 = new RunState(db, new Journal(), 1, 2, RunOptions.Full(Unlocks.All(db)));

            // Assert
            Assert.AreNotEqual(RunPhase.AspectOffer, run1.Phase);
            Assert.AreEqual(RunPhase.AspectOffer, run2.Phase);
        }
    }
}
