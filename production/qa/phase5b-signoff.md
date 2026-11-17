# Phase 5b sign-off (5b-08, QA part)

Run by `qa-engineer`, 2026-11-17, on the working tree of `feature/phase5b-hd2d` (uncommitted 5b-07). No game code was edited.
Fresh `WebBuild.Build` and `WebBuild.BuildDev` from the current tree (both exit 0, `evidence/phase5/5b-08/build-release.txt`, `build-dev.txt`).
All new evidence: `production/qa/evidence/phase5/5b-08/`. Version string not touched (owner decision).

**Verdict: PASS with accepted gaps (listed below). No failures. The release gate on size is met with 907,383 B headroom.**

| # | Item | Command or evidence | Real count | Result |
|---:|---|---|---|---|
| 1 | CI all | `tools/ci/run.sh all` -> `5b-08/ci.txt` | exit 0; lint green; check_repo 0 problems (BUILD-OUTPUT, CONTENT, ICONS, LANG, CORE-PURITY, META ok); rotate_sync OK; CoreTests 331/331 | pass |
| 2 | Core tests | `dotnet test tools/CoreTests` -> `5b-08/coretests.txt` | 331 total / 331 pass / 0 fail / 0 skip-excluded (BalanceMatrix and EventReport are `Explicit`) | pass |
| 3 | BalanceMatrix = Phase 4 baseline | `--filter BalanceMatrix` -> `5b-08/balance.txt` | 12 of 12 rows identical to the 5a sign-off: wins 132, 164, 134, 164, 166, 129, 173, 140, 163, 169, 144, 129 of 200 (avg depth 3.76..4.04). Content unchanged in 5b, no finding | pass |
| 4 | `InformedPlayer_WinsMostSeeds` | in the CoreTests run | test passes (asserts 24..31 of 40; 28/40 was the 5a figure). Exact count not re-derived this run (no scratch runner) | pass (note a) |
| 5 | EditMode | not re-run by QA; figure from the main session | 458 total / 452 pass / 0 fail / 6 skip | accepted from main session (note b) |
| 6 | Release size | `5b-08/bytes.txt` | data 3,414,845 + framework 74,558 + loader 48,779 + wasm 7,372,546 + index.html 6,561 = **10,917,289 B**; ceiling 11,824,672; **headroom 907,383 B**. vs 5a 10,724,285: +193,004 B (+1.80%). vs 5b-06 10,690,045: +227,244 B (+2.13%). Identical to the 5b-07b step 3 figure (reproducible) | pass |
| 7 | loadsmoke, release | `5b-08/loadsmoke.txt`, `loadsmoke/` | exit 0; loaded 5.0 s; 543 messages, 0 errors. (Earlier run: 4.1 s; this one overlapped the end of the dev build and the Core tests, so the time is a noisy upper figure) | pass |
| 8 | webtest on dev build | `5b-08/webtest.out`, `webtest/` | WEBTEST_EXIT 0; 38,575 messages, 0 errors, 129 screenshots; parts A-E incl. save damage (journal and profile start fresh), tutorial/pause, Settings EN<->VI kept after reload, title -> intro -> run | pass |
| 9 | autoplay on dev build | `5b-08/autoplay.out`, `autoplay/` | exit 0; 948 messages, 0 errors; 60 frames + second run, Goals from summary | pass |
| 10 | HD-2D moods and sway, dev build | `mainhd2d.mjs ... 1280x720 moods,sway` -> `5b-08/mainhd2d.out`, `mainhd2d/` | exit 0; checks 7/7; 2160 messages, 0 errors; depth 1..5 mood frames, effects off/on, sway frames | pass (sway/tiles quantified in 5b-07 evidence) |
| 11 | Descents EN | `descents.mjs ... 240 en` -> `5b-08/descents_en2/`, `descents_en.out` | exit 0; descents 2, 3, 4 photographed; 7938 messages, 0 errors | pass; d5 not photographed (gap) |
| 12 | Vault | `python3 tools/vault/build_vault.py` | 127 code types, 425 tests, 389 notes; `Content Health` -> Problems: none | pass |
| 13 | Build profile / build output | `git status` | `Web - Desktop - Development.asset` is **modified** (webGLDataCaching, webGLCompressionFormat, m_CodeOptimization, m_WebGLTextureSubtarget). It was already `M` before this run and the owner stages by hand; builds may rewrite it. Build output is git-ignored, none staged | info for owner (Q1) |

