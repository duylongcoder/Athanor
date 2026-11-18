---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/ArtLibrary.cs
tags: [code/game, kind/class]
aliases: []
---

# ArtLibrary

`public sealed class ArtLibrary` : `ScriptableObject`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/ArtLibrary.cs:8-42`

> Every sprite, effect sheet, font and material the game looks up by name. Built by Athanor > Build Scene.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Sprite[] sprites` | 10 |  |
| field | `public FlipbookSheet[] sheets` | 11 |  |
| field | `public TMP_FontAsset title, body, label, tiny` | 12 |  |
| field | `public TMP_FontAsset crux` | 14 | SVN-Coder's Crux, baked at 16: all four text kinds in Vietnamese. |
| field | `public Material lit, unlit, glow` | 15 |  |
| method | `public Sprite Sprite(string name)` | 20 |  |
| method | `public FlipbookSheet Sheet(string name)` | 33 | An exported effect sheet, or null when that effect has not been exported. |

## Uses
[[FlipbookSheet]]

## Used by
[[Art]], [[DepthTestBuilder]], [[EditorGlobals]], [[GameFlow]], [[SceneBuilder]]

## Tested by
[[DepthLayerTests]]
