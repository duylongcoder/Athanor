---
generated: true
type: code
layer: Game
kind: struct
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Bench/HudView.cs
tags: [code/game, kind/struct]
aliases: []
---

# HudView.Layout

`public struct HudView.Layout`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Bench/HudView.cs:29-63` · Nested in [[HudView]]

> The HUD's layout numbers (art pixels, y down), stored in Hud.prefab.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int rowEn, rowVi` | 32 | The pitch of the small note rows, English and Vietnamese (Crux at 16 needs 12 px, Pixel Operator 8 keeps 10). |
| field | `public int noteCharsEn, noteCharsVi` | 34 | Characters per line of the sputter note, English and Vietnamese. |
| field | `public Rect redrawRect` | 36 | The Redraw button in art space: under the right panel, right below the redraws-left counter. |
| field | `public Rect pauseRect` | 38 | The pause button's picture (28x20 at 12,152; its hit box is 44x44 at 4,140). SPEC item 11 deviation: the aspects shelf band is full, shown only while a pause can open. |
| field | `public float redrawLabelY` | 40 | How far below the Redraw box's top the label is placed, so the word and its shadow sit centred in the fill. |
| field | `public float pressedDy` | 42 | How far the Ignite and Redraw labels sink while pressed. |
| field | `public float catalystPitch, catalystSpread` | 44 | Catalyst sockets: pitch while few enough fit side by side (that many), the width they spread over when more overlap, the first socket's top-left, its size, the icon's offset in it and its size. |
| field | `public int catalystSideBySide` | 45 |  |
| field | `public Vector2 socketOrigin, socketSize, iconOffset, iconSize` | 46 |  |
| field | `public Vector2 aspectAnchor, potencyAnchor` | 48 | Where a tooltip about a held aspect points (first one) and the pitch between them; the potency number's tooltip point. |
| field | `public float aspectPitch` | 49 |  |
| field | `public float noteX, noteTopAspects, noteTop` | 51 | The depth notes: the left edge and the top of the first row with and without held aspects. |
| field | `public float potencyX, potencyTightX, potencyY` | 53 | The potency number: left edge (and the tight one that replaces the word), top; the bar's top-left, full width and height. |
| field | `public Vector2 barOrigin` | 54 |  |
| field | `public float barWidth, barHeight` | 55 |  |
| field | `public float tightFrom, tightGap, tightTo` | 57 | A number long enough to run into the target takes the word's place when from + width + gap passes to - target width. |
| field | `public Color pulseColor` | 59 | The Ignite button's glow: colour and alpha (min, swing). |
| field | `public Vector2 pulseAlpha` | 60 |  |
| field | `public Vector3 barTipScale` | 62 | The scale of the bar's bright tip. |
