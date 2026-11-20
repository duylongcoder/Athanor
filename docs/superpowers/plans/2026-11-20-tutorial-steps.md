# Tutorial Steps as Scene Objects Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Move each tutorial step's highlight areas, click pass-through flags, dim/lock options and text into scene objects, so the owner can add, move, resize and toggle them in Edit Mode without C# edits.

**Architecture:** Under `Tutorial` in Main.unity, one child object per step carries `TutorialStep`. Each area is a child carrying `TutorialArea`, which resolves to an art-pixel rect at runtime (`Fixed` own rect, `ScriptTile` a scripted tile, `View` a named game view), plus a margin. `TutorialController.Drive` keeps the waiting logic and calls steps by id. `TutorialTargets` is removed after a one-time migration that reproduces today's rects.

**Tech Stack:** Unity 6000.6.7f1, C#, NUnit EditMode tests (`unity test Athanor --mode EditMode`), `Px` for world/art conversion (1 unit = 1 art pixel, origin at screen centre, y up).

**Units:** all authoring values are world units. `Px.At` and `Px.Fit` already do the conversion; no new unit system.

---

## Decisions this plan takes from the code (spec addenda)

These are not in the spec yet. Task 0 writes them into `docs/superpowers/specs/2026-11-20-tutorial-steps-design.md`.

