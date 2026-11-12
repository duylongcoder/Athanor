using Athanor.Core;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>The journal survives a round trip through PlayerPrefs, and a damaged save never stops the game.</summary>
    public class SaveTests
    {
        string before;

        [SetUp] public void Keep() => before = PlayerPrefs.GetString(Save.Key, null);

        [TearDown]
        public void Restore()
        {
            if (before == null) PlayerPrefs.DeleteKey(Save.Key); else PlayerPrefs.SetString(Save.Key, before);
        }

        [Test]
        public void LoadJournal_WithGarbageString_ReturnsEmpty()
        {
            PlayerPrefs.SetString(Save.Key, "{{ not json at all");
            var journal = Save.LoadJournal();
            Assert.AreEqual(0, journal.KnownPairs.Count);
            Assert.AreEqual(0, journal.Hints.Count);
        }

        [Test]
        public void LoadJournal_WithOlderShape_ReturnsEmpty()
        {
            PlayerPrefs.SetString(Save.Key, "{\"known\":7,\"hints\":\"none\",\"version\":0}");
            var journal = Save.LoadJournal();
            Assert.AreEqual(0, journal.KnownPairs.Count);
            Assert.AreEqual(0, journal.Hints.Count);
        }

        [Test]
        public void LoadJournal_WithNothingSaved_ReturnsEmpty()
        {
            PlayerPrefs.DeleteKey(Save.Key);
            Assert.AreEqual(0, Save.LoadJournal().KnownPairs.Count);
        }

        [Test]
        public void StoreThenLoad_KeepsRecipesAndHints()
        {
            var journal = new Journal();
            journal.Learn("ember_coal", "deep_tear");
            journal.AddHint("iron_ore", Kind.Potion);
            Save.StoreJournal(journal);
            var loaded = Save.LoadJournal();
            Assert.IsTrue(loaded.IsKnown("deep_tear", "ember_coal"));
            Assert.IsTrue(loaded.HasHint("iron_ore", Kind.Potion));
        }

        [Test]
        public void StoreThenLoad_KeepsComboRecipes()
        {
            var journal = new Journal();
            journal.Learn(new[] { "sulfur", "ember_coal", "iron_ore" });
            Save.StoreJournal(journal);
            Assert.IsTrue(Save.LoadJournal().IsKnown(new[] { "ember_coal", "iron_ore", "sulfur" }));
        }

        [Test]
        public void LoadJournal_Phase1PairKeys_StillLoad()
        {
            PlayerPrefs.SetString(Save.Key, JsonUtility.ToJson(new JournalData { known = new[] { "deep_tear+ember_coal" } }));
            Assert.IsTrue(Save.LoadJournal().IsKnown("ember_coal", "deep_tear"));
        }
    }
}
