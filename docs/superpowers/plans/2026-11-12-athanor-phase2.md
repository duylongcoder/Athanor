# Athanor Phase 2 Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Replace the one-pass pair resolver with a Reaction-Index-driven chain engine that supports 2 to 5 item combos, consume everything placed on the bench, show hints in the Journal, add combo content, and rebalance with depth targets 300 / 1,500 / 7,500 / 37,500.

**Architecture:** A new pure planner (`ChainPlanner`, Core, no Unity) turns a bench layout into an ordered list of steps (hub tile, consumed tiles, recipe) plus the final layout. `IgniteResolver.Resolve` wraps it with scoring, journal learning and fizzle hints; `RunState.Ignite` applies the consumption rules; the bot (`AutoPlayer`) simulates through the same planner so it can never disagree with the game. The Game layer plays steps one by one, putting each result in its hub tile.

**Tech Stack:** C# (Core is plain, no Unity), NUnit via `dotnet test tools/CoreTests` and Unity edit mode, Unity 6000.6.7f1 WebGL, Node art generator `tools/art/`.

**Spec:** `docs/superpowers/specs/2026-11-12-athanor-phase2-design.md` (Phase 1 spec: `2026-11-11-athanor-design.md`; ledger: `.superpowers/sdd/2026-11-11-athanor/progress.md`)

## Global Constraints

- Hub rule: the occupied tile with the highest Reaction Index (RI) that has a matching recipe is the hub; RI ties go to the earlier tile (reading order).
- Priority: Reaction Index, then large combos, then small combos. Recipes have 2 to 5 items; at most 2 copies of one material per recipe.
- A recipe whose items' RIs sum to less than `minReactivity` does not exist for matching: no reaction, no message.
- A combo is a hub plus distinct occupied orthogonal neighbours (diagonals too only with the Diagonals catalyst); max 5 items. Corner hub: up to 3 items, edge-middle: 4, centre: 5.
- A reaction consumes its items; the result appears in the hub tile and takes part in the next round. Resolution ends when nothing matches.
- RI values 1 to 10, spread across tiers 0 to 4: each tier has at least one value <= 3 and one >= 8.
- Placed starters return to the satchel; placed discoveries are used up; only results made during the Ignite and still on the bench go to the rack (max 12, Phase 1 push-out rule: weakest, then oldest).
- Potency = base(tier) x (items - 1) x 2 if first this run x chain index (chain + 1 with ChainPlus), then catalysts. Base table unchanged: 20, 60, 180, 500, 1500.
- Depth targets: 300, 1500, 7500, 37500 (ratio 5). Depth 5 has none.
- `content.json` recipes: `{"items":[...],"minReactivity":N,"result":"id"}`; the loader still accepts `{a,b,result}`. Journal keys stay sorted ids joined with `+`, so saves from Phase 1 load unchanged.
- No git commits (the owner has not asked); record each finished task in the ledger instead. Close the Unity editor before `unity test` / `unity build`.

## Review Focus

Inputs the spec implies but no task would otherwise test; each has its test in the named task.

1. A bench with nothing adjacent, or a single item: Ignite stays disabled and nothing throws (Task 2, 3).
2. All nine tiles holding the same material, or ties on RI everywhere: the planner terminates, order is by tile index (Task 2).
3. A chain that makes several results at once into a full 12-slot rack: never above 12, the ultimate is never pushed out (Task 3).
4. A Phase 1 save (pair keys) and a damaged one: load and round-trip with combo keys, no exception (Task 1).
5. Content with a 6-item recipe, an unknown id, a duplicate item set or a missing/out-of-range RI: `ContentDb` throws `ArgumentException` (Task 1).

---

## File Structure

