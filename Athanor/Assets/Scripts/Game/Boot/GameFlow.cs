using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace Athanor.Game
{
    public enum FlowState { Boot, Title, Intro, Bench, Resolving, Journal, Descent, CatalystChoice, Ending, RunOver, AspectChoice, Event, Goals }

    /// <summary>Owns the run and moves the game between its screens.</summary>
    public sealed class GameFlow : MonoBehaviour
    {
        public ArtLibrary library;
        public GameObject workshop, cutsceneRoom;
        public Camera mainCamera;

        public static GameFlow I { get; private set; }
        public ContentDb Db { get; private set; }
        public Journal Journal { get; private set; }
        /// <summary>What the player keeps between runs besides the journal: stats, goals done, items won.</summary>
        public Profile Profile { get; private set; }
        public RunState Run { get; private set; }
        public BenchView Bench { get; private set; }
        public FlowState State { get; private set; }
        public RunOverView RunOver { get; private set; }
        /// <summary>Debug: the scripted player plays and cutscenes skip themselves. F9 toggles it in the editor and in development builds.</summary>
        public bool Auto;

        // the room's ambient colour, the moonbeam's strength and the accent colour, one per depth
        static readonly Color[] Ambient =
        {
            new Color(0.52f, 0.52f, 1f), new Color(0.40f, 0.62f, 0.86f), new Color(0.62f, 0.42f, 0.95f), new Color(0.86f, 0.40f, 0.46f), new Color(0.78f, 0.74f, 0.58f),
        };
        static readonly float[] Moon = { 0.62f, 0.5f, 0.4f, 0.3f, 0.5f };
        static readonly string[] Accent = { "#7cc0f8", "#6ee6c8", "#c486f4", "#ee6a3c", "#fff6b8" };

        ReactionCutscene reaction; SimpleCutscene story; TitleView title; JournalView journalView; CatalystChoiceView choice;
        EventView eventView; GoalsView goalsView; GoalToast toast;
        readonly List<GoalReward> goalsThisRun = new List<GoalReward>();
        Unlocks unlocks;
        FlowState goalsFrom;
        Light2D ambient, moon;
        AutoPlayer autoPlayer;
        FlowState journalFrom;
        long shownPotency;
        bool hurry, inCutscene, runEnded, autoReady;
        float autoClock;
        int runs;

        void Awake()
        {
            I = this;
            Make.Lib = library;
#if UNITY_WEBGL && !UNITY_EDITOR
            Application.targetFrameRate = -1; QualitySettings.vSyncCount = 0;      // the browser paces frames
#else
            Application.targetFrameRate = 60;
#endif
            Db = new ContentDb(JsonUtility.FromJson<ContentData>(Resources.Load<TextAsset>("content").text));
            var layout = JsonUtility.FromJson<LayoutData>(Resources.Load<TextAsset>("layout").text);
            Journal = Save.LoadJournal();
            Profile = Save.LoadProfile(Db);
            unlocks = Unlocks.For(Db, Profile);
            autoPlayer = new AutoPlayer(Db);
            var light = GameObject.Find("Ambient Light"); if (light != null) ambient = light.GetComponent<Light2D>();
            var beam = workshop.transform.Find("Moonbeam"); if (beam != null) moon = beam.GetComponent<Light2D>();

            gameObject.AddComponent<ScreenFx>().Build(mainCamera);
            Bench = Make.Root("Bench").gameObject.AddComponent<BenchView>();
            Bench.Build(layout, Db, null);
            Bench.IgniteRequested += Ignite;
            Bench.RedrawRequested += () => { if (State == FlowState.Bench && Run.Redraw()) Bench.Refresh(true); };
            Bench.JournalRequested += OpenJournal;

            reaction = gameObject.AddComponent<ReactionCutscene>(); reaction.Build(cutsceneRoom, workshop, Bench.gameObject);
            story = gameObject.AddComponent<SimpleCutscene>(); story.Build(cutsceneRoom, workshop, Bench.gameObject);
            title = Make.Root("Title").gameObject.AddComponent<TitleView>(); title.Build();
            title.JournalClicked += OpenJournal;
            title.GoalsClicked += OpenGoals;
            title.ResetConfirmed += ResetProgress;
            journalView = Make.Root("Journal").gameObject.AddComponent<JournalView>(); journalView.Build(Db);
            journalView.Closed += CloseJournal;
            choice = Make.Root("Catalyst Choice").gameObject.AddComponent<CatalystChoiceView>(); choice.Build();
            RunOver = Make.Root("Run Over").gameObject.AddComponent<RunOverView>(); RunOver.Build();
            RunOver.GoalsClicked += OpenGoals;
            eventView = Make.Root("Event").gameObject.AddComponent<EventView>(); eventView.Build();
            goalsView = Make.Root("Goals").gameObject.AddComponent<GoalsView>(); goalsView.Build(Db);
            goalsView.Closed += CloseGoals;
            toast = Make.Root("Goal Toast").gameObject.AddComponent<GoalToast>(); toast.Build();
            Bench.gameObject.SetActive(false);
            ScreenFx.I.SetBlack(1);
        }

        void Start() => StartCoroutine(Main());

        IEnumerator Main()
        {
            while (true)
            {
                yield return ShowTitle();
                if (!Save.IntroSeen)
                {
                    State = FlowState.Intro;
                    inCutscene = true; yield return story.Intro(); inCutscene = false;
                    Save.IntroSeen = true;
                }
                do yield return PlayRun(); while (RunOver.Again);
            }
        }

        // ---- screens -----------------------------------------------------------------------------

        IEnumerator ShowTitle()
        {
            State = FlowState.Title;
            Bench.gameObject.SetActive(false);
            Mood(1);
            title.Open(Journal.KnownPairs.Count, Db.Recipes.Count, Profile.Completed.Count, Db.Goals.Count);
            // a journal kept from before there were goals pays out the journal goals at once
            CheckGoals(false);
            yield return ScreenFx.I.FadeIn(0.7f);
            while (!title.BeginPressed || State != FlowState.Title)
            {
                if (Auto && State == FlowState.Title) break;
                yield return null;
            }
            yield return ScreenFx.I.FadeOut(0.35f);
            title.Close();
            Sparks.I.Clear();
        }

        /// <summary>One run from the first trial to its summary. Starts and ends on a black screen.</summary>
        IEnumerator PlayRun()
        {
            runs++;
            // what the profile has unlocked is fixed when the run starts: an item won during it joins the next one
            unlocks = Unlocks.For(Db, Profile);
            Run = new RunState(Db, Journal, System.Environment.TickCount * 31 + runs, 1, RunOptions.Full(unlocks));
            runEnded = false; shownPotency = 0;
            goalsThisRun.Clear();
            Profile.RunStarted(); Profile.DepthBegan(Run.Depth);
            CheckGoals();
            Mood(1);
            Bench.gameObject.SetActive(true);
            Bench.Bind(Run, Journal);
            Bench.Refresh(true);
            Bench.InputEnabled = false;
            State = FlowState.Descent;
            yield return ScreenFx.I.FadeIn(0.5f);
            yield return StartOfDepth();
            while (!runEnded) yield return null;
            yield return ScreenFx.I.FadeOut(0.35f);
            story.Close();
            Sparks.I.Clear();
            Bench.InputEnabled = false;
        }

        /// <summary>
        /// The start of a depth, after the descent (or the title, for depth 1): the aspect offer where there is one,
        /// then the event if one was drawn, then the bench is handed to the player.
        /// </summary>
        IEnumerator StartOfDepth()
        {
            Bench.InputEnabled = false;
            while (Run.Phase == RunPhase.AspectOffer || Run.Phase == RunPhase.EventOffer)
            {
                if (Auto) { autoPlayer.Decide(Run); Bench.Refresh(); continue; }       // the scripted player needs no screen
                if (Run.Phase == RunPhase.AspectOffer)
                {
                    State = FlowState.AspectChoice;
                    string picked = null;
                    yield return choice.Choose(Run.AspectOffer, id => picked = id);
                    Run.ChooseAspect(picked);
                }
                else
                {
                    State = FlowState.Event;
                    yield return eventView.Show(Run, () => { Save.StoreJournal(Journal); Bench.Refresh(); });
                }
                Bench.Refresh();
            }
            Bench.Refresh(true);
            State = FlowState.Bench;
            Bench.InputEnabled = true;
        }

        /// <summary>
        /// Completes the goals whose targets have been reached, draws their rewards and saves the profile.
        /// announce: false keeps them off the run summary (goals paid outside a run).
        /// </summary>
        void CheckGoals(bool inRun = true)
        {
            var done = Profile.Evaluate(Db, Journal);
            Save.StoreProfile(Profile);
            if (done.Count == 0) return;
            if (inRun) goalsThisRun.AddRange(done);
            toast.Add(done, Db);
            if (State == FlowState.Title) title.Progress(Journal.KnownPairs.Count, Db.Recipes.Count, Profile.Completed.Count, Db.Goals.Count);
        }

        void OpenJournal()
        {
            if (State != FlowState.Bench && State != FlowState.Title) return;
            if (State == FlowState.Title && title.Asking) return;
            journalFrom = State; State = FlowState.Journal;
            if (journalFrom == FlowState.Bench) Bench.InputEnabled = false;
            // on the title what counts is what the profile has now; in a run, what the run was started with
            journalView.Open(Journal, journalFrom == FlowState.Title ? Unlocks.For(Db, Profile).IsOpen : (System.Func<string, bool>)unlocks.IsOpen);
        }

        void OpenGoals()
        {
            if (State != FlowState.Title && State != FlowState.RunOver) return;
            if (State == FlowState.Title && title.Asking) return;
            goalsFrom = State; State = FlowState.Goals;
            goalsView.Open(Profile, Journal);
        }

        void CloseGoals()
        {
            if (State == FlowState.Goals) State = goalsFrom;
        }

        /// <summary>Erases goals, unlocks and stats (the title's Reset progress, once confirmed). The journal is kept.</summary>
        void ResetProgress()
        {
            if (State != FlowState.Title) return;
            Save.ResetProfile();
            Profile = Save.LoadProfile(Db);
            unlocks = Unlocks.For(Db, Profile);
            toast.Clear();
            CheckGoals(false);           // the journal still counts toward the journal goals
            title.Progress(Journal.KnownPairs.Count, Db.Recipes.Count, Profile.Completed.Count, Db.Goals.Count);
        }

        void CloseJournal()
        {
            if (State != FlowState.Journal) return;
            State = journalFrom;
            if (State == FlowState.Bench) Bench.InputEnabled = true;
        }

        /// <summary>Lighting for a depth: the deeper, the stranger the colour and the weaker the moon.</summary>
        void Mood(int depth)
        {
            int i = Mathf.Clamp(depth, 1, Ambient.Length) - 1;
            if (ambient != null) ambient.color = Ambient[i];
            if (moon != null) moon.intensity = Moon[i];
        }

        // ---- Ignite ------------------------------------------------------------------------------

        /// <summary>Spends a trial and plays out what happened. Ignored unless the bench is waiting for the player.</summary>
        public void Ignite()
        {
            if (State != FlowState.Bench || Run == null || !Run.CanIgnite) return;
            State = FlowState.Resolving;
            StartCoroutine(Resolve());
        }

        bool IsDiscovery(string id) => Db.Material(id).tier > 0;

        IEnumerator Resolve()
        {
            Bench.InputEnabled = false;
            hurry = false;
            shownPotency = Run.Potency;
            // the run is settled first: whatever is skipped from here on, nothing can be lost
            var outcome = Run.Ignite();
            Save.StoreJournal(Journal);
            Profile.IgniteResolved(outcome);
            // the end of a run is counted with the Ignite that ended it: closing the page during the ending cannot lose a win
            bool ended = outcome.PhaseAfter == RunPhase.Won || outcome.PhaseAfter == RunPhase.Lost;
            if (ended) Profile.RunEnded(Run);
            var earned = Profile.Evaluate(Db, Journal);
            Save.StoreProfile(Profile);
            Bench.ClearLinks();
            Bench.Hud.Show(Run, Db, Journal, false);

            int n = 0, shows = 0;
            foreach (var e in outcome.Events)
            {
                float pace = hurry || Auto ? 0.4f : Mathf.Max(0.55f, 1 - n * 0.07f);
                if (e.Sputter) { Bench.Hud.Note(e, Db); yield return Sputter(e, pace); }
                else if (e.Result == null) { Bench.Hud.Note(e, Db); yield return Fizzle(e, pace); }
                else
                {
                    if (e.Discovery) yield return Discover(e, 1 + 0.4f * Mathf.Min(shows++, 3));
                    Bench.Hud.Note(e, Db);
                    yield return React(e, pace, n);
                    n++;
                }
            }
            // what the chain made and left standing flies to the rack; everything else on the bench is spent
            int kept = outcome.Made.Count(m => m);
            int slot = Mathf.Max(0, Run.Hand.Count(IsDiscovery) - kept);
            float endPace = hurry || Auto ? 0.4f : 0.8f;
            for (int i = 0; i < Core.Bench.TileCount; i++)
            {
                var tile = Bench.Tile(i);
                if (tile.Material == null) continue;
                if (outcome.Made[i] && outcome.Final[i] != null)
                {
                    var m = Db.Material(outcome.Final[i]);
                    tile.Clear();
                    StartCoroutine(ToRack(m, GlowOf(m), tile.centre + new Vector3(0, 12, 0), slot++, endPace));
                    yield return Anim.Wait(0.08f);
                }
                else StartCoroutine(tile.Consume());
            }
            yield return Anim.Wait(kept > 0 ? 0.85f * endPace : 0.3f);
            foreach (var id in outcome.Dropped)
                StartCoroutine(Bench.FloatText("Lost: " + Db.Material(id).name, Px.At(320, 232), Px.Hex("#ee6a3c"), 1.6f));
            shownPotency = Run.Potency;
            Bench.Hud.SetPotency(shownPotency);
            Bench.Refresh(true);
            // goals this Ignite completed are announced now that it has played out, so they never interrupt it
            // (those of a run's last Ignite wait for the summary, after the ending)
            goalsThisRun.AddRange(earned);
            if (!ended) toast.Add(earned, Db);

            switch (outcome.PhaseAfter)
            {
                case RunPhase.DepthCleared: yield return Descend(); break;
                case RunPhase.Lost: yield return EndRun(false, earned); break;
                case RunPhase.Won: yield return EndRun(true, earned); break;
                default: State = FlowState.Bench; Bench.InputEnabled = true; break;
            }
        }

        static Color GlowOf(MaterialData m) => Px.Hex(string.IsNullOrEmpty(m.glow) ? "#f0dcc0" : m.glow);
        /// <summary>Where a reaction shows: over its hub; for a pair that did nothing, between the two.</summary>
        Vector3 Between(ReactionEvent e) => e.Result != null
            ? Bench.TileCentre(e.Hub) + new Vector3(0, 12, 0)
            : (Bench.TileCentre(e.Tiles[0]) + Bench.TileCentre(e.Tiles[e.Tiles.Length - 1])) / 2 + new Vector3(0, 12, 0);

        void FlashTiles(ReactionEvent e, Color c) { foreach (int t in e.Tiles) Bench.Tile(t).Flash(c); }

        /// <summary>A pair with no recipe: a grey puff, and a hint if there was something new to say.</summary>
        IEnumerator Fizzle(ReactionEvent e, float pace)
        {
            var grey = new Color(0.62f, 0.62f, 0.7f);
            var mid = Between(e);
            FlashTiles(e, grey.Alpha(0.5f));
            Sparks.I.Burst(mid, 10, grey.Alpha(0.8f), 26, 0.7f, 150, -18);
            Fx.Spawn("poof", mid, null, 150);
            // a plain fizzle is over quickly; one that taught something lingers and says so
            if (e.Hint != null) StartCoroutine(Bench.FloatText("NEW HINT", mid + new Vector3(0, 8, 0), Px.Hex("#c486f4"), 1.1f, true));
            yield return Anim.Wait((e.Hint != null ? 0.55f : 0.24f) * pace);
        }

        /// <summary>A combo that stood ready and lost a material to a livelier reaction: a dull cough over its tiles.</summary>
        IEnumerator Sputter(ReactionEvent e, float pace)
        {
            var dull = new Color(0.55f, 0.4f, 0.75f);
            FlashTiles(e, dull.Alpha(0.6f));
            foreach (int t in e.Tiles) Sparks.I.Burst(Bench.TileCentre(t) + new Vector3(0, 10, 0), 5, dull.Alpha(0.8f), 22, 0.6f, 150, -14);
            StartCoroutine(Bench.FloatText("SPOILED", Bench.TileCentre(e.Hub) + new Vector3(0, 20, 0), Px.Hex("#c486f4"), 1.0f, true));
            yield return Anim.Wait(0.6f * pace);
        }

        /// <summary>A recipe nobody has seen before: a beat of held breath on the bench, then the full cutscene.</summary>
        IEnumerator Discover(ReactionEvent e, float hurry)
        {
            var mid = Between(e);
            FlashTiles(e, Color.white);
            ScreenFx.I.Shake(2, 0.35f);
            Sparks.I.Burst(mid, 26, new Color(2.2f, 2.1f, 1.8f), 95, 0.5f, 150);
            yield return Anim.Wait(0.4f);
            reaction.Setup(e, Db, hurry);
            inCutscene = true;
            yield return reaction.Play();
            inCutscene = false;
        }

        /// <summary>
        /// A reaction on the bench: the tiles flash, the neighbours are drawn into the hub, the new material
        /// stands up there (ready for the next link of the chain) and the points fly to the counter.
        /// </summary>
        IEnumerator React(ReactionEvent e, float pace, int chain)
        {
            var m = Db.Material(e.Result);
            var colour = GlowOf(m);
            var mid = Between(e);
            int size = e.Tiles.Length;
            FlashTiles(e, colour);
            for (int k = 1; k < size; k++)
            {
                var from = Bench.TileCentre(e.Tiles[k]) + new Vector3(0, 10, 0);
                StartCoroutine(Bench.Tile(e.Tiles[k]).Consume());
                StartCoroutine(Drawn(from, mid, colour, 0.22f * pace));
            }
            yield return Anim.Wait(0.2f * pace);
            Bench.Tile(e.Hub).Clear();
            Bench.Tile(e.Hub).Show(m, true);
            Sparks.I.Burst(mid, 14 + Mathf.Min(chain, 6) * 5 + (size - 2) * 8, colour.Times(2.2f), 60 + Mathf.Min(chain, 6) * 10, 0.55f, 150, 40);
            Fx.Spawn("collect-burst", mid, null, 150);
            ScreenFx.I.Shake(Mathf.Min(1 + chain * 0.5f + (size - 2) * 0.6f, 5), 0.16f);
            if (chain >= 2 || size >= 4) ScreenFx.I.Flash(Mathf.Min(0.05f * (chain + size - 2), 0.22f), 0.2f);
            string label = "+" + HudView.Number(e.Potency);
            StartCoroutine(Bench.FloatText(label, mid + new Vector3(0, 16, 0), Px.Hex("#f8d250"), 0.5f + 0.5f * pace));
            if (size >= 3) StartCoroutine(Bench.FloatText(size + "-COMBO", mid + new Vector3(0, 30, 0), Px.Hex("#ee6a3c"), 0.9f, true));
            else if (chain >= 1) StartCoroutine(Bench.FloatText("CHAIN x" + (chain + 1), mid + new Vector3(0, 30, 0), Px.Hex("#6ee6c8"), 0.9f, true));
            StartCoroutine(Points(mid, e.Potency, 0.5f * pace));
            yield return Anim.Wait(0.5f * pace);
        }

        /// <summary>A consumed material streaks into the hub.</summary>
        IEnumerator Drawn(Vector3 from, Vector3 to, Color colour, float seconds)
        {
            yield return Anim.Over(seconds, t =>
            {
                var p = Vector3.Lerp(from, to, Anim.InOut(t));
                Sparks.I.Emit(p, Random.insideUnitCircle * 8, colour.Times(2), 0.25f, 151, 0, true);
            });
        }

        /// <summary>A bright mote carries the points to the potency counter; they are added when it lands.</summary>
        IEnumerator Points(Vector3 from, long points, float seconds)
        {
            var to = Bench.Hud.PotencyPosition;
            var gold = new Color(2.4f, 1.9f, 0.7f);
            yield return Anim.Over(seconds, t =>
            {
                var p = Vector3.Lerp(from, to, Anim.InOut(t)) + new Vector3(0, Anim.Hump(t) * 34, 0);
                Sparks.I.Emit(p, Random.insideUnitCircle * 8, gold, 0.3f, 172, 0, true);
                Sparks.I.Emit(p, Random.insideUnitCircle * 14, gold.Alpha(0.7f), 0.25f, 171);
            });
            shownPotency += points;
            Bench.Hud.SetPotency(shownPotency);
            Bench.Hud.Bump();
            Sparks.I.Burst(to, 8, gold, 45, 0.3f, 172);
        }

        /// <summary>The new material pops out between the two tiles, then arcs down into its well on the rack.</summary>
        IEnumerator ToRack(MaterialData m, Color colour, Vector3 from, int slot, float pace)
        {
            var halo = Make.Glow(Bench.transform, from, 26, colour.Alpha(0.6f), 164);
            var icon = Make.Sprite(Bench.transform, m.id, from, 165);
            yield return Anim.Over(0.3f * pace, t =>
            {
                float s = Anim.OutBack(t) * 2;
                icon.transform.localScale = new Vector3(s, s, 1);
                icon.transform.position = halo.transform.position = Px.Snap(from + new Vector3(0, t * 8, 0));
            });
            yield return Anim.Wait(0.12f * pace);
            Vector3 start = icon.transform.position, to = Bench.RackSlot(slot);
            yield return Anim.Over(0.4f * pace, t =>
            {
                float k = Anim.InOut(t), s = Mathf.Lerp(2, 1, k);
                var p = Px.Snap(Vector3.Lerp(start, to, k) + new Vector3(0, Anim.Hump(t) * 20, 0));
                icon.transform.position = halo.transform.position = p;
                icon.transform.localScale = new Vector3(s, s, 1);
                halo.color = colour.Alpha(0.6f * (1 - t));
                if (Random.value < 0.6f) Sparks.I.Emit(p, Random.insideUnitCircle * 10, colour.Times(2), 0.3f, 163);
            });
            Destroy(icon.gameObject); Destroy(halo.gameObject);
            Bench.PreviewOnRack(slot, m.id);
            Sparks.I.Burst(to, 8, colour.Times(2), 40, 0.3f, 163);
        }

        // ---- between depths, and the end ------------------------------------------------------------

        IEnumerator Descend()
        {
            State = FlowState.Descent;
            yield return Anim.Wait(0.6f);                    // let the potency counter finish
            int next = Run.Depth + 1;
            inCutscene = true;
            yield return story.Descent(next, Db.DepthName(next), Px.Hex(Accent[Mathf.Clamp(next, 1, Accent.Length) - 1]), () => Mood(next));
            inCutscene = false;
            State = FlowState.CatalystChoice;
            string picked = null;
            yield return choice.Choose((IReadOnlyList<CatalystData>)Run.CatalystOffer, id => picked = id, Auto ? 0.8f : -1);
            Run.ChooseCatalyst(picked);
            Profile.DepthBegan(Run.Depth);
            CheckGoals();
            shownPotency = 0;
            Bench.gameObject.SetActive(true);
            Bench.Hud.SetPotency(0, true);
            Bench.Refresh();
            yield return StartOfDepth();
        }

        /// <summary>The ending or the loss, then the summary. earned: the goals the last Ignite completed, already saved.</summary>
        IEnumerator EndRun(bool won, List<GoalReward> earned)
        {
            if (won)
            {
                State = FlowState.Ending;
                yield return Anim.Wait(0.6f);
                inCutscene = true;
                yield return story.Ending(Db.Material(Db.Ultimate));
                inCutscene = false;
            }
            else
            {
                ScreenFx.I.Shake(3, 0.5f);
                yield return Anim.Wait(0.9f);
            }
            toast.Add(earned, Db);
            State = FlowState.RunOver;
            yield return RunOver.Show(won, Run, Journal, Db, goalsThisRun);
            runEnded = true;
        }

        // ---- debug autoplay ------------------------------------------------------------------------

        void Update()
        {
            var kb = Keyboard.current;
            if (kb != null && kb.f9Key.wasPressedThisFrame && (Application.isEditor || Debug.isDebugBuild)) Auto = !Auto;
            if (State == FlowState.Resolving && !inCutscene && PointerInput.Down) hurry = true;
            if (!Auto) return;
            autoClock -= Time.deltaTime;
            if (autoClock > 0) return;
            autoClock = 0.45f;
            reaction.Skip(); story.Skip();
            if (State != FlowState.Bench || Run == null || Run.Phase != RunPhase.Playing || !Bench.InputEnabled) return;
            if (autoReady && Run.CanIgnite) { autoReady = false; Ignite(); return; }
            var move = autoPlayer.Decide(Run);
            autoReady = move == AutoMove.ReadyToIgnite;
            Bench.Refresh(move == AutoMove.Redrew);
        }
    }
}
