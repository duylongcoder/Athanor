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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs:23-200`

> Three parchment cards, pick one: a catalyst after a depth is cleared, or an aspect at the start of a run and of depths 3 and 5. An aspect's card shows the tile it would land on.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string OpenHook =` | 32 | The development-build log line once the dealt cards take a click (tools/web/webtest.mjs and descents.mjs click a card after it). Not "[choice] ...": descents.mjs counts that one from GameFlow.Descend. |
| method | `public Label CardName(int i)` | 60 | The name label of card i. |
| method | `public RarityStars CornerStars(int i)` | 62 | The star row in the top-left corner of card i. |
| method | `public void Bind(FxRefs effects)` | 67 | Hooks the clicks. GameFlow calls it once at start; a test calls it after instantiating the prefab. |
| method | `public IEnumerator Choose(IReadOnlyList<CatalystData> offer, Action<string> done, float autoAfter = -1)` | 86 | The catalysts offered after a depth. |
| method | `public IEnumerator Choose(IReadOnlyList<AspectInstance> offer, Action<string> done, float autoAfter = -1)` | 98 | The aspects offered at the start of a run and of depths 3 and 5. |
| method | `public IEnumerator Choose(string title, string line, IReadOnlyList<ChoiceCard> offer, Action<string> done, float autoAfter = -1)` | 112 | Shows the cards and waits for a pick. autoAfter >= 0 takes the first card after that many seconds (scripted play). |

## Uses
[[Art]], [[AspectEffects]], [[AspectInstance]], [[Bench]], [[ChoiceCard]], [[Clickable]], [[Dev]], [[FxRefs]], [[Label]], [[Loc]], [[Motion]], [[Palette]], [[Px]], [[Rarities]], [[RarityStars]], [[Sparks]], [[Wording]]

## Used by
[[GameFlow]], [[GoldenShots]]

## Tested by
[[ChoiceHookTests]], [[PrefabBakeTests]]
