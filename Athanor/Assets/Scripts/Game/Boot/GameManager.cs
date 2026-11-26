using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Athanor.Gameplay;
using PrimeTween;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace Athanor.Game
{
    /// <summary>Owns the run and moves the game between its screens.</summary>
    public sealed class GameManager : Singleton<GameManager>
    {
        /// <summary>The art library (sprites and effect sheets); GameManager publishes it as <see cref="Art.Lib"/>.</summary>
        public ArtLibrary library;
        /// <summary>The colours files share (Assets/Data/Palette.asset); GameManager publishes it as <see cref="Art.Palette"/>.</summary>
        [SerializeField] Palette palette;
        public GameObject workshop, cutsceneRoom;
        public Camera mainCamera;
        /// <summary>The HD-2D bench rig (null in a scene without it: the flat game).</summary>
        public DepthStage depthStage;

        public ContentDb Db => ContentManager.Instance.Db;
        public Journal Journal => ProgressManager.Instance.Journal;
        static ProgressManager Progress => ProgressManager.Instance;
        /// <summary>What the player keeps between runs besides the journal: stats, goals done, items won.</summary>
        public Profile Profile => ProgressManager.Instance.Profile;
        public RunState Run => RunManager.Instance.Run;
        static RunManager Runs => RunManager.Instance;
        /// <summary>The bench (Prefabs/Views/Bench.prefab in Main.unity): tiles, hand, rack and the Hud.</summary>
        public BenchView Bench => bench;
        /// <summary>The screen now up (read-only; <see cref="Flow"/> moves it).</summary>
        public FlowState State => flow.State;
        /// <summary>The screen state machine GameManager drives.</summary>
        public ScreenFlow Flow => flow;
        readonly ScreenFlow flow = new ScreenFlow();
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
                for (int i = 0; i < Athanor.Gameplay.Bench.TileCount; i++)
                {
                    var t = Bench.Tile(i);
                    var pts = DepthMath.DrawnFace(t.FaceCentre, t.FaceSize, Px.H / 2f - t.centre.y, depthStage.rig.CamOffset, depthStage.rig.ImageShift, 3f, depthStage.rig.distance, depthStage.rig.table.Shape);
                    Dev.Hook("[dev] tile " + i + " " + string.Join(" ", System.Array.ConvertAll(pts, p => p.x.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + "," + p.y.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture))));
                }
        }

        /// <summary>Waits and shake of the descent and the end of a run, in seconds.</summary>
        [System.Serializable]
        public struct RunTiming
        {
            /// <summary>Wait before the descent cutscene (the potency counter finishes) and before the ending.</summary>
            public float descendWait, endingWait;
            /// <summary>A lost run: shake strength, shake length, wait.</summary>
            public float lossShake, lossShakeSeconds, lossWait;
            /// <summary>The title fades in and out; a run fades in and out (black to the first depth, the summary to black).</summary>
            public float titleFadeIn, titleFadeOut, runFadeIn, runFadeOut;
            /// <summary>Autoplay: how long it waits on a catalyst choice, and the pause between its moves.</summary>
            public float autoChoiceWait, autoPace;
        }
        [SerializeField] RunTiming runTiming;

        /// <summary>The discovery sequence (the Reaction Cutscene object of Main.unity).</summary>
        [SerializeField] ReactionCutscene reaction;
        /// <summary>The intro, descent and ending (the Story Cutscene object of Main.unity).</summary>
        [SerializeField] SimpleCutscene story;
        /// <summary>The camera shake and the flash and fade of the whole screen (the Screen Fx object of Main.unity).</summary>
        [SerializeField] ScreenFx screenFx;
        [SerializeField] Sparks sparks;
        FxRefs fx;
        /// <summary>The Ignite choreography (on the GameManager object of Main.unity).</summary>
        [SerializeField] IgniteSequence ignite;
        /// <summary>The workshop window's effects (the Window object of Main.unity).</summary>
        [SerializeField] WindowView window;
        /// <summary>The tutorial overlay (the Tutorial object of Main.unity).</summary>
        [SerializeField] TutorialManager tutorial;
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
        Settings Settings => SaveManager.Instance.Settings;
        /// <summary>The journal screen, for the tutorial to point at parts of a page.</summary>
        public JournalView JournalView => journalView;
        /// <summary>The in-run pause menu (a prefab in Main.unity).</summary>
        [SerializeField] PauseMenu pause;
        [SerializeField] BenchView bench;
        Coroutine tutRoutine;
        int escGuardFrame = -1;
        bool runEnded, autoReady, abandoned;
        float autoClock;
        TutorialManager tut;
        /// <summary>A replay of the tutorial: a throwaway journal and profile, nothing written to storage.</summary>
        bool sandbox => flow.Sandbox;
        enum TitleChoice { Begin, HowToPlay, Intro }
        TitleChoice titleChoice;
        readonly List<GoalReward> heldGoals = new List<GoalReward>();

        void StoreJournal() => Progress.StoreJournal();
        void StoreProfile() => Progress.StoreProfile();
        /// <summary>Goal toasts wait while the tutorial plays (and are dropped in a replay).</summary>
        void ToastGoals(List<GoalReward> done)
        {
            if (tut != null) { if (!sandbox) heldGoals.AddRange(done); return; }
            toast.Add(done, Db);
        }

        protected override void OnAwake()
        {
            if (library == null) throw new System.InvalidOperationException("GameManager: library is not set in the scene");
            if (palette == null) throw new System.InvalidOperationException("GameManager: palette is not set in the scene");
            if (runOver == null) throw new System.InvalidOperationException("GameManager: runOver is not set in the scene");
            if (toast == null) throw new System.InvalidOperationException("GameManager: toast is not set in the scene");
            if (eventView == null) throw new System.InvalidOperationException("GameManager: eventView is not set in the scene");
            if (choice == null) throw new System.InvalidOperationException("GameManager: choice is not set in the scene");
            if (pause == null) throw new System.InvalidOperationException("GameManager: pause is not set in the scene");
            if (screenFx == null) throw new System.InvalidOperationException("GameManager: screenFx is not set in the scene");
            if (sparks == null) throw new System.InvalidOperationException("GameManager: sparks is not set in the scene");
            if (ignite == null) throw new System.InvalidOperationException("GameManager: ignite is not set in the scene");
            if (reaction == null) throw new System.InvalidOperationException("GameManager: reaction is not set in the scene");
            if (story == null) throw new System.InvalidOperationException("GameManager: story is not set in the scene");
            if (window == null) throw new System.InvalidOperationException("GameManager: window is not set in the scene");
            if (tutorial == null) throw new System.InvalidOperationException("GameManager: tutorial is not set in the scene");
            if (title == null) throw new System.InvalidOperationException("GameManager: title is not set in the scene");
            if (goalsView == null) throw new System.InvalidOperationException("GameManager: goalsView is not set in the scene");
            if (journalView == null) throw new System.InvalidOperationException("GameManager: journalView is not set in the scene");
            Art.Lib = library;
            Art.Palette = palette;
            RenderSettings.skybox = null;                  // a 2D game: no skybox material to keep in the build
            gameObject.AddComponent<AudioMuteHook>();
#if UNITY_WEBGL && !UNITY_EDITOR
            Application.targetFrameRate = -1; QualitySettings.vSyncCount = 0;      // the browser paces frames
#else
            Application.targetFrameRate = 60;
#endif
            PrimeTweenConfig.SetTweensCapacity(200);   // a long chain runs many FloatTexts, Consumes and rack flights at once

            fx = new FxRefs(screenFx, sparks);
            screenFx.Bind(mainCamera, depthStage != null ? depthStage.sway : null);
            PointerInput.Use(depthStage != null ? depthStage.rig : null);
            if (bench == null) throw new System.InvalidOperationException("GameManager.bench is not set: put Prefabs/Views/Bench.prefab in Main.unity and assign it");
            bench.Bind(Db, fx);
            if (depthStage != null) depthStage.PlaceTiles(Bench, Bench.TileCount);
            Bench.IgniteRequested += Ignite;
            Bench.RedrawRequested += OnRedraw;
            Bench.JournalRequested += OpenJournal;

            reaction.Bind(new[] { workshop, Bench.gameObject }, fx);
            ignite.Bind(Bench, reaction, fx, flow);
            story.Bind(fx);
            window.Bind();
            if (depthStage != null) { depthStage.window = window; if (depthStage.effects != null) depthStage.effects.window = window; window.Soft = depthStage.effects == null || depthStage.effects.effectsOn; }
            title.Bind(Settings, fx);
            title.SettingsClosed += SaveSettings;
            title.JournalClicked += OpenJournal;
            title.GoalsClicked += OpenGoals;
            title.ResetConfirmed += ResetProgress;
            tut = null;
            tutorial.Bind(this);
            journalView.Bind(Db);
            journalView.Closed += CloseJournal;
            choice.Bind(fx);
            runOver.Bind();
            runOver.GoalsClicked += OpenGoals;
            eventView.Bind(fx);
            goalsView.Bind(Db);
            goalsView.Closed += CloseGoals;
            toast.Bind(fx);
            pause.Bind(Settings);
            pause.CanOpen = CanPause;
            pause.Opened += OnPauseOpened;
            pause.Closed += OnPauseClosed;
            pause.QuitConfirmed += AbandonRun;
            pause.VibrationChanged += SaveSettings;
            Bench.Hud.PauseClicked += OpenPause;
            Bench.gameObject.SetActive(false);
            fx.Screen.SetBlack(1);
        }

        void SaveSettings() => SaveManager.Instance.StoreSettings();

        void OnRedraw() { if (State == FlowState.Bench && Run.Redraw()) Bench.Refresh(true); }

        void OnPauseOpened() => Bench.InputEnabled = false;
        void OnPauseClosed() { if (State == FlowState.Bench) Bench.InputEnabled = true; }
        void OpenPause() => pause.Open();

        /// <summary>Opens the Journal as the bench's Journal button would (the tutorial's auto-drive).</summary>
        public void RequestJournal() => OpenJournal();

        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopAllCoroutines(); Motion.StopOn(this);
            PointerInput.Use(null);
            if (runOver != null) runOver.GoalsClicked -= OpenGoals;
            if (goalsView != null) goalsView.Closed -= CloseGoals;
            if (journalView != null) journalView.Closed -= CloseJournal;
            if (title != null) { title.SettingsClosed -= SaveSettings; title.JournalClicked -= OpenJournal; title.GoalsClicked -= OpenGoals; title.ResetConfirmed -= ResetProgress; }
            if (pause != null) { pause.Opened -= OnPauseOpened; pause.Closed -= OnPauseClosed; pause.QuitConfirmed -= AbandonRun; pause.VibrationChanged -= SaveSettings; }
            if (Bench != null && Bench.Hud != null) Bench.Hud.PauseClicked -= OpenPause;
            if (Bench != null) { Bench.IgniteRequested -= Ignite; Bench.RedrawRequested -= OnRedraw; Bench.JournalRequested -= OpenJournal; }
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
                    flow.Enter(FlowState.Intro);
                    flow.InCutscene = true; yield return story.Intro(); flow.InCutscene = false;
                    story.Close(); fx.Sparks.Clear();
                    continue;
                }
                if (titleChoice == TitleChoice.HowToPlay) { yield return PlayTutorialReplay(); continue; }
                if (!SaveManager.Instance.IntroSeen)
                {
                    flow.Enter(FlowState.Intro);
                    flow.InCutscene = true; yield return story.Intro(); flow.InCutscene = false;
                    SaveManager.Instance.SetIntroSeen(true);
                }
                // the first run is the tutorial when the save has never finished it and the journal is empty
                bool first = !Auto && TutorialGate.Plan(SaveManager.Instance.TutorialDone, Journal.KnownPairs.Count) == TutorialPlan.Run;
                do { yield return PlayRun(first); first = false; } while (!abandoned && RunOver.Again);
            }
        }

        /// <summary>How to play, and "Play it" of the skip question: the tutorial on a throwaway journal and profile, then the title.</summary>
        IEnumerator PlayTutorialReplay()
        {
            flow.BeginSandbox(); Progress.BeginSandbox();
            yield return PlayRun(true);
            Progress.EndSandbox(); flow.EndSandbox();
            heldGoals.Clear();
        }

        // ---- screens -----------------------------------------------------------------------------

        IEnumerator ShowTitle()
        {
            flow.Enter(FlowState.Title);
            Bench.gameObject.SetActive(false);
            Mood(1, false);
            title.Open(Journal.KnownPairs.Count, Db.Recipes.Count, Profile.Completed.Count, Db.Goals.Count);
            // a journal kept from before there were goals pays out the journal goals at once
            CheckGoals(false);
            yield return fx.Screen.FadeIn(runTiming.titleFadeIn);
            titleChoice = TitleChoice.Begin;
            while (true)
            {
                if (Auto && State == FlowState.Title) break;
                if (State == FlowState.Title && title.IntroPressed) { titleChoice = TitleChoice.Intro; break; }
                if (State == FlowState.Title && title.HowToPressed) { titleChoice = TitleChoice.HowToPlay; break; }
                if (State == FlowState.Title && title.BeginPressed)
                {
                    // a journal with entries and a tutorial never finished: ask once
                    if (!Auto && TutorialGate.Plan(SaveManager.Instance.TutorialDone, Journal.KnownPairs.Count) == TutorialPlan.AskSkip)
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
            yield return fx.Screen.FadeOut(runTiming.titleFadeOut);
            title.Close();
            fx.Sparks.Clear();
        }

        /// <summary>One run from the first trial to its summary. Starts and ends on a black screen.</summary>
        IEnumerator PlayRun(bool tutorialRun = false)
        {
            // what the profile has unlocked is fixed when the run starts: an item won during it joins the next one
            var unlocks = Progress.RefreshUnlocks();
            var options = tutorialRun ? tutorial.Sequence.Options(unlocks) : RunOptions.Full(unlocks);
            options.LastLore = Profile.LastLore;
            Runs.Begin(options, tutorialRun ? tutorial.Sequence.seed : Runs.NextSeed());
            if (tutorialRun) { Run.Deal(tutorial.Sequence.Trial(0).Hand); tut = tutorial; } else tut = null;
            runEnded = false; abandoned = false;
            goalsThisRun.Clear();
            Profile.RunStarted(); Profile.DepthBegan(Run.Depth);
            CheckGoals();
            Mood(1);
            Bench.gameObject.SetActive(true);
            Bench.Bind(Run, Journal);
            Bench.Refresh(true);
            Bench.InputEnabled = false;
            flow.Enter(FlowState.Descent);
            Haptics.Pulse(Haptics.DepthCleared);
            yield return fx.Screen.FadeIn(runTiming.runFadeIn);
            if (DepthLook.ShowsRunIntro(Run.Depth, tut != null, sandbox))
            {
                Dev.Hook("[intro] " + Run.Depth);
                flow.InCutscene = true; yield return story.DepthIntro(DepthLook.IntroKey(Run.Depth)); flow.InCutscene = false;
            }
            yield return StartOfDepth();
            if (tut != null) tutRoutine = StartCoroutine(RunTutorial());
            while (!runEnded) yield return null;
            yield return fx.Screen.FadeOut(runTiming.runFadeOut);
            story.Close();
            fx.Sparks.Clear();
            Bench.InputEnabled = false;
        }

        /// <summary>The tutorial's lessons; a replay ends at 5.5 and goes back to the title, the first run goes on as a normal run.</summary>
        IEnumerator RunTutorial()
        {
            yield return tut.Drive();
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
                if (Auto) { Runs.AutoPlayer.Decide(Run); Bench.Refresh(); continue; }       // the scripted player needs no screen
                if (Run.Phase == RunPhase.AspectOffer)
                {
                    flow.Enter(FlowState.AspectChoice);
                    string picked = null;
                    Dev.Hook("[start] aspects");       // development builds only: tools/web/webtest.mjs waits for [start] aspects / ready
                    yield return choice.Choose(Run.AspectOffer, id => picked = id);
                    Run.ChooseAspect(picked);
                }
                else
                {
                    flow.Enter(FlowState.Event);
                    Profile.EventShown(Run.CurrentEvent);
                    StoreProfile();
                    yield return eventView.Show(Run, () => { StoreJournal(); Bench.Refresh(); });
                }
                Bench.Refresh();
            }
            Bench.Refresh(true);
            flow.Enter(FlowState.Bench);
            Bench.InputEnabled = true;
            Dev.Hook("[start] ready");
        }

        /// <summary>
        /// Completes the goals whose targets have been reached, draws their rewards and saves the profile.
        /// announce: false keeps them off the run summary (goals paid outside a run).
        /// </summary>
        void CheckGoals(bool inRun = true)
        {
            var done = Progress.EvaluateGoals();
            if (done.Count == 0) return;
            if (inRun) goalsThisRun.AddRange(done);
            ToastGoals(done);
            if (State == FlowState.Title) title.Progress(Journal.KnownPairs.Count, Db.Recipes.Count, Profile.Completed.Count, Db.Goals.Count);
        }

        void OpenJournal()
        {
            if (!flow.OpenJournal(title.Asking)) return;
            if (flow.JournalFrom == FlowState.Bench) Bench.InputEnabled = false;
            // on the title what counts is what the profile has now; in a run, what the run was started with
            journalView.Open(Journal, flow.JournalFrom == FlowState.Title ? Unlocks.For(Db, Profile).IsOpen : (System.Func<string, bool>)Progress.RunUnlocks.IsOpen);
        }

        void OpenGoals()
        {
            if (!flow.OpenGoals(title.Asking)) return;
            goalsView.Open(Profile, Journal);
        }

        void CloseGoals()
        {
            escGuardFrame = Time.frameCount;
            flow.CloseGoals();
        }

        /// <summary>Erases goals, unlocks and stats (the title's Reset progress, once confirmed). The journal is kept.</summary>
        void ResetProgress()
        {
            if (!flow.CanResetProgress) return;
            Progress.ResetProgress();
            toast.Clear();
            CheckGoals(false);           // the journal still counts toward the journal goals
            title.Progress(Journal.KnownPairs.Count, Db.Recipes.Count, Profile.Completed.Count, Db.Goals.Count);
        }

        /// <summary>A pause may open only while the bench is the player's: not in a cutscene, an offer, the descent or the same frame Esc closed a screen.</summary>
        bool CanPause() => flow.CanPause(Bench != null && Bench.InputEnabled, Run != null && Run.Phase == RunPhase.Playing)
            && Time.frameCount != escGuardFrame;

        /// <summary>Quit to title, confirmed: the run is thrown away uncounted (like closing the page) and Main goes back to the title.</summary>
        void AbandonRun()
        {
            if (!flow.CanAbandon(runEnded)) return;
            Time.timeScale = 1f;
            abandoned = true;
            Bench.InputEnabled = false;
            Progress.ReloadAfterQuit();
            goalsThisRun.Clear(); heldGoals.Clear(); toast.Clear();
            if (tut != null) { if (tutRoutine != null) StopCoroutine(tutRoutine); tutRoutine = null; tutorial.Abort(); tut = null; }
            Dev.Hook("[pause] quit");
            runEnded = true;
        }

        void CloseJournal()
        {
            escGuardFrame = Time.frameCount;
            if (!flow.CloseJournal()) return;
            if (State == FlowState.Bench) Bench.InputEnabled = true;
        }

        /// <summary>Lighting for a depth, read from the scene's presets (<see cref="DepthStage.Look"/>): the deeper, the stranger the colour and the weaker the moon.</summary>
        void Mood(int depth, bool inRun = true)
        {
            if (window == null) return;
            if (depthStage != null) depthStage.ShowMood(depth, DepthLook.ThemedWindow(inRun, tut != null, sandbox));
            // the view through the window changes with the depth, inside a normal run only; the title and the tutorials keep the night sky
            if (DepthLook.ThemedWindow(inRun, tut != null, sandbox)) window.Show(depth); else window.ShowNight();
        }

        // ---- Ignite ------------------------------------------------------------------------------

        /// <summary>Spends a trial and plays out what happened. Ignored unless the bench is waiting for the player.</summary>
        public void Ignite()
        {
            if (!flow.StartIgnite(Run != null && Run.CanIgnite)) return;
            StartCoroutine(Resolve());
        }

        IEnumerator Resolve()
        {
            Bench.InputEnabled = false;
            long potencyBefore = Run.Potency;
            // the run is settled first: whatever is skipped from here on, nothing can be lost
            var outcome = Runs.Settle();
            Profile.IgniteResolved(outcome);
            Dev.Hook("[ignite] d" + Run.Depth + " +" + outcome.Gained + " =" + Run.Potency + " " + outcome.PhaseAfter + " "
                + string.Join(",", outcome.Events.Where(e => e.Result != null).Select(e => e.Result)));
            // the end of a run is counted with the Ignite that ended it: closing the page during the ending cannot lose a win
            bool ended = outcome.PhaseAfter == RunPhase.Won || outcome.PhaseAfter == RunPhase.Lost;
            if (ended) Profile.RunEnded(Run);
            var earned = Progress.EvaluateGoals();
            Bench.ClearLinks();
            // the run is settled already: keep the Redraw button as it was until the chain has played (Bench.Refresh below updates it)
            Bench.Hud.Show(Run, Db, Journal, false, keepRedraw: true);

            yield return ignite.Play(outcome, Run, Db, potencyBefore, tut, Auto);
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
                default: flow.Enter(FlowState.Bench); Bench.InputEnabled = true; break;
            }
        }

        // ---- between depths, and the end ------------------------------------------------------------

        IEnumerator Descend()
        {
            flow.Enter(FlowState.Descent);
            yield return Anim.Wait(runTiming.descendWait);   // let the potency counter finish
            int next = Run.Depth + 1;
            flow.InCutscene = true;
            Dev.Hook("[descent] " + next);       // development builds only: tools/web/descents.mjs waits for it
            yield return story.Descent(next, Loc.DepthName(Db, next), depthStage != null ? depthStage.Look(next).accent : Color.white, () => Mood(next));
            flow.InCutscene = false;
            flow.Enter(FlowState.CatalystChoice);
            Dev.Hook("[choice] " + Run.Depth);      // development builds only: tools/web evidence scripts wait for it
            string picked = null;
            if (tut != null) yield return tut.OnOffer();
            var offer = (IReadOnlyList<CatalystData>)Run.CatalystOffer;
            bool autoPick = Auto || (tut != null && tut.AutoDrive);
            yield return choice.Choose(offer, id => picked = id, autoPick ? runTiming.autoChoiceWait : -1);
            string pickedName = offer.First(c => c.id == picked).name;
            Run.ChooseCatalyst(picked);
            if (tut != null) { tut.OfferTaken(); tut.AfterChoose(picked, pickedName); }
            Profile.DepthBegan(Run.Depth);
            CheckGoals();
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
                flow.Enter(FlowState.Ending);
                yield return Anim.Wait(runTiming.endingWait);
                flow.InCutscene = true;
                yield return story.Ending(Db.Material(Db.Ultimate));
                flow.InCutscene = false;
            }
            else
            {
                fx.Screen.Shake(runTiming.lossShake, runTiming.lossShakeSeconds);
                yield return Anim.Wait(runTiming.lossWait);
            }
            Haptics.Pulse(Haptics.RunEnded);
            ToastGoals(earned);
            flow.Enter(FlowState.RunOver);
            Dev.Hook("[runover] " + (won ? "won" : "lost"));
            yield return RunOver.Show(won, Run, Journal, Db, goalsThisRun);
            runEnded = true;
        }

        // ---- debug autoplay ------------------------------------------------------------------------

        void Update()
        {
            if (depthStage != null)
            {
                depthStage.SwayOn = Settings.sway;
                if (depthStage.rig.Flat != cutsceneRoom.activeSelf) depthStage.SetFlat(cutsceneRoom.activeSelf);
            }
            var kb = Keyboard.current;
            if (kb != null) DevKeys(kb);
            if (kb != null && kb.f9Key.wasPressedThisFrame && (Application.isEditor || Debug.isDebugBuild)) ToggleAuto();
            if (State == FlowState.Resolving && !flow.InCutscene && PointerInput.Down) ignite.Hurry = true;
            if (Bench != null && Bench.Hud != null) Bench.Hud.SetPauseShown(!pause.IsOpen && CanPause());
            if (tut != null && tut.AutoDrive) { reaction.RequestSkip(); story.RequestSkip(); }
            if (!Auto) return;
            autoClock -= Time.deltaTime;
            if (autoClock > 0) return;
            autoClock = runTiming.autoPace;
            reaction.RequestSkip(); story.RequestSkip();
            if (State != FlowState.Bench || Run == null || Run.Phase != RunPhase.Playing || !Bench.InputEnabled) return;
            if (autoReady && Run.CanIgnite) { autoReady = false; Ignite(); return; }
            var move = Runs.AutoPlayer.Decide(Run);
            autoReady = move == AutoMove.ReadyToIgnite;
            Bench.Refresh(move == AutoMove.Redrew);
        }
    }
}
