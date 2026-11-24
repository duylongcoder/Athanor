---
generated: true
type: test
file: Athanor/Assets/Tests/EditMode/ContentDatabaseTests.cs
tags: [test]
count: 6
---

# ContentDatabaseTests
`Athanor/Assets/Tests/EditMode/ContentDatabaseTests.cs` — 6 tests

## Covers
[[ContentDatabase]], [[ContentDb]], [[GameRules]], [[Kind]], [[MaterialDefinition]], [[RecipeDefinition]]

## Tests
- `ToContentData_TinyDatabase_BuildsAValidContentDb`
- `ToContentData_Material_WritesTheStringsTheRulesRead`
- `ToContentData_MaterialWithoutGlow_WritesAnEmptyGlowString`
- `ToContentData_NoRarityWeights_IsNullSoTheDrawStaysUniform`
- `ToContentData_RecipeWithAMissingItem_IsRejectedByContentDb`
- `ToContentData_CalledTwice_ReturnsIndependentCopies`
