using System.Collections.Generic;
using Athanor.Gameplay;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// What the player keeps between runs: the journal, the profile (stats, goals done, items won) and what is unlocked.
    /// A sandbox (How to play) swaps in a throwaway journal and profile and stores nothing.
    /// </summary>
    [DefaultExecutionOrder(-170)]
    public sealed class ProgressManager : Singleton<ProgressManager>
    {
        public Journal Journal { get; private set; }
        public Profile Profile { get; private set; }
        /// <summary>What was unlocked when the current run started; an item won during a run joins the next one.</summary>
        public Unlocks RunUnlocks { get; private set; }
        public bool Sandbox { get; private set; }
        Journal realJournal; Profile realProfile;
        static ContentDb Db => ContentManager.Instance.Db;

        protected override void OnAwake()
        {
            Journal = Save.LoadJournal();
            Profile = Save.LoadProfile(Db);
            RunUnlocks = Unlocks.For(Db, Profile);
        }

        public Unlocks RefreshUnlocks() => RunUnlocks = Unlocks.For(Db, Profile);

        public void StoreJournal() { if (!Sandbox) Save.StoreJournal(Journal); }
        public void StoreProfile() { if (!Sandbox) Save.StoreProfile(Profile); }

        /// <summary>Completes the goals whose targets are reached, draws their rewards and stores the profile.</summary>
        public List<GoalReward> EvaluateGoals()
        {
            var done = Profile.Evaluate(Db, Journal);
            StoreProfile();
            return done;
        }

        public void BeginSandbox()
        {
            realJournal = Journal; realProfile = Profile; Sandbox = true;
            Journal = new Journal(); Profile = new Profile(1);
        }

        public void EndSandbox()
        {
            Journal = realJournal; Profile = realProfile; Sandbox = false;
            RefreshUnlocks();
        }

        /// <summary>Erases goals, unlocks and stats (Reset progress). The journal is kept.</summary>
        public void ResetProgress()
        {
            Save.ResetProfile();
            Profile = Save.LoadProfile(Db);
            RefreshUnlocks();
        }

        /// <summary>Quit to title: back to what is stored, as if the page had been closed. A sandbox keeps its throwaway.</summary>
        public void ReloadAfterQuit()
        {
            Journal = JournalAfterQuit(Sandbox, Journal);
            Profile = ProfileAfterQuit(Sandbox, Profile, Db);
            RefreshUnlocks();
        }

        public static Journal JournalAfterQuit(bool sandbox, Journal current) => sandbox ? current : Save.LoadJournal();
        public static Profile ProfileAfterQuit(bool sandbox, Profile current, ContentDb db) => sandbox ? current : Save.LoadProfile(db);
    }
}
