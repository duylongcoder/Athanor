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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/CameraSway.cs:12-60`

> The idle sway and the world-only shake of the HD-2D bench. The sway moves the world camera (x = A sin(2 pi t / 6), y = A / 2 sin(2 pi t / 9), A = 4 art px) and re-aims it so the middle tile row (art y 188) stays still. The shake shifts the whole world picture by whole art px. Neither touches the HUD camera.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public bool swayOn = true` | 15 | The sway's on/off switch (the Settings toggle; the shake does not depend on it). |
| field | `public float amplitude` | 17 | A: camera travel in art px at z = 0. |
| field | `public SwayTiming timing` | 26 |  |
| field | `public Vector2 external` | 30 | The game's screen shake (ScreenFx), in whole art px with world y up, added to the shake of Shake. |
| method | `public void Shake(int size, float seconds)` | 35 | Shakes the world (never the HUD) by up to whole art px for a while. |
| method | `public static Vector2Int ShakeOffset(float t, int size, float rateX, float rateY)` | 43 | The shake offset at time t for a shake of the given size: whole art px, the mockup's x = round(a sin(rateX t)), y = round(2a/3 cos(rateY t)). |
| method | `public void Pose(float t)` | 47 | Places the world camera for time t. |

## Uses
[[DepthMath]], [[DepthRig]]

## Used by
[[DepthStage]], [[ScreenFx]]

## Tested by
[[LayoutPinTests]], [[MainDepthTests]]
