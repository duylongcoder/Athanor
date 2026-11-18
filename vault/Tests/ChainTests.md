---
generated: true
type: test
file: Athanor/Assets/Tests/EditMode/ChainTests.cs
tags: [test]
count: 26
---

# ChainTests
`Athanor/Assets/Tests/EditMode/ChainTests.cs` — 26 tests

## Covers
[[Bench]], [[ChainPlan]], [[ChainPlanner]], [[ContentDb]]

## Tests
- `Neighbours_WithAndWithoutDiagonals_ReturnOrthogonalAndDiagonalTiles`
- `Hub_WithTwoReactiveItems_IsTheHighestReactivity`
- `Hub_RiTie_GoesToEarlierTile`
- `Hub_WithoutMatch_DoesNotBlockOthers`
- `Plan_WithThreeItemComboAndPairs_FiresLargestComboFirst`
- `ShortOfReactivity_IsNoReaction_FallsBackToSmaller`
- `Heat_WithBystandersOnTheBench_IsTheHubPlusEverythingTouchingIt`
- `Company_TouchingTheHub_LiftsARecipeOverItsRequiredTotal`
- `DullCompany_BelowTheRequiredTotal_LeavesTheRecipeShort`
- `Company_NotTouchingTheHub_DoesNotLiftTheRecipe`
- `Company_OnADiagonal_CountsWithDiagonals`
- `Company_ConsumedEarlierInTheChain_NoLongerCounts`
- `ShortAt_WhenOnlyHeatIsLacking_NamesTheRecipe`
- `Chain_WhenResultMatchesAnotherRecipe_ReactsAgain`
- `Made_IsFalse_ForUntouchedItems`
- `Corner_WithTwoOrthogonalNeighbours_CannotHostFourItems`
- `EdgeMiddle_WithThreeNeighbours_HostsFourItems`
- `Centre_WithFourNeighbours_HostsFiveItems`
- `Diagonals_WhenEnabled_CountAsNeighbours`
- `TwoCopies_OfOneItemInARecipe_NeedTwoDistinctTiles`
- `PreEmpted_Combo_Sputters`
- `Pair_Preemption_DoesNotSputter`
- `NineIdenticalItems_Terminates_NoSteps`
- `EmptyAndSingleBench_WhenPlanned_HaveNoSteps`
- `Plan_WithPopulatedBench_DoesNotMutateInput`
- `MatchAt_WhenNothingFits_ReturnsNull`
