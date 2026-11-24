---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/AutoPlayer.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# AutoPlayer

`public sealed class AutoPlayer`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/AutoPlayer.cs:13-433`

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public double HintValue = 0` | 29 | What a hint is worth to this player when weighing an event: nothing to one who knows every recipe. |
| field | `public bool RedrawWhenIdle = true` | 31 | Redraw when no layout makes anything this player wants. Off for a player with no route to want yet. |
| field | `public bool SeekFrontier = false` | 33 | For a player who does not know the whole tree: with no route to the last substance, work toward the highest materials it can make. |
| field | `public Dictionary<string, int> Wanted` | 35 | Materials this player is working toward while it knows no route to the last substance (material, how many). |
| method | `public bool CanMake(string id)` | 37 | This player knows how to make the material from starting materials (or it is one). |
| method | `public int CostOf(string id)` | 39 | Reactions it takes to make one from starting materials. |
| property | `public bool KnowsTheWay` | 40 |  |
| method | `public int Demand(string id)` | 42 | How many of a material the last plan wanted in all, as an end or as an ingredient. |
| ctor | `public AutoPlayer(ContentDb db)` | 50 |  |
| method | `public void Step(RunState run)` | 66 | One whole decision, including pressing Ignite. |
| method | `public AutoMove Decide(RunState run)` | 72 | Picks a catalyst, or redraws, or lays the bench out and leaves the Ignite to the caller. |
| property | `struct Move` | 223 |  |

## Uses
[[AspectEffects]], [[AutoMove]], [[Bench]], [[BenchRules]], [[ChainPlanner]], [[ContentDb]], [[Effects]], [[IgniteResolver]], [[Ops]], [[RunPhase]], [[RunState]], [[ScoreContext]]

## Used by
[[GameFlow]], [[Newcomer]], [[Sim]]

## Tested by
[[LoreTests]], [[Phase3Tests]], [[RarityReportTests]], [[RunStateTests]], [[TutorialFlowTests]]
