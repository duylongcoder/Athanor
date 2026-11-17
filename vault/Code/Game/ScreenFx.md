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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Fx/ScreenFx.cs:7-58`

> Whole-screen effects: a white flash, camera shake, and fading to and from black.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public static ScreenFx I` | 9 |  |
| method | `public void Build(Camera camera, CameraSway worldSway = null)` | 19 | camera is the camera the shake moves, unless a depth rig is given: then the shake moves the world only (owner gate G2 #6), except in a cutscene, which is flat and shakes with its camera. |
| method | `public void Flash(float peak, float seconds)` | 31 |  |
| method | `public void Shake(float pixels, float seconds)` | 32 |  |
| method | `public IEnumerator FadeOut(float seconds)` | 34 |  |
| method | `public IEnumerator FadeIn(float seconds)` | 35 |  |
| method | `public void SetBlack(float a)` | 36 |  |

## Uses
[[CameraSway]], [[DepthRig]], [[Make]], [[Motion]], [[Px]]

## Used by
[[CatalystChoiceView]], [[EventView]], [[GameFlow]], [[ReactionCutscene]], [[SimpleCutscene]]
