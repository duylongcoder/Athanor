using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// What a depth looks like from the bench: the intro line under its name and the view through the window.
    /// Pure choices, no scene; the pictures and effects are drawn by <see cref="WindowView"/>.
    /// </summary>
    public static class DepthLook
    {
        /// <summary>Depths that have their own line and view; deeper ones (Endless) keep the last view and show no line.</summary>
        public const int Authored = 5;

        /// <summary>Lang key of the depth's intro line, or null when the depth has none (0, or beyond the authored five).</summary>
        public static string IntroKey(int depth) => depth >= 1 && depth <= Authored ? "depth." + depth + ".intro" : null;

        /// <summary>Which window view (1..5) a depth shows: the depth itself, clamped, so Endless keeps the deepest view.</summary>
        public static int View(int depth) => Mathf.Clamp(depth, 1, Authored);

        /// <summary>
        /// Whether a run starts with the depth 1 line (depth 1 never goes through the descent cutscene). Not in the first-run
        /// tutorial nor in How to play, whose scripted start must not be held up.
        /// </summary>
        public static bool ShowsRunIntro(int depth, bool tutorial, bool sandbox) => !tutorial && !sandbox && IntroKey(depth) != null && depth == 1;

        /// <summary>Whether the window shows the depth's own view; the title, the tutorial and How to play keep the original night sky.</summary>
        public static bool ThemedWindow(bool inRun, bool tutorial, bool sandbox) => inRun && !tutorial && !sandbox;

        /// <summary>Name of the window sprite in the art library for a depth.</summary>
        public static string WindowSprite(int depth) => "window_d" + View(depth);
    }
}
