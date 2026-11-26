---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Profile.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# RunOptions

`public sealed class RunOptions`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Profile.cs:206-224`

> Switches for a run. The default is a Phase 2 run: no aspects, no events, and (with no Unlocks) only what the content leaves open.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Unlocks Unlocks` | 208 |  |
| field | `public bool Aspects, Events` | 209 |  |
| field | `public string ForceEvent` | 211 | Balance checks: the id of an event to meet at every depth it may come to, in place of the draw. |
| field | `public int ForceEventAt` | 213 | With ForceEvent: the one depth to meet it at (the other depths then have no event). 0: every depth it may come to. |
| field | `public int QuietBefore` | 215 | The tutorial: no aspect offer and no event at a depth below this one. 0: none held back. |
| field | `public string[] FirstOffer` | 217 | The tutorial: the catalysts the run's first offer shows, in this order. Locked, held or unknown ids are left out. |
| field | `public string LastLore` | 219 | The Lore event seen last (Profile.LastLore): not drawn again until another Lore event has been seen. Null: none. |
| field | `public int PouchMode = 2` | 221 | How unlocked starters reach the hand. 2 (the game): from their own pouch, PouchCards a hand. 0: two copies in the satchel. 1: one copy in the satchel. 0 and 1 are kept for the balance report. |
| field | `public static readonly RunOptions Legacy = new RunOptions()` | 222 |  |
| method | `public static RunOptions Full(Unlocks unlocks)` | 223 |  |

## Uses
[[Unlocks]]

## Used by
[[BalanceReports]], [[GameManager]], [[GoldenShots]], [[RunManager]], [[RunState]], [[Sim]], [[TutorialSequence]]

## Tested by
[[HudTests]], [[LoreTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RarityReportTests]], [[RarityTests]], [[RulesTests]], [[TutorialFlowTests]], [[TutorialTests]]
