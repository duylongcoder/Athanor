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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs:91-101` · Nested in [[GameFlow]]

> Waits and shake of the descent and the end of a run, in seconds.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float descendWait, endingWait` | 94 | Wait before the descent cutscene (the potency counter finishes) and before the ending. |
| field | `public float lossShake, lossShakeSeconds, lossWait` | 96 | A lost run: shake strength, shake length, wait. |
| field | `public float titleFadeIn, titleFadeOut, runFadeIn, runFadeOut` | 98 | The title fades in and out; a run fades in and out (black to the first depth, the summary to black). |
| field | `public float autoChoiceWait, autoPace` | 100 | Autoplay: how long it waits on a catalyst choice, and the pause between its moves. |
