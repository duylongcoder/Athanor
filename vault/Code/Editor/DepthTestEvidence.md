---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/DepthTestEvidence.cs
tags: [code/editor, kind/class]
aliases: []
---

# DepthTestEvidence

`public static class DepthTestEvidence`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/DepthTestEvidence.cs:23-321`

> Renders the 5b-05 evidence of the depth test scene to PNG from the editor (cameras rendered into textures): the main scene's bench against the depth scene's rest frame (with pixel diffs), the five presets, the sway extremes, a shake, the tilt-shift band, and Light2D / moonbeam / bloom toggled under both cameras. Nothing is saved to any scene. Command line: unity run Athanor -- -executeMethod DepthTestEvidence.CaptureBatch -quit

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static void CaptureBatch()` | 31 | Batch entry: exits 1 on any failure. |
| method | `public static string Capture()` | 38 |  |

## Uses
[[Art]], [[DepthMood]], [[DepthRig]], [[DepthSwitch]], [[DepthTestBuilder]], [[DepthTestControls]], [[Dust]], [[EditorGlobals]], [[Flame]], [[FogSheet]], [[LayoutData]], [[Make]], [[Px]], [[Save]], [[TileView]], [[TiltShift]]
