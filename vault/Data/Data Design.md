---
type: data
tags: [data, schema]
---
# Data Design

The rules read plain `[Serializable]` classes in `ContentData.cs` ([[ContentData]], [[ContentDb]]; fields are public, no properties). The source of truth is one ScriptableObject asset per entry under `Athanor/Assets/Content` ([[ContentDatabase]], [[GameRules]], `MaterialDefinition` and the other `*Definition` types); `ContentDatabase.ToContentData()` builds the shape below, and `Athanor > Content > Write Snapshot` writes it to `tools/content/snapshot.json` for the tools that run outside Unity.

## Content shape  (the snapshot, `tools/content/snapshot.json`)
| Key | Shape | Notes / validation ([[ContentDb]]) | Vault |
|---|---|---|---|
| `materials[]` | `{id,name,kind,tier,reactivity,glow,rarity?,locked?}` | `kind` ∈ [[Kind]]; `reactivity` 1–10 = Reaction Index; tier 0 = starting material; `rarity` Common/Uncommon/Rare, `locked` = won from a goal | [[Materials]] |
| `recipes[]` | `{items[2..5],minReactivity,result}` | unordered; old `{a,b}` still read; `minReactivity` = heat needed | [[Recipes]] |
| `catalysts[]` | `{id,name,text,effect,kind,amount,rarity,locked}` | `effect` ∈ [[Effects]] | [[Catalysts]] |
| `aspects[]` | `{id,name,text,effect,scope,group,amount,tradeOff}` | `effect` ∈ [[AspectEffects]]; scope Tile/Link/Bench; group Cheap/Engine | [[Aspects]] |
| `events[]` | `{id,name,text,type,minDepth,maxDepth,weight,options[]}` | type ∈ `Ops.Types` (Choice, Gamble, Find, Shrine, Hazard, Boon, Lore) | [[Events]] |
| `events[].options[]` | `{label,requires,cost[],outcomes[]}` | `requires` ∈ `Ops.Needs`; one outcome drawn by weight | |
| `…outcomes[]` | `{weight,text,gain[]}`; effect = `{op,arg,amount,amount2}` | `op` ∈ [[Ops]] | |
| `goals[]` | `{id,text,stat,target,rewardKind,rewardRarity}` | `stat` ∈ [[StatIds]]; loader fails if a reward bucket has more goals than locked items | [[Goals]] |
| `rules` | flat object, see below | required; [[ContentDb]] throws without it and validates ranges | [[RulesData]] |
| `depthTargets[]`, `depthNames[]`, `ultimate`, `noEventWeight` | ints / strings | 4 targets for 5 depths; `ultimate` = id of the last substance | [[Depths]] |

### `rules`  (the run's tuning numbers, type [[RulesData]])
| Field | Meaning | Today |
|---|---|---|
| `handMin` | smallest hand size | 3 |
| `handBase` | hand size a run starts with | 5 |
| `trials` | trials per depth | 4 |
| `trialsFinalDepth` | trials at the final depth | 6 |
| `redraws` | redraws per run | 2 |
| `starterCopies` | copies of each starter material in the bench deck | 2 |
| `offerSize` | catalyst cards offered after a depth | 3 |
| `rackSize` | bench rack slots | 12 |
| `pouchCards` | cards the pouch adds to the hand | 1 |
| `maxRi` | cap on a tile's Reaction Index | 13 |
| `firstReactionMult` | multiplier on the first reaction of a trial | 2 |
| `aspectDepths[]` | depths (1-based) at which the run offers an aspect | 1, 3, 5 |
| `potencyByTier[]` | base potency by material tier (index = tier) | 0, 20, 60, 180, 500, 1500 |

`ContentDb.ValidateRules` throws when the block is missing or out of range; the rules read it (`ContentDb.Rules`) and there is no code fallback.

Balance rules the tests enforce: a path to the ultimate exists; tiers add up; Reaction Index spread across tiers; an informed player usually wins; 5-item combos stay rare; a hazard/gamble met once leaves ≥70% of event-free wins. Reports: README "Changing the game" (`BalanceReport`, `BalanceMatrix`, `RoleReport`, `EventReport`).

## Lang files  (`Resources/lang/en.json`, `vi.json`)
`{language,name,entries:[{key,text}]}`. Lookup: chosen language → English fallback → key ([[Lang]]). Key prefixes: `ui.*` (hud, journal, bench, title, runover, settings, choice, goals, toast), `story.*` (intro, ending), `tutorial.*`, `cutscene.*`, `effect.*`, `note.*`, `kind.*`. Rule: every `en` key exists in `vi` (checked by `LangTests`).

## Save data  (PlayerPrefs / browser storage, [[Save]])
| Key | Value | Shape |
|---|---|---|
| `athanor.journal.v1` | JSON of [[Journal]] | `JournalData {known[], hints[{material,partnerKind,heat}]}` |
| `athanor.profile.v1` | JSON of [[Profile]] | `ProfileData {version,rewardSeed,runsStarted,runsWon,wonNoRedraw,depthReached,ignites,chainLongest,comboLargest,bestIgnite,completed[],unlockedStarters[],unlockedCatalysts[],rewards[],lastLore}` |
| `athanor.intro.v1`, tutorial key | int 0/1 | seen flags |

A damaged journal/profile must load as empty/fresh (`SaveTests`). Changing a shape → bump the key version and keep a reader for the old one.

## layout.json  (`Editor/layout.json`)
Canvas `w:640,h:360`; `tiles[]` with `x,y,w` and `face{x,y,w,h}`; read through [[LayoutData]]. It is read by editor tooling only (the game build ships no layout file); change it with the layout tooling, not by hand.

## Where a change goes
| Change | Edit | Then |
|---|---|---|
| Recipe / material / balance | the asset under `Assets/Content` (Inspector) | `Athanor > Content > Write Snapshot`; icon in `tools/art/icons.mjs`; `unity test`; `BalanceReports.RunBatch`; rebuild vault |
| New catalyst/aspect effect | name list in `ContentData.cs` + handler in [[RunState]] / [[BenchRules]] / [[ChainPlanner]] | tests first; icon; vault |
| New event op | `Ops` + the apply code in `RunStart.cs` | `EventReport` |
| Player-visible text | `lang/en.json` and `lang/vi.json` together | `LangTests` |
