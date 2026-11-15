# Athanor Phase 5a: delta 3, the road to sign-off (re-plan, `phase-lead`, 2026-11-15 23:00)

Delta to [delta2](2026-11-15-athanor-phase5-delta2.md) and [briefs](2026-11-15-athanor-phase5-briefs.md) (neither edited). Decisions:
`docs/devlogs/phase-5-prep.md` R1-R5 and *Log*. "Common to every brief" of the briefs file still applies. Plan only; nothing here is run.

## 0. State (main session, ~22:50; supersedes delta2 section 0)

| Item | State |
|---|---|
| `tools/ci/run.sh all` | **green**: 71 python tests, unity-lint clean, `check_repo` 0, CoreTests 327/327. `run.sh core` sets `DOTNET_ROLL_FORWARD=Major` (owner approved, uncommitted) |
| B-2 `export.mjs` -> `export-window.mjs`, B-3 `CREDITS.md` PrimeTween note | done on disk |
| `tools/web/descents.mjs` | 20 s per-command timeout, `ws.onclose` reject, hard deadline `seconds+240` (`descents.mjs:28-41`) |
| 5a-14 descents | 300 s runs: `en2/` and `vi/` d1+d2 only (19 files each, 0 console errors). Older `en/` (17:53) has d1..d3. **d4, d5 never shot; d3 never in VI** |
| 5a-10 VI/EN pairs (W2-06 a) | `evidence/phase5/5a-10/` **empty** |
| EditMode | not re-run since 415/407/1 fail/1 inconc./6 skip (before stars/tooltip code and the test rename) |
| webtest flake `e10`, 09:20 `ArgumentOutOfRangeException`, SPEC (f) table | not done |
| Effect-sheet re-import (C-1), release build + size (C-2) | not done; `Build/web/Build` is the stale 09:07 one, 12,039,306 B; ceiling **11,824,672 B** |
| Dev build profile `.asset` | dirty again (webGLDataCaching, webGLCompressionFormat, m_CodeOptimization, m_WebGLTextureSubtarget); G1: keep reverted |
| `Plugins.meta`, `Plugins/WebGL` | untracked while the old `Plugins.meta` is staged-deleted |
| Owner said "as recommended" for delta2 Q2, Q3, Q5 | **not yet in the devlog** |
| Commit / branch | none; both wait for the owner's explicit command (R3) |

## 1. Main session steps (no agent; `tools/ci` and `docs/CI.md` have no role owner)

- **M-1 (first, before any dispatch) devlog.** Append to `phase-5-prep.md` *Log* one line each: (a) delta2 Q2 accepted: ~78% full-profile
  win rate under set A for 5a, tune after 5b; (b) Q3: rotate layer does not pause the game; (c) Q5: `rotate_sync.mjs` joins CI, `rotate.mjs`
  stays manual; (d) R4 gap list grows: Rare Journal detail (Lightning Sand locked on a fresh profile) and Uncommon tooltip join Legendary as
  accepted unshot rarity frames; (e) `run.sh core` sets `DOTNET_ROLL_FORWARD=Major` (only .NET 10 on this machine). Then the owner's answer
  to section 3 below, once given.
- **M-2 CI (after M-1, parallel with D-1: disjoint).** Add `node tools/web/rotate_sync.mjs` to `tools/ci/run.sh all` (own stage) and a row in
  `docs/CI.md`'s case-to-check map. Acceptance: `tools/ci/run.sh all` green with the new stage listed; quote counts.
- **M-3 Revert the build profile** `git checkout -- "Athanor/Assets/Settings/Build Profiles/Web - Desktop - Development.asset"` after D-1
  and after D-3 (each Unity build dirties it again). Final check before any commit: `git status --short "Athanor/Assets/Settings/Build Profiles"` empty.
- **M-4 Before any commit:** `git add Athanor/Assets/Plugins.meta Athanor/Assets/Plugins/WebGL` (keeps the folder GUID). Then the 8-commit
  split (Q9) and the branch change (R3) **only on the owner's word**.

## 2. Briefs, in order

Wave D-a: **D-1 ‖ M-2** (Unity/`tools/web` vs `tools/ci`, `docs/CI.md`). Then sequential: D-2 -> D-3 -> D-4 -> D-5 -> D-6 (one Unity owner at a time).

#### D-1 Close W2-06: `unity-gameplay-engineer` (owns `tools/web/*` this wave; no other agent edits it)
- **Objective:** finish the W2-06 evidence and checks that are still open.
- **Inputs:** briefs.md W2-06 (lines 107-116); `tools/web/descents.mjs` (`<url> <outDir> [seconds] [lang]`, one Chrome on port 9335, so EN
  and VI run one after the other), `tools/web/webtest.mjs`; dev build `Athanor/Build/web-dev` served by `python3 -m http.server 8765`;
  `SimpleCutscene.cs:54,146-201`; SPEC (f) and `evidence/phase5/5a-03/f1-*, f2-*`; `evidence/phase5/5a-11/console.txt:204`.
