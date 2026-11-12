# Athanor

A small run-based alchemy puzzle game. Lay fantasy materials on a 3x3 bench, Ignite, and see what reacts.
Nothing is labelled: every recipe has to be found by trying. Five depths, each asking for more potency,
ending in one substance the game never names in advance.

Built with Unity 6000.6.7f1 (Universal 2D, 2D lights, bloom, Pixel Perfect Camera) for the browser.

## Play

- Click a card (or a discovery on the rack), then click a tile. Or drag it there. Click a placed material to take it back.
- Neighbouring materials react when you press **Ignite**. A reaction uses up its materials, makes a new one and
  scores potency. A pair that does nothing leaves a hint.
- Every material has a **Reaction Index** (1 to 10, shown in the Journal once you have met it). The liveliest
  material on the bench that has something to react with goes first. It takes the biggest recipe its neighbours
  allow: up to four of them at once on the centre tile, three on an edge, two in a corner.
- The result appears in that material's tile and can react again with what is left around it. Each further
  reaction in the same Ignite scores more, and a combo of three or more scores more again.
- Everything you lay down is spent. Starting materials come back in later hands; a discovery you place is gone
  whether it reacts or not. Only what the chain made and left standing goes to your rack (12 places).
- Reach the depth's potency target before the trials run out, pick a catalyst, go deeper.
- **Redraws** (top right) swap your hand. The **Journal** keeps every reaction you have found, across runs. A hint
  stays on the page of the material it is about until you have found everything that material does.
- A click skips a cutscene. In the editor, **F9** lets a scripted player take over.

## Layout

| Path | What |
|---|---|
| `Athanor/` | The Unity project |
| `Athanor/Assets/Scripts/Core/` | Rules with no Unity dependency: content, satchel, Ignite, run state, scripted player |
| `Athanor/Assets/Scripts/Game/` | Everything on screen: bench, HUD, cutscenes, journal, menus, flow |
| `Athanor/Assets/Resources/content.json` | Materials, recipes, catalysts, depth targets |
| `Athanor/Assets/Editor/` | Scene, font, effect-sheet and web build tools (menu **Athanor**) |
| `tools/art/` | Generates every sprite |
| `tools/fx/` | Exports the optional pixelfx effect sheets |
| `tools/CoreTests/` | Runs the rule tests without Unity |
| `tools/web/` | Drives a web build in headless Chrome |
| `docs/` | The designs, the plans, the approved mockup and the devlogs (`docs/devlogs/`) |

## Common tasks

Open the project: `unity open Athanor` (or add the `Athanor` folder in Unity Hub), then open `Assets/Scenes/Main.unity` and press Play.

Regenerate art, then rebuild the scene and art library:

    node tools/art/export.mjs
    # in Unity: Athanor > Build Scene

Export the pixelfx effect sheets (optional; needs the `pixelfx` clone at the repository root, or `PIXELFX=/path`):

    tools/fx/export.sh
    # in Unity: Athanor > Import Effect Sheets

Run the tests:

    dotnet test tools/CoreTests                       # the rules
    unity test Athanor --mode EditMode                # the same tests plus the save tests, inside Unity (editor closed)

Build for the browser (writes `Athanor/Build/web/`; close the editor first):

    unity build Athanor --target WebGL --execute-method WebBuild.Build --allow-dirty-build        # release, slow
    unity build Athanor --target WebGL --execute-method WebBuild.BuildQuick --allow-dirty-build   # quick check

Serve it locally:

    python3 -m http.server 8080 -d Athanor/Build/web

Check a build in headless Chrome (needs `google-chrome` and Node 22+; screenshots and the console log go to the folder you name):

    node tools/web/webtest.mjs http://127.0.0.1:8080/ /tmp/athanor-web

It plays a first trial with real mouse events, resizes the window, reloads to see the journal kept, then damages
the saved journal and reloads to see the game start with an empty one.

The build is Gzip-compressed with Unity's built-in fallback, so any static host works. The page keeps the
640x360 picture at a whole-number scale, centred on black.

## Changing the game

- Recipes, materials, catalysts and targets are data: edit `content.json`. A material has a `reactivity` (1 to 10);
  a recipe is `{"items": [2 to 5 ids], "minReactivity": 0, "result": id}`. The tests check the rules still hold
  (a path to the last substance exists, tiers add up, the Reaction Index is spread across tiers), that a player
  who knows every recipe usually wins and that five-item combos stay rare.
- After changing content or scoring, read the balance report:
  `dotnet test tools/CoreTests --filter FullyQualifiedName~BalanceReport -l "console;verbosity=detailed"`.
- A new material needs an icon in `tools/art/icons.mjs` with the same id; the art export fails if one is missing.
- Sound is not included. `GameFlow` (reactions, descent, endings) and `ScreenFx` are the places to hook it in.

## Credits

See [CREDITS.md](CREDITS.md).
