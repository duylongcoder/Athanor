# Phase 5a sign-off (5a-16 / delta3 D-4)

Run by `qa-engineer`, 2026-11-16. Read-only on code, content, lang, devlogs and plans. New evidence from this run is in
`production/qa/evidence/phase5/5a-16/`. Source of earlier results: `docs/devlogs/phase-5-prep.md` Log.

**Verdict: PASS for 5a, with the accepted gaps listed below. No failures. One index finding for the main session (row 16).**

| # | Item | Command or evidence path | Real count | Result |
|---:|---|---|---|---|
| 1 | CI all, incl. `rotate_sync` | `tools/ci/run.sh all` (re-run today) | exit 0; python 71 OK; unity-lint clean; check_repo 0 problems (BUILD-OUTPUT, CONTENT, ICONS, LANG, CORE-PURITY, META ok); rotate_sync OK; CoreTests 327/327, 0 fail | pass |
| 2 | Balance under set A = Phase 4 baseline (`phase-4-lore.md:248-257`) | `DOTNET_ROLL_FORWARD=Major dotnet test tools/CoreTests --filter "FullyQualifiedName~BalanceMatrix" -l "console;verbosity=detailed"` (re-run today) | 12/12 rows identical in wins, avg depth and lost-at: 132, 164, 134, 164, 166, 129, 173, 140, 163, 169, 144, 129 of 200 | pass |
| 3 | `InformedPlayer_WinsMostSeeds` 28/40 | same command with `\|FullyQualifiedName~InformedPlayer`: 4/4 pass; exact count from a scratch reflection runner over `RunStateTests.Autoplay`, seeds 1..40, same content.json (byte-equal) | **28/40** (test asserts 24..31) | pass |
| 4 | EditMode | `unity test Athanor --mode EditMode` (re-run today, 01:58); `evidence/phase5/5a-16/editmode-results.xml` | 426 total / 420 pass / 0 fail / 0 inconclusive / 6 skip | pass |
| 5 | webtest on the dev build | `evidence/phase5/5a-10/webtest.out`, `5a-10/webtest/console.txt` | WEBTEST_EXIT 0; 419 messages, 0 errors, 129 screenshots | pass |
| 6 | Release size vs ceiling | `ls -l Athanor/Build/web/Build Athanor/Build/web/index.html` (build of 2026-11-16 01:09-01:10); provenance `5a-17/build-WebGL-1794766139532.provenance.json` outcome success, exit 0 | data 3,235,095 + framework 74,558 + loader 48,779 + wasm 7,359,292 + index.html 6,561 = **10,724,285 B** <= 11,824,672 B; headroom 1,100,387 B | pass (note a) |
| 7 | `rotate.mjs` | `evidence/phase5/5a-17/rotate.txt`, 9 PNGs in `5a-17/rotate/` | 67 ok, 0 failures, rc=0. **Template only** (WebGLTemplates page with a stub loader), not the built page | pass |
| 8 | loadsmoke on the release build | `python3 -m http.server 8090` in `Athanor/Build/web`, `node tools/web/loadsmoke.mjs http://127.0.0.1:8090/ production/qa/evidence/phase5/5a-16/loadsmoke` (re-run today, server stopped after) ; `5a-16/loadsmoke.txt` | LOADSMOKE_EXIT 0; loaded 4.8 s; 61 messages, 0 errors; title.png shows the title screen (Begin, Journal, Goals, How to play, Settings, Watch intro, font credit line) | pass (note b) |
| 9 | 5a-14 descents EN/VI | `5a-14/en3.out`, `5a-14/vi3.out` (+ `en3/`, `vi3/`) | EN: d1 intro frames + descents 2,3,4, 0 errors, exit 0. VI: same, 0 errors, exit 0. d5 not photographed | pass; d5 accepted gap |
| 10 | 5a-10 frames | `evidence/phase5/5a-10/*.png` | 8 (intro, event, catalyst_choice, run_over x EN/VI) + 2 (`run_over_won_en/vi`) = 10 present. Looked at `run_over_won_vi.png`: "Gieo hạt ở tầng 4" fits its row | pass |
| 11 | 5a-07 in game | `evidence/phase5/5a-07/ingame/` | 4 frames: collect-burst, holy-pillar, implosion, poof | pass |
| 12 | Rarity frames | `evidence/phase5/5a-14/rarity/` | 24 frames: hand, offer, tooltip, Journal list, Journal detail Common + Uncommon; EN/VI; 1x/2x. Not shot: Legendary Journal (R4), Rare Journal detail, Uncommon tooltip (R4 widened) | accepted-gap (note c) |
| 13 | VI depth name within ~2 px | `5a-14/vi4/` frames, figures from the D-1b log line | VI d2 130.0..166.0 vs mock 132.0..165.5 (top 2.0 px high, bottom 0.5 px); EN unchanged 131.0..173.0 (`en4`). Not re-measured by QA | pass |
| 14 | Build profile clean | `git status --short "Athanor/Assets/Settings/Build Profiles"` | empty | pass |
| 15 | No build output staged | `git diff --cached --name-only` filtered for `Athanor/Build/`, `bin/`, `obj/`, `test-results` | 0 entries; none tracked; all four are git-ignored (`.gitignore:14,142,143,144`) | pass |
| 16 | M-4 `Plugins.meta`, `Plugins/WebGL` | `git status --short --untracked-files=all ...` | **No longer untracked: staged** (`M Plugins.meta`, `A Plugins/WebGL/Athanor.jslib`, `A ...jslib.meta`) | info for main session |
| 17 | Evidence folders (5a-16 base list) | `find production/qa/evidence/phase5/<task> -type f` | 5a-03 89, 5a-07 9 (+ingame 4), 5a-09 1, 5a-10 present, 5a-11 622, 5a-12 64, 5a-13 11, 5a-14 present, 5a-rotate 9 | pass |
| 18 | Lang keys en+vi | CoreTests (row 1) include LangTests; check_repo LANG ok | 0 fail | pass |
| 19 | `autoplay.mjs` 0 errors (5a-16 base list) | none retained | not evidenced | open (Q1) |

Notes
- a. `5a-17/build-output.txt` is a 40-line tail of the Unity batch log and holds no byte figures; the sum above was taken from the files on disk, which carry the D-3 timestamps.
- b. No loadsmoke artifact existed before this run (the devlog line said "watched locally"); it is now retained in `5a-16/loadsmoke/`. `5a-17/webtest-release.txt` stops at `1_1_bench` by design (dev-only hooks), superseded by the release-gate decision.
- c. R4 asked for the Legendary gap to be "recorded in the 5a-14 evidence"; there was no note in `5a-14/rarity/`. This file is the record: Legendary Journal frame (no Legendary material), Rare Journal detail (Lightning Sand locked on a fresh profile), Uncommon hand tooltip.

Known gaps accepted by the owner and not re-tested: VI half-leading offsets of 2-7 px on "DEPTH n", intro line and DEPTH CLEARED banner; tight VI catalyst body lines; high VI button labels; ~78% full-profile win rate under set A; win at any depth; lost run-over screen covered by tests only; d5 descent frames.
