# Phase 6, Step 2 sign-off (Task 17, story P6-17)

Run 2026-11-19 on HEAD `608b4f86` (branch `feature/phase6-unity-refactor`). Step 2 is the Game-layer refactor: every screen is a
prefab or a `Main.unity` object, and `SceneBuilder`, the `Make` factories and `GameFlow.I` are gone. The baseline (Task 0) stays in
`baseline/`; the final run is in `task17/`.

## Full verification at HEAD

| Check | Result | File |
|---|---|---|
| CI `tools/ci/run.sh all` | exit 0; lint clean, repo check OK, rotate OK, Core 331 passed / 0 failed / 0 skipped | `task17/ci.log` |
| EditMode | exit 0; 505 total, 499 passed, 0 failed, 6 skipped | `task17/editmode.xml` |
| Release build `WebBuild.Build` | exit 0 | `task17/build-release.log` |
| Dev build `WebBuild.BuildDev` | exit 0 | `task17/build-dev.log` |
| WebTest | exit 0; 129 screenshots, 38,645 console messages, 0 errors | `task17/webtest.log`, `task17/web/` |
| HD2D `hud,tiles` | exit 1 (shake only, see below); checks 43/54, sway 27/27, 0 console errors | `task17/hd2d.log`, `task17/hd2d/report.json` |

Exit codes of all steps: `task17/exits.txt`.

### HD2D: exit 1 is the accepted shake gap

All 11 failing checks are `shake` points (`pre-inside=false post-inside=false drift=4.7-5.1 px`). Sway is 27/27, the hud checks pass
and there are 0 console errors, which is the gate in the plan. Shake is recorded, not gated (5b-08 accepted gap: harness latency,
proven by `DepthClickTests` in EditMode). It varies run to run on unchanged code:

| Run | shake |
|---|---|
| Task 0 baseline | 20/27 |
| Task 2 | 18/27 |
| Task 14 | 27/27 |
| Task 17 (this run) | 16/27 |

16/27 is the lowest of these. It is within what the plan calls run-to-run variance, but it is a number to watch, not a proof of no
change.

## Per view: before / after pixel difference

`views/<Name>/before.png` was captured from the old code builder, `after.png` from the baked prefab. `ViewBaker.Compare*` was deleted
with the builders in Task 16, so its logs are gone. The figures below were **recomputed** from the kept pictures with
`task17/pngdiff.py` (standard library only; counts pixels whose RGBA differs at all). Output: `task17/views-diff.txt`. The limit is
0.1 %.

| View | Differing pixels | Limit |
|---|---|---|
| RunOver | 0 of 230,400 (0.000 %) | 0.1 % |
| GoalToast | 0 of 230,400 (0.000 %) | 0.1 % |
| Event | 0 of 230,400 (0.000 %) | 0.1 % |
| CatalystChoice | 0 of 230,400 (0.000 %) | 0.1 % |
| Pause | 0 of 230,400 (0.000 %) | 0.1 % |
| Goals | 0 of 230,400 (0.000 %) | 0.1 % |
| Journal | 0 of 230,400 (0.000 %) | 0.1 % |
| Title (default, Skip, Confirm, Settings) | 0 of 230,400 (0.000 %) each, four poses | 0.1 % |
| Hud | 0 of 230,400 (0.000 %) | 0.1 % |

Not in this table, because no before/after pair exists and a full-frame diff is not usable there (sway, dust, candle flicker and a
random hand change every frame): Bench, tiles and cards (Task 14: sway positions within 0.3 art px of the baseline, `task14/README.md`),
and the screen effects, window, tutorial and two cutscenes (Task 15: `task15/` web and HD2D runs, `SceneBakeTests`).

## Release size

| | Bytes | vs ceiling 11,824,672 |
|---|---|---|
| Task 0 baseline | 10,917,289 | -907,383 |
| Now | 10,961,269 | -863,403 |

Change since Task 0: +43,980 B (+0.40 %). Measured with `find Athanor/Build/web/Build Athanor/Build/web/index.html -type f -exec stat -f%z {} + | awk '{s+=$1} END {print s}'`
(`task17/release-size.txt`).

Note: `WebBuild.Build` in this run was an incremental build that finished in 1 s and rewrote no files. Unity judged the output of the
earlier Task 16 release build (files stamped 2026-11-18 23:53) current for HEAD's sources, so the size is that of the Task 16 code.

## Test counts against the baseline

| | Task 0 | Now |
|---|---|---|
| Core (`dotnet test tools/CoreTests`) | 331 passed | 331 passed, 0 skipped |
| EditMode | 458 total, 452 passed, 6 skipped | 505 total, 499 passed, 6 skipped |

The EditMode total grew by 47 net over Phase 6 (bake round-trips, scene bake tests, prefab fixtures). The devlog states that Task 16
deleted no test; the other tasks were not audited for deletions. The 6 skipped are the `[Explicit]` reports that Task 18 turns into a
console program.

## Spec "Done when", bullet 1

`grep -rnI -e 'GameFlow\.I\b' -e 'layout\.json' -e 'SceneBuilder' Athanor/Assets tools` (build output excluded):

- `GameFlow.I`: no match.
- `SceneBuilder`: no match.
- `Build(...)` and `Make.Root/Sprite/Rect/Glow`: gone. The run-time spawners live on as `Spawn` (`Support/Spawn.cs`), see
  `docs/devlogs/phase-6-unity-refactor.md`, Task 16.
- **`layout.json`: DEVIATION, kept by the owner's decision.** It is `Athanor/Assets/Editor/layout.json`, with
  `Assets/Editor/LayoutData.cs`, a comment in `Scripts/Game/Support/TileData.cs`, and the write in `tools/art/export.mjs`. The art
  generator writes it and the Editor tools read it; the game and the WebGL build never load it. Recorded in
  `docs/devlogs/phase-6-unity-refactor.md` (Task 14). The spec bullet is therefore not met to the letter.

## Other "Done when" bullets, status

- Lint baseline empty: not yet (Phase C, Tasks 18-21).
- EditMode 0 skipped: not yet (Task 18).
- `docs/ATHANOR.md`, vault regeneration, `graphify-out/`: not yet (Phases E and F).
- Step 4 deletions: not yet (Phase D).
