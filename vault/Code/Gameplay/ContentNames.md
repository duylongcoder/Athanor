---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# ContentNames

`public static class ContentNames`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs:47-74`

> Enum values to and from the strings of ContentData.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static string Rarity(RarityId r)` | 50 | The rarity string; "" for Unset. |
| method | `public static string Kind(KindOrNone k)` | 52 | The kind string; "" for None. |
| method | `public static string Need(EventNeed n)` | 54 | The requirement string; "" for None. |
| method | `public static string Stat(GoalStat s)` | 56 | The camelCase stat id. |
| method | `public static string Reward(RewardKind k)` | 58 | The reward kind string. |
| method | `public static T Parse<T>(string s, T none) where T : struct, Enum` | 61 | The enum value named s; none for "" or null. Throws on an unknown name, so a bad import stops loudly. |
| method | `public static GoalStat ParseStat(string s)` | 64 | The GoalStat for a stat id; throws on an unknown id. |
| method | `public static RewardKind ParseReward(string s)` | 72 | The RewardKind for a reward string; throws on an unknown string. |

## Uses
[[EventNeed]], [[GoalStat]], [[Kind]], [[KindOrNone]], [[RarityId]], [[RewardKind]], [[StatIds]]

## Used by
[[CatalystDefinition]], [[EventOptionDef]], [[GoalDefinition]], [[MaterialDefinition]]

## Tested by
[[ContentEnumTests]]
