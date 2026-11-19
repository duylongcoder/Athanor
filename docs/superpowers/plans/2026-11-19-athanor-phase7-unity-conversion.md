# Athanor Phase 7: Unity conversion — Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Move every hard-coded rule, layout, colour and timing number out of C# into `content.json`, prefabs/scene and two ScriptableObjects, split `GameFlow`, make statics safe with domain reload off, and lock it in with lint. Nothing the player sees or scores may change.

**Architecture:** Rules numbers go to a `rules` block in `content.json` (read by Core, the dotnet tests and the balance reports). Presentation numbers go to the prefab or scene that owns them; values shared across views go to a `Palette` ScriptableObject. `GameFlow` becomes a composition root over a plain-C# `ScreenFlow` (Core) and an `IgniteSequence` component. Golden images and byte-identical reports prove nothing changed.

**Tech Stack:** Unity 6000.6.7f1 (URP 2D, WebGL), C# (Core: netstandard-compatible, compiled by `tools/CoreTests` net8.0), NUnit, PrimeTween 1.5.2, Python 3 lint (`tools/lint/unity_lint.py`).

**Spec:** `docs/superpowers/specs/2026-11-19-athanor-phase7-unity-conversion-design.md` (read it with this plan; § numbers below refer to it).

## Global Constraints

- WebGL only; the canvas stays 640x360 with whole-number scaling. No PC, Steam or resolution work.
- `Scripts/Core` has no `UnityEngine` (UL008, CORE-PURITY). `content.json` shapes: public fields, no properties, no dictionaries (read by `JsonUtility` and `System.Text.Json` with `IncludeFields = true`).
- `Main.unity` and the prefabs are the source of truth; no scene generator; no view built or laid out from code.
- Renaming a serialized field needs `[FormerlySerializedAs]` (UL301). Unity does not serialize `Action`, tuples, dictionaries, queues or non-`[Serializable]` classes.
- Every player-visible string is a `Lang` key present in both `lang/en.json` and `lang/vi.json`.
- Lint baseline stays empty (`tools/lint/baseline.json` `entries: []`). A check is never weakened to get green.
- `MIN_TESTS` 250 for Core; EditMode ends every task at 0 failed and 0 skipped.
- Release build (Build/ + `index.html`) ≤ **11,824,672 B**.
- **Nothing changes for the player:** the six `tools/CoreReports` outputs are byte-identical to the baseline; every golden shot differs by 0 % (`ViewCapture.Differ`); `webtest.mjs`, `autoplay.mjs`, `descents.mjs` pass.
- Agents never commit. A task ends at a **checkpoint**: report, then a suggested Conventional Commit message with `Story: P7-<n>` in the body; the owner commits.
- One owner per file per wave. Unity batch commands need the Editor closed.
- After C# or content changes: `python3 tools/vault/build_vault.py`, then no diff.

## Review Focus

- **Play, Stop, Play in the Editor (domain reload off):** the second session behaves like the first: no doubled click handlers, no stale camera or material. Pinned by `StaticResetTests` (Task 8) plus the manual check in Task 8.
- **An event note with n = 0 or a negative n** renders exactly as before (`0 trials`, `-2 trials`, `Reaction Index -1 this depth`). Pinned by `NoteText_*` cases (Task 6).
- **HUD with 5 or 6 catalysts** (the overlapping pitch `105 / (held - 1)`) and 3 aspects with depth notes, in Vietnamese (row pitch 12). Pinned by the `bench.crowded` golden shot (Task 1), re-checked in Task 13.
- **Tutorial highlights** still land on the Redraw button and the Journal page rectangles after those rectangles stop being statics. Pinned by `TutorialTargets_RedrawAndJournalSteps_MatchTheViews` (Task 13).
- **Descent banner accent per depth** equals the old `GameFlow.Accent` values. Pinned by `DepthMoodPresets_MainScene_MatchTheOldGameFlowLooks` (Task 11).

## Where this plan differs from the spec

1. **No `Pacing` asset.** After the split, every timing has one owner: `IgniteSequence`, GameFlow's `RunTiming`, and each cutscene. By the spec's own rule (§6.1, one owner → serialized field on the owner), those timings are fields. A `Pacing` asset is added only if a timing gets a second owner.
2. **`PointerInput.Rig` stays a static** of the allowlisted `PointerInput`, set through `PointerInput.Use(rig)` and cleared by its reset. `PointerInput.Resolve` and the click dispatch are static and are tested that way; making only the rig an instance would split one service in two.
3. **UL009–UL012 start as warnings** in Task 2 and become errors in the task that clears their last finding (spec §6.9). Warnings do not fail CI, so the baseline stays empty and the remaining count is visible all along.

## File map

| Path | Created / changed in | Responsibility |
|---|---|---|
| `Athanor/Assets/Editor/Shared/GoldenShots.cs` | 1 | the list of golden shots, render, write menu |
| `Athanor/Assets/Editor/Shared/Reserialize.cs` | 1 | force-reserialize view prefabs and `Main.unity` |
| `Athanor/Assets/Tests/EditModeGame/GoldenShotTests.cs` | 1 | every shot vs its PNG, both languages |
| `production/qa/evidence/phase7/baseline/` | 1 | report outputs, web results, golden PNGs (`golden/<shot>.<lang>.png`) |
| `tools/lint/unity_lint.py`, `tools/lint/tests/test_unity_lint.py`, `tools/lint/lint.config.json` | 2, then severity flips in 5, 8, 16 | UL009–UL014 |
| `Athanor/Assets/Scripts/Core/ContentData.cs` | 3, 6 | `RulesData`, `ContentData.rules` |
| `Athanor/Assets/Scripts/Core/ContentDb.cs` | 3, 4, 5 | `Rules`, `FinalDepth`, `BasePotency(int)`, validation |
| `Athanor/Assets/Resources/content.json` | 3 | `rules` block (content-designer) |
| `Athanor/Assets/Scripts/Core/RunState.cs`, `RunStart.cs`, `Ignite.cs`, `ScoreParts.cs`, `Profile.cs` | 4, 5, 6 | read `db.Rules`; `RunOptions.PouchMode`; `EventNote` |
| `Athanor/Assets/Tests/EditMode/RulesTests.cs` | 3, 4, 5 | rules block and its use |
| `Athanor/Assets/Scripts/Core/ScreenFlow.cs`, `Tests/EditMode/ScreenFlowTests.cs` | 9 | screen state machine |
| `Athanor/Assets/Scripts/Game/Fx/FxRefs.cs`, `Tests/EditModeGame/TestFx.cs` | 7 | replaces `ScreenFx.I`, `Sparks.I` |
| `Athanor/Assets/Tests/EditModeGame/StaticResetTests.cs` | 8 | allowlisted statics reset |
| `Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs` | 11 | the Ignite choreography |
| `Athanor/Assets/Scripts/Game/Fx/BurstSpec.cs` | 11 (reused in 15) | one serialized spark burst |
| `Athanor/Assets/Scripts/Game/Support/Palette.cs`, `Athanor/Assets/Data/Palette.asset` | 12 | shared colours |
| `Athanor/Assets/Tests/EditModeGame/LayoutPinTests.cs` | 13–16 | serialized values equal today's numbers |
| `docs/ATHANOR.md`, `vault/Data/Data Design.md` | 1, 2, 18 | decisions, rules, CI rows, figures |

## How a presentation number moves (used by Tasks 11–16)

Do this per file. The lint command lists what is left in one file (from the repo root):
`python3 tools/lint/unity_lint.py --root Athanor --config tools/lint/lint.config.json Assets/Scripts/Game/<path>.cs`

1. Classify each UL009 / UL010 finding, and each timing literal (`Anim.Wait`, `Motion.*`, tween durations, which lint does not see), by spec §6.1:
   - a **fixed position** goes into the prefab Transform, and the code line is deleted;
   - a **layout parameter used in a formula** becomes a field of a `[Serializable] struct <Type>Layout` on the component;
   - a **colour** becomes a field on the view, or `Palette` if two or more files use the same colour for the same role;
   - a **timing** becomes a field of a `[Serializable] struct <Type>Timing`.
   Keep `0`, `1`, `/ 2f` arithmetic, array sizes (shape) and dev-hook strings.
