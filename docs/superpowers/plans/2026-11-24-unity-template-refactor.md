# Unity Template Refactor Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Turn Athanor into a conventional Unity project: Manager singletons instead of the pure C# Core layer and the GameFlow composition root, ScriptableObject assets instead of `content.json`, Inspector-editable data instead of code constants, a data-driven tutorial, and PlayMode tests that prove the game still plays exactly as it did.

**Architecture:** The `Athanor.Core` assembly (no UnityEngine, compiled by dotnet) becomes the Unity assembly `Athanor.Gameplay`. Its rule classes (`RunState`, `IgniteResolver`, `ChainPlanner`, `BenchRules`, `Profile`, `Journal`) stay plain C# models, so seeded tests and balance simulations stay fast. State, flow, saves and Inspector settings move to six `Singleton<T>` MonoBehaviours on a `Managers` object in `Main.unity`. Content is one asset per material, recipe, catalyst, aspect, event and goal, plus `GameRules` and `ContentDatabase`. `ContentDatabase.ToContentData()` hands the rules the same plain data they read today, so no rule code changes. The tutorial becomes a `TutorialSequence` asset (steps, lit areas, scripted hands, lesson order) run by a `TutorialManager`.

**Tech Stack:** Unity 6000.6.7f1 (URP 2D, WebGL), C# 9, Unity Test Framework 1.8.0 (EditMode + PlayMode, NUnit 3), PrimeTween 1.5.2, the `unity` CLI wrapper, game-ci/unity-test-runner@v4, Python 3 (lint, repo check, vault), Node 22 (web checks).

---

## Owner decisions this plan is built on (2026-11-24)

| Question | Answer |
|---|---|
| How far does "remove Core C#" go | **Managers + plain models.** Delete the `Athanor.Core` assembly and the dotnet `tools/CoreTests`. Manager singletons own state, flow, saves and Inspector settings. The rule algorithms stay plain C# classes inside a Unity assembly. |
| ScriptableObject layout | **One asset per entry.** `MaterialDefinition`, `RecipeDefinition` (references material assets), `CatalystDefinition`, `AspectDefinition`, `EventDefinition`, `GoalDefinition`, plus `GameRules` and `ContentDatabase`. Saves keep string ids. |
| Tutorial | **`TutorialSequence` asset:** typed lesson actions, scripted trials that reference material assets, lit areas declared in each step, one `TutorialManager` that runs any sequence. |
| Balance reports | **Editor menu + batchmode:** `Athanor > Balance > …` and `BalanceReports.RunBatch`, with the same output lines, compared byte for byte against a baseline. |

## Facts found while planning (read before starting)

1. **`tools/CoreReports` does not compile at HEAD `7dba5cef`.** It still uses `RunState.PouchMode` and `RunState.FinalDepth`, which moved to `RunOptions.PouchMode` and `ContentDb.FinalDepth`. It also calls `Sim.Describe` without its new `finalDepth` argument. Task 1 fixes this first, so the baseline can be recorded.
2. `dotnet test tools/CoreTests` passes at HEAD: 369 passed, 0 failed. `Athanor/Assets/Tests/EditModeGame` has about 265 Game tests.
3. `Scripts/Data/*.cs` (assembly `Athanor.Data`) and `Editor/TempImport/ContentJsonImporter.cs` are an unused earlier attempt. `MaterialSO` and the others there are `[Serializable]` classes, not assets, and their `Rarity` enum has 4 tiers while the game has 7. Nothing references them, so Task 4 deletes them.
4. `ProjectSettings/EditorSettings.asset` has Enter Play Mode Options on with domain reload **off** (`m_EnterPlayModeOptions: 1`). Static fields survive Stop and Play, so every singleton must be cleared at `SubsystemRegistration`.
5. The `unity run` wrapper adds `-quit` itself. Passing `-quit` again exits with code 6. Every batch command below is `unity run Athanor -- -executeMethod <Type.Method>` with no `-quit`.
6. `docs/ATHANOR.md` §5 lists lint rules UL009–UL014, but `tools/lint/unity_lint.py` at HEAD only has UL000–UL008, UL100–UL105, UL200–UL204 and UL300–UL301. This plan does not depend on the missing rules. Task 28 corrects the document.
7. Tools outside Unity that read `content.json`: `tools/ci/check_repo.py` (CONTENT, ICONS), `tools/vault/build_vault.py`, `tools/art/export.mjs`, and three agent files. Task 13 switches them to a snapshot file the Editor generates. The game never reads that file.
8. `docs/superpowers/plans/2026-11-23-depth-scaling-progression.md` (not started) also edits Core and content. Do not run it alongside this plan. Finish this plan first, then rewrite that one against `Athanor.Gameplay` and the content assets.

## Global constraints

- **Nothing a player sees or scores changes.** Each stage ends with these four checks green:
  - the six balance reports are byte-identical to the Task 1 baseline;
  - `GoldenShotTests` show 0 % difference;
  - `tutorial_lit_baseline.json` still matches;
  - the PlayMode traces (Tasks 2 and 3) are identical.
- Never edit `.unity` or `.prefab` files as text. Scene and prefab changes are made by Editor scripts in this plan, run through `unity run`, or by hand in the Editor.
- Moving or renaming a MonoBehaviour or ScriptableObject script means `git mv` of the `.cs` **and** its `.meta`. The GUID keeps every scene and prefab reference. Renaming a serialized field needs `[FormerlySerializedAs]` (UL301).
- Tests are named `Method_Condition_Result`, carry `// Arrange`, `// Act` and `// Assert` comments, and have at least one `Assert` in the test method itself (UL101).
- Unity batch commands need the Editor closed (project lock).
- **Commits:** per *Decisions in force* (2026-11-18), the owner commits and agents never do. Each task ends with a checkpoint that gives a Conventional Commit message with `Story: R-<task>` in the body.
- Every player-visible string stays a `Lang` key in `en.json` and `vi.json`. This plan adds none, except the Editor-only inspector labels, which are not player-visible.

## Commands used throughout

| Name | Command | Pass when |
|---|---|---|
| EditMode | `unity test Athanor --mode EditMode` | 0 failed |
| PlayMode | `unity test Athanor --mode PlayMode` | 0 failed |
| Core (until Task 8) | `DOTNET_ROLL_FORWARD=Major dotnet test tools/CoreTests --nologo` | 0 failed, at least 250 passed |
| Reports (until Task 7) | `DOTNET_ROLL_FORWARD=Major DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 dotnet run --project tools/CoreReports --no-build -- <name>` | prints |
| Reports (from Task 7) | `unity run Athanor -- -executeMethod BalanceReports.RunBatch -report <name> -out <file>` | exit 0 |
| Batch tool | `unity run Athanor -- -executeMethod <Namespace.Type.Method>` | exit 0 |
| CI | `tools/ci/run.sh all` | exit 0 |
| Vault | `python3 tools/vault/build_vault.py` | then `git status vault` is clean once its changes are staged |

The report names are `balance-matrix`, `events`, `roles`, `newcomer`, `balance` and `trace`.

## File map (end state)

```
Athanor/Assets/
  Scripts/Gameplay/                 (was Scripts/Core) asmdef Athanor.Gameplay, namespace Athanor.Gameplay
    *.cs                            RunState, RunStart, Ignite, Chain, BenchRules, Satchel, Journal, Profile, ScreenFlow,
                                    Settings, Lang, ScoreParts, EffectOps, LinkPair, AutoPlayer, ContentDb, ContentData,
                                    Tutorial.cs (only TutorialTrial, TutorialLay, TutorialPlan, TutorialGate)
    Content/ContentEnums.cs         Inspector enums + ContentNames (enum <-> the strings the rules read)
    Content/MaterialDefinition.cs, RecipeDefinition.cs, CatalystDefinition.cs, AspectDefinition.cs,
            EventDefinition.cs, GoalDefinition.cs, GameRules.cs, ContentDatabase.cs
  Scripts/Game/Managers/            Singleton.cs, ContentManager.cs, SaveManager.cs, LocalizationManager.cs,
                                    ProgressManager.cs, RunManager.cs
  Scripts/Game/Boot/GameManager.cs  (was GameFlow.cs, same GUID)
  Scripts/Game/Tutorial/            TutorialManager.cs (was Cutscene/TutorialController.cs, same GUID),
                                    TutorialSequence.cs, TutorialTypes.cs, TutorialHole.cs, TutorialSettings.cs
  Content/                          ContentDatabase.asset, GameRules.asset, Materials/, Recipes/, Catalysts/,
                                    Aspects/, Events/, Goals/, Tutorial/Tutorial.asset
  Editor/Content/                   ContentJsonImporter.cs (one-shot, deleted in Task 13), ContentInspectors.cs
  Editor/Balance/BalanceReports.cs  (was tools/CoreReports/Program.cs)
  Editor/Simulation/                Sim.cs (was Tests/EditMode/Sim.cs), asmdef Athanor.Simulation (Editor only)
  Editor/Migration/                 ManagerSceneMigration.cs, TutorialSequenceMigration.cs
  Editor/ManagerInspectors.cs, Editor/TutorialSequenceEditor.cs
  Editor/Shared/ContentAssets.cs    loads ContentDatabase for tests and tools
  Editor/Shared/ContentSnapshot.cs  writes tools/content/snapshot.json
  Tests/EditMode/                   asmdef Athanor.Gameplay.Tests (was Athanor.Core.Tests)
  Tests/EditModeGame/               asmdef Athanor.Game.Tests
  Tests/PlayMode/                   asmdef Athanor.PlayMode.Tests, Baselines/*.txt
tools/content/snapshot.json         generated by Athanor > Content > Write Snapshot; read by Python/Node tools only
docs/architecture/adr-0001-unity-managers-and-content-assets.md
```

Deleted by the end: `Scripts/Core/` (moved), `Scripts/Data/`, `Editor/TempImport/`, `Resources/content.json`, `tools/CoreTests/`, `tools/CoreReports/`, `Game/Cutscene/TutorialStep.cs`, `Game/Cutscene/TutorialArea.cs`, `Editor/TutorialStepsMigration.cs`, `Editor/TutorialStepGizmo.cs`, the 30 `Step …` objects under the Tutorial object in `Main.unity`.

---

# Stage 0: Decision and safety net

### Task 0: ADR and Decisions in force

**Files:**
- Create: `docs/architecture/adr-0001-unity-managers-and-content-assets.md`
- Modify: `docs/ATHANOR.md` §4 (append three lines under *Process and architecture*)

- [ ] **Step 1: Write the ADR** with the sections of `.claude/docs/templates/architecture-decision-record.md` (required by `docs/CLAUDE.md`). Status stays `Proposed` until the owner reads it (Step 2). Fill the template's other headings (Date, Decision Makers, Summary, Architecture Diagram, Key Interfaces, Risks, Performance Implications, Migration Plan, Validation Criteria) from this plan: the interfaces are those of Tasks 10, 14, 16-18 and 20, the migration plan is Stages 0-7, the validation criteria are the four checks under *Global constraints*, and the rollback is "revert the branch; the Task 1-3 baselines still describe the old game".

```markdown
# ADR-0001: Unity managers, content assets and a data-driven tutorial

## Status
Proposed

## Engine Compatibility
Unity 6000.6.7f1 (project.yaml). `docs/engine-reference/unity` is pinned to 6.3 LTS and predates this editor. The APIs this
change leans on (`RuntimeInitializeOnLoadMethod(SubsystemRegistration)`, `DefaultExecutionOrder`,
`Object.FindFirstObjectByType(FindObjectsInactive)`, `Application.logMessageReceived`, `SceneManager.LoadSceneAsync(Additive)`,
and Enter Play Mode Options with domain reload off) are checked against the 6000.6 manual in Task 2 Step 1, before the first
of them is used.

## ADR Dependencies
None (first ADR). Supersedes lines of `docs/ATHANOR.md` §4 listed under Consequences.

## GDD Requirements Addressed
None: a structure change. No rule, number or player-visible text is intended to change (checked by golden shots, byte-identical
reports and PlayMode traces).

## Context
Athanor's rules live in `Athanor.Core`, a plain C# assembly with no UnityEngine that is also compiled by dotnet
(`tools/CoreTests`, `tools/CoreReports`). Content is `Resources/content.json`. `GameFlow` builds and wires everything at run time.
The owner wants the project to work like a Unity template: managers in the scene, data as assets edited in the Inspector,
and gameplay checked in Play Mode.

## Alternatives Considered
- Rewrite the rules as MonoBehaviours: rejected by the owner (slow seeded tests, 200-seed balance runs in a scene, high risk of drift).
- Keep the pure Core and add manager facades only: rejected (it does not remove the Core layer).
- One big content asset of [Serializable] arrays: rejected (hard to edit one entry, merge conflicts on one file).

## Decision
1. `Athanor.Core` becomes the Unity assembly `Athanor.Gameplay`. The rule models stay plain C# classes inside it.
2. Five `Singleton<T>` managers on the `Managers` object of `Main.unity` own content, saves, language, progress and the run:
   `ContentManager`, `SaveManager`, `LocalizationManager`, `ProgressManager`, `RunManager`. `TutorialManager` (the renamed
   `TutorialController`) stays on the `Tutorial` object, and `GameFlow` is renamed `GameManager`; both become singletons too, and
   `GameManager` keeps the screen loop. Each singleton is cleared at `SubsystemRegistration`, because domain reload is off.
3. Content is one ScriptableObject asset per entry under `Assets/Content`, listed by `ContentDatabase`. The rules still read the
   plain `ContentData` shape, built by `ContentDatabase.ToContentData()`. `content.json` is deleted. Tools outside Unity read
   `tools/content/snapshot.json`, which the Editor writes; `ContentSnapshotTests` fails when it is stale.
4. The tutorial is a `TutorialSequence` asset: steps, lit areas, scripted trials and the lesson order.
5. dotnet is retired. Every test runs in Unity (EditMode + PlayMode). The balance reports are an Editor tool with a batch entry.
   CI runs Unity tests through game-ci, which needs the `UNITY_LICENSE`, `UNITY_EMAIL` and `UNITY_PASSWORD` secrets on every PR
   (pending the owner's confirmation (a) below).

## Consequences
- Narrows the 2026-11-11 clause "rules in plain C# with content as data": rule models stay plain C#, now in a Unity assembly, and
  content stays data, now held in assets. Supersedes the 2026-11-19 clause `rule numbers live in content.json "rules"` (rule numbers
  move to `Assets/Content/GameRules.asset`), the 2026-11-18 clause on `tools/CoreReports`, the 2026-11-19 clause that the scripted
  hands stay in Core `TutorialScript`, and, from Task 24, the 2026-11-20 `TutorialStep` line. Amends the names in the 2026-11-19
  Phase 7 line (`GameFlow`, Core `ScreenFlow`).
- Retires UL008 and CORE-PURITY (or narrows them to `Scripts/Gameplay` except `Content/`, per (b)), the dotnet `core-tests` job and
  the `MIN_TESTS` guard on dotnet. The new `unity-tests` job has a `MIN_TESTS` guard of 600 (per (c)).
- CI needs a Unity licence on every PR (pending (a)). Until the secrets exist, `unity-tests` fails on purpose, so `ci-gate` is red.
- Content diffs are now Unity YAML rather than one JSON file. `snapshot.json` keeps a readable diff in reviews.
- Rule models must not call Unity APIs; only review enforces this once UL008 is retired (unless (b) keeps a narrowed UL008).
```

- [ ] **Step 2: Owner reads the ADR and sets `## Status` to `Accepted`.** Nothing after Task 0 starts while it says `Proposed` (`docs/CLAUDE.md`: a story on a Proposed ADR is blocked). Add the ADR to `docs/architecture/tr-registry.yaml` only if `/architecture-review` asks for it; this ADR adds no TR id.

- [ ] **Step 3: Append to `docs/ATHANOR.md` §4, under "Process and architecture"**

```markdown
- 2026-11-24 (ADR-0001): `Athanor.Core` becomes the Unity assembly `Athanor.Gameplay` (rule models stay plain C#); five Manager
  singletons on `Main.unity › Managers` own content, saves, language, progress and the run; `TutorialManager` (on the Tutorial
  object) and `GameManager` (was `GameFlow`) are singletons too. Narrows "rules in plain C#" (2026-11-11) to the assembly split only;
  the balance reports are an Editor tool (supersedes the 2026-11-18 `tools/CoreReports` clause).
- 2026-11-24 (ADR-0001): content is one ScriptableObject asset per entry under `Assets/Content` (`ContentDatabase`, `GameRules`);
  `content.json` is deleted; `tools/content/snapshot.json` is the generated copy for non-Unity tools. Supersedes "rule numbers
  live in content.json rules" (2026-11-19).
- 2026-11-24 (ADR-0001): dotnet is retired; all tests run in Unity (EditMode + PlayMode); CI job `unity-tests` (game-ci) replaces
  `core-tests` and needs the Unity secrets on every PR. The tutorial is the `TutorialSequence` asset run by `TutorialManager`
  (supersedes the 2026-11-19 `TutorialScript` clause, and the 2026-11-20 line on `TutorialStep` scene objects once Task 24 lands).
```

- [ ] **Step 4: Checkpoint (owner commits)**

```
docs(adr): record the move to Unity managers and content assets

Story: R-0
```

---

### Task 1: Fix the balance reports and record their baseline

**Files:**
- Modify: `tools/CoreReports/Program.cs` (lines 35-36, 85, 103-113, 131-132, 149, 160)
- Create: `production/qa/evidence/refactor/baseline/{balance-matrix,events,roles,newcomer,balance,trace}.txt`

- [ ] **Step 1: See the build fail**

Run: `DOTNET_ROLL_FORWARD=Major dotnet build tools/CoreReports --nologo -v q 2>&1 | grep error | sort -u`
Expected: 12 errors, CS0117 on `RunState.PouchMode` and `RunState.FinalDepth`, and CS7036 on `Sim.Describe(... finalDepth)`.

- [ ] **Step 2: Fix `FinalDepth`.** It moved to `ContentDb`. Every method that uses it already has a local `db`.

```bash
perl -pi -e 's/\bRunState\.FinalDepth\b/db.FinalDepth/g' tools/CoreReports/Program.cs
```

- [ ] **Step 3: Fix `PouchMode`.** It is now per run, in `RunOptions`. In `BalanceMatrix()` replace lines 35-36:

```csharp
        RunState.PouchMode = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_POUCH"), out int mode) ? mode : 2;
        w("MATRIX pouch mode " + RunState.PouchMode);
```

with:

```csharp
        int pouch = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_POUCH"), out int mode) ? mode : 2;
        w("MATRIX pouch mode " + pouch);
```

Then add `PouchMode = pouch` to every `new RunOptions { … }` inside `BalanceMatrix()` only:

```bash
perl -0pi -e 's/(static void BalanceMatrix\(\).*?\n    \})/ my $b=$1; $b =~ s#new RunOptions \{ #new RunOptions { PouchMode = pouch, #g; $b /se' tools/CoreReports/Program.cs
```

- [ ] **Step 4: Fix `Sim.Describe`.** Look at its signature in `Athanor/Assets/Tests/EditMode/Sim.cs`: `Describe(string, List<Career>, int, Action<string>, int finalDepth)`. At the four calls on lines 103-113 of `Program.cs`, add `, db.FinalDepth` as the last argument. For example:

```csharp
            Sim.Describe("admin", admin, adminRuns, w, db.FinalDepth);
```

- [ ] **Step 5: Build again**

Run: `DOTNET_ROLL_FORWARD=Major dotnet build tools/CoreReports --nologo -v q`
Expected: `Build succeeded. 0 Error(s)`.

- [ ] **Step 6: Record the six baselines**

```bash
mkdir -p production/qa/evidence/refactor/baseline
for r in balance-matrix events roles newcomer balance trace; do
  DOTNET_ROLL_FORWARD=Major DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 \
    dotnet run --project tools/CoreReports --no-build -- "$r" > "production/qa/evidence/refactor/baseline/$r.txt"
done
wc -l production/qa/evidence/refactor/baseline/*.txt
```

Expected: six non-empty files. `balance-matrix.txt` starts with `MATRIX pouch mode 2`.

- [ ] **Step 7: Run it twice to prove the output is deterministic**

```bash
for r in balance-matrix trace; do
  DOTNET_ROLL_FORWARD=Major DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 dotnet run --project tools/CoreReports --no-build -- "$r" \
    | diff - "production/qa/evidence/refactor/baseline/$r.txt" && echo "$r stable"
done
```

Expected: `balance-matrix stable` and `trace stable`.

- [ ] **Step 8: Checkpoint (owner commits)**

```
fix(reports): build CoreReports against RunOptions.PouchMode and ContentDb.FinalDepth

Records the refactor baseline of the six balance reports.
Story: R-1
```

---

### Task 2: PlayMode harness and autoplay trace baseline

Adds a fixed seed and one `[ignite]` log line to today's `GameFlow`. Neither changes play: the seed override is 0 by default, and the line is a dev hook. Then it records what the scripted player does in `Main.unity`.

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs`
- Create: `Athanor/Assets/Tests/PlayMode/Athanor.PlayMode.Tests.asmdef`
- Create: `Athanor/Assets/Tests/PlayMode/PlayModeKit.cs`
- Create: `Athanor/Assets/Tests/PlayMode/AutoplayTraceTests.cs`
- Create: `Athanor/Assets/Tests/PlayMode/Baselines/autoplay_seed_{11,22,33}.txt` (recorded)

- [ ] **Step 1: Check the CLI flags and the APIs this harness uses**

Run: `unity test --help`
Confirm that `--mode PlayMode` exists, and note the name of the test filter flag. This plan writes `--filter`; if the CLI calls it something else, use that name.

`docs/engine-reference/unity/VERSION.md` is pinned to 6.3 LTS, older than this editor (6.4–6.5 replaced `InstanceID` with `EntityId` and started an Editor Lifecycle API). In the 6000.6 Scripting API, confirm that these are not deprecated and behave as this plan uses them: `RuntimeInitializeOnLoadMethodAttribute` with `RuntimeInitializeLoadType.SubsystemRegistration` (still the reset hook with domain reload off), `DefaultExecutionOrderAttribute`, `Object.FindFirstObjectByType<T>(FindObjectsInactive)`, `Application.logMessageReceived`, `SceneManager.LoadSceneAsync(…, LoadSceneMode.Additive)`, and Enter Play Mode Options with domain reload disabled (`EditorSettings.enterPlayModeOptions`). If one changed, use its documented replacement here and in Tasks 3 and 14, and add a line to `docs/engine-reference/unity/deprecated-apis.md`.

- [ ] **Step 2: Add the seed override to `GameFlow`.** Under `public bool Auto;` add:

```csharp
        /// <summary>0: each run gets a fresh seed. Any other value: every run uses it (PlayMode traces, bug reproduction).</summary>
        [Tooltip("0 = a fresh seed each run. Other values: every run uses this seed (PlayMode traces, bug reproduction).")]
        [SerializeField] int seedOverride;
        public int SeedOverride { get => seedOverride; set => seedOverride = value; }
```

In `PlayRun`, replace

```csharp
            Run = new RunState(Db, Journal, tutorialRun ? TutorialScript.Seed : System.Environment.TickCount * 31 + runs, 1, options);
```

with

```csharp
            int seed = seedOverride != 0 ? seedOverride : System.Environment.TickCount * 31 + runs;
            Run = new RunState(Db, Journal, tutorialRun ? TutorialScript.Seed : seed, 1, options);
```

- [ ] **Step 3: Add the `[ignite]` hook.** In `Resolve()`, directly after `Profile.IgniteResolved(outcome);`:

```csharp
            Dev.Hook("[ignite] d" + Run.Depth + " +" + outcome.Gained + " =" + Run.Potency + " " + outcome.PhaseAfter + " "
                + string.Join(",", outcome.Events.Where(e => e.Result != null).Select(e => e.Result)));
