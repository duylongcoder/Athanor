---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/LayoutData.cs
tags: [code/game, kind/class]
aliases: []
---

# LayoutData

`public class LayoutData`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/LayoutData.cs:11-24`

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int w, h` | 13 |  |
| field | `public TileData[] tiles` | 14 |  |
| field | `public RectData[] placed` | 15 |  |
| field | `public PointData[] candles` | 16 |  |
| method | `public RectData Placed(string name)` | 19 | Where a cropped full-frame layer (such as "workshop_table") sits. |

## Used by
[[BenchView]], [[GameFlow]], [[SceneBuilder]]
