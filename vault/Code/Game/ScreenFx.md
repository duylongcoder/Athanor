---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Fx/ScreenFx.cs
tags: [code/game, kind/class]
aliases: []
---

# ScreenFx

`public sealed class ScreenFx` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Fx/ScreenFx.cs:7-49`

> Whole-screen effects: a white flash, camera shake, and fading to and from black.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public static ScreenFx I` | 9 |  |
| method | `public void Build(Camera camera)` | 15 |  |
| method | `public void Flash(float peak, float seconds)` | 25 |  |
| method | `public void Shake(float pixels, float seconds)` | 26 |  |
| method | `public IEnumerator FadeOut(float seconds)` | 28 |  |
| method | `public IEnumerator FadeIn(float seconds)` | 29 |  |
| method | `public void SetBlack(float a)` | 30 |  |

## Uses
[[Make]], [[Motion]], [[Px]]

## Used by
[[CatalystChoiceView]], [[EventView]], [[GameFlow]], [[ReactionCutscene]], [[SimpleCutscene]]
