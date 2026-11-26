---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Managers/SaveManager.cs
tags: [code/game, kind/class]
aliases: []
---

# SaveManager

`public sealed class SaveManager` : `Singleton<SaveManager>`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Managers/SaveManager.cs:8-35`

> The player's settings and the small saved flags. Storage itself stays in Save (PlayerPrefs; browser storage on the web).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public Settings Settings` | 10 |  |
| method | `protected override void OnAwake()` | 12 |  |
| method | `public void StoreSettings()` | 18 |  |
| property | `public bool IntroSeen` | 20 |  |
| method | `public void SetIntroSeen(bool seen)` | 21 |  |
| property | `public bool TutorialDone` | 22 |  |
| method | `public void SetTutorialDone(bool done)` | 23 |  |

## Uses
[[Haptics]], [[Save]], [[Settings]], [[Singleton]]

## Used by
[[GameManager]], [[LocalizationManager]], [[ManagerSceneMigration]]

## Tested by
[[ManagerBootTests]]
