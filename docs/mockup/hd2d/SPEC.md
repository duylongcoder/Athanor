# 5b-01 HD-2D mockup: spec, proposed values and questions for gate G2

Mockup: `docs/mockup/hd2d/index.html` (canvas 2D, no CDN, opens from disk with `file://`; needs `assets.js` beside it, rebuilt by
`bash docs/mockup/hd2d/build.sh` from the real Unity art, the phase5-ui fonts and `layout.json`).
Evidence: `production/qa/evidence/phase5/5b-01/` (19 frames, `page-controls.png`, `textures.txt`, `console.txt`), written by
`node docs/mockup/hd2d/shoot.mjs production/qa/evidence/phase5/5b-01` (56 checks, 0 console errors).
Status: **proposal for the owner at G2.** Nothing here is decided except what `docs/devlogs/phase-5-prep.md:52-58` already decided.
Coordinates are art pixels on 640x360, y down, unless a line says "px at 2x" (screen pixels of the 1280x720 world).

## What it shows

The bench scene in the approved 5b design: the **world** drawn at the display scale (2x = 1280x720 is the floor; 3x = 1920x1080; 1x only
for comparison) under the **HUD**, which is a separate flat 640x360 canvas shown with nearest-neighbour and never touched by any effect,
sway or shake. Effects, each with its own switch: tilt-shift along screen Y, pre-blurred far layer, pre-blurred near layer, foreground
strip, back fog, front fog, bloom, vignette. Five depth moods, camera sway, world shake, and a flat reference mode.

Panel buttons: "All effects on", "All effects off", "restdiff" (the hd2d world with every effect off against the flat 1:1 draw; must be 0).

### URL parameters (the same keys work in `window.hd2d.set({...})`)

| Key | Values (default) | Meaning |
|---|---|---|
| `mode` | `hd2d` / `flat` (`hd2d`) | flat = the plain 2D draw, no effect, no sway, no shake |
| `depth` | 1..5 (1) | loads that depth's preset (`bth bin vi fba ffa`) unless they are given too |
| `scale` | 1 / 2 / 3 (2) | display scale; the world is rendered at this scale |
| `strip` | `corners` / `full` (`corners`) | shape of the foreground strip |
| `tilt`, `ts`, `tc`, `tw` | 0/1 (1), 4, 190, 90 | tilt-shift on, strength (px at 2x), band centre (art y), band width (art px) |
| `far`, `fr` | 0/1 (1), 2 | far pre-blur on, radius (px at 2x) |
| `near`, `nr` | 0/1 (1), 6 | near pre-blur on, radius (px at 2x) |
| `fg` | 0/1 (1) | foreground strip |
| `fogb`, `fba` | 0/1 (1), preset | back fog on, alpha |
| `fogf`, `ffa` | 0/1 (1), preset | front fog on, alpha |
| `bloom`, `bth`, `bin` | 0/1 (1), preset | bloom on, threshold (0..1), intensity |
| `vig`, `vi` | 0/1 (1), preset | vignette on, strength |
| `sway`, `sa` | 0/1 (0), 4 | camera sway on, amplitude |
| `shake` | 0/1 (0) | world shake |
| `anim`, `t`, `still` | 0/1 (1), 2, `still=1` | time runs; start time in s; `still=1` freezes time at `t` (screenshots) |
| `hud` | 0/1 (1) | show the HUD |

Console helpers: `hd2d.restdiff()`, `hd2d.hudHash()`, `hd2d.hudMask()`, `hd2d.exportTextures()`.

## Layer stack and projection

Camera at rest: distance **D = 600** to the plane z = 0 (1 unit = 1 art px there), so vertical fov = 2 atan(180 / 600) = **33.40 degrees**
(`phase-4-depth.md:185`). z is negative away from the camera. A layer at depth z is scaled by **(D - z) / D** so that at rest it lands
exactly on its art pixels (restdiff = 0 at every depth, checked).

