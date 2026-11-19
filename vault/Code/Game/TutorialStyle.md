---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/TutorialSettings.cs
tags: [code/game, kind/class]
aliases: []
---

# TutorialStyle

`public sealed class TutorialStyle`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/TutorialSettings.cs:39-104`

> How the tutorial overlay looks and feels: the text box, the popup lines, the rim and arrow, and their timings.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public int baseOrder = 600` | 43 |  |
| field | `public int popupOrderOffset = 12` | 44 |  |
| field | `public int brokenOrderOffset = 6` | 45 |  |
| field | `public Vector2 boxPos = new Vector2(168, 14)` | 49 |  |
| field | `public float boxWidth = 304` | 50 |  |
| field | `public float boxBorder = 1` | 52 |  |
| field | `public Vector2 textInset = new Vector2(8, 7)` | 54 |  |
| field | `public float textWrapWidth = 288` | 56 |  |
| field | `public float boxPadding = 12` | 58 |  |
| field | `public float readExtra = 12` | 59 |  |
| field | `public float boxMaxHeight = 86` | 61 |  |
| field | `public int linePitch = 12, maxLines = 4` | 63 |  |
| field | `public int linePitchVi = 16, maxLinesVi = 3` | 65 |  |
| field | `public int journalBoxDrop = 248` | 67 |  |
| field | `public Vector2 popupPos = new Vector2(320, 70)` | 71 |  |
| field | `public float popupPitch = 20` | 72 |  |
| field | `public int popupFontSize = 16` | 73 |  |
| field | `public Color popupColor = new Color32(0xf0, 0xdc, 0xc0, 255)` | 74 |  |
| field | `public Color popupTotalColor = new Color32(0xff, 0xf6, 0xb8, 255)` | 76 |  |
| field | `public Color popupShadow = new Color32(0x0c, 0x07, 0x06, 255)` | 77 |  |
| field | `public Color brokenGhost = new Color(0.45f, 0.45f, 0.45f, 0.35f)` | 81 |  |
| field | `public float clickGuard = 0.25f` | 89 |  |
| field | `public float rimSpeed = 5` | 94 |  |
| field | `public float bumpDecay = 3` | 96 |  |
| field | `public float arrowAbove = 10, arrowBelow = 8` | 100 |  |
| field | `public float arrowFlipY = 80` | 102 |  |
| field | `public float arrowBobSpeed = 6, arrowBobAmplitude = 2` | 103 |  |

## Used by
[[TutorialController]]
