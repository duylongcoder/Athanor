---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/EventView.cs
tags: [code/game, kind/class]
aliases: []
---

# EventView

`public sealed class EventView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/EventView.cs:13-116`

> The event at the start of a depth: what was found, the options that can be taken (each with what it costs and may give), then what came of the one chosen.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Build()` | 32 |  |
| method | `public IEnumerator Show(RunState run, Action changed)` | 68 | Shows the run's current event, takes the option the player picks and shows what it did. Ends when Continue is pressed. |

## Uses
[[Align]], [[FontKind]], [[Label]], [[Make]], [[Motion]], [[Px]], [[RunState]], [[ScreenFx]], [[UiButton]], [[Wording]]

## Used by
[[GameFlow]]
