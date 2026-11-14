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
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/BenchRules.cs:83-90`

> What one reaction's score depends on beyond the recipe and the catalysts.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int Depth = 1, EmptyTiles` | 85 |  |
| field | `public BenchRules Rules = BenchRules.None` | 86 |  |
| field | `public bool[] EmptyAtIgnite` | 87 |  |
| field | `public bool TripleFirst` | 88 |  |
| field | `public Func<string, bool> IsStarter` | 89 |  |

## Uses
[[BenchRules]]

## Used by
[[AutoPlayer]], [[IgniteResolver]], [[RunState]]

## Tested by
[[Phase3Tests]]
