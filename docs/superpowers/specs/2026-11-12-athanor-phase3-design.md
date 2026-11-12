# Athanor Phase 3: unlocks, aspects and depth events

Date: 2026-11-12. Builds on `2026-11-12-athanor-phase2-design.md` (shipped). Where this document is silent, Phase 2
still holds. Product decisions are in `docs/devlogs/phase-3-plan.md` (owner rounds 1 to 3, all approved); this spec
turns them into rules, data and interfaces. "My call" marks a detail the owner did not decide.

## 1. Intent and scope

Phase 2 made one run deep. Phase 3 gives the player reasons to start another: a profile that grows (goals that
unlock catalysts and starters), a run that differs each time (aspects and depth events), and more to discover.

In scope:

| Pillar | Summary |
|---|---|
| A | Profile, stats, 22 goals, random unlocks by kind and rarity, Goals page, 26 catalysts, 13 starters |
| B | Aspects: 16 run-long bench modifiers, pick 1 of 3 at run start, depth 3 and depth 5 |
| C | Depth events: 16 events, one drawn at the start of every depth, after the aspect |
| D | 5 new starters, 24 new discoveries, 28 new recipes |

Out of scope (Phase 4): endless mode, story and framing, several Ultimates and endings. Also out: sound, mobile
layout, a currency, rarity-weighted draws, changes to the five depths, the targets of Phase 2 (they may be re-tuned in
the balance pass, see 11).

### Invariants

1. **A fresh profile plays as Phase 2 plus aspects and events.** Every new recipe has at least one new material
   among its ingredients, and every new material needs an unlocked starter, so no Phase 3 recipe is reachable
   before a starter is unlocked. The 59 recipes of Phase 2 stay exactly as they are (a test pins them).
2. **Core stays free of Unity.** Profile, stats, goals, aspects, events and the new engine hooks live in
   `Athanor/Assets/Scripts/Core/` and run in `dotnet test tools/CoreTests`.
3. **The scripted player and the game use the same planner.** Anything the bench rules change is read by
   `ChainPlanner`, so `AutoPlayer` cannot disagree with the game.
4. **The 145 Phase 2 tests keep passing unchanged.** New behaviour is opt-in through `RunOptions` (4.4).
5. **Reading a save never throws.** Same rule as the journal.

## 2. Architecture

### 2.1 New Core files

| File | Purpose |
|---|---|
| `Profile.cs` | `Profile` (stats, completed goals, unlocked items, reward log) and `ProfileData` (its serializable form) |
| `Goals.cs` | `GoalProgress`, evaluation, reward drawing |
| `Unlocks.cs` | `Unlocks`: which starters and catalysts are open; `Fresh(db)` and `All(db)` |
| `BenchRules.cs` | Per-run, per-depth bench modifiers read by the planner (tiles, links, shifts) |
| `Aspects.cs` | `AspectPool`: offers, placement on tiles, application to `BenchRules` |
| `Events.cs` | `EventPool`: eligibility, drawing, resolving an option into effects |
| `RunOptions.cs` | Switches and inputs for a run (unlocks, aspects on, events on, seeds) |

### 2.2 Changed Core files

| File | Change |
|---|---|
| `ContentData.cs` | New fields and types (section 3); new `Effects` names |
| `ContentDb.cs` | Reads and validates the new content; `GoalsList`, `Aspects`, `Events`; rarity helpers |
| `Chain.cs` | `ChainPlanner` takes `BenchRules` (neighbours, heat, hub choice, blocked tiles) |
| `Ignite.cs` | `IgniteResolver` takes `BenchRules`; `Score` takes a `ScoreContext` |
| `RunState.cs` | New phases, depth-start sequence, depth mods, unlock-aware satchel and offer, new catalyst effects |
| `Satchel.cs` | No change (built from the unlocked starters) |
| `AutoPlayer.cs` | Chooses aspects and event options; plans with `BenchRules` |

### 2.3 Changed Game files

| File | Change |
|---|---|
| `Boot/Save.cs` | `LoadProfile` and `StoreProfile` (key `athanor.profile.v1`), `ResetProfile` |
| `Boot/GameFlow.cs` | Owns the `Profile`; records stats; runs the aspect and event stages; goal toast; new flow states |
| `Ui/GoalsView.cs` (new) | The Goals page |
| `Ui/CatalystChoiceView.cs` | Generalised into a three-card chooser that also shows aspects (same layout, different card content) |
| `Ui/EventView.cs` (new) | Event panel with option buttons and a result line |
| `Ui/TitleView.cs` | A Goals button and a Reset progress button |
| `Ui/RunOverView.cs` | Lists goals completed during the run |
| `Bench/BenchView.cs`, `TileView.cs`, `LinkView.cs`, `HudView.cs` | Tile markers, blocked tiles, extra links, active-aspect strip |

### 2.4 Build and tools

`tools/art/icons.mjs` and `export.mjs` gain icons for the new materials, catalysts, aspects and event types; the
export refuses to run with a missing icon, as today. `Athanor > Build Scene` puts the new sprites in the `ArtLibrary`.
`tools/CoreTests` keeps compiling `Scripts/Core/**` and `Tests/EditMode/**`; no csproj change is needed.

## 3. Content schema

All content stays in `Resources/content.json`, read by `JsonUtility` in Unity and `System.Text.Json` in the tests.
Every new field has a default that reproduces Phase 2, so the old JSON still loads.

```csharp
[Serializable] public class MaterialData { /* existing */ public string rarity; public bool locked; }
    // rarity: starters only, "Common" | "Uncommon" | "Rare", empty otherwise.  locked: true = not available until unlocked (starters only).
[Serializable] public class CatalystData { /* existing */ public string rarity; public bool locked; }

[Serializable] public class GoalData  { public string id, text, stat, rewardKind, rewardRarity; public int target; }
[Serializable] public class AspectData { public string id, name, text, effect, scope, group; public float amount; public bool tradeOff; }
[Serializable] public class EventData  { public string id, name, text, type; public int minDepth, maxDepth, weight; public EventOption[] options; }
[Serializable] public class EventOption  { public string label, requires; public EffectData[] cost; public EventOutcome[] outcomes; }
[Serializable] public class EventOutcome { public int weight; public string text; public EffectData[] gain; }
[Serializable] public class EffectData   { public string op; public int amount, amount2; public string arg; }

ContentData adds: GoalData[] goals; AspectData[] aspects; EventData[] events; int noEventWeight;
```

