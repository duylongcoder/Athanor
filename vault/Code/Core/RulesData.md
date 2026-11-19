---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/ContentData.cs
tags: [code/core, kind/class]
aliases: []
---

# RulesData

`public class RulesData`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/ContentData.cs:29-53`

> The run's tuning numbers, read from the "rules" block of content.json. Required: ContentDb throws without it.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int handMin, handBase` | 32 | Smallest hand size and the hand size a run starts with. |
| field | `public int trials, trialsFinalDepth` | 34 | Trials per depth, and at the final depth. |
| field | `public int redraws` | 36 | Redraws per run. |
| field | `public int starterCopies` | 38 | Copies of each starter material in the bench deck. |
| field | `public int offerSize` | 40 | Catalyst cards offered after a depth. |
| field | `public int rackSize` | 42 | Bench rack slots. |
| field | `public int pouchCards` | 44 | Cards the pouch adds to the hand. |
| field | `public int maxRi` | 46 | Ceiling of a material's Reaction Index on a tile. |
| field | `public int firstReactionMult` | 48 | Multiplier on the first reaction of a trial. |
| field | `public int[] aspectDepths` | 50 | Depths (1-based) at which the run offers an aspect. |
| field | `public long[] potencyByTier` | 52 | Base potency by material tier; index is the tier. |

## Used by
[[ContentData]], [[ContentDb]]

## Tested by
[[RulesTests]]
