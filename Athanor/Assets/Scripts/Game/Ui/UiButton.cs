using System;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>A 120x26 red button with a label: brightens under the pointer, sinks when pressed.</summary>
    public sealed class UiButton : MonoBehaviour
    {
        public const int W = 120, H = 26;
        SpriteRenderer body;
        Label label;
        bool over, down;
        /// <summary>The button's label, for text that carries an argument.</summary>
        public Label Text => label;
        bool disabled;
        /// <summary>A greyed button: no hover, no press, and a click does nothing.</summary>
        public bool Disabled { get => disabled; set { disabled = value; if (value) over = down = false; if (label != null) label.Color = value ? Px.Hex("#8a7a6a") : Px.Hex("#fff6b8"); } }

        /// <summary>key: the Lang key of the label, which follows the language. x, y: art-space top-left corner.</summary>
        public static UiButton Create(Transform parent, string key, float x, float y, int order, Action onClick)
        {
            var go = Make.Root("Button " + key, parent).gameObject;
            go.transform.position = Px.Mid(x, y, W, H);
            var b = go.AddComponent<UiButton>();
            b.body = Make.Sprite(go.transform, "button_up", go.transform.position, order);
            b.label = Make.TextKey(go.transform, key, 0, 0, FontKind.Title, 16, Px.Hex("#fff6b8"), order + 2, Align.Centre, Px.Hex("#3b0d1e"));
            var c = Make.Click(go, new Vector2(W, H), order, () => { if (!b.disabled) onClick?.Invoke(); });
            c.onEnter = () => b.over = !b.disabled;
            c.onExit = () => { b.over = false; b.down = false; };
            c.onDown = () => b.down = !b.disabled;
            c.onUp = () => b.down = false;
            b.Update();
            return b;
        }

        void OnDisable() { over = down = false; }

        void Update()
        {
            body.sprite = Art.Lib.Sprite(down ? "button_down" : "button_up");
            body.color = disabled ? new Color(0.45f, 0.42f, 0.42f, 1) : over && !down ? new Color(1.25f, 1.15f, 1.05f, 1) : Color.white;
            label.transform.localPosition = new Vector3(0, down ? 8 : 10, 0);
        }
    }
}
