# Athanor Phase 5: audit of Wave 2 and the briefs still to run (delta to the Phase 5 plan)

Base plan: [2026-11-15-athanor-phase5.md](2026-11-15-athanor-phase5.md) (not edited). Decisions: [phase-5-prep.md](../../devlogs/phase-5-prep.md).
Written by `phase-lead` on 2026-11-15 from the working tree (231 entries in `git status --short`, nothing committed since `00b84e3e`).
Everything in "Common to every brief" of the base plan still applies (vault first, `DOTNET_ROLL_FORWARD=Major`, evidence folders,
`build_vault.py`, one owner per file per wave, no commits). Anything marked **unverified** was not checked by running it.

## 0. Checks run for this audit (real results)

| Check | Result |
|---|---|
| `tools/ci/run.sh all` | **RED.** Stops at lint: 3 x `UL100` (test name not `Method_Condition_Result`) in `Athanor/Assets/Tests/EditModeGame/DepthLookTests.cs:31,37,48`. |
| `tools/ci/run.sh repo` | green: BUILD-OUTPUT, CONTENT, ICONS, LANG, CORE-PURITY, META ok, 0 problems |
| `tools/ci/run.sh core` | **318 passed, 2 failed** of 320: `RarityTests.RealContent_HasNoWeightsYet_SoEveryRarityWeighsOne` (`RarityTests.cs:149`) and `RunStateTests.InformedPlayer_WinsMostSeeds` (`RunStateTests.cs:436-449`): **35/40 wins, expected 24..31**. |
| BalanceMatrix / `RarityReport` (ATHANOR_RARITY_REPORT=1) with `[4,3,3,3]` | RarityReport partial (run still going when written): win rate when the item is held, per bucket: **Common 72.0%, Uncommon 74.6%, Rare 73.7%, Legendary 86.1%** (400 runs per item). Policy rows on a fresh profile: offers are only C/U (43-49% / 50-61%, Rare and Legendary 0%: locked on a fresh profile), wins 61.0-68.8%. Read: Rare does not pay over Uncommon; weighting changes little for a fresh profile because Rare/Legendary are not unlocked. BalanceMatrix rows: **not produced**: `BalanceMatrix` is `[Test, Explicit]` (`BalanceMatrixTests.cs:47`) and the `~BalanceMatrix` filter did not select it; W2-03 must name it exactly (command below). |
| `unity test Athanor --mode EditMode` | **not run** (heavy). Unverified for every Wave 2 task. |
| Release build on disk | `Athanor/Build/web/Build` dated 09:07-09:08: data 4,591,973 + wasm 7,324,458 + framework 74,096 + loader 48,779 = 12,039,306 B (+ index.html ~3,053) = **~12.04 MB, over the 11,824,672 B ceiling by ~218 KB**. Built **before** `tools/fx/shrink.mjs` rewrote the 4 sheets (09:11; PNG now 238 KB together, was ~1.8 MB) and possibly before the window views were imported. Release vs development not confirmed: **unverified, must be rebuilt (W2-08)**. |

## 1. Audit of 5a-07 and 5a-09..5a-15 against the plan's acceptance

