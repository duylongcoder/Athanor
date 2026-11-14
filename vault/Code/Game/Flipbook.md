---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Fx/Flipbook.cs
tags: [code/game, kind/class]
aliases: []
---

# Flipbook

`public sealed class Flipbook` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Fx/Flipbook.cs:8-54`

> Plays a FlipbookSheet on a SpriteRenderer.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public FlipbookSheet sheet` | 10 |  |
| field | `public bool playOnEnable = true, destroyOnEnd` | 11 |  |
| field | `public event Action Finished` | 12 |  |
| property | `public bool IsPlaying` | 17 |  |
| method | `public void Play(FlipbookSheet s = null)` | 22 |  |

## Uses
[[FlipbookSheet]]

## Used by
[[Fx]]
