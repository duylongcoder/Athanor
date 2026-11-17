# 5b-05 DepthTest prototype: evidence and findings

Scene `Athanor/Assets/Scenes/DepthTest.unity`, built by **Athanor > Build Depth Test Scene** (`DepthTestBuilder.Build`).
`Main.unity` and `GameFlow.cs` untouched. Option 2 of `docs/devlogs/phase-4-depth.md` (perspective world camera into a
render texture, orthographic UI camera on top), with the G2 values of `phase-5-prep.md` *Gate G2*.

## How this evidence was made

| Set | Command | What it can show |
|---|---|---|
| `01..37*.png`, `report.txt` | `unity run Athanor -- -executeMethod DepthTestEvidence.CaptureBatch` | geometry, placement, sprites, fog, tilt-shift, sway, shake, presets, bloom/vignette |
| `webgl/title.png`, `webgl/console.txt` | `unity build Athanor --target WebGL --execute-method WebBuild.BuildDepthTest`, served, then `node tools/web/loadsmoke.mjs <url> webgl 8` | **lights** (see below), load, console |

**Editor captures do not draw point or sprite Light2D.** In batch mode, `Camera.Render` into a texture draws the global
light only: turning the moonbeam or the candle lights off changes 0.00 luma in *both* `Main.unity` and `DepthTest`
(`report.txt`, "Light under the perspective camera"), and the editor bench is darker than the 5a web build
(`5a-16/autoplay/s040.png`). So the editor set says nothing about lights, and the rest-frame diff compares two frames
without the same lights. The lights are judged on the WebGL player only.

## Acceptance

| Brief item | Result |
|---|---|
| Rest frame matches the bench (screenshot diff) | **Pass (geometry).** Effects off, no sway, 1x, post off: 112 of 230,400 px differ (0.049 %), max 6 levels. With bloom + vignette: 1.11 %, max 67, in the candle halos. 2x against the 1x bench upscaled: 2.7 % (post off), the resampling of the perspective camera at 2x. Lights excluded (above). |
| Light2D, cookie and bloom under perspective | **Pass, in the WebGL player** (`webgl/title.png`): the moonbeam shaft shows in the window, the table candles light the table and tiles, bloom on the flames. |
| Clicks on the 9 tiles by ray | **Pass.** `DepthClickTests`: centre of each tile at rest and at the four sway extremes, the camera-shown centre under sway and shake (within 0.02 art px), `WorldRay` equals Unity's own ray. |
| HUD-resolution recommendation | Below. |
| Option 1 fallback | Below. |
| EditMode 0 fail | **443 / 437 pass / 0 fail / 6 skipped.** |
| WebGL load | 2.4 s, 139 messages, **0 console errors**. |

Also measured (`report.txt`): the tilt-shift pass changes art rows 0..121 and 257..359, **none inside the sharp band
145..235** (G2 #10). Sway: wall rows 20..60 move 0.57 mean; the anchor rows 186..190 change 5.4 mean because the front fog
(z = +40, in front of the anchor) slides over them, as intended. The anchor geometry itself is pinned by the click tests.

## Bugs found and fixed while making this evidence

1. **Fog sheets covered the whole frame in grey blocks** (the first `11..15` captures). `DepthTestBuilder.FogSheet`
   starts the renderer from the 4 px `dot` sprite; swapping to the fog picture and to Tiled mode rescaled the transform
   by the size ratio (front sheet 80 x 27.5), and `DepthLayer` saved that as the rest scale. Fix: scale reset to 1 before
   the rest state is taken. Bounds now ±388 x ±51 units (front) instead of ±31,061 x ±1,412.
2. **Tiles saved in the scene broke when it reopened**: `TileView` kept its child renderers and face rectangle in
   non-serialized fields, so `Show` threw and `Contains` was always false (Play mode on `DepthTest` would have thrown
   every frame). Fix: `[SerializeField, HideInInspector]` on them; `Main` builds tiles at runtime and is unchanged.
   Regression test `Show_TileReloadedFromAScene_ShowsTheMaterialAndTakesClicks`, seen failing first.
3. `FogSheet.Offset` floored negative drifts (front sheet one texel ahead); now truncates, as its test asked.
4. The evidence script lost its main-scene captures when it opened the depth scene (unreferenced textures unloaded); now
   `HideAndDontSave`.

## HUD resolution: recommendation

**Keep the HUD at 640x360, shown at whole multiples (G2 #7 stands).** At 2x (`06`, `11..15`) the HUD is pixel-identical
in style to today's and crisp next to the 2x world. One thing to fix before 5b-07: in the WebGL player at **1x**
(a 1280x633 headless viewport, so the windowbox fell to 1x) the small HUD text (`POTENCY`, `CATALYSTS`, 8 px Tiny) is
garbled, while the 2x editor capture of the same HUD is clean. The cause is not found yet; the UI camera here has a
PixelPerfectCamera with no upscale texture and `GridSnapping.None`, unlike `Main`. 5b-07 should check `Main`'s HUD at 1x
after the camera split and give the UI camera the same pixel-perfect settings as today's camera.
1280x720 for the HUD is not recommended: it doubles every HUD asset or blurs them, and buys nothing the 2x world needs.

## Option 1 fallback (if G3 rejects the shimmer or the light behaviour)

From `phase-4-depth.md` option 1, built in the same test scene, about 1.5 days: one orthographic camera, no render
texture. A `ParallaxLayer` per layer root moves it by `Px.Snap(home + camShift * factor)` (bench, tiles, HUD at factor 0);
the table gets a line-scroll Sprite-Lit shader (per-row `uv.x` offset in whole texels) instead of the `DepthTable` mesh.
Reused unchanged from this prototype: the pre-blurred far and near art, the fog sheets and `FogSheet`, `DepthMood`
presets, `DepthSwitch`, `CameraSway` (its output becomes `camShift`), the tilt-shift pass (on the single camera). Dropped:
`DepthRig`'s two cameras, the ray click (clicks stay `PointerInput.ScreenToWorld`, the bench being the anchor). Loses:
real size change with depth; lights stay planar (as today).

## Open for 5b-06 / the owner

- The URP asset now lists a second renderer (`DepthRenderer2D`, index 1) for the world camera's tilt-shift feature.
  `Main` still uses index 0, but the renderer ships in the main build too; 5b-06 should count it.
- Fog grain shows as dither blocks over the tiles at 2x (`11`, `14`): G2 #2/#3 said judge it in motion at G3.
- `Athanor/Build/web-depth/` is build output (9 MB): not to be committed.