| Task | State | Evidence (read in code / on disk) | Missing |
|---|---|---|---|
| 5a-07 Flipbooks | **PARTIAL** | 4 sheets + `.asset` in `Athanor/Assets/Art/Fx/Sheets/`; `ArtLibrary.asset:353-357` has 4 `sheets` entries (imported); sheets shrunk after import (png/json 09:11 > asset 09:05); `production/qa/evidence/phase5/5a-07/*-before-after.png` are generator previews | re-import after the shrink (unverified that `.asset` matches the shrunk json); **screenshots of each effect in play** (none); size after import |
| 5a-09 Window views | **DONE** | `Art/Background/window_d1..d5.png` (+meta), `Art/Fx/window_drip.png`, `tools/art/export-window.mjs`, `evidence/phase5/5a-09/contact.png` | plan says `node tools/art/export.mjs` writes them; it is a separate `export-window.mjs`: confirm `export.mjs` calls it or note it (QA) |
| 5a-10 Lang in Game | **PARTIAL** | `Game/Support/Loc.cs` (107 lines), `Loc.` in 17 views; `Tests/EditModeGame/LocTests.cs:19` `ViJsonCharacters_AllInCruxAtlas_NoneMissing`, `:30`, `:43` (glyph tests exist); `evidence/5a-10/web/` 94 files incl. `d03_title_vi`, `d04_journal_vi`, `vi_*` tutorial; `console.txt` 0 errors | VI screenshots of **event, catalyst choice, run over, intro** (title, bench, journal exist); dead literal `ReactionCutscene.cs:76` "CLICK TO SKIP" (overwritten by `:244`, harmless but a literal); EditMode run result unverified |
| 5a-11 Settings, haptics, mute, skybox | **PARTIAL (near done)** | `Haptics.cs`, `AudioMute.cs`, `Plugins/WebGL/Athanor.jslib`; skybox **off**: `Editor/SceneBuilder.cs:30` `RenderSettings.skybox = null`, `Scenes/Main.unity:29` `m_SkyboxMaterial: {fileID: 0}`; round trip `SettingsHapticsTests.cs:28` `Settings_StoredThenLoaded_RoundTrip`, `:37` defaults; webtest part D `tools/web/webtest.mjs:187-218` (Settings, VI and back, reload); 36 shots | `evidence/5a-11/console.txt:204` has **1 error** `ArgumentOutOfRangeException` (09:20 run; the 10:38 run in 5a-10 has 0: re-run to prove fixed); manual Android pulse / iOS hidden row not recorded |
| 5a-12 Tutorial, How to play, Watch intro | **PARTIAL** | `TutorialController.cs` (399 lines); `TutorialRules.PlanFor` `TutorialController.cs:17-21`; title skip prompt `GameFlow.cs:206-216`, `TitleView.cs:96`; sandbox `GameFlow.cs:179-186`; `MarkDone` at end `TutorialController.cs:393`; tests `TutorialFlowTests.cs:47,53,61,86,95`; 64 shots EN+VI per step, console 0 errors | **decision #9 not in Core:** `Core/Tutorial.cs:109` still `!tutorialDone && journalEntries == 0`, the AskSkip branch is a rule living in the Game layer (hard rule); "Skip" (`GameFlow.cs:214`) returns to the title and needs a second Begin click (UX: unverified whether intended); `TutorialTests.cs:268` still encodes the old rule |
| 5a-13 Pause menu | **DONE (pending run)** | `Ui/PauseMenu.cs` (131); `PauseTests.cs:40-115` (time restore, Esc, quit discards, profile/journal unchanged); 11 shots incl. 500x300 and VI | EditMode run result unverified; webtest coverage of pause unverified |
| 5a-14 Depth intro, window swap, rarity | **PARTIAL** | intro line `SimpleCutscene.cs:54,158-177`; `DepthLook.cs:1-32`; `WindowView.cs` (186); `DepthLookTests.cs` (3 tests, **lint red**); `tools/web/descents.mjs`; `evidence/5a-14/en/` 9 files, only depth 1, plus `fail_en.png` (black room, run never reached a descent) | **rarity display not built:** no `star` sprite used in any `.cs` (`CardView.cs:43-47` and `CatalystChoiceView.cs:14,85` still draw the round `pip`; `JournalView.cs` has no rarity mark) vs SPEC changes 21-24 (`docs/mockup/phase5-ui/SPEC.md:51-55`); descents 2..5 in EN and all in VI missing; Descent text positions vs SPEC (f) unchecked (G1 item) |
| 5a-15 Rarity weights | **PARTIAL, not approved** | `content.json:2807` `"rarityWeights": [4,3,3,3]` | owner never approved values; 2 Core tests red; no before/after BalanceMatrix, no per-rarity report quoted |

Extra findings: `Plugins/` shows as staged-deleted **and** untracked (`Plugins/WebGL/Athanor.jslib` is new and needed; only the PrimeTween installer was meant to go). `"Sổ tay vẫn được giữ."` (`ui.pause.quit_body2`, vi.json:49): the claim is true in code, `GameFlow.cs:416-417` stores the journal right after every `Run.Ignite()`, and `PauseTests.cs:115` checks the quit keeps what is stored, but no test asserts "a discovery is stored before Quit" (W2-05). Rotate-device layer: **not built** (no `orientation|portrait|rotate` in `WebGLTemplates/Athanor/index.html`, `Athanor.jslib` or `Scripts/Game`).

