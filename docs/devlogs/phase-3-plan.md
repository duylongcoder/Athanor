# Phase 3: Unlocks, Aspects and Depth Events (plan draft)

Spec: `docs/superpowers/specs/2026-11-12-athanor-phase3-design.md` (written 2026-11-12).
Status: **Approved for spec (owner, round 3: "ok all").** Nothing is implemented. Drafted 2026-11-12 from the owner's
brainstorm.
Next step after approval: spec in `docs/superpowers/specs/`, then a task plan in `docs/superpowers/plans/`.

## Goal

Phase 2 made one run deep. Phase 3 makes the game worth **starting again**: each run should differ in what is
available (unlocks), what the bench does (aspects) and what happens on the way down (events). The player's long-term
progress is shown as goals, not as a shopping list.

Four pillars:

| # | Pillar | Gist |
|---|---|---|
| A | Achievements and unlocks | Goals with progress bars. Rewards are random unlocks (catalysts, starters) named only by rarity. |
| B | Aspects (bench modifiers) | Pick 1 of 3 at run start, depth 3, depth 5; run-long, act on bench tiles. |
| C | Depth events | A random event at the start of each depth, after the aspect. Large content pool. |
| D | Recipe tree growth | New materials and recipes, including those for the new starters. |

Out of scope, parked for Phase 4: see the last section.

## A. Achievements and unlocks

### Rules agreed

- Unlock conditions are not only "reach depth N". They also use **run count** and **ignite count**, and anything else
  the game can measure cheaply (see the stat list below).
- The screen shows the **goal and the progress**. The reward line names only the **category and rarity**, never the
  item: "Unlocks a new Uncommon starter", "Unlocks a new Rare starter", "Unlocks a new Uncommon catalyst",
  "Unlocks a new Rare catalyst", "Unlocks a new Legendary catalyst". (Owner, rounds 2 and 3.)
- **The reward is drawn at random when the goal completes**, from the items still locked in that bucket. The item is
  revealed at that moment. Two players can get different items from the same goal.
- **Catalysts and starters both have a rarity**, set by gameplay usefulness. Rarity drives the achievement's difficulty:
  the stronger the item, the harder the goal. This gives one dial for balancing goals against rewards.
- **The Goals page is grouped by reward rarity** (Uncommon, Rare, Legendary), not by goal family. Each line is labelled starter or catalyst. (Owner.)
- **A failed run still counts** toward goals. Only goals whose text says "win" need a won run (rule below).

### Rarity (provisional, to be validated by the balance bot)

| Rarity | Meaning | Existing catalysts (first guess) |
|---|---|---|
| Common | Narrow or situational; helps a plan that already works | Bellows, Bestiary |
| Uncommon | Solid on most benches | Heart Tile, First Spark, Quiet Bench, Eureka Lens |
| Rare | Changes how the bench is planned or adds run resources | Crooked Mirror, Resonant Slab, Deep Satchel |
| Legendary | Run-shaping | Hourglass (and new ones) |

- **Starting pool (owner agreed):** all Common plus half of the Uncommon. Everything else is locked.
- **Several of the new Phase 3 catalysts are Uncommon** (owner). Some of them join the starting pool, some are locked.
- **Draw odds stay flat for now** (owner): the catalyst choice at the end of a depth draws at random from the unlocked
  pool with equal odds. Rarity does not weight the draw in Phase 3; it only sets the goal difficulty.

### Starter rarity (owner, round 3)

- The 8 existing starters become **Common** and stay open.
- 5 new starters, all locked: **3 Uncommon and 2 Rare**. No Legendary starter (a starter is the weakest tier).
- Same test as catalysts, usefulness. Uncommon starters are safe and flexible (mid Reaction Index, link to several
  Kinds). Rare starters bend a plan (very high or very low index, or a scarce Kind).
- Unlocked starters join the satchel; hand size is unchanged. The bot measures whether a 13-starter satchel hurts
  win rate.

### Catalyst counts (target)

| Rarity | Existing | New | Total | Unlocked on a fresh profile | Locked |
|---|---|---|---|---|---|
| Common | 2 | 2 | 4 | 4 | 0 |
| Uncommon | 4 | 6 | 10 | 5 (Heart Tile, First Spark + 3 new) | 5 (Quiet Bench, Eureka Lens + 3 new) |
| Rare | 3 | 5 | 8 | 0 | 8 |
| Legendary | 1 | 3 | 4 | 0 | 4 |
| **Total** | **10** | **16** | **26** | **9** | **17** |

### Rule: goals never outnumber locked items

Owner requirement: the number of goals must not exceed the number of locked items. Because rewards are drawn per
bucket, the check is done **per bucket**, not only in total:

| Bucket | Total | Open at start | Locked | Goals | Check |
|---|---|---|---|---|---|
| Starter · Common | 8 | 8 | 0 | 0 | ok |
| Starter · Uncommon | 3 | 0 | 3 | 3 | 3 ≤ 3 |
| Starter · Rare | 2 | 0 | 2 | 2 | 2 ≤ 2 |
| Catalyst · Common | 4 | 4 | 0 | 0 | ok |
| Catalyst · Uncommon | 10 | 5 | 5 | 5 | 5 ≤ 5 |
| Catalyst · Rare | 8 | 0 | 8 | 8 | 8 ≤ 8 |
| Catalyst · Legendary | 4 | 0 | 4 | 4 | 4 ≤ 4 |
| **All** | **39** | **17** | **22** | **22** | 22 ≤ 22 |

- Every goal is therefore guaranteed a reward; no goal can complete onto an empty bucket.
- A **content test** enforces it: for each bucket, `goals ≤ locked items`. The test fails the build when someone adds
  a goal without a locked item (or unlocks an item by default without removing a goal).
- If a later phase adds items without adding goals, the surplus stays locked until a goal is added; the test allows
  `goals < locked`, but the spec should flag it so nothing is locked forever by accident.
- Aspects and events are **not** locked in Phase 3; they are all available from the first run.

### Stats the game must record (profile-wide)

Runs started, runs won, deepest depth, total ignites, total reactions, longest chain, largest combo (items), discoveries
found, ultimate made, best single-ignite potency, runs won with no redraw, catalyst/aspect pick counts. Kept in a new
profile save (below), not in the journal.

### Which runs count

| Family | Failed runs count? |
|---|---|
| Journey (reach depth N) | Yes: the deepest depth reached in any run |
| Persistence (start N runs) | Yes |
| Ignition (N ignites total) | Yes |
| Craft (chain, combo, potency in one Ignite) | Yes |
| Knowledge (journal entries) | Yes (the journal is already kept across runs) |
| Victory / Mastery (anything that says "win") | No: needs a won run |

### Goal list (22, one per locked item; numbers are placeholders to tune)

| Bucket | Goals |
|---|---|
| Starter · Uncommon (3) | Reach depth 2 · Start 3 runs · 25 ignites |
| Catalyst · Uncommon (5) | Make a chain of 3 · 10 journal entries · Reach depth 3 · Start 10 runs · 100 ignites |
| Starter · Rare (2) | Make a 4-combo · 20 journal entries |
| Catalyst · Rare (8) | Reach depth 4 · Start 25 runs · 300 ignites · Make a chain of 4 · Make a 5-combo · 30 journal entries · 1,500 potency in one Ignite · Win a run |
| Catalyst · Legendary (4) | Win 5 runs · Win without a redraw · 1,000 ignites · Complete the journal |

On the Goals page this is 8 Uncommon, 10 Rare and 4 Legendary lines. Difficulty rises with rarity: Uncommon goals
should fall in the first few runs, Rare over a few evenings, Legendary as long-term mastery. Check with the bot (see the balance plan).

### Unlockable starters

- Today there are 8 starters (tier 0). Phase 3 adds **5 unlockable starters** (3 Uncommon, 2 Rare, matching the 5 Starter goals) that
  enter the satchel for all later runs once unlocked.
- Each new starter needs 3 to 5 recipes so it does not become a dead card. That ties pillar A to pillar D.
- A new starter should widen the strategy (a new Kind mix, a high or low Reaction Index) rather than add raw power.

### Screens

- **Goals** page (from the menu): three sections by reward rarity (Uncommon, Rare, Legendary). Each line shows
  the goal, a progress bar and the generic reward text; an earned line shows the item it gave. A toast when a goal completes during a run, shown at the end of the Ignite so it does not interrupt play.
- **Catalyst choice** (end of depth) draws only from the unlocked pool.

## B. Aspects (run-long bench modifiers)

Name is a placeholder ("Aspect"). An aspect is like a catalyst but acts **on tiles and bench geometry**, which
catalysts do not touch today. It lasts for the whole run.

### When

- Offered at **run start**, at **depth 3** and at **depth 5**: three per run. These moments are **fixed**. (Owner.)
- **Three aspects are drawn at random; the player picks one.** (Owner.) The three are distinct and exclude aspects the
  run already holds.
- **Trade-off aspects are allowed** (owner): a strong effect with a cost. They are marked in the offer so the player
  sees the cost before picking. Share: about a fifth of the pool (3 of 16).
- Aspects stack. Because they act on tiles, two aspects on the same tile must resolve by a defined rule (decide in
  spec; simplest is that the draw never places a new aspect on an already modified tile).