1. **Per-step `dark` and `lockPad` fields.** Today `Show` takes `dark` and `free` (`free` = lockPad off). Used by 2.7 (no dim, no lock) and 5.3 (no dim, lock on for the offer). They become `TutorialStep.dark` (default true) and `TutorialStep.lockPad` (default true).
2. **Dynamic areas are view references, not fixed rects.** `ViewRef.EventHub` is the hub tile of the event that just played (1.6, 1.6b, 3.5, 5.4 result). `ViewRef.JournalCell` takes a material id (2.5, 2.6, 2.7). `ScriptTile` with `slot >= lay length` is skipped (the catalyst trial 5.4 has a variable count).
3. **Ignite stage of 5.4** gets its own step id `5.4i` with the same text key `tutorial.5_4`, because today the Lay and the Ignite share a key.
4. **Click on a read step is a per-step setting `clickAdvances`** (default on, which is today's behaviour). On: a click in a non-pass area advances the text. Off: that click only pulses the rim, like an await step. Clicks on the dark still advance a read step in both cases. Pass-through areas always reach the game.
6. **Catalyst slots are authored, not fixed.** Step 5.4 has one ScriptTile area per slot (0 to 4). The owner adds or removes slots on that step; a slot past the current catalyst's tile count is hidden, so the same step serves every catalyst.
7. **Play-mode keep is out of scope** (owner decision, 2026-11-20). Play-mode drags are discarded on Stop; edits are made in Edit Mode.
5. **Tile geometry moves.** `TutorialTargets.tileSize`, `tileShift` and `redrawMargin` move to `TutorialStyle` (they are one geometry for every tile, not per step).

---

## File Structure

| File | Status | Responsibility |
|---|---|---|
| `Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs` | Create | `AreaKind`, `ViewRef`, `ScriptTrial` enums; `TutorialArea` MonoBehaviour; pure static `TutorialRect` math (`ArtRect`, `Grow`) |
| `Athanor/Assets/Scripts/Game/Cutscene/TutorialStep.cs` | Create | `TutorialStep` MonoBehaviour: `textKey`, `dark`, `lockPad`, `boxPosOverride`; `Areas()` list |
| `Athanor/Assets/Scripts/Game/Cutscene/TutorialSettings.cs` | Modify | Remove `TutorialTargets` (line 12-35); add `tileSize`, `tileShift`, `redrawMargin` to `TutorialStyle` |
| `Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs` | Modify | Step lookup by id; `Show(TutorialStep, ...)`; resolve areas; lock-pad holes from `clicksPass` areas only; `Drive` uses ids; `Lit` recorder for tests |
| `Athanor/Assets/Editor/TutorialStepsMigration.cs` | Create | One-time "Athanor/Tutorial/Create Step Objects" menu; writes step children into Main.unity from the table in Task 4 |
| `Athanor/Assets/Editor/TutorialStepGizmo.cs` | Create | Scene-view preview for `TutorialStep` / `TutorialArea` (Edit Mode) |
| `Athanor/Assets/Tests/EditModeGame/TutorialAreaTests.cs` | Create | Pure rect math |
| `Athanor/Assets/Tests/EditModeGame/TutorialStepsBakeTests.cs` | Create | Every step id in `Drive` exists; lit rects equal the baseline |
| `Athanor/Assets/Tests/EditModeGame/SceneBakeTests.cs` | Modify | Line 21 test (`TutorialTargets_RedrawAndJournalSteps_MatchTheViews`) moves to the new step test; line 23 comment updated |
| `Athanor/Assets/Scenes/Main.unity` | Modify (by Editor tool only) | Step and area objects |
| `docs/superpowers/specs/2026-11-20-tutorial-steps-design.md` | Modify | Addenda from this plan |
| `docs/ATHANOR.md` | Modify | Decision in *Decisions in force* |
| `vault/` | Regenerate | `python3 tools/vault/build_vault.py` |

Tests that need Unity: close the Editor first (`unity test` fails while the project is open in a running Editor, per the 2026-11-20 session notes). Each task ends with a commit message; `Co-Authored-By` line per the session attribution rule.

---

### Task 0: Baseline and spec addenda

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs` (add recorder)
- Modify: `docs/superpowers/specs/2026-11-20-tutorial-steps-design.md`
- Test: `Athanor/Assets/Tests/EditModeGame/TutorialStepsBakeTests.cs`

- [ ] **Step 1: Add the recorder to `Show`.** `Show` gets a step id (today's text key, e.g. `tutorial.1_4`) as its first argument in Task 2. For the baseline we record the existing call only. Add to `TutorialController`:

```csharp
/// <summary>Test recorder: every shown step with the art rects it lit, in order. Filled by Show.</summary>
public readonly List<(string key, Rect[] lit)> Lit = new List<(string key, Rect[] lit)>();
```

Add `Lit.Add((text, (Rect[])holeRects.Clone()));` at the top of `Show` (after `Clear();`). `text` is the localised string, so the baseline keys are the English texts; this is enough to compare rects per step in order.

- [ ] **Step 2: Write the baseline test.** It drives the full tutorial in the Main scene with a fake bench (existing `MainSceneFixture`) and stores the `Lit` sequence to `Athanor/Assets/Tests/EditModeGame/Baselines/tutorial_lit_baseline.json` (generated once, committed).

- [ ] **Step 3: Run it to capture the baseline.** Run: `unity test Athanor --mode EditMode --filter TutorialStepsBakeTests` (Editor closed). Expected: PASS and the JSON file exists. Open the file and check it has one entry per shown step (about 30 entries).

- [ ] **Step 4: Write the spec addenda.** Add the five decisions listed above to `docs/superpowers/specs/2026-11-20-tutorial-steps-design.md` under a new section `## Addenda (plan review)`.

- [ ] **Step 5: Commit.**

```bash
git add Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs Athanor/Assets/Tests/EditModeGame/TutorialStepsBakeTests.cs Athanor/Assets/Tests/EditModeGame/Baselines/tutorial_lit_baseline.json docs/superpowers/specs/2026-11-20-tutorial-steps-design.md
git commit -m "test(tutorial): baseline of lit rects per step before the move to scene steps"
```

---

### Task 1: Pure rect math (TDD)

**Files:**
- Create: `Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs`
- Test: `Athanor/Assets/Tests/EditModeGame/TutorialAreaTests.cs`

- [ ] **Step 1: Write the failing test**

```csharp
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    public class TutorialAreaTests
    {
        [Test]
        public void Grow_AddsEachSideSeparately()
        {
            var r = TutorialRect.Grow(new Rect(10, 20, 30, 40), new Vector4(1, 2, 3, 4));   // left, top, right, bottom
            Assert.AreEqual(new Rect(9, 18, 34, 46), r);
        }

        [Test]
        public void ArtRect_OfWorldCentre_IsTheCentredArtRect()
        {
            // world (0,0) is the screen centre: art (320,180); a 20x10 area centred there has its top-left at (310,175)
            var r = TutorialRect.ArtRect(Vector2.zero, new Vector2(20, 10));
            Assert.AreEqual(new Rect(310, 175, 20, 10), r);
        }

        [Test]
        public void ArtRect_MatchesPxAt()
        {
            var world = Px.At(100, 50);   // art point (100,50) in world space
            var r = TutorialRect.ArtRect(new Vector2(world.x, world.y), new Vector2(8, 8));
            Assert.AreEqual(96f, r.x, 0.001f); Assert.AreEqual(46f, r.y, 0.001f);
        }
    }
}
```

- [ ] **Step 2: Run it to verify it fails.** Run: `unity test Athanor --mode EditMode --filter TutorialAreaTests`. Expected: FAIL, `TutorialRect` not defined.

- [ ] **Step 3: Write the minimal implementation**

```csharp
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Pure rectangle math for tutorial areas (art pixels, top-left origin). No scene needed, so it is tested alone.</summary>
    public static class TutorialRect
    {
        /// <summary>Grows r by margin = (left, top, right, bottom) pixels.</summary>
        public static Rect Grow(Rect r, Vector4 m) =>
            new Rect(r.x - m.x, r.y - m.y, r.width + m.x + m.z, r.height + m.y + m.w);

        /// <summary>The art-pixel rect (top-left, size) of an area whose world centre is given and whose size is in pixels.</summary>
        public static Rect ArtRect(Vector2 worldCentre, Vector2 size)
        {
            float cx = worldCentre.x + Px.W / 2f, cy = Px.H / 2f - worldCentre.y;
            return new Rect(cx - size.x / 2f, cy - size.y / 2f, size.x, size.y);
        }
    }

    /// <summary>What a tutorial area lights.</summary>
    public enum AreaKind { Fixed, ScriptTile, View }

    /// <summary>A named game view an area can follow (the rect comes from the view at run time).</summary>
    public enum ViewRef { Bench, Hand, Ignite, Rack, Redraw, Journal, JournalCell, JournalRecipes, JournalIndex, JournalHints, Offer, EventHub }

    /// <summary>Which scripted hand's tiles a ScriptTile area follows.</summary>
    public enum ScriptTrial { Reaction, Miss, Chain, Catalyst }
}
```

- [ ] **Step 4: Run the tests to verify they pass.** Run: `unity test Athanor --mode EditMode --filter TutorialAreaTests`. Expected: PASS, 3 tests.

- [ ] **Step 5: Commit.**

```bash
git add Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs Athanor/Assets/Tests/EditModeGame/TutorialAreaTests.cs
git commit -m "feat(tutorial): pure rect math and area enums for scene-authored steps"
```

---

### Task 2: TutorialArea and TutorialStep components

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs` (add the component)
- Create: `Athanor/Assets/Scripts/Game/Cutscene/TutorialStep.cs`
- Test: `Athanor/Assets/Tests/EditModeGame/TutorialAreaTests.cs` (add cases)

- [ ] **Step 1: Write the failing test**

```csharp
[Test]
public void Fixed_Area_ReadsItsTransform()
{
    var go = new GameObject("area");
    try
    {
        go.transform.position = Px.At(100, 50);
        var a = go.AddComponent<TutorialArea>();
        a.kind = AreaKind.Fixed; a.size = new Vector2(8, 8);
        Assert.AreEqual(new Rect(96, 46, 8, 8), a.FixedArtRect());
    }
    finally { Object.DestroyImmediate(go); }
}

[Test]
public void Step_Lists_Its_Areas_In_Child_Order()
{
    var root = new GameObject("step");
    try
    {
        var s = root.AddComponent<TutorialStep>();
        var a = new GameObject("a").transform; a.SetParent(root.transform); a.gameObject.AddComponent<TutorialArea>();
        var b = new GameObject("b").transform; b.SetParent(root.transform); b.gameObject.AddComponent<TutorialArea>();
        CollectionAssert.AreEqual(new[] { a.GetComponent<TutorialArea>(), b.GetComponent<TutorialArea>() }, s.Areas());
    }
    finally { Object.DestroyImmediate(root); }
}
```

- [ ] **Step 2: Run to verify they fail.** Expected: FAIL, `TutorialArea` and `TutorialStep` not defined.

- [ ] **Step 3: Implement `TutorialArea`** (append to `TutorialArea.cs`)

```csharp
/// <summary>
/// One lit window of a step. Fixed: the object's world position is the centre and <see cref="size"/> (pixels) its size.
/// ScriptTile and View follow the game at run time. <see cref="clicksPass"/> lets clicks in the window reach the game.
/// </summary>
public sealed class TutorialArea : MonoBehaviour
{
    public AreaKind kind = AreaKind.Fixed;
    [Tooltip("Fixed: size in pixels (= world units).")] public Vector2 size = new Vector2(16, 16);
    [Tooltip("ScriptTile: the scripted hand.")] public ScriptTrial trial = ScriptTrial.Reaction;
    [Tooltip("ScriptTile: index in the hand's Lay. -1 = the union of all of the hand's tiles.")] public int slot;
    [Tooltip("View: the view to follow.")] public ViewRef view = ViewRef.Bench;
    [Tooltip("View JournalCell: the material id shown in the Journal.")] public string cellId = "";
    [Tooltip("Pixels added to the area on left, top, right, bottom.")] public Vector4 margin;
    [Tooltip("Clicks in this area reach the game (the bench under it). Off = the area is highlight only.")] public bool clicksPass;

    /// <summary>Fixed areas only: the art-pixel rect from the transform and size.</summary>
    public Rect FixedArtRect() => TutorialRect.ArtRect(transform.position, size);
}
```

- [ ] **Step 4: Implement `TutorialStep`** (`TutorialStep.cs`)

```csharp
using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// One tutorial step, a child of the Tutorial object. Holds the text key, the dim and lock options, and its areas (child objects).
    /// The step id is the object's name (e.g. "1.4", "5.4i").
    /// </summary>
    public sealed class TutorialStep : MonoBehaviour
    {
        [Tooltip("Lang key of the text, e.g. tutorial.1_4. Two steps may share a key (5.4 and 5.4i).")]
        public string textKey = "";
        [Tooltip("Dim the screen outside the areas.")] public bool dark = true;
        [Tooltip("The lock pad is on: clicks on the dark pulse the rim, and clicks in pass-through areas reach the game.")] public bool lockPad = true;
        [Tooltip("Read steps only: a click in a non-pass area advances the text. Off: it only pulses the rim.")] public bool clickAdvances = true;
        [Tooltip("Optional text box position for this step (art pixels, top-left). Off = TutorialStyle.boxPos.")] public bool useBoxPos;
        public Vector2 boxPos;

        /// <summary>The areas in child order; the arrow sits over the last one.</summary>
        public List<TutorialArea> Areas()
        {
            var list = new List<TutorialArea>();
            foreach (Transform c in transform)
            {
                var a = c.GetComponent<TutorialArea>();
                if (a != null && a.isActiveAndEnabled) list.Add(a);
            }
            return list;
        }
    }
}
```

- [ ] **Step 5: Run the tests.** Expected: PASS.

- [ ] **Step 6: Commit.**

```bash
git add Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs Athanor/Assets/Scripts/Game/Cutscene/TutorialStep.cs Athanor/Assets/Tests/EditModeGame/TutorialAreaTests.cs
git commit -m "feat(tutorial): TutorialStep and TutorialArea components"
```

---

### Task 3: Resolve areas to art rects in TutorialController

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Cutscene/TutorialSettings.cs` (move tile geometry into `TutorialStyle`)
- Modify: `Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs`

