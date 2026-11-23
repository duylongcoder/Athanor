# Depth Scaling & Progression Overhaul Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:executing-plans or superpowers:subagent-driven-development to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Scale Athanor's depth progression from 5 to 10 levels with milestone ranges, expand rarity system to 6 tiers, add WorldSeed instant-win mechanic, expand bench grid after depth 10, and adjust camera/visuals per depth range.

**Architecture:** Core rules in `Athanor.Core` stay Unity-free; Game-layer visuals/handling in `Athanor.Game`. Depth milestones drive bench size changes, trial scaling, and difficulty curves. WorldSeed is treated as a unique single-tier catalyst with special win semantics decoupled from potency scoring.

**Tech Stack:** Unity 6000.6.7f1, C#, NUnit (EditMode tests), `dotnet test Athanor/Athanor.Core.Tests.csproj`, vault regeneration via `python3 tools/vault/build_vault.py`

**Spec:** This plan *is* the spec — distilled from player-facing Vietnamese requirements supplied inline. No external GDD section overrides these values.

## Global Constraints

- Tests first: every behavioral change has a failing test verified before implementation
- Core layer stays free of `UnityEngine` imports
- All player-visible strings must have `en.json` / `vi.json` entries
- `depthTargets.Length == depthNames.Length - 1`; FinalDepth = `depthNames.Length`
- WorldSeed does NOT count toward Potency value
- Bench expansions are governed by depth milestones, not permanent state
- Conventional Commits format for all commits

## Review Focus

1. **WorldSeed crafted at non-final depth triggers Won** — should end run immediately regardless of remaining trials
2. **BenchRules spatial logic holds at 5×5** — neighbor tables, reachability, hub scoring for larger grid
3. **Rarity weights distribute correctly across 6 tiers** — draw probabilities sum to 1.0
4. **Depth 10 checkpoint offers continue/stop UI path** — not auto-transition
5. **Potency score excludes WorldSeed result material** — scoring isolation

---

