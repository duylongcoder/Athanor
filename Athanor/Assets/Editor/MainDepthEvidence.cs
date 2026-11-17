using System.IO;
using System.Reflection;
using Athanor.Core;
using Athanor.Game;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Evidence frames of the HD-2D main scene (5b-07a), edit mode, no lights (the editor does not draw point or sprite lights):
/// unity run Athanor -- -executeMethod MainDepthEvidence.CaptureBatch
/// </summary>
public static class MainDepthEvidence
{
    const string Out = "../production/qa/evidence/phase5/5b-07";

    public static void CaptureBatch() => Debug.Log(Capture());

    static Texture2D Render(Camera cam, int w, int h)
    {
        var rt = RenderTexture.GetTemporary(new RenderTextureDescriptor(w, h, RenderTextureFormat.ARGB32, 24) { sRGB = true, msaaSamples = 1 });
        var old = cam.targetTexture;
        cam.targetTexture = rt; cam.Render(); cam.targetTexture = old;
        var tex = new Texture2D(w, h, TextureFormat.RGBA32, false) { hideFlags = HideFlags.HideAndDontSave };
        var active = RenderTexture.active; RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, w, h), 0, 0); tex.Apply();
        RenderTexture.active = active; RenderTexture.ReleaseTemporary(rt);
        return tex;
    }

    static Texture2D Full(DepthRig rig, int k)
    {
        rig.Apply();
        foreach (var t in Object.FindObjectsByType<TextMeshPro>(FindObjectsSortMode.None)) t.ForceMeshUpdate(true, true);
        rig.worldCamera.Render();
        return Render(rig.uiCamera, k * Px.W, k * Px.H);
    }

    static void Save(Texture2D tex, string name) => File.WriteAllBytes(Path.Combine(Out, name), tex.EncodeToPNG());

    public static string Capture()
    {
        Directory.CreateDirectory(Out);
        EditorGlobals.Inject();
        var lib = Art.Lib;
        var layout = JsonUtility.FromJson<LayoutData>(File.ReadAllText("Assets/Resources/layout.json"));
        EditorSceneManager.OpenScene("Assets/Scenes/Main.unity", OpenSceneMode.Single);
        var flow = Object.FindFirstObjectByType<GameFlow>();
        var stage = flow.depthStage; var rig = stage.rig;
        var ppc = rig.uiCamera.GetComponent<PixelPerfectCamera>(); if (ppc != null) ppc.enabled = false;
        foreach (var d in Object.FindObjectsByType<Dust>(FindObjectsSortMode.None)) d.gameObject.SetActive(false);
        var report = new System.Text.StringBuilder();

        // the bench: the real BenchView (tiles in depth groups, HUD, rack) bound to nothing
        var db = new ContentDb(JsonUtility.FromJson<ContentData>(File.ReadAllText("Assets/Resources/content.json")));
        var bench = Make.Root("Bench").gameObject.AddComponent<BenchView>();
        bench.Build(layout, db, null);
        stage.PlaceTiles(bench, layout.tiles.Length);
        string[] ids = { "ember_coal", "moondust", "iron_ore" }; int[] at = { 0, 4, 8 };
        for (int i = 0; i < 3; i++) bench.Tile(at[i]).Show(db.Material(ids[i]), false);
        foreach (var tile in new[] { 0, 4, 8 }) typeof(TileView).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(bench.Tile(tile), null);

        // a real run shows only the catalysts it holds (4 sockets fit the panel; a fifth overlaps): hide the unbound HUD's spare sockets
        var hud = bench.Hud;
        foreach (var f in new[] { "socketLit", "catalystIcon" })
        {
            var arr = (SpriteRenderer[])typeof(HudView).GetField(f, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(hud);
            for (int i = 4; i < arr.Length; i++) arr[i].gameObject.SetActive(false);
        }

        // settle: the first frame after a scene load catches the mood and tile transients, so render and discard two
        stage.ShowMood(1, true); rig.SetView(Vector2.zero, Vector2.zero);
        Full(rig, 1); Full(rig, 1);

        for (int d = 1; d <= 5; d++)
        {
            stage.ShowMood(d, true);
            rig.SetView(Vector2.zero, Vector2.zero);
            Save(Full(rig, 2), "0" + d + "-depth-" + d + "-rest-2x.png");
        }
        stage.ShowMood(3, true);
        rig.SetView(new Vector2(4, 2), Vector2.zero); Save(Full(rig, 2), "06-sway-x+4-y+2-2x.png");
        rig.SetView(new Vector2(-4, -2), Vector2.zero); Save(Full(rig, 2), "07-sway-x-4-y-2-2x.png");
        rig.SetView(Vector2.zero, new Vector2(3, -2)); Save(Full(rig, 2), "08-shake-world-only-2x.png");
        rig.SetView(Vector2.zero, Vector2.zero);
        Save(Full(rig, 1), "09-hud-1x.png");
        Save(Full(rig, 2), "10-hud-2x.png");

        // effects off, post off: the rest frame of the bench (compare 5b-05 frame 03)
        stage.effects.Set(false);
        rig.worldCamera.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
        stage.ShowMood(1, true);
        Save(Full(rig, 2), "11-effects-off-post-off-2x.png");
        stage.effects.Set(true);
        rig.worldCamera.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = true;

        // the Settings panel with the sway switch
        var title = Make.Root("Title").gameObject.AddComponent<TitleView>();
        var settings = new Settings();
        title.Build(settings);
        typeof(TitleView).GetMethod("OpenSettings", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(title, null);
        bench.gameObject.SetActive(false);
        title.gameObject.SetActive(true);
        var root = (Transform)typeof(TitleView).GetField("root", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(title);
        if (root != null) root.gameObject.SetActive(true);
        foreach (var f in typeof(TitleView).GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            if (f.Name == "panel") ((Transform)f.GetValue(title)).gameObject.SetActive(true);
        Save(Full(rig, 2), "12-settings-sway-on-2x.png");
        settings.sway = false;
        typeof(TitleView).GetMethod("RefreshSettings", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(title, null);
        Save(Full(rig, 2), "13-settings-sway-off-2x.png");
        report.AppendLine("frames written to " + Out);
        File.WriteAllText(Path.Combine(Out, "report.txt"), report.ToString());
        return report.ToString();
    }
}
