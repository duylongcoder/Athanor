using Athanor.Game;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Sets the globals GameFlow and Sparks would set from their serialized fields at runtime (<see cref="Art.Lib"/>, <see cref="Loc.Use"/>,
/// <see cref="SparkMesh.BaseMaterial"/>), loading the assets through the AssetDatabase. The batch tools (-executeMethod runs in a fresh
/// domain with no scene) and the Game tests call it first. Lives in the small Athanor.EditorShared assembly so both can reference it.
/// </summary>
public static class EditorGlobals
{
    /// <summary>Loads the art library, the language files and the particle material into the Game layer's statics.</summary>
    public static void Inject()
    {
        Art.Lib = AssetDatabase.LoadAssetAtPath<ArtLibrary>("Assets/Art/ArtLibrary.asset");
        Loc.Use(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/en.json"),
                AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/vi.json"));
        SparkMesh.BaseMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Resources/ParticleGlow.mat");
    }
}
