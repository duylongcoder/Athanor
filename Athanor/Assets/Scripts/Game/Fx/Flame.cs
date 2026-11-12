using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Athanor.Game
{
    /// <summary>A flickering flame: cycles its frames and wavers the light and halo that go with it.</summary>
    public sealed class Flame : MonoBehaviour
    {
        public Sprite[] frames;
        public SpriteRenderer body, halo;
        public Light2D glowLight;
        public float lightIntensity = 1, haloAlpha = 0.5f, framesPerSecond = 11, phase;

        void Update()
        {
            float t = Time.time;
            if (frames != null && frames.Length > 0) body.sprite = frames[(int)((t + phase) * framesPerSecond) % frames.Length];
            float flicker = 0.85f + 0.15f * Mathf.Sin(t * 11 + phase) * Mathf.Sin(t * 5.3f + phase * 2);
            if (glowLight != null) glowLight.intensity = lightIntensity * flicker;
            if (halo != null) halo.color = halo.color.Alpha(haloAlpha * flicker);
        }
    }
}
