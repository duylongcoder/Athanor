---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/RunState.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# IgniteOutcome

`public sealed class IgniteOutcome`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/RunState.cs:9-17`

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
[[GameFlow]], [[IgniteSequence]], [[Newcomer]], [[Profile]], [[RunState]], [[Sim]]

## Tested by
[[Phase3Tests]], [[RunStateTests]], [[TutorialTests]]
