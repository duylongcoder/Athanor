using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The decisions around the tutorial that need no scene, so they can be tested. What Begin does is the Core rule TutorialScript.Plan.</summary>
    public static class TutorialRules
    {
        /// <summary>Skipping (or finishing) the tutorial: never asked again.</summary>
        public static void MarkDone() => Save.TutorialDone = true;

        /// <summary>
        /// The answer to the skip question. Skip marks the tutorial done and the run starts at once (owner Q3, no second Begin);
        /// Play it replays the tutorial in the throwaway sandbox and leaves the save as it is (owner Q4). True: play the replay.
        /// </summary>
        public static bool AnswerSkipQuestion(bool playIt)
        {
            if (!playIt) MarkDone();
            return playIt;
        }
    }

    /// <summary>
    /// Plays the scripted tutorial over the bench: a dark screen with lit holes and a pulsing gold rim, an arrow,
    /// a text box at the top, click locks, and popups held until read. It reads the game's state; it decides no rule.
    /// </summary>
    public sealed class TutorialController : MonoBehaviour
    {
        const int Z = 600;
        static readonly Color Gold = Px.Hex("#f8d250");

        // the overlay's fixed parts (the Tutorial object of Main.unity); the dim cells and rim pieces are made per step
        [SerializeField] Transform root, cells;
        [SerializeField] Label line;
        [SerializeField] SpriteRenderer boxFill, boxEdge, arrow;
        readonly List<SpriteRenderer> rim = new List<SpriteRenderer>();
        readonly List<Label> extra = new List<Label>();
        Rect[] rimRects = new Rect[0]; Rect rimRect; bool rimOn; float pulse, bump;
        bool advance, shown; float armed; long brokenTotal;
        int trial;
        int boxDy;   // the box sits lower while the Journal is open (steps 2.5, 2.6), so it does not cover the page header
        string catalystName = "";
        GameFlow f;

        /// <summary>The trial being taught: 0 reaction, 1 miss, 2 chain, 3 the catalyst trial of depth 2.</summary>
        public int Trial => trial;
        /// <summary>True from the first line to the last (the real journal is never touched in a replay; see GameFlow).</summary>
        public bool Finished { get; private set; }
        /// <summary>The scores the popups showed, in order (checked by the tests against TutorialTests: 40 / 0 / 40 / 240 / ...).</summary>
        public readonly List<long> Shown = new List<long>();

        /// <summary>Gives the controller the game it reads; the lessons run in Drive.</summary>
        public void Bind(GameFlow flow) => f = flow;

#if UNITY_EDITOR
        /// <summary>Bake source for the Tutorial object of Main.unity (ViewBaker.BakeTutorial): the text box, the arrow and the (empty) cell root.</summary>
        public void Build()
        {
            root = Make.Root("Tutorial", transform);
            cells = Make.Root("cells", root);
            boxEdge = Make.Rect(root, 168, 14, 304, 50, Gold, Z + 10);
            boxFill = Make.Rect(root, 169, 15, 302, 48, Px.Hex("#1a0e0a"), Z + 11);
            line = Make.Text(root, "", 176, 21, FontKind.Body, 16, Px.Hex("#f0dcc0"), Z + 12, Align.Left, Px.Hex("#0c0706"), 288);
            arrow = Make.Rect(root, 0, 0, 7, 7, Gold, Z + 9);
            arrow.transform.rotation = Quaternion.Euler(0, 0, 45);
            root.gameObject.SetActive(false);
        }
#endif

        // ---- overlay ----------------------------------------------------------------------------

        /// <summary>The box round the holes. No holes (a text-only step such as 2.3) is the whole canvas: a full dim, no rim, no arrow.</summary>
        public static Rect Union(Rect[] r) { if (r == null || r.Length == 0) return new Rect(0, 0, Px.W, Px.H); var u = r[0]; for (int i = 1; i < r.Length; i++) u = Rect.MinMaxRect(Mathf.Min(u.xMin, r[i].xMin), Mathf.Min(u.yMin, r[i].yMin), Mathf.Max(u.xMax, r[i].xMax), Mathf.Max(u.yMax, r[i].yMax)); return u; }

        void Clear()
        {
            foreach (Transform c in cells) Destroy(c.gameObject);
            foreach (var r in rim) if (r != null) Destroy(r.gameObject);
            rim.Clear();
            foreach (var l in extra) if (l != null) Destroy(l.gameObject);
            extra.Clear();
        }

        /// <summary>Dark everywhere except the holes. Clicks on the dark pulse the rim once (and advance when advanceOnClick).</summary>
        void Show(string text, Rect[] holes, bool advanceOnClick, bool dim = true, string[] lines = null, bool free = false)
        {
            Clear(); advance = false; shown = true; armed = Time.unscaledTime + 0.25f;
            root.gameObject.SetActive(true);
            line.Text = text;
            // whole, even pixel edges: a dim rect with an odd width or height has its centre on a half pixel, the pixel-perfect camera
            // rounds that away, and the seam shows as a bright line one side and a dark line the other
            holes = holes.Select(h => Rect.MinMaxRect(Mathf.Floor(h.xMin / 2) * 2, Mathf.Floor(h.yMin / 2) * 2, Mathf.Ceil(h.xMax / 2) * 2, Mathf.Ceil(h.yMax / 2) * 2)).ToArray();
            // the grid cells made of the holes' edges; a cell inside a hole stays lit and lets the click through
            var xs = new SortedSet<float> { 0, Px.W }; var ys = new SortedSet<float> { 0, Px.H };
            foreach (var h in holes) { xs.Add(Mathf.Clamp(h.xMin, 0, Px.W)); xs.Add(Mathf.Clamp(h.xMax, 0, Px.W)); ys.Add(Mathf.Clamp(h.yMin, 0, Px.H)); ys.Add(Mathf.Clamp(h.yMax, 0, Px.H)); }
            var X = xs.ToArray(); var Y = ys.ToArray();
            // merged into the fewest rectangles (a run across a row, then rows of the same span stacked), so the dim has no seams
            // between neighbouring cells; only edges that touch a lit hole remain
            var runs = new List<Rect>();
            for (int j = 0; j + 1 < Y.Length; j++)
            {
                float start = -1;
                for (int i = 0; i + 1 < X.Length; i++)
                {
                    bool lit = holes.Any(h => h.Contains(new Vector2((X[i] + X[i + 1]) / 2, (Y[j] + Y[j + 1]) / 2)));
                    if (!lit && start < 0) start = X[i];
                    if (start >= 0 && (lit || i + 2 >= X.Length)) { runs.Add(Rect.MinMaxRect(start, Y[j], lit ? X[i] : X[i + 1], Y[j + 1])); start = -1; }
                }
            }
            for (int a = 0; a < runs.Count; a++)
                for (int b = a + 1; b < runs.Count; b++)
                    if (Mathf.Approximately(runs[a].xMin, runs[b].xMin) && Mathf.Approximately(runs[a].xMax, runs[b].xMax) && Mathf.Approximately(runs[a].yMax, runs[b].yMin))
                    { runs[a] = Rect.MinMaxRect(runs[a].xMin, runs[a].yMin, runs[a].xMax, runs[b].yMax); runs.RemoveAt(b); b = a; }
            foreach (var run in runs)
                {
                    float w = run.width, h2 = run.height;
                    var r = Make.Rect(cells, run.xMin, run.yMin, w, h2, new Color(0.03f, 0.01f, 0.07f, dim ? 0.88f : 0f), Z);
                    if (!free) Make.Click(r.gameObject, new Vector2(w, h2), Z, () => { bump = 1; if (advanceOnClick && Time.unscaledTime >= armed) advance = true; });
                }
            if (advanceOnClick)   // a read step: a click in the lit holes advances too, and never reaches the bench
                Make.Click(Make.Rect(cells, 0, 0, Px.W, Px.H, Color.clear, Z - 1).gameObject, new Vector2(Px.W, Px.H), Z - 1, () => { if (Time.unscaledTime >= armed) advance = true; });
            // rim and arrow around the union of the holes
            var u = Union(holes);
            bool full = u.width >= Px.W - 1 && u.height >= Px.H - 1;
            rimOn = (dim || free) && !full; rimRect = u;
            rimRects = full ? new Rect[0] : holes;
            for (int k = 0; k < 4 * rimRects.Length; k++) rim.Add(Make.Rect(cells, 0, 0, 2, 2, Gold, Z + 5));
            Dev.Hook("[tut] " + text + (lines != null ? " | " + string.Join(" / ", lines) : "") + " (dim cells " + cells.childCount + ")");
            PlaceRim(1);
            arrow.gameObject.SetActive(rimOn);
            // the held popup: score parts under the text box
            if (lines != null)
                for (int k = 0; k < lines.Length; k++)
                    extra.Add(Make.Text(cells, lines[k], 320, 70 + k * 20, FontKind.Body, 16, k == lines.Length - 1 ? Px.Hex("#fff6b8") : Px.Hex("#f0dcc0"), Z + 12, Align.Centre, Px.Hex("#0c0706")));
            // SPEC change 17: height = 12 + lines x pitch (+12 on a read step), never lower than y 100
            int pitch = Loc.Vi ? 16 : 12, maxLines = Loc.Vi ? 3 : 4;
            int n = Mathf.Clamp(Mathf.CeilToInt(line.Width / 288f), 1, maxLines);
            var boxH = Mathf.Min(12 + n * pitch + (advanceOnClick ? 12 : 0), 100 - 14);
            Place(boxEdge, 168, 14 + boxDy, 304, boxH); Place(boxFill, 169, 15 + boxDy, 302, boxH - 2);
            line.transform.position = Px.At(176, 21 + boxDy);
        }

        void PlaceRim(float a)
        {
            const float t = 2;
            for (int k = 0; k < rimRects.Length && rim.Count >= 4 * (k + 1); k++)
            {
                var u = rimRects[k]; int i = 4 * k;
                Place(rim[i], u.xMin - t, u.yMin - t, u.width + 2 * t, t); Place(rim[i + 1], u.xMin - t, u.yMax, u.width + 2 * t, t);
                Place(rim[i + 2], u.xMin - t, u.yMin, t, u.height); Place(rim[i + 3], u.xMax, u.yMin, t, u.height);
            }
            foreach (var r in rim) r.color = new Color(Gold.r, Gold.g, Gold.b, rimOn ? a : 0);
            // the arrow sits over the last hole (the thing to click), or under it when that is near the top
            if (rimRects.Length == 0) return;
            var last = rimRects[rimRects.Length - 1];
            float ay = last.yMin > 80 ? last.yMin - 10 : last.yMax + 8;
            arrow.transform.position = Px.At(last.center.x, ay + Mathf.Round(Mathf.Sin(Time.time * 6) * 2));
        }

        static void Place(SpriteRenderer r, float x, float y, float w, float h)
        {
            r.transform.position = Px.Mid(x, y, w, h); r.transform.localScale = new Vector3(w / 4f, h / 4f, 1);
        }

        void Update()
        {
            if (!shown) return;
            pulse += Time.deltaTime; bump = Mathf.Max(0, bump - Time.deltaTime * 3);
            PlaceRim(Mathf.Clamp01(0.55f + 0.3f * Mathf.Sin(pulse * 5) + bump));
        }

        /// <summary>Quit to title in the tutorial: stops the lessons and takes the overlay down. Does not mark the tutorial done.</summary>
        public void Abort() { StopAllCoroutines(); Hide(); }

        void Hide() { shown = false; Clear(); root.gameObject.SetActive(false); }

        // ---- steps ------------------------------------------------------------------------------

        /// <summary>A line to read: every click advances it, the holes only show what it talks about.</summary>
        IEnumerator Say(string key, params Rect[] holes) => SayText(Loc.T(key), holes, null);

        IEnumerator SayText(string text, Rect[] holes, string[] lines)
        {
            Show(text, holes, true, true, lines);
            yield return null;
            while (!advance) yield return null;
            Hide();
        }

        /// <summary>A line and a lock: clicks in the holes go through, every other click pulses the rim once. Ends when done().</summary>
        IEnumerator Await(string key, Func<bool> done, params Rect[] holes)
        {
            Show(Loc.T(key), holes, false);
            while (!done()) yield return null;
            Hide();
        }

        /// <summary>Step 3.6: the chain without Moondust, its bench greyed beside the score worked out before the Ignite (nothing was spent).</summary>
        IEnumerator Broken()
        {
            var lay = TutorialScript.Chain.Lay;
            var rects = lay.Select(l => TileRect(l.Tile)).ToArray();
            Show(Loc.T("tutorial.3_6"), new[] { Union(rects) }, true, true, new[] { HudView.Number(brokenTotal) });
            for (int i = 0; i < lay.Length; i++)
            {
                var r = Make.Sprite(cells, lay[i].Id, Px.Mid(rects[i].x, rects[i].y, rects[i].width, rects[i].height), Z + 6);
                r.color = i == lay.Length - 1 ? new Color(0.45f, 0.45f, 0.45f, 0.35f) : Color.white;
            }
            yield return null;
            while (!advance) yield return null;
            Hide();
        }

        /// <summary>The materials of a trial that have been laid, wrong ones taken back at once.</summary>
        bool Laid(TutorialTrial t, int n)
        {
            var run = f.Run; int ok = 0; bool changed = false;
            for (int i = 0; i < t.Lay.Length; i++)
            {
                var lay = t.Lay[i]; var on = run.Bench.Tiles[lay.Tile];
                if (i >= n) { if (on != null) { run.Unplace(lay.Tile); changed = true; } continue; }
                if (on == lay.Id) ok++;
                else if (on != null) { run.Unplace(lay.Tile); changed = true; }
            }
            for (int tile = 0; tile < Core.Bench.TileCount; tile++)       // anything off the script goes back to the hand
                if (run.Bench.Tiles[tile] != null && !t.Lay.Take(n).Any(l => l.Tile == tile)) { run.Unplace(tile); changed = true; }
            if (changed) f.Bench.Refresh();
            return ok == n;
        }

        IEnumerator Lay(string key, TutorialTrial t, int n)
        {
            var holes = new List<Rect> { HandRect };
            for (int i = 0; i < n; i++) holes.Add(TileRect(t.Lay[i].Tile));
            yield return Await(key, () => Laid(t, n), holes.ToArray());
        }

        /// <summary>Waits for the player to press Ignite and for the Ignite to play out.</summary>
        IEnumerator IgniteStep(string key)
        {
            yield return Await(key, () => f.State == FlowState.Resolving, IgniteRect);
            while (f.State == FlowState.Resolving) yield return null;
        }

        // ---- screen rectangles (art space, top left origin) -------------------------------------

        Rect TileRect(int i) { var c = f.Bench.TileCentre(i); return new Rect(Mathf.Round(c.x + 320 - 36), Mathf.Round(180 - c.y - 24), 72, 44); }
        static Rect HandRect => new Rect(126, 284, 392, Mathf.Min(80, Px.H - 284));
        static Rect IgniteRect => new Rect(515, 304, 122, 42);
        static Rect JournalRect => new Rect(516, 284, 122, 24);
        static Rect RackRect => new Rect(126, 252, 392, 32);
        static Rect RedrawRect => new Rect(HudView.RedrawX - 2, HudView.RedrawY - 2, HudView.RedrawW + 4, HudView.RedrawH + 4);

        // ---- hooks called by GameFlow during the Ignite -------------------------------------------

        /// <summary>After a reaction (or fizzle) has played: the held popups of the script.</summary>
        public IEnumerator AfterEvent(ReactionEvent e, int index)
        {
            if (f == null) yield break;
            var parts = ScoreParts.Of(f.Db, e);
            if (parts == null)
            {
                if (trial == 1 && !e.Sputter) yield return Say("tutorial.2_3");
                yield break;
            }
            Shown.Add(parts.Total);
            var hub = new Rect(TileRect(e.Hub).x, TileRect(e.Hub).y, 72, 44);
            switch (trial)
            {
                case 0:
                    yield return SayText(Loc.T("tutorial.1_6"), new[] { hub }, PartLines(parts));
                    yield return Say("tutorial.1_6b", hub);
                    break;
                case 2:
                    yield return SayText(Loc.T("tutorial.step", ("n", e.Chain)), new[] { hub }, PartLines(parts));
                    break;
                case 3:
                    yield return SayText(Loc.T("tutorial.5_4"), new[] { hub }, PartLines(parts));
                    break;
            }
        }

        string[] PartLines(ScoreParts p)
        {
            var l = new List<string>
            {
                Loc.T("tutorial.part.base", ("n", p.Base * p.Used)),
                Loc.T("tutorial.part.first", ("n", p.First)),
                Loc.T("tutorial.part.chain", ("n", p.Step)),
            };
            if (p.Bonus > 1.001) l.Add(Loc.T("tutorial.part.catalyst", ("name", catalystName), ("n", Mathf.RoundToInt((float)p.Bonus))));
            l.Add(HudView.Number(p.Total));
            return l.ToArray();
        }

        /// <summary>After the Ignite has played and its materials are spent: the next scripted hand, and the lines that follow.</summary>
        public IEnumerator AfterIgnite(IgniteOutcomeView o)
        {
            if (f == null) yield break;
            switch (trial)
            {
                case 0:
                    yield return Say("tutorial.1_7", RackRect);
                    break;
                case 2:
                    yield return Say("tutorial.3_4");
                    var chain = o.Events.Where(e => e.Result != null).Select(e => ScoreParts.Of(f.Db, e)).Where(p => p != null).ToList();
                    var both = chain.Select(p => $"{p.Base * p.Used} x{p.First} x{p.Step} = {HudView.Number(p.Total)}").ToArray();
                    yield return SayText(Loc.T("tutorial.3_5"), new[] { TileRect(4) }, both);
                    yield return Broken();
                    break;
            }
            trial++;
            if (trial < 3 && f.Run.Phase == RunPhase.Playing)
            {
                for (int t = 0; t < Core.Bench.TileCount; t++) f.Run.Unplace(t);     // a fizzle leaves the materials on the bench
                f.Run.Deal((trial == 1 ? TutorialScript.Miss : TutorialScript.Chain).Hand);
            }
        }

        /// <summary>The catalyst was taken: deal the scripted hand that shows it.</summary>
        public void AfterChoose(string catalystId, string name)
        {
            catalystName = name;
            var t = TutorialScript.CatalystTrial(catalystId);
            if (t != null) f.Run.Deal(t.Hand);
            catalystTrial = t;
        }
        TutorialTrial catalystTrial;

        /// <summary>The lines of the catalyst offer; the choice itself is the game's own screen.</summary>
        public IEnumerator OnOffer()
        {
            yield return Say("tutorial.5_1");
            yield return Say("tutorial.5_2");
            Show(Loc.T("tutorial.5_3"), new[] { new Rect(0, 60, Px.W, 240) }, false, false);
        }

        public void OfferTaken() => Hide();

        // ---- the driver --------------------------------------------------------------------------

        IEnumerator WaitBench(int t)
        {
            while (!(f.State == FlowState.Bench && trial == t && f.Bench.InputEnabled)) yield return null;
            yield return null;
        }

        /// <summary>Runs the lessons from the first bench to line 5.5. The game plays its own Ignite, descents and choice between.</summary>
        public IEnumerator Drive()
        {
            Dev.Hook("[drive] start");
            trial = 0; Finished = false; Shown.Clear();
            var all = new Rect(0, 0, Px.W, Px.H);
            var bench = Union(Enumerable.Range(0, Core.Bench.TileCount).Select(TileRect).ToArray());
            // trial 1: lay, Ignite, reaction
            yield return WaitBench(0);
            yield return Say("tutorial.1_1", bench);
            yield return Lay("tutorial.1_2", TutorialScript.Reaction, 1);
            yield return Lay("tutorial.1_3", TutorialScript.Reaction, 2);
            yield return Say("tutorial.1_4", HandRect, TileRect(4), TileRect(5));
            yield return IgniteStep("tutorial.1_5");
            // trial 2: a miss, and the Journal
            yield return WaitBench(1);
            yield return Lay("tutorial.2_1", TutorialScript.Miss, 2);
            yield return IgniteStep("tutorial.2_2");
            yield return WaitBench(2);
            yield return Await("tutorial.2_4", () => f.State == FlowState.Journal, JournalRect);
            var jv = f.JournalView;
            var cell = jv.ShowMaterial("ember_coal");
            boxDy = 248;
            yield return Say("tutorial.2_5", cell, JournalView.RecipesRect);
            yield return Say("tutorial.2_6", JournalView.IndexRect);
            boxDy = 0;
            cell = jv.ShowMaterial("glowmoss");
            Show(Loc.T("tutorial.2_7"), new[] { cell, JournalView.HintsRect }, false, false, null, true);
            while (f.State == FlowState.Journal) yield return null;
            Hide();
            // trial 3: chains
            yield return Say("tutorial.3_0", RedrawRect);
            yield return Lay("tutorial.3_1", TutorialScript.Chain, 3);
            yield return Say("tutorial.3_2", TileRect(4));
            brokenTotal = f.Run.Preview(TutorialScript.BrokenChain()).Sum(e => e.Potency);     // worked out before the Ignite, so nothing is spent
            yield return IgniteStep("tutorial.3_3");
            // the clear, the descent and the catalyst choice are the game's; then the catalyst trial
            while (!(f.State == FlowState.Bench && trial == 3 && f.Bench.InputEnabled)) yield return null;
            yield return null;
            var ct = catalystTrial;
            if (ct != null)
            {
                yield return Lay("tutorial.5_4", ct, ct.Lay.Length);
                yield return IgniteStep("tutorial.5_4");
                while (f.State != FlowState.Bench) yield return null;
            }
            yield return Say("tutorial.5_5");
            Finished = true;
            TutorialRules.MarkDone();
        }

        /// <summary>The stage the AfterIgnite hook needs from the game: what the Ignite made.</summary>
        public sealed class IgniteOutcomeView { public List<ReactionEvent> Events; }
    }
}
