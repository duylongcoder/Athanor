using System;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The "all effects off" switch of the depth test scene. Off: the far layers show the original sharp sprites and the
    /// fog sheets and the foreground are hidden, so the world at rest is today's bench. On: the pre-blurred far sprites,
    /// the fog and the foreground corners.
    /// </summary>
    public sealed class DepthSwitch : MonoBehaviour
    {
        /// <summary>A sprite that has a sharp picture and a pre-blurred one of the same centre.</summary>
        [Serializable]
        public struct SpriteSwap
        {
            public SpriteRenderer target;
            public Sprite sharp, soft;
        }

        /// <summary>A flame whose frames have a sharp set and a pre-blurred set.</summary>
        [Serializable]
        public struct FlameSwap
        {
            public Flame flame;
            public Sprite[] sharp, soft;
        }

        /// <summary>Whether the effects are showing.</summary>
        public bool effectsOn = true;
        public SpriteSwap[] sprites = new SpriteSwap[0];
        public FlameSwap[] flames = new FlameSwap[0];
        /// <summary>The window view of the main scene, which shows its pre-blurred pictures only with the effects on (or null).</summary>
        public WindowView window;
        /// <summary>Shown only with the effects on: the fog sheets and the foreground corners.</summary>
        public GameObject[] effectObjects = new GameObject[0];
        /// <summary>Enabled only with the effects on: the tilt-shift.</summary>
        public Behaviour[] effectBehaviours = new Behaviour[0];

        /// <summary>Shows the scene with the effects on or off.</summary>
        public void Set(bool on)
        {
            effectsOn = on;
            if (window != null) window.Soft = on;
            foreach (var s in sprites) if (s.target != null) s.target.sprite = on ? s.soft : s.sharp;
            foreach (var f in flames)
            {
                if (f.flame == null) continue;
                f.flame.frames = on ? f.soft : f.sharp;
                if (f.flame.body != null && f.flame.frames != null && f.flame.frames.Length > 0) f.flame.body.sprite = f.flame.frames[0];
            }
            foreach (var go in effectObjects) if (go != null) go.SetActive(on);
            foreach (var b in effectBehaviours) if (b != null) b.enabled = on;
        }
    }
}
