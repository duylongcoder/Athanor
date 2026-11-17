---
generated: true
type: test
file: Athanor/Assets/Tests/EditModeGame/DepthLayerTests.cs
tags: [test]
count: 9
---

# DepthLayerTests
`Athanor/Assets/Tests/EditModeGame/DepthLayerTests.cs` — 9 tests

## Covers
[[ArtLibrary]], [[DepthLayer]], [[DepthMath]], [[Px]]

## Tests
- `Fov_AtRest_Shows180UnitsAboveAndBelowTheCentre`
- `Scale_SpecDepths_MatchTheSpecTable`
- `Place_AtAnyDepth_IsSeenWhereItWasDrawn`
- `TableZ_MatchesTheSpecRows_AndClampsOutsideTheTop`
- `TableZ_EveryArtRow_LiesOnOnePlane`
- `Parallax_MatchesTheSpecTable_AndIsZeroAtTheAnchor`
- `DepthLayer_Apply_PlacesAndScalesTheObjectAndItsPointLight`
- `ArtLibrary_MainScene_HasNoDepthArt`
- `DepthArt_ImportSettings_KeepTheBlurSmoothAndTheFogTiling`
