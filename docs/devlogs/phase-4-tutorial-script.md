# Phase 4: Tutorial script

Status: **Script v2; owner decisions of 2026-11-13 recorded below.** Nothing is implemented. Part of [phase-4-prep.md](phase-4-prep.md); lore in
[phase-4-lore.md](phase-4-lore.md); build plan in `docs/superpowers/plans/2026-11-13-athanor-phase4-tutorial-lore.md`.
Scope (owner, 2026-11-13): first playthrough only; teaches **reactions**, the **Journal**, **Catalysts**; plays from a
**standardized save file** so every new player sees the same thing.

## What changed from the draft (v1), and why

v1 was checked against the Core code (`Ignite.cs`, `Chain.cs`, `RunState.cs`). Four things in it could not happen as written:

| # | v1 said | The code does | v2 |
|---|---|---|---|
| 1 | Trial 4 (redraw, free play to 300) | Trials 1 to 3 score 40 + 0 + 280 = **320**, so depth 1 is cleared by the chain in trial 3. Trial 4 is never reached | Trial 4 is cut. The chain clearing the depth is the payoff of the chain lesson. Redraw is not taught; it gets one pointer step, 3.0 (owner decisions 2 and 8) |
| 2 | "Nothing is spent by a miss" | Every Ignite spends a trial. The materials go back to the satchel | Line 2.3 says a trial was spent, and nothing else |
| 3 | "The result stayed on its tile and reacted again" | Sky Iron (index 3) stands on Ember Coal's tile, then **Moondust** (index 7) is the hub of step 2, so Starmetal Ore lands on Moondust's tile | Lines 3.3b and 3.4 follow the real order |
| 4 | Formula "times how many you used" | It is times **one less** than the materials used (a pair is x1) | Line 1.6b fixed |

Also new: aspects and events are held back until **depth 3** (v1: depth 1 only), because an event at the start of depth 2
would change the scripted catalyst hand. Catalyst lesson 5.4 now uses one reaction per catalyst.

## Shape

The tutorial is **the first run itself**: depth 1 and the first trial of depth 2, with scripted hands. It is not a separate
mode, so nothing has to be learned twice. After the catalyst is chosen and shown working, the run goes on as a normal run
(depth 2, random hands). Nobody can fail inside it: every hand and layout is fixed, so the scores are known in advance.

Plays once, after the opening cutscene, on a fresh profile. A "How to play" button on the title replays it on a throwaway
journal and profile, so a veteran's journal and goals are never touched. The replay ends at 5.5 and goes back to the title.

## The standardized save

| Part | Tutorial value | Why |
|---|---|---|
| Journal | empty | The Journal lesson needs a page that fills in front of the player |
| Profile | fresh: no goals, no unlocks, no stats | Same starting pool for everyone (`Unlocks.Fresh`) |
| Seed | fixed, `TutorialScript.Seed` | Same random hands after the script ends |
| Aspects, events | none before depth 3 (`RunOptions.QuietBefore = 3`) | Keeps the lesson to three ideas, and nothing touches the scripted hands |
| Hands | scripted, trial by trial (below), dealt with `RunState.Deal` | Each lesson needs one exact pair or chain on the bench |
| Catalyst offer | fixed for the first offer (`RunOptions.FirstOffer`) | Same three choices for everyone |
| Target | depth 1 = 300, as in the game | The script reaches 320 by trial 3 |
| Storage | `athanor.tutorial.v1`, written when 5.5 ends | A save without it and with a non-empty journal skips the tutorial |
| Goal toasts | held until 5.5 ends ("Reach depth 2" completes during the script) | Nothing on screen but the lesson |

## Script

Text is the on-screen line (the picture is 640x360, so one or two short lines). Every step highlights the thing it talks
about and **locks every other click** until the step is done. A click on the text box skips to the next line. The whole
tutorial can be skipped only by the title's "Skip tutorial" (asked once, before it starts).

Tiles are numbered 0 to 8 in reading order; 4 is the centre.

### Trial 1: lay, Ignite, reaction (reactions)

Hand: Ember Coal, Deep Tear, Iron Ore, Moondust, Bone Ash. Trials 4 → 3.

