using System.Collections.Generic;
using System.IO;
using System.Linq;
using Athanor.Game;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Builds Assets/Scenes/DepthTest.unity, the HD-2D prototype (task 5b-05, docs/devlogs/phase-4-depth.md "Prototype plan"):
/// the workshop bench of the main scene (built by SceneBuilder.BuildWorkshop), the nine tiles and the depth art placed in
/// depth with DepthLayer and DepthTable, a perspective world camera into a 640K x 360K texture (DepthRig), an orthographic
/// UI camera with a static copy of the HUD sprites, the tilt-shift, the fog sheets and the five depth presets.
/// The main scene, its library and the build's scene list are not touched. Hand edits to the test scene are overwritten.
/// Command line: unity run Athanor -- -executeMethod DepthTestBuilder.BuildBatch -quit
/// </summary>
public static class DepthTestBuilder
{
    public const string ScenePath = "Assets/Scenes/DepthTest.unity", Dir = "Assets/Settings/DepthTest";
    const string LibraryPath = "Assets/Art/ArtLibrary.asset", DepthArt = "Assets/Art/Depth/",
        MainRenderer = "Assets/Settings/Renderer2D.asset", PipelinePath = "Assets/Settings/UniversalRP.asset",
        RendererPath = Dir + "/DepthRenderer2D.asset", VolumePath = Dir + "/DepthVolume.asset";

    /// <summary>SPEC depths (docs/mockup/hd2d/SPEC.md, "Layer stack and projection").</summary>
    public const float WallZ = -400f, WindowZ = -398f, WallCandleZ = -392f, BackFogZ = -300f, DustZ = -300f, FrontFogZ = 40f, ForegroundZ = 180f;

    /// <summary>Sorting orders of the test scene: the SPEC's draw order (far, back fog, bench, front fog, foreground).
    /// The main scene's wall candles (3, 4) are above the table (2); here they go behind the back fog, which they never overlap.</summary>
    const int WallOrder = -12, WindowOrder = -9, WallCandleOrder = -8, WallFlameOrder = -7, BackFogOrder = -5,
        FrontFogOrder = 40, ForegroundOrder = 50, DisplayOrder = -1000;

