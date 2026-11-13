# Phase 4: Tutorial script (draft)

Status: **Draft for owner review.** Nothing is implemented. Part of [phase-4-prep.md](phase-4-prep.md).
Scope (owner, 2026-11-13): first playthrough only; teaches **reactions**, the **Journal**, **Catalysts**; plays from a
**standardized save file** so every new player sees the same thing.

## Shape

The tutorial is **the first run itself**: depth 1 and the first trial of depth 2, with scripted hands. It is not a separate
mode, so nothing has to be learned twice. After the catalyst is chosen and shown working, the run goes on as a normal run
(depth 2, random hands). Nobody can fail inside it: a trial that scores too little is given again.

Plays once, after the opening cutscene, on a fresh profile. A "How to play" button on the title replays it on a throwaway
copy of the save, so a veteran's journal and goals are never touched.

## The standardized save

| Part | Tutorial value | Why |
|---|---|---|
| Journal | empty | The Journal lesson needs a page that fills in front of the player |
| Profile | fresh: no goals, no unlocks, no stats | Same starting pool for everyone |
| Seed | fixed | Same catalyst offer, same hands |
| Aspects, events | off for depth 1 | Keeps the lesson to three ideas; both get a one-line note the first time they appear (open question 2) |
| Hands | scripted, trial by trial (below) | Each lesson needs one exact pair or chain on the bench |
| Target | depth 1 = 300 as in the game; the last trial is topped up by the script if short | Never a dead end |
| Storage | `athanor.tutorial.v1` key, written when the catalyst lesson ends | An old save without it and with a non-empty journal skips the tutorial |

## Script

Text is the on-screen line (the picture is 640x360, so one or two short lines). Every step highlights the thing it talks
about and **locks every other click** until the step is done. A click on the text box skips to the next line; no step is
skippable as a whole except by the title's "Skip tutorial" (asked once, before it starts).

### Trial 1: lay, Ignite, reaction (reactions)

Hand: Ember Coal, Deep Tear, Iron Ore, Moondust, Bone Ash.

| # | Line | Player does | Done when |
|---|---|---|---|
| 1.1 | "This is the bench. Nine tiles. Materials go on them." | read | click |
| 1.2 | "Click Ember Coal, then the lit tile." (drag also works) | place Ember Coal | placed |
| 1.3 | "Put Deep Tear next to it. Neighbours react." | place Deep Tear beside | placed |
| 1.4 | "Changed your mind? Click a placed material to take it back." | optional take back and re-place | placed again |
| 1.5 | "Ignite." (Ignite button pulses) | press Ignite | reaction plays |
| 1.6 | "Both are used up. A new material was made: Steam Wisp. Potency is your score." (the score popup is held on screen and broken into parts: "Base", "First time this run x2", "Chain step x1") | read | click |
| 1.6b | "Potency = what you made, times how many you used, doubled the first time, times the chain step. Remember the last part." | read | click |
| 1.7 | "What you make is kept on the rack, ready for later." (rack highlighted) | read | click |

Recipe used: `ember_coal + deep_tear -> steam_wisp`.

### Trial 2: a miss, and the Journal (the Journal)

Hand: Glowmoss, Iron Ore, Sunbloom, Moondust, Bone Ash (rack: Steam Wisp).

| # | Line | Player does | Done when |
|---|---|---|---|
| 2.1 | "Not every pair reacts. Try Glowmoss beside Iron Ore." | place both | placed |
| 2.2 | "Ignite." | press Ignite | nothing reacts, a hint shows |
| 2.3 | "Nothing happened. But you learn from it: a hint is left on Glowmoss's page." | read | click |
| 2.4 | "Open the Journal." (Journal button pulses) | open it | open |
| 2.5 | "Every reaction you find is written here, kept across runs. Steam Wisp is already in it." (page of Ember Coal, entry shown) | read | click |
| 2.6 | "The number is the Reaction Index (1 to 10). It decides who reacts first. Ember Coal is a 9." (index highlighted) | read | click |
| 2.7 | "The hint stays until you have found everything that material does." (hint highlighted) | read, close | closed |

Nothing is spent by a miss, but the pair is not repeated: the tutorial never puts a dud pair in a later hand.

### Trial 3: chains (reactions, second half)

Hand: Iron Ore, Ember Coal, Moondust, plus two fillers (Bone Ash, Sunbloom) the script marks "not now".

