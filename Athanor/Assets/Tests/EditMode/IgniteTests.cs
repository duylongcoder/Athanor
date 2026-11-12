using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    public class IgniteTests
    {
        ContentDb db; Journal journal; HashSet<string> fired;
        static readonly CatalystData[] None = new CatalystData[0];

        [SetUp] public void Init() { db = new ContentDb(TestContent.Tiny()); journal = new Journal(); fired = new HashSet<string>(); }

        static Bench On(params object[] tileThenId)
        {
            var b = new Bench();
            for (int i = 0; i < tileThenId.Length; i += 2) b.Tiles[(int)tileThenId[i]] = (string)tileThenId[i + 1];
            return b;
        }
        List<ReactionEvent> Go(Bench b, params CatalystData[] cats) => IgniteResolver.Resolve(b, db, journal, fired, cats).Events;

        /// <summary>The same on TestContent.Combo(): p 8, q 5, r 2, s 9, t 4, v 1.</summary>
        List<ReactionEvent> Combo(Bench b, int zMin = 15, params CatalystData[] cats) =>
            IgniteResolver.Resolve(b, new ContentDb(TestContent.Combo(zMin)), journal, fired, cats).Events;

        [Test] public void Pairs_FullBench_Has12Orthogonal_InReadingOrder()
        {
            var b = new Bench(); for (int i = 0; i < 9; i++) b.Tiles[i] = "a";
            var p = IgniteResolver.Pairs(b, false);
            Assert.AreEqual(12, p.Count);
            Assert.AreEqual((0, 1), p[0]); Assert.AreEqual((0, 3), p[1]); Assert.AreEqual((1, 2), p[2]); Assert.AreEqual((7, 8), p[11]);
        }

        [Test] public void Pairs_WithDiagonals_Has20()
        {
            var b = new Bench(); for (int i = 0; i < 9; i++) b.Tiles[i] = "a";
            Assert.AreEqual(20, IgniteResolver.Pairs(b, true).Count);
        }

        [Test] public void Pairs_DoNotWrapAcrossRows()
        {
            Assert.IsFalse(IgniteResolver.HasAnyPair(On(2, "a", 3, "b"), false));
        }

        [Test] public void NoPairs_ReturnsEmpty()
        {
            Assert.AreEqual(0, Go(On(0, "a", 8, "b")).Count);
            Assert.IsFalse(IgniteResolver.HasAnyPair(On(0, "a"), false));
        }

        [Test] public void SingleReaction_Scores40_OnFirstRunDiscovery()
        {
            var e = Go(On(0, "a", 1, "b")).Single();
            Assert.AreEqual("x", e.Result); Assert.IsTrue(e.Discovery); Assert.IsTrue(e.FirstThisRun);
            Assert.AreEqual(40, e.Potency);
            Assert.IsTrue(journal.IsKnown("b", "a"));
        }

        [Test] public void SecondReactionInIgnite_IsMultipliedBy2()
        {
            var ev = Go(On(0, "a", 1, "b", 7, "x", 8, "c"));
            Assert.AreEqual(2, ev.Count);
            Assert.AreEqual(40, ev[0].Potency);
            Assert.AreEqual(240, ev[1].Potency); // 60 x 2 (first this run) x 2 (second in chain)
        }

        [Test] public void SameRecipeTwiceInOneIgnite_OnlyFirstIsDiscovery()
        {
            var ev = Go(On(0, "a", 1, "b", 7, "a", 8, "b"));
            Assert.IsTrue(ev[0].Discovery);
            Assert.IsFalse(ev[1].Discovery); Assert.IsFalse(ev[1].FirstThisRun);
            Assert.AreEqual(40, ev[1].Potency); // 20 x chain 2
        }

        [Test] public void KnownRecipe_NextRun_ScoresFirstThisRunOnly()
        {
            journal.Learn("a", "b");
            var e = Go(On(0, "a", 1, "b")).Single();
            Assert.IsFalse(e.Discovery); Assert.IsTrue(e.FirstThisRun); Assert.AreEqual(40, e.Potency);
            var again = Go(On(0, "a", 1, "b")).Single();
            Assert.AreEqual(20, again.Potency);
        }

        [Test] public void Fizzle_GivesHint_ThenNoDuplicateHint()
        {
            var e1 = Go(On(0, "a", 1, "c")).Single();
            Assert.IsNull(e1.Result); Assert.AreEqual(0, e1.Potency);
            Assert.AreEqual("a", e1.Hint.material); Assert.AreEqual("Ore", e1.Hint.partnerKind);
            var e2 = Go(On(0, "a", 1, "c")).Single();
            Assert.AreEqual("c", e2.Hint.material); Assert.AreEqual("Metal", e2.Hint.partnerKind);
            Assert.IsNull(Go(On(0, "a", 1, "c")).Single().Hint);
        }

        /// <summary>p(8) + q(5) make x, but only with 17 of heat round the hub: on their own they are 13.</summary>
        static ContentDb NeedsCompany()
        {
            var d = TestContent.Combo(); d.recipes = new[] { TestContent.Rec(new[] { "p", "q" }, 17, "x") };
            return new ContentDb(d);
        }

        [Test] public void Fizzle_ForLackOfHeat_HintsAtTheHeatNeeded_Once()
        {
            var c = NeedsCompany();
            var e = IgniteResolver.Resolve(On(0, "p", 1, "q"), c, journal, fired, None).Events.Single();
            Assert.IsNull(e.Result);
            Assert.AreEqual("p", e.Hint.material); Assert.AreEqual(17, e.Hint.heat);
            var again = IgniteResolver.Resolve(On(0, "p", 1, "q"), c, journal, fired, None).Events.Single();
            Assert.AreEqual(0, again.Hint.heat, "the second time the ordinary hint comes");
            Assert.AreEqual("Potion", again.Hint.partnerKind);
        }

        [Test] public void Fizzle_ForLackOfHeat_SaysNothingAboutAKnownRecipe()
        {
            journal.Learn(new[] { "p", "q" });
            Assert.IsNull(IgniteResolver.Resolve(On(0, "p", 1, "q"), NeedsCompany(), journal, fired, None).Events.Single().Hint);
        }

        [Test] public void WithCompany_TheSameBenchReacts()
        {
            var events = IgniteResolver.Resolve(On(0, "p", 1, "q", 3, "t"), NeedsCompany(), journal, fired, None).Events;
            Assert.AreEqual("x", events[0].Result);
        }

        [Test] public void Fizzle_DoesNotHintAtAlreadyKnownRecipes()
        {
            journal.Learn("a", "b");
            var e = Go(On(0, "a", 1, "c")).Single();
            Assert.AreEqual("c", e.Hint.material);
        }

        [Test] public void Fizzles_ComeAfterReactions_AndDoNotAdvanceTheChain()
        {
            var ev = Go(On(0, "a", 1, "c", 7, "a", 8, "b"));
            Assert.AreEqual(2, ev.Count);
            Assert.AreEqual("x", ev[0].Result); Assert.AreEqual(40, ev[0].Potency); Assert.AreEqual(1, ev[0].Chain);
            Assert.IsNull(ev[1].Result); Assert.IsFalse(ev[1].Sputter);
            CollectionAssert.AreEqual(new[] { 0, 1 }, ev[1].Tiles); CollectionAssert.AreEqual(new[] { "a", "c" }, ev[1].Items);
        }

        [Test] public void KindDouble_Doubles() =>
            Assert.AreEqual(80, Go(On(0, "a", 1, "b"), TestContent.Cat(Effects.KindDouble, "Ore")).Single().Potency);

        [Test] public void KindDouble_OtherKind_NoChange() =>
            Assert.AreEqual(40, Go(On(0, "a", 1, "b"), TestContent.Cat(Effects.KindDouble, "Beast")).Single().Potency);

        [Test] public void FirstTriple_OnlyFirst()
        {
            var ev = Go(On(0, "a", 1, "b", 7, "x", 8, "c"), TestContent.Cat(Effects.FirstTriple));
            Assert.AreEqual(120, ev[0].Potency); Assert.AreEqual(240, ev[1].Potency);
        }

        [Test] public void CentreTwice_DoublesPotency_WhenCentreInvolved()
        {
            var e = Go(On(4, "a", 5, "b"), TestContent.Cat(Effects.CentreTwice)).Single();
            Assert.AreEqual(80, e.Potency);
        }

        [Test] public void CentreTwice_NoChange_AwayFromCentre() =>
            Assert.AreEqual(40, Go(On(0, "a", 1, "b"), TestContent.Cat(Effects.CentreTwice)).Single().Potency);

        [Test] public void EmptyTileBonus_Adds20PerEmptyTile() =>
            Assert.AreEqual(40 + 20 * 7, Go(On(0, "a", 1, "b"), TestContent.Cat(Effects.EmptyTileBonus, "", 20)).Single().Potency);

        [Test] public void ChainPlus_CountsOneStepHigher() =>
            Assert.AreEqual(80, Go(On(0, "a", 1, "b"), TestContent.Cat(Effects.ChainPlus)).Single().Potency);

        [Test] public void DiscoveryTriple_Triples() =>
            Assert.AreEqual(120, Go(On(0, "a", 1, "b"), TestContent.Cat(Effects.DiscoveryTriple)).Single().Potency);

        [Test] public void Diagonals_LetCornersReactWithCentre()
        {
            Assert.AreEqual(0, Go(On(0, "a", 4, "b")).Count);
            Assert.AreEqual("x", Go(On(0, "a", 4, "b"), TestContent.Cat(Effects.Diagonals)).Single().Result);
        }

        // ---- combos and chains ---------------------------------------------------------------------

        [Test] public void Reaction_Event_NamesHubTilesAndItems()
        {
            var e = Combo(On(0, "q", 1, "p")).Single();
            Assert.AreEqual(1, e.Hub);
            CollectionAssert.AreEqual(new[] { 1, 0 }, e.Tiles); CollectionAssert.AreEqual(new[] { "p", "q" }, e.Items);
            Assert.AreEqual("x", e.Result); Assert.AreEqual("x", e.Recipe.result);
            Assert.AreEqual(40, e.Potency);   // 20 x (2 - 1) x 2 first this run
        }

        [Test] public void ThreeItem_Scores240()
        {
            var e = Combo(On(4, "p", 3, "q", 5, "r")).Single();
            Assert.AreEqual("z", e.Result);
            Assert.AreEqual(240, e.Potency);  // 60 x (3 - 1) x 2
        }

        [Test] public void FiveItem_ScoresFourTimesBase()
        {
            var e = Combo(On(4, "s", 1, "p", 3, "q", 5, "r", 7, "t")).Single();
            Assert.AreEqual("big", e.Result);
            Assert.AreEqual(1440, e.Potency); // 180 x (5 - 1) x 2
        }

        [Test] public void Chain_SecondReaction_MultipliedByChain()
        {
            var ev = Combo(On(3, "p", 4, "q", 0, "v"));
            Assert.AreEqual(2, ev.Count);
            Assert.AreEqual(1, ev[0].Chain); Assert.AreEqual(40, ev[0].Potency);
            Assert.AreEqual(2, ev[1].Chain); Assert.AreEqual(240, ev[1].Potency);  // 60 x 1 x 2 first x 2 chain
            Assert.AreEqual(3, ev[1].Hub); CollectionAssert.AreEqual(new[] { "x", "v" }, ev[1].Items);
        }

        [Test] public void ChainPlus_AddsOne_ToEveryStep()
        {
            var ev = Combo(On(3, "p", 4, "q", 0, "v"), 15, TestContent.Cat(Effects.ChainPlus));
            Assert.AreEqual(80, ev[0].Potency); Assert.AreEqual(360, ev[1].Potency);
        }

        [Test] public void KindDouble_ChecksEveryConsumedItem() =>
            Assert.AreEqual(480, Combo(On(4, "p", 3, "q", 5, "r"), 15, TestContent.Cat(Effects.KindDouble, "Plant")).Single().Potency);

        [Test] public void Journal_LearnsComboKey()
        {
            var e = Combo(On(4, "p", 3, "q", 5, "r")).Single();
            Assert.IsTrue(e.Discovery);
            Assert.IsTrue(journal.IsKnown(new[] { "r", "q", "p" }));
            Assert.IsTrue(fired.Contains("p+q+r"));
        }

        [Test] public void UnderThreshold_Combo_FallsBack_AndTheLeftoverFizzles_NotSputters()
        {
            var ev = Combo(On(4, "p", 3, "q", 5, "r"), 16);
            Assert.AreEqual("x", ev[0].Result);
            Assert.AreEqual(2, ev.Count);
            Assert.IsNull(ev[1].Result); Assert.IsFalse(ev[1].Sputter);
            CollectionAssert.AreEqual(new[] { "x", "r" }, ev[1].Items);
            Assert.IsFalse(journal.IsKnown(new[] { "p", "q", "r" }));
        }

        [Test] public void Sputter_Event_HasNoPotencyAndNoDiscovery()
        {
            var ev = Combo(On(4, "p", 3, "q", 5, "r", 0, "s"));
            Assert.AreEqual("m", ev[0].Result);
            var sp = ev[1];
            Assert.IsTrue(sp.Sputter); Assert.IsNull(sp.Result); Assert.AreEqual(0, sp.Potency); Assert.IsFalse(sp.Discovery);
            CollectionAssert.AreEquivalent(new[] { 4, 3, 5 }, sp.Tiles);
            Assert.IsFalse(journal.IsKnown(new[] { "p", "q", "r" }));
            Assert.IsNull(ev[2].Result); Assert.IsFalse(ev[2].Sputter);   // p and r are left side by side
        }

        [Test] public void Fizzle_AfterChain_ForLeftoverPair_GivesHintOnce()
        {
            var first = Combo(On(0, "p", 1, "r")).Single();
            Assert.IsNull(first.Result); Assert.IsNotNull(first.Hint);
            int hints = journal.Hints.Count;
            for (int k = 0; k < 12; k++) Combo(On(0, "p", 1, "r"));
            Assert.IsNull(Combo(On(0, "p", 1, "r")).Single().Hint);
            Assert.That(journal.Hints.Count, Is.GreaterThan(hints));
        }

        [Test] public void Resolve_ReportsFinalBench_AndDoesNotMutateTheBench()
        {
            var b = On(3, "p", 4, "q", 0, "v");
            var res = IgniteResolver.Resolve(b, new ContentDb(TestContent.Combo()), journal, fired, None);
            Assert.AreEqual("p", b.Tiles[3]); Assert.AreEqual("q", b.Tiles[4]); Assert.AreEqual("v", b.Tiles[0]);
            Assert.AreEqual("u", res.Final[3]); Assert.IsTrue(res.Made[3]);
            Assert.AreEqual(1, res.Final.Count(t => t != null));
        }
    }
}
