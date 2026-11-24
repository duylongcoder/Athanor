using System;
using System.Linq;
using Athanor.Game;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Athanor.EditorTools
{
    /// <summary>Sets GameFlow.content in Main.unity to Assets/Content/ContentDatabase.asset and clears contentJson.</summary>
    public static class ContentFieldMigration
    {
        const string ScenePath = "Assets/Scenes/Main.unity";

        [MenuItem("Athanor/Migrate/Point GameFlow at ContentDatabase")]
        public static void Run()
        {
            var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            var game = scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<GameFlow>(true)).Single();
            var so = new SerializedObject(game);
            so.FindProperty("content").objectReferenceValue = ContentAssets.Database();
            so.FindProperty("contentJson").objectReferenceValue = null;
            so.ApplyModifiedPropertiesWithoutUndo();
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
        }

        public static void RunBatch()
        {
            try { Run(); }
            catch (Exception e) { Debug.LogError(e); EditorApplication.Exit(1); }
        }
    }
}
