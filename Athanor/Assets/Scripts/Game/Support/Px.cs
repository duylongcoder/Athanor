using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The art is laid out on a 640x360 canvas with y running down from the top-left corner.
    /// The world uses one unit per pixel with the origin at the screen centre and y up.
    /// </summary>
    public static class Px
    {
        public const int W = 640, H = 360;

        /// <summary>World position of an art-space point.</summary>
        public static Vector3 At(float x, float y) => new Vector3(x - W / 2f, H / 2f - y, 0);

        /// <summary>World position of the centre of an art-space rectangle given by its top-left corner and size.</summary>
        public static Vector3 Mid(float x, float y, float w, float h) => At(x + w / 2f, y + h / 2f);

        public static Vector3 Snap(Vector3 p) => new Vector3(Mathf.Round(p.x), Mathf.Round(p.y), p.z);

        /// <summary>Colour from "#rrggbb"; k scales brightness past 1 for glow, a is alpha.</summary>
        public static Color Hex(string hex, float a = 1, float k = 1)
        {
            if (string.IsNullOrEmpty(hex) || !ColorUtility.TryParseHtmlString(hex, out var c)) c = Color.white;
            return new Color(c.r * k, c.g * k, c.b * k, a);
        }

        public static Color Alpha(this Color c, float a) => new Color(c.r, c.g, c.b, a);
        public static Color Times(this Color c, float k) => new Color(c.r * k, c.g * k, c.b * k, c.a);
    }
}
