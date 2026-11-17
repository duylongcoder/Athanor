using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Athanor.Core;
using Athanor.Game;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Object = UnityEngine.Object;

/// <summary>
/// Renders the 5b-05 evidence of the depth test scene to PNG from the editor (cameras rendered into textures):
/// the main scene's bench against the depth scene's rest frame (with pixel diffs), the five presets, the sway extremes, a
/// shake, the tilt-shift band, and Light2D / moonbeam / bloom toggled under both cameras. Nothing is saved to any scene.
/// Command line: unity run Athanor -- -executeMethod DepthTestEvidence.CaptureBatch -quit
/// </summary>
public static class DepthTestEvidence
{
    const string Out = "../production/qa/evidence/phase5/5b-05";
    const int W = Px.W, H = Px.H;
    // tiles 0, 4 and 8 hold a material in every frame, so the tiles show
    static readonly (int tile, string id, string glow)[] Placed = { (0, "ember_coal", "#ff6a1e"), (4, "moondust", "#7cc0f8"), (8, "iron_ore", "") };

    /// <summary>Batch entry: exits 1 on any failure.</summary>
    public static void CaptureBatch()
    {
        try { Debug.Log(Capture()); }
        catch (Exception e) { Debug.LogException(e); if (Application.isBatchMode) EditorApplication.Exit(1); }
    }

