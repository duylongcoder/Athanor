---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/JournalCell.cs
tags: [code/game, kind/class]
aliases: []
---

# JournalCell

`public sealed class JournalCell` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/JournalCell.cs:10-15`

> One cell of the journal's grid: the material's icon with its click. An Uncommon or better starting material also gets a star at the cell's corner; that row is made when the cell is set up (5 of the 81 cells), not baked into every cell.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public SpriteRenderer icon` | 12 |  |
| field | `public Clickable click` | 13 |  |

## Uses
[[Clickable]], [[RarityStars]]

## Used by
[[JournalView]], [[TutorialController]], [[TutorialStepsMigration]], [[ViewRef]]

## Tested by
[[PrefabBakeTests]]
