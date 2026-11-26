---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Managers/ProgressManager.cs
tags: [code/game, kind/class]
aliases: []
---

# ProgressManager

`public sealed class ProgressManager` : `Singleton<ProgressManager>`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Managers/ProgressManager.cs:12-72`

> What the player keeps between runs: the journal, the profile (stats, goals done, items won) and what is unlocked. A sandbox (How to play) swaps in a throwaway journal and profile and stores nothing.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public Journal Journal` | 14 |  |
| property | `public Profile Profile` | 15 |  |
| property | `public Unlocks RunUnlocks` | 17 | What was unlocked when the current run started; an item won during a run joins the next one. |
| property | `public bool Sandbox` | 18 |  |
| method | `protected override void OnAwake()` | 22 |  |
| method | `public Unlocks RefreshUnlocks()` | 29 |  |
| method | `public void StoreJournal()` | 31 |  |
| method | `public void StoreProfile()` | 32 |  |
| method | `public List<GoalReward> EvaluateGoals()` | 35 | Completes the goals whose targets are reached, draws their rewards and stores the profile. |
| method | `public void BeginSandbox()` | 42 |  |
| method | `public void EndSandbox()` | 48 |  |
| method | `public void ResetProgress()` | 55 | Erases goals, unlocks and stats (Reset progress). The journal is kept. |
| method | `public void ReloadAfterQuit()` | 63 | Quit to title: back to what is stored, as if the page had been closed. A sandbox keeps its throwaway. |
| method | `public static Journal JournalAfterQuit(bool sandbox, Journal current)` | 70 |  |
| method | `public static Profile ProfileAfterQuit(bool sandbox, Profile current, ContentDb db)` | 71 |  |

## Uses
[[ContentDb]], [[ContentManager]], [[GoalReward]], [[Journal]], [[Profile]], [[Save]], [[Singleton]], [[Unlocks]]

## Used by
[[GameManager]], [[ManagerSceneMigration]], [[ProgressManagerEditor]], [[RunManager]]

## Tested by
[[ManagerBootTests]], [[PauseTests]]
