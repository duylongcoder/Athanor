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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/BenchView.cs:29-41` · Nested in [[BenchView]]

> Offsets and a colour of the bench, stored in Bench.prefab.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Vector3 joinLift` | 32 | Where the hand-drawn links of a join start and end, from a tile's centre. |
| field | `public Vector3 floatLiftFrom, floatLiftTo` | 34 | A floating number starts and ends this far above its point. |
| field | `public Color floatShadow` | 36 | The floating number's outline. |
| field | `public float ghostLift` | 38 | The dragged material's ghost rides this far above the pointer. |
| field | `public Vector3 tileTipLift, cardTipLift` | 40 | Where a tooltip points: above a tile's centre, above a card's top (half its height). |
