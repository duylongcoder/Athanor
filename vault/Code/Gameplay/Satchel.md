---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Satchel.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# Satchel

`public sealed class Satchel`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Satchel.cs:20-75`

> The player's deck of starting materials: a draw pile, a discard pile, and the cards currently out.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| ctor | `public Satchel(IEnumerable<string> cards, int seed)` | 27 |  |
| property | `public int Total` | 34 |  |
| method | `public List<string> Draw(int n)` | 36 |  |
| method | `public void Lose(string id)` | 53 | A card that is out (in the hand or on the bench) leaves the satchel for good. |
| method | `public void Discard(IEnumerable<string> ids)` | 55 |  |
| method | `public int Available(string id)` | 61 | Copies of a card in the draw and discard piles (not the ones out). |
| method | `public bool Take(string id)` | 67 | Takes a named card out of the draw pile, or else the discard pile, as if it had been drawn (the tutorial's scripted hands). False, and nothing changed, when neither pile has a copy. |

## Uses
[[Rng]]

## Used by
[[RunState]]

## Tested by
[[SatchelTests]], [[TutorialTests]]
