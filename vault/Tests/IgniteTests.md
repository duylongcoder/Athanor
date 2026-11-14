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
- `Pairs_DoNotWrapAcrossRows`
- `NoPairs_ReturnsEmpty`
- `SingleReaction_Scores40_OnFirstRunDiscovery`
- `SecondReactionInIgnite_IsMultipliedBy2`
- `SameRecipeTwiceInOneIgnite_OnlyFirstIsDiscovery`
- `KnownRecipe_NextRun_ScoresFirstThisRunOnly`
- `Fizzle_GivesHint_ThenNoDuplicateHint`
- `Fizzle_ForLackOfHeat_HintsAtTheHeatNeeded_Once`
- `Fizzle_ForLackOfHeat_SaysNothingAboutAKnownRecipe`
- `WithCompany_TheSameBenchReacts`
- `Fizzle_DoesNotHintAtAlreadyKnownRecipes`
- `Fizzles_ComeAfterReactions_AndDoNotAdvanceTheChain`
- `KindDouble_Doubles`
- `KindDouble_OtherKind_NoChange`
- `FirstTriple_OnlyFirst`
- `CentreTwice_DoublesPotency_WhenCentreInvolved`
- `CentreTwice_NoChange_AwayFromCentre`
- `EmptyTileBonus_Adds20PerEmptyTile`
- `ChainPlus_CountsOneStepHigher`
- `DiscoveryTriple_Triples`
- `Diagonals_LetCornersReactWithCentre`
- `Reaction_Event_NamesHubTilesAndItems`
- `ThreeItem_Scores240`
- `FiveItem_ScoresFourTimesBase`
- `Chain_SecondReaction_MultipliedByChain`
- `ChainPlus_AddsOne_ToEveryStep`
- `KindDouble_ChecksEveryConsumedItem`
- `Journal_LearnsComboKey`
- `UnderThreshold_Combo_FallsBack_AndTheLeftoverFizzles_NotSputters`
- `Sputter_Event_HasNoPotencyAndNoDiscovery`
- `Fizzle_AfterChain_ForLeftoverPair_GivesHintOnce`
- `Resolve_ReportsFinalBench_AndDoesNotMutateTheBench`