```

- [ ] **Step 4: Create the PlayMode assembly** `Athanor/Assets/Tests/PlayMode/Athanor.PlayMode.Tests.asmdef`. It is not Editor-only, which is what makes its tests PlayMode tests.

```json
{
    "name": "Athanor.PlayMode.Tests",
    "rootNamespace": "Athanor.Tests",
    "references": ["Athanor.Core", "Athanor.Game", "UnityEngine.TestRunner"],
    "includePlatforms": [],
    "excludePlatforms": [],
    "allowUnsafeCode": false,
    "overrideReferences": true,
    "precompiledReferences": ["nunit.framework.dll"],
    "autoReferenced": false,
    "defineConstraints": ["UNITY_INCLUDE_TESTS"],
    "versionDefines": [],
    "noEngineReferences": false
}
```

- [ ] **Step 5: Write the kit** `Athanor/Assets/Tests/PlayMode/PlayModeKit.cs`

```csharp
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Athanor.Core;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Athanor.Tests
{
    /// <summary>Main.unity loaded beside the test runner's scene.</summary>
    public static class MainScene
    {
        public const string Name = "Main";

        public static IEnumerator Load()
        {
            yield return SceneManager.LoadSceneAsync(Name, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Name));
            yield return null;
        }

        public static IEnumerator Unload()
        {
            var s = SceneManager.GetSceneByName(Name);
            if (s.isLoaded) yield return SceneManager.UnloadSceneAsync(s);
        }

        public static T Find<T>() where T : Object => Object.FindFirstObjectByType<T>(FindObjectsInactive.Include);
    }

    /// <summary>Takes the game's PlayerPrefs keys aside and gives the test a known start; Dispose puts the player's keys back.</summary>
    public sealed class PrefsSnapshot : IDisposable
    {
        static readonly string[] Strings = { Save.Key, Save.ProfileKey, Save.SettingsKey };
        static readonly string[] Ints = { Save.IntroKey, Save.TutorialKey };
        readonly Dictionary<string, string> strings = new Dictionary<string, string>();
        readonly Dictionary<string, int> ints = new Dictionary<string, int>();

        public PrefsSnapshot(bool introSeen, bool tutorialDone)
        {
            foreach (var k in Strings) if (PlayerPrefs.HasKey(k)) strings[k] = PlayerPrefs.GetString(k);
            foreach (var k in Ints) if (PlayerPrefs.HasKey(k)) ints[k] = PlayerPrefs.GetInt(k);
            foreach (var k in Strings.Concat(Ints)) PlayerPrefs.DeleteKey(k);
            PlayerPrefs.SetInt(Save.IntroKey, introSeen ? 1 : 0);
            PlayerPrefs.SetInt(Save.TutorialKey, tutorialDone ? 1 : 0);
            PlayerPrefs.SetString(Save.SettingsKey, new Settings { language = "en" }.ToJson());
            PlayerPrefs.Save();
        }

        public void Dispose()
        {
            foreach (var k in Strings.Concat(Ints)) PlayerPrefs.DeleteKey(k);
            foreach (var kv in strings) PlayerPrefs.SetString(kv.Key, kv.Value);
            foreach (var kv in ints) PlayerPrefs.SetInt(kv.Key, kv.Value);
            PlayerPrefs.Save();
        }
    }

    /// <summary>The gameplay lines of the dev hooks (Dev.Hook logs them), in order.</summary>
    public sealed class TraceRecorder : IDisposable
    {
        static readonly string[] Keep = { "[start]", "[intro]", "[descent]", "[choice]", "[runover]", "[ignite]", "[tut]", "[drive]", "[pause]" };
        public readonly List<string> Lines = new List<string>();

        public TraceRecorder() => Application.logMessageReceived += OnLog;
        void OnLog(string message, string stack, LogType type)
        {
            foreach (var k in Keep) if (message.StartsWith(k, StringComparison.Ordinal)) { Lines.Add(message); return; }
        }

        public bool Has(string prefix) => Lines.Any(l => l.StartsWith(prefix, StringComparison.Ordinal));

        /// <summary>The lines up to and including the first one that starts with prefix.</summary>
        public List<string> UpTo(string prefix)
        {
            int i = Lines.FindIndex(l => l.StartsWith(prefix, StringComparison.Ordinal));
            return i < 0 ? new List<string>(Lines) : Lines.Take(i + 1).ToList();
        }

        public void Dispose() => Application.logMessageReceived -= OnLog;
    }

    /// <summary>Text baselines under Tests/PlayMode/Baselines. ATHANOR_RECORD=1 writes them instead of comparing.</summary>
    public static class Baseline
    {
        static string Dir => Path.Combine(Application.dataPath, "Tests/PlayMode/Baselines");

        public static void Check(string file, IReadOnlyList<string> lines)
        {
            var path = Path.Combine(Dir, file);
            if (Environment.GetEnvironmentVariable("ATHANOR_RECORD") == "1")
            {
                Assert.Greater(lines.Count, 0, "nothing to record for " + file);
                Directory.CreateDirectory(Dir);
                File.WriteAllLines(path, lines);
                Assert.IsTrue(File.Exists(path), "recorded " + path);
                return;
            }
            Assert.IsTrue(File.Exists(path), "no baseline " + path + ": record it with ATHANOR_RECORD=1 on the commit before the change");
            var want = File.ReadAllLines(path);
            int n = Math.Min(want.Length, lines.Count);
            for (int i = 0; i < n; i++) Assert.AreEqual(want[i], lines[i], "line " + (i + 1) + " of " + file);
            Assert.AreEqual(want.Length, lines.Count, "line count of " + file);
        }
    }
}
```

- [ ] **Step 6: Write the trace test** `Athanor/Assets/Tests/PlayMode/AutoplayTraceTests.cs`

```csharp
using System.Collections;
using System.Collections.Generic;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Athanor.Tests
{
    /// <summary>Main.unity played by the scripted player (F9) on a fixed seed: the gameplay hooks equal the recorded run.</summary>
    public class AutoplayTraceTests
    {
        const float Speed = 8f, LimitSeconds = 900f;

        sealed class Result { public bool Ended; public List<string> Lines; }

        static IEnumerator Play(int seed, Result result)
        {
            var prefs = new PrefsSnapshot(introSeen: true, tutorialDone: true);
            var trace = new TraceRecorder();
            try
            {
                yield return MainScene.Load();
                var game = MainScene.Find<GameFlow>();
                game.SeedOverride = seed;
                game.Auto = true;
                Time.timeScale = Speed;
                float limit = Time.realtimeSinceStartup + LimitSeconds;
                while (!trace.Has("[runover]") && Time.realtimeSinceStartup < limit) yield return null;
                result.Ended = trace.Has("[runover]");
                result.Lines = trace.UpTo("[runover]");
            }
            finally { Time.timeScale = 1f; trace.Dispose(); prefs.Dispose(); }
            yield return MainScene.Unload();
        }

        [UnityTest, Timeout(1200000)]
        public IEnumerator Autoplay_Seed11_MatchesBaseline()
        {
            // Arrange
            var r = new Result();
            // Act
            yield return Play(11, r);
            // Assert
            Assert.IsTrue(r.Ended, "the run did not reach its summary in time");
            Baseline.Check("autoplay_seed_11.txt", r.Lines);
        }

        [UnityTest, Timeout(1200000)]
        public IEnumerator Autoplay_Seed22_MatchesBaseline()
        {
            // Arrange
            var r = new Result();
            // Act
            yield return Play(22, r);
            // Assert
            Assert.IsTrue(r.Ended, "the run did not reach its summary in time");
            Baseline.Check("autoplay_seed_22.txt", r.Lines);
        }

        [UnityTest, Timeout(1200000)]
        public IEnumerator Autoplay_Seed33_MatchesBaseline()
        {
            // Arrange
            var r = new Result();
            // Act
            yield return Play(33, r);
            // Assert
            Assert.IsTrue(r.Ended, "the run did not reach its summary in time");
            Baseline.Check("autoplay_seed_33.txt", r.Lines);
        }
    }
}
```

- [ ] **Step 7: Run it without baselines to see it fail**

Run: `unity test Athanor --mode PlayMode --filter AutoplayTraceTests`
Expected: 3 failed with `no baseline …autoplay_seed_11.txt`. That proves the run reaches its summary: a run that did not finish fails earlier, on `the run did not reach its summary in time`.

- [ ] **Step 8: Record, then run twice**

```bash
ATHANOR_RECORD=1 unity test Athanor --mode PlayMode --filter AutoplayTraceTests
unity test Athanor --mode PlayMode --filter AutoplayTraceTests
unity test Athanor --mode PlayMode --filter AutoplayTraceTests
```

Expected: the record run passes 3/3, and both compare runs pass 3/3. If a compare run fails, the trace is not deterministic. Find the line that differs, remove the cause (usually a hook that prints a time or a frame count), and record again. Do not loosen the comparison.

- [ ] **Step 9: Run EditMode** (`GameFlow` changed)

Run: `unity test Athanor --mode EditMode`
Expected: 0 failed.

- [ ] **Step 10: Checkpoint (owner commits)**

```
test(playmode): record seeded autoplay traces of Main.unity

Adds GameFlow.seedOverride (0 = unchanged) and an [ignite] dev hook.
Story: R-2
```

---

### Task 3: Tutorial auto-drive and tutorial trace baseline

Records the tutorial as it plays today, so that the rebuilt tutorial in Stage 5 can be compared line for line.

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs`
- Modify: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs`
- Modify: `Athanor/Assets/Scripts/Game/Ui/TitleView.cs`
- Create: `Athanor/Assets/Tests/PlayMode/TutorialTraceTests.cs`, `Baselines/tutorial_trace.txt`

- [ ] **Step 1: `TitleView`.** Under `public void ClearBegin() => BeginPressed = false;` add:

```csharp
        /// <summary>Presses Begin as a click would (PlayMode tests).</summary>
        public void RequestBegin() => PressBegin();
```

- [ ] **Step 2: `GameFlow`.** Add a public journal request under `void OpenPause() => pause.Open();`:

```csharp
        /// <summary>Opens the Journal as the bench's Journal button would (the tutorial's auto-drive).</summary>
        public void RequestJournal() => OpenJournal();
```

In `Descend()`, replace

```csharp
            yield return choice.Choose(offer, id => picked = id, Auto ? runTiming.autoChoiceWait : -1);
```

with

```csharp
            bool autoPick = Auto || (tut != null && tut.AutoDrive);
            yield return choice.Choose(offer, id => picked = id, autoPick ? runTiming.autoChoiceWait : -1);
```

In `Update()`, directly before `if (!Auto) return;`:

```csharp
            if (tut != null && tut.AutoDrive) { reaction.RequestSkip(); story.RequestSkip(); }
```

- [ ] **Step 3: `TutorialController`.** Add the field under `public readonly List<long> Shown = new List<long>();`:

```csharp
        /// <summary>Tests and debugging only: read lines advance by themselves and the scripted lays, Ignites and Journal moves are made for the player.</summary>
        [Tooltip("Tests and debugging only: read lines advance by themselves; the scripted lays, Ignites and Journal moves are made for the player.")]
        public bool AutoDrive;
```

In `SayText` and in `Broken`, directly after the `yield return null;` that comes before `while (!advance) yield return null;`, add:

```csharp
            if (AutoDrive) advance = true;
```

Replace `Await` with a version that takes a bot:

```csharp
        /// <summary>A line and a lock: clicks in pass-through areas go to the game, every other click pulses the rim once. Ends when done(). bot plays the move in AutoDrive.</summary>
        IEnumerator Await(string id, Func<bool> done, int hub = -1, Action bot = null)
        {
            var step = Step(id);
            Show(step, Loc.T(step.textKey), false, null, hub);
            while (!done()) { if (AutoDrive && bot != null) bot(); yield return null; }
            Hide();
        }
```

Add the lay bot under `Laid`:

```csharp
        /// <summary>AutoDrive: lays the next scripted material of the first n, one per frame, as a player would.</summary>
        void Lay(TutorialTrial t, int n)
        {
            var run = f.Run;
            for (int i = 0; i < n; i++)
            {
                var l = t.Lay[i];
                if (run.Bench.Tiles[l.Tile] == l.Id) continue;
                for (int h = 0; h < run.Hand.Count; h++)
                    if (run.Hand[h] == l.Id) { if (run.Place(h, l.Tile)) f.Bench.Refresh(); break; }
                return;
            }
        }
```

`IgniteStep` becomes:

```csharp
        IEnumerator IgniteStep(string id)
        {
            yield return Await(id, () => f.State == FlowState.Resolving, bot: f.Ignite);
            while (f.State == FlowState.Resolving) yield return null;
        }
```

In `Drive()`, give each waiting step its bot:

```csharp
            yield return Await("1.2", () => Laid(TutorialScript.Reaction, 1), bot: () => Lay(TutorialScript.Reaction, 1));
            yield return Await("1.3", () => Laid(TutorialScript.Reaction, 2), bot: () => Lay(TutorialScript.Reaction, 2));
            ...
            yield return Await("2.1", () => Laid(TutorialScript.Miss, 2), bot: () => Lay(TutorialScript.Miss, 2));
            ...
            yield return Await("2.4", () => f.State == FlowState.Journal, bot: f.RequestJournal);
            ...
            while (f.State == FlowState.Journal) { if (AutoDrive) f.JournalView.Close(); yield return null; }
            ...
            yield return Await("3.1", () => Laid(TutorialScript.Chain, 3), bot: () => Lay(TutorialScript.Chain, 3));
            ...
                yield return Await("5.4", () => Laid(ct, ct.Lay.Length), bot: () => Lay(ct, ct.Lay.Length));
```

(The `...` lines are the existing lines between them, unchanged.)

- [ ] **Step 4: Write the test** `Athanor/Assets/Tests/PlayMode/TutorialTraceTests.cs`

```csharp
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Athanor.Tests
{
    /// <summary>The first-run tutorial auto-driven from Begin to line 5.5: its lines and popup scores equal the recorded run.</summary>
    public class TutorialTraceTests
    {
        const float Speed = 8f, LimitSeconds = 900f;

        [UnityTest, Timeout(1200000)]
        public IEnumerator Tutorial_AutoDriven_MatchesBaseline()
        {
            // Arrange
            var prefs = new PrefsSnapshot(introSeen: true, tutorialDone: false);
            var trace = new TraceRecorder();
            bool finished = false; List<string> lines = null;
            try
            {
                yield return MainScene.Load();
                var game = MainScene.Find<GameFlow>();
                var tutorial = MainScene.Find<TutorialController>();
                tutorial.AutoDrive = true;
                Time.timeScale = Speed;
                while (game.State != FlowState.Title) yield return null;
                // Act
                MainScene.Find<TitleView>().RequestBegin();
                float limit = Time.realtimeSinceStartup + LimitSeconds;
                while (!tutorial.Finished && Time.realtimeSinceStartup < limit) yield return null;
                finished = tutorial.Finished;
                lines = trace.Lines.Concat(new[] { "[shown] " + string.Join(" ", tutorial.Shown) }).ToList();
            }
            finally { Time.timeScale = 1f; trace.Dispose(); prefs.Dispose(); }
            yield return MainScene.Unload();
            // Assert
            Assert.IsTrue(finished, "the tutorial did not reach line 5.5 in time");
            Baseline.Check("tutorial_trace.txt", lines);
        }
    }
}
```

- [ ] **Step 5: Record, then compare twice**

```bash
ATHANOR_RECORD=1 unity test Athanor --mode PlayMode --filter TutorialTraceTests
unity test Athanor --mode PlayMode --filter TutorialTraceTests
unity test Athanor --mode PlayMode --filter TutorialTraceTests
```

Expected: 1/1 passes each time. Open `Baselines/tutorial_trace.txt`. It must contain the `[tut]` lines for 1.1 … 5.5 and end with `[shown] 40 40 240 …`, the scores that `ScriptedTrials_Shown_Equal40_0_40_240` pins.

- [ ] **Step 6: EditMode and the autoplay traces still pass**

Run: `unity test Athanor --mode EditMode && unity test Athanor --mode PlayMode`
Expected: 0 failed.

- [ ] **Step 7: Checkpoint (owner commits)**

```
test(playmode): record the auto-driven tutorial trace

Adds TutorialController.AutoDrive (off by default), GameFlow.RequestJournal, TitleView.RequestBegin.
Story: R-3
```

---

# Stage 1: The Core assembly goes away

### Task 4: Delete the unused Data prototype

**Files:**
- Delete: `Athanor/Assets/Scripts/Data/` (+ `Data.meta`), `Athanor/Assets/Editor/TempImport/` (+ `TempImport.meta`)

- [ ] **Step 1: Prove nothing uses them**

Run: `grep -rln "Athanor\.Data\|GameConfigSO\|MaterialSO\|RulesSO\|BenchLayoutsSO" Athanor/Assets --include="*.cs" --include="*.asmdef" --include="*.asset" --include="*.unity" --include="*.prefab" | grep -v "Scripts/Data/\|Editor/TempImport/"`
Expected: no output.

- [ ] **Step 2: Delete**

```bash
git rm -r Athanor/Assets/Scripts/Data Athanor/Assets/Scripts/Data.meta Athanor/Assets/Editor/TempImport Athanor/Assets/Editor/TempImport.meta
```

- [ ] **Step 3: Verify**

Run: `unity test Athanor --mode EditMode && DOTNET_ROLL_FORWARD=Major dotnet test tools/CoreTests --nologo`
Expected: 0 failed in both.

- [ ] **Step 4: Checkpoint (owner commits)**

```
chore: remove the unused Athanor.Data prototype and its importer

Story: R-4
```

---

### Task 5: `Athanor.Core` becomes `Athanor.Gameplay`

**Files:**
- Move: `Athanor/Assets/Scripts/Core/` → `Athanor/Assets/Scripts/Gameplay/`
- Move: `Athanor.Core.asmdef` → `Athanor/Assets/Scripts/Gameplay/Athanor.Gameplay.asmdef`
- Move: `Athanor/Assets/Tests/EditMode/Athanor.Core.Tests.asmdef` → `Athanor.Gameplay.Tests.asmdef`
- Modify: every `.cs` that says `Athanor.Core` (80 files), `Athanor.Game.asmdef`, `Athanor.EditorShared.asmdef`, `Athanor.Game.Tests.asmdef`, `Athanor.PlayMode.Tests.asmdef`, `tools/CoreTests/CoreTests.csproj`, `tools/CoreReports/CoreReports.csproj`, `tools/lint/lint.config.json`, `tools/ci/check_repo.py`, `tools/vault/build_vault.py`

- [ ] **Step 1: Move the folder and the asmdef, keeping the GUIDs**

```bash
cd Athanor/Assets
git mv Scripts/Core Scripts/Gameplay
git mv Scripts/Core.meta Scripts/Gameplay.meta
git mv Scripts/Gameplay/Athanor.Core.asmdef Scripts/Gameplay/Athanor.Gameplay.asmdef
git mv Scripts/Gameplay/Athanor.Core.asmdef.meta Scripts/Gameplay/Athanor.Gameplay.asmdef.meta
git mv Tests/EditMode/Athanor.Core.Tests.asmdef Tests/EditMode/Athanor.Gameplay.Tests.asmdef
git mv Tests/EditMode/Athanor.Core.Tests.asmdef.meta Tests/EditMode/Athanor.Gameplay.Tests.asmdef.meta
cd ../..
```

- [ ] **Step 2: Write `Scripts/Gameplay/Athanor.Gameplay.asmdef`.** It no longer forbids UnityEngine; Task 10 puts ScriptableObjects in it.

```json
{
    "name": "Athanor.Gameplay",
    "rootNamespace": "Athanor.Gameplay",
    "references": [],
    "includePlatforms": [],
    "excludePlatforms": [],
    "allowUnsafeCode": false,
    "overrideReferences": false,
    "precompiledReferences": [],
    "autoReferenced": true,
    "defineConstraints": [],
    "versionDefines": [],
    "noEngineReferences": false
}
```

- [ ] **Step 3: Rename the namespace and the assembly references**

```bash
grep -rlZ -e "Athanor\.Core" Athanor/Assets tools --include="*.cs" --include="*.asmdef" --include="*.csproj" \
  | xargs -0 perl -pi -e 's/\bAthanor\.Core\b/Athanor.Gameplay/g'
grep -rlZ -E "(^|[^.A-Za-z])Core\.Bench\b" Athanor/Assets --include="*.cs" \
  | xargs -0 perl -pi -e 's/(?<![\w.])Core\.Bench\b/Gameplay.Bench/g'
perl -pi -e 's#Scripts/Core/#Scripts/Gameplay/#g' tools/CoreTests/CoreTests.csproj tools/CoreReports/CoreReports.csproj
grep -rnE "(^|[^.A-Za-z])Core\.[A-Z]" Athanor/Assets --include="*.cs"
```

Expected from the last grep: no output. If a line appears, replace its `Core.` with `Gameplay.` the same way.

- [ ] **Step 4: Retire the purity rules**

In `tools/lint/lint.config.json`, set `"pure_logic_paths": []` and change `_pure_logic_paths` to `"ADR-0001: Athanor.Gameplay is a Unity assembly; no pure-logic path is enforced."`.

In `tools/ci/check_repo.py`, delete the `CORE = …` line, the whole `check_core_purity` function, and its `"CORE-PURITY": check_core_purity,` entry.

In `tools/vault/build_vault.py` line 200, change `(UNITY / "Scripts" / "Core", "Core")` to `(UNITY / "Scripts" / "Gameplay", "Gameplay")`. On line 240, change `("Core", "Game", "Editor")` to `("Gameplay", "Game", "Editor")`.

- [ ] **Step 5: Compile and run everything**

```bash
unity test Athanor --mode EditMode
unity test Athanor --mode PlayMode
DOTNET_ROLL_FORWARD=Major dotnet test tools/CoreTests --nologo
DOTNET_ROLL_FORWARD=Major dotnet build tools/CoreReports --nologo -v q
for r in balance-matrix events roles newcomer balance trace; do
  DOTNET_ROLL_FORWARD=Major DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 dotnet run --project tools/CoreReports --no-build -- "$r" \
    | diff -q - "production/qa/evidence/refactor/baseline/$r.txt" || echo "DIFF $r"
done
tools/ci/run.sh all
python3 tools/vault/build_vault.py
```

Expected: every test run reports 0 failed, `dotnet test` shows 369 passed, no `DIFF` line, and `run.sh all` exits 0. The vault rewrites its Core notes as Gameplay notes. Stage those changes.

- [ ] **Step 6: Checkpoint (owner commits)**

```
refactor!: rename Athanor.Core to the Unity assembly Athanor.Gameplay

ADR-0001. Namespace Athanor.Core -> Athanor.Gameplay; UL008 and CORE-PURITY retired.
Story: R-5
```

---

# Stage 2: dotnet is retired

### Task 6: Tests and the simulation run only in Unity

**Files:**
- Move: `Athanor/Assets/Tests/EditMode/Sim.cs` → `Athanor/Assets/Editor/Simulation/Sim.cs`
- Create: `Athanor/Assets/Editor/Simulation/Athanor.Simulation.asmdef`
- Modify: `Athanor/Assets/Tests/EditMode/TestContent.cs`, `LangTests.cs`, `Athanor.Gameplay.Tests.asmdef`

- [ ] **Step 1: Move `Sim` into an Editor-only assembly, so it never ships in the web build**

```bash
mkdir -p Athanor/Assets/Editor/Simulation
git mv Athanor/Assets/Tests/EditMode/Sim.cs Athanor/Assets/Editor/Simulation/Sim.cs
git mv Athanor/Assets/Tests/EditMode/Sim.cs.meta Athanor/Assets/Editor/Simulation/Sim.cs.meta
```

`Athanor/Assets/Editor/Simulation/Athanor.Simulation.asmdef`:

```json
{
    "name": "Athanor.Simulation",
    "rootNamespace": "Athanor.Tests",
    "references": ["Athanor.Gameplay"],
    "includePlatforms": ["Editor"],
    "excludePlatforms": [],
    "allowUnsafeCode": false,
    "overrideReferences": false,
    "precompiledReferences": [],
    "autoReferenced": true,
    "defineConstraints": [],
    "versionDefines": [],
    "noEngineReferences": false
}
```

Make `Sim` public if it is not already (`public static class Sim`). Change its summary to: *"The simulated players behind the balance tests and the Editor balance reports."*

- [ ] **Step 2: Add the reference to the test assemblies.** In `Tests/EditMode/Athanor.Gameplay.Tests.asmdef` and `Tests/EditModeGame/Athanor.Game.Tests.asmdef`, add `"Athanor.Simulation"` and `"Athanor.EditorShared"` to `references` if they are missing.

- [ ] **Step 3: Remove the dotnet branches.** In `TestContent.Load()`, keep only the Unity branch:

```csharp
        public static ContentData Load()
        {
            var text = UnityEngine.Resources.Load<UnityEngine.TextAsset>("content").text;
            return UnityEngine.JsonUtility.FromJson<ContentData>(text);
        }
```

In `LangTests.cs`, delete every `#if !UNITY_5_3_OR_NEWER … #endif` block. For each `#if UNITY_5_3_OR_NEWER … #else … #endif`, keep the first branch and drop the `#else` branch. Then remove the remaining `#if UNITY_5_3_OR_NEWER` and `#endif` lines.

Run: `grep -rn "UNITY_5_3_OR_NEWER" Athanor/Assets`
Expected: no output.

- [ ] **Step 4: Verify**

