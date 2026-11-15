---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Clickable.cs
tags: [code/game, kind/class]
aliases: []
---

# Clickable

`public sealed class Clickable` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Clickable.cs:7-21`

> A rectangle around this object that the pointer can hover and click. The highest order under the pointer wins.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Vector2 size, offset` | 9 |  |
| field | `public int order` | 10 |  |
| field | `public Action onClick, onEnter, onExit, onDown, onUp` | 11 |  |
| method | `public bool Contains(Vector2 world)` | 13 |  |

## Uses
[[PointerInput]]

## Used by
[[Make]], [[PointerInput]]

## Tested by
[[PauseTests]]
