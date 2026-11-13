using System;
using System.Collections.Generic;
using System.Linq;

namespace Athanor.Core
{
    /// <summary>A goal that has just been completed, and the locked item it gave (null when its pool was empty).</summary>
    public sealed class GoalReward
    {
        public GoalData Goal;
        public string Item;
    }

    /// <summary>
    /// What the player keeps between runs besides the journal: counters, completed goals and the
    /// locked starters and catalysts won from them.
    /// </summary>
    public sealed class Profile
    {
        public int RunsStarted, RunsWon, WonNoRedraw, DepthReached, Ignites, ChainLongest, ComboLargest;
        public long BestIgnite;
        public int RewardSeed;
        readonly HashSet<string> completed = new HashSet<string>();
        readonly HashSet<string> starters = new HashSet<string>(), catalysts = new HashSet<string>();
        readonly List<RewardLogData> rewards = new List<RewardLogData>();

        public Profile(int rewardSeed = 1) { RewardSeed = rewardSeed; }

        public IReadOnlyCollection<string> Completed => completed;
        public IReadOnlyCollection<string> UnlockedStarters => starters;
        public IReadOnlyCollection<string> UnlockedCatalysts => catalysts;
        public IReadOnlyList<RewardLogData> Rewards => rewards;
        public bool IsComplete(string goalId) => completed.Contains(goalId);
        public bool HasUnlocked(string id) => starters.Contains(id) || catalysts.Contains(id);
        /// <summary>The item a completed goal gave, or null.</summary>
        public string RewardOf(string goalId) => rewards.FirstOrDefault(r => r.goal == goalId)?.item;

        // ---- recording ----------------------------------------------------------------------------

        public void RunStarted() => RunsStarted++;

        public void DepthBegan(int depth) => DepthReached = Math.Max(DepthReached, depth);

        public void IgniteResolved(IgniteOutcome outcome)
        {
            Ignites++;
            BestIgnite = Math.Max(BestIgnite, outcome.Gained);
            foreach (var e in outcome.Events)
            {
                if (e.Result == null) continue;
                ChainLongest = Math.Max(ChainLongest, e.Chain);
                ComboLargest = Math.Max(ComboLargest, e.Items.Length);
            }
        }

        /// <summary>Call once when a run reaches Won or Lost.</summary>
        public void RunEnded(RunState run)
        {
            DepthReached = Math.Max(DepthReached, run.Depth);
            if (run.Phase != RunPhase.Won) return;
            RunsWon++;
            if (run.RedrawsUsed == 0) WonNoRedraw++;
        }

        // ---- goals --------------------------------------------------------------------------------

        public long Stat(string id, ContentDb db, Journal journal)
        {
            switch (id)
            {
                case StatIds.RunsStarted: return RunsStarted;
                case StatIds.RunsWon: return RunsWon;
                case StatIds.WonNoRedraw: return WonNoRedraw;
                case StatIds.DepthReached: return DepthReached;
                case StatIds.Ignites: return Ignites;
                case StatIds.ChainLongest: return ChainLongest;
                case StatIds.ComboLargest: return ComboLargest;
                case StatIds.BestIgnite: return BestIgnite;
                case StatIds.JournalEntries: return journal == null ? 0 : journal.KnownPairs.Count;
                case StatIds.JournalComplete: return journal != null && db.Recipes.All(r => journal.IsKnown(r.items)) ? 1 : 0;
                default: return 0;
            }
        }

        /// <summary>How far a goal is, capped at its target.</summary>
        public long Progress(GoalData goal, ContentDb db, Journal journal) =>
            IsComplete(goal.id) ? goal.target : Math.Min(goal.target, Stat(goal.stat, db, journal));

        /// <summary>
        /// Completes every goal whose target has been reached, in the order of the content file, and draws
        /// each one's reward from the items still locked in its bucket.
        /// </summary>
        public List<GoalReward> Evaluate(ContentDb db, Journal journal)
        {
            var done = new List<GoalReward>();
            foreach (var goal in db.Goals)
            {
                if (completed.Contains(goal.id) || Stat(goal.stat, db, journal) < goal.target) continue;
                var pool = db.LockedIn(goal.rewardKind, goal.rewardRarity).Where(i => !HasUnlocked(i)).ToList();
                string item = null;
                if (pool.Count > 0)
                {
                    item = pool[new Rng(RewardSeed + completed.Count * 7919).Range(pool.Count)];
                    (goal.rewardKind == RewardKinds.Starter ? starters : catalysts).Add(item);
                }
                completed.Add(goal.id);
                rewards.Add(new RewardLogData { goal = goal.id, item = item });
                done.Add(new GoalReward { Goal = goal, Item = item });
            }
            return done;
        }

        // ---- saving -------------------------------------------------------------------------------

