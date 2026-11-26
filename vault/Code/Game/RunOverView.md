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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/RunOverView.cs:10-108`

> The summary at the end of a run, won or lost.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public bool Again` | 13 | True when the player chose to start another run rather than return to the title. |
| field | `public event System.Action GoalsClicked` | 15 | The line about goals was clicked: show the Goals page over this one. |
| property | `public struct Timing` | 19 | How the dialog comes in: fade and slide duration, the shade's alpha, where the body slides from. |
| method | `public void Bind()` | 32 | Hooks the clicks. GameManager calls it once at start; a test calls it after instantiating the prefab. |
| method | `public void Pick(bool again)` | 55 | Closes the summary with the given choice (also used by scripted play). |
| property | `public Label DepthName` | 58 | The depth row's name label (the first row). |
| property | `public Label DepthValue` | 60 | The depth row's value label (right-aligned, empty on a won run). |
| method | `public void ShowDepth(bool won, int depth, int finalDepth)` | 66 | Sets the depth row: "Depth reached n of 5" on a lost run, one line "Planted at depth n" on a won run (owner, phase-5-prep 2026-11-16: a win can come before depth 5). The view is reused between runs, so both paths set the name's key again. |
| method | `public IEnumerator Show(bool won, RunState run, Journal journal, ContentDb db, System.Collections.Generic.IReadOnlyList<GoalReward> goals)` | 74 |  |

## Uses
[[Clickable]], [[ContentDb]], [[GoalReward]], [[Journal]], [[Label]], [[Loc]], [[Motion]], [[Px]], [[RunState]], [[UiButton]]

## Used by
[[GameManager]], [[GoldenShots]]

## Tested by
[[LayoutPinTests]], [[PrefabBakeTests]], [[RunOverDepthTests]]
