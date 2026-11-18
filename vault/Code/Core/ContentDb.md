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
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/ContentDb.cs:10-237`

> Validated, indexed view of the game's content.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int MinItems = 2, MaxItems = 5, MaxCopies = 2, MinReactivity = 1, MaxReactivity = 10` | 19 |  |
| ctor | `public ContentDb(ContentData data)` | 23 |  |
| method | `public List<string> LockedIn(string rewardKind, string rarity)` | 147 | The locked items of one reward bucket, by id. |
| property | `public IReadOnlyList<MaterialData> Starters` | 155 |  |
| property | `public IReadOnlyList<MaterialData> Materials` | 156 |  |
| property | `public IReadOnlyList<RecipeData> Recipes` | 157 |  |
| property | `public IReadOnlyList<CatalystData> Catalysts` | 158 |  |
| property | `public IReadOnlyList<GoalData> Goals` | 159 |  |
| property | `public IReadOnlyList<AspectData> Aspects` | 160 |  |
| property | `public IReadOnlyList<EventData> Events` | 161 |  |
| property | `public int NoEventWeight` | 162 |  |
| method | `public int RarityWeight(string rarity)` | 165 | The draw weight of a rarity (rarityWeights, or 1 when the field is missing). |
| property | `public bool UniformRarity` | 168 | True when the weights are all equal (or missing): draws then keep the plain uniform shuffle. |
| method | `public List<CatalystData> DrawCatalysts(List<CatalystData> pool, int n, Rng rng)` | 171 | Removes and returns up to n catalysts from pool by rarity weight, without replacement. Uniform weights shuffle exactly as before. |
| method | `public CatalystData PickCatalyst(IList<CatalystData> pool, Rng rng)` | 180 | One catalyst from pool by rarity weight (uniform when the weights are missing or all zero in the pool). |
| method | `public CatalystData Catalyst(string id)` | 189 |  |
| property | `public string Ultimate` | 190 |  |
| method | `public MaterialData Material(string id)` | 192 |  |
| method | `public bool Has(string id)` | 193 |  |
| method | `public Kind KindOf(string id)` | 194 |  |
| method | `public IEnumerable<string> PartnersOf(string id)` | 195 |  |
| method | `public int Reactivity(string id)` | 197 |  |
| method | `public int TotalReactivity(RecipeData r)` | 198 |  |
| method | `public string KeyOf(RecipeData r)` | 199 |  |
| method | `public int Company(RecipeData r)` | 202 | How much Reaction Index a recipe needs from materials beside its hub that are not part of it. |
| method | `public IReadOnlyList<RecipeData> RecipesContaining(string id)` | 205 | Recipes this material is an ingredient of: largest first, then highest total reactivity. |
| method | `public bool TryCombo(IEnumerable<string> items, out RecipeData recipe)` | 208 | The recipe for exactly these materials, in any order. Does not look at minReactivity. |
| method | `public bool TryReact(string a, string b, out string result)` | 210 |  |
| method | `public int TargetFor(int depth)` | 218 | Potency needed to clear a depth; 0 where there is none (depth 5). |
| method | `public string DepthName(int depth)` | 224 |  |
| method | `public static long BasePotency(int tier)` | 231 |  |
| method | `public static string PairKey(string a, string b)` | 233 |  |
| method | `public static string ComboKey(IEnumerable<string> items)` | 236 | The journal's name for a recipe: its materials sorted and joined with '+'. |

## Uses
[[AspectEffects]], [[ContentData]], [[Effects]], [[Kind]], [[Ops]], [[Rarities]], [[Rng]], [[StatIds]]

## Used by
[[AutoPlayer]], [[BenchRules]], [[BenchView]], [[ChainPlanner]], [[GameFlow]], [[GoalToast]], [[GoalsView]], [[HudView]], [[IgniteResolver]], [[Journal]], [[JournalView]], [[Loc]], [[MainDepthEvidence]], [[Profile]], [[ReactionCutscene]], [[RunOverView]], [[RunState]], [[Save]], [[ScoreParts]], [[Unlocks]], [[ViewBaker]], [[Wording]]

## Tested by
[[ChainTests]], [[ContentTests]], [[HudTests]], [[IgniteTests]], [[JournalTests]], [[LoreTests]], [[PauseTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RarityReportTests]], [[RarityTests]], [[RoleSimTests]], [[RunStateTests]], [[SaveTests]], [[SceneBakeTests]], [[TutorialFlowTests]], [[TutorialTests]]
