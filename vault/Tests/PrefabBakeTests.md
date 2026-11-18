---
generated: true
type: test
file: Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs
tags: [test]
count: 22
---

# PrefabBakeTests
`Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs` — 22 tests

## Covers
[[CatalystChoiceView]], [[ChoiceCard]], [[Clickable]], [[ContentData]], [[ContentDb]], [[EventView]], [[FontKind]], [[GoalReward]], [[GoalRow]], [[GoalToast]], [[GoalsView]], [[Journal]], [[Label]], [[Loc]], [[Make]], [[PauseMenu]], [[Profile]], [[Rarities]], [[RunOptions]], [[RunOverView]], [[RunPhase]], [[RunState]], [[Settings]], [[UiButton]], [[Unlocks]], [[Wording]]

## Tests
- `Relabel_KeyedLabelAfterPrefabRoundTrip_FollowsTheLanguage`
- `Relabel_PlainLabelAfterPrefabRoundTrip_KeepsItsText`
- `Click_ButtonAfterPrefabRoundTrip_RaisesClicked`
- `Click_DisabledButtonAfterPrefabRoundTrip_DoesNothing`
- `Text_ButtonAfterPrefabRoundTrip_KeepsLabelAndBody`
- `Disabled_SetBeforePrefabRoundTrip_StaysDisabled`
- `ShowDepth_RunOverPrefab_WritesTheDepthRow`
- `Click_RunOverPrefabButton_PicksItsChoice`
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
