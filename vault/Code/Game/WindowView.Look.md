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

# WindowView.Look

`public struct WindowView.Look`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/WindowView.cs:43-52` · Nested in [[WindowView]]

> Colours and strengths of the effects.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Color mote, glow, point` | 46 | Mote colour (alpha is the most it shows), glow colour (alpha is the least; it swells by glowSwing), point colour (alpha is its steady strength). |
| field | `public float glowSwing` | 47 |  |
| field | `public float dipBase, dipSwing` | 49 | The point's dip: its least alpha and the swing added to it. |
| field | `public Color rootBackground` | 51 | What the roots view's roots are painted over before they creep back (its background). |
