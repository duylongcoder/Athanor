using System;
using System.Collections.Generic;
using Athanor.Gameplay;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The Goals page: every long-term goal with its progress, grouped by the rarity of what it unlocks.
    /// Before a goal is done its reward is named only by kind and rarity; afterwards the item it gave is shown.
    /// </summary>
    public sealed class GoalsView : MonoBehaviour
    {
        public event Action Closed;
        public bool IsOpen => gameObject.activeSelf;

        /// <summary>The progress bar's height and its drop below the row's top, stored in Goals.prefab.</summary>
        [Serializable]
        public struct Layout { public float barHeight, barDy; }
        [SerializeField] Layout layout;
        /// <summary>
        /// The page's grid (art px, y down), stored in Goals.prefab; the numbers match GOALS in tools/art/ui.mjs.
        /// <c>dotSize</c> is the side of the "dot" sprite the bar is stretched from.
        /// </summary>
        [Serializable]
        public struct Grid { public int x, y, leftX, rightX, top, colW, rowH, headH, pad, barW; public float dotSize; }
        [SerializeField] Grid grid;
        /// <summary>The bar's left end in the row: grid.barW short of the row's inner right edge.</summary>
        int BarLeft => grid.colW - 2 * grid.pad - grid.barW;

        [SerializeField] GoalRow rowPrefab;
        [SerializeField] Label[] heads = new Label[3];
        [SerializeField] Label found;
        [SerializeField] Clickable outside, closeClick;
        ContentDb db;
        readonly List<GoalRow> rows = new List<GoalRow>();
        bool wired;

        static Color Ink => Art.Palette.ink;
        static Color Faded => Art.Palette.faded;
        static Color Teal => Art.Palette.teal;
        // which column each rarity's section is in, and in what order: Uncommon and Legendary on the left, Rare on the right
        static readonly string[] Sections = { Rarities.Uncommon, Rarities.Legendary, Rarities.Rare };

        /// <summary>The "Close" click at the top right of the page.</summary>
        public Clickable CloseClick => closeClick;

        /// <summary>
        /// Takes the content and makes one row per goal from the GoalRow part (rows are not baked: a new goal needs its row).
        /// Hooks the clicks. GameManager calls it once at start; a test calls it after instantiating the prefab.
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

        /// <summary>A bar of the given width whose left end is grid.barW short of the row's right edge, 13 down from the row's top.</summary>
        void SetBar(SpriteRenderer bar, float width)
        {
            bar.enabled = width >= 1;
            float x = BarLeft + width / 2f;
            bar.transform.localScale = new Vector3(width / grid.dotSize, layout.barHeight / grid.dotSize, 1);
            bar.transform.localPosition = new Vector3(x, layout.barDy, 0);
        }

        public void Open(Profile profile, Journal journal)
        {
            int done = 0, used = 0;
            float[] y = { grid.y + grid.top + 6, grid.y + grid.top + 6 };       // the next free line of each column
            for (int s = 0; s < Sections.Length; s++)
            {
                int col = Sections[s] == Rarities.Rare ? 1 : 0, x = grid.x + (col == 0 ? grid.leftX : grid.rightX) + grid.pad;
                // goals still open first, in the order of the content; the finished ones after them
                var open = new List<GoalData>(); var finished = new List<GoalData>();
                foreach (var g in db.Goals)
                    if (g.rewardRarity == Sections[s]) (profile.IsComplete(g.id) ? finished : open).Add(g);
                if (open.Count + finished.Count == 0) { heads[s].Text = ""; continue; }
                heads[s].Text = Loc.T("ui.goals.section", ("rarity", Loc.Upper(Loc.Rarity(Sections[s]))), ("done", finished.Count), ("all", open.Count + finished.Count));
                heads[s].Color = Wording.RarityColor(Sections[s]).Times(0.55f).Alpha(1);
                heads[s].transform.position = Px.At(x, y[col]);
                y[col] += grid.headH;
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
                    SetBar(r.track, complete ? 0 : grid.barW);
                    SetBar(r.fill, complete ? 0 : Mathf.Floor(grid.barW * Mathf.Clamp01(progress / (float)g.target)));
                    r.icon.enabled = complete && !string.IsNullOrEmpty(item);
                    if (r.icon.enabled) r.icon.sprite = Art.Lib.Sprite(Wording.ItemSprite(db, item));
                    if (complete) done++;
                    y[col] += grid.rowH;
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
