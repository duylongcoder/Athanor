using System;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// A small panel with a title and two lines of text that appears beside whatever the pointer rests on.
    /// A hand card's tip adds a row under the name with its stars and tier word, and the panel grows by that row (SPEC 23).
    /// </summary>
    public sealed class Tooltip : MonoBehaviour
    {
        /// <summary>The panel's width, its height without the tier row, and the tier row's height (px), stored in the Bench prefab.</summary>
        [Serializable]
        public struct Size { public int width, baseHeight, tierRow; }
        [SerializeField] Size size;
        [SerializeField] Label title, body, tier;
        [SerializeField] SpriteRenderer panel;
        [SerializeField] RarityStars stars;
        /// <summary>The tooltip's inner offsets (px), stored in the Bench prefab.</summary>
        [Serializable]
        public struct Layout
        {
            /// <summary>Left padding, and the drops of the title, body, stars and tier word from the panel's top.</summary>
            public float pad, titleDy, bodyDy, starsDy, tierDy;
            /// <summary>The gap between the stars and the tier word; the margin kept to the screen edge.</summary>
            public float tierGap, edgeMargin;
            /// <summary>The 9-slice border, and the panel sprite's pivot.</summary>
            public float border; public Vector2 pivot;
            /// <summary>A body longer than this many characters sits tighter, at this line spacing.</summary>
            public int longText; public float tightSpacing;
        }
        [SerializeField] Layout layout;
        int h;

        /// <summary>
        /// The panel's 9-slice sprite (the panel stretches only in its middle, so the taller tip keeps the same border). It is made by
        /// Sprite.Create, which no prefab can keep, so a tooltip loaded from a prefab makes it here; the owner calls this once after instantiating it.
        /// Does nothing when the panel already has it.
        /// </summary>
        public void Bind()
        {
            h = size.baseHeight;
            if (panel.sprite != null && panel.sprite.border != Vector4.zero) return;
            var art = Art.Lib.Sprite("tooltip");
            panel.sprite = Sprite.Create(art.texture, art.textureRect, layout.pivot, art.pixelsPerUnit, 0, SpriteMeshType.FullRect, new Vector4(layout.border, layout.border, layout.border, layout.border));
            panel.drawMode = SpriteDrawMode.Sliced;
            panel.size = new Vector2(size.width, size.baseHeight);
        }

        /// <summary>Shows the tip above the anchor (below it when there is no room), kept on screen. A rarity adds the tier row.</summary>
        public void Show(string heading, string text, Vector2 anchor, float clearance = 20, string rarity = null)
        {
            bool row = rarity != null;
            h = size.baseHeight + (row ? size.tierRow : 0);
            panel.size = new Vector2(size.width, h);
            title.Text = heading; body.Text = text;
            body.main.lineSpacing = text != null && text.Length > layout.longText ? layout.tightSpacing : 0;      // a third line fits when the lines sit closer
            int left = -size.width / 2; float top = h / 2f;   // the panel's left edge and top, from its centre
            title.transform.localPosition = new Vector3(left + layout.pad, top - layout.titleDy, 0);
            body.transform.localPosition = new Vector3(left + layout.pad, top - layout.bodyDy - (row ? size.tierRow : 0), 0);
            stars.SetActive(row); tier.gameObject.SetActive(row);
            if (row)
            {
                int n = RarityStars.Count(rarity);
                stars.Root.localPosition = new Vector3(left + layout.pad, top - layout.starsDy, 0);
                stars.Show(rarity);
                tier.Text = Wording.RarityWord(rarity); // owner decision 4a: the brighter rarity colour here, not SPEC 23's dark tone (about 2.4:1 on the dark panel)
                tier.Color = Wording.RarityColor(rarity);
                tier.transform.localPosition = new Vector3(left + layout.pad + RarityStars.WidthOf(n) + layout.tierGap, top - layout.tierDy, 0);
            }
            float y = anchor.y + clearance + h / 2f;
            if (y + h / 2f > Px.H / 2f - layout.edgeMargin) y = anchor.y - clearance - h / 2f;
            float x = Mathf.Clamp(anchor.x, -Px.W / 2f + size.width / 2f + layout.edgeMargin, Px.W / 2f - size.width / 2f - layout.edgeMargin);
            transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
            gameObject.SetActive(true);
        }

        public void Hide() => gameObject.SetActive(false);
    }
}
