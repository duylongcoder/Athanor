---
type: data
tags: [data, schema]
---
# Data Design

All shapes are plain `[Serializable]` classes in `ContentData.cs` ([[ContentData]], [[ContentDb]]), read by `JsonUtility` in Unity and `System.Text.Json` in the dotnet tests (so fields are public, no properties).

## content.json  (`Athanor/Assets/Resources/content.json`)
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
| `depthTargets[]`, `depthNames[]`, `ultimate`, `noEventWeight` | ints / strings | 4 targets for 5 depths; `ultimate` = id of the last substance | [[Depths]] |

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
| Recipe / material / balance | `content.json` | icon in `tools/art/icons.mjs`; `dotnet test tools/CoreTests`; read BalanceMatrix; rebuild vault |
| New catalyst/aspect effect | name list in `ContentData.cs` + handler in [[RunState]] / [[BenchRules]] / [[ChainPlanner]] | tests first; icon; vault |
| New event op | `Ops` + the apply code in `RunStart.cs` | `EventReport` |
| Player-visible text | `lang/en.json` and `lang/vi.json` together | `LangTests` |
