---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/GoalToast.cs
tags: [code/game, kind/class]
aliases: []
---

# GoalToast

`public sealed class GoalToast` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/GoalToast.cs:11-73`

> A small panel at the top of the screen that says a goal was completed and what it unlocked. Toasts queue; each stays 2.5 seconds or until clicked. It never blocks anything under it but itself.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `struct Item` | 16 |  |
| method | `public void Build()` | 24 |  |
| method | `public void Add(IEnumerable<GoalReward> rewards, ContentDb db)` | 40 | Queues one toast for each goal just completed. |
| method | `public void Clear()` | 52 | Drops whatever is showing or waiting (the profile was reset). |

## Uses
[[Align]], [[Anim]], [[ContentDb]], [[FontKind]], [[GoalReward]], [[Label]], [[Make]], [[Px]], [[Sparks]], [[Wording]]

## Used by
[[GameFlow]]
