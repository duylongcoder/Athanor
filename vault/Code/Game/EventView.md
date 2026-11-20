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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/EventView.cs:13-140`

> The event at the start of a depth: what was found, the options that can be taken (each with what it costs and may give), then what came of the one chosen.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public struct Timing` | 29 | How the dialog comes in and goes: fade and slide durations, the shade's alpha, where the body slides from, and the flash after a result with notes. |
| property | `public struct Layout` | 32 | The option rows' look: tint under the pointer, label height at rest and pressed. |
| method | `public Label OptionLabel(int i)` | 45 | The label of option row i (rows past the event's option count are hidden). |
| property | `public Label Outcome` | 47 | What came of the option taken. |
| method | `public void Bind(FxRefs effects)` | 52 | Hooks the clicks. GameFlow calls it once at start; a test calls it after instantiating the prefab. |
| method | `public IEnumerator Show(RunState run, Action changed)` | 82 | Shows the run's current event, takes the option the player picks and shows what it did. Ends when Continue is pressed. |
| method | `public void Tick(float deltaTime)` | 127 | One frame of the option rows: a row's sprite, tint and label height are written only when its look (pressed, pointed at) changes. |

## Uses
[[Art]], [[Clickable]], [[Dev]], [[FxRefs]], [[Label]], [[Lang]], [[Loc]], [[Motion]], [[RunState]], [[UiButton]], [[Wording]]

## Used by
[[GameFlow]], [[GoldenShots]]

## Tested by
[[HotPathTests]], [[LayoutPinTests]], [[PrefabBakeTests]]
