---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Bench/BenchView.cs
tags: [code/game, kind/struct]
aliases: []
---

# BenchView.Layout

`public struct BenchView.Layout`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/BenchView.cs:30-46` · Nested in [[BenchView]]

> Offsets and a colour of the bench, stored in Bench.prefab.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Vector3 joinLift` | 33 | Where the hand-drawn links of a join start and end, from a tile's centre. |
| field | `public Vector3 floatLiftFrom, floatLiftTo` | 35 | A floating number starts and ends this far above its point. |
| field | `public Color floatShadow` | 37 | The floating number's outline. |
| field | `public float ghostLift` | 39 | The dragged material's ghost rides this far above the pointer. |
| field | `public Vector3 tileTipLift, cardTipLift` | 41 | Where a tooltip points: above a tile's centre, above a card's top (half its height). |
| field | `public float handX, handW, cardY, handMaxPitch` | 43 | The hand's strip: left edge, width, the cards' top, and the widest gap between two cards (art px). |
| field | `public float slotHit, tileHitHalfWidth, tileHitBelow, tileHitAbove` | 45 | A rack well takes a click within slotHit of its centre; a placed material within tileHitHalfWidth sideways and from tileHitBelow under to tileHitAbove over its tile centre. |
