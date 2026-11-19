---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Anim.cs
tags: [code/game, kind/class]
aliases: []
---

# Anim

`public static class Anim`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Anim.cs:11-32`

> Beats (Wait), easing curves, and Over for per-frame scripts that are not a move or a fade (the spark trails in GameFlow). Moves, fades and scales of objects go through Motion (PrimeTween, snapped to pixels).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static IEnumerator Over(float seconds, Action<float> step)` | 14 | Calls step with 0..1 over the given time, ending on exactly 1. |
| method | `public static IEnumerator Wait(float seconds, Func<bool> skip = null)` | 21 | Waits, ending early when skip returns true. |
| method | `public static float OutCubic(float t)` | 26 |  |
| method | `public static float InCubic(float t)` | 27 |  |
| method | `public static float InOut(float t)` | 28 |  |
| method | `public static float OutBack(float t)` | 29 |  |
| method | `public static float Hump(float t)` | 31 | 0 at both ends, 1 in the middle. |

## Used by
[[GameFlow]], [[GoalToast]], [[IgniteSequence]], [[Motion]], [[ReactionCutscene]], [[SimpleCutscene]], [[TileView]], [[TitleView]]
