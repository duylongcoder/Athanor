---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Content/ContentDatabase.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# ContentDatabase

`public sealed class ContentDatabase` : `ScriptableObject`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Content/ContentDatabase.cs:12-42`

> The game's content: every definition asset in the order the rules see it (the order matters: offers, draws and "the order of the file" tie-breaks follow it). ToContentData builds the plain data ContentDb validates.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public GameRules rules` | 14 |  |
| field | `public List<MaterialDefinition> materials = new List<MaterialDefinition>()` | 15 |  |
| field | `public List<RecipeDefinition> recipes = new List<RecipeDefinition>()` | 16 |  |
| field | `public List<CatalystDefinition> catalysts = new List<CatalystDefinition>()` | 17 |  |
| field | `public List<AspectDefinition> aspects = new List<AspectDefinition>()` | 18 |  |
| field | `public List<EventDefinition> events = new List<EventDefinition>()` | 19 |  |
| field | `public List<GoalDefinition> goals = new List<GoalDefinition>()` | 20 |  |
| method | `public ContentData ToContentData()` | 23 | A fresh copy of the content as the rules read it. Throws when rules is not set. |

## Uses
[[AspectDefinition]], [[CatalystDefinition]], [[ContentData]], [[EventDefinition]], [[GameRules]], [[GoalDefinition]], [[MaterialDefinition]], [[RecipeDefinition]]

## Used by
[[ContentAssets]], [[GameFlow]]

## Tested by
[[ContentDatabaseTests]]
