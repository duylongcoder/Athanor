using System.Collections;
using Athanor.Gameplay;
using PrimeTween;
using UnityEngine;
using Hint = UnityEngine.TooltipAttribute;   // not "Tooltip": the game has a Tooltip view of its own

namespace Athanor.Game
{
    /// <summary>The summary at the end of a run, won or lost.</summary>
    public sealed class RunOverView : MonoBehaviour
    {
        /// <summary>True when the player chose to start another run rather than return to the title.</summary>
        public bool Again { get; private set; }
        /// <summary>The line about goals was clicked: show the Goals page over this one.</summary>
        public event System.Action GoalsClicked;

        /// <summary>How the dialog comes in: fade and slide duration, the shade's alpha, where the body slides from.</summary>
        [System.Serializable]
        public struct Timing { public float fadeIn, slide, shadeAlpha, slideFrom; }
        [SerializeField] Timing timing;
        /// <summary>Letters of goal names that fit on one line before VIEW GOALS.</summary>
        [SerializeField, Hint("Letters of goal names that fit on one line before VIEW GOALS.")] int goalLineFits;
        [SerializeField] Label heading, sub, goalsLine;
        [SerializeField] Label[] names = new Label[4], values = new Label[4];
        [SerializeField] SpriteRenderer shade;
        [SerializeField] Transform body;
        [SerializeField] Clickable goalsClick;
        [SerializeField] UiButton again, toTitle;
        bool done, wired;

        /// <summary>Hooks the clicks. GameManager calls it once at start; a test calls it after instantiating the prefab.</summary>
        public void Bind()
        {
            if (wired) return;
            wired = true;
            goalsClick.onClick = OnGoals;
            again.Bind(); toTitle.Bind();       // the buttons' own Awake runs only when the dialog first opens
            again.Clicked += PickAgain;
            toTitle.Clicked += PickTitle;
        }

        void OnDestroy()
        {
            StopAllCoroutines(); Motion.StopOn(this);
            if (!wired) return;
            again.Clicked -= PickAgain;
            toTitle.Clicked -= PickTitle;
        }

        void OnGoals() { if (gameObject.activeSelf && !done) GoalsClicked?.Invoke(); }
        void PickAgain() => Pick(true);
        void PickTitle() => Pick(false);

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
        public void ShowDepth(bool won, int depth, int finalDepth)
        {
            // SetKey binds the key with its argument, so a language change rewrites whichever line is showing
            if (won) names[0].SetKey("ui.runover.planted_at", ("n", depth));
            else names[0].SetKey("ui.runover.depth_reached");
            values[0].Text = won ? "" : Loc.T("ui.common.of", ("a", depth), ("b", finalDepth));
        }

        public IEnumerator Show(bool won, RunState run, Journal journal, ContentDb db, System.Collections.Generic.IReadOnlyList<GoalReward> goals)
        {
            heading.Text = Loc.T(won ? "ui.runover.won" : "ui.runover.lost");
            heading.Color = Px.Hex(won ? "#f8d250" : "#ee6a3c");
            sub.Text = won ? Loc.T("ui.runover.won_sub", ("item", db.Material(db.Ultimate).name)) : Loc.T("ui.runover.lost_sub", ("depth", Loc.DepthName(db, run.Depth)));
            ShowDepth(won, run.Depth, db.FinalDepth);
            values[1].Text = run.DiscoveriesThisRun.ToString();
            values[2].Text = Loc.T("ui.common.of", ("a", journal.KnownPairs.Count), ("b", db.Recipes.Count));
            values[3].Text = goals.Count.ToString();
            // as many of the goals' names as fit on one line with "VIEW GOALS" after them: goalLineFits letters, inside the dialog's rules
            string list = "";
            for (int i = 0; i < goals.Count; i++)
            {
                string next = (list.Length > 0 ? ", " : "") + Loc.Upper(Loc.C("goal." + goals[i].Goal.id + ".text", goals[i].Goal.text));
                int left = goals.Count - i - 1;
                string more = left > 0 ? Loc.T("ui.runover.and_more", ("list", ""), ("n", left)) : "";
                if (list.Length + next.Length + more.Length > goalLineFits)
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
            yield return Sequence.Create()
                .Group(Motion.Fade(shade, 0, timing.shadeAlpha, timing.fadeIn))
                .Group(Motion.MoveLocal(body, new Vector3(0, timing.slideFrom, 0), Vector3.zero, timing.slide, Ease.OutBack))
                .ToYieldInstruction();
            while (!done) yield return null;
            gameObject.SetActive(false);
        }
    }
}
