---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs
tags: [code/game, kind/struct]
aliases: []
---

# ReactionCutscene.Layout

`public struct ReactionCutscene.Layout`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs:78-90` · Nested in [[ReactionCutscene]]

> Where things are (art space, y down) and how the motion between them is shaped.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float rimY` | 81 | The cauldron's rim: the burst, the sparks and the result start here. |
| field | `public float resultLift, resultRise` | 83 | The result floats resultLift above the rim and rises resultRise. |
| field | `public float spiralRadius, spiralPower, spiralMin, spiralSpin, spiralFlat, spiralY, spiralDrop` | 85 | The spiral of a material: radius, power of its shrinking, smallest radius, turns (radians), flattening, start y and how far it drops. |
| field | `public float bannerY, titleY, wordSlide` | 87 | The banner and the title sit here and slide by wordSlide while they fade in. |
| field | `public float plusY, plusShift` | 89 | The "+n" line at plusY; with a multiplier it moves plusShift left to make room. |
