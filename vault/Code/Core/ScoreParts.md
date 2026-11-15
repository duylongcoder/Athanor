---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/ScoreParts.cs
tags: [code/core, kind/class]
aliases: []
---

# ScoreParts

`public sealed class ScoreParts`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/ScoreParts.cs:7-29`

> One reaction's potency split into the factors the tutorial names: Total = Base x Used x First x Step x Bonus. Bonus is everything else (catalysts, the hub's tile, events); 1 when nothing else applied.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public long Base, Total` | 9 |  |
| field | `public int Used, First, Step` | 10 |  |
| field | `public double Bonus` | 11 |  |
| method | `public static ScoreParts Of(ContentDb db, ReactionEvent e)` | 14 | Null for a fizzle or a sputter. |

## Uses
[[ContentDb]], [[ReactionEvent]]

## Used by
[[TutorialController]]

## Tested by
[[TutorialTests]]
