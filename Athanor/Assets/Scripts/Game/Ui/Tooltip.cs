using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// A small panel with a title and two lines of text that appears beside whatever the pointer rests on.
    /// A hand card's tip adds a row under the name with its stars and tier word, and the panel grows by that row (SPEC 23).
    /// </summary>
    public sealed class Tooltip : MonoBehaviour
    {
        const int W = 176, BaseH = 58, RowH = 12, Order = 400;
        [SerializeField] Label title, body, tier;
        [SerializeField] SpriteRenderer panel;
        [SerializeField] RarityStars stars;
        int h = BaseH;

        /// <summary>
        /// The panel's 9-slice sprite (the panel stretches only in its middle, so the taller tip keeps the same border). It is made by
        /// Sprite.Create, which no prefab can keep, so a baked tooltip makes it here; the owner calls this once after instantiating it.
        /// Does nothing when the panel already has it.
        /// </summary>
        public void Bind()
        {
            if (panel.sprite != null && panel.sprite.border != Vector4.zero) return;
            var art = Art.Lib.Sprite("tooltip");
            panel.sprite = Sprite.Create(art.texture, art.textureRect, new Vector2(0.5f, 0.5f), art.pixelsPerUnit, 0, SpriteMeshType.FullRect, new Vector4(4, 4, 4, 4));
            panel.drawMode = SpriteDrawMode.Sliced;
            panel.size = new Vector2(W, BaseH);
        }

        public void Build(Transform parent)
        {
            transform.SetParent(parent, false);
            panel = Make.Sprite(transform, "tooltip", Vector3.zero, Order);
            panel.transform.localPosition = Vector3.zero;
            Bind();
            title = Make.Text(transform, "", 0, 0, FontKind.Title, 16, Px.Hex("#f0dcc0"), Order + 2);
            body = Make.Text(transform, "", 0, 0, FontKind.Body, 16, Px.Hex("#c8ac84"), Order + 2, Align.Left, null, W - 14);
            tier = Make.Text(transform, "", 0, 0, FontKind.Name, 16, Color.white, Order + 2);
            stars = new RarityStars(transform, Order + 2);
            gameObject.SetActive(false);
        }

        /// <summary>Shows the tip above the anchor (below it when there is no room), kept on screen. A rarity adds the tier row.</summary>
        public void Show(string heading, string text, Vector2 anchor, float clearance = 20, string rarity = null)
        {
            bool row = rarity != null;
            h = BaseH + (row ? RowH : 0);
            panel.size = new Vector2(W, h);
            title.Text = heading; body.Text = text;
            body.main.lineSpacing = text != null && text.Length > 62 ? -30 : 0;      // a third line fits when the lines sit closer
            title.transform.localPosition = new Vector3(-W / 2 + 8, h / 2f - 4, 0);
            body.transform.localPosition = new Vector3(-W / 2 + 8, h / 2f - 21 - (row ? RowH : 0), 0);
            stars.SetActive(row); tier.gameObject.SetActive(row);
            if (row)
            {
                int n = RarityStars.Count(rarity);
                stars.Root.localPosition = new Vector3(-W / 2 + 8, h / 2f - 22, 0);
                stars.Show(rarity);
                tier.Text = Wording.RarityWord(rarity); // owner decision 4a: the brighter rarity colour here, not SPEC 23's dark tone (about 2.4:1 on the dark panel)
                tier.Color = Wording.RarityColor(rarity);
                tier.transform.localPosition = new Vector3(-W / 2 + 8 + RarityStars.WidthOf(n) + 4, h / 2f - 19, 0);
            }
            float y = anchor.y + clearance + h / 2f;
            if (y + h / 2f > Px.H / 2f - 2) y = anchor.y - clearance - h / 2f;
            float x = Mathf.Clamp(anchor.x, -Px.W / 2f + W / 2f + 2, Px.W / 2f - W / 2f - 2);
            transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
            gameObject.SetActive(true);
        }

        public void Hide() => gameObject.SetActive(false);
    }
}