- [ ] **Step 1: Move the geometry.** In `TutorialSettings.cs`, add to `TutorialStyle`:

```csharp
[Header("Bench tile window")]
[Tooltip("The lit window round one tile.")] public Vector2 tileSize;
[Tooltip("From the tile's centre to the window's top-left corner.")] public Vector2 tileShift;
[Tooltip("Pixels added round the Redraw button's own rectangle (HudView).")] public float redrawMargin;
```

Then delete `TutorialTargets` (lines 9-35) and the field `targets` on `TutorialController` (line 36 in the current file). Keep `hand`, `ignite`, `journal`, `rack`, `offer` values for Task 4 migration: write them down first, from the scene's `TutorialTargets` in Main.unity, into the Task 4 table.

- [ ] **Step 2: Add the resolver.** In `TutorialController`, replace the rect properties (`TileRect`, `HandRect`, `IgniteRect`, `JournalRect`, `RackRect`, `RedrawRect`) with:

```csharp
/// <summary>The art-pixel rect of one bench tile (same maths as before: the tile's centre, shifted by TutorialStyle.tileShift).</summary>
Rect TileRect(int i)
{
    var c = f.Bench.TileCentre(i);
    float left = c.x + Px.W / 2f - style.tileShift.x, top = Px.H / 2f - c.y - style.tileShift.y;
    return new Rect(Mathf.Round(left), Mathf.Round(top), style.tileSize.x, style.tileSize.y);
}

/// <summary>The rect a view reference stands for right now; null when the view is not on screen.</summary>
Rect? ViewRect(ViewRef v, string cellId, int hub)
{
    switch (v)
    {
        case ViewRef.Bench: return UnionTiles(Enumerable.Range(0, Core.Bench.TileCount).ToArray());
        case ViewRef.Hand: { var r = style.hand; return new Rect(r.x, r.y, r.width, Mathf.Min(r.height, Px.H - r.y)); }
        case ViewRef.Ignite: return style.ignite;
        case ViewRef.Rack: return style.rack;
        case ViewRef.Redraw: { var r = f.Bench.Hud.RedrawRect; float m = style.redrawMargin; return new Rect(r.x - m, r.y - m, r.width + 2 * m, r.height + 2 * m); }
        case ViewRef.Journal: return style.journal;
        case ViewRef.JournalCell: return f.JournalView.CellRect(cellId);
        case ViewRef.JournalRecipes: return f.JournalView.RecipesRect;
        case ViewRef.JournalIndex: return f.JournalView.IndexRect;
        case ViewRef.JournalHints: return f.JournalView.HintsRect;
        case ViewRef.Offer: return style.offer;
        case ViewRef.EventHub: return TileRect(hub);
    }
    return null;
}

Rect UnionTiles(int[] tiles) => Union(tiles.Select(TileRect).ToArray());
```

