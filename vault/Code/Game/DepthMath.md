---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Depth/DepthMath.cs
tags: [code/game, kind/class]
aliases: []
---

# DepthMath

`public static class DepthMath`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/DepthMath.cs:10-127`

> The projection maths of the HD-2D bench (HD-2D mockup SPEC, "Layer stack and projection"; removed in P6-22, see git history). Pure functions, no scene. Depth z follows the SPEC: 0 is the plane where one unit is one art pixel, negative is away from the camera. In the Unity scene the camera looks along +z, so a layer at SPEC depth z sits at world z = -z.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| method | `public static float Scale(float z, float d)` | 13 | How much a layer at depth z is enlarged so that at rest it lands exactly on its art pixels: (D - z) / D. |
| method | `public static float FovDegrees(float d, float halfHeight = Px.H / 2f)` | 16 | Vertical field of view, in degrees, that shows halfHeight units above and below the centre at distance d. |
| method | `public static Vector3 Place(Vector2 rest, float z, float d)` | 19 | World position of a point that is seen at (world x, y on the plane z = 0) once pushed to depth z. |
| method | `public static Vector2 Seen(Vector3 world, float d)` | 26 | Where a world point is seen on the plane z = 0 by a camera at rest (the inverse of Place). |
| method | `public static Vector2 Project(Vector3 world, Vector2 cam, Vector2 shift, float d)` | 34 | Where a world point is drawn on the UI camera's world (art px, origin at the centre, y up) by a world camera moved by with the picture shifted by (the inverse of ViewportRay). |
| method | `public static Vector2[] DrawnFace(Vector2 faceCentre, Vector2 faceSize, float artY, Vector2 cam, Vector2 shift, float inset, float d, TableShape table` | 44 | The four corners of a tile's face, pulled in by , as drawn (a tile group sits at the depth of its row's table point, +0.2) in UI units. The dev key F10 logs them so a click test can aim at what the player sees. |
| method | `public static float TableZ(float artY, float d, TableShape table)` | 61 | Depth of an art row of the table. The table is a floor: 1 / distance runs linearly from the back edge (art y 132) to the front edge (art y 254), which is exactly a tilted plane; rows below the front edge are the table's upright front face and share the front edge's depth. |
| method | `public static float Parallax(float z, float zAnchor, float d)` | 71 | Parallax factor of a depth: a camera move of c shifts the layer by c * Parallax on screen (art px), with the picture re-aimed so that the anchor depth does not move. Positive follows the camera (far layers). |
| method | `public static Vector2 Sway(float t, float amplitude, float periodX, float periodY)` | 75 | The idle sway at time t: x = A sin(2 pi t / periodX), y = A / 2 sin(2 pi t / periodY), in art px, world y up. A and the periods are CameraSway's serialized fields (owner gate G2 #4: A = 4, 6 s and 9 s). |
| method | `public static Vector2 AnchorShift(Vector2 cam, float zAnchor, float d)` | 86 | The lens shift (art px on screen, world y up) that cancels a camera move of cam at the anchor depth, so that depth does not move: cam * D / (D - zAnchor). |
| method | `public static Matrix4x4 Projection(float d, Vector2 shift, float near, float far)` | 92 | The world camera's projection: the rest field of view on a 640:360 frame, with the picture shifted by art px (an off-axis frustum, the same as a lens shift). |
| method | `public static Ray ViewportRay(Vector2 viewport, Vector2 cam, Vector2 shift, float d)` | 104 | The ray through a viewport point (0..1, y up) of a world camera moved by cam from its rest position and shifted by shift art px. The same ray as Camera.ViewportPointToRay on a camera set up with Projection. |
| method | `public static bool HitTable(Ray ray, out Vector2 flat, float d, TableShape table)` | 114 | Where a ray meets the table's tilted top, as the flat (rest) world position of that point: the coordinates TileView.Contains and Clickable.Contains use. False when the ray runs parallel to it or away from it. |

## Uses
[[Px]], [[TableShape]]

## Used by
[[CameraSway]], [[DepthLayer]], [[DepthRig]], [[DepthStage]], [[DepthTable]], [[GameManager]]

## Tested by
[[DepthClickTests]], [[DepthLayerTests]], [[MainDepthTests]], [[MainDevHookTests]]
