using System;
using System.Collections;
using Athanor.Core;
using PrimeTween;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The event at the start of a depth: what was found, the options that can be taken (each with what it
    /// costs and may give), then what came of the one chosen.
    /// </summary>
    public sealed class EventView : MonoBehaviour
    {
        // the first option and Continue stay where Event.prefab puts them, whatever the event (tools/web/webtest.mjs clicks them)
        const int MaxOptions = 3, NoteCount = 4;

        [Serializable]
        sealed class Row
        {
            public Transform root; public SpriteRenderer body; public Label label, summary; public Clickable click;
            [NonSerialized] public bool over, down;
        }

        [SerializeField] Row[] rows = new Row[MaxOptions];
        [SerializeField] Label[] notes = new Label[NoteCount];
        [SerializeField] SpriteRenderer shade, icon;
        [SerializeField] Label heading, kind, text, outcome;
        [SerializeField] UiButton proceed;
        [SerializeField] Transform body;
        int picked, count;
        bool choosing, closing, wired;

        /// <summary>The label of option row i (rows past the event's option count are hidden).</summary>
        public Label OptionLabel(int i) => rows[i].label;
        /// <summary>What came of the option taken.</summary>
        public Label Outcome => outcome;

        /// <summary>Hooks the clicks. GameFlow calls it once at start; a test calls it after instantiating the prefab.</summary>
        public void Bind()
        {
            if (wired) return;
            wired = true;
            for (int i = 0; i < MaxOptions; i++)
            {
                int index = i;
                var r = rows[i];
                r.click.onClick = () => PickOption(index);
                r.click.onEnter = () => r.over = true;
                r.click.onExit = () => { r.over = false; r.down = false; };
                r.click.onDown = () => r.down = true;
                r.click.onUp = () => r.down = false;
            }
            proceed.Bind();                     // the button's own Awake runs only when the dialog first opens
            proceed.Clicked += Proceed;
        }

        void OnDestroy()
        {
            if (wired) proceed.Clicked -= Proceed;
        }

        void PickOption(int index) { if (choosing && index < count) picked = index; }
        void Proceed() => closing = true;

        /// <summary>Shows the run's current event, takes the option the player picks and shows what it did. Ends when Continue is pressed.</summary>
        public IEnumerator Show(RunState run, Action changed)
        {
            var e = run.CurrentEvent;
            count = Mathf.Min(run.EventOptions.Count, MaxOptions);
            icon.sprite = Art.Lib.Sprite("evt_" + e.type.ToLowerInvariant());
            heading.Text = Loc.C(Lang.EventKey(e.id, "name"), e.name); kind.Text = Loc.T("ui.event.kind", ("type", Loc.Upper(Loc.T("event_type." + e.type.ToLowerInvariant()))), ("n", run.Depth)); text.Text = Loc.C(Lang.EventKey(e.id, "text"), e.text);
            for (int i = 0; i < MaxOptions; i++)
            {
                rows[i].root.gameObject.SetActive(i < count);
                if (i >= count) continue;
                rows[i].over = rows[i].down = false;
                rows[i].label.Text = Wording.OptionLabel(e, run.EventOptions[i]);
                rows[i].summary.Text = Loc.Upper(Wording.OptionSummary(run.EventOptions[i]));
            }
            outcome.Text = ""; foreach (var n in notes) n.Text = "";
            proceed.gameObject.SetActive(false);
            picked = -1; closing = false; choosing = false;
            gameObject.SetActive(true);
            yield return Sequence.Create()
                .Group(Motion.Fade(shade, 0, 0.62f, 0.35f))
                .Group(Motion.MoveLocal(body, new Vector3(0, -40, 0), Vector3.zero, 0.35f, Ease.OutBack))
                .ToYieldInstruction();
            choosing = true;
            Dev.Hook("[event] options");      // development builds only: tools/web/webtest.mjs waits for these three lines
            while (picked < 0) yield return null;
            choosing = false;

            var option = run.EventOptions[picked];      // ChooseEventOption empties EventOptions, so keep the option for the outcome text
            var result = run.ChooseEventOption(picked);
            changed?.Invoke();
            for (int i = 0; i < MaxOptions; i++) rows[i].root.gameObject.SetActive(false);
            outcome.Text = Wording.OutcomeText(e, option, result.Text);
            for (int i = 0; i < NoteCount; i++) notes[i].Text = i < result.Notes.Count ? Wording.NoteText(result.Notes[i]) : "";
            if (result.Notes.Count > 0) ScreenFx.I.Flash(0.12f, 0.25f);
            proceed.gameObject.SetActive(true);
            Dev.Hook("[event] result");
            while (!closing) yield return null;
            yield return Motion.Fade(shade, 0.62f, 0, 0.2f).ToYieldInstruction();
            gameObject.SetActive(false);
            Dev.Hook("[event] closed");
        }

        void Update()
        {
            for (int i = 0; i < count && i < MaxOptions; i++)
            {
                var r = rows[i];
                r.body.sprite = Art.Lib.Sprite(r.down ? "option_down" : "option_up");
                r.body.color = r.over && !r.down ? new Color(1.25f, 1.15f, 1.05f, 1) : Color.white;
                r.label.transform.localPosition = new Vector3(0, r.down ? 7 : 9, 0);
            }
        }
    }
}