Also add `JournalView.CellRect(string id)` that returns the rect of the cell showing `id` (the existing `ShowMaterial(id)` return value). This is the only new method on a view; implement it next to `ShowMaterial` in the Journal view, using the same cell rect that `ShowMaterial` already returns.

- [ ] **Step 3: Resolve a step's areas.**

```csharp
/// <summary>The art-pixel holes of a step right now. Areas that cannot resolve (a hidden catalyst slot, a closed view) are skipped.</summary>
Rect[] Holes(TutorialStep step, int hub = -1)
{
    var list = new List<Rect>();
    foreach (var a in step.Areas())
    {
        Rect? r = null;
        switch (a.kind)
        {
            case AreaKind.Fixed: r = a.FixedArtRect(); break;
            case AreaKind.ScriptTile:
                var t = TrialOf(a.trial);
                if (t == null) break;
                if (a.slot < 0) { r = UnionTiles(t.Lay.Select(l => l.Tile).ToArray()); break; }
                if (a.slot < t.Lay.Length) r = TileRect(t.Lay[a.slot].Tile);
                break;
            case AreaKind.View: r = ViewRect(a.view, a.cellId, hub); break;
        }
        if (r.HasValue) list.Add(TutorialRect.Grow(r.Value, a.margin));
    }
    return list.ToArray();
}

TutorialTrial TrialOf(ScriptTrial s) => s switch
{
    ScriptTrial.Reaction => TutorialScript.Reaction,
    ScriptTrial.Miss => TutorialScript.Miss,
    ScriptTrial.Chain => TutorialScript.Chain,
    ScriptTrial.Catalyst => catalystTrial,
    _ => null,
};
```

