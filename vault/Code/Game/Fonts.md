---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Fonts.cs
tags: [code/game, kind/class]
aliases: []
---

# Fonts

`public static class Fonts`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Fonts.cs:10-33`

> Which font a FontKind uses in the language showing, and the size and line box that go with it.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static TMP_FontAsset FontFor(FontKind kind)` | 13 | The font of a kind in the language showing. Vietnamese uses SVN-Coder's Crux for all four kinds, never for the logo. |
| method | `public static float SizeFor(FontKind kind, int size)` | 21 | The point size for a kind: Tiny 8 is Crux 16 in Vietnamese (the marks need the height); the rest keep their size. |
| method | `public static int HalfLeading(UnityEngine.TextCore.FaceInfo face, float size)` | 28 | The half-leading of a font at a point size, in whole pixels (rounded up): how far below the top of a line box as tall as the size (the mockup's CSS, line-height = size) the font's ascent line sits. TMP puts the ascent line at the box top instead. Crux (ascent 7, descent -2 at 16): 11 px at 48, 7 at 32. |

## Uses
[[Art]], [[FontKind]], [[Loc]]

## Used by
[[Label]], [[Spawn]]

## Tested by
[[LabelLineBoxTests]]
