# Athanor

A small run-based alchemy puzzle for the browser: lay fantasy materials on a 3x3 bench, Ignite, and see what reacts. Nothing is
labelled, so every recipe has to be found by trying; five depths, each asking for more potency, end in one substance the game never
names in advance. Built with Unity 6000.6.7f1 (Universal 2D, Pixel Perfect Camera, 640x360).

## Build and run

Open the project with `unity open Athanor` (or add the `Athanor` folder in Unity Hub), open `Assets/Scenes/Main.unity` and press Play.

    unity test Athanor --mode EditMode                # rules and screens (editor closed)
    unity test Athanor --mode PlayMode                # seeded autoplay, tutorial and manager boot
    tools/ci/run.sh all                               # lint, repo guard, rotate text (what CI runs besides Unity)
    tools/ci/run.sh unity                             # EditMode + PlayMode in one go; run both before pushing

    unity run Athanor -- -executeMethod BalanceReports.RunBatch -report balance-matrix -out /tmp/m.txt   # balance report (menu Athanor > Balance)

    unity build Athanor --target WebGL --execute-method WebBuild.Build --allow-dirty-build        # release, writes Athanor/Build/web
    unity build Athanor --target WebGL --execute-method WebBuild.BuildQuick --allow-dirty-build   # quick check
    unity build Athanor --target WebGL --execute-method WebBuild.BuildDev --allow-dirty-build     # quick, F9 autoplay, writes Build/web-dev

    python3 -m http.server 8080 -d Athanor/Build/web                       # serve a build
    node tools/web/webtest.mjs http://127.0.0.1:8080/ /tmp/athanor-web     # headless Chrome check (Chrome and Node 22+)

If `unity` says the project is not a Unity project, give it the path (`./Athanor`). The editor must be closed for batch commands.

## Everything else

The game rules, architecture, data, decisions in force, CI rules, build figures, carry-overs and lore are in
[docs/ATHANOR.md](docs/ATHANOR.md). Credits: [CREDITS.md](CREDITS.md). Agents and house rules: `CLAUDE.md`.
