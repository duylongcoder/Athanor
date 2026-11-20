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

# Prize

`public struct Prize`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs:63-74` · Nested in [[ReactionCutscene]]

> The result, its pillar, rays and rings once the burst has gone off.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float glowAlpha, lightStrength, pillarBright, pillarAlpha, pillarPulseBase, pillarPulseAmplitude, pillarPulseSpeed` | 66 | Glow alpha and light strength of the result; pillar brightness, alpha and pulse = base + amplitude * sin(speed * t). |
| field | `public float bobSpeed, bobAmplitude` | 68 | The result bobs by bobAmplitude at bobSpeed. |
| field | `public float raySpin, shortRay` | 70 | Rays: spin speed, scale of every other ray, colour (alpha at full strength). |
| field | `public Color rayColor` | 71 |  |
| field | `public float ringSpeed, ringWidth, ringGrowW, ringHeight, ringGrowH, ringBright, ringAlpha` | 73 | Rings: cycles a second, width and height at birth and how much each grows, brightness, alpha. |