2. Add the fields with today's values as initializers; the code reads the fields.
3. Run `Reserialize.All` (Task 1) so the prefab or scene stores the values. For a fixed position, set the child's local position in the prefab to the position the deleted code produced.
4. Run `GoldenShotTests` and the file's existing tests: 0 % difference, all green.
5. Add a `LayoutPinTests` case per struct that reads the serialized values (`new SerializedObject(component).FindProperty("<struct>.<field>")`) and asserts today's literal.
6. Delete the initializers. Re-run step 4 and the pin test.
7. Re-run the lint command on the file: no UL009 / UL010 left.

---

### Task 1: Baseline, golden shots, decisions (P7-1)

**Files:**
- Create: `Athanor/Assets/Editor/Shared/GoldenShots.cs`, `Athanor/Assets/Editor/Shared/Reserialize.cs`, `Athanor/Assets/Tests/EditModeGame/GoldenShotTests.cs`
- Create: `production/qa/evidence/phase7/baseline/` (reports, web results, `golden/*.png`)
- Modify: `docs/ATHANOR.md` §4

**Interfaces:**
- Produces:
  - `public static class GoldenShots { public static IEnumerable<string> Names(); public static Texture2D Render(string name); public static string PathOf(string name, string lang); [MenuItem("Athanor/Write Golden Shots")] public static void WriteAll(); }`. `PathOf` returns `<repo>/production/qa/evidence/phase7/baseline/golden/<name>.<lang>.png`.
  - `public static class Reserialize { [MenuItem("Athanor/Reserialize Views and Scene")] public static void All(); }`. It calls `AssetDatabase.ForceReserializeAssets` on every prefab under `Assets/Prefabs` plus `Assets/Scenes/Main.unity` and `Assets/Data/*.asset`.

- [ ] **Step 1: Branch.** `git checkout -b feature/phase7-unity-conversion` from the current `feature/phase6-unity-refactor` head.
- [ ] **Step 2: Record the reports.** For each of `balance-matrix events roles newcomer balance trace`, run `DOTNET_ROLL_FORWARD=Major dotnet run --project tools/CoreReports -- <name> > production/qa/evidence/phase7/baseline/<name>.txt`. Expected: six non-empty files.
- [ ] **Step 3: Write `GoldenShots`.** It covers these shots, each built with `PrefabFixture` the way the named existing test builds it, using seeded data only:

  | Shot | Prefab and state | Built like |
  |---|---|---|
  | `title` / `title.settings` / `title.askskip` | Title: main buttons / Settings panel open / skip question | `TitleView` tests in `PrefabBakeTests` |
  | `bench.tutorial` | Bench + Hud, `RunState(db, new Journal(), TutorialScript.Seed, 1, TutorialScript.Options(Unlocks.Fresh(db)))`, `Deal(TutorialScript.Reaction.Hand)`, input off | `HudTests` |
  | `bench.crowded` | Bench + Hud with 6 catalysts, 3 aspects and 3 depth notes, input off | `HudTests` |
  | `journal.fresh` / `journal.known` | Journal with an empty journal / with 6 known pairs and 2 hints | `Bind_JournalPrefab_MakesOneCellPerMaterial` |
  | `goals.fresh` | Goals with `new Profile(1)` | `Open_GoalsPrefabFreshProfile_WritesEveryGoal` |
  | `choice.three` | CatalystChoice, three cards including a Rare | `Show_CatalystChoicePrefabRareCard_DrawsThreeStars` |
  | `event.options` / `event.outcome` | Event before and after the last option | `Click_EventPrefabLastOptionThenContinue_ShowsTheOutcomeAndCloses` |
  | `runover.won` / `runover.lost` | RunOver | `ShowDepth_RunOverPrefab_WritesTheDepthRow` |
  | `pause.menu` / `pause.quit` | Pause | `Open_PausePrefab_ShowsTheMenu` |
  | `toast.one` | GoalToast with one goal | `Add_GoalToastPrefab_ShowsTheGoalName` |

  A shot must not depend on `Time.time` or `Random`: put the bench in states where Ignite is off (no pulse), and call each view's `Show` / `Open` path, not `Update`.
- [ ] **Step 4: Write `GoldenShotTests`.**

  ```csharp
  [TestCaseSource(typeof(GoldenShots), nameof(GoldenShots.Names))]
  public void Render_Shot_MatchesTheGoldenImage(string name)
  {
      foreach (var lang in new[] { Loc.English, Loc.Vietnamese })
      {
          Loc.Set(lang);
          var golden = ViewCapture.Load(GoldenShots.PathOf(name, lang));
          Assert.IsNotNull(golden, "no golden image for " + name + "." + lang);
          Assert.AreEqual(0f, ViewCapture.Differ(GoldenShots.Render(name), golden), name + "." + lang);
      }
      Loc.Set(Loc.English);
  }
  ```
- [ ] **Step 5: Run it to see it fail.** `unity test Athanor --mode EditMode` (Editor closed). Expected: the `GoldenShotTests` cases fail with "no golden image for …".
- [ ] **Step 6: Write the goldens.** Run `Athanor > Write Golden Shots` in the Editor (or `unity build Athanor --target WebGL --execute-method GoldenShots.WriteAll --allow-dirty-build`). Expected: `2 ×` the shot count PNGs, 640x360.
- [ ] **Step 7: Run twice.** Run EditMode twice. Expected both times: 0 failed, 0 skipped. This shows the shots are deterministic.
- [ ] **Step 8: Web baseline.** Run the dev build plus `webtest.mjs`, `autoplay.mjs` and `descents.mjs` (commands in ATHANOR.md §6), with output in `production/qa/evidence/phase7/baseline/web/`. Expected: exit 0 for each.
- [ ] **Step 9: Write `Reserialize`.** Check it: run it once on the untouched project, then `git status`. The only expected changes are Unity reformatting. If it changes values, stop and report.
- [ ] **Step 10: Decisions.** Append to ATHANOR.md §4 *Process and architecture*:
  - `2026-11-19: Phase 7 (Unity conversion, spec 2026-11-19): rule numbers live in content.json "rules"; presentation numbers in the prefab or scene that owns them, shared colours in the Palette asset; GameFlow splits into Core ScreenFlow, the GameFlow bridge and IgniteSequence; Game statics only from the lint allowlist, each reset at SubsystemRegistration; Core reports note keys, not sentences. Nothing visible or scored changes (golden shots, byte-identical reports).`
  - `2026-11-19: resolution stays 640x360 (a 1920x1080 canvas needs every sprite redrawn; dropped); PC / Steam later, platform code only in bridge files.`
- [ ] **Step 11: Checkpoint.** Run the vault build, `tools/ci/run.sh all` and EditMode. Suggested message: `test(golden): phase 7 baseline shots and reports` with `Story: P7-1`.

### Task 2: Lint rules UL009–UL014 (P7-2)

**Files:**
- Modify: `tools/lint/unity_lint.py` (rule table near line 36, dispatch in `lint_source` near line 573, `DEFAULT_CONFIG` near line 75)
- Modify: `tools/lint/tests/test_unity_lint.py`, `tools/lint/lint.config.json`, `docs/ATHANOR.md` §5

