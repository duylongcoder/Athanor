---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/DevMode.cs
tags: [code/game, kind/class]
aliases: []
---

# DevMode

`public static class DevMode`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/DevMode.cs:13-22`

> Dev mode (editor only, menu Athanor/Dev Mode): while playing, a number changed in the Inspector of a view or in Palette.asset shows up at once. Sizes, colours and timings that are read every frame already do; the ones set when the bench is drawn (socket positions, the bar, the hand) are applied by redrawing the bench. Compiled out of every build.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public static bool Enabled` | 17 |  |

## Used by
[[DevLiveEdit]], [[DevModeMenu]]
