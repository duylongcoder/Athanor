---
generated: true
type: test
file: Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs
tags: [test]
count: 40
---

# PrefabBakeTests
`Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs` — 40 tests

## Covers
[[Bench]], [[BenchView]], [[CardView]], [[CatalystChoiceView]], [[ChoiceCard]], [[Clickable]], [[ContentData]], [[ContentDb]], [[EventView]], [[GoalReward]], [[GoalRow]], [[GoalToast]], [[GoalsView]], [[HudView]], [[Journal]], [[JournalCell]], [[JournalView]], [[Label]], [[Loc]], [[PauseMenu]], [[Profile]], [[Rarities]], [[RunOptions]], [[RunOverView]], [[RunPhase]], [[RunState]], [[Settings]], [[TitleView]], [[Tooltip]], [[UiButton]], [[Unlocks]], [[Wording]]

## Tests
- `Relabel_KeyedLabelInPrefab_FollowsTheLanguage`
- `Relabel_PlainLabelInPrefab_KeepsItsText`
- `Click_ButtonInPrefab_RaisesClicked`
- `Click_DisabledButtonInPrefab_DoesNothing`
- `Text_ButtonInPrefab_HasLabelAndBody`
- `Disabled_SetOnAButton_IsKeptInTheSerializedState`
- `ShowDepth_RunOverPrefab_WritesTheDepthRow`
- `Click_RunOverPrefabButton_PicksItsChoice`
- `Bind_EventPrefabWithoutFx_Throws`
- `Show_EventPrefab_ListsTheOptions`
- `Click_EventPrefabLastOptionThenContinue_ShowsTheOutcomeAndCloses`
- `Add_GoalToastPrefab_ShowsTheGoalName`
- `Click_ShowingGoalToastPrefab_SkipsToTheSlideOut`
- `Show_CatalystChoicePrefab_FillsThreeCards`
- `Show_CatalystChoicePrefabRareCard_DrawsThreeStars`
- `Click_CatalystChoicePrefabCardAfterTheDeal_PicksItsId`
- `Open_PausePrefab_ShowsTheMenu`
- `Click_PausePrefabResume_ClosesAndRestoresTime`
- `Click_PausePrefabCancelOnTheQuitQuestion_BacksOutToTheMenu`
- `Click_PausePrefabOutsideTheDialog_Resumes`
- `Bind_GoalsPrefab_MakesOneRowPerGoal`
- `Open_GoalsPrefabFreshProfile_WritesEveryGoal`
- `Click_GoalsPrefabClose_ClosesAndTellsTheOwner`
- `Bind_JournalPrefab_MakesOneCellPerMaterial`
- `Click_JournalPrefabCell_ShowsThatMaterial`
- `Click_JournalPrefabPageLabel_TurnsTheGridPage`
- `Click_JournalPrefabClose_ClosesAndTellsTheOwner`
- `Show_TooltipInBenchPrefab_HasItsSlicedPanel`
- `Bind_TitlePrefab_ShowsTheLogoText`
- `Click_TitlePrefabBegin_SetsBeginPressed`
- `Click_TitlePrefabSettingsThenLanguage_SwitchesTheLanguageAndBackCloses`
- `Click_TitlePrefabEraseLink_AsksThenConfirmingRaisesResetConfirmed`
- `AskSkip_TitlePrefab_AnswersPlayFromTheSkipDialog`
- `Bind_HudPrefab_ShowsTheDepthNumber`
- `Hover_HudPrefabSocketAndAspect_SetsAndClearsTheHoveredIndex`
- `Click_HudPrefabRedraw_RaisesRedrawClickedOnlyWhileARedrawIsLeft`
- `Click_HudPrefabPauseJournalAndIgnite_RaiseTheirEvents`
- `Refresh_BenchPrefab_HasNineTiles`
- `Refresh_BenchPrefab_DealsOneCardPerStartingMaterialFromTheCardPrefab`
- `Show_CardPrefab_OneWordNameHidesTheSecondLine`
