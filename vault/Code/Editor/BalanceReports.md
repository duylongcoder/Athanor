---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/Balance/BalanceReports.cs
tags: [code/editor, kind/class]
aliases: []
---

# BalanceReports

`public static class BalanceReports`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/Balance/BalanceReports.cs:16-254`

> The balance reports (Athanor > Balance, or BalanceReports.RunBatch). They print numbers and assert nothing. dotnet run --project tools/CoreReports -- balance-matrix | events | roles | newcomer | balance | trace Settings come from the same ATHANOR_* environment variables the old Explicit tests read.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static int Run(string report, TextWriter output)` | 23 | Writes one report to output; returns 0, or 2 for an unknown report name. |
| method | `public static void RunBatch()` | 45 | unity run Athanor -- -executeMethod BalanceReports.RunBatch -report balance-matrix -out /tmp/m.txt |

## Uses
[[ContentAssets]], [[ContentData]], [[ContentDb]], [[RunOptions]], [[RunPhase]], [[RunStats]], [[Sim]], [[Unlocks]]
