---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/SpriteImportRules.cs
tags: [code/editor, kind/class]
aliases: []
---

# SpriteImportRules

`public sealed class SpriteImportRules` : `AssetPostprocessor`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/SpriteImportRules.cs:10-57`

> Every texture under Assets/Art is pixel art: a point-filtered, uncompressed sprite at 1 pixel per unit. The one exception is Assets/Art/Depth (the HD-2D layers): its pre-blurred pictures (*_far, *_near) are bilinear, so the blur is not turned back into blocks when the world is drawn at 2x or 3x, and its fog sheets (fog_*) repeat.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public override uint GetVersion()` | 14 |  |
| method | `public static bool IsSoftDepthArt(string assetPath)` | 17 | True for a pre-blurred depth layer: Art/Depth/*_far*, *_near*. |
| method | `public static bool IsDepthFog(string assetPath)` | 25 | True for a tiling fog sheet: Art/Depth/fog_*. |

## Uses
[[AtlasBuilder]]
