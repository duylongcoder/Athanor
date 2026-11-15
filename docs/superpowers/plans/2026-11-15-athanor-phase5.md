# Athanor Phase 5 (5a foundation and content, 5b visuals): dispatch plan

Owner decisions this plan relies on: [phase-5-prep.md](../../devlogs/phase-5-prep.md) (written first).
Workflow: [Agent Workflow](../../../vault/Workflow/Agent%20Workflow.md). One owner per file per wave; parallel only on disjoint paths;
content and rules first, art/UI next, QA last. No agent commits.

## Common to every brief

- **Vault first:** `python3 tools/vault/q.py type|uses|find <x>`; never read a file over ~300 lines whole.
- **Core tests on this machine:** `DOTNET_ROLL_FORWARD=Major dotnet test tools/CoreTests` (only .NET 10 is installed;
  CI uses net8). Baseline: **298 passed, 0 failed**.
- **Balance report:** `cd tools/CoreTests && DOTNET_ROLL_FORWARD=Major dotnet test --filter BalanceMatrix -l "console;verbosity=detailed"`;
  baseline rows in `docs/devlogs/phase-4-lore.md:247-266`.
- **Unity:** `unity test Athanor --mode EditMode` (baseline 331 total, 325 passed, 6 skipped).
- **CI gate:** `tools/ci/run.sh all` must be green.
- **Hard rules:** every visible string is a Lang key in `en.json` and `vi.json`; item names stay English (no
  `material.*/catalyst.*/aspect.*.name` in vi.json); a new material/catalyst/aspect needs an icon id in `tools/art/icons.mjs`;
  every UI change leaves screenshots in `production/qa/evidence/phase5/<task-id>/`; after code/content changes run
  `python3 tools/vault/build_vault.py`; a design decision gets one line in `docs/devlogs/phase-5-prep.md` *Log* (via the main session).
- Size ceiling (owner): **+1 MB over the 5a-01 baseline for 5a and 5b together**.

---

# Phase 5a

## Wave 0 (parallel: build reads only; the two others write docs only)

### 5a-01 Size baseline — `web-release-engineer`
- **Objective:** record the release web build size before any Phase 5 change.
- **Inputs:** `Athanor/Assets/Editor/WebBuild.cs` (`WebBuild.Run(true)`), `docs/devlogs/phase-4-assets.md:455-475` (method),
  `:542-560` (last numbers: wasm 7,317,632, data 3,335,599, ~10.7 MB).
- **Acceptance:** report total, `web.wasm.unityweb`, `web.data.unityweb`, `web.framework.js` bytes from `ls -l Athanor/Build/web/Build`
  and the Editor.log "Build Report" block (Textures/Shaders/Fonts/Other). `node tools/web/webtest.mjs` 0 console errors.
- **Out of scope:** any cleanup (5a-06), any asset change.
- **Depends on:** none. **Output to main session:** the numbers, appended to phase-5-prep.md *Log*.

### 5a-02 Depth intro lines + lore audit — `lore-writer`
- **Objective:** write one line per depth (5) and the window view per depth, and confirm the Phase 4 event lore is in content.
- **Inputs:** `docs/devlogs/phase-4-lore.md:18-40` (world), `:177-213` (event table, limits name 22 / text 56 / outcome 52),
  `content.json` `depthNames` (`q.py find depthNames`), `SimpleCutscene.cs:146-194` (Descent: banner "DEPTH n" + name),
  `tools/art/scene.mjs:41-80` (the window is an arched pane, glass radius 40).
- **Deliverable:** new `docs/devlogs/phase-5-lore.md` with: 5 lines (`depth.<n>.intro`, at most 56 characters, shown under
  the depth name), one short art note per depth for the window view (what is seen through the window at that depth and
  one effect, e.g. drips, roots, motes), and an audit table: each of the 19 events, content.json text vs the lore table
  (match / differs). Add the 5 English keys to `Athanor/Assets/Resources/lang/en.json`.
- **Acceptance:** `DOTNET_ROLL_FORWARD=Major dotnet test tools/CoreTests --filter LangTests` passes (vi will report the 5
  new keys missing only if the test requires parity of story keys; if so, say so: 5a-08 adds them). Audit table complete.
- **Out of scope:** vi.json, art, code; the 7-scene storyboard (open question 3); ending lore (5b-03).

