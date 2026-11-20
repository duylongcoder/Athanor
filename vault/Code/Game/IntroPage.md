---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs
tags: [code/game, kind/struct]
aliases: []
---

# IntroPage

`public struct IntroPage`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs:25-33` · Nested in [[SimpleCutscene]]

> The opening story: the fades round it and the holds between its lines, in seconds.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float fadeIn, lineFade, fadeOut` | 28 | Fade in from black; a page's fade out between pages; the fade out to black at the end. |
| field | `public float hold, lastHold, finalHold` | 30 | Hold after a line; after the last line of the last page; after the last line of an earlier page. |
| field | `public float promptHold` | 32 | How long "click to begin" waits for a click. |
