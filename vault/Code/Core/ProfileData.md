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
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/ContentData.cs:47-55`

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int version = 1, rewardSeed` | 49 |  |
| field | `public int runsStarted, runsWon, wonNoRedraw, depthReached, ignites, chainLongest, comboLargest` | 50 |  |
| field | `public long bestIgnite` | 51 |  |
| field | `public string[] completed, unlockedStarters, unlockedCatalysts` | 52 |  |
| field | `public RewardLogData[] rewards` | 53 |  |
| field | `public string lastLore` | 54 |  |

## Used by
[[Profile]], [[Save]]

## Tested by
[[LoreTests]], [[Phase3Tests]], [[SaveTests]]
