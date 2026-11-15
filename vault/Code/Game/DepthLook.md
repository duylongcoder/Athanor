---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/DepthLook.cs
tags: [code/game, kind/class]
aliases: []
---

# DepthLook

`public static class DepthLook`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/DepthLook.cs:9-31`

> What a depth looks like from the bench: the intro line under its name and the view through the window. Pure choices, no scene; the pictures and effects are drawn by WindowView.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int Authored = 5` | 12 | Depths that have their own line and view; deeper ones (Endless) keep the last view and show no line. |
| method | `public static string IntroKey(int depth)` | 15 | Lang key of the depth's intro line, or null when the depth has none (0, or beyond the authored five). |
| method | `public static int View(int depth)` | 18 | Which window view (1..5) a depth shows: the depth itself, clamped, so Endless keeps the deepest view. |
| method | `public static bool ShowsRunIntro(int depth, bool tutorial, bool sandbox)` | 24 | Whether a run starts with the depth 1 line (depth 1 never goes through the descent cutscene). Not in the first-run tutorial nor in How to play, whose scripted start must not be held up. |
| method | `public static bool ThemedWindow(bool inRun, bool tutorial, bool sandbox)` | 27 | Whether the window shows the depth's own view; the title, the tutorial and How to play keep the original night sky. |
| method | `public static string WindowSprite(int depth)` | 30 | Name of the window sprite in the art library for a depth. |

## Used by
[[GameFlow]], [[SimpleCutscene]], [[WindowView]]

## Tested by
[[DepthLookTests]]
