using System.Linq;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>Rules about the content assets themselves.</summary>
    public class ContentAssetTests
    {
        [Test]
        public void Definitions_EveryAsset_IsNamedAfterItsId()
        {
            // Arrange
            var db = ContentAssets.Database();
            // Act
            var wrong = db.materials.Where(m => m.name != m.id).Select(m => m.name)
                .Concat(db.catalysts.Where(c => c.name != c.id).Select(c => c.name))
                .Concat(db.aspects.Where(a => a.name != a.id).Select(a => a.name))
                .Concat(db.events.Where(e => e.name != e.id).Select(e => e.name))
                .Concat(db.goals.Where(g => g.name != g.id).Select(g => g.name)).ToList();
            // Assert
            CollectionAssert.IsEmpty(wrong);
        }
    }
}