    [MenuItem("Athanor/Depth Test Evidence")]
    public static string Capture()
    {
        Directory.CreateDirectory(Out);
        var log = new StringBuilder();
        var lib = AssetDatabase.LoadAssetAtPath<ArtLibrary>("Assets/Art/ArtLibrary.asset");
        Make.Lib = lib;
        var layout = JsonUtility.FromJson<LayoutData>(File.ReadAllText("Assets/Resources/layout.json"));

        // ---- the main scene's bench (orthographic, 640x360, as the game renders it before the upscale)
        EditorSceneManager.OpenScene("Assets/Scenes/Main.unity", OpenSceneMode.Single);
        var mainCam = Camera.main;
        var ppc = mainCam.GetComponent<PixelPerfectCamera>(); if (ppc != null) ppc.enabled = false;
        mainCam.orthographicSize = H / 2f;
        var workshop = GameObject.Find("Workshop").transform;
        SetDust(false);
        var mainTiles = new TileView[layout.tiles.Length];
        var tileRoot = Make.Root("Tiles", workshop);
        for (int i = 0; i < mainTiles.Length; i++) { mainTiles[i] = new GameObject("Tile " + i).AddComponent<TileView>(); mainTiles[i].Build(i, layout.tiles[i], tileRoot); }
        ShowMaterials(mainTiles);
        var mainData = mainCam.GetComponent<UniversalAdditionalCameraData>();
        var main1 = Render(mainCam, W, H);
        mainData.renderPostProcessing = false;
        var main1NoPost = Render(mainCam, W, H);
        mainData.renderPostProcessing = true;
        Save(main1, "01-main-bench-1x.png");
        Save(Up(main1, 2), "01-main-bench-2x-nearest.png");
        var mainProbe = ProbeLights(() => Render(mainCam, W, H), workshop, AssetDatabase.LoadAssetAtPath<VolumeProfile>("Assets/Settings/AthanorVolume.asset"), "main", log);

        // ---- the depth test scene
        EditorSceneManager.OpenScene(DepthTestBuilder.ScenePath, OpenSceneMode.Single);
        var rig = Object.FindFirstObjectByType<DepthRig>();
        var effects = rig.GetComponent<DepthSwitch>();
        var mood = rig.GetComponent<DepthMood>();
        var controls = rig.GetComponent<DepthTestControls>();
        var tilt = rig.worldCamera.GetComponent<TiltShift>();
        rig.scale = 2; rig.Apply();
        SetDust(false);
        ShowMaterials(controls.tiles);
        effects.Set(false); mood.Set(0); rig.SetView(Vector2.zero, Vector2.zero);
        ForceText();

        var depth1 = RenderWorld(rig, W, H);
        var worldData = rig.worldCamera.GetComponent<UniversalAdditionalCameraData>();
        worldData.renderPostProcessing = false;
        var depth1NoPost = RenderWorld(rig, W, H);
        worldData.renderPostProcessing = true;
        var depth2 = RenderWorld(rig, 2 * W, 2 * H);
        worldData.renderPostProcessing = false;
        var depth2NoPost = RenderWorld(rig, 2 * W, 2 * H);
        worldData.renderPostProcessing = true;
        Save(depth1, "02-depth-rest-effects-off-1x.png");
        Save(depth2, "03-depth-rest-effects-off-2x.png");
        log.AppendLine("== Rest frame: DepthTest (effects off, no sway) against Main.unity's bench (the same three tiles filled, dust off)");
        log.AppendLine(Diff("1x, post on (bloom + vignette)", main1, depth1, 1, "04-diff-1x.png"));
        log.AppendLine(Diff("1x, post off", main1NoPost, depth1NoPost, 1, null));
        log.AppendLine(Diff("2x world texture vs main 1x upscaled nearest, post on", Up(main1, 2), depth2, 2, "05-diff-2x.png"));
        log.AppendLine(Diff("2x world texture vs main 1x upscaled nearest, post off", Up(main1NoPost, 2), depth2NoPost, 2, null));

        var depthProbe = ProbeLights(() => RenderWorld(rig, W, H), rig.transform.Find("World/Workshop"), rig.GetComponent<DepthMood>().volume.sharedProfile, "depth", log);
        log.AppendLine("== Light under the perspective camera vs the orthographic one (mean luma change in the region, 0..255)");
        foreach (var k in mainProbe.Keys) log.AppendLine($"  {k,-34} main {mainProbe[k],7:F2}   depth {depthProbe[k],7:F2}");

        // ---- effects on: the five presets at t = 2 s, through the UI camera (world texture + HUD), 1280x720
        effects.Set(true);
        SetDust(true);
        foreach (var fog in Object.FindObjectsByType<FogSheet>(FindObjectsSortMode.None)) fog.Set(2f);
        for (int d = 1; d <= 5; d++)
        {
            mood.Set(d);
            Save(RenderFull(rig), $"1{d}-effects-on-depth-{d}.png");
        }
        mood.Set(0);
        Save(RenderFull(rig), "10-effects-on-depth-0-todays-lights.png");
        effects.Set(false);
        Save(RenderFull(rig), "06-effects-off-with-hud-2x.png");
        effects.Set(true);

        // ---- the tilt-shift band: which art rows change when only the tilt-shift is turned off
        mood.Set(1);
        var withTilt = RenderWorld(rig, 2 * W, 2 * H);
        tilt.enabled = false;
        var noTilt = RenderWorld(rig, 2 * W, 2 * H);
        tilt.enabled = true;
        log.AppendLine("== Tilt-shift (depth 1, 2x): art rows changed by the pass (expect none inside 145..235)");
        log.AppendLine("  " + RowsChanged(withTilt, noTilt, 2));
        Save(noTilt, "22-depth-1-tilt-shift-off-2x.png");

        // ---- sway extremes and a shake (depth 1, effects on)
        var anchorRest = withTilt;
        foreach (var (name, cam) in new[] { ("23-sway-x+4-y+2", new Vector2(4, 2)), ("24-sway-x-4-y-2", new Vector2(-4, -2)) })
        {
            rig.SetView(cam, Vector2.zero);
            Save(RenderFull(rig), name + ".png");
            var world = RenderWorld(rig, 2 * W, 2 * H);
            log.AppendLine($"== Sway {cam}: " + Diff("world vs rest, 2x", anchorRest, world, 2, null).Trim());
            log.AppendLine("  anchor band art rows 186..190 mean abs change: " + MeanAbs(anchorRest, world, 2, 186, 190).ToString("F2")
                + "; wall rows 20..60: " + MeanAbs(anchorRest, world, 2, 20, 60).ToString("F2"));
        }
        rig.SetView(Vector2.zero, new Vector2(3, -2));
        Save(RenderFull(rig), "25-shake-x3-y-2-world-only.png");
        rig.SetView(Vector2.zero, Vector2.zero);

        mood.Set(1);
        SetDust(false);
        File.WriteAllText(Path.Combine(Out, "report.txt"), log.ToString());
        return "5b-05 evidence written to " + Path.GetFullPath(Out) + "\n" + log;
    }

    // ---------------------------------------------------------------- scene helpers

    static void ShowMaterials(TileView[] tiles)
    {
        foreach (var (tile, id, glow) in Placed) tiles[tile].Show(new MaterialData { id = id, glow = glow }, false);
    }

