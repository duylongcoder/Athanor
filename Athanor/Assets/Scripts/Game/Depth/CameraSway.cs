using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The idle sway and the world-only shake of the HD-2D bench. The sway moves the world camera (x = A sin(2 pi t / 6),
    /// y = A / 2 sin(2 pi t / 9), A = 4 art px) and re-aims it so the middle tile row (art y 188) stays still. The shake
    /// shifts the whole world picture by whole art px. Neither touches the HUD camera.
    /// </summary>
    [RequireComponent(typeof(DepthRig)), DefaultExecutionOrder(100)]
    public sealed class CameraSway : MonoBehaviour
    {
        /// <summary>The sway's on/off switch (the Settings toggle; the shake does not depend on it).</summary>
        public bool swayOn = true;
        /// <summary>A: camera travel in art px at z = 0.</summary>
        public float amplitude = DepthMath.SwayAmplitude;

        DepthRig rig;
        /// <summary>The game's screen shake (ScreenFx), in whole art px with world y up, added to the shake of <see cref="Shake"/>.</summary>
        public Vector2 external;
        float shakeUntil;
        int shakeSize;

        /// <summary>Shakes the world (never the HUD) by up to <paramref name="size"/> whole art px for a while.</summary>
        public void Shake(int size, float seconds)
        {
            shakeSize = size;
            shakeUntil = Time.time + seconds;
        }

        /// <summary>The shake offset at time t for a shake of the given size: whole art px, the mockup's
        /// x = round(a sin 53.1 t), y = round(2a/3 cos 41.7 t).</summary>
        public static Vector2Int ShakeOffset(float t, int size) =>
            new Vector2Int(Mathf.RoundToInt(size * Mathf.Sin(53.1f * t)), Mathf.RoundToInt(size * 2f / 3f * Mathf.Cos(41.7f * t)));

        /// <summary>Places the world camera for time t.</summary>
        public void Pose(float t)
        {
            if (rig == null) rig = GetComponent<DepthRig>();
            if (rig.Flat) { rig.SetView(Vector2.zero, Vector2.zero); return; }       // a cutscene: flat, nothing moves
            Vector2 cam = swayOn ? DepthMath.Sway(t, amplitude) : Vector2.zero;
            Vector2 shake = (t < shakeUntil ? (Vector2)ShakeOffset(t, shakeSize) : Vector2.zero) + external;
            rig.SetView(cam, shake);
        }

        void LateUpdate()
        {
            if (Application.isPlaying) Pose(Time.time);
        }
    }
}