## 2. Briefs still to run

Owner of a file in brackets. One owner per file per wave. Every brief: read the vault first (`python3 tools/vault/q.py type <X>`), no commits,
report Done / Verified / Handoffs / Open questions.

### Wave 2-fix A (parallel, disjoint paths)

#### W2-01 Fix the lint and the stale rarity test: `rules-engineer`
- **Objective:** make `tools/ci/run.sh lint` and the stale test honest without changing any rule.
- **Inputs:** `Athanor/Assets/Tests/EditModeGame/DepthLookTests.cs:31,37,48` (names), `tools/lint/lint.config.json` (UL100 pattern),
  `Athanor/Assets/Tests/EditMode/RarityTests.cs:149-153`, `ContentDb.RarityWeight` (`q.py member RarityWeight`).
- **Do:** rename the three tests to `Method_Condition_Result` (e.g. `WindowSprite_AnyDepth_NamesTheArtSprite`); replace
  `RealContent_HasNoWeightsYet_SoEveryRarityWeighsOne` by `RealContent_RarityWeights_AreValidAndPositive` (length 4, each >= 1,
  read from content, no hard-coded values) so the test no longer encodes a content number.
- **Acceptance:** `tools/ci/run.sh lint` 0 errors; `DOTNET_ROLL_FORWARD=Major dotnet test tools/CoreTests --filter RarityTests` 0 fail.
- **Out of scope:** `InformedPlayer_WinsMostSeeds` (its range is a balance target: W2-03), content.json, the baseline file
  `tools/lint/baseline.json` (never add these to the baseline).
- **Note:** `DepthLookTests.cs` is in `Tests/EditModeGame` (Game tests). The rules-engineer owns EditMode tests per the role table; if
  the main session prefers, give the rename to `unity-gameplay-engineer` in W2-04 instead. Not both.

#### W2-02 `ShouldRun` per decision #9, in Core, test first: `rules-engineer` (after W2-01, same agent, same sitting allowed)
- **Objective:** Core decides the tutorial plan from `TutorialDone` only, plus the skip question for a non-empty journal.
- **Inputs:** decision #9 `phase-5-prep.md:75`; `Core/Tutorial.cs:108-109`; `Tests/EditMode/TutorialTests.cs:262-270`;
  current Game copy `Game/Cutscene/TutorialController.cs:10-21` (`TutorialPlan`, `TutorialRules.PlanFor`).
- **Do:** tests first. `TutorialScript.ShouldRun(bool tutorialDone) => !tutorialDone`; add `enum TutorialPlan { None, Run, AskSkip }`
  and `TutorialScript.Plan(bool tutorialDone, int journalEntries)`: done -> None; not done and empty -> Run; not done and entries -> AskSkip.
  Rewrite the `ShouldRun_OnlyOnAFreshSave` cases. Keep an `[Obsolete]` 2-arg overload only if the Game layer still compiles against it
  until W2-04 lands; say which.
- **Acceptance:** Core tests 0 fail (TutorialTests count quoted); `tools/ci/run.sh repo` CORE-PURITY ok.
- **Out of scope:** Game layer edits (handoff W2-04), skip-prompt UI.
- **Depends on:** none (W2-01 only shares the agent).

#### W2-03 Rarity weights: options for the owner, then the chosen value: `content-designer` (sole owner of `content.json`)
- **Objective:** measure 3 weight sets, report them, and set the one the owner picks; nothing else in content.
- **Inputs:** 5a-04 field (`content.json:2807`, `q.py find rarityWeights`); `RarityReportTests.cs` (opt-in, `ATHANOR_RARITY_REPORT=1`);
  `BalanceMatrixTests.cs`; baseline rows `docs/devlogs/phase-4-lore.md:247-266`; seed-noise note `:267-273` (a 200-run row has ~4 points sd);
  `RunStateTests.cs:436-449` (fresh target 60-70%, 24..31 of 40); decision #2 (offer and event items only).
