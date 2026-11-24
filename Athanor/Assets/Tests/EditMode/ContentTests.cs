using System;
using System.Collections.Generic;
using System.Linq;
using Athanor.Gameplay;
using NUnit.Framework;

namespace Athanor.Tests
{
    public class ContentTests
    {
        ContentData data; ContentDb db;
        [SetUp] public void Load() { data = TestContent.Load(); db = new ContentDb(data); }

        [Test] public void Starters_InTheLoadedContent_Number13With8OfThemOpen()
        {
            Assert.AreEqual(13, db.Starters.Count);
            Assert.AreEqual(8, db.Starters.Count(m => !m.locked));
        }

        [Test] public void Kinds_AcrossAllMaterials_Number7AndEachIsUsed()
        {
            var used = data.materials.Select(m => db.KindOf(m.id)).Distinct().Count();
            Assert.AreEqual(7, used);
        }

        [Test] public void Discoveries_43FromPhase2_24FromPhase3()
        {
            var n = data.materials.Count(m => m.tier >= 1 && m.tier <= 4);
            Assert.AreEqual(67, n);
        }

        [Test] public void EveryMaterialFitsTheJournalGrid() =>
            Assert.That(data.materials.Length, Is.LessThanOrEqualTo(120));   // JournalView: two pages of 10 columns x 6 rows

        [Test] public void ComboRecipes_ByItemCount_FallInTheExpectedRanges()
        {
            Assert.That(db.Recipes.Count(r => r.items.Length == 3), Is.InRange(15, 19), "three-item");
            Assert.That(db.Recipes.Count(r => r.items.Length == 4), Is.InRange(5, 8), "four-item");
            Assert.That(db.Recipes.Count(r => r.items.Length == 5), Is.InRange(2, 4), "five-item");
        }

        [Test] public void Ultimate_TracedBackThroughRecipes_IsReachableViaAtLeastOneCombo()
        {
            var seen = new HashSet<string> { db.Ultimate };
            var queue = new Queue<string>(seen);
            bool viaCombo = false;
            while (queue.Count > 0)
            {
                var m = queue.Dequeue();
                foreach (var r in db.Recipes.Where(r => r.result == m))
                {
                    if (r.items.Length >= 3) viaCombo = true;
                    foreach (var i in r.items) if (seen.Add(i)) queue.Enqueue(i);
                }
            }
            Assert.IsTrue(viaCombo);
        }

        [Test] public void EveryDiscovery_ExceptKnownDeadEnds_IsUsedOrIsTheUltimate()
        {
            // the only dead ends are the four tier-4 results of the Phase 3 side branch
            var deadEnds = new[] { "thunder_heart", "ossuary_crown", "storm_philter", "copper_sun" };
            foreach (var m in data.materials.Where(m => m.tier >= 1 && m.id != db.Ultimate))
                Assert.AreEqual(deadEnds.Contains(m.id), db.RecipesContaining(m.id).Count == 0, m.id);
        }

        [Test] public void Catalysts_InTheLoadedContent_Number26With9OfThemOpen()
        {
            Assert.AreEqual(26, db.Catalysts.Count);
            Assert.AreEqual(9, db.Catalysts.Count(c => !c.locked));
        }

        [Test] public void DepthTargetsAre_300_1500_7500_37500()
        {
            Assert.AreEqual(300, db.TargetFor(1)); Assert.AreEqual(1500, db.TargetFor(2));
            Assert.AreEqual(7500, db.TargetFor(3)); Assert.AreEqual(37500, db.TargetFor(4));
            Assert.AreEqual(0, db.TargetFor(5));
        }

        [Test] public void EveryRecipe_InTheLoadedContent_UsesItemsForm()
        {
            foreach (var r in TestContent.Load().recipes)
            {
                Assert.IsNotNull(r.items, r.result); Assert.IsNull(r.a, r.result); Assert.IsNull(r.b, r.result);
            }
        }

