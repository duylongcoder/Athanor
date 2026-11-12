# Athanor Phase 2: reactivity, combos and chain reactions

Date: 2026-11-12. Builds on `2026-11-11-athanor-design.md` (Phase 1, shipped: 8 starters, 31 discoveries,
1 ultimate, pair recipes only, 11 plan tasks done). Where this document is silent, Phase 1 still holds.
"My call" marks a decision the owner did not make; everything else was decided by the owner.

## Intent

Phase 1 is a pile of independent pair reactions: every neighbouring pair resolves once, in reading order, from
the layout the player placed. Phase 2 turns the bench into a small system the player has to reason about.
Where you put things, which item is hottest, and how many neighbours it has decide what happens, and one
reaction feeds the next. The thrill stays the same (a surprising result after a string of trials) but now the
last step of a trial can cascade across the bench.

Three mechanisms are one engine and must be built and balanced together: **Reaction Index**, **combos of 3 to
5 items**, and **chain reactions**. The rest (consumption, journal hints, depth targets, new content) supports
or follows from them.

## Goals

- Placement matters more than before: which tile holds the hottest item decides the whole trial.
- Big combos are rare, hard to set up and clearly worth it.
- A chain is readable: the player can watch it happen link by link and understand afterwards why it went that way.
- The rack of kept materials stops filling up, so the 12 slots are enough however much content exists.
- Hints are something the player can look back at.
- After the data is in, the game is balanced again with the bot simulation (see "Balance pass").

## Non-goals

- New screens, new catalyst slots, sound, mobile layout (as Phase 1).
- Changing the five-depth structure, trials, redraws or the satchel's starter handling.
- Rebuilding the art pipeline. New materials need icons the usual way (`tools/art/icons.mjs`).

## Anti-goals

- Reaction Index that simply follows tier. A tier-0 starter must be able to outrank a tier-4 material.
- A 5-item combo that is a normal part of every trial. It should be an event.
- A chain that resolves instantly into a total with no way to follow it.
- Hidden rules the player can never infer. Order and eligibility must be learnable from the bench and the Journal.

## Decided by the owner

### Reaction Index (the owner's "reaction stat")

- Every material has a Reaction Index (RI), an integer. It has the **highest priority** in resolving a bench.
- Values are **spread across the tiers and mixed up**, not rising with tier. Every tier, 0 to 4, has both
  low and high values. This makes working out the firing order part of the puzzle.
- The tile holding the highest RI on the bench is the **focal point (hub)** of the next reaction.

### Combos

- A recipe has 2 to 5 items (`items` list, unordered). Hard cap 5, which is the contact limit on a 3x3 bench:
  a hub plus its four orthogonal neighbours.
- Priority when resolving: **Reaction Index, then large combos, then small combos.** The highest-RI item is
  the hub; around it the largest matching recipe is tried first, then smaller ones.
- A big combo does not fire merely because its layout exists. If its hub is not the highest-RI item at that
  moment, or an earlier reaction has used up one of its items, it does not fire.
- **Required total RI.** Every recipe has a required total Reaction Index (`minReactivity`). The RIs of its items
  must add up to at least that. If they fall short, the recipe is treated as if it did not exist: no reaction,
  no special message. The same already holds for items with no recipe at all: placed side by side they make
  nothing. The hub then falls back to smaller recipes, and if none apply, those items simply don't react
  (an ordinary fizzle, with a possible hint).

### Chain reaction

- Resolution runs in rounds on the bench itself, not once over the starting layout.
- When a reaction fires, its items are consumed and the result appears **in the hub's tile**. The result then
  takes part in the next round, with its own RI, against whatever is left around it.
