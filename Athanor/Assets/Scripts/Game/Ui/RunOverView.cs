using System.Collections;
using Athanor.Core;
using PrimeTween;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The summary at the end of a run, won or lost.</summary>
    public sealed class RunOverView : MonoBehaviour
    {
        const int O = 450, X = 160, Y = 75;
        /// <summary>True when the player chose to start another run rather than return to the title.</summary>
        public bool Again { get; private set; }
        /// <summary>The line about goals was clicked: show the Goals page over this one.</summary>
        public event System.Action GoalsClicked;

        Label heading, sub, goalsLine;
        readonly Label[] names = new Label[4], values = new Label[4];
        SpriteRenderer shade;
        bool done;

        public void Build()
        {
            var t = transform;
            shade = Make.Rect(t, 0, 0, Px.W, Px.H, Color.clear, O - 1);
            Make.Click(shade.gameObject, new Vector2(Px.W, Px.H), O - 1);      // nothing underneath can be clicked
            var body = Make.Root("Dialog", t);
            Make.SpriteAt(body, "dialog", X, Y, O);
            heading = Make.Text(body, "", 320, Y + 14, FontKind.Title, 32, Px.Hex("#f8d250"), O + 2, Align.Centre, Px.Hex("#3a1a08"));
            sub = Make.Text(body, "", 320, Y + 50, FontKind.Body, 16, Px.Hex("#f0dcc0"), O + 2, Align.Centre);
            Make.Rect(body, X + 24, Y + 72, 272, 1, Px.Hex("#6a4428"), O + 1);
            string[] rows = { "ui.runover.depth_reached", "ui.runover.discoveries", "ui.runover.journal", "ui.runover.goals" };
            for (int i = 0; i < rows.Length; i++)
            {
                names[i] = Make.TextKey(body, rows[i], X + 28, Y + 78 + i * 17, FontKind.Body, 16, Px.Hex("#a88a6a"), O + 2);
                values[i] = Make.Text(body, "", X + 292, Y + 76 + i * 17, FontKind.Title, 16, Px.Hex("#f0dcc0"), O + 2, Align.Right, Px.Hex("#0c0706"));
            }
            // which goals, in small print; a click on it opens the Goals page
            goalsLine = Make.Text(body, "", 320, Y + 147, FontKind.Tiny, 8, Px.Hex("#6ee6c8"), O + 2, Align.Centre);
            Make.Click(goalsLine.gameObject, new Vector2(272, 12), O + 2, () => { if (gameObject.activeSelf && !done) GoalsClicked?.Invoke(); }).offset = new Vector2(0, -4);
            Make.Rect(body, X + 24, Y + 160, 272, 1, Px.Hex("#6a4428"), O + 1);
            UiButton.Create(body, "ui.runover.again", X + 28, Y + 170, O + 2, () => Pick(true));
            UiButton.Create(body, "ui.runover.title", X + 172, Y + 170, O + 2, () => Pick(false));
            gameObject.SetActive(false);
        }

        /// <summary>Closes the summary with the given choice (also used by scripted play).</summary>
        public void Pick(bool again) { if (gameObject.activeSelf && !done) { Again = again; done = true; } }

        /// <summary>The depth row's name label (the first row).</summary>
        public Label DepthName => names[0];
        /// <summary>The depth row's value label (right-aligned, empty on a won run).</summary>
        public Label DepthValue => values[0];

        /// <summary>
        /// Sets the depth row: "Depth reached n of 5" on a lost run, one line "Planted at depth n" on a won run (owner, phase-5-prep
        /// 2026-11-16: a win can come before depth 5). The view is reused between runs, so both paths set the name's key again.
        /// </summary>
        public void ShowDepth(bool won, int depth)
        {
            // SetKey binds the key with its argument, so a language change rewrites whichever line is showing
            if (won) names[0].SetKey("ui.runover.planted_at", ("n", depth));
            else names[0].SetKey("ui.runover.depth_reached");
            values[0].Text = won ? "" : Loc.T("ui.common.of", ("a", depth), ("b", RunState.FinalDepth));
        }

        public IEnumerator Show(bool won, RunState run, Journal journal, ContentDb db, System.Collections.Generic.IReadOnlyList<GoalReward> goals)
        {
            heading.Text = Loc.T(won ? "ui.runover.won" : "ui.runover.lost");
            heading.Color = Px.Hex(won ? "#f8d250" : "#ee6a3c");
            sub.Text = won ? Loc.T("ui.runover.won_sub", ("item", db.Material(db.Ultimate).name)) : Loc.T("ui.runover.lost_sub", ("depth", Loc.DepthName(db, run.Depth)));
            ShowDepth(won, run.Depth);
            values[1].Text = run.DiscoveriesThisRun.ToString();
            values[2].Text = Loc.T("ui.common.of", ("a", journal.KnownPairs.Count), ("b", db.Recipes.Count));
            values[3].Text = goals.Count.ToString();
            // as many of the goals' names as fit on one line with "VIEW GOALS" after them: 25 letters, inside the dialog's rules
            const int Fits = 25;
            string list = "";
            for (int i = 0; i < goals.Count; i++)
            {
                string next = (list.Length > 0 ? ", " : "") + Loc.Upper(Loc.C("goal." + goals[i].Goal.id + ".text", goals[i].Goal.text));
                int left = goals.Count - i - 1;
                string more = left > 0 ? Loc.T("ui.runover.and_more", ("list", ""), ("n", left)) : "";
                if (list.Length + next.Length + more.Length > Fits)
                {
                    // the names so far were checked with this ending last time round; with none so far, say how many
                    list = list.Length > 0 ? Loc.T("ui.runover.and_more", ("list", list), ("n", goals.Count - i)) : Loc.P("ui.runover.goal_count", goals.Count);
                    break;
                }
                list += next;
            }
            goalsLine.Text = goals.Count > 0 ? list + "  -  " + Loc.T("ui.runover.view_goals") : Loc.T("ui.runover.view_goals");
            done = false;
            gameObject.SetActive(true);
            var body = transform.GetChild(1);
            yield return Sequence.Create()
                .Group(Motion.Fade(shade, 0, 0.62f, 0.35f))
                .Group(Motion.MoveLocal(body, new Vector3(0, -40, 0), Vector3.zero, 0.35f, Ease.OutBack))
                .ToYieldInstruction();
            while (!done) yield return null;
            gameObject.SetActive(false);
        }
    }
}
