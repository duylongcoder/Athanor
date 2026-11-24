using System.IO;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>tools/content/snapshot.json is the assets written out; a content change without a new snapshot fails here.</summary>
    public class ContentSnapshotTests
    {
        [Test]
        public void Snapshot_File_EqualsTheAssets()
        {
            // Arrange
            var path = ContentSnapshot.FilePath;
            Assert.IsTrue(File.Exists(path), path + " is missing: run Athanor > Content > Write Snapshot");
            // Act
            var want = ContentSnapshot.Text();
            // Assert
            Assert.AreEqual(want, File.ReadAllText(path), "the snapshot is stale: run Athanor > Content > Write Snapshot");
        }
    }
}
