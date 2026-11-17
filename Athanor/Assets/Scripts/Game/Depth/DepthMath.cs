using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The projection maths of the HD-2D bench (docs/mockup/hd2d/SPEC.md, "Layer stack and projection"). Pure functions,
    /// no scene. Depth z follows the SPEC: 0 is the plane where one unit is one art pixel, negative is away from the
    /// camera. In the Unity scene the camera looks along +z, so a layer at SPEC depth z sits at world z = -z.
    /// </summary>
    public static class DepthMath
    {
        /// <summary>D: the camera's distance to the plane z = 0 at rest.</summary>
        public const float RestDistance = 600f;
        /// <summary>The table's front edge (art y 254 and below) is this far from the camera: z = -60.</summary>
        public const float TableFront = 660f;
        /// <summary>The table's back edge (art y 132) is this far from the camera: 1.27 x the front, z = -238.2.</summary>
        public const float TableBack = 1.27f * TableFront;
        /// <summary>Art y of the table's back edge, and the number of art rows the tilted top covers.</summary>
        public const float TableTopY = 132f, TableRows = 122f;
        /// <summary>The art row that never moves under sway: the middle tile row.</summary>
        public const float AnchorY = 188f;

        /// <summary>How much a layer at depth z is enlarged so that at rest it lands exactly on its art pixels: (D - z) / D.</summary>
        public static float Scale(float z, float d = RestDistance) => (d - z) / d;

        /// <summary>Vertical field of view, in degrees, that shows halfHeight units above and below the centre at distance d.</summary>
        public static float FovDegrees(float d = RestDistance, float halfHeight = Px.H / 2f) => 2f * Mathf.Atan(halfHeight / d) * Mathf.Rad2Deg;

        /// <summary>World position of a point that is seen at <paramref name="rest"/> (world x, y on the plane z = 0) once pushed to depth z.</summary>
        public static Vector3 Place(Vector2 rest, float z, float d = RestDistance)
        {
            float s = Scale(z, d);
            return new Vector3(rest.x * s, rest.y * s, -z);
        }

        /// <summary>Where a world point is seen on the plane z = 0 by a camera at rest (the inverse of <see cref="Place"/>).</summary>
        public static Vector2 Seen(Vector3 world, float d = RestDistance)
        {
            float k = d / (d + world.z);
            return new Vector2(world.x * k, world.y * k);
        }

        /// <summary>
        /// Depth of an art row of the table. The table is a floor: 1 / distance runs linearly from the back edge
        /// (art y 132) to the front edge (art y 254), which is exactly a tilted plane; rows below the front edge are the
        /// table's upright front face and share the front edge's depth.
        /// </summary>
        public static float TableZ(float artY, float d = RestDistance, float front = TableFront, float back = TableBack,
            float topY = TableTopY, float rows = TableRows)
        {
            float r = Mathf.Clamp01((artY - topY) / rows);
            return d - 1f / (1f / back + (1f / front - 1f / back) * r);
        }

        /// <summary>
        /// Parallax factor of a depth: a camera move of c shifts the layer by c * Parallax on screen (art px), with the
        /// picture re-aimed so that the anchor depth does not move. Positive follows the camera (far layers).
        /// </summary>
        public static float Parallax(float z, float zAnchor, float d = RestDistance) => -(d / (d - z) - d / (d - zAnchor));

        /// <summary>Sway amplitude (camera travel in art px at z = 0) and the two periods, in seconds (owner gate G2 #4).</summary>
        public const float SwayAmplitude = 4f, SwayPeriodX = 6f, SwayPeriodY = 9f;

        /// <summary>The idle sway at time t: x = A sin(2 pi t / 6), y = A / 2 sin(2 pi t / 9), in art px, world y up.</summary>
        public static Vector2 Sway(float t, float amplitude = SwayAmplitude) =>
            new Vector2(amplitude * Mathf.Sin(2f * Mathf.PI * t / SwayPeriodX), amplitude / 2f * Mathf.Sin(2f * Mathf.PI * t / SwayPeriodY));

        /// <summary>
        /// The lens shift (art px on screen, world y up) that cancels a camera move of cam at the anchor depth, so that depth
        /// does not move: cam * D / (D - zAnchor).
        /// </summary>
        public static Vector2 AnchorShift(Vector2 cam, float zAnchor, float d = RestDistance) => cam * (d / (d - zAnchor));

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
        public static Ray ViewportRay(Vector2 viewport, Vector2 cam, Vector2 shift, float d = RestDistance)
        {
            float sx = (viewport.x - 0.5f) * Px.W - shift.x, sy = (viewport.y - 0.5f) * Px.H - shift.y;
            return new Ray(new Vector3(cam.x, cam.y, -d), new Vector3(sx / d, sy / d, 1f).normalized);
        }

        /// <summary>
        /// Where a ray meets the table's tilted top, as the flat (rest) world position of that point: the coordinates
        /// <c>TileView.Contains</c> and <c>Clickable.Contains</c> use. False when the ray runs parallel to it or away from it.
        /// </summary>
        public static bool HitTable(Ray ray, out Vector2 flat, float d = RestDistance, float front = TableFront, float back = TableBack,
            float topY = TableTopY, float rows = TableRows)
        {
            Vector3 a = Place(Px.At(0, topY), TableZ(topY, d, front, back, topY, rows), d);
            Vector3 b = Place(Px.At(0, topY + rows), TableZ(topY + rows, d, front, back, topY, rows), d);
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
}
