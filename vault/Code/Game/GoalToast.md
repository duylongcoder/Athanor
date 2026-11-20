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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/GoalToast.cs:11-87`

> A small panel at the top of the screen that says a goal was completed and what it unlocked. Toasts queue; each stays 2.5 seconds or until clicked. It never blocks anything under it but itself.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public struct Layout` | 15 | The toast's burst when it appears, how far past the top edge it hides, and its resting box (art px, y down), stored in GoalToast.prefab. |
| property | `public struct Timing` | 19 | Seconds a toast stays fully shown, and takes to slide in and again to slide out. |
| property | `public float SlideOutAt` | 23 | The age (seconds) at which a toast starts to slide out. |
| property | `struct Item` | 25 |  |
| property | `public Label Title` | 35 | The goal line of the toast showing (or last shown). |
| property | `public Label Line` | 37 | The reward line of the toast showing (or last shown). |
| method | `public void Bind(FxRefs effects)` | 40 | Hooks the click. GameFlow calls it once at start; a test calls it after instantiating the prefab. |
| method | `public void Add(IEnumerable<GoalReward> rewards, ContentDb db)` | 53 | Queues one toast for each goal just completed. |
| method | `public void Clear()` | 65 | Drops whatever is showing or waiting (the profile was reset). |

## Uses
[[Anim]], [[Art]], [[BurstSpec]], [[Clickable]], [[ContentDb]], [[FxRefs]], [[GoalReward]], [[Label]], [[Loc]], [[Px]], [[Sparks]], [[Wording]]

## Used by
[[GameFlow]], [[GoldenShots]]

## Tested by
[[LayoutPinTests]], [[PrefabBakeTests]]
