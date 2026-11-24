---
generated: true
type: test
file: Athanor/Assets/Tests/EditModeGame/SaveTests.cs
tags: [test]
count: 14
---

# SaveTests
`Athanor/Assets/Tests/EditModeGame/SaveTests.cs` — 14 tests

## Covers
[[ContentAssets]], [[ContentDb]], [[Journal]], [[Kind]], [[Profile]], [[ProfileData]], [[Save]]

## Tests
- `LoadProfile_WithNothingSaved_IsFresh_WithASeedOfItsOwn`
- `LoadProfile_WithGarbage_IsFresh`
- `StoreThenLoadProfile_WithStatsAndCompletedGoals_KeepsStatsGoalsAndUnlocks`
- `LoadProfile_WithIdsTheContentNoLongerHas_DropsThem`
- `ResetProfile_ErasesTheProfile_AndKeepsTheJournal`
- `LoadJournal_WithGarbageString_ReturnsEmpty`
- `LoadJournal_CorruptData_ReturnsEmptyJournalAndWarns`
- `LoadProfile_CorruptData_ReturnsFreshProfileAndWarns`
- `LoadJournal_WithOlderShape_ReturnsEmpty`
- `LoadJournal_WithNothingSaved_ReturnsEmpty`
- `StoreThenLoadJournal_WithAPairAndAHint_KeepsRecipesAndHints`
- `StoreThenLoadJournal_WithAThreeItemCombo_KeepsComboRecipes`
- `LoadJournal_Phase1PairKeys_StillLoad`
- `TutorialDone_DefaultsToFalse_AndRoundTrips`
