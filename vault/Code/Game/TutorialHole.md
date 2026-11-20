---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Cutscene/TutorialHole.cs
tags: [code/game, kind/class]
aliases: []
---

# TutorialHole

`public sealed class TutorialHole` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Cutscene/TutorialHole.cs:10-39`

> One lit window of the tutorial overlay: a SpriteMask that cuts a hole in the dim (the dim draws "visible outside mask"), and the four gold rim pieces round it. TutorialController keeps a few of these in the scene and opens as many as a step needs.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public void Open(Rect r, bool showRim)` | 18 | Lights this rectangle (art space, top-left corner and size) and shows the rim round it when asked. |
| method | `public void Close()` | 30 |  |
| method | `public void SetRimAlpha(float a)` | 33 | The rim's opacity (the pulse); its colour is the one set in the scene. |

## Uses
[[Px]]

## Used by
[[TutorialController]]

## Tested by
[[LayoutPinTests]], [[SceneBakeTests]]
