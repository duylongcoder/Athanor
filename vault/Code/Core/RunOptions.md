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

# RunOptions

`public sealed class RunOptions`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/Profile.cs:204-220`

> Switches for a run. The default is a Phase 2 run: no aspects, no events, and (with no Unlocks) only what the content leaves open.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Unlocks Unlocks` | 206 |  |
| field | `public bool Aspects, Events` | 207 |  |
| field | `public string ForceEvent` | 209 | Balance checks: the id of an event to meet at every depth it may come to, in place of the draw. |
| field | `public int ForceEventAt` | 211 | With ForceEvent: the one depth to meet it at (the other depths then have no event). 0: every depth it may come to. |
| field | `public int QuietBefore` | 213 | The tutorial: no aspect offer and no event at a depth below this one. 0: none held back. |
| field | `public string[] FirstOffer` | 215 | The tutorial: the catalysts the run's first offer shows, in this order. Locked, held or unknown ids are left out. |
| field | `public string LastLore` | 217 | The Lore event seen last (Profile.LastLore): not drawn again until another Lore event has been seen. Null: none. |
| field | `public static readonly RunOptions Legacy = new RunOptions()` | 218 |  |
| method | `public static RunOptions Full(Unlocks unlocks)` | 219 |  |

## Uses
[[Unlocks]]

## Used by
[[GameFlow]], [[RunState]], [[TutorialScript]]

## Tested by
[[HudTests]], [[LoreTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RarityReportTests]], [[RarityTests]], [[RoleSimTests]], [[TutorialFlowTests]], [[TutorialTests]]
