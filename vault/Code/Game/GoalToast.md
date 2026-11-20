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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/GoalToast.cs:11-85`

> A small panel at the top of the screen that says a goal was completed and what it unlocked. Toasts queue; each stays 2.5 seconds or until clicked. It never blocks anything under it but itself.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const float Stay = 2.5f` | 15 | Seconds a toast stays fully shown. |
| field | `public const float Slide = 0.25f` | 17 | Seconds a toast takes to slide in, and again to slide out. |
| property | `public struct Layout` | 21 | The toast's burst when it appears and how far past the top edge it hides, stored in GoalToast.prefab. |
| property | `struct Item` | 24 |  |
| property | `public Label Title` | 34 | The goal line of the toast showing (or last shown). |
| property | `public Label Line` | 36 | The reward line of the toast showing (or last shown). |
| method | `public void Bind(FxRefs effects)` | 39 | Hooks the click. GameFlow calls it once at start; a test calls it after instantiating the prefab. |
| method | `public void Add(IEnumerable<GoalReward> rewards, ContentDb db)` | 52 | Queues one toast for each goal just completed. |
| method | `public void Clear()` | 64 | Drops whatever is showing or waiting (the profile was reset). |

## Uses
[[Anim]], [[Art]], [[BurstSpec]], [[Clickable]], [[ContentDb]], [[FxRefs]], [[GoalReward]], [[Label]], [[Loc]], [[Px]], [[Sparks]], [[Wording]]

## Used by
[[GameFlow]], [[GoldenShots]]

## Tested by
[[LayoutPinTests]], [[PrefabBakeTests]]
