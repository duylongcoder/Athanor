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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/JournalView.cs:12-265`

> The journal: every material as an icon (a silhouette until it has been made), and for the selected one the reactions found so far, how many are still unknown, and any hints collected.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action Closed` | 33 |  |
| property | `public bool IsOpen` | 34 |  |
| field | `public const int PerPage = Cols * Rows` | 37 | Materials on one page of the grid. |
| property | `public Label Title` | 60 | The selected material's name on the right-hand page. |
| property | `public Clickable CloseClick` | 62 | The "Close" click at the top right of the book. |
| property | `public Clickable PageClick` | 64 | The page label above the grid: a click turns to the next page of materials. |
| method | `public void Bind(ContentDb content)` | 70 | Takes the content and makes one cell per material from the JournalCell part (cells are not baked: a new material needs its cell). Hooks the clicks. GameFlow calls it once at start; a test calls it after instantiating the prefab. |
| method | `public void Open(Journal book, Func<string, bool> openStarters = null)` | 128 | openStarters: which starting materials the player may use; null means all of them. |
| method | `public void Close()` | 179 |  |
| method | `public Rect ShowMaterial(string id)` | 190 | Opens the page of a material (its cell picked) and returns the rectangle of its cell in the grid. |
| property | `public Rect IndexRect` | 197 | The page's line "heat/ reactivity" and its pips. |
| property | `public Rect RecipesRect` | 199 | The recipe lines of the page. |
| property | `public Rect HintsRect` | 201 | The latest hints under the grid. |

## Uses
[[Art]], [[Clickable]], [[ContentDb]], [[HudView]], [[Journal]], [[JournalCell]], [[Kind]], [[Label]], [[Loc]], [[Palette]], [[Px]], [[Rarities]], [[RarityStars]], [[Wording]]

## Used by
[[GameFlow]], [[GoldenShots]], [[TutorialController]]

## Tested by
[[LayoutPinTests]], [[PrefabBakeTests]], [[SceneBakeTests]]
