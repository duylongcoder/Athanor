using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Every texture under Assets/Art is pixel art: a point-filtered, uncompressed sprite at 1 pixel per unit.
/// The one exception is Assets/Art/Depth (the HD-2D layers): its pre-blurred pictures (*_far, *_near) are bilinear, so the
/// blur is not turned back into blocks when the world is drawn at 2x or 3x, and its fog sheets (fog_*) repeat.
/// </summary>
public sealed class SpriteImportRules : AssetPostprocessor
{
    // bump when the rules change, so Unity reimports every texture with the new rules. Not bumped for the Art/Depth
    // exception: those textures were new (never imported under the old rule), and a bump would reimport all the others.
    public override uint GetVersion() => 2;

    /// <summary>True for a pre-blurred depth layer: Art/Depth/*_far*, *_near*.</summary>
    public static bool IsSoftDepthArt(string assetPath)
    {
        if (!assetPath.StartsWith(AtlasBuilder.DepthDir + "/")) return false;
        string name = Path.GetFileNameWithoutExtension(assetPath);
        return name.Contains("_far") || name.Contains("_near");
    }

    /// <summary>True for a tiling fog sheet: Art/Depth/fog_*.</summary>
    public static bool IsDepthFog(string assetPath) =>
        assetPath.StartsWith(AtlasBuilder.DepthDir + "/") && Path.GetFileNameWithoutExtension(assetPath).StartsWith("fog_");

    void OnPreprocessTexture()
    {
        if (!assetPath.StartsWith("Assets/Art/")) return;
        var ti = (TextureImporter)assetImporter;
        ti.textureType = TextureImporterType.Sprite;
        ti.spritePixelsPerUnit = 1;
        ti.filterMode = IsSoftDepthArt(assetPath) ? FilterMode.Bilinear : FilterMode.Point;
        ti.textureCompression = TextureImporterCompression.Uncompressed;
        ti.mipmapEnabled = false;
        ti.alphaIsTransparency = true;
        ti.wrapMode = IsDepthFog(assetPath) ? TextureWrapMode.Repeat : TextureWrapMode.Clamp;
        ti.maxTextureSize = 4096;
        ti.npotScale = TextureImporterNPOTScale.None;
        // explicit for the web build too, so no platform default can ever compress the pixel art
        var web = ti.GetPlatformTextureSettings("WebGL");
        web.overridden = true; web.format = TextureImporterFormat.RGBA32; web.textureCompression = TextureImporterCompression.Uncompressed;
        web.maxTextureSize = 4096; web.crunchedCompression = false;
        ti.SetPlatformTextureSettings(web);
        // an effect sheet (a .json beside it) is sliced by FlipbookImporter and an animation sheet by AtlasBuilder;
        // everything else is one sprite
        if (!File.Exists(Path.ChangeExtension(assetPath, ".json")) && !AtlasBuilder.IsSpriteSheet(assetPath))
            ti.spriteImportMode = SpriteImportMode.Single;
        var s = new TextureImporterSettings();
        ti.ReadTextureSettings(s);
        s.spriteMeshType = SpriteMeshType.FullRect;
        s.spriteExtrude = 0;
        s.spriteGenerateFallbackPhysicsShape = false;
        ti.SetTextureSettings(s);
    }
}
