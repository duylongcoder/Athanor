using System;
using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Mouse or touch position in world space, press state for this frame, and hover/click dispatch to Clickables.</summary>
    public sealed class PointerInput : MonoBehaviour
    {
        public static Vector2 World { get; private set; }
        /// <summary>The depth rig, when the bench is drawn by the perspective world camera (null: the flat game).</summary>
        public static DepthRig Rig;
        /// <summary>The pointer on the bench table, in flat coordinates (what a tile tests): found by the world ray, so a tile is
        /// hit where it is drawn, under sway and shake. Equals <see cref="World"/> without a rig or in a flat cutscene.</summary>
        public static Vector2 Bench { get; private set; }
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

        /// <summary>Sets <see cref="World"/> and finds <see cref="Bench"/> from it (Update's step, separate so tests can drive it).</summary>
        public static void Resolve(Vector2 world)
        {
            World = world;
            Bench = World;
            if (Rig != null && !Rig.Flat && Rig.BenchPoint(World, out var flat)) Bench = flat;
        }

        void Update()
        {
            var p = UnityEngine.InputSystem.Pointer.current;
            if (p == null) { Down = Up = Held = false; return; }
            Resolve(ScreenToWorld(p.position.ReadValue()));
            Down = p.press.wasPressedThisFrame;
            Up = p.press.wasReleasedThisFrame;
            Held = p.press.isPressed;

            Clickable top = null;
            foreach (var c in all)
                if (c.isActiveAndEnabled && c.Contains(World) && (top == null || c.order > top.order)) top = c;
            if (top != Hover)
            {
                var old = Hover; Hover = top;
                if (old != null && old.onExit != null) old.onExit();
                if (top != null && top.onEnter != null) top.onEnter();
            }
            if (Down)
            {
                pressedOn = Hover;
                if (Hover != null && Hover.onDown != null) Hover.onDown();
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