Under sway the camera moves by `cam` and the picture is re-aimed so that the **middle tile row (art y 188) never moves**:
`shift(z) = cam * par(z)`, `par(z) = -(D / (D - z) - D / (D - zAnchor))`, `zAnchor = tableZ(188) = -145.77`. This is what keeps clicks in place.
The table is a floor: each art row has its own depth, `tableZ(y) = D - 1 / (1/dB + (1/dF - 1/dB) * r)`, `dF = 660` (front edge, z = -60),
`dB = 1.27 * dF = 838.2` (back edge, z = -238.2), `r = (y - 132) / 122` clamped to 0..1.

| Draw order | Layer | z | Scale (D - z)/D | par(z) | Shift at sway 4 (art px, x) | Lit | Blur |
|---|---|---|---|---|---|---|---|
| 1 | wall (with mirrored overscan 96 x 54) | -400 | 1.667 | +0.2045 | +0.82 | yes | far |
| 1 | window view of the depth, moon shaft | -398 | 1.663 | +0.2033 | +0.81 | view unlit | far |
| 1 | wall candles, flames, halos | -392 | 1.653 | +0.1997 | +0.80 | candle lit, flame additive | far |
| 2 | back fog sheet, rect (-96,130) 832x150 | -300 | 1.500 | +0.1379 | +0.55 | yes + 0.35 | none |
| 3 | table back row (y 132) | -238.2 | 1.397 | +0.0887 | +0.35 | yes | none |
| 3 | tile row 1 (y 157) | -194.3 | 1.324 | +0.0491 | +0.20 | yes | none |
| 3 | **tile row 2 (y 188), the anchor** | -145.8 | 1.243 | 0 | 0 | yes | none |
| 3 | tile row 3 (y 219) | -102.9 | 1.172 | -0.0491 | -0.20 | yes | none |
| 3 | table front (y >= 254) | -60 | 1.100 | -0.1046 | -0.42 | yes | none |
| 4 | front fog sheet, rect (-96,150) 832x110 | +40 | 0.933 | -0.2669 | -1.07 | yes + 0.35 | none |
| 5 | foreground strip | +180 | 0.700 | -0.6240 | -2.50 | yes | near |
| post | tilt-shift, then bloom, then vignette | | | | | | world only |
| top | HUD (640x360, flat) | | | | never moves | no | never |

Table candles sit at `tableZ(candle.y + 19) + 0.5`; tiles, rings, glows and shadows at `tableZ(tile.y)` plus 0.1 to 0.4. Lights follow their
layer. Sorting is by explicit order, never by distance (`phase-4-depth.md:120`).
At sway 4 the wall and the foreground move 3.3 art px against each other (6.6 px at 2x); no tile moves more than 0.2 art px
(0.59 at the slider maximum of 12), against tile faces 24 to 25 px tall.

## Proposed values for the prototype (5b-05)

"Game" = in the build today. "Proposal" = new in this mockup, for the owner to judge.

### Fixed values

