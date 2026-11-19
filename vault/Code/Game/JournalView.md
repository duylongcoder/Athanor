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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/JournalView.cs:12-252`

> The journal: every material as an icon (a silhouette until it has been made), and for the selected one the reactions found so far, how many are still unknown, and any hints collected.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action Closed` | 19 |  |
| property | `public bool IsOpen` | 20 |  |
| field | `public const int PerPage = Cols * Rows` | 23 | Materials on one page of the grid. |
| property | `public Label Title` | 47 | The selected material's name on the right-hand page. |
| property | `public Clickable CloseClick` | 49 | The "Close" click at the top right of the book. |
| property | `public Clickable PageClick` | 51 | The page label above the grid: a click turns to the next page of materials. |
| method | `public void Bind(ContentDb content)` | 57 | Takes the content and makes one cell per material from the JournalCell part (cells are not baked: a new material needs its cell). Hooks the clicks. GameFlow calls it once at start; a test calls it after instantiating the prefab. |
| method | `public void Open(Journal book, Func<string, bool> openStarters = null)` | 115 | openStarters: which starting materials the player may use; null means all of them. |
| method | `public void Close()` | 166 |  |
| method | `public Rect ShowMaterial(string id)` | 177 | Opens the page of a material (its cell picked) and returns the rectangle of its cell in the grid. |
| property | `public static Rect IndexRect` | 184 | The page's line "heat/ reactivity" and its pips. |
| property | `public static Rect RecipesRect` | 186 | The recipe lines of the page. |
| property | `public static Rect HintsRect` | 188 | The latest hints under the grid. |

## Uses
[[Art]], [[Clickable]], [[ContentDb]], [[HudView]], [[Journal]], [[JournalCell]], [[Kind]], [[Label]], [[Loc]], [[Palette]], [[Px]], [[Rarities]], [[RarityStars]], [[Wording]]

## Used by
[[GameFlow]], [[GoldenShots]], [[TutorialController]]

## Tested by
[[PrefabBakeTests]], [[SceneBakeTests]]
