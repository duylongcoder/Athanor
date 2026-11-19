using System;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>A 120x26 red button with a label: brightens under the pointer, sinks when pressed.</summary>
    public sealed class UiButton : MonoBehaviour
    {
        public const int W = 120, H = 26;
        [SerializeField] SpriteRenderer body;
        [SerializeField] Label label;
        [SerializeField] bool disabled;
        /// <summary>The button's colours and label offsets, stored in each prefab that holds one.</summary>
        [Serializable]
        public struct Look
        {
            /// <summary>Body tint when disabled and when pointed at; label colour when disabled.</summary>
            public Color disabledTint, overTint, disabledText;
            /// <summary>The label's local y at rest and while pressed.</summary>
            public float labelUp, labelDown;
        }
        [SerializeField] Look look;
        bool over, down;
        /// <summary>Raised on a click while the button is enabled.</summary>
        public event Action Clicked;
        /// <summary>The button's label, for text that carries an argument.</summary>
        public Label Text => label;
        /// <summary>A greyed button: no hover, no press, and a click does nothing.</summary>
        public bool Disabled { get => disabled; set { disabled = value; if (value) over = down = false; if (label != null) label.Color = value ? look.disabledText : Art.Palette.buttonText; } }

        void Awake() { if (GetComponent<Clickable>() != null) Bind(); }

        /// <summary>Hooks this button's Clickable to it. Safe to call more than once: it assigns, it does not add.</summary>
        public void Bind()
        {
            var c = GetComponent<Clickable>();
            c.onClick = () => { if (!disabled) Clicked?.Invoke(); };
            c.onEnter = () => over = !disabled;
            c.onExit = () => { over = false; down = false; };
            c.onDown = () => down = !disabled;
            c.onUp = () => down = false;
        }

        void OnDisable() { over = down = false; }

        void Update()
        {
            body.sprite = Art.Lib.Sprite(down ? "button_down" : "button_up");
            body.color = disabled ? look.disabledTint : over && !down ? look.overTint : Color.white;
            label.transform.localPosition = new Vector3(0, down ? look.labelDown : look.labelUp, 0);
        }
    }
}
