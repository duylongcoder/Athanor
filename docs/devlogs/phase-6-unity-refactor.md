# Phase 6 — Unity refactor: decisions

Plan: `docs/superpowers/plans/2026-11-18-athanor-phase6-unity-refactor.md`. One entry per decision the plan left open or got wrong.

## Task 14 — Bench, tiles and cards are prefabs (2026-11-18)

- **layout.json moves to the Editor, it is not deleted** (owner's choice, recommended option). The plan said to delete it, but
  `SceneBuilder` and `DepthTestBuilder` (Editor tools, deleted in Task 16) still read its `placed` and `candles` for the
  workshop, and `tools/art/export.mjs` writes it. It is now `Assets/Editor/layout.json`, so the WebGL build carries no layout file
  and the game never loads one. `LayoutData` moved to `Assets/Editor/LayoutData.cs`; `TileData` and `RectData` stay in the Game
  assembly (`Support/TileData.cs`) because `TileView.Build` takes a `TileData`. `export.mjs` was edited to write the new path
  (not run: art drift).
- **The depth groups are not baked into the Bench prefab.** The plan's bake lambda also called `DepthStage.PlaceTiles`. But
  `GameFlow` calls `PlaceTiles` at run time anyway (the plan keeps that line), and `DepthLayer.AddGroup` captures the tile's
  position as its rest position. Baking it too would apply the group twice. The bake only stores the flat layout; `GameFlow`
  places the tiles as before.
- **`Build` stays as an editor-only bake source** on `TileView`, `CardView` and `BenchView` (`#if UNITY_EDITOR`), because the Editor
  builders (`DepthTestBuilder`, `DepthTestEvidence`) and `ViewBaker` still call them. They are deleted with the builders in Task 16.
  The player build has no `Build` for these three.
- **The Hud is a nested prefab** inside Bench.prefab (instantiated by `PrefabUtility.InstantiatePrefab` at bake time), so
  `GameFlow.hudPrefab` is gone. `BenchView.Bind(ContentDb)` runs once and finishes the Hud and the Tooltip.
- **The tooltip's 9-slice sprite is cleared before the Bench bake saves.** It is made by `Sprite.Create`, which no prefab keeps; left
  in place it was stored in `Main.unity` as an override of the instance. `Tooltip.Bind` makes it again at run time.
