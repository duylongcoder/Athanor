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
        /// <summary>GameObject layer of the bench in the main scene: the world camera draws this layer only, the UI camera every
        /// other one, so anything created later (a card, a float text, a spark) is UI unless it is made a child of the world.
        /// The depth test scene keeps its own convention (the world on layer 0, the HUD on <see cref="UiLayer"/>).</summary>
        public const int WorldLayer = 6;
        static readonly int MainTex = Shader.PropertyToID("_MainTex");

        /// <summary>D: the world camera's distance to the art plane (z = 0) at rest.</summary>
        public float distance;
        /// <summary>K: the world is rendered at 640K x 360K. A whole number, at least 2 (owner gate G2).</summary>
        [Min(2)] public int scale = 2;
        /// <summary>The perspective camera that renders the layers into <see cref="WorldTexture"/>.</summary>
        public Camera worldCamera;
        /// <summary>The orthographic camera that shows the world texture and the HUD.</summary>
        public Camera uiCamera;
        /// <summary>The full-screen quad (640 x 360 units, on the UI layer) that shows the world texture.</summary>
        public MeshRenderer worldDisplay;
        /// <summary>When playing, K follows the screen: the largest whole multiple of 640x360 that fits, at least 2.</summary>
        public bool autoScale = true;
        /// <summary>The world camera's clip planes (distance from the camera).</summary>
        public float near, far;
        /// <summary>The table the bench stands on: its shape gives the anchor depth and the hit test.</summary>
        public DepthTable table;
        /// <summary>Art y of the row that never moves under sway: the middle tile row.</summary>
        public float anchorY;

        MaterialPropertyBlock block;
        bool flat;
        int displayScale = 2;

        /// <summary>
        /// Flat mode, for the cutscenes: the world camera and the world picture are off and the UI camera draws the whole
        /// frame at z = 0 with the bloom and vignette (post processing on it). No sway, no perspective, no depth effect.
        /// </summary>
        public bool Flat
        {
            get => flat;
            set
            {
                flat = value;
                if (worldCamera != null) worldCamera.enabled = !value;
                if (worldDisplay != null) worldDisplay.enabled = !value;
                if (uiCamera != null && uiCamera.TryGetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>(out var data))
                    data.renderPostProcessing = value;
            }
        }

        /// <summary>The whole multiple of 640x360 that fits the screen (at least 1); K = max(2, this).</summary>
        public static int ScreenScale() => Mathf.Max(1, Mathf.FloorToInt(Mathf.Min(Screen.width / (float)Px.W, Screen.height / (float)Px.H)));

        /// <summary>What the world camera renders into: 640K x 360K, point filtered, created here and never saved.</summary>
        public RenderTexture WorldTexture { get; private set; }

        /// <summary>The world camera's position at rest. Sway and shake offset the camera from here.</summary>
        public Vector3 RestPosition => new Vector3(0, 0, -distance);

        /// <summary>The world camera's travel from rest (art px at z = 0, world y up): the sway.</summary>
        public Vector2 CamOffset { get; private set; }
        /// <summary>The shift of the whole world picture (art px, world y up): the anchor correction of the sway plus the shake.</summary>
        public Vector2 ImageShift { get; private set; }
        /// <summary>SPEC depth of the anchor row, which the sway never moves.</summary>
        public float AnchorZ => DepthMath.TableZ(anchorY, distance, table.Shape);

        /// <summary>
        /// Moves the world camera by cam (the sway) and shifts the world picture by shake whole art px; the anchor row stays
        /// still. The HUD camera is not touched.
        /// </summary>
        public void SetView(Vector2 cam, Vector2 shake)
        {
            CamOffset = cam;
            ImageShift = DepthMath.AnchorShift(cam, AnchorZ, distance) + shake;
            ApplyCamera();
        }

        /// <summary>Viewport point (0..1, y up) of the world texture under a point of the UI camera's world (640x360 units, origin at the centre).</summary>
        public static Vector2 ViewportOf(Vector2 ui)
        {
            float x = ui.x / Px.W + 0.5f, y = ui.y / Px.H + 0.5f;
            return new Vector2(x, y);
        }

        /// <summary>The world ray under a point of the UI camera's world, through the world texture and the world camera.</summary>
        public Ray WorldRay(Vector2 ui) => DepthMath.ViewportRay(ViewportOf(ui), CamOffset, ImageShift, distance);

        /// <summary>The flat (rest) position on the table under a point of the UI camera's world: what the bench's hit tests take.</summary>
        public bool BenchPoint(Vector2 ui, out Vector2 flat) => DepthMath.HitTable(WorldRay(ui), out flat, distance, table.Shape);

        void ApplyCamera()
        {
            if (worldCamera == null) return;
            worldCamera.orthographic = false;
            worldCamera.fieldOfView = DepthMath.FovDegrees(distance);
            worldCamera.nearClipPlane = near; worldCamera.farClipPlane = far;
            worldCamera.transform.SetPositionAndRotation(RestPosition + (Vector3)CamOffset, Quaternion.identity);
            worldCamera.projectionMatrix = DepthMath.Projection(distance, ImageShift, near, far);
        }

        void OnEnable() => Apply();
        void OnDisable() => Release();

        void Update()
        {
            if (Application.isPlaying && autoScale)
            {
                displayScale = ScreenScale();
                scale = Mathf.Max(2, displayScale);
            }
            // a lost texture (a browser tab that lost its graphics context) or a changed K
            if (WorldTexture == null || !WorldTexture.IsCreated() || WorldTexture.width != Px.W * Mathf.Max(2, scale)) Apply();
            // K = 2 on a 1x screen: the texture is shown at half size, and a bilinear read averages each 2 x 2 block
            var want = displayScale < scale && Application.isPlaying ? FilterMode.Bilinear : FilterMode.Point;
            if (WorldTexture != null && WorldTexture.filterMode != want) WorldTexture.filterMode = want;
        }

        /// <summary>Sets up the cameras and the world texture, and places every DepthLayer and DepthTable below this object.</summary>
        [ContextMenu("Apply")]
        public void Apply()
        {
            scale = Mathf.Max(2, scale);
            EnsureTexture();
            ApplyCamera();
            if (worldCamera != null) worldCamera.targetTexture = WorldTexture;
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
