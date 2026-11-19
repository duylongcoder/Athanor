---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Haptics.cs
tags: [code/game, kind/class]
aliases: []
---

# Haptics

`public static class Haptics`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Haptics.cs:11-56`

> Short vibration pulses for rare, meaningful moments (never clicks or Ignite). Only where the browser can vibrate (Android Chrome); Supported is false on desktop, iOS and in the editor, and the Settings row is hidden then.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int Discovery = 20, DepthCleared = 40, RunEnded = 60` | 13 |  |
| field | `public static bool Enabled = true` | 16 | The player's setting. |
| field | `public static bool? ForceSupported` | 18 | Tests set this to pretend a device that can vibrate; null means ask the platform. |
| field | `public static Action<int> Sink` | 20 | Tests read pulses here instead of the browser. |
| method | `public static void ResetStatics()` | 29 | Back to the declared defaults before the first scene (domain reload off). |
| property | `public static bool Supported` | 31 |  |
| method | `public static int Plan(bool enabled, bool supported, int ms)` | 45 | How long to vibrate: 0 unless the player wants it, the device can and the pulse has a length. |
| method | `public static void Pulse(int ms)` | 47 |  |

## Used by
[[GameFlow]], [[IgniteSequence]], [[PauseMenu]], [[TitleView]]

## Tested by
[[PauseTests]], [[SettingsHapticsTests]], [[StaticResetTests]]
