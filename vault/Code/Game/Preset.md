---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/DepthMood.cs
tags: [code/game, kind/struct]
aliases: []
---

# Preset

`public struct Preset`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/DepthMood.cs:17-33` · Nested in [[DepthMood]]

> One depth's look.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public string label` | 20 | For the inspector only; the player-facing depth names are Lang keys. |
| field | `public Color ambient` | 21 |  |
| field | `public float moon` | 22 |  |
| field | `public Color accent` | 23 |  |
| field | `public float bloomThreshold, bloomIntensity` | 25 | URP Bloom.threshold and Bloom.intensity. |
| field | `public float vignette` | 27 | URP Vignette.intensity. |
| field | `public float backFog, frontFog, fogMix` | 29 | Alpha of the back and the front fog sheet, and how far the fog's white is tinted toward the ambient colour. |
| property | `public Color FogTint` | 32 | The fog sheets' colour: lerp(white, ambient, fogMix). |

## Used by
[[DepthStage]]
