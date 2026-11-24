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

# EventDefinition

`public sealed class EventDefinition` : `ScriptableObject`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Content/EventDefinition.cs:47-64`

> An event drawn at the start of a depth.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public string id =` | 49 |  |
| field | `public EventKind type` | 52 |  |
| field | `public EventOptionDef[] options = new EventOptionDef[0]` | 56 |  |
| method | `public EventData ToData()` | 59 | The plain data the rules read. |

## Uses
[[EventKind]], [[EventOptionDef]], [[Tooltip]]

## Used by
[[ContentDatabase]]
