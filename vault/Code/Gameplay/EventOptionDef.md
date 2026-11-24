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

# EventOptionDef

`public sealed class EventOptionDef`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Content/EventDefinition.cs:31-43`

> One choice of an event: its requirement, cost and weighted outcomes.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public string label =` | 33 |  |
| field | `public EventNeed requires` | 34 |  |
| field | `public EventEffectDef[] cost = new EventEffectDef[0]` | 35 |  |
| field | `public EventOutcomeDef[] outcomes = new EventOutcomeDef[0]` | 36 |  |
| method | `public EventOption ToData()` | 38 | The plain data the rules read. |

## Uses
[[ContentNames]], [[EventEffectDef]], [[EventNeed]], [[EventOutcomeDef]]

## Used by
[[EventDefinition]]