Run: `unity test Athanor --mode EditMode`
Expected: 0 failed. The Gameplay.Tests count is the same as before the move (369 in Unity's count of the former Core tests).

- [ ] **Step 5: Checkpoint (owner commits)**

```
refactor(test): run the rule tests and the simulation only in Unity

Story: R-6
```

---

### Task 7: Balance reports in the Editor

**Files:**
- Move: `tools/CoreReports/Program.cs` → `Athanor/Assets/Editor/Balance/BalanceReports.cs`
- No test file: the reports assert nothing (ATHANOR.md §5). The byte-identical diff in Step 3 is the check.

- [ ] **Step 1: Move the file**

```bash
mkdir -p Athanor/Assets/Editor/Balance
git mv tools/CoreReports/Program.cs Athanor/Assets/Editor/Balance/BalanceReports.cs
```

- [ ] **Step 2: Turn it into an Editor tool.** Edits in `BalanceReports.cs`:

1. Usings: replace the first using lines with

```csharp
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Athanor.Gameplay;
using Athanor.Tests;
using UnityEditor;
using UnityEngine;
```

2. The class: replace `static class Program` with `public static class BalanceReports`. Replace the summary's first line with `/// The balance reports (Athanor > Balance, or BalanceReports.RunBatch). They print numbers and assert nothing.`

3. Replace `static int Main(string[] args)` and its `switch` with:

```csharp
    static TextWriter Out = TextWriter.Null;

    /// <summary>Writes one report to output; returns 0, or 2 for an unknown report name.</summary>
    public static int Run(string report, TextWriter output)
    {
        var culture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        Out = output;
        try
        {
            switch (report)
            {
                case "balance-matrix": BalanceMatrix(); return 0;
                case "events": EventReport(); return 0;
                case "roles": RoleReport(); return 0;
                case "newcomer": NewcomerTrace(); return 0;
                case "balance": BalanceReport(); return 0;
                case "trace": TraceRun(); return 0;
                default: Debug.LogError(Usage); return 2;
            }
        }
        finally { Out = TextWriter.Null; CultureInfo.CurrentCulture = culture; }
    }

    /// <summary>unity run Athanor -- -executeMethod BalanceReports.RunBatch -report balance-matrix -out /tmp/m.txt</summary>
    public static void RunBatch()
    {
        var args = Environment.GetCommandLineArgs();
        string Arg(string name) { int i = Array.IndexOf(args, name); return i >= 0 && i + 1 < args.Length ? args[i + 1] : null; }
        string report = Arg("-report"), outPath = Arg("-out");
        int code;
        if (outPath != null) using (var w = new StreamWriter(outPath) { NewLine = "\n" }) code = Run(report, w);
        else { var w = new StringWriter { NewLine = "\n" }; code = Run(report, w); Debug.Log(w.ToString()); }
        EditorApplication.Exit(code);
    }

    static void Menu(string report)
    {
        var w = new StringWriter { NewLine = "\n" };
        Run(report, w);
        var path = Path.GetFullPath("Temp/balance-" + report + ".txt");
        File.WriteAllText(path, w.ToString());
        Debug.Log("[balance] " + report + " written to " + path + "\n" + w);
        EditorUtility.RevealInFinder(path);
    }

    [MenuItem("Athanor/Balance/Balance Matrix")] static void MenuMatrix() => Menu("balance-matrix");
    [MenuItem("Athanor/Balance/Events")] static void MenuEvents() => Menu("events");
    [MenuItem("Athanor/Balance/Roles")] static void MenuRoles() => Menu("roles");
    [MenuItem("Athanor/Balance/Newcomer Trace")] static void MenuNewcomer() => Menu("newcomer");
    [MenuItem("Athanor/Balance/Balance")] static void MenuBalance() => Menu("balance");
    [MenuItem("Athanor/Balance/Trace One Run")] static void MenuTrace() => Menu("trace");

    static ContentData Content() => JsonUtility.FromJson<ContentData>(File.ReadAllText("Assets/Resources/content.json"));
```

4. Output: `perl -pi -e 's/Console\.WriteLine/Out.WriteLine/g; s/Console\.Out\b/Out/g; s/Console\.Error\.WriteLine/Debug.LogError/g' Athanor/Assets/Editor/Balance/BalanceReports.cs`

5. Content: `perl -pi -e 's/TestContent\.Load\(\)/Content()/g' Athanor/Assets/Editor/Balance/BalanceReports.cs`

The `ATHANOR_*` environment variables stay as they are. A batch run inherits them from the shell.

- [ ] **Step 3: Compare with the baseline**

```bash
mkdir -p Temp/reports
for r in balance-matrix events roles newcomer balance trace; do
  unity run Athanor -- -executeMethod BalanceReports.RunBatch -report "$r" -out "$PWD/Temp/reports/$r.txt"
  diff -q "Temp/reports/$r.txt" "production/qa/evidence/refactor/baseline/$r.txt" || echo "DIFF $r"
done
```

Expected: no `DIFF` line. If one differs, run `diff` on it. A difference in number format means a `ToString()` that ignores `CurrentCulture`, so give it `CultureInfo.InvariantCulture`. A difference in line endings means `NewLine` is not `"\n"`.

- [ ] **Step 4: Checkpoint (owner commits)**

```
refactor(reports): move the balance reports into the Editor with a batch entry

Byte-identical with production/qa/evidence/refactor/baseline.
Story: R-7
```

---

### Task 8: Delete the dotnet projects and move CI to Unity tests

**Files:**
- Delete: `tools/CoreTests/`, `tools/CoreReports/`
- Modify: `tools/ci/run.sh`, `.github/workflows/ci.yml`, `tools/ci/check_repo.py`, `project.yaml`

- [ ] **Step 1: Check the action's contract.** Read the README of `game-ci/unity-test-runner` v4 (https://game.ci/docs/github/test-runner). Confirm `testMode: all` and the output `artifactsPath`. If either is named differently, use the documented name below.

- [ ] **Step 2: Delete**

```bash
git rm -r tools/CoreTests tools/CoreReports
rm -rf tools/CoreTests tools/CoreReports   # bin/ and obj/ are untracked build output
```

- [ ] **Step 3: `tools/ci/run.sh`.** Delete the `MIN_TESTS` line and the `core()` function. Add:

```bash
# Local only: needs the unity CLI and a closed Editor. CI runs the same suites through game-ci (job unity-tests).
unitytests() {
  unity test Athanor --mode EditMode
  unity test Athanor --mode PlayMode
}
```

Change the header comment's `core` line to `#   tools/ci/run.sh unity     EditMode + PlayMode in Unity (local; CI uses game-ci)`. Then change the `case` to:

```bash
case "${1:-all}" in
  lint) lint ;;
  repo) repo ;;
  unity) unitytests ;;
  rotate) rotate ;;
  commits) shift; commits "$@" ;;
  all) lint; repo; rotate ;;
  *) echo "unknown job: $1" >&2; exit 2 ;;
esac
```

- [ ] **Step 4: `.github/workflows/ci.yml`.** Replace the whole `core-tests:` job with:

```yaml
  # Rule and screen tests (EditMode + PlayMode). ADR-0001: needs a Unity licence on every PR;
  # without the secrets it fails on purpose, like the release gate.
  unity-tests:
    name: unity-tests
    runs-on: ubuntu-latest
    timeout-minutes: 90
    env:
      UNITY_LICENSE: ${{ secrets.UNITY_LICENSE }}
      UNITY_EMAIL: ${{ secrets.UNITY_EMAIL }}
      UNITY_PASSWORD: ${{ secrets.UNITY_PASSWORD }}
      MIN_TESTS: '600'
    steps:
      - name: Unity secrets are set
        run: |
          [ -n "$UNITY_LICENSE" ] || { echo "::error::UNITY_LICENSE is not set: the Unity tests cannot run"; exit 1; }
      - uses: actions/checkout@v4
        with:
          lfs: true
      - uses: actions/cache@v4
        with:
          path: Athanor/Library
          key: Library-tests-${{ hashFiles('Athanor/Packages/manifest.json', 'Athanor/ProjectSettings/ProjectVersion.txt') }}
      - name: EditMode and PlayMode tests
        id: tests
        uses: game-ci/unity-test-runner@v4
        with:
          projectPath: Athanor
          unityVersion: auto
          testMode: all
          githubToken: ${{ secrets.GITHUB_TOKEN }}
      - name: Enough tests passed (guards against silently dropped tests)
        run: |
          PASSED=$(cat "${{ steps.tests.outputs.artifactsPath }}"/*.xml | grep -o '<test-case [^>]*result="Passed"' | wc -l)
          echo "passed: $PASSED (minimum $MIN_TESTS)"
          [ "$PASSED" -ge "$MIN_TESTS" ] || { echo "::error::only $PASSED tests passed, expected at least $MIN_TESTS"; exit 1; }
```

In `ci-gate`, change `needs: [lint, repo-guard, core-tests]` to `needs: [lint, repo-guard, unity-tests]`. In `release-gate`, change the step name `Unity EditMode tests (Core and Game layers)` to `Unity tests (EditMode + PlayMode)` and `testMode: EditMode` to `testMode: all`.

- [ ] **Step 5: `tools/ci/check_repo.py`.** In the BUILD-OUTPUT patterns, delete `r"^tools/Core(Tests|Reports)/(bin|obj)/",`.

- [ ] **Step 6: `project.yaml`.** Change `testing.framework` to `"NUnit (Unity Test Framework: EditMode + PlayMode)"`. Under `commands:`, add `test_play: "unity test Athanor --mode PlayMode"`.

- [ ] **Step 7: Verify**

```bash
tools/ci/run.sh all
tools/ci/run.sh unity
python3 tools/vault/build_vault.py
grep -rn "dotnet\|CoreTests\|CoreReports" tools .github project.yaml CLAUDE.md | grep -v "^tools/art"
```

Expected: both `run.sh` calls exit 0. The grep lists only `CLAUDE.md` lines, which Task 28 rewrites.

- [ ] **Step 8: Owner checkpoint.** Add `UNITY_LICENSE`, `UNITY_EMAIL` and `UNITY_PASSWORD` to the repository secrets. `ci-gate` stays red on every PR until they exist.

- [ ] **Step 9: Checkpoint (owner commits)**

```
ci!: replace dotnet core-tests with Unity EditMode + PlayMode tests

ADR-0001. Deletes tools/CoreTests and tools/CoreReports; unity-tests needs the Unity secrets.
Story: R-8
```

---

# Stage 3: Content as ScriptableObject assets

### Task 9: Inspector enums and their name tables

**Files:**
- Create: `Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs`
- Test: `Athanor/Assets/Tests/EditMode/ContentEnumTests.cs`

- [ ] **Step 1: Write the failing test** `Tests/EditMode/ContentEnumTests.cs`

```csharp
using System;
using System.Linq;
using Athanor.Gameplay;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>The Inspector enums of the content assets spell exactly the names the rules read.</summary>
    public class ContentEnumTests
    {
        static string[] Names<T>() where T : Enum => Enum.GetNames(typeof(T));

        [Test]
        public void CatalystEffect_Names_EqualEffectsAll()
        {
            // Arrange
            var want = Effects.All;
            // Act
            var got = Names<CatalystEffect>();
            // Assert
            CollectionAssert.AreEqual(want, got);
        }

        [Test]
        public void AspectEffect_Names_EqualAspectEffectsAll()
        {
            // Arrange / Act / Assert
            CollectionAssert.AreEqual(AspectEffects.All, Names<AspectEffect>());
        }

        [Test]
        public void EventOp_Names_EqualOpsAll()
        {
            // Arrange / Act / Assert
            CollectionAssert.AreEqual(Ops.All, Names<EventOp>());
        }

        [Test]
        public void EventKind_Names_EqualOpsTypes()
        {
            // Arrange / Act / Assert
            CollectionAssert.AreEqual(Ops.Types, Names<EventKind>());
        }

        [Test]
        public void EventNeed_NamesAfterNone_EqualOpsNeeds()
        {
            // Arrange / Act / Assert
            CollectionAssert.AreEqual(Ops.Needs, Names<EventNeed>().Skip(1).ToArray());
        }

        [Test]
        public void RarityId_NamesAfterUnset_EqualRaritiesAll()
        {
            // Arrange / Act / Assert
            CollectionAssert.AreEqual(Rarities.All, Names<RarityId>().Skip(1).ToArray());
        }

        [Test]
        public void KindOrNone_NamesAfterNone_EqualKind()
        {
            // Arrange / Act / Assert
            CollectionAssert.AreEqual(Names<Kind>(), Names<KindOrNone>().Skip(1).ToArray());
        }

        [Test]
        public void GoalStat_EveryValue_MapsToItsStatId()
        {
            // Arrange
            var values = (GoalStat[])Enum.GetValues(typeof(GoalStat));
            // Act
            var mapped = values.Select(ContentNames.Stat).ToArray();
            // Assert
            CollectionAssert.AreEqual(StatIds.All, mapped);
            foreach (var id in StatIds.All) Assert.AreEqual(id, ContentNames.Stat(ContentNames.ParseStat(id)));
        }

        [Test]
        public void ContentNames_NoneValues_WriteEmptyStrings()
        {
            // Arrange / Act / Assert
            Assert.AreEqual("", ContentNames.Rarity(RarityId.Unset));
            Assert.AreEqual("", ContentNames.Kind(KindOrNone.None));
            Assert.AreEqual("", ContentNames.Need(EventNeed.None));
            Assert.AreEqual(RarityId.Unset, ContentNames.Parse("", RarityId.Unset));
            Assert.AreEqual(RarityId.Rare, ContentNames.Parse("Rare", RarityId.Unset));
            Assert.AreEqual("starter", ContentNames.Reward(RewardKind.Starter));
            Assert.AreEqual(RewardKind.Catalyst, ContentNames.ParseReward("catalyst"));
        }
    }
}
```

- [ ] **Step 2: Run it and see it fail**

Run: `unity test Athanor --mode EditMode --filter ContentEnumTests`
Expected: compile error, `The type or namespace name 'CatalystEffect' could not be found`.

- [ ] **Step 3: Write `Scripts/Gameplay/Content/ContentEnums.cs`**

```csharp
using System;

namespace Athanor.Gameplay
{
    // The Inspector shows these; the rules read the strings ContentNames writes. ContentEnumTests pin every list
    // to the name tables in ContentData.cs, so a new effect means a new line in both places.

    /// <summary>Unset (no rarity written) then Rarities.All in order.</summary>
    public enum RarityId { Unset, Common, Uncommon, Rare, Epic, Legendary, Mythic, WorldSeed }

    /// <summary>None (no kind written) then Kind in order. A catalyst's kind filter.</summary>
    public enum KindOrNone { None, Ore, Metal, Crystal, Powder, Potion, Plant, Beast }

    /// <summary>Effects.All in order.</summary>
    public enum CatalystEffect
    {
        KindDouble, CentreTwice, FirstTriple, Diagonals, ExtraTrial, ExtraHand, DiscoveryTriple, EmptyTileBonus, ChainPlus,
        ComboBonus, PairBonus, FlatPerReaction, TierDouble, StarterDouble, ExtraRedraw, HeatPlus, Salvage, ChainFrom,
        DepthScale, FreeDiscovery, AllScale, SecondChance,
    }

    /// <summary>AspectEffects.All in order.</summary>
    public enum AspectEffect { Doubled, Anchor, Heated, Echo, Attuned, Seed, Fallow, Heavy, Cold, Volatile, Greedy, Bridge, OpenBench, Kiln, Crossroads, Pivot }

    public enum AspectScope { Tile, Link, Bench }
    public enum AspectGroup { Cheap, Engine }

    /// <summary>Ops.All in order (EffectData.op).</summary>
    public enum EventOp { Trials, Hand, Redraws, RiShift, GiveDiscovery, LoseRack, LoseCard, FirstTriple, Hint, BlockTile, HeatTile, CatalystUpgrade, CatalystGift, Nothing }

    /// <summary>Ops.Types in order. Not called EventType: UnityEngine has one.</summary>
    public enum EventKind { Choice, Gamble, Find, Shrine, Hazard, Boon, Lore }

    /// <summary>None (no requirement, written "") then Ops.Needs in order.</summary>
    public enum EventNeed { None, Rack, Catalyst, Trial, Redraw }

    /// <summary>StatIds.All in order (the ids are camelCase strings; ContentNames maps them).</summary>
    public enum GoalStat { RunsStarted, RunsWon, WonNoRedraw, DepthReached, Ignites, ChainLongest, ComboLargest, BestIgnite, JournalEntries, JournalComplete }

    public enum RewardKind { Starter, Catalyst }

    /// <summary>Enum values to and from the strings of ContentData.</summary>
    public static class ContentNames
    {
        public static string Rarity(RarityId r) => r == RarityId.Unset ? "" : r.ToString();
        public static string Kind(KindOrNone k) => k == KindOrNone.None ? "" : k.ToString();
        public static string Need(EventNeed n) => n == EventNeed.None ? "" : n.ToString();
        public static string Stat(GoalStat s) => StatIds.All[(int)s];
        public static string Reward(RewardKind k) => k == RewardKind.Starter ? RewardKinds.Starter : RewardKinds.Catalyst;

        /// <summary>The enum value named s; none for "" or null. Throws on an unknown name, so a bad import stops loudly.</summary>
        public static T Parse<T>(string s, T none) where T : struct, Enum => string.IsNullOrEmpty(s) ? none : (T)Enum.Parse(typeof(T), s);

        public static GoalStat ParseStat(string s)
        {
            int i = Array.IndexOf(StatIds.All, s);
            if (i < 0) throw new ArgumentException("unknown stat " + s);
            return (GoalStat)i;
        }

        public static RewardKind ParseReward(string s) => s == RewardKinds.Starter ? RewardKind.Starter
            : s == RewardKinds.Catalyst ? RewardKind.Catalyst : throw new ArgumentException("unknown reward kind " + s);
    }
}
```

- [ ] **Step 4: Run it**

Run: `unity test Athanor --mode EditMode --filter ContentEnumTests`
Expected: 9 passed.

- [ ] **Step 5: Checkpoint (owner commits)**

```
feat(content): add Inspector enums for content assets, pinned to the rule name tables

Story: R-9
```

---

### Task 10: Definition assets and `ContentDatabase`

**Files:**
- Create: `Athanor/Assets/Scripts/Gameplay/Content/{MaterialDefinition,RecipeDefinition,CatalystDefinition,AspectDefinition,EventDefinition,GoalDefinition,GameRules,ContentDatabase}.cs`
- Test: `Athanor/Assets/Tests/EditMode/ContentDatabaseTests.cs`

Each ScriptableObject class needs its own file with the same name, or Unity cannot keep the assets.

- [ ] **Step 1: Write the failing test** `Tests/EditMode/ContentDatabaseTests.cs`

```csharp
using System.Collections.Generic;
using Athanor.Gameplay;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>ContentDatabase built in memory hands the rules the plain data they read.</summary>
    public class ContentDatabaseTests
    {
        readonly List<Object> made = new List<Object>();
        T Make<T>() where T : ScriptableObject { var o = ScriptableObject.CreateInstance<T>(); made.Add(o); return o; }
        [TearDown] public void Clean() { foreach (var o in made) Object.DestroyImmediate(o); made.Clear(); }

        MaterialDefinition Mat(string id, Kind kind, int tier, int ri)
        {
            var m = Make<MaterialDefinition>();
            m.id = id; m.displayName = id.ToUpperInvariant(); m.kind = kind; m.tier = tier; m.reactivity = ri; m.glow = new Color32(0xff, 0x6a, 0x1e, 0xff);
            return m;
        }

        ContentDatabase Tiny()
        {
            var a = Mat("a", Kind.Ore, 0, 5); var b = Mat("b", Kind.Ore, 0, 5); var x = Mat("x", Kind.Metal, 1, 5);
            var r = Make<RecipeDefinition>(); r.items = new[] { a, b }; r.result = x;
            var rules = Make<GameRules>();
            rules.handMin = 3; rules.handBase = 5; rules.trials = 4; rules.trialsFinalDepth = 6; rules.redraws = 2; rules.starterCopies = 2;
            rules.offerSize = 3; rules.rackSize = 12; rules.pouchCards = 1; rules.maxRi = 13; rules.firstReactionMult = 2;
            rules.aspectDepths = new[] { 1, 3, 5 }; rules.potencyByTier = new long[] { 0, 20, 60, 180, 500, 1500 };
            rules.depthTargets = new[] { 300, 1500, 7500, 37500 }; rules.depthNames = new[] { "1", "2", "3", "4", "5" }; rules.ultimate = x;
            var db = Make<ContentDatabase>();
            db.rules = rules; db.materials.AddRange(new[] { a, b, x }); db.recipes.Add(r);
            return db;
        }

        [Test]
        public void ToContentData_TinyDatabase_BuildsAValidContentDb()
        {
            // Arrange
            var db = Tiny();
            // Act
            var content = new ContentDb(db.ToContentData());
            // Assert
            Assert.AreEqual(3, content.Materials.Count);
            Assert.AreEqual("x", content.Ultimate);
            Assert.IsTrue(content.TryCombo(new[] { "a", "b" }, out var recipe));
            Assert.AreEqual("x", recipe.result);
            Assert.AreEqual(4, content.Rules.trials);
            Assert.AreEqual(5, content.FinalDepth);
        }

        [Test]
        public void ToContentData_Material_WritesTheStringsTheRulesRead()
        {
            // Arrange
            var db = Tiny();
            // Act
            var m = db.ToContentData().materials[0];
            // Assert
            Assert.AreEqual("a", m.id); Assert.AreEqual("A", m.name); Assert.AreEqual("Ore", m.kind);
            Assert.AreEqual("#ff6a1e", m.glow); Assert.AreEqual("", m.rarity); Assert.IsFalse(m.locked);
        }

        [Test]
        public void ToContentData_NoRarityWeights_IsNullSoTheDrawStaysUniform()
        {
            // Arrange
            var db = Tiny();
            // Act
            var data = db.ToContentData();
            // Assert
            Assert.IsNull(data.rarityWeights);
            Assert.IsTrue(new ContentDb(data).UniformRarity);
        }

        [Test]
        public void ToContentData_RecipeWithAMissingItem_IsRejectedByContentDb()
        {
            // Arrange
            var db = Tiny();
            db.recipes[0].items = new MaterialDefinition[] { db.materials[0], null };
            // Act
            var data = db.ToContentData();
            // Assert
            Assert.Throws<System.ArgumentException>(() => new ContentDb(data));
        }

        [Test]
        public void ToContentData_CalledTwice_ReturnsIndependentCopies()
        {
            // Arrange
            var db = Tiny();
            // Act
            var first = db.ToContentData(); first.depthTargets[0] = 1; first.rules.potencyByTier[1] = 0;
            var second = db.ToContentData();
            // Assert
            Assert.AreEqual(300, second.depthTargets[0]);
            Assert.AreEqual(20, second.rules.potencyByTier[1]);
        }
    }
}
```

- [ ] **Step 2: Run it and see it fail**

Run: `unity test Athanor --mode EditMode --filter ContentDatabaseTests`
Expected: compile error, `MaterialDefinition could not be found`.

- [ ] **Step 3: Write the definitions**

`Scripts/Gameplay/Content/MaterialDefinition.cs`:

```csharp
using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>A material. The asset's file name is its id; the id is what saves store, so never change it after release.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Material", fileName = "new_material")]
    public sealed class MaterialDefinition : ScriptableObject
    {
        [Tooltip("Save id (journal, profile, icons in tools/art/icons.mjs). Never change it after release.")]
        public string id = "";
        [Tooltip("English name; Vietnamese keeps item names in English.")]
        public string displayName = "";
        public Kind kind;
        [Tooltip("0 = starter. Base potency is GameRules.potencyByTier[tier].")]
        [Min(0)] public int tier;
        [Range(ContentDb.MinReactivity, ContentDb.MaxReactivity)] public int reactivity = ContentDb.MinReactivity;
        [Tooltip("The glow of the material's tile and sparks.")]
        public Color glow = Color.white;
        [Tooltip("Starters only (tier 0): Common, Uncommon or Rare. Leave Unset for every other material.")]
        public RarityId rarity;
        [Tooltip("Starters only: must be won from a goal.")]
        public bool locked;

        public MaterialData ToData() => new MaterialData
        {
            id = id, name = displayName, kind = kind.ToString(), tier = tier, reactivity = reactivity,
            glow = "#" + ColorUtility.ToHtmlStringRGB(glow).ToLowerInvariant(), rarity = ContentNames.Rarity(rarity), locked = locked,
        };
    }
}
```

`Scripts/Gameplay/Content/RecipeDefinition.cs`:

```csharp
using System.Linq;
using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>A combo of 2 to 5 materials (unordered, at most 2 of one) that makes result.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Recipe", fileName = "new_recipe")]
    public sealed class RecipeDefinition : ScriptableObject
    {
        public MaterialDefinition[] items = new MaterialDefinition[0];
        [Tooltip("Total Reaction Index the hub and its company must reach.")]
        [Min(0)] public int minReactivity;
        public MaterialDefinition result;

        public RecipeData ToData() => new RecipeData
        {
            items = items.Select(i => i != null ? i.id : null).ToArray(),
            minReactivity = minReactivity,
            result = result != null ? result.id : null,
        };
    }
}
```

`Scripts/Gameplay/Content/CatalystDefinition.cs`:

```csharp
using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>A catalyst card: a run-long scoring effect chosen after a depth.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Catalyst", fileName = "new_catalyst")]
    public sealed class CatalystDefinition : ScriptableObject
    {
        [Tooltip("Save id; icon cat_<id> in tools/art/icons.mjs.")] public string id = "";
        public string displayName = "";
        [TextArea(2, 4)] public string text = "";
        public CatalystEffect effect;
        [Tooltip("KindDouble only: the kind it doubles.")] public KindOrNone kind;
        public float amount;
        public RarityId rarity = RarityId.Common;
        [Tooltip("Must be won from a goal (not Common).")] public bool locked;

        public CatalystData ToData() => new CatalystData
        {
            id = id, name = displayName, text = text, effect = effect.ToString(), kind = ContentNames.Kind(kind),
            amount = amount, rarity = ContentNames.Rarity(rarity), locked = locked,
        };
    }
}
```

`Scripts/Gameplay/Content/AspectDefinition.cs`:

```csharp
using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>A run-long bench modifier offered at GameRules.aspectDepths.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Aspect", fileName = "new_aspect")]
    public sealed class AspectDefinition : ScriptableObject
    {
        [Tooltip("Save id; icon asp_<id> in tools/art/icons.mjs.")] public string id = "";
        public string displayName = "";
        [TextArea(2, 4)] public string text = "";
        public AspectEffect effect;
        public AspectScope scope;
        public AspectGroup group;
        [Tooltip("Must be above 0 for Heated and Seed.")] public float amount;
        [Tooltip("Cold, Volatile and Greedy are the costly ones.")] public bool tradeOff;

        public AspectData ToData() => new AspectData
        {
            id = id, name = displayName, text = text, effect = effect.ToString(), scope = scope.ToString(),
            group = group.ToString(), amount = amount, tradeOff = tradeOff,
        };
    }
}
```

`Scripts/Gameplay/Content/EventDefinition.cs`:

```csharp
using System;
using System.Linq;
using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>One effect of an event option's cost or outcome.</summary>
    [Serializable]
    public sealed class EventEffectDef
    {
        public EventOp op;
        [Tooltip("Unused by every effect today; kept for the data shape.")] public string arg = "";
        public int amount, amount2;
        public EffectData ToData() => new EffectData { op = op.ToString(), arg = arg ?? "", amount = amount, amount2 = amount2 };
    }

    [Serializable]
    public sealed class EventOutcomeDef
    {
        [Min(0)] public int weight = 1;
        [Tooltip("At most 52 characters.")] public string text = "";
        public EventEffectDef[] gain = new EventEffectDef[0];
        public EventOutcome ToData() => new EventOutcome { weight = weight, text = text, gain = gain.Select(g => g.ToData()).ToArray() };
    }

    [Serializable]
    public sealed class EventOptionDef
    {
        public string label = "";
        public EventNeed requires;
        public EventEffectDef[] cost = new EventEffectDef[0];
        public EventOutcomeDef[] outcomes = new EventOutcomeDef[0];
        public EventOption ToData() => new EventOption
        {
            label = label, requires = ContentNames.Need(requires),
            cost = cost.Select(c => c.ToData()).ToArray(), outcomes = outcomes.Select(o => o.ToData()).ToArray(),
        };
    }

    /// <summary>An event drawn at the start of a depth.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Event", fileName = "new_event")]
    public sealed class EventDefinition : ScriptableObject
    {
        public string id = "";
        [Tooltip("At most 22 characters.")] public string displayName = "";
        [Tooltip("At most 56 characters.")] public string text = "";
        public EventKind type;
        [Min(1)] public int minDepth = 1;
        [Min(1)] public int maxDepth = 1;
        [Min(0)] public int weight = 1;
        public EventOptionDef[] options = new EventOptionDef[0];

        public EventData ToData() => new EventData
        {
            id = id, name = displayName, text = text, type = type.ToString(), minDepth = minDepth, maxDepth = maxDepth, weight = weight,
            options = options.Select(o => o.ToData()).ToArray(),
        };
    }
}
```

`Scripts/Gameplay/Content/GoalDefinition.cs`:

```csharp
using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>A long-term goal: reach target on a stat, win one random locked item of rewardKind and rewardRarity.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Goal", fileName = "new_goal")]
    public sealed class GoalDefinition : ScriptableObject
    {
        public string id = "";
        [TextArea(1, 3)] public string text = "";
        public GoalStat stat;
        [Min(1)] public int target = 1;
        public RewardKind rewardKind;
        [Tooltip("Uncommon or above; a starter reward is at most Rare.")] public RarityId rewardRarity = RarityId.Uncommon;

        public GoalData ToData() => new GoalData
        {
            id = id, text = text, stat = ContentNames.Stat(stat), target = target,
            rewardKind = ContentNames.Reward(rewardKind), rewardRarity = ContentNames.Rarity(rewardRarity),
        };
    }
}
```

`Scripts/Gameplay/Content/GameRules.cs`:

```csharp
using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>The run's tuning numbers (was the "rules" block of content.json) and the depth table.</summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Game Rules", fileName = "GameRules")]
    public sealed class GameRules : ScriptableObject
    {
        [Header("Hand and trials")]
        [Tooltip("Smallest hand size.")] public int handMin;
        [Tooltip("Hand size a run starts with.")] public int handBase;
        [Tooltip("Trials per depth.")] public int trials;
        [Tooltip("Trials at the final depth.")] public int trialsFinalDepth;
        [Tooltip("Redraws per run.")] public int redraws;
        [Header("Deck, rack, offers")]
        [Tooltip("Copies of each starter material in the bench deck.")] public int starterCopies;
        [Tooltip("Catalyst cards offered after a depth.")] public int offerSize;
        [Tooltip("Bench rack slots.")] public int rackSize;
        [Tooltip("Cards the pouch adds to the hand.")] public int pouchCards;
        [Header("Scoring")]
        [Tooltip("Ceiling of a material's Reaction Index on a tile.")] public int maxRi;
        [Tooltip("Multiplier on the first reaction of a trial.")] public int firstReactionMult;
        [Tooltip("Base potency by material tier; index is the tier.")] public long[] potencyByTier = new long[0];
        [Tooltip("Depths (1-based) at which the run offers an aspect.")] public int[] aspectDepths = new int[0];
        [Header("Depths")]
        [Tooltip("Potency to clear each depth but the last (one fewer than depthNames).")] public int[] depthTargets = new int[0];
        [Tooltip("One name per depth; the count is the final depth.")] public string[] depthNames = new string[0];
        [Tooltip("Making this material wins the run.")] public MaterialDefinition ultimate;
        [Header("Draws")]
        [Tooltip("Weight of 'no event' against the events' own weights.")] public int noEventWeight;
        [Tooltip("Empty = every rarity draws equally (owner decision 2026-11-15). Otherwise one weight per rarity, Common first.")]
        public int[] rarityWeights = new int[0];

        public RulesData ToData() => new RulesData
        {
            handMin = handMin, handBase = handBase, trials = trials, trialsFinalDepth = trialsFinalDepth, redraws = redraws,
            starterCopies = starterCopies, offerSize = offerSize, rackSize = rackSize, pouchCards = pouchCards, maxRi = maxRi,
            firstReactionMult = firstReactionMult, aspectDepths = (int[])aspectDepths.Clone(), potencyByTier = (long[])potencyByTier.Clone(),
        };
    }
}
```

`Scripts/Gameplay/Content/ContentDatabase.cs`:

```csharp
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Athanor.Gameplay
{
    /// <summary>
    /// The game's content: every definition asset in the order the rules see it (the order matters: offers, draws and
    /// "the order of the file" tie-breaks follow it). ToContentData builds the plain data ContentDb validates.
    /// </summary>
    [CreateAssetMenu(menuName = "Athanor/Content/Content Database", fileName = "ContentDatabase")]
    public sealed class ContentDatabase : ScriptableObject
    {
        public GameRules rules;
        public List<MaterialDefinition> materials = new List<MaterialDefinition>();
        public List<RecipeDefinition> recipes = new List<RecipeDefinition>();
        public List<CatalystDefinition> catalysts = new List<CatalystDefinition>();
        public List<AspectDefinition> aspects = new List<AspectDefinition>();
        public List<EventDefinition> events = new List<EventDefinition>();
        public List<GoalDefinition> goals = new List<GoalDefinition>();

        /// <summary>A fresh copy of the content as the rules read it. Throws when rules is not set.</summary>
        public ContentData ToContentData()
        {
            if (rules == null) throw new System.InvalidOperationException(name + ": rules (GameRules) is not set");
            return new ContentData
            {
                rules = rules.ToData(),
                materials = materials.Select(m => m.ToData()).ToArray(),
                recipes = recipes.Select(r => r.ToData()).ToArray(),
                catalysts = catalysts.Select(c => c.ToData()).ToArray(),
                aspects = aspects.Select(a => a.ToData()).ToArray(),
                events = events.Select(e => e.ToData()).ToArray(),
                goals = goals.Select(g => g.ToData()).ToArray(),
                depthTargets = (int[])rules.depthTargets.Clone(),
                depthNames = (string[])rules.depthNames.Clone(),
                ultimate = rules.ultimate != null ? rules.ultimate.id : "",
                noEventWeight = rules.noEventWeight,
                rarityWeights = rules.rarityWeights != null && rules.rarityWeights.Length > 0 ? (int[])rules.rarityWeights.Clone() : null,
            };
        }
    }
}
```

- [ ] **Step 4: Run it**

Run: `unity test Athanor --mode EditMode --filter ContentDatabaseTests`
Expected: 5 passed.

- [ ] **Step 5: Checkpoint (owner commits)**

```
feat(content): add ScriptableObject definitions and ContentDatabase.ToContentData

Story: R-10
```

---

### Task 11: Import `content.json` into assets and prove they match

**Files:**
- Create: `Athanor/Assets/Editor/Content/ContentJsonImporter.cs`
- Create: `Athanor/Assets/Editor/Shared/ContentAssets.cs`
- Modify: `Athanor/Assets/Editor/Shared/Athanor.EditorShared.asmdef` (it now references `Athanor.Gameplay` after Task 5's rename; confirm)
- Test: `Athanor/Assets/Tests/EditMode/ContentImportTests.cs` (deleted with content.json in Task 13), `Athanor/Assets/Tests/EditMode/ContentAssetTests.cs` (kept)
- Create (generated): `Athanor/Assets/Content/**`

- [ ] **Step 1: The loader for tests and tools** `Editor/Shared/ContentAssets.cs`

```csharp
using Athanor.Gameplay;
using UnityEditor;

/// <summary>The content assets for Editor tools and tests (no scene, no Resources).</summary>
public static class ContentAssets
{
    public const string Root = "Assets/Content", DatabasePath = Root + "/ContentDatabase.asset", RulesPath = Root + "/GameRules.asset";

    /// <summary>Assets/Content/ContentDatabase.asset; throws when it is missing.</summary>
    public static ContentDatabase Database()
    {
        var db = AssetDatabase.LoadAssetAtPath<ContentDatabase>(DatabasePath);
        if (db == null) throw new System.InvalidOperationException("missing " + DatabasePath + ": run Athanor > Content > Import content.json");
        return db;
    }

    /// <summary>The content as the rules read it.</summary>
    public static ContentData Data() => Database().ToContentData();
}
```

- [ ] **Step 2: Write the failing test** `Tests/EditMode/ContentImportTests.cs`

```csharp
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Athanor.Gameplay;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>The imported content assets say exactly what content.json says (null and "" are the same, glow ignores case).</summary>
    public class ContentImportTests
    {
        static object Normalize(object o)
        {
            if (o == null) return null;
            foreach (var f in o.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                var v = f.GetValue(o);
                if (f.FieldType == typeof(string)) f.SetValue(o, f.Name == "glow" ? ((string)v ?? "").ToLowerInvariant() : (string)v ?? "");
                else if (f.FieldType.IsArray)
                {
                    if (v == null) f.SetValue(o, Array.CreateInstance(f.FieldType.GetElementType(), 0));
                    else if (!f.FieldType.GetElementType().IsPrimitive && f.FieldType.GetElementType() != typeof(string))
                        foreach (var e in (IEnumerable)v) Normalize(e);
                }
                else if (f.FieldType.IsClass && v != null) Normalize(v);
            }
            return o;
        }

        static string[] Lines(ContentData d) => JsonUtility.ToJson(Normalize(d), true).Split('\n');

        [Test]
        public void ToContentData_ImportedAssets_EqualsContentJson()
        {
            // Arrange
            var json = JsonUtility.FromJson<ContentData>(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/content.json").text);
            foreach (var r in json.recipes) if (r.items == null || r.items.Length == 0) r.items = new[] { r.a, r.b };
            // Act
            var assets = ContentAssets.Data();
            // Assert
            var want = Lines(json); var got = Lines(assets);
            for (int i = 0; i < Math.Min(want.Length, got.Length); i++) Assert.AreEqual(want[i], got[i], "line " + (i + 1));
            Assert.AreEqual(want.Length, got.Length, "line count");
        }
    }
}
```

And a second file that outlives `content.json` (Task 13 deletes `ContentImportTests.cs` only), `Tests/EditMode/ContentAssetTests.cs`:

```csharp
using System.Linq;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>Rules about the content assets themselves.</summary>
    public class ContentAssetTests
    {
        [Test]
        public void Definitions_EveryAsset_IsNamedAfterItsId()
        {
            // Arrange
            var db = ContentAssets.Database();
            // Act
            var wrong = db.materials.Where(m => m.name != m.id).Select(m => m.name)
                .Concat(db.catalysts.Where(c => c.name != c.id).Select(c => c.name))
                .Concat(db.aspects.Where(a => a.name != a.id).Select(a => a.name))
                .Concat(db.events.Where(e => e.name != e.id).Select(e => e.name))
                .Concat(db.goals.Where(g => g.name != g.id).Select(g => g.name)).ToList();
            // Assert
            CollectionAssert.IsEmpty(wrong);
        }
    }
}
```

- [ ] **Step 3: Run it and see it fail**

Run: `unity test Athanor --mode EditMode --filter "ContentImportTests|ContentAssetTests"`
Expected: 2 failed with `missing Assets/Content/ContentDatabase.asset`.

- [ ] **Step 4: Write the importer** `Editor/Content/ContentJsonImporter.cs`

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Athanor.Gameplay;
using UnityEditor;
using UnityEngine;

namespace Athanor.EditorTools
{
    /// <summary>
    /// One-shot: turns Resources/content.json into the assets under Assets/Content, keeping every list's order.
    /// Running it again updates the assets by id. Deleted with content.json in Task 13.
    /// </summary>
    public static class ContentJsonImporter
    {
        const string Json = "Assets/Resources/content.json", Root = ContentAssets.Root;

        [MenuItem("Athanor/Content/Import content.json (one-shot)")]
        public static void Menu() => Import(Json);

        public static void RunBatch()
        {
            try { Import(Json); }
            catch (Exception e) { Debug.LogError(e); EditorApplication.Exit(1); }
        }

        public static ContentDatabase Import(string jsonPath)
        {
            var data = JsonUtility.FromJson<ContentData>(File.ReadAllText(jsonPath));
            foreach (var dir in new[] { "Materials", "Recipes", "Catalysts", "Aspects", "Events", "Goals" }) Directory.CreateDirectory(Path.Combine(Root, dir));
            var db = LoadOrCreate<ContentDatabase>(ContentAssets.DatabasePath);
            var rules = LoadOrCreate<GameRules>(ContentAssets.RulesPath);

            var mats = new Dictionary<string, MaterialDefinition>();
            db.materials.Clear();
            foreach (var m in data.materials)
            {
                var a = LoadOrCreate<MaterialDefinition>(Root + "/Materials/" + m.id + ".asset");
                a.id = m.id; a.displayName = m.name; a.kind = (Kind)Enum.Parse(typeof(Kind), m.kind); a.tier = m.tier; a.reactivity = m.reactivity;
                if (!ColorUtility.TryParseHtmlString(m.glow, out a.glow)) throw new ArgumentException("bad glow " + m.glow + " on " + m.id);
                a.rarity = ContentNames.Parse(m.rarity, RarityId.Unset); a.locked = m.locked;
                Dirty(a); mats[m.id] = a; db.materials.Add(a);
            }
            MaterialDefinition M(string id) => id != null && mats.TryGetValue(id, out var d) ? d : throw new ArgumentException("unknown material " + id);

            db.recipes.Clear();
            for (int i = 0; i < data.recipes.Length; i++)
            {
                var r = data.recipes[i];
                var items = r.items != null && r.items.Length > 0 ? r.items : new[] { r.a, r.b };
                var a = LoadOrCreate<RecipeDefinition>(Root + "/Recipes/" + i.ToString("000") + "_" + r.result + ".asset");
                a.items = items.Select(M).ToArray(); a.minReactivity = r.minReactivity; a.result = M(r.result);
                Dirty(a); db.recipes.Add(a);
            }

            db.catalysts.Clear();
            foreach (var c in data.catalysts ?? new CatalystData[0])
            {
                var a = LoadOrCreate<CatalystDefinition>(Root + "/Catalysts/" + c.id + ".asset");
                a.id = c.id; a.displayName = c.name; a.text = c.text; a.effect = ContentNames.Parse(c.effect, default(CatalystEffect));
                a.kind = ContentNames.Parse(c.kind, KindOrNone.None); a.amount = c.amount; a.rarity = ContentNames.Parse(c.rarity, RarityId.Unset); a.locked = c.locked;
                Dirty(a); db.catalysts.Add(a);
            }

            db.aspects.Clear();
            foreach (var s in data.aspects ?? new AspectData[0])
            {
                var a = LoadOrCreate<AspectDefinition>(Root + "/Aspects/" + s.id + ".asset");
                a.id = s.id; a.displayName = s.name; a.text = s.text; a.effect = ContentNames.Parse(s.effect, default(AspectEffect));
                a.scope = ContentNames.Parse(s.scope, default(AspectScope)); a.group = ContentNames.Parse(s.group, default(AspectGroup));
                a.amount = s.amount; a.tradeOff = s.tradeOff;
                Dirty(a); db.aspects.Add(a);
            }

            db.events.Clear();
            foreach (var e in data.events ?? new EventData[0])
            {
                var a = LoadOrCreate<EventDefinition>(Root + "/Events/" + e.id + ".asset");
                a.id = e.id; a.displayName = e.name; a.text = e.text; a.type = ContentNames.Parse(e.type, default(EventKind));
                a.minDepth = e.minDepth; a.maxDepth = e.maxDepth; a.weight = e.weight;
                a.options = (e.options ?? new EventOption[0]).Select(o => new EventOptionDef
                {
                    label = o.label, requires = ContentNames.Parse(o.requires, EventNeed.None),
                    cost = Effects(o.cost),
                    outcomes = (o.outcomes ?? new EventOutcome[0]).Select(u => new EventOutcomeDef { weight = u.weight, text = u.text, gain = Effects(u.gain) }).ToArray(),
                }).ToArray();
                Dirty(a); db.events.Add(a);
            }

            db.goals.Clear();
            foreach (var g in data.goals ?? new GoalData[0])
            {
                var a = LoadOrCreate<GoalDefinition>(Root + "/Goals/" + g.id + ".asset");
                a.id = g.id; a.text = g.text; a.stat = ContentNames.ParseStat(g.stat); a.target = g.target;
                a.rewardKind = ContentNames.ParseReward(g.rewardKind); a.rewardRarity = ContentNames.Parse(g.rewardRarity, RarityId.Unset);
                Dirty(a); db.goals.Add(a);
            }

            var rd = data.rules ?? throw new ArgumentException("content.json has no rules block");
            rules.handMin = rd.handMin; rules.handBase = rd.handBase; rules.trials = rd.trials; rules.trialsFinalDepth = rd.trialsFinalDepth;
            rules.redraws = rd.redraws; rules.starterCopies = rd.starterCopies; rules.offerSize = rd.offerSize; rules.rackSize = rd.rackSize;
            rules.pouchCards = rd.pouchCards; rules.maxRi = rd.maxRi; rules.firstReactionMult = rd.firstReactionMult;
            rules.aspectDepths = (int[])rd.aspectDepths.Clone(); rules.potencyByTier = (long[])rd.potencyByTier.Clone();
            rules.depthTargets = (int[])data.depthTargets.Clone(); rules.depthNames = (string[])data.depthNames.Clone();
            rules.ultimate = string.IsNullOrEmpty(data.ultimate) ? null : M(data.ultimate);
            rules.noEventWeight = data.noEventWeight; rules.rarityWeights = data.rarityWeights != null ? (int[])data.rarityWeights.Clone() : new int[0];
            Dirty(rules);
            db.rules = rules;
            Dirty(db);
            AssetDatabase.SaveAssets();
            Debug.Log("[content] imported " + db.materials.Count + " materials, " + db.recipes.Count + " recipes, " + db.catalysts.Count + " catalysts, "
                + db.aspects.Count + " aspects, " + db.events.Count + " events, " + db.goals.Count + " goals");
            return db;
        }

        static EventEffectDef[] Effects(EffectData[] list) => (list ?? new EffectData[0])
            .Select(x => new EventEffectDef { op = ContentNames.Parse(x.op, default(EventOp)), arg = x.arg ?? "", amount = x.amount, amount2 = x.amount2 }).ToArray();

        static void Dirty(UnityEngine.Object o) => EditorUtility.SetDirty(o);

        static T LoadOrCreate<T>(string path) where T : ScriptableObject
        {
            var a = AssetDatabase.LoadAssetAtPath<T>(path);
            if (a != null) return a;
            a = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(a, path);
            return a;
        }
    }
}
```

- [ ] **Step 5: Run the import**

Run: `unity run Athanor -- -executeMethod Athanor.EditorTools.ContentJsonImporter.RunBatch`
Expected: exit 0, and the Editor log shows `[content] imported 81 materials, 87 recipes, 26 catalysts, 16 aspects, 19 events, 22 goals`. Run `ls Athanor/Assets/Content/*` to see the folders filled. Every new `.asset` has a `.meta`.

- [ ] **Step 6: Run the equivalence test**

Run: `unity test Athanor --mode EditMode --filter "ContentImportTests|ContentAssetTests"`
Expected: 2 passed. If `line N` fails, the message shows the content.json line next to the asset line. Fix the importer or the definition's `ToData` that writes it, then import again. Do not edit the assets by hand to make this test pass.

- [ ] **Step 7: Checkpoint (owner commits)**

```
feat(content): import content.json into ScriptableObject assets

ContentImportTests proves the assets say exactly what content.json says.
Story: R-11
```

---

### Task 12: The game, the tests and the reports read the assets

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs` (field `contentJson`)
- Create: `Athanor/Assets/Editor/Migration/ContentFieldMigration.cs`
- Modify: `Athanor/Assets/Tests/EditMode/TestContent.cs`, `Athanor/Assets/Editor/Balance/BalanceReports.cs`, `Athanor/Assets/Editor/Shared/EditorGlobals.cs` (nothing loads content there today; confirm)

- [ ] **Step 1: Add the asset field next to the old one in `GameFlow`.** Replace

```csharp
        [SerializeField] TextAsset contentJson, langEn, langVi;
```

with

```csharp
        [SerializeField] TextAsset contentJson, langEn, langVi;
        /// <summary>The game's content (Assets/Content/ContentDatabase.asset).</summary>
        [SerializeField] ContentDatabase content;
```

- [ ] **Step 2: Wire it in the scene with a batch tool** `Editor/Migration/ContentFieldMigration.cs`

```csharp
using System;
using System.Linq;
using Athanor.Game;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Athanor.EditorTools
{
    /// <summary>Sets GameFlow.content in Main.unity to Assets/Content/ContentDatabase.asset and clears contentJson.</summary>
    public static class ContentFieldMigration
    {
        const string ScenePath = "Assets/Scenes/Main.unity";

        [MenuItem("Athanor/Migrate/Point GameFlow at ContentDatabase")]
        public static void Run()
        {
            var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            var game = scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<GameFlow>(true)).Single();
            var so = new SerializedObject(game);
            so.FindProperty("content").objectReferenceValue = ContentAssets.Database();
            so.FindProperty("contentJson").objectReferenceValue = null;
            so.ApplyModifiedPropertiesWithoutUndo();
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
        }

        public static void RunBatch()
        {
            try { Run(); }
            catch (Exception e) { Debug.LogError(e); EditorApplication.Exit(1); }
        }
    }
}
```

Run: `unity run Athanor -- -executeMethod Athanor.EditorTools.ContentFieldMigration.RunBatch`
Expected: exit 0. `git diff --stat Athanor/Assets/Scenes/Main.unity` shows a few changed lines on the GameFlow component.

- [ ] **Step 3: Read the asset in `GameFlow`.** Remove `contentJson` from the field list. The declaration becomes:

```csharp
        [SerializeField] TextAsset langEn, langVi;
        /// <summary>The game's content (Assets/Content/ContentDatabase.asset).</summary>
        [SerializeField] ContentDatabase content;
```

In `Awake`, replace the `contentJson` null check with

```csharp
            if (content == null) throw new System.InvalidOperationException("GameFlow: content (ContentDatabase) is not set in the scene");
```

and replace

```csharp
            Db = new ContentDb(JsonUtility.FromJson<ContentData>(contentJson.text));
```

with

```csharp
            Db = new ContentDb(content.ToContentData());
```

- [ ] **Step 4: Tests and reports read the asset**

`TestContent.Load()`:

```csharp
        /// <summary>The real content, from Assets/Content.</summary>
        public static ContentData Load() => ContentAssets.Data();
```

In `BalanceReports.cs`, replace the `Content()` line with

```csharp
    static ContentData Content() => ContentAssets.Data();
```

Then: `grep -rn "content.json\|Resources.Load<.*>(\"content\")" Athanor/Assets --include="*.cs"`
Expected: only `ContentJsonImporter.cs` and `ContentImportTests.cs`.

- [ ] **Step 5: Run every check**

```bash
unity test Athanor --mode EditMode
unity test Athanor --mode PlayMode
for r in balance-matrix events roles newcomer balance trace; do
  unity run Athanor -- -executeMethod BalanceReports.RunBatch -report "$r" -out "$PWD/Temp/reports/$r.txt"
  diff -q "Temp/reports/$r.txt" "production/qa/evidence/refactor/baseline/$r.txt" || echo "DIFF $r"
done
```

Expected: 0 failed, no `DIFF`, and the autoplay and tutorial traces identical. If a report differs although `ContentImportTests` passes, a rule checks `== null` where the assets now give `""` or an empty array. Find it with `grep -n "== null\|!= null" Athanor/Assets/Scripts/Gameplay/*.cs`, make it treat null and empty alike (`string.IsNullOrEmpty`, `?.Length > 0`), and add a test for that case.

- [ ] **Step 6: Checkpoint (owner commits)**

```
refactor(content): load content from ContentDatabase in the game, tests and reports

Story: R-12
```

---

### Task 13: Snapshot for outside tools, then delete `content.json`

**Files:**
- Create: `Athanor/Assets/Editor/Shared/ContentSnapshot.cs` (in `Athanor.EditorShared`, so the tests can call it)
- Test: `Athanor/Assets/Tests/EditMode/ContentSnapshotTests.cs`
- Create (generated): `tools/content/snapshot.json`
- Modify: `tools/ci/check_repo.py:18`, `tools/vault/build_vault.py:4,310`, `tools/art/export.mjs:14`, `tools/art/icons.mjs:509` (comment), `.claude/agents/content-designer.md`, `localizer-vi.md`, `lore-writer.md`
- Delete: `Athanor/Assets/Resources/content.json` (+ meta), `Athanor/Assets/Editor/Content/ContentJsonImporter.cs` (+ meta), `ContentImportTests.cs` (+ meta)

- [ ] **Step 1: Write the failing test** `Tests/EditMode/ContentSnapshotTests.cs`

```csharp
using System.IO;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>tools/content/snapshot.json is the assets written out; a content change without a new snapshot fails here.</summary>
    public class ContentSnapshotTests
    {
        [Test]
        public void Snapshot_File_EqualsTheAssets()
        {
            // Arrange
            var path = ContentSnapshot.FilePath;
            Assert.IsTrue(File.Exists(path), path + " is missing: run Athanor > Content > Write Snapshot");
            // Act
            var want = ContentSnapshot.Text();
            // Assert
            Assert.AreEqual(want, File.ReadAllText(path), "the snapshot is stale: run Athanor > Content > Write Snapshot");
        }
    }
}
```

- [ ] **Step 2: Run it and see it fail**

Run: `unity test Athanor --mode EditMode --filter ContentSnapshotTests`
Expected: compile error, `ContentSnapshot could not be found`.

- [ ] **Step 3: Write `Athanor/Assets/Editor/Shared/ContentSnapshot.cs`**

```csharp
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Writes the content assets as tools/content/snapshot.json (the shape content.json had) for the tools that are not Unity:
/// tools/ci/check_repo.py, tools/vault/build_vault.py, tools/art/export.mjs. The game never reads it.
/// </summary>
public static class ContentSnapshot
{
    public static string FilePath => Path.GetFullPath(Path.Combine(Application.dataPath, "../../tools/content/snapshot.json"));

    public static string Text() => JsonUtility.ToJson(ContentAssets.Data(), true).Replace("\r\n", "\n") + "\n";

    [MenuItem("Athanor/Content/Write Snapshot")]
    public static void Write()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
        File.WriteAllText(FilePath, Text());
        Debug.Log("[content] snapshot written to " + FilePath);
    }

    public static void RunBatch()
    {
        try { Write(); }
        catch (System.Exception e) { Debug.LogError(e); EditorApplication.Exit(1); }
    }
}
```

- [ ] **Step 4: Write the snapshot and run the test**

```bash
unity run Athanor -- -executeMethod ContentSnapshot.RunBatch
python3 -c "import json;d=json.load(open('tools/content/snapshot.json'));print(len(d['materials']),len(d['recipes']),d['ultimate'])"
unity test Athanor --mode EditMode --filter ContentSnapshotTests
```

Expected: `81 87 worldseed`, then 1 passed.

- [ ] **Step 5: Point the outside tools at the snapshot**

- `tools/ci/check_repo.py` line 18: `CONTENT = ROOT / "tools/content/snapshot.json"`. Change the comment on line 107 to `# content text lives in the content assets (English) and is localized by vi.json only`.
- `tools/vault/build_vault.py` line 4: `Resources/content.json` → `tools/content/snapshot.json (written from Assets/Content)`. Line 310: `d = json.loads((ROOT / "tools" / "content" / "snapshot.json").read_text(encoding="utf-8"))`. If the file has no `ROOT`, use `UNITY.parent.parent`.
- `tools/art/export.mjs` line 14: `const content = JSON.parse(readFileSync(join(assets, '../../tools/content/snapshot.json'), 'utf8'));`. Do **not** run `export.mjs`: it drifts (ATHANOR.md §3). Check it with `node --check tools/art/export.mjs`.
- `tools/art/icons.mjs` line 509: `/** Material id (as in Assets/Content/Materials) => builder returning a 24x24 Spr. */`
- In the three agent files, replace "content.json" with "the content assets in `Athanor/Assets/Content` (edit in the Inspector; then `Athanor > Content > Write Snapshot`)". `content-designer.md` also gets its acceptance command changed from the dotnet reports to `unity run Athanor -- -executeMethod BalanceReports.RunBatch -report balance-matrix -out /tmp/m.txt`.

- [ ] **Step 6: Delete the JSON and the one-shot importer**

```bash
git rm Athanor/Assets/Resources/content.json Athanor/Assets/Resources/content.json.meta \
  Athanor/Assets/Editor/Content/ContentJsonImporter.cs Athanor/Assets/Editor/Content/ContentJsonImporter.cs.meta \
  Athanor/Assets/Tests/EditMode/ContentImportTests.cs Athanor/Assets/Tests/EditMode/ContentImportTests.cs.meta
grep -rn "content\.json" Athanor/Assets tools .claude/agents --include="*.cs" --include="*.py" --include="*.mjs" --include="*.md"
```

Expected from the grep: no output.

- [ ] **Step 7: Run every check**

```bash
tools/ci/run.sh all
tools/ci/run.sh unity
python3 tools/vault/build_vault.py
```

Then repeat the report diff loop from Task 12, Step 5.
Expected: everything green, no `DIFF`. The vault material and recipe notes are unchanged, because the snapshot has the same ids and values.

- [ ] **Step 8: Checkpoint (owner commits)**

```
refactor(content)!: delete content.json; tools read tools/content/snapshot.json

ADR-0001. ContentSnapshotTests keeps the snapshot equal to the assets.
Story: R-13
```

---

# Stage 4: Manager singletons

### Task 14: `Singleton<T>`

**Files:**
- Create: `Athanor/Assets/Scripts/Game/Managers/Singleton.cs`
- Test: `Athanor/Assets/Tests/PlayMode/SingletonTests.cs`

- [ ] **Step 0: Re-read the Task 2 Step 1 API check.** It confirmed `RuntimeInitializeOnLoadMethod(SubsystemRegistration)` and `DefaultExecutionOrder` on Unity 6000.6. If it recorded a replacement in `docs/engine-reference/unity/deprecated-apis.md`, use it in `Singleton.cs` below.

- [ ] **Step 1: Write the failing test** `Tests/PlayMode/SingletonTests.cs`. Awake only runs in Play Mode, which is why this is a PlayMode test.

```csharp
using System.Collections;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Athanor.Tests
{
    public class SingletonTests
    {
        sealed class Probe : Singleton<Probe> { public int awakes; protected override void OnAwake() => awakes++; }

        [UnityTest]
        public IEnumerator Instance_AfterAwake_IsThatComponent()
        {
            // Arrange
            var go = new GameObject("probe");
            // Act
            var p = go.AddComponent<Probe>();
            yield return null;
            // Assert
            Assert.AreSame(p, Probe.Instance);
            Assert.AreEqual(1, p.awakes);
            Object.Destroy(go);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_SecondInstance_IsRemovedAndTheFirstStays()
        {
            // Arrange
            var a = new GameObject("a").AddComponent<Probe>();
            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex("second Probe"));
            // Act
            var b = new GameObject("b").AddComponent<Probe>();
            yield return null;
            // Assert
            Assert.AreSame(a, Probe.Instance);
            Assert.IsTrue(b == null, "the second component is destroyed");
            Object.Destroy(a.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator OnDestroy_TheInstance_ClearsIt()
        {
            // Arrange
            var go = new GameObject("probe"); go.AddComponent<Probe>();
            yield return null;
            // Act
            Object.Destroy(go);
            yield return null;
            // Assert
            Assert.IsFalse(Probe.Exists);
            Assert.Throws<System.InvalidOperationException>(() => { var _ = Probe.Instance; });
        }

        [UnityTest]
        public IEnumerator ResetStatics_AfterAStop_ForgetsEveryInstance()
        {
            // Arrange
            var go = new GameObject("probe"); go.AddComponent<Probe>();
            yield return null;
            // Act
            Singletons.ResetStatics();
            // Assert
            Assert.IsFalse(Probe.Exists);
            Object.Destroy(go);
            yield return null;
        }
    }
}
```

- [ ] **Step 2: Run it and see it fail**

Run: `unity test Athanor --mode PlayMode --filter SingletonTests`
Expected: compile error, `Singleton<> could not be found`.

- [ ] **Step 3: Write `Scripts/Game/Managers/Singleton.cs`**

```csharp
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// Forgets every manager instance when Play starts. Domain reload is off (EditorSettings), so statics survive Stop and Play;
    /// without this a second session would find the destroyed managers of the first.
    /// </summary>
    public static class Singletons
    {
        static readonly List<Action> clears = new List<Action>();

        internal static void Track(Action clear) { if (!clears.Contains(clear)) clears.Add(clear); }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void ResetStatics()
        {
            foreach (var c in clears) c();
            clears.Clear();
        }
    }

    /// <summary>
    /// A manager that exists once, in Main.unity › Managers. There is no lazy creation and no DontDestroyOnLoad: the scene owns it.
    /// Instance throws when the manager is not in the scene, so a missing manager fails at once rather than as a null later.
    /// Derived classes put their start-up in OnAwake and call base.OnDestroy.
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        static T instance;

        public static T Instance => instance != null ? instance
            : throw new InvalidOperationException(typeof(T).Name + " is not in the scene (Main.unity › Managers)");

        public static bool Exists => instance != null;

        static void Clear() => instance = null;

        protected void Awake()
        {
            if (instance != null && instance != this)
            {
                Debug.LogError("A second " + typeof(T).Name + " on " + name + " was removed; " + instance.name + " is the one in use.", this);
                Destroy(this);
                return;
            }
            instance = (T)this;
            Singletons.Track(Clear);
            OnAwake();
        }

        protected virtual void OnAwake() { }

        protected virtual void OnDestroy()
        {
            if (instance == this) instance = null;
        }
    }
}
```

- [ ] **Step 4: Run it**

Run: `unity test Athanor --mode PlayMode --filter SingletonTests`
Expected: 4 passed.

- [ ] **Step 5: Add `Singletons` to the allowed statics** in `docs/ATHANOR.md` §2, "Allowed statics". The list becomes nine classes, `…, RarityStars, Singletons`. Add one sentence: *"`Singletons` holds the clear actions of every `Singleton<T>.instance`; its reset forgets all of them (`SingletonTests`)."*

- [ ] **Step 6: Checkpoint (owner commits)**

```
feat(managers): add Singleton<T> with a SubsystemRegistration reset

Story: R-14
```

---

### Task 15: `GameFlow` becomes `GameManager`

**Files:**
- Move: `Scripts/Game/Boot/GameFlow.cs` → `Scripts/Game/Boot/GameManager.cs` (+ meta; same GUID)
- Modify: every `.cs` that names `GameFlow` (the list from the planning grep: 16 in `Scripts`, 7 in `Tests/EditModeGame`, `Editor/Shared/EditorGlobals.cs`, the PlayMode tests)

- [ ] **Step 1: Move with the meta, so the component in `Main.unity` keeps working**

```bash
git mv Athanor/Assets/Scripts/Game/Boot/GameFlow.cs Athanor/Assets/Scripts/Game/Boot/GameManager.cs
git mv Athanor/Assets/Scripts/Game/Boot/GameFlow.cs.meta Athanor/Assets/Scripts/Game/Boot/GameManager.cs.meta
grep -rlZ -w "GameFlow" Athanor/Assets --include="*.cs" | xargs -0 perl -pi -e 's/\bGameFlow\b/GameManager/g'
grep -rn -w "GameFlow" Athanor/Assets --include="*.cs"
```

Expected from the last grep: no output.

- [ ] **Step 2: Make it a singleton.** In `GameManager.cs`, change

```csharp
    public sealed class GameManager : MonoBehaviour
```

to

```csharp
    public sealed class GameManager : Singleton<GameManager>
```

Rename `void Awake()` to `protected override void OnAwake()`. Rename `void OnDestroy()` to `protected override void OnDestroy()` and add `base.OnDestroy();` as its first line.

- [ ] **Step 3: Verify**

```bash
unity test Athanor --mode EditMode
unity test Athanor --mode PlayMode
git diff --stat -- Athanor/Assets/Scenes Athanor/Assets/Prefabs
```

Expected: 0 failed, both trace baselines identical, and no scene or prefab diff (the GUID did not change).

- [ ] **Step 4: Checkpoint (owner commits)**

```
refactor(managers): rename GameFlow to GameManager (same script GUID)

Story: R-15
```

---

### Task 16: `ContentManager`, `SaveManager`, `LocalizationManager`

**Files:**
- Create: `Scripts/Game/Managers/ContentManager.cs`, `SaveManager.cs`, `LocalizationManager.cs`
- Create: `Editor/Migration/ManagerSceneMigration.cs`
- Modify: `Scripts/Game/Boot/GameManager.cs`
- Test: `Tests/PlayMode/ManagerBootTests.cs`

- [ ] **Step 1: Write the failing test** `Tests/PlayMode/ManagerBootTests.cs`

```csharp
using System.Collections;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Athanor.Tests
{
    /// <summary>Main.unity boots with its managers in place and in order.</summary>
    public class ManagerBootTests
    {
        [UnityTest]
        public IEnumerator Load_MainScene_EveryDataManagerIsUp()
        {
            // Arrange
            var prefs = new PrefsSnapshot(introSeen: true, tutorialDone: true);
            try
            {
                // Act
                yield return MainScene.Load();
                // Assert
                Assert.IsTrue(ContentManager.Exists && SaveManager.Exists && LocalizationManager.Exists, "a manager is missing from Main.unity › Managers");
                Assert.AreEqual(81, ContentManager.Instance.Db.Materials.Count);
                Assert.AreEqual("en", SaveManager.Instance.Settings.language);
                Assert.AreSame(ContentManager.Instance.Db, GameManager.Instance.Db);
            }
            finally { prefs.Dispose(); }
            yield return MainScene.Unload();
        }
    }
}
```

- [ ] **Step 2: Run it and see it fail**

Run: `unity test Athanor --mode PlayMode --filter ManagerBootTests`
Expected: compile error, `ContentManager could not be found`.

- [ ] **Step 3: Write the three managers**

`Scripts/Game/Managers/ContentManager.cs`:

```csharp
using Athanor.Gameplay;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The game's content: the ContentDatabase asset, validated and indexed once at start-up.</summary>
    [DefaultExecutionOrder(-200)]
    public sealed class ContentManager : Singleton<ContentManager>
    {
        [Tooltip("Assets/Content/ContentDatabase.asset")]
        [SerializeField] ContentDatabase database;

        public ContentDatabase Database => database;
        /// <summary>The validated content the rules read.</summary>
        public ContentDb Db { get; private set; }

        protected override void OnAwake()
        {
            if (database == null) throw new System.InvalidOperationException("ContentManager: database is not set (Assets/Content/ContentDatabase.asset)");
            Db = new ContentDb(database.ToContentData());
        }
    }
}
```

`Scripts/Game/Managers/SaveManager.cs`:

```csharp
using Athanor.Gameplay;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The player's settings and the small saved flags. Storage itself stays in Save (PlayerPrefs; browser storage on the web).</summary>
    [DefaultExecutionOrder(-190)]
    public sealed class SaveManager : Singleton<SaveManager>
    {
        public Settings Settings { get; private set; }

        protected override void OnAwake()
        {
            Settings = Save.LoadSettings();
            Haptics.Enabled = Settings.haptics;
        }

        public void StoreSettings() => Save.StoreSettings(Settings);

        public bool IntroSeen => Save.IntroSeen;
        public void SetIntroSeen(bool seen) => Save.SetIntroSeen(seen);
        public bool TutorialDone => Save.TutorialDone;
        public void SetTutorialDone(bool done) => Save.SetTutorialDone(done);

        [ContextMenu("Erase journal, profile, settings, intro and tutorial")]
        void EraseAll()
        {
            foreach (var k in new[] { Save.Key, Save.ProfileKey, Save.SettingsKey, Save.IntroKey, Save.TutorialKey }) PlayerPrefs.DeleteKey(k);
            PlayerPrefs.Save();
            Debug.Log("[save] erased; restart Play to start fresh");
        }

        [ContextMenu("Show the tutorial again")]
        void ForgetTutorial() => Save.SetTutorialDone(false);
    }
}
```

`Scripts/Game/Managers/LocalizationManager.cs`:

```csharp
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The two language files; publishes them through Loc and picks the start language from the saved settings.</summary>
    [DefaultExecutionOrder(-180)]
    public sealed class LocalizationManager : Singleton<LocalizationManager>
    {
        [Tooltip("Assets/Resources/lang/en.json")] [SerializeField] TextAsset english;
        [Tooltip("Assets/Resources/lang/vi.json")] [SerializeField] TextAsset vietnamese;

        protected override void OnAwake()
        {
            if (english == null || vietnamese == null) throw new System.InvalidOperationException("LocalizationManager: a language file is not set");
            Loc.Use(english, vietnamese);
            Loc.InitFromPlayer(SaveManager.Instance.Settings.language);
        }
    }
}
```

The language files stay JSON on purpose. They are not `content.json`. CI check LANG, `localizer-vi` and `rotate_sync.mjs` read them.

- [ ] **Step 4: Put them in the scene, with values copied from `GameManager`** `Editor/Migration/ManagerSceneMigration.cs`

```csharp
using System;
using System.Linq;
using Athanor.Game;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Athanor.EditorTools
{
    /// <summary>
    /// Creates Main.unity › Managers and its components, copying the references GameManager held. Idempotent: run it again after
    /// a task adds a manager. Menu: Athanor/Migrate/Add Managers to Main.
    /// </summary>
    public static class ManagerSceneMigration
    {
        const string ScenePath = "Assets/Scenes/Main.unity";

        [MenuItem("Athanor/Migrate/Add Managers to Main")]
        public static void Run()
        {
            var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            Apply(scene);
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
        }

        public static void RunBatch()
        {
            try { Run(); }
            catch (Exception e) { Debug.LogError(e); EditorApplication.Exit(1); }
        }

        public static void Apply(Scene scene)
        {
            var game = scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<GameManager>(true)).Single();
            var root = scene.GetRootGameObjects().FirstOrDefault(g => g.name == "Managers");
            if (root == null) { root = new GameObject("Managers"); SceneManager.MoveGameObjectToScene(root, scene); root.transform.SetAsFirstSibling(); }
            var from = new SerializedObject(game);
            Copy(from, "content", Ensure<ContentManager>(root), "database");
            Ensure<SaveManager>(root);
            var loc = Ensure<LocalizationManager>(root);
            Copy(from, "langEn", loc, "english");
            Copy(from, "langVi", loc, "vietnamese");
        }

        static T Ensure<T>(GameObject root) where T : Component => root.GetComponent<T>() ?? root.AddComponent<T>();

        /// <summary>Copies an object reference when GameManager still has the field and the target is still empty.</summary>
        static void Copy(SerializedObject from, string field, Component to, string toField)
        {
            var src = from.FindProperty(field);
            if (src == null) return;
            var dst = new SerializedObject(to);
            var p = dst.FindProperty(toField);
            if (p.objectReferenceValue == null) { p.objectReferenceValue = src.objectReferenceValue; dst.ApplyModifiedPropertiesWithoutUndo(); }
        }
    }
}
```

Run: `unity run Athanor -- -executeMethod Athanor.EditorTools.ManagerSceneMigration.RunBatch`
Expected: exit 0. Then open `Main.unity` in the Editor once and check that `Managers` has three components with `database`, `english` and `vietnamese` filled in. Close the Editor without changing anything.

- [ ] **Step 5: Take the moved parts out of `GameManager`.** Exact edits:

1. Delete the `[SerializeField] TextAsset langEn, langVi;` line, the `content` field, and their three null checks in `OnAwake`.
2. Replace `public ContentDb Db { get; private set; }` with `public ContentDb Db => ContentManager.Instance.Db;`.
3. Delete the field `Settings settings;`. Add the property `Settings Settings => SaveManager.Instance.Settings;`.
4. In `OnAwake`, delete these lines: `Loc.Use(langEn, langVi);`, `settings = Save.LoadSettings();`, `Loc.InitFromPlayer(settings.language);`, `Haptics.Enabled = settings.haptics;`, `Db = new ContentDb(content.ToContentData());`.
5. Replace `title.Bind(settings, fx);` with `title.Bind(Settings, fx);` and `pause.Bind(settings);` with `pause.Bind(Settings);`.
6. Replace `void SaveSettings() => Save.StoreSettings(settings);` with `void SaveSettings() => SaveManager.Instance.StoreSettings();`.
7. In `Update`, replace `depthStage.SwayOn = settings.sway;` with `depthStage.SwayOn = Settings.sway;`.
8. Replace `Save.IntroSeen` with `SaveManager.Instance.IntroSeen`, `Save.SetIntroSeen(true)` with `SaveManager.Instance.SetIntroSeen(true)`, and both `Save.TutorialDone` with `SaveManager.Instance.TutorialDone`.

Run: `grep -n "settings\b\|langEn\|langVi\|Loc.Use\|Save.LoadSettings" Athanor/Assets/Scripts/Game/Boot/GameManager.cs`
Expected: no output.

Tests that read `GameManager` fields `langEn`/`langVi`/`content` by name through `MainSceneFixture.FlowField` now fail. Point them at `LocalizationManager` and `ContentManager`: `main.All<ContentManager>()[0]`.

- [ ] **Step 6: Run every check**

```bash
unity test Athanor --mode EditMode
unity test Athanor --mode PlayMode
```

Expected: 0 failed, `ManagerBootTests` passes, and the autoplay and tutorial traces are identical.

- [ ] **Step 7: Checkpoint (owner commits)**

```
feat(managers): ContentManager, SaveManager and LocalizationManager in Main.unity