| File | Change | Responsibility |
|---|---|---|
| `Athanor/Assets/Scripts/Core/ContentData.cs` | modify | `reactivity`, `RecipeData.items/minReactivity` |
| `Athanor/Assets/Scripts/Core/ContentDb.cs` | modify | multiset recipe index, RI lookups, validation |
| `Athanor/Assets/Scripts/Core/Journal.cs` | modify | combo keys, `IsComplete`, `OpenHints` |
| `Athanor/Assets/Scripts/Core/Chain.cs` | **create** | `ChainPlanner`, `ChainStep`, `ChainPlan`: pure hub / match / chain logic |
| `Athanor/Assets/Scripts/Core/Ignite.cs` | modify | `Bench`, `ReactionEvent`, `IgniteResolver` (scoring, fizzles, hints) on top of the planner |
| `Athanor/Assets/Scripts/Core/RunState.cs` | modify | consumption, rack intake, outcome |
| `Athanor/Assets/Scripts/Core/AutoPlayer.cs` | modify | simulate through the planner, recipe-driven openings |
| `Athanor/Assets/Scripts/Game/**` | modify | chain playback, links, note, cutscene, Journal UI |
| `Athanor/Assets/Resources/content.json`, `tools/art/icons.mjs` | modify | RI table, ~12 materials, ~14 recipes, targets |
| `Athanor/Assets/Tests/EditMode/*` | modify/create | tests per task; `ChainTests.cs` new |
| `docs/devlogs/phase-2-reactivity-and-chains.md`, `README.md` | modify | log and docs |

---

### Task 1: Data model, content lookups, journal API

**Files:**
- Modify: `Core/ContentData.cs`, `Core/ContentDb.cs`, `Core/Journal.cs`, `Resources/content.json` (reactivity only), `Tests/EditMode/TestContent.cs`, `ContentTests.cs`, `SatchelTests.cs` (Journal tests live here), `Tests/EditModeGame/SaveTests.cs`

