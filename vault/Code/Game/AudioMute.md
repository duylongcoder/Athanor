---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/AudioMute.cs
tags: [code/game, kind/class]
aliases: []
---

# AudioMute

`public static class AudioMute`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/AudioMute.cs:11-33`

> Silences audio while the browser tab is hidden, the app is paused or the window has lost focus. No sound ships in Phase 5, so this is a hook: it sets AudioListener.pause and is audible once sound lands.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public static Action<bool> Sink` | 15 | Tests read the result here instead of AudioListener. |
| property | `public static bool Muted` | 16 |  |
| method | `public static bool ShouldMute(bool tabHidden, bool paused, bool hasFocus)` | 18 |  |
| method | `public static void SetHidden(bool v)` | 20 |  |
| method | `public static void SetPaused(bool v)` | 21 |  |
| method | `public static void SetFocus(bool v)` | 22 |  |
| method | `public static void ResetStatics()` | 25 | Unmuted, no sink, before the first scene (domain reload off). |
| method | `public static void Reset()` | 26 |  |

## Used by
[[AudioMuteHook]]

## Tested by
[[SettingsHapticsTests]], [[StaticResetTests]]
