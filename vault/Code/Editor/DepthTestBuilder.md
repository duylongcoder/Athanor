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
- Layer: **Editor** · Source: `Athanor/Assets/Editor/DepthTestBuilder.cs:19-360`

> Builds Assets/Scenes/DepthTest.unity, the HD-2D prototype (task 5b-05, docs/devlogs/phase-4-depth.md "Prototype plan"): the workshop bench of the main scene (built by SceneBuilder.BuildWorkshop), the nine tiles and the depth art placed in depth with DepthLayer and DepthTable, a perspective world camera into a 640K x 360K texture (DepthRig), an orthographic UI camera with a static copy of the HUD sprites, the tilt-shift, the fog sheets and the five depth presets. The main scene, its library and the build's scene list are not touched. Hand edits to the test scene are overwritten. Command line: unity run Athanor -- -executeMethod DepthTestBuilder.BuildBatch -quit

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string ScenePath = "Assets/Scenes/DepthTest.unity", Dir =` | 21 |  |
| field | `public const float WallZ = -400f, WindowZ = -398f, WallCandleZ = -392f, BackFogZ = -300f, DustZ = -300f, FrontFogZ = 40f, ForegroundZ = 180f` | 27 | SPEC depths (docs/mockup/hd2d/SPEC.md, "Layer stack and projection"). |
| method | `public static string Build()` | 35 |  |
| property | `internal struct World` | 125 | The result of ComposeWorld. |
| method | `internal static World ComposeWorld(DepthRig rig, Transform world, Transform workshop, LayoutData layout, ArtLibrary lib, Material tableMat, Material f` | 132 | Puts the workshop (already built by SceneBuilder.BuildWorkshop under world) in depth: wall with overscan, window, table mesh, candles, dust, fog sheets, foreground corners, the effects switch and the five moods. Shared by the depth test scene and the main scene. tilt may be null. |
| method | `public static void BuildBatch()` | 220 | The same, from the command line. |
| method | `internal static Sprite Art(string name)` | 222 |  |
| method | `internal static void Leaf(GameObject go, float z)` | 230 | Puts one object (whose parents sit at the origin, unscaled) at depth z. |
| method | `internal static SpriteRenderer FogSheet(Transform parent, string name, string sprite, float x, float y, float w, float h, int order, float speed, floa` | 232 |  |
| method | `internal static Material EnsureMaterial(string path, string shaderName)` | 270 |  |
| method | `internal static VolumeProfile BuildVolume()` | 281 |  |
| method | `internal static int EnsureRenderer(Material tilt)` | 307 | A copy of the main 2D renderer with the two tilt-shift passes, listed in the pipeline asset after the main one (which stays the default and keeps no feature). Returns the copy's index in the pipeline's renderer list. |

## Uses
[[Align]], [[Art]], [[ArtLibrary]], [[CameraSway]], [[DepthLayer]], [[DepthMath]], [[DepthMood]], [[DepthRig]], [[DepthSwitch]], [[DepthTable]], [[DepthTestControls]], [[DepthTiltShiftFeature]], [[EditorGlobals]], [[Flame]], [[FlameSwap]], [[FogSheet]], [[FontKind]], [[LayoutData]], [[Make]], [[Px]], [[SceneBuilder]], [[SpriteSwap]], [[TileView]], [[TiltShift]]

## Used by
[[DepthTestEvidence]], [[SceneBuilder]], [[WebBuild]]
