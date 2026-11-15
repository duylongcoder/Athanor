---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/WindowView.cs
tags: [code/game, kind/class]
aliases: []
---

# WindowView

`public sealed class WindowView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/WindowView.cs:11-185`

> The workshop window shows what the current depth "sees" and plays one tiny looping effect (docs/devlogs/phase-5-lore.md): 1 dust motes up a shaft, 2 drips down the glass, 3 a breathing amber glow, 4 roots creeping a row at a time, 5 a faint gold point that flickers. Effects are a fixed set of sprites moved from Time.time, so they stop with the pause and allocate nothing per frame.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Build(Transform workshop)` | 25 | Finds the pane in the workshop and makes the effect sprites (hidden). |
| method | `public void Show(int depth)` | 41 | Swaps the view and starts its effect for a depth (1..5; deeper keeps 5). |
| method | `public void ShowNight()` | 53 | Back to the original night sky with no effect (title screen, first-run tutorial, How to play). |

## Uses
[[DepthLook]], [[Dust]], [[Make]], [[Px]]

## Used by
[[GameFlow]]
