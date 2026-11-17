---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/DepthTestBuilder.cs
tags: [code/editor, kind/class]
aliases: []
---

# DepthTestBuilder

`public static class DepthTestBuilder`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/DepthTestBuilder.cs:19-344`

> Builds Assets/Scenes/DepthTest.unity, the HD-2D prototype (task 5b-05, docs/devlogs/phase-4-depth.md "Prototype plan"): the workshop bench of the main scene (built by SceneBuilder.BuildWorkshop), the nine tiles and the depth art placed in depth with DepthLayer and DepthTable, a perspective world camera into a 640K x 360K texture (DepthRig), an orthographic UI camera with a static copy of the HUD sprites, the tilt-shift, the fog sheets and the five depth presets. The main scene, its library and the build's scene list are not touched. Hand edits to the test scene are overwritten. Command line: unity run Athanor -- -executeMethod DepthTestBuilder.BuildBatch -quit

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string ScenePath = "Assets/Scenes/DepthTest.unity", Dir =` | 21 |  |
| field | `public const float WallZ = -400f, WindowZ = -398f, WallCandleZ = -392f, BackFogZ = -300f, DustZ = -300f, FrontFogZ = 40f, ForegroundZ = 180f` | 27 | SPEC depths (docs/mockup/hd2d/SPEC.md, "Layer stack and projection"). |
| method | `public static string Build()` | 35 |  |
| method | `public static void BuildBatch()` | 204 | The same, from the command line. |

## Uses
[[Align]], [[ArtLibrary]], [[CameraSway]], [[DepthLayer]], [[DepthMath]], [[DepthMood]], [[DepthRig]], [[DepthSwitch]], [[DepthTable]], [[DepthTestControls]], [[DepthTiltShiftFeature]], [[Flame]], [[FlameSwap]], [[FogSheet]], [[FontKind]], [[LayoutData]], [[Make]], [[Px]], [[SceneBuilder]], [[SpriteSwap]], [[TileView]], [[TiltShift]]

## Used by
[[DepthTestEvidence]], [[WebBuild]]