### 5a-03 UI specs and mockups — `ui-ux-designer`
- **Objective:** one mockup page for every 5a screen change, for the owner to approve before Unity work.
- **Inputs:** `docs/devlogs/phase-4-settings-localization.md:370-436` (panel layout), phase-5-prep.md section 7 decisions,
  `TitleView.cs:32-63` (current title positions; Begin 320,227 and Journal 320,257 must not move: `tools/web/webtest.mjs`),
  `HudView.cs:20-39` (Redraw at 556,88), `docs/devlogs/phase-4-tutorial-script.md:22-137` (highlight, click lock, text box,
  skip prompt), `CardView.cs`, `CatalystChoiceView.cs` (how rarity is shown today: faint), `docs/mockup/` (existing style).
- **Deliverable:** `docs/mockup/phase5-ui/index.html` with: (a) title with Settings, How to play, Watch intro, and the
  one-time "Skip tutorial?" prompt; (b) Settings panel (Vibration, Language, Reset; volume rows hidden), EN and VI text;
  (c) pause button + pause menu at 640x360 and at a phone's 2x/3x; (d) tutorial overlay: highlight, dimming, text box;
  (e) rarity display on cards, catalyst offer and journal, before/after; (f) the depth-intro line in the Descent.
  A key table (key, English) for every new string, for 5a-08.
- **Acceptance:** opens from disk with no console errors; screenshots of each panel in EN and VI in
  `production/qa/evidence/phase5/5a-03/`.
- **Out of scope:** `layout.json` and C# changes; 2.5D (5b).
- **Owner gate G1:** the owner approves (a) to (f) before Wave 2 Unity tasks start.

## Wave 1 (parallel, disjoint paths)

### 5a-04 Rarity-weighted item draws — `rules-engineer`
- **Objective:** item draws (catalyst offer and items given by events) pick by rarity weight from content; nothing else.
- **Inputs:** `RunState.cs:272-283` (BuildOffer: uniform shuffle), `RunStart.cs:331-333` (event item pick),
  `ContentData.cs:9,13,61` (rarity fields, `Rarities.Rank`), `ContentDb.cs:71-91` (validation), `Satchel.cs:13` (rng),
  phase-4-prep.md Orient 4 (rarity claim unmeasured).
- **Do:** tests first in a new `Athanor/Assets/Tests/EditMode/RarityTests.cs`: weights read from a new optional
  `content.json` field `rarityWeights` (Common/Uncommon/Rare/Legendary); missing field = all 1 = today's behaviour,
  deterministic per seed; FirstOffer (tutorial) is untouched; the satchel starter copies are **not** changed (open question 2).
- **Acceptance:** Core tests 298 + new pass, 0 fail; BalanceMatrix rows identical to the baseline with the field absent (quote them).
- **Out of scope:** `content.json` numbers (5a-15), aspects, events, display.

### 5a-05 Settings data in Core — `rules-engineer` (second dispatch, disjoint files)
- **Objective:** `Core/Settings.cs`: version, music, sfx, haptics, language; defaults (7, 8, true, ""), clamping, JSON round trip.
- **Inputs:** settings devlog `:293-317` (fields, storage), phase-5-prep.md decisions 5, 8.
- **Acceptance:** new `Tests/EditMode/SettingsTests.cs`: round trip, broken JSON gives defaults, clamp; no `UnityEngine` in Core
  (`tools/ci/run.sh repo`).
- **Out of scope:** volume curve (no sound), PlayerPrefs (5a-11).