| Value | Number | Source |
|---|---|---|
| World render scale | display scale, minimum 2x (1280x720) | decided (`phase-5-prep.md:53`) |
| Camera distance D, fov | 600, 33.40 degrees | `phase-4-depth.md:185` |
| Sway anchor | tile row at art y 188 | proposal |
| Table depth | front z -60, back 1.27 times further | proposal (fits the drawn perspective) |
| Layer depths | table above | proposal |
| Tilt-shift strength | 4 px at 2x = blur sigma 2 art px at full strength | proposal |
| Tilt-shift band | centre art y 190, width 90: sharp from y 145 to 235 | proposal |
| Tilt-shift feather | 110 art rows, smoothstep, from the band edge to full strength (full at y <= 35 and y >= 345) | proposal |
| Far pre-blur | 2 px at 2x = sigma 1 art px (wall, window view, wall candles, flames) | proposal |
| Near pre-blur | 6 px at 2x = sigma 3 art px (foreground strip) | proposal |
| Foreground strip | `corners`: left piece art x -96..144, right piece x 514..736, y 214..306, z +180, darkened 55 % | proposal, stand-in art |
| Fog sheets | 320x150 (back) and 320x110 (front), tileable, 4 alpha steps, 4x4 Bayer dither, white + alpha | Phase 4 fog proposal |
| Fog drift | back +4 art px/s, front -6 art px/s, in whole texels | proposal |
| Fog light | lit like any sprite, plus a floor of 0.35 so it never goes black | proposal |
| Fog tint | white mixed toward the depth's ambient colour by `fogMix` | proposal |
| Sway | x = A sin(2 pi t / 6 s), y = A/2 sin(2 pi t / 9 s), A = 4 (camera travel in art px at z = 0) | proposal (`phase-4-depth.md:178`: keep it small) |
| Shake | whole world, every layer the same whole art px, HUD not moved; mockup uses x = round(3 sin 53.1 t), y = round(2 cos 41.7 t) | world-only is proposed (`phase-5-prep.md:56`); sizes stay those of `ScreenFx.Shake` today (2 to 5 px: `GameFlow.cs:538,569,677`, `ReactionCutscene.cs:140`, `SimpleCutscene.cs:140`) |
| Ambient light intensity | 0.25 | game (`SceneBuilder.cs:47`) |
| Moon colour | (0.42, 0.60, 1.00) | game (`SceneBuilder.cs:140`) |
| Candle light | radius 170 (wall) / 210 (table), colour (1, 0.63, 0.33) | game (`SceneBuilder.cs:154`) |

Tilt-shift by row at the proposed values (sigma in art px): y 0..35: 2.00; y 60: 1.73; y 100: 0.72; y 120: 0.26; y 145..235: 0;
y 262: 0.31; y 300: 1.28; y 345+: 2.00. The nine tile faces (y 144..232) are inside the sharp band.

### Depth presets

| Depth | Name | Ambient (game) | Moon (game) | Accent (game) | Bloom threshold | Bloom intensity | Vignette | Back fog alpha | Front fog alpha | fogMix |
|---|---|---|---|---|---|---|---|---|---|---|
| 1 | The Workshop | 0.52, 0.52, 1.00 | 0.62 | #7cc0f8 | 0.80 | 0.55 | 0.30 | 0.40 | 0.14 | 0.45 |
| 2 | The Cellar | 0.40, 0.62, 0.86 | 0.50 | #6ee6c8 | 0.78 | 0.60 | 0.36 | 0.50 | 0.15 | 0.50 |
| 3 | The Undercroft | 0.62, 0.42, 0.95 | 0.40 | #c486f4 | 0.74 | 0.75 | 0.42 | 0.55 | 0.15 | 0.55 |
| 4 | The Furnace Deep | 0.86, 0.40, 0.46 | 0.30 | #ee6a3c | 0.70 | 0.90 | 0.48 | 0.45 | 0.13 | 0.60 |
| 5 | The Last Vault | 0.78, 0.74, 0.58 | 0.50 | #fff6b8 | 0.76 | 0.65 | 0.55 | 0.62 | 0.16 | 0.50 |

Ambient, Moon and Accent are the game's arrays (`GameFlow.cs:44-49`). The six columns from "Bloom threshold" on are **proposals**.
The game today has one setting for all depths: bloom threshold 1.0, intensity 1.1, scatter 0.62; vignette 0.34, smoothness 0.5
(`SceneBuilder.cs:120-122`).

Measured on the evidence frames (2x, t = 2 s; mean luma 0..255, and the share of pixels with a channel >= 250):

| Frame | 3x3 board | Table left of the board | Wall middle | Left shelf | Left void | Right void |
|---|---|---|---|---|---|---|
| 10 all off | 90.0, 1.7 % | 70.0 | 53.2 | 55.1 | 6.3 | 7.6 |
| 12 depth 1 | 99.1, 4.6 % | 78.1 | 53.0 | 46.3 | 19.3 | 35.0 |
| 13 depth 2 | 99.8, 4.8 % | 78.6 | 51.7 | 45.3 | 20.3 | 37.9 |
| 14 depth 3 | 98.7, 5.8 % | 78.7 | 59.1 | 42.6 | 18.6 | 35.3 |
| 15 depth 4 | 97.3, 5.8 % | 77.0 | 51.6 | 40.9 | 14.8 | 27.4 |
| 16 depth 5 | 101.8, 5.4 % | 77.8 | 48.6 | 40.6 | 18.5 | 35.8 |

