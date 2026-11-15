---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/Profile.cs
tags: [code/core, kind/class]
aliases: []
---

# Profile

`public sealed class Profile`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/Profile.cs:18-149`

> What the player keeps between runs besides the journal: counters, completed goals and the locked starters and catalysts won from them.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int RunsStarted, RunsWon, WonNoRedraw, DepthReached, Ignites, ChainLongest, ComboLargest` | 20 |  |
| field | `public long BestIgnite` | 21 |  |
| field | `public int RewardSeed` | 22 |  |
| field | `public string LastLore` | 24 | The id of the last Lore event the player was shown (kept across runs), or null. A run leaves it out of its next draw. |
| ctor | `public Profile(int rewardSeed = 1)` | 29 |  |
| property | `public IReadOnlyCollection<string> Completed` | 31 |  |
| property | `public IReadOnlyCollection<string> UnlockedStarters` | 32 |  |
| property | `public IReadOnlyCollection<string> UnlockedCatalysts` | 33 |  |
| property | `public IReadOnlyList<RewardLogData> Rewards` | 34 |  |
| method | `public bool IsComplete(string goalId)` | 35 |  |
| method | `public bool HasUnlocked(string id)` | 36 |  |
| method | `public string RewardOf(string goalId)` | 38 | The item a completed goal gave, or null. |
| method | `public void RunStarted()` | 42 |  |
| method | `public void EventShown(EventData e)` | 45 | Call when an event is shown. Only Lore events are remembered. |
| method | `public void DepthBegan(int depth)` | 47 |  |
| method | `public void IgniteResolved(IgniteOutcome outcome)` | 49 |  |
| method | `public void RunEnded(RunState run)` | 62 | Call once when a run reaches Won or Lost. |
| method | `public long Stat(string id, ContentDb db, Journal journal)` | 72 |  |
| method | `public long Progress(GoalData goal, ContentDb db, Journal journal)` | 91 | How far a goal is, capped at its target. |
| method | `public List<GoalReward> Evaluate(ContentDb db, Journal journal)` | 98 | Completes every goal whose target has been reached, in the order of the content file, and draws each one's reward from the items still locked in its bucket. |
| method | `public ProfileData ToData()` | 120 |  |
| method | `public static Profile FromData(ProfileData d, ContentDb db = null, int freshSeed = 1)` | 129 | Never throws: anything malformed or no longer in the content is skipped. db may be null (nothing is checked). |

## Uses
[[ContentDb]], [[GoalReward]], [[IgniteOutcome]], [[Journal]], [[ProfileData]], [[Rng]], [[RunPhase]], [[RunState]], [[StatIds]]

## Used by
[[GameFlow]], [[GoalsView]], [[Save]], [[Unlocks]]

## Tested by
[[LoreTests]], [[PauseTests]], [[Phase3Tests]], [[RoleSimTests]], [[SaveTests]], [[TutorialFlowTests]]
