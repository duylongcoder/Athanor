# Phase 4: depth layers and "2.5D" (feasibility)

Status: **Research and a browser mockup, for owner review.** No game code, scene, asset or setting was changed.
Checked 2026-11-13 against Unity 6000.6.7f1, URP with the 2D Renderer (`Assets/Settings/Renderer2D.asset`), one
orthographic camera with a PixelPerfectCamera (assets PPU 1, 640x360, grid snapping = upscale render texture), WebGL.

The owner asked: "Unity has no 2.5D format like we wanted at first. If possible, experiment with 3D using many depth
layers. Make a mockup first to review the feasibility."

## What "2.5D" meant at the start

The design spec (`docs/superpowers/specs/2026-11-11-athanor-design.md`) asks for "striking pixel art with a 2.5D retro
look": a 640x360 picture at integer scale with point filtering, an oak table and stone slab drawn in one-point
perspective, raised tiles with visible front faces, warm candle pools, a cool moonlight shaft and bloom. Today that
depth is all painted: the scene is flat sprites sorted by `sortingOrder`, lit by Light2D. Nothing moves in depth.

The real layers, from `Editor/SceneBuilder.cs` (`BuildWorkshop`) and the bench scripts:

| Layer | Source | Order | Notes |
|---|---|---|---|
| Workshop wall (bricks, shelves, dark floor) | `workshop_wall.png` 640x360 | 0 | lit |
| Window (sky, moon, lattice) | `window.png` at (280,16) | 1 | unlit |
| Table and slab (tiles are painted into it) | `workshop_table.png` at (102,132) | 2 | lit; rows 0-122 are the top, 123-144 the front apron |
| Wall candles / table candles | `candle.png` + 8-frame `flame_small_*` + glow + Light2D | 3-4 / 5-6 | 5 candles from `layout.json` |
| Moonbeam | Light2D sprite light with the `moon_shaft.png` cookie | - | intensity from `GameFlow.Mood(depth)` |
| Dust | `Dust.cs`, 46 one-pixel motes in the beam | 8 | `Px.Snap` each frame |
| Tiles: face glow, ring, shadow, halo, material icon, tile light | `TileView.cs` | 9-14+ | `Clickable`-style hit tests in world px |
| Rack, hand of cards | `BenchView.cs`, `CardView.cs` | 100+ | world sprites, not a Canvas |
| HUD panels, note, journal, Ignite | `HudView.cs` | O+ | world sprites in the same camera |

`GameFlow.Mood(depth)` sets the global ambient colour (blue, teal, violet, red, pale gold) and the moonbeam intensity
(0.62 down to 0.3).

## The mockup

`docs/mockup/depth/index.html` (open it straight from Finder; it needs internet once for three.js 0.160.0 from
jsDelivr). `build.sh` packs the real PNGs from `Athanor/Assets/Art`, the fonts from `docs/mockup/f` and
`Resources/layout.json` into `assets.js`, because a `file://` page cannot upload its own image files to WebGL.

Everything renders into a 640x360 WebGL canvas that CSS upscales with nearest-neighbour at a whole multiple. The HUD
(depth panel, catalysts, rack, cards, note, journal, Ignite) is a separate flat 640x360 canvas on top, drawn from the
real UI PNGs, and it never moves. A panel on the right switches variants, toggles every layer, moves each layer in
depth, and sets parallax, camera travel, orbit, dolly, table tilt, mood depth 1-5, depth fog, fog sheets and the
moonbeam volume live.

Layers in the mockup, far to near: wall (-400), window (-398), wall candles (-392), moonbeam volume (36 slices from the
window down to -150), back fog sheet (-300), table (back edge about -199, front edge -60, one strip per pixel row),
materials and table candles standing on their table row, dust in the beam (spread between the wall and -70), front fog
sheet (+40), large soft motes right in front of the camera (+260). Units are art pixels; z = 0 is the plane that maps
1:1 to the screen.

