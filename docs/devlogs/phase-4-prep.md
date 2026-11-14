# Phase 4 prep: OODA re-check of the content

Status: **Draft for owner review.** Nothing is implemented or decided. Written 2026-11-13 after Phase 3 was built.
Read first: [phase-3-handoff.md](phase-3-handoff.md), [phase-3-unlocks-aspects-events.md](phase-3-unlocks-aspects-events.md).

Method: a re-check of the content in four steps, Observe, Orient, Decide, Act. Each step ends with what is still open.

## Observe: what Phase 3 leaves on the table

- Phase 3 shipped: unlocks and goals, aspects, 16 depth events, 5 starters, 24 discoveries, 28 recipes. Core tests 245/245,
  EditMode 256 passed and 6 skipped. Release web build 10 MB, 0 console errors in headless Chrome.
- Balance (200 seeds, informed bot): fresh profile 67% wins, full profile 85%. A random player wins 0 of 200 on both.
- The parking lot (`phase-3-plan.md`, "Phase 4 parking lot") holds three items, all decided and none planned:
  1. Endless mode after depth 5, with a target that grows without limit.
  2. Story and framing: lore per depth, cutscenes.
  3. Several Ultimates, each with its own ending.
- The Phase 3 owner list flags five open points (`phase-3-unlocks-aspects-events.md`, "Open for the owner").

## Orient: what the observations mean for Phase 4

1. **Newcomer gap is the first problem.** A player who learns recipes from the journal needs hundreds of runs to reach the
   last substance. An informed player needs one or two. Phase 4 adds more Ultimates and endless play, and both assume a
   player who gets that far. Fixing the gap comes before adding length. (Owner point 1.)
2. **Goal numbers are unverified.** The informed player has about 19 of 22 goals after 40 runs. "Complete the journal" needs
   all 87 recipes, including those behind locked starters. Re-tune before endless mode, which depends on goals.
   (Owner point 2.)
3. **Balance outliers.** Cold Draft at depths 3, 4 and 5 of one run halves the bot's wins. Seven or more cards in hand cut
   long names until the card is pointed at. (Owner points 3 and 4.)
4. **Rarity claim is not measured.** The spec's "each rarity pays" check was not run bucket by bucket. Only "every catalyst"
   against "none" was measured (85% against 67%). Phase 4 should not rely on rarity tiers until this is checked.
   (Owner point 5.)
5. **Depends on Phase 3 systems.** Endless targets, Ultimates and endings all use the profile, goal and event systems that
   Phase 3 built. Nothing in Phase 4 can be designed without the Phase 3 numbers settled.

## Decide: Phase 4 focus (set by the owner, 2026-11-13)

Phase 4 has two main tasks for now:

1. **Tutorial (new).** Teaches a new player how to play, and is the first answer to the newcomer gap in Orient point 1.
2. **Lore content.** Story and framing: lore per depth, and the world the Ultimates belong to.

Owner direction (2026-11-13): guidance and lore focus entirely on the first playthrough.

- **Tutorial:** detailed instructions on the mechanics: reactions, the Journal Book, and Catalysts. Runs from a standardized
  save file made for the tutorial, so every new player gets the same starting state.
- **Lore:** the current opening cutscene is sufficient, but expand it to give a broader context for the story. Where
  possible, adjust the in-game events so they match the lore, for narrative cohesion.

Deferred for now, not dropped: endless mode after depth 5, and multiple Ultimates with their own endings. The
Orient points 2 to 4 (goal numbers, balance outliers, rarity check) are not Phase 4 tasks, but the tutorial's
guidance depends on the recipe and goal numbers, so they should be checked before the tutorial text is final.

Out of Phase 4 scope unless the owner says otherwise: mobile layout, currency, rarity-weighted draws (all parked in the
Phase 3 spec).

Added by the owner (2026-11-13, round 2): a Redraw button with its counter; a title Settings panel (music, SFX, vibration,
language EN/VI); asset work (atlases, particles, PrimeTween motion); sound design with FMOD (postponed in round 3);
environment light (AERO, replaced in round 3).
Research notes, not yet plans: [phase-4-settings-localization.md](phase-4-settings-localization.md),
[phase-4-packages.md](phase-4-packages.md), [phase-4-assets.md](phase-4-assets.md); cutscene storyboard in
[phase-4-lore.md](phase-4-lore.md).

Owner, round 3 (2026-11-13): FMOD and all sound work are **postponed** (licence questions); AERO is replaced by a sprite fog
layer idea; one JSON file per language (en, vi), Vietnamese first for content text while item, catalyst and aspect names stay
English; fonts SVN-Determination Sans and SVN-Coder's Crux to test; atlases by use (UI, Background, Item Icon, Catalyst Icon...)
and one spritesheet per animation; PrimeTween kept under Assets/Plugins, Demo removed, no .gitignore changes; time-based trails;
Dust and cutscene streams as loops; 2.5D depth layers to be tried as a mockup first. Size cleanup (unused font, skybox,
splash) is out of scope.