| # | Line | Player does | Done when |
|---|---|---|---|
| 1.1 | "This is the bench. Nine tiles. Materials go on them." | read | click |
| 1.2 | "Click Ember Coal, then the lit tile." (tile 4 lit; drag also works) | place Ember Coal | placed |
| 1.3 | "Put Deep Tear next to it. Neighbours react." (tile 5 lit) | place Deep Tear | placed |
| 1.4 | "Changed your mind? Click a placed material to take it back." | optional take back and re-place | both placed |
| 1.5 | "Ignite." (Ignite button pulses) | press Ignite | reaction plays |
| 1.6 | "A new material: Steam Wisp. The number is its potency, your score." Popup held, in parts: `Base 20`, `First time this run x2`, `Chain step x1` = **40** | read | click |
| 1.6b | "Potency: what you made, times one less than the materials used, doubled the first time, times the chain step." | read | click |
| 1.7 | "What you make is kept on the rack, ready for later." (rack highlighted) | read | click |

Recipe: `ember_coal + deep_tear -> steam_wisp` (tier 1, base 20).

### Trial 2: a miss, and the Journal (the Journal)

Hand: Glowmoss, Iron Ore, Sunbloom, Moondust, Bone Ash (rack: Steam Wisp). Trials 3 → 2.

| # | Line | Player does | Done when |
|---|---|---|---|
| 2.1 | "Not every pair reacts. Try Glowmoss beside Iron Ore." (tiles 3 and 4 lit) | place both | placed |
| 2.2 | "Ignite." | press Ignite | fizzle plays, hint shows |
| 2.3 | "Nothing reacted. The trial is spent, the materials come back. But a hint was left on Glowmoss's page." | read | click |
| 2.4 | "Open the Journal." (Journal button pulses) | open it | open |
| 2.5 | "Every reaction you find is written here, kept across runs. Steam Wisp is in it already." (Ember Coal's page) | read | click |
| 2.6 | "The number is the Reaction Index, 1 to 10. It decides who reacts first. Ember Coal is a 9." | read | click |
| 2.7 | "Glowmoss's page: it reacts with something Potion. The hint stays until you find it." (Glowmoss's page, hint highlighted) | read, close | closed |

Glowmoss must be on the lower tile (3): the hint goes to the first material of the pair in reading order. The hint is
the simplest unfound Glowmoss recipe: `deep_tear + glowmoss -> verdant_tonic`, so "something Potion". Glowmoss and
Iron Ore are never put together again in a scripted hand.

### Trial 3: chains (reactions, second half)

Hand: Iron Ore, Ember Coal, Moondust, plus Bone Ash and Sunbloom, which the script marks "not now". Trials 2 → 1.

| # | Line | Player does | Done when |
|---|---|---|---|
| 3.0 | "Redraw swaps the materials in your hand. Two per depth." (Redraw button highlighted; the counter above it shows 2) | read | click |
| 3.1 | "One reaction can set off the next. Lay Iron Ore, Ember Coal, Moondust in a row." (tiles 3, 4, 5 lit) | place three | placed |
| 3.2 | "Highest Reaction Index goes first. That is Ember Coal, 9." | read | click |
| 3.3 | "Ignite." | press Ignite | chain plays |
| 3.3b | Slow motion, one step per click, "Step 1" / "Step 2" over the hub tile. Step 1: Ember Coal + Iron Ore → Sky Iron on tile 4. Step 2: Moondust + Sky Iron → Starmetal Ore on tile 5 | watch | click per step |
| 3.4 | "Sky Iron was new, and it touched Moondust. Hot enough now, so they reacted too. That is a chain." | read | click |
| 3.5 | Both scores side by side, in parts. Step 1: `20 x2 x1` = **40**. Step 2: `60 x2 x2` = **240**. "Step 2 counts twice. Step 3 would count three times. Long chains are where the potency is." | read | click |
| 3.6 | "Without Moondust, the chain stops at step 1: 40 instead of 280." (the broken bench is shown greyed beside it; numbers from `RunState.Preview`, worked out before the Ignite, so nothing is spent) | read | click |
| 3.7 | Potency bar fills past 300 (320). The usual "Depth Cleared" descent plays. | watch | descent ends |

3.0 sits at the start of trial 3 because this hand holds two "not now" materials, the first time a player would want
new ones. It only points: the Redraw button stays locked like every other click, so no redraw is spent, the scripted hand
stays, and the counter still shows 2 when the depth clears.

Recipes: `ember_coal + iron_ore -> sky_iron` (tier 1), `sky_iron + moondust -> starmetal_ore` (tier 2, base 60, needs
heat 10: Moondust 7 + Sky Iron 3). Iron Ore and Moondust are two tiles apart, so `iron_ore + moondust -> quicksilver`
cannot fire.

### Catalyst offer (Catalysts)

Offer, fixed: **Bellows** (Ore reactions double), **Heart Tile** (reactions touching the centre double), **First Spark**
(first reaction of each Ignite triples). All three are open on a fresh profile.

| # | Line | Player does | Done when |
|---|---|---|---|
| 5.1 | "Depth cleared. Pick a catalyst. It helps for the rest of the run." | read | click |
| 5.2 | "Each one says what it does. Point at one to read it." | hover | any hover |
| 5.3 | "Take one." | choose | chosen |
| 5.4 | Descent to depth 2 (no event). The first hand is scripted for the choice (table below). "See the difference? That is your catalyst." Popup in parts with the catalyst's line | lay the lit pair, Ignite | reaction plays |
| 5.5 | "That is all there is to teach. Nothing is labelled from here. Mix, watch, remember." | read | click, then the normal run |

| Catalyst | Hand | Lay | Recipe | Without | With |
|---|---|---|---|---|---|
| Bellows | Ember Coal, Bone Ash, Glowmoss, Sunbloom, Deep Tear | Ember Coal 4, Bone Ash 5 | `ember_coal + bone_ash -> sulfur` | 40 | **80** (`Bellows x2`) |
| Heart Tile | Deep Tear, Moondust, Glowmoss, Sunbloom, Bone Ash | Deep Tear 4, Moondust 5 | `deep_tear + moondust -> frost_salt` | 40 | **80** (`Heart Tile x2`) |
| First Spark | Sunbloom, Moondust, Glowmoss, Deep Tear, Bone Ash | Sunbloom 4, Moondust 5 | `sunbloom + moondust -> pixie_wing` | 40 | **120** (`First Spark x3`) |

Depth 2 starts with 4 trials plus the 1 left from depth 1 (5). After 5.4 the player has 4 trials and 80 to 120 of the
1500 target, which is a normal start.

## Numbers

Measured by `TutorialTests` (2026-11-13), all as the script states: trial 1 = 40; trial 2 = 0 with the hint
"Glowmoss reacts with something Potion"; trial 3 = 40 + 240, hubs on tiles 4 then 5, depth 1 cleared at 320 with 1 trial
left; broken chain = 40; depth 2 first trial: Bellows 80, Heart Tile 80, First Spark 120. The same for 25 seeds.

## What I did not decide

1. The opening story's voice for the tutorial lines: kept plain on purpose (owner decision 5).
2. Whether the "How to play" replay should also show the opening cutscene. Proposed: no, it starts at 1.1.

## Decided by the owner (2026-11-13)

1. **The first run itself**, not a separate "Training Room". Approved.
2. **Extra first-seen notes:** none for aspects, events, goals or redraws (redraw gets one pointer step, see 8). Instead, the **chain and potency** lessons are
   stressed more: the score is broken into its parts, each chain step is shown on its own with a step counter, the
   chained and the broken-chain totals are compared (3.3b to 3.6), and the formula is stated once (1.6b).
3. **Fixed catalyst offer:** Bellows, Heart Tile, First Spark. Approved.
4. **Skip button** for returning players, asked once before the tutorial starts. Approved.
5. **Voice of the guide:** the plain neutral label, as drafted. Approved.
6. **Trial 4 is cut** (change 1). Approved.
7. **Aspects and events held back until depth 3** (`RunOptions.QuietBefore = 3`). Approved.
8. **Redraw:** no lesson. Redraw becomes its own button (today it is a click zone over the counter). The redraws-left
   counter sits directly above the button, and the button is greyed and cannot be clicked at 0. The tutorial points at it
   once (3.0), one short line, without spending a redraw. Built in the plan's Task 12.

## Next

- Core: the plan's Tasks 1 to 6 (scripted deal, fixed offer, quiet depths, preview, score parts, the script as data and
  its tests). Numbers go into this file once measured.
- Unity: Task 12, Redraw as its own button (needed before 3.0 can highlight it).
- Then the Unity side, in its own plan: a tutorial controller in `GameFlow` that drives the highlights, click locks and
  held popups, and the save key.
