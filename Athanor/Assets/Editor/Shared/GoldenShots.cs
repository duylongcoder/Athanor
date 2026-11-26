using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Athanor.Gameplay;
using Athanor.Game;
using PrimeTween;
using UnityEditor;
using UnityEngine;

namespace Athanor.EditorShared
{
    /// <summary>
    /// The golden shots: one picture of every view in a fixed, seeded state, in English and Vietnamese. Each view is built the way its
    /// prefab test builds it (instantiate, Bind, Show / Open), never through Update, so a shot holds no Time.time, no Random and no
    /// running animation: tweens are completed (Tween.CompleteAll) rather than waited for. Phase 7 compares every task against these.
    /// </summary>
    public static class GoldenShots
{
    const string Views = "Assets/Prefabs/Views/";
    static readonly string[] Shots =
    {
        "title", "title.settings", "title.askskip", "bench.tutorial", "bench.crowded", "journal.fresh", "journal.known", "goals.fresh",
        "choice.three", "event.options", "event.outcome", "runover.won", "runover.lost", "pause.menu", "pause.quit", "toast.one",
    };

    /// <summary>The shot names, in the order they are written.</summary>
    public static IEnumerable<string> Names() => Shots;

    /// <summary><repo>/production/qa/evidence/phase7/baseline/golden/&lt;name&gt;.&lt;lang&gt;.png</summary>
    public static string PathOf(string name, string lang)
    {
        var repo = Directory.GetParent(Directory.GetParent(Application.dataPath).FullName).FullName;
        return Path.Combine(repo, "production", "qa", "evidence", "phase7", "baseline", "golden", name + "." + lang + ".png");
    }

    [MenuItem("Athanor/Write Golden Shots")]
    public static void WriteAll()
    {
        EditorGlobals.Inject();
        try
        {
            foreach (var lang in new[] { Loc.English, Loc.Vietnamese })
            {
                Loc.Set(lang);
                foreach (var name in Shots) ViewCapture.Save(Render(name), PathOf(name, lang));
            }
        }
        finally { Loc.Set(Loc.English); }
        Debug.Log("[GoldenShots] wrote " + Shots.Length * 2 + " pictures");
    }

    /// <summary>Builds the view of a shot in the current language, draws it to 640x360 and removes it again.</summary>
    public static Texture2D Render(string name)
    {
        var root = Build(name);
        try
        {
            Tween.CompleteAll();
            return ViewCapture.RenderAsShown(root);
        }
        finally
        {
            Tween.StopAll();
            Time.timeScale = 1f;
            foreach (var m in root.GetComponentsInChildren<MonoBehaviour>(true))
                m.GetType().GetMethod("OnDestroy", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)?.Invoke(m, null);
            UnityEngine.Object.DestroyImmediate(root);
        }
    }

    static GameObject Build(string name)
    {
        switch (name)
        {
            case "title": return Title(t => { });
            case "title.settings": return Title(t => Click(t.transform.Find("Button ui.title.settings")));
            case "title.askskip": return Title(t => t.AskSkip(_ => { }));
            case "bench.tutorial": return BenchTutorial();
            case "bench.crowded": return BenchCrowded();
            case "journal.fresh": return JournalShot(new Journal());
            case "journal.known": return JournalShot(KnownJournal());
            case "goals.fresh": return Goals();
            case "choice.three": return Choice();
            case "event.options": return EventShot(false);
            case "event.outcome": return EventShot(true);
            case "runover.won": return RunOver(true);
            case "runover.lost": return RunOver(false);
            case "pause.menu": return Pause(false);
            case "pause.quit": return Pause(true);
            case "toast.one": return Toast();
        }
        throw new ArgumentException("no golden shot " + name);
    }

    // ---- helpers ------------------------------------------------------------------------------------

