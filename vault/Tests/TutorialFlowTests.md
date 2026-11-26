---
generated: true
type: test
file: Athanor/Assets/Tests/EditModeGame/TutorialFlowTests.cs
tags: [test]
count: 12
---

# TutorialFlowTests
`Athanor/Assets/Tests/EditModeGame/TutorialFlowTests.cs` — 12 tests

## Covers
[[AutoPlayer]], [[Bench]], [[ContentAssets]], [[ContentDb]], [[Journal]], [[Profile]], [[Px]], [[RunOptions]], [[RunPhase]], [[RunState]], [[Save]], [[TutorialGate]], [[TutorialManager]], [[TutorialPlan]], [[TutorialRules]], [[TutorialSequence]], [[TutorialStepDef]], [[Unlocks]]

## Tests
- `Union_WithNoHoles_IsTheWholeCanvas_SoATextOnlyStepDimsEverythingAndDrawsNoRim`
- `Union_OfHoles_IsTheirBoundingBox`
- `Blocks_ADarkStep_EvenWithoutLockPad_SoTheDimNeverLetsAClickThrough`
- `Blocks_ForEveryDarkStepOfTheSequence_IsTrue`
- `Plan_FreshSave_RunsTheTutorial`
- `Plan_JournalWithEntries_AsksOnce`
- `MarkDone_AfterSkip_SetsTutorialDone`
- `AnswerSkipQuestion_Skip_MarksDoneAndStartsTheRunWithoutAReplay`
- `AnswerSkipQuestion_PlayIt_ReplaysInTheSandboxAndLeavesTheSaveAlone`
- `ScriptedTrials_Shown_Equal40_0_40_240`
- `HowToPlay_ThrowawayJournalAndProfile_LeavesStorageByteIdentical`
- `ChooseEventOption_EmptiesEventOptions_SoViewsMustKeepTheChosenOption`
