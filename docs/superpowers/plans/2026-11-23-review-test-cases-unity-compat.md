# Test Audit & Unity Template Compliance Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Audit every Unity test file against `.claude/rules/test-standards.md`, fix any boundary or structure violations, and update the stale `commands.test` entry in `project.yaml` to target the actual Unity-native test runner.

**Architecture:** The project migrated from a standalone `tools/CoreTests` dotnet project into two Unity asmdef-separated test assemblies (`Athanor.Core.Tests` in `EditMode/`, `Athanor.Game.Tests` in `EditModeGame/`). All `core` tests run via `unity test Athanor --mode EditMode`; Game-layer editor tests require the `com.unity.test-framework` package already referenced in both asmdefs. The audit touches no production code — only `project.yaml` and test source files.

**Tech Stack:** Unity 6000.6.7f1, NUnit 3.x, `com.unity.test-framework` (both `UnityEngine.TestRunner` and `UnityEditor.TestRunner`), `Athanor.Core.Tests.asmdef`, `Athanor.Game.Tests.asmdef`.

**Spec:** `.claude/rules/test-standards.md` (§5); `vault/Workflow/Vault Guide.md` (vault re-build step).

## Global Constraints

- Every `[Test]` method must follow `[Scenario]_[Expected]` PascalCase; helper/lifecycle methods may not be annotated with `[Test]`.
- `Athanor/Assets/Tests/EditMode/` contains zero `UnityEngine` references — that assembly is pure C# rule testing; any accidental `using UnityEngine` here is a violation.
- `Athanor/Assets/Tests/EditModeGame/` uses `using UnityEditor;` and `using UnityEditor.SceneManagement;` intentionally — do not flag these as violations.
- `tools/CoreTests` no longer exists; its replacement paths are the Unity asmdef assemblies.
- After changing any test fixture or `content.json` reference inside a test, regenerate the vault: `python3 tools/vault/build_vault.py`.
- Commit per task; message prefix `test(` or `chore(test):`.

## Review Focus

- **Editor-only boundary leak**: An `EditMode` test importing `UnityEngine` or `UnityEditor` would break the no-Unity-gui rule for Core tests — verify the current split is clean.
- **Stale `commands.test` ghost branch**: CI runs `tools/ci/run.sh all` before merge; if the shell script resolves `project.yaml commands.test` at runtime, the build hangs waiting for a nonexistent path — confirm the smoke script accepts the new value.
- **PrefabFixture cleanup order**: Tests that forget `f.Dispose()` (or the older `f.Destroy()`) leave GameObjects floating between `[TearDown]` runs, leaking memory and making later tests see orphaned state — every caller must use `using var` after Task 3 lands.
- **Non-generic `System.Collections` in StaticResetTests**: The bare `using System.Collections;` likely points to legacy `ArrayList`-style patterns; replace with `using System.Collections.Generic;` if any generic type is used elsewhere in that file.
- **Missing Arrange/Act/Assert markers**: Files like `ScreenFlowTests.cs` have zero `// Arrange` / `// Act` / `// Assert` comment lines — the linter cannot enforce structure without them, making visual diffs harder during review.

---

### Task 1: Fix stale `project.yaml commands.test`

**Files:**
- Modify: `project.yaml:commands.test`

**Interfaces:**
- Consumes: nothing (configuration change only)
- Produces: updated `commands.test` string usable by `tools/ci/run.sh` and `unity test`

- [ ] **Step 1: Confirm current value is stale**

Run: `grep -n 'commands\.' project.yaml`
Expected output includes: `test: "dotnet test tools/CoreTests"`

- [ ] **Step 2: Replace with Unity EditMode test command**

In `project.yaml`, change:
```yaml
  test: "dotnet test tools/CoreTests"
```
to:
```yaml
  test: "unity test Athanor --mode EditMode"
```

- [ ] **Step 3: Run the command (dry-run or dry-test)**

