---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/TutorialStep.cs
tags: [code/game, kind/class]
aliases: []
---

# TutorialStep

`public sealed class TutorialStep` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/TutorialStep.cs:10-31`

> One tutorial step, a child of the Tutorial object. Holds the text key, the dim and lock options, and its areas (child objects). The step id is the object's name (e.g. "1.4", "5.4i").

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public string textKey =` | 13 |  |
| field | `public Vector2 boxPos` | 18 |  |
| method | `public List<TutorialArea> Areas()` | 21 | The areas in child order; the arrow sits over the last one. |

## Uses
[[Tooltip]], [[TutorialArea]]

## Used by
[[TutorialAreaEditor]], [[TutorialController]], [[TutorialStepsMigration]]

## Tested by
[[TutorialAreaTests]]
