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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/UiButton.cs:7-59`

> A 120x26 red button with a label: brightens under the pointer, sinks when pressed.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int W = 120, H = 26` | 9 |  |
| field | `public event Action Clicked` | 26 | Raised on a click while the button is enabled. |
| property | `public Label Text` | 28 | The button's label, for text that carries an argument. |
| property | `public bool Disabled` | 30 | A greyed button: no hover, no press, and a click does nothing. |
| method | `public void Bind()` | 35 | Hooks this button's Clickable to it. Safe to call more than once: it assigns, it does not add. |
| method | `public void Tick(float deltaTime)` | 50 | One frame of the button: the sprite, tint and label height are written only when the button's look (pressed, disabled, pointed at) changes. |

## Uses
[[Art]], [[Clickable]], [[Label]], [[Palette]]

## Used by
[[EventView]], [[PauseMenu]], [[RunOverView]], [[TitleView]]

## Tested by
[[HotPathTests]], [[LabelLineBoxTests]], [[LayoutPinTests]], [[PrefabBakeTests]]
