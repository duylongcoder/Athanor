---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/ArtLibraryBuilder.cs
tags: [code/editor, kind/class]
aliases: []
---

# ArtLibraryBuilder

`public static class ArtLibraryBuilder`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/ArtLibraryBuilder.cs:12-53`

> Collects the sprites, effect sheets, fonts and materials into Assets/Art/ArtLibrary.asset, the one lookup the game uses by name. Run it after new art or effect sheets are imported (the sprite and flipbook importers call it too). It touches no scene.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static void Refresh()` | 19 | Re-collects sprites and effect sheets into the art library (after new art or effects are imported). |
| method | `public static void RefreshBatch()` | 22 | From the command line (Unity closed): unity run Athanor -- -executeMethod ArtLibraryBuilder.RefreshBatch -quit |
| method | `public static ArtLibrary RefreshLibrary()` | 25 | Rebuilds the library asset and saves it. |

## Uses
[[ArtLibrary]], [[FlipbookSheet]]

## Used by
[[AtlasBuilder]], [[FlipbookImporter]]
