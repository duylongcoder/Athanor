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

# Ops

`public static class Ops`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/ContentData.cs:129-142`

> The event effect names (EffectData.op) and option requirements used in the content assets.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string Trials = "Trials", Hand = "Hand", Redraws = "Redraws", RiShift = "RiShift", GiveDiscovery = "GiveDiscovery", LoseRack = "LoseRack"` | 131 |  |
| property | `public static readonly string[] All =` | 134 |  |
| field | `public const string NeedRack = "Rack", NeedCatalyst = "Catalyst", NeedTrial = "Trial", NeedRedraw =` | 139 |  |
| property | `public static readonly string[] Needs =` | 140 |  |
| property | `public static readonly string[] Types =` | 141 |  |

## Used by
[[AutoPlayer]], [[ContentDb]], [[RunState]], [[Wording]]

## Tested by
[[ContentEnumTests]], [[ContentTests]], [[LoreTests]], [[Phase3Tests]], [[RarityTests]], [[RulesTests]]