## UI evidence (looked at myself)

| Screen | File (under `5b-08/`) | What I saw |
|---|---|---|
| Title | `webtest/a01_title_1280.png` | Blurred shelves left and right, tilted table, central dark vignette, big Athanor logo, Begin/Journal/Goals, three small buttons, font credit. Readable, no garble at 1280x720 |
| Bench depth 1 | `mainhd2d/mood-depth-1-1280x720.png`, `mood-depth-3-...` (same frame, depth chosen by dev key) | Perspective table, 3x3 board with lit tiles, HUD panels crisp over it, side blur, warm candle glow, fog grain at the sides. Window shows the sealed-gold glow |
| Bench depth 2..5 | `mainhd2d/mood-depth-2..5-1280x720.png`, `mood-depth-5-effects-off-...` | Mood changes (window darker and colder at 5, vignette stronger). Note: HUD text stays "Depth 1" because these frames step the mood by dev key, not by playing; the real depth HUD is in `descents_en2/b2..b4_bench_en.png` |
| Settings EN | `webtest/d01_settings_en.png`, `d02_settings_en_1x.png` | Row "Camera sway / On" over the language row, Reset progress, Back; fits |
| Settings VI | `webtest/d02_settings_vi.png`, `d02_settings_vi_1x.png` | "Lắc camera" with "Bật", "Ngôn ngữ / Tiếng Việt", "Cài đặt" title; label fits the row at ~120 px with room to the value box, diacritics intact. The value text sits near the top of its box (known VI half-leading offset) |
| Cutscene intro | `webtest/a03_intro.png` | Flat room (cauldron, candles, bricks) at z = 0, no tilt, typewriter line starting. Matches the Phase 5a look |
| Descent | `descents_en2/d3_en_3_4.png` (d2, d4 alongside) | "DEPTH 3 / The Humming Dark", intro line, goal toast on top, window rings; flat, unaffected by the HD-2D rig |
| Run over | `descents_en2/ra_runover_en.png` | "The Fire Dies" panel over the blurred perspective bench, stats rows, Descend again / Title; legible |
| Mid-run bench | `autoplay/q1_end.png` | Reaction popup +40, Last Reaction panel, hand slots, hint; HUD sharp over the world |

Not photographed: VI run over and VI descents under HD-2D (existing 5a VI frames are from before the rig), Legendary Journal frame.

## Accepted gaps (owner accepted earlier or by "đồng ý đề xuất"; not re-tested unless stated)

1. **Shake click in WebGL**: 25/45 in the harness (latency, not a bug); proven by `DepthClickTests.DrawnFace_UnderShakeUpToThreePx_...` (EditMode). Sway and rest tile clicks 45/45 each (5b-07 evidence), not re-run today.
2. **1x HUD garble in SwiftShader**: does not reproduce on release with `--gpu`; the software-rendered path was not checked.
3. **d5 descent frames** still missing (descents.mjs reaches d2-d4 only).
4. **VI half-leading offsets of 2-3 px** (and 2-7 px on older banners), tight VI catalyst lines, high VI buttons.
5. **Fog grain judged by eye**; no shimmer metric.
6. **Performance on the owner's G3 laptop**: numbers not recorded. Only the 5b-06 Intel UHD 630 figures exist (DepthTest 19.1 ms at 2x, 40.0 ms at 3x; Main 16.7 / 20.5). No frame-time measurement of the integrated Main was made in 5b-08.
7. Reduced-effects switch not built (G3 decision).
8. Not carried over: ~78% full-profile win rate under set A, win at any depth, lost run-over covered by tests only.

## Notes
- a. 28/40 is the 5a count from a scratch reflection runner; the unit test only bounds it.
- b. EditMode was not re-run here (Unity was busy with two builds, the batch tool serialises). The 458/452/0/6 figure is the main session's.
