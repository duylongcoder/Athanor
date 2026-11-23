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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/PauseMenu.cs:13-130`

> The in-run pause: Resume, Vibration (only where the device can), How to play (greyed: title only) and Quit to title with a confirm. Time.timeScale is 0 while it is open; it is put back on every way out (Resume, Esc, a click outside, Quit, disable, destroy). Its own animation uses nothing that reads scaled time, and it never touches AudioListener (AudioMute owns that).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public event Action QuitConfirmed` | 16 | The player confirmed "Quit this run?". The owner discards the run; the menu has already restored time. |
| field | `public event Action VibrationChanged` | 18 | Raised when the Vibration button changes the setting, so the owner can save it. |
| field | `public event Action Opened, Closed` | 20 | Raised when the menu opens and when it closes, so the owner can lock and unlock the bench under it. |
| field | `public Func<bool> CanOpen` | 22 | Whether a pause may open now (the bench is the player's, no cutscene; the tutorial can be paused and left). Null means yes. |
| property | `public bool IsOpen` | 24 |  |
| property | `public bool Confirming` | 25 |  |
| property | `public struct Layout` | 29 | Where the lower buttons sit (art px, y down): the dialog's top edge, the buttons' left edge and tops below the dialog's top, the hint's centre and top, and how far they move up without Vibration. |
| method | `public void Bind(Settings playerSettings)` | 39 | Takes the player's settings (Vibration writes to them) and hooks the clicks. GameFlow calls it once at start. |
| method | `public bool Open()` | 66 | Opens the menu and stops time. False when a pause is not allowed now or one is already open. |
| method | `public void Close()` | 77 | Closes whatever is up and puts time back. |
| method | `public void Toggle()` | 87 | Esc: opens; closes the menu; or backs out of the quit question to the menu. |

## Uses
[[Clickable]], [[Haptics]], [[Label]], [[Loc]], [[Px]], [[Settings]], [[UiButton]]

## Used by
[[GameFlow]], [[GoldenShots]]

## Tested by
[[LayoutPinTests]], [[PauseTests]], [[PrefabBakeTests]]
