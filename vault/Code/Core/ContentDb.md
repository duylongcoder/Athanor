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
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/ContentDb.cs:10-267`

> Validated, indexed view of the game's content.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int MinItems = 2, MaxItems = 5, MaxCopies = 2, MinReactivity = 1, MaxReactivity = 10` | 19 |  |
| ctor | `public ContentDb(ContentData data)` | 23 |  |
| property | `public RulesData Rules` | 148 | The tuning numbers from content.json. |
| property | `public int FinalDepth` | 151 | The last depth of a run: the number of named depths. |
| method | `public List<string> LockedIn(string rewardKind, string rarity)` | 177 | The locked items of one reward bucket, by id. |
| property | `public IReadOnlyList<MaterialData> Starters` | 185 |  |
| property | `public IReadOnlyList<MaterialData> Materials` | 186 |  |
| property | `public IReadOnlyList<RecipeData> Recipes` | 187 |  |
| property | `public IReadOnlyList<CatalystData> Catalysts` | 188 |  |
| property | `public IReadOnlyList<GoalData> Goals` | 189 |  |
| property | `public IReadOnlyList<AspectData> Aspects` | 190 |  |
| property | `public IReadOnlyList<EventData> Events` | 191 |  |
| property | `public int NoEventWeight` | 192 |  |
| method | `public int RarityWeight(string rarity)` | 195 | The draw weight of a rarity (rarityWeights, or 1 when the field is missing). |
| property | `public bool UniformRarity` | 198 | True when the weights are all equal (or missing): draws then keep the plain uniform shuffle. |
| method | `public List<CatalystData> DrawCatalysts(List<CatalystData> pool, int n, Rng rng)` | 201 | Removes and returns up to n catalysts from pool by rarity weight, without replacement. Uniform weights shuffle exactly as before. |
| method | `public CatalystData PickCatalyst(IList<CatalystData> pool, Rng rng)` | 210 | One catalyst from pool by rarity weight (uniform when the weights are missing or all zero in the pool). |
| method | `public CatalystData Catalyst(string id)` | 219 |  |
| property | `public string Ultimate` | 220 |  |
| method | `public MaterialData Material(string id)` | 222 |  |
| method | `public bool Has(string id)` | 223 |  |
| method | `public Kind KindOf(string id)` | 224 |  |
| method | `public IEnumerable<string> PartnersOf(string id)` | 225 |  |
| method | `public int Reactivity(string id)` | 227 |  |
| method | `public int TotalReactivity(RecipeData r)` | 228 |  |
| method | `public string KeyOf(RecipeData r)` | 229 |  |
| method | `public int Company(RecipeData r)` | 232 | How much Reaction Index a recipe needs from materials beside its hub that are not part of it. |
| method | `public IReadOnlyList<RecipeData> RecipesContaining(string id)` | 235 | Recipes this material is an ingredient of: largest first, then highest total reactivity. |
| method | `public bool TryCombo(IEnumerable<string> items, out RecipeData recipe)` | 238 | The recipe for exactly these materials, in any order. Does not look at minReactivity. |
| method | `public bool TryReact(string a, string b, out string result)` | 240 |  |
| method | `public int TargetFor(int depth)` | 248 | Potency needed to clear a depth; 0 where there is none (depth 5). |
| method | `public string DepthName(int depth)` | 254 |  |
| method | `public long BasePotency(int tier)` | 261 | Base potency of a material tier, from rules.potencyByTier; a tier past the table uses its last entry. |
| method | `public static string PairKey(string a, string b)` | 263 |  |
| method | `public static string ComboKey(IEnumerable<string> items)` | 266 | The journal's name for a recipe: its materials sorted and joined with '+'. |

## Uses
[[AspectEffects]], [[ContentData]], [[Effects]], [[Kind]], [[Ops]], [[Rarities]], [[Rng]], [[RulesData]], [[StatIds]]

## Used by
[[AutoPlayer]], [[BenchRules]], [[BenchView]], [[ChainPlanner]], [[GameFlow]], [[GoalToast]], [[GoalsView]], [[GoldenShots]], [[HudView]], [[IgniteResolver]], [[Journal]], [[JournalView]], [[Loc]], [[Profile]], [[ReactionCutscene]], [[RunOverView]], [[RunState]], [[Save]], [[ScoreParts]], [[Unlocks]], [[Wording]]

## Tested by
[[ChainTests]], [[ContentTests]], [[HudTests]], [[IgniteTests]], [[JournalTests]], [[LoreTests]], [[PauseTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RarityReportTests]], [[RarityTests]], [[RoleSimTests]], [[RulesTests]], [[RunOverDepthTests]], [[RunStateTests]], [[SaveTests]], [[SceneBakeTests]], [[TutorialFlowTests]], [[TutorialTests]]
