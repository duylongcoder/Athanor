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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/RunOverView.cs:9-103`

> The summary at the end of a run, won or lost.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public bool Again` | 13 | True when the player chose to start another run rather than return to the title. |
| field | `public event System.Action GoalsClicked` | 15 | The line about goals was clicked: show the Goals page over this one. |
| method | `public void Build()` | 22 |  |
| method | `public void Pick(bool again)` | 48 | Closes the summary with the given choice (also used by scripted play). |
| property | `public Label DepthName` | 51 | The depth row's name label (the first row). |
| property | `public Label DepthValue` | 53 | The depth row's value label (right-aligned, empty on a won run). |
| method | `public void ShowDepth(bool won, int depth)` | 59 | Sets the depth row: "Depth reached n of 5" on a lost run, one line "Planted at depth n" on a won run (owner, phase-5-prep 2026-11-16: a win can come before depth 5). The view is reused between runs, so both paths set the name's key again. |
| method | `public IEnumerator Show(bool won, RunState run, Journal journal, ContentDb db, System.Collections.Generic.IReadOnlyList<GoalReward> goals)` | 67 |  |

## Uses
[[Align]], [[ContentDb]], [[FontKind]], [[GoalReward]], [[Journal]], [[Label]], [[Loc]], [[Make]], [[Motion]], [[Px]], [[RunState]], [[UiButton]]

## Used by
[[GameFlow]]

## Tested by
[[RunOverDepthTests]]