Story: R-16
```

---

### Task 17: `ProgressManager` (journal, profile, goals, unlocks, sandbox)

**Files:**
- Create: `Scripts/Game/Managers/ProgressManager.cs`
- Modify: `Scripts/Game/Boot/GameManager.cs`, `Editor/Migration/ManagerSceneMigration.cs`, `Tests/EditModeGame/PauseTests.cs`
- Test: `Tests/PlayMode/ManagerBootTests.cs` (add a test)

- [ ] **Step 1: Add the failing test** to `ManagerBootTests`

```csharp
        [UnityTest]
        public IEnumerator Sandbox_BeginThenEnd_RestoresTheRealJournalAndProfile()
        {
            // Arrange
            var prefs = new PrefsSnapshot(introSeen: true, tutorialDone: true);
            try
            {
                yield return MainScene.Load();
                var p = ProgressManager.Instance;
                var journal = p.Journal; var profile = p.Profile;
                // Act
                p.BeginSandbox();
                bool swapped = p.Sandbox && p.Journal != journal && p.Profile != profile;
                p.EndSandbox();
                // Assert
                Assert.IsTrue(swapped, "a sandbox plays on a throwaway journal and profile");
                Assert.IsFalse(p.Sandbox);
                Assert.AreSame(journal, p.Journal);
                Assert.AreSame(profile, p.Profile);
            }
            finally { prefs.Dispose(); }
            yield return MainScene.Unload();
        }
