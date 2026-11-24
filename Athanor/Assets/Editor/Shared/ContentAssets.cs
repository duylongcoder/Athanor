using Athanor.Gameplay;
using UnityEditor;

/// <summary>The content assets for Editor tools and tests (no scene, no Resources).</summary>
public static class ContentAssets
{
    public const string Root = "Assets/Content", DatabasePath = Root + "/ContentDatabase.asset", RulesPath = Root + "/GameRules.asset";

    /// <summary>Assets/Content/ContentDatabase.asset; throws when it is missing.</summary>
    public static ContentDatabase Database()
    {
        var db = AssetDatabase.LoadAssetAtPath<ContentDatabase>(DatabasePath);
        if (db == null) throw new System.InvalidOperationException("missing " + DatabasePath + ": the content assets are missing; see docs/ATHANOR.md");
        return db;
    }

    /// <summary>The content as the rules read it.</summary>
    public static ContentData Data() => Database().ToContentData();
}
