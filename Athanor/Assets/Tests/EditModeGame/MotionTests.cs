using Athanor.Game;
using NUnit.Framework;
using PrimeTween;
using UnityEngine;
using Motion = Athanor.Game.Motion;   // UnityEngine has a Motion too

namespace Athanor.Tests
{
    /// <summary>
    /// Motion's helpers make live PrimeTween tweens. PrimeTween refuses a payload over 16 bytes: it logs an error and
    /// returns a dead tween, which a Sequence then refuses too (the rack flight and the catalyst hop did nothing).
    /// </summary>
    public class MotionTests
    {
        GameObject go;

        [SetUp] public void Create() => go = new GameObject("motion test", typeof(SpriteRenderer));

        [TearDown]
        public void Clean()
        {
            Tween.StopAll();
            Object.DestroyImmediate(go);
        }

        [Test]
        public void Arc_IsAlive_AndCanJoinASequence()
        {
            var t = go.transform;
            Assert.IsTrue(Motion.Arc(t, Vector3.zero, new Vector3(40, 0, 0), 20, 0.4f, Ease.InOutSine).isAlive);
            var hop = Sequence.Create().Group(Motion.Arc(t, Vector3.zero, Vector3.zero, 10, 0.2f));
            Assert.IsTrue(hop.isAlive);
        }

        [Test]
        public void Arc_EndsOnItsTarget_OnWholePixels()
        {
            var t = go.transform;
            var arc = Motion.Arc(t, new Vector3(0.3f, 0, 0), new Vector3(40.4f, 10.2f, 0), 20, 0.4f);
            arc.Complete();
            Assert.AreEqual(new Vector3(40, 10, 0), t.position);
        }

        [Test]
        public void EveryHelper_IsAlive()
        {
            var t = go.transform;
            Assert.IsTrue(Motion.Move(t, Vector3.zero, Vector3.one, 0.2f).isAlive);
            Assert.IsTrue(Motion.MoveLocal(t, Vector3.zero, Vector3.one, 0.2f).isAlive);
            Assert.IsTrue(Motion.Scale(t, 0, 1, 0.2f).isAlive);
            Assert.IsTrue(Motion.Fade(go.GetComponent<SpriteRenderer>(), 1, 0, 0.2f).isAlive);
        }
    }
}
