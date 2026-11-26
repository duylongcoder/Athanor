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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/GoalsView.cs:12-131`

> The Goals page: every long-term goal with its progress, grouped by the rarity of what it unlocks. Before a goal is done its reward is named only by kind and rarity; afterwards the item it gave is shown.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action Closed` | 14 |  |
| property | `public bool IsOpen` | 15 |  |
| property | `public struct Layout` | 19 | The progress bar's height and its drop below the row's top, stored in Goals.prefab. |
| property | `public struct Grid` | 26 | The page's grid (art px, y down), stored in Goals.prefab; the numbers match GOALS in tools/art/ui.mjs. dotSize is the side of the "dot" sprite the bar is stretched from. |
| property | `public Clickable CloseClick` | 46 | The "Close" click at the top right of the page. |
| method | `public void Bind(ContentDb content)` | 52 | Takes the content and makes one row per goal from the GoalRow part (rows are not baked: a new goal needs its row). Hooks the clicks. GameManager calls it once at start; a test calls it after instantiating the prefab. |
| method | `public void Open(Profile profile, Journal journal)` | 76 |  |
| method | `public void Close()` | 119 |  |

## Uses
[[Art]], [[Clickable]], [[ContentDb]], [[GoalRow]], [[Journal]], [[Label]], [[Loc]], [[Palette]], [[Profile]], [[Px]], [[Rarities]], [[Wording]]

## Used by
[[GameManager]], [[GoldenShots]]

## Tested by
[[LayoutPinTests]], [[PrefabBakeTests]]
