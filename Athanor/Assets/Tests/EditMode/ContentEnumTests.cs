using System;
using System.Linq;
using Athanor.Gameplay;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>The Inspector enums of the content assets spell exactly the names the rules read.</summary>
    public class ContentEnumTests
    {
        static string[] Names<T>() where T : Enum => Enum.GetNames(typeof(T));

        [Test]
        public void CatalystEffect_Names_EqualEffectsAll()
        {
            // Arrange
            var want = Effects.All;
            // Act
            var got = Names<CatalystEffect>();
            // Assert
            CollectionAssert.AreEqual(want, got);
        }

        [Test]
        public void AspectEffect_Names_EqualAspectEffectsAll()
        {
            // Arrange
            var want = AspectEffects.All;
            // Act
            var got = Names<AspectEffect>();
            // Assert
            CollectionAssert.AreEqual(want, got);
        }

        [Test]
        public void EventOp_Names_EqualOpsAll()
        {
            // Arrange
            var want = Ops.All;
            // Act
            var got = Names<EventOp>();
            // Assert
            CollectionAssert.AreEqual(want, got);
        }

        [Test]
        public void EventKind_Names_EqualOpsTypes()
        {
            // Arrange
            var want = Ops.Types;
            // Act
            var got = Names<EventKind>();
            // Assert
            CollectionAssert.AreEqual(want, got);
        }

        [Test]
        public void EventNeed_NamesAfterNone_EqualOpsNeeds()
        {
            // Arrange
            var want = Ops.Needs;
            // Act
            var got = Names<EventNeed>().Skip(1).ToArray();
            // Assert
            CollectionAssert.AreEqual(want, got);
        }

        [Test]
        public void RarityId_NamesAfterUnset_EqualRaritiesAll()
        {
            // Arrange
            var want = Rarities.All;
            // Act
            var got = Names<RarityId>().Skip(1).ToArray();
            // Assert
            CollectionAssert.AreEqual(want, got);
        }

        [Test]
        public void KindOrNone_NamesAfterNone_EqualKind()
        {
            // Arrange
            var want = Names<Kind>();
            // Act
            var got = Names<KindOrNone>().Skip(1).ToArray();
            // Assert
            CollectionAssert.AreEqual(want, got);
        }

        [Test]
        public void AspectScopeAndGroup_Names_AreTheStringsTheRulesCompare()
        {
            // Arrange
            var scopes = new[] { "Tile", "Link", "Bench" };
            var groups = new[] { "Cheap", "Engine" };
            // Act
            var gotScopes = Names<AspectScope>();
            var gotGroups = Names<AspectGroup>();
            // Assert
            CollectionAssert.AreEqual(scopes, gotScopes);
            CollectionAssert.AreEqual(groups, gotGroups);
        }

        [Test]
        public void GoalStat_EveryValue_MapsToItsStatId()
        {
            // Arrange
            var values = (GoalStat[])Enum.GetValues(typeof(GoalStat));
            // Act
            var mapped = values.Select(ContentNames.Stat).ToArray();
            // Assert
            CollectionAssert.AreEqual(StatIds.All, mapped);
            foreach (var id in StatIds.All) Assert.AreEqual(id, ContentNames.Stat(ContentNames.ParseStat(id)));
        }

        [Test]
        public void ContentNames_NoneValues_WriteEmptyStrings()
        {
            // Arrange
            var unset = RarityId.Unset;
            // Act
            var rarity = ContentNames.Rarity(unset);
            // Assert
            Assert.AreEqual("", rarity);
            Assert.AreEqual("", ContentNames.Kind(KindOrNone.None));
            Assert.AreEqual("", ContentNames.Need(EventNeed.None));
            Assert.AreEqual(RarityId.Unset, ContentNames.Parse("", RarityId.Unset));
            Assert.AreEqual(RarityId.Rare, ContentNames.Parse("Rare", RarityId.Unset));
            Assert.AreEqual("starter", ContentNames.Reward(RewardKind.Starter));
            Assert.AreEqual(RewardKind.Catalyst, ContentNames.ParseReward("catalyst"));
        }
    }
}
