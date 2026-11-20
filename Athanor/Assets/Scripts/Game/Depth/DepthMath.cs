using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The projection maths of the HD-2D bench (HD-2D mockup SPEC, "Layer stack and projection"; removed in P6-22, see git history). Pure functions,
    /// no scene. Depth z follows the SPEC: 0 is the plane where one unit is one art pixel, negative is away from the
    /// camera. In the Unity scene the camera looks along +z, so a layer at SPEC depth z sits at world z = -z.
    /// </summary>
    public static class DepthMath
    {
        /// <summary>How much a layer at depth z is enlarged so that at rest it lands exactly on its art pixels: (D - z) / D.</summary>
        public static float Scale(float z, float d) => (d - z) / d;

        /// <summary>Vertical field of view, in degrees, that shows halfHeight units above and below the centre at distance d.</summary>
        public static float FovDegrees(float d, float halfHeight = Px.H / 2f) => 2f * Mathf.Atan(halfHeight / d) * Mathf.Rad2Deg;

        /// <summary>World position of a point that is seen at <paramref name="rest"/> (world x, y on the plane z = 0) once pushed to depth z.</summary>
        public static Vector3 Place(Vector2 rest, float z, float d)
        {
            float s = Scale(z, d);
            return new Vector3(rest.x * s, rest.y * s, -z);
        }

        /// <summary>Where a world point is seen on the plane z = 0 by a camera at rest (the inverse of <see cref="Place"/>).</summary>
        public static Vector2 Seen(Vector3 world, float d)
        {
            float k = d / (d + world.z);
            return new Vector2(world.x * k, world.y * k);
        }

        /// <summary>Where a world point is drawn on the UI camera's world (art px, origin at the centre, y up) by a world camera moved by
        /// <paramref name="cam"/> with the picture shifted by <paramref name="shift"/> (the inverse of <see cref="ViewportRay"/>).</summary>
        public static Vector2 Project(Vector3 world, Vector2 cam, Vector2 shift, float d)
        {
            float k = d / (world.z + d);
            return new Vector2((world.x - cam.x) * k + shift.x, (world.y - cam.y) * k + shift.y);
        }

        /// <summary>
        /// The four corners of a tile's face, pulled in by <paramref name="inset"/>, as drawn (a tile group sits at the depth of its
        /// row's table point, +0.2) in UI units. The dev key F10 logs them so a click test can aim at what the player sees.
        /// </summary>
        public static Vector2[] DrawnFace(Vector2 faceCentre, Vector2 faceSize, float artY, Vector2 cam, Vector2 shift, float inset, float d, TableShape table)
        {
            float z = TableZ(artY, d, table) + 0.2f;
            float hx = faceSize.x / 2 - inset, hy = faceSize.y / 2 - inset;
            var pts = new Vector2[4];
            int n = 0;
            for (int sy = -1; sy <= 1; sy += 2)
                for (int sx = -1; sx <= 1; sx += 2)
                    pts[n++] = Project(Place(faceCentre + new Vector2(sx * hx, sy * hy), z, d), cam, shift, d);
            return pts;
        }

        /// <summary>
        /// Depth of an art row of the table. The table is a floor: 1 / distance runs linearly from the back edge
        /// (art y 132) to the front edge (art y 254), which is exactly a tilted plane; rows below the front edge are the
        /// table's upright front face and share the front edge's depth.
        /// </summary>
        public static float TableZ(float artY, float d, TableShape table)
        {
            float r = Mathf.Clamp01((artY - table.topY) / table.rows);
            return d - 1f / (1f / table.back + (1f / table.front - 1f / table.back) * r);
        }

        /// <summary>
        /// Parallax factor of a depth: a camera move of c shifts the layer by c * Parallax on screen (art px), with the
        /// picture re-aimed so that the anchor depth does not move. Positive follows the camera (far layers).
        /// </summary>
        public static float Parallax(float z, float zAnchor, float d) => -(d / (d - z) - d / (d - zAnchor));

        /// <summary>The idle sway at time t: x = A sin(2 pi t / periodX), y = A / 2 sin(2 pi t / periodY), in art px, world y up.
        /// A and the periods are <see cref="CameraSway"/>'s serialized fields (owner gate G2 #4: A = 4, 6 s and 9 s).</summary>
        public static Vector2 Sway(float t, float amplitude, float periodX, float periodY)
        {
            float x = amplitude * Mathf.Sin(2f * Mathf.PI * t / periodX);
            float y = amplitude / 2f * Mathf.Sin(2f * Mathf.PI * t / periodY);
            return new Vector2(x, y);
        }

        /// <summary>
        /// The lens shift (art px on screen, world y up) that cancels a camera move of cam at the anchor depth, so that depth
        /// does not move: cam * D / (D - zAnchor).
        /// </summary>
        public static Vector2 AnchorShift(Vector2 cam, float zAnchor, float d) => cam * (d / (d - zAnchor));

        /// <summary>
        /// The world camera's projection: the rest field of view on a 640:360 frame, with the picture shifted by
        /// <paramref name="shift"/> art px (an off-axis frustum, the same as a lens shift).
        /// </summary>
        public static Matrix4x4 Projection(float d, Vector2 shift, float near, float far)
        {
            var m = Matrix4x4.Perspective(FovDegrees(d), Px.W / (float)Px.H, near, far);
            m.m02 = -shift.x / (Px.W / 2f);
            m.m12 = -shift.y / (Px.H / 2f);
            return m;
        }

        /// <summary>
        /// The ray through a viewport point (0..1, y up) of a world camera moved by cam from its rest position and shifted
        /// by shift art px. The same ray as <c>Camera.ViewportPointToRay</c> on a camera set up with <see cref="Projection"/>.
        /// </summary>
        public static Ray ViewportRay(Vector2 viewport, Vector2 cam, Vector2 shift, float d)
        {
            float sx = (viewport.x - 0.5f) * Px.W - shift.x, sy = (viewport.y - 0.5f) * Px.H - shift.y;
            return new Ray(new Vector3(cam.x, cam.y, -d), new Vector3(sx / d, sy / d, 1f).normalized);
        }

        /// <summary>
        /// Where a ray meets the table's tilted top, as the flat (rest) world position of that point: the coordinates
        /// <c>TileView.Contains</c> and <c>Clickable.Contains</c> use. False when the ray runs parallel to it or away from it.
        /// </summary>
        public static bool HitTable(Ray ray, out Vector2 flat, float d, TableShape table)
        {
            Vector3 a = Place(Px.At(0, table.topY), TableZ(table.topY, d, table), d);
            Vector3 b = Place(Px.At(0, table.topY + table.rows), TableZ(table.topY + table.rows, d, table), d);
            Vector3 n = Vector3.Cross(b - a, Vector3.right);
            float den = Vector3.Dot(ray.direction, n);
            flat = default;
            if (Mathf.Abs(den) < 1e-6f) return false;
            float t = Vector3.Dot(a - ray.origin, n) / den;
            if (t <= 0) return false;
            flat = Seen(ray.GetPoint(t), d);
            return true;
        }
    }

    /// <summary>
    /// The table's shape as the maths needs it: the distance from the camera of its front and back edge at rest, the art y of
    /// its back edge and the number of art rows of the tilted top. <see cref="DepthTable"/> holds the numbers (Main.unity).
    /// </summary>
    public readonly struct TableShape
    {
        public readonly float front, back, topY, rows;

        public TableShape(float front, float back, float topY, float rows)
        {
            this.front = front; this.back = back; this.topY = topY; this.rows = rows;
        }
    }
}
