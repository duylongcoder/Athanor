---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/TitleView.cs
tags: [code/game, kind/class]
aliases: []
---

# TitleView

`public sealed class TitleView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/TitleView.cs:8-278`

> The title, shown over the candlelit workshop.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public bool BeginPressed` | 11 |  |
| property | `public bool HowToPressed` | 13 | How to play or Watch intro was pressed (GameManager reads them while the title waits). |
| property | `public bool IntroPressed` | 14 |  |
| method | `public void ClearBegin()` | 16 | Forgets a Begin that was answered by the skip question. |
| method | `public void RequestBegin()` | 18 | Presses Begin as a click would (PlayMode tests). |
| field | `public event Action JournalClicked, GoalsClicked, ResetConfirmed, SettingsClosed` | 19 |  |
| property | `public bool Asking` | 21 | The Settings panel or the "erase progress?" question is up: the buttons under it do nothing. |
| method | `public void Bind(Settings playerSettings, FxRefs effects)` | 74 | Takes the player's settings (the title edits them and raises SettingsClosed when the panel closes) and hooks every click. GameManager calls it once at start. |
| method | `public void AskSkip(Action<bool> answer)` | 140 | Asks "Skip the tutorial?"; answer(true) = play it, answer(false) = skip. |
| property | `public struct PanelBox` | 161 | The panel's top-left corner and the value cells' left edge, width and height (art px), stored in Title.prefab. |
| method | `public void Open(int known, int total, int goalsDone, int goals)` | 244 |  |
| method | `public void Progress(int known, int total, int goalsDone, int goals)` | 255 | The line under the buttons: how much of the journal and of the goals is done. |
| method | `public void Close()` | 262 |  |

## Uses
[[Anim]], [[Art]], [[Clickable]], [[Dev]], [[FxRefs]], [[Haptics]], [[Label]], [[Loc]], [[Palette]], [[Px]], [[Settings]], [[Sparks]], [[UiButton]]

## Used by
[[GameManager]], [[GoldenShots]]

## Tested by
[[LayoutPinTests]], [[PrefabBakeTests]]
