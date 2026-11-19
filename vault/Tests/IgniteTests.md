---
generated: true
type: test
file: Athanor/Assets/Tests/EditMode/IgniteTests.cs
tags: [test]
count: 34
---

# IgniteTests
`Athanor/Assets/Tests/EditMode/IgniteTests.cs` — 34 tests

## Covers
[[Bench]], [[ContentDb]], [[Effects]], [[IgniteResolver]], [[Journal]], [[ReactionEvent]]

## Tests
- `Pairs_FullBench_Has12Orthogonal_InReadingOrder`
- `Pairs_WithDiagonals_Has20`
- `Pairs_WithTilesEndingOneRowAndStartingTheNext_DoNotWrapAcrossRows`
- `NoPairs_WithOnlyDistantTiles_ReturnsEmpty`
- `SingleReaction_Scores40_OnFirstRunDiscovery`
- `SecondReactionInIgnite_AfterAFirstReaction_IsMultipliedBy2`
- `SameRecipeTwiceInOneIgnite_WithTwoMatchingPairs_OnlyFirstIsDiscovery`
- `KnownRecipe_NextRun_ScoresFirstThisRunOnly`
- `Fizzle_GivesHint_ThenNoDuplicateHint`
- `Fizzle_ForLackOfHeat_HintsAtTheHeatNeeded_Once`
- `Fizzle_ForLackOfHeat_SaysNothingAboutAKnownRecipe`
- `NeedsCompanyRecipe_WithCompanyBesideTheHub_TheSameBenchReacts`
- `Fizzle_WhenTheJournalKnowsARecipe_DoesNotHintAtAlreadyKnownRecipes`
- `Fizzles_ComeAfterReactions_AndDoNotAdvanceTheChain`
- `KindDouble_Doubles`
- `KindDouble_OtherKind_NoChange`
- `FirstTriple_WithTwoReactions_TriplesOnlyTheFirst`
- `CentreTwice_DoublesPotency_WhenCentreInvolved`
- `CentreTwice_NoChange_AwayFromCentre`
- `EmptyTileBonus_Adds20PerEmptyTile`
- `ChainPlus_CountsOneStepHigher`
- `DiscoveryTriple_Triples`
- `Diagonals_WithCornerAndCentre_LetCornersReactWithCentre`
- `Reaction_Event_NamesHubTilesAndItems`
- `ThreeItem_WithPQAndR_Scores240`
- `FiveItem_WithCentreAndFourNeighbours_ScoresFourTimesBase`
- `Chain_SecondReaction_MultipliedByChain`
- `ChainPlus_AddsOne_ToEveryStep`
- `KindDouble_ChecksEveryConsumedItem`
- `Journal_AfterAThreeItemCombo_LearnsComboKey`
- `UnderThreshold_Combo_FallsBack_AndTheLeftoverFizzles_NotSputters`
- `Sputter_Event_HasNoPotencyAndNoDiscovery`
- `Fizzle_AfterChain_ForLeftoverPair_GivesHintOnce`
- `Resolve_ReportsFinalBench_AndDoesNotMutateTheBench`
