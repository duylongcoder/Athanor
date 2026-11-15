# Phase 5b: Endings and the Worldseed frame

Status: **Draft, proposal only** (2026-11-16, task 5b-03, `lore-writer`). Nothing here is decided; no entry goes under
*Decided* in any lore file until the owner answers the questions below. No content, lang or code changes.

Canon this builds on: [phase-4-lore.md](phase-4-lore.md) "The world in five lines" and its mapping table, and its
*Decided* list (depth intros, Lore seen-flag, "The Keeper"); [phase-5-lore.md](phase-5-lore.md) *Decided* (one intro line
per depth, one window view per depth). Owner scope: [phase-5-prep.md](phase-5-prep.md) 5b: "Several Ultimates, each its
own ending, no true/good/bad ending: equal endings that depend on the Worldseed the player makes; lore first, brief and
questions only."

Facts today (no change proposed in this file):

- One ultimate: `content.json` `ultimate` is a single id (`vault/Data/Data Design.md:20`), `worldseed`, tier 5, Crystal,
  glow `#ffffff`. Depth 5 (The Quiet Below) asks for it instead of a potency target.
- Three recipes make it, all at heat >= 30: Living Gold + Azoth; Everflame + Heaven's Ash; Philosopher's Salt +
  Chrysopoeia (`vault/Content/Materials/mat.worldseed.md`).
- One ending: `GameFlow.EndRun` calls `story.Ending(Db.Material(Db.Ultimate))` (`GameFlow.cs:672`).
  `SimpleCutscene.Ending` (`SimpleCutscene.cs:208-232`) already tints by the material's `glow` and shows the icon by its
  `id`, then types `story.ending.1..3` (`en.json:160-162`) and the banner `cutscene.great_work`.
- Making the ultimate wins at any depth (Phase 1 ruling, `phase-1-mvp.md:19`; pinned by
  `RunStateTests.MakingTheUltimate_WinsAtAnyDepth`). Owner kept this in 5a and asked to revisit it with the lore in 5b
  (`phase-5-prep.md:191-192`). The won screen says "Planted at depth {n}" (`ui.runover.planted_at`).
- Four tier-4 materials are dead ends, used by no recipe (`vault/Content/Content Health.md`): `thunder_heart` (Beast,
  RI 10), `ossuary_crown` (Metal, RI 2), `storm_philter` (Potion, RI 8), `copper_sun` (Crystal, RI 6).

## World frame

The five canon lines stay true in every option below: the cellar grows; each Keeper made a seed at the bottom and
planted it; each seed added a depth; the Book comes back up; a won run plants a seed. What changes is what a seed *is*.

### Option 1: Roots of worlds (recommended)

A seed does not make a room. It grows a **world**, somewhere no Keeper has been. What the Keepers see of it is only its
**root**, pushing down: that root is the new depth. The cellar is not a stack of dungeons; it is the tangle of roots of
every world the Keepers planted, the newest near the top, the oldest at the bottom.

- Each Keeper plants a world they will never see. That is the melancholy, and it fits what is already there: "The last
  Keeper went down and did not come back up", the pinned note "Plant it. Do not climb back.", "Roots push through the
  ceiling. They are warm, and hum.", "The roots here are thicker than the walls."
- Different seeds plant different worlds, so different roots: the layers differ because the worlds differ. The game
  never shows a world, only what reaches down from it. Nothing is explained.
- Fits the existing depths without changing them: the Dripping Stair reads as the root of a wet world, the Humming Dark
  of a warm one, the Root of Stone of a still one (see *Worldseeds*). The Quiet Below is older than any world: the
  place where seeds are planted.
- Cost: lore only. Ending lines can carry it; no new screens are required by the frame itself.

### Option 2: Each depth is a world

Each seed grows a whole small world under the last one, and the next Keeper climbs down through them. The five depths
are five worlds made by five earlier Keepers.

- Strong and simple, but it is still a stack, only renamed.
- If the player's seed should show up as a depth in later runs, the depth list stops being fixed: rules, content and
  art per profile (large). If it should not, the player's ending changes nothing they will ever meet.

### Option 3: The world above

Seeds grow **up**. The workshop, the town, the sky over them are what the seeds made; the cellar deepening is the cost,
the hole each world leaves under it. The player's ending changes the surface: the title screen or the depth-1 window
("a pale slice of the workshop floor far above") shows the last world grown.

- Gives the ending a visible after-effect, which players like.
- Shows the result, which pulls against "nothing down here is labelled", and needs a surface view per ending (art, size).

