# Core C# Refactoring Priority Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Complete the remaining Core C# refactoring tasks from Phase 6 and Phase 7, prioritize tasks that establish clean architecture boundaries (Core purity, Unity-free rules, proper test coverage), and address test failures blocking CI.

**Architecture:** Core layer (`Athanor/Assets/Scripts/Core/`) stays pure C# with no `UnityEngine` dependencies. Game layer (`Athanor/Assets/Scripts/Game/`) handles Unity integration. Tests split: `EditMode/` for Core rules (dotnet test), `EditModeGame/` for Unity editor tests.

**Tech Stack:** Unity 6000.6.7f1, C#, NUnit, `dotnet test tools/CoreTests`, `unity test Athanor --mode EditMode`

---

## Current State Summary

| Layer | Files | Lines | Status |
|-------|-------|-------|--------|
| **Core** | 17 | 3,148 | Mostly complete; some UL011 statics remain |
| **Game** | 60+ | 8,093 | Partially refactored; some numbers still in code |
| **Editor** | 14 | ~2,000 | Art pipeline tools; one new candle fix script |
| **Core Tests (EditMode)** | 16 | ~2,000 | 367 passing |
| **Game Tests (EditModeGame)** | 32 | ~4,000 | 626/658 passing (32 failing) |

### Key Open Issues from ATHANOR.md §7

1. **32 EditMode test failures** — GoldenShotTests (no baselines), DepthLayerTests (path fix needed), candle import
2. **Phase 6 Task 5 paused** — RunOverView bake incomplete (uncommitted edits in ViewBaker.cs, GameFlow.cs, RunOverView.cs, PrefabBakeTests.cs)
3. **Phase 6 Tasks 5-25** — Full refactor plan exists but most tasks unstarted
4. **Core mutable statics** — 8 allowed but need reset coverage
5. **Presentation numbers in code** — §7.11(e) lists numbers that intentionally stay

---

## Plan: Phase 6 Completion + Phase 7 Cleanup (Prioritized)

### Priority 1: Fix CI-Blocking Test Failures (Immediate)

#### Task 1: Fix GoldenShotTests — Generate Baseline Images

**Files:**
- Modify: `Athanor/Assets/Editor/Shared/GoldenShots.cs`
- Create: `Athanor/Assets/Tests/EditModeGame/Baselines/` (32 PNGs)
- Test: `Athanor/Assets/Tests/EditModeGame/GoldenShotTests.cs`

- [ ] **Step 1: Add namespace to GoldenShots.cs for batch method discovery**

```csharp
// Add at top of GoldenShots.cs
namespace Athanor.Editor.Shared
{
    // ... existing class
}
```

- [ ] **Step 2: Run golden shot generation in batch mode**

```bash
unity batch Athanor --executeMethod Athanor.Editor.Shared.GoldenShots.WriteAll --log-file /tmp/golden.log -- -quit
```

Expected: Exit 0, 32 PNGs written to `Assets/Tests/EditModeGame/Baselines/`

- [ ] **Step 3: Verify baselines exist and have content**

```bash
ls -la Athanor/Assets/Tests/EditModeGame/Baselines/
# Should show 16 EN + 16 VI PNGs
```

- [ ] **Step 4: Run GoldenShotTests to confirm PASS**

Run: `unity test Athanor --mode EditMode --filter GoldenShotTests`
Expected: 16/16 PASS (0% diff)

- [ ] **Step 5: Commit**

```bash
git add Athanor/Assets/Editor/Shared/GoldenShots.cs Athanor/Assets/Tests/EditModeGame/Baselines/
git commit -m "test(golden): generate baseline images for all views (EN/VI)"
```

---

#### Task 2: Fix DepthLayerTests — Correct Art Path

**Files:**
- Modify: `Athanor/Assets/Tests/EditModeGame/DepthLayerTests.cs` (line with doubled path)

- [ ] **Step 1: Locate and fix the path**

Current: `Assets/Art/Art/Depth` → Correct: `Assets/Art/Depth`

```csharp
// In DepthLayerTests.cs, find the hardcoded path and fix
const string DepthArtPath = "Assets/Art/Depth";  // was "Assets/Art/Art/Depth"
```

