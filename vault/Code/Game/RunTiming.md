---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Boot/GameFlow.cs
tags: [code/game, kind/struct]
aliases: []
---

# RunTiming

`public struct RunTiming`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs:85-91` · Nested in [[GameFlow]]

> Waits and shake of the descent and the end of a run, in seconds.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float descendWait, endingWait` | 88 | Wait before the descent cutscene (the potency counter finishes) and before the ending. |
| field | `public float lossShake, lossShakeSeconds, lossWait` | 90 | A lost run: shake strength, shake length, wait. |
