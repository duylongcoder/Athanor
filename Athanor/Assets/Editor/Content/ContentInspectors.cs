using Athanor.Gameplay;
using UnityEditor;
using UnityEngine;

namespace Athanor.EditorTools
{
    /// <summary>ContentDatabase: counts, a Validate button, and a Write Snapshot button.</summary>
    [CustomEditor(typeof(ContentDatabase))]
    public sealed class ContentDatabaseEditor : Editor
    {
        string result;
        bool ok;

        public override void OnInspectorGUI()
        {
            var db = (ContentDatabase)target;
            EditorGUILayout.HelpBox(db.materials.Count + " materials, " + db.recipes.Count + " recipes, " + db.catalysts.Count + " catalysts, "
                + db.aspects.Count + " aspects, " + db.events.Count + " events, " + db.goals.Count + " goals. The list order is the order the rules see.", MessageType.None);
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Validate")) { result = ContentValidation.Check(db); ok = result == null; if (ok) result = "Valid."; }
                if (GUILayout.Button("Write Snapshot")) { ContentSnapshot.Write(); ok = true; result = "Snapshot written. Commit tools/content/snapshot.json with the assets."; }
            }
            if (result != null) EditorGUILayout.HelpBox(result, ok ? MessageType.Info : MessageType.Error);
            EditorGUILayout.Space();
            DrawDefaultInspector();
        }
    }

    /// <summary>MaterialDefinition: warns when the asset name and the id differ, and when a non-starter has a rarity or a lock.</summary>
    [CustomEditor(typeof(MaterialDefinition)), CanEditMultipleObjects]
    public sealed class MaterialDefinitionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var m = (MaterialDefinition)target;
            if (m.name != m.id) EditorGUILayout.HelpBox("The asset name (" + m.name + ") and the id (" + m.id + ") differ; ContentAssetTests expects them equal.", MessageType.Warning);
            if (m.tier != 0 && (m.rarity != RarityId.Unset || m.locked)) EditorGUILayout.HelpBox("Only a starter (tier 0) has a rarity or a lock.", MessageType.Error);
            DrawDefaultInspector();
        }
    }
}
