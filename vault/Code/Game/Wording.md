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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Wording.cs:9-203`

> The words and colours the Phase 3 screens share: rarities, aspects, event effects and goal rewards.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static Color RarityColor(string rarity)` | 11 |  |
| method | `public static Color RarityTone(string rarity)` | 23 | The dark tone of a rarity: the catalyst strip, the tooltip's tier word and the Journal page's tier word (SPEC 22). |
| method | `public static string RarityWord(string rarity)` | 35 | The tier word as the strip, tooltip and Journal show it: "RARE" / "HIẾM". |
| method | `public static Color AspectColor(string effect)` | 38 | The colour of an aspect's marker on its tile and of its link. |
| field | `public static readonly Color Warm = Px.Hex("#ffa63a")` | 58 |  |
| method | `public static string AspectRule(AspectInstance a, bool cost = true)` | 61 | An aspect's rule, with the kind an attuned tile drew written in. cost: false leaves the trade-off's price out. |
| method | `public static string AspectCost(AspectInstance a)` | 83 | "COST: this tile is shut for the run.", or null for an aspect that costs nothing. |
| method | `public static string Effect(EffectData fx)` | 91 | A few words for one event effect, as it is shown under an option before it is taken. |
| method | `public static string OptionSummary(EventOption o)` | 121 | What an option costs and what it may give: "give up a discovery: +1 trial", "50% +1 trial / 50% lose a card". |
| method | `public static string GoalText(GoalData g)` | 140 | A goal's text in this language. |
| method | `public static string OptionLabel(EventData e, EventOption o)` | 143 | The label of an event option in this language. |
| method | `public static string OutcomeText(EventData e, EventOption o, string english)` | 147 | What came of an option, in this language: the outcome the core drew (English text) found among the option's outcomes. |
| method | `public static string Note(string english)` | 175 | A note the core wrote in English ("Found Ember Coal"), in this language. The core still writes English (handoff: rules-engineer, notes as key + args); until then the sentence is matched back to its note.* key. No match: unchanged. |
| method | `public static string RewardLine(GoalData g)` | 187 | "New Rare catalyst": the kind and rarity of a goal's reward, never the item. Short, so it ends before the progress bar. |
| method | `public static string ItemName(ContentDb db, string id)` | 191 | The name of a starting material or catalyst won from a goal. |
| method | `public static string ItemSprite(ContentDb db, string id)` | 200 | The sprite of an item won from a goal: a material's icon, or a catalyst's. |
| method | `public static string Number(long n)` | 202 |  |

## Uses
[[AspectEffects]], [[AspectInstance]], [[ContentDb]], [[Kind]], [[Lang]], [[Loc]], [[Ops]], [[Px]], [[Rarities]]

## Used by
[[BenchView]], [[CatalystChoiceView]], [[EventView]], [[GoalToast]], [[GoalsView]], [[JournalView]], [[RarityStars]], [[Tooltip]]

## Tested by
[[LocTests]]
