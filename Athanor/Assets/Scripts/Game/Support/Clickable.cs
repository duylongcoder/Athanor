using System;
using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>A rectangle around this object that the pointer can hover and click. The highest order under the pointer wins.
    /// Holes (world-space rectangles) are cut out of it: the pointer there is not on this object, so it reaches what is underneath.</summary>
    public sealed class Clickable : MonoBehaviour
    {
        public Vector2 size, offset;
        public int order;
        /// <summary>World-space rectangles that are not part of this one (the tutorial's lit windows). Empty: a solid rectangle.</summary>
        public readonly List<Rect> holes = new List<Rect>();
        public Action onClick, onEnter, onExit, onDown, onUp;

        public bool Contains(Vector2 world)
        {
            Vector2 c = (Vector2)transform.position + offset;
            if (Mathf.Abs(world.x - c.x) > size.x / 2 || Mathf.Abs(world.y - c.y) > size.y / 2) return false;
            for (int i = 0; i < holes.Count; i++) if (holes[i].Contains(world)) return false;
            return true;
        }

        void OnEnable() => PointerInput.Register(this);
        void OnDisable() => PointerInput.Unregister(this);
    }
}
