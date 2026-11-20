---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/WindowView.cs
tags: [code/game, kind/struct]
aliases: []
---

# WindowView.Timing

`public struct WindowView.Timing`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/WindowView.cs:18-28` · Nested in [[WindowView]]

> The periods of the effects, in seconds; Main.unity holds the values (see LayoutPinTests).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float dustPeriod, dripPeriod, dripFall` | 21 | One mote rises in dustPeriod; a drip's whole cycle is dripPeriod and it falls for the first dripFall of it (a fraction). |
| field | `public float breathePeriod` | 23 | The glow swells and fades once in breathePeriod. |
| field | `public float flickerPeriod, flickerDip, flickerRate` | 25 | The point dips every flickerPeriod, for the first flickerDip of it, at flickerRate. |
| field | `public float rootStep` | 27 | The roots give back one more row every rootStep. |
