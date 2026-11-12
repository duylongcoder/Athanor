using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Athanor.Game;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEngine;

/// <summary>
/// Turns each pixelfx sheet in Assets/Art/Fx/Sheets (name.png + name.json, written by tools/fx/export.sh)
/// into sliced sprites and a FlipbookSheet asset, then adds them to the art library so Fx.Spawn(name) can play them.
/// </summary>
public static class FlipbookImporter
{
    public const string Dir = "Assets/Art/Fx/Sheets";

    // the parts of a pixelfx sheet's JSON that are used
    [Serializable] class Box { public int x, y, w, h; }
    [Serializable] class Frame { public Box frame; public int duration; }
    [Serializable] class Info { public bool loop; public float[] pivot; }
    [Serializable] class Meta { public Info pixelfx; }
    [Serializable] class Sheet { public Frame[] frames; public Meta meta; }

    [MenuItem("Athanor/Import Effect Sheets")]
    public static string Import()
    {
        if (!Directory.Exists(Dir)) return "No effect sheets found. Run tools/fx/export.sh first.";
        AssetDatabase.Refresh();
        var done = new List<string>();
        foreach (var jsonPath in Directory.GetFiles(Dir, "*.json").OrderBy(p => p))
        {
            string name = Path.GetFileNameWithoutExtension(jsonPath), png = Dir + "/" + name + ".png";
            var data = JsonUtility.FromJson<Sheet>(File.ReadAllText(jsonPath));
            var importer = AssetImporter.GetAtPath(png) as TextureImporter;
            if (importer == null || data == null || data.frames == null || data.frames.Length == 0) { Debug.LogWarning("Skipped effect sheet " + name); continue; }
            importer.GetSourceTextureWidthAndHeight(out _, out int height);

            // slice: the JSON counts y down from the top, Unity counts up from the bottom
            var pivot = data.meta != null && data.meta.pixelfx != null ? data.meta.pixelfx.pivot : null;
            var rects = new List<SpriteRect>();
            for (int i = 0; i < data.frames.Length; i++)
            {
                var f = data.frames[i].frame;
                rects.Add(new SpriteRect
                {
                    name = name + "_" + i.ToString("000"), spriteID = GUID.Generate(),
                    rect = new Rect(f.x, height - f.y - f.h, f.w, f.h), alignment = SpriteAlignment.Custom,
                    pivot = pivot != null && pivot.Length >= 2 ? new Vector2(pivot[0] / f.w, 1 - pivot[1] / f.h) : new Vector2(0.5f, 0.5f),
                });
            }
            importer.spriteImportMode = SpriteImportMode.Multiple;
            var factory = new SpriteDataProviderFactories();
            factory.Init();
            var provider = factory.GetSpriteEditorDataProviderFromObject(importer);
            provider.InitSpriteEditorDataProvider();
            provider.SetSpriteRects(rects.ToArray());
            provider.GetDataProvider<ISpriteNameFileIdDataProvider>().SetNameFileIdPairs(rects.Select(r => new SpriteNameFileIdPair(r.name, r.spriteID)));
            provider.Apply();
            importer.SaveAndReimport();

            string assetPath = Dir + "/" + name + ".asset";
            var sheet = AssetDatabase.LoadAssetAtPath<FlipbookSheet>(assetPath);
            if (sheet == null) { sheet = ScriptableObject.CreateInstance<FlipbookSheet>(); AssetDatabase.CreateAsset(sheet, assetPath); }
            sheet.frames = AssetDatabase.LoadAllAssetsAtPath(png).OfType<Sprite>().OrderBy(s => s.name).ToArray();
            sheet.durations = data.frames.Select(f => Mathf.Max(1, f.duration) / 1000f).ToArray();
            sheet.loop = data.meta != null && data.meta.pixelfx != null && data.meta.pixelfx.loop;
            EditorUtility.SetDirty(sheet);
            done.Add(name + " (" + sheet.frames.Length + " frames)");
        }
        AssetDatabase.SaveAssets();
        var lib = SceneBuilder.RefreshLibrary();
        string report = "Imported " + done.Count + " effect sheets: " + string.Join(", ", done) + ". Library now has " + lib.sheets.Length + " sheets.";
        Debug.Log(report);
        return report;
    }
}
