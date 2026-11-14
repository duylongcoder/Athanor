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

# ChainPlanner

`public static class ChainPlanner`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/Chain.cs:32-156`

> Works out what a bench does, with no side effects. Each round the liveliest tile (highest Reaction Index, earlier tile on a tie) that has a recipe around it becomes the hub; the largest recipe it can complete with its neighbours fires; the result takes the hub's tile and joins the next round.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int SputterSize = 3` | 35 | Sputters are only reported for combos of this many materials or more. |
| method | `public static IEnumerable<int> Neighbours(int tile, bool diagonals)` | 37 |  |
| method | `public static int[] Neighbours(int tile, BenchRules rules)` | 38 |  |
| method | `public static int Heat(string[] tiles, int hub, ContentDb db, bool diagonals)` | 45 | The Reaction Index of the material on a tile plus that of everything touching it. A recipe fires only when the heat round its hub reaches the recipe's required total, so materials that are not part of the recipe (company) can lift it over, and dull company can leave it short. |
| method | `public static int Heat(string[] tiles, int hub, ContentDb db, BenchRules rules)` | 47 |  |
| method | `public static ChainStep MatchAt(string[] tiles, int hub, ContentDb db, bool diagonals)` | 56 | The best recipe the material on this tile can complete as a hub, or null. |
| method | `public static ChainStep MatchAt(string[] tiles, int hub, ContentDb db, BenchRules rules)` | 57 |  |
| method | `public static RecipeData ShortAt(string[] tiles, int hub, ContentDb db, bool diagonals)` | 63 | The largest recipe laid out round this tile that does not fire only because the heat there is below its required total, or null. Used to tell the player what a dull bench was missing. |
| method | `public static RecipeData ShortAt(string[] tiles, int hub, ContentDb db, BenchRules rules)` | 64 |  |
| method | `public static ChainPlan Plan(string[] tiles, ContentDb db, bool diagonals, bool sputters = true)` | 109 | sputters: false skips looking for spoiled combos (the scripted player does not need them). |
| method | `public static ChainPlan Plan(string[] tiles, ContentDb db, BenchRules rules, bool sputters = true)` | 112 |  |

## Uses
[[BenchRules]], [[ChainPlan]], [[ChainStep]], [[ContentDb]]

## Used by
[[AutoPlayer]], [[BenchView]], [[IgniteResolver]]

## Tested by
[[ChainTests]], [[ContentTests]], [[Phase3Tests]], [[RoleSimTests]]
