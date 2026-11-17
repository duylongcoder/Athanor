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
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/ContentData.cs:28-42`

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public MaterialData[] materials` | 30 |  |
| field | `public RecipeData[] recipes` | 31 |  |
| field | `public CatalystData[] catalysts` | 32 |  |
| field | `public int[] depthTargets` | 33 |  |
| field | `public string[] depthNames` | 34 |  |
| field | `public string ultimate` | 35 |  |
| field | `public GoalData[] goals` | 36 |  |
| field | `public AspectData[] aspects` | 37 |  |
| field | `public EventData[] events` | 38 |  |
| field | `public int noEventWeight` | 39 |  |
| field | `public int[] rarityWeights` | 41 | Optional draw weights for catalyst offers and event gifts, in the order Common, Uncommon, Rare, Legendary. Missing: all 1 (a uniform draw). |

## Used by
[[ContentDb]], [[GameFlow]], [[Lang]], [[MainDepthEvidence]]

## Tested by
[[ContentTests]], [[HudTests]], [[LoreTests]], [[PauseTests]], [[Phase3Tests]], [[RarityTests]], [[RoleSimTests]], [[RunStateTests]], [[SaveTests]], [[TutorialFlowTests]], [[TutorialTests]]
