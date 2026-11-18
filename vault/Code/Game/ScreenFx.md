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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Fx/ScreenFx.cs:7-66`

> Whole-screen effects: a white flash, camera shake, and fading to and from black.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public static ScreenFx I` | 9 |  |
| method | `public void Bind(Camera camera, CameraSway worldSway = null)` | 19 | camera is the camera the shake moves, unless a depth rig is given: then the shake moves the world only (owner gate G2 #6), except in a cutscene, which is flat and shakes with its camera. Makes this the scene's ScreenFx.I. |
| method | `public void Build()` | 29 | Bake source for the Screen Fx object of Main.unity (ViewBaker.BakeScreenFx): the white flash and the black fade, full screen. |
| method | `public void Flash(float peak, float seconds)` | 38 |  |
| method | `public void Shake(float pixels, float seconds)` | 39 |  |
| method | `public IEnumerator FadeOut(float seconds)` | 41 |  |
| method | `public IEnumerator FadeIn(float seconds)` | 42 |  |
| method | `public void SetBlack(float a)` | 43 |  |

## Uses
[[Art]], [[CameraSway]], [[DepthRig]], [[Make]], [[Motion]], [[Px]]

## Used by
[[CatalystChoiceView]], [[EventView]], [[GameFlow]], [[ReactionCutscene]], [[SimpleCutscene]], [[ViewBaker]]

## Tested by
[[SceneBakeTests]]
