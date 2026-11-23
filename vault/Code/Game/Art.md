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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Art.cs:6-17`

> The art library the running game uses. GameFlow sets it from its serialized field; the Game tests set it in GameTestSetup.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public static ArtLibrary Lib` | 9 | The library; null until GameFlow.Awake or a test/editor setup assigns it. |
| field | `public static Palette Palette` | 12 | The colours files share (Assets/Data/Palette.asset); null until GameFlow.Awake or a test/editor setup assigns it. |
| method | `public static void ResetStatics()` | 16 | Forgets the library and the palette before the first scene (domain reload off). |

## Uses
[[ArtLibrary]], [[Palette]]

## Used by
[[BenchView]], [[CardView]], [[CatalystChoiceView]], [[EditorGlobals]], [[EventView]], [[Fonts]], [[Fx]], [[GameFlow]], [[GoalToast]], [[GoalsView]], [[HudView]], [[JournalView]], [[Label]], [[LinkView]], [[Loop]], [[RarityStars]], [[ReactionCutscene]], [[SimpleCutscene]], [[SparkParticles]], [[Sparks]], [[Spawn]], [[TileView]], [[TitleView]], [[Tooltip]], [[UiButton]], [[WindowView]], [[Wording]]

## Tested by
[[HotPathTests]], [[SparksTests]], [[StaticResetTests]]
