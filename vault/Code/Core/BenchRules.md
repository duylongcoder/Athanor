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

# BenchRules

`public sealed class BenchRules`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/BenchRules.cs:11-80`

> What catalysts, aspects and events change about the bench itself: which tiles touch, how lively a tile counts, which tiles are shut. The planner, the resolver and the scripted player all read this, so they cannot disagree. Build one, then leave it alone: the neighbour table is worked out once.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int MaxRi = 13` | 13 |  |
| field | `public bool Diagonals` | 15 |  |
| field | `public int RiShift, HeatPlus` | 16 |  |
| field | `public int Pivot = -1` | 17 |  |
| field | `public readonly bool[] Blocked = new bool[Bench.TileCount]` | 18 |  |
| field | `public readonly int[] RiBonus = new int[Bench.TileCount]` | 19 |  |
| field | `public readonly bool[] Kiln = new bool[Bench.TileCount], Crossroads = new bool[Bench.TileCount]` | 20 |  |
| method | `public readonly List<(int a, int b)> Links = new List<(int, int)>()` | 21 |  |
| property | `public readonly int[] HubMult =` | 23 |  |
| field | `public readonly bool[] Heavy = new bool[Bench.TileCount]` | 24 |  |
| field | `public readonly string[] AttunedKind = new string[Bench.TileCount]` | 25 |  |
| field | `public readonly int[] SeedBonus = new int[Bench.TileCount]` | 26 |  |
| property | `public static readonly BenchRules None = new BenchRules(), Diagonal = new BenchRules` | 29 | The Phase 2 bench, with or without diagonal neighbours. |
| method | `public static BenchRules Plain(bool diagonals)` | 30 |  |
| method | `public int[] Around(int tile)` | 35 | The tiles a hub on this tile reaches, ascending. |
| method | `public int Ri(ContentDb db, int tile, string id)` | 72 | The Reaction Index a material counts for on this tile. |
| method | `public bool Touch(int a, int b)` | 79 | Either tile reaches the other (Crossroads makes this one-sided for a hub; a pair needs only one side). |

## Uses
[[Bench]], [[ContentDb]]

## Used by
[[AutoPlayer]], [[ChainPlanner]], [[IgniteResolver]], [[RunState]], [[ScoreContext]]

## Tested by
[[Phase3Tests]], [[RoleSimTests]]
