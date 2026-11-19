using System;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The rarity glyph (SPEC changes 21-24): up to four 7x7 stars on an 8 px pitch, tinted, each with a dark outline made of
    /// four offset dark copies. The root sits at the top-left of the row. Built once and reused; showing a rarity allocates nothing.
    /// Serializable so a prefab keeps its rows; the corner-pivot glyph is made at runtime and set when stars are shown.
    /// </summary>
    [Serializable]
    public sealed class RarityStars
    {
        public const int Size = 7, Pitch = 8, Max = 4;
        static readonly Color OutlineColor = Px.Hex("#1a0e08");
        static readonly Vector2[] Offsets = { new Vector2(-1, 0), new Vector2(1, 0), new Vector2(0, -1), new Vector2(0, 1) };

        [SerializeField] SpriteRenderer[] fill = new SpriteRenderer[Max], edge = new SpriteRenderer[Max * 4];
        [SerializeField] Transform root;
        Color tint = Color.white; float alpha = 1; int shown;

        /// <summary>Where the row's top-left corner is (world position; the row hangs to the right and down).</summary>
        public Transform Root => root;

        /// <summary>For the serializer: a row stored in a prefab is filled in from it.</summary>
        public RarityStars() { }

        public RarityStars(Transform parent, int order)
        {
            root = Spawn.Root("stars", parent);
            for (int i = 0; i < Max; i++)
            {
                var corner = new Vector3(i * Pitch, 0, 0);   // the sprite hangs from its top-left corner, so every edge is on a whole pixel
                for (int k = 0; k < 4; k++)
                {
                    var e = edge[i * 4 + k] = Spawn.Sprite(root, "star", Vector3.zero, order);
                    e.transform.localPosition = corner + (Vector3)Offsets[k];
                }
                fill[i] = Spawn.Sprite(root, "star", Vector3.zero, order + 1);
                fill[i].transform.localPosition = corner;
            }
            Show(0, Color.white);
        }

        static Sprite source, hanging;

        /// <summary>
        /// The star sprite with its pivot at the top-left corner. An odd-sized sprite pivoted at its centre sits on half pixels,
        /// which the pixel-perfect camera rounds unevenly (a lost row, a doubled row); from a corner it always lands whole.
        /// </summary>
        static Sprite Glyph()
        {
            var art = Art.Lib.Sprite("star");
            if (hanging == null || source != art)
            {
                source = art;
                hanging = Sprite.Create(art.texture, art.textureRect, new Vector2(0, 1), art.pixelsPerUnit, 0, SpriteMeshType.FullRect);
                hanging.name = "star_corner";
            }
            return hanging;
        }

        /// <summary>1 Common, 2 Uncommon, 3 Rare, 4 Legendary.</summary>
        public static int Count(string rarity) => Rarities.Rank(rarity) + 1;

        /// <summary>Width of n stars in pixels.</summary>
        public static int WidthOf(int n) => n <= 0 ? 0 : (n - 1) * Pitch + Size;

        /// <summary>The stars of a rarity in its colour.</summary>
        public void Show(string rarity) => Show(Count(rarity), Wording.RarityColor(rarity));

        /// <summary>n stars (0 hides the row) in a colour.</summary>
        public void Show(int n, Color color)
        {
            shown = Mathf.Clamp(n, 0, Max); tint = color;
            Apply();
        }

        /// <summary>Fades the row with its card.</summary>
        public void SetAlpha(float a)
        {
            if (Mathf.Approximately(a, alpha)) return;
            alpha = a; Apply();
        }

        /// <summary>Outline at order, stars above it.</summary>
        public void SetOrder(int order)
        {
            for (int i = 0; i < edge.Length; i++) edge[i].sortingOrder = order;
            for (int i = 0; i < fill.Length; i++) fill[i].sortingOrder = order + 1;
        }

        public void SetActive(bool on) => root.gameObject.SetActive(on);

        void Apply()
        {
            var glyph = shown > 0 ? Glyph() : null;     // not baked: Sprite.Create makes no asset a prefab can keep
            for (int i = 0; i < Max; i++)
            {
                bool on = i < shown;
                fill[i].enabled = on; fill[i].color = tint.Alpha(alpha);
                if (on) fill[i].sprite = glyph;
                for (int k = 0; k < 4; k++)
                {
                    var e = edge[i * 4 + k];
                    e.enabled = on; e.color = OutlineColor.Alpha(alpha);
                    if (on) e.sprite = glyph;
                }
            }
        }
    }
}
