using System;
using System.Globalization;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The panels round the bench: depth and potency, catalysts, trials and redraws, the last-reaction note, journal and Ignite.</summary>
    public sealed class HudView : MonoBehaviour
    {
        const int O = 100;
        public event Action IgniteClicked, JournalClicked, RedrawClicked;
        /// <summary>Catalyst socket index the pointer is over, or -1.</summary>
        public int HoveredCatalyst { get; private set; } = -1;
        public bool RedrawHovered { get; private set; }
        /// <summary>Index of the held aspect the pointer is over (in the strip under the depth panel), or -1.</summary>
        public int HoveredAspect { get; private set; } = -1;
        /// <summary>The most catalysts the sockets can show: four from clearing depths and two from shrines.</summary>
        public const int MaxCatalysts = 6, MaxAspects = 3;
        /// <summary>The Redraw button in art space: under the right panel, right below the redraws-left counter.</summary>
        public const int RedrawX = 556, RedrawY = 88, RedrawW = 76, RedrawH = 20;

        Label depth, depthName, potency, potencyWord, target, trials, redraws, redrawsLabel, journalCount, igniteText, hintText;
        readonly Label[] note = new Label[4];
        SpriteRenderer barFill, barTip, ignite, ignitePulse;
        readonly SpriteRenderer[] socketLit = new SpriteRenderer[MaxCatalysts], catalystIcon = new SpriteRenderer[MaxCatalysts];
        readonly SpriteRenderer[] aspectSocket = new SpriteRenderer[MaxAspects], aspectIcon = new SpriteRenderer[MaxAspects];
        readonly Label[] depthNote = new Label[3];
        Label aspectsLabel;
        float catalystPitch = 35;
        float shown, bump; long goal, targetValue; bool canIgnite, igniteDown, canRedraw, redrawDown;
        SpriteRenderer redrawFill;
        Label redrawText;

        static readonly Color Dim = Px.Hex("#a88a6a"), Pale = Px.Hex("#f0dcc0"), Gold = Px.Hex("#f8d250"), Ink = Px.Hex("#3a2212");
        static readonly Color RedrawUp = Px.Hex("#1d5a52"), RedrawOver = Px.Hex("#26706a"), RedrawPressed = Px.Hex("#143f3a"),
            RedrawOff = Px.Hex("#3a2e28");

        public Vector3 PotencyPosition => Px.At(96, 56);
        /// <summary>Where a tooltip about a held catalyst or aspect points.</summary>
        public Vector3 CatalystAnchor(int i) => Px.At(503 + i * catalystPitch, 39);
        public Vector3 AspectAnchor(int i) => Px.At(22 + i * 29, 111);
        /// <summary>Where the Redraw tooltip (and the tutorial's highlight) points: the button's centre.</summary>
        public Vector3 RedrawAnchor => Px.Mid(RedrawX, RedrawY, RedrawW, RedrawH);
        /// <summary>The Redraw button is live: a redraw is left and the bench is being played (not an offer, not a cleared depth).</summary>
        public static bool CanRedraw(RunState run) => run != null && run.Phase == RunPhase.Playing && run.RedrawsLeft > 0;
        public static string Number(long n) => n.ToString("N0", CultureInfo.InvariantCulture);

        public void Build(Transform parent)
        {
            transform.SetParent(parent, false);
            var t = transform; var dark = Px.Hex("#0c0706");
            // top left: depth and potency
            Make.SpriteAt(t, "hud_left", 8, 8, O);
            depth = Make.Text(t, "", 16, 9, FontKind.Title, 32, Pale, O + 3, Align.Left, dark);
            depthName = Make.Text(t, "", 17, 37, FontKind.Body, 16, Dim, O + 3);
            potencyWord = Make.Text(t, "POTENCY", 17, 52, FontKind.Tiny, 8, Dim, O + 3);
            potency = Make.Text(t, "0", 70, 49, FontKind.Title, 16, Gold, O + 3, Align.Left, Px.Hex("#3a1a08"));
            target = Make.Text(t, "", 153, 52, FontKind.Tiny, 8, Dim, O + 3, Align.Right);
            barFill = Make.Rect(t, 18, 70, 1, 5, Color.white, O + 2); barFill.sprite = Make.Lib.Sprite("bar_fill");
            barTip = Make.Rect(t, 18, 70, 1, 5, Color.white, O + 3);
            // top right: catalysts, trials, redraws
            Make.SpriteAt(t, "hud_right", 480, 8, O);
            Make.Text(t, "CATALYSTS", 489, 14, FontKind.Tiny, 8, Dim, O + 3);
            for (int i = 0; i < MaxCatalysts; i++)
            {
                int x = 488 + i * 35, index = i;
                socketLit[i] = Make.SpriteAt(t, "socket_filled", x, 24, O + 1 + i * 2);
                catalystIcon[i] = Make.Sprite(t, "dot", Px.Mid(x + 3, 27, 24, 24), O + 2 + i * 2);
                var c = Make.Click(socketLit[i].gameObject, new Vector2(30, 30), O);
                c.onEnter = () => HoveredCatalyst = index; c.onExit = () => { if (HoveredCatalyst == index) HoveredCatalyst = -1; };
            }
            Make.Text(t, "TRIALS", 489, 63, FontKind.Tiny, 8, Dim, O + 3);
            trials = Make.Text(t, "", 534, 48, FontKind.Title, 32, Px.Hex("#ee6a3c"), O + 3, Align.Left, dark);
            redrawsLabel = Make.Text(t, "REDRAWS", 556, 63, FontKind.Tiny, 8, Dim, O + 3);
            redraws = Make.Text(t, "", 611, 48, FontKind.Title, 32, Px.Hex("#6ee6c8"), O + 3, Align.Left, dark);
            // the Redraw button, right under the counter: a dark one-pixel edge round a fill that greys out at zero
            Make.Rect(t, RedrawX, RedrawY, RedrawW, RedrawH, dark, O + 1);
            redrawFill = Make.Rect(t, RedrawX + 1, RedrawY + 1, RedrawW - 2, RedrawH - 2, RedrawUp, O + 2);
            redrawText = Make.Text(t, "Redraw", RedrawX + RedrawW / 2f, RedrawY, FontKind.Title, 16, Pale, O + 4, Align.Centre, dark);
            var redrawZone = Make.Root("redraw", t); redrawZone.position = RedrawAnchor;
            var rc = Make.Click(redrawZone.gameObject, new Vector2(RedrawW, RedrawH), O, () => { if (canRedraw) RedrawClicked?.Invoke(); });
            rc.onEnter = () => RedrawHovered = true;
            rc.onExit = () => { RedrawHovered = false; redrawDown = false; };
            rc.onDown = () => redrawDown = true; rc.onUp = () => redrawDown = false;
            // under the depth panel: the aspects this run holds, and what this depth's event changed
            aspectsLabel = Make.Text(t, "ASPECTS", 10, 88, FontKind.Tiny, 8, Dim, O + 3, Align.Left, dark);
            for (int i = 0; i < MaxAspects; i++)
            {
                int x = 9 + i * 29, index = i;
                aspectSocket[i] = Make.SpriteAt(t, "aspect_socket", x, 98, O + 1);
                aspectIcon[i] = Make.Sprite(t, "dot", Px.Mid(x + 1, 99, 24, 24), O + 2);
                var c = Make.Click(aspectSocket[i].gameObject, new Vector2(26, 26), O);
                c.onEnter = () => HoveredAspect = index; c.onExit = () => { if (HoveredAspect == index) HoveredAspect = -1; };
            }
            for (int i = 0; i < depthNote.Length; i++) depthNote[i] = Make.Text(t, "", 10, 130 + i * 10, FontKind.Tiny, 8, Px.Hex("#6ee6c8"), O + 3, Align.Left, dark);
            // bottom left: the last reaction and the latest hint
            Make.SpriteAt(t, "note", 4, 262, O);
            Make.Text(t, "LAST REACTION", 10, 266, FontKind.Tiny, 8, Px.Hex("#7a5430"), O + 3);
            for (int i = 0; i < 4; i++) note[i] = Make.Text(t, "", 10, 273 + i * 10, FontKind.Body, 16, Ink, O + 3);
            note[2].Color = Px.Hex("#11606a"); note[3].Color = Px.Hex("#9a3414");
            Make.Text(t, "HINT", 10, 318, FontKind.Tiny, 8, Px.Hex("#7a5430"), O + 3);
            // two lines: the longest hint ("Phoenix Feather stirs near a crystal.") wraps once
            hintText = Make.Text(t, "", 10, 322, FontKind.Body, 16, Px.Hex("#4a1f7a"), O + 3, Align.Left, null, 112);
            hintText.main.lineSpacing = -30;
            // bottom right: journal and Ignite
            Make.SpriteAt(t, "corner", 514, 284, O);
            var book = Make.SpriteAt(t, "book", 520, 290, O + 2);
            Make.Text(t, "Journal", 542, 289, FontKind.Title, 16, Pale, O + 3);
            journalCount = Make.Text(t, "", 630, 294, FontKind.Tiny, 8, Dim, O + 3, Align.Right);
            Make.Click(book.gameObject, new Vector2(118, 22), O, () => JournalClicked?.Invoke()).offset = new Vector2(46, 0);
            ignite = Make.SpriteAt(t, "ignite_up", 519, 310, O + 1);
            ignitePulse = Make.Rect(t, 521, 312, 108, 28, Color.clear, O + 2); ignitePulse.sharedMaterial = Make.Lib.glow;
            igniteText = Make.Text(t, "Ignite", 575, 306, FontKind.Title, 32, Px.Hex("#fff6b8"), O + 4, Align.Centre, Px.Hex("#3b0d1e"));
            var ic = Make.Click(ignite.gameObject, new Vector2(112, 36), O, () => { if (canIgnite) IgniteClicked?.Invoke(); });
            ic.onDown = () => igniteDown = true; ic.onUp = () => igniteDown = false; ic.onExit = () => igniteDown = false;
        }

        /// <summary>Everything except the potency number, which is animated separately.</summary>
        public void Show(RunState run, ContentDb db, Journal journal, bool inputEnabled)
        {
            depth.Text = "Depth " + run.Depth;
            depthName.Text = db.DepthName(run.Depth);
            targetValue = run.Target;
            target.Text = run.Target > 0 ? "/ " + Number(run.Target) : "NO TARGET";
            trials.Text = run.TrialsLeft.ToString();
            redraws.Text = run.RedrawsLeft.ToString();
            // the button follows the run, not inputEnabled: it stays lit while a reaction plays (clicks are gated in BenchView)
            canRedraw = CanRedraw(run);
            redraws.Alpha = redrawsLabel.Alpha = canRedraw ? 1 : 0.35f;
            // four sockets side by side; a fifth or sixth (a shrine's gift) makes them overlap to fit the panel
            int held = Mathf.Min(run.Catalysts.Count, MaxCatalysts);
            catalystPitch = held <= 4 ? 35 : 105f / (held - 1);
            for (int i = 0; i < MaxCatalysts; i++)
            {
                bool has = i < held;
                socketLit[i].enabled = catalystIcon[i].enabled = has;
                socketLit[i].gameObject.SetActive(has);
                if (!has) continue;
                float x = Mathf.Round(488 + i * catalystPitch);
                socketLit[i].transform.position = Px.Mid(x, 24, 30, 30);
                catalystIcon[i].transform.position = Px.Mid(x + 3, 27, 24, 24);
                catalystIcon[i].sprite = Make.Lib.Sprite("cat_" + run.Catalysts[i].id);
            }
            aspectsLabel.gameObject.SetActive(run.Aspects.Count > 0);
            for (int i = 0; i < MaxAspects; i++)
            {
                bool has = i < run.Aspects.Count;
                aspectSocket[i].gameObject.SetActive(has); aspectIcon[i].enabled = has;
                if (has) aspectIcon[i].sprite = Make.Lib.Sprite("asp_" + run.Aspects[i].Data.id);
            }
            if (run.Aspects.Count == 0) HoveredAspect = -1;
            // what this depth's event left behind (shut and warm tiles show on the bench itself)
            int line = 0, top = run.Aspects.Count > 0 ? 130 : 88;
            foreach (var l in depthNote) l.Text = "";
            void Say(string text) { if (line < depthNote.Length) { depthNote[line].Text = text; depthNote[line].transform.position = Px.At(10, top + line * 10); line++; } }
            if (run.RiShift != 0) Say("THIS DEPTH: REACTION INDEX " + (run.RiShift > 0 ? "+" : "") + run.RiShift);
            if (run.HandDelta != 0) Say("THIS DEPTH: " + (run.HandDelta > 0 ? "+" : "") + run.HandDelta + (Mathf.Abs(run.HandDelta) == 1 ? " CARD A HAND" : " CARDS A HAND"));
            if (run.TripleFirst) Say("NEXT FIRST REACTION: x3");
            journalCount.Text = journal.KnownPairs.Count + "/" + db.Recipes.Count;
            canIgnite = inputEnabled && run.CanIgnite;
        }

        public void SetPotency(long value, bool instant = false) { goal = value; if (instant) shown = value; }

        /// <summary>Writes one reaction on the note: what went in, what came of it and its potency.</summary>
        public void Note(ReactionEvent e, ContentDb db)
        {
            if (e.Sputter)
            {
                note[0].Text = "Something burned"; note[1].Text = "hotter and spoiled"; note[2].Text = "a mixture."; note[3].Text = "";
                note[2].Color = Px.Hex("#7a5430");
                return;
            }
            // a pair is written out; a combo names its hub and counts the rest (the journal has the full recipe)
            note[0].Text = db.Material(e.Items[0]).name;
            note[1].Text = e.Items.Length == 2 ? "+ " + db.Material(e.Items[1]).name : "+ " + (e.Items.Length - 1) + " around it";
            note[2].Text = e.Result != null ? "= " + db.Material(e.Result).name : "Nothing happens";
            note[2].Color = Px.Hex(e.Result != null ? "#11606a" : "#7a5430");
            note[3].Text = e.Result != null ? "+" + Number(e.Potency) + (e.Discovery ? "  NEW" : e.Chain > 1 ? "  chain " + e.Chain : "") : "";
            if (e.Hint != null) Hint(e.Hint, db);
        }

        public void Hint(HintData h, ContentDb db)
        {
            if (h == null) { hintText.Text = ""; return; }
            hintText.Text = HintLine(h, db);
        }

        /// <summary>"Ember Coal stirs near a potion.", or for a recipe the bench was too dull for, "Ember Coal needs heat 14."</summary>
        public static string HintLine(HintData h, ContentDb db, bool withName = true)
        {
            if (h.heat > 0) return (withName ? db.Material(h.material).name + " needs" : "Needs") + " heat " + h.heat + ".";
            string kind = h.partnerKind.ToLowerInvariant();
            return (withName ? db.Material(h.material).name + " stirs" : "Stirs") + " near " + ("aeiou".IndexOf(kind[0]) >= 0 ? "an " : "a ") + kind + ".";
        }

        /// <summary>A short kick of the potency number when points land on it.</summary>
        public void Bump() => bump = 1;

        public void ClearNote() { foreach (var n in note) n.Text = ""; }

        void Update()
        {
            // potency counts toward its goal: fast when far away, never slower than a few points a second
            if (shown != goal)
            {
                float step = Mathf.Max(30, Mathf.Abs(goal - shown) * 6) * Time.deltaTime;
                shown = Mathf.MoveTowards(shown, goal, step);
            }
            potency.Text = Number((long)Mathf.Round(shown));
            bump = Mathf.Max(0, bump - Time.deltaTime * 6);
            // a number long enough to run into the target takes the place of the word before it
            bool tight = 70 + potency.Width + 4 > 153 - target.Width;
            potencyWord.gameObject.SetActive(!tight);
            potency.transform.position = Px.At(tight ? 17 : 70, 49 - (bump > 0.5f ? 2 : bump > 0 ? 1 : 0));
            potency.Color = bump > 0.4f ? Color.white : Gold;
            float fill = targetValue > 0 ? Mathf.Clamp01(shown / targetValue) : 1;
            int w = Mathf.RoundToInt(132 * fill);
            barFill.enabled = barTip.enabled = w > 0;
            barFill.transform.position = Px.Mid(18, 70, w, 5); barFill.transform.localScale = new Vector3(w / 4f, 1, 1);
            barFill.color = targetValue > 0 ? Color.white : new Color(0.55f, 0.5f, 0.75f, 1);
            barTip.transform.position = Px.Mid(18 + Mathf.Max(0, w - 1), 70, 1, 5); barTip.transform.localScale = new Vector3(0.25f, 1.25f, 1);

            ignite.sprite = Make.Lib.Sprite(!canIgnite ? "ignite_off" : igniteDown ? "ignite_down" : "ignite_up");
            float pulse = 0.5f + 0.5f * Mathf.Sin(Time.time * 3);
            ignitePulse.color = canIgnite && !igniteDown ? new Color(1, 0.75f, 0.35f, 0.1f + 0.2f * pulse) : Color.clear;
            igniteText.Alpha = canIgnite ? 1 : 0.45f;
            igniteText.transform.position = Px.At(575, 306 + (canIgnite && igniteDown ? 2 : 0));
            redrawFill.color = !canRedraw ? RedrawOff : redrawDown ? RedrawPressed : RedrawHovered ? RedrawOver : RedrawUp;
            redrawText.Alpha = canRedraw ? 1 : 0.45f;
            redrawText.transform.position = Px.At(RedrawX + RedrawW / 2f, RedrawY + (canRedraw && redrawDown ? 2 : 0));
        }
    }
}
