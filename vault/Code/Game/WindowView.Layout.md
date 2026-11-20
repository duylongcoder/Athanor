---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/WindowView.cs
tags: [code/game, kind/struct]
aliases: []
---

# WindowView.Layout

`public struct WindowView.Layout`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/WindowView.cs:32-39` · Nested in [[WindowView]]

> Where the effects sit, as offsets from the pane's centre (art pixels, y up), and the paths they follow.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Vector2 moteX, moteY, dripX` | 35 | Motes: x range, y range (bottom to top). Drips: x range, y at the top and how far one falls. |
| field | `public float dripTop, dripDrop` | 36 |  |
| field | `public Vector2 glowAt, pointAt` | 38 | The amber glow and the point of light. |
