---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs
tags: [code/game, kind/class]
aliases: []
---

# TutorialArea

`public sealed class TutorialArea` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs:37-50`

> One lit window of a step. Fixed: the object's world position is the centre and size (pixels) its size. ScriptTile and View follow the game at run time. clicksPass lets clicks in the window reach the game.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public AreaKind kind = AreaKind.Fixed` | 39 |  |
| method | `public Rect FixedArtRect()` | 49 | Fixed areas only: the art-pixel rect from the transform and size. |

## Uses
[[AreaKind]], [[Bench]], [[ScriptTrial]], [[Tooltip]], [[TutorialRect]], [[ViewRef]]

## Used by
[[TutorialAreaEditor]], [[TutorialController]], [[TutorialStep]], [[TutorialStepsMigration]]

## Tested by
[[TutorialAreaTests]]
