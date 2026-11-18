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
- Layer: **Editor** · Source: `Athanor/Assets/Editor/ViewBaker.cs:15-220`

> Phase 6 only (deleted in Task 16): builds a view the old way once, keeps a reference picture, saves it as a prefab, puts the prefab into Main.unity and sets the GameFlow field. Then Compare checks the prefab draws the same picture. unity run Athanor -- -executeMethod ViewBaker.Bake<Name> (no -quit: unity run rejects it)

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string ScenePath =` | 18 | Path of the scene the baked views are placed in. |
| field | `public const string ViewDir =` | 20 | Folder that holds the view prefabs. |
| field | `public const string PartDir =` | 22 | Folder that holds the part prefabs (repeated pieces). |
| field | `public const string EvidenceDir =` | 24 | Folder (relative to the Unity project) for the before/after pictures. |
| method | `public static ContentDb Db()` | 51 | The content database read from Resources/content.json, for views that bind to content. |
| method | `public static void Bake<T>(string name, string flowField, Action<T> build, Action<T> beforeSave = null, string[] keepHidden = null) where T : MonoBeha` | 58 | Old Build in an empty scene, the "before" picture, the prefab; then the prefab into Main and onto GameFlow.flowField. beforeSave runs between the picture and the save (e.g. drop rows that Bind makes at runtime from a part prefab). |
| method | `public static void BakePart<T>(string name, Func<T> build) where T : Component` | 79 | A part (one repeated piece) built by its own function, saved into Prefabs/Parts. No scene change. |
| method | `public static void BakeInScene<T>(string name, string flowField, Action<T> build, bool asPrefab) where T : MonoBehaviour` | 95 | For views that build into scene objects (Bench with the HD-2D stage, cutscenes, window, screen effects): the old Build runs inside Main.unity under a new root; with a prefab name the root is saved as a connected prefab, without one it stays a plain scene object. The GameFlow field is set and the scene saved. Their "before" picture is the browser baseline (Task 0). |
| method | `public static void Compare(string name, Action<GameObject> prepare = null, string[] keepHidden = null)` | 117 | (keepHidden: names of hidden objects the picture leaves hidden.) The prefab alone in an empty scene against the "before" picture. Throws (non-zero exit) above 0.1 %. prepare runs on the instance before the picture (e.g. Bind(Db()) for a view whose rows come from content). |
| method | `public static void CompareRunOver()` | 132 |  |
| method | `public static void CompareGoalToast()` | 133 |  |
| method | `public static void CompareEvent()` | 134 |  |
| method | `public static void CompareCatalystChoice()` | 135 |  |
| method | `public static void ComparePause()` | 136 |  |
| method | `public static void CompareHud()` | 164 | The Hud prefab alone against the "before" picture of the old Build: within 0.1 %. |
| method | `public static void CompareTitle()` | 177 | The Title prefab in the same pose as its "before" pictures: all four must match within 0.1 %. |
| method | `public static void BakeCard()` | 198 | Parts/Card.prefab: one card, shown for the first starting material (the game calls Show for each hand card). |
| method | `public static void BakeBench()` | 210 | Prefabs/Views/Bench.prefab, in Main.unity (the bench sits in the HD-2D scene): the tiles from Editor/layout.json, the rack, the links, the Hud prefab nested, the card prefab as a reference. The depth groups are not baked: GameFlow places the tiles at run time as before. Its "before" picture is the browser baseline (Task 0, mainhd2d.mjs hud). |

## Uses
[[BenchView]], [[CardView]], [[ContentData]], [[ContentDb]], [[EditorGlobals]], [[GameFlow]], [[HudView]], [[LayoutData]], [[Loc]], [[Save]], [[Settings]], [[TitleView]], [[Tooltip]], [[ViewCapture]]
