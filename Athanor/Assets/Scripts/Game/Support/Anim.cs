using System;
using System.Collections;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// Beats (Wait), easing curves, and Over for per-frame scripts that are not a move or a fade (the spark trails in
    /// GameManager). Moves, fades and scales of objects go through Motion (DOTween, snapped to pixels).
    /// </summary>
    public static class Anim
    {
        /// <summary>Calls step with 0..1 over the given time, ending on exactly 1.</summary>
        public static IEnumerator Over(float seconds, Action<float> step)
        {
            for (float t = 0; t < seconds; t += Time.deltaTime) { step(t / seconds); yield return null; }
            step(1);
        }

        /// <summary>Waits, ending early when skip returns true.</summary>
        public static IEnumerator Wait(float seconds, Func<bool> skip = null)
        {
            for (float t = 0; t < seconds; t += Time.deltaTime) { if (skip != null && skip()) yield break; yield return null; }
        }

        public static float OutCubic(float t) => 1 - Mathf.Pow(1 - t, 3);
        public static float InCubic(float t) => t * t * t;
        public static float InOut(float t) => t * t * (3 - 2 * t);
        public static float OutBack(float t) { const float c = 1.70158f; float u = t - 1; return 1 + (c + 1) * u * u * u + c * u * u; }
        /// <summary>0 at both ends, 1 in the middle.</summary>
        public static float Hump(float t) => Mathf.Sin(Mathf.Clamp01(t) * Mathf.PI);
    }
}
