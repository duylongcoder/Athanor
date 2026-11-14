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
- `Neighbours_OrthogonalAndDiagonal`
- `Hub_IsHighestReactivity`
- `Hub_RiTie_GoesToEarlierTile`
- `Hub_WithoutMatch_DoesNotBlockOthers`
- `LargestComboFirst`
- `ShortOfReactivity_IsNoReaction_FallsBackToSmaller`
- `Heat_IsTheHubPlusEverythingTouchingIt`
- `Company_LiftsARecipeOverItsRequiredTotal`
- `DullCompany_LeavesTheRecipeShort`
- `Company_MustTouchTheHub`
- `Company_OnADiagonal_CountsWithDiagonals`
- `Company_ConsumedEarlierInTheChain_NoLongerCounts`
- `ShortAt_NamesTheRecipeThatOnlyLacksHeat`
- `Chain_ResultReactsAgain`
- `Made_IsFalse_ForUntouchedItems`
- `Corner_CannotHostFourItems`
- `EdgeMiddle_HostsFourItems`
- `Centre_HostsFiveItems`
- `Diagonals_CountAsNeighbours`
- `TwoCopies_NeedTwoDistinctTiles`
- `PreEmpted_Combo_Sputters`
- `Pair_Preemption_DoesNotSputter`
- `NineIdenticalItems_Terminates_NoSteps`
- `EmptyAndSingleBench_NoSteps`
- `Plan_DoesNotMutateInput`
- `MatchAt_NullWhenNothingFits`
