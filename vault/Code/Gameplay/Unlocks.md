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

# Unlocks

`public sealed class Unlocks`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Profile.cs:154-203`

> Which starting materials and catalysts a run may use.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public bool IsOpen(string id)` | 159 |  |
| property | `public int Count` | 160 |  |
| method | `public static Unlocks Fresh(ContentDb db)` | 163 | Everything that is not locked in the content. |
| method | `public static Unlocks All(ContentDb db)` | 171 |  |
| method | `public static Unlocks For(ContentDb db, Profile profile)` | 180 | What is open by default plus what this profile has won. |
| method | `public static Unlocks Half(ContentDb db, int seed)` | 190 | Fresh, plus the first half (by id, shuffled by seed) of every locked bucket. For balance runs. |

## Uses
[[ContentDb]], [[Profile]], [[Rarities]], [[Rng]]

## Used by
[[BalanceReports]], [[GameFlow]], [[GoldenShots]], [[RunOptions]], [[RunState]], [[Sim]], [[TutorialScript]]

## Tested by
[[HudTests]], [[LoreTests]], [[PauseTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RarityReportTests]], [[RulesTests]], [[TutorialFlowTests]], [[TutorialTests]]
