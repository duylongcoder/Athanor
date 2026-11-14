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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/PointerInput.cs:8-63`

> Mouse or touch position in world space, press state for this frame, and hover/click dispatch to Clickables.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public static Vector2 World` | 10 |  |
| property | `public static bool Down` | 11 |  |
| property | `public static bool Up` | 12 |  |
| property | `public static bool Held` | 13 |  |
| property | `public static Clickable Hover` | 14 |  |
| field | `public static event Action AnyPress` | 16 | Raised on every press, whatever is under it (used for click-to-skip). |
| method | `public static void Register(Clickable c)` | 21 |  |
| method | `public static void Unregister(Clickable c)` | 22 |  |
| method | `public static Vector2 ScreenToWorld(Vector2 screen)` | 25 | The game is drawn at a whole multiple of 640x360, centred; this undoes that. |

## Uses
[[Clickable]], [[Px]]

## Used by
[[BenchView]], [[Clickable]], [[Cutscene]], [[GameFlow]], [[SceneBuilder]]
