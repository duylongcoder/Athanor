---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/AtlasBuilder.cs
tags: [code/editor, kind/class]
aliases: []
---

# AtlasBuilder

`public static class AtlasBuilder`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/AtlasBuilder.cs:27-346`

> Two art steps that run after tools/art/export.mjs, both idempotent: 1. Sprite sheets: the numbered frames of each animation (Background/flame_small_0..7.png ...) are merged into one Multiple-mode sheet beside them (Background/flame_small.png). Sprite names stay flame_small_0..7, so the art library and Flame keep finding them by name. Scene and asset references to the old frames are moved to the sheet's sprites, and the frame files are deleted only after the sheet is checked pixel for pixel. 2. Sprite atlases: one Assets/Art/Atlases/NAME.spriteatlasv2 per group in Groups, uncompressed, point filtered, no mips, 2 px padding, no rotation, no tight packing, included in the build. Menu: Athanor > Build Sprite Sheets and Atlases. Command line: -executeMethod AtlasBuilder.BuildBatch

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string ArtDir = "Assets/Art", AtlasDir = "Assets/Art/Atlases", LibraryPath =` | 29 |  |
| field | `public const int Padding = 2, MaxSize = 2048, SheetGap = 2` | 30 |  |
| property | `public static readonly string[] Animations =` | 34 | Numbered frame sets that are animations (merged into one sheet each). tile_face_N and tile_ring_N are numbered too, but they are one picture per tile and row, not frames, so they stay separate sprites. |
| method | `public static bool IsSpriteSheet(string assetPath)` | 37 | True for a merged animation sheet: SpriteImportRules leaves it in Multiple mode. |
| property | `public static readonly Dictionary<string, string> Standalone = new Dictionary<string, string>` | 40 | Textures that stay out of every atlas. |
| method | `public static readonly (string name, string rule, Func<string, string, bool> match)[] Groups =` | 49 | Atlas groups by what the game uses the art for. Each texture under Assets/Art goes to the first group whose rule matches (folder relative to Assets/Art, texture name). |
| method | `public static string BuildAll()` | 66 |  |
| method | `public static void BuildBatch()` | 78 | From the command line (Unity closed): -executeMethod AtlasBuilder.BuildBatch. Exits 1 on any error. |
| method | `public static string PackAndReport()` | 86 | Packs every atlas for the active build target and reports each atlas's page sizes and fill. |
| method | `public static void PackBatch()` | 107 |  |
| method | `public static string BuildSpriteSheets()` | 124 | Merges each animation's frame PNGs into one sheet. Nothing to do when no loose frames are left. |
| method | `public static Dictionary<string, List<string>> Assign()` | 283 | The atlas group of each texture under Assets/Art (null when standalone). Throws on art no rule takes. |
| method | `public static string BuildAtlases()` | 301 |  |

## Uses
[[Save]], [[SceneBuilder]]

## Used by
[[SpriteImportRules]]
