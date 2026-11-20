---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/FogSheet.cs
tags: [code/game, kind/class]
aliases: []
---

# FogSheet

`public sealed class FogSheet` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/FogSheet.cs:10-51`

> A tiling fog sheet (a tiled SpriteRenderer with the "Athanor/Fog Lit" material) that drifts sideways in whole texels: the picture moves left by trunc(t * speed) texels (back sheet +4, front sheet -6 art px per second).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float speed` | 15 | Drift in art px (texels) per second; positive moves the picture left. |
| field | `public int width` | 17 | Width of the sheet's picture in texels (it repeats after this). |
| method | `public static int Offset(float t, float speed, int width)` | 27 | The drift at time t, in whole texels, 0 .. width - 1. Truncated toward zero so both directions step the same way (the shown drift never runs ahead of t * speed). |
| method | `public void Set(float t)` | 30 | Shows the sheet as it is at time t. |
| method | `public void Tick(float time)` | 47 | One frame at time : the sheet moves in whole texels, so most frames change nothing and write nothing. |

## Tested by
[[DepthClickTests]], [[HotPathTests]], [[LayoutPinTests]]
