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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/UiButton.cs:7-39`

> A 120x26 red button with a label: brightens under the pointer, sinks when pressed.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int W = 120, H = 26` | 9 |  |
| method | `public static UiButton Create(Transform parent, string text, float x, float y, int order, Action onClick)` | 15 | x, y: art-space top-left corner. |

## Uses
[[Align]], [[FontKind]], [[Label]], [[Make]], [[Px]]

## Used by
[[EventView]], [[RunOverView]], [[TitleView]]
