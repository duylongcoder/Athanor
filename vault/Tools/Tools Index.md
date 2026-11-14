---
generated: true
type: moc
tags: [moc/tools]
---

# Tools
Scripts outside Unity (art, fx, web tests, rule tests, this vault).

- `tools/art/export.mjs` — Writes every sprite the game uses into the Unity project, plus Resources/layout.json and a contact sheet.
- `tools/art/icons.mjs` — The material and catalyst icon set (24x24), drawn in code.
- `tools/art/painter.mjs` — Pixel-art painter: palette ramps, the Spr sprite buffer and an RGBA Canvas. No dependencies.
- `tools/art/png.mjs` — Minimal PNG writer: 8-bit RGBA, no dependencies beyond node:zlib.
- `tools/art/scene.mjs` — The workshop and cutscene scenery, as unlit albedo layers. Lighting is added in Unity.
- `tools/art/ui.mjs` — HUD and menu pieces, each drawn at the exact size it is shown (no stretching).
- `tools/fx/export.sh` — Exports the pixelfx effects the game plays as sprite sheets (PNG + JSON) into the Unity project.
- `tools/vault/build_vault.py` — 
- `tools/vault/q.py` — 
- `tools/web/autoplay.mjs` — Lets the scripted player play a development web build in headless Chrome and photographs it (no dependencies; Node 22+).
- `tools/web/webtest.mjs` — Drives the web build in headless Chrome over the DevTools protocol (no dependencies; Node 22+).
