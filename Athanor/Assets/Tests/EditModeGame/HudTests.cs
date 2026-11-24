using Athanor.Gameplay;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>The Redraw button is live only while a redraw is left and the bench is in play.</summary>
    public class HudTests
    {
        static ContentDb Db() => new ContentDb(ContentAssets.Data());

        [Test]
        public void CanRedraw_WhileRedrawsRemain_IsTrueUntilTheLastRedrawIsUsed()
        {
            var db = Db();
            var r = new RunState(db, new Journal(), 5);
            Assert.AreEqual(RunPhase.Playing, r.Phase);
            for (int i = db.Rules.redraws; i > 0; i--)
            {
                Assert.IsTrue(HudView.CanRedraw(r));
                Assert.IsTrue(r.Redraw());
            }
            Assert.AreEqual(0, r.RedrawsLeft);
            Assert.IsFalse(HudView.CanRedraw(r));
        }

        [Test]
        public void CanRedraw_IsFalse_OffTheBench_EvenWithRedrawsLeft()
        {
            var db = Db();
            // a full run opens depth 1 on an aspect offer (QuietBefore is 0 unless set)
            var r = new RunState(db, new Journal(), 5, 1, RunOptions.Full(Unlocks.Fresh(db)));
            Assert.AreEqual(RunPhase.AspectOffer, r.Phase);
            Assert.Greater(r.RedrawsLeft, 0);
            Assert.IsFalse(HudView.CanRedraw(r));
        }

        [Test]
        public void CanRedraw_IsFalse_WithNoRun() => Assert.IsFalse(HudView.CanRedraw(null));
    }
}
