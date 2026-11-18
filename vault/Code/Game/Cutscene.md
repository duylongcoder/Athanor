---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/Cutscene.cs
tags: [code/game, kind/class]
aliases: []
---

# Cutscene

`public abstract class Cutscene` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/Cutscene.cs:7-23`

> A sequence that plays to its end unless the player clicks to skip ahead.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `protected bool Skipped` | 9 |  |
| property | `protected bool Playing` | 10 |  |
| method | `public abstract IEnumerator Play()` | 12 |  |
| method | `public void RequestSkip()` | 14 |  |
| method | `protected void Begin()` | 17 | Call at the start of Play and whenever a skip has been dealt with. |
| method | `protected void ClearSkip()` | 18 |  |
| method | `protected void End()` | 19 |  |
| method | `protected virtual void OnEnable()` | 21 |  |
| method | `protected virtual void OnDisable()` | 22 |  |

## Uses
[[PointerInput]]

## Used by
[[ReactionCutscene]], [[SimpleCutscene]]
