---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/SceneBuilder.cs
tags: [code/editor, kind/class]
aliases: []
---

# SceneBuilder

`public static class SceneBuilder`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/SceneBuilder.cs:16-234`

> Builds Assets/Scenes/Main.unity and the art library from the exported sprites. Run it again after regenerating art (node tools/art/export.mjs) or exporting effect sheets. Hand edits to the scene are overwritten.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static string Build()` | 23 |  |
| method | `public static void BuildBatch()` | 117 | The same, from the command line: unity run Athanor -- -executeMethod SceneBuilder.BuildBatch -quit |
| method | `public static void AllBatch()` | 120 | Batch: fonts, effect sheets, then the scene and library. unity run Athanor -- -executeMethod SceneBuilder.AllBatch -quit |
| method | `public static ArtLibrary RefreshLibrary()` | 129 | Re-collects sprites and effect sheets into the art library (after new art or effects are imported). |
| method | `internal static Transform BuildWorkshop(LayoutData layout)` | 181 | The workshop bench (wall, window, table, candles, moonbeam, dust); DepthTestBuilder builds its copy with it. |

## Uses
[[Art]], [[ArtLibrary]], [[CameraSway]], [[DepthRig]], [[DepthStage]], [[DepthTestBuilder]], [[Dust]], [[EditorGlobals]], [[Flame]], [[FlipbookImporter]], [[FlipbookSheet]], [[FontBuilder]], [[GameFlow]], [[LayoutData]], [[Make]], [[PointerInput]], [[Px]], [[Sparks]], [[TiltShift]]

## Used by
[[AtlasBuilder]], [[DepthTestBuilder]], [[FlipbookImporter]]