        public ProfileData ToData() => new ProfileData
        {
            rewardSeed = RewardSeed, runsStarted = RunsStarted, runsWon = RunsWon, wonNoRedraw = WonNoRedraw, depthReached = DepthReached,
            ignites = Ignites, chainLongest = ChainLongest, comboLargest = ComboLargest, bestIgnite = BestIgnite,
            completed = completed.ToArray(), unlockedStarters = starters.ToArray(), unlockedCatalysts = catalysts.ToArray(),
            rewards = rewards.ToArray(),
        };

        /// <summary>Never throws: anything malformed or no longer in the content is skipped. db may be null (nothing is checked).</summary>
        public static Profile FromData(ProfileData d, ContentDb db = null, int freshSeed = 1)
        {
            if (d == null) return new Profile(freshSeed);
            var p = new Profile(d.rewardSeed == 0 ? freshSeed : d.rewardSeed)
            {
                RunsStarted = Math.Max(0, d.runsStarted), RunsWon = Math.Max(0, d.runsWon), WonNoRedraw = Math.Max(0, d.wonNoRedraw),
                DepthReached = Math.Max(0, d.depthReached), Ignites = Math.Max(0, d.ignites), ChainLongest = Math.Max(0, d.chainLongest),
                ComboLargest = Math.Max(0, d.comboLargest), BestIgnite = Math.Max(0, d.bestIgnite),
            };
            foreach (var id in d.completed ?? new string[0])
                if (!string.IsNullOrEmpty(id) && (db == null || db.Goals.Any(g => g.id == id))) p.completed.Add(id);
            foreach (var id in d.unlockedStarters ?? new string[0])
                if (!string.IsNullOrEmpty(id) && (db == null || (db.Has(id) && db.Material(id).tier == 0))) p.starters.Add(id);
            foreach (var id in d.unlockedCatalysts ?? new string[0])
                if (!string.IsNullOrEmpty(id) && (db == null || db.Catalyst(id) != null)) p.catalysts.Add(id);
            foreach (var r in d.rewards ?? new RewardLogData[0])
                if (r != null && !string.IsNullOrEmpty(r.goal) && p.completed.Contains(r.goal)) p.rewards.Add(r);
            return p;
        }
    }

    /// <summary>Which starting materials and catalysts a run may use.</summary>
    public sealed class Unlocks
    {
        readonly HashSet<string> open = new HashSet<string>();
        Unlocks() { }

        public bool IsOpen(string id) => open.Contains(id);
        public int Count => open.Count;

        /// <summary>Everything that is not locked in the content.</summary>
        public static Unlocks Fresh(ContentDb db)
        {
            var u = new Unlocks();
            foreach (var m in db.Starters) if (!m.locked) u.open.Add(m.id);
            foreach (var c in db.Catalysts) if (!c.locked) u.open.Add(c.id);
            return u;
        }

        public static Unlocks All(ContentDb db)
        {
            var u = new Unlocks();
            foreach (var m in db.Starters) u.open.Add(m.id);
            foreach (var c in db.Catalysts) u.open.Add(c.id);
            return u;
        }

        /// <summary>What is open by default plus what this profile has won.</summary>
        public static Unlocks For(ContentDb db, Profile profile)
        {
            var u = Fresh(db);
            if (profile == null) return u;
            foreach (var id in profile.UnlockedStarters) u.open.Add(id);
            foreach (var id in profile.UnlockedCatalysts) u.open.Add(id);
            return u;
        }

        /// <summary>Fresh, plus the first half (by id, shuffled by seed) of every locked bucket. For balance runs.</summary>
        public static Unlocks Half(ContentDb db, int seed)
        {
            var u = Fresh(db);
            var rng = new Rng(seed);
            foreach (var kind in new[] { RewardKinds.Starter, RewardKinds.Catalyst })
                foreach (var rarity in Rarities.All)
                {
                    var pool = db.LockedIn(kind, rarity);
                    rng.Shuffle(pool);
                    foreach (var id in pool.Take((pool.Count + 1) / 2)) u.open.Add(id);
                }
            return u;
        }
    }

    /// <summary>Switches for a run. The default is a Phase 2 run: no aspects, no events, and (with no Unlocks) only what the content leaves open.</summary>
    public sealed class RunOptions
    {
        public Unlocks Unlocks;
        public bool Aspects, Events;
        /// <summary>Balance checks: the id of an event to meet at every depth it may come to, in place of the draw.</summary>
        public string ForceEvent;
        /// <summary>With ForceEvent: the one depth to meet it at (the other depths then have no event). 0: every depth it may come to.</summary>
        public int ForceEventAt;
        public static readonly RunOptions Legacy = new RunOptions();
        public static RunOptions Full(Unlocks unlocks) => new RunOptions { Unlocks = unlocks, Aspects = true, Events = true };
    }
}
