using System;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>Phase 5a-04: catalyst offers and event gifts draw by the optional content.json "rarityWeights" (Common..Legendary).</summary>
    public class RarityTests
    {
        static CatalystData Cat(string effect, string rarity) =>
            new CatalystData { id = effect, name = effect, text = effect, effect = effect, kind = "", amount = 0, rarity = rarity };

        /// <summary>Four catalysts, one per rarity; depth 1 clears with a+b.</summary>
        static ContentData Pool(int[] weights)
        {
            var d = TestContent.Tiny();
            d.depthTargets = new[] { 30, 900, 2500, 7000 }; d.ultimate = "y";
            d.catalysts = new[]
            {
                Cat(Effects.ExtraHand, "Common"), Cat(Effects.ExtraTrial, "Uncommon"), Cat(Effects.FirstTriple, "Rare"), Cat(Effects.ChainPlus, "Legendary"),
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

        [Test] public void NoField_Equals_AllEqualWeights_Equals_Today()
        {
            for (int seed = 1; seed <= 20; seed++)
            {
                var none = Offer(Pool(null), seed);
                CollectionAssert.AreEqual(none, Offer(Pool(new[] { 1, 1, 1, 1 }), seed), "seed " + seed);
                CollectionAssert.AreEqual(none, Offer(Pool(new[] { 5, 5, 5, 5 }), seed), "seed " + seed);
            }
        }

        [Test] public void Offer_IsDeterministicPerSeed_AndStaysDistinct()
        {
            var w = new[] { 6, 3, 2, 1 };
            for (int seed = 1; seed <= 20; seed++)
            {
                var one = Offer(Pool(w), seed);
                CollectionAssert.AreEqual(one, Offer(Pool(w), seed));
                Assert.AreEqual(TestContent.Today.offerSize, one.Distinct().Count());
            }
        }

        [Test] public void Offer_HeavierRarityWeight_FavoursThatRarity()
        {
            int commonFirst = 0, legendaryFirst = 0; const int n = 300;
            for (int seed = 1; seed <= n; seed++)
            {
                var first = Offer(Pool(new[] { 10, 1, 1, 1 }), seed)[0];
                if (first == Effects.ExtraHand) commonFirst++;
                if (first == Effects.ChainPlus) legendaryFirst++;
            }
            // 10 of 13 for the Common one (about 77%), 1 of 13 for the Legendary
            Assert.That(commonFirst, Is.InRange(n * 62 / 100, n * 90 / 100), commonFirst + " of " + n);
            Assert.That(legendaryFirst, Is.LessThan(n / 6), legendaryFirst + " of " + n);
        }

        [Test] public void Offer_ZeroWeights_ExcludeARarityWhileOthersRemain()
        {
            for (int seed = 1; seed <= 30; seed++)
            {
                var offer = Offer(Pool(new[] { 0, 1, 1, 0 }), seed);
                CollectionAssert.Contains(offer, Effects.ExtraTrial); CollectionAssert.Contains(offer, Effects.FirstTriple);
                Assert.AreEqual(TestContent.Today.offerSize, offer.Distinct().Count());   // the third slot still fills
            }
        }

        [Test] public void FirstOffer_AnyWeights_IsUntouched()
        {
            var opts = new RunOptions { FirstOffer = new[] { Effects.ExtraHand, Effects.ChainPlus } };
            for (int seed = 1; seed <= 10; seed++)
            {
                var d = Pool(new[] { 0, 0, 0, 1 });
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

        [Test] public void Gift_NoField_Equals_Uniform_Today()
        {
            for (int seed = 1; seed <= 20; seed++)
                Assert.AreEqual(Gift(WithGift(null), seed), Gift(WithGift(new[] { 2, 2, 2, 2 }), seed), "seed " + seed);
        }

        [Test] public void Gift_FollowsTheWeights_AndIsDeterministic()
        {
            for (int seed = 1; seed <= 20; seed++)
            {
                Assert.AreEqual(Effects.ChainPlus, Gift(WithGift(new[] { 0, 0, 0, 1 }), seed));
                Assert.AreEqual(Gift(WithGift(new[] { 4, 3, 2, 1 }), seed), Gift(WithGift(new[] { 4, 3, 2, 1 }), seed));
            }
            int common = 0; const int n = 300;
            for (int seed = 1; seed <= n; seed++) if (Gift(WithGift(new[] { 10, 1, 1, 1 }), seed) == Effects.ExtraHand) common++;
            Assert.That(common, Is.InRange(n * 62 / 100, n * 90 / 100), common + " of " + n);
        }

        [Test] public void Validation_BadWeights_AreRejected()
        {
            Assert.Throws<ArgumentException>(() => new ContentDb(Pool(new[] { 1, 1, 1 })));
            Assert.Throws<ArgumentException>(() => new ContentDb(Pool(new[] { 1, -1, 1, 1 })));
            Assert.Throws<ArgumentException>(() => new ContentDb(Pool(new[] { 0, 0, 0, 0 })));
            Assert.DoesNotThrow(() => new ContentDb(Pool(new[] { 0, 1, 0, 0 })));
        }

        // Both states are valid: the field absent (every rarity weighs 1, the uniform draw) or present
        // (one weight per rarity, each at least 1). The test pins neither the numbers nor which state.
        [Test] public void RealContent_RarityWeights_AbsentIsUniformOrPresentIsValid()
        {
            var content = TestContent.Load();
            var db = new ContentDb(content);
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
    }
}
