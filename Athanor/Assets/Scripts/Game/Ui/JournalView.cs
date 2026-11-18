using System;
using System.Collections.Generic;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The journal: every material as an icon (a silhouette until it has been made), and for the selected one
    /// the reactions found so far, how many are still unknown, and any hints collected.
    /// </summary>
    public sealed class JournalView : MonoBehaviour
    {
        // the numbers match JOURNAL in tools/art/ui.mjs
        const int O = 440, X = 20, Y = 15, Cols = 10, Rows = 6, Cell = 27, Well = 25, GridX = 33, GridY = 60,
            PageX = 324, PageY = 30, PageW = 260, HintsY = 240, LineCount = 15, HintCount = 5,
            PipCount = ContentDb.MaxReactivity, WrapAt = 38;

        public event Action Closed;
        public bool IsOpen => gameObject.activeSelf;

        /// <summary>Materials on one page of the grid.</summary>
        public const int PerPage = Cols * Rows;

        ContentDb db; Journal journal;
        Func<string, bool> isOpen;      // which starting materials this profile may use (the rest are still to be won)
        [SerializeField] JournalCell cellPrefab;
        [SerializeField] Label pageLabel, found, materialsCount, title, sub, pageTier;
        [SerializeField] Label[] lines = new Label[LineCount], hints = new Label[HintCount];
        [SerializeField] SpriteRenderer pick, bigIcon;
        [SerializeField] RarityStars pageStars;
        [SerializeField] SpriteRenderer[] pips = new SpriteRenderer[PipCount];
        [SerializeField] Clickable outside, closeClick, pageClick;
        int page;
        int Pages => (db.Materials.Count + PerPage - 1) / PerPage;
        readonly List<JournalCell> cells = new List<JournalCell>();
        readonly HashSet<string> discovered = new HashSet<string>();
        int selected;
        bool wired;

        static readonly Color Ink = Px.Hex("#3a2212"), Faded = Px.Hex("#7a5430"), Teal = Px.Hex("#11606a"), Purple = Px.Hex("#4a1f7a");

        /// <summary>The selected material's name on the right-hand page.</summary>
        public Label Title => title;
        /// <summary>The "Close" click at the top right of the book.</summary>
        public Clickable CloseClick => closeClick;
        /// <summary>The page label above the grid: a click turns to the next page of materials.</summary>
        public Clickable PageClick => pageClick;

        /// <summary>
        /// Takes the content and makes one cell per material from the JournalCell part (cells are not baked: a new material needs its cell).
        /// Hooks the clicks. GameFlow calls it once at start; a test calls it after instantiating the prefab.
        /// </summary>
        public void Bind(ContentDb content)
        {
            db = content;
            if (wired) return;
            wired = true;
            outside.onClick = Close;                          // a click outside the book closes it
            closeClick.onClick = Close;
            pageClick.onClick = NextPage;
            for (int i = 0; i < db.Materials.Count; i++)
            {
                var cell = Instantiate(cellPrefab, transform);
                SetUpCell(cell, i);
                cells.Add(cell);
            }
        }

        void NextPage() => ShowPage((page + 1) % Pages);

        /// <summary>Gives the cell its material's icon, its place in the grid, its click and, for an Uncommon or better starter, its star.</summary>
        void SetUpCell(JournalCell cell, int i)
        {
            int index = i;
            var m = db.Materials[i];
            cell.name = "Cell " + i;
            cell.icon.name = m.id;
            cell.icon.sprite = Art.Lib.Sprite(m.id);
            cell.icon.transform.position = CellCentre(i);
            cell.click.onClick = () => Select(index);
            // one star at the corner of the cell for an Uncommon or better starting material (SPEC 24); Common shows none
            if (m.tier == 0 && Rarities.Rank(m.rarity) > 0)
            {
                cell.stars = new RarityStars(cell.transform, O + 4);
                cell.stars.Root.position = Px.At(X + GridX + (i % PerPage % Cols) * Cell - 1, Y + GridY + (i % PerPage / Cols) * Cell - 1);
                cell.stars.Show(1, Wording.RarityColor(m.rarity));
            }
        }

        Vector3 CellCentre(int i) => Px.Mid(X + GridX + (i % PerPage % Cols) * Cell, Y + GridY + (i % PerPage / Cols) * Cell, Well, Well);

        /// <summary>Shows one page of the grid; the selection moves to that page's first material if it was on another.</summary>
        void ShowPage(int p)
        {
            page = Mathf.Clamp(p, 0, Pages - 1);
            for (int i = 0; i < cells.Count; i++) cells[i].icon.gameObject.SetActive(i / PerPage == page);
            pageLabel.Text = Pages > 1 ? Loc.T("ui.journal.page", ("n", page + 1), ("pages", Pages)) : "";
            if (selected / PerPage != page) Select(page * PerPage);
            else pick.enabled = true;
            ShowCellStars();
        }

        /// <summary>The corner stars follow the page of the grid and show only for what has been found.</summary>
        void ShowCellStars()
        {
            for (int i = 0; i < cells.Count; i++)
                if (cells[i].stars != null) cells[i].stars.SetActive(i / PerPage == page && discovered.Contains(db.Materials[i].id));
        }

        /// <summary>openStarters: which starting materials the player may use; null means all of them.</summary>
        public void Open(Journal book, Func<string, bool> openStarters = null)
        {
            journal = book; isOpen = openStarters;
            discovered.Clear();
            foreach (var m in db.Starters) if (isOpen == null || isOpen(m.id)) discovered.Add(m.id);
            foreach (var r in db.Recipes) if (journal.IsKnown(r.items)) discovered.Add(r.result);
            for (int i = 0; i < cells.Count; i++)
                cells[i].icon.color = discovered.Contains(db.Materials[i].id) ? Color.white : new Color(0.16f, 0.1f, 0.05f, 0.4f);
            found.Text = Loc.T("ui.journal.found", ("known", journal.KnownPairs.Count), ("total", db.Recipes.Count));
            materialsCount.Text = discovered.Count + " / " + db.Materials.Count;
            var open = new List<HintData>();
            for (int h = journal.Hints.Count - 1; h >= 0 && open.Count < HintCount; h--)
                if (db.Has(journal.Hints[h].material) && !journal.IsComplete(journal.Hints[h].material, db)) open.Add(journal.Hints[h]);
            for (int i = 0; i < HintCount; i++)
            {
                hints[i].Text = i < open.Count ? HudView.HintLine(open[i], db) : i == 0 ? Loc.T("ui.journal.hint_empty") : "";
                hints[i].Color = i < open.Count ? Purple : Faded;
            }
            gameObject.SetActive(true);
            selected = Mathf.Clamp(selected, 0, cells.Count - 1);
            ShowPage(selected / PerPage);
            ShowCellStars();
            Select(selected);
        }

        string Names(IEnumerable<string> ids)
        {
            var names = new List<string>();
            foreach (var id in ids) names.Add(db.Material(id).name);
            return string.Join(" + ", names);
        }

        /// <summary>" (heat 22)" after a recipe that needs lively company beside its hub; nothing after one that does not.</summary>
        string Heat(RecipeData r) => db.Company(r) > 0 ? " " + Loc.T("ui.journal.heat", ("n", r.minReactivity)).Replace(' ', '\u00a0') : "";

        /// <summary>Breaks a long recipe line between words so it stays on the page; later lines are indented.</summary>
        static IEnumerable<string> Wrap(string text)
        {
            if (text.Length <= WrapAt) { yield return Plain(text); yield break; }
            string line = "";
            foreach (var word in text.Trim().Split(' '))
            {
                if (line.Length > 0 && line.Length + 1 + word.Length > WrapAt) { yield return Plain(line); line = "     " + word; }
                else line = line.Length == 0 ? "  " + word : line + " " + word;
            }
            if (line.Length > 0) yield return Plain(line);
        }

        /// <summary>A no-break space keeps "(heat 22)" on one line while wrapping; the fonts only have the plain one.</summary>
        static string Plain(string line) => line.Replace('\u00a0', ' ');

        public void Close()
        {
            if (!gameObject.activeSelf) return;
            gameObject.SetActive(false);
            Closed?.Invoke();
        }

        // ---- the tutorial points at parts of a page (art space, top left origin) ----
        int IndexOf(string id) { for (int i = 0; i < db.Materials.Count; i++) if (db.Materials[i].id == id) return i; return -1; }

        /// <summary>Opens the page of a material (its cell picked) and returns the rectangle of its cell in the grid.</summary>
        public Rect ShowMaterial(string id)
        {
            int i = Mathf.Max(0, IndexOf(id));
            ShowPage(i / PerPage); Select(i);
            return new Rect(X + GridX + (i % PerPage % Cols) * Cell - 1, Y + GridY + (i % PerPage / Cols) * Cell - 1, Well + 2, Well + 2);
        }
        /// <summary>The page's line "heat/ reactivity" and its pips.</summary>
        public static Rect IndexRect => new Rect(X + PageX + 12, Y + PageY + 91, PageW - 24, 22);
        /// <summary>The recipe lines of the page.</summary>
        public static Rect RecipesRect => new Rect(X + PageX + 12, Y + PageY + 111, PageW - 24, 60);
        /// <summary>The latest hints under the grid.</summary>
        public static Rect HintsRect => new Rect(X + GridX - 2, Y + HintsY - 2, Cols * Cell + 2, 14 + 2 * 11);

        void Select(int i)
        {
            selected = i;
            pick.transform.position = CellCentre(i);
            var m = db.Materials[i];
            bool known = discovered.Contains(m.id);
            bigIcon.sprite = Art.Lib.Sprite(m.id);
            bigIcon.color = known ? Color.white : new Color(0.16f, 0.1f, 0.05f, 0.4f);
            bool locked = m.tier == 0 && !known;
            title.Text = known ? m.name : locked ? Loc.T("ui.journal.locked") : Loc.T("ui.journal.unknown");
            title.SetSize(title.Text.Length > 13 ? 16 : 32);
            title.transform.position = Px.At(X + PageX + PageW / 2, Y + PageY + (title.Text.Length > 13 ? 72 : 62));
            sub.Text = known ? (m.tier == 0 ? Loc.T("ui.journal.sub_starter", ("kind", Loc.Upper(Loc.Kind(m.kind))), ("r", m.reactivity)) : Loc.T("ui.journal.sub_tier", ("kind", Loc.Upper(Loc.Kind(m.kind))), ("tier", m.tier), ("r", m.reactivity))) : "";
            // the rarity is only defined for starting materials; a made one, or one not found yet, shows none
            bool rated = known && m.tier == 0;
            pageStars.Show(rated ? RarityStars.Count(m.rarity) : 0, Wording.RarityColor(m.rarity));
            pageTier.Text = rated ? Wording.RarityWord(m.rarity) : "";
            pageTier.Color = Wording.RarityTone(m.rarity);
            for (int pip = 0; pip < PipCount; pip++)
                pips[pip].color = !known ? Color.clear : pip < m.reactivity ? Px.Hex("#c8501e") : Px.Hex("#b89860").Alpha(0.5f);

            var text = new List<(string, Color)>();
            if (locked) { text.Add((Loc.T("ui.journal.locked_text"), Faded)); text.Add((Loc.T("ui.journal.locked_how"), Faded)); }
            else if (!known) text.Add((Loc.T("ui.journal.empty"), Faded));
            else
            {
                if (m.tier > 0)
                {
                    text.Add((Loc.T("ui.journal.made_from"), Faded));
                    foreach (var r in db.Recipes)
                        if (r.result == m.id && journal.IsKnown(r.items))
                            foreach (var l in Wrap("  " + Names(r.items) + Heat(r))) text.Add((l, Ink));
                }
                int total = 0, got = 0;
                var reactions = new List<(string, Color)>();
                foreach (var r in db.RecipesContaining(m.id))
                {
                    total++;
                    if (!journal.IsKnown(r.items)) continue;
                    got++;
                    var others = new List<string>(r.items);
                    others.Remove(m.id);
                    foreach (var l in Wrap("  + " + Names(others) + " = " + db.Material(r.result).name + Heat(r))) reactions.Add((l, Teal));
                }
                text.Add((total == 0 ? Loc.T("ui.journal.no_more") : Loc.T("ui.journal.reactions_found", ("got", got), ("total", total)), Faded));
                text.AddRange(reactions);
                // hints stay on the material's page until everything it does has been found
                foreach (var h in journal.OpenHints(m.id, db)) text.Add((HudView.HintLine(h, db, false), Purple));
            }
            for (int k = 0; k < LineCount; k++)
            {
                bool cut = k == LineCount - 1 && text.Count > LineCount;
                lines[k].Text = cut ? Loc.T("ui.journal.more", ("n", text.Count - LineCount + 1)) : k < text.Count ? text[k].Item1 : "";
                lines[k].Color = cut ? Faded : k < text.Count ? text[k].Item2 : Ink;
            }
        }

        void Update()
        {
            var kb = UnityEngine.InputSystem.Keyboard.current;
            if (kb != null && kb.escapeKey.wasPressedThisFrame) Close();
        }
    }
}