### Task 1: Enum & Rarity Expansion

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/ContentData.cs` — Rarities class
- Test: `Athanor/Assets/Tests/EditMode/RarityTests.cs`

**Interfaces:**
- Consumes: None (foundation)
- Produces: `Rarities.Epic = "Epic"`, `Rarities.Mythic = "Mythic"`, `Rarities.WorldSeed = "WorldSeed"`, `Rarities.All` array length 7

- [ ] **Step 1: Write failing test for expanded rarities**

```csharp
[Test]
public void Rarities_All_HasSevenEntries()
{
    Assert.AreEqual(7, Rarities.All.Length);
    CollectionAssert.Contains(Rarities.All, Rarities.Epic);
    CollectionAssert.Contains(Rarities.All, Rarities.Mythic);
    CollectionAssert.Contains(Rarities.All, Rarities.WorldSeed);
}
```

- [ ] **Step 2: Run test — confirm RED**

Run: `dotnet test Athanor/Athanor.Core.Tests.csproj --filter "Rarities_All_HasSevenEntries"`
Expected: FAIL (member not defined)

- [ ] **Step 3: Expand Rarities constants in ContentData.cs**

Add three constants and extend `All` array:
```csharp
public const string Epic = "Epic", Mythic = "Mythic", WorldSeed = "WorldSeed";
public static readonly string[] All = { Common, Uncommon, Rare, Epic, Legendary, Mythic, WorldSeed };
```

- [ ] **Step 4: Run test — confirm GREEN**

Run: `dotnet test Athanor/Athanor.Core.Tests.csproj --filter "Rarities_All_HasSevenEntries"`
Expected: PASS

- [ ] **Step 5: Run full suite**

Run: `dotnet test Athanor/Athanor.Core.Tests.csproj`
Expected: ALL PASS

- [ ] **Step 6: Commit**

```bash
git add Athanor/Assets/Scripts/Core/ContentData.cs Athanor/Assets/Tests/EditMode/RarityTests.cs
git commit -m "feat(rarity): expand to 6 material tiers plus WorldSeed"
```

---

### Task 2: Depth System Expansion (5 → 10)

**Files:**
- Modify: `Athanor/Assets/Scripts/Data/GameConfigSO.cs` — depthTargets, depthNames
- Modify: `Athanor/Assets/Tests/EditMode/TestContent.cs` — Tiny() fixture
- Modify: `Athanor/Assets/Tests/EditMode/RulesTests.cs` — FinalDepth assertions
- Test: new/add tests in RulesTests.cs

**Interfaces:**
- Consumes: Rarities.All from Task 1
- Produces: FinalDepth = 10, depthTargets with 9 entries, depthNames with 10 entries

- [ ] **Step 1: Write failing test for FinalDepth = 10**

```csharp
[Test]
public void FinalDepth_ShippedContent_IsTen()
{
    var db = ContentDb.From(GameConfigSO.TestConfig());
    Assert.AreEqual(10, db.FinalDepth);
}
```

- [ ] **Step 2: Update TestContent.Tiny() to produce 10 depths**

In `TestContent.cs`:
```csharp
depthTargets = new[] { 300, 900, 2500, 7000, 15000, 30000, 60000, 120000, 250000 };
depthNames  = new[] { "1","2","3","4","5","6","7","8","9","10" };
```

- [ ] **Step 3: Run test — confirm RED**

Run: `dotnet test Athanor/Athanor.Core.Tests.csproj --filter "FinalDepth_ShippedContent_IsTen"`
Expected: FAIL (current FinalDepth = 5)

- [ ] **Step 4: Update GameConfigSO sample data and validation**

Ensure `depthTargets` length matches `depthNames.Length - 1`. Add tooltip comment.

- [ ] **Step 5: Run test — confirm GREEN, then full suite**

Run: `dotnet test Athanor/Athanor.Core.Tests.csproj`
Expected: ALL PASS

- [ ] **Step 6: Commit**

```bash
git add Athanor/Assets/Scripts/Data/GameConfigSO.cs Athanor/Assets/Tests/EditMode/TestContent.cs Athanor/Assets/Tests/EditMode/RulesTests.cs
git commit -m "feat(depth): scale progression to 10 levels with milestone targets"
```

---

### Task 3: WorldSeed Win/Lose Semantics

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/RunState.cs` — ignite resolution
- Modify: `Athanor/Assets/Scripts/Core/Ignite.cs` — Resolve/Score
- Test: `Athanor/Assets/Tests/EditMode/RunStateTests.cs`

**Interfaces:**
- Consumes: `Rarities.WorldSeed` constant from Task 1
- Produces: Instant-win on WorldSeed craft at any depth; lose at depth 100 if trials exhausted without WorldSeed

- [ ] **Step 1: Write failing test — WorldSeed triggers Won**

```csharp
[Test]
public void Igniting_WorldSeed_TransitionsToWon()
{
    var run = RunState.NewTestRun();
    // Place worldseed material on bench, ignite with matching recipe
    run.Placement["worldseed"] = ...; // see existing test patterns
    var outcome = run.Ignite(db, journal, catalysts);
    Assert.AreEqual(RunPhase.Won, run.Phase);
}
```

- [ ] **Step 2: Write failing test — WorldSeed excluded from Potency**

```csharp
[Test]
public void WorldSeed_Result_Not_Counted_In_Potency()
{
    // Craft worldseed; verify gained potency is 0 from this reaction
    Assert.AreEqual(0, outcome.Gained); // or however gained is tracked
}
```

- [ ] **Step 3: Implement Win condition in RunState.Ignite()**

After resolve, check:
```csharp
if (madeUltimate) Phase = RunPhase.Won; // already exists — ensure ultimateMaterialId == "worldseed"
```
Then override score: if result material is WorldSeed, set `gained = 0`.

- [ ] **Step 4: Implement Lose at Depth 100 without WorldSeed**

At depth index == FinalDepth - 1 (i.e., depth 10), if trials run out before crafting WorldSeed → Lost.
Update the lose condition check in `BeginDepth()` or the trials exhaustion branch.

