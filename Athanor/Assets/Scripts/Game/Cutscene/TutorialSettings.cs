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
        public Rect hand;
        [Hint("The Ignite button.")]
        public Rect ignite;
        [Hint("The Journal button.")]
        public Rect journal;
        [Hint("The satchel rack above the hand.")]
        public Rect rack;
        [Hint("The catalyst offer (step 5.3, a window with no dark).")]
        public Rect offer;

        [Header("A bench tile")]
        [Hint("The lit window round one tile.")]
        public Vector2 tileSize;
        [Hint("From the tile's centre to the window's top-left corner.")]
        public Vector2 tileShift;

        [Header("Redraw button")]
        [Hint("Pixels added round the Redraw button's own rectangle (HudView).")]
        public float redrawMargin;
    }

    /// <summary>How the tutorial overlay looks and feels: the text box, the popup lines, the rim and arrow, and their timings.</summary>
    [Serializable]
    public sealed class TutorialStyle
    {
        [Header("Sorting")]
        [Hint("The order of the dark. The popup lines and the Broken sprites sit above it by the offsets below. The Dim, ReadPad and rim objects carry their own orders in the scene.")]
        public int baseOrder;
        public int popupOrderOffset;
        public int brokenOrderOffset;

        [Header("Text box")]
        [Hint("Top-left corner of the box's border, when the box is at its usual place.")]
        public Vector2 boxPos;
        public float boxWidth;
        [Hint("Border thickness; the fill is inset by this on every side.")]
        public float boxBorder;
        [Hint("From the box's top-left corner to the first line of text.")]
        public Vector2 textInset;
        [Hint("The width a line of text wraps at; sets how many lines the box needs.")]
        public float textWrapWidth;
        [Hint("Height of the box = padding + lines x pitch (+ the read extra on a step that waits for a click).")]
        public float boxPadding;
        public float readExtra;
        [Hint("The tallest the box may be.")]
        public float boxMaxHeight;
        [Hint("English line pitch and most lines.")]
        public int linePitch, maxLines;
        [Hint("Vietnamese line pitch and most lines (the glyphs are taller).")]
        public int linePitchVi, maxLinesVi;
        [Hint("How far the box drops while the Journal is open (steps 2.5, 2.6), so it does not cover the page header.")]
        public int journalBoxDrop;

        [Header("Popup lines (the score parts held under the box)")]
        [Hint("Centre x and top y of the first line.")]
        public Vector2 popupPos;
        public float popupPitch;
        public int popupFontSize;
        public Color popupColor;
        [Hint("The last line (the total).")]
        public Color popupTotalColor;
        public Color popupShadow;

        [Header("Step 3.6: the broken chain")]
        [Hint("The last material of the chain, shown greyed.")]
        public Color brokenGhost;

        [Header("Lit windows")]
        [Hint("Hole edges are rounded out to a multiple of this many art pixels. 2 keeps a pixel-perfect camera from putting the edge on a half pixel; 1 turns it off.")]
        [Min(1)] public int holeSnap;

        [Header("Click lock")]
        [Hint("Seconds after a step shows before a click can advance it (so the click that ended the last step does not skip this one).")]
        public float clickGuard;

        [Header("Rim pulse")]
        [Range(0, 1)] public float rimBase;
        [Range(0, 1)] public float rimAmplitude;
        public float rimSpeed;
        [Hint("A click on the dark lifts the rim to full; this is how fast it falls back, per second.")]
        public float bumpDecay;

        [Header("Arrow")]
        [Hint("Gap above the last window, and below it when the window is near the top.")]
        public float arrowAbove, arrowBelow;
        [Hint("A window whose top is at or above this y gets the arrow below it.")]
        public float arrowFlipY;
        public float arrowBobSpeed, arrowBobAmplitude;
    }
}
