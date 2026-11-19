using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Athanor.Game
{
    /// <summary>The decisions around the tutorial that need no scene, so they can be tested. What Begin does is the Core rule TutorialScript.Plan.</summary>
    public static class TutorialRules
    {
        /// <summary>Skipping (or finishing) the tutorial: never asked again.</summary>
        public static void MarkDone() => Save.SetTutorialDone(true);

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
    /// The dark screen is one sprite drawn "visible outside mask"; each lit hole is a SpriteMask (the inverse mask), see <see cref="TutorialHole"/>.
    /// </summary>
    public sealed class TutorialController : MonoBehaviour
    {
        // the overlay's fixed parts (the Tutorial object of Main.unity); only the popup lines and the Broken sprites are made per step
        [SerializeField] Transform root;
        [SerializeField, FormerlySerializedAs("cells")] Transform extras;
        [SerializeField] Label line;
        [SerializeField] SpriteRenderer boxFill, boxEdge, arrow;
        [SerializeField] SpriteRenderer dim;                 // the whole screen, dark; the holes are masked out of it
        [SerializeField] Clickable lockPad;                  // on the dim: a click on the dark pulses the rim; the holes are cut out of it
        [SerializeField] Clickable readPad;                  // just under the lock: on a read step a click in a hole advances too
        [SerializeField] List<TutorialHole> holes = new List<TutorialHole>();   // the pool; a step with more holes than this adds one
        [SerializeField] TutorialTargets targets = new TutorialTargets();
        [SerializeField] TutorialStyle style = new TutorialStyle();
        readonly List<Label> labels = new List<Label>();
        Rect[] holeRects = new Rect[0]; bool rimOn; float pulse, bump, dimAlpha;
        bool advance, advanceOnClick, shown; float armed; long brokenTotal;
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

        void Awake()
        {
            dimAlpha = dim.color.a;                          // the dark of the scene's Dim object
            lockPad.onClick = () => { bump = 1; if (advanceOnClick && Time.unscaledTime >= armed) advance = true; };
            readPad.onClick = () => { if (Time.unscaledTime >= armed) advance = true; };
        }

        // ---- overlay ----------------------------------------------------------------------------

        /// <summary>The box round the holes. No holes (a text-only step such as 2.3) is the whole canvas: a full dim, no rim, no arrow.</summary>
        public static Rect Union(Rect[] r) { if (r == null || r.Length == 0) return new Rect(0, 0, Px.W, Px.H); var u = r[0]; for (int i = 1; i < r.Length; i++) u = Rect.MinMaxRect(Mathf.Min(u.xMin, r[i].xMin), Mathf.Min(u.yMin, r[i].yMin), Mathf.Max(u.xMax, r[i].xMax), Mathf.Max(u.yMax, r[i].yMax)); return u; }

        void Clear()
        {
            foreach (var h in holes) h.Close();
            foreach (Transform c in extras) Destroy(c.gameObject);
            labels.Clear();
        }

        /// <summary>Dark everywhere except the holes. Clicks on the dark pulse the rim once (and advance when advanceOnClick).</summary>
        void Show(string text, Rect[] rects, bool advanceOnRead, bool dark = true, string[] lines = null, bool free = false)
        {
            Clear(); advance = false; advanceOnClick = advanceOnRead; shown = true; armed = Time.unscaledTime + style.clickGuard;
            root.gameObject.SetActive(true);
            line.Text = text;
            // whole, even pixel edges: the pixel-perfect camera rounds a mask edge on a half pixel, and the dark would creep into the hole
            // on one side and the hole into the dark on the other
            if (holeRects.Length != rects.Length) holeRects = new Rect[rects.Length];
            float g = Mathf.Max(1, style.holeSnap);
            for (int i = 0; i < rects.Length; i++)
            {
                var h = rects[i];
                holeRects[i] = Rect.MinMaxRect(Mathf.Floor(h.xMin / g) * g, Mathf.Floor(h.yMin / g) * g, Mathf.Ceil(h.xMax / g) * g, Mathf.Ceil(h.yMax / g) * g);
            }
            var u = Union(holeRects);
            bool full = u.width >= Px.W - 1 && u.height >= Px.H - 1;
            rimOn = (dark || free) && !full;
            // the dark, and the holes cut out of it
            dim.color = dim.color.Alpha(dark ? dimAlpha : 0f);
            lockPad.gameObject.SetActive(!free);
            lockPad.holes.Clear();
            while (holes.Count < holeRects.Length) holes.Add(Instantiate(holes[0], holes[0].transform.parent));
            for (int i = 0; i < holeRects.Length; i++)
            {
                holes[i].Open(holeRects[i], rimOn);
                lockPad.holes.Add(new Rect(holeRects[i].x - Px.W / 2f, Px.H / 2f - holeRects[i].yMax, holeRects[i].width, holeRects[i].height));   // world space
            }
            readPad.gameObject.SetActive(advanceOnClick);    // a read step: a click in the lit holes advances too, and never reaches the bench
            Dev.Hook("[tut] " + text + (lines != null ? " | " + string.Join(" / ", lines) : "") + " (holes " + holeRects.Length + ")");
            PlaceRim(1);
            arrow.gameObject.SetActive(rimOn);
            // the held popup: score parts under the text box
            if (lines != null)
                for (int k = 0; k < lines.Length; k++)
                    labels.Add(Spawn.Text(extras, lines[k], style.popupPos.x, style.popupPos.y + k * style.popupPitch, FontKind.Body, style.popupFontSize, k == lines.Length - 1 ? style.popupTotalColor : style.popupColor, style.baseOrder + style.popupOrderOffset, Align.Centre, style.popupShadow));
            // SPEC change 17: height = 12 + lines x pitch (+12 on a read step), never lower than y 100
            int pitch = Loc.Vi ? style.linePitchVi : style.linePitch, maxLines = Loc.Vi ? style.maxLinesVi : style.maxLines;
            int n = Mathf.Clamp(Mathf.CeilToInt(line.Width / style.textWrapWidth), 1, maxLines);
            var boxH = Mathf.Min(style.boxPadding + n * pitch + (advanceOnClick ? style.readExtra : 0), style.boxMaxHeight);
            float bx = style.boxPos.x, by = style.boxPos.y + boxDy, b = style.boxBorder;
            Px.Fit(boxEdge.transform, bx, by, style.boxWidth, boxH); Px.Fit(boxFill.transform, bx + b, by + b, style.boxWidth - 2 * b, boxH - 2 * b);
            line.transform.position = Px.At(bx + style.textInset.x, by + style.textInset.y);
        }

        /// <summary>The rim's opacity (a) and the arrow, which sits over the last hole (the thing to click), or under it when that is near the top.</summary>
        void PlaceRim(float a)
        {
            for (int k = 0; k < holeRects.Length; k++) holes[k].SetRimAlpha(rimOn ? a : 0);
            if (holeRects.Length == 0) return;
            var last = holeRects[holeRects.Length - 1];
            float ay = last.yMin > style.arrowFlipY ? last.yMin - style.arrowAbove : last.yMax + style.arrowBelow;
            arrow.transform.position = Px.At(last.center.x, ay + Mathf.Round(Mathf.Sin(Time.time * style.arrowBobSpeed) * style.arrowBobAmplitude));
        }

        void Update()
        {
            if (!shown) return;
            pulse += Time.deltaTime; bump = Mathf.Max(0, bump - Time.deltaTime * style.bumpDecay);
            PlaceRim(Mathf.Clamp01(style.rimBase + style.rimAmplitude * Mathf.Sin(pulse * style.rimSpeed) + bump));
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
                var r = Spawn.Sprite(extras, lay[i].Id, Px.Mid(rects[i].x, rects[i].y, rects[i].width, rects[i].height), style.baseOrder + style.brokenOrderOffset);
                r.color = i == lay.Length - 1 ? style.brokenGhost : Color.white;
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

        Rect TileRect(int i)
        {
            var c = f.Bench.TileCentre(i);
            return new Rect(Mathf.Round(c.x + Px.W / 2f - targets.tileShift.x), Mathf.Round(Px.H / 2f - c.y - targets.tileShift.y), targets.tileSize.x, targets.tileSize.y);
        }
        /// <summary>The middle material of the scripted chain, the one the others touch (steps 3.2 and 3.5).</summary>
        Rect ChainHub => TileRect(TutorialScript.Chain.Lay[1].Tile);
        Rect HandRect { get { var r = targets.hand; return new Rect(r.x, r.y, r.width, Mathf.Min(r.height, Px.H - r.y)); } }   // never lower than the screen
        Rect IgniteRect => targets.ignite;
        Rect JournalRect => targets.journal;
        Rect RackRect => targets.rack;
        Rect RedrawRect { get { float m = targets.redrawMargin; var r = f.Bench.Hud.RedrawRect; return new Rect(r.x - m, r.y - m, r.width + 2 * m, r.height + 2 * m); } }

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
            var hub = TileRect(e.Hub);
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
                    yield return SayText(Loc.T("tutorial.3_5"), new[] { ChainHub }, both);
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
            Show(Loc.T("tutorial.5_3"), new[] { targets.offer }, false, false);
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
            var bench = Union(Enumerable.Range(0, Core.Bench.TileCount).Select(TileRect).ToArray());
            // trial 1: lay, Ignite, reaction
            yield return WaitBench(0);
            yield return Say("tutorial.1_1", bench);
            yield return Lay("tutorial.1_2", TutorialScript.Reaction, 1);
            yield return Lay("tutorial.1_3", TutorialScript.Reaction, 2);
            yield return Say("tutorial.1_4", TutorialScript.Reaction.Lay.Select(l => TileRect(l.Tile)).Prepend(HandRect).ToArray());
            yield return IgniteStep("tutorial.1_5");
            // trial 2: a miss, and the Journal
            yield return WaitBench(1);
            yield return Lay("tutorial.2_1", TutorialScript.Miss, 2);
            yield return IgniteStep("tutorial.2_2");
            yield return WaitBench(2);
            yield return Await("tutorial.2_4", () => f.State == FlowState.Journal, JournalRect);
            var jv = f.JournalView;
            var cell = jv.ShowMaterial("ember_coal");
            boxDy = style.journalBoxDrop;
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
            yield return Say("tutorial.3_2", ChainHub);
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
