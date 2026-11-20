using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Pure rectangle math for tutorial areas (art pixels, top-left origin). No scene needed, so it is tested alone.</summary>
    public static class TutorialRect
    {
        /// <summary>Grows r by margin = (left, top, right, bottom) pixels.</summary>
        public static Rect Grow(Rect r, Vector4 m) =>
            new Rect(r.x - m.x, r.y - m.y, r.width + m.x + m.z, r.height + m.y + m.w);

        /// <summary>The art-pixel rect (top-left, size) of an area whose world centre is given and whose size is in pixels.</summary>
        public static Rect ArtRect(Vector2 worldCentre, Vector2 size)
        {
            float cx = worldCentre.x + Px.W / 2f, cy = Px.H / 2f - worldCentre.y;
            return new Rect(cx - size.x / 2f, cy - size.y / 2f, size.x, size.y);
        }
    }

    /// <summary>What a tutorial area lights.</summary>
    public enum AreaKind { Fixed, ScriptTile, View }

    /// <summary>A named game view an area can follow (the rect comes from the view at run time).</summary>
    public enum ViewRef { Bench, Hand, Ignite, Rack, Redraw, Journal, JournalCell, JournalRecipes, JournalIndex, JournalHints, Offer, EventHub }

    /// <summary>Which scripted hand's tiles a ScriptTile area follows.</summary>
    public enum ScriptTrial { Reaction, Miss, Chain, Catalyst }

    /// <summary>
    /// One lit window of a step. Fixed: the object's world position is the centre and <see cref="size"/> (pixels) its size.
    /// ScriptTile and View follow the game at run time. <see cref="clicksPass"/> lets clicks in the window reach the game.
    /// </summary>
    public sealed class TutorialArea : MonoBehaviour
    {
        public AreaKind kind = AreaKind.Fixed;
        [Tooltip("Fixed: size in pixels (= world units).")] public Vector2 size = new Vector2(16, 16);
        [Tooltip("ScriptTile: the scripted hand.")] public ScriptTrial trial = ScriptTrial.Reaction;
        [Tooltip("ScriptTile: index in the hand's Lay. -1 = the union of all of the hand's tiles.")] public int slot;
        [Tooltip("View: the view to follow.")] public ViewRef view = ViewRef.Bench;
        [Tooltip("View JournalCell: the material id shown in the Journal.")] public string cellId = "";
        [Tooltip("Pixels added to the area on left, top, right, bottom.")] public Vector4 margin;
        [Tooltip("Clicks in this area reach the game (the bench under it). Off = the area is highlight only.")] public bool clicksPass;

        /// <summary>Fixed areas only: the art-pixel rect from the transform and size.</summary>
        public Rect FixedArtRect() => TutorialRect.ArtRect(transform.position, size);
    }
}
