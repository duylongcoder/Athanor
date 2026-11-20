using TMPro;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Horizontal anchor of a <see cref="Spawn.Text"/>.</summary>
    public enum Align { Left, Centre, Right }

    /// <summary>
    /// Run-time spawners: the objects the game makes while it plays (float numbers, fly-in icons, tutorial rects, link motes, rarity stars).
    /// Everything that is on screen from the start is a scene object or a prefab, not made here.
    /// </summary>
    public static class Spawn
    {
        /// <summary>Radius in pixels of the "glow" sprite at scale 1 (the sprite's shape): a glow of radius r is scaled by r / GlowRadius.</summary>
        const float GlowRadius = 32f;
        /// <summary>Box of a text that does not wrap (width 0): the box is only the layout frame, the text overflows it (a TextMeshPro convention, not a layout).</summary>
        const float DefaultTextWidth = 400f, TextBoxHeight = 100f;

        /// <summary>An empty child that draws with its parent (same layer).</summary>
        public static Transform Root(string name, Transform parent = null)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            if (parent != null) go.layer = parent.gameObject.layer;      // a child draws with its parent (the world camera or the UI camera)
            return go.transform;
        }

        /// <summary>A sprite centred on a world position.</summary>
        public static SpriteRenderer Sprite(Transform parent, string sprite, Vector3 worldPos, int order, Material material = null)
        {
            var go = new GameObject(sprite);
            go.transform.SetParent(parent, false);
            if (parent != null) go.layer = parent.gameObject.layer;
            go.transform.position = worldPos;
            var r = go.AddComponent<SpriteRenderer>();
            r.sprite = Art.Lib.Sprite(sprite);
            r.sharedMaterial = material != null ? material : Art.Lib.unlit;
            r.sortingOrder = order;
            return r;
        }

        /// <summary>A filled rectangle in art space.</summary>
        public static SpriteRenderer Rect(Transform parent, float x, float y, float w, float h, Color color, int order)
        {
            var r = Sprite(parent, "dot", Px.Mid(x, y, w, h), order);
            r.name = "rect";
            r.transform.localScale = new Vector3(w / Px.DotSize, h / Px.DotSize, 1);
            r.color = color;
            return r;
        }

        /// <summary>An additive soft glow of the given radius.</summary>
        public static SpriteRenderer Glow(Transform parent, Vector3 worldPos, float radius, Color color, int order)
        {
            var r = Sprite(parent, "glow", worldPos, order, Art.Lib.glow);
            float scale = radius / GlowRadius;
            r.transform.localScale = new Vector3(scale, scale, 1);
            r.color = color;
            return r;
        }

        /// <summary>Text whose top edge is at art-space y, anchored at x by its alignment. width > 0 wraps.</summary>
        public static Label Text(Transform parent, string text, float x, float y, FontKind font, int size, Color color, int order,
            Align align = Align.Left, Color? shadow = null, float width = 0)
        {
            var go = new GameObject("text");
            go.transform.SetParent(parent, false);
            if (parent != null) go.layer = parent.gameObject.layer;
            go.transform.position = Px.At(x, y);
            var label = go.AddComponent<Label>();
            label.kind = font; label.size = size;
            if (shadow.HasValue)
            {
                label.shadow = Tmp(Root("shadow", go.transform).gameObject, text, font, size, shadow.Value, order - 1, align, width);
                label.shadow.transform.localPosition = new Vector3(1, -1, 0);
            }
            label.main = Tmp(Root("face", go.transform).gameObject, text, font, size, color, order, align, width);
            label.ApplyBias();
            return label;
        }

        static TextMeshPro Tmp(GameObject go, string text, FontKind font, int size, Color color, int order, Align align, float width)
        {
            var t = go.AddComponent<TextMeshPro>();
            t.font = Fonts.FontFor(font);
            t.isOrthographic = true;
            t.fontSize = Fonts.SizeFor(font, size);
            t.color = color;
            t.richText = true;
            t.extraPadding = false;
            t.margin = Vector4.zero;
            t.textWrappingMode = width > 0 ? TextWrappingModes.Normal : TextWrappingModes.NoWrap;
            t.overflowMode = TextOverflowModes.Overflow;
            t.alignment = align == Align.Left ? TextAlignmentOptions.TopLeft : align == Align.Centre ? TextAlignmentOptions.Top : TextAlignmentOptions.TopRight;
            var rt = t.rectTransform;
            float pivotX = align == Align.Left ? 0 : align == Align.Centre ? 0.5f : 1;
            rt.pivot = new Vector2(pivotX, 1);
            rt.sizeDelta = new Vector2(width > 0 ? width : DefaultTextWidth, TextBoxHeight);
            rt.localPosition = Vector3.zero;
            t.sortingOrder = order;
            t.text = text;
            return t;
        }

        /// <summary>Makes a game object clickable: a rectangle of the given size, drawn at order, that calls onClick.</summary>
        public static Clickable Click(GameObject go, Vector2 size, int order, System.Action onClick = null)
        {
            var c = go.AddComponent<Clickable>();
            c.size = size; c.order = order; c.onClick = onClick;
            return c;
        }
    }
}
