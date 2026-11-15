# Phase 5 prep: owner decisions, scope 5a / 5b

Status: **5a signed off (2026-11-16)**, PASS with accepted gaps ([sign-off](../../production/qa/phase5a-signoff.md)); not committed. 5b not started. Plan: [2026-11-15-athanor-phase5.md](../superpowers/plans/2026-11-15-athanor-phase5.md).
Read first: [phase-4-prep.md](phase-4-prep.md) (Log 2026-11-15), the Phase 4 devlogs, [Phase 5 Kickoff](../../vault/Workflow/Phase%205%20Kickoff.md).

## State at the start (checked against the code, 2026-11-15)

- Plan `2026-11-13-athanor-phase4-tutorial-lore.md` Tasks 0 to 13 are all in the code (merged PR #1): Satchel.Take,
  RunState.Deal / Preview, QuietBefore, FirstOffer, ScoreParts, `Core/Tutorial.cs` (TutorialScript), lore texts and
  3 Lore events in content.json, the no-repeat rule (`GameFlow.cs:144`), the three-page intro (`SimpleCutscene.cs:110`),
  the tutorial save key (`Save.cs:11,71`), Redraw as its own button (`HudView.cs:20`). The checkboxes are now ticked.
- **Not built:** the Unity tutorial controller, "How to play" and "Skip tutorial" on the title, a replay of the intro.
- **Lang is not wired into the Game layer.** `Core/Lang.cs`, `lang/en.json` (234) and `lang/vi.json` (396) exist and are
  tested, but every view still writes English literals (e.g. `TitleView.cs:40-63`, `SimpleCutscene.cs:103-135,152`).
  Vietnamese is therefore not visible anywhere in the game yet. No `Core/Settings.cs`, no settings panel, no haptics.
- FMOD was never imported (no file in Assets, Packages or ProjectSettings). PrimeTween Demo is already deleted, the
  `InOut` to `Ease.InOutSine` change is already in `Motion`/ToRack, and `Assets/Plugins/PrimeTween` holds only the
  installer (the .tgz is in `Athanor/Packages/` only).
- `tools/CoreTests/bin,obj` are **not tracked on develop** (gitignored). They are tracked only on the stale branch
  `feature/2.5D_test-phase-4` (commit 98aa187f), which is older than develop and holds no 2.5D work.
- Core tests: 298 passed, 0 failed (run with `DOTNET_ROLL_FORWARD=Major`, this machine only has .NET 10; the project
  targets net8.0). No local `Athanor/Build`: the size baseline must be built first.

## Owner decisions (2026-11-15)

General
1. The repo is private: third-party packages and assets may be committed.
2. **FMOD is dropped** for good: remove it from Packages/Assets/Plugins (nothing to remove today; keep it out).
3. **Sound moves to a separate plan, outside Phase 5.** Music and cue notes in the lore stay notes.
4. **No atlas consolidation** (answer to phase-4-assets.md open question 1). See open question 1 in the plan.
5. **SparkMesh stays** (ParticleSystem costs about 264 KB of wasm).
6. Time-based trail emission: approved (already built: `Sparks.Trail`, `TrailRate = 60`).
7. The 4 pixelfx flipbooks (implosion, holy-pillar, collect-burst, poof): approved; export and ship.
8. Size cuts: remove the unused TMP font, the skybox and the splash.
9. **Size ceiling: +1 MB over the current ~10.7 MB for 5a and 5b together**; the 2.5D work counts in 5b. Measured with a
   release web build before and after.
10. PrimeTween: use the copy in `Packages/`, delete Demo, `InOut` becomes `Ease.InOutSine` (accepted).
11. Lore intro: all 11 proposed values in `phase-4-lore.md` (Owner input table) accepted and ticked.
12. Settings / localization open questions (section 7, 11 questions): the phase lead decides, below, with reasons, for the
    owner to review. Questions 9 (vibrate), 10 (pause menu on mobile) and 11 (mute on hidden tab) are approved for Phase 5.
13. **Font licence:** SVN-Determination Sans has no known licence; **blocks release**, the owner finds the source.
    Coder's Crux is CC BY 3.0 and needs a credit ("Coder's Crux by NAL").

Phase 5a (foundation and content): tutorial (Unity controller, Redraw already done, "How to play", skip for returning
players); intro and event lore verified and shown in Vietnamese; intro replay from the title; one line per depth on the
depth change, default version = the view outside the window changes plus an effect (full version, a new wall
background per depth, is an option only; bench and slots unchanged); rarity-weighted draws for items only
(materials/catalysts) by rarity tier, test-first, weights by content, BalanceMatrix quoted; a clearer rarity display;
pause menu, vibration, mute on hidden tab; settings and Vietnamese build (SVN fonts, key parity, glossary; item names
English; "The Keeper" unnamed); package cleanup and size cuts; flipbooks.

Phase 5b (visuals and new design, owner approves each step): HD-2D depth layer by option 2 in a `DepthTest` scene,
world rendered at >= 2x (1280x720) with the HUD resolution decided after; tilt-shift along screen Y in one pass (no depth
buffer, the table is flat) plus pre-blurred far/near sprites from tools/art; one blurred foreground strip at the table
edge; bloom and vignette per depth mood; **fog as lit sprites, over the background and drifting in front of the bench at
low alpha (owner's choice)**; camera sways only; shake moves the world only (proposed); cutscenes stay flat at z = 0 at
first; option 1 (parallax + line-scroll table) is the fallback; option 3 (3D renderer) dropped. Order: HTML mockup in
`docs/mockup` → owner → prototype → WebGL measure → owner → integrate. Dust/ember loops: one before/after HTML mockup
only, no code before review. Several Ultimates, each its own ending, **no true/good/bad ending: equal endings that
depend on the Worldseed the player makes**; lore first (the world is dungeons today), brief and questions only.
Notes, no tasks: endless mode after depth 5 (owner redesigns), currency (no purpose yet), sound (other plan).

## Owner answers to the open questions (2026-11-15, "agree with recommendations")

| # | Decision |
|---:|---|
| 1 | Atlases: keep what is built, add no more. |
| 2 | "Tier" is the rarity. Weights apply to catalyst offers and event reward items only; the satchel is unchanged. |
| 3 | The 7-scene intro storyboard assets (6 new, 30 frames) are made in 5b after gate G3, sharing `cellar_stair` with the Descent. |
| 4 | Descent is not in 5b; it moves to Phase 6. |
| 5 | The full per-depth intro (wall background swap) is decided after 5b. Default stays the simple window-scene change. |
| 6 | Branch `feature/2.5D_test-phase-4` is to be deleted and `feature/phase5b-hd2d` created from develop (done by the owner or on their explicit command; not yet done). |
| 7 | `refs/` (about 33 MB of video) is not committed; added to `.gitignore`. |
| 8 | Screen shake moves the world only. |
| 9 | `ShouldRun` relies only on the `TutorialDone` flag, plus a Skip prompt for saves with a non-empty journal (`rules-engineer`, test first). |
| 10 | `quiet_room` appearing about 1/4 as often under the no-repeat rule is accepted. |
| 11 | The 11 Settings answers in the next section are accepted. |
| 12 | ~~SVN-Determination Sans licence unknown~~ Superseded at G1: the font is deleted from the project (see Gate G1). |

## Gate G1 (owner, 2026-11-15)

Approved: all 8 mockup items (title row 3, Skip box, Settings, Pause, Tutorial overlay, Rarity, depth intro line, 16 keys + credit), and every open question as proposed: How to play / Watch intro stay on the title; "rotate device" layer added in 5a for portrait phones; "Sổ tay vẫn được giữ" only after the engineer confirms the journal writes on discovery; Vietnamese title keeps y 332 / 346; Descent text positions checked against the code at 5a-14.

Changed by the owner:
- **Rarity, Journal:** list and detail both put the rarity mark at the **top-left**. List cell: **one star** in the rarity colour. Detail page: **all n stars plus the rarity name**. Catalyst offer and hand cards: approved as drawn. (SPEC change 24 rewritten; the mockup frames still show the old dots until `ui-ux-designer` redraws them.)
- **Fonts:** SVN-Determination Sans is **deleted** (ttf + otf + metas removed from `Athanor/Assets/Art/Fonts/`). The release blocker is gone. Coder's Crux (CC BY 3.0) stays and keeps its credit.
- **Build profile:** `Web - Desktop - Development.asset` reverted to the committed version.
- **Baseline:** cold-load time is measured as a second baseline (5a-01 follow-up).
- **Ceiling:** 1 MiB. 5a+5b together must stay <= **11,824,672 bytes** (10,776,096 + 1,048,576).
- **Lore:** "seeds" stays at depth 5; the night-sky patch in the window stays.

Consequences / open questions created by the font decision (not decided):
- A (resolved: Crux for all four vi roles, owner agreed). Vietnamese Title/Name no longer has a font. Recommendation: use Coder's Crux for all four roles in vi (Crux has no en dash; LangTests already avoids it). Crux is wider, so Title buttons and HUD rows need a re-check in the screenshot pass.
- B (resolved: font file removed, SvnDet gone). `docs/mockup/phase5-ui/f/svn-determination.ttf` and `style.css` still load the deleted font for the vi frames. Remove the file and point `SvnDet` at Crux when the mockup is redrawn.
- C (resolved: 7x7 `star` sprite in tools/art/ui.mjs). Star glyph: the owner said stars; the mockup uses 7x7 round pips. Recommendation: draw a 7x7 star sprite (`tools/art`), same count rules.

## Settings and localization, section 7: decided by the phase lead (owner to review)

| # | Question | Decision | Why |
|---:|---|---|---|
| 1 | Who writes Vietnamese, which voice | `localizer-vi`, plain modern Vietnamese, "bạn" for the player, as in vi.json today | 396 entries already use that voice (8.4); changing it now means retranslating |
| 2 | Translate names? Logo? | Material, catalyst, aspect names stay English (owner decision 8.1.3); event and depth names are translated; the "Athanor" logo stays Alagard | Already the rule in vi.json and LangTests; depth/event names are story text |
| 3 | Different pixel font in Vietnamese | ~~Option C~~ Superseded at G1: Determination Sans is deleted. Title/Name font in vi is open (see Gate G1, open question A) | Was: both cover 134/134 letters |
| 4 | Forced capitals in Vietnamese | Keep them (as English); grow the 10 px HUD rows to 12-13 px; a string that still overflows in the screenshot pass switches to normal case | Same look in both languages; the risk is measured, not guessed |
| 5 | Default language | Follow the browser/device (`Application.systemLanguage`, else `navigator.language` via .jslib); English otherwise; saved choice wins | A Vietnamese player sees Vietnamese on first launch without finding Settings |
| 6 | Number format in vi | "1.500" in Vietnamese (`Wording.Number` per language), "1,500" in English | Goal texts already say "1.500"; mixing both on one screen is worse |
| 7 | Settings button or gear; move RESET | A fourth title button "Settings"; RESET PROGRESS moves into the panel | No new art; a 120x26 button is a safe tap target on phones; reset is rare and dangerous. Begin (320,227) and Journal (320,257) do not move (webtest) |
| 8 | Default volumes, Ambience bus | Data keeps music 7, sfx 8; **no Ambience field** (FMOD dropped); volume rows hidden until sound exists | Sound is outside Phase 5; a slider that does nothing is a bug report |
| 9 | When to vibrate (approved for Phase 5) | Discovery 20 ms, depth cleared 40 ms, run won or lost 60 ms; nothing on clicks or Ignite; only where `Haptics.Supported`; the toggle pulses once when turned on | Rare, meaningful moments; frequent pulses tire and drain battery |
| 10 | Pause menu in a run (approved) | Yes, on all platforms: a small pause button on the HUD, Esc on desktop. Resume, Vibration, How to play (disabled in run), Quit to title (confirm). `Time.timeScale = 0` while open. Language stays title-only (5.3). Quit discards the run uncounted, like closing the page | One flow everywhere; relabelling mid-run is the risky path 5.3 avoids; an abandoned run must not farm or lose goals |
| 11 | Mute when the tab is hidden (approved) | Yes: `AudioListener.pause` on `visibilitychange` (.jslib) and `OnApplicationPause/Focus`; built now as a hook with a test, audible once sound lands | Browsers expect it; cheap now, forgotten later |

## Owner answers to the briefs audit (2026-11-15, "all as phase-lead recommends")

Source: [2026-11-15-athanor-phase5-briefs.md](../superpowers/plans/2026-11-15-athanor-phase5-briefs.md), Open questions 1 to 9.

| # | Decision |
|---:|---|
| 1 | Rarity weights: set **B `[4,3,2,1]`**. `content-designer` measures B first (W2-03) and writes it only if `InformedPlayer_WinsMostSeeds` stays in 24..31 and no BalanceMatrix row moves more than 3 points without a written reason. |
| 2 | If B does not keep the target: weights stay all 1 (set A, field absent). The target range is not widened. Revisit after 5b. |
| 3 | The skip prompt's "Skip" starts the run at once (one click). |
| 4 | "Play it" keeps playing the tutorial in the throwaway sandbox (How to play); the real save does not change. |
| 5 | Rotate-device layer is HTML/CSS in the WebGL template (shown while loading, two strings picked by `navigator.language`). |
| 6 | Window views stay in `tools/art/export-window.mjs`, called from `export.mjs` so one command rebuilds all art. |
| 7 | The seven `2026-11-1*.txt` session transcripts leave the repo: moved to `production/session-logs/` (gitignored), `/20??-??-??-*.txt` ignored at the root; the six that were tracked are untracked (`git rm --cached`, staged, not committed). |
| 8 | PrimeTween licence: fine while the repo is private; note in `CREDITS.md`, decide again before any public release. |
| 9 | Commit split: the 8 commits of section 4 of the briefs file, `content.json` after the weights are settled. **Approved as a plan only; nothing is committed until the owner says so.** |
| 5a | Rotate layer trigger (owner, after `ui-ux-designer` found that `max-width: 900px` blocks portrait tablets and narrow desktop windows, the layer having no dismiss button): `(orientation: portrait) and (max-width: 639px) and (pointer: coarse)`. 639 px is where the 640x360 canvas drops below 1x. |
| 4a | Hand-card tooltip tier word: a brighter rarity colour for the tooltip only (the SPEC dark tone is about 2.4:1 on the dark panel). One deliberate deviation from SPEC change 24. |
| 4b | Hand-card tooltip keeps its body line; the panel grows 12 px for the star row and tier word. *Note: this question and 4c reached the owner with garbled text; both answers were the recommended option, so they stand but can be changed.* |
| 4c | Journal detail shows stars and tier word only for discovered starting materials (the only ones with a rarity); crafted, unknown and locked materials show none. List cell: one star only when discovered. No Legendary material exists in `content.json`, so a Legendary Journal frame cannot be shot from real content. |
| 5b | Rotate layer font: the template's system monospace, no embedded font (an extra Crux file would be about 80 KB against the 11,824,672 B ceiling). |

## Owner answers to the remaining Phase 5 questions (2026-11-15, "all as recommended")

| # | Decision |
|---:|---|
| R1 | Rarity weights: set A stands (`rarityWeights` absent, every rarity weighs 1). B `[4,3,2,1]` measured 35/40 against the 24..31 target and is not used. Rarity is display only (stars, tier word) in Phase 5; revisit after 5b. The target range is not widened. |
| R2 | 4b and 4c stand as written above (tooltip keeps its body line and grows 12 px; Journal shows stars and tier word only for discovered starting materials). |
| R3 | Branch and commits: the 8-commit plan and the branch change (`feature/phase5b-hd2d` from develop, old `feature/2.5D_test-phase-4` deleted) are done only after `tools/ci/run.sh all` is green and the 5a-14 vi evidence exists, and only on the owner's explicit command. Nothing is committed or branched by this answer. |
| R4 | Legendary Journal frame: not shot, no fake data and no new material added for a screenshot. The gap is recorded in the 5a-14 evidence; shoot it when a Legendary material exists in `content.json`. |
| R5 | Verification work still open, no owner decision needed: vi run of `tools/web/descents.mjs` for 5a-14, re-check of the 09:20 `ArgumentOutOfRangeException`, wasm+data size after the effect sheets are imported (headroom 1,562,611 B), and the missing VI shots of W2-06. |

## Log

- 2026-11-15: Phase 5 planned (5a: 17 tasks in 4 waves; 5b: 8 tasks behind 3 owner gates). Decisions above written
  before any task relies on them. The 11 lore intro values ticked in phase-4-lore.md; the Phase 4 plan's checkboxes
  ticked after checking the code.
- 2026-11-15 (Wave 0, 5a-02): each depth has one intro line (`depth.1..5.intro` in en.json) and a window-scene note for the
  pixel-artist; the 19 events match the lore table. Detail: [phase-5-lore.md](phase-5-lore.md). `LangTests` now fails on
  `Vietnamese_HasEveryUiKey` (vi.json lacks the 5 new keys): expected until 5a-08 adds them, 22/22 pass without the en.json change.
- 2026-11-15, 5a-01 size baseline (web-release-engineer, release build, no code change; tree has the lang/en.json edit of the lore task and the unchanged-at-build font file, neither affects bytes):
  `unity build Athanor --target WebGL --execute-method WebBuild.Build --allow-dirty-build` (55 s, 0 errors). Bytes from `ls -l Athanor/Build/web/Build`:
  `web.wasm.unityweb` 7,315,806; `web.data.unityweb` 3,334,398; `web.framework.js.unityweb` 74,060; `web.loader.js` 48,779; Build/ total 10,773,043;
  with index.html (3,053) 10,776,096 bytes (~10.28 MiB, Unity log "10 MB"). Against phase-4-assets: wasm -1,826, data -1,201, no growth.
  Build Report (uncompressed, `Athanor/Logs/build-WebGL-1794690038861.log`): Textures 14.8 MB (90.2%), Shaders 495 KB, Other 819.7 KB, headers 79.7 KB, Fonts not listed, total 16.4 MB. Largest: UI.spriteatlasv2 4.0 MB, built-in "Splash Screen Unity Logo" 2.7 MB (candidate for 5a-06).
  `webtest.mjs`: 0 console errors (577 messages, 25 screenshots), load 1.5 s on the localhost second visit (cache present).
  Ceiling for 5a+5b together: baseline 10,776,096 + 1 MiB (1,048,576) = **11,824,672 bytes**. (1 MB = 1,000,000 would be 11,776,096; the stricter is the latter.)
  Side effect: the build rewrote `Athanor/Assets/Settings/Build Profiles/Web - Desktop - Development.asset` (webGLDataCaching 0 to 1, webGLCompressionFormat 2 to 1); not reverted, owner to decide.
- 2026-11-15 (Wave 0, 5a-01/5a-03): baseline release build 10,776,096 B (cap 11,776,096). Mockup docs/mockup/phase5-ui/ (SPEC.md, 25 changes, 16 new keys) awaits owner at gate G1.
- 2026-11-15 (G1): owner approved the mockup with the Journal rarity change and deleted SVN-Determination Sans; build profile reverted; ceiling is 11,824,672 B (MiB). Open: vi Title/Name font, star glyph, mockup redraw.
- 2026-11-15 (Wave 1, owner agreed Gate G1 A/B/C and opened Wave 1):
  - 5a-04 done: optional `rarityWeights` [Common, Uncommon, Rare, Legendary] in content.json; absent = identical per seed; BalanceMatrix 12 rows identical to baseline; RarityTests 9. `content.json` numbers are 5a-15 (owner approves values first).
  - 5a-05 done: `Core/Settings.cs` + SettingsTests (13). 5a-08 done: vi for 5 depth intros, 15 new keys, ui.title.fonts; Crux only; LangTests 22/22. Core tests 320 pass.
  - 5a-07 done: 4 sheets exported (holy-pillar 1.18 MB, implosion 418 KB, ~1.8 MB together); `star` sprite. Unity import and in-game screenshots belong to Wave 2. 61 tracked PNGs drift from generator output: left as committed.
  - 5a-06 done: PrimeTween installer, Liberation TMP font and Unity splash removed; release build 10,262,061 B (-514,035 vs baseline; headroom to 11,824,672 is 1,562,611). Cold-load baseline: 1.5 s on localhost (coarse; equals warm). webtest 0 errors. Dev build profile reverted again.
  - Mockup redrawn (stars, Crux), 80 screenshots in production/qa/evidence/phase5/5a-03/.
  - Open: PrimeTween licence says no redistributing the tgz in derivative products; repo is private (G decision 1) so fine now, revisit if it goes public. Sheet size (1.8 MB) must fit the ceiling: wasm+data +1.8 MB uncompressed vs 1.56 MB headroom, check after import. lint errors in Wave 1 files sent to rules-engineer.
- 2026-11-15 (Wave 2, 5a-09): five window views `window_d1..d5.png` and `window_drip` from `tools/art/export-window.mjs`; contact sheet in evidence/phase5/5a-09.
- 2026-11-15 (Wave 2, 5a-10): `Game/Support/Loc.cs` wires `Lang` into every view; Crux for all vi roles; LocTests (glyph coverage of vi.json and en.json in the Crux atlas, item names English, number format per language). VI shots of event, catalyst choice, run over, intro still missing (W2-06).
- 2026-11-15 (Wave 2, 5a-11): Settings panel on the title, `athanor.settings.v1` round trip, Haptics (Android only), AudioMute on hidden tab via `Athanor.jslib`; skybox off (`SceneBuilder.cs:30`, `Main.unity`). webtest part D: Settings, VI and back, kept after reload. One `ArgumentOutOfRangeException` in the 09:20 webtest log, to re-check (W2-06).
- 2026-11-15 (Wave 2, 5a-12): TutorialController (highlights, click locks, held popups, goal toasts), skip prompt for a journal with entries, How to play on a throwaway journal/profile, Watch intro. Decision #9 still lives in the Game layer (`TutorialRules.PlanFor`); moved to Core by W2-02.
- 2026-11-15 (Wave 2, 5a-13): pause menu (button, Esc, Resume, Vibration, Quit with confirm, timeScale 0); Quit discards the run uncounted; the journal is stored after each Ignite (`GameFlow.cs:416-417`), so "Your journal is kept" is true in code (test W2-05).
- 2026-11-15 (Wave 2, 5a-14): depth intro line under the name in the Descent, themed window per depth in a normal run (not tutorial, not How to play), Endless keeps view 5. Rarity stars per G1 not yet built (W2-04).
- 2026-11-15 (Wave 2, 5a-15): `rarityWeights` [4,3,3,3] was put in content.json without owner approval: fresh informed player 35/40 (target 24..31). RarityReport at 400 runs per item: Common 72.0%, Uncommon 74.6%, Rare 73.7%, Legendary 86.1% win rate when held. Replaced by the owner's pick, set B (W2-03).
- 2026-11-15 (audit): `tools/ci/run.sh all` red on lint (DepthLookTests names); Core 318/320. Fix briefs W2-01..08 in [2026-11-15-athanor-phase5-briefs.md](../superpowers/plans/2026-11-15-athanor-phase5-briefs.md). Owner answered its 9 questions as recommended (table above). Wave 2-fix A dispatched: W2-01/02 `rules-engineer`, W2-03 `content-designer`, W2-07 step 1 `ui-ux-designer`.
- 2026-11-15 (clean-up, owner Q7): seven session transcripts moved to `production/session-logs/`, six untracked, root pattern ignored. Not committed.
- 2026-11-15 (Wave 2-fix A): W2-01/02 done (`rules-engineer`): lint clean; `TutorialScript.Plan` and `TutorialPlan` in Core, `ShouldRun(bool tutorialDone)`; Core 325/326, the one failure being `InformedPlayer_WinsMostSeeds` (W2-03). The `[Obsolete]` 2-arg `ShouldRun` stays until W2-04 removes it.
- 2026-11-15 (W2-04/05, `unity-gameplay-engineer`): Game layer calls `TutorialScript.Plan` (Game `TutorialPlan`/`PlanFor` removed, Core `[Obsolete]` 2-arg `ShouldRun` removed); "Skip" breaks to the run at once, "Play it" stays the sandbox (owner Q3, Q4), tests in `TutorialFlowTests`. W2-05: `GameFlow.SettleIgnite` (`run.Ignite()` then `Save.StoreJournal`, skipped in the sandbox) is the single place Resolve writes the journal; `Quit_AfterADiscovery_JournalInStorageHasIt` proves a discovery is in storage before Quit, so `ui.pause.quit_body2` and `ui.title.erase_keep` are true. Rarity stars (SPEC 21-24) are coded (`Game/Support/RarityStars.cs`, hand card, catalyst offer, tooltip, Journal list and detail) but **not yet seen in game**: no screenshots, no webtest, `tools/ci/run.sh all` not run. EditMode 415 tests, 407 pass, 1 fail (`RealContent_RarityWeights_AreValidAndPositive`, content side), 1 inconclusive (opt-in report), 6 skipped.
- 2026-11-15 (W2-04 evidence, `unity-gameplay-engineer`): rarity stars seen in game. 24 frames (hand card, catalyst offer, hand tooltip, Journal list, Journal detail Common and Uncommon; EN and VI, 1x and 2x) in `production/qa/evidence/phase5/5a-14/rarity/`, all from the dev build of 17:05 (`web-dev.wasm`, same time as `RarityStars.cs`). Fixed while looking: star sprite was distorted on a half pixel (now a top-left pivot sprite at whole pixels), catalyst tier word was about 8 px low (top edge `y+160`), hand cards of starter materials showed no tooltip so SPEC 23 had no effect (tooltip added, anchored at the card top). Tooltip tier word uses the brighter rarity colour (owner 4a). Known and accepted: the VI tier word on the catalyst strip sits about 2 px above the mock (Crux font bias). Not shot: Rare Journal detail (Lightning Sand is locked on a fresh profile), Legendary (no Legendary material exists), Uncommon tooltip (two stars). The offer frames come from the tutorial step, so a tutorial box covers part of the title. webtest run 1: 0 console errors but exit 1 from the random-event flake `e10` (a popup hid the Continue button); run 2 stopped by hand, so no clean webtest yet. `unity test` not yet re-run after the stars and tooltip code.
- 2026-11-15 (W2-07, rotate layer): spec change 26 in `docs/mockup/phase5-ui/SPEC.md`; keys `ui.rotate.title/body` in en.json and vi.json (LangTests 22/22); layer built in `WebGLTemplates/Athanor/index.html` (+3,508 B, 3,268 to 6,776 B, no font or raster), trigger `(orientation: portrait) and (max-width: 639px) and (pointer: coarse)`, system monospace. `node tools/web/rotate.mjs <out>` (8 viewport cases, 0 console errors; shown at 360x800 and 390x844 portrait touch, hidden on landscape, 768x1024 tablet, 500x800 and 1280x720 desktop) and `node tools/web/rotate_sync.mjs` (template table equals the two Lang keys, NFC) both pass; run by hand, not yet in CI. Not tested: a real Unity build, real Android/iOS monospace fonts (W2-08). Evidence: `production/qa/evidence/phase5/5a-rotate/`.
- 2026-11-15 (W2-03, content-designer): rarity weights measured at 200 seeds against the Phase 4 baseline. A (field absent) reproduces the baseline on all 12 rows and gives 28/40 on `InformedPlayer_WinsMostSeeds`; B `[4,3,2,1]` gives 35/40 (target 24..31) and moves 7 rows more than 3 points (fresh +4.5, fresh+all catalysts -10.0, fresh+aspects+events +5.5, fresh+all catalysts+aspects+events -11.5, full+aspects+events -4.0, fresh events only +7.5, full -3.5); C `[6,3,2,1]` gives 28/40 but moves 6 rows (-5.0, -5.0, -4.0, -12.0, -4.5, +4.0). Per owner Q2, `rarityWeights` is removed from content.json (all 1, content.json identical to HEAD). Revisit after 5b.
- 2026-11-15 (W2-03 follow-up, `rules-engineer`): `RealContent_RarityWeights_AreValidAndPositive` renamed `RealContent_RarityWeights_AbsentIsUniformOrPresentIsValid`; it accepts an absent field (every rarity weighs 1) or a present one (length 4, each >= 1). Core 326/326, lint 0 errors, `check_repo` 0 problems. `InformedPlayer_WinsMostSeeds` range untouched (A gives 28/40). Rarity weights therefore have no effect in the game for now; stars are display only.
- 2026-11-15 (owner, "all as recommended", delta2 open questions): Q2 the ~78% full-profile win rate under set A is accepted for 5a (tune after 5b). Q3 the rotate layer does not pause the game (no timed mechanic). Q5 `rotate_sync.mjs` joins CI as its own stage of `tools/ci/run.sh all`; `rotate.mjs` stays manual (needs a browser).
- 2026-11-15 (R4 widened): the Rare Journal detail (locked on a fresh profile) and the Uncommon hand tooltip join the Legendary Journal frame as accepted unshot rarity frames.
- 2026-11-15 (CI, owner approved): `tools/ci/run.sh core` sets `DOTNET_ROLL_FORWARD=Major` by default (overridable) because this machine has only .NET 10 and installing .NET 8 needs sudo. Same tests, same `MIN_TESTS`. `tools/ci/run.sh all` green: 71 python tests, unity-lint clean, check_repo 0, CoreTests 327/327.
- 2026-11-15 (5a-14 evidence): `tools/web/descents.mjs` got a 20 s per-command timeout, socket-close rejection and a hard deadline (seconds + 240) after a run hung 1.5 h. 300 s runs against the dev build photographed depths 1-2 in EN (`5a-14/en2/`) and VI (`5a-14/vi/`), 19 files each, 0 console errors. Plan for the rest: [delta3](../superpowers/plans/2026-11-15-athanor-phase5-delta3.md).
- 2026-11-15 (owner, delta3 open questions): (1) option A: one `descents.mjs` run per language at 720 s (hard deadline 960 s), EN then VI; any depth still missing is an accepted gap. (2) For R3, VI d1-d2 plus whatever option A adds is enough 5a-14 VI evidence.
- 2026-11-15 (D-1, `unity-gameplay-engineer`): EditMode 417 / 411 pass / 0 fail / 0 inconclusive / 6 skip (new `ChoiceHookTests`, `AutoHookTests`, written first). webtest exit 0, 419 messages, 0 errors, 129 shots; the 09:20 `ArgumentOutOfRangeException` did not reproduce (cause unknown, no fix). Flake e10: card clicked before the deal finished; dev-only `[cards] open` hook (`CatalystChoiceView.cs:29,162`) and `webtest.mjs` waits for it. Descents stalled because F9 is a blind toggle: `GameFlow.ToggleAuto()` logs `[auto] on|off`, `descents.mjs` sets the state and checks it. 720 s runs: EN and VI photographed d1-d4 (`5a-14/en3`, `vi3`), 0 errors; d5 is the accepted gap. 5a-10 has the 8 EN/VI frames. SPEC (f) code values match; EN within 1.5 px of the mock; VI depth name about 11 px high, DEPTH n and intro about 3 px high. Build profile reverted to HEAD (M-3). `tools/ci/run.sh all` green.
- 2026-11-16 (owner): fix the VI depth-name offset in 5a; accept the 2-3 px VI offsets, the tight VI catalyst body lines and the high VI button labels as known gaps, revisit in 5b. The VI scripted run won at depth 4 (Worldseed made, "4/5"): `rules-engineer` checks, read only, whether a win before depth 5 is the intended rule.
- 2026-11-16 (`rules-engineer`, read only): making the Ultimate wins at any depth (`RunState.cs:219,249-250`; pinned by `RunStateTests.MakingTheUltimate_WinsAtAnyDepth`), as the Phase 1 ruling says (`phase-1-mvp.md:19`), against the Phase 1 plan (`2026-11-11-athanor.md:210`) and the lore/README reading. BalanceMatrix unchanged today; the average won run ends near depth 4.
- 2026-11-16 (owner): **keep the win at any depth in 5a** (no Core change, no retune); revisit "depth 5 only" with the lore in 5b. The won run-over screen gets its own depth line instead of "Depth reached 4/5" (new en/vi keys by `lore-writer` and `localizer-vi`, `RunOverView.cs:55` by `unity-gameplay-engineer`); the lost screen keeps "n/5".
- 2026-11-16 (won-screen keys): `ui.runover.planted_at` "Planted at depth {n}" (`lore-writer`, en.json:111) and "Gieo hạt ở tầng {n}" (`localizer-vi`, vi.json:109); check_repo LANG ok. VI is about 155% of "Tầng đã tới": overflow to be checked on the won screenshot (fallback "Gieo ở tầng {n}").
- 2026-11-16 (D-1b, `unity-gameplay-engineer`): VI depth name was high because TMP Top alignment puts the ascent line at the box top while the mock's CSS line box adds half-leading; Crux's short ascent makes that about 11 px at 48. Opt-in `Label.LineBox` adds `Make.HalfLeading` for Crux in VI; only the Descent name opts in (`SimpleCutscene.cs:54`). `LabelLineBoxTests` 5 tests, red 3/5 first, then 5/5. Measured VI d2 130.0..166.0 vs mock 132.0..165.5; EN unchanged 131.0..173.0 (`5a-14/vi4`, `en4`, 0 errors).
- 2026-11-16 (D-2 part): `FlipbookImporter.Import` rewrote the 4 sheet metas (frames 42/53/51/49 match the shrunk json); `holy-pillar.asset` rewritten, the other three `.asset` byte-identical so their 09:05 timestamp stays: accepted, content verified (195 frame refs resolve). `ArtLibrary.asset` drops sheet frames from `sprites` as `SceneBuilder.RefreshLibrary` intends. In-game shots and the full EditMode run still to do. Build profile reverted again (M-3).
- 2026-11-16 (as recommended): the same VI half-leading offset on "DEPTH n", the intro line and the 32 pt "DEPTH CLEARED" banner (about 7 px) is a known gap for 5a; apply `LineBox` more widely in 5b after a `ui-ux-designer` pass.
- 2026-11-16 (won screen, `unity-gameplay-engineer`): `RunOverView.ShowDepth(won, depth)`: won binds `ui.runover.planted_at` on row 0 with an empty value, lost restores `ui.runover.depth_reached` + "n of 5"; both directions reset on the reused view and survive a language change. `RunOverDepthTests` 4 tests, red 3/4 first, then 4/4. Won screens EN and VI in `5a-10/run_over_won_{en,vi}.png` (720 s descents, both won at depth 4, 0 errors); "Gieo hạt ở tầng 4" fits (about 104 of 272 px), no fallback needed. Lost path covered by tests only. EditMode full 426 / 420 pass / 0 fail / 0 inconclusive / 6 skip.
- 2026-11-16 (5a-07 in game, partial): `5a-07/ingame/` has collect-burst and holy-pillar (just after the burst); poof and implosion not yet caught. `autoplay.mjs` stalls on this build (blind F9 toggle, like descents did).
- 2026-11-16 (D-3, `web-release-engineer`): release build `WebBuild.Build` (61 s, 0 errors; gzip `web.*.unityweb`, no Development option). Bytes: data 3,235,095; framework 74,558; loader 48,779; wasm 7,359,292; Build/ 10,717,724; index.html 6,561 (built; the 6,776 B figure is the template before Unity's macro processing; the built file is the number that counts). **Total 10,724,285 B, headroom to 11,824,672 B is 1,100,387 B for 5b** (+462,224 B vs 5a-06, the effect sheets). Textures 32.1 MB uncompressed (96%); holy-pillar 9.5 MB and implosion 6.1 MB are the first cut candidates if 5b needs room. `rotate.mjs` (template with stub loader, not the built page) 67 ok, 0 failures. `webtest.mjs` on the release build stops at `1_1_bench` (needs dev-only hooks) after a 3.8 s load with 0 console errors. Evidence `5a-17/`. Build profile reverted (M-3).
- 2026-11-16 (owner, release-gate): the GitHub release-gate builds `WebBuild.Build` and then runs `webtest.mjs`, which needs the dev-only hooks, so it would fail on the first tag. Fix: build both, the release build for the size budget and a load smoke (0 console errors), and `WebBuild.BuildDev` for the full `webtest.mjs`; no check removed. Size budget tightened from `WEB_BUDGET_MB=60` to a byte check: Build/ + index.html <= **11,824,672 B** (`docs/CI.md`).
- 2026-11-16 (release-gate built, main session): `.github/workflows/ci.yml` release-gate now builds `WebBuild.Build` and `WebBuild.BuildDev`; the release build gets the byte budget (`WEB_BUDGET_BYTES` 11824672, Build/ + index.html, local sum 10,724,285 B) and the new `tools/web/loadsmoke.mjs` (loaded = `#unity-canvas` present and `#loading` removed, 0 console errors, 20 s per command, hard deadline); the dev build gets the full `webtest.mjs`. `docs/CI.md` rows updated. Smoke watched both ways locally: release build exit 0 (loaded 4.7 s, 61 messages, 0 errors); nothing served exit 1 ("nopage"). The workflow itself cannot run here (needs Unity secrets); YAML parses. `tools/ci/run.sh all` green.
- 2026-11-16 (5a-07, `qa-engineer`): `ingame/implosion_reaction_cutscene.png` caught by turning the scripted player off before a hand Ignite (the scripted player calls `reaction.Skip()` every 0.45 s in dev builds, which jumps past the burst). Poof (fizzle) and the `autoplay.mjs` blind-F9 fix are still open.
- 2026-11-16 (5a-07 done, `qa-engineer`): `ingame/poof_fizzle.png` (depth 2, "Sunbloom + Glowmoss / Nothing happens", grey puff between tiles, no +N) accepted although three back-to-back fizzles overlap and the source is a q20 JPEG. 5a-07 in-game set: collect-burst, holy-pillar, implosion, poof. `autoplay.mjs` now uses the state-checked `setAuto` (from descents.mjs) and turns the scripted player on at the title, so a fresh profile skips the tutorial (it sat on the first tutorial prompt otherwise).
- 2026-11-16 (D-4 / 5a-16 sign-off, `qa-engineer`): **PASS with the accepted gaps**, checklist `production/qa/phase5a-signoff.md`. Re-run: CI all exit 0; BalanceMatrix 12 rows identical to the Phase 4 baseline (BalanceMatrix is Explicit: run with its own filter); `InformedPlayer_WinsMostSeeds` 28/40; EditMode 426/420/0/0/6; loadsmoke on the release build exit 0 (4.8 s, 61 messages, 0 errors, kept in `5a-16/loadsmoke/`). Accepted gaps: d5 descents, Legendary / Rare Journal detail / Uncommon tooltip rarity frames, VI 2-3 px offsets. The autoplay proof (exit 0, 78 messages, 0 errors, depth 2-4) is kept in `5a-16/autoplay/` by the main session. The Plugins files of M-4 are already staged (unknown stager), which is the state M-4 wants.
- 2026-11-16 (D-5 / 5a-17, `web-release-engineer`): D-3 release build reused (nothing in Assets newer except the dev build profile). `5a-17/bytes.txt`: total 10,724,285 B; 5a used 0 B of the 1 MiB allowance (51,811 B under the 5a-01 baseline); 5b has 1,100,387 B to the 11,824,672 B ceiling. Load 4.8 s (loadsmoke, 5a-16). Draft player notes in `CHANGELOG.md` (new file; main session corrected Settings / pause / rotate / rarity lines against this log). CREDITS has Coder's Crux (CC BY 3.0) and the PrimeTween note. **Version string not set: owner decides.**
- 2026-11-16 (owner decision #6, executed): local branch `feature/phase5b-hd2d` created from develop at `00b84e3e` (not checked out; the uncommitted 5a work stays on develop); local `feature/2.5D_test-phase-4` deleted (`b3cbb508`, 0 commits not in develop). The remote `origin/feature/2.5D_test-phase-4` still exists and the new branch is not pushed: both wait for the owner. 5a is still uncommitted, so 5b work should not start on the new branch until 5a is committed to develop and the branch is updated.
- 2026-11-16 (open): files were staged mid-wave by something other than the agents (webtest.mjs, CatalystChoiceView.cs, ChoiceHookTests.cs, part of GameFlow.cs, the build profile). No repo hook or script runs `git add`; source unknown. Re-check the index before any commit.
