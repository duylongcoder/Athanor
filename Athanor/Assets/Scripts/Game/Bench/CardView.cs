using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>A starting material in the hand, drawn as a parchment card.</summary>
    public sealed class CardView : MonoBehaviour
    {
        public const int W = 74, H = 70;
        public int handIndex;
        public string id;
        public Vector3 home;              // world position of the card's centre when at rest
        public bool Lifted, Selected, Dimmed;

        SpriteRenderer frame, glow, icon, pip;
        Color pipColor;
        Label line1, line2, kind;
        Transform lettering, picture;         // the lettering and the icon, which slide left while the next card covers this one's right
        float lift, delay, shift, slid;
        int baseOrder, frontOrder, shownOrder;

        static readonly Color Ink = Px.Hex("#3a2212");

        public void Build(MaterialData m, Transform parent, int order)
        {
            transform.SetParent(parent, false);
            id = m.id; baseOrder = order;
            // children are laid out around the card centre; the frame sprite carries a 3x4 drop shadow on its right and bottom
            frame = Make.Sprite(transform, "card", Vector3.zero, order);
            frame.transform.localPosition = new Vector3(1.5f, -2, 0);
            lettering = Make.Root("lettering", transform);
            picture = Make.Root("picture", transform);
            glow = Make.Glow(picture, Vector3.zero, 18, string.IsNullOrEmpty(m.glow) ? Color.clear : Px.Hex(m.glow, 0.45f), order + 1);
            glow.transform.localPosition = new Vector3(0, 15, 0);
            glow.transform.localScale = new Vector3(0.75f, 0.42f, 1);
            icon = Make.Sprite(picture, m.id, Vector3.zero, order + 2);
            icon.transform.localPosition = new Vector3(0, 15, 0);
            var words = m.name.Split(' ');
            string a = words[0], b = words.Length > 1 ? string.Join(" ", words, 1, words.Length - 1) : null;
            line1 = Text(a, b == null ? -3 : 2, FontKind.Name, 16, Ink);
            if (b != null) line2 = Text(b, -8, FontKind.Name, 16, Ink);
            kind = Text(m.kind.ToUpperInvariant(), -24, FontKind.Tiny, 8, Px.Hex(KindColors.Of(m.kind)));
            // rarity: a pip in the corner (grey for the eight everyone starts with)
            pip = Make.Sprite(transform, "pip", Vector3.zero, order + 3);
            pip.transform.localPosition = new Vector3(-W / 2 + 6, H / 2 - 6, 0);
            pipColor = Wording.RarityColor(m.rarity);
            pip.color = pipColor;
            shownOrder = order;
        }

        /// <summary>
        /// In a hand too wide to lay side by side the next card covers this one's right. visible is the width left
        /// showing: the name and the icon move to the middle of it. While the card is pointed at or picked it comes
        /// to the front (order front) and reads as a whole card again.
        /// </summary>
        public void Fan(float visible, int front)
        {
            // Never so far that the name starts off the card's left edge, over the card before. (Up to the edge itself:
            // in a hand of six an eight-letter name is as wide as the part of the card that shows.)
            float widest = Mathf.Max(line1.Width, line2 != null ? line2.Width : 0, kind.Width);
            shift = slid = Mathf.Max(0, Mathf.Min((W - visible) / 2f, W / 2f + 1 - widest / 2f));
            frontOrder = front;
            Slide();
        }

        void Slide()
        {
            lettering.localPosition = new Vector3(-Mathf.Round(slid), 0, 0);
            picture.localPosition = new Vector3(-Mathf.Round(Mathf.Min(slid, 10)), 0, 0);   // the icon stays inside its window
        }

        void SetOrder(int order)
        {
            if (order == shownOrder) return;
            shownOrder = order;
            frame.sortingOrder = order; glow.sortingOrder = order + 1; icon.sortingOrder = order + 2; pip.sortingOrder = order + 3;
            line1.Order = order + 3; if (line2 != null) line2.Order = order + 3; kind.Order = order + 3;
        }

        Label Text(string s, float y, FontKind font, int size, Color c)
        {
            var l = Make.Text(lettering, s, 0, 0, font, size, c, baseOrder + 3, Align.Centre);
            l.transform.localPosition = new Vector3(0, y, 0);
            return l;
        }

        /// <summary>Starts below the screen and slides up after a short stagger.</summary>
        public void DealIn(float wait) { delay = wait; transform.position = home + new Vector3(0, -90, 0); }

        public bool Contains(Vector2 world)
        {
            Vector2 c = transform.position;
            return Mathf.Abs(world.x - c.x) <= W / 2f && world.y <= c.y + H / 2f && world.y >= -Px.H / 2f;
        }

        void Update()
        {
            if (delay > 0) { delay -= Time.deltaTime; return; }
            lift = Mathf.MoveTowards(lift, Selected ? 14 : Lifted ? 9 : 0, Time.deltaTime * 90);
            var target = home + new Vector3(0, lift + (Selected ? Mathf.Round(Mathf.Sin(Time.time * 3) * 1.5f) : 0), 0);
            transform.position = Px.Snap(Vector3.Lerp(transform.position, target, 1 - Mathf.Exp(-Time.deltaTime * 18)));
            bool front = Selected || Lifted;
            slid = Mathf.MoveTowards(slid, front ? 0 : shift, Time.deltaTime * 160);
            Slide();
            SetOrder(front && frontOrder > 0 ? frontOrder : baseOrder);
            frame.sprite = Make.Lib.Sprite(Selected ? "card_sel" : "card");
            float a = Dimmed ? 0.35f : 1;
            frame.color = icon.color = Color.white.Alpha(a);
            pip.color = pipColor.Alpha(a);
            line1.Alpha = a; if (line2 != null) line2.Alpha = a; kind.Alpha = a;
        }
    }
}
