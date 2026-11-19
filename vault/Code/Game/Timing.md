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

# Timing

`public struct Timing`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs:19-48` · Nested in [[IgniteSequence]]

> Paces, waits and durations, in seconds unless the name says otherwise.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float paceHurry, paceFloor, paceDecay` | 22 | Pace of a link: fixed when hurried or auto, else max(paceFloor, 1 - n * paceDecay). |
| field | `public float endPaceHurry, endPace` | 24 | Pace of the flights to the rack, hurried or auto, and not. |
| field | `public float discoverHurryBase, discoverHurryStep` | 26 | The discovery cutscene's hurry factor: base + step * min(discoveries so far, cap). |
| field | `public int discoverHurryCap` | 27 |  |
| field | `public float discoverBeat` | 29 | The held breath before the discovery cutscene. |
| field | `public float fizzleHintWait, fizzleWait, fizzleHintText` | 31 | Fizzle: wait with and without a hint, and how long the hint text floats. |
| field | `public float sputterWait, sputterText` | 33 | Sputter: wait and text duration. |
| field | `public float drawSeconds, drawWait, reactWait, pointsSeconds, reactShake, reactFlash` | 35 | Reaction: draw-in streak, wait after it, wait at the end, the points mote, the flash and shake lengths. |
| field | `public float pointsText, pointsTextPace, comboText` | 37 | Floating "+n" text: base and the part that scales with pace; combo and chain text. |
| field | `public float discoverShake` | 39 | The discovery's shake length. |
| field | `public float rackPop, rackFly, rackHold, rackStagger` | 41 | Rack flight: pop, fly, hold between them (each times the end pace), stagger between flights. |
| field | `public float rackWaitKept, rackWaitNone` | 43 | The wait after the flights: with something kept (times end pace), and without. |
| field | `public float lostText` | 45 | How long the "lost" text of a dropped item floats. |
| field | `public float rackTrailChance` | 47 | Chance per frame of a trail spark on a flying icon. |
