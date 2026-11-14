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

# Ops

`public static class Ops`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/ContentData.cs:97-110`

> The event effect names (EffectData.op) and option requirements used in content.json.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string Trials = "Trials", Hand = "Hand", Redraws = "Redraws", RiShift = "RiShift", GiveDiscovery = "GiveDiscovery", LoseRack = "LoseRack"` | 99 |  |
| property | `public static readonly string[] All =` | 102 |  |
| field | `public const string NeedRack = "Rack", NeedCatalyst = "Catalyst", NeedTrial = "Trial", NeedRedraw =` | 107 |  |
| property | `public static readonly string[] Needs =` | 108 |  |
| property | `public static readonly string[] Types =` | 109 |  |

## Used by
[[AutoPlayer]], [[ContentDb]], [[RunState]], [[Wording]]

## Tested by
[[ContentTests]], [[LoreTests]], [[Phase3Tests]]