**Interfaces:**
- Produces (all in namespace `Athanor.Core`):
  - `MaterialData.reactivity : int`; `RecipeData { string[] items; int minReactivity; string result; string a, b; }`
  - `ContentDb.MinItems = 2, MaxItems = 5, MaxCopies = 2` (const int)
  - `static string ComboKey(IEnumerable<string> items)` (ordinal sort, join `+`); `PairKey(a,b)` = `ComboKey`
  - `int Reactivity(string id)`; `int TotalReactivity(RecipeData r)`; `string KeyOf(RecipeData r)`
  - `bool TryCombo(IEnumerable<string> items, out RecipeData recipe)` (exact multiset, ignores `minReactivity`)
  - `IReadOnlyList<RecipeData> RecipesContaining(string id)`: sorted by item count desc, then `TotalReactivity` desc, then file order
  - `IReadOnlyList<RecipeData> Recipes`: every recipe with `items` filled (legacy `a,b` folded in)
  - Kept: `TryReact(a,b,out result)`, `PartnersOf(id)` (distinct materials sharing any recipe), `Material`, `KindOf`, `Starters`, `TargetFor`, `BasePotency`
  - `Journal.Learn(IEnumerable<string>)`, `Journal.IsKnown(IEnumerable<string>)` (the two-string overloads stay); `bool IsComplete(string material, ContentDb db)`; `IEnumerable<HintData> OpenHints(string material, ContentDb db)` (the material's hints, none once `IsComplete`)
- Consumes: nothing.

- [ ] **Step 1: Write failing tests.** `ContentTests`: `Recipe_ThreeItems_LookedUpUnordered` (build content with `items {"p","q","r"}`; `TryCombo(["r","p","q"])` true); `Legacy_AB_Recipe_FoldedIntoItems` (`Recipes[0].items` equals `{a,b}`); `RecipesContaining_LargestFirst`; `Constructor_Throws_On6Items`, `..._OnThreeCopies`, `..._OnDuplicateItemSet`, `..._OnReactivityMissingOrOutOf1To10`, `..._OnMinReactivityAboveTotal` (each `Assert.Throws<ArgumentException>`); `Reactivity_SpreadAcrossTiers` on the real content: for tiers 0..4 `min <= 3 && max >= 8`, and `Reactivity_NotMonotonicInTier` (some tier-0 RI is greater than some tier-4 RI). `SatchelTests` (Journal): `IsComplete_OnlyWhenAllRecipesWithMaterialKnown`; `OpenHints_HiddenOnceComplete`; `Learn_ComboKey_RoundTripsThroughToDataFromData`. `SaveTests`: `Phase1_PairKeys_StillLoad` and a combo key `"a+b+c"` round trip; a damaged blob still gives an empty journal.
- [ ] **Step 2: Run** `dotnet test tools/CoreTests --filter "FullyQualifiedName~ContentTests|FullyQualifiedName~SatchelTests"`. Expected: compile errors, then FAIL.
- [ ] **Step 3: Implement** the interfaces above. `TestContent` gains `Combo(int zMin = 15)` (see Task 2 for the table) and `Rec(...)` overloads taking `string[] items, int min`; `Mat(...)` takes `reactivity`. Existing `Tiny()` materials get RIs (any 1..10).
- [ ] **Step 4: Give every one of the 40 existing materials a `reactivity` in `content.json`**, satisfying the spread tests (choose the values yourself; record the table in the ledger). Leave all recipes in the old `a,b` form with no threshold for now.
- [ ] **Step 5: Run** `dotnet test tools/CoreTests`. Expected: all pass (the 65 Phase 1 tests plus the new ones).
- [ ] **Step 6: Ledger line** "Task 1: complete" with the RI table.

---

### Task 2: Chain planner (hub, matching, chain, sputter)

**Files:**
- Create: `Core/Chain.cs`, `Tests/EditMode/ChainTests.cs`
- Modify: `Tests/EditMode/TestContent.cs`

**Interfaces:**
- Consumes: Task 1 (`ContentDb.RecipesContaining`, `Reactivity`, `TotalReactivity`, `RecipeData.minReactivity`).
- Produces:
  - `ChainStep { int Hub; int[] Tiles; RecipeData Recipe; bool Sputter; }` where `Tiles[0] == Hub` and the rest are the consumed neighbours (ascending). For a sputter step `Recipe` is the spoiled combo and `Tiles` its would-be layout.
  - `ChainPlan { List<ChainStep> Steps; string[] Final; bool[] Made; }`. `Steps` holds reactions and sputters in order; `Final` is the 9-tile layout after the chain; `Made[t]` is true where a result made during the plan still stands.
  - `static ChainPlan ChainPlanner.Plan(string[] tiles, ContentDb db, bool diagonals)` (does not mutate `tiles`).
  - `static ChainStep ChainPlanner.MatchAt(string[] tiles, int hub, ContentDb db, bool diagonals)`: the best recipe around `hub`, or `null`.
  - `static IEnumerable<int> ChainPlanner.Neighbours(int tile, bool diagonals)`.

`TestContent.Combo(int zMin = 15)`: starters `p`(RI 8) `q`(5) `r`(2) `s`(9) `t`(4) `v`(1); `x`(6, tier1) = p+q, min 10; `u`(3, tier2) = x+v, min 6; `z`(7, tier2) = p+q+r, min `zMin`; `m`(4, tier1) = s+q, min 10; `big`(5, tier3) = s+p+q+r+t, min 28; `quad`(5, tier3) = s+p+q+t, min 24.

- [ ] **Step 1: Write failing tests** in `ChainTests` (bench = 9-element `string[]`, `null` empty):
  - `Hub_IsHighestReactivity`: q at 0, p at 1 -> first step `Hub == 1` (p RI 8 > q RI 5), `Final[1] == "x"`, `Final[0] == null`.
  - `Hub_RiTie_GoesToEarlierTile`: p at 1 and p at 3 with q at 4 -> hub 1 is tried first; assert `Steps[0].Hub` equals the lower-index tile that has a match.
  - `Hub_WithoutMatch_DoesNotBlockOthers`: s at 8 (no neighbour partner) plus p at 3, q at 4 -> step hub 3.
  - `LargestComboFirst`: p at 4, q at 3, r at 5 -> one step, `Recipe.result == "z"`, `Tiles.Length == 3`, `Final[4] == "z"`, tiles 3 and 5 null.
  - `ShortOfReactivity_IsNoReaction_FallsBackToSmaller`: same layout with `Combo(16)` -> step `Recipe.result == "x"`, `Final[5] == "r"`.
  - `Chain_ResultReactsAgain`: p at 3, q at 4, v at 0 -> two steps: `x` then `u`; `Final[3] == "u"`, `Made[3]`, all other tiles null.
  - `Corner_CannotHostFourItems`: s at 0, p at 1, q at 3, t at 4 -> `quad` does not fire (corner has 2 neighbours); no `quad` step.
  - `Centre_HostsFiveItems`: s at 4, p at 1, q at 3, r at 5, t at 7 -> one step, `Recipe.result == "big"`, `Tiles.Length == 5`, `Final[4] == "big"`.
  - `Diagonals_Count_AsNeighbours_ButCapAtFive`: with `diagonals = true`, s at 4 and the four others on the corners -> `big` fires; nine identical items terminate.
  - `PreEmpted_Combo_Sputters`: p at 4, q at 3, r at 5, s at 0 -> first step is s+q -> `m` (hub 0), followed by a `Sputter` step whose `Recipe.result == "z"` and `Tiles` contain 4, 3, 5; `Final[4] == "p"`.
  - `Pair_Preemption_DoesNotSputter` (sputters only for 3+ item recipes).
  - `NineIdenticalItems_Terminates_NoSteps` and `EmptyAndSingleBench_NoSteps`.
- [ ] **Step 2: Run** `dotnet test tools/CoreTests --filter FullyQualifiedName~ChainTests`. Expected: FAIL (types missing).
- [ ] **Step 3: Implement `ChainPlanner`.** Match rule at a hub: for each recipe in `RecipesContaining(item(hub))` (already ordered size desc, then total RI desc), skip if `TotalReactivity < minReactivity`, otherwise try to give every remaining item (the recipe's multiset minus one copy of the hub's item) its own distinct occupied neighbour holding that id, lowest tile indices first; first recipe that fits wins. Round: occupied tiles by RI desc then index; first with a match is the hub; apply (consumed tiles null, hub tile = result, `Made[hub] = true`, consumed tiles' `Made` false). Sputter: before applying a step, for every other occupied tile compute `MatchAt`; if that match has `Tiles.Length >= 3` and shares a tile with the step being applied, emit one `Sputter` step per distinct tile set (after the reaction step). Each reaction removes at least one item, so the loop terminates.
- [ ] **Step 4: Run** the same filter. Expected: PASS.
- [ ] **Step 5: Ledger line** "Task 2: complete".

---

### Task 3: Resolver, scoring, hints, consumption

**Files:**
- Modify: `Core/Ignite.cs`, `Core/RunState.cs`, `Tests/EditMode/IgniteTests.cs`, `RunStateTests.cs`
- Mark `[Ignore("until Task 5")]`: `RunStateTests.ScriptedRun_ReachesWon`, `InformedPlayer_WinsMostSeeds`

**Interfaces:**
- Consumes: Task 2 (`ChainPlanner.Plan`), Task 1 (`Journal`, `ContentDb`).
- Produces:
  - `ReactionEvent { int Hub; int[] Tiles; string[] Items; string Result; RecipeData Recipe; bool Discovery, FirstThisRun, Sputter; long Potency; int Chain; HintData Hint; }`. Reactions: `Tiles[0]` is the hub, `Items` the consumed ids in tile order. Fizzle (`Result == null && !Sputter`): `Tiles`/`Items` are the two materials of the pair. `A, B, TileA, TileB, Repeat` are removed.
  - `ResolveResult { List<ReactionEvent> Events; string[] Final; bool[] Made; }` and `static ResolveResult IgniteResolver.Resolve(Bench bench, ContentDb db, Journal journal, ISet<string> fired, IReadOnlyList<CatalystData> catalysts)` (does not change `bench`). Events order: reactions and sputters as planned, then fizzles for every adjacent occupied pair left in `Final`, in reading order.
  - `Bench`, `Pairs`, `HasAnyPair`, `Has` unchanged. `RunState.HasFired(RecipeData)` replaces the two-string version (keep the old one as a wrapper). `RunState.Ignite()` returns `IgniteOutcome` as before; `Events`, `Gained`, `Dropped`, `PhaseAfter`.
  - `Effects.CentreTwice` now doubles the potency of a reaction whose tiles include the centre.

- [ ] **Step 1: Write failing tests.**
  - `IgniteTests` (using `Combo()`): `PairScores40_FirstRun` (p,q -> x: 20 x 1 x 2); `ThreeItem_Scores240` (z: 60 x 2 x 2); `FiveItem_ScoresFourTimesBase` (big tier 3: 180 x 4 x 2 = 1440); `SecondReaction_MultipliedByChain` (the p,q,v chain: first event `Chain == 1`, `Potency == 40`; second event `Chain == 2`, `Potency == 60 * 1 * 2 * 2 == 240`: tier-2 base, pair, first this run, second in chain); `ChainPlus_AddsOne`; `CentreTwice_DoublesPotency_WhenCentreInvolved` (and not otherwise); `KindDouble_ChecksEveryConsumedItem`; `Journal_LearnsComboKey`; `Fizzle_AfterChain_ForLeftoverPair_GivesHintOnce`; `UnderThreshold_Combo_Fizzles_NotSputters`; `Sputter_Event_HasNoPotencyAndNoDiscovery`; `ResolveDoesNotMutateBench`.
  - `RunStateTests`: `PlacedDiscovery_NeverReacting_IsLost` (rack unchanged, not in hand); `PlacedStarters_AlwaysReturnToSatchel`; `ChainSurvivor_GoesToRack_ConsumedIntermediateDoesNot` (p,q,v chain: rack gains `u` only); `Ultimate_AnywhereInChain_Wins`; `FullRack_SeveralResults_StaysAt12_NeverDropsUltimate`; `CanIgnite_FalseWithoutAdjacentPair`; `Unplace_StillWorksBeforeIgnite`; `FizzledDiscovery_NoLongerReturnsToHand`.
- [ ] **Step 2: Run** `dotnet test tools/CoreTests --filter "FullyQualifiedName~IgniteTests|FullyQualifiedName~RunStateTests"`. Expected: FAIL.
- [ ] **Step 3: Implement `Resolve`.** Run `ChainPlanner.Plan(bench.Tiles, db, diagonals)`; turn each reaction step into an event (`journal.Learn(items)`, `fired.Add(key)`, potency by the Global Constraints formula, `Chain` counts reactions only); then fizzles from `Final` using the Phase 1 hint search generalised to recipes: for each of the pair's two materials, for each unknown recipe containing it, for each other item's kind not yet hinted, `journal.AddHint`. `empty` for `EmptyTileBonus` is the number of empty tiles before the Ignite.
- [ ] **Step 4: Implement `RunState.Ignite`** per the consumption rules: snapshot the placed tiles, resolve, send every placed starter to the satchel, drop every placed discovery, add `Final` tiles flagged `Made` to the hand in tile order using the existing push-out loop, `Won` if any reaction result is the ultimate.
- [ ] **Step 5: Run** the filter, then `dotnet test tools/CoreTests`. Expected: all pass except the two ignored autoplay tests.
- [ ] **Step 6: Ledger line** "Task 3: complete" (note the ignored tests).

---

### Task 4: Content migration: items form, thresholds, depth targets

**Files:**
- Modify: `Resources/content.json`, `Tests/EditMode/ContentTests.cs`

- [ ] **Step 1: Failing tests.** Change `DepthTargetsAre_300_900_2500_7000` to `DepthTargetsAre_300_1500_7500_37500` (`TargetFor(1..4)`, `TargetFor(5) == 0`). Add `EveryRecipe_UsesItemsForm` (no recipe in the file has `a`/`b` set), `EveryRecipe_HasMinReactivity_BetweenOneAndTotal`, `EveryRecipe_Satisfiable_AlongTheUltimatePath` (starters-only closure under `TotalReactivity >= minReactivity` still reaches every material).
- [ ] **Step 2: Run** `dotnet test tools/CoreTests --filter FullyQualifiedName~ContentTests`. Expected: FAIL.
- [ ] **Step 3: Convert all 35 pair recipes** to `{items, minReactivity, result}`. Set each threshold to a value that keeps the recipe satisfiable but makes the weakest ingredient pairs matter (about 60 to 80% of the pair's RI total; record the rule in the ledger). Set `depthTargets` to `[300, 1500, 7500, 37500]`.
- [ ] **Step 4: Run** `dotnet test tools/CoreTests --filter FullyQualifiedName~ContentTests`. Expected: PASS.
- [ ] **Step 5: Ledger line** "Task 4: complete".

---

### Task 5: Bot on the new engine

**Files:**
- Modify: `Core/AutoPlayer.cs`, `Tests/EditMode/RunStateTests.cs` (remove the two `[Ignore]`s)

**Interfaces:**
- Consumes: `ChainPlanner.Plan`, `IgniteResolver` scoring, `ContentDb.RecipesContaining`, `RunState.HasFired(RecipeData)`.
- Produces: `AutoPlayer(ContentDb)`, `Decide(RunState) : AutoMove`, `Step(RunState)` as before.

- [ ] **Step 1: The two Phase 1 tests are the failing tests** (`ScriptedRun_ReachesWon`, `InformedPlayer_WinsMostSeeds` with a floor of 24 wins in 40 seeds). Add `Bot_PlacesFiveItemCombo_WhenHandAllowsIt`: a hand holding exactly the five items of a 5-item recipe, bot decision puts the highest-RI item on tile 4.
- [ ] **Step 2: Run** `dotnet test tools/CoreTests --filter FullyQualifiedName~RunStateTests`. Expected: FAIL (does not compile or does not win).
- [ ] **Step 3: Rewrite `Evaluate`** to call `ChainPlanner.Plan` on the candidate layout and score its reactions with the resolver's potency formula (extract the formula into `IgniteResolver.ScoreStep(...)` if it is not already callable). Keep the existing goal scoring (needed intermediates, rack pressure, trials versus target). Add recipe-driven openings: for each recipe whose items are all in the hand, try each hub tile with enough neighbours and each assignment of the other items to its neighbours; feed the best few into the existing greedy growth. Keep the full test suite under about two minutes.
- [ ] **Step 4: Run** `dotnet test tools/CoreTests`. Expected: all pass. If the informed bot wins fewer than 24 of 40, that is a balance result for Task 9, not a reason to weaken the test: raise it in the ledger and continue.
- [ ] **Step 5: Ledger line** "Task 5: complete" with the win count.

---

### Task 6: Game layer: chain playback, links, note, cutscene

**Files:**
- Modify: `Game/Boot/GameFlow.cs` (`Resolve`, `Fizzle`, `Discover`, `React`, `Between`), `Game/Bench/BenchView.cs` (links, tooltip), `Game/Bench/TileView.cs`, `Game/Bench/HudView.cs` (`Note`), `Game/Cutscene/ReactionCutscene.cs`

**Interfaces:**
- Consumes: `ReactionEvent`/`IgniteOutcome` from Task 3.
- Produces: `TileView.Show(MaterialData, bool pop)` / `Clear()` used during playback (existing); `HudView.Note(ReactionEvent, ContentDb)` accepts 2 to 5 items.

- [ ] **Step 1: Playback.** For each reaction event in order: flash every tile in `Tiles`, `Consume()` the neighbours, then `Show(result, pop: true)` on the hub tile (the result stays there as the next round's input), then score and float the points as now. Speed ramps with the event index as in Phase 1. A sputter event: a short grey flash on its tiles and the note line "Something burned hotter and spoiled a mixture."; no points. Fizzles after the chain as now. Items that survive as results (`Made` tiles) fly to the rack from their tile at the end; anything else on the bench is consumed. First-ever discoveries still play the full cutscene.
- [ ] **Step 2: `ReactionCutscene.Setup/Draw`** gather up to five materials (arrays sized 5, unused ones hidden); the header reads the names joined with " + "; the multiplier text uses `items - 1`.
- [ ] **Step 3: `HudView.Note`** lists the items (two lines for 4 or 5 names, within the existing note area; re-check the Task 11 layout of Phase 1). **BenchView links:** draw a link from the hub-to-be to its neighbours for each known recipe currently possible (use `ChainPlanner.Plan(...).Steps` on the placed layout, known recipes only via `journal.IsKnown`). Tooltip "Known reactions" counts known recipes containing the material.
- [ ] **Step 4: Verify** with `unity test Athanor --mode EditMode` (editor closed), then play-mode screenshots through the recipe in `athanor-environment-quirks` (capture and simulated mouse): a pair, a 3-item combo, a chain with the result sitting in the hub tile, a sputter. Expected: empty console, screenshots show the result in the hub tile and the neighbours gone.
- [ ] **Step 5: Ledger line** "Task 6: complete" with the screenshot paths.

---

### Task 7: Journal: Reaction Index, combos, hints per entry

**Files:**
- Modify: `Game/Ui/JournalView.cs` (`Open`, `Select`, `StillOpen`), `Game/Bench/HudView.cs` (counts), `Game/Ui/RunOverView.cs`, `Game/Boot/GameFlow.cs` (title count)

**Interfaces:** Consumes `Journal.OpenHints`, `Journal.IsComplete`, `ContentDb.RecipesContaining`, `ContentDb.Reactivity`.

- [ ] **Step 1:** Replace `StillOpen` with `journal.OpenHints(material, db)`. The collected-hints list on the page shows hints of the selected material only; the bottom-of-book list stays for the latest few hints (open ones only).
- [ ] **Step 2:** "Made from" and "Reactions found" lines iterate recipes: `Ember Coal + Iron Ore + Bone Ash = Sulfur`; the "k of n" counts known recipes containing the material. A discovered material's page shows its Reaction Index as a row of pips beside the tier line (undiscovered pages show none). Use rects/sprites already in `Make`; no new art is required.
- [ ] **Step 3: Verify** in play mode: open the Journal after a fizzle (hint visible on the right entry), then after completing that material's recipes (hint gone). Expected: screenshots; `Save` round trip test from Task 1 still passes.
- [ ] **Step 4: Ledger line** "Task 7: complete".

---

### Task 8: Phase 2 content: materials, combo recipes, icons

**Files:**
- Modify: `Resources/content.json`, `tools/art/icons.mjs`, `Tests/EditMode/ContentTests.cs`

- [ ] **Step 1: Failing tests (real content).** `DiscoveriesBetween40And46` (replaces 28 to 34); `ComboRecipeCounts`: three-item between 6 and 8, four-item between 4 and 6, five-item between 2 and 4 (spec: 7 / 5 / 3, about); `EveryRecipeResultTier_IsMaxInputTierPlus1` (unchanged, now over all items); `EveryMaterialReachableFromStarters` (closure over items, honouring thresholds); `UltimateReachable_ViaAtLeastOneCombo`; `EveryNewMaterialHasIcon` (art export check: run in Step 4); `Reactivity_StillSpread` after the additions; `EveryStarterHasAtLeast3Partners` stays.
- [ ] **Step 2: Run** the ContentTests filter. Expected: FAIL.
- [ ] **Step 3: Author the content** within those bounds: about 12 new materials (all 7 kinds still used, names in the style of the existing ones) with RIs chosen to keep the spread rule and to make hub placement a puzzle (some strong 3- and 4-item recipes whose highest-RI item belongs on an edge or the centre), and about 14 new recipes with `minReactivity` set so that a combo's items must be the lively ones. The two ultimate recipes may stay; at least one route to `worldseed` runs through a combo. Add an icon function for each new id in `ICONS` of `tools/art/icons.mjs` reusing the existing generators (`potionRound`, `crystals`, `ingot`, ...) with new palettes.
- [ ] **Step 4:** `node tools/art/export.mjs` (fails if any icon is missing), then `dotnet test tools/CoreTests`. Expected: art export prints the sprite count with no "no icon for" line; tests pass. In Unity run menu **Athanor > Build Scene** to rebuild the art library.
- [ ] **Step 5: Ledger line** "Task 8: complete" with the new table (id, kind, tier, RI) and recipe list.

---

### Task 9: Balance pass

**Files:**
- Modify: `Resources/content.json` (RIs, thresholds), `Core/ContentDb.cs` (`Base`, if needed), `Tests/EditMode/RunStateTests.cs` (`BalanceReport`, thresholds in the win test)

- [ ] **Step 1: Extend `BalanceReport`** (still `[Explicit]`) to print for 200 seeds: wins, average spare trials, depth reached by losers, and per run the count of 2-, 3-, 4-, 5-item reactions and how often the ultimate was made through a combo. Add an uninformed bot (random placement of the hand, same engine) and print its win rate.
- [ ] **Step 2: Run** `dotnet test tools/CoreTests --filter FullyQualifiedName~BalanceReport -l "console;verbosity=detailed"`. Record the numbers.
- [ ] **Step 3: Tune** until: the informed bot wins 60 to 85% of seeds; the uninformed bot rarely wins (a few percent at most); each depth is reachable at its depth and the first trial rarely clears depth 1; 5-item combos are uncommon even for the informed bot. Allowed knobs: RIs, `minReactivity`, base potency table, combo multiplier, rack size, trial counts, catalyst effects. Not allowed: the 5x target ratio, the Global Constraints rules. Change one knob at a time and log each run in the ledger.
- [ ] **Step 4: Fix the regression gate:** raise `InformedPlayer_WinsMostSeeds` to the tuned floor (e.g. 28 of 40) so later changes cannot silently break balance.
- [ ] **Step 5: Run** `dotnet test tools/CoreTests`, then `unity test Athanor --mode EditMode`. Expected: all pass.
- [ ] **Step 6: Ledger line** "Task 9: complete" with the final numbers.

---

### Task 10: Web build, docs, devlog

**Files:**
- Modify: `README.md` (Play, Changing the game), `CREDITS.md` if assets changed, `docs/devlogs/phase-2-reactivity-and-chains.md`, `docs/devlogs/README.md`

- [ ] **Step 1: Build and check** exactly as Phase 1 Task 11: `unity build Athanor --target WebGL --execute-method WebBuild.Build --allow-dirty-build`, serve `Athanor/Build/web`, run `node tools/web/webtest.mjs <url> <outDir>`. Expected: build Succeeded, 0 console errors, a trial plays, journal kept across reload, damaged save starts empty.
- [ ] **Step 2: Docs.** README: the new rules in "Play" (Reaction Index, combos, chains, consumption); "Changing the game" mentions `items`/`minReactivity`/`reactivity`. Devlog: replace "Next" with what shipped, the balance numbers and every ruling made while building; set the Phase 2 row of the devlog index to "Shipped".
- [ ] **Step 3: Ledger final line** with test counts, build size, console result. Self-review against the spec's Success list.

---

## Self-review

- **Spec coverage:** RI + hub (T1, T2), combos 2 to 5 and corner/edge/centre limits (T2), required total RI (T1, T2, T4), chain and result in hub tile (T2, T6), consumption and rack intake (T3), hints per entry and hiding when complete (T1, T7), depth targets 300/1500/7500/37500 (T4), scoring (T3), sputter (T2, T6), RI visible in Journal (T7), new content (T8), balance pass (T9), save compatibility (T1), web build (T10), devlog (T10). Spec call 8's optional "wants company of N" hint wording is dropped (the spec says "may").
- **Type consistency:** `ChainStep`/`ChainPlan` (T2) are consumed by `Resolve` (T3), `AutoPlayer` (T5) and `BenchView` (T6); `ReactionEvent.Tiles/Items/Hub` (T3) are used by GameFlow, HudView and ReactionCutscene (T6); `RunState.HasFired(RecipeData)` (T3) by the bot (T5); `Journal.OpenHints/IsComplete` (T1) by the Journal UI (T7).
- **Known risks:** the full suite is red between Task 2 and Task 3 only for the Phase 1 pair tests rewritten in Task 3, and the two autoplay tests are ignored from Task 3 until Task 5; Task 5 may expose balance problems that Task 9 fixes; bot search cost with five-item openings must be watched.
- **Proportion:** each task names signatures, test names with the spec's values, and commands; bodies are left to the implementer except the planner's match rule and round loop, which the tests do not determine.
