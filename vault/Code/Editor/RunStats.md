---
generated: true
type: code
layer: Editor
kind: class
namespace: Athanor.Tests
file: Athanor/Assets/Editor/Simulation/Sim.cs
tags: [code/editor, kind/class]
aliases: []
---

# RunStats

`public sealed class RunStats`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/Simulation/Sim.cs:483-491` · Nested in [[Sim]]

> What happened over one scripted run, for the balance report.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public readonly int[] BySize = new int[ContentDb.MaxItems + 1]` | 485 |  |
| field | `public int Ignites, LongestChain, Sputters, FirstTrialClears` | 486 |  |
| field | `public bool UltimateByCombo` | 487 |  |
| field | `public readonly HashSet<string> ComboMade = new HashSet<string>()` | 488 |  |
| field | `public readonly Dictionary<string, int> Combos = new Dictionary<string, int>()` | 489 |  |
| field | `public readonly List<string> ClearedIn = new List<string>()` | 490 |  |

## Uses
[[ContentDb]]

## Used by
[[BalanceReports]]

## Tested by
[[RunStateTests]]
