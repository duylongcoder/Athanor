# Athanor

A small run-based alchemy puzzle for the browser: lay fantasy materials on a 3x3 bench, Ignite, and see what reacts. Nothing is
labelled, so every recipe has to be found by trying; five depths, each asking for more potency, end in one substance the game never
names in advance. Built with Unity 6000.6.7f1 (Universal 2D, Pixel Perfect Camera, 640x360).

## Build and run

Open the project with `unity open Athanor` (or add the `Athanor` folder in Unity Hub), open `Assets/Scenes/Main.unity` and press Play.

    dotnet test tools/CoreTests                       # the rules, no Unity
    unity test Athanor --mode EditMode                # the same plus the Unity tests (editor closed)
    tools/ci/run.sh all                               # what CI runs; run it before pushing

    unity build Athanor --target WebGL --execute-method WebBuild.Build --allow-dirty-build        # release, writes Athanor/Build/web
    unity build Athanor --target WebGL --execute-method WebBuild.BuildQuick --allow-dirty-build   # quick check
    unity build Athanor --target WebGL --execute-method WebBuild.BuildDev --allow-dirty-build     # quick, F9 autoplay, writes Build/web-dev

    python3 -m http.server 8080 -d Athanor/Build/web                       # serve a build
    node tools/web/webtest.mjs http://127.0.0.1:8080/ /tmp/athanor-web     # headless Chrome check (Chrome and Node 22+)

If `unity` says the project is not a Unity project, give it the path (`./Athanor`). The editor must be closed for batch commands.

## Everything else

The game rules, architecture, data, decisions in force, CI rules, build figures, carry-overs and lore are in
[docs/ATHANOR.md](docs/ATHANOR.md). Credits: [CREDITS.md](CREDITS.md). Agents and house rules: `CLAUDE.md`.