    static void SetDust(bool on)
    {
        foreach (var dust in Object.FindObjectsByType<Dust>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            dust.gameObject.SetActive(on);
            if (!on) continue;
            // Start and LateUpdate do not run in the editor: call them once, so the motes exist
            if (dust.transform.Find("motes") == null) typeof(Dust).GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(dust, null);
            typeof(Dust).GetMethod("LateUpdate", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(dust, null);
        }
    }

    static void ForceText()
    {
        foreach (var t in Object.FindObjectsByType<TextMeshPro>(FindObjectsSortMode.None)) t.ForceMeshUpdate(true, true);
    }

    /// <summary>Mean luma change (0..255) in a few regions when the moonbeam, the candle lights or the bloom are turned off.</summary>
    static Dictionary<string, float> ProbeLights(Func<Texture2D> render, Transform workshop, VolumeProfile profile, string tag, StringBuilder log)
    {
        var result = new Dictionary<string, float>();
        var baseFrame = render();
        var moon = workshop.Find("Moonbeam").GetComponent<Light2D>();
        moon.enabled = false;
        var noMoon = render();
        moon.enabled = true;
        result["moonbeam off: shaft (240..400,100..250)"] = MeanLumaDelta(baseFrame, noMoon, 240, 100, 400, 250);
        var lights = workshop.GetComponentsInChildren<Flame>(true).Select(f => f.glowLight).Where(l => l != null).ToArray();
        foreach (var l in lights) l.enabled = false;
        var noCandles = render();
        foreach (var l in lights) l.enabled = true;
        result["candle lights off: table candle 0"] = MeanLumaDelta(baseFrame, noCandles, 110, 190, 200, 270);
        result["candle lights off: wall candle 3"] = MeanLumaDelta(baseFrame, noCandles, 170, 60, 250, 130);
        profile.TryGet(out Bloom bloom);
        float was = bloom.intensity.value;
        bloom.intensity.value = 0;
        var noBloom = render();
        bloom.intensity.value = was;
        result["bloom off: whole frame"] = MeanLumaDelta(baseFrame, noBloom, 0, 0, W, H);
        result["bloom off: pixels changed (%)"] = 100f * Changed(baseFrame, noBloom) / (W * H);
        Save(noMoon, $"3{(tag == "main" ? 0 : 5)}-{tag}-moonbeam-off.png");
        Save(noCandles, $"3{(tag == "main" ? 1 : 6)}-{tag}-candle-lights-off.png");
        Save(noBloom, $"3{(tag == "main" ? 2 : 7)}-{tag}-bloom-off.png");
        return result;
    }

    // ---------------------------------------------------------------- rendering

    // captures are HideAndDontSave: OpenScene unloads unreferenced objects, and the main bench is diffed after the depth scene opens
    static Texture2D Render(Camera cam, int w, int h)
    {
        var rt = RenderTexture.GetTemporary(new RenderTextureDescriptor(w, h, RenderTextureFormat.ARGB32, 24) { sRGB = true, msaaSamples = 1 });
        var old = cam.targetTexture;
        cam.targetTexture = rt;
        cam.Render();
        cam.targetTexture = old;
        var tex = new Texture2D(w, h, TextureFormat.RGBA32, false) { hideFlags = HideFlags.HideAndDontSave };
        var active = RenderTexture.active;
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, w, h), 0, 0);
        tex.Apply();
        RenderTexture.active = active;
        RenderTexture.ReleaseTemporary(rt);
        return tex;
    }

    /// <summary>The world camera alone, at any size (its projection does not depend on the target's size).</summary>
    static Texture2D RenderWorld(DepthRig rig, int w, int h)
    {
        var tex = Render(rig.worldCamera, w, h);
        rig.worldCamera.targetTexture = rig.WorldTexture;
        return tex;
    }

    /// <summary>What the player sees at 2x: the world into its texture, then the UI camera (texture + HUD) at 1280x720.</summary>
    static Texture2D RenderFull(DepthRig rig)
    {
        rig.Apply();
        ForceText();
        rig.worldCamera.Render();
        return Render(rig.uiCamera, 2 * W, 2 * H);
    }

    // ---------------------------------------------------------------- pictures

    static void Save(Texture2D tex, string name) => File.WriteAllBytes(Path.Combine(Out, name), tex.EncodeToPNG());

    static Texture2D Up(Texture2D src, int k)
    {
        var s = src.GetPixels32();
        int w = src.width, h = src.height;
        var d = new Color32[w * k * h * k];
        for (int y = 0; y < h * k; y++)
            for (int x = 0; x < w * k; x++) d[y * w * k + x] = s[(y / k) * w + x / k];
        var tex = new Texture2D(w * k, h * k, TextureFormat.RGBA32, false) { hideFlags = HideFlags.HideAndDontSave };
        tex.SetPixels32(d); tex.Apply();
        return tex;
    }

    static int Changed(Texture2D a, Texture2D b)
    {
        var p = a.GetPixels32(); var q = b.GetPixels32();
        int n = 0;
        for (int i = 0; i < p.Length; i++) if (p[i].r != q[i].r || p[i].g != q[i].g || p[i].b != q[i].b) n++;
        return n;
    }

    /// <summary>Counts the differing pixels, the largest channel difference, and where they are (art-px cells of 40 x 40, y down).</summary>
    static string Diff(string label, Texture2D a, Texture2D b, int k, string image)
    {
        var p = a.GetPixels32(); var q = b.GetPixels32();
        int w = a.width, h = a.height, n = 0, over2 = 0, max = 0;
        var cells = new Dictionary<(int, int), int>();
        var red = image == null ? null : new Color32[p.Length];
        for (int i = 0; i < p.Length; i++)
        {
            int dm = Mathf.Max(Mathf.Abs(p[i].r - q[i].r), Mathf.Max(Mathf.Abs(p[i].g - q[i].g), Mathf.Abs(p[i].b - q[i].b)));
            if (red != null) { byte g = (byte)((p[i].r + p[i].g + p[i].b) / 9); red[i] = dm > 0 ? new Color32(255, (byte)Mathf.Max(0, 200 - dm * 8), 0, 255) : new Color32(g, g, g, 255); }
            if (dm == 0) continue;
            n++; if (dm > 2) over2++; max = Mathf.Max(max, dm);
            int ax = (i % w) / k, ay = (h - 1 - i / w) / k;
            var key = (ax / 40 * 40, ay / 40 * 40);
            cells[key] = cells.TryGetValue(key, out int c) ? c + 1 : 1;
        }
        if (image != null) { var t = new Texture2D(w, h, TextureFormat.RGBA32, false) { hideFlags = HideFlags.HideAndDontSave }; t.SetPixels32(red); t.Apply(); Save(t, image); }
        string where = string.Join(", ", cells.OrderByDescending(e => e.Value).Take(6).Select(e => $"x{e.Key.Item1}..{e.Key.Item1 + 39} y{e.Key.Item2}..{e.Key.Item2 + 39}: {e.Value}"));
        return $"  {label}: {n} of {p.Length} pixels differ ({100.0 * n / p.Length:F3} %), {over2} by more than 2 levels, max {max}" + (n > 0 ? "\n    most in " + where : "");
    }

    /// <summary>The art rows (y down) in which any pixel differs, as ranges.</summary>
    static string RowsChanged(Texture2D a, Texture2D b, int k)
    {
        var p = a.GetPixels32(); var q = b.GetPixels32();
        int w = a.width, h = a.height;
        var rows = new bool[H];
        for (int i = 0; i < p.Length; i++)
            if (p[i].r != q[i].r || p[i].g != q[i].g || p[i].b != q[i].b) rows[(h - 1 - i / w) / k] = true;
        var ranges = new List<string>();
        for (int y = 0; y < H; y++)
        {
            if (!rows[y]) continue;
            int s = y; while (y + 1 < H && rows[y + 1]) y++;
            ranges.Add(s == y ? s.ToString() : s + ".." + y);
        }
        return ranges.Count == 0 ? "no row changed" : "rows changed: " + string.Join(", ", ranges);
    }

    static float Luma(Color32 c) => 0.299f * c.r + 0.587f * c.g + 0.114f * c.b;

    /// <summary>Mean luma of a minus b over an art-space rectangle (y down) of a 640x360 frame.</summary>
    static float MeanLumaDelta(Texture2D a, Texture2D b, int x0, int y0, int x1, int y1)
    {
        var p = a.GetPixels32(); var q = b.GetPixels32();
        int w = a.width, h = a.height; double sum = 0; int n = 0;
        for (int y = y0; y < y1; y++)
            for (int x = x0; x < x1; x++) { int i = (h - 1 - y) * w + x; sum += Luma(p[i]) - Luma(q[i]); n++; }
        return (float)(sum / n);
    }

    /// <summary>Mean absolute luma change between two frames of scale k over art rows y0..y1.</summary>
    static float MeanAbs(Texture2D a, Texture2D b, int k, int y0, int y1)
    {
        var p = a.GetPixels32(); var q = b.GetPixels32();
        int w = a.width, h = a.height; double sum = 0; int n = 0;
        for (int y = y0 * k; y < (y1 + 1) * k; y++)
            for (int x = 0; x < w; x++) { int i = (h - 1 - y) * w + x; sum += Mathf.Abs(Luma(p[i]) - Luma(q[i])); n++; }
        return (float)(sum / n);
    }
}
