# 5b-06 Measure the DepthTest HD-2D prototype (for Owner gate G3)

Measured only; no fixes, no decisions. Branch `feature/phase5b-hd2d`, HEAD `d36e07d` (2026-11-17 15:15), tree clean. Both release
builds were made after that commit (Main 15:18, DepthTest 15:39) with `unity build Athanor --target WebGL --execute-method
WebBuild.Build` / `WebBuild.BuildDepthTest`. Chrome 157.0.8103.38, Linux, 12-core desktop.

## 1. Size (`bytes.txt`)

| file (bytes) | 5a-17 recorded (macOS) | a409cd5 rebuilt here (Linux) | **Main, branch tip (Linux)** | DepthTest (reference) |
|---|---|---|---|---|
| data | 3,235,095 | 3,185,549 | 3,191,719 | 2,898,565 |
| wasm | 7,359,292 | 7,360,681 | 7,368,428 | 7,368,428 |
| framework.js | 74,558 | 74,558 | 74,558 | 74,558 |
| loader.js | 48,779 | 48,779 | 48,779 | 48,779 |
| index.html | 6,561 | 6,561 | 6,561 | 6,585 |
| **total** | **10,724,285** | 10,676,128 | **10,690,045** | 10,396,915 |

- **The number that counts against the ceiling is Main (b)**: 10,690,045 B. Ceiling 11,824,672 B; **headroom 1,134,627 B**
  (90.4 % used). DepthTest (10,396,915 B) is a one-scene build for measuring and never ships.
- Cross-host caveat: the 5a-17 figure was built on macOS. A different host moves the bytes (data -48 KB for the same sources).
  So a straight subtraction from 5a-17 (-34,240 B) is not the cost of 5b. I rebuilt the pre-5b commit `a409cd5` on this Linux
  host (copy of the repo in scratch, Unity run with `-projectPath`): **5b-so-far costs +13,917 B (+0.13 %)**: data +6,170
  (URP asset lists `DepthRenderer2D`, depth settings), wasm +7,747 (new Game scripts: DepthRig, CameraSway, FogSheet, ...).
  Using the recorded 5a-17 base plus that same-host delta, headroom is 1,086,470 B. Either way growth is far below the 10 % flag.
- The 19 depth PNGs (152,134 B on disk) are **not in Main yet**: Main's build report lists none of them, and the 4 depth shaders
  (FogLit, DepthMeshLit, TiltShift, DepthDisplay, 28 KB source) are not in Main either.
- **Estimate for 5b-07**: depth art is flat, low-colour pixel art, so gzip of the raw RGBA of all 19 images is 150,280 B (zlib -9),
  about their PNG size; the 4 shaders add roughly 10 KB compressed (more if WebGL shader variants multiply), scene objects and
  materials a few KB. **Expect +165 to +210 KB, Main about 10.86 to 10.90 MB, headroom about 0.92 to 0.97 MB.** Unknown:
  Unity may store `workshop_wall_far` (640x360) in the data bundle as raw RGBA (900 KB uncompressed, 42 KB gzipped); the
  estimate assumes the data file is gzipped as it is today. Replacing the old Background wall with the new one would lower it.

## 2. Frame time (`perf/`, `matrix.log`, `env.json` in each folder)

Method: `tools/web/depthprobe.mjs`, requestAnimationFrame deltas over 15 s (20 s on SwiftShader) after a 3 s settle, no
screenshots during the sample, first 1/8 of frames dropped. Viewport set exactly to 1280x720 (2x) and 1920x1080 (3x); canvas
reported 1:1. DepthTest keys sent over DevTools: `5` = depth-5 preset (heaviest fog), `E` = effects off (bloom, vignette, tilt-shift,
fog off). Sway is on throughout. Main is the title screen (what loads), the only Main scene measured.

**GPU available: yes, a real GPU, but not a laptop one.** Headless Chrome with `--use-angle=vulkan` reports
`ANGLE (Intel, Vulkan 1.3.255 (Intel(R) UHD Graphics 630 (CML GT2)), Intel open-source Mesa driver)`: the integrated GPU of this
Comet Lake **desktop** CPU, a low-end iGPU, roughly the class of a thin-laptop iGPU. The default headless flags give
`SwiftShader` (CPU rendering), shown for completeness; its numbers say nothing about a laptop. "Uncapped" = vsync and the
frame-rate limit switched off; it is not a clean uncapped number (Main still reads 82 fps at 2x), so use the capped rows for
dropped frames and treat uncapped as the cost trend. The sample also includes headless compositing.

