using Athanor.Game;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Scene-view preview of the tutorial steps in Main.unity (Edit Mode): every Fixed area is drawn as an outline, green when clicks pass
/// through it and amber when it is highlight only, with the step name and its text key. Select a Fixed area to
/// move it with the handle; its size is set in the Inspector, in pixels. ScriptTile and View areas follow the game and are not drawn.
/// </summary>
[CustomEditor(typeof(TutorialArea))]
public sealed class TutorialAreaEditor : Editor
{
    static readonly Color Pass = Color.green, Lit = new Color(1f, 0.7f, 0.2f);

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected | GizmoType.Pickable)]
    static void Draw(TutorialArea a, GizmoType type)
    {
        if (a.kind != AreaKind.Fixed) return;
        Gizmos.color = a.clicksPass ? Pass : Lit;
        Gizmos.DrawWireCube(a.transform.position, new Vector3(a.size.x, a.size.y, 0));
        if ((type & GizmoType.Selected) == 0) return;
        var step = a.GetComponentInParent<TutorialStep>();
        if (step == null) return;
        Handles.Label(a.transform.position + new Vector3(-a.size.x * 0.5f, a.size.y * 0.5f, 0), step.name + "  " + step.textKey);
    }

    void OnSceneGUI()
    {
        var a = (TutorialArea)target;
        if (a.kind != AreaKind.Fixed) return;
        EditorGUI.BeginChangeCheck();
        var p = Handles.PositionHandle(a.transform.position, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(a.transform, "Move tutorial area");
            a.transform.position = new Vector3(Mathf.Round(p.x), Mathf.Round(p.y), a.transform.position.z);   // whole pixels
        }
    }
}
