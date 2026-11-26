# MEMORY: Athanor change history

Running change log for the owner and for Claude sessions that open this repo cold. Newest entry first. Facts only; every line
traces to a commit, a file in the tree or a decision in `docs/ATHANOR.md`. "Unverified" means no evidence was found in files or git.

To add an entry (after every task group):
1. Put a dated block at the top of the Change log: commit hash and one line, or "uncommitted" with the touched areas.
2. Update Current state if a plan task changed status; add any new decision to `docs/ATHANOR.md` § 4 and point to it from here.
3. Run the update procedure at the bottom (vault, graphify).

## Current state (as of 2026-11-26)

- Branch `feature/phase7-unity-conversion`, HEAD at `0d14b2d3` (2026-11-26) plus the commit that adds this file's automation set. The Unity template refactor is **committed**:
  stages 0-3 in `2d7cb831`, stages 4-7 in `0d14b2d3` (605 paths, most of them `vault/` and `graphify-out/`). Their tests were NOT re-run after that commit.
- Plan: `.claude/2026-11-24-unity-template-refactor.md` (stages 0-7, tasks 0-29; a copy is at
  `docs/superpowers/plans/2026-11-24-unity-template-refactor.md`). Decision record: `docs/architecture/adr-0001-unity-managers-and-content-assets.md`
  (status: Accepted, owner, 2026-11-24).
