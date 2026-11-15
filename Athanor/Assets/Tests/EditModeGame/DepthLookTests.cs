using NUnit.Framework;

namespace Athanor.Game.Tests
{
    public class DepthLookTests
    {
        [TestCase(1, "depth.1.intro")]
        [TestCase(3, "depth.3.intro")]
        [TestCase(5, "depth.5.intro")]
        public void IntroKey_IsTheDepthsKey(int depth, string key) => Assert.AreEqual(key, DepthLook.IntroKey(depth));

        [TestCase(0)]
        [TestCase(6)]
        [TestCase(12)]
        public void IntroKey_NoneOutsideTheAuthoredDepths(int depth) => Assert.IsNull(DepthLook.IntroKey(depth));

        [TestCase(1, 1)] [TestCase(2, 2)] [TestCase(3, 3)] [TestCase(4, 4)] [TestCase(5, 5)]
        public void View_EachDepthHasItsOwn(int depth, int view) => Assert.AreEqual(view, DepthLook.View(depth));

        [TestCase(6)] [TestCase(9)] [TestCase(100)]
        public void View_DeeperThanFive_KeepsTheLast(int depth)
        {
            Assert.AreEqual(5, DepthLook.View(depth));
            Assert.AreEqual("window_d5", DepthLook.WindowSprite(depth));
        }

        [TestCase(0)] [TestCase(-1)]
        public void View_BelowOne_IsTheFirst(int depth) => Assert.AreEqual("window_d1", DepthLook.WindowSprite(depth));

        [Test]
        public void WindowSprite_AnyDepth_NamesTheArtSprite()
        {
            for (int d = 1; d <= DepthLook.Authored; d++) Assert.AreEqual("window_d" + d, DepthLook.WindowSprite(d));
        }

        [Test]
        public void ShowsRunIntro_OnlyDepthOneOfANormalRun_IsTrue()
        {
            Assert.IsTrue(DepthLook.ShowsRunIntro(1, false, false));
            Assert.IsFalse(DepthLook.ShowsRunIntro(1, true, false), "first-run tutorial");
            Assert.IsFalse(DepthLook.ShowsRunIntro(1, false, true), "How to play");
            Assert.IsFalse(DepthLook.ShowsRunIntro(1, true, true));
            for (int d = 2; d <= 7; d++) Assert.IsFalse(DepthLook.ShowsRunIntro(d, false, false), "later depths come by the descent: " + d);
            Assert.IsFalse(DepthLook.ShowsRunIntro(0, false, false));
        }

        [Test]
        public void ThemedWindow_OutsideANormalRun_IsFalse()
        {
            Assert.IsTrue(DepthLook.ThemedWindow(true, false, false));
            Assert.IsFalse(DepthLook.ThemedWindow(false, false, false), "title");
            Assert.IsFalse(DepthLook.ThemedWindow(true, true, false), "tutorial");
            Assert.IsFalse(DepthLook.ThemedWindow(true, false, true), "How to play");
        }
    }
}
