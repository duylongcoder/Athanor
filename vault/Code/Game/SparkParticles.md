---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Fx/SparkParticles.cs
tags: [code/game, kind/class]
aliases: []
---

# SparkParticles

`sealed class SparkParticles`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Fx/SparkParticles.cs:13-77`

> Sparks drawn by ParticleSystems (the 3.3 design in docs/devlogs/phase-4-assets.md), only compiled with ATHANOR_SPARK_PARTICLES. Not shipped: it pulls the ParticleSystem engine module into the wasm, which was the thing measured. One system per (order, gravity, drag, sparkle). Colours are divided by K and multiplied back by the material, because particle colours are 8 bit.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const float K = 3` | 15 |  |
| property | `public int LiveCount` | 24 |  |
| method | `public void Clear()` | 26 |  |
| method | `public void Emit(Sparks owner, Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity, bool sparkle, float drag)` | 28 |  |

## Uses
[[Art]], [[Make]], [[SparkMesh]], [[Sparks]]

## Used by
[[Sparks]]
