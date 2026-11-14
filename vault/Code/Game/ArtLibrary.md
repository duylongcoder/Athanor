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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/ArtLibrary.cs:8-40`

> Every sprite, effect sheet, font and material the game looks up by name. Built by Athanor > Build Scene.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Sprite[] sprites` | 10 |  |
| field | `public FlipbookSheet[] sheets` | 11 |  |
| field | `public TMP_FontAsset title, body, label, tiny` | 12 |  |
| field | `public Material lit, unlit, glow` | 13 |  |
| method | `public Sprite Sprite(string name)` | 18 |  |
| method | `public FlipbookSheet Sheet(string name)` | 31 | An exported effect sheet, or null when that effect has not been exported. |

## Uses
[[FlipbookSheet]]

## Used by
[[GameFlow]], [[Make]], [[SceneBuilder]]

## Tested by
[[SparksTests]]
