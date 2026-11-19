---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Spawn.cs
tags: [code/game, kind/class]
aliases: []
---

# Spawn

`public static class Spawn`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Spawn.cs:13-106`

> Run-time spawners: the objects the game makes while it plays (float numbers, fly-in icons, tutorial rects, link motes, rarity stars). Everything that is on screen from the start is a scene object or a prefab, not made here.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static Transform Root(string name, Transform parent = null)` | 16 | An empty child that draws with its parent (same layer). |
| method | `public static SpriteRenderer Sprite(Transform parent, string sprite, Vector3 worldPos, int order, Material material = null)` | 25 | A sprite centred on a world position. |
| method | `public static SpriteRenderer Rect(Transform parent, float x, float y, float w, float h, Color color, int order)` | 39 | A filled rectangle in art space. |
| method | `public static SpriteRenderer Glow(Transform parent, Vector3 worldPos, float radius, Color color, int order)` | 49 | An additive soft glow of the given radius. |
| method | `public static Label Text(Transform parent, string text, float x, float y, FontKind font, int size, Color color, int order, Align align = Align.Left, C` | 58 | Text whose top edge is at art-space y, anchored at x by its alignment. width > 0 wraps. |
| method | `public static Clickable Click(GameObject go, Vector2 size, int order, System.Action onClick = null)` | 100 | Makes a game object clickable: a rectangle of the given size, drawn at order, that calls onClick. |

## Uses
[[Align]], [[Art]], [[Clickable]], [[FontKind]], [[Fonts]], [[Label]], [[Px]]

## Used by
[[BenchView]], [[Fx]], [[IgniteSequence]], [[LinkView]], [[RarityStars]], [[ReactionCutscene]], [[TutorialController]]

## Tested by
[[MainDepthTests]]
