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
        // the numbers match GOALS in tools/art/ui.mjs; the page itself (Goals.prefab) is laid out in the prefab
        const int X = 20, Y = 15, LeftX = 14, RightX = 304, Top = 30, ColW = 282, RowH = 20, HeadH = 14, Pad = 10, BarW = 92;

        public event Action Closed;
        public bool IsOpen => gameObject.activeSelf;

        [SerializeField] GoalRow rowPrefab;
        [SerializeField] Label[] heads = new Label[3];
        [SerializeField] Label found;
        [SerializeField] Clickable outside, closeClick;
        ContentDb db;
        readonly List<GoalRow> rows = new List<GoalRow>();
        bool wired;

        static readonly Color Ink = Px.Hex("#3a2212"), Faded = Px.Hex("#7a5430"), Teal = Px.Hex("#11606a");
        // which column each rarity's section is in, and in what order: Uncommon and Legendary on the left, Rare on the right
        static readonly string[] Sections = { Rarities.Uncommon, Rarities.Legendary, Rarities.Rare };

        /// <summary>The "Close" click at the top right of the page.</summary>
        public Clickable CloseClick => closeClick;

        /// <summary>
        /// Takes the content and makes one row per goal from the GoalRow part (rows are not baked: a new goal needs its row).
        /// Hooks the clicks. GameFlow calls it once at start; a test calls it after instantiating the prefab.
        /// </summary>
        public void Bind(ContentDb content)
        {
            db = content;
            if (wired) return;
            wired = true;
            outside.onClick = Close;                          // a click outside the page closes it
            closeClick.onClick = Close;
            for (int i = 0; i < db.Goals.Count; i++)
            {
                var r = Instantiate(rowPrefab, transform);       // at the view's origin, as the old loop made them; Open places them
                r.name = "Goal " + i;
                rows.Add(r);
            }
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
                    r.gameObject.SetActive(true);
                    r.transform.position = Px.At(x, y[col]);
                    r.text.Text = Wording.GoalText(g); r.text.Color = complete ? Faded : Ink;
                    string item = profile.RewardOf(g.id);
                    r.count.Text = complete ? "" : Wording.Number(progress) + " / " + Wording.Number(g.target);
                    r.reward.Text = Loc.Upper(complete ? Loc.T("ui.goals.unlocked", ("item", Wording.ItemName(db, item))) : Wording.RewardLine(g));
                    r.reward.Color = complete ? Teal : Faded;
                    SetBar(r.track, complete ? 0 : BarW);
                    SetBar(r.fill, complete ? 0 : Mathf.Floor(BarW * Mathf.Clamp01(progress / (float)g.target)));
                    r.icon.enabled = complete && !string.IsNullOrEmpty(item);
                    if (r.icon.enabled) r.icon.sprite = Art.Lib.Sprite(Wording.ItemSprite(db, item));
                    if (complete) done++;
                    y[col] += RowH;
                }
                y[col] += 4;
            }
            for (int i = used; i < rows.Count; i++) rows[i].gameObject.SetActive(false);
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
