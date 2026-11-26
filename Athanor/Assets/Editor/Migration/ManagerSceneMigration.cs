using System;
using System.Linq;
using Athanor.Game;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Athanor.EditorTools
{
    /// <summary>
    /// Creates Main.unity › Managers and its components, and fills their asset fields (database, english, vietnamese) from
    /// their asset paths when empty. Idempotent: run it again after a task adds a manager. Menu: Athanor/Migrate/Add Managers to Main.
    /// </summary>
    public static class ManagerSceneMigration
    {
        const string ScenePath = "Assets/Scenes/Main.unity";

        [MenuItem("Athanor/Migrate/Add Managers to Main")]
        public static void Run()
        {
            var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            Apply(scene);
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
        }

        public static void RunBatch()
        {
            try { Run(); }
            catch (Exception e) { Debug.LogError(e); EditorApplication.Exit(1); }
        }

        public static void Apply(Scene scene)
        {
            var root = scene.GetRootGameObjects().FirstOrDefault(g => g.name == "Managers");
            if (root == null) { root = new GameObject("Managers"); SceneManager.MoveGameObjectToScene(root, scene); root.transform.SetAsFirstSibling(); }
            Fill(Ensure<ContentManager>(root), "database", "Assets/Content/ContentDatabase.asset");
            Ensure<SaveManager>(root);
            Ensure<ProgressManager>(root);
            Ensure<RunManager>(root);
            var loc = Ensure<LocalizationManager>(root);
            Fill(loc, "english", "Assets/Resources/lang/en.json");
            Fill(loc, "vietnamese", "Assets/Resources/lang/vi.json");
        }

        static T Ensure<T>(GameObject root) where T : Component
        {
            var c = root.GetComponent<T>();
            if (c == null) c = root.AddComponent<T>();
            return c;
        }

        /// <summary>Points an empty object field at the asset on that path.</summary>
        static void Fill(Component to, string field, string assetPath)
        {
            var so = new SerializedObject(to);
            var p = so.FindProperty(field);
            if (p.objectReferenceValue != null) return;
            p.objectReferenceValue = AssetDatabase.LoadMainAssetAtPath(assetPath) ?? throw new InvalidOperationException("missing " + assetPath);
            so.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