| build | scale | renderer | pacing | scene | mean ms | p95 ms | p99 ms | fps | frames >33 ms |
|---|---|---|---|---|---|---|---|---|---|
| DepthTest | 2x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync 60 Hz | startup preset, fx on | 19.07 | 33.3 | 66.6 | 52.4 | 16/776 |
| DepthTest | 2x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync 60 Hz | depth 5, fx on | 19.84 | 33.4 | 33.4 | 50.4 | 35/746 |
| DepthTest | 2x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync 60 Hz | depth 5, fx OFF (E) | 16.67 | 16.7 | 16.8 | 60 | 0/890 |
| DepthTest | 2x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync+limit off | startup preset, fx on | 16.9 | 33.7 | 34.7 | 59.2 | 57/876 |
| DepthTest | 2x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync+limit off | depth 5, fx on | 17.03 | 33.6 | 34.8 | 58.7 | 51/870 |
| DepthTest | 2x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync+limit off | depth 5, fx OFF (E) | 14.34 | 30.7 | 33.2 | 69.7 | 9/1035 |
| DepthTest | 2x | SwiftShader (CPU) | vsync 60 Hz | startup preset, fx on | 253.37 | 266.7 | 300.1 | 3.9 | 69/69 |
| DepthTest | 2x | SwiftShader (CPU) | vsync 60 Hz | fx OFF (E) | 136.64 | 150 | 233.3 | 7.3 | 134/136 |
| DepthTest | 3x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync 60 Hz | startup preset, fx on | 40 | 50.1 | 66.6 | 25 | 177/365 |
| DepthTest | 3x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync 60 Hz | depth 5, fx on | 41.26 | 66.6 | 66.7 | 24.2 | 207/353 |
| DepthTest | 3x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync 60 Hz | depth 5, fx OFF (E) | 25.01 | 33.4 | 50 | 40 | 95/587 |
| DepthTest | 3x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync+limit off | startup preset, fx on | 30.95 | 47.5 | 51.4 | 32.3 | 110/475 |
| DepthTest | 3x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync+limit off | depth 5, fx on | 30.94 | 48.3 | 51.1 | 32.3 | 114/474 |
| DepthTest | 3x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync+limit off | depth 5, fx OFF (E) | 25.74 | 33.1 | 43.4 | 38.9 | 23/572 |
| DepthTest | 3x | SwiftShader (CPU) | vsync 60 Hz | startup preset, fx on | 604.14 | 649.9 | 666.6 | 1.7 | 28/28 |
| DepthTest | 3x | SwiftShader (CPU) | vsync 60 Hz | fx OFF (E) | 303.2 | 366.6 | 466.6 | 3.3 | 57/57 |
| Main | 2x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync 60 Hz | title screen | 16.67 | 16.8 | 16.8 | 60 | 0/890 |
| Main | 2x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync+limit off | title screen | 12.17 | 27.4 | 31 | 82.2 | 3/1222 |
| Main | 2x | SwiftShader (CPU) | vsync 60 Hz | title screen | 64.07 | 83.4 | 116.6 | 15.6 | 297/302 |
| Main | 3x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync 60 Hz | title screen | 20.5 | 33.4 | 33.4 | 48.8 | 23/721 |
| Main | 3x | Intel UHD 630 (ANGLE/Vulkan, Mesa) | vsync+limit off | title screen | 19.89 | 34.1 | 34.9 | 50.3 | 48/743 |
| Main | 3x | SwiftShader (CPU) | vsync 60 Hz | title screen | 97 | 133.3 | 216.6 | 10.3 | 192/195 |

Reading (Intel UHD 630, vsync 60 Hz, DepthTest vs Main title): at **2x** DepthTest averages 19.1 ms (52 fps, p95 33 ms, 16 frames
over 33 ms) against Main 16.7 ms; at **3x** 40.0 ms (25 fps, p95 50 ms) against Main 20.5 ms (49 fps). Switching effects off gives
16.7 ms at 2x and 25.0 ms at 3x (depth 5), so the post chain and fog are about 3 ms at 2x and about 15 ms at 3x on this iGPU;
the perspective world alone is cheap at 2x. Frame times land on 16.7 / 33.4 / 50 ms steps: it is vsync quantisation. Main itself
already drops from 60 to ~49 fps at 3x on this iGPU. On SwiftShader everything is below 5 fps at 2x (Main title 15.6 fps): not
usable as a performance number.

**Manual procedure for the owner's laptop** (no laptop was available to me): serve `Athanor/Build/web-depth` with
`python3 -m http.server 8081 -d Athanor/Build/web-depth`, then on the laptop (Node 22+, Chrome; the probe needs no install) run
`node tools/web/depthprobe.mjs http://127.0.0.1:8081/ out3x 1920x1080 wait:3,perf:default:15,key:5,wait:2,perf:depth5:15,key:e,wait:2,perf:fxoff:15 --gpu`
(and `1280x720` for 2x). `--gpu` uses ANGLE/Vulkan; on macOS or Windows set `GPU_FLAGS=--use-angle=metal` or `--use-angle=d3d11` first. Read the
renderer name in `out3x/env.json` and mean/p95 in `default.json`, `depth5.json`, `fxoff.json`. Run it plugged in, on the GPU you care about. For the eyes only: press `E` (effects) and `5` (depth 5) and watch
Chrome's `Rendering > Frame Rendering Stats` overlay.

## 3. Shimmer (`shimmer/`)