- [ ] **Step 5: Run tests — confirm GREEN, then full suite**

Run: `dotnet test Athanor/Athanor.Core.Tests.csproj`
Expected: ALL PASS

- [ ] **Step 6: Commit**

```bash
git add Athanor/Assets/Scripts/Core/RunState.cs Athanor/Assets/Scripts/Core/Ignite.cs Athanor/Assets/Tests/EditMode/RunStateTests.cs
git commit -m "feat(worldseed): instant-win on craft; lose at final depth without it; exclude from potency"
```

---

### Task 4: Bench Expansion After Depth 10

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/Ignite.cs` — Bench class (make Size dynamic)
- Modify: `Athanor/Assets/Scripts/Core/BenchRules.cs` — tile count references
- Modify: `Athanor/Assets/Scripts/Core/RunState.cs` — hand size tied to bench size
- Test: `Athanor/Assets/Tests/EditMode/RulesTests.cs` — spatial tests at 5×5

**Interfaces:**
- Consumes: depth milestone from Task 2
- Produces: Bench.TileCount = 25 after depth > 10, recalculated Centre = 12

**Ruling:** Rather than changing the immutable `const int`, introduce `CurrentSize()` on the Bench instance (set during BeginDepth). Static constants remain for backward compatibility of helper methods. BenchRules builds adjacency for the active size.

- [ ] **Step 1: Write failing test — bench reports 5×5 after depth milestone**

```csharp
[Test]
public void Bench_Size_BecomesFiveByFive_AfterDepthTen()
{
    var run = RunState.NewTestRun();
    run.AdvanceDepth(10); // past milestone
    Assert.AreEqual(25, run.Bench.TileCount);
    Assert.AreEqual(12, run.Bench.Centre);
}
```

- [ ] **Step 2: Make Bench.Size and TileCount instance properties**

Replace constants with instance-backed properties. Maintain static defaults for helpers. Update all callers passing `Bench.TileCount` to use the instance when available, or provide an overload.

- [ ] **Step 3: Update BenchRules.Build() for dynamic grid sizes**

Replace hardcoded 3 with `bench.Size`. Recompute neighbours, hub table, blocked/heavy/attuned arrays dynamically from layout entry (expect layout entries scaled to current size).

- [ ] **Step 4: Tie hand size to bench size**

Hand size = `bench.Size * bench.Size / 2` rounded up (13 for 5×5).

- [ ] **Step 5: Run tests — confirm GREEN, full suite**

Run: `dotnet test Athanor/Athanor.Core.Tests.csproj`
Expected: ALL PASS

- [ ] **Step 6: Commit**

```bash
git add Athanor/Assets/Scripts/Core/Ignite.cs Athanor/Assets/Scripts/Core/BenchRules.cs Athanor/Assets/Scripts/Core/RunState.cs Athanor/Assets/Tests/EditMode/RulesTests.cs
git commit -m "feat(bench): expand to 5x5 after depth 10 milestone"
```

---

### Task 5: Milestone Difficulty Scaling

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/ContentDb.cs` — target computation
- Modify: `Athanor/Assets/Scripts/Data/GameConfigSO.cs` — depthTargets spread
- Test: `Athanor/Assets/Tests/EditMode/RulesTests.cs`

**Interfaces:**
- Consumes: depthNames array from Task 2
- Produces: Per-depth trial counts, potency targets reflecting milestones at 10, 30, 60

- [ ] **Step 1: Write failing test — depthTargets has nine entries for ten names**

```csharp
[Test]
public void DepthTargets_Length_IsOneLess_Than_DepthNames()
{
    var cfg = GameConfigSO.TestConfig();
    Assert.AreEqual(cfg.depthNames.Length - 1, cfg.depthTargets.Length);
}
```

- [ ] **Step 2: Spread targets across four milestone bands**

Band 1–10: moderate growth  
Band 11–30: steep ramp  
Band 31–60: accelerating  
Band 61–100: exponential  