```

- [ ] **Step 2: Run it and see it fail**

Run: `unity test Athanor --mode PlayMode --filter ManagerBootTests`
Expected: compile error, `ProgressManager could not be found`.

- [ ] **Step 3: Write `Scripts/Game/Managers/ProgressManager.cs`**

```csharp
using System.Collections.Generic;
using Athanor.Gameplay;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// What the player keeps between runs: the journal, the profile (stats, goals done, items won) and what is unlocked.
    /// A sandbox (How to play) swaps in a throwaway journal and profile and stores nothing.
    /// </summary>
    [DefaultExecutionOrder(-170)]
    public sealed class ProgressManager : Singleton<ProgressManager>
    {
        public Journal Journal { get; private set; }
        public Profile Profile { get; private set; }
        /// <summary>What was unlocked when the current run started; an item won during a run joins the next one.</summary>
        public Unlocks RunUnlocks { get; private set; }
        public bool Sandbox { get; private set; }
        Journal realJournal; Profile realProfile;
        static ContentDb Db => ContentManager.Instance.Db;

        protected override void OnAwake()
        {
            Journal = Save.LoadJournal();
            Profile = Save.LoadProfile(Db);
            RunUnlocks = Unlocks.For(Db, Profile);
        }

        public Unlocks RefreshUnlocks() => RunUnlocks = Unlocks.For(Db, Profile);

        public void StoreJournal() { if (!Sandbox) Save.StoreJournal(Journal); }
        public void StoreProfile() { if (!Sandbox) Save.StoreProfile(Profile); }

        /// <summary>Completes the goals whose targets are reached, draws their rewards and stores the profile.</summary>
        public List<GoalReward> EvaluateGoals()
        {
            var done = Profile.Evaluate(Db, Journal);
            StoreProfile();
            return done;
        }

        public void BeginSandbox()
        {
            realJournal = Journal; realProfile = Profile; Sandbox = true;
            Journal = new Journal(); Profile = new Profile(1);
        }

        public void EndSandbox()
        {
            Journal = realJournal; Profile = realProfile; Sandbox = false;
            RefreshUnlocks();
        }

        /// <summary>Erases goals, unlocks and stats (Reset progress). The journal is kept.</summary>
        public void ResetProgress()
        {
            Save.ResetProfile();
            Profile = Save.LoadProfile(Db);
            RefreshUnlocks();
        }

        /// <summary>Quit to title: back to what is stored, as if the page had been closed. A sandbox keeps its throwaway.</summary>
        public void ReloadAfterQuit()
        {
            Journal = JournalAfterQuit(Sandbox, Journal);
            Profile = ProfileAfterQuit(Sandbox, Profile, Db);
            RefreshUnlocks();
        }

        public static Journal JournalAfterQuit(bool sandbox, Journal current) => sandbox ? current : Save.LoadJournal();
        public static Profile ProfileAfterQuit(bool sandbox, Profile current, ContentDb db) => sandbox ? current : Save.LoadProfile(db);
    }
}
```

- [ ] **Step 4: Scene.** In `ManagerSceneMigration.Apply`, after `Ensure<SaveManager>(root);`, add `Ensure<ProgressManager>(root);`. Then run `unity run Athanor -- -executeMethod Athanor.EditorTools.ManagerSceneMigration.RunBatch`.

- [ ] **Step 5: Take the moved parts out of `GameManager`.** Exact edits:

1. Replace `public Journal Journal { get; private set; }` with `public Journal Journal => ProgressManager.Instance.Journal;`. Replace `public Profile Profile { get; private set; }` with `public Profile Profile => ProgressManager.Instance.Profile;`. Add `static ProgressManager Progress => ProgressManager.Instance;`.
2. Delete the fields `Unlocks unlocks;` and `Journal realJournal; Profile realProfile;`.
3. Replace `void StoreJournal() { if (!sandbox) Save.StoreJournal(Journal); }` with `void StoreJournal() => Progress.StoreJournal();`, and the same for `StoreProfile`.
4. In `OnAwake`, delete `Journal = Save.LoadJournal(); Profile = Save.LoadProfile(Db); unlocks = Unlocks.For(Db, Profile);`.
5. `PlayTutorialReplay` becomes:

```csharp
        IEnumerator PlayTutorialReplay()
        {
            flow.BeginSandbox(); Progress.BeginSandbox();
            yield return PlayRun(true);
            Progress.EndSandbox(); flow.EndSandbox();
            heldGoals.Clear();
        }
