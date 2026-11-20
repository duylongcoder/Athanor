# Tutorial steps as scene objects — design

Date: 2026-11-20. Status: draft for owner review. Owner: Athanor owner.

## Problem

Tutorial highlight areas are numbers in `TutorialTargets` (art-pixel rects, top-left origin), shared by every step, and each step's list of areas is hard-coded in `TutorialController.Drive`. Consequences:

1. A rect cannot be seen or dragged; tuning means guessing numbers and replaying.
2. Changing a shared rect (`hand`, `ignite`) changes every step that uses it.
3. Adding or removing an area for one step needs a C# edit.
4. A lit area always lets clicks through (`lockPad.holes` mirrors the holes), except the global `free` flag. A step cannot be "highlight only".
5. Play-mode edits are lost on Stop; Dev Mode does not cover the tutorial.

## Goal

Per step, the owner can edit without touching C#:

- the list of highlight areas (position, size, add, remove);
- for each area, whether clicks pass through it (default: blocked);
- the step's instruction text (Lang key) and, optionally, the text box position.

Changes are visible live in Play, and fully workable in Edit Mode without playing.

## Design

### Scene layout

Under the `Tutorial` object of Main.unity, one child per step (`Step 1.4`, `Step 2.5`, …) carrying `TutorialStep`:

| Field | Meaning |
|---|---|
| `textKey` | Lang key, e.g. `tutorial.1_4`. Inspector shows the English text read-only. |
| `boxPosOverride` | Optional top-left of the text box for this step. Off = `TutorialStyle.boxPos`. |
| areas | Child objects, each with `TutorialArea`. Sibling order = hole order; the arrow sits over the last one (as today). |

Each `TutorialArea`:

| Field | Meaning |
|---|---|
| `kind` | `Fixed` (own rect), `ScriptTile` (n-th tile of the current scripted trial, plus margin), `View` (Redraw button, Journal Recipes / Index / Hints rect, plus margin). |
| `rect` | For `Fixed`: the rect, edited with the Rect Tool in Scene view. |
| `margin` | For `ScriptTile` and `View`: pixels added per side (left/top/right/bottom). |
| `clicksPass` | Clicks inside this area reach the game. Default off. Nothing sets it automatically. |

`ScriptTile` and `View` exist because their position depends on the trial or on another view; a fixed rect cannot follow them.

### Runtime

`Drive` keeps all waiting logic (lay, ignite, journal open, popups). It stops passing rects: `Say("1.4")` / `Await("1.4", done)` look up the `TutorialStep` by id and `Show` reads its areas. `Show` still snaps rects to the pixel grid (`holeSnap`) and still drives `TutorialHole` and the lock pad, but the lock pad's cut-outs now come only from areas with `clicksPass` on. The `free` flag and `TutorialTargets` are removed. The step re-reads its areas every frame while shown, so Play-mode edits show at once.

### Authoring

- **Edit Mode:** selecting a step draws a Scene-view preview: dim outside, lit areas with rim, pass-through areas in a different colour, a label with the step id and text.
- **Play Mode:** drag areas in Scene view; the overlay follows live. A Editor menu item `Athanor/Tutorial/Keep Play Edits` records the current step values and applies them to the scene when Play stops (Unity otherwise discards them). Same pattern for later Dev Mode work.

### Migration

A one-time Editor tool creates the step objects from today's behaviour (the rects now in `TutorialTargets` and the lists in `Drive`), and sets `clicksPass` per area to match today's behaviour, so the tutorial plays identically before the owner changes anything. Steps that depend on scripted tiles use `ScriptTile`. After migration `TutorialTargets` is deleted.

### Out of scope

Step order and waiting conditions stay in code. No change to Lang keys, `TutorialScript` (Core), or the rim/arrow/dim look.

## Testing

- EditMode: every step id used by `Drive` exists in the scene and has a non-empty `textKey` with en and vi entries; an area of kind `Fixed` has a positive size; `SceneBakeTests` updated for the removal of `TutorialTargets`.
- A migration test: the lit rects for each step equal those of the old code (captured before deletion).
- Retained screenshots of each tutorial step in `production/qa/evidence/` (project rule for UI changes).
- Vault regenerated, `tools/ci/run.sh all` green, a decision line in `docs/ATHANOR.md` § Decisions in force.

## Units

The whole project lays out in world units. `Px` (Game/Support/Px.cs) fixes 1 world unit = 1 art pixel, origin at the screen centre, y up. So an area is authored directly as a world-space `RectTransform` (Rect Tool works as is); the runtime converts it to art-space rect with `Px` where `TutorialHole` / `Show` need it. No custom unit conversion is needed. Inspector values for `Fixed` areas are world units; the art-space top-left shown as a read-only helper.

## Addenda (plan review)

1. `dark` and `lockPad` are per-step fields (default on). Step 2.7 has both off; step 5.3 has dark off, lock on.
2. Dynamic areas are view references: `EventHub` (hub tile of the event just played), `JournalCell` (a material id). A `ScriptTile` slot past the trial's lay length is skipped (the catalyst trial has a variable count).
3. The ignite stage of 5.4 is its own step `5.4i` with the same text key as 5.4.
4. `clickAdvances` per step (default on): on a read step a click in a non-pass area advances the text; off, it only pulses the rim. Clicks on the dark still advance a read step.
5. `tileSize`, `tileShift` and `redrawMargin` move to `TutorialStyle` (one geometry for every tile).
6. Catalyst slots are authored on step 5.4: one ScriptTile area per slot 0..4; slots past the catalyst's tile count are hidden.
7. Play-mode keep is out of scope (owner, 2026-11-20): edits are made in Edit Mode.