        /// <summary>
        /// Some bench fires this recipe first: it laid round the centre, with nothing or with starting
        /// materials (at most two of each, as the satchel holds) on the tiles left over.
        /// </summary>
        bool CanFire(RecipeData r)
        {
            int[] around = { 1, 3, 5, 7 };
            var starters = db.Starters.Select(m => m.id).ToList();
            foreach (var hubItem in r.items.Distinct())
            {
                var others = r.items.ToList(); others.Remove(hubItem);
                var tiles = new string[Bench.TileCount];
                tiles[Bench.Centre] = hubItem;
                for (int k = 0; k < others.Count; k++) tiles[around[k]] = others[k];
                if (Fills(r, tiles, around, others.Count, starters)) return true;
            }
            return false;
        }

        bool Fills(RecipeData r, string[] tiles, int[] around, int slot, List<string> starters)
        {
            if (slot == around.Length)
            {
                var plan = ChainPlanner.Plan(tiles, db, false);
                return plan.Steps.Count > 0 && plan.Steps[0].Recipe == r && !plan.Steps[0].Sputter;
            }
            if (Fills(r, tiles, around, slot + 1, starters)) return true;
            foreach (var s in starters)
            {
                if (tiles.Count(t => t == s) >= db.Rules.starterCopies) continue;
                tiles[around[slot]] = s;
                bool ok = Fills(r, tiles, around, slot + 1, starters);
                tiles[around[slot]] = null;
                if (ok) return true;
            }
            return false;
        }

        [Test] public void EveryRecipe_CanFire_WithStartersForCompany()
        {
            foreach (var r in db.Recipes) Assert.IsTrue(CanFire(r), db.KeyOf(r) + " can never fire");
        }

        [Test] public void RequiredTotals_Bite_FromTierTwoUp()
        {
            foreach (var r in db.Recipes.Where(r => db.Material(r.result).tier == 1))
                Assert.AreEqual(0, db.Company(r), db.KeyOf(r) + ": a first discovery needs no company");
            for (int tier = 2; tier <= 5; tier++)
                Assert.That(db.Recipes.Count(r => db.Material(r.result).tier == tier && db.Company(r) > 0), Is.GreaterThanOrEqualTo(2),
                    "tier " + tier + " has no recipe that needs company");
        }

        [Test] public void Company_ForARecipeAskingMoreThanItsOwnHeat_IsWhatItLacksOnItsOwn()
        {
            var c = new ContentDb(TestContent.Combo(19));
            Assert.IsTrue(c.TryCombo(new[] { "p", "q", "r" }, out var z));
            Assert.AreEqual(4, c.Company(z));   // 19 asked, 15 of its own
            Assert.IsTrue(c.TryCombo(new[] { "p", "q" }, out var x));
            Assert.AreEqual(0, c.Company(x));   // 10 asked, 13 of its own
        }

        [Test] public void Constructor_Throws_WhenNoCompanyCouldEverBeEnough()
        {
            // five materials fill the hub and all four tiles round it: nothing else can help
            Assert.Throws<ArgumentException>(() => new ContentDb(With(TestContent.Rec(new[] { "s", "p", "q", "r", "t" }, 29, "big"))));
            Assert.DoesNotThrow(() => new ContentDb(With(TestContent.Rec(new[] { "s", "p", "q", "r", "t" }, 28, "big"))));
            // four leave one tile free: at most one material of Reaction Index 10
            Assert.DoesNotThrow(() => new ContentDb(With(TestContent.Rec(new[] { "s", "p", "q", "t" }, 36, "quad"))));
            Assert.Throws<ArgumentException>(() => new ContentDb(With(TestContent.Rec(new[] { "s", "p", "q", "t" }, 37, "quad"))));
        }

        [Test] public void BasePotency_ForTiersOneToFive_MatchesTheTable()
        {
            Assert.AreEqual(20, db.BasePotency(1)); Assert.AreEqual(60, db.BasePotency(2));
            Assert.AreEqual(180, db.BasePotency(3)); Assert.AreEqual(500, db.BasePotency(4));
            Assert.AreEqual(1500, db.BasePotency(5));
        }

        [Test] public void TryReact_WithThePairInEitherOrder_GivesTheSameResult()
        {
            Assert.IsTrue(db.TryReact("ember_coal", "deep_tear", out var r1));
            Assert.IsTrue(db.TryReact("deep_tear", "ember_coal", out var r2));
            Assert.AreEqual("steam_wisp", r1); Assert.AreEqual(r1, r2);
        }

        [Test] public void TryReact_False_ForUnknownPair()
        {
            Assert.IsFalse(db.TryReact("ember_coal", "ember_coal", out var r));
            Assert.IsNull(r);
        }

