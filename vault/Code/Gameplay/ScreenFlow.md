---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/ScreenFlow.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# ScreenFlow

`public sealed class ScreenFlow`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/ScreenFlow.cs:9-100`

> The screen state machine: which screen is up and which moves are allowed. No Unity types.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public FlowState State` | 14 | The current screen; Boot at construction. |
| property | `public FlowState JournalFrom` | 17 | Where the open Journal returns to (Bench or Title). |
| property | `public bool InCutscene` | 20 | True while a cutscene plays. |
| property | `public bool Sandbox` | 23 | True while the sandbox is on. |
| field | `public event Action<FlowState, FlowState> Changed` | 26 | Raised as (from, to) on a real change; not raised when the state stays the same. |
| method | `public void Enter(FlowState state)` | 29 | Moves to a screen unconditionally; raises Changed if it differs. |
| method | `public bool OpenJournal(bool titleAsking)` | 38 | Opens the Journal from the Bench, or from the Title when it is not asking a question. |
| method | `public bool CloseJournal()` | 50 | Leaves the Journal for JournalFrom. |
| method | `public bool OpenGoals(bool titleAsking)` | 58 | Opens Goals from the Title (when not asking) or from RunOver. |
| method | `public bool CloseGoals()` | 70 | Leaves Goals for the screen it was opened from. |
| method | `public bool StartIgnite(bool canIgnite)` | 78 | Bench and canIgnite moves to Resolving. |
| method | `public bool CanPause(bool benchInput, bool runPlaying)` | 86 | Pausing needs the Bench, bench input, no cutscene and a run in play. |
| method | `public bool CanAbandon(bool runEnded)` | 90 | Abandoning needs the Bench and a run that has not ended. |
| property | `public bool CanResetProgress` | 93 | Resetting progress is offered on the Title only. |
| method | `public void BeginSandbox()` | 96 | Turns the sandbox on. |
| method | `public void EndSandbox()` | 99 | Turns the sandbox off. |

## Uses
[[Bench]], [[FlowState]], [[Journal]]

## Used by
[[GameFlow]], [[IgniteSequence]]

## Tested by
[[ScreenFlowTests]]
