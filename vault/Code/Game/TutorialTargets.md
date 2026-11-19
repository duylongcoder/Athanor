---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/TutorialSettings.cs
tags: [code/game, kind/class]
aliases: []
---

# TutorialTargets

`public sealed class TutorialTargets`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/TutorialSettings.cs:12-35`

> Where the tutorial points. Art space: 640x360, x to the right, y down from the top-left corner. Every rectangle a step lights comes from here (or from the view it talks about: the Journal's pages, the Redraw button).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Rect hand = new Rect(126, 284, 392, 80)` | 16 |  |
| field | `public Rect ignite = new Rect(515, 304, 122, 42)` | 18 |  |
| field | `public Rect journal = new Rect(516, 284, 122, 24)` | 20 |  |
| field | `public Rect rack = new Rect(126, 252, 392, 32)` | 22 |  |
| field | `public Rect offer = new Rect(0, 60, 640, 240)` | 24 |  |
| field | `public Vector2 tileSize = new Vector2(72, 44)` | 28 |  |
| field | `public Vector2 tileShift = new Vector2(36, 24)` | 30 |  |
| field | `public float redrawMargin = 2` | 34 |  |

## Used by
[[TutorialController]]
