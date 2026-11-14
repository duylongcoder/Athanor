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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs:23-190`

> Three parchment cards, pick one: a catalyst after a depth is cleared, or an aspect at the start of a run and of depths 3 and 5. An aspect's card shows the tile it would land on.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Build()` | 43 |  |
| method | `public IEnumerator Choose(IReadOnlyList<CatalystData> offer, Action<string> done, float autoAfter = -1)` | 79 | The catalysts offered after a depth. |
| method | `public IEnumerator Choose(IReadOnlyList<AspectInstance> offer, Action<string> done, float autoAfter = -1)` | 92 | The aspects offered at the start of a run and of depths 3 and 5. |
| method | `public IEnumerator Choose(string title, string line, IReadOnlyList<ChoiceCard> offer, Action<string> done, float autoAfter = -1)` | 106 | Shows the cards and waits for a pick. autoAfter >= 0 takes the first card after that many seconds (scripted play). |

## Uses
[[Align]], [[AspectEffects]], [[AspectInstance]], [[Bench]], [[ChoiceCard]], [[FontKind]], [[Label]], [[Make]], [[Motion]], [[Px]], [[Rarities]], [[RunState]], [[ScreenFx]], [[Sparks]], [[Wording]]

## Used by
[[GameFlow]]
