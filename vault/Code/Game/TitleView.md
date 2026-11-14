---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/TitleView.cs
tags: [code/game, kind/class]
aliases: []
---

# TitleView

`public sealed class TitleView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/TitleView.cs:7-99`

> The title, shown over the candlelit workshop.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public bool BeginPressed` | 10 |  |
| field | `public event Action JournalClicked, GoalsClicked, ResetConfirmed` | 11 |  |
| property | `public bool Asking` | 13 | The "erase progress?" question is up: the buttons under it do nothing. |
| method | `public void Build()` | 22 |  |
| method | `public void Open(int known, int total, int goalsDone, int goals)` | 67 |  |
| method | `public void Progress(int known, int total, int goalsDone, int goals)` | 77 | The line under the buttons: how much of the journal and of the goals is done. |
| method | `public void Close()` | 83 |  |

## Uses
[[Align]], [[Anim]], [[FontKind]], [[Label]], [[Make]], [[Px]], [[Sparks]], [[UiButton]]

## Used by
[[GameFlow]]
