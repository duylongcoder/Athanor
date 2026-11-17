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
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Depth/DepthMath.cs:10-114`

> The projection maths of the HD-2D bench (docs/mockup/hd2d/SPEC.md, "Layer stack and projection"). Pure functions, no scene. Depth z follows the SPEC: 0 is the plane where one unit is one art pixel, negative is away from the camera. In the Unity scene the camera looks along +z, so a layer at SPEC depth z sits at world z = -z.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const float RestDistance = 600f` | 13 | D: the camera's distance to the plane z = 0 at rest. |
| field | `public const float TableFront = 660f` | 15 | The table's front edge (art y 254 and below) is this far from the camera: z = -60. |
| field | `public const float TableBack = 1.27f * TableFront` | 17 | The table's back edge (art y 132) is this far from the camera: 1.27 x the front, z = -238.2. |
| field | `public const float TableTopY = 132f, TableRows = 122f` | 19 | Art y of the table's back edge, and the number of art rows the tilted top covers. |
| field | `public const float AnchorY = 188f` | 21 | The art row that never moves under sway: the middle tile row. |
| method | `public static float Scale(float z, float d = RestDistance)` | 24 | How much a layer at depth z is enlarged so that at rest it lands exactly on its art pixels: (D - z) / D. |
| method | `public static float FovDegrees(float d = RestDistance, float halfHeight = Px.H / 2f)` | 27 | Vertical field of view, in degrees, that shows halfHeight units above and below the centre at distance d. |
| method | `public static Vector3 Place(Vector2 rest, float z, float d = RestDistance)` | 30 | World position of a point that is seen at (world x, y on the plane z = 0) once pushed to depth z. |
| method | `public static Vector2 Seen(Vector3 world, float d = RestDistance)` | 37 | Where a world point is seen on the plane z = 0 by a camera at rest (the inverse of Place). |
| method | `public static float TableZ(float artY, float d = RestDistance, float front = TableFront, float back = TableBack, float topY = TableTopY, float rows = ` | 48 | Depth of an art row of the table. The table is a floor: 1 / distance runs linearly from the back edge (art y 132) to the front edge (art y 254), which is exactly a tilted plane; rows below the front edge are the table's upright front face and share the front edge's depth. |
| method | `public static float Parallax(float z, float zAnchor, float d = RestDistance)` | 59 | Parallax factor of a depth: a camera move of c shifts the layer by c * Parallax on screen (art px), with the picture re-aimed so that the anchor depth does not move. Positive follows the camera (far layers). |
| field | `public const float SwayAmplitude = 4f, SwayPeriodX = 6f, SwayPeriodY = 9f` | 62 | Sway amplitude (camera travel in art px at z = 0) and the two periods, in seconds (owner gate G2 #4). |
| method | `public static Vector2 Sway(float t, float amplitude = SwayAmplitude)` | 65 | The idle sway at time t: x = A sin(2 pi t / 6), y = A / 2 sin(2 pi t / 9), in art px, world y up. |
| method | `public static Vector2 AnchorShift(Vector2 cam, float zAnchor, float d = RestDistance)` | 72 | The lens shift (art px on screen, world y up) that cancels a camera move of cam at the anchor depth, so that depth does not move: cam * D / (D - zAnchor). |
| method | `public static Matrix4x4 Projection(float d, Vector2 shift, float near, float far)` | 78 | The world camera's projection: the rest field of view on a 640:360 frame, with the picture shifted by art px (an off-axis frustum, the same as a lens shift). |
| method | `public static Ray ViewportRay(Vector2 viewport, Vector2 cam, Vector2 shift, float d = RestDistance)` | 90 | The ray through a viewport point (0..1, y up) of a world camera moved by cam from its rest position and shifted by shift art px. The same ray as Camera.ViewportPointToRay on a camera set up with Projection. |
| method | `public static bool HitTable(Ray ray, out Vector2 flat, float d = RestDistance, float front = TableFront, float back = TableBack, float topY = TableTop` | 100 | Where a ray meets the table's tilted top, as the flat (rest) world position of that point: the coordinates TileView.Contains and Clickable.Contains use. False when the ray runs parallel to it or away from it. |

## Uses
[[Px]]

## Used by
[[CameraSway]], [[DepthLayer]], [[DepthRig]], [[DepthTable]], [[DepthTestBuilder]]

## Tested by
[[DepthClickTests]], [[DepthLayerTests]]
