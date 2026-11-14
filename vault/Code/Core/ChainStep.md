---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/Chain.cs
tags: [code/core, kind/class]
aliases: []
---

# ChainStep

`public sealed class ChainStep`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/Chain.cs:11-18`

> One thing the bench does during an Ignite. A reaction: the hub and the neighbours it consumed (Tiles[0] is the hub, the rest ascending). A sputter: a combo of three or more whose layout was standing ready and was broken by the reaction before it.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int Hub` | 13 |  |
| field | `public int[] Tiles` | 14 |  |
| field | `public string[] Items` | 15 |  |
| field | `public RecipeData Recipe` | 16 |  |
| field | `public bool Sputter` | 17 |  |

## Used by
[[ChainPlan]], [[ChainPlanner]]