**Interfaces:**
- Produces: config keys `presentation_paths` (default `["Assets/Scripts/Game"]`), `static_allowlist` (list of class names, empty by default) and `platform_define_allowlist` (list of paths). Rules:

  | Rule | Scope | Flags |
  |---|---|---|
  | UL009 | `presentation_paths` | `Px.Hex(` with a string literal, or `new Color(` / `new Color32(` with any number literal argument |
  | UL010 | `presentation_paths` | `Px.At(`, `Px.Mid(`, `new Rect(`, `new Vector2(`, `new Vector3(` with a number literal argument other than `0`, `1`, `0f`, `1f` |
  | UL011 | `pure_logic_paths` | a `static` field that is not `readonly` or `const`, a `static event`, or a static auto-property with a setter |
  | UL012 | `presentation_paths` | the UL011 shapes plus `static readonly` `List`/`Dictionary`/`HashSet`/`Queue`/`Stack` fields, in a class not in `static_allowlist`; also an allowlisted class whose file has no `RuntimeInitializeLoadType.SubsystemRegistration` |
  | UL013 | `Assets/Scripts` | `Task.Run`, `new Thread(`, `.Result` on a Task-typed call, `.Wait()`, `System.Threading.Timer` |
  | UL014 | `Assets/Scripts` | `#if` / `#elif` naming `UNITY_WEBGL`, `UNITY_STANDALONE*`, `UNITY_ANDROID`, `UNITY_IOS`, outside `platform_define_allowlist` |

  All rules run on `mask_source` output, so strings and comments never match. `Editor/` and test paths are outside these scopes.

- [ ] **Step 1: Write the failing tests.** In `test_unity_lint.py`, add one positive and one negative test per rule in the existing style (`self.lint(src, rel=..., **over)`, `rules_of(...)`). Names:
  - `test_ul009_hex_literal_in_game`
  - `test_ul009_hex_in_editor_ok`
  - `test_ul010_px_at_literal`
  - `test_ul010_vector_zero_one_ok`
  - `test_ul011_mutable_static_in_core`
  - `test_ul011_static_readonly_array_ok`
  - `test_ul012_static_list_not_allowlisted`
  - `test_ul012_allowlisted_without_reset`
  - `test_ul012_allowlisted_with_reset_ok`
  - `test_ul013_task_run`
  - `test_ul013_in_comment_ok`
  - `test_ul014_webgl_define_outside_allowlist`
  - `test_ul014_in_allowlist_ok`

  Example:

  ```python
  def test_ul010_px_at_literal(self):
      v = self.lint("class A { void F() { var p = Px.At(575, 306); } }", rel="Assets/Scripts/Game/A.cs",
                    presentation_paths=["Assets/Scripts/Game"])
      self.assertIn("UL010", rules_of(v))
  ```
- [ ] **Step 2: Run them to see them fail.** `python3 -m unittest discover -s tools/lint/tests -q`. Expected: the 13 new tests FAIL.
- [ ] **Step 3: Implement.** Add the six `check_*` functions and register them in `lint_source`, plus rule table entries with Vietnamese messages like the existing ones.
- [ ] **Step 4: Run them to see them pass.** Run the same command. Expected: OK.
- [ ] **Step 5: Configure.** In `lint.config.json`:
  - `"severity": {"UL009": "warning", "UL010": "warning", "UL011": "warning", "UL012": "warning"}`
  - `"platform_define_allowlist": ["Assets/Scripts/Game/Support/Haptics.cs", "Assets/Scripts/Game/Support/AudioMute.cs", "Assets/Scripts/Game/Boot/GameFlow.cs", "Assets/Scripts/Game/Boot/Save.cs", "Assets/Scripts/Game/Support/Dev.cs"]`

  UL013 and UL014 stay errors from now on.
- [ ] **Step 6: Run CI.** `tools/ci/run.sh lint`. Expected: exit 0. The warnings print; save their count per rule in the checkpoint report as the starting figure.
- [ ] **Step 7: Document.** Add six rows to the ATHANOR.md §5 table. Each row's "Caught by" names the rule and adds "(warning until Phase 7 Task N)" for UL009–UL012.
- [ ] **Step 8: Checkpoint.** Suggested message: `ci(lint): rules for hard-coded numbers, statics, threads and platform defines`, `Story: P7-2`.

### Task 3: The `rules` block (P7-3)

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/ContentData.cs`, `Athanor/Assets/Scripts/Core/ContentDb.cs`
- Modify: `Athanor/Assets/Resources/content.json` (owner: content-designer)
- Create: `Athanor/Assets/Tests/EditMode/RulesTests.cs`

**Interfaces:**
- Produces:
  - `[Serializable] public class RulesData { public int handMin, handBase, trials, trialsFinalDepth, redraws, starterCopies, offerSize, rackSize, pouchCards, firstReactionMult; public int[] aspectDepths; public long[] potencyByTier; }`
  - `ContentData.rules` (type `RulesData`)
  - `ContentDb.Rules` (`RulesData`)
  - `ContentDb.FinalDepth` (`int`, equals `depthNames.Length`)

- [ ] **Step 1: Write the failing tests** in `RulesTests.cs` (namespace `Athanor.Tests`):

  ```csharp
  [Test] public void Rules_ShippedContent_HasTodaysValues()
  {
      var r = new ContentDb(TestContent.Load()).Rules;
      Assert.AreEqual(3, r.handMin); Assert.AreEqual(5, r.handBase); Assert.AreEqual(4, r.trials); Assert.AreEqual(6, r.trialsFinalDepth);
      Assert.AreEqual(2, r.redraws); Assert.AreEqual(2, r.starterCopies); Assert.AreEqual(3, r.offerSize); Assert.AreEqual(12, r.rackSize);
      Assert.AreEqual(1, r.pouchCards); Assert.AreEqual(2, r.firstReactionMult);
      CollectionAssert.AreEqual(new[] { 1, 3, 5 }, r.aspectDepths);
      CollectionAssert.AreEqual(new long[] { 0, 20, 60, 180, 500, 1500 }, r.potencyByTier);
  }
  [Test] public void FinalDepth_ShippedContent_IsFive() => Assert.AreEqual(5, new ContentDb(TestContent.Load()).FinalDepth);
  ```

  Add the throwing cases on a loaded `ContentData` with one field changed. Each asserts `Assert.Throws<ArgumentException>(() => new ContentDb(data))`:
  - `Constructor_NoRulesBlock_Throws` (`rules = null`)
  - `Constructor_PotencyTableShorterThanTiers_Throws` (the table needs an entry for the highest material tier)
  - `Constructor_AspectDepthOutsideTheRun_Throws` (`aspectDepths = {6}`)
  - `Constructor_HandBaseBelowHandMin_Throws`
  - `Constructor_DepthTargetsNotOneFewerThanDepths_Throws`
- [ ] **Step 2: Run them to see them fail.** `DOTNET_ROLL_FORWARD=Major dotnet test tools/CoreTests --filter RulesTests`. Expected: FAIL (`RulesData` not defined).
- [ ] **Step 3: Implement.** Add `RulesData`, `ContentData.rules`, `ContentDb.Rules`, `ContentDb.FinalDepth` and the validation in the `ContentDb` constructor. The messages follow the existing "… must be …" style.
- [ ] **Step 4: Add the block.** Content-designer adds the `rules` object to `content.json` with exactly the values in Step 1.
- [ ] **Step 5: Run every test.** Run all `dotnet test tools/CoreTests` and EditMode. Expected: 0 failed; the Core count is above `MIN_TESTS`.
- [ ] **Step 6: Document.** Content-designer adds a `rules` table (field, meaning, today's value) to `vault/Data/Data Design.md`, and a row to the ATHANOR.md §3 key table.
- [ ] **Step 7: Checkpoint.** Suggested message: `feat(content): rules block in content.json`, `Story: P7-3`.

### Task 4: Core reads `db.Rules` (P7-4)

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/RunState.cs:22-24,51,66,74,104-109`, `RunStart.cs`, `Ignite.cs:127-128`, `ScoreParts.cs:19-21`, `ContentDb.cs:230-231`
- Modify the callers of the removed constants (`grep -rn 'RunState\.\(MinHand\|BaseHand\|BaseTrials\|BaseRedraws\|FinalDepth\|FinalTrials\|StarterCopies\|OfferSize\|RackSize\|PouchCards\)\|ContentDb\.BasePotency'`):
  - `Scripts/Game/Bench/BenchView.cs`, `Ui/CatalystChoiceView.cs`, `Ui/RunOverView.cs`, `Cutscene/ReactionCutscene.cs:74`
  - `Tests/EditMode/*` (Phase3Tests, RunStateTests, RarityTests, ContentTests, Sim), `Tests/EditModeGame/HudTests.cs`, `RunOverDepthTests.cs`
  - `tools/CoreReports/Program.cs`
