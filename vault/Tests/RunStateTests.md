---
generated: true
type: test
file: Athanor/Assets/Tests/EditMode/RunStateTests.cs
tags: [test]
count: 31
---

# RunStateTests
`Athanor/Assets/Tests/EditMode/RunStateTests.cs` — 31 tests

## Covers
[[AutoMove]], [[AutoPlayer]], [[Bench]], [[ContentData]], [[ContentDb]], [[Effects]], [[IgniteOutcome]], [[Journal]], [[RunPhase]], [[RunState]]

## Tests
- `NewRun_Has5Cards_4Trials_2Redraws_Target300`
- `Place_Then_Unplace_ReturnsCard`
- `Place_OnOccupiedTile_False`
- `CanIgnite_False_WithoutAdjacentPair`
- `Ignite_SpendsTrial_ClearsBench_RefillsHand_KeepsUnplayedCards`
- `ReachingTarget_GivesDepthCleared_WithThreeDistinctOffers`
- `ChooseCatalyst_AdvancesDepth_ResetsPotencyAndTrials`
- `OutOfTrials_BelowTarget_IsLost`
- `Depth5_HasSixTrials_NoTarget_AndMakingUltimate_IsWon`
- `Depth5_PotencyAlone_DoesNotClear`
- `Redraw_ReplacesHand_AndDecrements_FalseAtZero`
- `ExtraHand_WhenChosenAsCatalyst_Gives6Cards`
- `SmallSatchel_WithFewerCardsThanTheHand_DrawsWhatExists`
- `Discovery_StaysInHand_WithoutTakingAStarterSlot`
- `Discovery_ThatReacts_IsUsedUp_ButStartersGoBackToTheSatchel`
- `PlacedDiscovery_NeverReacting_IsLost`
- `Unplace_BeforeIgnite_KeepsTheDiscovery`
- `PlacedStarters_AfterIgnite_AlwaysReturnToTheSatchel`
- `ChainSurvivor_GoesToRack_ConsumedIntermediateDoesNot`
- `Ultimate_MadeMidChain_Wins`
- `HasFired_BeforeAndAfterIgnitingTheRecipe_ReportsByRecipe`
- `Redraw_AfterMakingADiscovery_KeepsDiscoveries`
- `FullRack_SeveralResults_StaysAt12_AndDropsTheWeakest`
- `MakingTheUltimate_AtDepthOne_WinsAtAnyDepth`
- `Bot_PlacesFiveItemCombo_WithTheLiveliestOnTheCentre`
- `Bot_WithARecipeAskingForCompany_GivesItTheCompanyItNeeds`
- `Bot_WithAMadeDiscoveryAskingForCompany_GivesItTheCompanyItNeeds`
- `Bot_DoesNotThrowAwayADiscovery_WhenForcedToIgnite`
- `ScriptedRun_WithSeedOne_ReachesWon`
- `InformedPlayer_Over40Seeds_WinsMostSeeds`
- `InformedPlayer_Over40Seeds_RarelyFiresAFiveItemCombo`
