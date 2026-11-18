---
generated: true
type: test
file: Athanor/Assets/Tests/EditModeGame/PauseTests.cs
tags: [test]
count: 10
---

# PauseTests
`Athanor/Assets/Tests/EditModeGame/PauseTests.cs` — 10 tests

## Covers
[[Bench]], [[Clickable]], [[ContentData]], [[ContentDb]], [[FlowState]], [[GameFlow]], [[Haptics]], [[Journal]], [[PauseMenu]], [[Profile]], [[RunState]], [[Save]], [[Settings]], [[TutorialController]], [[TutorialScript]], [[Unlocks]]

## Tests
- `Open_StopsTime_AndEveryExitRestoresIt`
- `Quit_RestoresTime_BeforeTheOwnerHearsOfIt`
- `Esc_Toggles_AndBacksOutOfTheQuitQuestion`
- `Abort_TutorialRun_DoesNotMarkItDone`
- `Open_NotAllowed_IsRefused`
- `Allowed_OnlyOnTheBenchWithInput_NotInCutscene_TutorialIsPausable`
- `VibrationButton_NoHaptics_IsHidden`
- `AfterQuit_ProfileAndJournalAreWhatIsStored_AndStorageIsNotWritten`
- `Quit_AfterADiscovery_JournalInStorageHasIt`
- `SettleIgnite_Sandbox_StoresNothing`
