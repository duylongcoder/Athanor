using System.IO;
using Athanor.Game;
using TMPro;
using UnityEngine;

/// <summary>Renders a GameObject alone (in an otherwise empty scene) to a 640x360 image, the way the UI camera draws it.</summary>
public static class ViewCapture
{
    /// <summary>
    /// Draws root to a 640x360 texture. Root and every hidden object under it are made active for the shot, then restored: a view
    /// that hides a part until it is needed (the goal toast's panel) is still compared whole.
    /// </summary>
    public static Texture2D Render(GameObject root)
    {
        var hidden = new System.Collections.Generic.List<GameObject>();
        foreach (var t in root.GetComponentsInChildren<Transform>(true)) if (!t.gameObject.activeSelf) hidden.Add(t.gameObject);
        foreach (var g in hidden) g.SetActive(true);
        GameObject camGo = null;
        RenderTexture rt = null;
        var previous = RenderTexture.active;
        try
        {
            foreach (var t in root.GetComponentsInChildren<TMP_Text>(true)) t.ForceMeshUpdate();
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
            for (int i = hidden.Count - 1; i >= 0; i--) hidden[i].SetActive(false);
        }
    }

    /// <summary>Writes tex as a PNG at path, creating the folder.</summary>
    public static void Save(Texture2D tex, string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllBytes(path, tex.EncodeToPNG());
    }

    /// <summary>Reads a PNG written by Save back into a texture.</summary>
    public static Texture2D Load(string path)
    {
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
