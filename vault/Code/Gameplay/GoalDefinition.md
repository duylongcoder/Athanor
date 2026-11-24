---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Content/GoalDefinition.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# GoalDefinition

`public sealed class GoalDefinition` : `ScriptableObject`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Content/GoalDefinition.cs:7-22`

> A long-term goal: reach target on a stat, win one random locked item of rewardKind and rewardRarity.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public string id =` | 9 |  |
| field | `public GoalStat stat` | 11 |  |
| field | `public RewardKind rewardKind` | 13 |  |
| method | `public GoalData ToData()` | 17 | The plain data the rules read. |

## Uses
[[ContentNames]], [[GoalStat]], [[RarityId]], [[RewardKind]], [[Tooltip]]

## Used by
[[ContentDatabase]]
