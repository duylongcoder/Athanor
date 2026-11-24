# Core Architecture Refactor: Manager Classes + ScriptableObject Data + SerializedFields

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Refactor Core to use Manager classes with Singleton pattern, migrate Content.json to ScriptableObject-based .asset files, and move all configurable numbers to SerializedField in the Unity layer.

**Architecture:** **MAJOR CHANGE** — This plan proposes breaking the current Core/Game separation. Current architecture (ATHANOR.md §2): Core = pure C#, no UnityEngine, deterministic rules; Game = Unity integration. This plan moves Core to Unity-dependent Manager classes. Requires ADR and full test pipeline migration.

**Tech Stack:** Unity 6000.6.7f1, C#, NUnit, ScriptableObject, Addressables (optional)

---

## ⚠️ CRITICAL ARCHITECTURE CONFLICTS

### Current Decisions in Force (ATHANOR.md §4)
| Decision | Date | Impact on This Plan |
|----------|------|---------------------|
| **CORE-PURITY** | 2026-11-18 | Core MUST have NO `UnityEngine` references (UL008 error). Manager/Singleton = Unity-dependent. |
| **Content.json dual-use** | 2026-11-19 | `content.json` read by `JsonUtility` (Unity) AND `System.Text.Json` (dotnet CoreTests). ScriptableObject breaks dotnet tests. |
| **Rules in content.json** | 2026-11-19 | Rule numbers (hand size, trials, potency table) live in `content.json` `rules` block, read as `ContentDb.Rules`. |
| **Numbers placement** | 2026-11-19 | Rule tuning → `content.json`; Board shape → Core `const`; View layout → prefab/ScriptableObject. |
| **No DI framework** | 2026-11-20 | `Bind`/`FxRefs` pass dependencies; no Manager/Singleton pattern in Core. |

### Breaking Changes Required
1. **CoreTests (dotnet)** → Must become Unity EditMode tests only (lose fast CI, deterministic runs)
2. **Content.json** → Replace with ScriptableObject assets (lose JSON portability, diffability, mergeability)
3. **Core static analysis** → UL008, UL011, UL013, UL014 no longer apply to former Core code
4. **CI pipeline** → `dotnet test tools/CoreTests` removed; only `unity test` remains

---

## ADR Required Before Implementation

**Task 0: Architecture Decision Record**

- [ ] **Step 1: Create ADR using `/architecture-decision`**
  - Title: "Migrate Core to Unity Manager Classes and ScriptableObject Data"
  - Context: Current Core/Game split, CORE-PURITY, dual JSON pipeline
  - Decision: Replace Core with Manager Singletons; replace content.json with ScriptableObject assets
  - Consequences: Loss of dotnet test pipeline, Unity-only tests, larger WebGL build, no pure-C# rule verification
  - Dependencies: UL008/UL011/UL013/UL014 lint rules retired; `tools/CoreTests` deleted; `project.yaml` test command changed

- [ ] **Step 2: Get owner approval on ADR**
- [ ] **Step 3: Update `docs/ATHANOR.md` §4 with new decisions**

---

## Phase 1: Core → Manager Migration (Breaking)

### Task 1: Create Manager Base Classes

**Files:**
- Create: `Athanor/Assets/Scripts/Core/Managers/ManagerBase.cs`
- Create: `Athanor/Assets/Scripts/Core/Managers/IGameManager.cs`
- Modify: `Athanor/Assets/Scripts/Core/ContentDb.cs` → Manager

```csharp
// ManagerBase.cs
using UnityEngine;

namespace Athanor.Core.Managers
{
    public abstract class ManagerBase<T> : MonoBehaviour where T : ManagerBase<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                    if (_instance == null)
                    {
                        var go = new GameObject(typeof(T).Name);
                        _instance = go.AddComponent<T>();
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            OnInit();
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }

        protected abstract void OnInit();
    }

    public interface IGameManager
    {
        void Initialize();
        void Shutdown();
    }
}
```

- [ ] **Step 1: Write failing test for Manager singleton**
- [ ] **Step 2: Implement ManagerBase**
- [ ] **Step 3: Run EditMode test** → PASS
- [ ] **Step 4: Commit**

---

### Task 2: Migrate ContentDb → ContentManager (ScriptableObject)