Capture: `tools/web/depthshimmer.sh` (DepthTest, GPU, depth 3, 60 pictures over about 12 to 24 s, sway period 6 s and 9 s),
analysis `tools/web/depthshimmer.py`. Pictures arrive 200 to 400 ms apart (headless draws on request), so these are samples of
the motion, not every frame. Regions in art px: wall bricks (155,22)-(260,58), bench edge (175,238)-(465,252), tiles
(212,146)-(428,238), fog over tiles (0,150)-(105,235), HUD panel (control). `changed` = share of pixels whose luma moved by
more than 8 levels between consecutive pictures (`shimmer.txt`, `shimmer.json`).

| region | 2x sway on / off, fx on | 3x sway on / off, fx on | 2x sway on / off, **fx off** | 3x sway on / off, **fx off** | still pair 1 s (2x / 3x), fx on |
|---|---|---|---|---|---|
| wall | 0.00 / 0.00 % | 0.00 / 0.00 % | 2.22 / 0.00 % | 2.08 / 0.00 % | 0.00 / 0.00 % |
| bench edge | 24.1 / 27.7 % | 21.5 / 18.6 % | 1.01 / 1.00 % | 1.65 / 0.86 % | 10.4 / 10.8 % |
| tiles | 16.7 / 19.0 % | 14.1 / 13.0 % | 0.68 / 0.40 % | 0.83 / 0.39 % | 8.5 / 8.7 % |
| fog over tiles | 47.7 / 46.4 % | 34.2 / 31.3 % | 0.00 / 0.00 % | 0.00 / 0.00 % | 12.1 / 9.7 % |
| HUD panel | 0.00 / 0.00 % | 0.00 / 0.00 % | 0.00 / 0.00 % | 0.00 / 0.00 % | 0.00 / 0.00 % |

What it shows, and what it does not:
- With effects on, **the pixel change on the bench and tiles is the same with sway off as with sway on** (24 vs 28 %, 17 vs 19 %
  at 2x). The change comes from the fog sheets drifting and the dithered fog grain (the fog region alone changes 31 to 48 % between
  pictures) plus the candle lights and dust, not from the sway. With effects off the bench and tiles change 0.4 to 1.7 %, and
  the sway adds only 0 to 0.8 points (candle and dust motion remain).
- A still pair a second apart with sway off already differs by 8 to 12 % on bench, tiles and fog: the grain is animated at
  rest. Whether it reads as pleasant mist or as noise is the G2 #2/#3 "judge in motion" question; numbers cannot settle it.
- The HUD panel does not change at all (0.00 %) at 2x and 3x.
- The wall metric is weak: bricks are dark and low contrast, so a move under 8 luma levels registers as 0 %. The wall shows
  2 % changed with effects off and sway on. I did not prove sub-pixel crawl on the wall, either way; judge it in the GIFs.
- Anchor rows: the bench edge changes 1 % with effects off, sway on or off, so the anchor geometry is steady, as the click tests say.

Files for the owner to judge in motion (native resolution, 120 ms per picture, so about 1.5x to 3x slower than real time):
`sway-2x-bench-crop.gif`, `sway-3x-bench-crop.gif` (bench, tiles, table edge), `sway-2x-fog-over-tiles-crop.gif`,
`sway-3x-fog-over-tiles-crop.gif` (the fog grain over the tiles and left shelf, G2 #2/#3), `strip-2x-fog-over-tiles.png`,
`strip-3x-fog-over-tiles.png` (six pictures across the sway), `diff-{2x,3x}-{sway,still}-pair-x8.png` (picture difference
times 8), `frame-{2x,3x}-depth3-sway.png`, `frame-{2x,3x}-depth5-fog.png`, `frame-{2x,3x}-effects-off.png`. Real-time viewing
in a browser (`E` toggles effects, `S` the sway, `1..5` depth) is still the only honest judge of the grain.

## 4. Console

Every run loaded with **0 console errors, 0 exceptions** (Log domain included): Main 68 messages, DepthTest 170 to 6,700 messages
(it logs while running; no errors). Load time to canvas-up: DepthTest 1.0 to 3.2 s, Main 1.0 to 3.2 s on localhost (not a network
figure; the release is about 10.4 MB over gzip). Per-run `perf/*/console.txt`.

## 5. HUD at 1x (`hud1x/`): reproduced

At a 1x display (`640x360` viewport and the `1280x633` viewport of 5b-05) the small HUD text is garbled in the DepthTest player:
`POTENCY`, `CATALYSTS`, `LAST REACTION` are illegible; `Journal`, `Redraw` and `Ignite` read. At 2x and 3x the same texts are clean
(`shimmer/frame-2x-depth3-sway.png`). Observed only, no fix (5b-05 already assigns the check of `Main`'s HUD at 1x and the UI camera
settings to 5b-07). Note 1280x633 windowboxes to 1x (640x360 centred).

## Tools added (all under `tools/web/`, mine)

`depthprobe.mjs` (rAF frame time, renderer name, console, picture sequences), `depthmatrix.sh` (the frame-time matrix),
`depthshimmer.sh` and `depthshimmer.py` (sequences and per-region numbers). Scratch folders from the first trial runs were removed.
Build output stays in `Athanor/Build/` (gitignored, not staged).