- This repeats until no reaction is possible. The materials made during the Ignite that are still on the bench
  then go to the rack (the owner's "queue").

### Consumption

- Materials on the bench are consumed by Ignite. The rack has 12 slots and content keeps growing, so
  materials may not sit unused in the rack.
- Starters (tier 0) are not consumed in the sense of being lost: they return to the satchel and come back as the
  hand refreshes (5 per turn, as Phase 1).
- Discoveries (tier 1 and up) placed on the bench are used up, whether or not they react. The risk is placing
  one and getting no reaction out of it. The Phase 1 rule "a fizzled discovery returns to hand" goes away.

### Hints

- A hint is stored on the Journal entry of the material it is about, and listed there for later reading. It
  stays in the Last Reaction panel as before.
- Once that material is fully unlocked (every recipe it takes part in has been found), its hints no longer show.
  Phase 1 already hides hints that point at nothing unfound; extend that to "all of this material's recipes found".

### Depth targets

- Geometric progression with ratio 5: **300, 1,500, 7,500, 37,500** for depths 1 to 4. Depth 5 has no target.
- Scoring must be tuned in the balance pass so these remain reachable (see below).

## My calls (tune freely; the builder check will list them again)

1. **Hub choice.** The hub each round is the highest-RI tile that has at least one matching recipe around it.
   A high-RI item with no recipe does not block the others. Ties in RI go to the earlier tile in reading order.
2. **Matching.** A recipe matches a hub when the hub's item is one of the recipe's items and the remaining
   items can be found on distinct occupied orthogonal neighbours of the hub. Neighbours need not all be used.
   Among matching recipes of the same size, prefer the one whose consumed neighbours have the higher total RI,
   then the earlier tile. Consequence: a corner hub has two neighbours, so it can host up to 3 items; an
   edge-middle hub up to 4; the centre up to 5. So 5-item combos only happen with the hub on the centre tile.
3. **Repeated items.** A recipe may use the same material up to twice (starters come in pairs).
4. **Consumption timing.** Placing is still reversible; the click on a placed card still takes it back. Items are
   consumed when Ignite is pressed.
5. **What goes to the rack.** Only materials *made during the Ignite* and still on the bench at the end. A
   discovery that was placed and never reacted is lost; a starter that was placed and never reacted goes back to
   the satchel. Full rack: the Phase 1 push-out rule (weakest, then oldest) stays.
6. **Scoring.** A reaction scores the result's base potency (Phase 1 table) times `(items - 1)`, so a pair is
   x1 and a 5-item combo x4; the "nth reaction in one Ignite is multiplied by n" chain rule and the x2
   first-discovery rule stay. Phase 1 catalysts keep their text; `CentreTwice` now scores a reaction whose hub or
   any consumed tile is the centre twice instead of firing twice. `Diagonals` makes diagonal neighbours count as
   neighbours (still max 5 items per recipe).
7. **Pre-empted combos.** If at the start of a round all items of a recipe are on the bench in a valid shape and
   meet its required total RI, but a different reaction fires first and breaks it, show a short "sputters" beat on
   those tiles and the line "Something burned hotter and spoiled a mixture." in Last Reaction. It names no recipe,
   is not a discovery, and does not score. (Falling short of the required total RI never shows this; that is
   simply no reaction.) Drop this call if you would rather have no message at all.
7b. **Required totals.** Set per recipe in the data, roughly rising with size so that 4 and 5-item combos need
   several high-RI items, not just the right ingredients. Validated: every recipe is satisfiable with the
   materials' RIs, and the path to the ultimate from starters meets every total along the way.
8. **Fizzles and hints.** After the chain has ended, every remaining pair of neighbouring items on the bench with
   no recipe fizzles and may give a hint (the Phase 1 hint rules). A hint never names a material. It may state
   that the partner is part of a group ("wants company of 3").
9. **Reaction Index is visible.** The Journal shows each discovered material's RI (e.g. flame pips) so the player
   can plan placement. Undiscovered materials show nothing. Without this the ordering would be unfair to learn.
10. **Content size (Phase 2).** About 12 new materials and about 14 new recipes: 7 three-item, 5 four-item,
    3 five-item (some recipes produce intermediates for others). A path to the ultimate must still exist from
    starters alone, and at least one route to it must use a combo. The two existing ultimate recipes may stay.
11. **Save compatibility.** The journal stores recipe keys as sorted ids joined with `+`; that already holds for
    pairs, so old saves load unchanged. `content.json` recipes move from `{a,b,result}` to `{items:[...],minReactivity,result}`;
    the loader accepts both until the old form is migrated.
12. **Bench visuals.** Links glow between a hub and its neighbours for recipes the player already knows. Each
    round plays one reaction, speeding up as in Phase 1. First-ever discoveries still get the full cutscene.

## Constraints

- Rules stay in plain C# under `Athanor/Assets/Scripts/Core/`, covered by tests that run with `dotnet test tools/CoreTests`
  and in Unity edit mode. Content stays data (`Athanor/Assets/Resources/content.json`).
- The release must still build for WebGL (`tools/web/webtest.mjs` must pass with 0 console errors).
- No commits unless the owner asks (see the progress ledger).

## What changes in the code (pointers for the planner)

- `ContentData`/`ContentDb`: `MaterialData.reactivity`; recipes as `items` lists; lookup by hub + neighbour set;
  validation (RI present and spread, recipe size 2 to 5, items known, path to ultimate from starters).
- `Ignite.cs` (`IgniteResolver`): replace the single reading-order pass with the round loop; `ReactionEvent`
  carries the hub tile, the consumed tiles, the item count and a pre-empted flag.
- `RunState.Ignite`: consumption rules above, what goes to the rack, `FillHand` unchanged for starters.
- `Journal`: hints per material, "fully unlocked" test, nothing else new in the saved data.
- `AutoPlayer` (the scripted player and the balance bot) must understand hubs, combos and ordering.
- Game layer: chain playback in `GameFlow`, bench links, the Journal entry layout (RI pips, hints list), and
  the new icons in `tools/art/`.

## Balance pass (after the content data exists)

The owner asked for balancing as the next step once the Phase 2 data is compiled. Use the existing balance
report test and bot simulation, extended for the new rules. Success criteria carried from Phase 1:

- An informed bot (knows every recipe and the firing order) wins most seeds (Phase 1 was about 82%; stay in
  the 60 to 85% range and report the number).
- An uninformed bot (places at random) wins rarely.
- Each depth target (300, 1,500, 7,500, 37,500) is reachable at its depth by the informed bot without relying on a
  single catalyst, and not trivially cleared on the first trial.
- Report how often 3, 4 and 5-item combos fire per run. A 5-item combo should be uncommon even for the informed bot.

What the builder may adjust to get there: the RI table, base potency table, combo multiplier, rack size, trials,
catalyst effects. What it may not adjust: the target ratio (5) or the rules in "Decided by the owner".

## Left to the builder

- The RI table, the new materials, names, kinds and recipes, within the sizes and rules above.
- Timings, easing and particle counts for the chain playback and the "sputters" beat.
- Journal layout details, the exact wording of new hints, and icon art.
- How to extend the bot.

## Success

- A trial with a hub on the centre tile and four prepared neighbours produces a 5-item combo, the result appears
  in the centre, and it reacts again with anything left around it.
- Swapping which item sits in the hub changes the outcome, and the Journal's RI pips explain why.
- A discovery placed without a reaction is gone; the rack never holds more than 12.
- A fizzle's hint can be re-read in the Journal entry of its material and disappears once that material is complete.
- Targets are 300, 1,500, 7,500, 37,500 and the balance report is within the criteria above.
- Rule tests pass; the web build loads with no console errors.
