using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Athanor.Game
{
    /// <summary>
    /// The five depth presets of the HD-2D test scene (HD-2D mockup SPEC, "Depth presets", with the URP conversions
    /// of "Mapping mockup values to Unity"): ambient light, moonbeam, bloom, vignette, the fog sheets and the window view.
    /// Depth 0 is today's look (the main scene's single setting).
    /// </summary>
    public sealed class DepthMood : MonoBehaviour
    {
        /// <summary>One depth's look.</summary>
        [Serializable]
        public struct Preset
        {
            /// <summary>For the inspector only; the player-facing depth names are Lang keys.</summary>
            public string label;
            public Color ambient;
            public float moon;
            public Color accent;
            /// <summary>URP Bloom.threshold and Bloom.intensity.</summary>
            public float bloomThreshold, bloomIntensity;
            /// <summary>URP Vignette.intensity.</summary>
            public float vignette;
            /// <summary>Alpha of the back and the front fog sheet, and how far the fog's white is tinted toward the ambient colour.</summary>
            public float backFog, frontFog, fogMix;

            /// <summary>The fog sheets' colour: lerp(white, ambient, fogMix).</summary>
            public Color FogTint => Color.Lerp(Color.white, ambient, fogMix);
        }

        /// <summary>Depth 1..5 (index 0..4).</summary>
        public Preset[] presets = Defaults();
        /// <summary>The depth showing: 0 is today's look, 1..5 a preset.</summary>
        public int depth;
        public Light2D ambient, moon;
        public Volume volume;
        public SpriteRenderer backFog, frontFog;
        /// <summary>The effects switch whose sprite swap at <see cref="windowSwap"/> is the window view.</summary>
        public DepthSwitch effects;
        public int windowSwap;
        /// <summary>The flat window of today's game (depth 0), and the five depth views, sharp and pre-blurred.</summary>
        public Sprite windowNight;
        /// <summary>When set (the main scene), the window view is this one's job, not the sprite swap above.</summary>
        public WindowView window;
        public Sprite[] windowSharp = new Sprite[0], windowSoft = new Sprite[0];

        /// <summary>The SPEC's presets. Ambient, moon and accent are the game's arrays (GameFlow); bloom threshold =
        /// 1.0 + (bth - 0.80), bloom intensity = 1.1 * bin / 0.55, vignette = 0.34 * vi / 0.30.</summary>
        public static Preset[] Defaults()
        {
            Preset P(string label, Color amb, float moon, string accent, float bth, float bin, float vi, float bf, float ff, float mix) =>
                new Preset
                {
                    label = label, ambient = amb, moon = moon, accent = Px.Hex(accent),
                    bloomThreshold = 1f + (bth - 0.80f), bloomIntensity = 1.1f * bin / 0.55f, vignette = 0.34f * vi / 0.30f,
                    backFog = bf, frontFog = ff, fogMix = mix,
                };
            return new[]
            {
                P("1", new Color(0.52f, 0.52f, 1.00f), 0.62f, "#7cc0f8", 0.80f, 0.55f, 0.30f, 0.40f, 0.14f, 0.45f),
                P("2", new Color(0.40f, 0.62f, 0.86f), 0.50f, "#6ee6c8", 0.78f, 0.60f, 0.36f, 0.50f, 0.15f, 0.50f),
                P("3", new Color(0.62f, 0.42f, 0.95f), 0.40f, "#c486f4", 0.74f, 0.75f, 0.42f, 0.55f, 0.15f, 0.55f),
                P("4", new Color(0.86f, 0.40f, 0.46f), 0.30f, "#ee6a3c", 0.70f, 0.90f, 0.48f, 0.45f, 0.13f, 0.60f),
                P("5", new Color(0.78f, 0.74f, 0.58f), 0.50f, "#fff6b8", 0.76f, 0.65f, 0.55f, 0.62f, 0.16f, 0.50f),
            };
        }

        /// <summary>Shows depth d (0: today's look; 1..5: a preset).</summary>
        public void Set(int d) => Set(d, true);

        /// <summary>Shows depth d; themedWindow false keeps the night-sky window (title, tutorials) while lights, post and fog follow d.</summary>
        public void Set(int d, bool themedWindow)
        {
            depth = Mathf.Clamp(d, 0, presets.Length);
            // today's single setting (the Main.unity volume as authored) and depth 1's lights, with depth 1's fog
            var p = depth == 0 ? presets[0] : presets[depth - 1];
            float threshold = depth == 0 ? 1.0f : p.bloomThreshold, intensity = depth == 0 ? 1.1f : p.bloomIntensity, vig = depth == 0 ? 0.34f : p.vignette;
            if (ambient != null) ambient.color = p.ambient;
            if (moon != null) moon.intensity = p.moon;
            var profile = volume == null ? null : Application.isPlaying ? volume.profile : volume.sharedProfile;
            if (profile != null)
            {
                if (profile.TryGet(out Bloom bloom)) { bloom.threshold.Override(threshold); bloom.intensity.Override(intensity); }
                if (profile.TryGet(out Vignette v)) v.intensity.Override(vig);
            }
            Color tint = p.FogTint;
            if (backFog != null) backFog.color = tint.Alpha(p.backFog);
            if (frontFog != null) frontFog.color = tint.Alpha(p.frontFog);
            if (effects != null && windowSwap >= 0 && windowSwap < effects.sprites.Length)
            {
                bool view = themedWindow && depth > 0 && depth <= windowSharp.Length;
                effects.sprites[windowSwap].sharp = view ? windowSharp[depth - 1] : windowNight;
                effects.sprites[windowSwap].soft = view && depth <= windowSoft.Length ? windowSoft[depth - 1] : windowNight;
                effects.Set(effects.effectsOn);
            }
        }
    }
}
