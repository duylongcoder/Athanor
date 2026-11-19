---
generated: true
type: test
file: Athanor/Assets/Tests/EditMode/ContentTests.cs
tags: [test]
count: 41
---

# ContentTests
`Athanor/Assets/Tests/EditMode/ContentTests.cs` — 41 tests

## Covers
[[Bench]], [[ChainPlanner]], [[ContentData]], [[ContentDb]], [[Ops]], [[RunState]]

## Tests
- `Starters_InTheLoadedContent_Number13With8OfThemOpen`
- `Kinds_AcrossAllMaterials_Number7AndEachIsUsed`
- `Discoveries_43FromPhase2_24FromPhase3`
- `EveryMaterialFitsTheJournalGrid`
- `ComboRecipes_ByItemCount_FallInTheExpectedRanges`
- `Ultimate_TracedBackThroughRecipes_IsReachableViaAtLeastOneCombo`
- `EveryDiscovery_ExceptKnownDeadEnds_IsUsedOrIsTheUltimate`
- `Catalysts_InTheLoadedContent_Number26With9OfThemOpen`
- `DepthTargetsAre_300_1500_7500_37500`
- `EveryRecipe_InTheLoadedContent_UsesItemsForm`
- `EveryRecipe_CanFire_WithStartersForCompany`
- `RequiredTotals_Bite_FromTierTwoUp`
- `Company_ForARecipeAskingMoreThanItsOwnHeat_IsWhatItLacksOnItsOwn`
- `Constructor_Throws_WhenNoCompanyCouldEverBeEnough`
- `BasePotency_ForTiersOneToFive_MatchesTheTable`
- `TryReact_WithThePairInEitherOrder_GivesTheSameResult`
- `TryReact_False_ForUnknownPair`
- `EveryRecipe_ResultTier_IsMaxInputTierPlus1`
- `Ultimate_AndItsRecipes_IsTier5FromTier4Inputs`
- `EveryMaterial_FollowingRecipesFromStarters_IsReachable`
- `EveryStarter_CountingPartners_HasAtLeast3`
- `Constructor_Throws_OnUnknownMaterialInRecipe`
- `Constructor_Throws_OnDuplicatePair`
- `Recipe_ThreeItems_LookedUpUnordered`
- `Legacy_AB_Recipe_FoldedIntoItems`
- `RecipesContaining_AnItemInSeveralRecipes_ListsLargestFirst`
- `Reactivity_OfAnItemAndAThreeItemCombo_GivesItemAndTotalValues`
- `PartnersOf_AnItemInCombos_CoversComboMembers`
- `Constructor_Throws_On6Items`
- `Constructor_Throws_On1Item`
- `Constructor_Throws_OnThreeCopies`
- `Constructor_Allows_TwoCopies`
- `Constructor_Throws_OnDuplicateItemSet`
- `Constructor_Throws_OnReactivityMissingOrOutOf1To10`
- `Constructor_Throws_OnNegativeMinReactivity`
- `Reactivity_OfMaterialsInEachTier_HasBothLowAndHighValues`
- `Reactivity_OfBestStarterAndWorstTier4_IsNotMonotonicInTier`
- `ComboKey_IsOrderIndependent`
- `PairKey_IsOrderIndependent`
- `EventTexts_OfEveryEvent_FitTheEventWindow`
- `LoreEvents_DoNothing_AndTellTheStoryInOrder`
