---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Tutorial.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# TutorialGate

`public static class TutorialGate`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Tutorial.cs:27-38`

> When the first run is the tutorial: only the TutorialDone flag decides, plus the skip question for an old save.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static bool ShouldRun(bool tutorialDone)` | 30 | The tutorial is offered until the save has finished (or skipped) it. |
| method | `public static TutorialPlan Plan(bool tutorialDone, int journalEntries)` | 33 | Done: None. Not done with an empty journal: Run. Not done with journal entries (a save from before the tutorial): AskSkip, asked once. |

## Uses
[[TutorialPlan]]

## Used by
[[GameManager]]

## Tested by
[[TutorialFlowTests]], [[TutorialTests]]
