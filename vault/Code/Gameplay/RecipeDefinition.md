---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Content/RecipeDefinition.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# RecipeDefinition

`public sealed class RecipeDefinition` : `ScriptableObject`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Content/RecipeDefinition.cs:8-22`

> A combo of 2 to 5 materials (unordered, at most 2 of one) that makes result.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public MaterialDefinition[] items = new MaterialDefinition[0]` | 10 |  |
| field | `public MaterialDefinition result` | 13 |  |
| method | `public RecipeData ToData()` | 16 | The plain data the rules read. |

## Uses
[[MaterialDefinition]], [[Tooltip]]

## Used by
[[ContentDatabase]]

## Tested by
[[ContentDatabaseTests]]
