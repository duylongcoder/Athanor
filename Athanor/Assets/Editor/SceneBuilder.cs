using System.Collections.Generic;
using System.IO;
using System.Linq;
using Athanor.Game;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Builds Assets/Scenes/Main.unity and the art library from the exported sprites. Run it again after
/// regenerating art (node tools/art/export.mjs) or exporting effect sheets. Hand edits to the scene are overwritten.
/// </summary>
public static class SceneBuilder
{
    const string ScenePath = "Assets/Scenes/Main.unity", LibraryPath = "Assets/Art/ArtLibrary.asset",
        VolumePath = "Assets/Settings/AthanorVolume.asset", GlowPath = "Assets/Art/Shaders/Glow.mat",
        UrpMaterials = "Packages/com.unity.render-pipelines.universal/Runtime/Materials/";

    [MenuItem("Athanor/Build Scene")]
    public static string Build()
    {
        var lib = BuildLibrary();
        Make.Lib = lib;
        var layout = JsonUtility.FromJson<LayoutData>(File.ReadAllText("Assets/Resources/layout.json"));
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // camera: 640x360 pixels, scaled up by whole numbers
        var camGo = new GameObject("Main Camera") { tag = "MainCamera" };
        camGo.transform.position = new Vector3(0, 0, -10);
        var cam = camGo.AddComponent<Camera>();
        cam.orthographic = true; cam.orthographicSize = Px.H / 2f; cam.nearClipPlane = 0.1f; cam.farClipPlane = 50;
        cam.clearFlags = CameraClearFlags.SolidColor; cam.backgroundColor = Px.Hex("#0d0a12");
        camGo.AddComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;
        var ppc = camGo.AddComponent<PixelPerfectCamera>();
        ppc.assetsPPU = 1; ppc.refResolutionX = Px.W; ppc.refResolutionY = Px.H;
        ppc.gridSnapping = PixelPerfectCamera.GridSnapping.UpscaleRenderTexture;
        ppc.cropFrame = PixelPerfectCamera.CropFrame.Windowbox;

        var volume = new GameObject("Post Processing").AddComponent<Volume>();
        volume.isGlobal = true; volume.sharedProfile = BuildVolume();

        var global = new GameObject("Ambient Light").AddComponent<Light2D>();
        global.lightType = Light2D.LightType.Global; global.color = new Color(0.52f, 0.52f, 1f); global.intensity = 0.25f;

        var workshop = BuildWorkshop(layout);
        var cutscene = BuildCutsceneRoom();
        cutscene.gameObject.SetActive(false);

        var game = new GameObject("Game");
        game.AddComponent<PointerInput>();
        game.AddComponent<Sparks>();
        var flow = game.AddComponent<GameFlow>();
        flow.library = lib; flow.workshop = workshop.gameObject; flow.cutsceneRoom = cutscene.gameObject; flow.mainCamera = cam;

        Directory.CreateDirectory("Assets/Scenes");
        EditorSceneManager.SaveScene(scene, ScenePath);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
        AssetDatabase.SaveAssets();
        return "scene built: " + lib.sprites.Length + " sprites, " + lib.sheets.Length + " effect sheets";
    }

    /// <summary>The same, from the command line: unity run Athanor -- -executeMethod SceneBuilder.BuildBatch -quit</summary>
    public static void BuildBatch() => Debug.Log(Build());

    /// <summary>Re-collects sprites and effect sheets into the art library (after new art or effects are imported).</summary>
    public static ArtLibrary RefreshLibrary() { var lib = BuildLibrary(); AssetDatabase.SaveAssets(); return lib; }