```

6. In `PlayRun`, replace `unlocks = Unlocks.For(Db, Profile);` with `var unlocks = Progress.RefreshUnlocks();`.
7. In `CheckGoals`, replace `var done = Profile.Evaluate(Db, Journal);` and the `StoreProfile();` after it with `var done = Progress.EvaluateGoals();`.
8. In `OpenJournal`, replace `(System.Func<string, bool>)unlocks.IsOpen` with `(System.Func<string, bool>)Progress.RunUnlocks.IsOpen`.
9. `ResetProgress` body becomes:

```csharp
            if (!flow.CanResetProgress) return;
            Progress.ResetProgress();
            toast.Clear();
            CheckGoals(false);           // the journal still counts toward the journal goals
            title.Progress(Journal.KnownPairs.Count, Db.Recipes.Count, Profile.Completed.Count, Db.Goals.Count);
```

10. In `AbandonRun`, replace the three lines `Journal = JournalAfterQuit(…); Profile = ProfileAfterQuit(…); unlocks = Unlocks.For(Db, Profile);` with `Progress.ReloadAfterQuit();`. Delete the static `JournalAfterQuit` and `ProfileAfterQuit` from `GameManager`.
11. In `Resolve`, replace `var earned = Profile.Evaluate(Db, Journal);` and the `StoreProfile();` after it with `var earned = Progress.EvaluateGoals();`.
12. In `PauseTests.cs`, replace `GameManager.ProfileAfterQuit` with `ProgressManager.ProfileAfterQuit` (2 places), and `GameManager.JournalAfterQuit` the same way if it appears.

`sandbox => flow.Sandbox` stays: `ScreenFlow` owns its own sandbox guard, and `ProgressManager.Sandbox` now always changes together with it.

Run: `grep -n "unlocks\b\|realJournal\|Save.LoadJournal\|Save.LoadProfile\|Profile.Evaluate" Athanor/Assets/Scripts/Game/Boot/GameManager.cs`
Expected: no output.

- [ ] **Step 6: Run every check**

```bash
unity test Athanor --mode EditMode
unity test Athanor --mode PlayMode
```

Expected: 0 failed, and the traces are identical.

- [ ] **Step 7: Checkpoint (owner commits)**

```
feat(managers): ProgressManager owns journal, profile, goals, unlocks and the sandbox

Story: R-17
```

---

### Task 18: `RunManager` (the run, its seed, the scripted player)

**Files:**
- Create: `Scripts/Game/Managers/RunManager.cs`
- Modify: `Scripts/Game/Boot/GameManager.cs`, `Editor/Migration/ManagerSceneMigration.cs`, `Tests/PlayMode/AutoplayTraceTests.cs`

- [ ] **Step 1: Point the trace test at the new home of the seed.** In `AutoplayTraceTests.Play`, replace `game.SeedOverride = seed;` with `RunManager.Instance.SeedOverride = seed;`.

- [ ] **Step 2: Run it and see it fail**

Run: `unity test Athanor --mode PlayMode --filter AutoplayTraceTests`
Expected: compile error, `RunManager could not be found`.

- [ ] **Step 3: Write `Scripts/Game/Managers/RunManager.cs`**

```csharp
using Athanor.Gameplay;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The run being played, its seed, and the scripted player (F9). Rules stay in RunState; this only owns it.</summary>
    [DefaultExecutionOrder(-160)]
    public sealed class RunManager : Singleton<RunManager>
    {
        [Tooltip("0 = a fresh seed each run. Other values: every run uses this seed (PlayMode traces, bug reproduction).")]
        [SerializeField] int seedOverride;

        public int SeedOverride { get => seedOverride; set => seedOverride = value; }
        /// <summary>The run being played; null before the first one.</summary>
        public RunState Run { get; private set; }
        public AutoPlayer AutoPlayer { get; private set; }
        /// <summary>The seed the current run started with (shown in the Inspector, for bug reports).</summary>
        public int LastSeed { get; private set; }
        int runs;

        protected override void OnAwake() => AutoPlayer = new AutoPlayer(ContentManager.Instance.Db);

        /// <summary>The next run's seed: the override, or a fresh one.</summary>
        public int NextSeed()
        {
            runs++;
            return seedOverride != 0 ? seedOverride : System.Environment.TickCount * 31 + runs;
        }

        /// <summary>Starts a run at depth 1 on the player's journal.</summary>
        public RunState Begin(RunOptions options, int seed)
        {
            LastSeed = seed;
            return Run = new RunState(ContentManager.Instance.Db, ProgressManager.Instance.Journal, seed, 1, options);
        }

        /// <summary>Ignites and stores the journal at once, so a discovery is saved before anything else can happen. A sandbox stores nothing.</summary>
        public IgniteOutcome Settle()
        {
            var outcome = Run.Ignite();
            ProgressManager.Instance.StoreJournal();
            return outcome;
        }
    }
}
```

- [ ] **Step 4: Scene.** Add `Ensure<RunManager>(root);` to `ManagerSceneMigration.Apply` after `ProgressManager`, then run the batch.

- [ ] **Step 5: Take the moved parts out of `GameManager`.** Exact edits:

1. Replace `public RunState Run { get; private set; }` with `public RunState Run => RunManager.Instance.Run;`. Add `static RunManager Runs => RunManager.Instance;`.
2. Delete the fields `AutoPlayer autoPlayer;`, `int runs;`, `seedOverride` and the `SeedOverride` property from Task 2.
3. In `OnAwake`, delete `autoPlayer = new AutoPlayer(Db);`.
4. In `PlayRun`, replace `runs++;` and the two seed lines with:

```csharp
            Runs.Begin(options, tutorialRun ? TutorialScript.Seed : Runs.NextSeed());
```

5. Replace both `autoPlayer.Decide(Run)` with `Runs.AutoPlayer.Decide(Run)`.
6. In `Resolve`, replace `var outcome = SettleIgnite(Run, Journal, sandbox);` with `var outcome = Runs.Settle();`. Delete the static `SettleIgnite` method.

Run: `grep -n "autoPlayer\|seedOverride\|new RunState\|SettleIgnite\|runs++" Athanor/Assets/Scripts/Game/Boot/GameManager.cs`
Expected: no output.

The original code incremented `runs` for the tutorial run too. `NextSeed` is not called for a tutorial run, so the second run's fresh seed differs from before. That is invisible: fresh seeds are random by design, and the traces use the override.

- [ ] **Step 6: Run every check**

```bash
unity test Athanor --mode EditMode
unity test Athanor --mode PlayMode
```

Expected: 0 failed, and the traces are identical.

- [ ] **Step 7: Checkpoint (owner commits)**

```
feat(managers): RunManager owns the run, its seed and the scripted player

Story: R-18
```

---

### Task 19: PlayMode checks for a second session and a clean console

**Files:**
- Test: `Tests/PlayMode/ManagerBootTests.cs` (add two tests)

- [ ] **Step 1: Add the tests**

```csharp
        [UnityTest]
        public IEnumerator Load_TwiceInOnePlaySession_ManagersAreTheSecondScenes()
        {
            // Arrange
            var prefs = new PrefsSnapshot(introSeen: true, tutorialDone: true);
            try
            {
                yield return MainScene.Load();
                var first = RunManager.Instance;
                yield return MainScene.Unload();
                bool clearedOnUnload = !RunManager.Exists;
                // Act
                yield return MainScene.Load();
                // Assert
                Assert.IsTrue(clearedOnUnload, "unloading Main forgets its managers");
                Assert.IsTrue(RunManager.Exists);
                Assert.AreNotSame(first, RunManager.Instance);
            }
            finally { prefs.Dispose(); }
            yield return MainScene.Unload();
        }

        [UnityTest]
        public IEnumerator Load_MainScene_LogsNoErrorAndReachesTheTitle()
        {
            // Arrange
            var prefs = new PrefsSnapshot(introSeen: true, tutorialDone: true);
            try
            {
                // Act
                yield return MainScene.Load();
                for (int i = 0; i < 120 && GameManager.Instance.State != Athanor.Gameplay.FlowState.Title; i++) yield return null;
                // Assert
                Assert.AreEqual(Athanor.Gameplay.FlowState.Title, GameManager.Instance.State);
                LogAssert.NoUnexpectedReceived();
            }
            finally { prefs.Dispose(); }
            yield return MainScene.Unload();
        }
```

- [ ] **Step 2: Run them**

Run: `unity test Athanor --mode PlayMode --filter ManagerBootTests`
Expected: 4 passed. If a test fails because of a doubled event handler or a leftover static, fix its owner's `ResetStatics`/`OnDestroy`. Do not change the test.

- [ ] **Step 3: Manual Play check (owner, once).** In the Editor, press Play, then Stop, then Play again. Go Title → Bench → Journal → Title. There should be no console error, and each click should act once. This closes ATHANOR.md §7 item 11(f).

- [ ] **Step 4: Checkpoint (owner commits)**

```
test(playmode): a second load of Main and a clean boot to the title

Story: R-19
```

---

# Stage 5: The tutorial is rebuilt as data

### Task 20: Tutorial data types

**Files:**
- Create: `Scripts/Game/Tutorial/TutorialTypes.cs`, `Scripts/Game/Tutorial/TutorialSequence.cs`
- Move: `Scripts/Game/Cutscene/TutorialHole.cs`, `TutorialSettings.cs` → `Scripts/Game/Tutorial/` (+ metas)
- Modify: `Scripts/Game/Cutscene/TutorialArea.cs` (move the shared enums and `TutorialRect` out)

- [ ] **Step 1: Move the overlay parts that stay**

```bash
mkdir -p Athanor/Assets/Scripts/Game/Tutorial
for f in TutorialHole TutorialSettings; do
  git mv Athanor/Assets/Scripts/Game/Cutscene/$f.cs Athanor/Assets/Scripts/Game/Tutorial/$f.cs
  git mv Athanor/Assets/Scripts/Game/Cutscene/$f.cs.meta Athanor/Assets/Scripts/Game/Tutorial/$f.cs.meta
done
```

- [ ] **Step 2: `Scripts/Game/Tutorial/TutorialTypes.cs`.** Cut `TutorialRect`, `AreaKind`, `ViewRef` and `ScriptTrial` out of `Cutscene/TutorialArea.cs` and paste them here unchanged. Then add the step and area definitions:

```csharp
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    // (TutorialRect, AreaKind, ViewRef, ScriptTrial: moved here unchanged from Cutscene/TutorialArea.cs)

    /// <summary>One lit window of a step. Fixed uses rect; ScriptTile and View follow the game at run time.</summary>
    [Serializable]
    public sealed class TutorialAreaDef
    {
        public string name = "";
        public AreaKind kind = AreaKind.Fixed;
        [Tooltip("Fixed: art pixels, top-left origin (x, y, width, height).")] public Rect rect;
        [Tooltip("ScriptTile: the scripted hand.")] public ScriptTrial trial = ScriptTrial.Reaction;
        [Tooltip("ScriptTile: index in the hand's lay; -1 = all of its tiles.")] public int slot;
        [Tooltip("View: the view to follow.")] public ViewRef view = ViewRef.Bench;
        [Tooltip("View JournalCell: the material shown in the Journal.")] public string cellId = "";
        [Tooltip("Pixels added on left, top, right, bottom.")] public Vector4 margin;
        [Tooltip("Clicks in this window reach the game. Off = highlight only.")] public bool clicksPass;
    }

    /// <summary>One tutorial line: its text, whether the screen darkens and locks, and what it lights.</summary>
    [Serializable]
    public sealed class TutorialStepDef
    {
        [Tooltip("The id the script names, e.g. 1.4 or 5.4i.")] public string id = "";
        [Tooltip("Lang key, e.g. tutorial.1_4. Two steps may share one.")] public string textKey = "";
        [Tooltip("Dim the screen outside the areas.")] public bool dark = true;
        [Tooltip("Clicks on the dark pulse the rim; clicks in pass-through areas reach the game.")] public bool lockPad = true;
        [Tooltip("Read steps: a click in a non-pass area advances the text. Off: it only pulses the rim.")] public bool clickAdvances = true;
        [Tooltip("Own text box position (art pixels, top-left). Off = TutorialStyle.boxPos.")] public bool useBoxPos;
        public Vector2 boxPos;
        [Tooltip("In order; the arrow sits over the last one.")] public List<TutorialAreaDef> areas = new List<TutorialAreaDef>();
    }

    /// <summary>What one entry of the lesson script does.</summary>
    public enum TutorialActionKind
    {
        /// <summary>Wait until the bench is the player's and the trial counter is <c>count</c>.</summary>
        WaitBench,
        /// <summary>Show <c>step</c> and wait for a click.</summary>
        Say,
        /// <summary>Show <c>step</c> until the first <c>count</c> lays of the current trial are on the bench (-1 = all).</summary>
        AwaitLay,
        /// <summary>Show <c>step</c> until the player Ignites, then wait for the Ignite to play out.</summary>
        Ignite,
        /// <summary>Show <c>step</c> until the Journal opens.</summary>
        AwaitJournal,
        /// <summary>Open the Journal page of the step's first JournalCell area, then Say.</summary>
        SayOnJournalPage,
        /// <summary>Open the page, show <c>step</c> and keep it up while the Journal stays open.</summary>
        HoldWhileJournal,
        /// <summary>Work out the score of the current trial without its last lay (shown later by Broken). Nothing is spent.</summary>
        MeasureBrokenChain,
        /// <summary>Show <c>step</c> over the current trial without its last lay, greyed, beside the measured score.</summary>
        Broken,
        /// <summary>Show <c>step</c> with one line per reaction of the last Ignite: base x first x chain = total.</summary>
        ChainSummary,
        /// <summary>Wait until the Ignite has played out and the bench is back.</summary>
        WaitResolved,
    }

    [Serializable]
    public struct TutorialAction
    {
        public TutorialActionKind kind;
        [Tooltip("The step this action shows (its id).")] public string step;
        [Tooltip("AwaitLay: lays needed (-1 = all). WaitBench: the trial index.")] public int count;
    }

    [Serializable]
    public struct TutorialLayDef
    {
        public Athanor.Gameplay.MaterialDefinition material;
        [Range(0, 8)] public int tile;
    }

    /// <summary>One scripted trial and the lines it plays.</summary>
    [Serializable]
    public sealed class TutorialTrialDef
    {
        public string name = "";
        [Tooltip("The hand dealt for this trial.")] public Athanor.Gameplay.MaterialDefinition[] hand = new Athanor.Gameplay.MaterialDefinition[0];
        [Tooltip("Where each material goes (this tile, not any lit tile: another order can break the chain).")] public TutorialLayDef[] lay = new TutorialLayDef[0];
        [Tooltip("What the Ignite makes, in chain order (empty for the miss). TutorialSequenceTests check it.")] public Athanor.Gameplay.MaterialDefinition[] makes = new Athanor.Gameplay.MaterialDefinition[0];
        [Tooltip("Shown with the score parts after each reaction (empty: none).")] public string reactionStep = "";
        [Tooltip("Read after reactionStep (empty: none).")] public string reactionFollowUp = "";
        [Tooltip("reactionStep uses the 'tutorial.step' text with the chain step number.")] public bool reactionChainText;
        [Tooltip("Shown when an Ignite of this trial fizzles (no reaction, not a sputter).")] public string fizzleStep = "";
        [Tooltip("Played after this trial's Ignite, before the next hand is dealt.")] public List<TutorialAction> afterIgnite = new List<TutorialAction>();
    }

    [Serializable]
    public sealed class CatalystTrialDef
    {
        public Athanor.Gameplay.CatalystDefinition catalyst;
        public TutorialTrialDef trial = new TutorialTrialDef();
    }
}
```

- [ ] **Step 3: `Scripts/Game/Tutorial/TutorialSequence.cs`**

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Athanor.Gameplay;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The whole tutorial as data: its run settings, the scripted trials, the steps (text and lit areas) and the lesson order.
    /// TutorialManager plays it; nothing in the manager names a step id.
    /// </summary>
    [CreateAssetMenu(menuName = "Athanor/Tutorial Sequence", fileName = "Tutorial")]
    public sealed class TutorialSequence : ScriptableObject
    {
        [Header("Run")]
        [Tooltip("The tutorial run's seed.")] public int seed;
        [Tooltip("No aspect offer and no event before this depth, so nothing touches the scripted hands.")] public int quietBefore;
        [Tooltip("The first catalyst offer.")] public CatalystDefinition[] firstOffer = new CatalystDefinition[0];

        [Header("Scripted trials (index = trial number)")]
        public TutorialTrialDef[] trials = new TutorialTrialDef[0];
        [Tooltip("The trial after the first catalyst choice, by the catalyst taken.")] public CatalystTrialDef[] catalystTrials = new CatalystTrialDef[0];

        [Header("The catalyst offer")]
        [Tooltip("Read before the cards open.")] public string[] offerLines = new string[0];
        [Tooltip("Shown while the player picks (taken down when the card is taken).")] public string offerHold = "";

        [Header("Steps and script")]
        public List<TutorialStepDef> steps = new List<TutorialStepDef>();
        public List<TutorialAction> script = new List<TutorialAction>();

        public TutorialStepDef Step(string id) => steps.FirstOrDefault(s => s.id == id) ?? throw new InvalidOperationException("No tutorial step " + id + " in " + name);

        public TutorialTrial Trial(int i) => ToTrial(trials[i]);

        /// <summary>The trial after choosing this catalyst; null for a catalyst not in the list.</summary>
        public TutorialTrial CatalystTrial(string catalystId) => ToTrial(CatalystTrialDef(catalystId));

        public TutorialTrialDef CatalystTrialDef(string catalystId) =>
            catalystTrials.FirstOrDefault(c => c.catalyst != null && c.catalyst.id == catalystId)?.trial;

        public string[] FirstOfferIds() => firstOffer.Select(c => c.id).ToArray();

        /// <summary>The run options of the tutorial run: the game's, with the quiet start and the fixed first offer.</summary>
        public RunOptions Options(Unlocks unlocks) => new RunOptions
        {
            Unlocks = unlocks, Aspects = true, Events = true, QuietBefore = quietBefore, FirstOffer = FirstOfferIds(),
        };

        public static TutorialTrial ToTrial(TutorialTrialDef d) => d == null ? null : new TutorialTrial
        {
            Hand = d.hand.Select(m => m.id).ToArray(),
            Lay = d.lay.Select(l => new TutorialLay(l.material.id, l.tile)).ToArray(),
            Makes = d.makes.Select(m => m.id).ToArray(),
        };

        /// <summary>The bench of a trial, 9 tiles, null where empty.</summary>
        public static string[] Layout(TutorialTrial t) => Layout(t.Lay);

        /// <summary>The trial without its last lay: the broken chain that Broken compares against.</summary>
        public static string[] BrokenLayout(TutorialTrial t) => Layout(t.Lay.Take(t.Lay.Length - 1).ToArray());

        static string[] Layout(TutorialLay[] lay)
        {
            var tiles = new string[Bench.TileCount];
            foreach (var l in lay) tiles[l.Tile] = l.Id;
            return tiles;
        }
    }
}
```

- [ ] **Step 4: Compile**

Run: `unity test Athanor --mode EditMode --filter TutorialAreaTests`
Expected: 4 passed. `TutorialRect` moved but kept its name and namespace.

- [ ] **Step 5: Checkpoint (owner commits)**

```
feat(tutorial): add TutorialSequence and its step, area, trial and action types

Story: R-20
```

---

### Task 21: Migrate the scene steps and `TutorialScript` into `Tutorial.asset`

**Files:**
- Create: `Editor/Migration/TutorialSequenceMigration.cs`
- Test: `Tests/EditModeGame/TutorialSequenceTests.cs`
- Create (generated): `Athanor/Assets/Content/Tutorial/Tutorial.asset`

- [ ] **Step 1: Write the failing test** `Tests/EditModeGame/TutorialSequenceTests.cs`

```csharp
using System.Linq;
using Athanor.Game;
using Athanor.Gameplay;
using NUnit.Framework;
using UnityEditor;

namespace Athanor.Tests
{
    /// <summary>Tutorial.asset holds what TutorialScript and the scene's step objects held.</summary>
    public class TutorialSequenceTests
    {
        const string Path = "Assets/Content/Tutorial/Tutorial.asset";
        static TutorialSequence Seq() => AssetDatabase.LoadAssetAtPath<TutorialSequence>(Path);

        static void Same(TutorialTrial want, TutorialTrial got, string what)
        {
            CollectionAssert.AreEqual(want.Hand, got.Hand, what + " hand");
            CollectionAssert.AreEqual(want.Lay.Select(l => l.Id + "@" + l.Tile), got.Lay.Select(l => l.Id + "@" + l.Tile), what + " lay");
            CollectionAssert.AreEqual(want.Makes, got.Makes, what + " makes");
        }

        [Test]
        public void Sequence_RunSettings_EqualTutorialScript()
        {
            // Arrange
            var s = Seq();
            // Act / Assert
            Assert.IsNotNull(s, Path);
            Assert.AreEqual(TutorialScript.Seed, s.seed);
            Assert.AreEqual(TutorialScript.QuietBefore, s.quietBefore);
            CollectionAssert.AreEqual(TutorialScript.Offer, s.FirstOfferIds());
        }

        [Test]
        public void Sequence_Trials_EqualTutorialScript()
        {
            // Arrange
            var s = Seq();
            // Act / Assert
            Same(TutorialScript.Reaction, s.Trial(0), "reaction");
            Same(TutorialScript.Miss, s.Trial(1), "miss");
            Same(TutorialScript.Chain, s.Trial(2), "chain");
            foreach (var id in TutorialScript.Offer) Same(TutorialScript.CatalystTrial(id), s.CatalystTrial(id), id);
            CollectionAssert.AreEqual(TutorialScript.BrokenChain(), TutorialSequence.BrokenLayout(s.Trial(2)));
        }

        [Test]
        public void Sequence_EveryStepTheScriptNames_Exists()
        {
            // Arrange
            var s = Seq();
            var named = s.script.Select(a => a.step)
                .Concat(s.trials.SelectMany(t => new[] { t.reactionStep, t.reactionFollowUp, t.fizzleStep }.Concat(t.afterIgnite.Select(a => a.step))))
                .Concat(s.catalystTrials.SelectMany(c => new[] { c.trial.reactionStep }))
                .Concat(s.offerLines).Concat(new[] { s.offerHold })
                .Where(id => !string.IsNullOrEmpty(id)).Distinct();
            // Act
            var missing = named.Where(id => s.steps.All(st => st.id != id)).ToList();
            // Assert
            CollectionAssert.IsEmpty(missing);
        }
    }
}
```

- [ ] **Step 2: Run it and see it fail**

Run: `unity test Athanor --mode EditMode --filter TutorialSequenceTests`
Expected: 3 failed. `Sequence_RunSettings_EqualTutorialScript` reports `Assets/Content/Tutorial/Tutorial.asset` is null.

