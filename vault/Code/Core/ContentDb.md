---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/ContentDb.cs
tags: [code/core, kind/class]
aliases: []
---

# ContentDb

`public sealed class ContentDb`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/ContentDb.cs:10-205`

> Validated, indexed view of the game's content.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int MinItems = 2, MaxItems = 5, MaxCopies = 2, MinReactivity = 1, MaxReactivity = 10` | 19 |  |
| ctor | `public ContentDb(ContentData data)` | 23 |  |
| method | `public List<string> LockedIn(string rewardKind, string rarity)` | 141 | The locked items of one reward bucket, by id. |
| property | `public IReadOnlyList<MaterialData> Starters` | 149 |  |
| property | `public IReadOnlyList<MaterialData> Materials` | 150 |  |
| property | `public IReadOnlyList<RecipeData> Recipes` | 151 |  |
| property | `public IReadOnlyList<CatalystData> Catalysts` | 152 |  |
| property | `public IReadOnlyList<GoalData> Goals` | 153 |  |
| property | `public IReadOnlyList<AspectData> Aspects` | 154 |  |
| property | `public IReadOnlyList<EventData> Events` | 155 |  |
| property | `public int NoEventWeight` | 156 |  |
| method | `public CatalystData Catalyst(string id)` | 157 |  |
| property | `public string Ultimate` | 158 |  |
| method | `public MaterialData Material(string id)` | 160 |  |
| method | `public bool Has(string id)` | 161 |  |
| method | `public Kind KindOf(string id)` | 162 |  |
| method | `public IEnumerable<string> PartnersOf(string id)` | 163 |  |
| method | `public int Reactivity(string id)` | 165 |  |
| method | `public int TotalReactivity(RecipeData r)` | 166 |  |
| method | `public string KeyOf(RecipeData r)` | 167 |  |
| method | `public int Company(RecipeData r)` | 170 | How much Reaction Index a recipe needs from materials beside its hub that are not part of it. |
| method | `public IReadOnlyList<RecipeData> RecipesContaining(string id)` | 173 | Recipes this material is an ingredient of: largest first, then highest total reactivity. |
| method | `public bool TryCombo(IEnumerable<string> items, out RecipeData recipe)` | 176 | The recipe for exactly these materials, in any order. Does not look at minReactivity. |
| method | `public bool TryReact(string a, string b, out string result)` | 178 |  |
| method | `public int TargetFor(int depth)` | 186 | Potency needed to clear a depth; 0 where there is none (depth 5). |
| method | `public string DepthName(int depth)` | 192 |  |
| method | `public static long BasePotency(int tier)` | 199 |  |
| method | `public static string PairKey(string a, string b)` | 201 |  |
| method | `public static string ComboKey(IEnumerable<string> items)` | 204 | The journal's name for a recipe: its materials sorted and joined with '+'. |

## Uses
[[AspectEffects]], [[ContentData]], [[Effects]], [[Kind]], [[Ops]], [[Rarities]], [[StatIds]]

## Used by
[[AutoPlayer]], [[BenchRules]], [[BenchView]], [[ChainPlanner]], [[GameFlow]], [[GoalToast]], [[GoalsView]], [[HudView]], [[IgniteResolver]], [[Journal]], [[JournalView]], [[Profile]], [[ReactionCutscene]], [[RunOverView]], [[RunState]], [[Save]], [[ScoreParts]], [[Unlocks]], [[Wording]]

## Tested by
[[ChainTests]], [[ContentTests]], [[HudTests]], [[IgniteTests]], [[JournalTests]], [[LoreTests]], [[Phase3Tests]], [[RoleSimTests]], [[RunStateTests]], [[SaveTests]], [[TutorialTests]]
