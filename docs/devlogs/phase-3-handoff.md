# Phase 3 handoff

Last updated: 2026-11-13, after the last pass. **Phase 3 is built.** Read the devlog first:
`docs/devlogs/phase-3-unlocks-aspects-events.md`.

- Spec: `docs/superpowers/specs/2026-11-12-athanor-phase3-design.md`. Section 16 ("As built") is right wherever it
  differs from the sections before it.
- Owner decisions: `docs/devlogs/phase-3-plan.md`.
- There was no task plan and no ledger; the spec's section 15 was the order of work.
- Nothing is committed (not asked for). Everything is staged or in the working tree.

## State

- `dotnet test tools/CoreTests` → 245/245. `unity test ./Athanor --mode EditMode` → 256 passed, 6 skipped.
- Release web build in `Athanor/Build/web` (10 MB), checked in headless Chrome with `tools/web/webtest.mjs`:
  0 console errors.
- Autoplay build in `Athanor/Build/web-dev` (`WebBuild.BuildDev`, F9 = scripted player), driven by
  `tools/web/autoplay.mjs`.
- Balance, informed bot, 200 seeds, with aspects and events: fresh 135 (67%), full 170 (85%); random player
  0 of 200 on both.

## Reports (none runs unless asked for)

    cd tools/CoreTests
    dotnet test --filter BalanceMatrix -l "console;verbosity=detailed"     # win rate by profile
    dotnet test --filter EventReport -l "console;verbosity=detailed"       # what each event costs; the random player
    ATHANOR_TAUGHT=4 ATHANOR_PLAYERS=12 ATHANOR_RUNS=600 \
      dotnet test --filter RoleReport -l "console;verbosity=detailed"      # two players' careers (takes a while)
    dotnet test --filter "FullyQualifiedName~BalanceReport" -l "console;verbosity=detailed"   # the Phase 2 report

## Open for the owner

The devlog's last section has them with their numbers. The first is the one that matters for Phase 4: a player
who learns the recipes from the journal takes hundreds of runs to reach the last substance, while one who knows
them makes it in the first run or two.
