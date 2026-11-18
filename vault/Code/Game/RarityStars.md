---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/RarityStars.cs
tags: [code/game, kind/class]
aliases: []
---

# RarityStars

`public sealed class RarityStars`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/RarityStars.cs:10-98`

> The rarity glyph (SPEC changes 21-24): up to four 7x7 stars on an 8 px pitch, tinted, each with a dark outline made of four offset dark copies. The root sits at the top-left of the row. Built once and reused; showing a rarity allocates nothing.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const int Size = 7, Pitch = 8, Max = 4` | 12 |  |
| property | `public Transform Root` | 20 | Where the row's top-left corner is (world position; the row hangs to the right and down). |
| ctor | `public RarityStars(Transform parent, int order)` | 22 |  |
| method | `public static int Count(string rarity)` | 58 | 1 Common, 2 Uncommon, 3 Rare, 4 Legendary. |
| method | `public static int WidthOf(int n)` | 61 | Width of n stars in pixels. |
| method | `public void Show(string rarity)` | 64 | The stars of a rarity in its colour. |
| method | `public void Show(int n, Color color)` | 67 | n stars (0 hides the row) in a colour. |
| method | `public void SetAlpha(float a)` | 74 | Fades the row with its card. |
| method | `public void SetOrder(int order)` | 81 | Outline at order, stars above it. |
| method | `public void SetActive(bool on)` | 87 |  |

## Uses
[[Art]], [[Make]], [[Px]], [[Rarities]], [[Wording]]

## Used by
[[Card]], [[CardView]], [[CatalystChoiceView]], [[JournalView]], [[Tooltip]]
