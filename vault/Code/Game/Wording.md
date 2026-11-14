---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Wording.cs
tags: [code/game, kind/class]
aliases: []
---

# Wording

`public static class Wording`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Wording.cs:9-131`

> The words and colours the Phase 3 screens share: rarities, aspects, event effects and goal rewards.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static Color RarityColor(string rarity)` | 11 |  |
| method | `public static Color AspectColor(string effect)` | 23 | The colour of an aspect's marker on its tile and of its link. |
| field | `public static readonly Color Warm = Px.Hex("#ffa63a")` | 43 |  |
| method | `public static string AspectRule(AspectInstance a, bool cost = true)` | 48 | An aspect's rule, with the kind an attuned tile drew written in. cost: false leaves the trade-off's price out. |
| method | `public static string AspectCost(AspectInstance a)` | 57 | "COST: this tile is shut for the run.", or null for an aspect that costs nothing. |
| method | `public static string Effect(EffectData fx)` | 67 | A few words for one event effect, as it is shown under an option before it is taken. |
| method | `public static string OptionSummary(EventOption o)` | 97 | What an option costs and what it may give: "give up a discovery: +1 trial", "50% +1 trial / 50% lose a card". |
| method | `public static string RewardLine(GoalData g)` | 115 | "New Rare catalyst": the kind and rarity of a goal's reward, never the item. Short, so it ends before the progress bar. |
| method | `public static string ItemName(ContentDb db, string id)` | 119 | The name of a starting material or catalyst won from a goal. |
| method | `public static string ItemSprite(ContentDb db, string id)` | 128 | The sprite of an item won from a goal: a material's icon, or a catalyst's. |
| method | `public static string Number(long n)` | 130 |  |

## Uses
[[AspectEffects]], [[AspectInstance]], [[ContentDb]], [[Kind]], [[Ops]], [[Px]], [[Rarities]]

## Used by
[[BenchView]], [[CardView]], [[CatalystChoiceView]], [[EventView]], [[GoalToast]], [[GoalsView]]
