using System.Collections.Generic;
using System.Linq;
using Athanor.Game;
using TMPro;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Collects the sprites, effect sheets, fonts and materials into Assets/Art/ArtLibrary.asset, the one lookup the game uses by name.
/// Run it after new art or effect sheets are imported (the sprite and flipbook importers call it too). It touches no scene.
/// </summary>
public static class ArtLibraryBuilder
{
    const string LibraryPath = "Assets/Art/ArtLibrary.asset",
        UrpMaterials = "Packages/com.unity.render-pipelines.universal/Runtime/Materials/";

    /// <summary>Re-collects sprites and effect sheets into the art library (after new art or effects are imported).</summary>
    [MenuItem("Athanor/Refresh Art Library")]
    public static void Refresh() => Debug.Log("art library: " + RefreshLibrary().sprites.Length + " sprites");

    /// <summary>From the command line (Unity closed): unity run Athanor -- -executeMethod ArtLibraryBuilder.RefreshBatch -quit</summary>
    public static void RefreshBatch() => Debug.Log("art library: " + RefreshLibrary().sprites.Length + " sprites");

    /// <summary>Rebuilds the library asset and saves it.</summary>
    public static ArtLibrary RefreshLibrary() { var lib = BuildLibrary(); AssetDatabase.SaveAssets(); return lib; }

    static ArtLibrary BuildLibrary()
    {
        var lib = AssetDatabase.LoadAssetAtPath<ArtLibrary>(LibraryPath);
        if (lib == null) { lib = ScriptableObject.CreateInstance<ArtLibrary>(); AssetDatabase.CreateAsset(lib, LibraryPath); }
        var sheets = AssetDatabase.FindAssets("t:FlipbookSheet", new[] { "Assets/Art" })
            .Select(g => AssetDatabase.LoadAssetAtPath<FlipbookSheet>(AssetDatabase.GUIDToAssetPath(g))).Where(s => s != null).ToArray();
        var sheetTextures = new HashSet<Texture>(sheets.Where(s => s.frames.Length > 0 && s.frames[0] != null).Select(s => (Texture)s.frames[0].texture));
        // a texture can hold several sprites (the animation sheets made by AtlasBuilder), so load them all
        // Art/Depth (the HD-2D layers) is in the library too: the main scene is the HD-2D bench; each name occurs once
        lib.sprites = AssetDatabase.FindAssets("t:Sprite", new[] { "Assets/Art" }).Distinct()
            .Select(AssetDatabase.GUIDToAssetPath)
            .SelectMany(p => AssetDatabase.LoadAllAssetsAtPath(p).OfType<Sprite>())
            .Where(s => s != null && !sheetTextures.Contains(s.texture)).OrderBy(s => s.name).ToArray();
        lib.sheets = sheets;
        lib.title = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Art/Fonts/Alagard.asset");
        lib.body = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Art/Fonts/BitPotion.asset");
        lib.label = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Art/Fonts/PixelOperator-Bold.asset");
        lib.tiny = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Art/Fonts/PixelOperator8.asset");
        lib.crux = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Art/Fonts/Crux.asset");
        lib.lit = AssetDatabase.LoadAssetAtPath<Material>(UrpMaterials + "Sprite-Lit-Default.mat");
        lib.unlit = AssetDatabase.LoadAssetAtPath<Material>(UrpMaterials + "Sprite-Unlit-Default.mat");
        var glow = AssetDatabase.LoadAssetAtPath<Material>("Assets/Resources/ParticleGlow.mat");
        lib.glow = glow;
        EditorUtility.SetDirty(lib);
        return lib;
    }
}
