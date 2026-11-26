---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Managers/Singleton.cs
tags: [code/game, kind/class]
aliases: []
---

# Singletons

`public static class Singletons`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Managers/Singleton.cs:11-23`

> Forgets every manager instance when Play starts. Domain reload is off (EditorSettings), so statics survive Stop and Play; without this a second session would find the destroyed managers of the first.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `internal static void Track(Action clear)` | 15 |  |
| method | `public static void ResetStatics()` | 18 |  |

## Used by
[[Singleton]]

## Tested by
[[SingletonTests]]
