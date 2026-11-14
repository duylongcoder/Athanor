---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/GoalsView.cs
tags: [code/game, kind/class]
aliases: []
---

# GoalsView

`public sealed class GoalsView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/GoalsView.cs:12-136`

> The Goals page: every long-term goal with its progress, grouped by the rarity of what it unlocks. Before a goal is done its reward is named only by kind and rarity; afterwards the item it gave is shown.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action Closed` | 17 |  |
| property | `public bool IsOpen` | 18 |  |
| method | `public void Build(ContentDb content)` | 34 |  |
| method | `public void Open(Profile profile, Journal journal)` | 81 |  |
| method | `public void Close()` | 124 |  |

## Uses
[[Align]], [[ContentDb]], [[FontKind]], [[Journal]], [[Label]], [[Make]], [[Profile]], [[Px]], [[Rarities]], [[Wording]]

## Used by
[[GameFlow]]
