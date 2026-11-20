---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/JournalView.cs
tags: [code/game, kind/struct]
aliases: []
---

# JournalView.Layout

`public struct JournalView.Layout`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/JournalView.cs:22-30` · Nested in [[JournalView]]

> The numbers of the book that are not its grid (art px, y down), stored in Journal.prefab.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Rect indexRect, recipesRect, hintsRect` | 25 | What the tutorial lights: the page's reactivity line and pips, its recipe lines, the hints under the grid. |
| field | `public float titleTop, titleTopLong` | 27 | The title's top on the page for a short and a long name, the length that counts as long, and its two sizes. |
| field | `public Color hint, unknown` | 29 | Hint ink, and the tint of a silhouette (a material not found yet). |