Note: `ScriptTrial.Catalyst` uses `catalystTrial` (already a field). Slots past its `Lay` length are skipped by the `a.slot < t.Lay.Length` check.

- [ ] **Step 4: Compile.** Run: `unity recompile` (Editor open is fine for this). Expected: no errors. Fix any reference to the removed `targets` field in `Drive` now; Task 5 rewrites `Drive` fully.

- [ ] **Step 5: Commit.**

```bash
git add Athanor/Assets/Scripts/Game/Cutscene/TutorialSettings.cs Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs Athanor/Assets/Scripts/Game/Cutscene/TutorialStep.cs
git commit -m "refactor(tutorial): resolve scene areas to art rects; tile geometry moves to TutorialStyle"
```

---

### Task 4: Show by step, and migration tool

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs` (`Show`, `Clear`, `Lit` recorder)
- Create: `Athanor/Assets/Editor/TutorialStepsMigration.cs`
- Test: `Athanor/Assets/Tests/EditModeGame/TutorialStepsBakeTests.cs`

- [ ] **Step 1: Change `Show` to take a step.** New signature:

```csharp
void Show(TutorialStep step, string text, bool advanceOnRead, string[] lines = null, int hub = -1)
{
    Clear(); advance = false; advanceOnClick = advanceOnRead; shown = true; armed = Time.unscaledTime + style.clickGuard;
    root.gameObject.SetActive(true);
    line.Text = text;
    var areas = step.Areas();
    holeRects = Holes(step, hub);
    for (int i = 0; i < holeRects.Length; i++) holeRects[i] = Snap(holeRects[i]);
    var u = Union(holeRects);
    bool full = u.width >= Px.W - 1 && u.height >= Px.H - 1;
    rimOn = step.dark && !full;
    dim.color = dim.color.Alpha(step.dark ? dimAlpha : 0f);
    lockPad.gameObject.SetActive(step.lockPad);
    lockPad.holes.Clear();
    for (int i = 0; i < holeRects.Length; i++)
    {
        holes[i].Open(holeRects[i], rimOn);   // (pool growth as today)
        if (areas[i].clicksPass)              // only pass-through areas are cut from the lock pad
        {
            var corner = Px.At(holeRects[i].x, holeRects[i].yMax);
            lockPad.holes.Add(new Rect(corner.x, corner.y, holeRects[i].width, holeRects[i].height));
        }
    }
    // rest of Show unchanged: readPad, Dev.Hook, popup lines, box placement (use step.useBoxPos ? step.boxPos : style.boxPos)
}
```

Caveats for the implementer: `Holes` skips unresolved areas, so `areas[i]` must be matched to `holeRects[i]` by building both in the same loop. Change `Holes` to return `(Area, Rect)[]` in this task, so the index matches. Keep the existing pool growth loop (`while (holes.Count < holeRects.Length) holes.Add(...)`) and the `Snap` on grid (`style.holeSnap`), which is the code currently in `Show`.

Also in `Show`: `lines`, `Dev.Hook` and the `Lit.Add` recorder (Task 0) use `step.textKey`.

Click routing (replaces `readPad`): add a `holePad` Clickable over the lit holes, above `lockPad`, with `holes` = the pass-through rects (so clicks there reach the bench). Its `onClick`: if `advanceOnClick && step.clickAdvances` set `advance = true`; otherwise `bump = 1` (rim pulse only). `lockPad` keeps its current `onClick` (pulse, and advance on read steps). Set `holePad.gameObject.SetActive(holeRects.Length > 0)`. Adding `holePad` to the tutorial's scene object is part of this task (a new child of `Tutorial`, same layer and order rules as `readPad`).

- [ ] **Step 2: Write the step-table test** (before migration; it fails until the scene has the steps).

```csharp
[Test]
public void EveryStepId_UsedByDrive_ExistsInTheScene()
{
    var main = new MainSceneFixture();
    try
    {
        var t = main.FlowField<TutorialController>("tutorial");
        var steps = t.GetComponentsInChildren<TutorialStep>(true).Select(s => s.name).ToHashSet();
        foreach (var id in TutorialStepIds.All) Assert.IsTrue(steps.Contains(id), "missing step " + id);
    }
    finally { main.Dispose(); }
}
```

Also add `TutorialStepIds.All` (a `static class TutorialStepIds` with a `string[] All` listing every id from the Task 4 table below); the test enforces parity with the scene.

- [ ] **Step 3: Migration table and tool.** The tool (`Editor/TutorialStepsMigration.cs`, menu `Athanor/Tutorial/Create Step Objects`) creates children under `Tutorial` in Main.unity from this table. Values in `ViewRef` / `ScriptTrial` / `clicksPass` reproduce the current lit rects and pass-through behaviour (`Await` and `Lay` steps pass clicks in holes; `Say` steps advance on click in holes; the offer passes).

| Step id | Text key | dark | lockPad | Areas (kind, params, clicksPass) |
|---|---|---|---|---|
| 1.1 | tutorial.1_1 | on | on | View Bench, pass off |
| 1.2 | tutorial.1_2 | on | on | View Hand; ScriptTile Reaction slot 0. Both pass on |
| 1.3 | tutorial.1_3 | on | on | View Hand; ScriptTile Reaction 0, 1. Pass on |
| 1.4 | tutorial.1_4 | on | on | View Hand; ScriptTile Reaction 0, 1. Pass off |
| 1.5 | tutorial.1_5 | on | on | View Ignite, pass on |
| 1.6 | tutorial.1_6 | on | on | View EventHub, pass off |
| 1.6b | tutorial.1_6b | on | on | View EventHub, pass off |
| 1.7 | tutorial.1_7 | on | on | View Rack, pass off |
| 2.1 | tutorial.2_1 | on | on | View Hand; ScriptTile Miss 0, 1. Pass on |
| 2.2 | tutorial.2_2 | on | on | View Ignite, pass on |
| 2.3 | tutorial.2_3 | on | on | none (full dim, no rim) |
| 2.4 | tutorial.2_4 | on | on | View Journal, pass on |
| 2.5 | tutorial.2_5 | on | on | View JournalCell ember_coal; View JournalRecipes. Pass off |
| 2.6 | tutorial.2_6 | on | on | View JournalCell ember_coal; View JournalIndex. Pass off |
| 2.7 | tutorial.2_7 | off | off | View JournalCell glowmoss; View JournalHints. Pass off |
| 3.0 | tutorial.3_0 | on | on | View Redraw. Pass off |
| 3.1 | tutorial.3_1 | on | on | View Hand; ScriptTile Chain 0, 1, 2. Pass on |
| 3.2 | tutorial.3_2 | on | on | ScriptTile Chain slot 1 (hub). Pass off |
| 3.3 | tutorial.3_3 | on | on | View Ignite, pass on |
| 3.4 | tutorial.3_4 | on | on | none |
| 3.5 | tutorial.3_5 | on | on | View EventHub (hub of the chain's event). Pass off |
| 3.6 | tutorial.3_6 | on | on | ScriptTile Chain slot -1 (union of the chain). Pass off |
| 5.1 | tutorial.5_1 | on | on | none |
| 5.2 | tutorial.5_2 | on | on | none |
| 5.3 | tutorial.5_3 | off | on | View Offer, pass on |
| 5.4 | tutorial.5_4 | on | on | View Hand; ScriptTile Catalyst 0..4 (hidden past its length). Pass on |
| 5.4i | tutorial.5_4 | on | on | View Ignite, pass on |
| 5.5 | tutorial.5_5 | on | on | none |

Note on the hub and dynamic rows: `View EventHub` and `ScriptTile Catalyst` are resolved at run time, so the table only fixes the kind. `Holes(step, hub)` takes the hub from the event being shown.

The tool must be idempotent: running it twice updates the children by name rather than adding duplicates. Its unit test lives in `TutorialStepsBakeTests`.

- [ ] **Step 4: Run the migration, then the step test.** Run: `unity test Athanor --mode EditMode --filter TutorialStepsBakeTests` (Editor closed). Expected: PASS.

- [ ] **Step 5: Lit-rect parity test.** Compare the `Lit` sequence of a full tutorial run to `tutorial_lit_baseline.json` from Task 0, rect by rect, with a tolerance of 0.5 px. Expected: PASS. If a step differs, the table row is wrong; fix the table and re-run the migration.

- [ ] **Step 6: Commit.**

```bash
git add Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs Athanor/Assets/Editor/TutorialStepsMigration.cs Athanor/Assets/Tests/EditModeGame/TutorialStepsBakeTests.cs Athanor/Assets/Scenes/Main.unity
git commit -m "feat(tutorial): steps and areas in Main.unity, migrated with lit-rect parity"
```

---

### Task 5: Drive by step id; remove TutorialTargets

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs` (`Say`, `SayText`, `Await`, `Lay`, `IgniteStep`, `Broken`, `AfterEvent`, `AfterIgnite`, `OnOffer`, `Drive`)
- Modify: `Athanor/Assets/Tests/EditModeGame/SceneBakeTests.cs` (lines 21-40 test and comment)

