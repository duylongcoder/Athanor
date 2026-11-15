# Athanor Phase 5: delta 2 after Wave 2-fix A (re-plan, `phase-lead`, 2026-11-15 18:00)

Delta to [2026-11-15-athanor-phase5-briefs.md](2026-11-15-athanor-phase5-briefs.md) (not edited). Decisions: `docs/devlogs/phase-5-prep.md`
"Owner answers to the briefs audit" (Q1-Q9, 5a, 4a-4c, 5b) and *Log* lines 168-173. Everything in "Common to every brief" still applies.

## 0. State checked on disk at 18:00 (real results)

| Check | Result |
|---|---|
| `DOTNET_ROLL_FORWARD=Major dotnet test tools/CoreTests` | **326 passed, 0 failed** |
| `tools/ci/run.sh lint` | OK, unity-lint clean |
| `content.json` vs HEAD | identical (`git diff --quiet` ok); `rarityWeights` absent = set A, per owner Q2 |
| CS0104 `TutorialPlan` clash | **gone**: Game `TutorialPlan`/`PlanFor` deleted, `TutorialController.cs:11` is `TutorialRules` (MarkDone, AnswerSkipQuestion); no `[Obsolete]` left in Core |
| Skip = run at once (Q3) | in code `GameFlow.cs:209-216` |
| Rarity stars | `Game/Support/RarityStars.cs`; 24 frames in `evidence/phase5/5a-14/rarity/` |
| Rotate layer | built in `WebGLTemplates/Athanor/index.html` (6,776 B); `tools/web/rotate.mjs`, `rotate_sync.mjs` pass by hand, not in CI |
| W2-06 | **in progress now** (`descents.mjs` 17:45, `5a-14/en/d1..d3` 17:53-17:56; `5a-14/vi/` empty) |
| Release build on disk | still the 09:07 one, 12,039,306 B in `Build/`: stale, pre-shrink |
| `unity test Athanor --mode EditMode` | last result 415 / 407 pass / 1 fail / 1 inconc. / 6 skip, **before** the stars/tooltip code and the rarity test rename: not re-run |
| `tools/ci/run.sh all` | not run since W2-04 |
| Q6 `export.mjs` calls `export-window.mjs` | **not done** (no match in `tools/art/export.mjs`) |
| Q8 PrimeTween note in `CREDITS.md` | **not done** (`CREDITS.md:25` has no redistribution note) |
| `Plugins/` | still staged-deleted and untracked (`Plugins.meta`, `Plugins/WebGL/Athanor.jslib(.meta)`) |

## 1. Briefs still to run

### Wave B (now; W2-06 owner is busy, so only disjoint paths in parallel)

#### B-1 Finish W2-06: `unity-gameplay-engineer` (already running; do not dispatch a second one)
- As `briefs.md` W2-06 (a)-(d). Add: (e) run `unity test Athanor --mode EditMode` once after the stars/tooltip code and quote total/pass/fail/skip;
  (f) fix the webtest random-event flake `e10` (popup hides Continue) in `tools/web/webtest.mjs` so a clean run exits 0 (no retries, no skipped checks).
- **Acceptance:** EditMode 0 fail (count quoted); `node tools/web/webtest.mjs` exit 0, 0 `[error]`; descents d1..d5 EN+VI files exist; SPEC (f) table in report.
- **Out of scope:** content, template, art. Files: `GameFlow.cs`, `SimpleCutscene.cs`, `tools/web/*`, `Tests/EditModeGame/*`.

#### B-2 One art command: `pixel-artist` (parallel with B-1; disjoint)
- **Objective:** `node tools/art/export.mjs` also runs `export-window.mjs` (owner Q6).
- **Inputs:** `tools/art/export.mjs`, `tools/art/export-window.mjs`, evidence `5a-09/contact.png`.
- **Acceptance:** `node tools/art/export.mjs` then `git status --short Athanor/Assets/Art/Background` shows no change to `window_d1..d5.png` (byte-identical output) or the drift listed.
- **Out of scope:** new art, other sheets.

#### B-3 Licence note: `web-release-engineer` (parallel; disjoint)
- **Objective:** `CREDITS.md` notes the PrimeTween redistribution clause and "re-decide before public release" (owner Q8).
- **Acceptance:** `grep -n -i "redistribut" CREDITS.md` one hit.

### Wave C (after B-1 reports; one Unity owner)

#### C-1 W2-08 unity part: `unity-gameplay-engineer`
- As `briefs.md` W2-08 (re-import `Athanor > Import Effect Sheets` after shrink; 4 in-game shots into `evidence/phase5/5a-07/ingame/`).
- **Acceptance:** `.asset` newer than the shrunk `.json`; 4 shots; EditMode 0 fail.

#### C-2 W2-08 web part: `web-release-engineer` (after C-1)
- Release build `WebBuild.Build`; `ls -l Athanor/Build/web/Build` + index.html (6,776 B) vs **11,824,672 B**; Build Report Textures line;
  confirm release (not dev) in the log; open the built page with `node tools/web/rotate.mjs` against the real build (portrait 390x844 touch).
- If over: top texture contributors, cut proposals as `pixel-artist` briefs. Do not cut alone.
- **Acceptance:** total bytes and headroom for 5b quoted.

### Wave D (sequential, last)

1. **5a-16 `qa-engineer`**: as `briefs.md` 5a-16; plus `rotate.mjs` + `rotate_sync.mjs` results; rarity frames list with the accepted gaps (Open q. 4); balance under set A equals Phase 4 baseline (12 rows).
2. **5a-17 `web-release-engineer`**: as `briefs.md` (can reuse C-2's build if no code changed since; say so).
3. **5a-18 `vault-librarian`**: `python3 tools/vault/build_vault.py`, 0 broken links, Phases row.
4. **Main session**: `git add Athanor/Assets/Plugins.meta Athanor/Assets/Plugins/WebGL` (keeps the folder GUID) before any commit; then the 8-commit split only on the owner's word (Q9).

### Phase 5b
Unchanged from `briefs.md` section 3. Starts only after 5a is committed/merged and the owner creates `feature/phase5b-hd2d` (decision #6).
5b-08 ceiling = what C-2/5a-17 leaves under 11,824,672 B.

## 2. Open questions (owner, new only)

1. Re-open rarity set C? W2-03 at 2000 seeds: C informed 28/40 (in range), but unlocked profiles drop 3-6 points. Owner rule Q1/Q2 already applied (B failed, A kept). *Recommend: keep A, revisit after 5b.*
2. Full profile win rate ~78% under A (Phase 4 baseline, unchanged): accept for 5a? *Recommend: accept; tune after 5b.*
3. Rotate layer: the game keeps running underneath (no pause). *Recommend: keep; no timed mechanic.*
4. Rarity frames not shootable (Rare Journal detail locked on fresh profile, no Legendary material, Uncommon tooltip): accept the gap or add a debug profile? *Recommend: accept for 5a.*
5. Add `rotate.mjs` / `rotate_sync.mjs` to CI (`tools/ci/run.sh`, `docs/CI.md`)? *Recommend: `rotate_sync` yes (cheap, no browser); `rotate.mjs` stays manual.*
