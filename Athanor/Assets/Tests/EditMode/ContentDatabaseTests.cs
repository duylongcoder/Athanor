using System.Collections.Generic;
using Athanor.Gameplay;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>ContentDatabase built in memory hands the rules the plain data they read.</summary>
    public class ContentDatabaseTests
    {
        readonly List<Object> made = new List<Object>();
        T Make<T>() where T : ScriptableObject { var o = ScriptableObject.CreateInstance<T>(); made.Add(o); return o; }
        [TearDown] public void Clean() { foreach (var o in made) Object.DestroyImmediate(o); made.Clear(); }

        MaterialDefinition Mat(string id, Kind kind, int tier, int ri)
        {
            var m = Make<MaterialDefinition>();
            m.id = id; m.displayName = id.ToUpperInvariant(); m.kind = kind; m.tier = tier; m.reactivity = ri; m.glow = new Color32(0xff, 0x6a, 0x1e, 0xff);
            return m;
        }

        ContentDatabase Tiny()
        {
            var a = Mat("a", Kind.Ore, 0, 5); var b = Mat("b", Kind.Ore, 0, 5); var x = Mat("x", Kind.Metal, 1, 5);
            var r = Make<RecipeDefinition>(); r.items = new[] { a, b }; r.result = x;
            var rules = Make<GameRules>();
            rules.handMin = 3; rules.handBase = 5; rules.trials = 4; rules.trialsFinalDepth = 6; rules.redraws = 2; rules.starterCopies = 2;
            rules.offerSize = 3; rules.rackSize = 12; rules.pouchCards = 1; rules.maxRi = 13; rules.firstReactionMult = 2;
            rules.aspectDepths = new[] { 1, 3, 5 }; rules.potencyByTier = new long[] { 0, 20, 60, 180, 500, 1500 };
            rules.depthTargets = new[] { 300, 1500, 7500, 37500 }; rules.depthNames = new[] { "1", "2", "3", "4", "5" }; rules.ultimate = x;
            var db = Make<ContentDatabase>();
            db.rules = rules; db.materials.AddRange(new[] { a, b, x }); db.recipes.Add(r);
            return db;
        }

        [Test]
        public void ToContentData_TinyDatabase_BuildsAValidContentDb()
        {
            // Arrange
            var db = Tiny();
            // Act
            var content = new ContentDb(db.ToContentData());
            // Assert
            Assert.AreEqual(3, content.Materials.Count);
            Assert.AreEqual("x", content.Ultimate);
            Assert.IsTrue(content.TryCombo(new[] { "a", "b" }, out var recipe));
            Assert.AreEqual("x", recipe.result);
            Assert.AreEqual(4, content.Rules.trials);
            Assert.AreEqual(5, content.FinalDepth);
        }

        [Test]
        public void ToContentData_Material_WritesTheStringsTheRulesRead()
        {
            // Arrange
            var db = Tiny();
            // Act
            var m = db.ToContentData().materials[0];
            // Assert
            Assert.AreEqual("a", m.id); Assert.AreEqual("A", m.name); Assert.AreEqual("Ore", m.kind);
            Assert.AreEqual("#ff6a1e", m.glow); Assert.AreEqual("", m.rarity); Assert.IsFalse(m.locked);
        }

        [Test]
        public void ToContentData_MaterialWithoutGlow_WritesAnEmptyGlowString()
        {
            // Arrange
            var db = Tiny();
            db.materials[0].hasGlow = false;
            // Act
            var data = db.ToContentData();
            // Assert
            Assert.AreEqual("", data.materials[0].glow);
            Assert.AreEqual("#ff6a1e", data.materials[1].glow);
        }

        [Test]
        public void ToContentData_NoRarityWeights_IsNullSoTheDrawStaysUniform()
        {
            // Arrange
            var db = Tiny();
            // Act
            var data = db.ToContentData();
            // Assert
            Assert.IsNull(data.rarityWeights);
            Assert.IsTrue(new ContentDb(data).UniformRarity);
        }

        [Test]
        public void ToContentData_RecipeWithAMissingItem_IsRejectedByContentDb()
        {
            // Arrange
            var db = Tiny();
            db.recipes[0].items = new MaterialDefinition[] { db.materials[0], null };
            // Act
            var data = db.ToContentData();
            // Assert
            Assert.Throws<System.ArgumentException>(() => new ContentDb(data));
        }

        [Test]
        public void ToContentData_CalledTwice_ReturnsIndependentCopies()
        {
            // Arrange
            var db = Tiny();
            // Act
            var first = db.ToContentData(); first.depthTargets[0] = 1; first.rules.potencyByTier[1] = 0;
            var second = db.ToContentData();
            // Assert
            Assert.AreEqual(300, second.depthTargets[0]);
            Assert.AreEqual(20, second.rules.potencyByTier[1]);
        }
    }
}
