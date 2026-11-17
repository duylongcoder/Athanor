using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Spawns exported pixelfx effects by name.</summary>
    public static class Fx
    {
        /// <summary>Plays the named effect once at a world position. Returns null when that effect has not been exported.</summary>
        public static Flipbook Spawn(string name, Vector2 pos, Transform parent = null, int sortingOrder = 50)
        {
            var sheet = Art.Lib.Sheet(name);
            if (sheet == null) return null;
            var go = new GameObject("fx " + name);
            go.transform.SetParent(parent, false);
            go.transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), 0);
            var r = go.AddComponent<SpriteRenderer>();
            r.sharedMaterial = Art.Lib.glow;
            r.sortingOrder = sortingOrder;
            var book = go.AddComponent<Flipbook>();
            book.destroyOnEnd = !sheet.loop;
            book.Play(sheet);
            return book;
        }
    }
}
