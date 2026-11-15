using System;
using System.Collections.Generic;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The Goals page: every long-term goal with its progress, grouped by the rarity of what it unlocks.
    /// Before a goal is done its reward is named only by kind and rarity; afterwards the item it gave is shown.
    /// </summary>
    public sealed class GoalsView : MonoBehaviour
    {
        // the numbers match GOALS in tools/art/ui.mjs
        const int O = 470, X = 20, Y = 15, LeftX = 14, RightX = 304, Top = 30, ColW = 282, RowH = 20, HeadH = 14, Pad = 10, BarW = 92;

        public event Action Closed;
        public bool IsOpen => gameObject.activeSelf;

        sealed class Row
        {
            public Transform root; public Label text, count, reward; public SpriteRenderer track, fill, icon;
        }

        ContentDb db;
        readonly List<Row> rows = new List<Row>();
        readonly Label[] heads = new Label[3];
        Label found;

        static readonly Color Ink = Px.Hex("#3a2212"), Faded = Px.Hex("#7a5430"), Teal = Px.Hex("#11606a"), Pale = Px.Hex("#f0dcc0"), Dark = Px.Hex("#0c0706");
        // which column each rarity's section is in, and in what order: Uncommon and Legendary on the left, Rare on the right
        static readonly string[] Sections = { Rarities.Uncommon, Rarities.Legendary, Rarities.Rare };

        public void Build(ContentDb content)
        {
            db = content;
            var t = transform;
            var shade = Make.Rect(t, 0, 0, Px.W, Px.H, new Color(0, 0, 0, 0.97f), O - 1);
            Make.Click(shade.gameObject, new Vector2(Px.W, Px.H), O - 1, Close);            // a click outside the page closes it
            var panel = Make.SpriteAt(t, "goals", X, Y, O);
            Make.Click(panel.gameObject, new Vector2(600, 330), O);
            Make.TextKey(t, "ui.goals.title", X + 18, Y + 7, FontKind.Title, 16, Pale, O + 2, Align.Left, Dark);
            found = Make.Text(t, "", X + 300, Y + 11, FontKind.Tiny, 8, Px.Hex("#a88a6a"), O + 2, Align.Centre);
            var close = Make.TextKey(t, "ui.common.close", X + 582, Y + 7, FontKind.Title, 16, Pale, O + 2, Align.Right, Dark);
            Make.Click(close.gameObject, new Vector2(60, 22), O + 3, Close).offset = new Vector2(-22, -8);
            for (int i = 0; i < heads.Length; i++) heads[i] = Make.Text(t, "", 0, 0, FontKind.Tiny, 8, Faded, O + 2);
            for (int i = 0; i < db.Goals.Count; i++)
            {
                var r = new Row { root = Make.Root("Goal " + i, t) };
                r.text = Make.Text(r.root, "", 0, 0, FontKind.Body, 16, Ink, O + 2);
                // two lines to a row: the goal and its count, then the reward and the bar. Body lettering sits 4 below its top edge
                r.text.transform.localPosition = new Vector3(0, 3, 0);
                r.count = Make.Text(r.root, "", 0, 0, FontKind.Tiny, 8, Faded, O + 2, Align.Right);
                r.count.transform.localPosition = new Vector3(ColW - 2 * Pad, -2, 0);
                r.reward = Make.Text(r.root, "", 0, 0, FontKind.Tiny, 8, Faded, O + 2);
                r.reward.transform.localPosition = new Vector3(0, -11, 0);
                r.track = Bar(r.root, Px.Hex("#b89860"));
                r.fill = Bar(r.root, Px.Hex("#c8501e"));
                r.icon = Make.Sprite(r.root, "dot", Vector3.zero, O + 3);
                r.icon.transform.localPosition = new Vector3(ColW - 2 * Pad - 12, -RowH / 2f + 1, 0);
                rows.Add(r);
            }
            gameObject.SetActive(false);
        }

        static SpriteRenderer Bar(Transform parent, Color color)
        {
            var r = Make.Sprite(parent, "dot", Vector3.zero, O + 2);
            r.name = "bar"; r.color = color;
            return r;
        }

        /// <summary>A bar of the given width whose left end is BarW short of the row's right edge, 13 down from the row's top.</summary>
        static void SetBar(SpriteRenderer bar, float width)
        {
            bar.enabled = width >= 1;
            bar.transform.localScale = new Vector3(width / 4f, 3 / 4f, 1);
            bar.transform.localPosition = new Vector3(ColW - 2 * Pad - BarW + width / 2f, -14.5f, 0);
        }

        public void Open(Profile profile, Journal journal)
        {
            int done = 0, used = 0;
            float[] y = { Y + Top + 6, Y + Top + 6 };       // the next free line of each column
            for (int s = 0; s < Sections.Length; s++)
            {
                int col = Sections[s] == Rarities.Rare ? 1 : 0, x = X + (col == 0 ? LeftX : RightX) + Pad;
                // goals still open first, in the order of the content; the finished ones after them
                var open = new List<GoalData>(); var finished = new List<GoalData>();
                foreach (var g in db.Goals)
                    if (g.rewardRarity == Sections[s]) (profile.IsComplete(g.id) ? finished : open).Add(g);
                if (open.Count + finished.Count == 0) { heads[s].Text = ""; continue; }
                heads[s].Text = Loc.T("ui.goals.section", ("rarity", Loc.Upper(Loc.Rarity(Sections[s]))), ("done", finished.Count), ("all", open.Count + finished.Count));
                heads[s].Color = Wording.RarityColor(Sections[s]).Times(0.55f).Alpha(1);
                heads[s].transform.position = Px.At(x, y[col]);
                y[col] += HeadH;
                open.AddRange(finished);
                foreach (var g in open)
                {
                    var r = rows[used++];
                    bool complete = profile.IsComplete(g.id);
                    long progress = profile.Progress(g, db, journal);
                    r.root.gameObject.SetActive(true);
                    r.root.position = Px.At(x, y[col]);
                    r.text.Text = Wording.GoalText(g); r.text.Color = complete ? Faded : Ink;
                    string item = profile.RewardOf(g.id);
                    r.count.Text = complete ? "" : Wording.Number(progress) + " / " + Wording.Number(g.target);
                    r.reward.Text = Loc.Upper(complete ? Loc.T("ui.goals.unlocked", ("item", Wording.ItemName(db, item))) : Wording.RewardLine(g));
                    r.reward.Color = complete ? Teal : Faded;
                    SetBar(r.track, complete ? 0 : BarW);
                    SetBar(r.fill, complete ? 0 : Mathf.Floor(BarW * Mathf.Clamp01(progress / (float)g.target)));
                    r.icon.enabled = complete && !string.IsNullOrEmpty(item);
                    if (r.icon.enabled) r.icon.sprite = Make.Lib.Sprite(Wording.ItemSprite(db, item));
                    if (complete) done++;
                    y[col] += RowH;
                }
                y[col] += 4;
            }
            for (int i = used; i < rows.Count; i++) rows[i].root.gameObject.SetActive(false);
            found.Text = Loc.T("ui.goals.complete", ("done", done), ("all", db.Goals.Count));
            gameObject.SetActive(true);
        }

        public void Close()
        {
            if (!gameObject.activeSelf) return;
            gameObject.SetActive(false);
            Closed?.Invoke();
        }

        void Update()
        {
            var kb = UnityEngine.InputSystem.Keyboard.current;
            if (kb != null && kb.escapeKey.wasPressedThisFrame) Close();
        }
    }
}
