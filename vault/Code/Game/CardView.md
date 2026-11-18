---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Bench/CardView.cs
tags: [code/game, kind/class]
aliases: []
---

# CardView

`public sealed class CardView` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/CardView.cs:7-111`

> A starting material in the hand, drawn as a parchment card.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int W = 74, H = 70` | 9 |  |
| field | `public int handIndex` | 10 |  |
| field | `public string id` | 11 |  |
| field | `public Vector3 home` | 12 |  |
| field | `public bool Lifted, Selected, Dimmed` | 13 |  |
| method | `public void Build(MaterialData m, Transform parent, int order)` | 24 |  |
| method | `public void Fan(float visible, int front)` | 55 | In a hand too wide to lay side by side the next card covers this one's right. visible is the width left showing: the name and the icon move to the middle of it. While the card is pointed at or picked it comes to the front (order front) and reads as a whole card again. |
| method | `public void DealIn(float wait)` | 87 | Starts below the screen and slides up after a short stagger. |
| method | `public bool Contains(Vector2 world)` | 89 |  |

## Uses
[[Align]], [[Art]], [[FontKind]], [[Kind]], [[KindColors]], [[Label]], [[Loc]], [[Make]], [[Px]], [[RarityStars]]

## Used by
[[BenchView]]
