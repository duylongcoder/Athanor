---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Make.cs
tags: [code/game, kind/class]
aliases: []
---

# Make

`public static class Make`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Make.cs:12-157`

> Factories for the sprites, text and lights the views are built from.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public static ArtLibrary Lib` | 14 |  |
| method | `public static Transform Root(string name, Transform parent = null)` | 16 |  |
| method | `public static SpriteRenderer Sprite(Transform parent, string sprite, Vector3 worldPos, int order, Material material = null)` | 24 | A sprite centred on a world position. |
| method | `public static SpriteRenderer SpriteAt(Transform parent, string sprite, float x, float y, int order, Material material = null)` | 37 | A sprite placed by the art-space position of its top-left corner. |
| method | `public static SpriteRenderer Rect(Transform parent, float x, float y, float w, float h, Color color, int order)` | 44 | A filled rectangle in art space. |
| method | `public static SpriteRenderer Glow(Transform parent, Vector3 worldPos, float radius, Color color, int order)` | 54 | An additive soft glow of the given radius. |
| method | `public static Label Text(Transform parent, string text, float x, float y, FontKind font, int size, Color color, int order, Align align = Align.Left, C` | 63 | Text whose top edge is at art-space y, anchored at x by its alignment. width > 0 wraps. |
| method | `public static Label TextKey(Transform parent, string key, float x, float y, FontKind font, int size, Color color, int order, Align align = Align.Left,` | 82 | Text for a Lang key: it rewrites itself, and swaps its font, when the language changes. |
| method | `public static TMP_FontAsset FontFor(FontKind kind)` | 91 | The font of a kind in the language showing. Vietnamese uses SVN-Coder's Crux for all four kinds, never for the logo. |
| method | `public static float SizeFor(FontKind kind, int size)` | 99 | The point size for a kind: Tiny 8 is Crux 16 in Vietnamese (the marks need the height); the rest keep their size. |
| method | `public static int HalfLeading(UnityEngine.TextCore.FaceInfo face, float size)` | 106 | The half-leading of a font at a point size, in whole pixels (rounded up): how far below the top of a line box as tall as the size (the mockup's CSS, line-height = size) the font's ascent line sits. TMP puts the ascent line at the box top instead. Crux (ascent 7, descent -2 at 16): 11 px at 48, 7 at 32. |
| method | `public static Light2D Light(Transform parent, Vector3 worldPos, float radius, Color color, float intensity, float squash = 1)` | 135 | A round 2D light. squash below 1 flattens it into an ellipse lying on a surface. |
| method | `public static Clickable Click(GameObject go, Vector2 size, int order, System.Action onClick = null)` | 151 |  |

## Uses
[[Align]], [[ArtLibrary]], [[Clickable]], [[FontKind]], [[Label]], [[Loc]], [[Px]]

## Used by
[[BenchView]], [[CardView]], [[CatalystChoiceView]], [[EventView]], [[Fx]], [[GameFlow]], [[GoalToast]], [[GoalsView]], [[HudView]], [[JournalView]], [[Label]], [[LinkView]], [[Loop]], [[PauseMenu]], [[RarityStars]], [[ReactionCutscene]], [[RunOverView]], [[SceneBuilder]], [[ScreenFx]], [[SimpleCutscene]], [[SparkParticles]], [[Sparks]], [[TileView]], [[TitleView]], [[Tooltip]], [[TutorialController]], [[UiButton]], [[WindowView]]

## Tested by
[[ChoiceHookTests]], [[LabelLineBoxTests]], [[LangTests]], [[PauseTests]], [[RunOverDepthTests]], [[SparksTests]]
