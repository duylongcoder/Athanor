# Phase 6 baseline (2026-11-18, branch feature/phase6-unity-refactor)

| Check | Command | Result |
|---|---|---|
| CI | `tools/ci/run.sh all` | exit 0; CoreTests 331 passed / 0 failed / 6 explicit skipped (ci.txt) |
| EditMode | `unity test ./Athanor --mode EditMode` | exit 0; total 458, passed 452, failed 0, skipped 6; no failures (editmode.txt) |
| BalanceMatrix | `DOTNET_ROLL_FORWARD=Major dotnet test tools/CoreTests --filter "FullyQualifiedName~BalanceMatrix" -l "console;verbosity=detailed"` | 2 tests passed, numbers captured (BalanceMatrix.txt). Fresh: wins 132/200 (66%), avg depth 3,76 |
| EventReport | same, `~EventReport` | 1 passed, numbers captured |
| RoleReport | same, `~RoleReport` | 1 passed, numbers captured |
| BalanceReport | same, `~BalanceReport` | 1 passed, numbers captured (informed: wins 132/200) |
| Web build | `unity build Athanor --target WebGL --execute-method WebBuild.BuildDev --allow-dirty-build` | exit 0 |
| webtest | `node tools/web/webtest.mjs http://localhost:8080/ .../web` (server: `python3 -m http.server 8080 -d Athanor/Build/web-dev`) | exit 0; 129 screenshots, 0 console errors (web-log.txt, web/) |
| mainhd2d | `node tools/web/mainhd2d.mjs http://localhost:8080/ .../hd2d 640x360 hud,tiles` | exit 1; checks 47/54; sway 27/27, shake 20/27; 0 console errors (hd2d-log.txt, hd2d/) |
| Release size | find ... Athanor/Build/web/Build + index.html | 10917289 bytes |

## mainhd2d baseline failures (pre-existing, not fixed)
7 shake points FAIL "pre-inside=false post-inside=false drift=1.3-5.7px": tile2 p3, tile3 p0, tile4 p0, tile5 p3, tile6 p3, tile7 p0, tile8 p0. The refactor must not make this worse (target: same 47/54, or better).

## Notes
- The dev build modified the tracked file `Athanor/Assets/Settings/Build Profiles/Web - Desktop - Development.asset` (Unity side effect, not hand-edited). Left for the lead to review/revert.
