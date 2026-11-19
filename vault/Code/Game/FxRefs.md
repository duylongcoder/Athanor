---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Fx/FxRefs.cs
tags: [code/game, kind/class]
aliases: []
---

# FxRefs

`public sealed class FxRefs`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Fx/FxRefs.cs:6-19`

> The scene's two effect players, handed to every view that flashes, shakes or throws sparks (no statics).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public readonly ScreenFx Screen` | 9 | Flash, shake and fades. |
| field | `public readonly Sparks Sparks` | 11 | Bursts, trails and embers. |
| ctor | `public FxRefs(ScreenFx screen, Sparks sparks)` | 13 |  |

## Uses
[[ScreenFx]], [[Sparks]]

## Used by
[[BenchView]], [[CatalystChoiceView]], [[EventView]], [[GameFlow]], [[GoalToast]], [[GoldenShots]], [[ReactionCutscene]], [[SimpleCutscene]], [[TileView]], [[TitleView]]
