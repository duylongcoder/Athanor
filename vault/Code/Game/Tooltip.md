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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/Tooltip.cs:10-79`

> A small panel with a title and two lines of text that appears beside whatever the pointer rests on. A hand card's tip adds a row under the name with its stars and tier word, and the panel grows by that row (SPEC 23).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public struct Size` | 14 | The panel's width, its height without the tier row, and the tier row's height (px), stored in the Bench prefab. |
| method | `public void Bind()` | 40 | The panel's 9-slice sprite (the panel stretches only in its middle, so the taller tip keeps the same border). It is made by Sprite.Create, which no prefab can keep, so a tooltip loaded from a prefab makes it here; the owner calls this once after instantiating it. Does nothing when the panel already has it. |
| method | `public void Show(string heading, string text, Vector2 anchor, float clearance = 20, string rarity = null)` | 51 | Shows the tip above the anchor (below it when there is no room), kept on screen. A rarity adds the tier row. |
| method | `public void Hide()` | 78 |  |

## Uses
[[Art]], [[Label]], [[Px]], [[RarityStars]], [[Wording]]

## Used by
[[AspectDefinition]], [[BenchView]], [[CatalystDefinition]], [[ContentManager]], [[EventDefinition]], [[EventEffectDef]], [[EventOutcomeDef]], [[GameRules]], [[GoalDefinition]], [[LocalizationManager]], [[MaterialDefinition]], [[RecipeDefinition]], [[RunManager]], [[TutorialManager]]

## Tested by
[[HotPathTests]], [[LayoutPinTests]], [[PrefabBakeTests]]