Reading: no preset blows out the board (the bright pixels are the gold frame, the rings and the candle flames); the vignette is 0 on the
board centre and at most 0.07 (depth 1) to 0.13 (depth 5) at the board corners; the front fog lifts the board by about 6 luma (93.4 without it).
The presets differ little on the bench; the mood is carried by the wall tint and the window view.

## Mapping mockup values to Unity

| Mockup | Unity (URP 2D Renderer, linear colour space, HDR on) | Note |
|---|---|---|
| World canvas at scale K | world camera (perspective, fov 33.40) into a 640K x 360K RenderTexture, K = whole display scale, at least 2 | `phase-4-depth.md:104-109` said 640x360; the owner raised it to >= 2x |
| Layer rect + z | sprite at z, `localScale = (D - z) / D` (`DepthLayer`) | rest frame identical; check by screenshot diff |
| `cam * par(z)` with a fixed anchor | camera translates by `cam` and its lens shift (or aim) cancels the anchor row's shift | `CameraSway`; equal to an orbit around the bench row for these small moves |
| "px at 2x" radii (`ts`, `fr`, `nr`) | divide by 2 for art px. CSS `blur(r)` is a Gaussian with sigma = r | far sigma 1 art px, near sigma 3 art px, tilt-shift up to sigma 2 art px |
| Far / near pre-blur | blurred textures from `tools/art` (5b-04), no runtime cost | a texture blurred at art resolution is upscaled by K with bilinear, not point, or the blur turns blocky |
| Tilt-shift (6 blur levels blended per row) | one full-screen pass on the world RT: blur radius per row = `ts * smoothstep(0, 1, max(0, abs(y - tc) - tw/2) / 110)`; no depth buffer | applied before bloom |
| Bloom threshold `bth` (0.70..0.80 of display white, on max(r,g,b)) | URP `Bloom.threshold` | the canvas clamps at 1, so it has to bloom below white; the game renders HDR (glow colours up to 2.4) and blooms above 1.0. Starting proposal: threshold = 1.0 + (bth - 0.80), i.e. 1.00, 0.98, 0.94, 0.90, 0.96 |
| Bloom intensity `bin` (0.55..0.90, alpha of the added blurred bright pass) | URP `Bloom.intensity` | starting proposal: 1.1 * bin / 0.55, i.e. 1.10, 1.20, 1.50, 1.80, 1.30; scatter stays 0.62 |
| Vignette `vi` | URP `Vignette.intensity` | not the same scale. The mockup darkens by `min(1, 1.6 vi) * f`, f rising from 0 at 252 px from the centre to 1 at 794 px (2x): screen corner 1.42 vi, middle of the left edge 1.15 vi, table left edge 0.50 vi, board centre 0. Starting proposal: 0.34 * vi / 0.30 |
| Fog alpha, `fogMix` | `SpriteRenderer.color` alpha; tint = lerp(white, Ambient[depth], fogMix); Sprite-Lit material | texel-stepped UV scroll |
| Light map (ambient + candles + moon, planar) | Light2D as today | the mockup's candle intensities (1.2 / 1.55) are stand-ins, not values to copy |
| HUD canvas | UI camera (orthographic, PixelPerfectCamera) drawing the world RT under the HUD | shake and sway act on the world camera only |

The URP numbers in the three "starting proposal" rows are my conversion, **not verified in Unity**: 5b-05 sets them, compares a
screenshot with frames 12 to 16 and reports the numbers it ends with.

## Textures 5b-04 would ship, and the byte estimate

Measured from `hd2d.exportTextures()` in headless Chrome (`production/qa/evidence/phase5/5b-01/textures.txt`). These are **PNG bytes of
stand-in textures**, not Unity build bytes: the build stores textures in the gzip-compressed data file, and blurred pictures compress
worse than flat pixel art (the sharp wall PNG is 15,843 B; blurred it is 163,511 B). 5b-06 measures the real delta.