    [MenuItem("Athanor/Build Depth Test Scene")]
    public static string Build()
    {
        var lib = AssetDatabase.LoadAssetAtPath<ArtLibrary>(LibraryPath);
        if (lib == null) throw new System.InvalidOperationException("no " + LibraryPath + ": run Athanor/Build Scene first");
        EditorGlobals.Inject();
        var layout = JsonUtility.FromJson<LayoutData>(File.ReadAllText("Assets/Resources/layout.json"));
        Directory.CreateDirectory(Dir);
        var tableMat = EnsureMaterial(Dir + "/DepthTable.mat", "Athanor/Depth Mesh Lit");
        var displayMat = EnsureMaterial(Dir + "/DepthDisplay.mat", "Athanor/Depth Display");
        var fogMat = EnsureMaterial(Dir + "/Fog.mat", "Athanor/Fog Lit");
        var tiltMat = EnsureMaterial(Dir + "/TiltShift.mat", "Athanor/Tilt Shift");
        int rendererIndex = EnsureRenderer(tiltMat);

        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        RenderSettings.skybox = null;
        var rig = new GameObject("Depth Rig").AddComponent<DepthRig>();
        var rt = rig.transform;

        // cameras: the world (perspective, into the texture) and the UI (orthographic, the texture under the HUD)
        var worldCam = new GameObject("World Camera").AddComponent<Camera>();
        worldCam.transform.SetParent(rt, false);
        worldCam.clearFlags = CameraClearFlags.SolidColor; worldCam.backgroundColor = Px.Hex("#0d0a12");
        worldCam.cullingMask = ~(1 << DepthRig.UiLayer); worldCam.depth = -1; worldCam.allowHDR = true; worldCam.allowMSAA = false;
        var worldData = worldCam.gameObject.AddComponent<UniversalAdditionalCameraData>();
        worldData.renderPostProcessing = true; worldData.SetRenderer(rendererIndex);
        var tilt = worldCam.gameObject.AddComponent<TiltShift>();
        tilt.material = tiltMat; tilt.Push();

        var uiGo = new GameObject("UI Camera") { tag = "MainCamera", layer = DepthRig.UiLayer };
        uiGo.transform.SetParent(rt, false);
        uiGo.transform.position = new Vector3(0, 0, -10);
        var uiCam = uiGo.AddComponent<Camera>();
        uiCam.orthographic = true; uiCam.orthographicSize = Px.H / 2f; uiCam.nearClipPlane = 0.1f; uiCam.farClipPlane = 50;
        uiCam.clearFlags = CameraClearFlags.SolidColor; uiCam.backgroundColor = Color.black;
        uiCam.cullingMask = 1 << DepthRig.UiLayer; uiCam.allowMSAA = false;
        uiGo.AddComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
        var ppc = uiGo.AddComponent<PixelPerfectCamera>();
        // no upscale texture: the UI camera draws at the screen's size, so the world texture (640K x 360K) is shown 1:1
        ppc.assetsPPU = 1; ppc.refResolutionX = Px.W; ppc.refResolutionY = Px.H;
        ppc.gridSnapping = PixelPerfectCamera.GridSnapping.None; ppc.cropFrame = PixelPerfectCamera.CropFrame.Windowbox;

        var display = new GameObject("World Display") { layer = DepthRig.UiLayer };
        display.transform.SetParent(rt, false);
        display.transform.localScale = new Vector3(Px.W, Px.H, 1);
        display.AddComponent<MeshFilter>().sharedMesh = Resources.GetBuiltinResource<Mesh>("Quad.fbx");
        var displayRenderer = display.AddComponent<MeshRenderer>();
        displayRenderer.sharedMaterial = displayMat; displayRenderer.sortingOrder = DisplayOrder;
        displayRenderer.shadowCastingMode = ShadowCastingMode.Off; displayRenderer.receiveShadows = false;

        rig.worldCamera = worldCam; rig.uiCamera = uiCam; rig.worldDisplay = displayRenderer; rig.scale = 2;

        var volume = new GameObject("Post Processing").AddComponent<Volume>();
        volume.isGlobal = true; volume.sharedProfile = BuildVolume();
        var ambient = new GameObject("Ambient Light").AddComponent<Light2D>();
        ambient.lightType = Light2D.LightType.Global; ambient.color = new Color(0.52f, 0.52f, 1f); ambient.intensity = 0.25f;

        // the world: the main scene's workshop, then every piece moved to its depth
        var world = Make.Root("World", rt);
        var workshop = SceneBuilder.BuildWorkshop(layout);
        workshop.SetParent(world, true);
        // the nine tiles, each at its row's depth on the table
        var tileRoot = Make.Root("Tiles", world);
        var tiles = new TileView[layout.tiles.Length];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new GameObject("Tile " + i).AddComponent<TileView>();
            tiles[i].Build(i, layout.tiles[i], tileRoot);
            float z = DepthMath.TableZ(layout.tiles[i].y) + 0.2f;
            foreach (Transform leaf in tiles[i].transform) Leaf(leaf.gameObject, z);
        }

        var composed = ComposeWorld(rig, world, workshop, layout, lib, tableMat, fogMat, ambient, volume, tilt);
        var effects = composed.effects; var mood = composed.mood; var swaps = new List<DepthSwitch.SpriteSwap>(effects.sprites); var flames = new List<DepthSwitch.FlameSwap>(effects.flames);
        BuildHud(rt);
        var sway = rig.gameObject.AddComponent<CameraSway>();
        var controls = rig.gameObject.AddComponent<DepthTestControls>();
        controls.rig = rig; controls.sway = sway; controls.effects = effects; controls.mood = mood; controls.tiles = tiles;

