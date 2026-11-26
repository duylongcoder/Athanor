using Athanor.Game;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Select the Tutorial object in Main.unity, pick a step in the Inspector, and its Fixed areas are drawn in the Scene view
/// (green: clicks pass, amber: highlight only) with a move handle each. Values are written to Tutorial.asset.
/// </summary>
[CustomEditor(typeof(TutorialManager))]
public sealed class TutorialSequenceEditor : Editor
{
    static readonly Color Pass = Color.green, Lit = new Color(1f, 0.7f, 0.2f);
    static int stepIndex;

    TutorialSequence Seq => (TutorialSequence)serializedObject.FindProperty("sequence").objectReferenceValue;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var seq = Seq;
        if (seq == null || seq.steps.Count == 0) return;
        EditorGUILayout.Space();
        var names = seq.steps.ConvertAll(s => s.id + "  (" + s.textKey + ")").ToArray();
        stepIndex = Mathf.Clamp(EditorGUILayout.Popup("Preview step", stepIndex, names), 0, names.Length - 1);
        if (GUILayout.Button("Select Tutorial.asset")) Selection.activeObject = seq;
        SceneView.RepaintAll();
    }

    /// <summary>Art pixels (top-left origin) to the world centre of the rect.</summary>
    static Vector3 Centre(Rect r) => new Vector3(r.x + r.width / 2f - Px.W / 2f, Px.H / 2f - (r.y + r.height / 2f), 0);

    void OnSceneGUI()
    {
        var seq = Seq;
        if (seq == null || stepIndex >= seq.steps.Count) return;
        var step = seq.steps[stepIndex];
        foreach (var a in step.areas)
        {
            if (a.kind != AreaKind.Fixed) continue;
            Handles.color = a.clicksPass ? Pass : Lit;
            var c = Centre(a.rect);
            Handles.DrawWireCube(c, new Vector3(a.rect.width, a.rect.height, 0));
            Handles.Label(c + new Vector3(-a.rect.width / 2f, a.rect.height / 2f, 0), step.id + " " + a.name);
            EditorGUI.BeginChangeCheck();
            var moved = Handles.PositionHandle(c, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(seq, "Move tutorial area");
                a.rect.x = Mathf.Round(moved.x + Px.W / 2f - a.rect.width / 2f);
                a.rect.y = Mathf.Round(Px.H / 2f - moved.y - a.rect.height / 2f);
                EditorUtility.SetDirty(seq);
            }
        }
    }
}
