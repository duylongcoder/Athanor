using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// A tiling fog sheet (a tiled SpriteRenderer with the "Athanor/Fog Lit" material) that drifts sideways in whole
    /// texels: the picture moves left by trunc(t * speed) texels (back sheet +4, front sheet -6 art px per second).
    /// </summary>
    [ExecuteAlways, RequireComponent(typeof(SpriteRenderer))]
    public sealed class FogSheet : MonoBehaviour
    {
        static readonly int Scroll = Shader.PropertyToID("_ScrollTexels");

        /// <summary>Drift in art px (texels) per second; positive moves the picture left.</summary>
        public float speed = 4f;
        /// <summary>Width of the sheet's picture in texels (it repeats after this).</summary>
        public int width = 320;

        MaterialPropertyBlock block;

        /// <summary>
        /// The drift at time t, in whole texels, 0 .. width - 1. Truncated toward zero so both directions step the same way
        /// (the shown drift never runs ahead of t * speed).
        /// </summary>
        public static int Offset(float t, float speed, int width) => (((int)(t * speed) % width) + width) % width;

        /// <summary>Shows the sheet as it is at time t.</summary>
        public void Set(float t)
        {
            var r = GetComponent<SpriteRenderer>();
            if (block == null) block = new MaterialPropertyBlock();
            r.GetPropertyBlock(block);
            block.SetFloat(Scroll, Offset(t, speed, width));
            r.SetPropertyBlock(block);
        }

        void Update()
        {
            if (Application.isPlaying) Set(Time.time);
        }
    }
}
