using System;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>A rectangle around this object that the pointer can hover and click. The highest order under the pointer wins.</summary>
    public sealed class Clickable : MonoBehaviour
    {
        public Vector2 size, offset;
        public int order;
        public Action onClick, onEnter, onExit, onDown, onUp;

        public bool Contains(Vector2 world)
        {
            Vector2 c = (Vector2)transform.position + offset;
            return Mathf.Abs(world.x - c.x) <= size.x / 2 && Mathf.Abs(world.y - c.y) <= size.y / 2;
        }

        void OnEnable() => PointerInput.Register(this);
        void OnDisable() => PointerInput.Unregister(this);
    }
}
