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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/EventView.cs:13-132`

> The event at the start of a depth: what was found, the options that can be taken (each with what it costs and may give), then what came of the one chosen.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public struct Timing` | 27 | How the dialog comes in and goes: fade and slide durations, the shade's alpha, where the body slides from, and the flash after a result with notes. |
| property | `public struct Layout` | 30 | The option rows' look: tint under the pointer, label height at rest and pressed. |
| method | `public Label OptionLabel(int i)` | 43 | The label of option row i (rows past the event's option count are hidden). |
| property | `public Label Outcome` | 45 | What came of the option taken. |
| method | `public void Bind(FxRefs effects)` | 50 | Hooks the clicks. GameFlow calls it once at start; a test calls it after instantiating the prefab. |
| method | `public IEnumerator Show(RunState run, Action changed)` | 80 | Shows the run's current event, takes the option the player picks and shows what it did. Ends when Continue is pressed. |

## Uses
[[Art]], [[Clickable]], [[Dev]], [[FxRefs]], [[Label]], [[Lang]], [[Loc]], [[Motion]], [[RunState]], [[UiButton]], [[Wording]]

## Used by
[[GameFlow]], [[GoldenShots]]

## Tested by
[[LayoutPinTests]], [[PrefabBakeTests]]
