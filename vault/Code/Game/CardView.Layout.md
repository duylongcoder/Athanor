---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Bench/CardView.cs
tags: [code/game, kind/struct]
aliases: []
---

# CardView.Layout

`public struct CardView.Layout`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/CardView.cs:22-32` · Nested in [[CardView]]

> Offsets and speeds of the card, stored in Card.prefab.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float nameOneLine, nameTwoLines` | 25 | The name's height from the card's middle: a one-line name, and the first of two lines. |
| field | `public float pictureSlideMax` | 27 | The icon slides left no further than this (it stays inside its window). |
| field | `public float dealDrop` | 29 | A dealt card starts this far below its place. |
| field | `public float selectedLift, liftedLift, liftSpeed, bobAmp, bobSpeed` | 31 | How far a selected and a pointed-at card rise, how fast, and the selected one's bob (amplitude, speed). |
