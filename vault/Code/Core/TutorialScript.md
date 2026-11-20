---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/Tutorial.cs
tags: [code/core, kind/class]
aliases: []
---

# TutorialScript

`public static class TutorialScript`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/Tutorial.cs:32-123`

> The tutorial (docs/ATHANOR.md §8, v2) as data. Every number the script shows is pinned by TutorialTests; change both together.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int Seed = 20261113` | 34 |  |
| field | `public const int QuietBefore = 3` | 36 | No aspect offer and no event before this depth, so nothing touches the scripted hands. |
| property | `public static readonly string[] Offer =` | 37 |  |
| property | `public static readonly TutorialTrial Reaction = new TutorialTrial` | 40 | Trial 1: a reaction. 40 potency. |
| property | `public static readonly TutorialTrial Miss = new TutorialTrial` | 48 | Trial 2: a miss. Glowmoss is on the lower tile so the hint is written on its page. |
| property | `public static readonly TutorialTrial Chain = new TutorialTrial` | 56 | Trial 3: a chain, 40 then 240, which clears depth 1 (320 of 300). |
| property | `public static readonly TutorialTrial[] Depth1 =` | 63 |  |
| method | `public static string[] Layout(TutorialTrial trial)` | 66 | The bench of a trial, 9 tiles, null where empty. |
| method | `public static string[] BrokenChain()` | 69 | Trial 3 without its last material (Moondust): the broken chain that step 3.6 compares against. |
| method | `public static TutorialTrial CatalystTrial(string catalystId)` | 79 | The first trial of depth 2, laid so the chosen catalyst shows. Null for a catalyst not on the offer. |
| method | `public static RunOptions Options(Unlocks unlocks)` | 106 | The run options of the tutorial run: the game's, with the quiet start and the fixed first offer. |
| method | `public static bool ShouldRun(bool tutorialDone)` | 112 | The tutorial is offered until the save has finished (or skipped) it. Only the TutorialDone flag decides. |
| method | `public static TutorialPlan Plan(bool tutorialDone, int journalEntries)` | 118 | What Begin does: done is None; not done with an empty journal is Run; not done with journal entries (a save from before the tutorial) is AskSkip, asked once. |

## Uses
[[Bench]], [[RunOptions]], [[TutorialLay]], [[TutorialPlan]], [[TutorialTrial]], [[Unlocks]]

## Used by
[[GameFlow]], [[GoldenShots]], [[TutorialController]]

## Tested by
[[PauseTests]], [[TutorialFlowTests]], [[TutorialStepsBakeTests]], [[TutorialTests]]
