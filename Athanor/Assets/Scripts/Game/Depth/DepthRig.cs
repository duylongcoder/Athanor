using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The camera split of the HD-2D bench. The world camera is a perspective camera at distance D from the art plane; it
    /// renders the layers under this object into a RenderTexture of 640K x 360K pixels (K = whole display scale). The UI
    /// camera is orthographic (with the PixelPerfectCamera) and draws that texture on a full-screen quad under the HUD.
    /// Sway, shake and the world's post effects act on the world camera and its texture only; the HUD never moves.
    /// </summary>
    [ExecuteAlways]
    public sealed class DepthRig : MonoBehaviour
    {
        /// <summary>GameObject layer of everything the UI camera draws (Unity's built-in "UI" layer); the world camera draws the rest.</summary>
        public const int UiLayer = 5;
        static readonly int MainTex = Shader.PropertyToID("_MainTex");

        /// <summary>D: the world camera's distance to the art plane (z = 0) at rest.</summary>
        public float distance = DepthMath.RestDistance;
        /// <summary>K: the world is rendered at 640K x 360K. A whole number, at least 2 (owner gate G2).</summary>
        [Min(2)] public int scale = 2;
        /// <summary>The perspective camera that renders the layers into <see cref="WorldTexture"/>.</summary>
        public Camera worldCamera;
        /// <summary>The orthographic camera that shows the world texture and the HUD.</summary>
        public Camera uiCamera;
        /// <summary>The full-screen quad (640 x 360 units, on the UI layer) that shows the world texture.</summary>
        public MeshRenderer worldDisplay;

        MaterialPropertyBlock block;

        /// <summary>What the world camera renders into: 640K x 360K, point filtered, created here and never saved.</summary>
        public RenderTexture WorldTexture { get; private set; }

        /// <summary>The world camera's position at rest. Sway and shake offset the camera from here.</summary>
        public Vector3 RestPosition => new Vector3(0, 0, -distance);

        void OnEnable() => Apply();
        void OnDisable() => Release();

        void Update()
        {
            // a lost texture (a browser tab that lost its graphics context) or a changed K
            if (WorldTexture == null || !WorldTexture.IsCreated() || WorldTexture.width != Px.W * Mathf.Max(2, scale)) Apply();
        }

        /// <summary>Sets up the cameras and the world texture, and places every DepthLayer and DepthTable below this object.</summary>
        [ContextMenu("Apply")]
        public void Apply()
        {
            scale = Mathf.Max(2, scale);
            EnsureTexture();
            if (worldCamera != null)
            {
                worldCamera.orthographic = false;
                worldCamera.fieldOfView = DepthMath.FovDegrees(distance);
                worldCamera.transform.SetPositionAndRotation(RestPosition, Quaternion.identity);
                worldCamera.targetTexture = WorldTexture;
            }
            if (worldDisplay != null)
            {
                if (block == null) block = new MaterialPropertyBlock();
                worldDisplay.GetPropertyBlock(block);
                block.SetTexture(MainTex, WorldTexture);
                worldDisplay.SetPropertyBlock(block);
            }
            foreach (var layer in GetComponentsInChildren<DepthLayer>(true)) layer.Apply(distance);
            foreach (var table in GetComponentsInChildren<DepthTable>(true)) table.Rebuild(distance);
        }

        void EnsureTexture()
        {
            int w = Px.W * scale, h = Px.H * scale;
            if (WorldTexture != null && WorldTexture.width == w && WorldTexture.height == h)
            {
                if (!WorldTexture.IsCreated()) WorldTexture.Create();
                return;
            }
            Release();
            WorldTexture = new RenderTexture(w, h, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB)
            {
                name = "DepthRig World " + w + "x" + h, filterMode = FilterMode.Point, wrapMode = TextureWrapMode.Clamp,
                antiAliasing = 1, useMipMap = false, autoGenerateMips = false, hideFlags = HideFlags.DontSave,
            };
            WorldTexture.Create();
        }

        void Release()
        {
            if (WorldTexture == null) return;
            if (worldCamera != null && worldCamera.targetTexture == WorldTexture) worldCamera.targetTexture = null;
            WorldTexture.Release();
            if (Application.isPlaying) Destroy(WorldTexture); else DestroyImmediate(WorldTexture);
            WorldTexture = null;
        }
    }
}
