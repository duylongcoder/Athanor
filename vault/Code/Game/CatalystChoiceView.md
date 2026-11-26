---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs
tags: [code/game, kind/class]
aliases: []
---

# CatalystChoiceView

`public sealed class CatalystChoiceView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs:23-211`

> Three parchment cards, pick one: a catalyst after a depth is cleared, or an aspect at the start of a run and of depths 3 and 5. An aspect's card shows the tile it would land on.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string OpenHook =` | 32 | The development-build log line once the dealt cards take a click (tools/web/webtest.mjs and descents.mjs click a card after it). Not "[choice] ...": descents.mjs counts that one from GameManager.Descend. |
| property | `public struct CardRow` | 37 | One card's size, the gap between cards and the top of the row (art px), stored in CatalystChoice.prefab. |
| property | `public struct Timing` | 41 | The chooser's motion: the deal (base and per card, flight, stagger, the shade's fade divisor), the exit (length, the chosen card's hop as a share of it) and the flash on a pick. |
| property | `public struct Layout` | 44 | The chooser's distances (px) and looks: the shade's alpha, how far below home cards start and fall to, the chosen card's hop, the hover lift and its speed, kicker colours, the pick burst. |
| method | `public Label CardName(int i)` | 71 | The name label of card i. |
| method | `public RarityStars CornerStars(int i)` | 73 | The star row in the top-left corner of card i. |
| method | `public void Bind(FxRefs effects)` | 78 | Hooks the clicks. GameManager calls it once at start; a test calls it after instantiating the prefab. |
| method | `public IEnumerator Choose(IReadOnlyList<CatalystData> offer, Action<string> done, float autoAfter = -1)` | 97 | The catalysts offered after a depth. |
| method | `public IEnumerator Choose(IReadOnlyList<AspectInstance> offer, Action<string> done, float autoAfter = -1)` | 109 | The aspects offered at the start of a run and of depths 3 and 5. |
| method | `public IEnumerator Choose(string title, string line, IReadOnlyList<ChoiceCard> offer, Action<string> done, float autoAfter = -1)` | 123 | Shows the cards and waits for a pick. autoAfter >= 0 takes the first card after that many seconds (scripted play). |

## Uses
[[Art]], [[AspectEffects]], [[AspectInstance]], [[Bench]], [[BurstSpec]], [[ChoiceCard]], [[Clickable]], [[Dev]], [[FxRefs]], [[Label]], [[Loc]], [[Motion]], [[Palette]], [[Px]], [[Rarities]], [[RarityStars]], [[Sparks]], [[Wording]]

## Used by
[[GameManager]], [[GoldenShots]]

## Tested by
[[ChoiceHookTests]], [[LayoutPinTests]], [[PrefabBakeTests]]
