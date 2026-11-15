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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/Tooltip.cs:9-62`

> A small panel with a title and two lines of text that appears beside whatever the pointer rests on. A hand card's tip adds a row under the name with its stars and tier word, and the panel grows by that row (SPEC 23).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Build(Transform parent)` | 17 |  |
| method | `public void Show(string heading, string text, Vector2 anchor, float clearance = 20, string rarity = null)` | 35 | Shows the tip above the anchor (below it when there is no room), kept on screen. A rarity adds the tier row. |
| method | `public void Hide()` | 61 |  |

## Uses
[[Align]], [[FontKind]], [[Label]], [[Make]], [[Px]], [[RarityStars]], [[Wording]]

## Used by
[[BenchView]]
