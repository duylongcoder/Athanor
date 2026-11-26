---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Tutorial/TutorialSequence.cs
tags: [code/game, kind/class]
aliases: []
---

# TutorialSequence

`public sealed class TutorialSequence` : `ScriptableObject`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Tutorial/TutorialSequence.cs:15-71`

> The whole tutorial as data: its run settings, the scripted trials, the steps (text and lit areas) and the lesson order. TutorialManager plays it; nothing in the manager names a step id.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public TutorialTrialDef[] trials = new TutorialTrialDef[0]` | 23 |  |
| field | `public List<TutorialStepDef> steps = new List<TutorialStepDef>()` | 31 |  |
| field | `public List<TutorialAction> script = new List<TutorialAction>()` | 32 |  |
| method | `public TutorialStepDef Step(string id)` | 34 |  |
| method | `public TutorialTrial Trial(int i)` | 36 |  |
| method | `public TutorialTrial CatalystTrial(string catalystId)` | 39 | The trial after choosing this catalyst; null for a catalyst not in the list. |
| method | `public TutorialTrialDef CatalystTrialDef(string catalystId)` | 41 |  |
| method | `public string[] FirstOfferIds()` | 44 |  |
| method | `public RunOptions Options(Unlocks unlocks)` | 47 | The run options of the tutorial run: the game's, with the quiet start and the fixed first offer. |
| method | `public static TutorialTrial ToTrial(TutorialTrialDef d)` | 52 |  |
| method | `public static string[] Layout(TutorialTrial t)` | 60 | The bench of a trial, 9 tiles, null where empty. |
| method | `public static string[] BrokenLayout(TutorialTrial t)` | 63 | The trial without its last lay: the broken chain that Broken compares against. |

## Uses
[[Bench]], [[CatalystDefinition]], [[CatalystTrialDef]], [[RunOptions]], [[TutorialAction]], [[TutorialLay]], [[TutorialStepDef]], [[TutorialTrial]], [[TutorialTrialDef]], [[Unlocks]]

## Used by
[[GoldenShots]], [[TutorialManager]], [[TutorialSequenceEditor]]

## Tested by
[[PauseTests]], [[TutorialFlowTests]], [[TutorialTests]]
