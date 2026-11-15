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
            PipCount = ContentDb.MaxReactivity, PipW = 5, PipGap = 2, WrapAt = 38;

        public event Action Closed;
        public bool IsOpen => gameObject.activeSelf;

        ContentDb db; Journal journal;
        Func<string, bool> isOpen;      // which starting materials this profile may use (the rest are still to be won)
        Label pageLabel;
        int page;
        const int PerPage = Cols * Rows;
        int Pages => (db.Materials.Count + PerPage - 1) / PerPage;
        readonly List<SpriteRenderer> icons = new List<SpriteRenderer>();
        readonly HashSet<string> discovered = new HashSet<string>();
        readonly Label[] lines = new Label[LineCount], hints = new Label[HintCount];
        SpriteRenderer pick, bigIcon;
        RarityStars pageStars;
        Label pageTier;
        readonly Dictionary<int, RarityStars> cellStars = new Dictionary<int, RarityStars>();
        readonly SpriteRenderer[] pips = new SpriteRenderer[PipCount];
        Label found, materialsCount, title, sub;
        int selected;

        static readonly Color Ink = Px.Hex("#3a2212"), Faded = Px.Hex("#7a5430"), Teal = Px.Hex("#11606a"), Purple = Px.Hex("#4a1f7a"),
            Pale = Px.Hex("#f0dcc0"), Dark = Px.Hex("#0c0706");

        public void Build(ContentDb content)
        {
            db = content;
            var t = transform;
            var shade = Make.Rect(t, 0, 0, Px.W, Px.H, new Color(0, 0, 0, 0.97f), O - 1);
            Make.Click(shade.gameObject, new Vector2(Px.W, Px.H), O - 1, Close);         // a click outside the book closes it
            var panel = Make.SpriteAt(t, "journal", X, Y, O);
            Make.Click(panel.gameObject, new Vector2(600, 330), O);
            Make.TextKey(t, "ui.journal.title", X + 18, Y + 7, FontKind.Title, 16, Pale, O + 2, Align.Left, Dark);
            found = Make.Text(t, "", X + 300, Y + 11, FontKind.Tiny, 8, Px.Hex("#a88a6a"), O + 2, Align.Centre);
            var close = Make.TextKey(t, "ui.common.close", X + 582, Y + 7, FontKind.Title, 16, Pale, O + 2, Align.Right, Dark);
            Make.Click(close.gameObject, new Vector2(60, 22), O + 3, Close).offset = new Vector2(-22, -8);

            // left page: the grid of materials, then the latest hints
            Make.TextKey(t, "ui.journal.materials", X + GridX, Y + 43, FontKind.Tiny, 8, Faded, O + 2);
            materialsCount = Make.Text(t, "", X + GridX + Cols * Cell - (Cell - Well), Y + 43, FontKind.Tiny, 8, Faded, O + 2, Align.Right);
            pick = Make.Sprite(t, "journal_pick", Vector3.zero, O + 1);
            // more materials than the grid holds: they are shown a page at a time
            pageLabel = Make.Text(t, "", X + GridX + 136, Y + 43, FontKind.Tiny, 8, Px.Hex("#9a3414"), O + 2, Align.Centre);
            Make.Click(pageLabel.gameObject, new Vector2(110, 12), O + 3, () => ShowPage((page + 1) % Pages)).offset = new Vector2(0, -4);
            for (int i = 0; i < db.Materials.Count; i++)
            {
                int index = i;
                var icon = Make.Sprite(t, db.Materials[i].id, CellCentre(i), O + 2);
                Make.Click(icon.gameObject, new Vector2(Cell, Cell), O + 3, () => Select(index));
                icons.Add(icon);
                // one star at the corner of the cell for an Uncommon or better starting material (SPEC 24); Common shows none
                if (db.Materials[i].tier == 0 && Rarities.Rank(db.Materials[i].rarity) > 0)
                {
                    var s = new RarityStars(t, O + 4);
                    s.Root.position = Px.At(X + GridX + (i % PerPage % Cols) * Cell - 1, Y + GridY + (i % PerPage / Cols) * Cell - 1);
                    s.Show(1, Wording.RarityColor(db.Materials[i].rarity));
                    cellStars[i] = s;
                }
            }
            Make.TextKey(t, "ui.journal.latest_hints", X + GridX, Y + HintsY, FontKind.Tiny, 8, Faded, O + 2);
            for (int i = 0; i < HintCount; i++) hints[i] = Make.Text(t, "", X + GridX, Y + HintsY + 9 + i * 11, FontKind.Body, 16, Purple, O + 2);

            // right page: the selected material
            int cx = X + PageX + PageW / 2;
            bigIcon = Make.Sprite(t, "dot", Px.At(cx, Y + PageY + 36), O + 2);
            bigIcon.transform.localScale = new Vector3(2, 2, 1);
            title = Make.Text(t, "", cx, Y + PageY + 62, FontKind.Title, 32, Ink, O + 2, Align.Centre);
            // the rarity mark at the page's top-left: n stars at (page x+14, y+8), the tier word on the row below (SPEC 24)
            pageStars = new RarityStars(t, O + 3);
            pageStars.Root.position = Px.At(X + PageX + 14, Y + PageY + 8);
            pageTier = Make.Text(t, "", X + PageX + 14, Y + PageY + 18, FontKind.Name, 16, Ink, O + 2);
            sub = Make.Text(t, "", cx, Y + PageY + 94, FontKind.Tiny, 8, Faded, O + 2, Align.Centre);
            // the Reaction Index: one lit pip per point
            int pipsLeft = cx - (PipCount * (PipW + PipGap) - PipGap) / 2;
            for (int i = 0; i < PipCount; i++) pips[i] = Make.Rect(t, pipsLeft + i * (PipW + PipGap), Y + PageY + 104, PipW, 3, Color.clear, O + 2);
            Make.Rect(t, X + PageX + 16, Y + PageY + 109, PageW - 32, 1, Px.Hex("#b89860"), O + 1);
            for (int i = 0; i < LineCount; i++) lines[i] = Make.Text(t, "", X + PageX + 18, Y + PageY + 112 + i * 11, FontKind.Body, 16, Ink, O + 2);
            gameObject.SetActive(false);
        }

        Vector3 CellCentre(int i) => Px.Mid(X + GridX + (i % PerPage % Cols) * Cell, Y + GridY + (i % PerPage / Cols) * Cell, Well, Well);

        /// <summary>Shows one page of the grid; the selection moves to that page's first material if it was on another.</summary>
        void ShowPage(int p)
        {
            page = Mathf.Clamp(p, 0, Pages - 1);
            for (int i = 0; i < icons.Count; i++) icons[i].gameObject.SetActive(i / PerPage == page);
            pageLabel.Text = Pages > 1 ? Loc.T("ui.journal.page", ("n", page + 1), ("pages", Pages)) : "";
            if (selected / PerPage != page) Select(page * PerPage);
            else pick.enabled = true;
            ShowCellStars();
        }

        /// <summary>The corner stars follow the page of the grid and show only for what has been found.</summary>
        void ShowCellStars()
        {
            foreach (var kv in cellStars) kv.Value.SetActive(kv.Key / PerPage == page && discovered.Contains(db.Materials[kv.Key].id));
        }

        /// <summary>openStarters: which starting materials the player may use; null means all of them.</summary>
        public void Open(Journal book, Func<string, bool> openStarters = null)
        {
            journal = book; isOpen = openStarters;
            discovered.Clear();
            foreach (var m in db.Starters) if (isOpen == null || isOpen(m.id)) discovered.Add(m.id);
            foreach (var r in db.Recipes) if (journal.IsKnown(r.items)) discovered.Add(r.result);
            for (int i = 0; i < icons.Count; i++)
                icons[i].color = discovered.Contains(db.Materials[i].id) ? Color.white : new Color(0.16f, 0.1f, 0.05f, 0.4f);
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
            selected = Mathf.Clamp(selected, 0, icons.Count - 1);
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
            bigIcon.sprite = Make.Lib.Sprite(m.id);
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
