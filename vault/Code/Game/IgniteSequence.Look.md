---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs
tags: [code/game, kind/struct]
aliases: []
---

# IgniteSequence.Look

`public struct IgniteSequence.Look`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs:55-85` · Nested in [[IgniteSequence]]

> Bursts, offsets, sizes and colours.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public BurstSpec fizzleBurst, sputterBurst, discoverBurst, reactBurst, pointsLand, rackLand` | 57 |  |
| field | `public Color fizzleFlash, sputterFlash` | 59 | Tile flash colours (alpha included): fizzle, sputter. |
| field | `public Color drawTint, rackTrailTint, pointsTint, pointsTrailTint` | 61 | Colours multiplied by the material's glow: the draw-in and rack trails; reactBurst.color and rackLand.color too. |
| field | `public Color defaultGlow` | 63 | The glow of a material that has none. |
| field | `public Color pointsText, comboText, chainText, hintText, spoiledText, lostText` | 65 | Texts: "+n", combo, chain, new hint, spoiled, lost. |
| field | `public Vector3 hubLift, hintTextLift, spoilTileLift, spoilTextLift, drawLift, pointsTextLift, comboTextLift, rackLift, rackUp` | 67 | Where a reaction shows relative to its hub, and the offsets of the texts and streaks from it. |
| field | `public Vector2 lostAt` | 69 | Where the "lost" text floats. |
| field | `public int burstPerChain, burstChainCap, burstPerExtra` | 71 | Burst growth: extra particles per chain link (capped at chainCap links) and per tile beyond a pair; extra speed per link. |
| field | `public float speedPerChain` | 72 |  |
| field | `public float shakeBase, shakePerChain, shakePerExtra, shakeMax, discoverShake` | 74 | Shake: base + perChain * chain + perExtra * (size - 2), capped. |
| field | `public int flashChain, flashSize` | 76 | Screen flash for a long chain: starts at this chain / size, strength per step, capped. |
| field | `public float flashPerStep, flashMax` | 77 |  |
| field | `public float drawJitter, drawLife, pointsJitter, pointsJitter2, pointsLife, pointsLife2, pointsHump, rackJitter, rackLife` | 79 | Trail sparks: jitter and life of a draw-in streak and of the points mote (two layers), sort orders. |
| field | `public int drawOrder, pointsOrder, pointsOrder2, rackTrailOrder` | 80 |  |
| field | `public float haloSize, haloAlpha, popScale, arcHeight` | 82 | The rack icon: halo size, halo alpha, pop scale, order of halo and icon, arc height. |
| field | `public int haloOrder, iconOrder` | 83 |  |

## Uses
[[BurstSpec]]
