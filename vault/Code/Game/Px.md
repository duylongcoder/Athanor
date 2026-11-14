---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Px.cs
tags: [code/game, kind/class]
aliases: []
---

# Px

`public static class Px`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Px.cs:9-30`

> The art is laid out on a 640x360 canvas with y running down from the top-left corner. The world uses one unit per pixel with the origin at the screen centre and y up.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int W = 640, H = 360` | 11 |  |
| method | `public static Vector3 At(float x, float y)` | 14 | World position of an art-space point. |
| method | `public static Vector3 Mid(float x, float y, float w, float h)` | 17 | World position of the centre of an art-space rectangle given by its top-left corner and size. |
| method | `public static Vector3 Snap(Vector3 p)` | 19 |  |
| method | `public static Color Hex(string hex, float a = 1, float k = 1)` | 22 | Colour from "#rrggbb"; k scales brightness past 1 for glow, a is alpha. |
| method | `public static Color Alpha(this Color c, float a)` | 28 |  |
| method | `public static Color Times(this Color c, float k)` | 29 |  |

## Used by
[[BenchView]], [[CardView]], [[CatalystChoiceView]], [[Dust]], [[EventView]], [[GameFlow]], [[GoalToast]], [[GoalsView]], [[HudView]], [[JournalView]], [[LinkView]], [[Loop]], [[Make]], [[Motion]], [[PointerInput]], [[ReactionCutscene]], [[RunOverView]], [[SceneBuilder]], [[ScreenFx]], [[SimpleCutscene]], [[TileView]], [[TitleView]], [[Tooltip]], [[UiButton]], [[Wording]]
