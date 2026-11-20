---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/ContentData.cs
tags: [code/core, kind/class]
aliases: []
---

# ContentData

`public class ContentData`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/ContentData.cs:56-71`

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public RulesData rules` | 58 |  |
| field | `public MaterialData[] materials` | 59 |  |
| field | `public RecipeData[] recipes` | 60 |  |
| field | `public CatalystData[] catalysts` | 61 |  |
| field | `public int[] depthTargets` | 62 |  |
| field | `public string[] depthNames` | 63 |  |
| field | `public string ultimate` | 64 |  |
| field | `public GoalData[] goals` | 65 |  |
| field | `public AspectData[] aspects` | 66 |  |
| field | `public EventData[] events` | 67 |  |
| field | `public int noEventWeight` | 68 |  |
| field | `public int[] rarityWeights` | 70 | Optional draw weights for catalyst offers and event gifts, in the order Common, Uncommon, Rare, Legendary. Missing: all 1 (a uniform draw). |

## Uses
[[RulesData]]

## Used by
[[ContentDb]], [[GameFlow]], [[GoldenShots]], [[Lang]]

## Tested by
[[ContentTests]], [[HotPathTests]], [[HudTests]], [[LoreTests]], [[PauseTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RarityTests]], [[RulesTests]], [[RunOverDepthTests]], [[RunStateTests]], [[SaveTests]], [[SceneBakeTests]], [[TutorialFlowTests]], [[TutorialStepsBakeTests]], [[TutorialTests]]
