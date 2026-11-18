using Athanor.Core;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>The journal and the profile survive a round trip through PlayerPrefs, and a damaged save never stops the game.</summary>
    public class SaveTests
    {
        string before, profileBefore;

        [SetUp] public void Keep()
        {
            before = PlayerPrefs.GetString(Save.Key, null);
            profileBefore = PlayerPrefs.GetString(Save.ProfileKey, null);
        }

        [TearDown]
        public void Restore()
        {
            if (before == null) PlayerPrefs.DeleteKey(Save.Key); else PlayerPrefs.SetString(Save.Key, before);
            if (profileBefore == null) PlayerPrefs.DeleteKey(Save.ProfileKey); else PlayerPrefs.SetString(Save.ProfileKey, profileBefore);
        }

        static ContentDb Db() => new ContentDb(JsonUtility.FromJson<ContentData>(Resources.Load<TextAsset>("content").text));

        [Test]
        public void LoadProfile_WithNothingSaved_IsFresh_WithASeedOfItsOwn()
        {
            PlayerPrefs.DeleteKey(Save.ProfileKey);
            var p = Save.LoadProfile(Db());
            Assert.AreEqual(0, p.RunsStarted); Assert.AreEqual(0, p.Completed.Count); Assert.AreEqual(0, p.UnlockedCatalysts.Count);
            Assert.AreNotEqual(0, p.RewardSeed);
        }

        [Test]
        public void LoadProfile_WithGarbage_IsFresh()
        {
            foreach (var bad in new[] { "{{ not json at all", "{\"completed\":7,\"rewards\":\"none\"}", "[]", "null" })
            {
                PlayerPrefs.SetString(Save.ProfileKey, bad);
                var p = Save.LoadProfile(Db());
                Assert.AreEqual(0, p.Completed.Count, bad); Assert.AreEqual(0, p.Ignites, bad); Assert.AreNotEqual(0, p.RewardSeed, bad);
            }
        }

        [Test]
        public void StoreThenLoadProfile_WithStatsAndCompletedGoals_KeepsStatsGoalsAndUnlocks()
        {
            var db = Db();
            var p = new Profile(321) { RunsStarted = 12, RunsWon = 1, DepthReached = 4, Ignites = 130, ChainLongest = 3, ComboLargest = 4, BestIgnite = 5000000000L };
            var done = p.Evaluate(db, new Journal());
            Assert.That(done.Count, Is.GreaterThan(5));
            Save.StoreProfile(p);
            var q = Save.LoadProfile(db);
            Assert.AreEqual(321, q.RewardSeed); Assert.AreEqual(12, q.RunsStarted); Assert.AreEqual(1, q.RunsWon); Assert.AreEqual(4, q.DepthReached);
            Assert.AreEqual(130, q.Ignites); Assert.AreEqual(3, q.ChainLongest); Assert.AreEqual(4, q.ComboLargest); Assert.AreEqual(5000000000L, q.BestIgnite);
            CollectionAssert.AreEquivalent(p.Completed, q.Completed);
            CollectionAssert.AreEquivalent(p.UnlockedStarters, q.UnlockedStarters);
            CollectionAssert.AreEquivalent(p.UnlockedCatalysts, q.UnlockedCatalysts);
            foreach (var g in done) Assert.AreEqual(g.Item, q.RewardOf(g.Goal.id));
        }

        [Test]
        public void LoadProfile_WithIdsTheContentNoLongerHas_DropsThem()
        {
            PlayerPrefs.SetString(Save.ProfileKey, JsonUtility.ToJson(new ProfileData
            {
                rewardSeed = 5, completed = new[] { "g_depth2", "a_goal_that_was_cut" }, unlockedStarters = new[] { "salt_crystal", "worldseed", "gone" },
                unlockedCatalysts = new[] { "hourglass", "gone" },
            }));
            var p = Save.LoadProfile(Db());
            CollectionAssert.AreEquivalent(new[] { "g_depth2" }, p.Completed);
            CollectionAssert.AreEquivalent(new[] { "salt_crystal" }, p.UnlockedStarters);
            CollectionAssert.AreEquivalent(new[] { "hourglass" }, p.UnlockedCatalysts);
        }

        [Test]
        public void ResetProfile_ErasesTheProfile_AndKeepsTheJournal()
        {
            var journal = new Journal();
            journal.Learn("ember_coal", "deep_tear");
            Save.StoreJournal(journal);
            Save.StoreProfile(new Profile(9) { RunsStarted = 4 });
            Save.ResetProfile();
            Assert.AreEqual(0, Save.LoadProfile(Db()).RunsStarted);
            Assert.IsTrue(Save.LoadJournal().IsKnown("deep_tear", "ember_coal"));
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
        public void StoreThenLoadJournal_WithAPairAndAHint_KeepsRecipesAndHints()
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
        public void StoreThenLoadJournal_WithAThreeItemCombo_KeepsComboRecipes()
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

        [Test]
        public void TutorialDone_DefaultsToFalse_AndRoundTrips()
        {
            bool had = PlayerPrefs.HasKey(Save.TutorialKey);
            int kept = PlayerPrefs.GetInt(Save.TutorialKey, 0);
            try
            {
                PlayerPrefs.DeleteKey(Save.TutorialKey);
                Assert.IsFalse(Save.TutorialDone);
                Save.TutorialDone = true;
                Assert.IsTrue(Save.TutorialDone);
            }
            finally
            {
                if (had) PlayerPrefs.SetInt(Save.TutorialKey, kept); else PlayerPrefs.DeleteKey(Save.TutorialKey);
                PlayerPrefs.Save();
            }
        }
    }
}
