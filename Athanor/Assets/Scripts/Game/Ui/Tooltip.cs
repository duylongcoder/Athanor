using UnityEngine;

namespace Athanor.Game
{
    /// <summary>A small panel with a title and two lines of text that appears beside whatever the pointer rests on.</summary>
    public sealed class Tooltip : MonoBehaviour
    {
        const int W = 176, H = 58, Order = 400;
        Label title, body;

        public void Build(Transform parent)
        {
            transform.SetParent(parent, false);
            Make.Sprite(transform, "tooltip", Vector3.zero, Order).transform.localPosition = Vector3.zero;
            title = Make.Text(transform, "", 0, 0, FontKind.Title, 16, Px.Hex("#f0dcc0"), Order + 2);
            title.transform.localPosition = new Vector3(-W / 2 + 8, H / 2 - 4, 0);
            body = Make.Text(transform, "", 0, 0, FontKind.Body, 16, Px.Hex("#c8ac84"), Order + 2, Align.Left, null, W - 14);
            body.transform.localPosition = new Vector3(-W / 2 + 8, H / 2 - 21, 0);
            gameObject.SetActive(false);
        }

        /// <summary>Shows the tip above the anchor (below it when there is no room), kept on screen.</summary>
        public void Show(string heading, string text, Vector2 anchor, float clearance = 20)
        {
            title.Text = heading; body.Text = text;
            body.main.lineSpacing = text != null && text.Length > 62 ? -30 : 0;      // a third line fits when the lines sit closer
            float y = anchor.y + clearance + H / 2f;
            if (y + H / 2f > Px.H / 2f - 2) y = anchor.y - clearance - H / 2f;
            float x = Mathf.Clamp(anchor.x, -Px.W / 2f + W / 2f + 2, Px.W / 2f - W / 2f - 2);
            transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
            gameObject.SetActive(true);
        }

        public void Hide() => gameObject.SetActive(false);
    }
}
