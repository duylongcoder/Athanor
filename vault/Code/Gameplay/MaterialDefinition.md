---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Content/MaterialDefinition.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# MaterialDefinition

`public sealed class MaterialDefinition` : `ScriptableObject`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Content/MaterialDefinition.cs:7-32`

> A material. The asset's file name is its id; the id is what saves store, so never change it after release.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public string id =` | 10 |  |
| field | `public string displayName =` | 12 |  |
| field | `public Kind kind` | 13 |  |
| field | `public Color glow = Color.white` | 18 |  |
| field | `public bool hasGlow = true` | 20 |  |
| field | `public RarityId rarity` | 22 |  |
| field | `public bool locked` | 24 |  |
| method | `public MaterialData ToData()` | 27 | The plain data the rules read. |

## Uses
[[ContentDb]], [[ContentNames]], [[Kind]], [[RarityId]], [[Tooltip]]

## Used by
[[ContentDatabase]], [[GameRules]], [[MaterialDefinitionEditor]], [[RecipeDefinition]], [[TutorialLayDef]], [[TutorialTrialDef]]

## Tested by
[[ContentDatabaseTests]]