| Texture | Size | PNG bytes | In set |
|---|---|---|---|
| `workshop_wall_far` (blurred at art resolution, sigma 1) | 640x360 | 163,511 | A |
| `workshop_wall_far@2x` (blurred at 2x, sigma 2 px) | 1280x720 | 480,414 | B, instead of the row above |
| `window_d1_far` .. `window_d5_far` | 84x93 each | 59,881 (13,511 / 12,571 / 11,521 / 11,786 / 10,492) | A, B |
| `candle_far` | 16x24 | 1,065 | A, B |
| `flame_small_far` (8 frames) | 122x14 | 1,824 | A, B |
| `fg_corner_left_near`, `fg_corner_right_near` (sigma 3) | 252x104, 234x104 | 33,878 (16,294 + 17,584) | A, B |
| `fog_back`, `fog_front` (white + alpha) | 320x150, 320x110 | 23,190 (12,894 + 10,296) | A, B |
| `fg_corner_left`, `fg_corner_right` (sharp sources) | 240x92, 222x92 | 5,947 | not shipped |

| Set | PNG bytes | Share of the 1,100,387 B left for 5b |
|---|---|---|
| **A**: wall blurred at art resolution | **283,349** | 25.7 % |
| **B**: wall blurred at 2x | 600,252 | 54.5 % |
| All 15 exported | 769,710 | 69.9 % |

Ceiling: the release build must stay <= 11,824,672 B; it is 10,724,285 B today, so 5b has 1,100,387 B. Recommendation for 5b-04: set A.
The 2x wall costs 316,903 B more for a difference a sigma-1 blur mostly hides. The full-width strip, if chosen, replaces the two corner
textures with one 832x80 strip (not exported; expect the same order of size).

## Evidence and what the checks prove

| Frames | Content | Checks (`shoot.mjs`) |
|---|---|---|
| 01..11 | depth 1: all 8 effects on, each off in turn (tilt-shift, far, near, strip, back fog, front fog, bloom, vignette), all off, flat | HUD pixels identical to 01; every switch changes the world; 10-all-off = 11-flat pixel for pixel |
| 12..16 | one frame per depth preset, all effects on | page values = that preset; HUD pixels identical to an all-off frame of the same depth; restdiff = 0 at each depth; 5 different HUD hashes; 12 = 01 |
| 17, 18 | sway at t = 1.5 s (camera right, x +4.00, y +1.73) and t = 4.5 s (camera left, x -4.00, y 0) | HUD pixels identical to 01; world differs from the rest frame of the same time; 17 differs from 18 |
| 19 | shake at t = 2 s (world moved 2 art px left) | HUD pixels identical to 01; world differs from 01 |
| `page-controls.png` | the whole page with the panel | restdiff = 0 after all frames |

## Known limits of the mockup

- Canvas 2D is not URP. Bloom here is an 8-bit bright pass on a clamped picture; URP blooms HDR values above 1. The vignette is a radial
  gradient, not URP's formula. Expect the Unity look to differ in strength; the presets are starting values.
- No Light2D: one planar light map (ambient + candle pools + moon cookie) multiplies each layer. Whether Light2D, the moon cookie and bloom
  behave under a perspective camera is the main unknown of 5b-05 (`phase-4-depth.md:112-116`), and this page cannot answer it.
- The foreground strip is stand-in art, cut from existing sprites and darkened. The left corner piece reads as a dark smudge. Real art is 5b-04.
- "Flat reference" has no bloom and no vignette, so it is darker and plainer than the game today (which has both). It is the baseline for
  restdiff, not a picture of the shipped game.
- Pre-blur is done with a canvas filter at run time; the game would use baked textures.
- The table is drawn as 145 row strips. Unity picks strips, a tilted quad or a line-scroll shader (`phase-4-depth.md:190`).
- Sub-pixel motion: layers snap to whole screen pixels, so at 2x the wall moves in half art px steps (about 5 positions over one swing).
  Shimmer at 2x and 3x is measured on the real build in 5b-06.
