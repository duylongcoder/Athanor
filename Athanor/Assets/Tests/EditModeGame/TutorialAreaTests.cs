using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    public class TutorialAreaTests
    {
        readonly System.Collections.Generic.List<GameObject> made = new System.Collections.Generic.List<GameObject>();

        [TearDown] public void Clean() { foreach (var g in made) if (g != null) Object.DestroyImmediate(g); made.Clear(); }

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

        [Test]
        public void Fixed_Area_ReadsItsTransform()
        {
            var go = new GameObject("area"); made.Add(go);
            {
                go.transform.position = Px.At(100, 50);   // the art point (100,50) in world space is the area's centre
                var a = go.AddComponent<TutorialArea>();
                a.kind = AreaKind.Fixed; a.size = new Vector2(8, 8);
                var r = a.FixedArtRect();
                Assert.AreEqual(96f, r.x, 0.001f); Assert.AreEqual(46f, r.y, 0.001f);
                Assert.AreEqual(8f, r.width, 0.001f); Assert.AreEqual(8f, r.height, 0.001f);
            }

        }

        [Test]
        public void Step_Lists_Its_Areas_In_Child_Order()
        {
            var root = new GameObject("step"); made.Add(root);
            {
                var s = root.AddComponent<TutorialStep>();
                var a = new GameObject("a").transform; a.SetParent(root.transform); a.gameObject.AddComponent<TutorialArea>();
                var b = new GameObject("b").transform; b.SetParent(root.transform); b.gameObject.AddComponent<TutorialArea>();
                CollectionAssert.AreEqual(new[] { a.GetComponent<TutorialArea>(), b.GetComponent<TutorialArea>() }, s.Areas());
            }

        }
    }
}
