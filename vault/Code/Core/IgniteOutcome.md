---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/RunState.cs
tags: [code/core, kind/class]
aliases: []
---

# IgniteOutcome

`public sealed class IgniteOutcome`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/RunState.cs:9-17`

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public List<ReactionEvent> Events` | 11 |  |
| field | `public long Gained` | 12 |  |
| field | `public List<string> Dropped = new List<string>()` | 13 |  |
| field | `public string[] Final` | 14 |  |
| field | `public bool[] Made` | 15 |  |
| field | `public RunPhase PhaseAfter` | 16 |  |

## Uses
[[ReactionEvent]], [[RunPhase]]

## Used by
[[Profile]], [[RunState]]

## Tested by
[[Phase3Tests]], [[RoleSimTests]], [[RunStateTests]], [[TutorialTests]]
