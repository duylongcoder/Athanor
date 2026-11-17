# Athanor Phase 6 — Unity-native refactor and cleanup (design)

Date: 2026-11-17. Owner-approved in conversation, 2026-11-17. Branch: `feature/phase6-unity-refactor` (from `feature/phase5b-hd2d`).
This spec is temporary: step 5 folds it into `docs/ATHANOR.md` and deletes it.

## Goal

Close Phase 5 as it stands, then make the Game layer a normal Unity project (scene + prefabs + Inspector references instead of
hierarchies built in code), clear the lint baseline, delete outdated material, merge the project docs into one current file, and
regenerate the graphify map of `Athanor/`.

## Order of work

Each step ends with `tools/ci/run.sh all` green. The owner commits; agents do not.

1. Close Phase 5
2. Refactor the Game layer to prefabs + Inspector (view by view)
3. Clear the lint baseline
4. Delete outdated content
5. Merge project docs into `docs/ATHANOR.md`
6. Graphify `Athanor/`

## 1. Close Phase 5

- New devlog `docs/devlogs/phase-5-close.md`: what shipped (5a, 5b-01..08, G2, G3), final numbers from
  `production/qa/phase5b-signoff.md`, and the carry-overs listed below. (It is folded into `ATHANOR.md` in step 5.)
- Carry-overs, not decided now: 5b-02 loops mockup review; 5b-03 endings lore (frame, three seeds, win at any depth vs depth 5
  only, shared ending lines); remote `origin/feature/2.5D_test-phase-4` deletion; G3 carry-overs (7-scene storyboard, wider
  `LineBox` use, VI text pass); reduced-effects switch.
- No version change (`project.yaml` stays `0.4.0`), no release notes, no merge to `develop`/`main`.
- Update `production/session-state/active.md` to point at Phase 6.

## 2. Refactor the Game layer to prefabs + Inspector

### Stays as is

- `Athanor.Core` (no `UnityEngine`) and its 331 Core tests; `dotnet test tools/CoreTests` unchanged.
- `content.json` and `lang/en.json`, `lang/vi.json` stay JSON (Core and the dotnet tests read them). They are passed in as
  `[SerializeField] TextAsset` fields on `GameFlow` instead of `Resources.Load`.
- Shaders, art PNGs, URP/2D renderer settings, PrimeTween.
- The development hooks that `tools/web/*.mjs` read (`Dev.Hook` lines, F6–F10 keys, `AutoHook`).
- Pure helper statics with no scene state: `Px`, `DepthMath`, `Wording`, `KindColors`, `Motion`, `Anim`, `Haptics`, `Dev`,
  `AudioMute`, `TutorialRules`, `DepthLook`, `Save`, `Loc` (the `Loc` resource load becomes an injected `TextAsset`).

### Target structure

- `Assets/Scenes/Main.unity` is the source of truth, committed and edited in the Editor. No tool regenerates it.
- `GameFlow` is the scene root. It holds `[SerializeField]` references to every view in the scene: `BenchView`, `HudView`,
  `TitleView`, `JournalView`, `CatalystChoiceView`, `RunOverView`, `EventView`, `GoalsView`, `GoalToast`, `PauseMenu`,
  `ReactionCutscene`, `SimpleCutscene`, `TutorialController`, `WindowView`, `ScreenFx`, `DepthStage`.
- One prefab per view in `Assets/Prefabs/Views/`; repeated parts in `Assets/Prefabs/Parts/` (tile, card, button, label,
  journal entry, and any other piece a view instantiates more than once).
- Data-driven repeats (bench tiles, catalyst cards, journal rows, goal rows) are `Instantiate`d from a `[SerializeField]`
  prefab, then filled with data.
- Views receive what they need through an explicit `Bind(...)` call from `GameFlow` (content db, run state, settings, callbacks).
  No view reads `GameFlow.I`.
- Sprites, materials and fonts are assigned in the prefab. String lookups through `ArtLibrary` are removed where a prefab
  reference replaces them; `ArtLibrary` stays only for lookups by content id (material/catalyst/aspect icons), if any remain.

### Removed

- `Make.cs` hierarchy factories (`Root`, `Sprite`, `SpriteAt`, `Rect`, `Glow`, text/light helpers).
- Every view's `Build(...)` method.
- `Editor/SceneBuilder.cs` and `Assets/Resources/layout.json` (positions live in the prefabs and the scene). The `layout.json`
  output of `tools/art/export.mjs` is dropped too.
- The `GameFlow.I` singleton.

