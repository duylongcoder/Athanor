using System.Linq;
using Athanor.Gameplay;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>The chain planner on TestContent.Combo(): p 8, q 5, r 2, s 9, t 4, v 1 (Reaction Index).</summary>
    public class ChainTests
    {
        static ContentDb Db(int zMin = 15) => new ContentDb(TestContent.Combo(zMin));

        static string[] On(params object[] tileThenId)
        {
            var b = new string[Bench.TileCount];
            for (int i = 0; i < tileThenId.Length; i += 2) b[(int)tileThenId[i]] = (string)tileThenId[i + 1];
            return b;
        }

        static ChainPlan Plan(string[] tiles, int zMin = 15, bool diagonals = false) => ChainPlanner.Plan(tiles, Db(zMin), diagonals);

        [Test] public void Neighbours_WithAndWithoutDiagonals_ReturnOrthogonalAndDiagonalTiles()
        {
            CollectionAssert.AreEquivalent(new[] { 1, 3, 5, 7 }, ChainPlanner.Neighbours(4, false).ToList());
            CollectionAssert.AreEquivalent(new[] { 1, 3 }, ChainPlanner.Neighbours(0, false).ToList());
            CollectionAssert.AreEquivalent(new[] { 1, 3, 4 }, ChainPlanner.Neighbours(0, true).ToList());
            Assert.AreEqual(8, ChainPlanner.Neighbours(4, true).Count());
        }

        [Test] public void Hub_WithTwoReactiveItems_IsTheHighestReactivity()
        {
            var plan = Plan(On(0, "q", 1, "p"));
            Assert.AreEqual(1, plan.Steps.Count);
            Assert.AreEqual(1, plan.Steps[0].Hub);
            CollectionAssert.AreEqual(new[] { 1, 0 }, plan.Steps[0].Tiles);
            Assert.AreEqual("x", plan.Final[1]); Assert.IsNull(plan.Final[0]);
        }

        [Test] public void Hub_RiTie_GoesToEarlierTile()
        {
            var plan = Plan(On(1, "p", 3, "p", 4, "q"));
            Assert.AreEqual(1, plan.Steps[0].Hub);
            Assert.AreEqual("x", plan.Final[1]); Assert.AreEqual("p", plan.Final[3]); Assert.IsNull(plan.Final[4]);
        }

        [Test] public void Hub_WithoutMatch_DoesNotBlockOthers()
        {
            var plan = Plan(On(8, "s", 3, "p", 4, "q"));
            Assert.AreEqual(1, plan.Steps.Count);
            Assert.AreEqual(3, plan.Steps[0].Hub);
            Assert.AreEqual("s", plan.Final[8]);
        }

        [Test] public void Plan_WithThreeItemComboAndPairs_FiresLargestComboFirst()
        {
            var plan = Plan(On(4, "p", 3, "q", 5, "r"));
            Assert.AreEqual(1, plan.Steps.Count);
            Assert.AreEqual("z", plan.Steps[0].Recipe.result);
            CollectionAssert.AreEqual(new[] { 4, 3, 5 }, plan.Steps[0].Tiles);
            Assert.AreEqual("z", plan.Final[4]); Assert.IsNull(plan.Final[3]); Assert.IsNull(plan.Final[5]);
        }

        [Test] public void ShortOfReactivity_IsNoReaction_FallsBackToSmaller()
        {
            var plan = Plan(On(4, "p", 3, "q", 5, "r"), zMin: 16);
            Assert.AreEqual(1, plan.Steps.Count);
            Assert.AreEqual("x", plan.Steps[0].Recipe.result);
            Assert.IsFalse(plan.Steps[0].Sputter);
            Assert.AreEqual("x", plan.Final[4]); Assert.AreEqual("r", plan.Final[5]);
        }

        // ---- company: the hub's other neighbours count toward a recipe's required total ----
        // p(8) on the centre with q(5) and r(2) is 15 of heat; t(4) touching the hub makes it 19.

        [Test] public void Heat_WithBystandersOnTheBench_IsTheHubPlusEverythingTouchingIt()
        {
            var db = Db();
            Assert.AreEqual(19, ChainPlanner.Heat(On(4, "p", 3, "q", 5, "r", 1, "t"), 4, db, false));
            Assert.AreEqual(15, ChainPlanner.Heat(On(4, "p", 3, "q", 5, "r", 0, "t"), 4, db, false));
            Assert.AreEqual(19, ChainPlanner.Heat(On(4, "p", 3, "q", 5, "r", 0, "t"), 4, db, true));
            Assert.AreEqual(8, ChainPlanner.Heat(On(4, "p"), 4, db, false));
            Assert.AreEqual(0, ChainPlanner.Heat(On(4, "p"), 0, db, false));
        }

        [Test] public void Company_TouchingTheHub_LiftsARecipeOverItsRequiredTotal()
        {
            var plan = Plan(On(4, "p", 3, "q", 5, "r", 1, "t"), zMin: 19);
            Assert.AreEqual(1, plan.Steps.Count);
            Assert.AreEqual("z", plan.Steps[0].Recipe.result);
            CollectionAssert.AreEqual(new[] { 4, 3, 5 }, plan.Steps[0].Tiles);   // the bystander is not consumed
            Assert.AreEqual("t", plan.Final[1]); Assert.IsFalse(plan.Made[1]);
        }

        [Test] public void DullCompany_BelowTheRequiredTotal_LeavesTheRecipeShort()
        {
            var plan = Plan(On(4, "p", 3, "q", 5, "r", 1, "t"), zMin: 20);
            Assert.AreEqual("x", plan.Steps[0].Recipe.result);
        }

        [Test] public void Company_NotTouchingTheHub_DoesNotLiftTheRecipe()
        {
            var plan = Plan(On(4, "p", 3, "q", 5, "r", 0, "t"), zMin: 19);
            Assert.AreEqual("x", plan.Steps[0].Recipe.result);
        }

        [Test] public void Company_OnADiagonal_CountsWithDiagonals()
        {
            var plan = Plan(On(4, "p", 3, "q", 5, "r", 0, "t"), zMin: 19, diagonals: true);
            Assert.AreEqual("z", plan.Steps[0].Recipe.result);
        }

        [Test] public void Company_ConsumedEarlierInTheChain_NoLongerCounts()
        {
            // s(9) fires first with the q beside it; that q was the company the z on the centre was counting on
            var d = TestContent.Combo(20);   // 8 + 5 + 2 + the q on tile 1 = 20
            var plan = ChainPlanner.Plan(On(4, "p", 3, "q", 5, "r", 1, "q", 0, "s"), new ContentDb(d), false);
            Assert.AreEqual("m", plan.Steps[0].Recipe.result);
            Assert.IsFalse(plan.Steps.Any(s => !s.Sputter && s.Recipe.result == "z"));
        }

        [Test] public void ShortAt_WhenOnlyHeatIsLacking_NamesTheRecipe()
        {
            var db = Db(19);
            var tiles = On(4, "p", 3, "q", 5, "r");
            Assert.AreEqual("z", ChainPlanner.ShortAt(tiles, 4, db, false).result);
            Assert.IsNull(ChainPlanner.ShortAt(On(4, "p", 3, "q", 5, "r", 1, "t"), 4, db, false), "with company nothing is short");
            Assert.IsNull(ChainPlanner.ShortAt(tiles, 3, db, false), "q's pair with p has heat enough");
            Assert.IsNull(ChainPlanner.ShortAt(On(4, "p", 5, "r"), 4, db, false), "no recipe has that shape");
            Assert.IsNull(ChainPlanner.ShortAt(tiles, 0, db, false), "an empty tile");
        }

        [Test] public void Chain_WhenResultMatchesAnotherRecipe_ReactsAgain()
        {
            var plan = Plan(On(3, "p", 4, "q", 0, "v"));
            CollectionAssert.AreEqual(new[] { "x", "u" }, plan.Steps.Select(s => s.Recipe.result).ToList());
            Assert.AreEqual(3, plan.Steps[1].Hub);
            Assert.AreEqual("u", plan.Final[3]); Assert.IsTrue(plan.Made[3]);
            Assert.AreEqual(1, plan.Final.Count(t => t != null));
            Assert.AreEqual(1, plan.Made.Count(m => m));
        }

        [Test] public void Made_IsFalse_ForUntouchedItems()
        {
            var plan = Plan(On(8, "s", 3, "p", 4, "q"));
            Assert.IsFalse(plan.Made[8]); Assert.IsTrue(plan.Made[3]);
        }

        [Test] public void Corner_WithTwoOrthogonalNeighbours_CannotHostFourItems()
        {
            var plan = Plan(On(0, "s", 1, "p", 3, "q", 4, "t"));
            Assert.IsFalse(plan.Steps.Any(s => s.Recipe.result == "quad"));
            Assert.AreEqual("m", plan.Steps[0].Recipe.result);   // the corner hub falls back to its pair
        }

        [Test] public void EdgeMiddle_WithThreeNeighbours_HostsFourItems()
        {
            var plan = Plan(On(1, "s", 0, "p", 2, "q", 4, "t"));
            Assert.AreEqual("quad", plan.Steps[0].Recipe.result);
            Assert.AreEqual(4, plan.Steps[0].Tiles.Length);
        }

        [Test] public void Centre_WithFourNeighbours_HostsFiveItems()
        {
            var plan = Plan(On(4, "s", 1, "p", 3, "q", 5, "r", 7, "t"));
            Assert.AreEqual(1, plan.Steps.Count);
            Assert.AreEqual("big", plan.Steps[0].Recipe.result);
            CollectionAssert.AreEqual(new[] { 4, 1, 3, 5, 7 }, plan.Steps[0].Tiles);
            CollectionAssert.AreEqual(new[] { "s", "p", "q", "r", "t" }, plan.Steps[0].Items);
            Assert.AreEqual("big", plan.Final[4]);
            Assert.AreEqual(1, plan.Final.Count(t => t != null));
        }

        [Test] public void Diagonals_WhenEnabled_CountAsNeighbours()
        {
            var tiles = On(4, "s", 0, "p", 2, "q", 6, "r", 8, "t");
            Assert.AreEqual(0, Plan(tiles).Steps.Count);
            Assert.AreEqual("big", Plan(tiles, diagonals: true).Steps[0].Recipe.result);
        }

        [Test] public void TwoCopies_OfOneItemInARecipe_NeedTwoDistinctTiles()
        {
            var d = TestContent.Combo(); d.recipes = new[] { TestContent.Rec(new[] { "p", "p", "q" }, 0, "x") };
            var db = new ContentDb(d);
            Assert.AreEqual(0, ChainPlanner.Plan(On(4, "p", 3, "q"), db, false).Steps.Count);
            Assert.AreEqual(1, ChainPlanner.Plan(On(4, "p", 1, "p", 3, "q"), db, false).Steps.Count);
        }

        [Test] public void PreEmpted_Combo_Sputters()
        {
            var plan = Plan(On(4, "p", 3, "q", 5, "r", 0, "s"));
            Assert.AreEqual(2, plan.Steps.Count);
            Assert.AreEqual("m", plan.Steps[0].Recipe.result); Assert.AreEqual(0, plan.Steps[0].Hub); Assert.IsFalse(plan.Steps[0].Sputter);
            Assert.IsTrue(plan.Steps[1].Sputter);
            Assert.AreEqual("z", plan.Steps[1].Recipe.result);
            CollectionAssert.AreEquivalent(new[] { 4, 3, 5 }, plan.Steps[1].Tiles);
            Assert.AreEqual("p", plan.Final[4]); Assert.AreEqual("r", plan.Final[5]); Assert.AreEqual("m", plan.Final[0]);
        }

        [Test] public void Pair_Preemption_DoesNotSputter()
        {
            var plan = Plan(On(0, "s", 3, "q", 4, "p"));
            Assert.AreEqual(1, plan.Steps.Count);
            Assert.IsFalse(plan.Steps[0].Sputter);
        }

        [Test] public void NineIdenticalItems_Terminates_NoSteps()
        {
            var tiles = Enumerable.Repeat("p", Bench.TileCount).ToArray();
            Assert.AreEqual(0, Plan(tiles).Steps.Count);
            Assert.AreEqual(0, Plan(tiles, diagonals: true).Steps.Count);
        }

        [Test] public void EmptyAndSingleBench_WhenPlanned_HaveNoSteps()
        {
            Assert.AreEqual(0, Plan(On()).Steps.Count);
            var plan = Plan(On(4, "p"));
            Assert.AreEqual(0, plan.Steps.Count); Assert.AreEqual("p", plan.Final[4]);
        }

        [Test] public void Plan_WithPopulatedBench_DoesNotMutateInput()
        {
            var tiles = On(0, "q", 1, "p");
            Plan(tiles);
            Assert.AreEqual("q", tiles[0]); Assert.AreEqual("p", tiles[1]);
        }

        [Test] public void MatchAt_WhenNothingFits_ReturnsNull()
        {
            Assert.IsNull(ChainPlanner.MatchAt(On(0, "p", 1, "r"), 0, Db(), false));
            Assert.IsNull(ChainPlanner.MatchAt(On(0, "p"), 3, Db(), false));
        }
    }
}