- **Do, step 1 (no approval needed, measure only):** run BalanceMatrix + RarityReport for each set, field removed between runs:
  - **A `[1,1,1,1]`** (= field absent, today's behaviour, control).
  - **B `[4,3,2,1]`** (recommended: the classic falloff; rare items stay rare; least risk to the 60-70% fresh target).
  - **C `[6,3,2,1]`** (steeper: Legendary at 1/12 of offers; use if B still lifts fresh wins).
  - and the current **`[4,3,3,3]`** (in the tree, gives 35/40 on `InformedPlayer_WinsMostSeeds`).
  Commands: `cd tools/CoreTests && DOTNET_ROLL_FORWARD=Major dotnet test --filter "Name=BalanceMatrix" -l "console;verbosity=detailed"` (Explicit test: select it by exact name and check the 12 rows are printed; `ATHANOR_SEEDS` sets the seed count, default 200)
  and `ATHANOR_RARITY_REPORT=1 DOTNET_ROLL_FORWARD=Major dotnet test --filter "FullyQualifiedName~RarityReport" -l "console;verbosity=detailed"`.
- **Report:** all 12 BalanceMatrix rows per set next to the baseline, the delta per row in points, the per-rarity win rate, and
  `InformedPlayer_WinsMostSeeds` wins/40 per set. A row moving more than 3 points needs a written reason (cite the seed-noise note,
  or run the 10,000-seed check as in phase-4-lore). **Then stop: the owner picks (Open question 1).**
- **Do, step 2 (after the owner's pick):** write the chosen array; leave content.json otherwise byte-identical (newline at EOF as before).
- **Acceptance:** Core tests 320 (+ W2-01/02 changes) 0 fail **including `InformedPlayer_WinsMostSeeds` in 24..31** without widening its
  range; if no set keeps it in range, report that and hand the range question to the owner (do not edit the test).
- **Out of scope:** satchel, aspects, events, new items, test ranges.

#### W2-04 Game layer follows the Core tutorial plan + rarity stars: `unity-gameplay-engineer` (after W2-02)
- **Objective:** the Game layer calls `TutorialScript.Plan`, and the rarity display matches SPEC changes 21-24.
- **Inputs:** `TutorialController.cs:10-25`, `GameFlow.cs:170-216`; `TutorialFlowTests.cs:47-66`; SPEC `docs/mockup/phase5-ui/SPEC.md:51-55`
  (changes 21-24: 7x7 star, n = 1..4, 8 px pitch, dark outline of 4 copies, hand card row at x+3,y+3, catalyst card at x+6,y+6, journal
  list one star top-left Uncommon+ at cell x-1,y-1, detail n stars at 358,53 and tier word Name 16 at 358,63); `Art/UI/star.png`
  (5a-07); `CardView.cs:43-47`, `CatalystChoiceView.cs:14,85-86`, `JournalView.cs`; frames `evidence/phase5/5a-03/e1..e3-*`.
- **Do:** delete `TutorialPlan`/`TutorialRules.PlanFor` from Game (keep `MarkDone` there, it is save plumbing) and use the Core plan;
  decide nothing new. Skip answer: if the owner says "Skip starts the run" (Open question 3), break to the run instead of `continue`.
  Rarity: star sprite into `ArtLibrary` (re-import if needed), replace the pip on hand card, catalyst offer, tooltip, journal per SPEC.
- **Acceptance:** `unity test Athanor --mode EditMode` 0 fail (count quoted); screenshots EN + VI 1x and 2x of hand, offer, journal list,
  journal detail (Common and Legendary) in `production/qa/evidence/phase5/5a-14/rarity/`; webtest 0 console errors.
- **Out of scope:** content values, star art (pixel-artist), descents (W2-06).

#### W2-05 "Your journal is kept" proof: `unity-gameplay-engineer` (same sitting as W2-04, same files family; sequential)
- **Objective:** a test that a discovery made in a run is in storage before Quit, so `ui.pause.quit_body2` / `ui.title.erase_keep` are true.
- **Inputs:** `GameFlow.cs:409-417` (`StoreJournal()` right after `Run.Ignite()`), `GameFlow.cs:358-359` (`JournalAfterQuit`), `PauseTests.cs:115-129`.
- **Acceptance:** new EditMode test `Quit_AfterADiscovery_JournalInStorageHasIt` passes; one Log line drafted for the main session.
- **Out of scope:** text changes (if false, Open question to the owner, `localizer-vi` changes the line).

### Wave 2-fix B (after W2-04/05; one Unity owner, sequential)

#### W2-06 Missing evidence and the descents: `unity-gameplay-engineer`
- **Objective:** close the evidence gaps of 5a-10, 5a-11, 5a-14 and check the Descent text positions against SPEC (f).
- **Inputs:** `tools/web/descents.mjs` (needs `WebBuild.BuildDev`, `node descents.mjs <url> <outDir> [seconds] [lang]`),
  `tools/web/webtest.mjs`, `SimpleCutscene.cs:54,146-201`, SPEC (f) and frames `5a-03/f1-*, f2-*`; `evidence/5a-14/en/fail_en.png`
  (run never reached a descent: raise `seconds` or fix the scripted start).
- **Do:** (a) VI shots for 5a-10: event, catalyst choice, run over, intro (+ EN pairs) into `evidence/phase5/5a-10/`; (b) re-run webtest and
  confirm `ArgumentOutOfRangeException` (`5a-11/console.txt:204`) is gone, else fix and name the cause; (c) descents 1..5 in EN and VI into
  `evidence/phase5/5a-14/en|vi/`, longest line (`f2` worst case) included; (d) table: SPEC (f) y positions vs `SimpleCutscene.cs` values.
- **Acceptance:** files exist; every `console.txt` 0 `[error]`; EditMode 0 fail.
- **Out of scope:** new features.

#### W2-07 Rotate-device layer for portrait phones: `ui-ux-designer` spec, then `unity-gameplay-engineer` or `web-release-engineer`
- **Objective:** a portrait phone sees a "rotate your device" layer instead of a squeezed 640x360 canvas (approved at G1).
- **Step 1 `ui-ux-designer`** (parallel with Wave 2-fix A): one frame in `docs/mockup/phase5-ui/` (EN+VI), keys `ui.rotate.title/body`
  (handoff to `localizer-vi` for en/vi), whether it is HTML in the template (no Unity cost) or in-game. Recommendation: HTML/CSS in
  `WebGLTemplates/Athanor/index.html`, shown by `@media (orientation: portrait) and (max-width: 900px)`; text from a small table in the
  template (it shows before Unity loads, so `Lang` is not available): **Open question 5**.
- **Step 2:** owner of `WebGLTemplates/` is `web-release-engineer` (template) per role table; implement after the spec.
- **Acceptance:** Chrome DevTools device emulation portrait/landscape screenshots in `evidence/phase5/5a-rotate/`; webtest desktop unchanged.

#### W2-08 Flipbooks in play and size after import: `unity-gameplay-engineer` then `web-release-engineer`
- **Objective:** prove the 4 effects play in the game and that 5a still fits the ceiling.
- **Inputs:** `Fx.cs:9`, call sites `GameFlow.cs:356,407` (re-locate, file changed), `ReactionCutscene.cs:142-143`; `tools/fx/shrink.mjs`;
  `ArtLibrary.asset:353-357`.
- **Do (unity):** `Athanor > Import Effect Sheets` again after the shrink; screenshots of implosion, holy-pillar, collect-burst, poof in play
  into `evidence/phase5/5a-07/ingame/`.
- **Do (web-release):** release build `WebBuild.Build`, `ls -l Athanor/Build/web/Build`, Build Report Textures line, total incl. index.html vs
  **11,824,672 B**. If over: list the top texture contributors and hand cuts to `pixel-artist` (frame step, crop) as new briefs; do not cut alone.
- **Acceptance:** 4 in-game shots; total bytes quoted with headroom left for 5b.

### Wave 3 (last, sequential)

#### 5a-16 Sign-off: `qa-engineer` (as base plan, plus)
- `tools/ci/run.sh all` green (lint included); Core count; `unity test Athanor --mode EditMode` count vs baseline 331/325/6;
  BalanceMatrix with the chosen weights matching W2-03; `node tools/web/webtest.mjs` and `node tools/web/autoplay.mjs` 0 errors;
  evidence complete for 5a-03, 07, 09, 10, 11, 12, 13, 14 (+rarity), rotate; window views produced by the documented command;
  every new string a key with en+vi (`--filter LangTests` 0 fail). Bugs come back as briefs.

#### 5a-17 Release build: `web-release-engineer`
- Size vs 5a-01 (10,776,096) and vs ceiling 11,824,672; the 5a share of the 1 MiB stated and the remainder for 5b; release notes draft;
  `CREDITS.md` has Coder's Crux (NAL, CC BY 3.0) and PrimeTween.

#### 5a-18 Vault: `vault-librarian`
- `python3 tools/vault/build_vault.py`; the 23 new untracked vault notes (`vault/Code/Game/*`, `vault/Tests/*`, `vault/Workflow/Glossary.md`)
  regenerate cleanly; no broken links. Phase row in `vault/Phases/Phases.md` and `docs/devlogs/README.md` (main session or phase-lead).

## 3. Phase 5b: review of the base briefs (5b-01..08)

Pre-conditions (owner): branch **`feature/phase5b-hd2d` from `develop` after 5a is committed and merged**; created by the owner or on their
explicit command (decision #6), and `feature/2.5D_test-phase-4` deleted the same way. `.gitignore` already has `refs/` (`.gitignore:146-147`,
uncommitted): commit it with the 5a docs commit. 5b uses the ceiling remaining after 5a-17 (number from 5a-17, not from the base plan).

| Task | Add to the base brief |
|---|---|
| 5b-01 | Inputs: `refs/2.5D Reference/` is local only (not in git): if absent on the machine, stop and say so. Deliverable adds a size note per effect (shader passes, textures) so G2 sees cost. Acceptance adds 1x and 2x screenshots per preset, toggles on/off pairs. **G2** = owner approves values (tilt band, blur, fog alpha, bloom per depth). |
| 5b-02 | Acceptance: opens from disk, 0 console errors, screenshots 30/60/144 Hz in `evidence/phase5/5b-02/`. Owner review only, no gate needed. |
| 5b-03 | Acceptance: the file lists each question with 2-3 options and a recommendation; no content keys. Consult `narrative-director` as a framework consultant only. Owner answers before any endings task exists. |
| 5b-04 | Depends on G2. Inputs: the G2 values; `tools/art/scene.mjs`. Acceptance adds bytes per new PNG and the sum (feeds 5b-06). Out of scope: storyboard assets (Q3 decision: after G3). |
| 5b-05 | Depends on 5b-04. Paths: `Assets/Scenes/DepthTest.unity`, a new `Editor/DepthTestBuilder.cs`, new `Game/Depth/*`; **must not touch `Main.unity`, `GameFlow.cs`**. Acceptance adds EditMode 0 fail and the ray-click test on 9 tiles. |
| 5b-06 | Acceptance: delta vs 5a-17 and vs remaining ceiling, frame time (ms, laptop GPU, 60 s average), shimmer at 2x/3x screenshots. **G3**. |
| 5b-07 | After G3. Inputs: depth devlog `phase-4-depth.md:181-199`, 5b-05 report. Acceptance: webtest + autoplay 0 errors, every 5a evidence screen re-shot (no regressions), EditMode 0 fail. Shake = world only (decision #8). |
| 5b-08 | As 5a-16/17; total growth over 5a-01 <= 1,048,576 B. |

## 4. Clean-up and risks before more implementation

1. **Stray file at root:** `2026-11-15-084514-hin-ti-cn-spec-no-tn-ng-t-phase-4-v-idea.txt` (untracked; a session transcript like the six
   older `2026-11-1*.txt` already in the root). Recommend: move all seven out of the repo or add `/20??-??-??-*.txt` to `.gitignore`; owner decides (Q7).
2. **`Plugins/` mismatch:** git shows `D  Athanor/Assets/Plugins.meta` staged and `?? Athanor/Assets/Plugins.meta`, `?? Athanor/Assets/Plugins/` untracked.
   `Plugins/WebGL/Athanor.jslib` is needed (haptics, mute, language). Before committing: `git add Athanor/Assets/Plugins.meta Athanor/Assets/Plugins/WebGL`
   so the folder meta is not lost (else Unity regenerates a new GUID).
3. **Size:** last on-disk build ~12.04 MB > 11,824,672 (unverified whether release, and pre-shrink). W2-08 settles it before 5a-17.
4. **CI red** (lint) and Core red (2 tests): W2-01 and W2-03 fix; CI is the merge gate.
5. **Proposed commit split (do not commit; the owner decides):**
   1. `chore: phase 5 size cuts` (5a-06: PrimeTween installer, TMP font, splash, `.gitignore`, `CREDITS.md`, build profile).
   2. `feat(core): rarity weights and settings` (5a-04, 5a-05: `Core/ContentData.cs`, `ContentDb.cs`, `RunStart.cs`, `RunState.cs`, `Settings.cs`, `Tests/EditMode/Rarity*`, `SettingsTests`).
   3. `feat(lang): depth intros and phase 5 keys` (5a-02, 5a-08: `lang/en.json`, `lang/vi.json`, `phase-5-lore.md`).
   4. `feat(art): flipbooks, window views, star` (5a-07, 5a-09: `Art/Fx/*`, `Art/Background/window_d*`, `Art/UI/star*`, `tools/fx/shrink.mjs`, `tools/art/export-window.mjs`, `ArtLibrary.asset`).
   5. `feat(game): Lang in the Game layer` (5a-10: `Loc.cs`, fonts `Crux.asset` and font assets, `FontBuilder.cs`, views, `LocTests`).
   6. `feat(game): settings, haptics, mute, pause` (5a-11, 5a-13: `Haptics.cs`, `AudioMute.cs`, `Plugins/WebGL`, `PauseMenu.cs`, `Save.cs`, `SceneBuilder.cs`, `Main.unity`, tests, `webtest.mjs`).
   7. `feat(game): tutorial controller, how to play, depth look` (5a-12, 5a-14: `TutorialController.cs`, `DepthLook.cs`, `WindowView.cs`, `SimpleCutscene.cs`, `tools/web/descents.mjs`, tests).
   8. `docs: phase 5 plan, mockups, evidence, vault` (`docs/`, `production/qa/evidence/phase5/`, `vault/`).
   Files shared by several tasks (`GameFlow.cs`, `TitleView.cs`, `HudView.cs`, `Main.unity`) cannot be split by hunk safely without review; put them in commit 7 or
   commit 5-7 as one `feat(game)` commit. `content.json` waits for W2-03.

## 5. Devlog Log lines for the main session (to append to `docs/devlogs/phase-5-prep.md` *Log*)

- 2026-11-15 (Wave 2, 5a-09): five window views `window_d1..d5.png` and `window_drip` from `tools/art/export-window.mjs`; contact sheet in evidence/phase5/5a-09.
- 2026-11-15 (Wave 2, 5a-10): `Game/Support/Loc.cs` wires `Lang` into every view; Crux for all vi roles; LocTests (glyph coverage of vi.json and en.json in the Crux atlas, item names English, number format per language). VI shots of event, catalyst choice, run over, intro still missing (W2-06).
- 2026-11-15 (Wave 2, 5a-11): Settings panel on the title, `athanor.settings.v1` round trip, Haptics (Android only), AudioMute on hidden tab via `Athanor.jslib`; skybox off (`SceneBuilder.cs:30`, `Main.unity`). webtest part D: Settings, VI and back, kept after reload. One `ArgumentOutOfRangeException` in the 09:20 webtest log, to re-check.
- 2026-11-15 (Wave 2, 5a-12): TutorialController (highlights, click locks, held popups, goal toasts), skip prompt for a journal with entries, How to play on a throwaway journal/profile, Watch intro. Decision #9 still lives in the Game layer (`TutorialRules.PlanFor`); moved to Core by W2-02.
- 2026-11-15 (Wave 2, 5a-13): pause menu (button, Esc, Resume, Vibration, Quit with confirm, timeScale 0); Quit discards the run uncounted; the journal is stored after each Ignite, so "Your journal is kept" is true (test W2-05).
- 2026-11-15 (Wave 2, 5a-14): depth intro line under the name in the Descent, themed window per depth in a normal run (not tutorial, not How to play), Endless keeps view 5. Rarity stars per G1 not yet built (W2-04).
- 2026-11-15 (Wave 2, 5a-15): `rarityWeights` [4,3,3,3] put in content.json without owner approval: fresh informed player 35/40 (target 24..31); held until the owner picks a set (W2-03).
- 2026-11-15 (audit): `tools/ci/run.sh all` red on lint (DepthLookTests names); Core 318/320. Fix briefs W2-01..08 in [2026-11-15-athanor-phase5-briefs.md](../superpowers/plans/2026-11-15-athanor-phase5-briefs.md).

## Handoffs (dispatch order)

| Wave | Tasks | Parallel? |
|---|---|---|
| 2-fix A | W2-01 then W2-02 (`rules-engineer`, one sitting) ‖ W2-03 step 1 (`content-designer`, measure only) ‖ W2-07 step 1 (`ui-ux-designer`) | yes: Core/tests vs content.json vs docs/mockup are disjoint. W2-03 must re-run after W2-01 if it lands first (test names only, no effect on numbers). |
| owner | Q1 weights, Q3 skip behaviour, Q5 rotate layer, Q7 stray files | |
| 2-fix B | W2-03 step 2 (`content-designer`) ‖ W2-04 -> W2-05 -> W2-06 -> W2-08 unity part (`unity-gameplay-engineer`, sequential: shared `GameFlow.cs`); `localizer-vi` for `ui.rotate.*` ‖ W2-07 step 2 (`web-release-engineer`, template only) | content vs Game vs template disjoint |
| 3 | W2-08 web-release size -> 5a-16 `qa-engineer` -> 5a-17 `web-release-engineer` -> 5a-18 `vault-librarian` | sequential |
| 5b | after 5a commit + branch by owner: 5b-01 ‖ 5b-02 ‖ 5b-03 -> G2 -> 5b-04 -> 5b-05 -> 5b-06 -> G3 -> 5b-07 -> 5b-08 | as listed |

Also: `producer` not needed. Main session adds the phase row to `docs/devlogs/README.md` (phase-lead did not touch devlogs in this pass, per brief).

## Open questions (owner)

1. **Rarity weights:** which set: A `[1,1,1,1]`, **B `[4,3,2,1]` (recommended)**, C `[6,3,2,1]`, or keep `[4,3,3,3]`? Why B: it is the
   usual falloff, keeps Legendary rare, and is the likeliest to keep the fresh win rate inside 60-70%; `[4,3,3,3]` lifted the informed
   fresh player to 35/40 (87.5%). Answer after W2-03 step 1 numbers.
2. If no set keeps `InformedPlayer_WinsMostSeeds` in 24..31: widen the target or keep weights at 1? *Recommend: keep 1 (A) and revisit
   after 5b; the 60-70% target is an owner number.*
3. Skip prompt "Skip": today it returns to the title and a second Begin starts the run (`GameFlow.cs:214`). *Recommend: Skip starts
   the run at once (one click, as the mockup a2 implies).*
4. "Play it" on the skip prompt plays the tutorial in the throwaway sandbox (How to play), not the real first run. *Recommend: keep
   (the journal already has entries; the real save must not change).*
5. Rotate-device layer: HTML in the WebGL template (before Unity loads, no Lang, two hard-coded strings picked by `navigator.language`) or
   in-game (Lang keys, only after load)? *Recommend: HTML, cheaper and visible during load.*
6. 5a-09 window views are produced by `tools/art/export-window.mjs`, not `export.mjs` as the plan says: accept? *Recommend: accept, but
   call it from `export.mjs` so one command rebuilds all art.*
7. Seven session transcripts `2026-11-1*.txt` at the repo root: delete, move to `production/session-logs/` (gitignored), or commit?
   *Recommend: move out and gitignore `/20??-??-??-*.txt`; they are not project docs.*
8. PrimeTween licence forbids redistributing the tgz in a derivative product: fine while the repo is private. *Recommend: note it in
   `CREDITS.md` and re-decide before any public release.*
9. Commit split in section 4: approve the 8-commit split (or one squash commit per wave)? *Recommend: the 8 commits, `content.json` after Q1.*