**Files:**
- Create: `Athanor/Assets/Scripts/Core/Managers/ContentManager.cs`
- Create: `Athanor/Assets/Resources/ContentData.asset` (ScriptableObject)
- Create: `Athanor/Assets/Scripts/Data/ContentDataSO.cs` (ScriptableObject definition)
- Delete: `Athanor/Assets/Scripts/Core/ContentDb.cs`, `ContentData.cs`
- Modify: `Athanor/Assets/Tests/EditMode/ContentTests.cs` → Unity test

```csharp
// ContentDataSO.cs
using UnityEngine;

namespace Athanor.Data
{
    [CreateAssetMenu(fileName = "ContentData", menuName = "Athanor/Content Data")]
    public class ContentDataSO : ScriptableObject
    {
        public MaterialData[] materials;
        public RecipeData[] recipes;
        public CatalystData[] catalysts;
        public AspectData[] aspects;
        public EventData[] events;
        public GoalData[] goals;
        public RulesData rules;
        public int[] depthTargets;
        public string[] depthNames;
        public string ultimate;
        public int noEventWeight;
    }

    [System.Serializable] public class MaterialData { public string id, name, kind; public int tier, reactivity; public bool glow, locked; public string rarity; }
    [System.Serializable] public class RecipeData { public string[] items; public int minReactivity; public string result; }
    [System.Serializable] public class CatalystData { public string id, name, text, effect, kind; public int amount; public string rarity; public bool locked; }
    [System.Serializable] public class AspectData { public string id, name, text, effect, scope, group; public int amount; public bool tradeOff; }
    [System.Serializable] public class EventData { public string id, name, text, type; public int minDepth, maxDepth, weight; public EventOption[] options; }
    [System.Serializable] public class EventOption { public string label; public string[] requires; public CostData[] costs; public OutcomeData[] outcomes; }
    [System.Serializable] public class CostData { public string kind; public int amount; }
    [System.Serializable] public class OutcomeData { public int weight; public string text; public GainData[] gain; }
    [System.Serializable] public class GainData { public string kind; public int amount; }
    [System.Serializable] public class GoalData { public string id, text, stat; public int target; public string rewardKind, rewardRarity; }
    [System.Serializable] public class RulesData { public int handMin, handBase, trials, trialsFinalDepth, redraws, starterCopies, offerSize, rackSize, pouchCards, firstReactionMult, maxRi; public int[] aspectDepths; public int[] potencyByTier; }
}
```

```csharp
// ContentManager.cs
using UnityEngine;
using Athanor.Core.Managers;

namespace Athanor.Core.Managers
{
    public class ContentManager : ManagerBase<ContentManager>, IGameManager
    {
        [SerializeField] private ContentDataSO _contentData;
        public ContentDataSO Data => _contentData;

        protected override void OnInit()
        {
            if (_contentData == null)
                _contentData = Resources.Load<ContentDataSO>("ContentData");
        }

        public void Initialize() { /* already done in Awake */ }
        public void Shutdown() { }
    }
}
```

- [ ] **Step 1: Create ContentDataSO ScriptableObject**
- [ ] **Step 2: Migrate content.json → ContentData.asset** (Editor script)
- [ ] **Step 3: Create ContentManager**
- [ ] **Step 4: Update all Core references from `ContentDb` → `ContentManager.Instance.Data`**
- [ ] **Step 5: Run tests** → PASS
- [ ] **Step 6: Commit**

---

### Task 3: Migrate RunState → RunManager

**Files:**
- Create: `Athanor/Assets/Scripts/Core/Managers/RunManager.cs`
- Modify: `Athanor/Assets/Scripts/Core/RunState.cs` → instance methods on RunManager
- Modify: `Athanor/Assets/Scripts/Core/RunStart.cs` → merge into RunManager
- Modify: All Core files using `RunState` static methods

```csharp
// RunManager.cs
using UnityEngine;
using System.Collections.Generic;
using Athanor.Core.Managers;

namespace Athanor.Core.Managers
{
    public class RunManager : ManagerBase<RunManager>, IGameManager
    {
        [SerializeField] private RunConfig _config;
        public RunState State { get; private set; }

        protected override void OnInit()
        {
            State = new RunState(_config);
        }

        public void NewRun(RunOptions options) => State = RunState.NewRun(options, ContentManager.Instance.Data);
        public void Initialize() { }
        public void Shutdown() { State = null; }
    }

    [System.Serializable]
    public class RunConfig
    {
        public int baseHand = 5;
        public int baseTrials = 4;
        public int finalTrials = 6;
        public int baseRedraws = 2;
        public int starterCopies = 2;
        public int rackSize = 12;
        public int pouchCards = 1;
        public int firstReactionMult = 1;
        public int maxRi = 10;
    }
}
```

