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

# Newcomer

`sealed class Newcomer`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/Simulation/Sim.cs:70-381` · Nested in [[Sim]]

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| ctor | `public Newcomer(ContentDb truth, Journal journal)` | 78 |  |
| method | `public readonly List<(string a, string b)> targets = new List<(string, string)>()` | 130 |  |
| property | `public int TriedCount` | 131 |  |
| property | `public int ColdCount` | 132 |  |
| method | `public string Status(string a, string b)` | 133 |  |
| method | `public bool CanMakeNow(string m)` | 134 |  |
| method | `public bool Decide(RunState run)` | 177 | One decision. Returns true when the bench is laid and ready to ignite. |
| method | `public void Learn(IgniteOutcome outcome, BenchRules rules)` | 369 | A pair that did nothing is crossed off, unless the journal says one of the two is waiting for more heat than there was round them: then it stays open, to be tried again in livelier company. |

## Uses
[[AutoMove]], [[AutoPlayer]], [[Bench]], [[BenchRules]], [[ChainPlanner]], [[ContentData]], [[ContentDb]], [[IgniteOutcome]], [[IgniteResolver]], [[Journal]], [[RunPhase]], [[RunState]]