| # | Line | Player does | Done when |
|---|---|---|---|
| 3.1 | "One reaction can set off the next. Lay Iron Ore, Ember Coal, Moondust in a row, Ember Coal in the middle." | place three in a row | placed |
| 3.2 | "Ember Coal has the highest index, so it goes first." | read | click |
| 3.3 | "Ignite." | press Ignite | Iron Ore + Ember Coal -> Sky Iron, then Sky Iron + Moondust -> Starmetal Ore |
| 3.3b | Slow motion: each step is played one at a time. The tile of the step glows and a step counter ("Step 1", "Step 2") appears over it. | watch | click per step |
| 3.4 | "The result stayed on its tile and reacted again. That is a chain." | read | click |
| 3.5 | The two scores are shown side by side with the parts broken out: Step 1 = base x1, Step 2 = base x2. "Step 2 counts twice. Step 3 counts three times. Long chains are where the potency is." | read | click |
| 3.6 | "Now break the chain: take Moondust off first and Ignite once. Then you see how much less it is." Optional replay, undo-able once, shows the same bench without the second reaction beside the chained total. | optional | click |

Recipes used: `ember_coal + iron_ore -> sky_iron`, `sky_iron + moondust -> starmetal_ore` (needs reactivity 10: 3 + 7).
The row puts Iron Ore and Moondust two tiles apart, so only the Ember Coal route is available.

### Trial 4: redraw and finish the depth (practice)

| # | Line | Player does | Done when |
|---|---|---|---|
| 4.1 | "You need 300 potency before the trials run out." (target and trials counters highlighted) | read | click |
| 4.2 | The scripted hand is a weak one. "A bad hand? Redraw swaps it. You get two a depth." (button top right) | press Redraw | new hand |
| 4.3 | "Now make what you like. A long chain pays most. The Journal will tell you what you have tried." The potency bar fills with each Ignite, and a chain of 3 or more shows its step numbers again. | free play, Journal open to anyone | target reached |

Fallback: if the player spends the trials without 300, the script says "Close. Try that trial again." and restores it.

### Catalyst offer (Catalysts)

Offer, fixed: **Bellows** (Ore reactions double), **Heart Tile** (centre-tile reactions double), **First Spark** (first
reaction of each trial triples). All three are unlocked from the start.

| # | Line | Player does | Done when |
|---|---|---|---|
| 5.1 | "Depth cleared. Pick a catalyst. It helps for the rest of the run." | read | click |
| 5.2 | "Each one says what it does. Point at one to read it." (tooltip) | hover | any hover |
| 5.3 | "Take one." | choose | chosen |
| 5.4 | Descent to depth 2. Trial 1 hand is scripted to use the choice: Bellows gets two Ore reactions, Heart Tile gets the centre, First Spark gets a first-reaction setup. "See the difference? That is your catalyst." (score popup shows the bonus) | one Ignite | reaction plays |
| 5.5 | "That is all there is to teach. Nothing is labelled from here. Mix, watch, remember." | read | click, then the normal run |

Recipes for 5.4 are picked per catalyst from the existing starters, and tested like the rest.

## What I did not decide

1. Exact potency per step. Checking each hand and layout needs a scripted test in Core, written once the owner approves the
   shape. No number above is measured.
2. Goals, aspects, events, redraws and the rack are touched only in passing. A one-line first-seen note for aspects and
   events (depth 3 offers) is proposed, but not scripted here.
3. The tutorial's lines sit in the opening story's voice only if the lore pass says so; they are plain on purpose.

## Decided by the owner (2026-11-13)

1. **The first run itself**, not a separate "Training Room". Approved.
2. **Extra first-seen notes:** none for aspects, events, goals or redraws. Instead, the **chain and potency** lessons are
   stressed more: the score is broken into its parts, each chain step is shown on its own with a step counter, the
   chained and the broken-chain totals are compared (3.3b to 3.6), and the formula is stated once (1.6b).
3. **Fixed catalyst offer:** Bellows, Heart Tile, First Spark. Approved.
4. **Skip button** for returning players, asked once before the tutorial starts. Approved.
5. **Voice of the guide:** the plain neutral label, as drafted. Approved.

## Next

- Core test: for each trial, the scripted hand and layout give the recipe, the chain and the step scores written above, and
  a catalyst-specific second-depth hand for each of the three catalysts. Numbers go into this file once measured.
- Then the Unity side: a tutorial controller in `GameFlow` that drives the highlights and click locks, and the save key.
