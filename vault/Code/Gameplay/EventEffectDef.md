---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Content/EventDefinition.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# EventEffectDef

`public sealed class EventEffectDef`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Content/EventDefinition.cs:9-16`

> One effect of an event option's cost or outcome.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public EventOp op` | 11 |  |
| field | `public int amount, amount2` | 13 |  |
| method | `public EffectData ToData()` | 15 | The plain data the rules read. |

## Uses
[[EventOp]], [[Tooltip]]

## Used by
[[EventOptionDef]], [[EventOutcomeDef]]