    /// <summary>Effect players for a shot's view (the shots fire none): a ScreenFx with its two sprites and a Sparks, under the view.</summary>
    static FxRefs Fx(GameObject parent)
    {
        var go = new GameObject("fx");
        go.transform.SetParent(parent.transform, false);
        var screen = go.AddComponent<ScreenFx>();
        var so = new SerializedObject(screen);
        foreach (var n in new[] { "flash", "black" })
        {
            var g = new GameObject(n);
            g.transform.SetParent(go.transform, false);
            so.FindProperty(n).objectReferenceValue = g.AddComponent<SpriteRenderer>();
        }
        so.ApplyModifiedPropertiesWithoutUndo();
        return new FxRefs(screen, go.AddComponent<Sparks>());
    }

    static GameObject Instance(string path)
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab == null) throw new InvalidOperationException("no prefab at " + path);
        return (GameObject)PrefabUtility.InstantiatePrefab(prefab);
    }

    static ContentDb Db() => new ContentDb(ContentAssets.Data());

    static void Click(Component c) => c.GetComponent<Clickable>().onClick();

    static T Get<T>(object owner, string field) => (T)owner.GetType().GetField(field, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(owner);

    static void Set(object owner, string field, object value) =>
        owner.GetType().GetField(field, BindingFlags.Instance | BindingFlags.NonPublic).SetValue(owner, value);

    // ---- the shots ----------------------------------------------------------------------------------

    static GameObject Title(Action<TitleView> then)
    {
        var go = Instance(Views + "Title.prefab");
        var view = go.GetComponent<TitleView>();
        view.Bind(new Settings(), Fx(go));
        view.Open(0, 0, 0, 0);
        // Open runs Update once on the real clock (fade-in by deltaTime, glow pulse by Time.time): pin it to the finished fade and the
        // pulse at its midpoint, so the shot does not depend on how long the Editor took
        Set(view, "age", 5f);
        typeof(TitleView).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(view, null);
        Get<SpriteRenderer>(view, "glow").color = new Color(1f, 0.55f, 0.2f, 0.28f + 0.12f * 0.5f);
        then(view);
        return go;
    }

    static GameObject BenchWith(RunState run, Journal journal, ContentDb db)
    {
        var go = Instance(Views + "Bench.prefab");
        var view = go.GetComponent<BenchView>();
        view.Bind(db, Fx(go));
        view.Bind(run, journal);
        view.InputEnabled = false;       // Ignite is off: no pulse
        view.Refresh();
        return go;
    }

    static GameObject BenchTutorial()
    {
        var db = Db();
        var seq = AssetDatabase.LoadAssetAtPath<TutorialSequence>("Assets/Content/Tutorial/Tutorial.asset");
        var run = new RunState(db, new Journal(), seq.seed, 1, seq.Options(Unlocks.Fresh(db)));
        run.Deal(seq.Trial(0).Hand);
        return BenchWith(run, new Journal(), db);
    }

    /// <summary>A bench with all six catalyst sockets, all three aspects and all three depth notes.</summary>
    static GameObject BenchCrowded()
    {
        var db = Db();
        var run = new RunState(db, new Journal(), 5);
        var catalysts = Get<List<CatalystData>>(run, "catalysts");
        for (int i = 0; i < HudView.MaxCatalysts; i++) catalysts.Add(db.Catalysts[i]);
        var aspects = Get<List<AspectInstance>>(run, "aspects");
        int[] tiles = { 0, 2, 6 };
        foreach (var data in db.Aspects)
        {
            if (aspects.Count == HudView.MaxAspects) break;
            if (data.effect == AspectEffects.Cold || data.effect == AspectEffects.Bridge) continue;
            var inst = new AspectInstance { Data = data };
            if (data.scope != "Bench") inst.Tiles = new[] { tiles[aspects.Count] };
            if (data.effect == AspectEffects.Attuned) inst.Kind = Kind.Ore.ToString();
            aspects.Add(inst);
        }
        Set(run, "riShift", 2); Set(run, "handDelta", -1); Set(run, "tripleFirst", true);
        typeof(RunState).GetMethod("RebuildRules", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(run, null);
        return BenchWith(run, new Journal(), db);
    }

    static Journal KnownJournal()
    {
        var db = Db();
        var book = new Journal();
        for (int i = 0; i < 6; i++) book.Learn(db.Recipes[i].items);
        book.AddHint(db.Starters[0].id, Kind.Potion);
        book.AddHeatHint(db.Starters[1].id, 14);
        return book;
    }

    static GameObject JournalShot(Journal book)
    {
        var go = Instance(Views + "Journal.prefab");
        var view = go.GetComponent<JournalView>();
        view.Bind(Db());
        view.Open(book);
        return go;
    }

    static GameObject Goals()
    {
        var go = Instance(Views + "Goals.prefab");
        var db = Db();
        var view = go.GetComponent<GoalsView>();
        view.Bind(db);
        view.Open(new Profile(1), new Journal());
        return go;
    }

    static GameObject Choice()
    {
        var go = Instance(Views + "CatalystChoice.prefab");
        var view = go.GetComponent<CatalystChoiceView>();
        view.Bind(Fx(go));
        var cards = new List<ChoiceCard>
        {
            new ChoiceCard { id = "a", sprite = "dot", name = "Alpha", text = "first" },
            new ChoiceCard { id = "b", sprite = "dot", name = "Beta", text = "second", rarity = Rarities.Rare },
            new ChoiceCard { id = "c", sprite = "dot", name = "Gamma", text = "third" },
        };
        var choose = view.Choose("title", "line", cards, _ => { });
        choose.MoveNext();
        Tween.CompleteAll();              // the deal is over: the cards are home
        choose.MoveNext();
        return go;
    }

    /// <summary>The first event in the content assets a fresh run meets at depth 1.</summary>
    static RunState EventRun()
    {
        var db = Db();
        foreach (var e in db.Events)
        {
            var options = new RunOptions { Unlocks = Unlocks.Fresh(db), Events = true, ForceEvent = e.id, ForceEventAt = 1 };
            var run = new RunState(db, new Journal(), 1, 1, options);
            if (run.Phase == RunPhase.EventOffer) return run;
        }
        throw new InvalidOperationException("no event in the content opens a fresh run at depth 1");
    }

    static GameObject EventShot(bool outcome)
    {
        var go = Instance(Views + "Event.prefab");
        var view = go.GetComponent<EventView>();
        view.Bind(Fx(go));
        var run = EventRun();
        int last = run.EventOptions.Count - 1;      // the walk-on option: no gain, so no screen flash
        var show = view.Show(run, null);
        show.MoveNext();
        Tween.CompleteAll();                        // slide-in done
        if (!outcome) return go;
        show.MoveNext();
        Click(view.OptionLabel(last).GetComponentInParent<Clickable>());
        show.MoveNext();                            // outcome shown, waiting for Continue
        return go;
    }

    static GameObject RunOver(bool won)
    {
        var go = Instance(Views + "RunOver.prefab");
        var view = go.GetComponent<RunOverView>();
        view.Bind();
        view.ShowDepth(won, 4, Db().FinalDepth);
        return go;
    }

    static GameObject Pause(bool quitQuestion)
    {
        var go = Instance(Views + "Pause.prefab");
        var menu = go.GetComponent<PauseMenu>();
        menu.Bind(new Settings());
        menu.Open();
        if (quitQuestion) Click(go.transform.Find("Pause menu").Find("Button ui.pause.quit"));
        return go;
    }

    static GameObject Toast()
    {
        var go = Instance(Views + "GoalToast.prefab");
        var toast = go.GetComponent<GoalToast>();
        toast.Bind(Fx(go));
        toast.Add(new[] { new GoalReward { Goal = new GoalData { id = "g_depth2", text = "Reach depth 2" } } }, null);
        var update = typeof(GoalToast).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic);
        update.Invoke(toast, null);                 // takes the goal off the queue
        Set(toast, "age", 1f);                      // inside the hold: the panel is fully down whatever deltaTime was
        update.Invoke(toast, null);
        return go;
    }
}
} // namespace Athanor.Editor.Shared
