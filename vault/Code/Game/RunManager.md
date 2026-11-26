---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Managers/RunManager.cs
tags: [code/game, kind/class]
aliases: []
---

# RunManager

`public sealed class RunManager` : `Singleton<RunManager>`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Managers/RunManager.cs:8-47`

> The run being played, its seed, and the scripted player (F9). Rules stay in RunState; this only owns it.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public int SeedOverride` | 13 |  |
| property | `public RunState Run` | 15 | The run being played; null before the first one. |
| property | `public AutoPlayer AutoPlayer` | 16 |  |
| property | `public int LastSeed` | 18 | The seed the current run started with (shown in the Inspector, for bug reports). |
| method | `protected override void OnAwake()` | 21 |  |
| method | `public int NextSeed()` | 24 | The next run's seed: the override, or a fresh one. |
| method | `public RunState Begin(RunOptions options, int seed)` | 31 | Starts a run at depth 1 on the player's journal. |
| method | `public IgniteOutcome Settle()` | 38 | Ignites and stores the journal at once, so a discovery is saved before anything else can happen. A sandbox stores nothing. |
| method | `public static IgniteOutcome SettleIgnite(RunState run, Journal journal, bool sandbox)` | 41 | The Ignite and the journal store as one step; PauseTests call it without a scene. |

## Uses
[[AutoPlayer]], [[ContentManager]], [[IgniteOutcome]], [[Journal]], [[ProgressManager]], [[RunOptions]], [[RunState]], [[Save]], [[Singleton]], [[Tooltip]]

## Used by
[[GameManager]], [[ManagerSceneMigration]], [[RunManagerEditor]]

## Tested by
[[ManagerBootTests]], [[PauseTests]]
