# Phase 2: Reaction Index, combos and chain reactions

Dates: 2026-11-12. Spec: `docs/superpowers/specs/2026-11-12-athanor-phase2-design.md`.
Plan: `docs/superpowers/plans/2026-11-12-athanor-phase2.md` (10 tasks, all done).

## Goal

Make the bench a system to reason about. Three mechanisms are one engine and were built and balanced together:
Reaction Index, combos of 3 to 5 items, and chain reactions. Consumption, journal hints and new depth targets
support them.

## Decisions (owner)

- Every material has a Reaction Index; values are spread across tiers, not rising with tier.
- The highest-index tile is the hub. Priority: Reaction Index, then large combos, then small combos.
- A recipe has 2 to 5 items (the contact limit on a 3x3: hub plus four neighbours).
- A recipe has a required total index. If the items fall short, it counts as no reaction at all. The hub's other
  neighbours (bystanders not part of the recipe) count toward this total, so the same recipe can succeed in lively
  company and fail in dull company. This is called the **company rule**.
- Chain: a result appears in the hub's tile and keeps reacting until nothing can; survivors made this Ignite go
  to the rack.
- Everything placed on the bench is consumed; starters return to the satchel, discoveries do not.
- Hints are kept on the material's Journal entry and vanish once that material is complete.
- Depth targets grow geometrically with ratio 5: 300, 1,500, 7,500, 37,500.
- Balancing starts once the Phase 2 data exists.

## What shipped

- **Engine** (`Core/Chain.cs`, `Ignite.cs`, `RunState.cs`). Each round the occupied tile with the highest
  Reaction Index that has a recipe around it is the hub; ties go to the earlier tile. It takes the largest recipe
  its neighbours allow (corner 3 items, edge 4, centre 5). The items are consumed, the result stands in the hub's
  tile and competes in the next round. When nothing matches, leftover neighbours fizzle and may give a hint.
- **Scoring.** Base potency of the result (20 / 60 / 180 / 500 / 1,500 by tier) x (items - 1) x 2 the first time
  this run x the reaction's place in the chain, then catalysts.
- **Consumption.** Placed starters go back to the satchel, placed discoveries are gone, results still standing
  go to the rack (12, weakest pushed out, never the last substance).
- **Spoiled combos.** When a combo of three or more stood ready and a livelier reaction took one of its
  materials, its tiles flash and the note reads "Something burned hotter and spoiled a mixture."
- **Journal.** Each entry shows the material's Reaction Index (pips and number), its recipes including combos,
  and its open hints. A hint disappears once every recipe of that material is found.
- **Content.** 52 materials (8 starters, 43 discoveries, 1 last substance) and 59 recipes: 44 pairs, 7 with
  three items, 6 with four, 2 with five. Three roads to the last substance, one of them wholly through combos.
- **Screen.** Reactions play one at a time with the result in the hub tile; labels "N-COMBO", "CHAIN xN",
  "SPOILED"; the discovery cutscene gathers up to five materials.
- **Bot.** The scripted player now asks the same planner the game uses, so it cannot disagree with the rules.

## Balance pass

Measured with the balance report over 200 seeds (`BalanceReport` in `RunStateTests.cs`).

| Measure | Result |
|---|---|
| Player who knows every recipe (the bot) | wins 170 of 200 (85%), about 4 trials to spare |
| Player placing at random | wins 0 of 200; 188 end at depth 1 |
| Depth 1 cleared on the first trial | 6 of 200 |
| Losses of the informed player by depth 1 to 4 | 7 / 3 / 12 / 8 |
| Reactions per run with 2 / 3 / 4 / 5 items | 26.9 / 1.0 / 2.0 / 0.03 |
| Runs with a five-item combo | 5 of 200 |
| Informed player with one catalyst removed from the game | 155 to 174 of 200, whichever is removed |
| Informed player with no catalysts at all | 92 of 200; depth 3 is where most of those end |

What changed, and what did not:

- The four-item shortcut to Heaven's Ash was a five-item recipe when the content was first written. The bot
  used it in every win, so a five-item combo was routine. It lost Moondust and became a four-item recipe; the
  two remaining five-item recipes (Primal Bloom, Dream Spore) are now rare peaks.