### Migration method (approach A: bake view by view)

For each view, in this order: Title, Journal, CatalystChoice, RunOver, Event, Goals, GoalToast, Pause, HUD, cutscenes
(Reaction, Simple, Window, Tutorial), ScreenFx, Bench + tiles, DepthStage/HD-2D rig:

1. Capture a reference screenshot of the view from the current build (before the change).
2. A temporary Editor script `Athanor > Bake View` (`Assets/Editor/ViewBaker.cs`), run through the `unity` CLI, calls the existing
   `Build(...)` once and saves the result with `PrefabUtility.SaveAsPrefabAsset`.
3. Change the view to use serialized fields and the `Bind(...)` entry point; delete its `Build(...)` and the `Make.*` calls it used.
4. Place the prefab in `Main.unity` and wire it on `GameFlow`.
5. Verify (below). Next view only when green.

After the last view: delete `ViewBaker.cs`, `Make.cs`'s factories and `SceneBuilder.cs`.

### Verification per view

- `tools/ci/run.sh all` green (lint with no new findings, repo guard, rotate, Core tests).
- `unity test Athanor --mode EditMode`: 0 failures.
- Development WebGL build: `tools/web/webtest.mjs` and `tools/web/mainhd2d.mjs` exit 0, no console errors.
- Screenshot of the view vs the reference: under 0.1 % of pixels differ. Kept in `production/qa/evidence/phase6/<view>/`.
- Release build stays under the 11,824,672 B ceiling (checked once at the end of step 2).

### Tests

- EditMode tests that build a view with `Build(...)` load its prefab with `AssetDatabase.LoadAssetAtPath` and instantiate it, with
  a `[TearDown]` that destroys it.
- Game logic tests (hooks, motion, depth math, save, settings) are unchanged except for the API rename to `Bind(...)`.

## 3. Clear the lint baseline

Target: `tools/lint/baseline.json` has 0 entries, and no new findings from step 2.

| Rule | Count | Fix |
|---|---|---|
| UL100 test name not `Method_Condition_Result` | 131 | Rename the method only. Test logic unchanged. |
| UL103 `[Ignore]`/`[Explicit]`/`[Retry]` | 6 | These are the 6 skipped EditMode tests. Fix and re-enable each one that still tests something real; delete the obsolete ones and give the reason in the commit body. |
| UL101 test without an assertion | 6 | Add a real `Assert`. |
| UL005 event not unsubscribed | 9 | Add the matching `-=` in `OnDisable`/`OnDestroy`. |
| UL001, UL002, UL003, UL004 | 6 | Fix in code; several disappear with step 2. |

Then write the empty baseline
(`python3 tools/lint/unity_lint.py --root Athanor --config tools/lint/lint.config.json --all --write-baseline tools/lint/baseline.json`)
and run `tools/ci/run.sh all`. Core test count must stay at or above `MIN_TESTS` (250).

## 4. Delete outdated content

Everything below except `refs/` stays recoverable from git history or its own remote.

| Group | Delete | Follow-up edits |
|---|---|---|
| DepthTest prototype | `Assets/Scenes/DepthTest.unity`, `Editor/DepthTestBuilder.cs`, `Editor/DepthTestEvidence.cs`, `Assets/Settings/DepthTest/*` | Move shared materials (`DepthTable`, `DepthDisplay`, `Fog`, `TiltShift`) to `Assets/Art/Depth/Materials/`. Remove `WebBuild.BuildDepthTest`. Delete `tools/web/depthmatrix.sh`, `depthprobe.mjs`, `depthshimmer.py`, `depthshimmer.sh`. Fix the `TileView` comment that names `DepthTestBuilder`. |
| Old evidence and mockups | `production/qa/evidence/*` except `phase6/`, `docs/mockup/`, `docs/devlogs/phase-*-shots/` | Remove links to them in the docs merged in step 5. |
| Framework extras | `studio-tests/`, `graphify-out/` (regenerated in step 6), `Map.canvas` | None. |
| Nested repos | `pixelfx/`, `pixel-effects/` (clean clones; remotes `github.com/patch-gg/pixelfx`, `git.patch.gg/patch/pixel-effects`) | Remove their gitlinks. Delete `tools/fx/export.sh` and `tools/fx/shrink.mjs` (the sheets already live in `Assets/Art/Fx`). Update `.claude/agents/pixel-artist.md` and `README.md`. |
| `refs/2.5D Reference/` | 30 MB of videos and screenshots, untracked, **no other copy** | Ask the owner to confirm once more immediately before deleting. |
| Outdated plan/spec/sign-off docs | Handled in step 5. | |

