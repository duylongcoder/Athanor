---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/DepthSwitch.cs
tags: [code/game, kind/class]
aliases: []
---

# DepthSwitch

`public sealed class DepthSwitch` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/DepthSwitch.cs:11-52`

> The "all effects off" switch of the depth test scene. Off: the far layers show the original sharp sprites and the fog sheets and the foreground are hidden, so the world at rest is today's bench. On: the pre-blurred far sprites, the fog and the foreground corners.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public bool effectsOn = true` | 30 | Whether the effects are showing. |
| field | `public SpriteSwap[] sprites = new SpriteSwap[0]` | 31 |  |
| field | `public FlameSwap[] flames = new FlameSwap[0]` | 32 |  |
| field | `public GameObject[] effectObjects = new GameObject[0]` | 34 | Shown only with the effects on: the fog sheets and the foreground corners. |
| field | `public Behaviour[] effectBehaviours = new Behaviour[0]` | 36 | Enabled only with the effects on: the tilt-shift. |
| method | `public void Set(bool on)` | 39 | Shows the scene with the effects on or off. |

## Uses
[[Flame]]

## Used by
[[DepthMood]], [[DepthTestBuilder]], [[DepthTestControls]], [[DepthTestEvidence]]
