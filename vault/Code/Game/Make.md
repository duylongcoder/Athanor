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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Make.cs:11-123`

> Factories for the sprites, text and lights the views are built from.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public static ArtLibrary Lib` | 13 |  |
| method | `public static Transform Root(string name, Transform parent = null)` | 15 |  |
| method | `public static SpriteRenderer Sprite(Transform parent, string sprite, Vector3 worldPos, int order, Material material = null)` | 23 | A sprite centred on a world position. |
| method | `public static SpriteRenderer SpriteAt(Transform parent, string sprite, float x, float y, int order, Material material = null)` | 36 | A sprite placed by the art-space position of its top-left corner. |
| method | `public static SpriteRenderer Rect(Transform parent, float x, float y, float w, float h, Color color, int order)` | 43 | A filled rectangle in art space. |
| method | `public static SpriteRenderer Glow(Transform parent, Vector3 worldPos, float radius, Color color, int order)` | 53 | An additive soft glow of the given radius. |
| method | `public static Label Text(Transform parent, string text, float x, float y, FontKind font, int size, Color color, int order, Align align = Align.Left, C` | 62 | Text whose top edge is at art-space y, anchored at x by its alignment. width > 0 wraps. |
| method | `public static Light2D Light(Transform parent, Vector3 worldPos, float radius, Color color, float intensity, float squash = 1)` | 101 | A round 2D light. squash below 1 flattens it into an ellipse lying on a surface. |
| method | `public static Clickable Click(GameObject go, Vector2 size, int order, System.Action onClick = null)` | 117 |  |

## Uses
[[Align]], [[ArtLibrary]], [[Clickable]], [[FontKind]], [[Label]], [[Px]]

## Used by
[[BenchView]], [[CardView]], [[CatalystChoiceView]], [[EventView]], [[Fx]], [[GameFlow]], [[GoalToast]], [[GoalsView]], [[HudView]], [[JournalView]], [[LinkView]], [[Loop]], [[ReactionCutscene]], [[RunOverView]], [[SceneBuilder]], [[ScreenFx]], [[SimpleCutscene]], [[SparkParticles]], [[Sparks]], [[TileView]], [[TitleView]], [[Tooltip]], [[UiButton]]

## Tested by
[[LangTests]], [[SparksTests]]
