using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Athanor.Core;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>
    /// The baseline of the rectangles each tutorial step lit before the steps moved into the scene (art pixels, top-left origin, before the hole snap).
    /// It is computed from the old TutorialTargets values and the old per-step lists of Drive, once, and committed.
    /// </summary>
    public class TutorialStepsBakeTests
    {
        public const string BaselinePath = "Assets/Tests/EditModeGame/Baselines/tutorial_lit_baseline.json";

        [Serializable] public class StepRects { public string id; public Rect[] lit; }
        [Serializable] public class Baseline { public List<StepRects> steps = new List<StepRects>(); }

        /// <summary>Capture of the OLD behaviour (needs TutorialController.targets). Run once before the move; kept for the record.</summary>
        [Test, Explicit("writes the baseline from the old TutorialTargets")]
        public void CaptureBaseline()
        {
            using (var main = new MainSceneFixture())
            {
                var t = main.FlowField<TutorialController>("tutorial");
                var targets = MainSceneFixture.Field<object>(t, "targets");
                Rect R(string n) => (Rect)targets.GetType().GetField(n).GetValue(targets);
                Vector2 V(string n) => (Vector2)targets.GetType().GetField(n).GetValue(targets);
                float redrawMargin = (float)targets.GetType().GetField("redrawMargin").GetValue(targets);
                var bench = main.Flow.Bench;
                Rect Tile(int i)
                {
                    var c = bench.TileCentre(i);
                    return new Rect(Mathf.Round(c.x + Px.W / 2f - V("tileShift").x), Mathf.Round(Px.H / 2f - c.y - V("tileShift").y), V("tileSize").x, V("tileSize").y);
                }
                Rect Hand() { var r = R("hand"); return new Rect(r.x, r.y, r.width, Mathf.Min(r.height, Px.H - r.y)); }
                var ch = TutorialScript.Chain.Lay; var re = TutorialScript.Reaction.Lay; var mi = TutorialScript.Miss.Lay;
                var rd = bench.Hud.RedrawRect;
                var b = new Baseline();
                void Add(string id, params Rect[] lit) => b.steps.Add(new StepRects { id = id, lit = lit });
                Add("1.1", TutorialController.Union(Enumerable.Range(0, Core.Bench.TileCount).Select(Tile).ToArray()));
                Add("1.2", Hand(), Tile(re[0].Tile));
                Add("1.3", Hand(), Tile(re[0].Tile), Tile(re[1].Tile));
                Add("1.4", Hand(), Tile(re[0].Tile), Tile(re[1].Tile));
                Add("1.5", R("ignite"));
                Add("1.6", Tile(ch[1].Tile));      // hub of the event that just played; the chain hub stands in
                Add("1.6b", Tile(ch[1].Tile));
                Add("1.7", R("rack"));
                Add("2.1", Hand(), Tile(mi[0].Tile), Tile(mi[1].Tile));
                Add("2.2", R("ignite"));
                Add("2.3");
                Add("2.4", R("journal"));
                Add("3.0", new Rect(rd.x - redrawMargin, rd.y - redrawMargin, rd.width + 2 * redrawMargin, rd.height + 2 * redrawMargin));
                Add("3.1", Hand(), Tile(ch[0].Tile), Tile(ch[1].Tile), Tile(ch[2].Tile));
                Add("3.2", Tile(ch[1].Tile));
                Add("3.3", R("ignite"));
                Add("3.4");
                Add("3.5", Tile(ch[1].Tile));
                Add("3.6", TutorialController.Union(ch.Select(l => Tile(l.Tile)).ToArray()));
                Add("5.1"); Add("5.2");
                Add("5.3", R("offer"));
                Add("5.5");
                var jv = main.Flow.JournalView;
                var db = new ContentDb(JsonUtility.FromJson<ContentData>(Resources.Load<TextAsset>("content").text));
                Rect Cell(string id) => JournalView.CellRect(Enumerable.Range(0, db.Materials.Count).First(i => db.Materials[i].id == id));
                Add("2.5", Cell("ember_coal"), jv.RecipesRect);
                Add("2.6", jv.IndexRect);
                Add("2.7", Cell("glowmoss"), jv.HintsRect);
                File.WriteAllText(BaselinePath, JsonUtility.ToJson(b, true));
            }
        }
    }
}