### Pool (16 at launch: 11 cheap, 5 needing an engine hook)

Each aspect states its tile (random, chosen at grant time and visible). **TO** marks a trade-off aspect.

| Aspect | Effect |
|---|---|
| Doubled Tile | Reactions with the hub on this tile score x2. |
| Anchor Tile | A material placed here is not consumed by an Ignite (starters already return; this protects discoveries). |
| Heated Tile | A material placed here counts +3 Reaction Index. Helps the company rule. |
| Echo Tile | The result made on this tile also sends a copy of itself to the rack. |
| Attuned Tile | A chosen Kind placed here counts as a Tier higher for scoring. |
| Bridge | Two named non-adjacent tiles count as neighbours. |
| Open Bench | One corner tile counts as an edge (4 contacts) for recipe size. |
| Crossroads | A hub on this tile also counts its diagonal neighbours. |
| Seed Tile | An empty tile at Ignite adds 15 potency to each reaction touching it. |
| Fallow Tile | One extra hand card is drawn if this tile stays empty. |
| Kiln Tile | The first reaction hub on this tile each Ignite is guaranteed to take the largest recipe. |
| Cold Tile (TO) | Locked (cannot be used) but +1 trial per depth. |
| Heavy Tile | Reactions here count one chain step higher. |
| Pivot Tile | If the material on this tile can react, it is the hub of the next reaction whatever the Reaction Index of the others. |
| Volatile Tile (TO) | Reactions with the hub here score x3, but the material placed here is lost even if it is a starter. |
| Greedy Bench (TO) | +1 hand card each trial, -1 redraw for the run. |

**Delivery (owner, round 3):** the first 11 (Doubled, Anchor, Heated, Echo, Attuned, Seed, Fallow, Heavy, Cold,
Volatile, Greedy) change scoring, consumption or hand, so they ship first. The other 5 (Bridge, Open Bench, Kiln, Crossroads,
Pivot) change neighbour rules and need an engine hook; they ship only if that hook proves small, otherwise they are
cut. Three of the 16 are trade-offs (Cold, Volatile, Greedy; about a fifth; the earlier count of four was a mistake); an offer of three may contain more than one. Hollow Tile and Mirror Tile were replaced by Crossroads and Pivot Tile when the spec was written: Hollow could only lower heat, which never helps, and Mirror cost too much for its effect.

### Engine implication

Per-tile data is new. The bench needs a per-run `TileMod[9]` (or a list of modifiers with a tile index) that
`Chain` and `Ignite` read when picking the hub, choosing the recipe, consuming items and scoring. Keep this in Core so
the dotnet tests and the bot cover it. The scripted player must be able to plan with it.

### UI

Modified tiles need a visible marker (an overlay on the tile that does not clash with the glow) and a tooltip. The
HUD lists active aspects. Granting is a short cutscene beat, like the catalyst choice.

## C. Depth events

- At the **start of each depth** (including depth 1), one **random event** is drawn from a weighted pool. Events can be
  tied to a depth range. Which event comes differs per depth and per run.
- **Order at the start of a depth (owner, fixed): aspect first, then event.** At run start, depth 3 and depth 5 the
  player picks an aspect, then meets the event; at depths 2 and 4 there is only the event.
- Content volume: **16 events at launch** (4 Choice, 3 Gamble, 2 Find, 2 Shrine, 3 Hazard, 1 Boon, 1 Lore), plus a "nothing happens" weight so the
  player cannot count on one every time.

### Constraint: there is no currency

The game has no gold. Events may spend only what exists: **trials, hand size, rack items, potency carried,
catalysts, tile state, journal hints**. This keeps the economy small and is a design feature, not a gap. If events
need a currency later, add one in a deliberate step.

### Event types and examples

| Type | Example |
|---|---|
| Choice | "A cracked altar": give up a rack item for +1 trial. |
| Gamble | "A dripping well": a random starter returns, or a random hand card is lost. |
| Find | "A forgotten satchel": a free discovery goes to the rack. |
| Shrine | "A sealed shrine": offer a catalyst, receive a stronger one of the next rarity. |
| Hazard | "Damp air": this depth, Reaction Index counts -1 for all materials, target stays. |
| Boon | "Warm stone": this depth, one tile is Heated (a one-depth aspect). |
| Lore | A single line with no effect, used as a cooldown event. |

### Validity rules (to enforce in tests)

- An event never makes a depth impossible (a Hazard must not push the target above what the bot can reach).
- An event whose cost cannot be paid is not drawn (e.g., no rack item to give).
- Draw is seeded per run for tests and replay.

## D. Recipe tree growth

