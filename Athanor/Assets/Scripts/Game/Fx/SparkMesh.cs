using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Athanor.Game
{
    /// <summary>
    /// Many 1 px dots and 5x5 sparkles drawn as one mesh at one sorting order: one renderer and at most two draws
    /// (one per texture) instead of a SpriteRenderer each. Fill it every frame with Begin, Add, End. Centres are
    /// rounded to whole pixels here, and colours stay floats so HDR glow (above 1) reaches the bloom.
    /// Needs no engine module beyond what the game already uses (no ParticleSystem).
    /// </summary>
    public sealed class SparkMesh : MonoBehaviour
    {
        struct Vert { public Vector3 pos; public Color color; public Vector2 uv; }
        struct Quad { public Vector2 pos; public Color color; }

        /// <summary>A dot is drawn at a quarter of its 4x4 sprite (1 px), as the SpriteRenderers did; a sparkle at full size.</summary>
        public const float DotScale = 0.25f;

        static readonly VertexAttributeDescriptor[] Layout =
        {
            new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3),
            new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.Float32, 4),
            new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2),
        };
        const MeshUpdateFlags Quiet = MeshUpdateFlags.DontRecalculateBounds | MeshUpdateFlags.DontValidateIndices;
        static readonly Bounds Everything = new Bounds(Vector3.zero, new Vector3(1e5f, 1e5f, 1e3f));
        static Material baseMaterial;
        static readonly Dictionary<Texture, Material> materials = new Dictionary<Texture, Material>();

        Mesh mesh;
        MeshRenderer meshRenderer;
        Vector2 dotMin, dotMax, sparkMin, sparkMax;         // quad corners around the centre, in pixels
        Vector2 dotUv0, dotUv1, sparkUv0, sparkUv1;
        Quad[] dots = new Quad[32], sparks = new Quad[32];
        Vert[] verts = new Vert[0];
        int dotCount, sparkCount, quadCapacity, drawn;
        bool boundsSet;

        public int Count => dotCount + sparkCount;

        /// <summary>A child of parent at its origin; positions passed to Add are in parent's space. sparkle may be null.</summary>
        public static SparkMesh Create(Transform parent, string name, int order, Sprite dot, Sprite sparkle)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var m = go.AddComponent<SparkMesh>();
            m.Setup(order, dot, sparkle);
            return m;
        }

        /// <summary>The material for a texture: the shared "ParticleGlow" material (Resources) with that texture.</summary>
        public static Material MaterialFor(Texture texture)
        {
            if (texture != null && materials.TryGetValue(texture, out var m) && m != null) return m;
            if (baseMaterial == null) baseMaterial = Resources.Load<Material>("ParticleGlow");
            m = new Material(baseMaterial) { name = "ParticleGlow " + (texture != null ? texture.name : "white") };
            if (texture != null) { m.SetTexture("_MainTex", texture); materials[texture] = m; }
            return m;
        }

        void Setup(int order, Sprite dot, Sprite sparkle)
        {
            if (sparkle == null) sparkle = dot;
            Corners(dot, DotScale, out dotMin, out dotMax, out dotUv0, out dotUv1);
            Corners(sparkle, 1, out sparkMin, out sparkMax, out sparkUv0, out sparkUv1);
            mesh = new Mesh { name = name };
            mesh.MarkDynamic();
            gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterials = new[] { MaterialFor(dot.texture), MaterialFor(sparkle.texture) };
            meshRenderer.sortingOrder = order;
            meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
            meshRenderer.receiveShadows = false;
            meshRenderer.lightProbeUsage = LightProbeUsage.Off;
            meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
            meshRenderer.enabled = false;
        }

        static void Corners(Sprite s, float scale, out Vector2 min, out Vector2 max, out Vector2 uv0, out Vector2 uv1)
        {
            var b = s.bounds;
            min = b.min * scale; max = b.max * scale;
            var uv = s.uv;                                  // atlas-aware; read once
            uv0 = uv1 = uv[0];
            foreach (var u in uv) { uv0 = Vector2.Min(uv0, u); uv1 = Vector2.Max(uv1, u); }
        }

        public void Begin() { dotCount = sparkCount = 0; }

        public void Add(Vector2 pos, Color color, bool sparkle)
        {
            if (sparkle) { if (sparkCount == sparks.Length) System.Array.Resize(ref sparks, sparks.Length * 2); sparks[sparkCount++] = new Quad { pos = pos, color = color }; }
            else { if (dotCount == dots.Length) System.Array.Resize(ref dots, dots.Length * 2); dots[dotCount++] = new Quad { pos = pos, color = color }; }
        }

        /// <summary>Uploads what was added since Begin. Allocates only when the most quads so far grows.</summary>
        public void End()
        {
            const int Most = 16384;                          // 16-bit indices
            if (dotCount > Most) dotCount = Most;
            if (dotCount + sparkCount > Most) sparkCount = Most - dotCount;
            int n = dotCount + sparkCount;
            if (n == 0 && drawn == 0) return;
            drawn = n;
            meshRenderer.enabled = n > 0;
            if (n == 0) return;
            if (n > quadCapacity) Grow(n);
            int v = 0;
            for (int i = 0; i < dotCount; i++) v = Put(v, dots[i], dotMin, dotMax, dotUv0, dotUv1);
            for (int i = 0; i < sparkCount; i++) v = Put(v, sparks[i], sparkMin, sparkMax, sparkUv0, sparkUv1);
            mesh.SetVertexBufferData(verts, 0, 0, v, 0, Quiet);
            // Bounds are given by hand (DontRecalculateBounds): one huge box, so the mesh is never culled; it is all on
            // screen. Each submesh carries it too, because a default descriptor has an invalid box ("Converting invalid
            // MinMaxAABB"), and the mesh box is set only once vertices exist, never on an empty mesh.
            var d0 = new SubMeshDescriptor(0, dotCount * 6) { bounds = Everything };
            var d1 = new SubMeshDescriptor(dotCount * 6, sparkCount * 6) { bounds = Everything };
            mesh.SetSubMesh(0, d0, Quiet);
            mesh.SetSubMesh(1, d1, Quiet);
            if (!boundsSet) { mesh.bounds = Everything; boundsSet = true; }
        }

        int Put(int v, in Quad q, Vector2 min, Vector2 max, Vector2 uv0, Vector2 uv1)
        {
            float x = Mathf.Round(q.pos.x), y = Mathf.Round(q.pos.y);
            verts[v] = new Vert { pos = new Vector3(x + min.x, y + min.y, 0), color = q.color, uv = new Vector2(uv0.x, uv0.y) };
            verts[v + 1] = new Vert { pos = new Vector3(x + min.x, y + max.y, 0), color = q.color, uv = new Vector2(uv0.x, uv1.y) };
            verts[v + 2] = new Vert { pos = new Vector3(x + max.x, y + max.y, 0), color = q.color, uv = new Vector2(uv1.x, uv1.y) };
            verts[v + 3] = new Vert { pos = new Vector3(x + max.x, y + min.y, 0), color = q.color, uv = new Vector2(uv1.x, uv0.y) };
            return v + 4;
        }

        void Grow(int need)
        {
            quadCapacity = Mathf.Max(64, Mathf.NextPowerOfTwo(need));
            verts = new Vert[quadCapacity * 4];
            var idx = new ushort[quadCapacity * 6];
            for (int q = 0; q < quadCapacity; q++)
            {
                int i = q * 6, b = q * 4;
                idx[i] = (ushort)b; idx[i + 1] = (ushort)(b + 1); idx[i + 2] = (ushort)(b + 2);
                idx[i + 3] = (ushort)b; idx[i + 4] = (ushort)(b + 2); idx[i + 5] = (ushort)(b + 3);
            }
            mesh.SetVertexBufferParams(quadCapacity * 4, Layout);
            mesh.SetIndexBufferParams(quadCapacity * 6, IndexFormat.UInt16);
            mesh.SetIndexBufferData(idx, 0, 0, idx.Length, Quiet);
            // Growing subMeshCount derives each new submesh's box from the mesh's box, which is invalid ("Converting
            // invalid MinMaxAABB") while the mesh has vertex storage but no vertex data and no box. So give it zeroed
            // vertices and the box first, then the submesh count. This runs only when the most quads so far grows.
            mesh.SetVertexBufferData(verts, 0, 0, verts.Length, 0, Quiet);
            mesh.bounds = Everything;
            mesh.subMeshCount = 2;
            boundsSet = true;
        }

        void OnDestroy()
        {
            if (mesh == null) return;
            if (Application.isPlaying) Destroy(mesh); else DestroyImmediate(mesh);
        }

        /// <summary>For tests: the vertex positions and colours uploaded by the last End.</summary>
        public Mesh Mesh => mesh;
    }
}