- Test: `Athanor/Assets/Tests/EditMode/RulesTests.cs`

**Interfaces:**
- Consumes: `ContentDb.Rules`, `ContentDb.FinalDepth` (Task 3).
- Produces: `public long ContentDb.BasePotency(int tier)` (now an instance method). The constants listed above no longer exist; callers use `db.Rules.<field>` / `db.FinalDepth`.

- [ ] **Step 1: Write the failing tests.** Each loads `TestContent.Load()`, changes one rule, and builds `new ContentDb(data)`:
  - `HandSize_HandBaseSeven_IsSeven`: `new RunState(db, new Journal(), 1).HandSize == 7`
  - `TrialsLeft_TrialsSix_StartsDepthOneWithSix`
  - `BasePotency_TableDoubled_IsDoubled`: `db.BasePotency(2) == 120` with `potencyByTier = {0,40,120,360,1000,3000}`
  - `Phase_AspectDepthsTwo_OffersAspectsAtDepthTwoOnly`: with `RunOptions.Full(Unlocks.All(db))`, depth 1 is not `AspectOffer`
- [ ] **Step 2: Run them to see them fail.** Expected: FAIL. The constants still decide.
- [ ] **Step 3: Implement.** Replace every listed constant and literal with its `db.Rules` / `db.FinalDepth` read. `BasePotency` reads `Rules.potencyByTier`, clamped as today. `Score` multiplies by `db.Rules.firstReactionMult` where `ScoreParts.First` and `Ignite.cs:128` had `2`.
- [ ] **Step 4: Run every test.** Run all Core tests and EditMode. Expected: 0 failed.
- [ ] **Step 5: Reports identical.** For each report, run `dotnet run --project tools/CoreReports -- <name> | diff - production/qa/evidence/phase7/baseline/<name>.txt`. Expected: no output for all six.
- [ ] **Step 6: Classify the leftovers** (spec §6.2): `BenchRules.MaxRi = 13`, `Chain.SputterSize = 3`, the `>= 2` thresholds at `RunStart.cs:165,351`, the `0x…` seed salts, `TutorialScript.Seed`, `QuietBefore`, the reward hash `7919` in `Profile.cs:108`. Put a table (number, tuning or shape, why) in the checkpoint report; content-designer checks it.
  - Each one marked **tuning** becomes a new `RulesData` field, added to `content.json` with today's value, asserted in `Rules_ShippedContent_HasTodaysValues`, and read by the code.
  - Each one marked **shape** becomes a named `const`.

  Re-run Step 5.
- [ ] **Step 7: Checkpoint.** Run golden, vault and CI. Suggested message: `refactor(core): rule numbers come from content.json`, `Story: P7-4`.

### Task 5: `PouchMode` into `RunOptions`, required amounts (P7-5)

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/RunState.cs:48-50,79-84`, `Profile.cs` (`RunOptions`), `RunStart.cs:65,67,368-369`, `ContentDb.cs`
- Modify: `tools/CoreReports/Program.cs:35-36`, `Athanor/Assets/Tests/EditMode/Sim.cs`
- Modify: `tools/lint/lint.config.json` (UL011 → error)
- Test: `RulesTests.cs`

**Interfaces:**
- Produces: `public int RunOptions.PouchMode = 2;` (0 and 1 kept for the balance report). `RunState.PouchMode` no longer exists.

- [ ] **Step 1: Write the failing tests.**
  - `Constructor_HeatedAspectWithoutAmount_Throws`
  - `Constructor_SeedAspectWithoutAmount_Throws`
  - `Constructor_HeatTileEffectWithoutAmount_Throws` (each sets that `amount` to 0 on loaded content)
  - `Satchel_PouchModeZero_PutsWonStartersInTheSatchel` (a RunOptions with `PouchMode = 0` and unlocked locked starters: the satchel is larger than with mode 2)
- [ ] **Step 2: Run them to see them fail.**
- [ ] **Step 3: Implement.** Validation in `ContentDb` for `Heated` / `Seed` aspects and the `HeatTile` op (amount > 0). Delete the `: 3` / `: 40` fallbacks. `RunState` reads `options.PouchMode`. `CoreReports` sets `PouchMode` on every `RunOptions` it builds, from `ATHANOR_POUCH`, and still prints `MATRIX pouch mode <n>`.
- [ ] **Step 4: Run every test, then diff the reports.** Expected: 0 failed; no diff. Also run `ATHANOR_POUCH=0 dotnet run --project tools/CoreReports -- balance-matrix | head -1`. Expected: `MATRIX pouch mode 0`.
- [ ] **Step 5: UL011 to error.** Set UL011 severity to `"error"`, then run `tools/ci/run.sh lint`. Expected: exit 0, no UL011 lines.
- [ ] **Step 6: Checkpoint.** Suggested message: `refactor(core): no mutable statics in Core; aspect amounts required`, `Story: P7-5`.

### Task 6: Core notes as keys (P7-6)

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/RunStart.cs:16-20` (`EventResult`) and every `result.Notes.Add` at `RunStart.cs:281-388`
- Modify: `Athanor/Assets/Scripts/Game/Support/Wording.cs:154-184`, `Athanor/Assets/Scripts/Game/Ui/EventView.cs:99`
- Modify: `Athanor/Assets/Resources/lang/en.json` (`note.lost`, see Step 4)
- Create: `Athanor/Assets/Tests/EditModeGame/NoteTextTests.cs`

**Interfaces:**
- Produces:
  - `[Serializable] public sealed class EventNote { public string key; public int n; public string item, other, kind; public bool plural; }`
  - `EventResult.Notes` (`List<EventNote>`)
  - `public static string Wording.NoteText(EventNote note)`
- Key mapping:

  | Note | key | fields |
  |---|---|---|
  | trials / hand / redraws | `note.trials`, `note.hand`, `note.redraws` | `n`, `plural = true` |
  | Reaction Index shift | `note.ri_shift` | `n` |
  | heat tile | `note.heat_tile` | `n` |
  | found / gave up / lost / received | `note.found`, `note.gave_up`, `note.lost`, `note.received` | `item` = material or catalyst name |
  | hint | `note.hint` | `item`, `kind` = `Kind.ToString()` |
  | upgrade | `note.became` | `item` = old name, `other` = new name |
  | first triple / tile shut | `note.first_triple`, `note.block_tile` | none |

- [ ] **Step 1: Capture today's text.** Before changing anything, write `NoteTextTests` with `[TestCase]`s whose expected values are today's output:
  - English: the Core string for each row, including `n = 1`, `n = -2` and `n = 0` for the plural notes and `ri_shift`.
  - Vietnamese: `Wording.Note(<that English string>)` under `Loc.Set(Loc.Vietnamese)`.

  To get the literals, print them once with the old code. Use item `"Iron Ore"`, other `"Copper"` and kind `"Metal"`. Test names: `NoteText_<Key>_<N>_MatchesTheOldWording(string lang, ...)`. Assert `Assert.AreEqual(expected, Wording.NoteText(note))`.
- [ ] **Step 2: Run them to see them fail.** Expected: FAIL (`EventNote` and `NoteText` not defined).
- [ ] **Step 3: Implement.**
  - `RunStart` adds `EventNote`s.
  - `NoteText` uses `Loc.P(key, n)` for plurals, otherwise `Loc.T(key, ("n", n), ("item", item), ("a", item), ("b", other), ("kind", Loc.Vi ? Loc.Indef(kind) : kind))`.
  - `EventView` calls `NoteText`.
  - Delete `NoteForms`, `Wording.Note` and `using System.Text.RegularExpressions`.