- [ ] **Step 1: Convert RunState static methods → instance**
- [ ] **Step 2: Create RunManager with RunConfig SerializedField**
- [ ] **Step 3: Update all callers (BenchRules, Ignite, Chain, Journal, Satchel, Profile, Tutorial, AutoPlayer)**
- [ ] **Step 4: Run tests** → PASS
- [ ] **Step 5: Commit**

---

### Task 4: Migrate Remaining Core Classes to Managers

| Core Class | Manager | Key Changes |
|------------|---------|-------------|
| `BenchRules` | `BenchManager` | Instance methods, uses RunManager.State.Bench |
| `Ignite` | `IgniteManager` | Instance, uses ContentManager, RunManager |
| `Chain` | `ChainManager` | Instance |
| `Journal` | `JournalManager` | Instance, persists via SaveManager |
| `Profile` | `ProfileManager` | Instance, persists via SaveManager |
| `Satchel` | `SatchelManager` | Instance |
| `ScoreParts` | `ScoreManager` | Instance |
| `ScreenFlow` | `ScreenFlowManager` | Instance (already partially done in Phase 7) |
| `Settings` | `SettingsManager` | Instance, persists via SaveManager |
| `Lang` | `LocalizationManager` | Instance, uses Resources.Load for lang assets |
| `Tutorial` | `TutorialManager` | Instance |
| `AutoPlayer` | `AutoPlayerManager` | Instance |

- [ ] **Step 1: For each class, create Manager wrapper**
- [ ] **Step 2: Convert static → instance, add SerializedField config**
- [ ] **Step 3: Update all cross-references**
- [ ] **Step 4: Run tests per manager**
- [ ] **Step 5: Commit per manager**

---

### Task 5: Delete tools/CoreTests and Dotnet Pipeline

**Files:**
- Delete: `tools/CoreTests/` (entire folder)
- Modify: `project.yaml` → `commands.test: "unity test Athanor --mode EditMode"`
- Modify: `tools/ci/run.sh` → remove `core` job
- Modify: `.github/workflows/` → remove dotnet steps

- [ ] **Step 1: Verify all Core tests ported to Unity EditMode**
- [ ] **Step 2: Delete tools/CoreTests**
- [ ] **Step 3: Update CI config**
- [ ] **Step 4: Run `tools/ci/run.sh all`** → GREEN
- [ ] **Step 5: Commit**

---

## Phase 2: Game Layer ScriptableObject Data

### Task 6: Create Game Config ScriptableObjects

**Files:**
- Create: `Athanor/Assets/Scripts/Data/GameConfigSO.cs` (already exists per depth plan)
- Create: `Athanor/Assets/Scripts/Data/BenchConfigSO.cs`
- Create: `Athanor/Assets/Scripts/Data/IgniteConfigSO.cs`
- Create: `Athanor/Assets/Scripts/Data/UIConfigSO.cs`
- Create: `Athanor/Assets/Data/*.asset` files

```csharp
// BenchConfigSO.cs
using UnityEngine;

namespace Athanor.Data
{
    [CreateAssetMenu(fileName = "BenchConfig", menuName = "Athanor/Bench Config")]
    public class BenchConfigSO : ScriptableObject
    {
        [Header("Grid")]
        public int size = 3;
        public int tileCount => size * size;
        public int centre => tileCount / 2;
        
        [Header("Layout (art pixels)")]
        public Vector2 origin;
        public float tilePitch;
        public Vector2 handOrigin;
        public float handPitch;
        
        [Header("Rules")]
        public int minItems = 2;
        public int maxItems = 5;
        public int maxCopies = 2;
    }
}
```

