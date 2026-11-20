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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Px.cs:9-37`

> The art is laid out on a 640x360 canvas with y running down from the top-left corner. The world uses one unit per pixel with the origin at the screen centre and y up.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int W = 640, H = 360` | 11 |  |
| method | `public static Vector3 At(float x, float y)` | 14 | World position of an art-space point. |
| method | `public static Vector3 Mid(float x, float y, float w, float h)` | 17 | World position of the centre of an art-space rectangle given by its top-left corner and size. |
| method | `public static void Fit(Transform t, float x, float y, float w, float h)` | 20 | Puts a 4x4 "dot" sprite object over an art-space rectangle (top-left corner and size): the object's centre and scale. |
| method | `public static Vector3 Snap(Vector3 p)` | 26 |  |
| method | `public static Color Hex(string hex, float a = 1, float k = 1)` | 29 | Colour from "#rrggbb"; k scales brightness past 1 for glow, a is alpha. |
| method | `public static Color Alpha(this Color c, float a)` | 35 |  |
| method | `public static Color Times(this Color c, float k)` | 36 |  |

## Used by
[[BenchView]], [[CardView]], [[CatalystChoiceView]], [[DepthMath]], [[DepthRig]], [[DepthStage]], [[DepthTable]], [[Dust]], [[GameFlow]], [[GoalToast]], [[GoalsView]], [[HudView]], [[IgniteSequence]], [[JournalView]], [[LinkView]], [[Loop]], [[Motion]], [[PauseMenu]], [[PointerInput]], [[ReactionCutscene]], [[RunOverView]], [[SimpleCutscene]], [[Spawn]], [[TileView]], [[TitleView]], [[Tooltip]], [[TutorialController]], [[TutorialHole]], [[ViewCapture]], [[WindowView]]

## Tested by
[[DepthClickTests]], [[DepthLayerTests]], [[LayoutPinTests]], [[MainDepthTests]], [[TutorialFlowTests]]
