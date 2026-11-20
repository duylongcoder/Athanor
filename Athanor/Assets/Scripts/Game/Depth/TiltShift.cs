using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The tilt-shift of the world camera: sharp from art y 145 to 235, then a blur that rises over 110 rows (smoothstep)
    /// to sigma 1.5 art px (owner gate G2 #9, #10). Drawn by <see cref="DepthTiltShiftFeature"/> before bloom, only for a
    /// camera that has this component enabled.
    /// </summary>
    [ExecuteAlways, RequireComponent(typeof(Camera))]
    public sealed class TiltShift : MonoBehaviour
    {
        static readonly HashSet<Camera> cameras = new HashSet<Camera>();
        static readonly int Band = Shader.PropertyToID("_TiltBand");

        /// <summary>The blur material the renderer features draw with (shader "Athanor/Tilt Shift").</summary>
        public Material material;
        /// <summary>The sharp band, in art rows (y down).</summary>
        public float bandTop, bandBottom;
        /// <summary>Rows from the band edge to full strength.</summary>
        public float feather;
        /// <summary>Blur sigma at full strength, in art px.</summary>
        public float maxSigma;

        /// <summary>Forgets every camera before the first scene (domain reload off).</summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void ResetStatics() => cameras.Clear();

        /// <summary>True when the tilt-shift is on for this camera.</summary>
        public static bool IsOn(Camera camera) => camera != null && cameras.Contains(camera);

        /// <summary>Blur sigma (art px) of an art row: maxSigma * smoothstep(0, 1, distance from the band / feather).</summary>
        public static float Sigma(float artY, float top, float bottom, float feather, float maxSigma)
        {
            float d = Mathf.Max(0, Mathf.Max(top - artY, artY - bottom));
            float x = Mathf.Clamp01(d / feather);
            return maxSigma * x * x * (3 - 2 * x);
        }

        void OnEnable() { cameras.Add(GetComponent<Camera>()); Push(); }
        void OnDisable() => cameras.Remove(GetComponent<Camera>());
        void OnValidate() => Push();

        /// <summary>Writes the band to the material.</summary>
        public void Push()
        {
            if (material != null) material.SetVector(Band, new Vector4(bandTop, bandBottom, feather, maxSigma));
        }
    }
}
