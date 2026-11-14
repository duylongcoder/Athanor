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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/RunOverView.cs:9-86`

> The summary at the end of a run, won or lost.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public bool Again` | 13 | True when the player chose to start another run rather than return to the title. |
| field | `public event System.Action GoalsClicked` | 15 | The line about goals was clicked: show the Goals page over this one. |
| method | `public void Build()` | 22 |  |
| method | `public void Pick(bool again)` | 48 | Closes the summary with the given choice (also used by scripted play). |
| method | `public IEnumerator Show(bool won, RunState run, Journal journal, ContentDb db, System.Collections.Generic.IReadOnlyList<GoalReward> goals)` | 50 |  |

## Uses
[[Align]], [[ContentDb]], [[FontKind]], [[GoalReward]], [[Journal]], [[Label]], [[Make]], [[Motion]], [[Px]], [[RunState]], [[UiButton]]

## Used by
[[GameFlow]]
