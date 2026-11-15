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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/JournalView.cs:12-255`

> The journal: every material as an icon (a silhouette until it has been made), and for the selected one the reactions found so far, how many are still unknown, and any hints collected.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action Closed` | 19 |  |
| property | `public bool IsOpen` | 20 |  |
| method | `public void Build(ContentDb content)` | 42 |  |
| method | `public void Open(Journal book, Func<string, bool> openStarters = null)` | 118 | openStarters: which starting materials the player may use; null means all of them. |
| method | `public void Close()` | 169 |  |
| method | `public Rect ShowMaterial(string id)` | 180 | Opens the page of a material (its cell picked) and returns the rectangle of its cell in the grid. |
| property | `public static Rect IndexRect` | 187 | The page's line "heat/ reactivity" and its pips. |
| property | `public static Rect RecipesRect` | 189 | The recipe lines of the page. |
| property | `public static Rect HintsRect` | 191 | The latest hints under the grid. |

## Uses
[[Align]], [[ContentDb]], [[FontKind]], [[HudView]], [[Journal]], [[Kind]], [[Label]], [[Loc]], [[Make]], [[Px]], [[Rarities]], [[RarityStars]], [[Wording]]

## Used by
[[GameFlow]], [[TutorialController]]
