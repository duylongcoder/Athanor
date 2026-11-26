using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>
    /// The dev-only hooks tools/web/mainhd2d.mjs drives (5b-07b): F6 mood steps, F10 tile faces in UI units, the bench's
    /// place / unplace lines. The key handling is guarded by Application.isEditor || Debug.isDebugBuild and the log calls are Dev.Hook.
    /// </summary>
    public class MainDevHookTests
    {
        [Test]
        public void NextMood_StepsOneToFive_AndWraps()
        {
            Assert.AreEqual(2, GameManager.NextMood(1));
            Assert.AreEqual(5, GameManager.NextMood(4));
            Assert.AreEqual(1, GameManager.NextMood(5));
            Assert.AreEqual(1, GameManager.NextMood(0));
        }

        [Test]
        public void HookLines_ForEachHook_MatchTheScriptPatterns()
        {
            Assert.AreEqual("[dev] mood 3", GameManager.MoodLine(3));
            Assert.AreEqual("[dev] effects on", GameManager.EffectsLine(true));
            Assert.AreEqual("[dev] effects off", GameManager.EffectsLine(false));
            Assert.AreEqual("[bench] place 4", BenchView.PlaceLine(4));
            Assert.AreEqual("[bench] unplace 7", BenchView.UnplaceLine(7));
        }

        [Test]
        public void DrawnFace_AtRestView_IsTheInsetFaceInUiUnits()
        {
            var pts = DepthMath.DrawnFace(new Vector2(10, -20), new Vector2(40, 20), 150f, Vector2.zero, Vector2.zero, 3f, DepthFixture.D, DepthFixture.Table);
            Assert.AreEqual(4, pts.Length);
            CollectionAssert.AreEquivalent(new[] { new Vector2(-7, -27), new Vector2(27, -27), new Vector2(-7, -13), new Vector2(27, -13) },
                System.Array.ConvertAll(pts, p => new Vector2(Mathf.Round(p.x), Mathf.Round(p.y))));
        }

        [Test]
        public void DrawnFace_WithSway_MovesTheFaceButNotAtTheAnchorRow()
        {
            var cam = new Vector2(4, 2);
            var shift = DepthMath.AnchorShift(cam, DepthMath.TableZ(DepthFixture.AnchorY, DepthFixture.D, DepthFixture.Table), DepthFixture.D);
            var rest = DepthMath.DrawnFace(Vector2.zero, new Vector2(40, 20), 100f, Vector2.zero, Vector2.zero, 3f, DepthFixture.D, DepthFixture.Table);
            var swayed = DepthMath.DrawnFace(Vector2.zero, new Vector2(40, 20), 100f, cam, shift, 3f, DepthFixture.D, DepthFixture.Table);
            Assert.Greater(Vector2.Distance(rest[0], swayed[0]), 0.1f);
            var anchorRest = DepthMath.DrawnFace(Vector2.zero, new Vector2(40, 20), DepthFixture.AnchorY, Vector2.zero, Vector2.zero, 3f, DepthFixture.D, DepthFixture.Table);
            var anchorSway = DepthMath.DrawnFace(Vector2.zero, new Vector2(40, 20), DepthFixture.AnchorY, cam, shift, 3f, DepthFixture.D, DepthFixture.Table);
            Assert.Less(Vector2.Distance(anchorRest[0], anchorSway[0]), 0.3f);   // the 0.2 depth offset of a tile group
        }
    }
}
