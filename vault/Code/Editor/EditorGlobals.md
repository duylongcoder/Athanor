---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/Shared/EditorGlobals.cs
tags: [code/editor, kind/class]
aliases: []
---

# EditorGlobals

`public static class EditorGlobals`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/Shared/EditorGlobals.cs:10-20`

> Sets the globals GameFlow and Sparks would set from their serialized fields at runtime (Art.Lib, Loc.Use, SparkMesh.BaseMaterial), loading the assets through the AssetDatabase. The batch tools (-executeMethod runs in a fresh domain with no scene) and the Game tests call it first. Lives in the small Athanor.EditorShared assembly so both can reference it.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static void Inject()` | 13 | Loads the art library, the language files and the particle material into the Game layer's statics. |

## Uses
[[Art]], [[ArtLibrary]], [[Loc]], [[SparkMesh]]

## Used by
[[GoldenShots]]

## Tested by
[[LocTests]]
