---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/ScoreParts.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# ScoreParts

`public sealed class ScoreParts`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/ScoreParts.cs:7-29`

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
