using UnityEditor;
using UnityEngine;

public class FixCandleImport
{
    [MenuItem("Athanor/Fix Candle Import Settings")]
    public static void Fix()
    {
        string path = "Assets/Art/Depth/candle_far.png";
        var ti = (TextureImporter)AssetImporter.GetAtPath(path);
        
        // Set default settings
        ti.filterMode = FilterMode.Bilinear;
        ti.wrapMode = TextureWrapMode.Clamp;
        
        // Force reimport
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate | ImportAssetOptions.ForceSynchronousImport);
        
        // Verify
        var ti2 = (TextureImporter)AssetImporter.GetAtPath(path);
        Debug.Log("After fix - filterMode: " + ti2.filterMode + ", wrapMode: " + ti2.wrapMode);
    }
}
