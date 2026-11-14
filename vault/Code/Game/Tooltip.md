---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/Tooltip.cs
tags: [code/game, kind/class]
aliases: []
---

# Tooltip

`public sealed class Tooltip` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/Tooltip.cs:6-35`

> A small panel with a title and two lines of text that appears beside whatever the pointer rests on.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Build(Transform parent)` | 11 |  |
| method | `public void Show(string heading, string text, Vector2 anchor, float clearance = 20)` | 23 | Shows the tip above the anchor (below it when there is no room), kept on screen. |
| method | `public void Hide()` | 34 |  |

## Uses
[[Align]], [[FontKind]], [[Label]], [[Make]], [[Px]]

## Used by
[[BenchView]]
