using System.Collections.Generic;
using System.Linq;
using Athanor.Game;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Athanor.EditorTools
{
    /// <summary>
    /// Creates (or updates, by name) the tutorial step objects under the Tutorial object of Main.unity, from the table below.
    /// The table reproduces what Drive lit and which clicks reached the game before the steps were scene objects; after the first run
    /// the scene is the source and this tool is only needed to restore a step to that state.
    /// Menu: Athanor/Tutorial/Create Step Objects. Batch: unity run Athanor -- -executeMethod Athanor.EditorTools.TutorialStepsMigration.RunBatch -quit
    /// </summary>
    public static class TutorialStepsMigration
    {
        const string ScenePath = "Assets/Scenes/Main.unity";

        // fixed windows, art pixels (x, y, width, height), top-left origin
        static readonly Rect Hand = new Rect(126, 284, 392, 76), Ignite = new Rect(515, 304, 122, 42), Journal = new Rect(516, 284, 122, 24),
            Rack = new Rect(126, 252, 392, 32), Offer = new Rect(0, 60, 640, 240);

        sealed class A
        {
            public string name; public AreaKind kind; public Rect rect; public ScriptTrial trial; public int slot; public ViewRef view; public string cell = ""; public Vector4 margin; public bool pass;
        }
        static A Fixed(string n, Rect r, bool pass) => new A { name = n, kind = AreaKind.Fixed, rect = r, pass = pass };
        static A Tile(ScriptTrial t, int slot, bool pass) => new A { name = slot < 0 ? "Tiles " + t : "Tile " + t + " " + slot, kind = AreaKind.ScriptTile, trial = t, slot = slot, pass = pass };
        static A View(ViewRef v, bool pass, string cell = "", Vector4 margin = default) => new A { name = v + (cell != "" ? " " + cell : ""), kind = AreaKind.View, view = v, cell = cell, pass = pass, margin = margin };

        sealed class S
        {
            public string id, key; public bool dark = true, lockPad = true; public bool boxDrop; public A[] areas;
        }
        static S Step(string id, string key, params A[] areas) => new S { id = id, key = key, areas = areas };

        const int JournalBoxDrop = 248;   // the box drops while the Journal is open (2.5, 2.6): y = boxPos.y + 248

        static List<S> Table()
        {
            var R = ScriptTrial.Reaction; var M = ScriptTrial.Miss; var C = ScriptTrial.Chain; var K = ScriptTrial.Catalyst;
            return new List<S>
            {
                Step("1.1", "tutorial.1_1", View(ViewRef.Bench, false)),
                Step("1.2", "tutorial.1_2", Fixed("Hand", Hand, true), Tile(R, 0, true)),
                Step("1.3", "tutorial.1_3", Fixed("Hand", Hand, true), Tile(R, 0, true), Tile(R, 1, true)),
                Step("1.4", "tutorial.1_4", Fixed("Hand", Hand, false), Tile(R, 0, false), Tile(R, 1, false)),
                Step("1.5", "tutorial.1_5", Fixed("Ignite", Ignite, true)),
                Step("1.6", "tutorial.1_6", View(ViewRef.EventHub, false)),
                Step("1.6b", "tutorial.1_6b", View(ViewRef.EventHub, false)),
                Step("1.7", "tutorial.1_7", Fixed("Rack", Rack, false)),
                Step("2.1", "tutorial.2_1", Fixed("Hand", Hand, true), Tile(M, 0, true), Tile(M, 1, true)),
                Step("2.2", "tutorial.2_2", Fixed("Ignite", Ignite, true)),
                Step("2.3", "tutorial.2_3"),
                Step("2.4", "tutorial.2_4", Fixed("Journal button", Journal, true)),
                new S { id = "2.5", key = "tutorial.2_5", boxDrop = true, areas = new[] { View(ViewRef.JournalCell, false, "ember_coal"), View(ViewRef.JournalRecipes, false) } },
                new S { id = "2.6", key = "tutorial.2_6", boxDrop = true, areas = new[] { View(ViewRef.JournalIndex, false) } },
                new S { id = "2.7", key = "tutorial.2_7", dark = false, lockPad = false, areas = new[] { View(ViewRef.JournalCell, false, "glowmoss"), View(ViewRef.JournalHints, false) } },
                Step("3.0", "tutorial.3_0", View(ViewRef.Redraw, false, "", new Vector4(2, 2, 2, 2))),
                Step("3.1", "tutorial.3_1", Fixed("Hand", Hand, true), Tile(C, 0, true), Tile(C, 1, true), Tile(C, 2, true)),
                Step("3.2", "tutorial.3_2", Tile(C, 1, false)),
                Step("3.3", "tutorial.3_3", Fixed("Ignite", Ignite, true)),
                Step("3.3e", "tutorial.step", View(ViewRef.EventHub, false)),
                Step("3.4", "tutorial.3_4"),
                Step("3.5", "tutorial.3_5", Tile(C, 1, false)),
                Step("3.6", "tutorial.3_6", Tile(C, -1, false)),
                Step("5.1", "tutorial.5_1"),
                Step("5.2", "tutorial.5_2"),
                new S { id = "5.3", key = "tutorial.5_3", dark = false, areas = new[] { Fixed("Offer", Offer, true) } },
                Step("5.4", "tutorial.5_4", Fixed("Hand", Hand, true), Tile(K, 0, true), Tile(K, 1, true), Tile(K, 2, true), Tile(K, 3, true), Tile(K, 4, true)),
                Step("5.4i", "tutorial.5_4", Fixed("Ignite", Ignite, true)),
                Step("5.4r", "tutorial.5_4", View(ViewRef.EventHub, false)),
                Step("5.5", "tutorial.5_5"),
            };
        }

        [MenuItem("Athanor/Tutorial/Create Step Objects")]
        public static void Run()
        {
            var scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            Apply(scene);
            EditorSceneManager.SaveScene(scene);
            AssetDatabase.SaveAssets();
        }

        /// <summary>From the command line (Unity closed). Exits 1 on any error.</summary>
        public static void RunBatch()
        {
            try { Run(); }
            catch (System.Exception e) { Debug.LogError(e); EditorApplication.Exit(1); }
        }

        static void Apply(UnityEngine.SceneManagement.Scene scene)
        {
            var ctl = scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<TutorialController>(true)).Single();
            var so = new SerializedObject(ctl);
            var style = so.FindProperty("style");
            style.FindPropertyRelative("tileSize").vector2Value = new Vector2(72, 44);
            style.FindPropertyRelative("tileShift").vector2Value = new Vector2(36, 24);
            var boxPos = style.FindPropertyRelative("boxPos").vector2Value;
            so.ApplyModifiedPropertiesWithoutUndo();

            foreach (var s in Table())
            {
                var t = ctl.transform.Find("Step " + s.id);
                if (t == null) { var go = new GameObject("Step " + s.id); t = go.transform; t.SetParent(ctl.transform, false); }
                var step = t.GetComponent<TutorialStep>();
                if (step == null) step = t.gameObject.AddComponent<TutorialStep>();
                step.name = "Step " + s.id;
                step.textKey = s.key; step.dark = s.dark; step.lockPad = s.lockPad; step.clickAdvances = true;
                step.useBoxPos = s.boxDrop; step.boxPos = s.boxDrop ? new Vector2(boxPos.x, boxPos.y + JournalBoxDrop) : Vector2.zero;
                foreach (var old in t.Cast<Transform>().ToArray()) Object.DestroyImmediate(old.gameObject);   // idempotent: areas are rebuilt
                foreach (var a in s.areas)
                {
                    var g = new GameObject(a.name); g.transform.SetParent(t, false);
                    var area = g.AddComponent<TutorialArea>();
                    area.kind = a.kind; area.trial = a.trial; area.slot = a.slot; area.view = a.view; area.cellId = a.cell; area.margin = a.margin; area.clicksPass = a.pass;
                    if (a.kind == AreaKind.Fixed)
                    {
                        area.size = a.rect.size;
                        g.transform.position = Px.At(a.rect.x + a.rect.width / 2f, a.rect.y + a.rect.height / 2f);
                    }
                }
                EditorUtility.SetDirty(step);
            }
        }
    }
}
