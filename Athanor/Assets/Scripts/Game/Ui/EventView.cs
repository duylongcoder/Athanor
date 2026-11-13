using System;
using System.Collections;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The event at the start of a depth: what was found, the options that can be taken (each with what it
    /// costs and may give), then what came of the one chosen.
    /// </summary>
    public sealed class EventView : MonoBehaviour
    {
        // the first option and Continue are always in the same place, whatever the event (tools/web/webtest.mjs clicks them)
        const int O = 440, X = 160, Y = 75, OptionW = 288, OptionH = 24, OptionX = X + 16, OptionY = Y + 78, Pitch = 38, MaxOptions = 3, NoteCount = 4;

        sealed class Row
        {
            public Transform root; public SpriteRenderer body; public Label label, summary; public bool over, down;
        }

        readonly Row[] rows = new Row[MaxOptions];
        readonly Label[] notes = new Label[NoteCount];
        SpriteRenderer shade, icon;
        Label heading, kind, text, outcome;
        UiButton proceed;
        Transform body;
        int picked, count;
        bool choosing, closing;

        public void Build()
        {
            var t = transform;
            shade = Make.Rect(t, 0, 0, Px.W, Px.H, Color.clear, O - 1);
            Make.Click(shade.gameObject, new Vector2(Px.W, Px.H), O - 1);
            body = Make.Root("Dialog", t);
            Make.SpriteAt(body, "dialog", X, Y, O);
            icon = Make.Sprite(body, "dot", Px.Mid(X + 14, Y + 12, 24, 24), O + 2);
            heading = Make.Text(body, "", X + 46, Y + 10, FontKind.Title, 16, Px.Hex("#f8d250"), O + 2, Align.Left, Px.Hex("#3a1a08"));
            kind = Make.Text(body, "", X + 46, Y + 28, FontKind.Tiny, 8, Px.Hex("#a88a6a"), O + 2);
            Make.Rect(body, X + 14, Y + 42, 292, 1, Px.Hex("#6a4428"), O + 1);
            text = Make.Text(body, "", X + 16, Y + 46, FontKind.Body, 16, Px.Hex("#f0dcc0"), O + 2, Align.Left, null, 288);
            text.main.lineSpacing = -30;
            for (int i = 0; i < MaxOptions; i++)
            {
                int index = i;
                var r = rows[i] = new Row { root = Make.Root("Option " + i, body) };
                r.root.position = Px.Mid(OptionX, OptionY + i * Pitch, OptionW, OptionH);
                r.body = Make.Sprite(r.root, "option_up", r.root.position, O + 2);
                r.label = Make.Text(r.root, "", 0, 0, FontKind.Title, 16, Px.Hex("#fff6b8"), O + 4, Align.Centre, Px.Hex("#3b0d1e"));
                r.summary = Make.Text(r.root, "", 0, 0, FontKind.Tiny, 8, Px.Hex("#c8ac84"), O + 3, Align.Centre);
                r.summary.transform.localPosition = new Vector3(0, -OptionH / 2 - 1, 0);
                var c = Make.Click(r.root.gameObject, new Vector2(OptionW, OptionH), O + 2, () => { if (choosing && index < count) picked = index; });
                c.onEnter = () => r.over = true;
                c.onExit = () => { r.over = false; r.down = false; };
                c.onDown = () => r.down = true;
                c.onUp = () => r.down = false;
            }
            outcome = Make.Text(body, "", 320, Y + 82, FontKind.Body, 16, Px.Hex("#f0dcc0"), O + 2, Align.Centre, null, 288);
            outcome.main.lineSpacing = -30;
            for (int i = 0; i < NoteCount; i++) notes[i] = Make.Text(body, "", 320, Y + 108 + i * 13, FontKind.Title, 16, Px.Hex("#6ee6c8"), O + 2, Align.Centre, Px.Hex("#0c0706"));
            proceed = UiButton.Create(body, "Continue", 260, Y + 172, O + 2, () => closing = true);
            gameObject.SetActive(false);
        }

        /// <summary>Shows the run's current event, takes the option the player picks and shows what it did. Ends when Continue is pressed.</summary>
        public IEnumerator Show(RunState run, Action changed)
        {
            var e = run.CurrentEvent;
            count = Mathf.Min(run.EventOptions.Count, MaxOptions);
            icon.sprite = Make.Lib.Sprite("evt_" + e.type.ToLowerInvariant());
            heading.Text = e.name; kind.Text = e.type.ToUpperInvariant() + "  -  DEPTH " + run.Depth; text.Text = e.text;
            for (int i = 0; i < MaxOptions; i++)
            {
                rows[i].root.gameObject.SetActive(i < count);
                if (i >= count) continue;
                rows[i].over = rows[i].down = false;
                rows[i].label.Text = run.EventOptions[i].label;
                rows[i].summary.Text = Wording.OptionSummary(run.EventOptions[i]).ToUpperInvariant();
            }
            outcome.Text = ""; foreach (var n in notes) n.Text = "";
            proceed.gameObject.SetActive(false);
            picked = -1; closing = false; choosing = false;
            gameObject.SetActive(true);
            yield return Anim.Over(0.35f, t =>
            {
                shade.color = new Color(0, 0, 0, 0.62f * t);
                body.localPosition = new Vector3(0, Mathf.Round((1 - Anim.OutBack(t)) * -40), 0);
            });
            choosing = true;
            while (picked < 0) yield return null;
            choosing = false;

            var result = run.ChooseEventOption(picked);
            changed?.Invoke();
            for (int i = 0; i < MaxOptions; i++) rows[i].root.gameObject.SetActive(false);
            outcome.Text = result.Text ?? "";
            for (int i = 0; i < NoteCount; i++) notes[i].Text = i < result.Notes.Count ? result.Notes[i] : "";
            if (result.Notes.Count > 0) ScreenFx.I.Flash(0.12f, 0.25f);
            proceed.gameObject.SetActive(true);
            while (!closing) yield return null;
            yield return Anim.Over(0.2f, t => shade.color = new Color(0, 0, 0, 0.62f * (1 - t)));
            gameObject.SetActive(false);
        }

        void Update()
        {
            for (int i = 0; i < count && i < MaxOptions; i++)
            {
                var r = rows[i];
                r.body.sprite = Make.Lib.Sprite(r.down ? "option_down" : "option_up");
                r.body.color = r.over && !r.down ? new Color(1.25f, 1.15f, 1.05f, 1) : Color.white;
                r.label.transform.localPosition = new Vector3(0, r.down ? 7 : 9, 0);
            }
        }
    }
}
