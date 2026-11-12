using System;
using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Mouse or touch position in world space, press state for this frame, and hover/click dispatch to Clickables.</summary>
    public sealed class PointerInput : MonoBehaviour
    {
        public static Vector2 World { get; private set; }
        public static bool Down { get; private set; }
        public static bool Up { get; private set; }
        public static bool Held { get; private set; }
        public static Clickable Hover { get; private set; }
        /// <summary>Raised on every press, whatever is under it (used for click-to-skip).</summary>
        public static event Action AnyPress;

        static readonly List<Clickable> all = new List<Clickable>();
        static Clickable pressedOn;

        public static void Register(Clickable c) { if (!all.Contains(c)) all.Add(c); }
        public static void Unregister(Clickable c) { all.Remove(c); if (Hover == c) Hover = null; if (pressedOn == c) pressedOn = null; }

        /// <summary>The game is drawn at a whole multiple of 640x360, centred; this undoes that.</summary>
        public static Vector2 ScreenToWorld(Vector2 screen)
        {
            float scale = Mathf.Max(1, Mathf.Floor(Mathf.Min(Screen.width / (float)Px.W, Screen.height / (float)Px.H)));
            float x = (screen.x - (Screen.width - Px.W * scale) / 2f) / scale - Px.W / 2f;
            float y = (screen.y - (Screen.height - Px.H * scale) / 2f) / scale - Px.H / 2f;
            return new Vector2(x, y);
        }

        void Update()
        {
            var p = UnityEngine.InputSystem.Pointer.current;
            if (p == null) { Down = Up = Held = false; return; }
            World = ScreenToWorld(p.position.ReadValue());
            Down = p.press.wasPressedThisFrame;
            Up = p.press.wasReleasedThisFrame;
            Held = p.press.isPressed;

            Clickable top = null;
            foreach (var c in all)
                if (c.isActiveAndEnabled && c.Contains(World) && (top == null || c.order > top.order)) top = c;
            if (top != Hover)
            {
                var old = Hover; Hover = top;
                old?.onExit?.Invoke();
                top?.onEnter?.Invoke();
            }
            if (Down)
            {
                pressedOn = Hover;
                Hover?.onDown?.Invoke();
                AnyPress?.Invoke();
            }
            if (Up)
            {
                var was = pressedOn; pressedOn = null;
                if (was != null) { was.onUp?.Invoke(); if (was == Hover) was.onClick?.Invoke(); }
            }
        }
    }
}
