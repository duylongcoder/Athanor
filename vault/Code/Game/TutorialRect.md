---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs
tags: [code/game, kind/class]
aliases: []
---

# TutorialRect

`public static class TutorialRect`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs:6-22`

> Pure rectangle math for tutorial areas (art pixels, top-left origin). No scene needed, so it is tested alone.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static Rect Grow(Rect r, Vector4 m)` | 11 | Grows r by margin = (left, top, right, bottom) pixels. |
| method | `public static Rect ArtRect(Vector2 worldCentre, Vector2 size)` | 15 | The art-pixel rect (top-left, size) of an area whose world centre is given and whose size is in pixels. |

## Uses
[[Px]]

## Used by
[[TutorialArea]], [[TutorialController]]

## Tested by
[[TutorialAreaTests]]