    static ArtLibrary BuildLibrary()
    {
        var lib = AssetDatabase.LoadAssetAtPath<ArtLibrary>(LibraryPath);
        if (lib == null) { lib = ScriptableObject.CreateInstance<ArtLibrary>(); AssetDatabase.CreateAsset(lib, LibraryPath); }
        var sheets = AssetDatabase.FindAssets("t:FlipbookSheet", new[] { "Assets/Art" })
            .Select(g => AssetDatabase.LoadAssetAtPath<FlipbookSheet>(AssetDatabase.GUIDToAssetPath(g))).Where(s => s != null).ToArray();
        var sheetTextures = new HashSet<Texture>(sheets.Where(s => s.frames.Length > 0 && s.frames[0] != null).Select(s => (Texture)s.frames[0].texture));
        lib.sprites = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets/Art" })
            .Select(g => AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(g)))
            .Where(s => s != null && !sheetTextures.Contains(s.texture)).OrderBy(s => s.name).ToArray();
        lib.sheets = sheets;
        lib.title = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Fonts/Alagard.asset");
        lib.body = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Fonts/BitPotion.asset");
        lib.label = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Fonts/PixelOperator-Bold.asset");
        lib.tiny = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Fonts/PixelOperator8.asset");
        lib.lit = AssetDatabase.LoadAssetAtPath<Material>(UrpMaterials + "Sprite-Lit-Default.mat");
        lib.unlit = AssetDatabase.LoadAssetAtPath<Material>(UrpMaterials + "Sprite-Unlit-Default.mat");
        var glow = AssetDatabase.LoadAssetAtPath<Material>(GlowPath);
        if (glow == null) { glow = new Material(Shader.Find("Athanor/Sprite Glow")); AssetDatabase.CreateAsset(glow, GlowPath); }
        lib.glow = glow;
        EditorUtility.SetDirty(lib);
        return lib;
    }

    static VolumeProfile BuildVolume()
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
        var bloom = Add<Bloom>();
        bloom.threshold.value = 1.0f; bloom.intensity.value = 1.1f; bloom.scatter.value = 0.62f;
        var vignette = Add<Vignette>();
        vignette.intensity.value = 0.34f; vignette.smoothness.value = 0.5f; vignette.color.value = Color.black;
        Add<Tonemapping>().mode.value = TonemappingMode.None;
        EditorUtility.SetDirty(profile);
        return profile;
    }

    static Transform BuildWorkshop(LayoutData layout)
    {
        var root = Make.Root("Workshop");
        Make.SpriteAt(root, "workshop_wall", 0, 0, 0, Make.Lib.lit);
        var pane = layout.Placed("window");
        Make.SpriteAt(root, "window", pane.x, pane.y, 1);
        var table = layout.Placed("workshop_table");
        Make.SpriteAt(root, "workshop_table", table.x, table.y, 2, Make.Lib.lit);

        var moon = new GameObject("Moonbeam").AddComponent<Light2D>();
        moon.transform.SetParent(root, false);
        moon.lightType = Light2D.LightType.Sprite; moon.lightCookieSprite = Make.Lib.Sprite("moon_shaft");
        moon.color = new Color(0.42f, 0.6f, 1f); moon.intensity = 0.62f;

        var frames = Enumerable.Range(0, 8).Select(k => Make.Lib.Sprite("flame_small_" + k)).ToArray();
        for (int i = 0; i < layout.candles.Length; i++)
        {
            var c = layout.candles[i];
            bool onWall = c.y < 120;
            var candle = Make.Root("Candle " + i, root);
            Make.SpriteAt(candle, "candle", c.x, c.y, onWall ? 3 : 5, Make.Lib.lit);
            var wick = Px.At(c.x + 6, c.y + 1);
            var flame = Make.SpriteAt(candle, "flame_small_0", c.x, c.y - 8, onWall ? 4 : 6, Make.Lib.glow).gameObject.AddComponent<Flame>();
            flame.frames = frames; flame.body = flame.GetComponent<SpriteRenderer>(); flame.body.color = new Color(1.6f, 1.6f, 1.6f, 1); flame.phase = i * 1.3f;
            flame.halo = Make.Glow(candle, wick + new Vector3(0, 4, 0), 30, new Color(1f, 0.6f, 0.23f, 0.5f), onWall ? 4 : 6);
            flame.haloAlpha = 0.55f;
            flame.glowLight = Make.Light(candle, wick + new Vector3(0, 2, 0), onWall ? 170 : 210, new Color(1f, 0.63f, 0.33f), 1, 0.8f);
            flame.lightIntensity = onWall ? 1.2f : 1.55f;
        }
        var dust = Make.Root("Dust", root).gameObject.AddComponent<Dust>();
        dust.material = Make.Lib.glow; dust.sprite = Make.Lib.Sprite("dot");
        return root;
    }

    static Transform BuildCutsceneRoom()
    {
        var root = Make.Root("Cutscene Room");
        Make.SpriteAt(root, "cutscene_room", 0, 0, 300, Make.Lib.lit);
        Make.SpriteAt(root, "logs", 260, 282, 301, Make.Lib.lit);
        Make.SpriteAt(root, "cauldron", 220, 118, 303, Make.Lib.lit);
        var frames = Enumerable.Range(0, 8).Select(k => Make.Lib.Sprite("flame_big_" + k)).ToArray();
        for (int i = 0; i < 7; i++)
        {
            float x = 272 + i * 16 + Mathf.Sin(i * 3) * 3, y = 290 - (i % 2) * 3;
            var flame = Make.SpriteAt(root, "flame_big_0", Mathf.Round(x) - 10, y - 17, 304, Make.Lib.glow).gameObject.AddComponent<Flame>();
            flame.name = "Fire " + i;
            flame.frames = frames; flame.body = flame.GetComponent<SpriteRenderer>(); flame.body.color = new Color(1.6f, 1.6f, 1.6f, 1); flame.phase = i * 1.3f;
            if (i % 2 == 0) { flame.halo = Make.Glow(root, Px.At(x, y - 8), 60, new Color(1f, 0.6f, 0.23f, 0.4f), 304); flame.haloAlpha = 0.4f; }
        }
        var fire = Make.Light(root, Px.At(320, 285), 250, new Color(1f, 0.57f, 0.27f), 1.4f, 0.62f);
        fire.name = "Fire Light";
        return root;
    }
}
