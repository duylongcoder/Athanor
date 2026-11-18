---
generated: true
type: test
file: Athanor/Assets/Tests/EditModeGame/SaveTests.cs
tags: [test]
count: 12
---

# SaveTests
`Athanor/Assets/Tests/EditModeGame/SaveTests.cs` — 12 tests

## Covers
[[ContentData]], [[ContentDb]], [[Journal]], [[Kind]], [[Profile]], [[ProfileData]], [[Save]]

## Tests
- `LoadProfile_WithNothingSaved_IsFresh_WithASeedOfItsOwn`
- `LoadProfile_WithGarbage_IsFresh`
- `StoreThenLoadProfile_WithStatsAndCompletedGoals_KeepsStatsGoalsAndUnlocks`
- `LoadProfile_WithIdsTheContentNoLongerHas_DropsThem`
- `ResetProfile_ErasesTheProfile_AndKeepsTheJournal`
- `LoadJournal_WithGarbageString_ReturnsEmpty`
- `LoadJournal_WithOlderShape_ReturnsEmpty`
- `LoadJournal_WithNothingSaved_ReturnsEmpty`
- `StoreThenLoadJournal_WithAPairAndAHint_KeepsRecipesAndHints`
- `StoreThenLoadJournal_WithAThreeItemCombo_KeepsComboRecipes`
- `LoadJournal_Phase1PairKeys_StillLoad`
- `TutorialDone_DefaultsToFalse_AndRoundTrips`
