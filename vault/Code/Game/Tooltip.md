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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/Tooltip.cs:10-75`

> A small panel with a title and two lines of text that appears beside whatever the pointer rests on. A hand card's tip adds a row under the name with its stars and tier word, and the panel grows by that row (SPEC 23).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Bind()` | 37 | The panel's 9-slice sprite (the panel stretches only in its middle, so the taller tip keeps the same border). It is made by Sprite.Create, which no prefab can keep, so a tooltip loaded from a prefab makes it here; the owner calls this once after instantiating it. Does nothing when the panel already has it. |
| method | `public void Show(string heading, string text, Vector2 anchor, float clearance = 20, string rarity = null)` | 47 | Shows the tip above the anchor (below it when there is no room), kept on screen. A rarity adds the tier row. |
| method | `public void Hide()` | 74 |  |

## Uses
[[Art]], [[Label]], [[Px]], [[RarityStars]], [[Wording]]

## Used by
[[BenchView]]

## Tested by
[[HotPathTests]], [[LayoutPinTests]], [[PrefabBakeTests]]
