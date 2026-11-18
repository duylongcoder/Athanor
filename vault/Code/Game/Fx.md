---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Fx/Fx.cs
tags: [code/game, kind/class]
aliases: []
---

# Fx

`public static class Fx`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Fx/Fx.cs:6-24`

> Spawns exported pixelfx effects by name.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static Flipbook Spawn(string name, Vector2 pos, Transform parent = null, int sortingOrder = 50)` | 9 | Plays the named effect once at a world position. Returns null when that effect has not been exported. |

## Uses
[[Art]], [[Flipbook]]

## Used by
[[GameFlow]], [[ReactionCutscene]]

## Tested by
[[Phase3Tests]]
