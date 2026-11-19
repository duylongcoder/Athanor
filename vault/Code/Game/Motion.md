---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Motion.cs
tags: [code/game, kind/class]
aliases: []
---

# Motion

`public static class Motion`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Motion.cs:16-77`

> One-shot tweens on PrimeTween, kept in one place so every move is snapped to whole pixels (Px.Snap). Each call takes its start value explicitly and returns the Tween: group several with Sequence.Create().Group(..), and wait for them in a coroutine with Motion.Wait or .ToYieldInstruction() (never yield the struct itself, it gets boxed). Scaled time, as Anim.Over. The ease defaults to Linear here, not to PrimeTween's own default (OutQuad). Only for animations that run to a fixed end; spring follows, hover lifts, idle loops and particles stay hand-written.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static Tween Move(Transform t, Vector3 from, Vector3 to, float seconds, Ease ease = Ease.Linear)` | 19 | World position, snapped every frame. |
| method | `public static Tween MoveLocal(Transform t, Vector3 from, Vector3 to, float seconds, Ease ease = Ease.Linear)` | 23 | Local position, snapped every frame. |
| property | `sealed class ArcPath` | 30 | An arc is the tween's target, not its payload: PrimeTween payloads are at most 16 bytes, and a bigger one gives a dead tween. One small object per arc. |
| method | `public static Tween Arc(Transform t, Vector3 from, Vector3 to, float height, float seconds, Ease ease = Ease.Linear)` | 36 | World position along a line eased by ease, lifted by height in a hump (Anim.Hump over plain time) and snapped. With from equal to to it is a hop on the spot. |
| method | `public static Tween Scale(Transform t, float from, float to, float seconds, Ease ease = Ease.Linear)` | 42 | Uniform x and y scale (z stays 1). |
| method | `public static Tween Fade(SpriteRenderer r, float from, float to, float seconds, Ease ease = Ease.Linear)` | 46 | Alpha of a sprite; its colour is kept. |
| method | `public static Tween Fade(Label l, float from, float to, float seconds, Ease ease = Ease.Linear)` | 50 | Alpha of a label and its shadow. |
| method | `public static void StopOn(Component owner)` | 57 | Stops every tween that targets the component or anything under its object: the transforms, sprites and labels the helpers above animate. A view calls it from OnDestroy, after StopAllCoroutines, so nothing outlives it into the next Play. |
| method | `public static IEnumerator Wait(Tween tween, Func<bool> hurry = null)` | 67 | Waits for a tween. When hurry returns true it jumps to its end values at once. |
| method | `public static IEnumerator Wait(Sequence sequence, Func<bool> hurry = null)` | 73 | Waits for a sequence. When hurry returns true it jumps to its end values at once. |

## Uses
[[Anim]], [[Label]], [[Px]]

## Used by
[[BenchView]], [[CatalystChoiceView]], [[EventView]], [[GameFlow]], [[RunOverView]], [[ScreenFx]], [[SimpleCutscene]], [[TileView]]

## Tested by
[[MotionTests]]
