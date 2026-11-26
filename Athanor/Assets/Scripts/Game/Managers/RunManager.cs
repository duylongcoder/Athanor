using Athanor.Gameplay;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The run being played, its seed, and the scripted player (F9). Rules stay in RunState; this only owns it.</summary>
    [DefaultExecutionOrder(-160)]
    public sealed class RunManager : Singleton<RunManager>
    {
        [Tooltip("0 = a fresh seed each run. Other values: every run uses this seed (PlayMode traces, bug reproduction).")]
        [SerializeField] int seedOverride;

        public int SeedOverride { get => seedOverride; set => seedOverride = value; }
        /// <summary>The run being played; null before the first one.</summary>
        public RunState Run { get; private set; }
        public AutoPlayer AutoPlayer { get; private set; }
        /// <summary>The seed the current run started with (shown in the Inspector, for bug reports).</summary>
        public int LastSeed { get; private set; }
        int runs;

        protected override void OnAwake() => AutoPlayer = new AutoPlayer(ContentManager.Instance.Db);

        /// <summary>The next run's seed: the override, or a fresh one.</summary>
        public int NextSeed()
        {
            runs++;
            return seedOverride != 0 ? seedOverride : System.Environment.TickCount * 31 + runs;
        }

        /// <summary>Starts a run at depth 1 on the player's journal.</summary>
        public RunState Begin(RunOptions options, int seed)
        {
            LastSeed = seed;
            return Run = new RunState(ContentManager.Instance.Db, ProgressManager.Instance.Journal, seed, 1, options);
        }

        /// <summary>Ignites and stores the journal at once, so a discovery is saved before anything else can happen. A sandbox stores nothing.</summary>
        public IgniteOutcome Settle() => SettleIgnite(Run, ProgressManager.Instance.Journal, ProgressManager.Instance.Sandbox);

        /// <summary>The Ignite and the journal store as one step; PauseTests call it without a scene.</summary>
        public static IgniteOutcome SettleIgnite(RunState run, Journal journal, bool sandbox)
        {
            var outcome = run.Ignite();
            if (!sandbox) Save.StoreJournal(journal);
            return outcome;
        }
    }
}
