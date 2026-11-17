---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Bench/Dust.cs
tags: [code/game, kind/class]
aliases: []
---

# Dust

`public sealed class Dust` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/Dust.cs:9-34`

> Dust drifting down through the moonbeam: a loop, each mote's position a pure function of time, all of them drawn as one SparkMesh (one renderer) at order 8.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int count = 46` | 11 |  |
| field | `public Material material` | 13 | Not used since the motes became one mesh (it has its own material); kept so the built scene still loads it. |
| field | `public Sprite sprite` | 14 |  |

## Uses
[[Px]], [[SparkMesh]]

## Used by
[[DepthTestEvidence]], [[SceneBuilder]], [[WindowView]]