- What the refactor replaced:
  - `Athanor.Core` assembly -> `Athanor.Gameplay` (`Athanor/Assets/Scripts/Gameplay`, rule models stay plain C#). `Scripts/Core` and `Scripts/Data` are gone.
  - `GameFlow` -> `GameManager` (`Scripts/Game/Boot/GameManager.cs`, staged rename) plus five managers in `Scripts/Game/Managers/`
    (`ContentManager`, `SaveManager`, `LocalizationManager`, `ProgressManager`, `RunManager`, base `Singleton.cs`).
  - `Resources/content.json` -> one ScriptableObject asset per entry under `Athanor/Assets/Content/` (+ `GameRules.asset`, `ContentDatabase.asset`);
    `tools/content/snapshot.json` is the generated copy for Python/Node tools.
  - Scene-object tutorial steps (`TutorialStep`, `TutorialArea`) -> `Athanor/Assets/Content/Tutorial/Tutorial.asset` run by `TutorialManager`.
  - dotnet retired: `tools/CoreTests` and `tools/CoreReports` deleted; balance reports are `Athanor/Assets/Editor/Balance/BalanceReports.cs`; CI job `unity-tests` (`.github/workflows/ci.yml`; `MIN_TESTS: 600` is the CI minimum-pass setting, not a test result).

Plan tasks against the tree (evidence = file or git; passing tests were NOT re-run for this note):

| Tasks | Status | Evidence |
|---|---|---|
| 0 ADR | done, committed | ADR file added in `2d7cb831`; decisions in ATHANOR.md § 4 (2026-11-24) |
| 1-3 report and trace baselines | done, committed | `production/qa/evidence/refactor/baseline/*.txt` (6 reports), `Tests/PlayMode/Baselines/*` (autoplay seeds 11/22/33, `tutorial_trace.txt`) |
| 4 delete Data prototype | done, committed | `Scripts/Data` and `Editor/TempImport` absent |
| 5 Core -> Gameplay | done, committed | renames R099 in `2d7cb831`; `Scripts/Core` absent |
| 6-8 Unity-only tests, balance in Editor, dotnet deleted | done, committed | `Editor/Balance`, `Editor/Simulation`, `tools/CoreTests` absent, `ci.yml` job `unity-tests` |
| 9-13 content assets, snapshot, content.json deleted | done, committed | `Content/` asset folders, `Tests/EditMode/Content*Tests.cs`, `tools/content/snapshot.json`, `Resources/content.json` absent |
| 14-18 Singleton, GameManager, 5 managers | present, in `0d14b2d3` | `Scripts/Game/Managers/` untracked; `GameFlow.cs -> GameManager.cs` staged; `Managers` object in `Main.unity` |
| 19 PlayMode second-session / clean boot | present, in `0d14b2d3` | `Tests/PlayMode/ManagerBootTests.cs`, `SingletonTests.cs` untracked; pass/fail unverified |
| 20-24 tutorial as data | present, in `0d14b2d3` | `Tutorial.asset`, `TutorialSequence.cs`, `TutorialTypes.cs`, `TutorialManager.cs`; `TutorialStep.cs`, `TutorialArea.cs`, `TutorialStepsMigration.cs` deleted (staged); `Main.unity` has 0 `Step ...` objects (30 at HEAD) |
| 25 Inspectors | present, in `0d14b2d3` | `Editor/ManagerInspectors.cs`, `Editor/Content/ContentInspectors.cs`, `Editor/TutorialSequenceEditor.cs`, `Scripts/Gameplay/Content/ContentValidation.cs` |
| 26-27 tooltip and view grids in the Inspector | partly, in `0d14b2d3` | `Tooltip.cs` and other `Ui/*View.cs` modified; which grids moved is unverified |
| 28 documents, agents, vault | present, in `0d14b2d3` | `CLAUDE.md`, `README.md`, `docs/ATHANOR.md`, six `.claude/agents/*.md`, `.claude/docs/agent-roster.md`, `vault/` modified. `Athanor/Execution_Plan.md` still exists (plan says ask the owner before deleting) |
| 29 final gate | not done / unverified | no web-build size, test counts or final evidence recorded; ATHANOR.md § 6 figures not updated by this refactor as far as seen |

Other state: `production/session-state/active.md` is stale (2026-11-18, Phase 6 Task 5); do not trust its "current task". Graphify map
`graphify-out/GRAPH_REPORT.md` is dated 2026-11-26: 2932 nodes, 10233 edges, 116 communities.

## Change log

### 2026-11-26 (automation set, DOTween decision, this file)
- `MEMORY.md` created; `.gitignore` now ignores `Athanor/.claude/` (nested worktrees; the root pattern `.claude/worktrees/` is anchored to the repo root).
- Scene refresh (Unity CLI): `ManagerSceneMigration.RunBatch` made no scene change (Managers already complete), `Main.unity` is unchanged; `Reserialize.All` rewrote the 7 view prefabs
  (block order and float rounding only, checked by a diff reviewer). EditMode filter `SceneBakeTests|GoldenShotTests|LayoutPinTests|PrefabBakeTests|MainDepthTests`: 101 passed, golden shots 0 %.
  Evidence: `production/qa/evidence/scene-update-2026-11-26/` (copies of the baseline golden PNGs, EN and VI).
- Hooks added in `.claude/settings.json`: `guard-scene-files.sh` (PreToolUse Write|Edit; blocks text edits to `.unity` / `.prefab`, override `ATHANOR_ALLOW_SCENE_EDIT=1`) and
  `unity_lint.py --hook` (PostToolUse Write|Edit; exit 2 on lint errors in the edited file). Both were run with a passing and a failing input.
- Skills `/scene-refresh` and `/log-change`, agent `scene-diff-reviewer`, and `.mcp.json` (context7; github over HTTP, needs `GITHUB_PERSONAL_ACCESS_TOKEN` in the environment).
- Decision: **DOTween replaces PrimeTween** (`docs/ATHANOR.md` § 4, 2026-11-26). The code migration is NOT done: 15 C# files (`Support/Motion.cs`, `Support/Anim.cs`, views, `IgniteSequence`, `GameManager`,
  tests, `GoldenShots`), three asmdefs and `Packages/manifest.json` still reference PrimeTween; DOTween is not installed. `CREDITS.md` keeps the PrimeTween licence note until the package is removed.
- Not done on purpose: the SessionStart `detect-gaps.sh` warnings ("sparse design docs", "no production planning") are expected for a project documented in `docs/ATHANOR.md` and `vault/`; switching them off means
  changing `modes.workflow` in `project.yaml`, an owner decision.

