---
generated: true
type: test
file: Athanor/Assets/Tests/EditMode/LoreTests.cs
tags: [test]
count: 9
---

# LoreTests
`Athanor/Assets/Tests/EditMode/LoreTests.cs` — 9 tests

## Covers
[[AutoMove]], [[AutoPlayer]], [[ContentData]], [[ContentDb]], [[Journal]], [[Ops]], [[Profile]], [[ProfileData]], [[RunOptions]], [[RunPhase]], [[RunState]], [[Unlocks]]

## Tests
- `TheLastLoreEvent_WhenRemembered_IsNotDrawn`
- `WithoutALastLore_OverManySeeds_EveryLoreEventCanCome`
- `ALastLore_ThatIsNotALoreEvent_ChangesNothing`
- `InARun_ALoreEvent_IsNotShownTwiceInARow`
- `AnotherLoreEvent_InBetween_LetsTheFirstComeBack`
- `ANonLoreEvent_InBetween_DoesNotLetItComeBack`
- `SkippingALoreEvent_WhenPlayedWithAndWithoutIt_IsBalanceNeutral`
- `Profile_WhenShownLoreBoonAndNullEvents_RemembersOnlyLoreEvents`
- `Profile_LastLore_IsSaved_AndDroppedWhenNoLongerALoreEvent`
