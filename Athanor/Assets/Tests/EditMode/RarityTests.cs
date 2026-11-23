using System;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>Phase 5a-04: catalyst offers and event gifts draw by the optional content.json "rarityWeights" (Common..WorldSeed).</summary>
    public class RarityTests
    {
        static CatalystData Cat(string effect, string rarity) =>
            new CatalystData { id = effect, name = effect, text = effect, effect = effect, kind = "", amount = 0, rarity = rarity };

        /// <summary>Seven catalysts, one per rarity; depth 1 clears with a+b.</summary>
        static ContentData Pool(int[] weights)
        {
            var d = TestContent.Tiny();
            d.depthNames = new[] { "1", "2" };
            d.depthTargets = new[] { 30 }; d.ultimate = "y";
            d.rules.aspectDepths = new[] { 1, 2 };
            d.catalysts = new[]
            {
                Cat(Effects.ExtraHand, Rarities.Common), Cat(Effects.ExtraTrial, Rarities.Uncommon),
                Cat(Effects.FirstTriple, Rarities.Rare), Cat(Effects.ComboBonus, Rarities.Epic),
                Cat(Effects.ChainPlus, Rarities.Legendary), Cat(Effects.TierDouble, Rarities.Mythic),
                Cat(Effects.Diagonals, Rarities.WorldSeed),
            };
            d.rarityWeights = weights;
            return d;
        }

        static string[] Offer(ContentData d, int seed)
        {
            for (int s = seed; s < seed + 500; s++)
            {
                var r = new RunState(new ContentDb(d), new Journal(), s, 1);
                var hand = r.Hand.ToList();
                if (!hand.Contains("a") || !hand.Contains("b")) continue;
                r.Place(hand.IndexOf("a"), 0); r.Place(r.Hand.ToList().IndexOf("b"), 1);
                if (r.Ignite().PhaseAfter != RunPhase.DepthCleared) continue;
                return r.CatalystOffer.Select(c => c.id).ToArray();
            }
            throw new Exception("no seed reaches an offer");
        }

        [Test]
        public void NoField_Equals_AllEqualWeights_Equals_Today()
        {
            // Arrange
            var ones = Enumerable.Repeat(1, Rarities.All.Length).ToArray();
            var fives = Enumerable.Repeat(5, Rarities.All.Length).ToArray();

            // Act & Assert
            for (int seed = 1; seed <= 20; seed++)
            {
                var none = Offer(Pool(null), seed);
                CollectionAssert.AreEqual(none, Offer(Pool(ones), seed), "seed " + seed);
                CollectionAssert.AreEqual(none, Offer(Pool(fives), seed), "seed " + seed);
            }
        }

        [Test]
        public void Offer_IsDeterministicPerSeed_AndStaysDistinct()
        {
            // Arrange
            var w = new[] { 6, 3, 2, 1, 1, 1, 1 };

            // Act & Assert
            for (int seed = 1; seed <= 20; seed++)
            {
                var one = Offer(Pool(w), seed);
                CollectionAssert.AreEqual(one, Offer(Pool(w), seed));
                Assert.AreEqual(TestContent.Today.offerSize, one.Distinct().Count());
            }
        }

        [Test]
        public void Offer_HeavierRarityWeight_FavoursThatRarity()
        {
            // Arrange — a heavy Common weight against seven total; Common should dominate, WorldSeed (last) rarely leads
            var w = new[] { 10, 1, 1, 1, 1, 1, 1 };
            int commonFirst = 0, worldSeedFirst = 0; const int n = 300;

            // Act
            for (int seed = 1; seed <= n; seed++)
            {
                var first = Offer(Pool(w), seed)[0];
                if (first == Effects.ExtraHand) commonFirst++;
                if (first == Effects.Diagonals) worldSeedFirst++;
            }

            // Assert — 10 of 16 for the Common one (about 62.5%), 1 of 16 for the WorldSeed
            Assert.That(commonFirst, Is.InRange(n * 48 / 100, n * 80 / 100), commonFirst + " of " + n);
            Assert.That(worldSeedFirst, Is.LessThan(n / 6), worldSeedFirst + " of " + n);
        }

        [Test]
        public void Offer_ZeroWeights_ExcludeARarityWhileOthersRemain()
        {
            // Arrange — Uncommon and Rare are open; Common and the four heavier tiers are closed
            var w = new[] { 0, 1, 1, 0, 0, 0, 0 };

            // Act & Assert
            for (int seed = 1; seed <= 30; seed++)
            {
                var offer = Offer(Pool(w), seed);
                CollectionAssert.Contains(offer, Effects.ExtraTrial); CollectionAssert.Contains(offer, Effects.FirstTriple);
                Assert.AreEqual(TestContent.Today.offerSize, offer.Distinct().Count());   // the third slot still fills
            }
        }

        [Test]
        public void FirstOffer_AnyWeights_IsUntouched()
        {
            // Arrange
            var opts = new RunOptions { FirstOffer = new[] { Effects.ExtraHand, Effects.ChainPlus } };
            var w = new[] { 0, 0, 0, 0, 1, 0, 0 };

            // Act & Assert
            for (int seed = 1; seed <= 10; seed++)
            {
                var d = Pool(w);
                for (int s = seed; s < seed + 500; s++)
                {
                    var r = new RunState(new ContentDb(d), new Journal(), s, 1, opts);
                    var hand = r.Hand.ToList();
                    if (!hand.Contains("a") || !hand.Contains("b")) continue;
                    r.Place(hand.IndexOf("a"), 0); r.Place(r.Hand.ToList().IndexOf("b"), 1);
                    if (r.Ignite().PhaseAfter != RunPhase.DepthCleared) continue;
                    CollectionAssert.AreEqual(new[] { Effects.ExtraHand, Effects.ChainPlus }, r.CatalystOffer.Select(c => c.id).ToArray());
                    break;
                }
            }
        }

        static ContentData WithGift(int[] weights)
        {
            var d = Pool(weights);
            d.events = new[] { new EventData { id = "e", name = "e", text = "e", type = "Boon", minDepth = 1, maxDepth = 5, weight = 1,
                options = new[] { new EventOption { label = "go", outcomes = new[] { new EventOutcome { weight = 1, text = "go",
                    gain = new[] { new EffectData { op = Ops.CatalystGift } } } } } } } };
            d.noEventWeight = 0;
            return d;
        }

        static string Gift(ContentData d, int seed)
        {
            var r = new RunState(new ContentDb(d), new Journal(), seed, 1, new RunOptions { Events = true });
            Assert.AreEqual(RunPhase.EventOffer, r.Phase);
            r.ChooseEventOption(0);
            return r.Catalysts.Count == 1 ? r.Catalysts[0].id : null;
        }

        [Test]
        public void Gift_NoField_Equals_Uniform_Today()
        {
            // Arrange
            var twos = Enumerable.Repeat(2, Rarities.All.Length).ToArray();

            // Act & Assert
            for (int seed = 1; seed <= 20; seed++)
                Assert.AreEqual(Gift(WithGift(null), seed), Gift(WithGift(twos), seed), "seed " + seed);
        }

        [Test]
        public void Gift_FollowsTheWeights_AndIsDeterministic()
        {
            // Arrange
            var onlyLegendary = new[] { 0, 0, 0, 0, 1, 0, 0 };
            var descending = new[] { 4, 3, 2, 1, 1, 1, 1 };
            var heavyCommon = new[] { 10, 1, 1, 1, 1, 1, 1 };

            // Act & Assert
            for (int seed = 1; seed <= 20; seed++)
            {
                Assert.AreEqual(Effects.ChainPlus, Gift(WithGift(onlyLegendary), seed));
                Assert.AreEqual(Gift(WithGift(descending), seed), Gift(WithGift(descending), seed));
            }
            int common = 0; const int n = 300;
            for (int seed = 1; seed <= n; seed++) if (Gift(WithGift(heavyCommon), seed) == Effects.ExtraHand) common++;
            Assert.That(common, Is.InRange(n * 48 / 100, n * 80 / 100), common + " of " + n);
        }

        [Test]
        public void Validation_BadWeights_AreRejected()
        {
            // Arrange — one short of the seven rarities, and a negative one
            var shortWeights = Enumerable.Repeat(1, Rarities.All.Length - 1).ToArray();
            var negative = new[] { 1, -1, 1, 1, 1, 1, 1 };
            var allZero = Enumerable.Repeat(0, Rarities.All.Length).ToArray();
            var oneOpen = new[] { 0, 1, 0, 0, 0, 0, 0 };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new ContentDb(Pool(shortWeights)));
            Assert.Throws<ArgumentException>(() => new ContentDb(Pool(negative)));
            Assert.Throws<ArgumentException>(() => new ContentDb(Pool(allZero)));
            Assert.DoesNotThrow(() => new ContentDb(Pool(oneOpen)));
        }

        // Both states are valid: the field absent (every rarity weighs 1, the uniform draw) or present
        // (one weight per rarity, each at least 1). The test pins neither the numbers nor which state.
        [Test]
        public void RealContent_RarityWeights_AbsentIsUniformOrPresentIsValid()
        {
            // Arrange
            var content = TestContent.Load();

            // Act
            var db = new ContentDb(content);

            // Assert
            if (content.rarityWeights == null)
            {
                foreach (var r in Rarities.All) Assert.AreEqual(1, db.RarityWeight(r), "absent field weighs 1: " + r);
                Assert.IsTrue(db.UniformRarity, "absent field is the uniform draw");
            }
            else
            {
                Assert.AreEqual(Rarities.All.Length, content.rarityWeights.Length, "one weight per rarity");
                foreach (var r in Rarities.All) Assert.GreaterOrEqual(db.RarityWeight(r), 1, r);
            }
        }

        /// <summary>The rarity enum covers six material tiers plus WorldSeed as a unique tier.</summary>
        [Test]
        public void Rarities_All_HasSevenEntries()
        {
            // Arrange
            // Act
            var all = Rarities.All;

            // Assert
            Assert.AreEqual(7, all.Length);
            CollectionAssert.Contains(all, Rarities.Epic);
            CollectionAssert.Contains(all, Rarities.Mythic);
            CollectionAssert.Contains(all, Rarities.WorldSeed);
        }
    }
}