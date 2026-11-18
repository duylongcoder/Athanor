---
generated: true
type: test
file: Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs
tags: [test]
count: 12
---

# PrefabBakeTests
`Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs` — 12 tests

## Covers
[[Clickable]], [[ContentData]], [[ContentDb]], [[EventView]], [[FontKind]], [[GoalReward]], [[GoalToast]], [[Journal]], [[Label]], [[Loc]], [[Make]], [[RunOptions]], [[RunOverView]], [[RunPhase]], [[RunState]], [[UiButton]], [[Unlocks]], [[Wording]]

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
