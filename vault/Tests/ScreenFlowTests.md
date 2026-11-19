---
generated: true
type: test
file: Athanor/Assets/Tests/EditMode/ScreenFlowTests.cs
tags: [test]
count: 20
---

# ScreenFlowTests
`Athanor/Assets/Tests/EditMode/ScreenFlowTests.cs` — 20 tests

## Covers
[[Bench]], [[Journal]], [[ScreenFlow]]

## Tests
- `OpenJournal_FromBench_ClosesBackToBench`
- `OpenJournal_FromTitle_ClosesBackToTitle`
- `OpenJournal_WhileTitleAsks_IsRefused`
- `OpenJournal_FromResolving_IsRefused`
- `CloseJournal_NotInJournal_ChangesNothing`
- `OpenGoals_FromRunOver_ClosesBackToRunOver`
- `OpenGoals_FromBench_IsRefused`
- `StartIgnite_BenchAndCanIgnite_GoesToResolving`
- `StartIgnite_CannotIgnite_StaysOnBench`
- `StartIgnite_FromJournal_IsRefused`
- `CanPause_BenchInputNoCutscenePlaying_IsTrue`
- `CanPause_InCutscene_IsFalse`
- `CanPause_InputOff_IsFalse`
- `CanPause_RunNotPlaying_IsFalse`
- `CanPause_NotOnBench_IsFalse`
- `CanAbandon_RunEnded_IsFalse`
- `CanResetProgress_OnTitle_IsTrue`
- `Changed_Enter_ReportsFromAndTo`
- `Changed_EnterSameState_IsNotRaised`
- `Sandbox_BeginThenEnd_IsFalseAgain`
