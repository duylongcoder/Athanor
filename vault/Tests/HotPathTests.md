---
generated: true
type: test
file: Athanor/Assets/Tests/EditModeGame/HotPathTests.cs
tags: [test]
count: 12
---

# HotPathTests
`Athanor/Assets/Tests/EditModeGame/HotPathTests.cs` — 12 tests

## Covers
[[Art]], [[BenchView]], [[CardView]], [[Clickable]], [[ContentData]], [[ContentDb]], [[Dust]], [[EventView]], [[Flame]], [[FogSheet]], [[HudView]], [[Journal]], [[Label]], [[LinkView]], [[Loc]], [[PointerInput]], [[RunState]], [[ScreenFx]], [[Sparks]], [[TileView]], [[Tooltip]], [[UiButton]]

## Tests
- `Tick_IdleHud_AllocatesNothing`
- `Tick_PotencyUnchanged_KeepsTheSameText`
- `Tick_IdleBench_AllocatesNothing`
- `Tick_BenchWithTooltipUp_AllocatesNothing`
- `Tick_TileAndTileWithMaterial_AllocateNothing`
- `Tick_IdleCard_AllocatesNothing`
- `Tick_IdleEventView_AllocatesNothing`
- `Tick_IdleUiButton_AllocatesNothing`
- `Tick_FogSheet_AllocatesNothingWithinATexelAndWhenOneIsCrossed`
- `Tick_IdleScreenFx_AllocatesNothing`
- `IdleBench_TenSecondsOfEveryUpdate_AllocatesZeroBytes`
- `Continuous_DustSparksAndFlame_AllocateNothingWhileRunning`
