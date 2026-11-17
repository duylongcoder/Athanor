using System.Collections.Generic;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// A small panel at the top of the screen that says a goal was completed and what it unlocked.
    /// Toasts queue; each stays 2.5 seconds or until clicked. It never blocks anything under it but itself.
    /// </summary>
    public sealed class GoalToast : MonoBehaviour
    {
        const int O = 490, W = 300, H = 36, X = 170, Y = 4;
        const float Stay = 2.5f, Slide = 0.25f;

        struct Item { public string goal, reward, sprite; }

        readonly Queue<Item> queue = new Queue<Item>();
        Transform panel;
        SpriteRenderer icon;
        Label title, line;
        float age = -1;

        public void Build()
        {
            panel = Make.Root("Panel", transform);
            panel.position = Px.Mid(X, Y, W, H);
            Make.Sprite(panel, "toast", panel.position, O);
            icon = Make.Sprite(panel, "dot", Vector3.zero, O + 2);
            icon.transform.localPosition = new Vector3(-W / 2 + 19, 0, 0);
            title = Make.Text(panel, "", 0, 0, FontKind.Tiny, 8, Px.Hex("#f8d250"), O + 2);
            title.transform.localPosition = new Vector3(-W / 2 + 38, H / 2 - 6, 0);
            line = Make.Text(panel, "", 0, 0, FontKind.Title, 16, Px.Hex("#f0dcc0"), O + 2, Align.Left, Px.Hex("#0c0706"));
            line.transform.localPosition = new Vector3(-W / 2 + 38, H / 2 - 15, 0);
            Make.Click(panel.gameObject, new Vector2(W, H), O, () => { if (age >= 0) age = Mathf.Max(age, Stay + Slide); });
            panel.gameObject.SetActive(false);
        }

        /// <summary>Queues one toast for each goal just completed.</summary>
        public void Add(IEnumerable<GoalReward> rewards, ContentDb db)
        {
            foreach (var r in rewards)
                queue.Enqueue(new Item
                {
                    goal = Loc.T("ui.toast.goal", ("goal", Loc.Upper(Wording.GoalText(r.Goal)))),
                    reward = r.Item == null ? Loc.T("ui.toast.nothing") : Loc.T("ui.toast.unlocked", ("item", Wording.ItemName(db, r.Item))),
                    sprite = r.Item == null ? null : Wording.ItemSprite(db, r.Item),
                });
        }

        /// <summary>Drops whatever is showing or waiting (the profile was reset).</summary>
        public void Clear() { queue.Clear(); age = -1; panel.gameObject.SetActive(false); }

        void Update()
        {
            if (age < 0)
            {
                if (queue.Count == 0) return;
                var next = queue.Dequeue();
                title.Text = next.goal; line.Text = next.reward;
                icon.enabled = next.sprite != null;
                if (next.sprite != null) icon.sprite = Art.Lib.Sprite(next.sprite);
                age = 0;
                panel.gameObject.SetActive(true);
                if (Sparks.I != null) Sparks.I.Burst(Px.Mid(X, Y, W, H), 18, new Color(2.2f, 1.7f, 0.6f), 90, 0.6f, O + 3, 20);
            }
            age += Time.deltaTime;
            // slides down from above the screen, waits, slides back up
            float shown = age < Slide ? Anim.OutCubic(age / Slide) : age < Slide + Stay ? 1 : 1 - Anim.InCubic(Mathf.Clamp01((age - Slide - Stay) / Slide));
            panel.position = Px.Snap(Px.Mid(X, Y - (1 - shown) * (H + Y + 2), W, H));
            if (age >= Stay + 2 * Slide) { age = -1; panel.gameObject.SetActive(false); }
        }
    }
}
