using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    public class TutorialAreaTests
    {
        [Test]
        public void Grow_EachMarginSide_AddsSeparately()
        {
            var r = TutorialRect.Grow(new Rect(10, 20, 30, 40), new Vector4(1, 2, 3, 4));   // left, top, right, bottom
            Assert.AreEqual(new Rect(9, 18, 34, 46), r);
        }

        [Test]
        public void ArtRect_OfWorldCentre_IsTheCentredArtRect()
        {
            // world (0,0) is the screen centre: art (320,180); a 20x10 area centred there has its top-left at (310,175)
            Assert.AreEqual(new Rect(310, 175, 20, 10), TutorialRect.ArtRect(Vector2.zero, new Vector2(20, 10)));
        }
    }
}