### 2026-11-26 (`0d14b2d3`): refactor stages 4-7
- Stage 4: `Singleton<T>`, `GameManager` (renamed from `GameFlow`, same GUID), `ContentManager`, `SaveManager`, `LocalizationManager`,
  `ProgressManager`, `RunManager`; edits to `Boot/IgniteSequence.cs`, `Boot/Save.cs`, `Support/Loc.cs`, `Support/Art.cs`, `Support/Palette.cs`, `Support/Anim.cs`, `Ui/*View.cs`.
- Stage 5: `TutorialSequence` asset and types; `Tutorial/TutorialManager.cs` (was `Cutscene/TutorialController.cs`), `Tutorial/TutorialHole.cs`, `TutorialSettings.cs` moved; `TutorialTests.cs` moved to `Tests/EditModeGame`.
- Stage 6: Inspectors (`ManagerInspectors`, `ContentInspectors`, `TutorialSequenceEditor`), `ContentValidation` + `ContentValidationTests`.
- Stage 7: `CLAUDE.md`, `README.md`, `docs/ATHANOR.md`, agents and roster updated; vault and graphify regenerated.
- Tests touched: `Tests/EditModeGame/*` (Tutorial*, Scene*, Pause, Loc, LayoutPin, MainDepth, GameTestSetup), `Tests/PlayMode/AutoplayTraceTests.cs`, `TutorialTraceTests.cs`.
- Editor/prefab side effects in the same tree: `Prefabs/Views/{Bench,CatalystChoice,Goals,Journal,RunOver,Title}.prefab`, `Scenes/Main.unity`,
  `Art/Depth/*.png.meta` (19), `Editor/{ArtLibraryBuilder,AtlasBuilder,FontBuilder}.cs`, `Settings/UniversalRenderPipelineGlobalSettings.asset`. Camera drift (y 0.125, height 0.75) was in the `Main.unity` diff earlier on 2026-11-26; it is not in commit `0d14b2d3` (checked with `git diff -U0 HEAD~1 HEAD -- Athanor/Assets/Scenes/Main.unity`). See Gotchas.
- Evidence retained: `production/qa/evidence/refactor/autoplay/`, `production/qa/evidence/refactor/web/` (untracked).

### 2026-11-25: `2d7cb831` refactor stages 0-3 (commit message is generic: "Refactor code structure...")
- ADR-0001 and the refactor plan files; `Core` -> `Gameplay`, `Scripts/Data` deleted, `content.json` replaced by definition assets (254 `.asset` files under `Content/` today)
  (materials, recipes, catalysts, aspects, events, goals) + `GameRules`/`ContentDatabase`; `BalanceReports` and `Sim` moved into the Editor;
  `tools/CoreTests` and `tools/CoreReports` deleted; `ci.yml` and `tools/ci/run.sh` moved to Unity tests; baselines recorded.
  1,116 paths (mostly assets and vault).

### 2026-11-24
- `844c6937` feat(refactor): core C# refactoring priority plan; CI-blocking test failures fixed; golden-shot baselines under `production/qa/evidence/phase7/baseline/golden/`; lint `assertion_regex` updated.
- `49ebb63a` Texture import settings: Depth textures Bilinear -> Point, fog wrap Clamp; added `CheckSprites`, `FixCandleImport`; removed `DiagTest`.

### 2026-11-23
- `7dba5cef` "update": vault regeneration, vault notes for the editor-side Json* import types (157 files).
- `ebfa3a22` feat(rarity): 6 material tiers plus WorldSeed.
- `12bf3612` Arrange/Act/Assert markers in all Unity tests; `3bcd3c40` test commands -> `unity test EditMode`, art roots moved to `Assets/Art/...`; `95c8be51` code refactor.