- No clicks, no hover, no cutscenes, no descent. During a shake the bench moves under a steady pointer; 5b-05 decides whether tile hit
  tests use the shaken or the unshaken world.
- Vietnamese: the mockup adds no text and no Lang key; the HUD is the unchanged 640x360 layer, so there is no new overflow case.
- Legibility: nothing new is carried by colour alone; the HUD keeps full contrast because no effect touches it.

## Questions for the owner at G2

Not decided. Each can be tried in the page with the control named.

| # | Question | Options | Recommendation |
|---|---|---|---|
| 1 | Foreground strip: corners or full width? (`Corners` / `Full width`) | (a) corners: two pieces above the note and above the Journal box, visible about 130 px wide each. (b) full width: almost all of it is under the rack and the cards; only a bottle and two rocks show at the right. (c) no strip. | (a) corners, with real art from `pixel-artist`; the left piece must be lighter than the stand-in and stay clear of the candle at (150,232). |
| 2 | Front fog over the tiles: keep it, and at what alpha? (`Front sheet`, `Front alpha`) | (a) as shown, 0.13 to 0.16 over the whole bench (lifts the board about 6 luma and lays a dither grain over tiles and icons). (b) keep it but off the 3x3 board, only over the table margins and the dark sides. (c) lower, about 0.08. (d) off. | (a) at 0.14 for the prototype: icons stay readable in every frame. Judge the drifting grain in motion at G3; (b) is the fallback. |
| 3 | Fog grain: the mist is pixel art at 640x360, so on the 2x world it shows as 2x2 dither blocks beside smooth blur. | (a) keep the pixel mist. (b) draw the fog at 2x (finer grain, about 4 times the bytes of 23,190). (c) smooth fog without dither. | (a) for the prototype; decide between (a) and (b) on the real build. |
| 4 | Sway amplitude (`Sway amplitude`) | 0 (no sway: depth is invisible, only blur, fog and bloom remain) / 2 / 4 / 6 to 8 | 4: wall +-0.8 art px, foreground +-2.5 art px, tiles under 0.2 art px. |
| 5 | Should sway have an off switch in Settings (motion comfort)? | (a) yes, one toggle (a new Lang key in en and vi). (b) no. | (a). |
| 6 | Shake: world only, as shown in frame 19? | (a) world only, HUD steady. (b) world and HUD, as today. | (a). |
| 7 | HUD resolution: 640x360 or 1280x720? | (a) keep the 640x360 HUD, shown at whole multiples (as in the mockup): no new art, fonts or layout. (b) redraw the HUD at 1280x720: every UI sprite, font size and `layout.json` box changes. | (a). 5b-05 still owes its written recommendation after seeing it in Unity. |
| 8 | Are the five depth presets accepted as starting values for the prototype? | (a) accept as they are. (b) widen the spread between depths (they differ little on the bench). (c) one setting for all depths, only the tint changes. | (a): safe in every frame (no blown-out board, vignette stays off the tiles); widen in Unity, where HDR bloom behaves differently. |
| 9 | The window view is the depth's identity (5a), and here it is blurred twice (far pre-blur + tilt-shift, about sigma 2 art px): at depth 3 the lattice is gone and the view is a soft shape. (`Far`, `Far radius`, `Strength`) | (a) accept: soft background is the HD-2D look. (b) soften less: far radius 1, tilt-shift strength 3 (the lattice is faintly back). (c) no pre-blur on the window view, wall still blurred. | (b) as the starting value, checked again in the prototype. |
| 10 | Tilt-shift band: sharp from art y 145 to 235, full blur at the top 35 rows and under the cards. (`Band centre`, `Band width`) | (a) accept. (b) wider band (wall candles and shelves sharper). (c) weaker strength overall. | (a), with the strength following question 9. |
| 11 | World resolution on larger displays | (a) world rendered at the whole display scale (2x at 720p, 3x at 1080p), as in the mockup. (b) fixed 1280x720, stretched 1.5x at 1080p. | (a): a fractional stretch smears pixel art; the cost is measured in 5b-06. |