        // saved as the 5b-06 measurement case: effects on, depth 1, sway on (it moves in play mode only)
        effects.Set(true);
        mood.Set(1);
        rig.Apply();

        Directory.CreateDirectory("Assets/Scenes");
        EditorSceneManager.SaveScene(scene, ScenePath);
        AssetDatabase.SaveAssets();
        return "depth test scene built: " + ScenePath + ", " + swaps.Count + " sprite swaps, " + flames.Count + " flames, renderer index " + rendererIndex;
    }

    /// <summary>The result of <see cref="ComposeWorld"/>.</summary>
    internal struct World { public DepthSwitch effects; public DepthMood mood; public int windowSwap; public SpriteRenderer window; public Light2D moon; public int swapCount, flameCount; }

    /// <summary>
    /// Puts the workshop (already built by SceneBuilder.BuildWorkshop under world) in depth: wall with overscan, window,
    /// table mesh, candles, dust, fog sheets, foreground corners, the effects switch and the five moods. Shared by the depth
    /// test scene and the main scene. tilt may be null.
    /// </summary>
    internal static World ComposeWorld(DepthRig rig, Transform world, Transform workshop, LayoutData layout, ArtLibrary lib,
        Material tableMat, Material fogMat, Light2D ambient, Volume volume, TiltShift tilt)
    {
        var swaps = new List<DepthSwitch.SpriteSwap>();
        var flames = new List<DepthSwitch.FlameSwap>();

        var wall = workshop.Find("workshop_wall").GetComponent<SpriteRenderer>();
        wall.sortingOrder = WallOrder;
        var wallSoft = Art("workshop_wall_far");
        swaps.Add(new DepthSwitch.SpriteSwap { target = wall, sharp = wall.sprite, soft = wallSoft });
        Leaf(wall.gameObject, WallZ);
        // mirrored overscan round the wall, so a swayed camera never sees past its edge (the SPEC's 96 x 54 margin)
        for (int dy = -1; dy <= 1; dy++)
            for (int dx = -1; dx <= 1; dx++)
            {
                if (dx == 0 && dy == 0) continue;
                var copy = Make.Sprite(workshop, "workshop_wall", wall.transform.position + new Vector3(dx * Px.W, dy * Px.H, 0), WallOrder - 1, lib.lit);
                copy.name = "wall overscan " + dx + "," + dy; copy.flipX = dx != 0; copy.flipY = dy != 0;
                swaps.Add(new DepthSwitch.SpriteSwap { target = copy, sharp = copy.sprite, soft = wallSoft });
                Leaf(copy.gameObject, WallZ);
            }

        var window = workshop.Find("window").GetComponent<SpriteRenderer>();
        window.sortingOrder = WindowOrder;
        int windowSwap = swaps.Count;
        swaps.Add(new DepthSwitch.SpriteSwap { target = window, sharp = window.sprite, soft = window.sprite });
        Leaf(window.gameObject, WindowZ);

        var placedTable = layout.Placed("workshop_table");
        var tableGo = workshop.Find("workshop_table").gameObject;
        var tableSprite = tableGo.GetComponent<SpriteRenderer>().sprite;
        Object.DestroyImmediate(tableGo.GetComponent<SpriteRenderer>());
        tableGo.AddComponent<MeshFilter>();
        var tableRenderer = tableGo.AddComponent<MeshRenderer>();
        tableRenderer.sharedMaterial = tableMat; tableRenderer.sortingOrder = 2;
        tableRenderer.shadowCastingMode = ShadowCastingMode.Off; tableRenderer.receiveShadows = false;
        var table = tableGo.AddComponent<DepthTable>();
        table.sprite = tableSprite; table.artX = placedTable.x; table.artY = placedTable.y;

        var moon = workshop.Find("Moonbeam").GetComponent<Light2D>();
        Leaf(moon.gameObject, WindowZ);

        var candleSoft = Art("candle_far");
        var flameSoft = Enumerable.Range(0, 8).Select(k => Art("flame_small_far_" + k)).ToArray();
        for (int i = 0; i < layout.candles.Length; i++)
        {
            var c = layout.candles[i];
            bool onWall = c.y < 120;
            var root = workshop.Find("Candle " + i);
            float z = onWall ? WallCandleZ : DepthMath.TableZ(c.y + 19) + 0.5f;
            foreach (Transform leaf in root)
            {
                Leaf(leaf.gameObject, z);
                var sr = leaf.GetComponent<SpriteRenderer>();
                if (sr == null || !onWall) continue;
                var flame = leaf.GetComponent<Flame>();
                if (flame != null) { sr.sortingOrder = WallFlameOrder; flames.Add(new DepthSwitch.FlameSwap { flame = flame, sharp = flame.frames, soft = flameSoft }); }
                else if (sr.sprite != null && sr.sprite.name == "candle") { sr.sortingOrder = WallCandleOrder; swaps.Add(new DepthSwitch.SpriteSwap { target = sr, sharp = sr.sprite, soft = candleSoft }); }
                else sr.sortingOrder = WallFlameOrder;   // the halo
            }
        }
        Leaf(workshop.Find("Dust").gameObject, DustZ);

        // effects: the fog sheets and the foreground corners
        var backFog = FogSheet(world, "Fog Back", "fog_back", -96, 130, 832, 150, BackFogOrder, 4f, BackFogZ, fogMat);
        var frontFog = FogSheet(world, "Fog Front", "fog_front", -96, 150, 832, 110, FrontFogOrder, -6f, FrontFogZ, fogMat);
        // SPEC: left piece art x -96..144, right 514..736, y 214..306; the pictures carry 6 px of blur margin each side
        var fgLeft = Make.Sprite(world, "dot", Px.Mid(-96, 214, 240, 92), ForegroundOrder, lib.lit);
        fgLeft.name = "Foreground Left"; fgLeft.sprite = Art("fg_corner_left_near"); Leaf(fgLeft.gameObject, ForegroundZ);
        var fgRight = Make.Sprite(world, "dot", Px.Mid(514, 214, 222, 92), ForegroundOrder, lib.lit);
        fgRight.name = "Foreground Right"; fgRight.sprite = Art("fg_corner_right_near"); Leaf(fgRight.gameObject, ForegroundZ);


        var effects = rig.gameObject.AddComponent<DepthSwitch>();
        effects.sprites = swaps.ToArray(); effects.flames = flames.ToArray();
        effects.effectObjects = new[] { backFog.gameObject, frontFog.gameObject, fgLeft.gameObject, fgRight.gameObject };
        effects.effectBehaviours = tilt != null ? new Behaviour[] { tilt } : new Behaviour[0];

        var mood = rig.gameObject.AddComponent<DepthMood>();
        mood.ambient = ambient; mood.moon = moon; mood.volume = volume; mood.backFog = backFog; mood.frontFog = frontFog;
        mood.effects = effects; mood.windowSwap = windowSwap; mood.windowNight = window.sprite;
        mood.windowSharp = Enumerable.Range(1, 5).Select(d => lib.Sprite("window_d" + d)).ToArray();
        mood.windowSoft = Enumerable.Range(1, 5).Select(d => Art("window_d" + d + "_far")).ToArray();

        return new World { effects = effects, mood = mood, windowSwap = windowSwap, window = window, moon = moon, swapCount = swaps.Count, flameCount = flames.Count };
    }

    /// <summary>The same, from the command line.</summary>
    public static void BuildBatch() => Debug.Log(Build());

    internal static Sprite Art(string name)
    {
        var s = AssetDatabase.LoadAssetAtPath<Sprite>(DepthArt + name + ".png");
        if (s == null) throw new FileNotFoundException("no depth sprite " + DepthArt + name + ".png");
        return s;
    }

    /// <summary>Puts one object (whose parents sit at the origin, unscaled) at depth z.</summary>
    internal static void Leaf(GameObject go, float z) => DepthLayer.Add(go, z);

    internal static SpriteRenderer FogSheet(Transform parent, string name, string sprite, float x, float y, float w, float h, int order, float speed, float z, Material mat)
    {
        var r = Make.Sprite(parent, "dot", Px.Mid(x, y, w, h), order, mat);
        r.name = name; r.sprite = Art(sprite);
        r.drawMode = SpriteDrawMode.Tiled; r.tileMode = SpriteTileMode.Continuous; r.size = new Vector2(w, h);
        // the swap from the 4 px "dot" to the fog picture rescales the transform by their size ratio (80 x 27.5 for the
        // front sheet, filling the frame with blocks): the tiled size alone sets the sheet's extent
        r.transform.localScale = Vector3.one;
        var sheet = r.gameObject.AddComponent<FogSheet>();
        sheet.speed = speed; sheet.width = (int)r.sprite.rect.width;
        Leaf(r.gameObject, z);
        return r;
    }

    /// <summary>A static copy of the bench HUD's panels (the Hud prefab, BenchView's rack) and a few of its labels, on the UI layer.</summary>
    static void BuildHud(Transform parent)
    {
        const int O = 100;
        var hud = Make.Root("HUD", parent);
        Make.SpriteAt(hud, "hud_left", 8, 8, O);
        Make.SpriteAt(hud, "hud_right", 480, 8, O);
        for (int i = 0; i < 6; i++) Make.SpriteAt(hud, "socket_filled", 488 + i * 35, 24, O + 1);
        for (int i = 0; i < 3; i++) Make.SpriteAt(hud, "aspect_socket", 9 + i * 29, 98, O + 1);
        Make.SpriteAt(hud, "note", 4, 262, O);
        Make.SpriteAt(hud, "corner", 514, 284, O);
        Make.SpriteAt(hud, "book", 520, 290, O + 2);
        Make.SpriteAt(hud, "ignite_up", 519, 310, O + 1);
        Make.SpriteAt(hud, "rack", 130, 256, O);
        var dim = Px.Hex("#a08868"); var dark = Px.Hex("#0c0706");
        Make.TextKey(hud, "ui.hud.potency", 17, 52, FontKind.Tiny, 8, dim, O + 3);
        Make.TextKey(hud, "ui.hud.catalysts", 489, 14, FontKind.Tiny, 8, dim, O + 3);
        Make.TextKey(hud, "ui.hud.last_reaction", 10, 266, FontKind.Tiny, 8, Px.Hex("#7a5430"), O + 3);
        Make.TextKey(hud, "ui.hud.journal", 542, 289, FontKind.Title, 16, Px.Hex("#f0dcc0"), O + 3);
        Make.TextKey(hud, "ui.hud.ignite", 575, 306, FontKind.Title, 32, Px.Hex("#fff6b8"), O + 4, Align.Centre, Px.Hex("#3b0d1e"));
        Make.TextKey(hud, "ui.bench.redraw", 594, 88, FontKind.Title, 16, Px.Hex("#f0dcc0"), O + 4, Align.Centre, dark);
        foreach (var t in hud.GetComponentsInChildren<Transform>(true)) t.gameObject.layer = DepthRig.UiLayer;
    }

    internal static Material EnsureMaterial(string path, string shaderName)
    {
        var shader = Shader.Find(shaderName);
        if (shader == null) throw new System.InvalidOperationException("shader not found: " + shaderName);
        var m = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (m == null) { m = new Material(shader); AssetDatabase.CreateAsset(m, path); }
        else if (m.shader != shader) m.shader = shader;
        EditorUtility.SetDirty(m);
        return m;
    }

    internal static VolumeProfile BuildVolume()
    {
        AssetDatabase.DeleteAsset(VolumePath);
        var profile = ScriptableObject.CreateInstance<VolumeProfile>();
        AssetDatabase.CreateAsset(profile, VolumePath);
        T Add<T>() where T : VolumeComponent
        {
            var c = profile.Add<T>(true);
            c.name = typeof(T).Name; c.hideFlags = HideFlags.HideInInspector | HideFlags.HideInHierarchy;
            AssetDatabase.AddObjectToAsset(c, profile);
            return c;
        }
        // today's single setting (SceneBuilder.BuildVolume); DepthMood sets the per-depth values
        var bloom = Add<Bloom>();
        bloom.threshold.value = 1.0f; bloom.intensity.value = 1.1f; bloom.scatter.value = 0.62f;
        var vignette = Add<Vignette>();
        vignette.intensity.value = 0.34f; vignette.smoothness.value = 0.5f; vignette.color.value = Color.black;
        Add<Tonemapping>().mode.value = TonemappingMode.None;
        EditorUtility.SetDirty(profile);
        return profile;
    }

    /// <summary>
    /// A copy of the main 2D renderer with the two tilt-shift passes, listed in the pipeline asset after the main one
    /// (which stays the default and keeps no feature). Returns the copy's index in the pipeline's renderer list.
    /// </summary>
    internal static int EnsureRenderer(Material tilt)
    {
        var data = AssetDatabase.LoadAssetAtPath<Renderer2DData>(RendererPath);
        if (data == null)
        {
            if (!AssetDatabase.CopyAsset(MainRenderer, RendererPath)) throw new IOException("cannot copy " + MainRenderer);
            data = AssetDatabase.LoadAssetAtPath<Renderer2DData>(RendererPath);
        }
        var features = data.rendererFeatures.OfType<DepthTiltShiftFeature>().ToList();
        if (features.Count != 2)
        {
            var so = new SerializedObject(data);
            var list = so.FindProperty("m_RendererFeatures"); var map = so.FindProperty("m_RendererFeatureMap");
            foreach (var old in data.rendererFeatures.Where(f => f != null).ToList()) AssetDatabase.RemoveObjectFromAsset(old);
            list.ClearArray(); map.ClearArray();
            features.Clear();
            for (int pass = 0; pass < 2; pass++)
            {
                var f = ScriptableObject.CreateInstance<DepthTiltShiftFeature>();
                f.name = pass == 0 ? "Tilt Shift Horizontal" : "Tilt Shift Vertical";
                f.hideFlags = HideFlags.HideInHierarchy;
                AssetDatabase.AddObjectToAsset(f, data);
                AssetDatabase.TryGetGUIDAndLocalFileIdentifier(f, out string _, out long id);
                list.InsertArrayElementAtIndex(pass); list.GetArrayElementAtIndex(pass).objectReferenceValue = f;
                map.InsertArrayElementAtIndex(pass); map.GetArrayElementAtIndex(pass).longValue = id;
                features.Add(f);
            }
            so.ApplyModifiedPropertiesWithoutUndo();
        }
        for (int pass = 0; pass < 2; pass++)
        {
            var f = features[pass];
            f.injectionPoint = FullScreenPassRendererFeature.InjectionPoint.BeforeRenderingPostProcessing;
            f.fetchColorBuffer = true; f.requirements = ScriptableRenderPassInput.None; f.bindDepthStencilAttachment = false;
            f.passMaterial = tilt; f.passIndex = pass;
            f.SetActive(true);
            EditorUtility.SetDirty(f);
        }
        data.SetDirty(); EditorUtility.SetDirty(data);

        var pipeline = AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(PipelinePath);
        var pso = new SerializedObject(pipeline);
        var renderers = pso.FindProperty("m_RendererDataList");
        for (int i = 0; i < renderers.arraySize; i++)
            if (renderers.GetArrayElementAtIndex(i).objectReferenceValue == data) return i;
        int index = renderers.arraySize;
        renderers.InsertArrayElementAtIndex(index);
        renderers.GetArrayElementAtIndex(index).objectReferenceValue = data;
        pso.ApplyModifiedPropertiesWithoutUndo();
        EditorUtility.SetDirty(pipeline);
        AssetDatabase.SaveAssets();
        return index;
    }
}
