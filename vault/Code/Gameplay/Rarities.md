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

# Rarities

`public static class Rarities`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/ContentData.cs:88-95`

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string Common = "Common", Uncommon = "Uncommon", Rare = "Rare", Epic = "Epic", Legendary = "Legendary", Mythic = "Mythic", WorldSeed =` | 90 |  |
| property | `public static readonly string[] All =` | 92 |  |
| method | `public static int Rank(string rarity)` | 93 |  |
| method | `public static string Of(string rarity)` | 94 |  |

## Used by
[[BenchView]], [[CatalystChoiceView]], [[ContentDb]], [[GoalsView]], [[GoldenShots]], [[JournalView]], [[Loc]], [[RarityStars]], [[RunState]], [[Unlocks]], [[Wording]]

## Tested by
[[ContentEnumTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RarityReportTests]], [[RarityTests]]