        [Test] public void EveryRecipe_ResultTier_IsMaxInputTierPlus1()
        {
            foreach (var r in db.Recipes)
            {
                var want = r.items.Max(i => db.Material(i).tier) + 1;
                Assert.AreEqual(want, db.Material(r.result).tier, db.KeyOf(r));
            }
        }

        [Test] public void Ultimate_AndItsRecipes_IsTier5FromTier4Inputs()
        {
            Assert.AreEqual(5, db.Material(db.Ultimate).tier);
            foreach (var r in db.Recipes.Where(r => r.result == db.Ultimate))
                foreach (var i in r.items) Assert.AreEqual(4, db.Material(i).tier);
        }

        [Test] public void EveryMaterial_FollowingRecipesFromStarters_IsReachable()
        {
            var have = new HashSet<string>(db.Starters.Select(m => m.id));
            bool grew = true;
            while (grew)
            {
                grew = false;
                foreach (var r in db.Recipes)
                    if (r.items.All(have.Contains) && have.Add(r.result)) grew = true;
            }
            var missing = data.materials.Select(m => m.id).Where(id => !have.Contains(id)).ToList();
            Assert.IsEmpty(missing);
        }

        [Test] public void EveryStarter_CountingPartners_HasAtLeast3()
        {
            foreach (var s in db.Starters) Assert.That(db.PartnersOf(s.id).Count(), Is.GreaterThanOrEqualTo(3), s.id);
        }

        [Test] public void Constructor_Throws_OnUnknownMaterialInRecipe()
        {
            var d = TestContent.Tiny(); d.recipes = new[] { TestContent.Rec("a", "nope", "x") };
            Assert.Throws<ArgumentException>(() => new ContentDb(d));
        }

        [Test] public void Constructor_Throws_OnDuplicatePair()
        {
            var d = TestContent.Tiny(); d.recipes = new[] { TestContent.Rec("a", "b", "x"), TestContent.Rec("b", "a", "y") };
            Assert.Throws<ArgumentException>(() => new ContentDb(d));
        }

        [Test] public void Recipe_ThreeItems_LookedUpUnordered()
        {
            var c = new ContentDb(TestContent.Combo());
            Assert.IsTrue(c.TryCombo(new[] { "r", "p", "q" }, out var rec));
            Assert.AreEqual("z", rec.result);
            Assert.IsFalse(c.TryCombo(new[] { "p", "q", "q" }, out var none));
            Assert.IsNull(none);
        }

        [Test] public void Legacy_AB_Recipe_FoldedIntoItems()
        {
            var c = new ContentDb(TestContent.Tiny());
            CollectionAssert.AreEquivalent(new[] { "a", "b" }, c.Recipes[0].items);
            Assert.IsTrue(c.TryReact("b", "a", out var r)); Assert.AreEqual("x", r);
        }

        [Test] public void RecipesContaining_AnItemInSeveralRecipes_ListsLargestFirst()
        {
            var c = new ContentDb(TestContent.Combo());
            var results = c.RecipesContaining("p").Select(r => r.result).ToList();
            CollectionAssert.AreEqual(new[] { "big", "quad", "z", "x" }, results);
        }

        [Test] public void Reactivity_OfAnItemAndAThreeItemCombo_GivesItemAndTotalValues()
        {
            var c = new ContentDb(TestContent.Combo());
            Assert.AreEqual(8, c.Reactivity("p"));
            Assert.IsTrue(c.TryCombo(new[] { "p", "q", "r" }, out var z));
            Assert.AreEqual(15, c.TotalReactivity(z));
            Assert.AreEqual("p+q+r", c.KeyOf(z));
        }

        [Test] public void PartnersOf_AnItemInCombos_CoversComboMembers()
        {
            var c = new ContentDb(TestContent.Combo());
            CollectionAssert.AreEquivalent(new[] { "p", "q", "s", "t" }, c.PartnersOf("r").ToList());
        }

        static ContentData With(params RecipeData[] recipes) { var d = TestContent.Combo(); d.recipes = recipes; return d; }

        [Test] public void Constructor_Throws_On6Items() =>
            Assert.Throws<ArgumentException>(() => new ContentDb(With(TestContent.Rec(new[] { "p", "q", "r", "s", "t", "v" }, 0, "big"))));

