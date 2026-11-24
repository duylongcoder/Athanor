---
generated: true
type: code
layer: Gameplay
kind: struct
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Tutorial.cs
tags: [code/gameplay, kind/struct]
aliases: []
---

# TutorialLay

`public readonly struct TutorialLay`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Tutorial.cs:13-18`

> One material laid on one tile. This material goes on this tile, not just any lit tile: another arrangement of the same materials can break the chain (trial 3 then scores 40 and does not clear the depth) or move the hint to the other material (trial 2).

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public readonly string Id` | 15 |  |
| field | `public readonly int Tile` | 16 |  |
| ctor | `public TutorialLay(string id, int tile)` | 17 |  |

## Used by
[[TutorialScript]], [[TutorialTrial]]