### 5a-06 Package cleanup and size cuts — `web-release-engineer`
- **Objective:** remove what the owner dropped and record the size after.
- **Inputs:** packages devlog `:11-31`, `:53-70`, `:188-237`; assets devlog `:234-243` (2.9 size items); `CREDITS.md`.
- **Do:** confirm no FMOD anywhere (`git ls-files | grep -i fmod` only Unity's own setting); delete `Athanor/Assets/Plugins/PrimeTween`
  (installer only; the package resolves from `Athanor/Packages/com.kyrylokuzyk.primetween.tgz`); confirm Demo absent and the
  InOutSine ease in `Motion`; remove the unused TMP LiberationSans font (check `TMP Settings.asset` default font and fallbacks
  first); disable the splash (ProjectSettings); credit lines in `CREDITS.md`: Coder's Crux (NAL, CC BY 3.0), PrimeTween, and
  (Determination Sans was deleted at G1: no credit line.)
- **Acceptance:** release build bytes vs 5a-01; `tools/ci/run.sh all` green; `node tools/web/webtest.mjs` 0 errors.
- **Out of scope:** skybox (lives in `SceneBuilder`, handoff to 5a-11), atlases (open question 1), `.gitignore`.

### 5a-07 Flipbook export — `pixel-artist`
- **Objective:** export the 4 approved pixelfx sheets so the wired `Fx.Spawn` calls play.
- **Inputs:** `tools/fx/export.sh` (uses `./pixelfx`, present), `Fx.cs:9`, call sites `GameFlow.cs:356,407`,
  `ReactionCutscene.cs:142-143`, assets devlog `:477-500` step 6.
- **Acceptance:** 4 PNG+JSON in `Athanor/Assets/Art/Fx/Sheets/`; after `Athanor > Import Effect Sheets` (handoff to the
  unity engineer if the agent cannot run the Editor) `ArtLibrary.sheets` has 4 entries; screenshots of each effect in play.
- **Out of scope:** new effects, atlases, trails (already time based).

### 5a-08 Vietnamese for the new keys — `localizer-vi` (sole owner of `en.json` and `vi.json` this wave)
- **Objective:** en + vi entries for every new key from 5a-02 and the 5a-03 key table; parity and glossary.
- **Inputs:** `phase-5-lore.md` (5a-02), `docs/mockup/phase5-ui/` key table, settings devlog `:586-676` (glossary, voice, limits).
- **Acceptance:** `DOTNET_ROLL_FORWARD=Major dotnet test tools/CoreTests --filter LangTests` 0 fail (parity, NFC, every vi
  character in both SVN fonts, no item names); `tools/ci/run.sh repo` green.
- **Out of scope:** code; item names.

## Wave 2 (after G1; Unity tasks are sequential because they share `GameFlow.cs`/`TitleView.cs`; 5a-09 and 5a-15 run in parallel with them)

### 5a-09 Window views per depth — `pixel-artist` (parallel)
- **Objective:** 5 window views (same pane shape, transparent outside the glass) plus the effect sprite from 5a-02's notes.
- **Inputs:** `tools/art/scene.mjs:41-80` (`windowPane`), `tools/art/export.mjs:31-34`, `phase-5-lore.md`.
- **Acceptance:** `node tools/art/export.mjs` writes `Art/Background/window_d1..d5.png`; a contact sheet in
  `production/qa/evidence/phase5/5a-09/`.
- **Out of scope:** wall backgrounds per depth (the "full" option, not ordered), wiring (5a-14).

### 5a-10 Lang in the Game layer — `unity-gameplay-engineer`
- **Objective:** every view reads its text from `Lang`, with per-language fonts, so the game runs in EN and VI.
- **Inputs:** settings devlog `:426-458` (relabel option 1, tasks 3, 4, 7, 9), `:488-543` (font roles and sizes: Determination
  13.333/26.667/40, atlas at 40, Crux RASTER_HINTED), `Core/Lang.cs:40-188`, `Athanor/Assets/Editor/FontBuilder.cs`,
  `ArtLibrary.cs`, `Make.cs`, `UiButton.cs`, `Wording.cs`, all views (`q.py uses Make`).
- **Acceptance:** EditMode test: every character in vi.json is in the VI font atlases; webtest screenshots in EN match the
  current ones; VI screenshots of title, bench, journal, event, catalyst choice, run over, intro in
  `production/qa/evidence/phase5/5a-10/`; no `#key` on screen; `unity test Athanor --mode EditMode` 0 fail.
- **Out of scope:** the settings panel (5a-11), new content text.

### 5a-11 Settings panel, haptics, mute, skybox — `unity-gameplay-engineer`
- **Objective:** title Settings panel per the approved mockup, saved settings, haptics, mute on hidden tab.
- **Inputs:** settings devlog `:308-366`, `:370-425`; phase-5-prep.md section 7 rows 5, 7, 8, 9, 11; `Save.cs:11-74`;
  `GameFlow.cs:101-137`; `WebGLTemplates/Athanor/index.html`; `Editor/SceneBuilder.cs` (skybox off, from 5a-06).
- **Acceptance:** `Save` round-trip test for `athanor.settings.v1`; webtest extended: open Settings, switch VI and back, 0
  console errors, Begin/Journal unchanged; manual: pulse on Android Chrome, row hidden on desktop and iOS; screenshots.
- **Out of scope:** volume sliders (hidden), the pause menu (5a-13).

### 5a-12 Tutorial controller, How to play, Watch intro — `unity-gameplay-engineer`
- **Objective:** play the TutorialScript on the first run, with highlights, click locks, held popups and goal toasts, a one-time
  skip prompt, "How to play" on a throwaway journal/profile ending at 5.5, and an intro replay from the title.
- **Inputs:** tutorial-script devlog `:22-137`, `:149-163`; `Core/Tutorial.cs:29-109` (Seed, Offer, Depth1, CatalystTrial,
  Options, ShouldRun); `RunState.Deal/Preview`, `ScoreParts.cs`; `Save.cs:64-74` (IntroSeen, TutorialDone); `GameFlow.cs:101-168`;
  `HudView.cs:20` (Redraw pointer step 3.0); `SimpleCutscene.cs:110-140` (Intro, replay = same coroutine).
- **Acceptance:** EditMode tests: a fresh save runs the tutorial, `TutorialDone` set after 5.5, a skip sets it, How to play leaves
  the real journal/profile byte-identical; scores shown equal `TutorialTests` numbers (40 / 0 / 40+240 / 320); webtest clicks
  through the tutorial with 0 errors; a screenshot per step in EN and VI.
- **Out of scope:** changing `ShouldRun` (open question 9, rules-engineer if approved), tutorial text.

### 5a-13 Pause menu — `unity-gameplay-engineer`
- **Objective:** in-run pause per decision row 10 (button, Esc, Resume, Vibration, Quit to title with confirm, timeScale 0).
- **Inputs:** phase-5-prep.md row 10; `HudView.cs`; `GameFlow.cs:281` (a run counts only at the Ignite that ends it), `:521`.
- **Acceptance:** EditMode test: Quit discards the run, profile unchanged; screenshots 1x and phone scale; webtest 0 errors.
- **Out of scope:** language switch in a run.

### 5a-14 Depth intro line, window swap, rarity display — `unity-gameplay-engineer`
- **Objective:** show `depth.<n>.intro` under the depth name in the Descent, swap the window view and play its effect at the
  midway point, and implement the approved rarity display.
- **Inputs:** `SimpleCutscene.cs:146-194` (midway at `:169`), `GameFlow.cs:253-258` (Mood), `:476-482`; 5a-09 sprites;
  5a-03 mockup (e) and (f); `CardView.cs`, `CatalystChoiceView.cs`, `JournalView.cs`.
- **Acceptance:** screenshots of the 5 descents and the rarity states in EN and VI; `unity test Athanor --mode EditMode` 0 fail.
- **Out of scope:** wall backgrounds per depth, bench and slots.

### 5a-15 Rarity weights — `content-designer` (parallel, `content.json` only)
- **Objective:** set `rarityWeights` and show, bucket by bucket, that each rarity pays.
- **Inputs:** 5a-04 field; phase-4-prep.md Orient 4; BalanceMatrix baseline `phase-4-lore.md:247-266`.
- **Acceptance:** Core tests 0 fail; BalanceMatrix before/after quoted in full; a per-rarity row (or a RoleReport run) showing
  win rate with each rarity bucket; no row moves more than 3 points without a written reason.
- **Out of scope:** new items, Ultimates.

## Wave 3 (last)

### 5a-16 Sign-off — `qa-engineer`
- **Objective:** verify 5a end to end in EN and VI.
- **Acceptance:** `tools/ci/run.sh all` green; EditMode counts; BalanceMatrix numbers re-run; `node tools/web/webtest.mjs` and
  `tools/web/autoplay.mjs` 0 console errors; evidence folders for 5a-03, 07, 09-14 complete; a bug list returned as new briefs.

### 5a-17 Release build after 5a — `web-release-engineer`
- **Acceptance:** size vs 5a-01 with the 5a share of the +1 MB ceiling stated; release notes draft. Then `vault-librarian`
  runs `python3 tools/vault/build_vault.py` and the main session updates the devlog index.

---

# Phase 5b (each step behind an owner gate)

### 5b-01 HD-2D mockup — `ui-ux-designer` (parallel with 5b-02, 5b-03)
- **Objective:** a browser mockup of the approved HD-2D design for the owner.
- **Inputs:** `refs/2.5D Reference/` (2 images, 4 videos: Octopath-style), `docs/devlogs/phase-4-depth.md:89-199`,
  `docs/mockup/depth/` (old variants A/B/C), phase-5-prep.md 5b decisions.
- **Deliverable:** `docs/mockup/hd2d/index.html`: world at 1280x720 under a 640x360 HUD; tilt-shift along screen Y (strength,
  band); pre-blurred far and near layers; blurred foreground strip at the table edge; lit sprite fog behind and in front of the
  bench at low alpha; bloom and vignette per depth mood (5 presets); sway on/off; world-only shake. Toggles for each.
- **Acceptance:** 0 console errors; screenshots per depth preset in `production/qa/evidence/phase5/5b-01/`.
- **Owner gate G2.**

### 5b-02 Dust and ember loops mockup — `ui-ux-designer`
- **Objective:** a before/after page so the owner can judge looping emitters; **no code change**.
- **Inputs:** `Bench/Dust.cs`, `SimpleCutscene.cs:259+` (embers, streaks), `Fx/Sparks.cs:74-89`.
- **Deliverable:** `docs/mockup/loops/index.html` side by side, 30/60/144 Hz switch. **Owner review.**

### 5b-03 Endings and Worldseed lore frame — `lore-writer` (consult `narrative-director` if needed)
- **Objective:** a proposal for the world (beyond "dungeons") and the design questions for several equal endings.
- **Deliverable:** `docs/devlogs/phase-5-endings.md`: how Worldseeds differ and why each plants a different world, the
  questions below with options, no content or code. Questions to answer with the owner: how many Ultimates; what decides which
  one is made (recipe path, catalysts, last depth, Journal); does each need its own cutscene and art; does the journal track
  endings; how does it touch goals and unlocks.
- **Out of scope:** content.json, cutscene code.

### 5b-04 Layer art for the prototype — `pixel-artist` (after G2)
- **Objective:** pre-blurred far/near layers, the foreground table-edge strip and fog sprites, generated in `tools/art`.
- **Acceptance:** `node tools/art/export.mjs` writes them; a contact sheet in evidence.

### 5b-05 DepthTest prototype — `unity-gameplay-engineer` (after 5b-04)
- **Objective:** option 2 in `Assets/Scenes/DepthTest.unity`, built by an Editor menu item, without touching `Main.unity`.
- **Inputs:** depth devlog `:104-141`, `:181-199` steps 1-8; 5b-01 approved values.
- **Acceptance:** rest frame matches the bench (screenshot diff); Light2D, cookie and bloom work under perspective; clicks on the 9
  tiles by ray; a written HUD-resolution recommendation (640x360 vs 1280x720); option 1 fallback described if it fails.

### 5b-06 Measure the prototype — `web-release-engineer`
- **Acceptance:** WebGL build of DepthTest: size delta vs 5a-17 (against the remaining ceiling), frame time on a laptop GPU,
  shimmer check at 2x and 3x. **Owner gate G3.**

### 5b-07 Integrate — `unity-gameplay-engineer` (after G3)
- **Objective:** bring the approved layer to `Main`: camera split, `PointerInput` ray, HUD layer, cutscenes flat at z = 0.

### 5b-08 Sign-off and size — `qa-engineer`, then `web-release-engineer`
- **Acceptance:** as 5a-16/17; total growth over 5a-01 at most 1 MB.

## Notes, no tasks (Needs design)
- **Endless mode after depth 5:** the owner redesigns the structure. Old questions: scoring and difficulty growth; is the
  record saved; does it replace the ending or run beside it.
- **Currency:** no idea or purpose yet; needs design.
- **Sound:** a separate plan outside Phase 5.

## Open questions (with recommendation)
1. Atlases are already built (9 `.spriteatlasv2` + `AtlasBuilder`). "No atlas" = remove them, or just no further atlas work?
   *Recommend: keep them, no further work (download unchanged by removal; batching already tested).*
2. Rarity weights apply to the catalyst offer and event item rewards; are satchel starter copies also weighted, and does
   "tier" mean rarity (Common..Legendary)? *Recommend: rarity; offer and events only; satchel unchanged.*
3. The 7-scene storyboard (6 new art assets, 30 frames): when? *Recommend: 5b after G3, sharing `cellar_stair` with the Descent.*
4. Descent (depth devlog step 10) in 5b? *Recommend: no; Phase 6, after integration, with the stair art from 3.*
5. Depth intro "full" option (wall per depth): *Recommend: decide after 5b, since the HD-2D layers change the wall.*
6. Stale branch `feature/2.5D_test-phase-4` (no 2.5D work, tracked bin/obj): *Recommend: delete it; start `feature/phase5b-hd2d` from develop.*
7. `refs/` (~33 MB of video) is untracked: *Recommend: do not commit; add `refs/` to `.gitignore`.*
8. Shake moves the world only (proposed): *Recommend: yes.*
9. `TutorialScript.ShouldRun` skips for good once the journal has a recipe, so a player who quits after trial 1 never finishes:
   *Recommend: use the `TutorialDone` flag only, plus the skip prompt for saves with a non-empty journal (rules-engineer, test first).*
10. `quiet_room` now shows about a quarter as often (no-repeat rule): *Recommend: accept.*
11. Review the 11 section-7 decisions in phase-5-prep.md.
