---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Art.cs
tags: [code/game, kind/class]
aliases: []
---

# Art

`public static class Art`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Art.cs:4-8`

> The art library the running game uses. GameFlow sets it from its serialized field; the Game tests set it in GameTestSetup.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public static ArtLibrary Lib` | 7 | The library; null until GameFlow.Awake or a test/editor setup assigns it. |

## Uses
[[ArtLibrary]]

## Used by
[[BenchView]], [[CardView]], [[CatalystChoiceView]], [[DepthTestBuilder]], [[DepthTestEvidence]], [[EditorGlobals]], [[EventView]], [[Fx]], [[GameFlow]], [[GoalToast]], [[GoalsView]], [[HudView]], [[JournalView]], [[Label]], [[LinkView]], [[Loop]], [[MainDepthEvidence]], [[Make]], [[RarityStars]], [[ReactionCutscene]], [[SceneBuilder]], [[ScreenFx]], [[SimpleCutscene]], [[SparkParticles]], [[Sparks]], [[TileView]], [[Tooltip]], [[UiButton]], [[WindowView]]

## Tested by
[[SparksTests]]
