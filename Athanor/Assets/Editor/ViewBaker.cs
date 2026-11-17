using System;
using System.IO;
using Athanor.Core;
using Athanor.Game;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Phase 6 only (deleted in Task 16): builds a view the old way once, keeps a reference picture, saves it as a prefab,
/// puts the prefab into Main.unity and sets the GameFlow field. Then Compare checks the prefab draws the same picture.
/// unity run Athanor -- -executeMethod ViewBaker.Bake&lt;Name&gt; (no -quit: unity run rejects it)
/// </summary>
public static class ViewBaker
{
    public const string ScenePath = "Assets/Scenes/Main.unity", ViewDir = "Assets/Prefabs/Views", PartDir = "Assets/Prefabs/Parts";
    public const string EvidenceDir = "../production/qa/evidence/phase6/views";
    const float MaxDiffer = 0.1f;

    static void Prepare()
    {
        EditorGlobals.Inject();
        Loc.Set(Loc.English);
    }

    public static ContentDb Db() =>
        new ContentDb(JsonUtility.FromJson<ContentData>(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/content.json").text));

    /// <summary>Old Build in an empty scene, the "before" picture, the prefab; then the prefab into Main and onto GameFlow.flowField.</summary>
    public static void Bake<T>(string name, string flowField, Action<T> build) where T : MonoBehaviour
    {
        Prepare();
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var go = new GameObject(name);
        build(go.AddComponent<T>());
        ViewCapture.Save(ViewCapture.Render(go), EvidenceDir + "/" + name + "/before.png");
        Directory.CreateDirectory(ViewDir);
        var prefab = PrefabUtility.SaveAsPrefabAsset(go, ViewDir + "/" + name + ".prefab");

        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        var old = GameObject.Find(name);
        if (old != null) Object.DestroyImmediate(old);                    // a second bake replaces the first
        var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, scene);
        var so = new SerializedObject(Object.FindFirstObjectByType<GameFlow>());
        so.FindProperty(flowField).objectReferenceValue = instance.GetComponent<T>();
        so.ApplyModifiedPropertiesWithoutUndo();
        EditorSceneManager.SaveScene(scene);
        Debug.Log("[phase6] baked " + name);
    }

    /// <summary>A part (one repeated piece) built by its own function, saved into Prefabs/Parts. No scene change.</summary>
    public static void BakePart<T>(string name, Func<T> build) where T : Component
    {
        Prepare();
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var part = build();
        part.gameObject.name = name;
        Directory.CreateDirectory(PartDir);
        PrefabUtility.SaveAsPrefabAsset(part.gameObject, PartDir + "/" + name + ".prefab");
        Debug.Log("[phase6] baked part " + name);
    }

    /// <summary>
    /// For views that build into scene objects (Bench with the HD-2D stage, cutscenes, window, screen effects): the old Build runs
    /// inside Main.unity under a new root; with a prefab name the root is saved as a connected prefab, without one it stays a plain
    /// scene object. The GameFlow field is set and the scene saved. Their "before" picture is the browser baseline (Task 0).
    /// </summary>
    public static void BakeInScene<T>(string name, string flowField, Action<T> build, bool asPrefab) where T : MonoBehaviour
    {
        Prepare();
        var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        var old = GameObject.Find(name);
        if (old != null) Object.DestroyImmediate(old);
        var go = new GameObject(name);
        var view = go.AddComponent<T>();
        build(view);
        if (asPrefab)
        {
            Directory.CreateDirectory(ViewDir);
            PrefabUtility.SaveAsPrefabAssetAndConnect(go, ViewDir + "/" + name + ".prefab", InteractionMode.AutomatedAction);
        }
        var so = new SerializedObject(Object.FindFirstObjectByType<GameFlow>());
        so.FindProperty(flowField).objectReferenceValue = view;
        so.ApplyModifiedPropertiesWithoutUndo();
        EditorSceneManager.SaveScene(scene);
        Debug.Log("[phase6] baked " + name + " in the scene");
    }

    /// <summary>
    /// The prefab alone in an empty scene against the "before" picture. Throws (non-zero exit) above 0.1 %. prepare runs on the
    /// instance before the picture (e.g. Bind(Db()) for a view whose rows come from content).
    /// </summary>
    public static void Compare(string name, Action<GameObject> prepare = null)
    {
        Prepare();
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(ViewDir + "/" + name + ".prefab");
        var go = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        prepare?.Invoke(go);
        var after = ViewCapture.Render(go);
        ViewCapture.Save(after, EvidenceDir + "/" + name + "/after.png");
        float p = ViewCapture.Differ(ViewCapture.Load(EvidenceDir + "/" + name + "/before.png"), after);
        Debug.Log("[phase6] " + name + " " + p.ToString("0.000") + " % px differ");
        if (p > MaxDiffer) throw new Exception("[phase6] " + name + " differs by " + p.ToString("0.000") + " %, limit " + MaxDiffer);
    }

    // One pair of entry points per view is added by its task, e.g.
    //   public static void BakeRunOver() => Bake<RunOverView>("RunOver", "runOver", v => v.Build());
    //   public static void CompareRunOver() => Compare("RunOver");
}
