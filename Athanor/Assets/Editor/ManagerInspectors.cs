using System.Linq;
using Athanor.Game;
using UnityEditor;
using UnityEngine;

namespace Athanor.EditorTools
{
    /// <summary>RunManager: while playing, the run in progress and a button to pin its seed.</summary>
    [CustomEditor(typeof(RunManager))]
    public sealed class RunManagerEditor : Editor
    {
        public override bool RequiresConstantRepaint() => Application.isPlaying;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var m = (RunManager)target;
            if (!Application.isPlaying || m.Run == null) { EditorGUILayout.HelpBox("Play to see the run.", MessageType.None); return; }
            var r = m.Run;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Seed", m.LastSeed.ToString());
            EditorGUILayout.LabelField("Phase", r.Phase.ToString());
            EditorGUILayout.LabelField("Depth", r.Depth.ToString());
            EditorGUILayout.LabelField("Potency", r.Potency + " / " + r.Target);
            EditorGUILayout.LabelField("Trials left", r.TrialsLeft.ToString());
            EditorGUILayout.LabelField("Redraws left", r.RedrawsLeft.ToString());
            EditorGUILayout.LabelField("Hand", string.Join(", ", r.Hand));
            EditorGUILayout.LabelField("Catalysts", string.Join(", ", r.Catalysts.Select(c => c.id)));
            if (GUILayout.Button("Use this seed for every run")) { Undo.RecordObject(m, "Seed override"); m.SeedOverride = m.LastSeed; }
        }
    }

    /// <summary>ProgressManager: while playing, the journal and profile counts.</summary>
    [CustomEditor(typeof(ProgressManager))]
    public sealed class ProgressManagerEditor : Editor
    {
        public override bool RequiresConstantRepaint() => Application.isPlaying;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var m = (ProgressManager)target;
            if (!Application.isPlaying || m.Profile == null) { EditorGUILayout.HelpBox("Play to see the journal and profile.", MessageType.None); return; }
            EditorGUILayout.LabelField("Sandbox", m.Sandbox.ToString());
            EditorGUILayout.LabelField("Journal entries", m.Journal.KnownPairs.Count.ToString());
            EditorGUILayout.LabelField("Goals done", m.Profile.Completed.Count.ToString());
            EditorGUILayout.LabelField("Runs started / won", m.Profile.RunsStarted + " / " + m.Profile.RunsWon);
        }
    }
}
