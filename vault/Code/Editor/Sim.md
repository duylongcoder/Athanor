---
generated: true
type: code
layer: Editor
kind: class
namespace: Athanor.Tests
file: Athanor/Assets/Editor/Simulation/Sim.cs
tags: [code/editor, kind/class]
aliases: []
---

# Sim

`public static class Sim`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/Simulation/Sim.cs:9-568`

> The simulated players behind the balance tests and the Editor balance reports.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static Unlocks With(ContentDb db, bool starters, bool catalysts)` | 11 |  |
| method | `public static RunState Autoplay(ContentDb db, int seed, RunOptions options)` | 21 |  |
| method | `public static string Line(ContentDb db, string label, Func<int, RunOptions> options, int seeds)` | 30 |  |
| method | `public static RunState RandomPlay(ContentDb db, int seed, RunOptions options)` | 44 | A player who knows nothing, on the whole game: a random aspect, a random option, a random handful of cards on random tiles. |
| method | `public static Career Play(ContentDb db, bool admin, int player, int runs, bool aspects = true, bool events = true, List<string> log = null, int logFro` | 394 |  |
| method | `public static string Pct(double x)` | 447 |  |
| method | `public static void Describe(string role, List<Career> careers, int runs, Action<string> w, int finalDepth)` | 449 |  |
| method | `public static RunState Autoplay(ContentDb db, int seed, List<string> log = null, RunStats stats = null)` | 493 |  |
| method | `public static RunState RandomPlay(ContentDb db, int seed)` | 540 | A player who knows nothing: lays a random handful of cards on random tiles and takes a random catalyst. |
| method | `public static Journal FullJournal(ContentDb db)` | 562 |  |

## Uses
[[AutoMove]], [[AutoPlayer]], [[Bench]], [[BenchRules]], [[ChainPlanner]], [[ContentData]], [[ContentDb]], [[IgniteOutcome]], [[IgniteResolver]], [[Journal]], [[Profile]], [[ProfileData]], [[RunOptions]], [[RunPhase]], [[RunState]], [[Unlocks]]

## Used by
[[BalanceReports]]

## Tested by
[[RarityReportTests]], [[RoleSimTests]], [[RunStateTests]]
