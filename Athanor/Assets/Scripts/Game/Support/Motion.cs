using System;
using System.Collections;
using PrimeTween;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// One-shot tweens on PrimeTween, kept in one place so every move is snapped to whole pixels (Px.Snap).
    /// Each call takes its start value explicitly and returns the Tween: group several with
    /// Sequence.Create().Group(..), and wait for them in a coroutine with Motion.Wait or .ToYieldInstruction()
    /// (never yield the struct itself, it gets boxed). Scaled time, as Anim.Over. The ease defaults to Linear here,
    /// not to PrimeTween's own default (OutQuad). Only for animations that run to a fixed end; spring follows,
    /// hover lifts, idle loops and particles stay hand-written.
    /// </summary>
    public static class Motion
    {
        /// <summary>World position, snapped every frame.</summary>
        public static Tween Move(Transform t, Vector3 from, Vector3 to, float seconds, Ease ease = Ease.Linear)
            => Tween.Custom(t, from, to, seconds, (tr, v) => tr.position = Px.Snap(v), ease);

        /// <summary>Local position, snapped every frame.</summary>
        public static Tween MoveLocal(Transform t, Vector3 from, Vector3 to, float seconds, Ease ease = Ease.Linear)
            => Tween.Custom(t, from, to, seconds, (tr, v) => tr.localPosition = Px.Snap(v), ease);

        /// <summary>
        /// An arc is the tween's target, not its payload: PrimeTween payloads are at most 16 bytes, and a bigger one
        /// gives a dead tween. One small object per arc.
        /// </summary>
        sealed class ArcPath { public Transform t; public Vector3 from, to; public float height; public Ease ease; }

        /// <summary>
        /// World position along a line eased by ease, lifted by height in a hump (Anim.Hump over plain time) and snapped.
        /// With from equal to to it is a hop on the spot.
        /// </summary>
        public static Tween Arc(Transform t, Vector3 from, Vector3 to, float height, float seconds, Ease ease = Ease.Linear)
            => Tween.Custom(new ArcPath { t = t, from = from, to = to, height = height, ease = ease },
                new TweenSettings<float>(0f, 1f, seconds, Ease.Linear),
                (p, k) => { if (p.t != null) p.t.position = Px.Snap(Vector3.Lerp(p.from, p.to, Easing.Evaluate(k, p.ease)) + new Vector3(0, Anim.Hump(k) * p.height, 0)); });

        /// <summary>Uniform x and y scale (z stays 1).</summary>
        public static Tween Scale(Transform t, float from, float to, float seconds, Ease ease = Ease.Linear)
            => Tween.Custom(t, from, to, seconds, (tr, s) => tr.localScale = new Vector3(s, s, 1), ease);

        /// <summary>Alpha of a sprite; its colour is kept.</summary>
        public static Tween Fade(SpriteRenderer r, float from, float to, float seconds, Ease ease = Ease.Linear)
            => Tween.Custom(r, from, to, seconds, (sr, a) => sr.color = sr.color.Alpha(a), ease);

        /// <summary>Alpha of a label and its shadow.</summary>
        public static Tween Fade(Label l, float from, float to, float seconds, Ease ease = Ease.Linear)
            => Tween.Custom(l, from, to, seconds, (lb, a) => lb.Alpha = a, ease);

        /// <summary>
        /// Stops every tween that targets the component or anything under its object: the transforms, sprites and labels the
        /// helpers above animate. A view calls it from OnDestroy, after StopAllCoroutines, so nothing outlives it into the next Play.
        /// </summary>
        public static void StopOn(Component owner)
        {
            if (owner == null) return;
            Tween.StopAll(onTarget: owner);
            foreach (var t in owner.GetComponentsInChildren<Transform>(true)) Tween.StopAll(onTarget: t);
            foreach (var r in owner.GetComponentsInChildren<SpriteRenderer>(true)) Tween.StopAll(onTarget: r);
            foreach (var l in owner.GetComponentsInChildren<Label>(true)) Tween.StopAll(onTarget: l);
        }

        /// <summary>Waits for a tween. When hurry returns true it jumps to its end values at once.</summary>
        public static IEnumerator Wait(Tween tween, Func<bool> hurry = null)
        {
            while (tween.isAlive) { if (hurry != null && hurry()) tween.Complete(); else yield return null; }
        }

        /// <summary>Waits for a sequence. When hurry returns true it jumps to its end values at once.</summary>
        public static IEnumerator Wait(Sequence sequence, Func<bool> hurry = null)
        {
            while (sequence.isAlive) { if (hurry != null && hurry()) sequence.Complete(); else yield return null; }
        }
    }
}
