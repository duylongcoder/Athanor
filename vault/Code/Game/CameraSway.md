---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/CameraSway.cs
tags: [code/game, kind/class]
aliases: []
---

# CameraSway

`public sealed class CameraSway` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/CameraSway.cs:11-47`

> The idle sway and the world-only shake of the HD-2D bench. The sway moves the world camera (x = A sin(2 pi t / 6), y = A / 2 sin(2 pi t / 9), A = 4 art px) and re-aims it so the middle tile row (art y 188) stays still. The shake shifts the whole world picture by whole art px. Neither touches the HUD camera.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public bool swayOn = true` | 14 | The sway's on/off switch (the Settings toggle is built at integration, 5b-07). |
| field | `public float amplitude = DepthMath.SwayAmplitude` | 16 | A: camera travel in art px at z = 0. |
| method | `public void Shake(int size, float seconds)` | 23 | Shakes the world (never the HUD) by up to whole art px for a while. |
| method | `public static Vector2Int ShakeOffset(float t, int size)` | 31 | The shake offset at time t for a shake of the given size: whole art px, the mockup's x = round(a sin 53.1 t), y = round(2a/3 cos 41.7 t). |
| method | `public void Pose(float t)` | 35 | Places the world camera for time t. |

## Uses
[[DepthMath]], [[DepthRig]]

## Used by
[[DepthTestBuilder]], [[DepthTestControls]]