- [ ] **Step 1: Look up a step by id.**

```csharp
TutorialStep Step(string id)
{
    var s = GetComponentsInChildren<TutorialStep>(true).FirstOrDefault(x => x.name == id);
    if (s == null) throw new System.InvalidOperationException("No tutorial step " + id);
    return s;
}
```

- [ ] **Step 2: Rewrite the helpers to take an id.** For each, the pattern is the same: `Say(id)`, `SayText(id, lines, hub)`, `Await(id, done)`, `Lay(id, n)`, `IgniteStep(id)`. Example:

```csharp
IEnumerator Say(string id, int hub = -1) => SayText(id, null, hub);

IEnumerator SayText(string id, string[] lines, int hub = -1)
{
    var step = Step(id);
    Show(step, Loc.T(step.textKey), true, lines, hub);
    yield return null;
    while (!advance) yield return null;
    Hide();
}

IEnumerator Await(string id, Func<bool> done, int hub = -1)
{
    var step = Step(id);
    Show(step, Loc.T(step.textKey), false, null, hub);
    while (!done()) yield return null;
    Hide();
}
```

`Lay(id, t, n)` becomes `Await(id, () => Laid(t, n))`. The `n` only controls `Laid`; areas are chosen by the step. `IgniteStep(id)` becomes `Await(id, () => f.State == FlowState.Resolving)` followed by the existing wait for `Resolving` to end.