- [ ] **Step 3: Write the migration** `Editor/Migration/TutorialSequenceMigration.cs`

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Athanor.Game;
using Athanor.Gameplay;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Athanor.EditorTools
{
    /// <summary>
    /// One-shot: writes Assets/Content/Tutorial/Tutorial.asset from TutorialScript and the step objects under Main.unity › Tutorial,
    /// with the lesson order that TutorialController.Drive played. Then sets the Tutorial object's sequence field.
    /// </summary>
    public static class TutorialSequenceMigration
    {
        const string ScenePath = "Assets/Scenes/Main.unity", AssetPath = "Assets/Content/Tutorial/Tutorial.asset";

        [MenuItem("Athanor/Migrate/Tutorial Steps To Sequence Asset")]
        public static void Run()
        {
            var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            var controller = scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<TutorialController>(true)).Single();
            var seq = AssetDatabase.LoadAssetAtPath<TutorialSequence>(AssetPath);
            if (seq == null)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(AssetPath));
                seq = ScriptableObject.CreateInstance<TutorialSequence>();
                AssetDatabase.CreateAsset(seq, AssetPath);
            }
            Fill(seq, controller);
            EditorUtility.SetDirty(seq);
            AssetDatabase.SaveAssets();
            var so = new SerializedObject(controller);
            so.FindProperty("sequence").objectReferenceValue = seq;
            so.ApplyModifiedPropertiesWithoutUndo();
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
        }

        public static void RunBatch()
        {
            try { Run(); }
            catch (Exception e) { Debug.LogError(e); EditorApplication.Exit(1); }
        }

        static MaterialDefinition Mat(ContentDatabase db, string id) => db.materials.Single(m => m.id == id);
        static CatalystDefinition Cat(ContentDatabase db, string id) => db.catalysts.Single(c => c.id == id);

        static TutorialTrialDef Trial(ContentDatabase db, string name, TutorialTrial t) => new TutorialTrialDef
        {
            name = name,
            hand = t.Hand.Select(id => Mat(db, id)).ToArray(),
            lay = t.Lay.Select(l => new TutorialLayDef { material = Mat(db, l.Id), tile = l.Tile }).ToArray(),
            makes = t.Makes.Select(id => Mat(db, id)).ToArray(),
        };

        static TutorialAction A(TutorialActionKind k, string step = "", int count = 0) => new TutorialAction { kind = k, step = step, count = count };

        static void Fill(TutorialSequence seq, TutorialController controller)
        {
            var db = ContentAssets.Database();
            seq.seed = TutorialScript.Seed;
            seq.quietBefore = TutorialScript.QuietBefore;
            seq.firstOffer = TutorialScript.Offer.Select(id => Cat(db, id)).ToArray();

            var reaction = Trial(db, "Reaction", TutorialScript.Reaction);
            reaction.reactionStep = "1.6"; reaction.reactionFollowUp = "1.6b";
            reaction.afterIgnite = new List<TutorialAction> { A(TutorialActionKind.Say, "1.7") };
            var miss = Trial(db, "Miss", TutorialScript.Miss);
            miss.fizzleStep = "2.3";
            var chain = Trial(db, "Chain", TutorialScript.Chain);
            chain.reactionStep = "3.3e"; chain.reactionChainText = true;
            chain.afterIgnite = new List<TutorialAction>
            {
                A(TutorialActionKind.Say, "3.4"), A(TutorialActionKind.ChainSummary, "3.5"), A(TutorialActionKind.Broken, "3.6"),
            };
            seq.trials = new[] { reaction, miss, chain };
            seq.catalystTrials = TutorialScript.Offer.Select(id =>
            {
                var t = Trial(db, id, TutorialScript.CatalystTrial(id));
                t.reactionStep = "5.4r";
                return new CatalystTrialDef { catalyst = Cat(db, id), trial = t };
            }).ToArray();
            seq.offerLines = new[] { "5.1", "5.2" };
            seq.offerHold = "5.3";

            // the order Drive played (TutorialController.cs, Drive)
            seq.script = new List<TutorialAction>
            {
                A(TutorialActionKind.WaitBench, count: 0), A(TutorialActionKind.Say, "1.1"),
                A(TutorialActionKind.AwaitLay, "1.2", 1), A(TutorialActionKind.AwaitLay, "1.3", 2),
                A(TutorialActionKind.Say, "1.4"), A(TutorialActionKind.Ignite, "1.5"),
                A(TutorialActionKind.WaitBench, count: 1), A(TutorialActionKind.AwaitLay, "2.1", 2), A(TutorialActionKind.Ignite, "2.2"),
                A(TutorialActionKind.WaitBench, count: 2), A(TutorialActionKind.AwaitJournal, "2.4"),
                A(TutorialActionKind.SayOnJournalPage, "2.5"), A(TutorialActionKind.Say, "2.6"), A(TutorialActionKind.HoldWhileJournal, "2.7"),
                A(TutorialActionKind.Say, "3.0"), A(TutorialActionKind.AwaitLay, "3.1", 3), A(TutorialActionKind.Say, "3.2"),
                A(TutorialActionKind.MeasureBrokenChain), A(TutorialActionKind.Ignite, "3.3"),
                A(TutorialActionKind.WaitBench, count: 3), A(TutorialActionKind.AwaitLay, "5.4", -1), A(TutorialActionKind.Ignite, "5.4i"),
                A(TutorialActionKind.WaitResolved), A(TutorialActionKind.Say, "5.5"),
            };

            seq.steps = controller.GetComponentsInChildren<TutorialStep>(true).Select(st => new TutorialStepDef
            {
                id = st.name.StartsWith("Step ") ? st.name.Substring(5) : st.name,
                textKey = st.textKey, dark = st.dark, lockPad = st.lockPad, clickAdvances = st.clickAdvances,
                useBoxPos = st.useBoxPos, boxPos = st.boxPos,
                areas = st.GetComponentsInChildren<TutorialArea>(true).Where(a => a.transform.parent == st.transform && a.gameObject.activeSelf && a.enabled).Select(a => new TutorialAreaDef
                {
                    name = a.name, kind = a.kind, rect = a.kind == AreaKind.Fixed ? a.FixedArtRect() : default,
                    trial = a.trial, slot = a.slot, view = a.view, cellId = a.cellId, margin = a.margin, clicksPass = a.clicksPass,
                }).ToList(),
            }).ToList();
        }
    }
}
```

The `areas` filter (`activeSelf && enabled`) matches what `TutorialStep.Areas()` kept (`isActiveAndEnabled`). The game uses `isActiveAndEnabled`, but it reads false for every object in a batch run, where the Tutorial root is inactive, so the migration checks each area itself instead.

- [ ] **Step 4: Add the field the migration sets.** In `TutorialController.cs`, under `[SerializeField] TutorialStyle style …`:

```csharp
        /// <summary>The tutorial as data (Assets/Content/Tutorial/Tutorial.asset).</summary>
        [SerializeField] TutorialSequence sequence;
```

- [ ] **Step 5: Run the migration and the test**

```bash
unity run Athanor -- -executeMethod Athanor.EditorTools.TutorialSequenceMigration.RunBatch
unity test Athanor --mode EditMode --filter TutorialSequenceTests
```

Expected: exit 0, then 3 passed. The asset lists 30 steps.

- [ ] **Step 6: Checkpoint (owner commits)**

```
feat(tutorial): migrate the scene steps and TutorialScript into Tutorial.asset

Story: R-21
```

---

### Task 22: `TutorialManager` plays the sequence

**Files:**
- Move: `Scripts/Game/Cutscene/TutorialController.cs` → `Scripts/Game/Tutorial/TutorialManager.cs` (+ meta; same GUID)
- Modify: `TutorialManager.cs`, `Scripts/Game/Boot/GameManager.cs`, `Scripts/Game/Boot/IgniteSequence.cs` (type name only), tests that name `TutorialController`

- [ ] **Step 1: Rename with the GUID**

```bash
git mv Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs Athanor/Assets/Scripts/Game/Tutorial/TutorialManager.cs
git mv Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs.meta Athanor/Assets/Scripts/Game/Tutorial/TutorialManager.cs.meta
grep -rlZ -w "TutorialController" Athanor/Assets --include="*.cs" | xargs -0 perl -pi -e 's/\bTutorialController\b/TutorialManager/g'
```

- [ ] **Step 2: Make it a singleton and read steps from the asset.** In `TutorialManager.cs`:

1. `public sealed class TutorialManager : MonoBehaviour` becomes `[DefaultExecutionOrder(-150)] public sealed class TutorialManager : Singleton<TutorialManager>`.
2. `void Awake()` becomes `protected override void OnAwake()`. Add a null check as its first line: `if (sequence == null) throw new InvalidOperationException("TutorialManager: sequence is not set (Assets/Content/Tutorial/Tutorial.asset)");`.
3. Add `public TutorialSequence Sequence => sequence;`.
4. Delete the field `Dictionary<string, TutorialStep> stepsById;` and replace the whole `public TutorialStep Step(string id)` method with:

```csharp
        /// <summary>The step with this id in the sequence.</summary>
        public TutorialStepDef Step(string id) => sequence.Step(id);
```

5. Change the type `TutorialStep` to `TutorialStepDef` in the signatures of `Show`, `Resolve` and `Holes`. In `Resolve`, replace `foreach (var a in step.Areas())` with `foreach (var a in step.areas)`, `List<(TutorialArea area, Rect rect)>` with `List<(TutorialAreaDef area, Rect rect)>`, `var list = new List<(TutorialArea, Rect)>();` with `var list = new List<(TutorialAreaDef, Rect)>();`, and `case AreaKind.Fixed: r = a.FixedArtRect(); break;` with `case AreaKind.Fixed: r = a.rect; break;`.
6. In `ViewRect`, change the parameter type `TutorialArea a` to `TutorialAreaDef a`.
7. In `OpenJournalPage`, replace `foreach (var a in Step(id).Areas())` with `foreach (var a in Step(id).areas)`.
8. Replace `TrialOf` with:

```csharp
        TutorialTrial TrialOf(ScriptTrial s) => s switch
        {
            ScriptTrial.Reaction => sequence.Trial(0),
            ScriptTrial.Miss => sequence.Trial(1),
            ScriptTrial.Chain => sequence.Trial(2),
            ScriptTrial.Catalyst => catalystTrial,
            _ => null,
        };
```

9. Replace `TutorialScript.Chain.Lay` in `Broken()` with `CurrentTrial().Lay`, then make `Broken` take the step id: `IEnumerator Broken(string id)`, with `var step = Step(id);`.

- [ ] **Step 3: Replace the driver and the hooks with the sequence runner.** Delete `Drive()`, `AfterEvent`, `AfterIgnite`, `AfterChoose`, `OnOffer` and the field `TutorialTrial catalystTrial;`. In their place:

```csharp
        TutorialTrial catalystTrial; TutorialTrialDef catalystDef;
        IgniteOutcomeView lastIgnite;

        /// <summary>The trial being taught now: a scripted one, or the catalyst trial after them.</summary>
        TutorialTrial CurrentTrial() => trial < sequence.trials.Length ? sequence.Trial(trial) : catalystTrial;
        TutorialTrialDef CurrentDef() => trial < sequence.trials.Length ? sequence.trials[trial] : catalystDef;

        /// <summary>Runs the sequence's script from the first bench to its last line. The game plays its own Ignite, descents and choice between.</summary>
        public IEnumerator Drive()
        {
            Dev.Hook("[drive] start");
            trial = 0; Finished = false; Shown.Clear(); catalystTrial = null; catalystDef = null;
            foreach (var a in sequence.script) yield return Play(a);
            Finished = true;
            TutorialRules.MarkDone();
        }

        IEnumerator Play(TutorialAction a)
        {
            var t = CurrentTrial();
            switch (a.kind)
            {
                case TutorialActionKind.WaitBench:
                    while (!(f.State == FlowState.Bench && trial == a.count && f.Bench.InputEnabled)) yield return null;
                    yield return null;
                    break;
                case TutorialActionKind.Say:
                    yield return Say(a.step);
                    break;
                case TutorialActionKind.AwaitLay:
                    if (t == null) break;                                   // a catalyst with no scripted trial
                    int n = a.count < 0 ? t.Lay.Length : a.count;
                    yield return Await(a.step, () => Laid(t, n), bot: () => Lay(t, n));
                    break;
                case TutorialActionKind.Ignite:
                    if (t == null) break;
                    yield return IgniteStep(a.step);
                    break;
                case TutorialActionKind.WaitResolved:
                    if (t == null) break;
                    while (f.State != FlowState.Bench) yield return null;
                    break;
                case TutorialActionKind.AwaitJournal:
                    yield return Await(a.step, () => f.State == FlowState.Journal, bot: f.RequestJournal);
                    break;
                case TutorialActionKind.SayOnJournalPage:
                    OpenJournalPage(a.step);
                    yield return Say(a.step);
                    break;
                case TutorialActionKind.HoldWhileJournal:
                    OpenJournalPage(a.step);
                    var hold = Step(a.step);
                    Show(hold, Loc.T(hold.textKey), false);
                    while (f.State == FlowState.Journal) { if (AutoDrive) f.JournalView.Close(); yield return null; }
                    Hide();
                    break;
                case TutorialActionKind.MeasureBrokenChain:
                    brokenTotal = f.Run.Preview(TutorialSequence.BrokenLayout(t)).Sum(e => e.Potency);   // worked out before the Ignite, so nothing is spent
                    break;
                case TutorialActionKind.Broken:
                    yield return Broken(a.step);
                    break;
                case TutorialActionKind.ChainSummary:
                    var parts = lastIgnite.Events.Where(e => e.Result != null).Select(e => ScoreParts.Of(f.Db, e)).Where(p => p != null).ToList();
                    yield return SayText(a.step, parts.Select(p => $"{p.Base * p.Used} x{p.First} x{p.Step} = {HudView.Number(p.Total)}").ToArray());
                    break;
            }
        }

        // ---- hooks called by GameManager and IgniteSequence ------------------------------------------

        /// <summary>After a reaction (or fizzle) has played: the held popups of the current trial.</summary>
        public IEnumerator AfterEvent(ReactionEvent e, int index)
        {
            if (f == null) yield break;
            var def = CurrentDef();
            if (def == null) yield break;
            var parts = ScoreParts.Of(f.Db, e);
            if (parts == null)
            {
                if (!string.IsNullOrEmpty(def.fizzleStep) && !e.Sputter) yield return Say(def.fizzleStep);
                yield break;
            }
            Shown.Add(parts.Total);
            if (string.IsNullOrEmpty(def.reactionStep)) yield break;
            string text = def.reactionChainText ? Loc.T("tutorial.step", ("n", e.Chain)) : null;
            yield return SayText(def.reactionStep, PartLines(parts), e.Hub, text);
            if (!string.IsNullOrEmpty(def.reactionFollowUp)) yield return Say(def.reactionFollowUp, e.Hub);
        }

        /// <summary>After the Ignite has played and its materials are spent: the trial's lines, then the next scripted hand.</summary>
        public IEnumerator AfterIgnite(IgniteOutcomeView o)
        {
            if (f == null) yield break;
            lastIgnite = o;
            var def = CurrentDef();
            if (def != null) foreach (var a in def.afterIgnite) yield return Play(a);
            trial++;
            if (trial < sequence.trials.Length && f.Run.Phase == RunPhase.Playing)
            {
                for (int t = 0; t < Gameplay.Bench.TileCount; t++) f.Run.Unplace(t);     // a fizzle leaves the materials on the bench
                f.Run.Deal(sequence.Trial(trial).Hand);
            }
        }

        /// <summary>The catalyst was taken: deal the scripted hand that shows it.</summary>
        public void AfterChoose(string catalystId, string name)
        {
            catalystName = name;
            catalystDef = sequence.CatalystTrialDef(catalystId);
            catalystTrial = TutorialSequence.ToTrial(catalystDef);
            if (catalystTrial != null) f.Run.Deal(catalystTrial.Hand);
        }

        /// <summary>The lines of the catalyst offer; the choice itself is the game's own screen.</summary>
        public IEnumerator OnOffer()
        {
            foreach (var id in sequence.offerLines) yield return Say(id);
            if (string.IsNullOrEmpty(sequence.offerHold)) yield break;
            var hold = Step(sequence.offerHold);
            Show(hold, Loc.T(hold.textKey), false);
        }
```

Behaviour notes, checked against the old code:
- Old `AfterEvent` case 1 (miss) showed 2.3 only when `trial == 1`. The new code shows `fizzleStep` only for the trial that has one, which is the Miss trial.
- Old `AfterIgnite` dealt the next hand when `trial < 3`. The new code deals it when `trial < sequence.trials.Length`, and the length is 3.
- Old `Drive` wrapped 5.4 and 5.4i in `if (ct != null)`. The new `AwaitLay`, `Ignite` and `WaitResolved` skip when `CurrentTrial()` is null.
- The catalyst trial's reaction popup was case 3 (`5.4r`). It is now the `reactionStep` of each catalyst trial.

- [ ] **Step 4: `GameManager` reads the sequence.** Replace:
- `TutorialScript.Options(unlocks)` with `tutorial.Sequence.Options(unlocks)`;
- `TutorialScript.Seed` with `tutorial.Sequence.seed`;
- `TutorialScript.Reaction.Hand` (in `PlayRun`) with `tutorial.Sequence.Trial(0).Hand`.

Check the leftovers: `grep -rn "TutorialScript\." Athanor/Assets/Scripts`
Expected: only `TutorialScript.Plan` (two places in `GameManager`). Task 24 renames it.

- [ ] **Step 5: Run every check, the tutorial trace first**

```bash
unity test Athanor --mode PlayMode --filter TutorialTraceTests
unity test Athanor --mode EditMode
unity test Athanor --mode PlayMode
```

Expected: the tutorial trace is identical line for line, `[shown]` included, and everything else is 0 failed. A diff in the trace means a hook does not match the old switch. Read the first differing line and fix the runner, not the baseline.

- [ ] **Step 6: Checkpoint (owner commits)**

```
refactor(tutorial)!: TutorialManager plays Tutorial.asset instead of a hard-coded Drive

The PlayMode tutorial trace is identical to the recorded one.
Story: R-22
```

---

### Task 23: Port the tutorial tests to the asset

**Files:**
- Move: `Tests/EditMode/TutorialTests.cs` → `Tests/EditModeGame/TutorialTests.cs` (it now needs the Game assembly and the AssetDatabase)
- Modify: `TutorialTests.cs`, `Tests/EditModeGame/TutorialStepsBakeTests.cs`, `TutorialFlowTests.cs`, `TutorialAreaTests.cs`

- [ ] **Step 1: Move**

```bash
git mv Athanor/Assets/Tests/EditMode/TutorialTests.cs Athanor/Assets/Tests/EditModeGame/TutorialTests.cs
git mv Athanor/Assets/Tests/EditMode/TutorialTests.cs.meta Athanor/Assets/Tests/EditModeGame/TutorialTests.cs.meta
```

- [ ] **Step 2: Read the asset in `TutorialTests`.** Add at the top of the class:

```csharp
        static readonly TutorialSequence Seq = UnityEditor.AssetDatabase.LoadAssetAtPath<TutorialSequence>("Assets/Content/Tutorial/Tutorial.asset");
