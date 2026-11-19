---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/DepthTestControls.cs
tags: [code/game, kind/class]
aliases: []
---

# DepthTestControls

`public sealed class DepthTestControls` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/DepthTestControls.cs:11-41`

> Play-mode controls of the depth test scene only: the pointer hovers the nine tiles through the world camera (DepthRig.BenchPoint), 1..5 pick a depth preset (0 today's look), E toggles the effects, S the sway, K shakes. Not part of the game: the test scene has no PointerInput.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public DepthRig rig` | 13 |  |
| field | `public CameraSway sway` | 14 |  |
| field | `public DepthSwitch effects` | 15 |  |
| field | `public DepthMood mood` | 16 |  |
| field | `public TileView[] tiles = new TileView[0]` | 17 |  |
| property | `public int Hovered` | 19 | The tile under the pointer, or -1. |

## Uses
[[CameraSway]], [[DepthMood]], [[DepthRig]], [[DepthSwitch]], [[TileView]]