Example targets array (9 values, one per transition):
```csharp
new[] { 300, 900, 2500, 7000, 15000, 30000, 60000, 120000, 250000 }
```

- [ ] **Step 3: Validate test runs green**

Run: `dotnet test Athanor/Athanor.Core.Tests.csproj --filter "DepthTargets_Length"`
Expected: PASS

- [ ] **Step 4: Commit**

```bash
git add Athanor/Assets/Scripts/Data/GameConfigSO.cs Athanor/Assets/Tests/EditMode/RulesTests.cs
git commit -m "feat(balance): spread depth targets across four milestone bands"
```

---

### Task 6: Checkpoint at Depth 10 (Player Choice)

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/RunState.cs` — Phase transitions
- Modify: `Athanor/Assets/Scripts/Game/Flow/*` or relevant screen — continue/stop UI
- Test: `Athanor/Assets/Tests/EditMode/ScreenFlowTests.cs`

**Interfaces:**
- Consumes: depth milestone value from Task 2
- Produces: New phase `DepthCheckpoint` offering Continue / Stop choices

- [ ] **Step 1: Write failing test — reaching depth 10 with cleared phase yields checkpoint**

```csharp
[Test]
public void ClearingDepth_Ten_TransitionsToDepthCheckpoint()
{
    var run = RunState.NewTestRun();
    // advance to depth 10, clear it
    run.BeginDepth(); // sets Depth = 10 (index 9)
    run.TriggerIgniteAndClear(/* ... */);
    Assert.AreEqual(RunPhase.DepthCheckpoint, run.Phase);
}
```

- [ ] **Step 2: Add DepthCheckpoint to RunPhase enum**

```csharp
public enum RunPhase { Playing, DepthCleared, DepthCheckpoint, Lost, Won, AspectOffer, EventOffer }
```

- [ ] **Step 3: Implement checkpoint transition in RunState**

When `Depth == 10 && Phase == DepthCleared` after ignition success, transition to `DepthCheckpoint`.

- [ ] **Step 4: Wire UI screen for continue/stop choice**

Existing DepthCleared flow handles catalyst choice; extend with checkpoint decision screen.

- [ ] **Step 5: Run tests — GREEN, full suite**

Run: `dotnet test Athanor/Athanor.Core.Tests.csproj` && `unity test Athanor --mode EditMode`
Expected: ALL PASS

- [ ] **Step 6: Commit**

```bash
git add Athanor/Assets/Scripts/Core/RunState.cs Athanor/Assets/Tests/EditMode/ScreenFlowTests.cs
git commit -m "feat(checkpoint): player choice at depth 10 milestone"
```

---

### Task 7: Auto-Escalation at Depths 30 & 60

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/RunState.cs`
- Test: `Athanor/Assets/Tests/EditMode/RunStateTests.cs`

**Interfaces:**
- Consumes: depth milestone values (30, 60)
- Produces: Automatic difficulty escalation (higher targets, fewer trials) at these depths

- [ ] **Step 1: Write failing test — depth 30 triggers escalated mode**

```csharp
[Test]
public void ReachingDepthThirty_ActivatesEscalatedDifficulty()
{
    var run = RunState.NewTestRun();
    run.AdvanceToDepth(30);
    Assert.IsTrue(run.Escalated);
    Assert.Greater(run.Target, run.PreviousTarget);
}
```

- [ ] **Step 2: Implement escalation flag and target bump**

On `BeginDepth()` when depth index is 29 (depth 30) or 59 (depth 60), set `Escalated = true` and multiply target by escalation factor (e.g., ×1.5).

- [ ] **Step 3: Run tests — GREEN, full suite**

Expected: ALL PASS

- [ ] **Step 4: Commit**

```bash
git add Athanor/Assets/Scripts/Core/RunState.cs Athanor/Assets/Tests/EditMode/RunStateTests.cs
git commit -m "feat(difficulty): auto-escalate at depth milestones 30 and 60"
```

---

