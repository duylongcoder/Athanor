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
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/ContentData.cs:54-69`

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public RulesData rules` | 56 |  |
| field | `public MaterialData[] materials` | 57 |  |
| field | `public RecipeData[] recipes` | 58 |  |
| field | `public CatalystData[] catalysts` | 59 |  |
| field | `public int[] depthTargets` | 60 |  |
| field | `public string[] depthNames` | 61 |  |
| field | `public string ultimate` | 62 |  |
| field | `public GoalData[] goals` | 63 |  |
| field | `public AspectData[] aspects` | 64 |  |
| field | `public EventData[] events` | 65 |  |
| field | `public int noEventWeight` | 66 |  |
| field | `public int[] rarityWeights` | 68 | Optional draw weights for catalyst offers and event gifts, in the order Common, Uncommon, Rare, Legendary. Missing: all 1 (a uniform draw). |

## Uses
[[RulesData]]

## Used by
[[ContentDb]], [[GameFlow]], [[GoldenShots]], [[Lang]]

## Tested by
[[ContentTests]], [[HudTests]], [[LoreTests]], [[PauseTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RarityTests]], [[RulesTests]], [[RunStateTests]], [[SaveTests]], [[TutorialFlowTests]], [[TutorialTests]]
