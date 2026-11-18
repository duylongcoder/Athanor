---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/Ignite.cs
tags: [code/core, kind/class]
aliases: []
---

# IgniteResolver

`public static class IgniteResolver`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/Ignite.cs:43-196`

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static List<(int a, int b)> Pairs(Bench bench, bool diagonals)` | 46 | Occupied neighbouring pairs in reading order (a < b). |
| method | `public static List<(int a, int b)> Pairs(Bench bench, BenchRules rules)` | 48 |  |
| method | `public static bool HasAnyPair(Bench bench, bool diagonals)` | 61 |  |
| method | `public static bool HasAnyPair(Bench bench, BenchRules rules)` | 62 |  |
| method | `public static bool Has(IReadOnlyList<CatalystData> catalysts, string effect)` | 64 |  |
| method | `public static ResolveResult Resolve(Bench bench, ContentDb db, Journal journal, ISet<string> firedThisRun, IReadOnlyList<CatalystData> catalysts, Scor` | 71 | Plays the bench out: the chain of reactions, then a fizzle for every pair of neighbours left over. Updates the journal and the set of recipes fired this run; the bench itself is not changed. |
| method | `public static long Score(ContentDb db, RecipeData recipe, int[] tiles, bool firstThisRun, bool discovery, int chain, int emptyTiles, IReadOnlyList<Cat` | 116 | Potency of one reaction: the result's base, times one less than the number of materials used, doubled the first time this run, times its place in the chain, then the catalysts and the hub's tile. |
| method | `public static long Score(ContentDb db, RecipeData recipe, int[] tiles, bool firstThisRun, bool discovery, int chain, IReadOnlyList<CatalystData> catal` | 120 |  |

## Uses
[[Bench]], [[BenchRules]], [[ChainPlanner]], [[ContentDb]], [[Effects]], [[Journal]], [[ReactionEvent]], [[ResolveResult]], [[ScoreContext]]

## Used by
[[AutoPlayer]], [[RunState]]

## Tested by
[[IgniteTests]], [[Phase3Tests]]
