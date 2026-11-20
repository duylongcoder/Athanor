---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Fx/BurstSpec.cs
tags: [code/game, kind/struct]
aliases: []
---

# BurstSpec

`public struct BurstSpec`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Fx/BurstSpec.cs:8-23`

> One serialized spark burst: what Sparks.Burst takes besides the position.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int count` | 10 |  |
| field | `public Color color` | 11 |  |
| field | `public float speed, life` | 12 |  |
| field | `public int order` | 13 |  |
| field | `public float gravity` | 14 |  |
| ctor | `public BurstSpec(int count, Color color, float speed, float life, int order, float gravity = 0)` | 16 |  |
| method | `public void Play(Sparks sparks, Vector2 pos)` | 22 | Throws the burst at pos. |

## Uses
[[Sparks]]

## Used by
[[CatalystChoiceView]], [[GoalToast]], [[IgniteSequence]], [[IgniteSequence.Look]]