Run: `unity test Athanor --mode EditMode` (if Unity CLI available) or at minimum verify the string parses without error.
Expected: YAML key valid; no dotnet or CoreTests reference remains in the file.

- [ ] **Step 4: Commit**

```bash
git add project.yaml
git commit -m "chore(test): update commands.test to unity test EditMode"
```

---

### Task 2: Verify naming compliance across all `[Test]` methods

**Files:**
- Read-only audit of all `.cs` files under `Athanor/Assets/Tests/EditMode/` and `Athanor/Assets/Tests/EditModeGame/`

**Interfaces:**
- Consumes: nothing
- Produces: short audit report listing any names that fail the `[Scenario]_[Expected]` PascalCase check

- [ ] **Step 1: Grep for every `[Test]`-annotated method signature**

Run:
```bash
grep -rn '\[Test\]' Athanor/Assets/Tests/EditMode/*.cs Athanor/Assets/Tests/EditModeGame/*.cs | head -600
```
Then extract each `public void METHODNAME()` line and run:
```bash
grep -hEn 'public void [a-zA-Z_]+\(' Athanor/Assets/Tests/EditMode/*.cs Athanor/Assets/Tests/EditModeGame/*.cs | grep -vE '^.*public void [A-Z][a-zA-Z]*_[A-Z]'
```

- [ ] **Step 2: Check the output**

If the second command produces zero results, naming is compliant — skip Step 3.
If it produces any matches, inspect those files manually to confirm the match is actually a `[Test]` method (not a `[SetUp]`/`[TearDown]`/helper), then rename the violated one to `[Scenario]_[Expected]` PascalCase.

- [ ] **Step 3: (Only if violations exist) Rename non-compliant test methods**

Rename to satisfy PascalCase `Category_Scenario_Expected`:
```bash
# Example — adjust to actual violation:
git mv Athanor/Assets/Tests/EditModeGame/FooTests.cs Athanor/Assets/Tests/EditModeGame/FooTests.cs  # no-op rename not needed
# Edit the method name in-file; update any [TestCaseSource] or custom filters referencing the old name.
```

- [ ] **Step 4: Commit (if any rename was made); otherwise skip commit for this task**

```bash
# Only if renames were done:
git add Athanor/Assets/Tests/
git commit -m "test(naming): ensure all [Test] methods follow PascalCase Scenario_Expected"
```

---

### Task 3: Standardise PrefabFixture cleanup — adopt `using var`

**Files:**
- Read: `Athanor/Assets/Tests/EditModeGame/PrefabFixture.cs`
- Read/Modify: every file in `Athanor/Assets/Tests/EditModeGame/` that instantiates `new PrefabFixture(...)`

**Interfaces:**
- Consumes: `PrefabFixture` class (unchanged); existing callers with `try { } finally { f.Destroy(); }`
- Produces: all callers rewritten as `using var f = new PrefabFixture(...);` for deterministic cleanup

- [ ] **Step 1: Confirm PrefabFixture implements IDisposable**

Read `Athanor/Assets/Tests/EditModeGame/PrefabFixture.cs:1-5`. If it does NOT declare `IDisposable`, add the interface and wrap the body's `Dispose()` call around `Destroy()` — otherwise proceed as-is.

- [ ] **Step 2: Find every call site**

Run:
```bash
grep -rln 'new PrefabFixture' Athanor/Assets/Tests/EditModeGame/
grep -rn 'PrefabFixture\b' Athanor/Assets/Tests/EditModeGame/ | grep -v '^Binary'
```

- [ ] **Step 3: Rewrite each call site**

Replace every pattern of:
```csharp
var f = new PrefabFixture(SomePrefab);
try { ... }
finally { f.Destroy(); }
```
with:
```csharp
using var f = new PrefabFixture(SomePrefab);
...   // body unchanged
```
(If a particular test needs deferred destroy — e.g., because another object keeps a reference — keep the explicit `try/finally`; those cases are rare and noted inline.)

