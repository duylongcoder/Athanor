using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>Phase 4: the hooks the tutorial needs, and the tutorial script replayed with every number it shows.</summary>
    public class TutorialTests
    {
        ContentDb db;
        [SetUp] public void Load() => db = new ContentDb(TestContent.Load());

        // ---- satchel ------------------------------------------------------------------------------

        [Test]
        public void Satchel_Take_PullsANamedCard_FromDrawOrDiscard()
        {
            var s = new Satchel(new[] { "a", "b", "b" }, 1);
            Assert.AreEqual(2, s.Available("b"));
            Assert.IsTrue(s.Take("b"));
            Assert.AreEqual(1, s.Available("b"));
            Assert.AreEqual(3, s.Total);                 // a card that is out still belongs to the satchel
            s.Discard(new[] { "b" });
            Assert.AreEqual(2, s.Available("b"));        // on the discard pile it can be taken again
            Assert.IsTrue(s.Take("b"));
            Assert.IsTrue(s.Take("b"));
            Assert.IsFalse(s.Take("b"));
            Assert.IsFalse(s.Take("zzz"));
        }
    }
}
