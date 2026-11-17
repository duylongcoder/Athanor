using System.IO;
using Athanor.Game;
using TMPro;
using UnityEngine;

/// <summary>Renders a GameObject alone (in an otherwise empty scene) to a 640x360 image, the way the UI camera draws it.</summary>
public static class ViewCapture
{
    public static Texture2D Render(GameObject root)
    {
        bool wasActive = root.activeSelf;
        root.SetActive(true);
        foreach (var t in root.GetComponentsInChildren<TMP_Text>(true)) t.ForceMeshUpdate();
        var camGo = new GameObject("phase6 capture camera");
        var cam = camGo.AddComponent<Camera>();
        cam.orthographic = true; cam.orthographicSize = Px.H / 2f;
        cam.transform.position = new Vector3(0, 0, -10);
        cam.clearFlags = CameraClearFlags.SolidColor; cam.backgroundColor = Color.black;
        cam.cullingMask = ~(1 << DepthRig.WorldLayer);
        var rt = RenderTexture.GetTemporary(Px.W, Px.H, 24, RenderTextureFormat.ARGB32);
        cam.targetTexture = rt;
        cam.Render();                       // warm-up: first draw uploads textures and shaders
        cam.Render(); cam.targetTexture = null;
        var previous = RenderTexture.active; RenderTexture.active = rt;
        var tex = new Texture2D(Px.W, Px.H, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, Px.W, Px.H), 0, 0); tex.Apply();
        RenderTexture.active = previous; RenderTexture.ReleaseTemporary(rt);
        Object.DestroyImmediate(camGo);
        root.SetActive(wasActive);
        return tex;
    }

    public static void Save(Texture2D tex, string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllBytes(path, tex.EncodeToPNG());
    }

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
