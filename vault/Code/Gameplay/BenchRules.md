---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/BenchRules.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# BenchRules

`public sealed class BenchRules`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/BenchRules.cs:11-79`

> What catalysts, aspects and events change about the bench itself: which tiles touch, how lively a tile counts, which tiles are shut. The planner, the resolver and the scripted player all read this, so they cannot disagree. Build one, then leave it alone: the neighbour table is worked out once.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public bool Diagonals` | 14 |  |
| field | `public int RiShift, HeatPlus` | 15 |  |
| field | `public int Pivot = -1` | 16 |  |
| field | `public readonly bool[] Blocked = new bool[Bench.TileCount]` | 17 |  |
| field | `public readonly int[] RiBonus = new int[Bench.TileCount]` | 18 |  |
| field | `public readonly bool[] Kiln = new bool[Bench.TileCount], Crossroads = new bool[Bench.TileCount]` | 19 |  |
| field | `public readonly List<LinkPair> Links = new List<LinkPair>()` | 20 |  |
| property | `public readonly int[] HubMult =` | 22 |  |
| field | `public readonly bool[] Heavy = new bool[Bench.TileCount]` | 23 |  |
| field | `public readonly string[] AttunedKind = new string[Bench.TileCount]` | 24 |  |
| field | `public readonly int[] SeedBonus = new int[Bench.TileCount]` | 25 |  |
| property | `public static readonly BenchRules None = new BenchRules(), Diagonal = new BenchRules` | 28 | The Phase 2 bench, with or without diagonal neighbours. |
| method | `public static BenchRules Plain(bool diagonals)` | 29 |  |
| method | `public int[] Around(int tile)` | 34 | The tiles a hub on this tile reaches, ascending. |
| method | `public int Ri(ContentDb db, int tile, string id)` | 71 | The Reaction Index a material counts for on this tile. |
| method | `public bool Touch(int a, int b)` | 78 | Either tile reaches the other (Crossroads makes this one-sided for a hub; a pair needs only one side). |

## Uses
[[Bench]], [[ContentDb]], [[LinkPair]]

## Used by
[[AutoPlayer]], [[ChainPlanner]], [[IgniteResolver]], [[Newcomer]], [[RunState]], [[ScoreContext]], [[Sim]]

## Tested by
[[Phase3Tests]]
