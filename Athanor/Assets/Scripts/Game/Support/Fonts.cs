using TMPro;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Logo is the "Athanor" lettering: Alagard in every language. The other kinds follow the language (Loc).</summary>
    public enum FontKind { Title, Body, Name, Tiny, Logo, Numeral }

    /// <summary>Which font a <see cref="FontKind"/> uses in the language showing, and the size and line box that go with it.</summary>
    public static class Fonts
    {
        /// <summary>The font of a kind in the language showing. Vietnamese uses SVN-Coder's Crux for all four kinds, never for the logo.</summary>
        public static TMP_FontAsset FontFor(FontKind kind)
        {
            if (kind == FontKind.Logo || kind == FontKind.Numeral) return Art.Lib.title;   // Numeral: digits only, so the English face and size in every language
            if (Loc.Vi && Art.Lib.crux != null) return Art.Lib.crux;
            return kind == FontKind.Title ? Art.Lib.title : kind == FontKind.Body ? Art.Lib.body : kind == FontKind.Name ? Art.Lib.label : Art.Lib.tiny;
        }

        /// <summary>The point size for a kind: Tiny 8 is Crux 16 in Vietnamese (the marks need the height); the rest keep their size.</summary>
        public static float SizeFor(FontKind kind, int size) => kind == FontKind.Tiny && Loc.Vi && Art.Lib.crux != null ? size * 2 : size;

        /// <summary>
        /// The half-leading of a font at a point size, in whole pixels (rounded up): how far below the top of a line box as
        /// tall as the size (the mockup's CSS, line-height = size) the font's ascent line sits. TMP puts the ascent line at
        /// the box top instead. Crux (ascent 7, descent -2 at 16): 11 px at 48, 7 at 32.
        /// </summary>
        public static int HalfLeading(UnityEngine.TextCore.FaceInfo face, float size)
        {
            float scale = size / face.pointSize;
            return Mathf.Max(0, Mathf.CeilToInt((size - (face.ascentLine - face.descentLine) * scale) / 2 - 0.001f));
        }
    }
}
