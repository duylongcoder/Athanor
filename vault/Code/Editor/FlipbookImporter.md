---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/FlipbookImporter.cs
tags: [code/editor, kind/class]
aliases: []
---

# FlipbookImporter

`public static class FlipbookImporter`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/FlipbookImporter.cs:14-77`

> Turns each pixelfx sheet in Assets/Art/Fx/Sheets (name.png + name.json, written by tools/fx/export.sh) into sliced sprites and a FlipbookSheet asset, then adds them to the art library so Fx.Spawn(name) can play them.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string Dir =` | 16 |  |
| method | `public static string Import()` | 26 |  |

## Uses
[[FlipbookSheet]], [[SceneBuilder]]

## Used by
[[SceneBuilder]]
