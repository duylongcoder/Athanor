---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/ContentData.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# ProfileData

`public class ProfileData`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/ContentData.cs:78-86`

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int version = 1, rewardSeed` | 80 |  |
| field | `public int runsStarted, runsWon, wonNoRedraw, depthReached, ignites, chainLongest, comboLargest` | 81 |  |
| field | `public long bestIgnite` | 82 |  |
| field | `public string[] completed, unlockedStarters, unlockedCatalysts` | 83 |  |
| field | `public RewardLogData[] rewards` | 84 |  |
| field | `public string lastLore` | 85 |  |

## Used by
[[Profile]], [[Save]], [[Sim]]

## Tested by
[[LoreTests]], [[Phase3Tests]], [[RulesTests]], [[SaveTests]]