- The base potency table was tried six ways and left as it was. The win rate did not respond to it (170 to 175
  wins whatever the table). Runs are decided by whether the tier-4 combos are assembled in time, not by a few
  points more or less.
- 85% is the top of the range the spec allows (60 to 85%; Phase 1 was 82%). Two rules make the game harder if
  that is wanted, and neither was applied: one redraw per depth instead of two gives 76.5%; carrying at most one
  unspent trial to the next depth gives 73.5%. Both also make depth 3 harder for a player with weak catalysts.

Two tests now guard the result: the informed player must win at least 32 of 40 seeds (measured 36), and may fire
a five-item combo in at most 8 of 40 runs.

## Decided by the owner: company rule

The required total Reaction Index could not fail in play if only the recipe's items counted. The **company rule**
makes placement a puzzle: the hub's other neighbours (bystanders not part of the recipe) count toward the total RI,
so `minReactivity` is checked against `sum(ingredient RIs) + sum(neighbour RIs)`. Now the same recipe succeeds in
lively company and fails in dull company. This is implemented in `ContentDb.Company()` and `ChainPlanner.MatchAt()`.
41 of 59 recipes have `minReactivity > 0`; 23 of them require company to reach their threshold.

## Rulings made while building

The ledger (`.superpowers/sdd/2026-11-12-athanor-phase2/progress.md`, not in git) has every one with its
reason. The ones that change what a player sees:

- Hub choice skips a high-index material that has nothing to react with, so it does not block the bench.
- A spoiled-combo message appears only for combos of three or more, so ordinary pairs competing do not spam it.
- A first hint points at the simplest unfound recipe of a material (pairs before combos).
- The note panel writes a pair in full and a combo as "<hub> + N around it"; the cutscene and the Journal carry
  the full recipe.
- Links on the bench follow the planned chain only as far as the player already knows the recipes.
- Results stay on their tile during playback and fly to the rack together at the end.
- The Journal grid went from 8x5 to 10x6 cells to hold 52 materials.
- Three combos make existing tier-4 materials from one tier-3 material plus lower ones. That is what makes a big
  combo worth the trouble.
- The Heart Tile card now reads "Reactions touching the centre tile score double." It used to fire the reaction
  twice, which a chain cannot do.
- The Journal's line under a starting material reads "STARTER" (was "STARTING MATERIAL", which no longer fitted
  beside the Reaction Index).

## Log

- 2026-11-12: brainstormed with the owner; wrote the design document; created this folder.
- 2026-11-12: plan written; Tasks 1 to 8 built (rules, bot, screen, Journal, content, icons).
- 2026-11-12: balance pass (Task 9); play-mode check of combos, chain, spoiled combo, cutscene and Journal;
  web build and browser check (Task 10).

## Verification

- Rule tests: `dotnet test tools/CoreTests` 145 of 145 pass. Inside Unity (edit mode): 151 pass, 2 skipped (the
  balance report and trace run, which only run when asked for).
- Balance report (200 seeds, after company rule added): informed bot wins 170/200 (85%, same as before); uninformed
  bot 0/200; 5-item combo in 6/200 runs; reactions per run by size: 26.79 / 0.64 / 1.94 / 0.04.
- Release web build (2026-11-12, re-checked after company rule): succeeded, 9.9 MB. Headless Chrome run with
  `tools/web/webtest.mjs`: 0 console errors, a first trial played with real mouse events, journal kept across a
  reload, a damaged save starts with an empty journal.
- Played in the editor at full speed with staged benches; screenshots in `phase-2-shots/`:
  - [a 3-item combo whose result stands in the hub tile](phase-2-shots/combo-3-result-in-hub.png), and went on
    to chain into Storm Glass;
  - [a spoiled combo](phase-2-shots/spoiled-combo.png);
  - [a 5-item combo on the centre tile](phase-2-shots/combo-5-centre.png);
  - [the discovery cutscene with four materials](phase-2-shots/cutscene-4-items.png);
  - [a Journal entry with its Reaction Index and hint](phase-2-shots/journal-hint-and-index.png).
- Not done: no second reviewer read the code (the same author reviewed it against the plan's checklist), and
  nothing is committed to git.
