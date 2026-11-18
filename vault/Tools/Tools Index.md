---
generated: true
type: moc
tags: [moc/tools]
---

# Tools
Scripts outside Unity (art, fx, web tests, rule tests, this vault).

- `tools/art/blur.mjs` — Gaussian blur on premultiplied alpha, so transparent pixels never darken the edge.
- `tools/art/export-depth.mjs` — HD-2D DepthTest layer art (task 5b-04): pre-blurred far layers, two near foreground corners, two mist layers.
- `tools/art/export-window.mjs` — Writes only the window views (Background/window_d1..d5.png, Fx/window_drip.png) and a contact sheet; no layout.json, no other sprites.
- `tools/art/export.mjs` — Writes every sprite the game uses into the Unity project, plus Editor/layout.json and a contact sheet.
- `tools/art/icons.mjs` — The material and catalyst icon set (24x24), drawn in code.
- `tools/art/painter.mjs` — Pixel-art painter: palette ramps, the Spr sprite buffer and an RGBA Canvas. No dependencies.
- `tools/art/png.mjs` — Minimal PNG writer: 8-bit RGBA, no dependencies beyond node:zlib.
- `tools/art/scene.mjs` — The workshop and cutscene scenery, as unlit albedo layers. Lighting is added in Unity.
- `tools/art/ui.mjs` — HUD and menu pieces, each drawn at the exact size it is shown (no stretching).
- `tools/ci/check_repo.py` — 
- `tools/ci/run.sh` — The one rule set for CI. The workflow and a developer both call this, so "green locally" means "green in CI".
- `tools/fx/export.sh` — Exports the pixelfx effects the game plays as sprite sheets (PNG + JSON) into the Unity project.
- `tools/fx/shrink.mjs` — Shrinks exported pixelfx sheets in place (called by export.sh after the copy):
- `tools/lint/tests/test_unity_lint.py` — 
- `tools/lint/unity_lint.py` — 
- `tools/setup/protect-branch.sh` — Bật branch protection: bắt buộc PR + status check "ci-gate" xanh (gom lint, repo-guard, core-tests) + nhánh phải cập nhật,
- `tools/vault/build_vault.py` — 
- `tools/vault/q.py` — 
- `tools/web/autoplay.mjs` — Lets the scripted player play a development web build in headless Chrome and photographs it (no dependencies; Node 22+).
- `tools/web/depthmatrix.sh` — 5b-06: the frame-time matrix. Needs the release builds served: Main on :8080 (Build/web), DepthTest on :8081 (Build/web-depth).
- `tools/web/depthprobe.mjs` — 5b-06 probe for the HD-2D prototype build (and Main, for comparison): frame time, renderer name, console errors, frame sequences.
- `tools/web/depthshimmer.py` — 
- `tools/web/depthshimmer.sh` — 5b-06: frame sequences of the DepthTest build for the shimmer check, at 2x (1280x720) and 3x (1920x1080) on the GPU.
- `tools/web/descents.mjs` — Photographs each Descent cutscene (5a-14: the depth intro line and the window view) of a development build in headless Chrome.
- `tools/web/loadsmoke.mjs` — Load smoke for a RELEASE web build (the release-gate's size-checked build has none of the dev-only hooks webtest.mjs needs).
- `tools/web/mainhd2d.mjs` — 5b-07b: WebGL player checks of the HD-2D bench in Main (development build from WebBuild.BuildDev, dev keys and hooks).
- `tools/web/rotate.mjs` — Renders the WebGL template (macros replaced by dummy values, a stub Unity loader) in headless Chrome and checks the rotate-device
- `tools/web/rotate_sync.mjs` — Fails when the rotate-device table in the WebGL template differs from ui.rotate.title / ui.rotate.body in lang/en.json and lang/vi.json
- `tools/web/webtest.mjs` — Drives the web build in headless Chrome over the DevTools protocol (no dependencies; Node 22+).
