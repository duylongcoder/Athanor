---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Content/GameRules.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# GameRules

`public sealed class GameRules` : `ScriptableObject`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Content/GameRules.cs:7-41`

> The run's tuning numbers (was the "rules" block of the content assets) and the depth table.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int[] rarityWeights = new int[0]` | 32 |  |
| method | `public RulesData ToData()` | 35 | The plain rules block, with its own copies of the arrays. |

## Uses
[[MaterialDefinition]], [[RulesData]], [[Tooltip]]

## Used by
[[ContentDatabase]]

## Tested by
[[ContentDatabaseTests]]
