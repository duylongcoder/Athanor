---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Content/CatalystDefinition.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# CatalystDefinition

`public sealed class CatalystDefinition` : `ScriptableObject`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Content/CatalystDefinition.cs:7-24`

> A catalyst card: a run-long scoring effect chosen after a depth.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public string displayName =` | 10 |  |
| field | `public CatalystEffect effect` | 12 |  |
| field | `public float amount` | 14 |  |
| field | `public RarityId rarity = RarityId.Common` | 15 |  |
| method | `public CatalystData ToData()` | 19 | The plain data the rules read. |

## Uses
[[CatalystEffect]], [[ContentNames]], [[Kind]], [[KindOrNone]], [[RarityId]], [[Tooltip]]

## Used by
[[ContentDatabase]]
