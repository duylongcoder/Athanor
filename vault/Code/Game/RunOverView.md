---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/RunOverView.cs
tags: [code/game, kind/class]
aliases: []
---

# RunOverView

`public sealed class RunOverView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/RunOverView.cs:9-106`

> The summary at the end of a run, won or lost.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public bool Again` | 12 | True when the player chose to start another run rather than return to the title. |
| field | `public event System.Action GoalsClicked` | 14 | The line about goals was clicked: show the Goals page over this one. |
| property | `public struct Timing` | 18 | How the dialog comes in: fade and slide duration, the shade's alpha, where the body slides from. |
| method | `public void Bind()` | 29 | Hooks the clicks. GameFlow calls it once at start; a test calls it after instantiating the prefab. |
| method | `public void Pick(bool again)` | 52 | Closes the summary with the given choice (also used by scripted play). |
| property | `public Label DepthName` | 55 | The depth row's name label (the first row). |
| property | `public Label DepthValue` | 57 | The depth row's value label (right-aligned, empty on a won run). |
| method | `public void ShowDepth(bool won, int depth, int finalDepth)` | 63 | Sets the depth row: "Depth reached n of 5" on a lost run, one line "Planted at depth n" on a won run (owner, phase-5-prep 2026-11-16: a win can come before depth 5). The view is reused between runs, so both paths set the name's key again. |
| method | `public IEnumerator Show(bool won, RunState run, Journal journal, ContentDb db, System.Collections.Generic.IReadOnlyList<GoalReward> goals)` | 71 |  |

## Uses
[[Clickable]], [[ContentDb]], [[GoalReward]], [[Journal]], [[Label]], [[Loc]], [[Motion]], [[Px]], [[RunState]], [[UiButton]]

## Used by
[[GameFlow]], [[GoldenShots]]

## Tested by
[[LayoutPinTests]], [[PrefabBakeTests]], [[RunOverDepthTests]]
