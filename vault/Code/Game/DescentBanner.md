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

# DescentBanner

`public struct DescentBanner`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs:37-45` · Nested in [[SimpleCutscene]]

> Descent beat 1, the win over the bench: "depth cleared" rises into place, with a flash, a shake and a burst.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float flash, flashSeconds, shake, shakeSeconds` | 39 |  |
| field | `public float rise, fromY, toY, hold` | 41 | The banner fades in and rises from art y fromY to toY in this long, then holds. |
| field | `public BurstSpec burst` | 43 | The burst over the bench: where (art y), and its flattening (see Sparks.Burst). |
| field | `public float burstY, burstSquash` | 44 |  |

## Uses
[[BurstSpec]]