### Task 8: Camera & Visual Effects per Milestone Range

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Views/*` — camera controller
- Modify: `Athanor/Assets/Scripts/Game/Effects/*` — milestone VFX
- Test: Screenshot evidence (no automated test for visuals)

**Interfaces:**
- Consumes: current depth milestone range
- Produces: Orthogonal camera while placing materials; normal perspective while cooking; milestone-specific background tint/VFX

- [ ] **Step 1: Document camera states (design pass, no code yet)**

Orthographic projection during placement phase. Perspective/angled during ignition/cooking. Smooth lerp between states.

- [ ] **Step 2: Implement camera mode toggle in Game view**

```csharp
public void SetCameraMode(CameraMode mode) { /* ortho vs perspective */ }
public enum CameraMode { Placement, Cooking }
```

- [ ] **Step 3: Trigger camera mode on phase transitions**

Placement mode when `Phase == Playing && !igniting`. Cooking mode during ignite resolution.

- [ ] **Step 4: Add milestone range visual tint**

Milestone band colours (soft overlay):
- 1–10: cool blue tint
- 11–30: amber tint
- 31–60: violet tint
- 61–100: crimson tint

- [ ] **Step 5: Capture retained screenshots for QA evidence**

Place in `production/qa/evidence/` per milestone range.

- [ ] **Step 6: Commit**

```bash
git add Athanor/Assets/Scripts/Game/Views/CameraController.cs Athanor/Assets/Scripts/Game/Effects/MilestoneVfx.cs
git commit -m "feat(camera): orthogonal placement, perspective cooking, milestone tints"
```

---

### Task 9: Icon Design System for New Models

**Files:**
- Create: `tools/art/icons.mjs` additions for Epic, Mythic, WorldSeed
- Modify: `assets/icon-specs.md` (new doc)
- Pixel artist agent produces sprite sheets

**Interfaces:**
- Consumes: rarity names from Task 1
- Produces: Icons for Epic tier 1&2, Mythic tier 1&2, WorldSeed (unique)

- [ ] **Step 1: Define icon grid specs**

64×64 px icons. Each rarity gets distinct border colour + glow pattern:
- Common: grey border
- Uncommon: green border
- Rare: blue border
- Epic: purple border
- Legendary: gold border
- Mythic: iridescent/shimmer border
- WorldSeed: white-gold radiant halo, unique center motif

- [ ] **Step 2: Generate icons via tools/art/icons.mjs**

Run icon generator with new rarity entries.

- [ ] **Step 3: Verify icons appear in content.json / atlas**

- [ ] **Step 4: Commit**

```bash
git add Athanor/Assets/Art/Icons/ Athanor/Assets/Resources/content.json
git commit -m "feat(art): icon set for Epic, Mythic rarities and WorldSeed"
```

---

### Task 10: Vault Regeneration & Balance Report

**Files:**
- Run: `python3 tools/vault/build_vault.py`
- Run: `dotnet run --project tools/CoreReports -- balance-matrix|events|roles|newcomer|balance|trace`
- Update: `docs/ATHANOR.md` § Decisions in force

**Interfaces:**
- Consumes: all previous tasks' structural changes
- Produces: Updated vault notes referencing new types/constants

- [ ] **Step 1: Regenerate vault**

Run: `python3 tools/vault/build_vault.py`
Expected: clean output, no errors

- [ ] **Step 2: Generate balance report**

Run: `dotnet run --project tools/CoreReports -- balance-matrix`
Expected: matrix shows targets aligned with new depth curve

- [ ] **Step 3: Add decision record to ATHANOR.md**

Document depth 10 checkpoint, WorldSeed win condition, bench expansion rule.

- [ ] **Step 4: Commit**

```bash
git add vault/ docs/ATHANOR.md
git commit -m "chore(vault): regenerate after depth/rarity/bench overhaul"
```

---

## Completion Order Note

Tasks 1→2 are hard prerequisites for 3→4→5. Task 6 depends on 2. Task 7 depends on 2. Tasks 8 and 9 are independent of core logic and can proceed in parallel once the enums are in place. Task 10 is final only.