```

Apply this mapping with `perl -pi -e` on the file, one expression per row:

| Old | New |
|---|---|
| `TutorialScript.Seed` | `Seq.seed` |
| `TutorialScript.QuietBefore` | `Seq.quietBefore` |
| `TutorialScript.Offer` | `Seq.FirstOfferIds()` |
| `TutorialScript.Reaction` | `Seq.Trial(0)` |
| `TutorialScript.Miss` | `Seq.Trial(1)` |
| `TutorialScript.Chain` | `Seq.Trial(2)` |
| `TutorialScript.Depth1` | `new[] { Seq.Trial(0), Seq.Trial(1), Seq.Trial(2) }` |
| `TutorialScript.Layout(` | `TutorialSequence.Layout(` |
| `TutorialScript.BrokenChain()` | `TutorialSequence.BrokenLayout(Seq.Trial(2))` |
| `TutorialScript.CatalystTrial(` | `Seq.CatalystTrial(` |
| `TutorialScript.Options(` | `Seq.Options(` |

Add `using Athanor.Game;`. `TutorialScript.Plan` and `TutorialScript.ShouldRun` stay until Step 4.

- [ ] **Step 3: `TutorialStepsBakeTests` reads the asset.** Its `[SetUp]` still opens Main through `MainSceneFixture` and binds the manager. Replace the step lookups:
- `tutorial.GetComponentsInChildren<TutorialStep>(true)` with `tutorial.Sequence.steps`;
- `step.Areas()` with `step.areas`;
- `step.name` with `step.id`.

Rename `EveryStepId_UsedByDrive_ExistsInTheScene` to `EveryStepId_UsedByTheScript_ExistsInTheSequence`, with the body of `TutorialSequenceTests.Sequence_EveryStepTheScriptNames_Exists` on `tutorial.Sequence`. `EveryStep_Rects_EqualTheBaseline` stays as it is: it compares `tutorial.Holes(step)` with `Baselines/tutorial_lit_baseline.json`, and that comparison is the guard for the area migration.

- [ ] **Step 4: Rename what is left of `TutorialScript`.** In `Scripts/Gameplay/Tutorial.cs`, delete everything in `TutorialScript` except `ShouldRun` and `Plan`, and rename the class to `TutorialGate`:

```csharp
    /// <summary>When the first run is the tutorial: only the TutorialDone flag decides, plus the skip question for an old save.</summary>
    public static class TutorialGate
    {
        /// <summary>The tutorial is offered until the save has finished (or skipped) it.</summary>
        public static bool ShouldRun(bool tutorialDone) => !tutorialDone;

        /// <summary>Done: None. Not done with an empty journal: Run. Not done with journal entries (a save from before the tutorial): AskSkip, asked once.</summary>
        public static TutorialPlan Plan(bool tutorialDone, int journalEntries)
        {
            if (!ShouldRun(tutorialDone)) return TutorialPlan.None;
            return journalEntries == 0 ? TutorialPlan.Run : TutorialPlan.AskSkip;
        }
    }
```

`TutorialPlan`, `TutorialLay` and `TutorialTrial` stay in the file. Then:

```bash
grep -rlZ "TutorialScript\.\(Plan\|ShouldRun\)" Athanor/Assets --include="*.cs" | xargs -0 perl -pi -e 's/TutorialScript\.(Plan|ShouldRun)/TutorialGate.$1/g'
grep -rn "TutorialScript" Athanor/Assets --include="*.cs"
```

Expected from the last grep: only `TutorialSequenceTests.cs` and `TutorialSequenceMigration.cs`. Their one lasting check, `Sequence_EveryStepTheScriptNames_Exists`, already lives on in `TutorialStepsBakeTests` (Step 3). Delete both files: the rest of them compared the asset against a class that is gone.

```bash
git rm Athanor/Assets/Tests/EditModeGame/TutorialSequenceTests.cs Athanor/Assets/Tests/EditModeGame/TutorialSequenceTests.cs.meta \
  Athanor/Assets/Editor/Migration/TutorialSequenceMigration.cs Athanor/Assets/Editor/Migration/TutorialSequenceMigration.cs.meta
```

- [ ] **Step 5: Run**

```bash
unity test Athanor --mode EditMode
unity test Athanor --mode PlayMode
```

Expected: 0 failed. `ScriptedTrials_Shown_Equal40_0_40_240` and `EveryStep_Rects_EqualTheBaseline` pass on the asset.

- [ ] **Step 6: Checkpoint (owner commits)**

```
test(tutorial): pin the tutorial through Tutorial.asset; TutorialScript becomes TutorialGate

Story: R-23
```

---

### Task 24: Remove the scene step objects and edit areas from the asset

**Files:**
- Create: `Editor/TutorialSequenceEditor.cs`
- Create (temporary): `Editor/Migration/TutorialSceneCleanup.cs`
- Delete: `Scripts/Game/Cutscene/TutorialStep.cs`, `TutorialArea.cs` (+ metas), `Editor/TutorialStepsMigration.cs`, `Editor/TutorialStepGizmo.cs` (+ metas), the 30 `Step …` objects in `Main.unity`

- [ ] **Step 1: Delete the step objects from the scene with a batch tool** `Editor/Migration/TutorialSceneCleanup.cs`

```csharp
using System;
using System.Linq;
using Athanor.Game;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Athanor.EditorTools
{
    /// <summary>One-shot: deletes the TutorialStep objects under Main.unity › Tutorial (the steps live in Tutorial.asset now).</summary>
    public static class TutorialSceneCleanup
    {
        public static void RunBatch()
        {
            try
            {
                var scene = EditorSceneManager.OpenScene("Assets/Scenes/Main.unity", OpenSceneMode.Single);
                var steps = scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<TutorialStep>(true)).Select(s => s.gameObject).ToList();
                foreach (var go in steps) UnityEngine.Object.DestroyImmediate(go);
                Debug.Log("[tutorial] removed " + steps.Count + " step objects");
                EditorSceneManager.MarkSceneDirty(scene);
                EditorSceneManager.SaveScene(scene);
            }
            catch (Exception e) { Debug.LogError(e); EditorApplication.Exit(1); }
        }
    }
}
```

Run: `unity run Athanor -- -executeMethod Athanor.EditorTools.TutorialSceneCleanup.RunBatch`
Expected: exit 0, and the log shows `removed 30 step objects`.

- [ ] **Step 2: Delete the old types and tools**

```bash
git rm Athanor/Assets/Scripts/Game/Cutscene/TutorialStep.cs Athanor/Assets/Scripts/Game/Cutscene/TutorialStep.cs.meta \
  Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs.meta \
  Athanor/Assets/Editor/TutorialStepsMigration.cs Athanor/Assets/Editor/TutorialStepsMigration.cs.meta \
  Athanor/Assets/Editor/TutorialStepGizmo.cs Athanor/Assets/Editor/TutorialStepGizmo.cs.meta \
  Athanor/Assets/Editor/Migration/TutorialSceneCleanup.cs Athanor/Assets/Editor/Migration/TutorialSceneCleanup.cs.meta
```

`TutorialAreaTests.Fixed_Area_ReadsItsTransform` and `Step_Lists_Its_Areas_In_Child_Order` test the deleted components, so delete those two tests. `Grow_EachMarginSide_AddsSeparately` and `ArtRect_OfWorldCentre_IsTheCentredArtRect` test `TutorialRect` and stay.

- [ ] **Step 3: Scene-view editing of a step's Fixed areas** `Editor/TutorialSequenceEditor.cs`

```csharp
using Athanor.Game;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Select the Tutorial object in Main.unity, pick a step in the Inspector, and its Fixed areas are drawn in the Scene view
/// (green: clicks pass, amber: highlight only) with a move handle each. Values are written to Tutorial.asset.
/// </summary>
[CustomEditor(typeof(TutorialManager))]
public sealed class TutorialSequenceEditor : Editor
{
    static readonly Color Pass = Color.green, Lit = new Color(1f, 0.7f, 0.2f);
    static int stepIndex;

    TutorialSequence Seq => (TutorialSequence)serializedObject.FindProperty("sequence").objectReferenceValue;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var seq = Seq;
        if (seq == null || seq.steps.Count == 0) return;
        EditorGUILayout.Space();
        var names = seq.steps.ConvertAll(s => s.id + "  (" + s.textKey + ")").ToArray();
        stepIndex = Mathf.Clamp(EditorGUILayout.Popup("Preview step", stepIndex, names), 0, names.Length - 1);
        if (GUILayout.Button("Select Tutorial.asset")) Selection.activeObject = seq;
        SceneView.RepaintAll();
    }

    /// <summary>Art pixels (top-left origin) to the world centre of the rect.</summary>
    static Vector3 Centre(Rect r) => new Vector3(r.x + r.width / 2f - Px.W / 2f, Px.H / 2f - (r.y + r.height / 2f), 0);

    void OnSceneGUI()
    {
        var seq = Seq;
        if (seq == null || stepIndex >= seq.steps.Count) return;
        var step = seq.steps[stepIndex];
        foreach (var a in step.areas)
        {
            if (a.kind != AreaKind.Fixed) continue;
            Handles.color = a.clicksPass ? Pass : Lit;
            var c = Centre(a.rect);
            Handles.DrawWireCube(c, new Vector3(a.rect.width, a.rect.height, 0));
            Handles.Label(c + new Vector3(-a.rect.width / 2f, a.rect.height / 2f, 0), step.id + " " + a.name);
            EditorGUI.BeginChangeCheck();
            var moved = Handles.PositionHandle(c, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(seq, "Move tutorial area");
                a.rect.x = Mathf.Round(moved.x + Px.W / 2f - a.rect.width / 2f);
                a.rect.y = Mathf.Round(Px.H / 2f - moved.y - a.rect.height / 2f);
                EditorUtility.SetDirty(seq);
            }
        }
    }
}
```

- [ ] **Step 4: Run every check**

```bash
unity test Athanor --mode EditMode
unity test Athanor --mode PlayMode
grep -rn "TutorialStep\b\|TutorialArea\b" Athanor/Assets --include="*.cs"
```

Expected: 0 failed, the tutorial trace and the lit baseline identical, and the grep shows no output (`TutorialStepDef` and `TutorialAreaDef` do not match the whole word).

- [ ] **Step 5: Update the decision.** In `docs/ATHANOR.md` §4, the 2026-11-20 tutorial line is now superseded: delete it, because the 2026-11-24 line from Task 0 covers it. In §2, under "Scene objects", the Tutorial object no longer has step children. Say so.

- [ ] **Step 6: Checkpoint (owner commits)**

```
refactor(tutorial)!: remove the scene step objects; edit areas on Tutorial.asset

Story: R-24
```

---

# Stage 6: Control in the Inspector

### Task 25: Inspectors for content and managers

**Files:**
- Create: `Athanor/Assets/Scripts/Gameplay/Content/ContentValidation.cs`
- Create: `Athanor/Assets/Editor/Content/ContentInspectors.cs`, `Athanor/Assets/Editor/ManagerInspectors.cs`
- Test: `Athanor/Assets/Tests/EditMode/ContentValidationTests.cs`

- [ ] **Step 1: Write the failing test** `Tests/EditMode/ContentValidationTests.cs`

```csharp
using Athanor.Gameplay;
using NUnit.Framework;

namespace Athanor.Tests
{
    public class ContentValidationTests
    {
        [Test]
        public void Check_TheGamesContent_IsValid()
        {
            // Arrange
            var db = ContentAssets.Database();
            // Act
            var problem = ContentValidation.Check(db);
            // Assert
            Assert.IsNull(problem, problem);
        }

        [Test]
        public void Check_NoRules_SaysSo()
        {
            // Arrange
            var db = UnityEngine.ScriptableObject.CreateInstance<ContentDatabase>();
            // Act
            var problem = ContentValidation.Check(db);
            // Assert
            StringAssert.Contains("rules", problem);
            UnityEngine.Object.DestroyImmediate(db);
        }
    }
}
```

- [ ] **Step 2: Run it and see it fail**

Run: `unity test Athanor --mode EditMode --filter ContentValidationTests`
Expected: compile error, `ContentValidation could not be found`.

- [ ] **Step 3: `Scripts/Gameplay/Content/ContentValidation.cs`**

```csharp
namespace Athanor.Gameplay
{
    /// <summary>Runs ContentDb's validation on a database and returns the first problem, or null when the content is valid.</summary>
    public static class ContentValidation
    {
        public static string Check(ContentDatabase db)
        {
            try { new ContentDb(db.ToContentData()); return null; }
            catch (System.Exception e) { return e.Message; }
        }
    }
}
```

- [ ] **Step 4: `Editor/Content/ContentInspectors.cs`**

```csharp
using Athanor.Gameplay;
using UnityEditor;
using UnityEngine;

/// <summary>ContentDatabase: counts, a Validate button, and a Write Snapshot button.</summary>
[CustomEditor(typeof(ContentDatabase))]
public sealed class ContentDatabaseEditor : Editor
{
    string result;

    public override void OnInspectorGUI()
    {
        var db = (ContentDatabase)target;
        EditorGUILayout.HelpBox(db.materials.Count + " materials, " + db.recipes.Count + " recipes, " + db.catalysts.Count + " catalysts, "
            + db.aspects.Count + " aspects, " + db.events.Count + " events, " + db.goals.Count + " goals. The list order is the order the rules see.", MessageType.None);
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Validate")) result = ContentValidation.Check(db) ?? "Valid.";
            if (GUILayout.Button("Write Snapshot")) { ContentSnapshot.Write(); result = "Snapshot written. Commit tools/content/snapshot.json with the assets."; }
        }
        if (result != null) EditorGUILayout.HelpBox(result, result == "Valid." || result.StartsWith("Snapshot") ? MessageType.Info : MessageType.Error);
        EditorGUILayout.Space();
        DrawDefaultInspector();
    }
}

/// <summary>Any content definition: a reminder that saves store its id, and a check of the whole database after an edit.</summary>
[CustomEditor(typeof(MaterialDefinition)), CanEditMultipleObjects]
public sealed class MaterialDefinitionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var m = (MaterialDefinition)target;
        if (m.name != m.id) EditorGUILayout.HelpBox("The asset name (" + m.name + ") and the id (" + m.id + ") differ; ContentAssetTests expects them equal.", MessageType.Warning);
        if (m.tier != 0 && (m.rarity != RarityId.Unset || m.locked)) EditorGUILayout.HelpBox("Only a starter (tier 0) has a rarity or a lock.", MessageType.Error);
        DrawDefaultInspector();
    }
}
```

`ContentSnapshot` is in `Athanor.EditorShared`, which `Assembly-CSharp-Editor` sees, since `autoReferenced` is true.

- [ ] **Step 5: `Editor/ManagerInspectors.cs`.** Readouts while playing, buttons always.

```csharp
using System.Linq;
using Athanor.Game;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RunManager))]
public sealed class RunManagerEditor : Editor
{
    public override bool RequiresConstantRepaint() => Application.isPlaying;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var m = (RunManager)target;
        if (!Application.isPlaying || m.Run == null) { EditorGUILayout.HelpBox("Play to see the run.", MessageType.None); return; }
        var r = m.Run;
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Seed", m.LastSeed.ToString());
        EditorGUILayout.LabelField("Phase", r.Phase.ToString());
        EditorGUILayout.LabelField("Depth", r.Depth.ToString());
        EditorGUILayout.LabelField("Potency", r.Potency + " / " + r.Target);
        EditorGUILayout.LabelField("Trials left", r.TrialsLeft.ToString());
        EditorGUILayout.LabelField("Redraws left", r.RedrawsLeft.ToString());
        EditorGUILayout.LabelField("Hand", string.Join(", ", r.Hand));
        EditorGUILayout.LabelField("Catalysts", string.Join(", ", r.Catalysts.Select(c => c.id)));
        if (GUILayout.Button("Use this seed for every run")) { Undo.RecordObject(m, "Seed override"); m.SeedOverride = m.LastSeed; }
    }
}

[CustomEditor(typeof(ProgressManager))]
public sealed class ProgressManagerEditor : Editor
{
    public override bool RequiresConstantRepaint() => Application.isPlaying;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var m = (ProgressManager)target;
        if (!Application.isPlaying || m.Profile == null) { EditorGUILayout.HelpBox("Play to see the journal and profile.", MessageType.None); return; }
        EditorGUILayout.LabelField("Sandbox", m.Sandbox.ToString());
        EditorGUILayout.LabelField("Journal entries", m.Journal.KnownPairs.Count.ToString());
        EditorGUILayout.LabelField("Goals done", m.Profile.Completed.Count.ToString());
        EditorGUILayout.LabelField("Runs started / won", m.Profile.RunsStarted + " / " + m.Profile.RunsWon);
    }
}
```

`SaveManager`'s context menu items (Task 16) give the erase buttons. Nothing more is needed there.

- [ ] **Step 6: Run**

Run: `unity test Athanor --mode EditMode --filter ContentValidationTests`
Expected: 2 passed. Then open the Editor once. Check that the ContentDatabase Inspector shows the counts and that Validate says `Valid.`, and that during Play the RunManager Inspector shows the run.

- [ ] **Step 7: Checkpoint (owner commits)**

```
feat(editor): content validation and live manager inspectors

Story: R-25
```

---

### Task 26: Tooltip sizes in the Inspector (the worked example for Task 27)

The pattern: a `const` used in a layout formula becomes a serialized struct on the view. It starts with today's values as a field initializer, gets saved into the prefab, and then loses the initializer. `LayoutPinTests` pin the saved values, and `GoldenShotTests` prove nothing moved. The struct is new and kept apart from the existing `Layout`, because C# 9 structs cannot have field initializers.

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Ui/Tooltip.cs:12`, `Athanor/Assets/Prefabs/Views/Bench.prefab` (via Reserialize)
- Test: `Athanor/Assets/Tests/EditModeGame/LayoutPinTests.cs`

- [ ] **Step 1: Write the failing pin** in `LayoutPinTests`

```csharp
        [Test]
        public void Tooltip_Size_HasTodaysNumbers()
        {
            // Arrange
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Views/Bench.prefab");
            var tip = prefab.GetComponentInChildren<Tooltip>(true);
            // Act
            var so = new SerializedObject(tip);
            // Assert
            AssertFloats(so, "size", new Dictionary<string, float> { { "width", 176 }, { "baseHeight", 58 }, { "tierRow", 12 } });
        }
```

Run: `unity test Athanor --mode EditMode --filter Tooltip_Size_HasTodaysNumbers`
Expected: FAIL, `size.width` (property not found).

- [ ] **Step 2: Add the struct with today's values as the initializer.** In `Tooltip.cs`, replace

```csharp
        const int W = 176, BaseH = 58, RowH = 12, Order = 400;
```

with

```csharp
        /// <summary>The panel's width, its height without the tier row, and the tier row's height (px), stored in the Bench prefab.</summary>
        [Serializable]
        public struct Size { public int width, baseHeight, tierRow; }
        [SerializeField] Size size = new Size { width = 176, baseHeight = 58, tierRow = 12 };
```

Then replace every use of the old constants: `perl -pi -e 's/\bBaseH\b/size.baseHeight/g; s/\bRowH\b/size.tierRow/g; s/(?<![\w.])W\b/size.width/g' Athanor/Assets/Scripts/Game/Ui/Tooltip.cs`. Fix `int h = BaseH;` by hand: field initializers cannot read other fields, so it becomes `int h;` and `Bind()` sets `h = size.baseHeight;`. `Order` was unused (`grep -n "Order" Tooltip.cs` shows only the declaration), so it goes.

Run: `grep -n "\bW\b\|BaseH\|RowH" Athanor/Assets/Scripts/Game/Ui/Tooltip.cs`
Expected: no output.

- [ ] **Step 3: Save the values into the prefab**

Run: `unity run Athanor -- -executeMethod Reserialize.All`
Expected: exit 0. `git diff Athanor/Assets/Prefabs/Views/Bench.prefab` shows `size: {width: 176, baseHeight: 58, tierRow: 12}` (or the YAML block form) added to the Tooltip component.

- [ ] **Step 4: Remove the initializer** (no code defaults): `[SerializeField] Size size;`

- [ ] **Step 5: Run the pin and the golden shots**

Run: `unity test Athanor --mode EditMode --filter "LayoutPinTests|GoldenShotTests"`
Expected: all pass, 0 % difference.

- [ ] **Step 6: Checkpoint (owner commits)**

```
refactor(ui): Tooltip panel sizes are Inspector fields of the Bench prefab

Story: R-26
```

---

### Task 27: Page grids of the other views in the Inspector

The same six steps as Task 26, one view at a time, with a checkpoint after each. The constants come from the planning grep. **Sort orders (`O`) and font sizes stay in code** (ATHANOR.md §7 item 11(e): they are relative layering inside many formulas). **Data shapes stay `const`** (`Cols`, `Rows`, `PerPage`, `Slots`, `LineCount`, `HintCount`, `PipCount`).

| View (prefab) | Constants → new struct and field | Initializer |
|---|---|---|
| `GoalsView` (`Views/Goals.prefab`) | `X, Y, LeftX, RightX, Top, ColW, RowH, HeadH, Pad, BarW` (int), `DotSize` (float) → `struct Grid { public int x, y, leftX, rightX, top, colW, rowH, headH, pad, barW; public float dotSize; }`, field `grid`; `BarLeft` becomes `int BarLeft => grid.colW - 2 * grid.pad - grid.barW;` | `new Grid { x = 20, y = 15, leftX = 14, rightX = 304, top = 30, colW = 282, rowH = 20, headH = 14, pad = 10, barW = 92, dotSize = 4 }` |
| `JournalView` (`Views/Journal.prefab`) | `X, Y, Cell, Well, GridX, GridY, PageX, PageY, PageW, WrapAt` → `struct Grid { public int x, y, cell, well, gridX, gridY, pageX, pageY, pageW, wrapAt; }`, field `grid` | `new Grid { x = 20, y = 15, cell = 27, well = 25, gridX = 33, gridY = 60, pageX = 324, pageY = 30, pageW = 260, wrapAt = 38 }` (copy the values of line 15-17 of `JournalView.cs` at the time; read the rest of the `const` list there first) |
| `CatalystChoiceView` (`Views/CatalystChoice.prefab`) | `CardW, CardH, Gap, Top` → `struct Cards { public int width, height, gap, top; }`, field `cards` | `new Cards { width = 150, height = 180, gap = 18, top = 112 }` |
| `TitleView` settings panel (`Views/Title.prefab`) | `PX, PY, CellX, CellW, CellH` → `struct Panel { public int x, y, cellX, cellW, cellH; }`, field `panel`; `CellMid` becomes `float CellMid => panel.cellX + panel.cellW / 2f;` (`P = O + 20` is a sort order and stays) | `new Panel { x = 160, y = 75, cellX = 300, cellW = 150, cellH = 26 }` |
| `RunOverView` (`Views/RunOver.prefab`) | `Fits` (letters of goal names that fit on the line) → `[SerializeField, Tooltip("Letters of goal names that fit on one line before VIEW GOALS.")] int goalLineFits = 25;` | `25` |

For each row:

- [ ] **Step A: Pin test.** Add `<View>_<Field>_HasTodaysNumbers` to `LayoutPinTests`, loading the prefab in the table and calling `AssertFloats(so, "<field>", …)` with the initializer values. Run it: FAIL (property not found).
- [ ] **Step B: Code.** Add the struct and the initialized field. Replace each constant with `<field>.<name>`, using `perl -pi -e 's/(?<![\w.])Name\b/field.name/g'`, one constant at a time, then fix by hand anything inside another `const` initializer. Grep that the old names are gone.
- [ ] **Step C:** `unity run Athanor -- -executeMethod Reserialize.All`. The prefab diff shows the new block.
- [ ] **Step D:** Remove the initializer.
- [ ] **Step E:** `unity test Athanor --mode EditMode --filter "LayoutPinTests|GoldenShotTests"`. All pass, 0 %.
- [ ] **Step F: Checkpoint (owner commits)** `refactor(ui): <View> page grid is an Inspector field of its prefab` with `Story: R-27`.

After the last row, update ATHANOR.md §7 item 11(e): remove the views moved here from the "stay in code" list.

---

# Stage 7: Documents and the final gate

### Task 28: Documents, agents, vault

**Files:**
- Modify: `CLAUDE.md`, `docs/ATHANOR.md` §2, §3, §5, §6, `vault/Design/Architecture.md` (hand-written), `.claude/agents/rules-engineer.md`, `.claude/agents/unity-gameplay-engineer.md`, `.claude/agents/content-designer.md`, `.claude/agents/qa-engineer.md`, `README.md` (build/test section)
- Delete: `Athanor/Execution_Plan.md` (its tasks are all closed and it names a Linux path from another machine), after asking the owner

- [ ] **Step 1: `CLAUDE.md`**
  - Technology Stack: `C# — rules in Athanor.Gameplay (plain C# models), managers and screens in Athanor.Game`. Tests: `Unity Test Framework — unity test Athanor --mode EditMode and --mode PlayMode`.
  - Project layout: replace `Resources/` with `content.json, …` → `Assets/Content` (content assets), `Resources/lang/*.json`. Replace `tools/CoreTests` and `CoreReports` with `Editor/Balance` (`Athanor > Balance`).
  - Hard rules: replace "No game rules in the Game layer; no `UnityEngine` in Core" with "No game rules in views or managers: rules live in the `Athanor.Gameplay` models. New rule → test first." Replace the content rule with "Content changes require `unity test` plus the balance reports (`BalanceReports.RunBatch`) and a fresh `tools/content/snapshot.json`; quote the numbers." Replace "Run `tools/ci/run.sh all`" with "Run `tools/ci/run.sh all` and `tools/ci/run.sh unity`".
- [ ] **Step 2: `docs/ATHANOR.md`**
  - §2: replace the "Core / Game split" paragraph with the managers (one line each, as in ADR-0001) and the plain-model rule. Replace the "Where a number goes" table row `content.json rules` with `Assets/Content/GameRules.asset`. Under "Main.unity and prefabs", add the `Managers` object.
  - §3: retitle "Content assets". Replace the `content.json` table intro with the asset folders. Keep the field table (the shape is the same, see `ContentData.cs`). Add "edit in the Inspector, then `Athanor > Content > Write Snapshot`".
  - §5: drop UL008/CORE-PURITY rows. Replace the `dotnet test` row with `unity-tests (game-ci, EditMode + PlayMode, MIN_TESTS 600)`. Add a row for `ContentSnapshotTests` and one for the PlayMode traces. Fix the lint list to what `unity_lint.py` really has (fact 6).
  - §6: replace the Core tests row with EditMode and PlayMode rows, and the reports with the batch command. Update the figures after Task 29.
- [ ] **Step 3: Agents.** `rules-engineer.md`: its lane is now `Scripts/Gameplay` (and its tests run in Unity). `unity-gameplay-engineer.md`: add `Scripts/Game/Managers` and `Scripts/Game/Tutorial`. `content-designer.md`: done in Task 13; check it. `qa-engineer.md`: replace `dotnet test` and `tools/CoreReports` with the Unity commands.
- [ ] **Step 4: Vault**

Run: `python3 tools/vault/build_vault.py && git status --short vault`
Expected: the generated notes change to the new types. Stage them. Then fix any broken `[[link]]` in hand-written notes that named `GameFlow`, `TutorialController`, `TutorialScript`, `ContentData` from JSON, or `Core`: run `grep -rln "GameFlow\|TutorialController\|TutorialScript\|content.json" vault --include="*.md" | xargs grep -L "generated: true"`.

- [ ] **Step 5: Ask the owner before deleting `Athanor/Execution_Plan.md`.**

- [ ] **Step 6: Checkpoint (owner commits)**

```
docs: describe the managers, content assets and Unity-only tests

Story: R-28
```

---

### Task 29: Final gate

- [ ] **Step 1: Every automated check**

```bash
tools/ci/run.sh all
tools/ci/run.sh unity
for r in balance-matrix events roles newcomer balance trace; do
  unity run Athanor -- -executeMethod BalanceReports.RunBatch -report "$r" -out "$PWD/Temp/reports/$r.txt"
  diff -q "Temp/reports/$r.txt" "production/qa/evidence/refactor/baseline/$r.txt" || echo "DIFF $r"
done
python3 tools/vault/build_vault.py && git status --short vault
```

Expected: exit 0 everywhere, 0 failed, no `DIFF`, vault clean.

- [ ] **Step 2: Web builds and checks** (ATHANOR.md §6)

```bash
unity build Athanor --target WebGL --execute-method WebBuild.Build --allow-dirty-build
find Athanor/Build/web/Build Athanor/Build/web/index.html -type f -exec stat -f%z {} + | awk '{s+=$1} END {print s}'
unity build Athanor --target WebGL --execute-method WebBuild.BuildDev --allow-dirty-build
python3 -m http.server 8080 -d Athanor/Build/web-dev &
node tools/web/webtest.mjs http://localhost:8080/ production/qa/evidence/refactor/web
node tools/web/autoplay.mjs http://127.0.0.1:8080/ production/qa/evidence/refactor/autoplay 120 3
kill %1
```

Expected: both builds exit 0. The byte count is at most **11,824,672**, against **10,919,503 B** at the end of Phase 7. Quote the new number; the managers and assets should move it by a few KB, and `Sim` is Editor-only. `webtest` and `autoplay` exit 0 with 0 console errors.

- [ ] **Step 3: Owner checks in the Editor.** Play / Stop / Play (Task 19 Step 3). Play the tutorial by hand once, to see that the lines, holes and arrow look as before. Change one number in `GameRules.asset` (`trials` 4 → 5), press Play, and see 5 trials on the HUD. Then undo it.

- [ ] **Step 4: Record the figures** in ATHANOR.md §6: test counts (EditMode, PlayMode), build size, and the web checks, with the evidence folder `production/qa/evidence/refactor/`.

- [ ] **Step 5: Checkpoint (owner commits)**

```
chore(release): refactor gate passed — reports byte-identical, traces identical, size <budget>

Story: R-29
```

---

## Self-review against the request

| Request | Covered by |
|---|---|
| Remove Core C# → Manager singletons | Tasks 5 (assembly), 8 (dotnet gone), 14 to 19 (`Singleton<T>`, `GameManager`, Content/Save/Localization/Progress/Run managers), 22 (`TutorialManager`) |
| `content.json` → ScriptableObject classes and assets | Tasks 9 to 13 (enums, definitions, import with proof, switch, delete + snapshot) |
| Per-object data → Inspector `SerializeField` | Tasks 10 (Tooltips/Range/Min on every definition), 16 to 18 (manager fields: seed override, language files, database), 21 to 24 (tutorial steps, areas, trials, script in the Inspector + Scene handles), 26 and 27 (view grids), 25 (validation and live readouts) |
| Compare gameplay and fit PlayMode | Tasks 2 and 3 (seeded autoplay + tutorial traces recorded before any change), checked at the end of every later task; 14 and 19 (domain-reload-off safety, a second load, clean boot); 29 (web checks) |
| Rebuild the tutorial | Tasks 20 to 24 |
| Better control and customization | Task 25 (inspectors), Task 7 (balance menu), `SaveManager` context menu (Task 16), `RunManager` seed (Task 18) |

Known approximations, stated so they are not mistaken for done:
- Task 27's `JournalView` row asks the implementer to read the rest of the `const` list (lines 15-17) before writing the struct. The planning grep showed those lines, but the full set of uses was not traced.
- The `unity test` filter flag name is assumed to be `--filter` and is checked in Task 2 Step 1.
- `unity_lint.py` lacks UL009–UL014 at HEAD (fact 6). Restoring them is out of scope here. If the owner wants them back, that is its own task.