### 3.1 Validation (`ContentDb`, throws `ArgumentException` like today)

- `rarity` on a material is allowed only when `tier == 0`, and must then be one of the three starter rarities.
  A starter with no rarity is read as Common (so Phase 2 content loads).
- `locked` is allowed only on starters and catalysts. Only items with rarity above Common may be locked (a locked
  Common item could never be won: no goal pays out Common).
- Catalyst `rarity` is one of Common, Uncommon, Rare, Legendary; missing is read as Common.
- Goal: unique `id`; `stat` is one of the stat ids in 4.2; `target >= 1`; `rewardKind` is `starter` or `catalyst`;
  `rewardRarity` is Uncommon, Rare or Legendary; no Legendary starter.
- **Goal budget (the owner's rule):** for every bucket (`rewardKind` x `rewardRarity`), the number of goals must not
  exceed the number of locked items in it. A violation throws, so the build cannot ship with a goal that completes onto
  an empty pool. A bucket with more locked items than goals is allowed but reported by a test as a warning list, so
  nothing stays locked by accident.
- Aspect and event ids are unique; `effect`, `op`, `requires` and `type` are known names (listed in 7 and 8); an
  event has at least one option with no `requires` and an empty `cost` ("walk on"), so it can always be left.
- Recipes: the existing checks; plus every recipe in a pinned list of the 59 Phase 2 keys must still exist unchanged.

## 4. Pillar A: profile, goals and unlocks

### 4.1 Profile

`Profile` is the long-term player state, separate from the `Journal` (which keeps its own key and format).

```csharp
public sealed class Profile {
  public Stats Stats;                       // counters below
  public HashSet<string> Completed;         // goal ids
  public HashSet<string> UnlockedStarters, UnlockedCatalysts;   // ids of locked items that were won
  public List<RewardLog> Rewards;           // goalId -> itemId, in order, for the Goals page
  public int RewardSeed;                    // random per profile, fixed at creation
  public ProfileData ToData(); public static Profile FromData(ProfileData d);   // FromData never throws
}
```

`Unlocks` answers "is this open": an item is open when it is not `locked` in content, or its id is in the profile's
unlocked sets. `Unlocks.Fresh(db)` is the open-by-default set; `Unlocks.All(db)` opens everything (tests, balance).
Ids in the save that no longer exist in content are dropped on load.

### 4.2 Stats

Counters are updated by `Profile` methods that `GameFlow` calls; the calls are plain functions of `RunState` and
`IgniteOutcome`, so they are tested without Unity.

| Stat id | Kind | Updated when |
|---|---|---|
| `runsStarted` | count | a run is created |
| `runsWon` | count | a run ends in `Won` |
| `wonNoRedraw` | count | a run ends in `Won` having used no redraw (`RunState.RedrawsUsed == 0`) |
| `depthReached` | max | a depth begins |
| `ignites` | count | an Ignite resolves |
| `chainLongest` | max | an Ignite resolves: the highest `ReactionEvent.Chain` in it |
| `comboLargest` | max | an Ignite resolves: the most `Items` in one reaction |
| `bestIgnite` | max | an Ignite resolves: `Gained` |
| `journalEntries` | derived | read from the journal (`KnownPairs.Count`) when progress is evaluated |
| `journalComplete` | derived | 1 when every recipe in content is known |

Failed runs update every stat, so only goals on `runsWon` and `wonNoRedraw` need a win in effect; there is no
separate "needs a win" flag. A run abandoned at the title screen counts as started.

### 4.3 Goals and rewards

22 goals, one per locked item. The numbers are placeholders for the balance pass (11); ids and buckets are fixed.

| Bucket | id | Text | Stat | Target |
|---|---|---|---|---|
| Starter, Uncommon | g_depth2 | Reach depth 2 | depthReached | 2 |
| | g_runs3 | Start 3 runs | runsStarted | 3 |
| | g_ignite25 | Ignite 25 times | ignites | 25 |
| Starter, Rare | g_combo4 | Make a 4-combo | comboLargest | 4 |
| | g_journal20 | Find 20 journal entries | journalEntries | 20 |
| Catalyst, Uncommon | g_chain3 | Make a chain of 3 | chainLongest | 3 |
| | g_journal10 | Find 10 journal entries | journalEntries | 10 |
| | g_depth3 | Reach depth 3 | depthReached | 3 |
| | g_runs10 | Start 10 runs | runsStarted | 10 |
| | g_ignite100 | Ignite 100 times | ignites | 100 |
| Catalyst, Rare | g_depth4 | Reach depth 4 | depthReached | 4 |
| | g_runs25 | Start 25 runs | runsStarted | 25 |
| | g_ignite300 | Ignite 300 times | ignites | 300 |
| | g_chain4 | Make a chain of 4 | chainLongest | 4 |
| | g_combo5 | Make a 5-combo | comboLargest | 5 |
| | g_journal30 | Find 30 journal entries | journalEntries | 30 |
| | g_big1500 | Score 1,500 in one Ignite | bestIgnite | 1500 |
| | g_win1 | Win a run | runsWon | 1 |
| Catalyst, Legendary | g_win5 | Win 5 runs | runsWon | 5 |
| | g_noredraw | Win without a redraw | wonNoRedraw | 1 |
| | g_ignite1000 | Ignite 1,000 times | ignites | 1000 |
| | g_journalfull | Complete the journal | journalComplete | 1 |

(The table has 22 rows: 3 + 2 + 5 + 8 + 4.)

**Evaluation.** `Goals.Evaluate(profile, db, journal)` returns the goals that are newly complete. It is called after
every Ignite, when a depth begins, when a run ends, and once on load (so a player who already has a journal gets the
journal goals at once). For each newly complete goal, in file order:

1. Collect the locked items of the goal's bucket that the profile has not unlocked, sorted by id.
2. Pick one with `Rng(RewardSeed + Completed.Count * 7919)`. Add it to the profile's unlocked set, log the pair.
3. If the bucket is somehow empty (content changed after a save), complete the goal with no item and log it; the
   content test makes this impossible for shipped content.

Progress for a goal is `min(stat, target) / target`. Completed goals stay complete; stats never go down.

### 4.4 Run options

```csharp
public sealed class RunOptions {
  public Unlocks Unlocks;          // null: everything open (Phase 2 behaviour)
  public bool Aspects, Events;     // false: skip those stages (Phase 2 behaviour)
  public static readonly RunOptions Legacy = new RunOptions();
}
public RunState(ContentDb db, Journal journal, int seed, int startDepth = 1, RunOptions options = null)
```

`options == null` means `Legacy`. The game passes `Unlocks` from the profile and turns aspects and events on.

## 5. Rarity, catalysts and starters

### 5.1 What rarity means

Rarity is a label for usefulness, set once by the designer and checked by the bot (section 11). It has two effects:
it chooses which goals can unlock an item (the bucket), and it shows on cards. It does **not** change draw odds: the
catalyst offer draws uniformly from the open catalysts the run does not hold yet.

| Rarity | Test |
|---|---|
| Common | Narrow or situational; helps a plan that already works |
| Uncommon | Solid on most benches; one clear line of text |
| Rare | Changes how the bench is planned, or adds a run resource |
| Legendary | Shapes the whole run |

### 5.2 Catalysts: 26 (10 existing, 16 new)

Open = in a fresh profile. `Locked` rows can only be won. New effect names are in 5.3.

| id | Name | Rarity | Open | Effect (amount) | Text |
|---|---|---|---|---|---|
| bellows | Bellows | Common | yes | KindDouble Ore | Reactions involving an Ore score double. |
| bestiary | Bestiary | Common | yes | KindDouble Beast | Reactions involving a Beast part score double. |
| herbarium | Herbarium (new) | Common | yes | KindDouble Plant | Reactions involving a Plant score double. |
| alembic | Alembic (new) | Common | yes | KindDouble Potion | Reactions involving a Potion score double. |
| heart_tile | Heart Tile | Uncommon | yes | CentreTwice | Reactions touching the centre tile score double. |
| first_spark | First Spark | Uncommon | yes | FirstTriple | The first reaction of each trial scores triple. |
| wide_crucible | Wide Crucible (new) | Uncommon | yes | ComboBonus 1.5 | Reactions of three or more materials score half again. |
| quick_hands | Quick Hands (new) | Uncommon | yes | PairBonus 1.5 | Reactions of two materials score half again. |
| grain_of_gold | Grain of Gold (new) | Uncommon | yes | FlatPerReaction 30 | Every reaction gains 30 potency. |
| quiet_bench | Quiet Bench | Uncommon | locked | EmptyTileBonus 20 | Each reaction gains 20 potency per empty tile. |
| eureka_lens | Eureka Lens | Uncommon | locked | DiscoveryTriple | New discoveries score triple. |
| tier_seeker | Tier Seeker (new) | Uncommon | locked | TierDouble 3 | Reactions that make a tier 3 or higher result score double. |
| hearth_habit | Hearth Habit (new) | Uncommon | locked | StarterDouble 2 | Reactions using two or more starting materials score double. |
| second_wind | Second Wind (new) | Uncommon | locked | ExtraRedraw 1 | One more redraw at every depth. |
| crooked_mirror | Crooked Mirror | Rare | locked | Diagonals | Diagonal neighbours also react. |
| resonant_slab | Resonant Slab | Rare | locked | ChainPlus | Chains build faster: each reaction counts one step higher. |
| deep_satchel | Deep Satchel | Rare | locked | ExtraHand 1 | Draw one more material each trial. |
| warm_bench | Warm Bench (new) | Rare | locked | HeatPlus 1 | Every material counts one more Reaction Index for the heat round a hub. |
| salvage | Salvage (new) | Rare | locked | Salvage | Discoveries left standing on the bench after a chain go to the rack. |
| cascade | Cascade (new) | Rare | locked | ChainFrom 3 | Reactions from the third in a chain on score double. |
| descent | Descent (new) | Rare | locked | DepthScale 0.25 | Reactions score a quarter more for each depth below the first. |
| tinkers_eye | Tinker's Eye (new) | Rare | locked | FreeDiscovery | The first new discovery at each depth costs no trial. |
| hourglass | Hourglass | Legendary | locked | ExtraTrial 1 | One more trial at every depth. |
| bottomless_satchel | Bottomless Satchel (new) | Legendary | locked | ExtraHand 2 | Draw two more materials each trial. |
| philosophers_fire | Philosopher's Fire (new) | Legendary | locked | AllScale 2 | Every reaction scores double. |
| last_breath | Last Breath (new) | Legendary | locked | SecondChance 2 | Once per run, a lost depth gives two more trials. |

Counts: Common 4, Uncommon 10, Rare 8, Legendary 4 (26). Open 9, locked 17. My call: names and exact amounts are
first drafts; the balance pass may change amounts but not rarity buckets without re-checking the goal budget.

### 5.3 New effects: exact meaning

Effects multiply in the order the catalysts were taken, as Phase 2 does. `p` is the running potency of a reaction.

| Effect | Meaning |
|---|---|
| `ComboBonus x` | `p *= x` if the recipe has 3 or more items |
| `PairBonus x` | `p *= x` if the recipe has exactly 2 items |
| `FlatPerReaction n` | `bonus += n` (added after multipliers, like `EmptyTileBonus`) |
| `TierDouble t` | `p *= 2` if the result's tier is `t` or more |
| `StarterDouble n` | `p *= 2` if at least `n` ingredients are starters (tier 0; a recipe lists copies separately) |
| `ExtraRedraw n` | `RedrawsLeft` at the start of a depth is `BaseRedraws + n` |
| `HeatPlus n` | in the planner's heat sum, every material counts `n` more (not for hub order) |
| `Salvage` | after the chain, a discovery that was placed and is still standing, unconsumed, goes to the rack instead of being lost |
| `ChainFrom k` | `p *= 2` if the reaction's chain position is `k` or more |
| `DepthScale a` | `p *= 1 + a * (Depth - 1)` |
| `FreeDiscovery` | the first Ignite of each depth that contains a new discovery does not spend a trial |
| `AllScale x` | `p *= x` |
| `SecondChance n` | when `TrialsLeft` would reach 0 without clearing the depth, once per run: `TrialsLeft = n`, phase stays `Playing` |
| `ExtraHand n` | `HandSize += n` (Phase 2 read this as 1; `amount` 0 or missing still means 1) |

`Score` gains a `ScoreContext { int Depth; int EmptyTiles; int[] Tiles; ... }` argument in place of the growing list of
loose parameters; the Phase 2 signature stays as an overload that fills the context.

### 5.4 Starters: 13 (8 existing, 5 new)

The 8 existing starters become Common and stay open. Five new starters are locked (table in 6.1). Unlocked starters
join the satchel (two copies each, as today); hand size does not change, so a larger satchel gives more variety per
hand. The bot measures the effect on win rate (11); if it is harmful, the fallback is one copy of each new starter.

## 6. Pillar D: recipe growth

### 6.1 New materials: 5 starters and 24 discoveries

Rarity of a starter follows the 5.1 test: the Uncommon ones are flexible mid-index materials that fill the two Kinds
the starters lacked (Crystal, Metal) plus a second Beast; the Rare ones bend a plan.

**New starters (locked):**

| id | Name | Kind | Tier | RI | Rarity |
|---|---|---|---|---|---|
| salt_crystal | Salt Crystal | Crystal | 0 | 5 | Uncommon |
| copper_filings | Copper Filings | Metal | 0 | 5 | Uncommon |
| grave_beetle | Grave Beetle | Beast | 0 | 4 | Uncommon |
| lightning_sand | Lightning Sand | Powder | 0 | 10 | Rare |
| hollow_bone | Hollow Bone | Beast | 0 | 1 | Rare |

**New discoveries:**

| id | Name | Kind | Tier | RI |
|---|---|---|---|---|
| brine | Brine | Potion | 1 | 3 |
| verdigris | Verdigris | Powder | 1 | 6 |
| carapace | Carapace | Beast | 1 | 2 |
| static_dust | Static Dust | Powder | 1 | 9 |
| marrow | Marrow | Potion | 1 | 3 |
| spark_quartz | Spark Quartz | Crystal | 1 | 8 |
| bronze | Bronze | Metal | 2 | 4 |
| lye | Lye | Potion | 2 | 7 |
| blue_vitriol | Blue Vitriol | Crystal | 2 | 5 |
| tempest_glass | Tempest Glass | Crystal | 2 | 10 |
| scarab_amulet | Scarab Amulet | Metal | 2 | 3 |
| ossuary_dust | Ossuary Dust | Powder | 2 | 6 |
| thunderstone | Thunderstone | Ore | 2 | 9 |
| quickfire | Quickfire | Potion | 2 | 8 |
| stormcaller_rod | Stormcaller Rod | Metal | 3 | 9 |
| reliquary_bone | Reliquary Bone | Beast | 3 | 3 |
| salt_of_ages | Salt of Ages | Powder | 3 | 5 |
| ball_lightning | Ball Lightning | Potion | 3 | 10 |
| grave_lantern | Grave Lantern | Crystal | 3 | 4 |
| green_bronze | Green Bronze | Metal | 3 | 7 |
| thunder_heart | Thunder Heart | Beast | 4 | 10 |
| ossuary_crown | Ossuary Crown | Metal | 4 | 2 |
| storm_philter | Storm Philter | Potion | 4 | 8 |
| copper_sun | Copper Sun | Crystal | 4 | 6 |

Totals after Phase 3: 81 materials (13 starters, 67 discoveries, 1 last substance).

### 6.2 New recipes: 28

Every row contains at least one new material (invariant 1). Rules kept from Phase 2: 2 to 5 items, at most 2 copies of
one material, the required total is reachable (`min <= total + (5 - items) * 10`). "Company" is the heat that bystanders
must add (`min - total`).

| Items | Result | Min | Company |
|---|---|---|---|
| salt_crystal + deep_tear | brine | 0 | 0 |
| copper_filings + bone_ash | verdigris | 0 | 0 |
| grave_beetle + iron_ore | carapace | 0 | 0 |
| lightning_sand + moondust | static_dust | 0 | 0 |
| hollow_bone + sunbloom | marrow | 0 | 0 |
| lightning_sand + salt_crystal | spark_quartz | 0 | 0 |
| hollow_bone + grave_beetle | carapace | 0 | 0 |
| grave_beetle + bone_ash + deep_tear | marrow | 0 | 0 |
| copper_filings + sky_iron | bronze | 10 | 2 |
| brine + bone_ash | lye | 10 | 2 |
| verdigris + brine | blue_vitriol | 10 | 1 |
| spark_quartz + static_dust | tempest_glass | 10 | 0 |
| carapace + copper_filings | scarab_amulet | 10 | 3 |
| marrow + bone_ash + carapace | ossuary_dust | 10 | 0 |
| static_dust + forge_slag | thunderstone | 10 | 0 |
| spark_quartz + ember_coal | quickfire | 10 | 0 |
| salt_crystal + copper_filings + ember_coal | bronze | 10 | 0 |
| grave_beetle + moonpetal + hollow_bone | ossuary_dust | 10 | 1 |
| tempest_glass + bronze | stormcaller_rod | 14 | 0 |
| scarab_amulet + marrow + hollow_bone | reliquary_bone | 14 | 7 |
| lye + blue_vitriol + moonpetal | salt_of_ages | 14 | 0 |
| quickfire + thunderstone | ball_lightning | 14 | 0 |
| ossuary_dust + scarab_amulet + glowmoss | grave_lantern | 14 | 3 |
| bronze + primal_bloom | green_bronze | 14 | 0 |
| stormcaller_rod + ball_lightning + phoenix_feather | thunder_heart | 22 | 0 |
| reliquary_bone + grave_lantern + void_pearl | ossuary_crown | 22 | 14 |
| ball_lightning + salt_of_ages + aurora_dust + lightning_sand | storm_philter | 22 | 0 |
| green_bronze + sun_amalgam + copper_filings | copper_sun | 22 | 5 |


Shape: 17 pairs, 10 with three items, 1 with four. Required totals follow the result tier as in Phase 2 (0, 10, 14, 22).
Every new starter is in at least three recipes. Every new discovery has a road to it.

### 6.3 Dead ends and hidden chains

None of the 24 new discoveries is an ingredient of a Phase 2 recipe, so none leads to the last substance: the new
branch is a side road that scores well and fills the journal. The four tier-4 results are the dead ends that matter:

- `thunder_heart`: three rare materials, no company needed. The loud route.
- `ossuary_crown`: needs 14 heat from bystanders, so the hub must be the centre tile with two lively neighbours. The
  quiet route, and the hardest recipe in the game to arrange.
- `storm_philter` (four items) and `copper_sun` (three items, company 5).

A tier-4 result is worth 500 base x (items - 1), so a first-time `thunder_heart` is 500 x 2 x 2 = 2,000 before the chain.
The journal now has 87 recipes; "Complete the journal" needs all of them, including the ones behind every locked starter.

## 7. Pillar B: aspects

### 7.1 Model

An **aspect** is a run-long bench modifier. It acts on tiles (or links, or the bench as a whole) and never on the
catalyst list. The run keeps `List<AspectInstance>`:

```csharp
public sealed class AspectInstance { public AspectData Data; public int[] Tiles; public string Kind; }
```

`Tiles` is empty for bench-wide aspects (Greedy Bench), one tile for most, two for Bridge. `Kind` is set only for
Attuned Tile (a random `Kind`).

### 7.2 Grant

At run start, at the start of depth 3 and at the start of depth 5 (and only if `RunOptions.Aspects`):

1. Candidates = the pool minus aspects the run already holds, minus aspects that cannot be placed (see below).
2. Draw 3 distinct candidates with the run's aspect RNG (`seed ^ 0x2545F491`).
3. The player picks one. If fewer than 3 candidates exist, offer what there is. If none, skip.
4. A tile-bound aspect gets its tile (and Attuned Tile its Kind) at draw time, from a free tile (one holding no other
   aspect). The offer card shows that tile, so the player sees where it will land before picking, and the pick keeps it.

Placement rules: Bridge needs two free tiles that are not orthogonal neighbours; Open Bench needs a free corner; any
other tile-bound aspect needs one free tile. With 9 tiles and 3 aspects per run there is always room.

### 7.3 The pool: 16 aspects

`scope`: Tile (one tile), Link (tiles joined), Bench (whole bench). `group`: Cheap (scoring, consuming, hand) or Engine
(changes which tiles touch which, or how the hub is chosen). TO = trade-off.

| id | Name | Group | TO | Rule |
|---|---|---|---|---|
| doubled_tile | Doubled Tile | Cheap | | A reaction whose hub is on this tile scores x2. |
| anchor_tile | Anchor Tile | Cheap | | The material placed here is not lost when a reaction consumes it: a discovery goes to the rack, a starter returns as usual. |
| heated_tile | Heated Tile | Cheap | | A material on this tile counts +3 Reaction Index, for hub order and heat (a tile can reach 13). |
| echo_tile | Echo Tile | Cheap | | The result of a reaction whose hub is on this tile is also sent, as a copy, to the rack. |
| attuned_tile | Attuned Tile | Cheap | | A reaction whose hub is on this tile and whose recipe contains the attuned Kind uses the base potency of the next tier up (tier 5 is the cap). |
| seed_tile | Seed Tile | Cheap | | If this tile is empty at Ignite, every reaction whose hub touches it gains 40 potency. |
| fallow_tile | Fallow Tile | Cheap | | If this tile is empty at Ignite, the next hand draws one more starting material. |
| heavy_tile | Heavy Tile | Cheap | | A reaction whose hub is on this tile counts one chain step higher. |
| cold_tile | Cold Tile | Cheap | TO | The tile is blocked (nothing can be placed there), but each depth has one more trial. |
| volatile_tile | Volatile Tile | Cheap | TO | A reaction whose hub is on this tile scores x3, but a starter placed here when you Ignite is lost for the run (it does not return to the satchel). Discoveries are unaffected. |
| greedy_bench | Greedy Bench | Cheap | TO | The hand is one card bigger, but you have one redraw fewer each depth. |
| bridge | Bridge | Engine | | Two tiles that are not neighbours are linked: they count as neighbours of each other. |
| open_bench | Open Bench | Engine | | A corner tile is also linked to the centre tile. |
| kiln_tile | Kiln Tile | Engine | | A hub on this tile ignores the recipe's required heat. |
| crossroads | Crossroads | Engine | | A hub on this tile also counts its diagonal neighbours. |
| pivot_tile | Pivot Tile | Engine | | If the material on this tile can react, it is the hub of the next reaction whatever the Reaction Index of the others. |

Three of the 16 are trade-offs (about a fifth). Plan correction: the plan and review page said four. An earlier draft had
Hollow Tile and Mirror Tile; Hollow could only lower heat (heat is only ever a floor, so it never helps to lower it) and
Mirror was too costly for its effect, so they became Crossroads and Pivot Tile.

Cheap aspects ship in step 3.3 and Engine aspects in 3.4. Owner decision: the Engine ones are built only if the
neighbour-rule hook turns out small, so step 3.4 stays separate and cuttable.

### 7.4 Engine: BenchRules

```csharp
public sealed class BenchRules {
  public bool[] Blocked = new bool[9];
  public int[]  RiBonus = new int[9];            // Heated Tile; also event "heat a tile"
  public bool[] Kiln = new bool[9], Crossroads = new bool[9];
  public int    Pivot = -1;                      // tile, or -1
  public List<(int a, int b)> Links = new();     // Bridge, Open Bench
  public int    RiShift;                         // events: every material, this depth only
  public int    HeatPlus;                        // Warm Bench
  public bool   Diagonals;                       // Crooked Mirror (replaces the loose bool)
  public static readonly BenchRules None = new BenchRules();
}
```

`RunState` owns the rules and rebuilds them from catalysts, aspects and the current depth's mods whenever one changes.
Planner and resolver read them:

| Place | Change |
|---|---|
| `ChainPlanner.Neighbours(tile, rules)` | base orthogonal, plus diagonals if `Diagonals` or `Crossroads[tile]`, plus `Links`; blocked tiles are never neighbours |
| `ChainPlanner.Ri(tile, id, rules)` | `clamp(db.Reactivity(id) + RiBonus[tile] + RiShift, 1, 13)`; used for hub order |
| `ChainPlanner.Heat` | sums `Ri(...) + HeatPlus` over the hub and its neighbours |
| `ChainPlanner.Find` | `Kiln[hub]` skips the `heat < minReactivity` test |
| `ChainPlanner.Plan` | hub order: `Pivot` first if it has a match, then by `Ri`, then earlier tile |
| `IgniteResolver.Pairs` / `HasAnyPair` | undirected: a pair exists if either tile reaches the other; blocked tiles excluded |
| `IgniteResolver.Score` | `Doubled`, `Volatile`, `Heavy`, `Attuned`, `Seed` read the hub tile (`tiles[0]`) |
| `RunState.Place` | refuses blocked tiles |
| `RunState.Ignite` | Anchor, Echo, Volatile, Fallow act here (they change what returns, not the chain) |

When `rules` is null the code behaves exactly as in Phase 2 (a test compares the two on the pinned benches).

### 7.5 UI

A tile with an aspect shows a small marker (icon in a corner, drawn over the tile, never over the glow); a blocked tile
shows a dark lid. Extra links use the existing `LinkView` line with the aspect's colour. The HUD gets a strip of the
active aspects (icons); hovering a marker or a strip icon shows the `Tooltip` with name and rule. Granting uses the
three-card chooser: name, rule text, a tile preview, a TO badge on trade-offs showing the cost line in a second colour.

## 8. Pillar C: depth events

### 8.1 Flow

At the start of every depth (1 to 5), after the aspect stage if there is one, `RunState` draws one event from the pool
(only if `RunOptions.Events`). The event RNG is `seed ^ 0x7F4A7C15`, stepped once per depth so a seed reproduces a run.

Draw: eligible = events whose depth range contains the depth, and which have at least one option the run can take
(an option is takeable when its `requires` and `cost` can be paid; the "walk on" option always can). Weighted by
`weight`, plus `noEventWeight` for "nothing happens". The result is an `EventData` or none.

The player sees the text and the takeable options, picks one, and sees the outcome (when an option has several
outcomes, one is drawn by weight with the same RNG). The effects apply, the result line shows, the player continues.

### 8.2 Effects (`EffectData.op`)

Duration is the current depth unless stated. "Random" uses the event RNG.

| op | arg / amount | Meaning |
|---|---|---|
| `Trials` | amount (+/-) | Change this depth's trials |
| `Hand` | amount (+/-) | Change this depth's hand size |
| `Redraws` | amount (+/-) | Change this depth's redraws (floor 0) |
| `RiShift` | amount (+/-) | Every material counts that much more or less Reaction Index this depth (floor 1) |
| `GiveDiscovery` | amount = min tier, amount2 = max tier | A random discovery of that tier range goes to the rack |
| `LoseRack` | amount | Remove that many random discoveries from the rack (cost) |
| `LoseHandCard` | amount | Remove that many random starters from the hand |
| `FirstTriple` | | The first reaction this depth scores triple |
| `Hint` | | Reveal a journal hint for a random unfinished material |
| `BlockTile` | | A random free tile is blocked this depth |
| `HeatTile` | amount = bonus (default 3) | A random free tile gets that Reaction Index bonus this depth |
| `CatalystUpgrade` | | Swap a random held catalyst for a random open, unheld catalyst of the next rarity up (nothing for a held Legendary) |
| `CatalystOffer` | | Offer the usual three catalysts now |
| `Nothing` | | No effect (Lore) |

`requires` values: `Rack` (rack has a discovery), `Catalyst` (run holds a catalyst), `Trial` (at least 2 trials this
depth), `Redraw` (at least one redraw this depth). A `cost` of `Trials` negative is only allowed when `requires` says `Trial`. `Hand` and `Trials` cannot take a
depth below 1 trial or 3 cards.

### 8.3 The 16 events

`weight` 3 = common, 2 = usual, 1 = rare. Depth range in brackets. Every event also has a "Walk on" option (no
requirement, no effect), not repeated below.

| id | Type | Depths | Weight | Text | Option(s) |
|---|---|---|---|---|---|
| cracked_altar | Choice | 1 to 4 | 2 | A cracked altar asks for an offering. | Give a rack discovery: cost LoseRack 1 (requires Rack), gain Trials +1 |
| tilted_workbench | Choice | 1 to 5 | 2 | The bench is tilted, but roomy. | Take two more cards, lose a trial: Hand +2, Trials -1 (requires Trial). Or: Redraws +1 |
| old_ledger | Choice | 2 to 5 | 2 | A damp ledger lists what others tried. | Read it: cost Redraws -1 (requires Redraw), gain Hint |
| hungry_furnace | Choice | 2 to 5 | 2 | A furnace gapes. | Feed it a rack discovery: cost LoseRack 1 (requires Rack), gain FirstTriple |
| dripping_well | Gamble | 1 to 5 | 2 | Something glints in a dripping well. | Reach in: 50% Trials +1, 50% LoseHandCard 1 |
| shifting_bench | Gamble | 2 to 5 | 2 | The bench grinds and shifts. | Let it settle: 50% HeatTile, 50% BlockTile |
| smugglers_crate | Gamble | 3 to 5 | 2 | A crate, half open. | Open it: 70% GiveDiscovery tier 1 to 3, 30% BlockTile |
| forgotten_satchel | Find | 1 to 3 | 2 | A forgotten satchel, still warm. | Take it: GiveDiscovery tier 1 to 2 |
| glinting_seam | Find | 3 to 5 | 2 | A seam of something glinting. | Take it: GiveDiscovery tier 2 to 3 |
| sealed_shrine | Shrine | 2 to 5 | 1 | A sealed shrine hums. | Offer a catalyst (requires Catalyst): CatalystUpgrade |
| quiet_shrine | Shrine | 3 to 5 | 1 | A quiet shrine, patient. | Kneel for two trials (requires Trial): cost Trials -2, gain CatalystOffer |
| damp_air | Hazard | 2 to 5 | 1 | The air is damp; everything cools. | Forced: RiShift -1 (no walk-on; the single option is "Go on") |
| falling_grit | Hazard | 2 to 5 | 1 | Grit falls from the ceiling. | Forced: BlockTile |
| cold_draft | Hazard | 3 to 5 | 1 | A cold draft empties your pockets. | Forced: Hand -1 |
| warm_stone | Boon | 1 to 5 | 1 | A warm stone in the wall. | Lean on it: HeatTile |
| quiet_room | Lore | 1 to 5 | 3 | A quiet room. Dust turns slowly in the light. | Nothing |

`noEventWeight` = 3. Hazards are forced (a single "Go on" option), which is the one exception to the walk-on rule;
the validation test accepts an event whose only option has no cost and no `requires`.

### 8.4 Event rules checked by tests

- An event never makes a depth impossible: for every event and depth, a bot run starting at that depth with the
  event forced wins at least 70% as often as the same run with no event (balance test, section 11).
- A cost that cannot be paid never appears as an option (`LoseRack` with an empty rack, etc.).
- The same seed gives the same sequence of events.
- Effects last the current depth and are cleared by `BeginDepth`; the tests check that nothing leaks to the next one.

## 9. Run flow

### 9.1 Phases

`RunPhase` gains `AspectOffer` and `EventOffer` (before `Playing`). Phase 2 runs (`Legacy`) never enter them.

```
RunState created ──► BeginDepth ──► [AspectOffer] ──► [EventOffer] ──► Playing
                          ▲            (depth 1, 3, 5)     (every depth)     │ Ignite…
                          │                                                  ▼
         ChooseCatalyst ◄─┴──────────── DepthCleared ◄──────────────────── (target met)
                                         Lost / Won end the run
```

`BeginDepth` order: reset depth mods and `BenchRules` depth part; set base trials/redraws; if an aspect stage applies,
build the offer and stop in `AspectOffer`; then draw the event and stop in `EventOffer` if there is one; then apply
effects, `FillHand`, and enter `Playing`.

### 9.2 RunState additions (public surface)

```csharp
IReadOnlyList<AspectOfferItem> AspectOffer;   void ChooseAspect(string id);          // Phase AspectOffer
EventData CurrentEvent; IReadOnlyList<EventOption> EventOptions;                       // Phase EventOffer
EventResult ChooseEventOption(int index);                                              // applies, returns the result line
IReadOnlyList<AspectInstance> Aspects; BenchRules Rules;
int RedrawsUsed;                                                                       // for wonNoRedraw
```

`Place`, `Redraw`, `Ignite` already refuse when `Phase != Playing`.

### 9.3 GameFlow

`PlayRun` and `Descend` call a shared `StartOfDepth()` coroutine after the descent cutscene (or after the title for
depth 1): if `Run.Phase == AspectOffer` show the chooser and call `ChooseAspect`; if `EventOffer` show `EventView`
and call `ChooseEventOption`; then bench. The catalyst chooser at the end of a depth is unchanged except that its
offer is built from `Unlocks`. After each Ignite `GameFlow` records stats, evaluates goals, stores the profile and shows
a toast for each new goal. New `FlowState` values: `AspectChoice`, `Event`, `Goals`.

## 10. Screens

- **Goals page** (title button; also opened from the run-over screen): three sections, Uncommon, Rare, Legendary. Each
  goal is a row: text, a progress bar, and the reward line. The reward line before completion names only kind and
  rarity: "Unlocks a new Uncommon starter". After completion it shows the item and its icon. Completed rows sort to the
  bottom of their section. Same parchment style as the Journal; close with the same button.
- **Goal toast**: top of the bench, 2.5 seconds, queued, shown after the Ignite resolves: "Goal complete: Reach depth 3.
  Unlocked: Tier Seeker". Clicking skips it. It never delays the next Ignite.
- **Aspect chooser**: three cards: icon, name, rule, tile preview (mini 3x3 with the tile lit), TO badge with cost.
- **Event panel**: title, text, 1 to 3 option buttons (disabled with a reason when not takeable, or hidden: my call,
  hidden), a result line after the choice and a Continue button.
- **Title**: Journal, Goals, Reset progress. Reset asks "Erase goals and unlocks? This cannot be undone." with Cancel and
  Erase, and clears the profile only; the journal is kept. My call: the journal is the player's knowledge of recipes, and
  erasing it is a separate decision for a later phase.
- **Run over**: a line listing goals completed this run.
- **Card rarity**: catalyst and starter cards show their rarity as a coloured corner pip (grey, green, blue, amber).

Layout reuses the Phase 2 pixel grid (`Px`), fonts (`FontKind`) and the existing `Make` helpers. No new fonts.

## 11. Bot and balance

### 11.1 AutoPlayer

- `Decide` handles `AspectOffer` and `EventOffer`.
- Informed policy: aspect by a fixed ranking (Kiln, Heated, Doubled, Echo, Anchor, Greedy, Pivot, Attuned, Seed,
  Heavy, Fallow, Crossroads, Bridge, Open Bench, Volatile, Cold; my call, to be tuned) and a tile choice already made by the draw;
  event option by simple rules (take free gains, pay costs only for Trials +1 or Catalyst gains, walk on otherwise).
- Random policy: uniform over the takeable options (the baseline that must stay near 0%).
- Planning uses the run's `BenchRules` through the planner.

### 11.2 Profiles and targets

`BalanceReport` (in `RunStateTests.cs`) gains a profile argument: **fresh** (`Unlocks.Fresh`), **mid** (half of every
locked bucket, chosen by seed), **full** (`Unlocks.All`). Owner targets, informed bot, 200 seeds:

| Profile | Informed bot | Random player |
|---|---|---|
| Fresh | 60 to 70% | near 0% |
| Full | 80 to 90% | near 0% |

Phase 2 ended at 85% with no aspects or events, so a fresh profile must come down. Tuning knobs, in this order:
event weights and hazard strength, depth targets, aspect amounts. Nothing is removed from Phase 2 content to hit a
number. Other checks:

- Each rarity pays: bot win rate rises with catalysts held, by rarity bucket (Common < Uncommon < Rare < Legendary on average).
- The 13-starter satchel does not lower the fresh+all-starters win rate by more than 5 points against 8 starters.
- Goal pacing with a fresh bot over 50 consecutive runs (profile persisting): Uncommon goals complete in the first
  10 runs, Rare in the first 40, Legendary mostly not. Numbers in 4.3 are adjusted to fit.
- Hazards: see 8.4.

## 12. Save and migration

- Key `athanor.profile.v1`: JSON of `ProfileData { int version; stats; string[] completed, unlockedStarters,
  unlockedCatalysts; RewardLogData[] rewards; int rewardSeed }`. Stored after every Ignite, depth change and run end,
  and on completing a goal. Missing or unreadable data gives a new profile with a random `rewardSeed`.
- The journal key is unchanged. A returning player's journal counts for the journal goals on first load.
- Reset progress deletes `athanor.profile.v1` only.
- Web: PlayerPrefs is browser storage already; the profile is a few hundred bytes.
- No Phase 2 content is renamed, so old saves keep working.

## 13. Tests

Core, run by `dotnet test tools/CoreTests` and again in Unity EditMode:

| Area | Tests |
|---|---|
| Content | schema loads; old JSON (no new fields) loads as Phase 2; **goal budget per bucket**; no locked Common items; no Legendary starter; unique ids; every event has a walk-on or a free single option; every new recipe contains a new material; the 59 Phase 2 recipes are pinned |
| Profile | stats update on a scripted run; failed runs count; `FromData` of garbage returns an empty profile; round trip |
| Goals | progress math; a goal completes once; reward comes from its bucket; same seed gives the same reward; an empty bucket does not crash; journal goals complete on load |
| Unlocks | `Fresh` has 9 catalysts and 8 starters open; `All` has everything; the offer and satchel respect it |
| Catalysts | each new effect: one test with a hand-built bench; `SecondChance` once only; `FreeDiscovery` once per depth |
| BenchRules | `None` equals Phase 2 on the pinned benches; each aspect on a small bench; blocked tile refuses `Place`; links appear in `Pairs`; Pivot and Kiln change hub and heat as stated |
| Aspects | offer is 3 distinct, excludes held and unplaceable; tile placement never collides; seeded |
| Events | eligibility by depth and cost; weights; seeded; effects expire; hazard floors |
| Flow | `RunState` walks `AspectOffer`, `EventOffer`, `Playing` in order at depths 1, 3, 5 and `EventOffer` only at 2 and 4; `Legacy` skips both |
| Game (Unity EditMode) | `Save` profile round trip and corrupt-data tests |
| Balance | the report with profiles and the hazard comparison (not part of the default run; as in Phase 2) |

## 14. Risks and calls for the owner

1. **Fresh-profile difficulty.** The 60 to 70% target is below Phase 2's 85%, and aspects (buffs) push it up. If events
   alone cannot bring it down, depth targets move. This is the main tuning risk.
2. **Satchel dilution** with 13 starters (5.4). Measured; fallback is one copy of each new starter.
3. **Locked Commons.** None exist, by the validation rule, because no goal pays Common. Adding a Common reward bucket
   later is a content change plus a rule change.
4. **Rewards are random.** A player may win a Rare starter before they can use it well. Accepted (owner).
5. **Goal numbers** are placeholders until the bot has run (11.2).
6. **Aspect pivot rules** are the part most likely to need revising once the engine hook exists: Pivot and Crossroads
   make the hub rule asymmetric. The hub rule for Phase 2 is documented; this spec changes it only when the aspect is held.
7. **Art volume.** 29 material icons, 16 catalyst icons, 16 aspect icons, 7 event-type icons, rarity pips, tile markers.
   All generated by `tools/art`; none drawn by hand.

## 15. Work breakdown (input to the plan)

| Step | Scope | Exit |
|---|---|---|
| 3.0 | `Profile`, `Stats`, `Goals`, `Unlocks`, schema, validation, `RunOptions`, save | content and profile tests green |
| 3.1 | Rarity fields, 16 new catalysts, new effects, goals data, Goals page, toast, pool filtering | a run unlocks a catalyst end to end |
| 3.2 | 5 starters, 24 discoveries, 28 recipes, icons, journal fits 87 entries | content tests, art export, journal checked in Unity |
| 3.3 | `BenchRules`, the 11 Cheap aspects, new phases, chooser, tile markers | aspects work in the bot and on screen |
| 3.4 | The 5 Engine aspects (cuttable) | `None` rules equal Phase 2; each hook tested |
| 3.5 | Events: system, 8 then 16, `EventView` | seeded events, expiry, hazards bounded |
| 3.6 | Balance, polish, web build, devlog and handoff | owner targets met or reported |

Each step ends with `dotnet test tools/CoreTests` and, from 3.1, `unity test Athanor --mode EditMode`. The web build is
rebuilt and checked in headless Chrome at 3.6.
