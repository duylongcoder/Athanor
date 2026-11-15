---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/UiButton.cs
tags: [code/game, kind/class]
aliases: []
---

# UiButton

`public sealed class UiButton` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/UiButton.cs:7-44`

> A 120x26 red button with a label: brightens under the pointer, sinks when pressed.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int W = 120, H = 26` | 9 |  |
| property | `public Label Text` | 14 | The button's label, for text that carries an argument. |
| property | `public bool Disabled` | 17 | A greyed button: no hover, no press, and a click does nothing. |
| method | `public static UiButton Create(Transform parent, string key, float x, float y, int order, Action onClick)` | 20 | key: the Lang key of the label, which follows the language. x, y: art-space top-left corner. |

## Uses
[[Align]], [[FontKind]], [[Label]], [[Make]], [[Px]]

## Used by
[[EventView]], [[PauseMenu]], [[RunOverView]], [[TitleView]]