        [Test] public void Constructor_Throws_On1Item() =>
            Assert.Throws<ArgumentException>(() => new ContentDb(With(TestContent.Rec(new[] { "p" }, 0, "x"))));

        [Test] public void Constructor_Throws_OnThreeCopies() =>
            Assert.Throws<ArgumentException>(() => new ContentDb(With(TestContent.Rec(new[] { "p", "p", "p" }, 0, "x"))));

        [Test] public void Constructor_Allows_TwoCopies() =>
            Assert.DoesNotThrow(() => new ContentDb(With(TestContent.Rec(new[] { "p", "p", "q" }, 0, "x"))));

        [Test] public void Constructor_Throws_OnDuplicateItemSet() =>
            Assert.Throws<ArgumentException>(() => new ContentDb(With(
                TestContent.Rec(new[] { "p", "q", "r" }, 0, "z"), TestContent.Rec(new[] { "r", "q", "p" }, 0, "x"))));

        [Test] public void Constructor_Throws_OnReactivityMissingOrOutOf1To10()
        {
            var d = TestContent.Combo(); d.materials[0].reactivity = 0;
            Assert.Throws<ArgumentException>(() => new ContentDb(d));
            d = TestContent.Combo(); d.materials[0].reactivity = 11;
            Assert.Throws<ArgumentException>(() => new ContentDb(d));
        }

        [Test] public void Constructor_Throws_OnNegativeMinReactivity() =>
            Assert.Throws<ArgumentException>(() => new ContentDb(With(TestContent.Rec(new[] { "p", "q" }, -1, "x"))));

        [Test] public void Reactivity_OfMaterialsInEachTier_HasBothLowAndHighValues()
        {
            for (int tier = 0; tier <= 4; tier++)
            {
                var ri = data.materials.Where(m => m.tier == tier).Select(m => m.reactivity).ToList();
                Assert.That(ri.Min(), Is.LessThanOrEqualTo(3), "tier " + tier + " has no low value");
                Assert.That(ri.Max(), Is.GreaterThanOrEqualTo(8), "tier " + tier + " has no high value");
            }
        }

        [Test] public void Reactivity_OfBestStarterAndWorstTier4_IsNotMonotonicInTier()
        {
            int bestStarter = data.materials.Where(m => m.tier == 0).Max(m => m.reactivity);
            int worstTier4 = data.materials.Where(m => m.tier == 4).Min(m => m.reactivity);
            Assert.That(bestStarter, Is.GreaterThan(worstTier4));
        }

        [Test] public void ComboKey_IsOrderIndependent() =>
            Assert.AreEqual("a+b+c", ContentDb.ComboKey(new[] { "c", "a", "b" }));

        [Test] public void PairKey_IsOrderIndependent() => Assert.AreEqual(ContentDb.PairKey("b", "a"), ContentDb.PairKey("a", "b"));

        [Test]
        public void EventTexts_OfEveryEvent_FitTheEventWindow()
        {
            foreach (var e in data.events)
            {
                Assert.LessOrEqual(e.name.Length, 22, e.id + " name");
                Assert.LessOrEqual(e.text.Length, 56, e.id + " text");
                foreach (var o in e.options)
                    foreach (var x in o.outcomes) Assert.LessOrEqual(x.text.Length, 52, e.id + " outcome");
            }
        }

        [Test]
        public void LoreEvents_DoNothing_AndTellTheStoryInOrder()
        {
            var lore = data.events.Where(e => e.type == "Lore").ToList();
            CollectionAssert.AreEquivalent(new[] { "quiet_room", "names_in_wall", "warm_roots", "pinned_note" }, lore.Select(e => e.id));
            foreach (var e in lore)
            {
                Assert.AreEqual(1, e.options.Length, e.id);
                Assert.IsTrue(e.options[0].outcomes.All(x => x.gain.All(g => g.op == Ops.Nothing)), e.id);
            }
            var byId = lore.ToDictionary(e => e.id);
            Assert.Less(byId["names_in_wall"].maxDepth, byId["warm_roots"].minDepth);
            Assert.Less(byId["warm_roots"].maxDepth, byId["pinned_note"].minDepth);
        }
    }
}
