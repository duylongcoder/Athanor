---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/DepthLayer.cs
tags: [code/game, kind/class]
aliases: []
---

# DepthLayer

`public sealed class DepthLayer` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/DepthLayer.cs:10-84`

> One flat thing of the bench placed in depth: it is moved to its depth and enlarged by (D - z) / D, so that from the camera at rest it covers exactly the art pixels it covers in the flat game. Sorting stays by sortingOrder.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public float z` | 13 | SPEC depth: 0 is the art plane, negative is away from the camera. |
| field | `public Vector2 rest` | 15 | World x, y of the object in the flat game (its position on the plane z = 0). |
| field | `public Vector3 restScale = Vector3.one` | 17 | Its scale in the flat game. |
| field | `public Light2D pointLight` | 20 | A point light on this object, or null. URP builds a point light's matrix from its radius and ignores the transform's scale, so the radius is what gets enlarged. |
| field | `public float restInner, restOuter` | 22 | The point light's inner and outer radius in the flat game. |
| method | `public static DepthLayer Add(GameObject go, float z)` | 25 | Takes the object's present position, scale and point light as its flat (rest) state and sets its depth. |
| field | `public Light2D[] groupLights = new Light2D[0]` | 47 | Lights below a group layer (see AddGroup): their radii are enlarged with the group, because a point light ignores the scale of its parents. |
| field | `public float[] groupInner = new float[0], groupOuter = new float[0]` | 48 |  |
| method | `public static DepthLayer AddGroup(GameObject go, float z)` | 55 | A group at the origin: everything below it is in flat (rest) coordinates, and the group is moved to depth z and enlarged by (D - z) / D as a whole. Children keep their own scale and tweens (an icon's pop), and may move by setting localPosition. The point lights below it are enlarged by their radius. |
| method | `public void Apply(float d)` | 67 | Places the object for a camera at distance d from the art plane. |

## Uses
[[DepthMath]]

## Used by
[[DepthRig]], [[DepthStage]], [[DepthTestBuilder]], [[WindowView]]

## Tested by
[[DepthLayerTests]], [[MainDepthTests]]
