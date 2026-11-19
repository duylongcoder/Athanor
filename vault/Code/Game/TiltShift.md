---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/TiltShift.cs
tags: [code/game, kind/class]
aliases: []
---

# TiltShift

`public sealed class TiltShift` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/TiltShift.cs:12-46`

> The tilt-shift of the world camera: sharp from art y 145 to 235, then a blur that rises over 110 rows (smoothstep) to sigma 1.5 art px (owner gate G2 #9, #10). Drawn by DepthTiltShiftFeature before bloom, only for a camera that has this component enabled.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Material material` | 18 | The blur material the renderer features draw with (shader "Athanor/Tilt Shift"). |
| field | `public float bandTop = 145f, bandBottom = 235f` | 20 | The sharp band, in art rows (y down). |
| field | `public float feather = 110f` | 22 | Rows from the band edge to full strength. |
| field | `public float maxSigma = 1.5f` | 24 | Blur sigma at full strength, in art px. |
| method | `public static bool IsOn(Camera camera)` | 27 | True when the tilt-shift is on for this camera. |
| method | `public static float Sigma(float artY, float top, float bottom, float feather, float maxSigma)` | 30 | Blur sigma (art px) of an art row: maxSigma * smoothstep(0, 1, distance from the band / feather). |
| method | `public void Push()` | 42 | Writes the band to the material. |

## Used by
[[DepthTiltShiftFeature]]

## Tested by
[[DepthClickTests]]