`Broken()` uses `Show(Step("3.6"), ...)` with its ghost sprites, unchanged.

- [ ] **Step 3: Update the call sites in `Drive`, `AfterEvent`, `AfterIgnite`, `OnOffer`.** Ids replace the keys. Examples:

```csharp
yield return Say("1.1");
yield return Lay("1.2", TutorialScript.Reaction, 1);
yield return Say("2.5");                    // was: Say("tutorial.2_5", cell, jv.RecipesRect)
yield return IgniteStep("5.4i");            // was: IgniteStep("tutorial.5_4")
yield return SayText("1.6", PartLines(parts), e.Hub);
```

Remove `boxDy` and `journalBoxDrop`: the Journal box drop becomes a per-step `boxPosOverride` on 2.5 and 2.6 in the scene (Task 4 table: set `useBoxPos` and `boxPos` on those two steps, with `style.journalBoxDrop` value). Set it in the migration tool so the baseline holds.

Delete `free` from every call; `lockPad` and `dark` now come from the step.

- [ ] **Step 4: Update `SceneBakeTests`.** Replace the test at line 21 (`TutorialTargets_RedrawAndJournalSteps_MatchTheViews`) with a check that the step `2.5` contains a `View JournalRecipes` area, and that `Holes` for the `Redraw` step is the button rect grown by `style.redrawMargin`. Update the comment at line 23 (remove `TutorialTargets margin (2)`; use `TutorialStyle.redrawMargin`).

