---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Bench/TileView.cs
tags: [code/game, kind/struct]
aliases: []
---

# TileView.Layout

`public struct TileView.Layout`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/TileView.cs:33-49` · Nested in [[TileView]]

> Colours and offsets of the tile, stored in Bench.prefab.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Color shutLid` | 36 | The lid over a shut tile. |
| field | `public Color hoverFace` | 38 | The face's glow while pointed at (alpha is the pulse). |
| field | `public Vector3 consumeLift` | 40 | The puff of Consume starts this far above the centre; the icon stands this far above it. |
| field | `public float iconLift` | 41 |  |
| field | `public float popRise` | 43 | A material popping in starts this far below its place. |
| field | `public BurstSpec consumeBurst` | 45 | The puff of Consume: count, speed, life, order, gravity (its colour is the tile's glow times consumeTint, so the spec's own colour is unused). |
| field | `public float consumeTint` | 46 |  |
| field | `public float consumeSeconds, consumeGrow` | 48 | The icon fades out and grows to consumeGrow times its size over consumeSeconds. |

## Uses
[[BurstSpec]]
