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

# IgniteSequence.Timing

`public struct IgniteSequence.Timing`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs:22-51` · Nested in [[IgniteSequence]]

> Paces, waits and durations, in seconds unless the name says otherwise.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float paceHurry, paceFloor, paceDecay` | 25 | Pace of a link: fixed when hurried or auto, else max(paceFloor, 1 - n * paceDecay). |
| field | `public float endPaceHurry, endPace` | 27 | Pace of the flights to the rack, hurried or auto, and not. |
| field | `public float discoverHurryBase, discoverHurryStep` | 29 | The discovery cutscene's hurry factor: base + step * min(discoveries so far, cap). |
| field | `public int discoverHurryCap` | 30 |  |
| field | `public float discoverBeat` | 32 | The held breath before the discovery cutscene. |
| field | `public float fizzleHintWait, fizzleWait, fizzleHintText` | 34 | Fizzle: wait with and without a hint, and how long the hint text floats. |
| field | `public float sputterWait, sputterText` | 36 | Sputter: wait and text duration. |
| field | `public float drawSeconds, drawWait, reactWait, pointsSeconds, reactShake, reactFlash` | 38 | Reaction: draw-in streak, wait after it, wait at the end, the points mote, the flash and shake lengths. |
| field | `public float pointsText, pointsTextPace, comboText` | 40 | Floating "+n" text: base and the part that scales with pace; combo and chain text. |
| field | `public float discoverShake` | 42 | The discovery's shake length. |
| field | `public float rackPop, rackFly, rackHold, rackStagger` | 44 | Rack flight: pop, fly, hold between them (each times the end pace), stagger between flights. |
| field | `public float rackWaitKept, rackWaitNone` | 46 | The wait after the flights: with something kept (times end pace), and without. |
| field | `public float lostText` | 48 | How long the "lost" text of a dropped item floats. |
| field | `public float rackTrailChance` | 50 | Chance per frame of a trail spark on a flying icon. |
