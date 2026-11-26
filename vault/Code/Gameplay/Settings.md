---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Settings.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# Settings

`public sealed class Settings`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Settings.cs:12-151`

> Player settings: volumes, haptics and language. Unity-free; Game stores the JSON (PlayerPrefs, key athanor.settings.v1). Public fields so JsonUtility can read it too; ToJson/FromJson are hand-written so Core needs no serializer.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int MaxVolume = 10` | 14 |  |
| field | `public int version = 1` | 17 | For later migrations. |
| field | `public int music = 7` | 19 | 0-10, 0 = off. |
| field | `public int sfx = 8` | 21 | 0-10, 0 = off. |
| field | `public bool haptics = true` | 22 |  |
| field | `public bool sway = true` | 24 | The idle camera sway of the HD-2D bench (owner gate G2 #5). On by default; the shake is not affected. |
| field | `public string language =` | 26 | "" (not chosen: use the system language), "en" or "vi". |
| method | `public Settings Clamp()` | 29 | Forces every field into range. Returns this. |
| method | `public string ToJson()` | 38 | Flat JSON object, same shape JsonUtility writes. |
| method | `public static Settings FromJson(string json)` | 52 | Reads settings; null, empty or unreadable text gives defaults. Missing fields keep defaults; values are clamped. |

## Used by
[[GameManager]], [[GoldenShots]], [[LocalizationManager]], [[PauseMenu]], [[Save]], [[SaveManager]], [[TitleView]]

## Tested by
[[ManagerBootTests]], [[PauseTests]], [[PrefabBakeTests]], [[SettingsHapticsTests]], [[SettingsTests]]
