using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// One-shot tweens on DOTween, kept in one place so every move is snapped to whole pixels (Px.Snap).
    /// Each call takes its start value explicitly and returns the Tween: group several with
    /// Motion.Seq().Group(..), and wait for them in a coroutine with Motion.Wait or .ToYieldInstruction().
    /// Scaled time, as Anim.Over. The ease defaults to Linear here, not to DOTween's own default (OutQuad):
    /// every helper sets its ease, so nothing depends on <c>DOTween.defaultEaseType</c>. Each helper also guards its target,
    /// so a tween that outlives a destroyed object does nothing. Only for animations that run to a fixed end; spring follows,
    /// hover lifts, idle loops and particles stay hand-written.
    /// </summary>
    public static class Motion
    {
        /// <summary>Clears DOTween's statics when Play starts: domain reload is off, so its internal object would otherwise outlive a Stop.</summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetStatics() => DOTween.Clear(true);

        /// <summary>World position, snapped every frame.</summary>
        public static Tween Move(Transform t, Vector3 from, Vector3 to, float seconds, Ease ease = Ease.Linear)
            => DOVirtual.Vector3(from, to, seconds, v => { if (t != null) t.position = Px.Snap(v); }).SetEase(ease).SetTarget(t);

        /// <summary>Local position, snapped every frame.</summary>
        public static Tween MoveLocal(Transform t, Vector3 from, Vector3 to, float seconds, Ease ease = Ease.Linear)
            => DOVirtual.Vector3(from, to, seconds, v => { if (t != null) t.localPosition = Px.Snap(v); }).SetEase(ease).SetTarget(t);

        /// <summary>
        /// World position along a line eased by ease, lifted by height in a hump (Anim.Hump over plain time) and snapped.
        /// With from equal to to it is a hop on the spot.
        /// </summary>
        public static Tween Arc(Transform t, Vector3 from, Vector3 to, float height, float seconds, Ease ease = Ease.Linear)
            => DOVirtual.Float(0f, 1f, seconds, k =>
            {
                if (t == null) return;
                t.position = Px.Snap(Vector3.Lerp(from, to, DOVirtual.EasedValue(0f, 1f, k, ease)) + new Vector3(0, Anim.Hump(k) * height, 0));
            }).SetEase(Ease.Linear).SetTarget(t);

        /// <summary>Uniform x and y scale (z stays 1).</summary>
        public static Tween Scale(Transform t, float from, float to, float seconds, Ease ease = Ease.Linear)
            => DOVirtual.Float(from, to, seconds, s => { if (t != null) t.localScale = new Vector3(s, s, 1); }).SetEase(ease).SetTarget(t);

        /// <summary>Alpha of a sprite; its colour is kept.</summary>
        public static Tween Fade(SpriteRenderer r, float from, float to, float seconds, Ease ease = Ease.Linear)
            => DOVirtual.Float(from, to, seconds, a => { if (r != null) r.color = r.color.Alpha(a); }).SetEase(ease).SetTarget(r);

        /// <summary>Alpha of a label and its shadow.</summary>
        public static Tween Fade(Label l, float from, float to, float seconds, Ease ease = Ease.Linear)
            => DOVirtual.Float(from, to, seconds, a => { if (l != null) l.Alpha = a; }).SetEase(ease).SetTarget(l);

        /// <summary>A number between from and to handed to set together with target, for what no helper above covers.</summary>
        public static Tween Custom<T>(T target, float from, float to, float seconds, Action<T, float> set, Ease ease = Ease.Linear) where T : class
            => DOVirtual.Float(from, to, seconds, v => set(target, v)).SetEase(ease).SetTarget(target);

        /// <summary>A tween that does nothing for this long: it holds a Seq open, as the longest member.</summary>
        public static Tween Delay(float seconds) => DOVirtual.Float(0f, 1f, seconds, _ => { }).SetEase(Ease.Linear);

        /// <summary>A new, empty sequence. Add tweens that start together with Group, and one at a time with Insert.</summary>
        public static Sequence Seq() => DOTween.Sequence();

        /// <summary>Adds a tween that starts with the others already in the sequence.</summary>
        public static Sequence Group(this Sequence sequence, Tween tween) => sequence.Join(tween);

        /// <summary>
        /// Stops every tween that targets the component or anything under its object: the transforms, sprites and labels the
        /// helpers above animate. A view calls it from OnDestroy, after StopAllCoroutines, so nothing outlives it into the next Play.
        /// </summary>
        public static void StopOn(Component owner)
        {
            if (owner == null) return;
            DOTween.Kill(owner);
            foreach (var t in owner.GetComponentsInChildren<Transform>(true)) DOTween.Kill(t);
            foreach (var r in owner.GetComponentsInChildren<SpriteRenderer>(true)) DOTween.Kill(r);
            foreach (var l in owner.GetComponentsInChildren<Label>(true)) DOTween.Kill(l);
        }

        /// <summary>Waits for a tween or a sequence. When hurry returns true it jumps to its end values at once.</summary>
        public static IEnumerator Wait(Tween tween, Func<bool> hurry = null)
        {
            while (tween.IsActive()) { if (hurry != null && hurry()) tween.Complete(); else yield return null; }
        }

        /// <summary>For <c>yield return tween.ToYieldInstruction()</c>: waits until the tween is done or stopped.</summary>
        public static IEnumerator ToYieldInstruction(this Tween tween) => Wait(tween);
    }
}
