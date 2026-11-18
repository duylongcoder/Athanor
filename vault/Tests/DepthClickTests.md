---
generated: true
type: test
file: Athanor/Assets/Tests/EditModeGame/DepthClickTests.cs
tags: [test]
count: 9
---

# DepthClickTests
`Athanor/Assets/Tests/EditModeGame/DepthClickTests.cs` — 9 tests

## Covers
[[BenchView]], [[DepthMath]], [[DepthRig]], [[FogSheet]], [[Px]], [[TileView]], [[TiltShift]]

## Tests
- `ClickOnTileCentre_AtRestAndUnderMaximumSway_HitsThatTile`
- `ClickWhereTheCameraShowsTheTileCentre_UnderSwayAndShake_FindsThatPoint`
- `DrawnFace_UnderShakeUpToThreePx_IsHitByTheRayAtCentreAndInsetCorners`
- `WorldRay_UnderSwayAndShake_IsTheWorldCamerasOwnRay`
- `Sway_KeepsTheAnchorRowStill_AndMovesTheWallLessThanAnArtPixel`
- `SetView_ShakeOnly_MovesEveryDepthByTheSameWholeArtPixels`
- `TiltShiftSigma_IsZeroInTheBand_AndRisesToTheMaximum`
- `Show_TileReloadedFromAScene_ShowsTheMaterialAndTakesClicks`
- `FogOffset_StepsInWholeTexels_AndWraps`
