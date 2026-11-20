---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/Tooltip.cs
tags: [code/game, kind/struct]
aliases: []
---

# Tooltip.Layout

`public struct Tooltip.Layout`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/Tooltip.cs:18-28` · Nested in [[Tooltip]]

> The tooltip's inner offsets (px), stored in the Bench prefab.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float pad, titleDy, bodyDy, starsDy, tierDy` | 21 | Left padding, and the drops of the title, body, stars and tier word from the panel's top. |
| field | `public float tierGap, edgeMargin` | 23 | The gap between the stars and the tier word; the margin kept to the screen edge. |
| field | `public float border` | 25 | The 9-slice border, and the panel sprite's pivot. |
| field | `public int longText` | 27 | A body longer than this many characters sits tighter, at this line spacing. |
