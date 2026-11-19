---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Boot/Save.cs
tags: [code/game, kind/class]
aliases: []
---

# Save

`public static class Save`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Boot/Save.cs:8-88`

> Keeps the journal and the profile between sessions in PlayerPrefs (browser storage in a web build).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string Key = "athanor.journal.v1", IntroKey = "athanor.intro.v1", ProfileKey = "athanor.profile.v1", TutorialKey = "athanor.tutorial.v1",` | 10 |  |
| method | `public static Journal LoadJournal()` | 14 | The saved journal; an empty one when nothing is saved or what is saved cannot be read. |
| method | `public static void StoreJournal(Journal journal)` | 24 |  |
| method | `public static Profile LoadProfile(ContentDb db)` | 34 | The saved profile (stats, goals done, items won); a new one with a reward seed of its own when nothing is saved or what is saved cannot be read. Anything the content no longer has is dropped. |
| method | `public static void StoreProfile(Profile profile)` | 45 |  |
| method | `public static void ResetProfile()` | 52 | Forgets goals, unlocks and stats. The journal is kept. |
| method | `public static Settings LoadSettings()` | 59 | The saved settings; defaults when nothing is saved or what is saved cannot be read (Settings.FromJson never throws). |
| method | `public static void StoreSettings(Settings settings)` | 64 |  |
| property | `public static bool IntroSeen` | 76 |  |
| property | `public static bool TutorialDone` | 83 | The tutorial was played to its end (or skipped). See TutorialScript.ShouldRun. |

## Uses
[[ContentDb]], [[Journal]], [[Profile]], [[ProfileData]], [[Settings]]

## Used by
[[AtlasBuilder]], [[GameFlow]], [[TutorialRules]], [[ViewCapture]]

## Tested by
[[PauseTests]], [[SaveTests]], [[SettingsHapticsTests]], [[TutorialFlowTests]]
