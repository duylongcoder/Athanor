using System.Collections.Generic;
using System.IO;
using UnityEditor;

/// <summary>
/// Rewrites every view prefab and the data assets in the current serializer format, so a field added to a component
/// is stored with its value. Phase 7 uses it after a number moves from code to a prefab.
/// Main.unity is left out on purpose: on the untouched project a reserialize rewrote a camera viewport rect (a value change) and
/// crashed the Editor, so scene-owned values are set by hand or by a dedicated editor script and checked with a diff.
/// </summary>
public static class Reserialize
{
    [MenuItem("Athanor/Reserialize Views and Data")]
    public static void All()
    {
        var paths = new List<string>();
        foreach (var guid in AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Prefabs" })) paths.Add(AssetDatabase.GUIDToAssetPath(guid));
        if (Directory.Exists("Assets/Data"))
            foreach (var guid in AssetDatabase.FindAssets("t:ScriptableObject", new[] { "Assets/Data" }))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                if (path.EndsWith(".asset")) paths.Add(path);
            }
        AssetDatabase.ForceReserializeAssets(paths);
        UnityEngine.Debug.Log("[Reserialize] " + paths.Count + " assets");
    }
}