- [ ] **Step 4: Commit**

```bash
git add Athanor/Assets/Tests/EditModeGame/
git commit -m "test(cleanup): standardise PrefabFixture to using-var pattern"
```

---

### Task 4: Add missing Arrange / Act / Assert markers

**Files:**
- Read/Modify: `Athanor/Assets/Tests/EditMode/ScreenFlowTests.cs` (confirmed zero marker comments)
- Optionally read/modify other files with very few markers (e.g. `RulesTests.cs` with ~2)

**Interfaces:**
- Consumes: existing test bodies (read-only inspection)
- Produces: in-line `// Arrange`, `// Act`, `// Assert` comments at logical boundaries inside affected tests

- [ ] **Step 1: Inspect ScreenFlowTests.cs**

Read `Athanor/Assets/Tests/EditMode/ScreenFlowTests.cs` in full. Identify each `[Test]` method's logical sections.

- [ ] **Step 2: Insert markers**

Add exactly one of each comment block per test, placed immediately before the relevant section:
```csharp
[Test]
public void OpenJournal_FromBench_ClosesBackToBench()
{
    // Arrange
    ...

    // Act
    ...

    // Assert
    ...
}
```

- [ ] **Step 3: Repeat for RulesTests.cs and any other sparse files**

Same pattern; only touch files whose comment count is below 10% of their assertion count.

- [ ] **Step 4: Commit**

```bash
git add Athanor/Assets/Tests/EditMode/ScreenFlowTests.cs Athanor/Assets/Tests/EditMode/RulesTests.cs
git commit -m "test(structure): add Arrange/Act/Assert markers where absent"
```

---

### Task 5: Clean up `System.Collections` in `StaticResetTests.cs`

**Files:**
- Read/Modify: `Athanor/Assets/Tests/EditModeGame/StaticResetTests.cs`

**Interfaces:**
- Consumes: `using System.Collections;` (likely unused or legacy)
- Produces: `using System.Collections.Generic;` if generics are used; removal of the old using if it serves nothing

- [ ] **Step 1: Inspect the file for non-generic collection usages**

Read `Athanor/Assets/Tests/EditModeGame/StaticResetTests.cs` fully. Grep for any type from `System.Collections` (non-generic): `ArrayList`, `Hashtable`, `Queue`, `Stack`, `SortedList`.

- [ ] **Step 2: Replace if any usage found**

If a non-generic collection is used, rewrite to its generic counterpart (`List<T>`, `Queue<T>`, etc.). Then replace:
```csharp
using System.Collections;
```
with:
```csharp
using System.Collections.Generic;
```
(Keep `using System.Reflection;` and all other existing using directives.)

- [ ] **Step 3: Remove the directive if it is entirely unused**

- [ ] **Step 4: Commit**

```bash
git add Athanor/Assets/Tests/EditModeGame/StaticResetTests.cs
git commit -m "test(style): replace System.Collections with System.Collections.Generic in StaticResetTests"
```

---

### Task 6: Regenerate vault and run CI gate

**Files:**
- None written; verification only

**Interfaces:**
- Consumes: changes from Tasks 1–5
- Produces: green CI gate (`tools/ci/run.sh all`) plus fresh vault notes

- [ ] **Step 1: Regenerate vault notes**

Run:
```bash
python3 tools/vault/build_vault.py
```
Expected: exit 0; no errors; regenerated notes reflect any content/test data touched.

- [ ] **Step 2: Run the CI gate**

Run:
```bash
tools/ci/run.sh all
```
Expected: all checks green. If a check fails because `commands.test` ran the old dotnet path, confirm Task 1 landed; if it fails due to a Unity test run error, capture the failure and fix or quarantine — never weaken the check to get green.

- [ ] **Step 3: Commit vault regeneration**

```bash
git add vault/ docs/athenor.md  # whatever the build_vault.py touches
git commit -m "chore(vault): regenerate after test-audit fixes"
```
