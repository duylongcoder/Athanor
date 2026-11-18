using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using PrimeTween;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace Athanor.Game
{
    public enum FlowState { Boot, Title, Intro, Bench, Resolving, Journal, Descent, CatalystChoice, Ending, RunOver, AspectChoice, Event, Goals }

    /// <summary>Owns the run and moves the game between its screens.</summary>
    public sealed class GameFlow : MonoBehaviour
    {
        /// <summary>The art library (sprites and effect sheets); GameFlow publishes it as <see cref="Art.Lib"/>.</summary>
        public ArtLibrary library;
        /// <summary>content.json and the two language files, set in the scene. Awake throws when one is empty.</summary>
        [SerializeField] TextAsset contentJson, langEn, langVi;
        public GameObject workshop, cutsceneRoom;
        public Camera mainCamera;
        /// <summary>The HD-2D bench rig (null in a scene without it: the flat game).</summary>
        public DepthStage depthStage;

        public static GameFlow I { get; private set; }
        public ContentDb Db { get; private set; }
        public Journal Journal { get; private set; }
        /// <summary>What the player keeps between runs besides the journal: stats, goals done, items won.</summary>
        public Profile Profile { get; private set; }
        public RunState Run { get; private set; }
        public BenchView Bench { get; private set; }
        public FlowState State { get; private set; }
        /// <summary>The run summary (a prefab in Main.unity).</summary>
        [SerializeField] RunOverView runOver;
        public RunOverView RunOver => runOver;
        /// <summary>Debug: the scripted player plays and cutscenes skip themselves. F9 toggles it in the editor and in development builds.</summary>
        public bool Auto;

        /// <summary>The development-build log line of the F9 toggle, followed by "on" or "off" (tools/web/descents.mjs reads it).</summary>
        public const string AutoHook = "[auto] ";

        /// <summary>Turns the scripted player on or off (F9) and logs its new state in development builds.</summary>
        public void ToggleAuto()
        {
            Auto = !Auto;
            Dev.Hook(AutoHook + (Auto ? "on" : "off"));
        }

        // ---- development keys (tools/web/mainhd2d.mjs): F6 mood, F7 shake, F8 effects, F10 tile faces; editor and development builds only

        /// <summary>The depth mood after <paramref name="mood"/> in the F6 cycle 1..5.</summary>
        public static int NextMood(int mood) => mood % 5 + 1;
        public static string MoodLine(int mood) => "[dev] mood " + mood;
        public static string EffectsLine(bool on) => "[dev] effects " + (on ? "on" : "off");
        int devMood;

        void DevKeys(Keyboard kb)
        {
            if (!(Application.isEditor || Debug.isDebugBuild) || depthStage == null) return;
            if (kb.f6Key.wasPressedThisFrame) { devMood = NextMood(devMood); Mood(devMood); Dev.Hook(MoodLine(devMood)); }
            if (kb.f7Key.wasPressedThisFrame) depthStage.sway.Shake(3, 4f);
            if (kb.f8Key.wasPressedThisFrame && depthStage.effects != null)
            {
                bool on = !depthStage.effects.effectsOn;
                depthStage.effects.Set(on);
                if (window != null) window.Soft = on;
                Dev.Hook(EffectsLine(on));
            }
            if (kb.f10Key.wasPressedThisFrame && Bench != null)
                for (int i = 0; i < Athanor.Core.Bench.TileCount; i++)
                {
                    var t = Bench.Tile(i);
                    var pts = DepthMath.DrawnFace(t.FaceCentre, t.FaceSize, Px.H / 2f - t.centre.y, depthStage.rig.CamOffset, depthStage.rig.ImageShift, 3f);
                    Dev.Hook("[dev] tile " + i + " " + string.Join(" ", System.Array.ConvertAll(pts, p => p.x.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + "," + p.y.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture))));
                }
        }

        // the room's ambient colour, the moonbeam's strength and the accent colour, one per depth
        static readonly Color[] Ambient =
        {
            new Color(0.52f, 0.52f, 1f), new Color(0.40f, 0.62f, 0.86f), new Color(0.62f, 0.42f, 0.95f), new Color(0.86f, 0.40f, 0.46f), new Color(0.78f, 0.74f, 0.58f),
        };
        static readonly float[] Moon = { 0.62f, 0.5f, 0.4f, 0.3f, 0.5f };
        static readonly string[] Accent = { "#7cc0f8", "#6ee6c8", "#c486f4", "#ee6a3c", "#fff6b8" };

        ReactionCutscene reaction; SimpleCutscene story; WindowView window;
        /// <summary>The title screen (a prefab in Main.unity).</summary>
        [SerializeField] TitleView title;
        /// <summary>The journal (a prefab in Main.unity; its cells come from the JournalCell part).</summary>
        [SerializeField] JournalView journalView;
        /// <summary>The Goals page (a prefab in Main.unity; its rows come from the GoalRow part).</summary>
        [SerializeField] GoalsView goalsView;
        /// <summary>The three-card chooser for catalysts and aspects (a prefab in Main.unity).</summary>
        [SerializeField] CatalystChoiceView choice;
        /// <summary>The event dialog at the start of a depth (a prefab in Main.unity).</summary>
        [SerializeField] EventView eventView;
        /// <summary>The goal-complete toasts (a prefab in Main.unity).</summary>
        [SerializeField] GoalToast toast;
        readonly List<GoalReward> goalsThisRun = new List<GoalReward>();
        Unlocks unlocks;
        Settings settings;
        FlowState goalsFrom;
        Light2D ambient, moon;
        AutoPlayer autoPlayer;
        FlowState journalFrom;
        /// <summary>The journal screen, for the tutorial to point at parts of a page.</summary>
        public JournalView JournalView => journalView;
        long shownPotency;
        /// <summary>The in-run pause menu (a prefab in Main.unity).</summary>
        [SerializeField] PauseMenu pause;
        /// <summary>Parts/Hud.prefab: the bench instantiates it until the bench is a prefab itself.</summary>
        [SerializeField] HudView hudPrefab;
        Coroutine tutRoutine;
        int escGuardFrame = -1;
        bool hurry, inCutscene, runEnded, autoReady, abandoned;
        float autoClock;
        int runs;
        TutorialController tut, tutorial;
        Journal realJournal; Profile realProfile;
        /// <summary>A replay of the tutorial: a throwaway journal and profile, nothing written to storage.</summary>
        bool sandbox;
        enum TitleChoice { Begin, HowToPlay, Intro }
        TitleChoice titleChoice;
        readonly List<GoalReward> heldGoals = new List<GoalReward>();

        void StoreJournal() { if (!sandbox) Save.StoreJournal(Journal); }
        void StoreProfile() { if (!sandbox) Save.StoreProfile(Profile); }
        /// <summary>Goal toasts wait while the tutorial plays (and are dropped in a replay).</summary>
        void ToastGoals(List<GoalReward> done)
        {
            if (tut != null) { if (!sandbox) heldGoals.AddRange(done); return; }
            toast.Add(done, Db);
        }

        void Awake()
        {
            if (contentJson == null) throw new System.InvalidOperationException("GameFlow: contentJson is not set in the scene");
            if (langEn == null) throw new System.InvalidOperationException("GameFlow: langEn is not set in the scene");
            if (langVi == null) throw new System.InvalidOperationException("GameFlow: langVi is not set in the scene");
            if (library == null) throw new System.InvalidOperationException("GameFlow: library is not set in the scene");
            if (runOver == null) throw new System.InvalidOperationException("GameFlow: runOver is not set in the scene");
            if (toast == null) throw new System.InvalidOperationException("GameFlow: toast is not set in the scene");
            if (eventView == null) throw new System.InvalidOperationException("GameFlow: eventView is not set in the scene");
            if (choice == null) throw new System.InvalidOperationException("GameFlow: choice is not set in the scene");
            if (pause == null) throw new System.InvalidOperationException("GameFlow: pause is not set in the scene");
            if (title == null) throw new System.InvalidOperationException("GameFlow: title is not set in the scene");
            if (goalsView == null) throw new System.InvalidOperationException("GameFlow: goalsView is not set in the scene");
            if (journalView == null) throw new System.InvalidOperationException("GameFlow: journalView is not set in the scene");
            I = this;
            Art.Lib = library;
            Loc.Use(langEn, langVi);
            settings = Save.LoadSettings();
            Loc.InitFromPlayer(settings.language);
            Haptics.Enabled = settings.haptics;
            RenderSettings.skybox = null;                  // a 2D game: no skybox material to keep in the build
            gameObject.AddComponent<AudioMuteHook>();
#if UNITY_WEBGL && !UNITY_EDITOR
            Application.targetFrameRate = -1; QualitySettings.vSyncCount = 0;      // the browser paces frames
#else
            Application.targetFrameRate = 60;
#endif
            PrimeTweenConfig.SetTweensCapacity(200);   // a long chain runs many FloatTexts, Consumes and rack flights at once
            Db = new ContentDb(JsonUtility.FromJson<ContentData>(contentJson.text));
            var layout = JsonUtility.FromJson<LayoutData>(Resources.Load<TextAsset>("layout").text);
            Journal = Save.LoadJournal();
            Profile = Save.LoadProfile(Db);
            unlocks = Unlocks.For(Db, Profile);
            autoPlayer = new AutoPlayer(Db);
            var light = GameObject.Find("Ambient Light"); if (light != null) ambient = light.GetComponent<Light2D>();
            var beam = workshop.transform.Find("Moonbeam"); if (beam != null) moon = beam.GetComponent<Light2D>();

            gameObject.AddComponent<ScreenFx>().Build(mainCamera, depthStage != null ? depthStage.sway : null);
            PointerInput.Rig = depthStage != null ? depthStage.rig : null;
            Bench = Make.Root("Bench").gameObject.AddComponent<BenchView>();
            if (hudPrefab == null) throw new System.InvalidOperationException("GameFlow.hudPrefab is not set: bake Parts/Hud.prefab into Main.unity");
            Bench.Build(layout, Db, null, hudPrefab);
            if (depthStage != null) depthStage.PlaceTiles(Bench, layout.tiles.Length);
            Bench.IgniteRequested += Ignite;
            Bench.RedrawRequested += () => { if (State == FlowState.Bench && Run.Redraw()) Bench.Refresh(true); };
            Bench.JournalRequested += OpenJournal;

            reaction = gameObject.AddComponent<ReactionCutscene>(); reaction.Build(cutsceneRoom, workshop, Bench.gameObject);
            story = gameObject.AddComponent<SimpleCutscene>(); story.Build(cutsceneRoom, workshop, Bench.gameObject);
            window = gameObject.AddComponent<WindowView>(); window.Build(workshop.transform);
            if (depthStage != null) { depthStage.window = window; if (depthStage.effects != null) depthStage.effects.window = window; window.Soft = depthStage.effects == null || depthStage.effects.effectsOn; }
            title.Bind(settings);
            title.SettingsClosed += SaveSettings;
            title.JournalClicked += OpenJournal;
            title.GoalsClicked += OpenGoals;
            title.ResetConfirmed += ResetProgress;
            tut = null;
            tutorial = gameObject.AddComponent<TutorialController>(); tutorial.Build();
            journalView.Bind(Db);
            journalView.Closed += CloseJournal;
            choice.Bind();
            runOver.Bind();
            runOver.GoalsClicked += OpenGoals;
            eventView.Bind();
            goalsView.Bind(Db);
            goalsView.Closed += CloseGoals;
            toast.Bind();
            pause.Bind(settings);
            pause.CanOpen = CanPause;
            pause.Opened += OnPauseOpened;
            pause.Closed += OnPauseClosed;
            pause.QuitConfirmed += AbandonRun;
            pause.VibrationChanged += SaveSettings;
            Bench.Hud.PauseClicked += OpenPause;
            Bench.gameObject.SetActive(false);
            ScreenFx.I.SetBlack(1);
        }

        void SaveSettings() => Save.StoreSettings(settings);

        void OnPauseOpened() => Bench.InputEnabled = false;
        void OnPauseClosed() { if (State == FlowState.Bench) Bench.InputEnabled = true; }
        void OpenPause() => pause.Open();

        void OnDestroy()
        {
            PointerInput.Rig = null;
            if (runOver != null) runOver.GoalsClicked -= OpenGoals;
            if (goalsView != null) goalsView.Closed -= CloseGoals;
            if (journalView != null) journalView.Closed -= CloseJournal;
            if (title != null) { title.SettingsClosed -= SaveSettings; title.JournalClicked -= OpenJournal; title.GoalsClicked -= OpenGoals; title.ResetConfirmed -= ResetProgress; }
            if (pause != null) { pause.Opened -= OnPauseOpened; pause.Closed -= OnPauseClosed; pause.QuitConfirmed -= AbandonRun; pause.VibrationChanged -= SaveSettings; }
            if (Bench != null && Bench.Hud != null) Bench.Hud.PauseClicked -= OpenPause;
            Time.timeScale = 1f;
        }

        void Start() => StartCoroutine(Main());

        IEnumerator Main()
        {
            while (true)
            {
                yield return ShowTitle();
                if (titleChoice == TitleChoice.Intro)
                {
                    State = FlowState.Intro;
                    inCutscene = true; yield return story.Intro(); inCutscene = false;
                    story.Close(); Sparks.I.Clear();
                    continue;
                }
                if (titleChoice == TitleChoice.HowToPlay) { yield return PlayTutorialReplay(); continue; }
                if (!Save.IntroSeen)
                {
                    State = FlowState.Intro;
                    inCutscene = true; yield return story.Intro(); inCutscene = false;
                    Save.IntroSeen = true;
                }
                // the first run is the tutorial when the save has never finished it and the journal is empty
                bool first = !Auto && TutorialScript.Plan(Save.TutorialDone, Journal.KnownPairs.Count) == TutorialPlan.Run;
                do { yield return PlayRun(first); first = false; } while (!abandoned && RunOver.Again);
            }
        }

        /// <summary>How to play, and "Play it" of the skip question: the tutorial on a throwaway journal and profile, then the title.</summary>
        IEnumerator PlayTutorialReplay()
        {
            realJournal = Journal; realProfile = Profile; sandbox = true;
            Journal = new Journal(); Profile = new Profile(1);
            yield return PlayRun(true);
            Journal = realJournal; Profile = realProfile; sandbox = false;
            unlocks = Unlocks.For(Db, Profile);
            heldGoals.Clear();
        }

        // ---- screens -----------------------------------------------------------------------------

        IEnumerator ShowTitle()
        {
            State = FlowState.Title;
            Bench.gameObject.SetActive(false);
            Mood(1, false);
            title.Open(Journal.KnownPairs.Count, Db.Recipes.Count, Profile.Completed.Count, Db.Goals.Count);
            // a journal kept from before there were goals pays out the journal goals at once
            CheckGoals(false);
            yield return ScreenFx.I.FadeIn(0.7f);
            titleChoice = TitleChoice.Begin;
            while (true)
            {
                if (Auto && State == FlowState.Title) break;
                if (State == FlowState.Title && title.IntroPressed) { titleChoice = TitleChoice.Intro; break; }
                if (State == FlowState.Title && title.HowToPressed) { titleChoice = TitleChoice.HowToPlay; break; }
                if (State == FlowState.Title && title.BeginPressed)
                {
                    // a journal with entries and a tutorial never finished: ask once
                    if (!Auto && TutorialScript.Plan(Save.TutorialDone, Journal.KnownPairs.Count) == TutorialPlan.AskSkip)
                    {
                        int answer = 0; title.AskSkip(play => answer = play ? 1 : 2);
                        while (answer == 0) yield return null;
                        title.ClearBegin();
                        // Skip starts the run now (owner Q3); Play it is the sandbox replay (Q4)
                        titleChoice = TutorialRules.AnswerSkipQuestion(answer == 1) ? TitleChoice.HowToPlay : TitleChoice.Begin;
                        break;
                    }
                    break;
                }
                yield return null;
            }
            yield return ScreenFx.I.FadeOut(0.35f);
            title.Close();
            Sparks.I.Clear();
        }

        /// <summary>One run from the first trial to its summary. Starts and ends on a black screen.</summary>
        IEnumerator PlayRun(bool tutorialRun = false)
        {
            runs++;
            // what the profile has unlocked is fixed when the run starts: an item won during it joins the next one
            unlocks = Unlocks.For(Db, Profile);
            var options = tutorialRun ? TutorialScript.Options(unlocks) : RunOptions.Full(unlocks);
            options.LastLore = Profile.LastLore;
            Run = new RunState(Db, Journal, tutorialRun ? TutorialScript.Seed : System.Environment.TickCount * 31 + runs, 1, options);
            if (tutorialRun) { Run.Deal(TutorialScript.Reaction.Hand); tut = tutorial; } else tut = null;
            runEnded = false; abandoned = false; shownPotency = 0;
            goalsThisRun.Clear();
            Profile.RunStarted(); Profile.DepthBegan(Run.Depth);
            CheckGoals();
            Mood(1);
            Bench.gameObject.SetActive(true);
            Bench.Bind(Run, Journal);
            Bench.Refresh(true);
            Bench.InputEnabled = false;
            State = FlowState.Descent;
            Haptics.Pulse(Haptics.DepthCleared);
            yield return ScreenFx.I.FadeIn(0.5f);
            if (DepthLook.ShowsRunIntro(Run.Depth, tut != null, sandbox))
            {
                Dev.Hook("[intro] " + Run.Depth);
                inCutscene = true; yield return story.DepthIntro(DepthLook.IntroKey(Run.Depth)); inCutscene = false;
            }
            yield return StartOfDepth();
            if (tut != null) tutRoutine = StartCoroutine(RunTutorial());
            while (!runEnded) yield return null;
            yield return ScreenFx.I.FadeOut(0.35f);
            story.Close();
            Sparks.I.Clear();
            Bench.InputEnabled = false;
        }

        /// <summary>The tutorial's lessons; a replay ends at 5.5 and goes back to the title, the first run goes on as a normal run.</summary>
        IEnumerator RunTutorial()
        {
            yield return tut.Drive(this);
            var pending = heldGoals.ToArray(); heldGoals.Clear();
            bool replay = sandbox;
            tut = null;
            if (pending.Length > 0) toast.Add(new List<GoalReward>(pending), Db);
            if (replay) runEnded = true;
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
                    Dev.Hook("[start] aspects");       // development builds only: tools/web/webtest.mjs waits for [start] aspects / ready
                    yield return choice.Choose(Run.AspectOffer, id => picked = id);
                    Run.ChooseAspect(picked);
                }
                else
                {
                    State = FlowState.Event;
                    Profile.EventShown(Run.CurrentEvent);
                    StoreProfile();
                    yield return eventView.Show(Run, () => { StoreJournal(); Bench.Refresh(); });
                }
                Bench.Refresh();
            }
            Bench.Refresh(true);
            State = FlowState.Bench;
            Bench.InputEnabled = true;
            Dev.Hook("[start] ready");
        }

        /// <summary>
        /// Completes the goals whose targets have been reached, draws their rewards and saves the profile.
        /// announce: false keeps them off the run summary (goals paid outside a run).
        /// </summary>
        void CheckGoals(bool inRun = true)
        {
            var done = Profile.Evaluate(Db, Journal);
            StoreProfile();
            if (done.Count == 0) return;
            if (inRun) goalsThisRun.AddRange(done);
            ToastGoals(done);
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
            escGuardFrame = Time.frameCount;
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

        /// <summary>A pause may open only while the bench is the player's: not in a cutscene, an offer, the descent or the same frame Esc closed a screen.</summary>
        bool CanPause() => PauseMenu.Allowed(State, Bench != null && Bench.InputEnabled, inCutscene)
            && Run != null && Run.Phase == RunPhase.Playing && Time.frameCount != escGuardFrame;

        /// <summary>The journal after a quit: what is stored, as if the page had been closed (a How-to-play sandbox keeps its throwaway).</summary>
        public static Journal JournalAfterQuit(bool sandbox, Journal current) => sandbox ? current : Save.LoadJournal();
        /// <summary>The profile after a quit: what is stored. A run counts only at the Ignite that ends it, so nothing is added.</summary>
        public static Profile ProfileAfterQuit(bool sandbox, Profile current, ContentDb db) => sandbox ? current : Save.LoadProfile(db);

        /// <summary>Quit to title, confirmed: the run is thrown away uncounted (like closing the page) and Main goes back to the title.</summary>
        void AbandonRun()
        {
            if (State != FlowState.Bench || runEnded) return;
            Time.timeScale = 1f;
            abandoned = true;
            Bench.InputEnabled = false;
            Journal = JournalAfterQuit(sandbox, Journal);
            Profile = ProfileAfterQuit(sandbox, Profile, Db);
            unlocks = Unlocks.For(Db, Profile);
            goalsThisRun.Clear(); heldGoals.Clear(); toast.Clear();
            if (tut != null) { if (tutRoutine != null) StopCoroutine(tutRoutine); tutRoutine = null; tutorial.Abort(); tut = null; }
            Dev.Hook("[pause] quit");
            runEnded = true;
        }

        void CloseJournal()
        {
            escGuardFrame = Time.frameCount;
            if (State != FlowState.Journal) return;
            State = journalFrom;
            if (State == FlowState.Bench) Bench.InputEnabled = true;
        }

        /// <summary>Lighting for a depth: the deeper, the stranger the colour and the weaker the moon.</summary>
        void Mood(int depth, bool inRun = true)
        {
            int i = Mathf.Clamp(depth, 1, Ambient.Length) - 1;
            if (ambient != null) ambient.color = Ambient[i];
            if (moon != null) moon.intensity = Moon[i];
            if (window == null) return;
            if (depthStage != null) depthStage.ShowMood(depth, DepthLook.ThemedWindow(inRun, tut != null, sandbox));
            // the view through the window changes with the depth, inside a normal run only; the title and the tutorials keep the night sky
            if (DepthLook.ThemedWindow(inRun, tut != null, sandbox)) window.Show(depth); else window.ShowNight();
        }

        // ---- Ignite ------------------------------------------------------------------------------

        /// <summary>Spends a trial and plays out what happened. Ignored unless the bench is waiting for the player.</summary>
        public void Ignite()
        {
            if (State != FlowState.Bench || Run == null || !Run.CanIgnite) return;
            State = FlowState.Resolving;
            StartCoroutine(Resolve());
        }

        /// <summary>Ignites and stores the journal at once, so a discovery is in storage before anything else can happen (Quit, closed page). A sandbox stores nothing.</summary>
        public static IgniteOutcome SettleIgnite(RunState run, Journal journal, bool sandbox)
        {
            var outcome = run.Ignite();
            if (!sandbox) Save.StoreJournal(journal);
            return outcome;
        }

        bool IsDiscovery(string id) => Db.Material(id).tier > 0;

        IEnumerator Resolve()
        {
            Bench.InputEnabled = false;
            hurry = false;
            shownPotency = Run.Potency;
            // the run is settled first: whatever is skipped from here on, nothing can be lost
            var outcome = SettleIgnite(Run, Journal, sandbox);
            Profile.IgniteResolved(outcome);
            // the end of a run is counted with the Ignite that ended it: closing the page during the ending cannot lose a win
            bool ended = outcome.PhaseAfter == RunPhase.Won || outcome.PhaseAfter == RunPhase.Lost;
            if (ended) Profile.RunEnded(Run);
            var earned = Profile.Evaluate(Db, Journal);
            StoreProfile();
            Bench.ClearLinks();
            // the run is settled already: keep the Redraw button as it was until the chain has played (Bench.Refresh below updates it)
            Bench.Hud.Show(Run, Db, Journal, false, keepRedraw: true);

            int n = 0, shows = 0;
            foreach (var e in outcome.Events)
            {
                float pace = hurry || Auto ? 0.4f : Mathf.Max(0.55f, 1 - n * 0.07f);
                if (e.Sputter) { Bench.Hud.Note(e, Db); yield return Sputter(e, pace); }
                else if (e.Result == null) { Bench.Hud.Note(e, Db); yield return Fizzle(e, pace); if (tut != null) yield return tut.AfterEvent(e, n); }
                else
                {
                    if (e.Discovery) yield return Discover(e, 1 + 0.4f * Mathf.Min(shows++, 3));
                    Bench.Hud.Note(e, Db);
                    yield return React(e, pace, n);
                    if (tut != null) yield return tut.AfterEvent(e, n);
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
                StartCoroutine(Bench.FloatText(Loc.T("ui.bench.lost", ("item", Db.Material(id).name)), Px.At(320, 232), Px.Hex("#ee6a3c"), 1.6f));
            shownPotency = Run.Potency;
            Bench.Hud.SetPotency(shownPotency);
            if (tut != null) yield return tut.AfterIgnite(new TutorialController.IgniteOutcomeView { Events = outcome.Events });
            Bench.Refresh(true);
            // goals this Ignite completed are announced now that it has played out, so they never interrupt it
            // (those of a run's last Ignite wait for the summary, after the ending)
            goalsThisRun.AddRange(earned);
            if (!ended) ToastGoals(earned);

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
            if (e.Hint != null) StartCoroutine(Bench.FloatText(Loc.T("ui.bench.new_hint"), mid + new Vector3(0, 8, 0), Px.Hex("#c486f4"), 1.1f, true));
            yield return Anim.Wait((e.Hint != null ? 0.55f : 0.24f) * pace);
        }

        /// <summary>A combo that stood ready and lost a material to a livelier reaction: a dull cough over its tiles.</summary>
        IEnumerator Sputter(ReactionEvent e, float pace)
        {
            var dull = new Color(0.55f, 0.4f, 0.75f);
            FlashTiles(e, dull.Alpha(0.6f));
            foreach (int t in e.Tiles) Sparks.I.Burst(Bench.TileCentre(t) + new Vector3(0, 10, 0), 5, dull.Alpha(0.8f), 22, 0.6f, 150, -14);
            StartCoroutine(Bench.FloatText(Loc.T("ui.bench.spoiled"), Bench.TileCentre(e.Hub) + new Vector3(0, 20, 0), Px.Hex("#c486f4"), 1.0f, true));
            yield return Anim.Wait(0.6f * pace);
        }

        /// <summary>A recipe nobody has seen before: a beat of held breath on the bench, then the full cutscene.</summary>
        IEnumerator Discover(ReactionEvent e, float hurry)
        {
            Haptics.Pulse(Haptics.Discovery);
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
            if (size >= 3) StartCoroutine(Bench.FloatText(Loc.T("ui.bench.combo", ("n", size)), mid + new Vector3(0, 30, 0), Px.Hex("#ee6a3c"), 0.9f, true));
            else if (chain >= 1) StartCoroutine(Bench.FloatText(Loc.T("ui.bench.chain", ("n", chain + 1)), mid + new Vector3(0, 30, 0), Px.Hex("#6ee6c8"), 0.9f, true));
            StartCoroutine(Points(mid, e.Potency, 0.5f * pace));
            yield return Anim.Wait(0.5f * pace);
        }

        /// <summary>A consumed material streaks into the hub.</summary>
        IEnumerator Drawn(Vector3 from, Vector3 to, Color colour, float seconds)
        {
            yield return Anim.Over(seconds, t =>
            {
                var p = Vector3.Lerp(from, to, Anim.InOut(t));
                Sparks.I.Trail(p, Random.insideUnitCircle * 8, colour.Times(2), 0.25f, 151, 0, true);
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
                Sparks.I.Trail(p, Random.insideUnitCircle * 8, gold, 0.3f, 172, 0, true);
                Sparks.I.Trail(p, Random.insideUnitCircle * 14, gold.Alpha(0.7f), 0.25f, 171);
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
            float pop = 0.3f * pace, fly = 0.4f * pace;
            var up = from + new Vector3(0, 8, 0);
            yield return Sequence.Create()
                .Group(Motion.Scale(icon.transform, 0, 2, pop, Ease.OutBack))
                .Group(Motion.Move(icon.transform, from, up, pop))
                .Group(Motion.Move(halo.transform, from, up, pop))
                .ToYieldInstruction();
            yield return Anim.Wait(0.12f * pace);
            Vector3 start = icon.transform.position, to = Bench.RackSlot(slot);
            var flight = Sequence.Create()
                .Group(Motion.Arc(icon.transform, start, to, 20, fly, Ease.InOutSine))
                .Group(Motion.Arc(halo.transform, start, to, 20, fly, Ease.InOutSine))
                .Group(Motion.Scale(icon.transform, 2, 1, fly, Ease.InOutSine))
                .Group(Motion.Fade(halo, 0.6f, 0, fly));
            while (flight.isAlive)
            {
                if (Random.value < 0.6f) Sparks.I.Trail(icon.transform.position, Random.insideUnitCircle * 10, colour.Times(2), 0.3f, 163);
                yield return null;
            }
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
            Dev.Hook("[descent] " + next);       // development builds only: tools/web/descents.mjs waits for it
            yield return story.Descent(next, Loc.DepthName(Db, next), Px.Hex(Accent[Mathf.Clamp(next, 1, Accent.Length) - 1]), () => Mood(next));
            inCutscene = false;
            State = FlowState.CatalystChoice;
            Dev.Hook("[choice] " + Run.Depth);      // development builds only: tools/web evidence scripts wait for it
            string picked = null;
            if (tut != null) yield return tut.OnOffer();
            var offer = (IReadOnlyList<CatalystData>)Run.CatalystOffer;
            yield return choice.Choose(offer, id => picked = id, Auto ? 0.8f : -1);
            string pickedName = offer.First(c => c.id == picked).name;
            Run.ChooseCatalyst(picked);
            if (tut != null) { tut.OfferTaken(); tut.AfterChoose(picked, pickedName); }
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
            Haptics.Pulse(Haptics.RunEnded);
            ToastGoals(earned);
            State = FlowState.RunOver;
            Dev.Hook("[runover] " + (won ? "won" : "lost"));
            yield return RunOver.Show(won, Run, Journal, Db, goalsThisRun);
            runEnded = true;
        }

        // ---- debug autoplay ------------------------------------------------------------------------

        void Update()
        {
            if (depthStage != null)
            {
                depthStage.SwayOn = settings.sway;
                if (depthStage.rig.Flat != cutsceneRoom.activeSelf) depthStage.SetFlat(cutsceneRoom.activeSelf);
            }
            var kb = Keyboard.current;
            if (kb != null) DevKeys(kb);
            if (kb != null && kb.f9Key.wasPressedThisFrame && (Application.isEditor || Debug.isDebugBuild)) ToggleAuto();
            if (State == FlowState.Resolving && !inCutscene && PointerInput.Down) hurry = true;
            if (Bench != null && Bench.Hud != null) Bench.Hud.SetPauseShown(!pause.IsOpen && CanPause());
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