- [ ] **Step 5: Run the full EditMode suite.** Run: `unity test Athanor --mode EditMode` (Editor closed). Expected: PASS, including the Task 4 parity test.

- [ ] **Step 6: Commit.**

```bash
git add Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs Athanor/Assets/Tests/EditModeGame/SceneBakeTests.cs Athanor/Assets/Editor/TutorialStepsMigration.cs Athanor/Assets/Scenes/Main.unity
git commit -m "refactor(tutorial): Drive calls steps by id; TutorialTargets removed"
```

---

### Task 6: Edit Mode preview (gizmo)

**Files:**
- Create: `Athanor/Assets/Editor/TutorialStepGizmo.cs`

- [ ] **Step 1: Implement the gizmo.** It draws, for the selected step's children: a rectangle outline per area (green when `clicksPass`, amber otherwise), a `Handles.Label` with the step id and the first 40 characters of the English text, and a `Handles.PositionHandle` on each Fixed area so the owner moves it in Scene view. Sizes are edited in the Inspector, in pixels.

```csharp
using Athanor.Game;
using UnityEditor;
using UnityEngine;

public static class TutorialStepGizmo
{
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    static void Draw(TutorialArea a, GizmoType type)
    {
        Gizmos.color = a.clicksPass ? Color.green : new Color(1f, 0.7f, 0.2f);
        var c = a.transform.position;
        Gizmos.DrawWireCube(c, new Vector3(a.size.x, a.size.y, 0));
    }
}
```

Fixed areas also get a position handle in `OnSceneGUI` (a custom `[CustomEditor(typeof(TutorialArea))]` with `Handles.PositionHandle`, and an `Undo.RecordObject` before each change).

- [ ] **Step 2: Verify in Scene view.** Open Main.unity, select `Tutorial/Step 1.4`, check the areas are drawn and the handle moves the area. Retain a screenshot to `production/qa/evidence/tutorial-gizmo-2026-11-20.png` (the rule for UI changes).

- [ ] **Step 3: Commit.**

```bash
git add Athanor/Assets/Editor/TutorialStepGizmo.cs production/qa/evidence/tutorial-gizmo-2026-11-20.png
git commit -m "feat(tutorial): Scene-view preview and handles for step areas"
```

---

### Task 7: Evidence, vault, decision, CI

**Files:**
- Create: `production/qa/evidence/tutorial-steps-*.png` (one per step touched: 1.1 to 5.5, about 30 files, or a contact sheet)
- Modify: `docs/ATHANOR.md` (*Decisions in force*)
- Regenerate: `vault/`

- [ ] **Step 1: Screenshots.** For each step, run the tutorial in the WebGL player (`web-release-engineer` skill) or in Play, and retain one screenshot per step. Compare with the baseline (Task 0): the lit areas must match.

- [ ] **Step 2: Decision and vault.** Add to *Decisions in force* in `docs/ATHANOR.md`: "Tutorial highlight areas live in scene objects (`TutorialStep` / `TutorialArea` under `Tutorial` in Main.unity); clicks pass only through areas with `clicksPass`; `TutorialTargets` is removed." Then run: `python3 tools/vault/build_vault.py`.

- [ ] **Step 3: CI.** Run: `tools/ci/run.sh all`. Expected: green. Do not weaken a check.

- [ ] **Step 4: Commit.**

```bash
git add docs/ATHANOR.md vault production/qa/evidence
git commit -m "docs(tutorial): decision and vault for scene-authored steps; evidence per step"
```

---

## Owner decisions (2026-11-20)

- Read-step click: per-step `clickAdvances` (Task 2, Task 4 click routing).
- Catalyst slots: authored per step, hidden when past the catalyst's tile count (Task 3).
- Play-mode keep: dropped. Edits happen in Edit Mode only.

## Self-review

- Spec coverage: scene steps and areas (Tasks 2, 4); clicks pass per area (Tasks 1-4); read-step click per step (Task 2, Task 4); text per step (Task 2, `textKey`); Edit Mode editing (Task 6); migration with parity (Task 4); removal of `TutorialTargets` (Tasks 3, 5); tests, screenshots, vault, CI (Tasks 0, 4, 5, 7).
- Placeholder scan: Task 4's table has every step id used by `Drive`; the hub-dependent and catalyst rows are explicit about run-time resolution.
- Type consistency: `TutorialArea` fields (`kind`, `size`, `trial`, `slot`, `view`, `cellId`, `margin`, `clicksPass`) are used the same way in Tasks 1-5; `Holes` returns rects matched by index to areas, which the Task 4 caveat requires to be done in one loop.