- [ ] **Step 2: Run DepthLayerTests**

Run: `unity test Athanor --mode EditMode --filter DepthLayerTests`
Expected: PASS (or only candle_far.png filter mode failure remaining)

- [ ] **Step 3: Commit**

```bash
git add Athanor/Assets/Tests/EditModeGame/DepthLayerTests.cs
git commit -m "test(depth): fix doubled art folder path in DepthLayerTests"
```

---

#### Task 3: Fix candle_far.png Import Settings

**Files:**
- Modify: `Athanor/Assets/Editor/FixCandleImport.cs` (ensure it works)
- Meta: `Athanor/Assets/Art/Depth/candle_far.png.meta`

- [ ] **Step 1: Verify FixCandleImport.cs compiles and runs**

```bash
unity batch Athanor --executeMethod Athanor.Editor.FixCandleImport.Fix --log-file /tmp/candle.log -- -quit
```

Expected: Exit 0, meta file updated to Bilinear/Clamp

- [ ] **Step 2: Verify meta file changed**

```bash
grep -A5 "filterMode" Athanor/Assets/Art/Depth/candle_far.png.meta
# Should show filterMode: 1 (Bilinear), wrapMode: 0 (Clamp)
```

- [ ] **Step 3: Re-run DepthLayerTests**

Run: `unity test Athanor --mode EditMode --filter DepthLayerTests`
Expected: ALL PASS

- [ ] **Step 4: Commit**

```bash
git add Athanor/Assets/Art/Depth/candle_far.png.meta Athanor/Assets/Editor/FixCandleImport.cs
git commit -m "fix(art): candle_far.png Bilinear/Clamp import settings"
```

---

### Priority 2: Resume Phase 6 Task 5 — RunOverView Bake

#### Task 4: Complete RunOverView Bake (Phase 6 Task 5)

