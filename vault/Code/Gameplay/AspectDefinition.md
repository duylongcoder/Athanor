---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Content/AspectDefinition.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# AspectDefinition

`public sealed class AspectDefinition` : `ScriptableObject`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Content/AspectDefinition.cs:7-24`

> A run-long bench modifier offered at GameRules.aspectDepths.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public string displayName =` | 10 |  |
| field | `public AspectEffect effect` | 12 |  |
| field | `public AspectScope scope` | 13 |  |
| field | `public AspectGroup group` | 14 |  |
| method | `public AspectData ToData()` | 19 | The plain data the rules read. |

## Uses
[[AspectEffect]], [[AspectGroup]], [[AspectScope]], [[Tooltip]]

## Used by
[[ContentDatabase]]
