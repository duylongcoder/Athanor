using System.Collections.Generic;
using System.Linq;
using Athanor.Gameplay;
using NUnit.Framework;

namespace Athanor.Tests
{
    public class SatchelTests
    {
        static readonly string[] Ten = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };

        [Test] public void Draw_SameSeed_SameOrder()
        {
            var x = new Satchel(Ten, 7).Draw(10); var y = new Satchel(Ten, 7).Draw(10);
            CollectionAssert.AreEqual(x, y);
            CollectionAssert.AreEquivalent(Ten, x);
        }

        [Test] public void Draw_DifferentSeed_DifferentOrder()
        {
            CollectionAssert.AreNotEqual(new Satchel(Ten, 1).Draw(10), new Satchel(Ten, 2).Draw(10));
        }

        [Test] public void Draw_ReshufflesDiscard_WhenEmpty()
        {
            var s = new Satchel(new[] { "a", "b", "c" }, 1);
            var first = s.Draw(3); s.Discard(first);
            var second = s.Draw(3);
            CollectionAssert.AreEquivalent(new[] { "a", "b", "c" }, second);
        }

        [Test] public void Draw_ReturnsFewer_WhenNotEnoughCards()
        {
            Assert.AreEqual(3, new Satchel(new[] { "a", "b", "c" }, 1).Draw(5).Count);
        }

        [Test] public void Draw_WhenAllCardsAreAlreadyOut_DoesNotReturnCardsStillOut()
        {
            var s = new Satchel(new[] { "a", "b", "c" }, 1);
            s.Draw(3);
            Assert.AreEqual(0, s.Draw(2).Count);
        }
    }

    public class JournalTests
    {
        [Test] public void Learn_IsUnordered_AndIdempotent()
        {
            var j = new Journal();
            Assert.IsTrue(j.Learn("a", "b")); Assert.IsFalse(j.Learn("b", "a"));
            Assert.IsTrue(j.IsKnown("b", "a")); Assert.IsFalse(j.IsKnown("a", "c"));
            Assert.AreEqual(1, j.KnownPairs.Count);
        }

        [Test] public void HeatHint_NoDuplicates_AndSurvivesSaving()
        {
            var j = new Journal();
            Assert.IsTrue(j.AddHeatHint("p", 17)); Assert.IsFalse(j.AddHeatHint("p", 17));
            Assert.IsTrue(j.AddHeatHint("p", 20)); Assert.IsTrue(j.AddHint("p", Kind.Potion));
            var k = Journal.FromData(j.ToData());
            Assert.AreEqual(3, k.Hints.Count);
            Assert.AreEqual(17, k.Hints[0].heat); Assert.AreEqual("Potion", k.Hints[2].partnerKind); Assert.AreEqual(0, k.Hints[2].heat);
            Assert.IsFalse(k.AddHeatHint("p", 17));
        }

        [Test] public void AddHint_WhenRepeated_AddsNoDuplicates()
        {
            var j = new Journal();
            Assert.IsTrue(j.AddHint("a", Kind.Potion)); Assert.IsFalse(j.AddHint("a", Kind.Potion));
            Assert.IsTrue(j.AddHint("a", Kind.Ore));
            Assert.AreEqual(2, j.Hints.Count);
        }

        [Test] public void Journal_WithAPairAndAHint_RoundTripsThroughData()
        {
            var j = new Journal(); j.Learn("a", "b"); j.AddHint("c", Kind.Beast);
            var k = Journal.FromData(j.ToData());
            Assert.IsTrue(k.IsKnown("a", "b")); Assert.AreEqual(1, k.Hints.Count);
            Assert.AreEqual("Beast", k.Hints[0].partnerKind);
            Assert.IsFalse(k.AddHint("c", Kind.Beast));
        }

        [Test] public void Learn_ComboKey_RoundTripsThroughData()
        {
            var j = new Journal();
            Assert.IsTrue(j.Learn(new[] { "c", "a", "b" })); Assert.IsFalse(j.Learn(new[] { "b", "c", "a" }));
            var k = Journal.FromData(j.ToData());
            Assert.IsTrue(k.IsKnown(new[] { "a", "b", "c" })); Assert.IsFalse(k.IsKnown("a", "b"));
        }

        [Test] public void Phase1_PairKeys_StillLoad()
        {
            var k = Journal.FromData(new JournalData { known = new[] { "deep_tear+ember_coal" } });
            Assert.IsTrue(k.IsKnown("ember_coal", "deep_tear"));
        }

        [Test] public void IsComplete_WithSomeRecipesKnown_IsTrueOnlyWhenAllRecipesWithMaterialKnown()
        {
            var db = new ContentDb(TestContent.Combo());
            var j = new Journal();
            Assert.IsFalse(j.IsComplete("v", db));            // v is only in x+v
            j.Learn("x", "v");
            Assert.IsTrue(j.IsComplete("v", db));
            Assert.IsTrue(j.IsComplete("x", db));             // x is an ingredient of x+v only
            j.Learn("p", "q");
            Assert.IsFalse(j.IsComplete("p", db));            // p is still in z, big and quad
        }

        [Test] public void OpenHints_WhenAMaterialIsComplete_AreHiddenOnceComplete()
        {
            var db = new ContentDb(TestContent.Combo());
            var j = new Journal();
            j.AddHint("v", Kind.Metal); j.AddHint("p", Kind.Potion);
            Assert.AreEqual(1, j.OpenHints("v", db).Count());
            j.Learn("x", "v");
            Assert.AreEqual(0, j.OpenHints("v", db).Count());
            Assert.AreEqual(1, j.OpenHints("p", db).Count());
        }

        [Test] public void FromData_Null_GivesEmpty() => Assert.AreEqual(0, Journal.FromData(null).KnownPairs.Count);

        [Test] public void FromData_NullArraysOrEntries_GivesEmpty()
        {
            var j = Journal.FromData(new JournalData { known = new string[] { null, "" }, hints = new HintData[] { null, new HintData(), new HintData { material = "a", partnerKind = "Nope" } } });
            Assert.AreEqual(0, j.KnownPairs.Count); Assert.AreEqual(0, j.Hints.Count);
            Assert.AreEqual(0, Journal.FromData(new JournalData()).KnownPairs.Count);
        }
    }
}