## Act: next steps before any Phase 4 code

1. Owner decides the tutorial's shape: a guided first run, journal hints, or both. Still open: the lore text
   (how much, and where it is shown: the opening cutscene, depth intros, or journal entries).
2. Design the tutorial save file: fixed profile, bench, starting materials and discoveries, so the tutorial plays the same
   every time. Plan the lore expansion of the opening cutscene, then list the depth events that need changing for coherence.
3. Run the existing reports for the newcomer baseline, so the tutorial has a measured starting point:
   `cd tools/CoreTests` then `dotnet test --filter BalanceMatrix -l "console;verbosity=detailed"`, and
   `ATHANOR_TAUGHT=4 ATHANOR_PLAYERS=12 ATHANOR_RUNS=600 dotnet test --filter RoleReport -l "console;verbosity=detailed"`.
   Save the output in this devlog.
4. Write the Phase 4 spec in `docs/superpowers/specs/` covering the tutorial and the lore, then a task plan.

## Log

- 2026-11-13: OODA re-check drafted from the Phase 3 handoff and devlog. No code changed, no reports run yet.

- 2026-11-13: Newcomer baseline before the lore pass. BalanceMatrix (pouch mode 2, 200 runs per row):
  - fresh                                        wins 132/200 (66%), avg depth 3,76, lost at 1..5: 7 4 23 33 1
  - fresh + all catalysts                        wins 164/200 (82%), avg depth 3,81, lost at 1..5: 7 3 10 16 0
  - fresh + all starters                         wins 134/200 (67%), avg depth 3,83, lost at 1..5: 4 3 25 34 0
  - half                                         wins 164/200 (82%), avg depth 3,91, lost at 1..5: 3 1 11 21 0
  - full                                         wins 166/200 (83%), avg depth 3,87, lost at 1..5: 4 2 10 18 0
  - fresh +aspects+events                        wins 135/200 (67%), avg depth 3,88, lost at 1..5: 4 2 28 31 0
  - fresh + all catalysts +aspects+events        wins 170/200 (85%), avg depth 3,93, lost at 1..5: 4 3 7 16 0
  - fresh + all starters +aspects+events         wins 145/200 (72%), avg depth 4,05, lost at 1..5: 0 1 19 34 1
  - half +aspects+events                         wins 163/200 (81%), avg depth 3,99, lost at 1..5: 0 1 15 21 0
  - full +aspects+events                         wins 170/200 (85%), avg depth 4,00, lost at 1..5: 0 2 8 20 0
  - fresh, aspects only                          wins 144/200 (72%), avg depth 3,90, lost at 1..5: 3 3 25 25 0
  - fresh, events only                           wins 128/200 (64%), avg depth 3,77, lost at 1..5: 6 4 27 35 0
  RoleReport (taught 4, 12 players, 600 runs):
  - NEWCOMER: first Ultimate at run: median 371, mean 332,5, fastest 175, slowest 474, quartiles 240 / 443; never within 600 runs: 6/12
  - NEWCOMER: all runs: win 9%, average depth reached 3,26, losses by depth 1..5: 1% 10% 59% 29% 0%, stuck runs 0
  - NEWCOMER TOLD TIER<=4: first Ultimate at run: median 184, mean 155,4, fastest 23, slowest 466, quartiles 47 / 205; never within 600 runs: 4/12
  - NEWCOMER TOLD TIER<=4: all runs: win 23%, average depth reached 3,57, losses by depth 1..5: 1% 5% 48% 44% 1%, stuck runs 0
- 2026-11-15: Plan `2026-11-13-athanor-phase4-tutorial-lore.md` done: Deal, QuietBefore, FirstOffer, Preview, ScoreParts,
  TutorialScript with TutorialTests; event lore pass and 3 Lore events (Vietnamese updated); Lore events not shown twice
  in a row; intro in three pages; tutorial save key; Redraw as its own button. Core tests 298 passed; Unity EditMode
  331 total, 325 passed, 6 skipped. Screenshots in `phase-4-shots/`. Open for the owner: what `ShouldRun` does when a
  new player quits mid-tutorial (it is skipped for good once the journal has a recipe); `quiet_room` now shows about a
  quarter as often (the no-repeat rule turns most of its draws into no event); the 3-point balance rule cannot resolve
  an event-pool change at 200 runs per row (see phase-4-lore.md). Next: the Unity tutorial controller plan.
