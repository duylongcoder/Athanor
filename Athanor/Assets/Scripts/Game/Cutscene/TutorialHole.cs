using UnityEngine;
using Hint = UnityEngine.TooltipAttribute;   // not "Tooltip": the game has a Tooltip view of its own

namespace Athanor.Game
{
    /// <summary>
    /// One lit window of the tutorial overlay: a SpriteMask that cuts a hole in the dim (the dim draws "visible outside mask"),
    /// and the four gold rim pieces round it. TutorialController keeps a few of these in the scene and opens as many as a step needs.
    /// </summary>
    public sealed class TutorialHole : MonoBehaviour
    {
        [SerializeField] SpriteMask mask;
        [SerializeField] SpriteRenderer top, bottom, left, right;
        [Hint("Rim thickness in art pixels.")]
        [SerializeField, Min(0)] float rimThickness;

        /// <summary>Lights this rectangle (art space, top-left corner and size) and shows the rim round it when asked.</summary>
        public void Open(Rect r, bool showRim)
        {
            gameObject.SetActive(true);
            float t = rimThickness;
            Px.Fit(mask.transform, r.x, r.y, r.width, r.height);
            Px.Fit(top.transform, r.xMin - t, r.yMin - t, r.width + 2 * t, t);
            Px.Fit(bottom.transform, r.xMin - t, r.yMax, r.width + 2 * t, t);
            Px.Fit(left.transform, r.xMin - t, r.yMin, t, r.height);
            Px.Fit(right.transform, r.xMax, r.yMin, t, r.height);
            top.enabled = bottom.enabled = left.enabled = right.enabled = showRim;
        }

        public void Close() => gameObject.SetActive(false);

        /// <summary>The rim's opacity (the pulse); its colour is the one set in the scene.</summary>
        public void SetRimAlpha(float a)
        {
            Tint(top, a); Tint(bottom, a); Tint(left, a); Tint(right, a);
        }

        static void Tint(SpriteRenderer r, float a) { var c = r.color; c.a = a; r.color = c; }
    }
}
