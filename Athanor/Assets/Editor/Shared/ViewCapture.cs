using System.IO;
using System.Linq;
using Athanor.Game;
using TMPro;
using UnityEngine;

/// <summary>Renders a GameObject alone (in an otherwise empty scene) to a 640x360 image, the way the UI camera draws it.</summary>
public static class ViewCapture
{
    /// <summary>
    /// Draws root to a 640x360 texture. Root and every hidden object under it are made active for the shot, then restored: a view
    /// that hides a part until it is needed (the goal toast's panel) is still compared whole. Objects named in keepHidden stay hidden
    /// (a view with several dialogs that never show together).
    /// </summary>
    public static Texture2D Render(GameObject root, params string[] keepHidden)
    {
        var hidden = new System.Collections.Generic.List<GameObject>();
        foreach (var t in root.GetComponentsInChildren<Transform>(true))
            if (!t.gameObject.activeSelf && (keepHidden == null || System.Array.IndexOf(keepHidden, t.name) < 0)) hidden.Add(t.gameObject);
        foreach (var g in hidden) g.SetActive(true);
        var ranked = new System.Collections.Generic.List<(Renderer r, int order)>();
        GameObject camGo = null;
        RenderTexture rt = null;
        var previous = RenderTexture.active;
        try
        {
            foreach (var t in root.GetComponentsInChildren<TMP_Text>(true)) t.ForceMeshUpdate();
            // Renderers of one sorting order tie (a label and the line it overlaps, a label and its shadow); the engine breaks a tie by
            // instance id, which changes from run to run. For the shot, rank the renderers by (layer, order, hierarchy position) and give each
            // its rank as order: the same picture every time, and the same stacking wherever the orders differ. Restored below.
            int rank = 0;
            foreach (var r in root.GetComponentsInChildren<Renderer>(false)
                         .Select((r, i) => (r, i)).OrderBy(x => SortingLayer.GetLayerValueFromID(x.r.sortingLayerID)).ThenBy(x => x.r.sortingOrder).ThenBy(x => x.i))
            { ranked.Add((r.r, r.r.sortingOrder)); r.r.sortingOrder = rank++; }
            camGo = new GameObject("phase6 capture camera");
            var cam = camGo.AddComponent<Camera>();
            cam.orthographic = true; cam.orthographicSize = Px.H / 2f;
            cam.transform.position = new Vector3(0, 0, -10);
            cam.clearFlags = CameraClearFlags.SolidColor; cam.backgroundColor = Color.black;
            cam.cullingMask = ~(1 << DepthRig.WorldLayer);
            rt = RenderTexture.GetTemporary(Px.W, Px.H, 24, RenderTextureFormat.ARGB32);
            cam.targetTexture = rt;
            cam.Render();                       // warm-up: first draw uploads textures and shaders
            cam.Render(); cam.targetTexture = null;
            RenderTexture.active = rt;
            var tex = new Texture2D(Px.W, Px.H, TextureFormat.RGBA32, false);
            tex.ReadPixels(new Rect(0, 0, Px.W, Px.H), 0, 0); tex.Apply();
            return tex;
        }
        finally
        {
            RenderTexture.active = previous;
            if (rt != null) RenderTexture.ReleaseTemporary(rt);
            if (camGo != null) Object.DestroyImmediate(camGo);
            foreach (var (r, order) in ranked) if (r != null) r.sortingOrder = order;
            for (int i = hidden.Count - 1; i >= 0; i--) hidden[i].SetActive(false);
        }
    }

    /// <summary>
    /// Renders root exactly as it stands: it is made active, and every object under it that is inactive now stays inactive (the
    /// view's own Show / Open decided what is visible). Golden shots use this; Render forces hidden parts on.
    /// </summary>
    public static Texture2D RenderAsShown(GameObject root)
    {
        root.SetActive(true);
        var keep = new System.Collections.Generic.List<string>();
        foreach (var t in root.GetComponentsInChildren<Transform>(true)) if (!t.gameObject.activeSelf) keep.Add(t.name);
        return Render(root, keep.ToArray());
    }

    /// <summary>Writes tex as a PNG at path, creating the folder.</summary>
    public static void Save(Texture2D tex, string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllBytes(path, tex.EncodeToPNG());
    }

    /// <summary>Reads a PNG written by Save back into a texture; null when there is no such file.</summary>
    public static Texture2D Load(string path)
    {
        if (!File.Exists(path)) return null;
        var tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
        tex.LoadImage(File.ReadAllBytes(path));
        return tex;
    }

    /// <summary>Percent of pixels whose RGBA differs at all.</summary>
    public static float Differ(Texture2D a, Texture2D b)
    {
        var pa = a.GetPixels32(); var pb = b.GetPixels32();
        if (pa.Length != pb.Length) return 100f;
        int n = 0;
        for (int i = 0; i < pa.Length; i++)
            if (pa[i].r != pb[i].r || pa[i].g != pb[i].g || pa[i].b != pb[i].b || pa[i].a != pb[i].a) n++;
        return 100f * n / pa.Length;
    }
}
