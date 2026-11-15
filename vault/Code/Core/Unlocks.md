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

# Unlocks

`public sealed class Unlocks`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/Profile.cs:152-201`

> Which starting materials and catalysts a run may use.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public bool IsOpen(string id)` | 157 |  |
| property | `public int Count` | 158 |  |
| method | `public static Unlocks Fresh(ContentDb db)` | 161 | Everything that is not locked in the content. |
| method | `public static Unlocks All(ContentDb db)` | 169 |  |
| method | `public static Unlocks For(ContentDb db, Profile profile)` | 178 | What is open by default plus what this profile has won. |
| method | `public static Unlocks Half(ContentDb db, int seed)` | 188 | Fresh, plus the first half (by id, shuffled by seed) of every locked bucket. For balance runs. |

## Uses
[[ContentDb]], [[Profile]], [[Rarities]], [[Rng]]

## Used by
[[GameFlow]], [[RunOptions]], [[RunState]], [[TutorialScript]]

## Tested by
[[HudTests]], [[LoreTests]], [[PauseTests]], [[Phase3Tests]], [[RarityReportTests]], [[RoleSimTests]], [[TutorialFlowTests]], [[TutorialTests]]
