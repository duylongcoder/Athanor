---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs
tags: [code/game, kind/struct]
aliases: []
---

# SimpleCutscene.Look

`public struct SimpleCutscene.Look`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs:75-89` · Nested in [[SimpleCutscene]]

> What Update draws each frame: the floating prize and its rays, the rune circle and its ring, the colours of the streams.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float prizeY, bobSpeed, bobAmplitude, glowAlpha, lightIntensity` | 78 | The prize floats about art y prizeY (bobbing by bobAmplitude at bobSpeed); its glow and light at full strength. |
| field | `public float rayStep, raySpin, shortRay` | 80 | Rays: angle step, spin speed, scale of every other ray, colour (alpha at full strength). |
| field | `public Color rayColor` | 81 |  |
| field | `public float runeBright, pulseBase, pulseAmplitude, pulseSpeed, runeGlow` | 83 | Rune: brightness (also the ring's), pulse = base + amplitude * sin(speed * t), glow alpha. |
| field | `public float ringSpeed, ringWidth, ringGrowW, ringHeight, ringGrowH, ringAlpha` | 85 | Ring: cycles per second; width and height at birth and how much each grows; alpha. |
| field | `public Color emberColor` | 87 | Streams: the embers' colour; the streaks' and motes' brightness (the depth's colour times it) and the streaks' alpha. |
| field | `public float streakBright, streakAlpha, moteBright` | 88 |  |
