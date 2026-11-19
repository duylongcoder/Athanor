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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/GoalsView.cs:12-119`

> The Goals page: every long-term goal with its progress, grouped by the rarity of what it unlocks. Before a goal is done its reward is named only by kind and rarity; afterwards the item it gave is shown.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action Closed` | 17 |  |
| property | `public bool IsOpen` | 18 |  |
| property | `public Clickable CloseClick` | 35 | The "Close" click at the top right of the page. |
| method | `public void Bind(ContentDb content)` | 41 | Takes the content and makes one row per goal from the GoalRow part (rows are not baked: a new goal needs its row). Hooks the clicks. GameFlow calls it once at start; a test calls it after instantiating the prefab. |
| method | `public void Open(Profile profile, Journal journal)` | 64 |  |
| method | `public void Close()` | 107 |  |

## Uses
[[Art]], [[Clickable]], [[ContentDb]], [[GoalRow]], [[Journal]], [[Label]], [[Loc]], [[Palette]], [[Profile]], [[Px]], [[Rarities]], [[Wording]]

## Used by
[[GameFlow]], [[GoldenShots]]

## Tested by
[[PrefabBakeTests]]