**Files:**
- Modify: `Athanor/Assets/Editor/ViewBaker.cs`
- Modify: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs`
- Modify: `Athanor/Assets/Scripts/Game/Ui/RunOverView.cs`
- Modify: `Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs`
- Scene: `Athanor/Assets/Scenes/Main.unity` (by Editor tool)

- [ ] **Step 1: Review current uncommitted diff**

```bash
git diff Athanor/Assets/Editor/ViewBaker.cs Athanor/Assets/Scripts/Game/Boot/GameFlow.cs Athanor/Assets/Scripts/Game/Ui/RunOverView.cs Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs
```

- [ ] **Step 2: Decide: continue from diff or restart**
  - If diff is clean and logical: continue
  - If diff has issues: `git checkout --` those 4 files and redo from plan

- [ ] **Step 3: Implement ViewBaker.BakeRunOverView()**

```csharp
// In ViewBaker.cs
[MenuItem("Athanor/Bake/RunOverView")]
public static void BakeRunOverView()
{
    var stage = UnityEditor.SceneManagement.StageUtility.GetCurrentStageHandle();
    var root = stage.FindComponentOfType<Athanor.Game.RunOverView>();
    if (root == null) { Debug.LogError("RunOverView not found in scene"); return; }
    
    // Bake: capture serialized Layout + Timing from the scene object
    // Compare with golden shot baseline (0.1% px tolerance)
    BakeView(root, "RunOverView");
}
```

- [ ] **Step 4: Add PrefabBakeTests case for RunOverView**

```csharp
[Test]
public void RunOverView_Bake_MatchesGoldenBaseline()
{
    var fixture = new PrefabFixture(Resources.Load<GameObject>("Prefabs/Views/RunOverView"));
    try
    {
        var view = fixture.Root.GetComponent<RunOverView>();
        var baked = ViewBaker.BakeInMemory(view);
        Assert.IsTrue(ImageCompare.WithinTolerance(baked, "RunOverView"), "RunOverView bake differs from baseline");
    }
    finally { fixture.Dispose(); }
}
```

- [ ] **Step 5: Run the bake and test**

```bash
unity batch Athanor --executeMethod Athanor.Editor.ViewBaker.BakeRunOverView --log-file /tmp/bake.log -- -quit
unity test Athanor --mode EditMode --filter PrefabBakeTests.RunOverView_Bake_MatchesGoldenBaseline
```

Expected: Bake succeeds, test PASS

- [ ] **Step 6: Commit**

```bash
git add Athanor/Assets/Editor/ViewBaker.cs Athanor/Assets/Scripts/Game/Boot/GameFlow.cs Athanor/Assets/Scripts/Game/Ui/RunOverView.cs Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs Athanor/Assets/Scenes/Main.unity
git commit -m "feat(bake): RunOverView baked to prefab with golden-shot parity"
```

---

### Priority 3: Core Purity & Static Analysis (Foundation)

#### Task 5: Audit Core for UL011 Violations (Mutable Statics)

**Files:**
- Read: All `Athanor/Assets/Scripts/Core/*.cs`
- Test: `Athanor/Assets/Tests/EditMode/StaticResetTests.cs` (add Core cases)

- [ ] **Step 1: Scan Core for mutable statics**

```bash
grep -rn "static " Athanor/Assets/Scripts/Core/ | grep -v "const\|readonly\|\[RuntimeInitializeOnLoadMethod\]"
```

- [ ] **Step 2: For each mutable static found, verify it has a reset method**

Check `StaticResetTests.cs` covers all Core classes with allowed statics.

- [ ] **Step 3: Add missing resets and tests**

If any Core class has mutable statics not in allowlist → move to Game layer or make immutable.

- [ ] **Step 4: Run lint**

```bash
python3 tools/lint/unity_lint.py Athanor/Assets/Scripts/Core/
```

Expected: 0 UL011 errors

- [ ] **Step 5: Commit**

```bash
git add Athanor/Assets/Scripts/Core/ Athanor/Assets/Tests/EditMode/StaticResetTests.cs
git commit -m "refactor(core): verify no mutable statics outside allowlist; add resets"
```

---

#### Task 6: Verify CORE-PURITY (No UnityEngine in Core)

**Files:**
- Read: All `Athanor/Assets/Scripts/Core/*.cs`

- [ ] **Step 1: Scan for UnityEngine imports**

```bash
grep -rn "using UnityEngine" Athanor/Assets/Scripts/Core/
```

- [ ] **Step 2: Fix any violations**

Move Unity-dependent code to Game layer.

- [ ] **Step 3: Run repo guard**

```bash
python3 tools/ci/check_repo.py --core-purity
```

Expected: PASS

- [ ] **Step 4: Commit**

```bash
git add Athanor/Assets/Scripts/Core/
git commit -m "refactor(core): enforce CORE-PURITY, no UnityEngine references"
```

---

### Priority 4: Phase 7 Remaining Refactor Tasks (Structural)

#### Task 7: Move Remaining Presentation Numbers to Prefabs/Scene

**Files:**
- Modify: Various Game layer files (per §7.11(e) list)
- Scene: `Athanor/Assets/Scenes/Main.unity`

**Numbers that intentionally STAY in code (§7.11(e)) — DO NOT MOVE:**
- `Flame` flicker and `Flipbook` 1/30 s
- Font sizes and sort orders
- `Dev` hook strings
- `Tooltip` `W`/`BaseH`/`RowH` (layout used in formulas)
- `CX = 320` / `O = 300` in `ReactionCutscene` and `SimpleCutscene`
- Sizes in `WindowView` and `RunOverView` (`Fits = 25`)
- Float-text fade split in `BenchView.FloatText` (0.7 / 0.3)
- Page-grid constants in `GoalsView`, `JournalView`, `TitleView` settings panel, `CatalystChoiceView`

**Numbers to VERIFY are in prefabs/scene:**
- All `Palette` colors (shared across views) → `Assets/Data/Palette.asset` ✅ Done
- `DepthMood.presets` in `Main.unity` (5 entries) ✅ Done
- `GameFlow.RunTiming` fades and autoplay pace → prefab ✅ Done
- `TileView` consume burst, `BenchView` hand strip, hit boxes, deal stagger, tooltip delay → prefab ✅ Done
- `GoalToast` box and `Timing` → prefab ✅ Done
- `PauseMenu.dialogTop` → prefab ✅ Done

- [ ] **Step 1: Audit each view for hardcoded positions/sizes not in §7.11(e)**

```bash
grep -rn "Px.At\|Px.Mid\|new Rect\|new Vector2\|new Vector3" Athanor/Assets/Scripts/Game/ | grep -v "0f\|1f\|0\|1)"
```

- [ ] **Step 2: For each violation not in §7.11(e), move to serialized field**

Add `[SerializeField]` field to the view, remove the hardcoded value, set in prefab/scene.

- [ ] **Step 3: Add LayoutPinTests cases**

```csharp
[Test]
public void ViewName_SerializedLayout_MatchesPinnedValue()
{
    var fixture = new PrefabFixture(Resources.Load<GameObject>("Prefabs/Views/ViewName"));
    try
    {
        var view = fixture.Root.GetComponent<ViewName>();
        Assert.AreEqual(expectedValue, view.layout.fieldName);
    }
    finally { fixture.Dispose(); }
}
```

- [ ] **Step 4: Run EditMode tests**

Expected: ALL PASS

- [ ] **Step 5: Commit**

```bash
git add Athanor/Assets/Scripts/Game/ Athanor/Assets/Tests/EditModeGame/LayoutPinTests.cs Athanor/Assets/Scenes/Main.unity
git commit -m "refactor(ui): move presentation numbers to prefabs; pin with LayoutPinTests"
```

---

#### Task 8: Remove SceneBuilder Legacy (Phase 6 Task 16 Follow-up)

**Files:**
- Verify: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs` — no `SceneBuilder` references
- Verify: `Athanor/Assets/Editor/` — no `SceneBuilder.cs`

- [ ] **Step 1: Confirm SceneBuilder is fully removed**

```bash
grep -r "SceneBuilder" Athanor/Assets/
```

Expected: No results

- [ ] **Step 2: Verify factories are used instead**

Check `Spawn` class (`Athanor/Assets/Scripts/Game/Support/Spawn.cs`) covers: Root, Sprite, Rect, Glow, Text, Click

- [ ] **Step 3: Run tests**

Expected: ALL PASS

- [ ] **Step 4: Commit (if any cleanup needed)**

```bash
git add -A
git commit -m "refactor(game): confirm SceneBuilder removed; Spawn factories in use"
```

---

#### Task 9: IgniteSequence Depth Looks Moved to Scene (Phase 7 Task 11)

**Files:**
- Verify: `Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs` — no depth look logic
- Verify: `Athanor/Assets/Scripts/Game/Cutscene/DepthLook.cs` — handles depth looks
- Scene: `Athanor/Assets/Scenes/Main.unity` — DepthLook objects present

- [ ] **Step 1: Verify IgniteSequence uses DepthLook from scene**

```bash
grep -n "DepthLook" Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs
```

Expected: References to `DepthLook` component, no hardcoded depth logic

- [ ] **Step 2: Run EditMode tests for IgniteSequence**

Run: `unity test Athanor --mode EditMode --filter IgniteSequence`
Expected: PASS

- [ ] **Step 3: Commit**

```bash
git add Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs Athanor/Assets/Scripts/Game/Cutscene/DepthLook.cs
git commit -m "refactor(ignite): depth looks moved to scene via DepthLook component"
```

---

#### Task 10: Palette Asset for Shared Colours (Phase 7 Task 12)

**Files:**
- Verify: `Athanor/Assets/Scripts/Game/Support/Palette.cs` — ScriptableObject exists
- Verify: `Athanor/Assets/Data/Palette.asset` — asset created
- Verify: `Art.Palette` published by GameFlow

- [ ] **Step 1: Verify Palette asset is used**

```bash
grep -rn "Art.Palette\|Palette\." Athanor/Assets/Scripts/Game/ | head -20
```

Expected: Views read colors from `Art.Palette` (rarity, aspect, gold, ink, pips, button text, star outline)

- [ ] **Step 2: Run tests**

Expected: PASS

- [ ] **Step 3: Commit**

```bash
git add Athanor/Assets/Scripts/Game/Support/Palette.cs Athanor/Assets/Data/Palette.asset
git commit -m "feat(ui): shared colours in Palette ScriptableObject"
```

---

### Priority 5: Content & Balance (Phase 7 Tasks 13+)

#### Task 11: Bench Layout Numbers in Prefabs (Phase 7 Task 13)

**Files:**
- Verify: `Athanor/Assets/Scripts/Game/Bench/BenchView.cs` — layout numbers serialized
- Prefab: `Athanor/Assets/Prefabs/Views/Bench.prefab`

- [ ] **Step 1: Verify BenchView has serialized Layout struct**

```csharp
// In BenchView.cs
[Serializable]
public struct Layout
{
    public Vector2 origin;
    public float tilePitch;
    public Vector2 handOrigin;
    public float handPitch;
    // ... all bench layout numbers
}
[SerializeField] public Layout layout;
```

- [ ] **Step 2: Verify prefab has values set**

Open `Bench.prefab` in Inspector, check `Layout` fields populated.

- [ ] **Step 3: Add LayoutPinTests case**

- [ ] **Step 4: Run tests**

Expected: PASS

- [ ] **Step 5: Commit**

```bash
git add Athanor/Assets/Scripts/Game/Bench/BenchView.cs Athanor/Assets/Prefabs/Views/Bench.prefab Athanor/Assets/Tests/EditModeGame/LayoutPinTests.cs
git commit -m "refactor(bench): layout numbers in prefab; pinned by LayoutPinTests"
```

---

#### Task 12: Tutorial Step/Area Scene Objects (Phase 7 / 2026-11-20 Decision)

**Files:**
- Create: `Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs`
- Create: `Athanor/Assets/Scripts/Game/Cutscene/TutorialStep.cs`
- Modify: `Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs`
- Modify: `Athanor/Assets/Scripts/Game/Cutscene/TutorialSettings.cs` (remove TutorialTargets)
- Create: `Athanor/Assets/Editor/TutorialStepsMigration.cs`
- Create: `Athanor/Assets/Editor/TutorialStepGizmo.cs`
- Test: `Athanor/Assets/Tests/EditModeGame/TutorialAreaTests.cs`
- Test: `Athanor/Assets/Tests/EditModeGame/TutorialStepsBakeTests.cs`
- Scene: `Athanor/Assets/Scenes/Main.unity` (Tutorial object with step children)

**Note:** Detailed plan exists at `docs/superpowers/plans/2026-11-20-tutorial-steps.md` (Tasks 0-7)

- [ ] **Step 1: Execute the tutorial-steps plan** (7 tasks, ~30 steps)
- [ ] **Step 2: Run all EditMode tests**
- [ ] **Step 3: Regenerate vault**
- [ ] **Step 4: Commit**

---

#### Task 13: Depth Scaling & Progression Overhaul (Phase 7+)

**Files:**
- Plan: `docs/superpowers/plans/2026-11-23-depth-scaling-progression.md` (10 tasks)

**Scope:** 5→10 depths, 6 rarity tiers + WorldSeed, bench 3×3→5×5 after depth 10, camera/visual per milestone

- [ ] **Step 1: Execute depth-scaling plan** (10 tasks)
- [ ] **Step 2: Run Core tests** (`dotnet test`)
- [ ] **Step 3: Run EditMode tests**
- [ ] **Step 4: Generate balance reports**
- [ ] **Step 5: Regenerate vault**
- [ ] **Step 6: Commit**

---

### Priority 6: Test Audit & Unity Compliance

#### Task 14: Execute Test Audit Plan

**Files:**
- Plan: `docs/superpowers/plans/2026-11-23-review-test-cases-unity-compat.md` (6 tasks)

- [ ] **Step 1: Fix `project.yaml commands.test`** (Task 1)
- [ ] **Step 2: Verify naming compliance** (Task 2)
- [ ] **Step 3: Standardize PrefabFixture cleanup** (Task 3)
- [ ] **Step 4: Add Arrange/Act/Assert markers** (Task 4)
- [ ] **Step 5: Clean up System.Collections** (Task 5)
- [ ] **Step 6: Regenerate vault + run CI** (Task 6)

---

### Priority 7: Final Integration & Release Prep

#### Task 15: Full CI Gate & Release Build

**Files:** None new; verification only

- [ ] **Step 1: Run full CI gate**

```bash
tools/ci/run.sh all
```

Expected: ALL GREEN (lint, repo-guard, core-tests, editmode, rotate, commits)

- [ ] **Step 2: Run release build**

```bash
unity build Athanor --target WebGL --execute-method WebBuild.Build --allow-dirty-build
```

Expected: Exit 0, `Athanor/Build/web` created

- [ ] **Step 3: Check build size**

```bash
du -sh Athanor/Build/web/
# Must be ≤ 11,824,672 B
```

- [ ] **Step 4: Run web tests**

```bash
python3 -m http.server 8080 -d Athanor/Build/web &
node tools/web/webtest.mjs http://localhost:8080/ /tmp/webtest
node tools/web/mainhd2d.mjs http://localhost:8080/ /tmp/hd2d 640x360 hud,tiles
node tools/web/loadsmoke.mjs http://localhost:8080/ /tmp/smoke
```

Expected: All exit 0, 0 console errors

- [ ] **Step 5: Run balance reports**

```bash
DOTNET_ROLL_FORWARD=Major dotnet run --project tools/CoreReports -- balance-matrix
DOTNET_ROLL_FORWARD=Major dotnet run --project tools/CoreReports -- events
DOTNET_ROLL_FORWARD=Major dotnet run --project tools/CoreReports -- roles
DOTNET_ROLL_FORWARD=Major dotnet run --project tools/CoreReports -- newcomer
DOTNET_ROLL_FORWARD=Major dotnet run --project tools/CoreReports -- balance
DOTNET_ROLL_FORWARD=Major dotnet run --project tools/CoreReports -- trace
```

Expected: Numbers match baseline or documented changes

- [ ] **Step 6: Regenerate vault**

```bash
python3 tools/vault/build_vault.py
```

Expected: No diff

- [ ] **Step 7: Commit final state**

```bash
git add -A
git commit -m "chore(release): CI green, build size OK, web tests pass, vault current"
```

---

## Execution Order Summary

| Priority | Task | Est. Effort | Depends On |
|----------|------|-------------|------------|
| 1 | Fix GoldenShotTests | 30 min | — |
| 1 | Fix DepthLayerTests path | 15 min | — |
| 1 | Fix candle_far.png import | 30 min | — |
| 2 | Complete RunOverView bake (P6 Task 5) | 2 hr | Task 1-3 |
| 3 | Core UL011 audit | 1 hr | — |
| 3 | CORE-PURITY verify | 30 min | — |
| 4 | Move remaining presentation numbers | 3 hr | Task 3 |
| 4 | SceneBuilder cleanup verify | 30 min | — |
| 4 | IgniteSequence depth looks | 30 min | — |
| 4 | Palette asset verify | 30 min | — |
| 5 | Bench layout numbers | 1 hr | Task 4 |
| 5 | Tutorial scene objects | 4 hr | Task 4 |
| 5 | Depth scaling overhaul | 8 hr | Task 5, 11 |
| 6 | Test audit & compliance | 2 hr | — |
| 7 | Full CI + release build | 1 hr | All above |

**Total Estimate:** ~24 hours of focused work

---

## Self-Review Checklist

- [ ] **Spec coverage:** All Phase 6 Tasks 5-25 addressed? All Phase 7 items from ATHANOR.md §7 addressed?
- [ ] **Placeholder scan:** No "TBD", "implement later", or vague steps
- [ ] **Type consistency:** Method signatures match across tasks
- [ ] **Contract coverage:** CORE-PURITY, UL009-UL014, LANG, ICONS, CONTENT checks included
- [ ] **Test evidence:** Every behavioral change has test-first step
- [ ] **Vault regeneration:** Included after structural changes
- [ ] **CI gate:** Final verification step included

---

**Plan saved to:** `docs/superpowers/plans/2026-11-24-core-refactor-priority.md`

**Two execution options:**

1. **Subagent-Driven (recommended)** — I dispatch a fresh subagent per task, review between tasks, fast iteration
   - **REQUIRED SUB-SKILL:** Use `superpowers:subagent-driven-development`

2. **Inline Execution** — Execute tasks in this session using `superpowers:executing-plans`, batch execution with checkpoints
   - **REQUIRED SUB-SKILL:** Use `superpowers:executing-plans`

**Which approach?**