## 5. Merge project docs into `docs/ATHANOR.md`

**Folded in, then deleted:** `docs/devlogs/*.md` (17 + `phase-5-close.md`), `docs/superpowers/plans/*.md` (7),
`docs/superpowers/specs/*.md` (3 + this spec), `production/qa/phase5a-signoff.md`, `production/qa/phase5b-signoff.md`, `docs/CI.md`.

**`ATHANOR.md` describes the project as it is now**, in these sections:

1. The game: premise, run loop, rules, scoring, depth, content kinds
2. Architecture: Core / Game split, scene + prefabs, `GameFlow` and views, HD-2D rig, save, localization
3. Data: `content.json`, `lang/*.json`, icons, art pipeline (`tools/art`)
4. Decisions in force: one line each, with a date
5. CI and rules: the merge rule, the failure-to-check table (from `CI.md`), baseline policy
6. Build, test, release: commands, size ceiling, web checks
7. Carry-overs: from the Phase 5 close
8. Lore and text voice: the parts of the lore devlogs that still bind writing

Not carried over: rejected options, prototype measurements that no longer apply, finished plan steps, and old phase histories.
Git history keeps them.

**Kept as separate files:** every `CLAUDE.md` (Claude Code reads them by path), `README.md` (shortened to game + build, links
`ATHANOR.md`), `CHANGELOG.md`, `CREDITS.md` (asset licence credits), the framework docs (`docs/COLLABORATIVE-DESIGN-PRINCIPLE.md`,
`docs/WORKFLOW-GUIDE.md`, `docs/migration-guide-v1.1.md`, `docs/skill-flow-diagrams.md`, `docs/engine-reference/`), `.claude/`,
`vault/`.

**Follow-up edits:**

- `CLAUDE.md`: the hard rule "Devlog per phase in `docs/devlogs/phase-N-*.md`" becomes "Record a decision in *Decisions in force*
  of `docs/ATHANOR.md` before relying on it". The CI hard rule points at the CI section of `ATHANOR.md`. Update the project layout
  table (`docs/devlogs`, `docs/superpowers`, `docs/mockup`).
- `tools/vault/build_vault.py`: read phases and decisions from `ATHANOR.md` instead of `docs/devlogs/`; regenerate the vault.
- Every link to a deleted file in `.claude/agents/`, `.claude/rules/`, `.claude/docs/`, `vault/` hand-written notes,
  `tools/` comments and `README.md` points to the matching `ATHANOR.md` section. A grep for each deleted path returns nothing.

## 6. Graphify `Athanor/`

- Run the `graphify` skill on `Athanor/` only. Output to `graphify-out/` at the repo root.
- Exclude cache and build output: `Library/`, `Build/`, `Logs/`, `UserSettings/`, `Data/`, `Temp/`, `obj/`, and the
  Unity-generated `*.csproj` / `*.slnx`.
- Read nothing outside `Athanor/`.

## Done when

- `Main.unity` + prefabs drive every screen. No `Build(...)`, `Make.Root/Sprite/Rect/Glow`, `SceneBuilder`, `layout.json` or
  `GameFlow.I` remains (grep returns nothing).
- Lint baseline empty. `tools/ci/run.sh all` green. EditMode 0 failures and 0 skipped. Web checks exit 0. Release under the ceiling.
- Per-view screenshots under 0.1 % pixel difference, kept in `production/qa/evidence/phase6/`.
- Step 4 deletions done. Grep for every deleted path finds no reference.
- `docs/ATHANOR.md` is the single project doc. The vault has been regenerated.
- `graphify-out/` regenerated from `Athanor/` alone.

## Risks

- **Prefab serialization of runtime-only state.** A `Build(...)` may set fields that are not serialized (private, non-`[SerializeField]`,
  or Unity-unserializable types). The baked prefab would lose them. Mitigation: per view, list the fields `Build` sets, make the
  needed ones `[SerializeField]` before baking (UL301 `FormerlySerializedAs` applies to renames).
- **HD-2D rig.** `DepthStage`/`DepthRig` create a render texture and cameras at runtime. The render texture stays created at
  runtime; the cameras and the layer objects become scene objects.
- **Pixel diffs from animated grain/fog.** Compare with effects off (F8) and at a fixed frame, the same way as the 5b-05 rest-frame check.
- **Release size.** Prefabs and the scene add YAML but no art. The size is checked at the end of step 2.
