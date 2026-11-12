using System.Collections;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The summary at the end of a run, won or lost.</summary>
    public sealed class RunOverView : MonoBehaviour
    {
        const int O = 450, X = 160, Y = 75;
        /// <summary>True when the player chose to start another run rather than return to the title.</summary>
        public bool Again { get; private set; }

        Label heading, sub;
        readonly Label[] names = new Label[3], values = new Label[3];
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
            string[] rows = { "Depth reached", "Discoveries this run", "Journal" };
            for (int i = 0; i < 3; i++)
            {
                names[i] = Make.Text(body, rows[i], X + 28, Y + 80 + i * 20, FontKind.Body, 16, Px.Hex("#a88a6a"), O + 2);
                values[i] = Make.Text(body, "", X + 292, Y + 78 + i * 20, FontKind.Title, 16, Px.Hex("#f0dcc0"), O + 2, Align.Right, Px.Hex("#0c0706"));
            }
            Make.Rect(body, X + 24, Y + 146, 272, 1, Px.Hex("#6a4428"), O + 1);
            UiButton.Create(body, "Descend again", X + 28, Y + 164, O + 2, () => Pick(true));
            UiButton.Create(body, "Title", X + 172, Y + 164, O + 2, () => Pick(false));
            gameObject.SetActive(false);
        }

        /// <summary>Closes the summary with the given choice (also used by scripted play).</summary>
        public void Pick(bool again) { if (gameObject.activeSelf && !done) { Again = again; done = true; } }

        public IEnumerator Show(bool won, RunState run, Journal journal, ContentDb db)
        {
            heading.Text = won ? "The Great Work" : "The Fire Dies";
            heading.Color = Px.Hex(won ? "#f8d250" : "#ee6a3c");
            sub.Text = won ? "You made the " + db.Material(db.Ultimate).name + "." : "Out of trials in " + db.DepthName(run.Depth) + ".";
            values[0].Text = run.Depth + " of " + RunState.FinalDepth;
            values[1].Text = run.DiscoveriesThisRun.ToString();
            values[2].Text = journal.KnownPairs.Count + " of " + db.Recipes.Count;
            done = false;
            gameObject.SetActive(true);
            var body = transform.GetChild(1);
            yield return Anim.Over(0.35f, t =>
            {
                shade.color = new Color(0, 0, 0, 0.62f * t);
                body.localPosition = new Vector3(0, Mathf.Round((1 - Anim.OutBack(t)) * -40), 0);
            });
            while (!done) yield return null;
            gameObject.SetActive(false);
        }
    }
}
