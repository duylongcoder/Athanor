using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

/// <summary>Bakes the pixel fonts into TextMeshPro bitmap font assets at their native size (crisp, point-filtered).</summary>
public static class FontBuilder
{
    const string Dir = "Assets/Art/Fonts/";

    /// <summary>Batch: unity run Athanor -- -executeMethod FontBuilder.BuildBatch -quit</summary>
    public static void BuildBatch() { Debug.Log(Build()); }

    [MenuItem("Athanor/Build Fonts")]
    public static string Build()
    {
        var log = new StringBuilder();
        foreach (var (file, size) in new[] { ("Alagard", 16), ("BitPotion", 16), ("PixelOperator-Bold", 16), ("PixelOperator8", 8) })
            log.AppendLine(Bake(file, size));
        log.AppendLine(BakeCrux());
        AssetDatabase.SaveAssets();
        return log.ToString();
    }

    /// <summary>The text kinds of Vietnamese: SVN-Coder's Crux, 16 px, RASTER_HINTED (advances round to 6 px), every character both language files use.</summary>
    static string BakeCrux()
    {
        var font = AssetDatabase.LoadAssetAtPath<Font>(Dir + "SVN-Coder's Crux.ttf");
        if (font == null) return "Crux: font not found";
        var set = new SortedSet<char>();
        for (char c = ' '; c <= '~'; c++) set.Add(c);
        foreach (char c in "’×…“”‘’—·•«»°") set.Add(c);
        foreach (char c in "àáâãèéêìíòóôõùúýăđĩũơưạảấầẩẫậắằẳẵặẹẻẽếềểễệỉịọỏốồổỗộớờởỡợụủứừửữựỳỵỷỹ".ToUpperInvariant() + "àáâãèéêìíòóôõùúýăđĩũơưạảấầẩẫậắằẳẵặẹẻẽếềểễệỉịọỏốồổỗộớờởỡợụủứừửữựỳỵỷỹ") set.Add(c);   // all 134 letters, used or not
        foreach (var lang in new[] { "vi", "en" })
        {
            var file = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/" + lang + ".json");
            if (file == null) continue;
            var parsed = Athanor.Gameplay.Lang.Parse(file.text);
            string all = (parsed.name ?? "") + string.Concat(parsed.entries.Select(e => e.text));
            // Screens write many strings in capitals (Loc.Upper), so bake the capitals too: Ộ is not in vi.json until it is uppercased.
            foreach (char c in all + all.ToUpperInvariant())
                if (c >= ' ' && c != '\u2013' && c != '\u00a0') set.Add(c);
        }
        string path = Dir + "Crux.asset";
        AssetDatabase.DeleteAsset(path);
        var asset = TMP_FontAsset.CreateFontAsset(font, 16, 1, GlyphRenderMode.RASTER_HINTED, 512, 512, AtlasPopulationMode.Dynamic, false);
        asset.TryAddCharacters(string.Concat(set), out string missing);
        asset.atlasPopulationMode = AtlasPopulationMode.Static;
        asset.name = "Crux";
        AssetDatabase.CreateAsset(asset, path);
        var tex = asset.atlasTextures[0];
        tex.name = "Crux Atlas"; tex.filterMode = FilterMode.Point;
        AssetDatabase.AddObjectToAsset(tex, asset);
        asset.material.name = "Crux Material";
        AssetDatabase.AddObjectToAsset(asset.material, asset);
        EditorUtility.SetDirty(asset);
        return "Crux: " + asset.characterTable.Count + " characters, atlas " + tex.width + "x" + tex.height + ", line height " + asset.faceInfo.lineHeight + ", missing '" + missing + "'";
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
