---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs
tags: [code/game, kind/class]
aliases: []
---

# Loop

`sealed class Loop`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs:309-356` · Nested in [[SimpleCutscene]]

> A stream of glowing particles played as a loop instead of emitted: rate x life fixed slots, each reborn every life seconds at a hashed place and speed, so its position is a function of the clock. A slot shows for a life when it is reborn while the stream is on (with probability strength), so turning a stream on fills it from its source and turning it off lets the particles in flight finish, as an emitter would. One SparkMesh.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| ctor | `public Loop(Transform parent, string name, int order, float rate, float life, float x0, float x1, float y0, float y1, float vx0, float vx1, float vy0,` | 316 |  |
| method | `public void Draw(float t, float strength, Color color)` | 327 |  |

## Uses
[[Art]], [[Px]], [[SparkMesh]]
