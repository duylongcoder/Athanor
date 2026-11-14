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
- A run starts by picking one of three **aspects**, and you pick another at depths 3 and 5. An aspect changes the
  bench until the run ends: a tile that doubles its reaction, two tiles joined by a bridge, a shut tile that pays
  an extra trial. Its tile carries a small gem; the aspects you hold sit under the depth panel.
- Every depth may open with an **event**: a find, a gamble, a shrine, a hazard. Each option says what it costs
  and what it may give, and what it does lasts that depth only.
- **Goals** (on the title screen) run across all your runs: reach a depth, ignite so many times, fill the journal.
  Each one unlocks a new catalyst or a new starting material, named only by its rarity until you earn it.
  Starting materials you have won come one to a hand, on top of the usual five, and open new recipes.
- **Redraws** (top right) swap your hand. The **Journal** keeps every reaction you have found, across runs. A hint
  stays on the page of the material it is about until you have found everything that material does.
- A click skips a cutscene. In the editor, **F9** lets a scripted player take over.

## Layout

| Path | What |
|---|---|
| `Athanor/` | The Unity project |
| `Athanor/Assets/Scripts/Core/` | Rules with no Unity dependency: content, satchel, Ignite, run state, profile and goals, aspects and events, scripted player |
| `Athanor/Assets/Scripts/Game/` | Everything on screen: bench, HUD, cutscenes, journal, menus, flow |
| `Athanor/Assets/Resources/content.json` | Materials, recipes, catalysts, depth targets, goals, aspects, events |
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
    # in Unity: Athanor > Build Scene, or with the editor closed:
    unity run Athanor -- -executeMethod SceneBuilder.BuildBatch

Export the pixelfx effect sheets (optional; needs the `pixelfx` clone at the repository root, or `PIXELFX=/path`):

    tools/fx/export.sh
    # in Unity: Athanor > Import Effect Sheets

Run the tests:

    dotnet test tools/CoreTests                       # the rules
    unity test Athanor --mode EditMode                # the same tests plus the save tests, inside Unity (editor closed)

Build for the browser (writes `Athanor/Build/web/`; close the editor first):

    unity build Athanor --target WebGL --execute-method WebBuild.Build --allow-dirty-build        # release, slow
    unity build Athanor --target WebGL --execute-method WebBuild.BuildQuick --allow-dirty-build   # quick check
    unity build Athanor --target WebGL --execute-method WebBuild.BuildDev --allow-dirty-build     # quick, with F9 autoplay; writes Build/web-dev

Serve it locally:

    python3 -m http.server 8080 -d Athanor/Build/web

Check a build in headless Chrome (needs `google-chrome` and Node 22+; screenshots and the console log go to the folder you name):

    node tools/web/webtest.mjs http://127.0.0.1:8080/ /tmp/athanor-web

It takes an aspect and the first depth's event, plays a first trial with real mouse events, resizes the window,
reloads to see the journal and the goals kept, then damages the saved journal and profile and reloads to see the
game start with an empty journal and a fresh profile.

To see a whole run without playing it, serve the autoplay build (`Athanor/Build/web-dev`) and let the scripted
player take the bench; it photographs the game every few seconds (goal toasts, big hands, the run summary, the
Goals page with finished goals):

    node tools/web/autoplay.mjs http://127.0.0.1:8081/ /tmp/athanor-auto 120 3

If `unity` says the project is not a Unity project, it has matched the name `Athanor` to another folder in the
Hub's list: give it the path (`unity test ./Athanor ...` or the full path).

The build is Gzip-compressed with Unity's built-in fallback, so any static host works. The page keeps the
640x360 picture at a whole-number scale, centred on black.

## Changing the game

- Recipes, materials, catalysts and targets are data: edit `content.json`. A material has a `reactivity` (1 to 10);
  a recipe is `{"items": [2 to 5 ids], "minReactivity": 0, "result": id}`. The tests check the rules still hold
  (a path to the last substance exists, tiers add up, the Reaction Index is spread across tiers), that a player
  who knows every recipe usually wins and that five-item combos stay rare.
- After changing content or scoring, read the balance report:
  `dotnet test tools/CoreTests --filter FullyQualifiedName~BalanceReport -l "console;verbosity=detailed"`.
  `--filter BalanceMatrix` shows the win rate by profile (fresh, half, full) with and without aspects and events;
  `--filter RoleReport` plays whole careers of two simulated players, one who knows every recipe and one who
  learns them from the journal (`ATHANOR_PLAYERS`, `ATHANOR_RUNS` set how many and how long);
  `--filter EventReport` puts each event in the informed player's way and shows what it costs. A hazard or a
  gamble met once, at any depth it may come to, must leave at least 70% of the wins a run with no event has.
- A goal is `{"id", "text", "stat", "target", "rewardKind": "starter" | "catalyst", "rewardRarity"}`. The content
  will not load when a reward bucket has more goals than locked items, so a goal can never pay out nothing.
- An aspect needs an icon `asp_<id>` and an event type an icon in `EVENT_ICONS`, both in `tools/art/icons.mjs`.
- A new material or catalyst needs an icon in `tools/art/icons.mjs` with the same id; the art export fails if one is missing.
- Sound is not included. `GameFlow` (reactions, descent, endings) and `ScreenFx` are the places to hook it in.

## Credits

See [CREDITS.md](CREDITS.md).

## Obsidian vault

Project knowledge (code structure, data design, content, events, agents, phases) lives in `vault/`; open the repository root in Obsidian. Regenerate with `python3 tools/vault/build_vault.py`, query with `python3 tools/vault/q.py`. See `vault/Workflow/Vault Guide.md`.

## Claude Code agents

The repo uses the Claude Code Game Studios framework plus twelve Athanor roles (`.claude/agents/`): see `CLAUDE.md` and `vault/Workflow/Agent Workflow.md`.
