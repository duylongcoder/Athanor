---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs
tags: [code/game, kind/class]
aliases: []
---

# TutorialRules

`public static class TutorialRules`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs:11-25`

> The decisions around the tutorial that need no scene, so they can be tested. What Begin does is the Core rule TutorialScript.Plan.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static void MarkDone()` | 14 | Skipping (or finishing) the tutorial: never asked again. |
| method | `public static bool AnswerSkipQuestion(bool playIt)` | 20 | The answer to the skip question. Skip marks the tutorial done and the run starts at once (owner Q3, no second Begin); Play it replays the tutorial in the throwaway sandbox and leaves the save as it is (owner Q4). True: play the replay. |

## Uses
[[Save]]

## Used by
[[GameFlow]], [[TutorialController]]

## Tested by
[[TutorialFlowTests]]
