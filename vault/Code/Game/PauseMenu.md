---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/PauseMenu.cs
tags: [code/game, kind/class]
aliases: []
---

# PauseMenu

`public sealed class PauseMenu` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/PauseMenu.cs:13-131`

> The in-run pause: Resume, Vibration (only where the device can), How to play (greyed: title only) and Quit to title with a confirm. Time.timeScale is 0 while it is open; it is put back on every way out (Resume, Esc, a click outside, Quit, disable, destroy). Its own animation uses nothing that reads scaled time, and it never touches AudioListener (AudioMute owns that).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action QuitConfirmed` | 17 | The player confirmed "Quit this run?". The owner discards the run; the menu has already restored time. |
| field | `public event Action VibrationChanged` | 19 | Raised when the Vibration button changes the setting, so the owner can save it. |
| field | `public event Action Opened, Closed` | 21 | Raised when the menu opens and when it closes, so the owner can lock and unlock the bench under it. |
| field | `public Func<bool> CanOpen` | 23 | Whether a pause may open now (the bench is the player's, no cutscene; the tutorial can be paused and left). Null means yes. |
| property | `public bool IsOpen` | 25 |  |
| property | `public bool Confirming` | 26 |  |
| method | `public static bool Allowed(FlowState state, bool benchInput, bool inCutscene)` | 36 | The state of a pause: may it open, given the flow state and what is playing. |
| method | `public void Bind(Settings playerSettings)` | 40 | Takes the player's settings (Vibration writes to them) and hooks the clicks. GameFlow calls it once at start. |
| method | `public bool Open()` | 67 | Opens the menu and stops time. False when a pause is not allowed now or one is already open. |
| method | `public void Close()` | 78 | Closes whatever is up and puts time back. |
| method | `public void Toggle()` | 88 | Esc: opens; closes the menu; or backs out of the quit question to the menu. |

## Uses
[[Bench]], [[Clickable]], [[FlowState]], [[Haptics]], [[Label]], [[Loc]], [[Px]], [[Settings]], [[UiButton]]

## Used by
[[GameFlow]]

## Tested by
[[PauseTests]], [[PrefabBakeTests]]
