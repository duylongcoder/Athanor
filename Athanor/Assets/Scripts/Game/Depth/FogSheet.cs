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
        public float speed;
        /// <summary>Width of the sheet's picture in texels (it repeats after this).</summary>
        public int width;

        MaterialPropertyBlock block;
        SpriteRenderer sheet;
        int shownOffset = -1;

        /// <summary>
        /// The drift at time t, in whole texels, 0 .. width - 1. Truncated toward zero so both directions step the same way
        /// (the shown drift never runs ahead of t * speed).
        /// </summary>
        public static int Offset(float t, float speed, int width) => (((int)(t * speed) % width) + width) % width;

        /// <summary>Shows the sheet as it is at time t.</summary>
        public void Set(float t)
        {
            if (sheet == null) sheet = GetComponent<SpriteRenderer>();
            if (block == null) block = new MaterialPropertyBlock();
            sheet.GetPropertyBlock(block);
            int offset = Offset(t, speed, width);
            block.SetFloat(Scroll, offset);
            sheet.SetPropertyBlock(block);
            shownOffset = offset;
        }

        void Update()
        {
            if (Application.isPlaying) Tick(Time.time);
        }

        /// <summary>One frame at time <paramref name="time"/>: the sheet moves in whole texels, so most frames change nothing and write nothing.</summary>
        public void Tick(float time)
        {
            if (Offset(time, speed, width) != shownOffset) Set(time);
        }
    }
}
