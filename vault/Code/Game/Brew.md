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

# Brew

`public struct Brew`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs:49-59` · Nested in [[ReactionCutscene]]

> The rune circle, the brew and the materials: colours, brightness and how they pulse.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Color runeStart, brewStart, brewGain` | 52 | Colours before the burst: the rune's; the brew's at the start and what it gains by the end of the gathering (rgb). |
| field | `public float runeBright, runePulseBase, runePulseAmplitude, runePulseSpeed, runeLight` | 54 | Rune: brightness, pulse = base + amplitude * sin(speed * t), light strength. |
| field | `public float frameRate, brewBright, alphaBase, alphaAfter, alphaGather, glowBase, glowAfter, glowGain, lightGain, lightBase, lightAfter` | 56 | Brew: frames a second, brightness, alpha (base; added after the burst; added with the gathering), glow alpha (before; after; gained), light (gained; base; after). |
| field | `public float matLight, matGlow` | 58 | Each flying material: light strength and glow alpha. |
