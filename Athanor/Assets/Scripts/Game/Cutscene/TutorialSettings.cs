using System;
using UnityEngine;
using Hint = UnityEngine.TooltipAttribute;   // not "Tooltip": the game has a Tooltip view of its own

namespace Athanor.Game
{
    /// <summary>
    /// Where the tutorial points. Art space: 640x360, x to the right, y down from the top-left corner.
    /// Every rectangle a step lights comes from here (or from the view it talks about: the Journal's pages, the Redraw button).
    /// </summary>
    [Serializable]
    public sealed class TutorialTargets
    {
        [Header("Rectangles (x, y, width, height)")]
        [Hint("The hand of cards under the bench. Cut at the bottom of the screen.")]
        public Rect hand = new Rect(126, 284, 392, 80);
        [Hint("The Ignite button.")]
        public Rect ignite = new Rect(515, 304, 122, 42);
        [Hint("The Journal button.")]
        public Rect journal = new Rect(516, 284, 122, 24);
        [Hint("The satchel rack above the hand.")]
        public Rect rack = new Rect(126, 252, 392, 32);
        [Hint("The catalyst offer (step 5.3, a window with no dark).")]
        public Rect offer = new Rect(0, 60, 640, 240);

        [Header("A bench tile")]
        [Hint("The lit window round one tile.")]
        public Vector2 tileSize = new Vector2(72, 44);
        [Hint("From the tile's centre to the window's top-left corner.")]
        public Vector2 tileShift = new Vector2(36, 24);

        [Header("Redraw button")]
        [Hint("Pixels added round the Redraw button's own rectangle (HudView).")]
        public float redrawMargin = 2;
    }

    /// <summary>How the tutorial overlay looks and feels: the text box, the popup lines, the rim and arrow, and their timings.</summary>
    [Serializable]
    public sealed class TutorialStyle
    {
        [Header("Sorting")]
        [Hint("The order of the dark. The popup lines and the Broken sprites sit above it by the offsets below. The Dim, ReadPad and rim objects carry their own orders in the scene.")]
        public int baseOrder = 600;
        public int popupOrderOffset = 12;
        public int brokenOrderOffset = 6;

        [Header("Text box")]
        [Hint("Top-left corner of the box's border, when the box is at its usual place.")]
        public Vector2 boxPos = new Vector2(168, 14);
        public float boxWidth = 304;
        [Hint("Border thickness; the fill is inset by this on every side.")]
        public float boxBorder = 1;
        [Hint("From the box's top-left corner to the first line of text.")]
        public Vector2 textInset = new Vector2(8, 7);
        [Hint("The width a line of text wraps at; sets how many lines the box needs.")]
        public float textWrapWidth = 288;
        [Hint("Height of the box = padding + lines x pitch (+ the read extra on a step that waits for a click).")]
        public float boxPadding = 12;
        public float readExtra = 12;
        [Hint("The tallest the box may be.")]
        public float boxMaxHeight = 86;
        [Hint("English line pitch and most lines.")]
        public int linePitch = 12, maxLines = 4;
        [Hint("Vietnamese line pitch and most lines (the glyphs are taller).")]
        public int linePitchVi = 16, maxLinesVi = 3;
        [Hint("How far the box drops while the Journal is open (steps 2.5, 2.6), so it does not cover the page header.")]
        public int journalBoxDrop = 248;

        [Header("Popup lines (the score parts held under the box)")]
        [Hint("Centre x and top y of the first line.")]
        public Vector2 popupPos = new Vector2(320, 70);
        public float popupPitch = 20;
        public int popupFontSize = 16;
        public Color popupColor = new Color32(0xf0, 0xdc, 0xc0, 255);
        [Hint("The last line (the total).")]
        public Color popupTotalColor = new Color32(0xff, 0xf6, 0xb8, 255);
        public Color popupShadow = new Color32(0x0c, 0x07, 0x06, 255);

        [Header("Step 3.6: the broken chain")]
        [Hint("The last material of the chain, shown greyed.")]
        public Color brokenGhost = new Color(0.45f, 0.45f, 0.45f, 0.35f);

        [Header("Lit windows")]
        [Hint("Hole edges are rounded out to a multiple of this many art pixels. 2 keeps a pixel-perfect camera from putting the edge on a half pixel; 1 turns it off.")]
        [Min(1)] public int holeSnap = 2;

        [Header("Click lock")]
        [Hint("Seconds after a step shows before a click can advance it (so the click that ended the last step does not skip this one).")]
        public float clickGuard = 0.25f;

        [Header("Rim pulse")]
        [Range(0, 1)] public float rimBase = 0.55f;
        [Range(0, 1)] public float rimAmplitude = 0.3f;
        public float rimSpeed = 5;
        [Hint("A click on the dark lifts the rim to full; this is how fast it falls back, per second.")]
        public float bumpDecay = 3;

        [Header("Arrow")]
        [Hint("Gap above the last window, and below it when the window is near the top.")]
        public float arrowAbove = 10, arrowBelow = 8;
        [Hint("A window whose top is at or above this y gets the arrow below it.")]
        public float arrowFlipY = 80;
        public float arrowBobSpeed = 6, arrowBobAmplitude = 2;
    }
}
