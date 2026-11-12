using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>Every texture under Assets/Art is pixel art: a point-filtered, uncompressed sprite at 1 pixel per unit.</summary>
public sealed class SpriteImportRules : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        if (!assetPath.StartsWith("Assets/Art/")) return;
        var ti = (TextureImporter)assetImporter;
        ti.textureType = TextureImporterType.Sprite;
        ti.spritePixelsPerUnit = 1;
        ti.filterMode = FilterMode.Point;
        ti.textureCompression = TextureImporterCompression.Uncompressed;
        ti.mipmapEnabled = false;
        ti.alphaIsTransparency = true;
        ti.wrapMode = TextureWrapMode.Clamp;
        ti.maxTextureSize = 4096;
        ti.npotScale = TextureImporterNPOTScale.None;
        // an effect sheet (a .json beside it) is sliced by FlipbookImporter; everything else is one sprite
        if (!File.Exists(Path.ChangeExtension(assetPath, ".json"))) ti.spriteImportMode = SpriteImportMode.Single;
        var s = new TextureImporterSettings();
        ti.ReadTextureSettings(s);
        s.spriteMeshType = SpriteMeshType.FullRect;
        s.spriteExtrude = 0;
        s.spriteGenerateFallbackPhysicsShape = false;
        ti.SetTextureSettings(s);
    }
}
