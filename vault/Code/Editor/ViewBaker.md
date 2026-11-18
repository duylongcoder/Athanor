---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/ViewBaker.cs
tags: [code/editor, kind/class]
aliases: []
---

# ViewBaker

`public static class ViewBaker`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/ViewBaker.cs:15-131`

> Phase 6 only (deleted in Task 16): builds a view the old way once, keeps a reference picture, saves it as a prefab, puts the prefab into Main.unity and sets the GameFlow field. Then Compare checks the prefab draws the same picture. unity run Athanor -- -executeMethod ViewBaker.Bake<Name> (no -quit: unity run rejects it)

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string ScenePath =` | 18 | Path of the scene the baked views are placed in. |
| field | `public const string ViewDir =` | 20 | Folder that holds the view prefabs. |
| field | `public const string PartDir =` | 22 | Folder that holds the part prefabs (repeated pieces). |
| field | `public const string EvidenceDir =` | 24 | Folder (relative to the Unity project) for the before/after pictures. |
| method | `public static ContentDb Db()` | 51 | The content database read from Resources/content.json, for views that bind to content. |
| method | `public static void Bake<T>(string name, string flowField, Action<T> build) where T : MonoBehaviour` | 55 | Old Build in an empty scene, the "before" picture, the prefab; then the prefab into Main and onto GameFlow.flowField. |
| method | `public static void BakePart<T>(string name, Func<T> build) where T : Component` | 74 | A part (one repeated piece) built by its own function, saved into Prefabs/Parts. No scene change. |
| method | `public static void BakeInScene<T>(string name, string flowField, Action<T> build, bool asPrefab) where T : MonoBehaviour` | 90 | For views that build into scene objects (Bench with the HD-2D stage, cutscenes, window, screen effects): the old Build runs inside Main.unity under a new root; with a prefab name the root is saved as a connected prefab, without one it stays a plain scene object. The GameFlow field is set and the scene saved. Their "before" picture is the browser baseline (Task 0). |
| method | `public static void Compare(string name, Action<GameObject> prepare = null)` | 112 | The prefab alone in an empty scene against the "before" picture. Throws (non-zero exit) above 0.1 %. prepare runs on the instance before the picture (e.g. Bind(Db()) for a view whose rows come from content). |
| method | `public static void CompareRunOver()` | 127 |  |
| method | `public static void CompareGoalToast()` | 128 |  |
| method | `public static void CompareEvent()` | 129 |  |
| method | `public static void CompareCatalystChoice()` | 130 |  |

## Uses
[[ContentData]], [[ContentDb]], [[EditorGlobals]], [[GameFlow]], [[Loc]], [[Save]], [[ViewCapture]]