**Recommendation: Option 1.** It keeps every canon line and every shipped text, it makes the endings equal by nature (a
world is not better than another world, only different), and it costs nothing until the owner chooses how far to stage
it. Option 3's after-effect can be borrowed later in a small form (a tint in the depth-1 shaft of light), without
committing to showing a world.

## Worldseeds

Proposal: the three recipe paths that exist today become three seeds, one world each. They follow the old three
principles of alchemy (what flows, what burns, what keeps), which the ingredients already lean towards. Names below are
**working ids for this file only**; player-facing names are an open question (Q9), and the game never shows a seed's name
before the player has made it.

| Working id | Made from (existing recipe) | What it is | The world it plants | Root it sends down (fits depth) |
|---|---|---|---|---|
| `seed_flowing` | Living Gold (Metal, RI 4) + Azoth (Potion, RI 7) | Living metal and the water of the work: the path of quicksilver, of spirit, of things that move | A world that is wet and alive and never still: rivers, rain, green | Water finding its way down: the Dripping Stair |
| `seed_burning` | Everflame (Potion, RI 9) + Heaven's Ash (Powder, RI 1) | Fire that does not end and the ash of the sky: the path of sulphur, of soul, of heat and light | A world of long light and warm air, a sky that is never quite dark | A slow warm note in the dark: the Humming Dark |
| `seed_keeping` | Philosopher's Salt (Powder, RI 6) + Chrysopoeia (Metal, RI 2) | Salt that fixes and gold that is made: the path of salt, of body, of what lasts | A world of stone and crystal that does not wear, where things are kept | Roots that have taken the stone: the Root of Stone |

Why each plants a different world: a seed carries what went into it. "It hums with every reaction that came before it"
(`story.ending.2`) already says so; the frame only adds that the reactions decide *which* world. The player does not
choose a world from a menu; they choose it by the road they brewed down.

Facts for `content-designer` (not judged here): Living Gold, Azoth, Everflame, Heaven's Ash and Chrysopoeia each have
two recipes; Philosopher's Salt has one (3 items). So `seed_keeping` has fewer roads in today. Equal endings may need
equal reach; that is a balance question (Q2 and Q11).

### Sample ending lines (voice check only, not for `en.json`)

The structure proposed keeps `story.ending.1` and `story.ending.3` as they are and gives each seed its own second line.
None names the substance or says what went into it. Lengths kept near the current line 2 (48 characters).

| Seed | Line 2 | Chars |
|---|---|---:|
| shared today | It hums with every reaction that came before it. | 48 |
| `seed_flowing` | It runs between your fingers and does not spill. | 48 |
| `seed_burning` | It burns without smoke, and does not burn you. | 46 |
| `seed_keeping` | It is heavy and cold, as if it meant to last. | 45 |

Possible Book line for a planted seed (if Q4 adds a page): "Planted. You will not see what grows." (37)

## Questions

