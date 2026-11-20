using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The table as a floor: one mesh lying on the tilted plane of <see cref="DepthMath.TableZ"/> (plus the upright front
    /// face below the front edge), cut along every art row. A plain tilted quad would foreshorten the art, because the
    /// texture is interpolated in perspective; pinning each art row to its own depth makes the picture land on its art
    /// pixels at rest, with shared vertices (no seams) and one draw.
    /// </summary>
    [ExecuteAlways, RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public sealed class DepthTable : MonoBehaviour
    {
        static readonly int MainTex = Shader.PropertyToID("_MainTex");

        /// <summary>The table picture; its rows are art rows.</summary>
        public Sprite sprite;
        /// <summary>Art-space position of the picture's top-left corner.</summary>
        public float artX, artY;
        /// <summary>Camera distance the mesh was last built for.</summary>
        public float distance;
        /// <summary>Distance from the camera of the front and the back edge of the table top, at rest.</summary>
        public float front, back;
        /// <summary>Art y of the back edge and the number of art rows of the tilted top.</summary>
        public float topY, rows;

        Mesh mesh;
        MaterialPropertyBlock block;

        void OnEnable() => Rebuild(distance);

        void OnDestroy()
        {
            if (mesh == null) return;
            if (Application.isPlaying) Destroy(mesh); else DestroyImmediate(mesh);
        }

        /// <summary>Depth of an art row of this table.</summary>
        public float RowZ(float y) => DepthMath.TableZ(y, distance, Shape);

        /// <summary>The table's shape for <see cref="DepthMath"/>.</summary>
        public TableShape Shape => new TableShape(front, back, topY, rows);

        /// <summary>World position of an art-space point lying on the table.</summary>
        public Vector3 Point(float x, float y) => DepthMath.Place(Px.At(x, y), RowZ(y), distance);

        /// <summary>Builds the mesh for a camera at distance d and binds the sprite's texture.</summary>
        public void Rebuild(float d)
        {
            distance = d;
            if (sprite == null) return;
            transform.position = Vector3.zero; transform.localScale = Vector3.one;
            int w = (int)sprite.rect.width, h = (int)sprite.rect.height;
            // the sprite's place in its texture (the source picture in the editor, an atlas page in a build)
            Vector4 uv = UnityEngine.Sprites.DataUtility.GetOuterUV(sprite);
            var vertices = new List<Vector3>((h + 1) * 2);
            var uvs = new List<Vector2>((h + 1) * 2);
            var colors = new List<Color32>((h + 1) * 2);
            var triangles = new List<int>(h * 6);
            for (int j = 0; j <= h; j++)
            {
                float y = artY + j, v = Mathf.Lerp(uv.w, uv.y, j / (float)h);
                vertices.Add(Point(artX, y)); vertices.Add(Point(artX + w, y));
                uvs.Add(new Vector2(uv.x, v)); uvs.Add(new Vector2(uv.z, v));
                colors.Add(Color.white); colors.Add(Color.white);
                if (j == h) break;
                int a = j * 2;
                triangles.Add(a); triangles.Add(a + 1); triangles.Add(a + 2);
                triangles.Add(a + 1); triangles.Add(a + 3); triangles.Add(a + 2);
            }
            if (mesh == null) mesh = new Mesh { name = "DepthTable", hideFlags = HideFlags.DontSave };
            mesh.Clear();
            mesh.SetVertices(vertices); mesh.SetUVs(0, uvs); mesh.SetColors(colors); mesh.SetTriangles(triangles, 0);
            mesh.RecalculateBounds();
            GetComponent<MeshFilter>().sharedMesh = mesh;
            var r = GetComponent<MeshRenderer>();
            if (block == null) block = new MaterialPropertyBlock();
            r.GetPropertyBlock(block);
            block.SetTexture(MainTex, sprite.texture);
            r.SetPropertyBlock(block);
        }
    }
}
