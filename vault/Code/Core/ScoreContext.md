---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/BenchRules.cs
tags: [code/core, kind/class]
aliases: []
---

# ScoreContext

`public sealed class ScoreContext`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/BenchRules.cs:82-89`

> What one reaction's score depends on beyond the recipe and the catalysts.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int Depth = 1, EmptyTiles` | 84 |  |
| field | `public BenchRules Rules = BenchRules.None` | 85 |  |
| field | `public bool[] EmptyAtIgnite` | 86 |  |
| field | `public bool TripleFirst` | 87 |  |
| field | `public Func<string, bool> IsStarter` | 88 |  |

## Uses
[[BenchRules]]

## Used by
[[AutoPlayer]], [[IgniteResolver]], [[RunState]]

## Tested by
[[Phase3Tests]]
