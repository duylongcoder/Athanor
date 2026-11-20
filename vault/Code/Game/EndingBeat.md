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

# EndingBeat

`public struct EndingBeat`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs:65-71` · Nested in [[SimpleCutscene]]

> The ending: the fades round the story, the hold after a line, the final flash and burst, the banner's place.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float fadeOut, fadeIn, hold, flash, flashSeconds, promptHold` | 67 |  |
| field | `public BurstSpec burst` | 69 | The burst over the prize: where (art y); its colour is multiplied by the prize's glow. |
| field | `public float burstY, bannerY` | 70 |  |

## Uses
[[BurstSpec]]
