---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/ContentData.cs
tags: [code/core, kind/class]
aliases: []
---

# ProfileData

`public class ProfileData`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/ContentData.cs:49-57`

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int version = 1, rewardSeed` | 51 |  |
| field | `public int runsStarted, runsWon, wonNoRedraw, depthReached, ignites, chainLongest, comboLargest` | 52 |  |
| field | `public long bestIgnite` | 53 |  |
| field | `public string[] completed, unlockedStarters, unlockedCatalysts` | 54 |  |
| field | `public RewardLogData[] rewards` | 55 |  |
| field | `public string lastLore` | 56 |  |

## Used by
[[Profile]], [[Save]]

## Tested by
[[LoreTests]], [[Phase3Tests]], [[SaveTests]]
