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

        /// <summary>x, y: art-space top-left corner.</summary>
        public static UiButton Create(Transform parent, string text, float x, float y, int order, Action onClick)
        {
            var go = Make.Root("Button " + text, parent).gameObject;
            go.transform.position = Px.Mid(x, y, W, H);
            var b = go.AddComponent<UiButton>();
            b.body = Make.Sprite(go.transform, "button_up", go.transform.position, order);
            b.label = Make.Text(go.transform, text, 0, 0, FontKind.Title, 16, Px.Hex("#fff6b8"), order + 2, Align.Centre, Px.Hex("#3b0d1e"));
            var c = Make.Click(go, new Vector2(W, H), order, onClick);
            c.onEnter = () => b.over = true;
            c.onExit = () => { b.over = false; b.down = false; };
            c.onDown = () => b.down = true;
            c.onUp = () => b.down = false;
            b.Update();
            return b;
        }

        void OnDisable() { over = down = false; }

        void Update()
        {
            body.sprite = Make.Lib.Sprite(down ? "button_down" : "button_up");
            body.color = over && !down ? new Color(1.25f, 1.15f, 1.05f, 1) : Color.white;
            label.transform.localPosition = new Vector3(0, down ? 8 : 10, 0);
        }
    }
}