- [ ] **Step 4: `note.lost` (the one deliberate text change in the phase; owner decided 2026-11-19).** The old Core string was `Lost a {name} for this run` ("a" even before a vowel: "Lost a Iron Ore"). `en.json` already has `Lost {item} for this run`, so keep `en.json` as it is and expect `Lost Iron Ore for this run` in the English `NoteText_Lost_*` case (change that one expected value from Step 1's capture). The `vi` entry and every other note are unchanged. Mention this change in the checkpoint report and in the CHANGELOG draft.
- [ ] **Step 5: Run them to see them pass,** plus all Core tests (the Phase3Tests note counts are unchanged) and EditMode. `grep -rn 'Regex' Athanor/Assets/Scripts` is empty.
- [ ] **Step 6: Check the languages.** Run `tools/ci/run.sh repo` (the LANG check). Localizer-vi confirms no key is missing.
- [ ] **Step 7: Checkpoint.** Suggested message: `refactor(core): event notes are lang keys, not English sentences`, `Story: P7-6`.

### Task 7: `ScreenFx.I` and `Sparks.I` out (P7-7)

**Files:**
- Create: `Athanor/Assets/Scripts/Game/Fx/FxRefs.cs`, `Athanor/Assets/Tests/EditModeGame/TestFx.cs`
- Modify: `Fx/ScreenFx.cs:9,22`, `Fx/Sparks.cs:15,38`, `Boot/GameFlow.cs` (22 uses), `Cutscene/ReactionCutscene.cs` (10), `Cutscene/SimpleCutscene.cs` (9), `Bench/TileView.cs`, `Bench/BenchView.cs`, `Ui/CatalystChoiceView.cs`, `Ui/EventView.cs`, `Ui/GoalToast.cs`, `Ui/TitleView.cs`
- Modify: `Athanor/Assets/Scenes/Main.unity` (GameFlow's new `sparks` field), and the Game tests that call the changed `Bind`s

**Interfaces:**
- Produces:
  - `public sealed class FxRefs { public readonly ScreenFx Screen; public readonly Sparks Sparks; public FxRefs(ScreenFx screen, Sparks sparks); }` (the constructor throws `ArgumentNullException` on a null argument)
  - New Bind signatures (each throws `ArgumentNullException` when `fx` is null):
    - `BenchView.Bind(ContentDb db, FxRefs fx)`
    - `TileView.Bind(FxRefs fx)` (called by BenchView)
    - `ReactionCutscene.Bind(GameObject[] hide, FxRefs fx)`
    - `SimpleCutscene.Bind(FxRefs fx)`
    - `CatalystChoiceView.Bind(FxRefs fx)`
    - `EventView.Bind(FxRefs fx)`
    - `GoalToast.Bind(FxRefs fx)`
    - `TitleView.Bind(Settings settings, FxRefs fx)`
  - `public static FxRefs TestFx.Make(Transform parent)`: a ScreenFx whose `flash` and `black` are set to new SpriteRenderers through `SerializedObject`, plus a Sparks.

- [ ] **Step 1: Write the failing test.** In `PrefabBakeTests`, add `Bind_EventPrefabWithoutFx_Throws`: `Assert.Throws<ArgumentNullException>(() => view.Bind(null))`.
- [ ] **Step 2: Run it to see it fail** (there is no such overload yet).
- [ ] **Step 3: Implement.**
  - GameFlow gets `[SerializeField] Sparks sparks;`, which `Awake` checks like the other views, and builds `fx = new FxRefs(screenFx, sparks)` before any `Bind`.
  - Every `ScreenFx.I` / `Sparks.I` becomes the bound reference.
  - Delete both `I` properties.
  - Wire `sparks` in `Main.unity` to the existing Sparks object.
  - Update the tests to pass `TestFx.Make(...)`.
- [ ] **Step 4: Verify.** `grep -rn 'ScreenFx\.I\b\|Sparks\.I\b' Athanor/Assets` is empty. EditMode (golden included) has 0 failed. On the dev build, `webtest.mjs` and `descents.mjs` exit 0.
- [ ] **Step 5: Checkpoint.** Suggested message: `refactor(fx): pass ScreenFx and Sparks through Bind`, `Story: P7-7`. In ATHANOR.md §7, carry-over 7 is closed.

### Task 8: Allowlisted statics with resets (P7-8)

**Files:**
- Modify: `Game/Support/Loc.cs`, `Support/PointerInput.cs`, `Support/Art.cs`, `Support/Haptics.cs`, `Support/AudioMute.cs`, `Fx/SparkMesh.cs`, `Depth/TiltShift.cs`, `Support/RarityStars.cs`, `Boot/GameFlow.cs:185,233`
- Modify: `Athanor/Assets/Tests/EditModeGame/MainDepthTests.cs` (3 uses of `PointerInput.Rig =`)
- Create: `Athanor/Assets/Tests/EditModeGame/StaticResetTests.cs`
- Modify: `tools/lint/lint.config.json` (`static_allowlist`, UL012 → error), `docs/ATHANOR.md` §2

**Interfaces:**
- Produces:
  - In each of the eight classes, `[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)] public static void ResetStatics()`. It puts every static back to its declared default and drops every event handler. `SparkMesh` and `RarityStars` destroy the objects they created (`Object.DestroyImmediate` when not null).
  - `public static void PointerInput.Use(DepthRig rig)`, with `PointerInput.Rig` becoming `{ get; private set; }`.
  - Allowlist: `Loc, PointerInput, Art, Haptics, AudioMute, SparkMesh, TiltShift, RarityStars`.

- [ ] **Step 1: Write the failing tests** in `StaticResetTests`. The `[TearDown]` calls `EditorGlobals.Inject()` so later tests keep their globals.
  - `ResetStatics_Loc_DropsChangedHandlersAndGoesBackToEnglish`: subscribe, `Loc.Set(Loc.Vietnamese)`, reset; `Loc.Code` is `"en"` after a re-`Use`, and the handler is not called on the next `Set`.
  - `ResetStatics_PointerInput_ClearsRigHoverAndAnyPress`
  - `ResetStatics_Haptics_RestoresEnabledAndClearsSink` (`Enabled == true`, `Sink == null`, `ForceSupported == null`)
  - `ResetStatics_AudioMute_RestoresUnmuted`
  - `ResetStatics_SparkMesh_EmptiesTheMaterialCache`
  - `ResetStatics_TiltShift_ForgetsCameras`
  - `ResetStatics_RarityStars_DropsTheCachedGlyph`
  - `ResetStatics_Art_ClearsTheLibrary`
- [ ] **Step 2: Run them to see them fail.**
- [ ] **Step 3: Implement** the resets, `PointerInput.Use`, and the GameFlow and test call sites.
- [ ] **Step 4: Clear UL012.** Set `static_allowlist`. Run lint and fix every remaining UL012 warning in `Scripts/Game`: remove the static, or turn a constant list into an array. Adding a class to the allowlist needs a line in ATHANOR.md §2. Then set UL012 to `"error"`.
- [ ] **Step 5: Stop what each component starts** (spec §6.5). For every component that calls `StartCoroutine` or starts a PrimeTween tween on its own objects (`grep -rln 'StartCoroutine\|Tween\.\|Sequence\.Create\|Motion\.' Athanor/Assets/Scripts/Game`), its `OnDisable` (or `OnDestroy` when it must outlive a disable) stops them with `StopAllCoroutines()` and `Tween.StopAll(onTarget: <its object>)`. Skip components already doing this. List the components changed in the report.
- [ ] **Step 6: Run every test.** EditMode, golden and CI: green.
- [ ] **Step 7: Manual check** (Editor, domain reload off): Play → Title → Bench → Journal → Title → Stop → Play → the same path. Expected: no console error, and one click opens the Journal exactly once (not twice). Record it in the checkpoint report.
- [ ] **Step 8: Document.** In ATHANOR.md §2, add an "Allowed statics" paragraph that lists the eight and the reset rule.
- [ ] **Step 9: Checkpoint.** Suggested message: `fix(game): reset statics at play start (domain reload off)`, `Story: P7-8`.

### Task 9: Core `ScreenFlow` (P7-9)

**Files:**
- Create: `Athanor/Assets/Scripts/Core/ScreenFlow.cs`, `Athanor/Assets/Tests/EditMode/ScreenFlowTests.cs`

**Interfaces:**
- Produces (namespace `Athanor.Core`):

  ```csharp
  public enum FlowState { Boot, Title, Intro, Bench, Resolving, Journal, Descent, CatalystChoice, Ending, RunOver, AspectChoice, Event, Goals }
  public sealed class ScreenFlow
  {
      public FlowState State { get; }                  // Boot at construction
      public FlowState JournalFrom { get; }            // where the open Journal returns to
      public bool InCutscene { get; set; }
      public bool Sandbox { get; }
      public event Action<FlowState, FlowState> Changed;   // (from, to); not raised when the state does not change
      public void Enter(FlowState state);
      public bool OpenJournal(bool titleAsking);       // Bench, or Title when not asking -> Journal
      public bool CloseJournal();                      // Journal -> JournalFrom
      public bool OpenGoals(bool titleAsking);         // Title when not asking, or RunOver -> Goals
      public bool CloseGoals();                        // Goals -> where it was opened
      public bool StartIgnite(bool canIgnite);         // Bench and canIgnite -> Resolving
      public bool CanPause(bool benchInput, bool runPlaying);   // Bench && benchInput && !InCutscene && runPlaying
      public bool CanAbandon(bool runEnded);           // Bench && !runEnded
      public bool CanResetProgress { get; }            // Title
      public void BeginSandbox(); public void EndSandbox();
  }
  ```

- [ ] **Step 1: Write the failing tests** (dotnet). One per rule, `Method_Condition_Result`:
  - `OpenJournal_FromBench_ClosesBackToBench`
  - `OpenJournal_FromTitle_ClosesBackToTitle`
  - `OpenJournal_WhileTitleAsks_IsRefused`
  - `OpenJournal_FromResolving_IsRefused`
  - `CloseJournal_NotInJournal_ChangesNothing`
  - `OpenGoals_FromRunOver_ClosesBackToRunOver`
  - `OpenGoals_FromBench_IsRefused`
  - `StartIgnite_BenchAndCanIgnite_GoesToResolving`
  - `StartIgnite_CannotIgnite_StaysOnBench`
  - `StartIgnite_FromJournal_IsRefused`
  - `CanPause_BenchInputNoCutscenePlaying_IsTrue`
  - `CanPause_InCutscene_IsFalse`
  - `CanPause_InputOff_IsFalse`
  - `CanPause_RunNotPlaying_IsFalse`
  - `CanPause_NotOnBench_IsFalse`
  - `CanAbandon_RunEnded_IsFalse`
  - `CanResetProgress_OnTitle_IsTrue`
  - `Changed_Enter_ReportsFromAndTo`
  - `Changed_EnterSameState_IsNotRaised`
  - `Sandbox_BeginThenEnd_IsFalseAgain`
- [ ] **Step 2: Run them to see them fail.** `dotnet test tools/CoreTests --filter ScreenFlowTests`.
- [ ] **Step 3: Implement** `ScreenFlow`.
- [ ] **Step 4: Run them to see them pass,** plus the full Core suite.
- [ ] **Step 5: Checkpoint.** Suggested message: `feat(core): ScreenFlow, the screen state machine`, `Story: P7-9`.

### Task 10: `GameFlow` on `ScreenFlow` (P7-10)

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs` (remove `FlowState` enum at :12, `State` setter, `journalFrom`, `goalsFrom`, `inCutscene`, `sandbox`, `CanPause`)
- Modify: `Athanor/Assets/Scripts/Game/Ui/PauseMenu.cs:36-37` (delete `Allowed`)
- Modify: `Athanor/Assets/Tests/EditModeGame/PauseTests.cs` (5 uses), and every Game file using `FlowState` (add `using Athanor.Core;`)

**Interfaces:**
- Consumes: `ScreenFlow` (Task 9).
- Produces:
  - `public FlowState GameFlow.State => flow.State` (read-only, for tests and tools)
  - `public ScreenFlow GameFlow.Flow { get; }`

- [ ] **Step 1: Move the tests.** Move the `PauseMenu.Allowed` cases from `PauseTests` into `ScreenFlowTests.CanPause_*`, covering the same inputs. Delete the old ones.
- [ ] **Step 2: Implement.**
  - GameFlow owns `readonly ScreenFlow flow = new ScreenFlow();`.
  - Every `State = X` becomes `flow.Enter(X)`.
  - `OpenJournal`, `CloseJournal`, `OpenGoals`, `CloseGoals`, `Ignite`, `AbandonRun`, `ResetProgress` and `CanPause` ask `flow` and react: `Bench.InputEnabled`, the view's `Open`, saves.
  - `escGuardFrame`, the title loop, saves and the sandbox data swap stay in GameFlow; the swap calls `flow.BeginSandbox()` / `EndSandbox()`.
- [ ] **Step 3: Verify.** Core tests, EditMode (golden included), `webtest.mjs`, `descents.mjs` and `autoplay.mjs`: all pass. Report GameFlow's line count.
- [ ] **Step 4: Checkpoint.** Suggested message: `refactor(game): GameFlow drives screens through ScreenFlow`, `Story: P7-10`.

### Task 11: `IgniteSequence`, depth looks from the scene (P7-11)

**Files:**
- Create: `Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs`
- Modify: `Boot/GameFlow.cs`
  - delete :80-85 (`Ambient`, `Moon`, `Accent`) and :181-182 (`GameObject.Find("Ambient Light")`, Moonbeam)
  - move :532-569 and :585-717 into `IgniteSequence`
  - `Descend` / `EndRun` waits go into a `[Serializable] struct RunTiming`
- Modify: `Depth/DepthStage.cs` (add `Look`), `Depth/DepthMood.cs` (delete `Defaults()` and the `presets` initializer), `Main.unity` (add the IgniteSequence component on the GameFlow object and wire it)
- Test: `Tests/EditModeGame/MainDepthTests.cs`, `LayoutPinTests.cs`

**Interfaces:**
- Consumes: `FxRefs` (Task 7), `ScreenFlow` (Task 9).
- Produces:

  ```csharp
  public sealed class IgniteSequence : MonoBehaviour
  {
      public void Bind(BenchView bench, ReactionCutscene reaction, FxRefs fx, ScreenFlow flow);
      public bool Hurry { get; set; }      // GameFlow sets it on a press while Resolving, as Update does today
      public IEnumerator Play(IgniteOutcome outcome, RunState run, ContentDb db, long potencyBefore, TutorialController tutorial, bool auto);
  }
  public DepthMood.Preset DepthStage.Look(int depth);   // presets[Clamp(depth, 1, presets.Length) - 1]
  ```

  `Play` covers the events loop, rack flights, dropped-item text, the final potency and `tutorial.AfterIgnite`. It sets `flow.InCutscene` around the discovery cutscene. Settling the Ignite and the `PhaseAfter` switch stay in GameFlow. Every literal in the moved code becomes a field of `IgniteSequence.Timing` (pace, waits, durations) or `IgniteSequence.Look` (bursts, offsets, colours), following the recipe above.
  - This task creates `Game/Fx/BurstSpec.cs`: `[Serializable] public struct BurstSpec { public int count; public Color color; public float speed, life; public int order; public float gravity; }`. It is used for every `Sparks.Burst` call moved here, and is reused by Task 15.

- [ ] **Step 1: Pin the depth looks.** Write `DepthMoodPresets_MainScene_MatchTheOldGameFlowLooks` in `MainDepthTests` and run it now. For depth 1..5, the scene's `DepthMood.presets[d-1]` must have:
  - ambient `(0.52,0.52,1)`, `(0.40,0.62,0.86)`, `(0.62,0.42,0.95)`, `(0.86,0.40,0.46)`, `(0.78,0.74,0.58)`
  - moon `0.62, 0.5, 0.4, 0.3, 0.5`
  - accent `#7cc0f8, #6ee6c8, #c486f4, #ee6a3c, #fff6b8` (tolerance 1/255)

  If it fails, stop and report the differing values. The accent is shown from GameFlow today, so the scene must be set to GameFlow's accent.
- [ ] **Step 2: Implement** `IgniteSequence` and `DepthStage.Look`.
  - `GameFlow.Mood` keeps only the `depthStage.ShowMood` and window calls. With no `DepthStage`, it sets nothing.
  - `Descend` reads `depthStage.Look(next).accent`.
  - Delete the arrays, the two light lookups, `DepthMood.Defaults()` and the `presets` initializer. Tests that `AddComponent<DepthMood>()` set `presets` themselves.
- [ ] **Step 3: Reserialize.** Run `Reserialize.All`. Add `LayoutPinTests.IgniteSequence_MainScene_KeepsTodaysTiming` and `…KeepsTodaysLook`, asserting the values that were at `GameFlow.cs:535-717`. Then delete the initializers (recipe steps 5–6).
- [ ] **Step 4: Verify.** EditMode (golden), `webtest.mjs`, `autoplay.mjs` and `descents.mjs` pass. Also play one run in the Editor: a chain, a discovery and a descent look as before. Record this in the report.
- [ ] **Step 5: Checkpoint.** Suggested message: `refactor(game): IgniteSequence; depth looks only in the scene`, `Story: P7-11`.

### Task 12: `Palette` (P7-12)

**Files:**
- Create: `Athanor/Assets/Scripts/Game/Support/Palette.cs`, `Athanor/Assets/Data/Palette.asset` (+ `.meta`, and the `Data` folder `.meta`)
- Modify: `Support/Art.cs` (add `Palette`), `Support/Wording.cs` (22 colours), `Support/RarityStars.cs`, `Boot/GameFlow.cs` (`[SerializeField] Palette palette`, Awake check, `Art.Palette = palette`), `Editor/Shared/EditorGlobals.cs` (load `Assets/Data/Palette.asset`)
- Test: `LayoutPinTests.cs`

**Interfaces:**
- Produces: `[CreateAssetMenu(menuName = "Athanor/Palette")] public sealed class Palette : ScriptableObject` and `public static Palette Art.Palette`. Extend `Art.ResetStatics` to clear it and `ResetStatics_Art_ClearsTheLibrary` to assert it.
  - The fields are named by role. Start with every colour in `Wording` (rarity text, rarity dark and the rest, as `[Serializable] struct RarityColors { public Color common, uncommon, rare, legendary; }` where the code switches on rarity), `defaultGlow` (`#f0dcc0`), and every other hex that appears in two or more files for the same role (from the UL009 list).
  - A colour used by one file stays in that file's own field (Tasks 13–16).

- [ ] **Step 1: Write the failing test.** `Palette_Asset_HasTodaysColours` loads the asset and asserts each field against the hex it replaces, e.g. `AreEqual(Px.Hex("#8ad04a"), p.rarityText.uncommon)` for each field.
- [ ] **Step 2: Run it to see it fail** (the asset does not exist yet).
- [ ] **Step 3: Implement.** Create the asset with those values, switch the users to `Art.Palette.<field>`, and wire the `palette` field in `Main.unity`.
- [ ] **Step 4: Verify.** EditMode with golden passes, and the lint run on `Wording.cs` shows no UL009.
- [ ] **Step 5: Checkpoint.** Suggested message: `refactor(ui): shared colours in the Palette asset`, `Story: P7-12`.

### Task 13: Bench numbers (P7-13, wave 6a)

**Files:**
- Modify: `Game/Bench/HudView.cs`, `BenchView.cs`, `TileView.cs`, `CardView.cs`, `LinkView.cs`, `Dust.cs`, `Prefabs/Views/Bench.prefab`, `Prefabs/Parts/Hud.prefab`, `Prefabs/Parts/Card.prefab`
- Modify: `Game/Cutscene/TutorialController.cs` (reads the HUD rectangle from the instance)
- Test: `LayoutPinTests.cs`, `SceneBakeTests.cs`

**Interfaces:**
- Produces: `public Rect HudView.RedrawRect { get; }` and `public Rect HudView.PauseRect { get; }`, both art-pixel rects from `HudView.Layout`. These replace `HudView.RedrawX/Y/W/H` and `PauseX/Y/W/H`. `MaxCatalysts` and `MaxAspects` stay `const` (array sizes).

- [ ] **Step 1: Pin the tutorial.** Write `TutorialTargets_RedrawAndJournalSteps_MatchTheViews` (SceneBakeTests, on `MainSceneFixture`). It asserts that the Redraw step's lit rectangle equals today's (the `RedrawX/Y/W/H` values plus the `TutorialTargets` margin), and does the same for the Journal step against `JournalView.RecipesRect`. Run it: PASS. It stays green through Tasks 13–14.
- [ ] **Step 2: Apply the recipe** to the six files. HudView specifics:
  - `Row` 10/12 becomes `layout.rowEn` / `rowVi`.
  - The sputter wrap widths 19/17 become `layout.noteCharsEn` / `noteCharsVi`.
  - The catalyst pitch 35 and spread 105, socket origin 488,24 and icon offset 3,3 become layout fields.
  - The potency bar origin 18,70, width 132, height 5 and the tight test `70 + w + 4 > 153 - target` become layout fields.
  - The fixed Ignite label position 575,306 moves to the prefab, with only its pressed offset (2) staying in code.
  - The Redraw and pause colours become serialized fields (or `Palette` if shared).
  - Delete `AspectDy` (always 0).
- [ ] **Step 3: Switch the tutorial over.** `TutorialController` reads `game.Bench.Hud.RedrawRect`. Re-run the Step 1 test.
- [ ] **Step 4: Verify.** Golden (both languages, including `bench.crowded`), the pin tests and EditMode pass. Lint on the six files shows no UL009 / UL010.
- [ ] **Step 5: Checkpoint.** Suggested message: `refactor(bench): layout numbers in the prefabs`, `Story: P7-13`.

### Task 14: Screen numbers (P7-14, wave 6b)

**Files:**
- Modify: `Game/Ui/JournalView.cs`, `GoalsView.cs`, `CatalystChoiceView.cs`, `EventView.cs`, `RunOverView.cs`, `PauseMenu.cs`, `TitleView.cs`, `GoalToast.cs`, `Tooltip.cs`, `UiButton.cs`, `Support/RarityStars.cs`, `Support/Label.cs` (if it has any finding), and their prefabs under `Prefabs/Views` and `Prefabs/Parts`
- Modify: `TutorialController.cs` (Journal rectangles from the instance)

**Interfaces:**
- Produces: `public Rect JournalView.IndexRect / RecipesRect / HintsRect { get; }` as instance properties from `JournalView.Layout`, replacing the statics at `JournalView.cs:181-185`.

- [ ] **Step 1: Apply the recipe** to each file. Its timings (fade and slide durations, e.g. `EventView` 0.35 s / 0.62 alpha / -40 px, 0.2 s) go into `<View>Timing`.
- [ ] **Step 2: Switch the tutorial over.** `TutorialController` reads `game.JournalView.RecipesRect` (and the others). The Task 13 Step 1 test stays green.
- [ ] **Step 3: Verify.** Golden (every Title, Journal, Goals, Choice, Event, RunOver, Pause and Toast shot, both languages), the pin tests and EditMode pass. Lint on these files shows no UL009 / UL010.
- [ ] **Step 4: Checkpoint.** Suggested message: `refactor(ui): screen layout and timing in the prefabs`, `Story: P7-14`.

### Task 15: Cutscene numbers (P7-15, wave 6c)

**Files:**
- Modify: `Game/Cutscene/SimpleCutscene.cs`, `ReactionCutscene.cs`, `WindowView.cs`, `TutorialController.cs`, `TutorialSettings.cs`, `TutorialHole.cs`, `Cutscene.cs`, `Main.unity`
- Test: `LayoutPinTests.cs`

- [ ] **Step 1: Group the timings by beat.** Each cutscene's timings become timing structs grouped by beat (spec §6.3), one struct per beat. For example, in `SimpleCutscene`:
  - `IntroPage { fadeIn 0.9, lineFade 0.35, hold 0.8, lastHold 0.3, finalHold 1.6, fadeOut 0.4 }`
  - `DescentBanner { flash 0.3/0.5, shake 2/0.3, rise 0.3 from art y 102 to 88, hold 0.9, … }`

  Positions (`Px.At(CX, 170)` and the rest) go to the scene object's child Transforms, or to a `Layout` struct when a formula uses them. Spark bursts become `BurstSpec` fields (the type from Task 11).
- [ ] **Step 2: Remove the Tutorial defaults.** Remove the initializers from `TutorialTargets` and `TutorialStyle` (spec §8 call 2), after `Reserialize.All`. Pin tests assert today's values for the main fields: dialog rect, popup offsets, rim thickness, arrow blink, timings.
- [ ] **Step 3: Verify.** EditMode and pin tests pass. `descents.mjs` passes on the dev build (the cutscenes play to the end). Watch the intro, one descent, one discovery and the ending in the Editor and confirm they look and time as before; record this.
- [ ] **Step 4: Checkpoint.** Suggested message: `refactor(cutscene): choreography numbers in serialized beats`, `Story: P7-15`.

### Task 16: Depth and effects numbers, lint to error (P7-16, wave 6d)

**Files:**
- Modify: `Game/Depth/DepthRig.cs`, `DepthTable.cs`, `DepthMath.cs`, `CameraSway.cs`, `FogSheet.cs`, `TiltShift.cs`, `DepthLayer.cs`, `Game/Fx/Flame.cs`, `Flipbook.cs`, `ScreenFx.cs`, `Sparks.cs`, `SparkMesh.cs`, `Fx/*`, `Game/Support/*` (any remaining findings), `Main.unity`
- Delete: `Game/Depth/DepthTestControls.cs` (+ `.meta`), if `grep -rn DepthTestControls Athanor/Assets` finds no user outside itself
- Modify: `tools/lint/lint.config.json` (UL009, UL010 → error), `docs/ATHANOR.md` §5

- [ ] **Step 1: Apply the recipe.**
  - `DepthMath` stays a set of pure functions. Its constants that describe the rig (e.g. the rest distance) become `DepthRig` serialized fields and are passed in. `Px.W/H` stay.
  - `CameraSway`'s A = 4 px and periods 6 s / 9 s become serialized fields with those values (decision 2026-11-16).
  - The tilt-shift band 145..235 stays in its renderer feature asset if it is already serialized there.
- [ ] **Step 2: Lint the whole folder.** Run `python3 tools/lint/unity_lint.py --root Athanor --config tools/lint/lint.config.json --all` and filter for UL009 / UL010. Expected: none in `Assets/Scripts/Game`.
- [ ] **Step 3: Lint to error.** Set UL009 and UL010 to `"error"` and remove the four `severity` overrides added in Task 2. Then `tools/ci/run.sh all`: exit 0.
- [ ] **Step 4: Verify.** EditMode, golden, `mainhd2d.mjs` (sway 27/27, rest, hud) and `descents.mjs` pass.
- [ ] **Step 5: Checkpoint.** Suggested message: `refactor(depth): rig and effect numbers serialized; UL009/UL010 enforced`, `Story: P7-16`.

### Task 17: Hot paths (P7-17)

**Files:**
- Modify: `Game/Bench/HudView.cs` (`Update` at :211-241), then every `Update` that does work when idle (start with `BenchView`, `TileView`, `CardView`, `JournalView`, `TitleView`, `GoalToast`, `TutorialController`, `PointerInput`)
- Test: `Athanor/Assets/Tests/EditModeGame/HotPathTests.cs`

**Interfaces:**
- Produces: `public void HudView.Tick(float deltaTime)`, which `Update` calls with `Time.deltaTime`. The same `Tick(float)` shape applies to any other component this task makes testable.

- [ ] **Step 1: Write the failing tests:**

  ```csharp
  [Test] public void Tick_IdleHud_AllocatesNothing()
  {
      var hud = /* Bench prefab Hud, Show(...) with a seeded run, SetPotency(120, instant: true) */;
      hud.Tick(0.016f);                                    // first frame may lay out
      Assert.That(() => hud.Tick(0.016f), Is.Not.AllocatingGCMemory());
  }
  [Test] public void Tick_PotencyUnchanged_KeepsTheSameText() { /* the potency Label's text object reference is unchanged after a second Tick */ }
  ```

  `Is.Not.AllocatingGCMemory` comes from `UnityEngine.TestTools.Constraints`; add `using Is = UnityEngine.TestTools.Constraints.Is;`.
- [ ] **Step 2: Run them to see them fail.** Today the potency text is formatted every frame.
- [ ] **Step 3: Implement the HUD.** `Tick` formats the potency only when `Mathf.Round(shown)` changes. It swaps the Ignite sprite and moves its label only when `canIgnite` / `igniteDown` change, lays out the bar only when its width changes, and sets `ignitePulse.color` only while `canIgnite && !igniteDown`.
- [ ] **Step 4: Go through the other `Update` methods.** In each, return early when idle, and do no string building, sprite lookup or `GetComponent` per frame. Add a `Tick_Idle<Type>_AllocatesNothing` test for each component changed.
- [ ] **Step 5: Measure.** On the dev build in the Editor Profiler (Play, reach the bench, idle 10 s), the CPU Usage "GC Alloc" for the frame's script updates must be 0 B. Write the figure into ATHANOR.md §6.
- [ ] **Step 6: Verify.** EditMode and golden pass.
- [ ] **Step 7: Checkpoint.** Suggested message: `perf(game): idle frames allocate nothing`, `Story: P7-17`.

### Task 18: Docs, vault, sign-off (P7-18)

**Files:**
- Modify: `docs/ATHANOR.md`:
  - §2 architecture: ScreenFlow, IgniteSequence, FxRefs, Palette, the allowed statics, the number-placement rule from spec §6.1, and the threading and platform rules from spec §6.8
  - §3: `rules`
  - §5: final rows
  - §6: figures
  - §7 carry-overs: close 7
- Modify: `vault/` (regenerate; `vault/Design/Architecture.md` by vault-librarian)
- Delete: `docs/superpowers/specs/2026-11-19-athanor-phase7-unity-conversion-design.md` and this plan, once their lasting content is in ATHANOR.md (2026-11-19 rule)

- [ ] **Step 1: Release build size.** `unity build Athanor --target WebGL --execute-method WebBuild.Build --allow-dirty-build`, then measure Build/ + `index.html`. Expected: ≤ 11,824,672 B. Record it next to 10,961,269 B, and note whether removing `Regex` changed it.
- [ ] **Step 2: Final checks.** On the release build: `loadsmoke.mjs`. On the dev build: `webtest.mjs`, `autoplay.mjs`, `descents.mjs` and `mainhd2d.mjs`. All exit 0.
- [ ] **Step 3: Diff the reports** against the baseline: no diff for all six.
- [ ] **Step 4: Run the suites.** `dotnet test tools/CoreTests` and EditMode: 0 failed, 0 skipped. Record the counts in ATHANOR.md §6.
- [ ] **Step 5: Update the docs** (sections above), then `python3 tools/vault/build_vault.py` followed by `tools/ci/run.sh all`: exit 0, no vault diff.
- [ ] **Step 6: Checkpoint.** Report every figure. Suggested message: `docs: phase 7 merged into ATHANOR.md`, `Story: P7-18`.