- **Do:** (a) EN+VI pairs of event, catalyst choice, run over, intro into `evidence/phase5/5a-10/`; (b) webtest re-run: is the
  `ArgumentOutOfRangeException` gone (else fix, name the cause); (c) fix flake `e10` (random-event popup hides Continue) in `webtest.mjs`,
  no retries, no skipped checks; (d) SPEC (f) y-positions vs `SimpleCutscene.cs` table in the report; (e) `unity test Athanor --mode EditMode`
  once, quote total/pass/fail/inconclusive/skip; (f) descents d3..d5 **as the owner decides in section 3** (default if unanswered: option A).
- **Acceptance:** `ls production/qa/evidence/phase5/5a-10` shows 8 files (4 EN, 4 VI); `node tools/web/webtest.mjs` exit 0 and
  `grep -c "\[error\]"` 0 in its `console.txt`; EditMode 0 fail; descents per section 3 with `console.txt` 0 `[error]` each.
- **Out of scope:** content, template, art, effect import (D-2), any debug "jump to depth" hook unless the owner picks option B.

#### D-2 Effect sheets in play (delta2 C-1): `unity-gameplay-engineer` (after D-1)
- **Objective:** re-import the shrunk sheets and show the 4 effects in play.
- **Inputs:** briefs.md W2-08 (lines 127-135); `tools/fx/shrink.mjs`; `Fx.cs:9`; `ArtLibrary.asset:353-357`; `ReactionCutscene.cs:142-143`;
  `python3 tools/vault/q.py uses Fx` for current call sites (line numbers in `GameFlow.cs` have moved).
- **Acceptance:** each sheet `.asset` newer than its shrunk `.json`; 4 shots (implosion, holy-pillar, collect-burst, poof) in
  `evidence/phase5/5a-07/ingame/`; EditMode 0 fail (count quoted).
- **Out of scope:** new effects, frame-count or crop changes (those go to `pixel-artist` via D-3).

#### D-3 Release build and size (delta2 C-2): `web-release-engineer` (after D-2)
- **Objective:** a fresh release build proves 5a fits the ceiling.
- **Inputs:** `Athanor/Assets/Editor/WebBuild.cs` (`WebBuild.Build`), `WebGLTemplates/Athanor/index.html` (6,776 B), 5a-01 baseline in the
  devlog *Log* (10,776,096 B), last release 10,262,061 B (5a-06).
- **Do:** `unity build Athanor --target WebGL --execute-method WebBuild.Build`; confirm release (not development) in the log; `ls -l
  Athanor/Build/web/Build`; Build Report Textures line; `node tools/web/rotate.mjs` against the built page (portrait 390x844 touch).
- **Acceptance:** total bytes (Build/ + index.html) vs 11,824,672 quoted with headroom for 5b. If over: top texture contributors and cut
  proposals as `pixel-artist` briefs (Handoffs); do not cut alone.
- **Out of scope:** code, art, template changes.

#### D-4 Sign-off 5a-16: `qa-engineer` (after D-3; M-2 done)
- As briefs.md 5a-16 (lines 139-143) plus: `tools/ci/run.sh all` green incl. the `rotate_sync` stage; `rotate.mjs` result from D-3;
  rarity frames list with the accepted gaps (Legendary, Rare Journal detail, Uncommon tooltip; R4 + M-1d); balance under set A equals the
  Phase 4 baseline (12 rows) and `InformedPlayer_WinsMostSeeds` 28/40; descents per section 3.
- **Acceptance:** checklist table, each row pass/fail with the command and the real count. Failures come back as briefs to the owner role.

#### D-5 Release notes 5a-17: `web-release-engineer` (after D-4)
- As briefs.md 5a-17 (lines 145-147). Reuse the D-3 build if `git status` shows no code, content or asset change since; say so.
- **Acceptance:** 5a share of the 1 MiB and the 5b remainder stated; release-notes draft path; `grep -n "Crux\|PrimeTween" CREDITS.md` hits.

#### D-6 Vault 5a-18: `vault-librarian` (last)
- `python3 tools/vault/build_vault.py`; 0 broken links; Phase 5 row in `vault/Phases/Phases.md`. Acceptance: generator exit 0, link count quoted.

Then M-3 (final revert check) and M-4 (wait for the owner).

## 3. The owner decision needed: d3..d5 descent photographs (B-1 acceptance says d1..d5, EN+VI)

The scripted player needs real play time per depth (300 s reached d2; the 17:45 EN run reached d3), and a run-over ends a run early.

- **A. One longer try per language:** `node tools/web/descents.mjs <url> <out> 720 en|vi` (deadline 960 s; ~16 min each, sequential).
  Whatever depths are reached are kept; any still missing is recorded as an accepted gap in 5a-14.
- **B. Deterministic:** add a development-build-only "start at depth N" hook (new code, a test, `unity-gameplay-engineer`), then shoot
  d1..d5 EN+VI. Certain, but a new feature this late in 5a and one more dev build.
- **C. Accept now:** d1..d2 EN+VI and d3 EN as they are; d4/d5 covered by the 5a-09 window contact sheet and `DepthLookTests`.

**Recommendation: A, falling back to C for what A misses.** No new code; the per-depth window choice is already unit-tested, so the photos
check the look, not the rule. R3 asks for "the 5a-14 vi evidence" before commit: the owner should say whether VI d1..d2 (+ whatever A adds) meets it.