### 2026-11-19 to 2026-11-20 (Phase 7 Unity conversion, before this refactor)
- 2026-11-20: tutorial steps became scene objects (`6622f350`, `4099d480`; later superseded by `Tutorial.asset`); Phase 7 merged into ATHANOR.md (`cbce4090`); layout and depth numbers into prefabs and the scene (`af3f1f11`, `0a47bc64`, `4c2d7992`); idle frames allocate nothing (`e4e28d75`).
- 2026-11-19: `ScreenFlow` (`96d9c19e`), `IgniteSequence` (`14141ecd`), `GameRules` block (`334c1fb7`), lint rules (`a4d33092`), statics reset at play start (`a0c4808e`); devlogs folded into ATHANOR.md (`51042948`, vault `7a7c3041`).

## Decisions and rulings

Dated decisions live in `docs/ATHANOR.md` § 4 "Decisions in force"; not duplicated here. The ones this refactor relies on:
- ADR-0001: `docs/architecture/adr-0001-unity-managers-and-content-assets.md`; its three ATHANOR.md lines are dated 2026-11-24 (Gameplay assembly + managers; content as assets; dotnet retired + `TutorialSequence`).
- 2026-11-19 Phase 7 lines (rule numbers in `GameRules`, statics only from the lint allowlist) and 2026-11-20 Phase 7 close / non-goals.
- 2026-11-18: the owner commits, agents never do (also in CLAUDE.md "Collaboration Protocol").
- CI rules and the case-to-check map: `docs/ATHANOR.md` § 5; the lint rules that actually exist are UL000-008, UL100-105, UL200-204, UL300-301.

## Gotchas

- Tween library: the decision is DOTween, the code still uses PrimeTween until the migration task runs (see the 2026-11-26 entry). Do not add new PrimeTween calls.
- `unity test` / `unity run` with the bare project name `Athanor` may open a different checkout (found in a worktree: 0 tests found and reported as pass). Pass the absolute project path, `/Users/bunker/Desktop/New Folder/Athanor`. The refactor worktree named in earlier notes (`unity-template-refactor-32f3f3`) is no longer registered in `git worktree list`; the work is in the main checkout.
- The `unity run` wrapper adds `-quit`; passing `-quit` again exits with code 6. Batch form: `unity run Athanor -- -executeMethod <Type.Method>`. Editor must be closed (project lock). Unity wipes `Temp/` on start: write batch reports elsewhere.
- Batch saves of `Athanor/Assets/Scenes/Main.unity` rewrite Camera `y: 0.125` / `height: 0.75` and four light `m_LocalPosition` values. After every scene-migration batch run `git diff -U0 Athanor/Assets/Scenes/Main.unity` and revert those lines. It was present in the uncommitted `Main.unity` diff on 2026-11-26 and is gone from commit `0d14b2d3`; the rule still holds for the next batch run.
- Enter Play Mode Options has domain reload off: statics survive Stop/Play, so singletons and allowlisted statics reset at `SubsystemRegistration`.
- Art exporter drift: do not run `node tools/art/export.mjs` (re-encodes PNGs, changes `layout.json`); use `node tools/art/export-depth.mjs` alone (from `production/session-state/active.md`; re-check before relying on it, the file is stale).
- Never edit `.unity`/`.prefab` as text; move scripts with `git mv` of the `.cs` and `.meta`; renamed serialized fields need `[FormerlySerializedAs]` (UL301).
- Run full suites once per task group (groups 1-4, 5-8, 9-13, 14-18, 19, 20-24, 25-27, 28, 29), not per task; baseline-recording tests stay in their task.

## Update procedure

After every task group:
1. Add an entry at the top of the Change log (hash or "uncommitted", touched areas, test counts if the group ran them) and fix Current state.
2. `python3 tools/vault/build_vault.py` (generated notes are never hand-edited).
3. `/graphify Athanor/Assets --update` (full `/graphify Athanor/Assets` after renames or moves, add `--force` after deletions); record node/edge counts in the entry.
4. Before pushing: `tools/ci/run.sh all` and `tools/ci/run.sh unity`. The owner commits; agents do not.
