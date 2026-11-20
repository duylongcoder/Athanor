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
        [SerializeField] Clickable readPad;                  // just under the lock: catches clicks in the lit windows that do not pass through (advance a read step, or pulse the rim)
        [SerializeField] List<TutorialHole> holes = new List<TutorialHole>();   // the pool; a step with more holes than this adds one
        [SerializeField] TutorialStyle style = new TutorialStyle();
        readonly List<Label> labels = new List<Label>();
        Rect[] holeRects = new Rect[0]; bool rimOn; float pulse, bump, dimAlpha;
        bool advance, advanceOnClick, shown; float armed; long brokenTotal;
        int trial;
        bool stepClickAdvances = true;   // the current step's clickAdvances
        Dictionary<string, TutorialStep> stepsById;
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
            readPad.onClick = () => { if (Time.unscaledTime < armed) return; if (advanceOnClick && stepClickAdvances) advance = true; else bump = 1; };
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

        /// <summary>
        /// Dark everywhere except the step's areas. A click on the dark pulses the rim once (and advances when advanceOnClick).
        /// A click in an area that does not pass reaches readPad (advance, or pulse); in an area that passes it reaches the game.
        /// </summary>
        void Show(TutorialStep step, string text, bool advanceOnRead, string[] lines = null, int hub = -1)
        {
            Clear(); advance = false; advanceOnClick = advanceOnRead; stepClickAdvances = step.clickAdvances; shown = true; armed = Time.unscaledTime + style.clickGuard;
            root.gameObject.SetActive(true);
            line.Text = text;
            var areas = Resolve(step, hub);
            // whole, even pixel edges: the pixel-perfect camera rounds a mask edge on a half pixel, and the dark would creep into the hole
            // on one side and the hole into the dark on the other
            if (holeRects.Length != areas.Count) holeRects = new Rect[areas.Count];
            float g = Mathf.Max(1, style.holeSnap);
            for (int i = 0; i < areas.Count; i++)
            {
                var h = areas[i].rect;
                holeRects[i] = Rect.MinMaxRect(Mathf.Floor(h.xMin / g) * g, Mathf.Floor(h.yMin / g) * g, Mathf.Ceil(h.xMax / g) * g, Mathf.Ceil(h.yMax / g) * g);
            }
            var u = Union(holeRects);
            bool full = u.width >= Px.W - 1 && u.height >= Px.H - 1;
            rimOn = (step.dark || !step.lockPad) && !full;
            // the dark, and the holes cut out of it
            dim.color = dim.color.Alpha(step.dark ? dimAlpha : 0f);
            lockPad.gameObject.SetActive(step.lockPad);
            lockPad.holes.Clear(); readPad.holes.Clear();
            while (holes.Count < holeRects.Length) holes.Add(Instantiate(holes[0], holes[0].transform.parent));
            for (int i = 0; i < holeRects.Length; i++)
            {
                holes[i].Open(holeRects[i], rimOn);
                var corner = Px.At(holeRects[i].x, holeRects[i].yMax);      // the hole's bottom-left corner in world space
                var world = new Rect(corner.x, corner.y, holeRects[i].width, holeRects[i].height);
                lockPad.holes.Add(world);                                   // every lit window is open in the lock ...
                if (areas[i].area.clicksPass) readPad.holes.Add(world);     // ... and the ones that pass are open in the read pad too
            }
            readPad.gameObject.SetActive(step.lockPad);
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
            var bp = step.useBoxPos ? step.boxPos : style.boxPos;
            float bx = bp.x, by = bp.y, b = style.boxBorder;
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

        /// <summary>The step object with this id (its name), under the Tutorial object.</summary>
        public TutorialStep Step(string id)
        {
            if (stepsById == null || !stepsById.TryGetValue(id, out var st) || st == null)
            {
                stepsById = new Dictionary<string, TutorialStep>();
                foreach (var x in GetComponentsInChildren<TutorialStep>(true)) stepsById[x.name.StartsWith("Step ") ? x.name.Substring(5) : x.name] = x;   // objects are named "Step 1.4"
                if (!stepsById.TryGetValue(id, out st)) throw new InvalidOperationException("No tutorial step " + id);
            }
            return st;
        }

        /// <summary>A line to read: a click advances it, the areas only show what it talks about.</summary>
        IEnumerator Say(string id, int hub = -1) => SayText(id, null, hub);

        IEnumerator SayText(string id, string[] lines, int hub = -1, string text = null)
        {
            var step = Step(id);
            Show(step, text ?? Loc.T(step.textKey), true, lines, hub);
            yield return null;
            while (!advance) yield return null;
            Hide();
        }

        /// <summary>A line and a lock: clicks in pass-through areas go to the game, every other click pulses the rim once. Ends when done().</summary>
        IEnumerator Await(string id, Func<bool> done, int hub = -1)
        {
            var step = Step(id);
            Show(step, Loc.T(step.textKey), false, null, hub);
            while (!done()) yield return null;
            Hide();
        }

        /// <summary>Step 3.6: the chain without Moondust, its bench greyed beside the score worked out before the Ignite (nothing was spent).</summary>
        IEnumerator Broken()
        {
            var lay = TutorialScript.Chain.Lay;
            var step = Step("3.6");
            Show(step, Loc.T(step.textKey), true, new[] { HudView.Number(brokenTotal) });
            for (int i = 0; i < lay.Length; i++)
            {
                var r = TileRect(lay[i].Tile);
                var sp = Spawn.Sprite(extras, lay[i].Id, Px.Mid(r.x, r.y, r.width, r.height), style.baseOrder + style.brokenOrderOffset);
                sp.color = i == lay.Length - 1 ? style.brokenGhost : Color.white;
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

        /// <summary>Waits for the player to press Ignite and for the Ignite to play out.</summary>
        IEnumerator IgniteStep(string id)
        {
            yield return Await(id, () => f.State == FlowState.Resolving);
            while (f.State == FlowState.Resolving) yield return null;
        }

        // ---- screen rectangles (art space, top left origin) -------------------------------------

        Rect TileRect(int i)
        {
            var c = f.Bench.TileCentre(i);
            float left = c.x + Px.W / 2f - style.tileShift.x, top = Px.H / 2f - c.y - style.tileShift.y;
            return new Rect(Mathf.Round(left), Mathf.Round(top), style.tileSize.x, style.tileSize.y);
        }

        Rect UnionTiles(IEnumerable<int> tiles) => Union(tiles.Select(TileRect).ToArray());

        TutorialTrial TrialOf(ScriptTrial s) => s switch
        {
            ScriptTrial.Reaction => TutorialScript.Reaction,
            ScriptTrial.Miss => TutorialScript.Miss,
            ScriptTrial.Chain => TutorialScript.Chain,
            ScriptTrial.Catalyst => catalystTrial,
            _ => null,
        };

        /// <summary>The rect a view reference stands for right now; null when it is not on screen.</summary>
        Rect? ViewRect(TutorialArea a, int hub)
        {
            switch (a.view)
            {
                case ViewRef.Bench: return UnionTiles(Enumerable.Range(0, Core.Bench.TileCount));
                case ViewRef.Redraw: return f.Bench.Hud.RedrawRect;
                case ViewRef.JournalCell: return f.JournalView.CellRect(a.cellId);
                case ViewRef.JournalRecipes: return f.JournalView.RecipesRect;
                case ViewRef.JournalIndex: return f.JournalView.IndexRect;
                case ViewRef.JournalHints: return f.JournalView.HintsRect;
                case ViewRef.EventHub: return hub >= 0 ? TileRect(hub) : (Rect?)null;
            }
            return null;
        }

        /// <summary>The step's areas with their art-pixel rects right now (margins added, before the hole snap). An area that cannot resolve (a catalyst slot past the lay, a closed view) is left out.</summary>
        List<(TutorialArea area, Rect rect)> Resolve(TutorialStep step, int hub)
        {
            var list = new List<(TutorialArea, Rect)>();
            foreach (var a in step.Areas())
            {
                Rect? r = null;
                switch (a.kind)
                {
                    case AreaKind.Fixed: r = a.FixedArtRect(); break;
                    case AreaKind.ScriptTile:
                        var t = TrialOf(a.trial);
                        if (t == null) break;
                        if (a.slot < 0) r = UnionTiles(t.Lay.Select(l => l.Tile));
                        else if (a.slot < t.Lay.Length) r = TileRect(t.Lay[a.slot].Tile);
                        break;
                    case AreaKind.View: r = ViewRect(a, hub); break;
                }
                if (r.HasValue) list.Add((a, TutorialRect.Grow(r.Value, a.margin)));
            }
            return list;
        }

        /// <summary>The art-pixel rects a step lights right now, before the hole snap. For the tests and the Scene-view preview.</summary>
        public Rect[] Holes(TutorialStep step, int hub = -1) => Resolve(step, hub).Select(x => x.rect).ToArray();

        // ---- hooks called by GameFlow during the Ignite -------------------------------------------

        /// <summary>After a reaction (or fizzle) has played: the held popups of the script.</summary>
        public IEnumerator AfterEvent(ReactionEvent e, int index)
        {
            if (f == null) yield break;
            var parts = ScoreParts.Of(f.Db, e);
            if (parts == null)
            {
                if (trial == 1 && !e.Sputter) yield return Say("2.3");
                yield break;
            }
            Shown.Add(parts.Total);
            switch (trial)
            {
                case 0:
                    yield return SayText("1.6", PartLines(parts), e.Hub);
                    yield return Say("1.6b", e.Hub);
                    break;
                case 2:
                    yield return SayText("3.3e", PartLines(parts), e.Hub, Loc.T("tutorial.step", ("n", e.Chain)));
                    break;
                case 3:
                    yield return SayText("5.4r", PartLines(parts), e.Hub);
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
                    yield return Say("1.7");
                    break;
                case 2:
                    yield return Say("3.4");
                    var chain = o.Events.Where(e => e.Result != null).Select(e => ScoreParts.Of(f.Db, e)).Where(p => p != null).ToList();
                    var both = chain.Select(p => $"{p.Base * p.Used} x{p.First} x{p.Step} = {HudView.Number(p.Total)}").ToArray();
                    yield return SayText("3.5", both);
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
            yield return Say("5.1");
            yield return Say("5.2");
            var offer = Step("5.3");
            Show(offer, Loc.T(offer.textKey), false);
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
            // trial 1: lay, Ignite, reaction
            yield return WaitBench(0);
            yield return Say("1.1");
            yield return Await("1.2", () => Laid(TutorialScript.Reaction, 1));
            yield return Await("1.3", () => Laid(TutorialScript.Reaction, 2));
            yield return Say("1.4");
            yield return IgniteStep("1.5");
            // trial 2: a miss, and the Journal
            yield return WaitBench(1);
            yield return Await("2.1", () => Laid(TutorialScript.Miss, 2));
            yield return IgniteStep("2.2");
            yield return WaitBench(2);
            yield return Await("2.4", () => f.State == FlowState.Journal);
            OpenJournalPage("2.5");
            yield return Say("2.5");
            yield return Say("2.6");
            OpenJournalPage("2.7");
            var last = Step("2.7");
            Show(last, Loc.T(last.textKey), false);
            while (f.State == FlowState.Journal) yield return null;
            Hide();
            // trial 3: chains
            yield return Say("3.0");
            yield return Await("3.1", () => Laid(TutorialScript.Chain, 3));
            yield return Say("3.2");
            brokenTotal = f.Run.Preview(TutorialScript.BrokenChain()).Sum(e => e.Potency);     // worked out before the Ignite, so nothing is spent
            yield return IgniteStep("3.3");
            // the clear, the descent and the catalyst choice are the game's; then the catalyst trial
            while (!(f.State == FlowState.Bench && trial == 3 && f.Bench.InputEnabled)) yield return null;
            yield return null;
            var ct = catalystTrial;
            if (ct != null)
            {
                yield return Await("5.4", () => Laid(ct, ct.Lay.Length));
                yield return IgniteStep("5.4i");
                while (f.State != FlowState.Bench) yield return null;
            }
            yield return Say("5.5");
            Finished = true;
            TutorialRules.MarkDone();
        }

        /// <summary>Opens the Journal page of the first JournalCell area of a step, so the area has a cell to light.</summary>
        void OpenJournalPage(string id)
        {
            foreach (var a in Step(id).Areas())
                if (a.kind == AreaKind.View && a.view == ViewRef.JournalCell) { f.JournalView.ShowMaterial(a.cellId); return; }
        }

        /// <summary>The stage the AfterIgnite hook needs from the game: what the Ignite made.</summary>
        public sealed class IgniteOutcomeView { public List<ReactionEvent> Events; }
    }
}
