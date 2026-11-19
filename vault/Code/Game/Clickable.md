---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Clickable.cs
tags: [code/game, kind/class]
aliases: []
---

# Clickable

`public sealed class Clickable` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Clickable.cs:9-27`

> A rectangle around this object that the pointer can hover and click. The highest order under the pointer wins. Holes (world-space rectangles) are cut out of it: the pointer there is not on this object, so it reaches what is underneath.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Vector2 size, offset` | 11 |  |
| field | `public int order` | 12 |  |
| field | `public readonly List<Rect> holes = new List<Rect>()` | 14 | World-space rectangles that are not part of this one (the tutorial's lit windows). Empty: a solid rectangle. |
| field | `public Action onClick, onEnter, onExit, onDown, onUp` | 15 |  |
| method | `public bool Contains(Vector2 world)` | 17 |  |

## Uses
[[PointerInput]]

## Used by
[[Card]], [[CatalystChoiceView]], [[EventView]], [[GoalToast]], [[GoalsView]], [[GoldenShots]], [[HudView]], [[JournalCell]], [[JournalView]], [[PauseMenu]], [[PointerInput]], [[Row]], [[RunOverView]], [[Spawn]], [[TitleView]], [[TutorialController]], [[UiButton]]

## Tested by
[[PauseTests]], [[PrefabBakeTests]], [[SceneBakeTests]]
