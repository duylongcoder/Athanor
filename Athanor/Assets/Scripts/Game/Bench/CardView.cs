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

        SpriteRenderer frame, glow, icon;
        Label line1, line2, kind;
        float lift, delay;
        int baseOrder;

        static readonly Color Ink = Px.Hex("#3a2212");

        public void Build(MaterialData m, Transform parent, int order)
        {
            transform.SetParent(parent, false);
            id = m.id; baseOrder = order;
            // children are laid out around the card centre; the frame sprite carries a 3x4 drop shadow on its right and bottom
            frame = Make.Sprite(transform, "card", Vector3.zero, order);
            frame.transform.localPosition = new Vector3(1.5f, -2, 0);
            glow = Make.Glow(transform, Vector3.zero, 18, string.IsNullOrEmpty(m.glow) ? Color.clear : Px.Hex(m.glow, 0.45f), order + 1);
            glow.transform.localPosition = new Vector3(0, 15, 0);
            glow.transform.localScale = new Vector3(0.75f, 0.42f, 1);
            icon = Make.Sprite(transform, m.id, Vector3.zero, order + 2);
            icon.transform.localPosition = new Vector3(0, 15, 0);
            var words = m.name.Split(' ');
            string a = words[0], b = words.Length > 1 ? string.Join(" ", words, 1, words.Length - 1) : null;
            line1 = Text(a, b == null ? -3 : 2, FontKind.Name, 16, Ink);
            if (b != null) line2 = Text(b, -8, FontKind.Name, 16, Ink);
            kind = Text(m.kind.ToUpperInvariant(), -24, FontKind.Tiny, 8, Px.Hex(KindColors.Of(m.kind)));
        }

        Label Text(string s, float y, FontKind font, int size, Color c)
        {
            var l = Make.Text(transform, s, 0, 0, font, size, c, baseOrder + 3, Align.Centre);
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
            frame.sprite = Make.Lib.Sprite(Selected ? "card_sel" : "card");
            float a = Dimmed ? 0.35f : 1;
            frame.color = icon.color = Color.white.Alpha(a);
            line1.Alpha = a; if (line2 != null) line2.Alpha = a; kind.Alpha = a;
        }
    }
}
