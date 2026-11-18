# Phase 6 Task 14 — Bench, tiles and cards are prefabs

Story P6-14. Run 2026-11-18 on the code of the commit that holds this folder.

| Step | Result | File |
|---|---|---|
| CI (`tools/ci/run.sh all`) | exit 0; Core 331/331 | `ci.log` |
| EditMode | 500 tests, 494 passed, 0 failed, 6 skipped (3 new) | `editmode.xml` |
| New tests red first | compile errors (no `Bind(ContentDb)`, `TileCount`, `Show`) | `red.log` |
| Mutation check | with `CardView.Show` line-2 and name lines removed, 2 of the 3 new tests fail; restored | (not kept) |
| Bakes | `ViewBaker.BakeCard`, `ViewBaker.BakeBench`, exit 0 | `bake-card.log`, `bake-bench.log` |
| Dev build | exit 0 | `build.log` |
| WebTest | exit 0, 129 screenshots, 0 console errors | `webtest.log`, `web/` |
| HD2D `hud,tiles` | exit 0, sway 27/27, shake 27/27 (baseline 27/27 and 20/27), 0 console errors | `hd2d/` |
| Descents | exit 0, 0 console errors | `descents.log`, `descents/` |

## hud comparison with the Task 0 baseline

The pictures in `hd2d/` were **not** compared by eye by the agent: its image viewer is blocked by a hook, so a person should open
`hd2d/hud-bench-640x360.png` beside `../baseline/hd2d/hud-bench-640x360.png`. What was measured instead:

- Every sway check position of the nine tiles is within 0.3 art px of the baseline (11 of 27 identical, 16 within 0.3), the same
  jitter as between two runs on unchanged code.
- A full-frame pixel diff is **not** a usable gate here: the Task 13 run, accepted earlier, differs from the baseline by 13–17 %
  (sway, dust, candle flicker, a random starting hand: the hand strip alone differs by 55–68 % in both).
