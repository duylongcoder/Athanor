---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/DepthTiltShiftFeature.cs
tags: [code/game, kind/class]
aliases: []
---

# DepthTiltShiftFeature

`public class DepthTiltShiftFeature` : `FullScreenPassRendererFeature`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/DepthTiltShiftFeature.cs:9-17`

> A full-screen pass of the tilt-shift (one blur direction per feature) that runs only for cameras with an enabled TiltShift, so the HUD camera and the main game's camera are never blurred.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)` | 12 |  |

## Uses
[[TiltShift]]