- [ ] **Step 1: Create each ScriptableObject with SerializedFields for all configurable numbers**
- [ ] **Step 2: Create .asset files in Assets/Data/**
- [ ] **Step 3: Reference from GameFlow.Bind()**
- [ ] **Step 4: Run tests** → PASS
- [ ] **Step 5: Commit**

---

### Task 7: Move All Hardcoded Numbers to SerializedField

**Files:** All `Athanor/Assets/Scripts/Game/**/*.cs`

**Numbers to serialize (from ATHANOR.md §7.11(e) — these INTENTIONALLY stay in code, DO NOT MOVE):**
- `Flame` flicker and `Flipbook` 1/30 s
- Font sizes and sort orders
- `Dev` hook strings
- `Tooltip` `W`/`BaseH`/`RowH` (layout used in formulas)
- `CX = 320` / `O = 300` in `ReactionCutscene` and `SimpleCutscene`
- Sizes in `WindowView` and `RunOverView` (`Fits = 25`)
- Float-text fade split in `BenchView.FloatText` (0.7 / 0.3)
- Page-grid constants in `GoalsView`, `JournalView`, `TitleView`, `CatalystChoiceView`

**Numbers to MOVE to SerializedField / ScriptableObject:**
```bash
# Find all hardcoded numbers in Game layer (excluding §7.11(e))
grep -rn "Px.At\|Px.Mid\|new Rect\|new Vector[23]\|new Color\|[0-9]\{2,\}" Athanor/Assets/Scripts/Game/ \
  | grep -v "0f\|1f\|0\|1\|// §7.11"
```

- [ ] **Step 1: Audit each Game file for hardcoded numbers**
- [ ] **Step 2: For each number not in §7.11(e):**
  - Add `[SerializeField]` field to owning class
  - Replace hardcoded value with field reference
  - Set value in prefab/ScriptableObject
- [ ] **Step 3: Add LayoutPinTests cases for each serialized field**
- [ ] **Step 4: Run EditMode tests** → PASS
- [ ] **Step 5: Commit**

---

## Phase 3: Test Migration & CI

### Task 8: Port CoreTests to Unity EditMode

**Files:**
- Create: `Athanor/Assets/Tests/EditModeGame/CoreRulesTests.cs` (from tools/CoreTests)
- Modify: All test files to use Manager.Instance instead of static calls
- Delete: `tools/CoreTests/`

- [ ] **Step 1: Copy each Core test to EditModeGame**
- [ ] **Step 2: Update to use Manager singletons**
- [ ] **Step 3: Add GameTestSetup for Manager initialization**
- [ ] **Step 4: Run `unity test Athanor --mode EditMode`** → ALL PASS
- [ ] **Step 5: Commit**

---

### Task 9: Update Lint Rules

**Files:**
- Modify: `tools/lint/unity_lint.py` → remove UL008, UL011, UL013, UL014 for former Core paths
- Modify: `tools/lint/lint.config.json` → update `core_purity_paths`, `static_allowlist`
- Modify: `tools/ci/check_repo.py` → remove CORE-PURITY check

- [ ] **Step 1: Update lint config**
- [ ] **Step 2: Run `python3 tools/lint/unity_lint.py Athanor/Assets/Scripts/`** → 0 errors
- [ ] **Step 3: Commit**

---

### Task 10: Full CI Gate & Validation

- [ ] **Step 1: Run `tools/ci/run.sh all`** → GREEN
- [ ] **Step 2: Run release build** → size ≤ 11,824,672 B
- [ ] **Step 3: Run web tests** → 0 console errors
- [ ] **Step 4: Run balance reports** (now via Unity batch mode)
- [ ] **Step 5: Regenerate vault** `python3 tools/vault/build_vault.py`
- [ ] **Step 6: Commit final state**

---

## Self-Review Checklist

- [ ] **ADR written and approved** — Required before any code change
- [ ] **Spec coverage:** All three requirements addressed (Manager/Singleton, ScriptableObject data, SerializedField)
- [ ] **Placeholder scan:** No TBD, every step has code
- [ ] **Type consistency:** Manager interfaces match across tasks
- [ ] **Contract coverage:** ADR covers CORE-PURITY, dotnet pipeline, JSON dual-use, lint rules
- [ ] **Breaking changes documented:** tools/CoreTests deleted, dotnet test removed, UL008/UL011/UL013/UL014 retired
- [ ] **Migration path:** Content.json → ContentData.asset via Editor script (not manual)
- [ ] **Test evidence:** Every Manager has EditMode tests

---

## Execution Options

**Plan saved to:** `docs/superpowers/plans/2026-11-24-architecture-refactor-core-manager.md`

**Two execution options:**

1. **Subagent-Driven (recommended)** — Fresh subagent per task, review between tasks
   - **REQUIRED SUB-SKILL:** `superpowers:subagent-driven-development`

2. **Inline Execution** — Batch execution with checkpoints
   - **REQUIRED SUB-SKILL:** `superpowers:executing-plans`

**⚠️ Prerequisite:** Owner must approve ADR before Task 1 begins. This is a **one-way architectural change** — reverting requires git history.

**Which approach?**