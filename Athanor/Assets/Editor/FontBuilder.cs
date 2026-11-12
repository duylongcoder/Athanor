using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

/// <summary>Bakes the pixel fonts into TextMeshPro bitmap font assets at their native size (crisp, point-filtered).</summary>
public static class FontBuilder
{
    const string Dir = "Assets/Art/Fonts/";

    [MenuItem("Athanor/Build Fonts")]
    public static string Build()
    {
        var log = new StringBuilder();
        foreach (var (file, size) in new[] { ("Alagard", 16), ("BitPotion", 16), ("PixelOperator-Bold", 16), ("PixelOperator8", 8) })
            log.AppendLine(Bake(file, size));
        AssetDatabase.SaveAssets();
        return log.ToString();
    }

    static string Bake(string file, int size)
    {
        var font = AssetDatabase.LoadAssetAtPath<Font>(Dir + file + ".ttf");
        if (font == null) return file + ": font not found";
        string path = Dir + file + ".asset";
        AssetDatabase.DeleteAsset(path);
        var asset = TMP_FontAsset.CreateFontAsset(font, size, 1, GlyphRenderMode.RASTER, 256, 256, AtlasPopulationMode.Dynamic, false);
        var chars = new StringBuilder();
        for (char c = ' '; c <= '~'; c++) chars.Append(c);
        chars.Append("’×…");
        asset.TryAddCharacters(chars.ToString(), out string missing);
        asset.atlasPopulationMode = AtlasPopulationMode.Static;
        asset.name = file;
        AssetDatabase.CreateAsset(asset, path);
        var tex = asset.atlasTextures[0];
        tex.name = file + " Atlas"; tex.filterMode = FilterMode.Point;
        AssetDatabase.AddObjectToAsset(tex, asset);
        asset.material.name = file + " Material";
        AssetDatabase.AddObjectToAsset(asset.material, asset);
        EditorUtility.SetDirty(asset);
        return file + ": " + asset.characterTable.Count + " characters, line height " + asset.faceInfo.lineHeight + ", ascent " + asset.faceInfo.ascentLine + ", missing '" + missing + "', shader " + asset.material.shader.name;
    }
}
