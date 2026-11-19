---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/DepthStage.cs
tags: [code/game, kind/class]
aliases: []
---

# DepthStage

`public sealed class DepthStage` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/DepthStage.cs:10-44`

> The HD-2D bench of the main scene as the game sees it: the rig (world camera, UI camera), the sway, the per-depth moods and the effects switch, all objects of Main.unity. GameFlow talks to this and nothing else of the depth layer. Cutscenes stay flat (z = 0): SetFlat turns the world camera off and lets the UI camera draw the frame.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public DepthRig rig` | 12 |  |
| field | `public CameraSway sway` | 13 |  |
| field | `public DepthMood mood` | 14 |  |
| field | `public DepthSwitch effects` | 15 |  |
| field | `public WindowView window` | 16 |  |
| method | `public void PlaceTiles(BenchView bench, int count)` | 19 | Puts the nine tiles at their row's depth on the table and on the world layer. |
| method | `public void SetFlat(bool flat)` | 31 | Flat mode for a cutscene (true) and back to the bench (false). |
| method | `public void ShowMood(int depth, bool themedWindow)` | 34 | Lighting, post effects, fog and window view for a depth (1..5). |
| method | `public DepthMood.Preset Look(int depth)` | 40 | The look of a depth (1..5; others are clamped): ambient colour, moon, accent. |
| property | `public bool SwayOn` | 43 | The Settings switch: the idle sway on or off (the shake stays). |

## Uses
[[BenchView]], [[CameraSway]], [[DepthLayer]], [[DepthMath]], [[DepthMood]], [[DepthRig]], [[DepthSwitch]], [[Look]], [[Preset]], [[Px]], [[WindowView]]

## Used by
[[GameFlow]]
