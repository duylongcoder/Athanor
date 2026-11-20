---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/DepthTable.cs
tags: [code/game, kind/class]
aliases: []
---

# DepthTable

`public sealed class DepthTable` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/DepthTable.cs:13-83`

> The table as a floor: one mesh lying on the tilted plane of DepthMath.TableZ (plus the upright front face below the front edge), cut along every art row. A plain tilted quad would foreshorten the art, because the texture is interpolated in perspective; pinning each art row to its own depth makes the picture land on its art pixels at rest, with shared vertices (no seams) and one draw.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public Sprite sprite` | 18 | The table picture; its rows are art rows. |
| field | `public float artX, artY` | 20 | Art-space position of the picture's top-left corner. |
| field | `public float distance` | 22 | Camera distance the mesh was last built for. |
| field | `public float front, back` | 24 | Distance from the camera of the front and the back edge of the table top, at rest. |
| field | `public float topY, rows` | 26 | Art y of the back edge and the number of art rows of the tilted top. |
| method | `public float RowZ(float y)` | 40 | Depth of an art row of this table. |
| property | `public TableShape Shape` | 43 | The table's shape for DepthMath. |
| method | `public Vector3 Point(float x, float y)` | 46 | World position of an art-space point lying on the table. |
| method | `public void Rebuild(float d)` | 49 | Builds the mesh for a camera at distance d and binds the sprite's texture. |

## Uses
[[DepthMath]], [[Px]], [[TableShape]]

## Used by
[[DepthRig]]

## Tested by
[[LayoutPinTests]]
