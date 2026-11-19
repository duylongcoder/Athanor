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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/EventView.cs:13-124`

> The event at the start of a depth: what was found, the options that can be taken (each with what it costs and may give), then what came of the one chosen.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public Label OptionLabel(int i)` | 35 | The label of option row i (rows past the event's option count are hidden). |
| property | `public Label Outcome` | 37 | What came of the option taken. |
| method | `public void Bind(FxRefs effects)` | 42 | Hooks the clicks. GameFlow calls it once at start; a test calls it after instantiating the prefab. |
| method | `public IEnumerator Show(RunState run, Action changed)` | 72 | Shows the run's current event, takes the option the player picks and shows what it did. Ends when Continue is pressed. |

## Uses
[[Art]], [[Clickable]], [[Dev]], [[FxRefs]], [[Label]], [[Lang]], [[Loc]], [[Motion]], [[RunState]], [[UiButton]], [[Wording]]

## Used by
[[GameFlow]], [[GoldenShots]]

## Tested by
[[PrefabBakeTests]]
