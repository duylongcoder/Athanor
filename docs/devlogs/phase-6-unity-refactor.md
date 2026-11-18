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

## Task 15, part A — Screen Fx, Window and Tutorial are scene objects (2026-11-18)

- **Each is its own object in `Main.unity`, baked with `BakeInScene(asPrefab: false)`:** `Screen Fx`, `Window`, `Tutorial`
  (`ViewBaker.BakeScreenFx/BakeWindow/BakeTutorial`), referenced by `GameFlow.screenFx/window/tutorial`. `GameFlow.Awake` throws if one is
  missing, like the other views. `Build` stays as an `#if UNITY_EDITOR` bake source (deleted in Task 16).
- **`WindowView.Bind()` takes no argument and `Drive()` lost its `GameFlow` parameter.** The plan wrote `rootTex`/`rootSprite` in `Bind`;
  they are made by `StartRoots` (depth 4) at run time and stay there. `Bind` only keeps `night` (the pane's sprite) and `centre` (the
  pane's flat centre), which the old `Build` computed. The pane is a serialized reference to the workshop's `window` sprite.
  `TutorialController.Bind(GameFlow)` stores the flow once, so `Drive()` needs no argument.
- **`ScreenFx` serializes only `flash` and `black`; `Bind(Camera, CameraSway)` keeps `I`, the camera home and the rig.** `LateUpdate`
  returns while the camera is unbound (a scene opened in the editor runs no play loop, but this keeps a stray instance harmless).
- **`BakeWindow` removes an earlier `WindowFx` first**: it is a child of the workshop, not a root, so `RemoveRoots` does not see it and a
  second bake would double the effect sprites. The group (`DepthLayer.AddGroup`) is stored as `Build` made it.
- **Tutorial's per-step pieces stay runtime objects.** The dim cells, rim pieces and extra labels are made in `Show` from data
  (`Make.Rect/Text/Click/Sprite`), so `rim` and `extra` are not serialized. Task 16 deletes those `Make` factories: it must keep them
  (or give them a home) for `TutorialController`, which is not a bake source.
- **Tests:** `MainSceneFixture` opens Main.unity beside the test scene (like `MainDepthTests`); `SceneBakeTests` has one test per baked
  component; `PauseTests.Abort_TutorialRun_DoesNotMarkItDone` takes the baked tutorial from the scene.

## Task 15, part B — the two cutscenes are scene objects (2026-11-18)

- **`Reaction Cutscene` and `Story Cutscene` are their own objects in `Main.unity`** (`ViewBaker.BakeReaction/BakeStory`, `asPrefab: false`),
  referenced by `GameFlow.reaction/story`; `GameFlow.Awake` throws when one is missing. Neither class assumed it sat on `GameFlow`'s object
  (no `GetComponent<GameFlow>`, no `gameObject.` use), so nothing else had to be passed through.
- **`ReactionCutscene.Bind(GameObject[] others)` keeps `others` out of the scene and calls `Stage(false)`.** The workshop and the bench are
  handed in at run time, as the plan says. The plan's field list missed `matGlow`, `ui`, `plus`, `times` and `total`; all are serialized.
  `matCol` is not: `Setup` fills it for every reaction, so it is a run-time buffer and not scene data (the plan listed it).
- **The `Build` of both stays a bake source and no longer touches the rest of the scene.** `ReactionCutscene.Build(room)` used to call
  `Stage(false)`, which would also have switched the workshop and the bench on and the room off in the saved scene; it now only hides its
  own stage and UI. The room, workshop and bench keep the states they had (checked against HEAD: only objects were added).
- **The story stage is a child of the `Story Cutscene` object** (`Make.Root("Story", transform)`, as the tutorial does). It was a scene root
  before; at the origin with the same layer the picture is the same, and a second bake now replaces it with its object. `BakeReaction`
  removes an earlier `Reaction` and `Reaction UI` under the cutscene room first, because they are children of the room, not roots.
- **`SimpleCutscene` has no `Bind`.** Its room, workshop and bench are serialized references to scene objects, so there is nothing to hand it at
  run time; its stage is saved hidden. The `Loop`s stay runtime (made in the first `Update`). UL001 is fixed: `rayFaces` holds the ray sprites
  made by `Build`, so `Update` no longer calls `GetComponent` per ray per frame. The `ReactionCutscene` still calls `GetChild(0).GetComponent`
  in `Draw` (14 per frame): not an `Update`, so not a lint finding, left alone. The UL001 line of `tools/lint/baseline.json` is now stale.
- **Tests:** `SceneBakeTests` gains one test per cutscene (every part non-null, array lengths, the stage hidden, `Bind` switching the
  others, the ray faces being the ray sprites).

## Task 16 — builders and the singleton removed (2026-11-18)

- **`Make` became `Spawn`, not `Fonts` alone.** The plan assumed `Make.*` only served the bake sources. It does not: `TutorialController`
  (dim rects, clicks, rim, text lines, greyed sprites), `GameFlow.ToRack` (glow + sprite), `BenchView.FloatText` (text), `LinkView` (sprite
  motes) and the `RarityStars(Transform, int)` constructor (the Journal makes its corner stars when a cell is set up) spawn objects while the
  game runs. So `Support/Make.cs` is now `Support/Spawn.cs` (class `Spawn`: `Root`, `Sprite`, `Rect`, `Glow`, `Text`, `Click` and the private `Tmp`)
  and `FontFor`, `SizeFor`, `HalfLeading` moved to `Support/Fonts.cs` (class `Fonts`, with the `FontKind` enum). `SpriteAt`, `TextKey` and `Light` had no
  caller left and were deleted. No behaviour change.
- **`Settings/DepthTest/DepthRenderer2D.asset` is kept.** `UniversalRP.asset` lists it as renderer 1 and Main's camera uses index 1, so deleting the
  folder would have broken the release build. It moved (GUID kept) to `Assets/Settings/DepthRenderer2D.asset`, next to `Renderer2D.asset`; the
  four materials moved to `Assets/Art/Depth/Materials/`. Only `DepthVolume.asset` (used by `DepthTest.unity` alone) went with the scene.
- **Tests:** the bake round-trip tests now load the shipped prefabs (`RunOver.prefab` button and label, `Bench.prefab` tooltip) and
  `LabelLineBoxTests` uses a Title label from `RunOver.prefab`. The round-trip of `Disabled` is checked in the serialized state.
  No test was deleted.
