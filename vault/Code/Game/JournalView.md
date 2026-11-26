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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/JournalView.cs:12-274`

> The journal: every material as an icon (a silhouette until it has been made), and for the selected one the reactions found so far, how many are still unknown, and any hints collected.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public struct Grid` | 18 | The book's grid and its right-hand page (art px, y down), stored in Journal.prefab; the numbers match JOURNAL in tools/art/ui.mjs. |
| field | `public event Action Closed` | 38 |  |
| property | `public bool IsOpen` | 39 |  |
| field | `public const int PerPage = Cols * Rows` | 42 | Materials on one page of the grid. |
| property | `public Label Title` | 65 | The selected material's name on the right-hand page. |
| property | `public Clickable CloseClick` | 67 | The "Close" click at the top right of the book. |
| property | `public Clickable PageClick` | 69 | The page label above the grid: a click turns to the next page of materials. |
| method | `public void Bind(ContentDb content)` | 75 | Takes the content and makes one cell per material from the JournalCell part (cells are not baked: a new material needs its cell). Hooks the clicks. GameManager calls it once at start; a test calls it after instantiating the prefab. |
| method | `public void Open(Journal book, Func<string, bool> openStarters = null)` | 133 | openStarters: which starting materials the player may use; null means all of them. |
| method | `public void Close()` | 184 |  |
| method | `public Rect ShowMaterial(string id)` | 195 | Opens the page of a material (its cell picked) and returns the rectangle of its cell in the grid. |
| method | `public Rect CellRect(int i)` | 202 | The cell of material number i in the grid of its page (art space). |
| method | `public Rect CellRect(string id)` | 204 | The cell of a material by id, without opening its page. |
| property | `public Rect IndexRect` | 206 | The page's line "heat/ reactivity" and its pips. |
| property | `public Rect RecipesRect` | 208 | The recipe lines of the page. |
| property | `public Rect HintsRect` | 210 | The latest hints under the grid. |

## Uses
[[Art]], [[Clickable]], [[ContentDb]], [[HudView]], [[Journal]], [[JournalCell]], [[Kind]], [[Label]], [[Loc]], [[Palette]], [[Px]], [[Rarities]], [[RarityStars]], [[Wording]]

## Used by
[[GameManager]], [[GoldenShots]], [[TutorialManager]]

## Tested by
[[LayoutPinTests]], [[PrefabBakeTests]], [[SceneBakeTests]], [[TutorialStepsBakeTests]]
