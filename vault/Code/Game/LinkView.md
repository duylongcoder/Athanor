---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Bench/LinkView.cs
tags: [code/game, kind/class]
aliases: []
---

# LinkView

`public sealed class LinkView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/LinkView.cs:7-44`

> Motes of light travelling between neighbouring tiles whose reaction is already in the journal.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Color color` | 11 | The colour of the motes (past 1 for glow), how fast they travel and how high they arc. |
| field | `public float speed = 0.35f, arc = 7, alpha = 0.9f` | 12 |  |
| field | `public Vector2 moteScale` | 14 | How big each mote is, and how far above the line between two tiles' centres the motes travel. |
| field | `public float lift` | 15 |  |
| method | `public void Set(List<(Vector3 a, Vector3 b)> pairs)` | 19 |  |

## Uses
[[Art]], [[Px]], [[Spawn]]

## Used by
[[BenchView]]

## Tested by
[[HotPathTests]], [[LayoutPinTests]]
