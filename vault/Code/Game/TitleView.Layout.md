---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Ui/TitleView.cs
tags: [code/game, kind/struct]
aliases: []
---

# TitleView.Layout

`public struct TitleView.Layout`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Ui/TitleView.cs:42-54` · Nested in [[TitleView]]

> The title's positions and colours (art px, y down), stored in Title.prefab.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float centerX, logoY, logoRise, outlineSpread` | 45 | The screen's centre line, the logo's rest height and how far it rises from, and how far the four outline copies sit from it. |
| field | `public float progressY, progressYVi, creditY, creditYVi` | 47 | The line under the buttons and the credit line, English and Vietnamese (the 12 px rows leave 2 px at the 360 px edge). |
| field | `public Color resetRest, resetOver, cellRest, cellOver` | 49 | The erase link's colour at rest and pointed at; the settings cells' fill at rest and pointed at, and how far the fill is inset. |
| field | `public float pitch, pitchNoVib, firstRow, firstRowNoVib, dividerX, dividerW, dividerH, dividerY, dividerYNoVib, resetY, resetYNoVib` | 51 | The settings rows: pitch and first row's drop with and without the Vibration row, the lower divider's left, width and height and its drop, the erase link's drop. |
| field | `public float labelX, labelDy, valueDy, valueDx, boxInset, boxSize, fillInset, fillSize` | 53 | Inside a row: label left, label and value drops, value left, check box inset and size, its fill's inset and size. |
