---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Palette.cs
tags: [code/game, kind/class]
aliases: []
---

# Palette

`public sealed class Palette` : `ScriptableObject`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Palette.cs:18-41`

> The colours two or more files share, by role (Assets/Data/Palette.asset). A colour only one file uses stays on that file's view. GameFlow publishes it as Art.Palette.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public RarityColors rarityText` | 22 | Rarity word and stars on a dark panel. |
| field | `public RarityColors rarityTone` | 24 | Rarity word on parchment, and the catalyst strip. |
| field | `public Color gold` | 27 |  |
| field | `public Color anchor, warm, echo, attuned, seed, fallow, heavy, cold, volatileTile, kiln, pivot` | 28 |  |
| field | `public Color defaultGlow` | 32 | Glow of a material that has none. |
| field | `public Color ink, faded, teal` | 34 | Ink, faded ink and teal of the parchment pages (Journal, Goals, choice cards). |
| field | `public Color edge` | 36 | Resting border of the pause button and the title's cells. |
| field | `public Color pipOn, pipOff` | 38 | Reactivity pips and choice tiles: lit and unlit. |
| field | `public Color buttonText` | 40 | Label of an enabled button. |

## Uses
[[RarityColors]]

## Used by
[[Art]], [[CatalystChoiceView]], [[EditorGlobals]], [[GameFlow]], [[GoalsView]], [[HudView]], [[JournalView]], [[ReactionCutscene]], [[TileView]], [[TitleView]], [[UiButton]], [[Wording]]

## Tested by
[[LayoutPinTests]], [[StaticResetTests]]
