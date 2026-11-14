---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Fx/Flame.cs
tags: [code/game, kind/class]
aliases: []
---

# Flame

`public sealed class Flame` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Fx/Flame.cs:7-22`

> A flickering flame: cycles its frames and wavers the light and halo that go with it.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Sprite[] frames` | 9 |  |
| field | `public SpriteRenderer body, halo` | 10 |  |
| field | `public Light2D glowLight` | 11 |  |
| field | `public float lightIntensity = 1, haloAlpha = 0.5f, framesPerSecond = 11, phase` | 12 |  |

## Used by
[[SceneBuilder]]
