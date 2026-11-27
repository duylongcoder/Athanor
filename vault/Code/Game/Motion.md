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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Motion.cs:17-88`

> One-shot tweens on DOTween, kept in one place so every move is snapped to whole pixels (Px.Snap). Each call takes its start value explicitly and returns the Tween: group several with Motion.Seq().Group(..), and wait for them in a coroutine with Motion.Wait or .ToYieldInstruction(). Scaled time, as Anim.Over. The ease defaults to Linear here, not to DOTween's own default (OutQuad): every helper sets its ease, so nothing depends on DOTween.defaultEaseType. Each helper also guards its target, so a tween that outlives a destroyed object does nothing. Only for animations that run to a fixed end; spring follows, hover lifts, idle loops and particles stay hand-written.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static Tween Move(Transform t, Vector3 from, Vector3 to, float seconds, Ease ease = Ease.Linear)` | 24 | World position, snapped every frame. |
| method | `public static Tween MoveLocal(Transform t, Vector3 from, Vector3 to, float seconds, Ease ease = Ease.Linear)` | 28 | Local position, snapped every frame. |
| method | `public static Tween Arc(Transform t, Vector3 from, Vector3 to, float height, float seconds, Ease ease = Ease.Linear)` | 35 | World position along a line eased by ease, lifted by height in a hump (Anim.Hump over plain time) and snapped. With from equal to to it is a hop on the spot. |
| method | `public static Tween Scale(Transform t, float from, float to, float seconds, Ease ease = Ease.Linear)` | 43 | Uniform x and y scale (z stays 1). |
| method | `public static Tween Fade(SpriteRenderer r, float from, float to, float seconds, Ease ease = Ease.Linear)` | 47 | Alpha of a sprite; its colour is kept. |
| method | `public static Tween Fade(Label l, float from, float to, float seconds, Ease ease = Ease.Linear)` | 51 | Alpha of a label and its shadow. |
| method | `public static Tween Custom<T>(T target, float from, float to, float seconds, Action<T, float> set, Ease ease = Ease.Linear) where T : class` | 55 | A number between from and to handed to set together with target, for what no helper above covers. |
| method | `public static Tween Delay(float seconds)` | 59 | A tween that does nothing for this long: it holds a Seq open, as the longest member. |
| method | `public static Sequence Seq()` | 62 | A new, empty sequence. Add tweens that start together with Group, and one at a time with Insert. |
| method | `public static Sequence Group(this Sequence sequence, Tween tween)` | 65 | Adds a tween that starts with the others already in the sequence. |
| method | `public static void StopOn(Component owner)` | 71 | Stops every tween that targets the component or anything under its object: the transforms, sprites and labels the helpers above animate. A view calls it from OnDestroy, after StopAllCoroutines, so nothing outlives it into the next Play. |
| method | `public static IEnumerator Wait(Tween tween, Func<bool> hurry = null)` | 81 | Waits for a tween or a sequence. When hurry returns true it jumps to its end values at once. |
| method | `public static IEnumerator ToYieldInstruction(this Tween tween)` | 87 | For yield return tween.ToYieldInstruction(): waits until the tween is done or stopped. |

## Uses
[[Anim]], [[Label]], [[Px]]

## Used by
[[BenchView]], [[CatalystChoiceView]], [[EventView]], [[GameManager]], [[IgniteSequence]], [[RunOverView]], [[ScreenFx]], [[SimpleCutscene]], [[TileView]]

## Tested by
[[MotionTests]]
