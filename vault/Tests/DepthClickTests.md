---
generated: true
type: test
file: Athanor/Assets/Tests/EditModeGame/DepthClickTests.cs
tags: [test]
count: 8
---

# DepthClickTests
`Athanor/Assets/Tests/EditModeGame/DepthClickTests.cs` — 8 tests

## Covers
[[ArtLibrary]], [[DepthMath]], [[DepthRig]], [[FogSheet]], [[LayoutData]], [[Make]], [[Px]], [[TileView]], [[TiltShift]]

## Tests
- `ClickOnTileCentre_AtRestAndUnderMaximumSway_HitsThatTile`
- `ClickWhereTheCameraShowsTheTileCentre_UnderSwayAndShake_FindsThatPoint`
- `WorldRay_UnderSwayAndShake_IsTheWorldCamerasOwnRay`
- `Sway_KeepsTheAnchorRowStill_AndMovesTheWallLessThanAnArtPixel`
- `SetView_ShakeOnly_MovesEveryDepthByTheSameWholeArtPixels`
- `TiltShiftSigma_IsZeroInTheBand_AndRisesToTheMaximum`
- `Show_TileReloadedFromAScene_ShowsTheMaterialAndTakesClicks`
- `FogOffset_StepsInWholeTexels_AndWraps`
