using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Athanor.Game
{
    public enum FontKind { Title, Body, Name, Tiny }
    public enum Align { Left, Centre, Right }

    /// <summary>Factories for the sprites, text and lights the views are built from.</summary>
    public static class Make
    {
        public static ArtLibrary Lib;

        public static Transform Root(string name, Transform parent = null)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            return go.transform;
        }

        /// <summary>A sprite centred on a world position.</summary>
        public static SpriteRenderer Sprite(Transform parent, string sprite, Vector3 worldPos, int order, Material material = null)
        {
            var go = new GameObject(sprite);
            go.transform.SetParent(parent, false);
            go.transform.position = worldPos;
            var r = go.AddComponent<SpriteRenderer>();
            r.sprite = Lib.Sprite(sprite);
            r.sharedMaterial = material != null ? material : Lib.unlit;
            r.sortingOrder = order;
            return r;
        }

        /// <summary>A sprite placed by the art-space position of its top-left corner.</summary>
        public static SpriteRenderer SpriteAt(Transform parent, string sprite, float x, float y, int order, Material material = null)
        {
            var s = Lib.Sprite(sprite);
            return Sprite(parent, sprite, Px.Mid(x, y, s.rect.width, s.rect.height), order, material);
        }

        /// <summary>A filled rectangle in art space.</summary>
        public static SpriteRenderer Rect(Transform parent, float x, float y, float w, float h, Color color, int order)
        {
            var r = Sprite(parent, "dot", Px.Mid(x, y, w, h), order);
            r.name = "rect";
            r.transform.localScale = new Vector3(w / 4f, h / 4f, 1);
            r.color = color;
            return r;
        }

        /// <summary>An additive soft glow of the given radius.</summary>
        public static SpriteRenderer Glow(Transform parent, Vector3 worldPos, float radius, Color color, int order)
        {
            var r = Sprite(parent, "glow", worldPos, order, Lib.glow);
            r.transform.localScale = new Vector3(radius / 32f, radius / 32f, 1);
            r.color = color;
            return r;
        }

        /// <summary>Text whose top edge is at art-space y, anchored at x by its alignment. width > 0 wraps.</summary>
        public static Label Text(Transform parent, string text, float x, float y, FontKind font, int size, Color color, int order,
            Align align = Align.Left, Color? shadow = null, float width = 0)
        {
            var go = new GameObject("text");
            go.transform.SetParent(parent, false);
            go.transform.position = Px.At(x, y);
            var label = go.AddComponent<Label>();
            if (shadow.HasValue)
            {
                label.shadow = Tmp(Root("shadow", go.transform).gameObject, text, font, size, shadow.Value, order - 1, align, width);
                label.shadow.transform.localPosition = new Vector3(1, -1, 0);
            }
            label.main = Tmp(Root("face", go.transform).gameObject, text, font, size, color, order, align, width);
            return label;
        }

        static TextMeshPro Tmp(GameObject go, string text, FontKind font, int size, Color color, int order, Align align, float width)
        {
            var t = go.AddComponent<TextMeshPro>();
            t.font = font == FontKind.Title ? Lib.title : font == FontKind.Body ? Lib.body : font == FontKind.Name ? Lib.label : Lib.tiny;
            t.isOrthographic = true;
            t.fontSize = size;
            t.color = color;
            t.richText = true;
            t.extraPadding = false;
            t.margin = Vector4.zero;
            t.textWrappingMode = width > 0 ? TextWrappingModes.Normal : TextWrappingModes.NoWrap;
            t.overflowMode = TextOverflowModes.Overflow;
            t.alignment = align == Align.Left ? TextAlignmentOptions.TopLeft : align == Align.Centre ? TextAlignmentOptions.Top : TextAlignmentOptions.TopRight;
            var rt = t.rectTransform;
            rt.pivot = new Vector2(align == Align.Left ? 0 : align == Align.Centre ? 0.5f : 1, 1);
            rt.sizeDelta = new Vector2(width > 0 ? width : 400, 100);
            rt.localPosition = Vector3.zero;
            t.sortingOrder = order;
            t.text = text;
            return t;
        }

        /// <summary>A round 2D light. squash below 1 flattens it into an ellipse lying on a surface.</summary>
        public static Light2D Light(Transform parent, Vector3 worldPos, float radius, Color color, float intensity, float squash = 1)
        {
            var go = new GameObject("light");
            go.transform.SetParent(parent, false);
            go.transform.position = worldPos;
            go.transform.localScale = new Vector3(1, squash, 1);
            var l = go.AddComponent<Light2D>();
            l.lightType = Light2D.LightType.Point;
            l.pointLightInnerRadius = 0;
            l.pointLightOuterRadius = radius;
            l.falloffIntensity = 0.7f;
            l.color = color;
            l.intensity = intensity;
            return l;
        }

        public static Clickable Click(GameObject go, Vector2 size, int order, System.Action onClick = null)
        {
            var c = go.AddComponent<Clickable>();
            c.size = size; c.order = order; c.onClick = onClick;
            return c;
        }
    }
}
