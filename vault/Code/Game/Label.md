---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Label.cs
tags: [code/game, kind/class]
aliases: []
---

# Label

`public sealed class Label` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Label.cs:11-116`

> A line of pixel text with an optional one-pixel drop shadow. A label made from a Lang key (Make.TextKey) rewrites itself when the language changes; every label swaps its font to the language's.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public TextMeshPro main, shadow` | 13 |  |
| field | `public FontKind kind` | 15 | The kind and the size (English points) it was made with, kept for font swaps. |
| field | `public int size` | 16 |  |
| property | `public int Lines` | 23 | Name-role text: how many lines the name has. Crux sits higher in its box than the English face, so Vietnamese names are nudged down 5 px (two lines) or 1 px (one) to land on the English rows. |
| property | `public bool LineBox` | 32 | Display text placed as the mockup places it, in a line box as tall as its size (the Descent's depth name, SPEC (f)). Crux's ascent line is short for its size, so in Vietnamese the face moves down by Crux's half-leading at the current size (11 px at 48, 7 at 32). English is left alone: Alagard's half-leading is under 2 px and the English name already matches the mock. |
| method | `public void ApplyBias()` | 35 | Moves the face (and its shadow) down by the font's vertical bias; nothing outside Vietnamese Name and line-box text. |
| method | `public void Bind(string textKey, params (string name, object value)[] textArgs)` | 59 | Ties the label to a key: the text is rewritten whenever the language changes. |
| method | `public void SetKey(string textKey, params (string name, object value)[] textArgs)` | 62 | Sets the key and its text together. |
| method | `public void SetSize(int points)` | 65 | Changes the size (in English points); the font follows the language. |
| property | `public string Text` | 93 |  |
| property | `public Color Color` | 99 |  |
| property | `public float Alpha` | 101 |  |
| property | `public int Order` | 110 |  |
| property | `public float Width` | 115 |  |

## Uses
[[Art]], [[FontKind]], [[Loc]], [[Make]]

## Used by
[[BenchView]], [[Card]], [[CardView]], [[CatalystChoiceView]], [[EventView]], [[GoalRow]], [[GoalToast]], [[GoalsView]], [[HudView]], [[JournalView]], [[Make]], [[Motion]], [[PauseMenu]], [[ReactionCutscene]], [[Row]], [[RunOverView]], [[SimpleCutscene]], [[TitleView]], [[Tooltip]], [[TutorialController]], [[UiButton]]

## Tested by
[[LabelLineBoxTests]], [[PrefabBakeTests]], [[RunOverDepthTests]]