**One trick makes all of this cheap: projection-preserving placement.** A plane at depth z is scaled by
`(D - z) / D` (D = 600, the rest camera's distance to z = 0) and its position by the same factor. From the rest
camera, every layer lands exactly on its 2D art pixels, so the rest frame is identical to today's game. Depth only
shows when the camera moves. The table's rows get depths from the drawn perspective: it is 343 px wide at the back and
436 px at the front, so the back edge is 1.27x further away, with rows linear in 1/distance (as a real floor is).

### Variants

- **A. Layered parallax 2D.** Orthographic camera. Each layer is a flat sprite moved by
  `-shift * strength * (D/d - D/d_bench)` and rounded to whole pixels. The bench row is the anchor: it never moves,
  so every click lands where it does today. The table is cut into 1 px rows, each with its own offset (SNES-style line
  scroll), which reads as a tilted surface. Lights are planar, as Light2D's are.
- **B. Perspective 2.5D.** Perspective camera (fov 33.4 degrees), the same planes at real depths, the camera orbiting
  the middle tile row so the bench stays centred. Depth fog per layer (distance from the camera), and candle light
  falls off in depth as well as across the screen.
- **C. Descent.** Variant B plus real 3D geometry under the workshop: 30 steps (risers and treads) and side walls
  textured with bricks cut from the wall PNG, and three torches. The camera dives down the stair into fog. At the
  fogged midpoint the mood switches to the next depth (as `Mood()` does today behind the black screen), and the camera
  settles back onto the bench from above.

### What I saw (headless Chrome screenshots, no console errors in any variant)

- At rest, A and B match the current bench exactly: same art, same pixel grid, HUD crisp.
- A, posed: the wall and shelves slide against the table, the line-scrolled table reads as tilted, the bench and its
  materials stay put. Every edge stays on the pixel grid. At first 1-px gaps opened between table rows whose rounded
  offsets differed. The fix is for each row to carry one extra row above and below, with the nearer row drawn on top.
  In Unity this would be a line-scroll shader on the table sprite, not 145 sprites.
- B, posed: the strongest sense of depth. Materials stand on their tiles, the window and wall shift behind the table.
  The cost is resampling. A plane at a non-integer scale and a sub-pixel position is resampled by nearest-neighbour,
  so some pixel rows and columns double or drop. Hard to see in a still frame; it shimmers while the camera moves.
  It is fine for a slow sway of a few px, and visible with large moves. Table strips needed the same overlap fix.
- B with depth 4 mood, strong fog and the window pushed to -1600: the back wall sinks into the red-tinted fog, the
  table stays warm. This is the "the deeper, the thicker the air" idea working with no fullscreen pass.
- C: the stair reads as a real stairwell in the same pixel style (bricks from the wall PNG), the fog-out hides the
  mood swap. It also shows the limits: real 3D geometry needs a depth buffer (painter's sorting by plane centre hid
  the torches until the stair used depth testing), and the stair needs its own art to look good.

## Doing it in Unity: options

### 1. Keep the 2D Renderer, orthographic camera, add a parallax script (variant A)

- A `ParallaxLayer` component on each layer root (wall, window, sconces, dust, fog sheets, near motes). Each frame it
  sets `localPosition = Px.Snap(home + camShift * factor)`. The camera itself does not move. The bench, tiles, rack,
  cards and HUD have factor 0.
- The table: a small Sprite-Lit Shader Graph that offsets `uv.x` (and optionally `uv.y`) by a per-row amount
  (`rowFactor = f(row)` matching the drawn perspective), quantised to whole texels. One sprite, one material.
- Everything keeps working: PixelPerfectCamera, Light2D (lights on a moving layer move with it), Sprite-Lit-Default,
  bloom, `sortingOrder`, `Px.Snap`, `PointerInput.ScreenToWorld`, `Clickable.Contains` (bench is the anchor), WebGL
  cost (a few transforms per frame).
- Limit: depth is a slide, not a rotation. Objects do not change size or reveal sides. Lights are planar.
- Effort: **1.5 to 2 days**, including the line-scroll shader, the fog sheets and an option to turn it off.

### 2. Keep the 2D Renderer, perspective world camera into a 640x360 RenderTexture (variant B)

- The world camera turns perspective and renders into a 640x360 RenderTexture with `filterMode = Point`, no MSAA,
  with post-processing (bloom) on. A second, orthographic UI camera with the PixelPerfectCamera draws that texture as
  a full-screen sprite at the lowest order, then the HUD on top. This keeps the picture pixel art: the 3D is drawn at
  the game's resolution and only ever upscaled by whole multiples.
- **PixelPerfectCamera only works with an orthographic camera.** It stays on the UI camera only. The world camera
  gets crispness from the low-res target, not from snapping.
- Sprites stay `SpriteRenderer`s with Sprite-Lit-Default. Light2D still lights them, but 2D lights stay 2D: they are
  drawn into screen-space light textures, so a candle lights whatever is under it on screen whatever its z. Distance
  falloff in depth (as in the mockup) needs a custom Sprite-Lit Shader Graph, or simply placing lights well.
  The 2D Renderer does accept perspective cameras. Verify the light textures and the moonbeam cookie in the test scene
  before building anything on them.
- Placement: put each layer at its depth and scale it by `(D - z) / D` (the rest frame stays identical). For the
  table, use either many row strips (as in the mockup) or one quad tilted to the matching angle, with art redrawn
  for it.
- Sorting: keep explicit `sortingOrder` per layer (the 2D Renderer sorts sorting layer, then order, then distance), so
  nothing changes order when the camera moves. Real 3D meshes (a stair) need ZWrite/ZTest and an opaque or
  alpha-clipped material; transparent sprites never write depth.
- What breaks, and the fix:
  - `PointerInput.ScreenToWorld` assumes the art is the screen. The bench needs a ray:
    `worldCam.ViewportPointToRay(uiPoint / 640x360)` intersected with the bench plane, then the existing
    `Clickable.Contains` / `TileView.Contains` in that plane's coordinates. HUD clickables stay as they are on the UI
    camera. 24 `Make.Click` sites, most of them HUD.
  - `Px.Snap` (15 uses in 9 files) means nothing for objects the perspective camera sees off-axis. Keep it for the HUD,
    and drop it for world motion, or snap in the reference plane only.
  - HUD, rack, cards, popups, tooltips, `ScreenFx` (fade/flash) must move to the UI camera's layer. Today everything is
    one camera with no Canvas, so this is a layer/culling-mask split. `ScreenFx` shake moves the camera transform: decide
    whether shake moves the world only (likely nicer) or both.
  - Cutscenes (`ReactionCutscene`, `SimpleCutscene`) are full-frame sprite scenes: they can stay flat on the world
    camera at z = 0 (identical at rest) or get the same depth treatment later.
  - Light2D cookie for the moonbeam is a screen-shaped sprite light. Under perspective it projects from its own plane.
    Keep it at the wall depth or treat it as a slab of slices like the mockup.
- WebGL cost: one extra 640x360 target and a blit, a few hundred more draw calls if the table is cut into rows
  (prefer a tilted quad or a strip shader), same light count. Cheap.
- Effort: **2 to 3 days** for a test scene that answers the open questions (Light2D + perspective + RT + bloom +
  ray-based clicks), then **4 to 6 days** to bring the main scene, input, HUD split and cutscenes over.

### 3. Universal (3D) Renderer, perspective camera, sprite quads, 3D lights

- Sprites become quads with a URP Lit / Simple Lit material (or `SpriteRenderer` with a 3D-lit Shader Graph), lit by
  point and spot lights. `Sprite-Lit-Default` and **Light2D only work with the 2D Renderer**, so every light is
  rebuilt: 5 candles, tile glows, cutscene fire, the moonbeam (a spot light with a cookie), 11 `Make.Light` /
  `Light2D` sites in 8 files, and `GameFlow.Mood`.
- Gains: real 3D lighting and optional shadows; the depth buffer that **AERO** needs (if the quads write depth:
  alpha-clipped opaque). Real distance fog for free.
- Mixing 3D lights with sprites: URP Lit on a quad works, but pixel-art sprites lit by smooth per-pixel 3D lights lose
  the hand-placed look unless the lighting is quantised (a ramp in a custom shader). Camera stacking cannot mix a 2D
  Renderer camera with a Universal Renderer camera in one stack, as far as I know. The HUD would then also move to
  the 3D renderer (an overlay camera, unlit sprites).
- WebGL cost: forward lighting with per-object light limits, shadows and a raymarched volumetric fog are the most
  expensive things we could add to a WebGL build on integrated GPUs.
- Effort: **2 to 3 weeks**, with a visible risk to the approved look.

## Fog, and the AERO question

`phase-4-packages.md` found AERO unusable with the 2D Renderer and proposed lit sprite fog sheets between the room and
the bench instead. The mockup implements that proposal in both A and B: pixel mist (4 alpha steps, Bayer dither,
scrolled in whole texels) on two sheets, lit by the same candle and moon lights, tinted from the depth's ambient. B adds
**per-layer depth fog**: each sprite is mixed toward the mood's fog colour by its distance from the camera. In Unity
that is either a `_Fog` value per layer set by a small script (one material property block per layer root), or the
fog term inside the custom Sprite-Lit Shader Graph using view depth.

Both work with options 1 and 2 and need no fullscreen pass, so the HUD is never fogged. Only option 3 would make AERO
usable, and it is not worth the cost for this.

## Recommendation

**Prototype option 2 in a test scene, and ship option 1 if the prototype disappoints.** Option 2 keeps the 2D Renderer,
Light2D, Sprite-Lit, bloom and all the art, and gives the real "planes in depth" look in the mockup's variant B. The
640x360 render target keeps the result pixel art, and the HUD stays crisp on its own camera. Option 1 is the safe
version of the same idea at a third of the cost. Option 3 (the 3D renderer, for AERO and 3D lights) costs weeks and
puts the approved look at risk. I would not do it.

Camera motion should stay small (a slow sway and a gentle follow of a few px), because larger moves make perspective
resampling shimmer visible. Save big camera moves for the descent, which is fogged anyway.

## Prototype plan (ordered; no change to the main scene)

1. **Test scene** `Assets/Scenes/DepthTest.unity`, built by a new editor menu item (as `SceneBuilder` builds the main
   scene), using copies of the workshop layers only: wall, window, table, 5 candles with Light2D, the moonbeam, dust.
2. **Camera split**: world camera (perspective, fov from `2*atan(180/D)`, D = 600) into a 640x360 point-filtered
   RenderTexture with bloom; UI camera (orthographic, PixelPerfectCamera) shows the RT plus a static copy of the HUD
   sprites. Check: the rest frame is identical to the main scene's bench (screenshot diff).
3. **Layer placement script** `DepthLayer` (z, scale by `(D - z)/D`) and a `CameraSway` (mouse follow + idle sway,
   orbiting the bench row). Check Light2D, the moonbeam cookie and bloom under perspective. This is the main unknown.
4. **Table**: strips vs one tilted quad vs the line-scroll shader; pick the one with no seams and the fewest draws.
5. **Clicks**: ray from the RT pixel to the bench plane; hover and click on the nine tiles in the test scene.
6. **Fog**: two lit sprite fog sheets plus the per-layer fog term, driven by a copy of the `Mood` arrays.
7. **WebGL build** of the test scene; measure frame time on a laptop GPU, and check the shimmer at 2x and 3x scale.
8. **Owner review.** If the shimmer or the light behaviour is not acceptable, build option 1 (parallax script +
   line-scroll table) in the same test scene instead, about 1.5 days.
9. Only after approval: bring it into the main scene (HUD/UI camera split, `PointerInput` ray, cutscenes), then the
   descent (step 10).
10. **Descent**: the stair needs its own art (tools/art) and depth-tested materials. Use the fogged midpoint to swap
    the mood, as the mockup's variant C does.
