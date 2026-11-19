---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/PointerInput.cs
tags: [code/game, kind/class]
aliases: []
---

# PointerInput

`public sealed class PointerInput` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/PointerInput.cs:8-87`

> Mouse or touch position in world space, press state for this frame, and hover/click dispatch to Clickables.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public static Vector2 World` | 10 |  |
| property | `public static DepthRig Rig` | 12 | The depth rig, when the bench is drawn by the perspective world camera (null: the flat game). |
| property | `public static Vector2 Bench` | 15 | The pointer on the bench table, in flat coordinates (what a tile tests): found by the world ray, so a tile is hit where it is drawn, under sway and shake. Equals World without a rig or in a flat cutscene. |
| property | `public static bool Down` | 16 |  |
| property | `public static bool Up` | 17 |  |
| property | `public static bool Held` | 18 |  |
| property | `public static Clickable Hover` | 19 |  |
| field | `public static event Action AnyPress` | 21 | Raised on every press, whatever is under it (used for click-to-skip). |
| method | `public static void Use(DepthRig rig)` | 27 | Sets the depth rig that Bench is found through (null: the flat game). |
| method | `public static void ResetStatics()` | 31 | Back to the declared defaults before the first scene (domain reload off keeps statics across Play). |
| method | `public static void Register(Clickable c)` | 37 |  |
| method | `public static void Unregister(Clickable c)` | 38 |  |
| method | `public static Vector2 ScreenToWorld(Vector2 screen)` | 41 | The game is drawn at a whole multiple of 640x360, centred; this undoes that. |
| method | `public static void Resolve(Vector2 world)` | 50 | Sets World and finds Bench from it (Update's step, separate so tests can drive it). |

## Uses
[[Bench]], [[Clickable]], [[DepthRig]], [[Px]]

## Used by
[[BenchView]], [[Clickable]], [[Cutscene]], [[GameFlow]]

## Tested by
[[MainDepthTests]], [[StaticResetTests]]
