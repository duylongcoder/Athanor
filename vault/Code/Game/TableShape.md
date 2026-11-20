---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/DepthMath.cs
tags: [code/game, kind/struct]
aliases: []
---

# TableShape

`public readonly struct TableShape`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/DepthMath.cs:133-141`

> The table's shape as the maths needs it: the distance from the camera of its front and back edge at rest, the art y of its back edge and the number of art rows of the tilted top. DepthTable holds the numbers (Main.unity).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public readonly float front, back, topY, rows` | 135 |  |
| ctor | `public TableShape(float front, float back, float topY, float rows)` | 137 |  |

## Used by
[[DepthMath]], [[DepthTable]]
