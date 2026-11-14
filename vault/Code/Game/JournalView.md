---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/JournalView.cs
tags: [code/game, kind/class]
aliases: []
---

# JournalView

`public sealed class JournalView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/JournalView.cs:12-210`

> The journal: every material as an icon (a silhouette until it has been made), and for the selected one the reactions found so far, how many are still unknown, and any hints collected.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action Closed` | 19 |  |
| property | `public bool IsOpen` | 20 |  |
| method | `public void Build(ContentDb content)` | 39 |  |
| method | `public void Open(Journal book, Func<string, bool> openStarters = null)` | 96 | openStarters: which starting materials the player may use; null means all of them. |
| method | `public void Close()` | 146 |  |

## Uses
[[Align]], [[ContentDb]], [[FontKind]], [[HudView]], [[Journal]], [[Label]], [[Make]], [[Px]]

## Used by
[[GameFlow]]
