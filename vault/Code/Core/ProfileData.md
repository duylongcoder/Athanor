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
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/ContentData.cs:76-84`

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int version = 1, rewardSeed` | 78 |  |
| field | `public int runsStarted, runsWon, wonNoRedraw, depthReached, ignites, chainLongest, comboLargest` | 79 |  |
| field | `public long bestIgnite` | 80 |  |
| field | `public string[] completed, unlockedStarters, unlockedCatalysts` | 81 |  |
| field | `public RewardLogData[] rewards` | 82 |  |
| field | `public string lastLore` | 83 |  |

## Used by
[[Profile]], [[Save]]

## Tested by
[[LoreTests]], [[Phase3Tests]], [[SaveTests]]
