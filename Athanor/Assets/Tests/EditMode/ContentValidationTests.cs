using Athanor.Gameplay;
using NUnit.Framework;

namespace Athanor.Tests
{
    public class ContentValidationTests
    {
        [Test]
        public void Check_TheGamesContent_IsValid()
        {
            // Arrange
            var db = ContentAssets.Database();
            // Act
            var problem = ContentValidation.Check(db);
            // Assert
            Assert.IsNull(problem, problem);
        }

        [Test]
        public void Check_NoRules_SaysSo()
        {
            // Arrange
            var db = UnityEngine.ScriptableObject.CreateInstance<ContentDatabase>();
            // Act
            var problem = ContentValidation.Check(db);
            // Assert
            StringAssert.Contains("rules", problem);
            UnityEngine.Object.DestroyImmediate(db);
        }
    }
}