| # | Question | Options | Recommendation | Touches |
|---:|---|---|---|---|
| 1 | How many Ultimates? | (a) 3, one per existing recipe path. (b) 4 or 5, adding seeds from the dead ends (Q8). (c) 1 Worldseed, with the ending varied by how it was made. | **(a) 3** first; keep (b) as a later add. Three uses roads that already exist and are balanced. | rules (win check on a set of ids); content (`ultimate` becomes a list, 3 materials); art (3 icons in `tools/art/icons.mjs`); size small |
| 2 | What decides which one is made? | (a) The recipe path: each recipe gives its own seed id. (b) One `worldseed` id; the run remembers which pair made it. (c) Catalysts held at the end. (d) The last depth reached. (e) The Journal (what the Book already knows). (f) The Kind used most in the run. | **(a) recipe path.** Deterministic, data-driven, readable in the Journal like any material, no new run state. (c)-(f) feel arbitrary or punish players for past runs. | rules (`RunState` win on any ultimate; balance test "a path to the ultimate exists" per seed); content (3 result ids); balance (BalanceMatrix per seed so each is reachable about as often) |
| 3 | Own cutscene and art per ending? | (a) Shared `Ending()`; each seed has its own icon, glow and line 2. (b) Shared, plus one effect and one closing view per seed (e.g. rain, a warm haze, a slow glitter). (c) A full cutscene with new background art per ending. | **(a) now, (b) if the owner wants more.** `SimpleCutscene.Ending` already reads `glow` and `id` from the material, so (a) is mostly text. (c) multiplies art and size by three. | UI (cutscene takes the made seed, `GameFlow.cs:672`); art ((a) 3 icons, (b) 3 effects, (c) 3 backgrounds); size ((c) largest); lang (3 new line-2 keys, vi too) |
| 4 | Does the Journal track endings? | (a) No; seeds are ordinary Journal entries. (b) A "Planted" page: one slot per seed, unmade ones blank and unlabelled, no hints. (c) Profile counts only, shown on the run-over screen. | **(b)**: the Book is the thing that comes back up; it should remember what was planted. Blank slots tell a player there is more without saying how. | UI (Journal page); save (profile gains a planted list: shape change, key version bump and old reader, `SaveTests`); lang (page title, slot line) |
| 5 | Goals and unlocks? | (a) No new goals; `g_win1`/`g_win5` count any seed. (b) One goal: plant every seed once. (c) A different reward per seed. | **(a) + (b).** One "plant them all" goal rewards variety. (c) makes one ending pay more than another, which breaks "equal endings". | content (1 goal; the loader fails if a reward bucket has more goals than locked items); rules (new stat, e.g. distinct seeds planted, in `StatIds`) |
| 6 | Win at any depth, or depth 5 only? | (a) Keep any depth (today). (b) Depth 5 only: a seed made earlier is kept and planted on reaching depth 5. (c) Any depth, and the ending line says where ("Planted at depth {n}" already exists). | **(b), lore view, after a BalanceMatrix check.** Every line points to the bottom: "At the bottom they made something", "At the bottom of everything: a seed.", depth 5 "the place where seeds are planted", the pinned note at depth 5. Under Option 1 a world's root starts where it is planted, which reads as the bottom. If the win rate drops too far, (c) is the fallback. Owner decision. | rules (`RunState.cs:219,249-250`, test `MakingTheUltimate_WinsAtAnyDepth` changes, test first); balance (average won run ends near depth 4 today, so win rate will move); UI (`ui.runover.planted_at` becomes constant or goes) |
| 7 | Endless after depth 5 (note only; the owner is redesigning Endless) | (a) Endless ignores which seed was planted. (b) Endless goes down into the root of the world just planted, so its layers take that seed's mood. | **No decision.** Keep (b) in mind as a hook when Endless is redesigned; it fits Option 1 with no extra canon. | rules, content, art: only if (b) is chosen later |
| 8 | Do the four tier-4 dead ends feed endings? | (a) Leave them as dead ends. (b) Pair them into new seeds: `thunder_heart` + `storm_philter` (both storm) and `ossuary_crown` + `copper_sun`. (c) Use one as a modifier: in the final mix it changes the ending's mood, not the seed. | **(a) for this pass, (b) as the candidate for a 4th and 5th seed.** The storm pair is a natural world (weather, thunder). The second pair has no obvious shared theme yet; (c) adds a hidden rule a player cannot see. | content (recipes, heat, balance); art (icons); rules ((c) only) |
| 9 | What is each seed called on screen? | (a) All are "Worldseed", told apart by colour and line 2. (b) Each has its own name, shown only once made; until then an unlabelled slot. | **(b)**, names chosen by the owner (item names stay English per the Phase 5 decision). The current "Worldseed" could remain the family word in lore, never a label before it is made. | content (material names); lang (Journal, vi glossary); art (icons per id) |
| 10 | Is a planting visible after the ending? | (a) No; only the Book remembers. (b) A small trace: the depth-1 shaft of light takes the last seed's tint. (c) A surface view per seed (Option 3). | **(a) now, (b) later** if wanted. (b) is one tint per seed on an effect that exists; (c) is a new view per ending. | art ((b) tint, (c) views); UI (read last seed from profile); save ((b) last seed planted) |
| 11 | Must the three roads be equally long? | (a) Yes: balance them so each seed is made about as often. (b) No: roads differ, endings stay equal in reward and weight. | **(b), with a floor:** no seed so rare a player never sees it. Equal endings is about worth, not frequency; some roads can be harder. `content-designer` sets the floor with BalanceMatrix. | content, balance report |
| 12 | After a won run, is the next run the same Keeper? | (a) Unsaid. (b) A new Keeper takes the bench, as the intro implies. | **(a) unsaid.** The intro already covers it; saying more explains too much. | lore only |

## Open questions for the owner

1. Which world frame: Option 1 (roots of worlds), 2 (each depth a world) or 3 (the world above)?
2. Three seeds from the three existing recipe paths (flowing / burning / keeping), or a different count?
3. Win at any depth, or depth 5 only (Q6)? This one decides most of the ending text.
4. May `story.ending.1` and `story.ending.3` stay shared and only line 2 vary per seed?