- Add **20 to 30 materials** and the recipes that connect them, including the recipes for the new starters.
- Add **dead-end discoveries**: rare or odd materials that score well but do not lead on to the ultimate. They make
  discovery a goal in itself.
- Add **one or two hidden chains** (3 specific rare materials in a combo) that give a very large single result.
- Keep the Reaction Index spread and the company rule in mind: new materials must not all be lively or all dull.
- Art: the generator in `tools/art/` already produces every sprite; extend it, do not draw by hand.

## Foundation work (must come first)

1. **Profile save.** The journal save (`athanor.journal.v1`) holds only the journal. Add a second, versioned key for
   profile data: stats, goals completed, unlocked catalysts/starters. Failing to read it must return an empty profile,
   never crash (same as the journal).
2. **Core services** with no Unity dependency: `Stats`, `Goals` (evaluate progress), `Unlocks`. Tested with dotnet.
3. **Content schema.** Extend `content.json` and `ContentData` for goals, aspects, events, rarity and the unlock
   flag. Keep reading older fields as Phase 2 did with `a`/`b`. Starters get a rarity field too.
5. **Reset progress** button in the menu, with a confirmation step (owner, round 3).
4. **Run hooks.** `RunState` raises events (ignite done, depth reached, chain, combo) that `Stats` listens to.

## Proposed order of work

| Step | Content | Why here |
|---|---|---|
| 3.0 | Foundation above | Everything depends on it. |
| 3.1 | Rarity, new catalysts, goals, Goals page, catalyst pool filtering | Smallest end-to-end loop that makes runs matter. |
| 3.2 | Recipe growth (D) with the new starters | New starters need recipes; the tree needs data before balancing. |
| 3.3 | Aspects: per-tile data in Core, the 11 cheap aspects, tile marker UI, pick-1-of-3 screen | The only item that changes the engine; do it once the data is stable. |
| 3.4 | The 5 engine-hook aspects | Only if the neighbour-rule hook is small; cut whatever is not. |
| 3.5 | Events: system, 8 events, then fill to 16 | Uses trial/rack/aspect hooks built in 3.3. |
| 3.6 | Balance, UI polish, web build, handoff | See below. |

## Balance plan

- Phase 2 left the bot at 85% on 200 seeds, "at the top of the allowed range" (see `phase-2-handoff.md`). Phase 3
  adds power (catalysts, aspects, starters), so the baseline must be reset on purpose.
- Measure three profiles: **fresh** (no unlocks), **mid** and **full**. Target win rates, provisional: fresh 60 to
  70%, full 80 to 90% for the informed bot, random player stays near 0%.
- Goals must be reachable: check that a bot with a fresh profile can complete early goals within a few runs.
- Use the balance report in `RunStateTests.cs`; add rows for aspects and events.

## Owner decisions (round 2, 2026-11-12)

1. Starting pool: all Common + half Uncommon. Some new Phase 3 catalysts are Uncommon. Draw odds flat (random) for now.
2. Goals page: grouped by reward rarity.
3. Rewards: random within their bucket; the text names only category and rarity ("Unlocks a new Rare catalyst").
4. Goals never outnumber locked items; checked per bucket (table above) and by a content test.
5. Aspects: draw 3, pick 1.
6. Trade-off aspects: allowed.
7. Order: aspect first (fixed moments), then the event (random per depth and run).
8. Failed runs count toward goals, except goals that need a win.

## Owner decisions (round 3, 2026-11-12: "ok all")

1. Starters have a rarity: 8 existing are Common (open); 5 new locked: 3 Uncommon, 2 Rare; no Legendary starter.
2. Starter rarity uses the same usefulness test as catalysts.
3. Unlocked starters join the satchel; hand size unchanged; the bot checks the effect.
4. Of the 6 new Uncommon catalysts, the 3 simplest start open, the 3 needing planning are locked (the spec picks them).
5. Aspects: 11 cheap ones first, the 5 engine-hook ones only if the hook is small.
6. Trade-offs about a fifth of the pool (3 of 16), marked on the offer card.
7. 16 events at launch in the type split above; the plan's examples are a starting point.
8. Reset progress button, with a confirmation.
9. Balance targets: informed bot 60 to 70% fresh, 80 to 90% fully unlocked, random player near 0%.
10. Goal numbers are tuned by the bot after the content exists, not fixed in the spec.

## Left for the spec

1. The final named list of catalysts (26) and starters (13) with their rarity and effect.
2. The final named list of aspects and events.
3. Exact goal numbers, after the bot pass.

## Phase 4 parking lot (decided, not planned)

- Endless mode after depth 5, with a target that grows without limit.
- Story and framing: lore per depth, cutscenes.
- Several **Ultimates**, each with its own ending.

These need Phase 3's profile, goal and event systems, so they are deliberately after it.
