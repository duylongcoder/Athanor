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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Fx/ScreenFx.cs:7-53`

> Whole-screen effects: a white flash, camera shake, and fading to and from black.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Bind(Camera camera, CameraSway worldSway = null)` | 18 | camera is the camera the shake moves, unless a depth rig is given: then the shake moves the world only (owner gate G2 #6), except in a cutscene, which is flat and shakes with its camera. |
| method | `public void Flash(float peak, float seconds)` | 25 |  |
| method | `public void Shake(float pixels, float seconds)` | 26 |  |
| method | `public IEnumerator FadeOut(float seconds)` | 28 |  |
| method | `public IEnumerator FadeIn(float seconds)` | 29 |  |
| method | `public void SetBlack(float a)` | 30 |  |

## Uses
[[CameraSway]], [[DepthRig]], [[Motion]]

## Used by
[[FxRefs]], [[GameFlow]], [[GoldenShots]]

## Tested by
[[SceneBakeTests]]
