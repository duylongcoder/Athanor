using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Writes the content assets as tools/content/snapshot.json (the shape the content assets had) for the tools that are not Unity:
/// tools/ci/check_repo.py, tools/vault/build_vault.py, tools/art/export.mjs. The game never reads it.
/// </summary>
public static class ContentSnapshot
{
    /// <summary>Absolute path of tools/content/snapshot.json.</summary>
    public static string FilePath => Path.GetFullPath(Path.Combine(Application.dataPath, "../../tools/content/snapshot.json"));

    /// <summary>The snapshot text for the current assets.</summary>
    public static string Text() => JsonUtility.ToJson(ContentAssets.Data(), true).Replace("\r\n", "\n") + "\n";

    [MenuItem("Athanor/Content/Write Snapshot")]
    public static void Write()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
        File.WriteAllText(FilePath, Text());
        Debug.Log("[content] snapshot written to " + FilePath);
    }

    /// <summary>Batch entry: unity run Athanor -- -executeMethod ContentSnapshot.RunBatch.</summary>
    public static void RunBatch()
    {
        try { Write(); }
        catch (System.Exception e) { Debug.LogError(e); EditorApplication.Exit(1); }
    }
}
