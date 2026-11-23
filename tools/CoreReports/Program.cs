using System;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using Athanor.Tests;

/// <summary>
/// The balance reports. They print numbers and assert nothing, so they live here rather than in the test suite.
///   dotnet run --project tools/CoreReports -- balance-matrix | events | roles | newcomer | balance | trace
/// Settings come from the same ATHANOR_* environment variables the old Explicit tests read.
/// </summary>
static class Program
{
    const string Usage = "usage: CoreReports balance-matrix | events | roles | newcomer | balance | trace";

    static int Main(string[] args)
    {
        switch (args.Length > 0 ? args[0] : null)
        {
            case "balance-matrix": BalanceMatrix(); return 0;
            case "events": EventReport(); return 0;
            case "roles": RoleReport(); return 0;
            case "newcomer": NewcomerTrace(); return 0;
            case "balance": BalanceReport(); return 0;
            case "trace": TraceRun(); return 0;
            default: Console.Error.WriteLine(Usage); return 2;
        }
    }

    static void BalanceMatrix()
    {
        var db = new ContentDb(TestContent.Load());
        Action<string> w = Console.WriteLine;
        int seeds = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_SEEDS"), out int n) ? n : 200;
        RunState.PouchMode = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_POUCH"), out int mode) ? mode : 2;
        w("MATRIX pouch mode " + RunState.PouchMode);
        foreach (bool mods in new[] { false, true })
        {
            string tag = mods ? " +aspects+events" : "";
            w("MATRIX " + Sim.Line(db, "fresh" + tag, s => new RunOptions { Unlocks = Unlocks.Fresh(db), Aspects = mods, Events = mods }, seeds));
            w("MATRIX " + Sim.Line(db, "fresh + all catalysts" + tag, s => new RunOptions { Unlocks = Sim.With(db, false, true), Aspects = mods, Events = mods }, seeds));
            w("MATRIX " + Sim.Line(db, "fresh + all starters" + tag, s => new RunOptions { Unlocks = Sim.With(db, true, false), Aspects = mods, Events = mods }, seeds));
            w("MATRIX " + Sim.Line(db, "half" + tag, s => new RunOptions { Unlocks = Unlocks.Half(db, s), Aspects = mods, Events = mods }, seeds));
            w("MATRIX " + Sim.Line(db, "full" + tag, s => new RunOptions { Unlocks = Unlocks.All(db), Aspects = mods, Events = mods }, seeds));
        }
        w("MATRIX " + Sim.Line(db, "fresh, aspects only", s => new RunOptions { Unlocks = Unlocks.Fresh(db), Aspects = true }, seeds));
        w("MATRIX " + Sim.Line(db, "fresh, events only", s => new RunOptions { Unlocks = Unlocks.Fresh(db), Events = true }, seeds));
    }

    /// <summary>
    /// The spec's rule for events (8.4): none may make a depth impossible. Each event in turn is put in the informed
    /// player's way and the wins are set against the same runs with no event at all (the percentage): a hazard or a
    /// gamble once at each depth it may come to, which must keep 70%, and every event at all its depths in one run.
    /// Then the player who knows nothing, who must stay near no wins whatever is unlocked.
    /// </summary>
    static void EventReport()
    {
        var db = new ContentDb(TestContent.Load());
        Action<string> w = Console.WriteLine;
        int seeds = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_SEEDS"), out int n) ? n : 200;
        int Wins(string force, int at = 0)
        {
            int wins = 0;
            for (int seed = 1; seed <= seeds; seed++)
                if (Sim.Autoplay(db, seed, new RunOptions { Unlocks = Unlocks.Fresh(db), Aspects = true, Events = force != null, ForceEvent = force, ForceEventAt = at }).Phase == RunPhase.Won) wins++;
            return wins;
        }
        int plain = Wins(null);
        string Of(int wins) => wins + "/" + seeds + " (" + (100 * wins / Math.Max(1, plain)) + "%)";
        w("EVENT " + "no event".PadRight(18) + " wins " + plain + "/" + seeds);
        string only = Environment.GetEnvironmentVariable("ATHANOR_EVENT");      // one event's line, by id
        foreach (var e in db.Events)
        {
            if (!string.IsNullOrEmpty(only) && e.id != only) continue;
            // met once, at each depth it may come to (the spec's measure), then at all of them in one run (the worst a run can draw)
            var once = new List<string>();
            if (e.type == "Hazard" || e.type == "Gamble" || Environment.GetEnvironmentVariable("ATHANOR_EVERY_EVENT") == "1")
                for (int d = e.minDepth; d <= e.maxDepth; d++) once.Add("d" + d + " " + Of(Wins(e.id, d)));
            w("EVENT " + e.id.PadRight(18) + " " + e.type.PadRight(7) + " at every depth " + e.minDepth + " to " + e.maxDepth + ": " + Of(Wins(e.id))
                + (once.Count > 0 ? "; once, at " + string.Join(", ", once) : ""));
        }
        if (!string.IsNullOrEmpty(only)) return;
        foreach (bool full in new[] { false, true })
        {
            int wins = 0; var endedAt = new int[RunState.FinalDepth + 1];
            for (int seed = 1; seed <= seeds; seed++)
            {
                var run = Sim.RandomPlay(db, seed, RunOptions.Full(full ? Unlocks.All(db) : Unlocks.Fresh(db)));
                if (run.Phase == RunPhase.Won) wins++;
                endedAt[run.Depth]++;
            }
            w("RANDOM " + (full ? "full" : "fresh").PadRight(6) + " wins " + wins + "/" + seeds + ", ended at depth 1..5: " + string.Join(" ", endedAt.Skip(1)));
        }
    }

    static void RoleReport()
    {
        var db = new ContentDb(TestContent.Load());
        Action<string> w = Console.WriteLine;
        int players = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_PLAYERS"), out int p) ? p : 40;
        int adminRuns = 60, newRuns = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_RUNS"), out int r) ? r : 200;
        var admins = Enumerable.Range(1, players).Select(i => Sim.Play(db, true, i, adminRuns)).ToList();
        Sim.Describe("ADMIN", admins, adminRuns, w);
        var newcomers = Enumerable.Range(1, players).Select(i => Sim.Play(db, false, i, newRuns)).ToList();
        Sim.Describe("NEWCOMER", newcomers, newRuns, w);
        if (int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_TAUGHT"), out int taught))
        {
            var told = Enumerable.Range(1, players).Select(i => Sim.Play(db, false, i, newRuns, true, true, null, 0, taught)).ToList();
            Sim.Describe("NEWCOMER TOLD TIER<=" + taught, told, newRuns, w);
        }
        // the same two players with no aspects and no events, to see what Phase 3's run modifiers do
        var plainAdmins = Enumerable.Range(1, players).Select(i => Sim.Play(db, true, i, adminRuns, false, false)).ToList();
        Sim.Describe("ADMIN (no aspects, no events)", plainAdmins, adminRuns, w);
    }

    static void NewcomerTrace()
    {
        var db = new ContentDb(TestContent.Load());
        var log = new List<string>();
        int from = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_FROM"), out int f) ? f : 100;
        Sim.Play(db, false, 1, from + 2, true, true, log, from);
        foreach (var line in log) Console.WriteLine("TRACE " + line);
    }

    static void BalanceReport()
    {
        var db = new ContentDb(TestContent.Load());
        const int Seeds = 200;
        int wins = 0, spare = 0, byCombo = 0, firstTrial = 0, ignites = 0, sputters = 0, runsWith5 = 0; var lost = new List<string>();
        var combos = new SortedDictionary<string, int>(); var comboRuns = new SortedDictionary<string, int>();
        var clearedTrials = new int[RunState.FinalDepth + 1]; var cleared = new int[RunState.FinalDepth + 1];
        var bySize = new int[ContentDb.MaxItems + 1]; var lostAt = new int[RunState.FinalDepth + 1]; var chains = new List<int>();
        for (int seed = 1; seed <= Seeds; seed++)
        {
            var st = new Sim.RunStats();
            var run = Sim.Autoplay(db, seed, null, st);
            if (run.Phase == RunPhase.Won) { wins++; spare += run.TrialsLeft; if (st.UltimateByCombo) byCombo++; }
            else { lost.Add(run.Depth + ":" + run.Potency); lostAt[run.Depth]++; }
            for (int k = 2; k <= ContentDb.MaxItems; k++) bySize[k] += st.BySize[k];
            if (st.BySize[5] > 0) runsWith5++;
            foreach (var c in st.Combos)
            {
                combos[c.Key] = (combos.TryGetValue(c.Key, out int n) ? n : 0) + c.Value;
                comboRuns[c.Key] = (comboRuns.TryGetValue(c.Key, out int m) ? m : 0) + 1;
            }
            foreach (var c in st.ClearedIn) { int d = c[0] - '0'; cleared[d]++; clearedTrials[d] += int.Parse(c.Substring(2)); }
            firstTrial += st.FirstTrialClears; ignites += st.Ignites; sputters += st.Sputters; chains.Add(st.LongestChain);
        }
        int randomWins = 0; var randomDepth = new int[RunState.FinalDepth + 1];
        for (int seed = 1; seed <= Seeds; seed++)
        {
            var run = Sim.RandomPlay(db, seed);
            if (run.Phase == RunPhase.Won) randomWins++;
            randomDepth[run.Depth]++;
        }
        // the same player on the same content with no catalysts on offer: no depth may depend on one
        // (goals go too: the content will not load with a goal that has no locked catalyst left to pay out)
        var bare = TestContent.Load(); bare.catalysts = new CatalystData[0]; bare.goals = new GoalData[0];
        var bareDb = new ContentDb(bare);
        int bareWins = 0; var bareLostAt = new int[RunState.FinalDepth + 1];
        for (int seed = 1; seed <= Seeds; seed++)
        {
            var run = Sim.Autoplay(bareDb, seed);
            if (run.Phase == RunPhase.Won) bareWins++; else bareLostAt[run.Depth]++;
        }
        // and with each catalyst taken out of the game in turn: the win rate must not hang on any one of them
        var without = new List<string>();
        foreach (var c in db.Catalysts)
        {
            var less = TestContent.Load(); less.catalysts = less.catalysts.Where(x => x.id != c.id).ToArray(); less.goals = new GoalData[0];
            var lessDb = new ContentDb(less);
            int n = 0;
            for (int seed = 1; seed <= Seeds; seed++) if (Sim.Autoplay(lessDb, seed).Phase == RunPhase.Won) n++;
            without.Add(c.id + " " + n);
        }
        var w = Console.Out;
        w.WriteLine("informed, one catalyst removed: wins of " + Seeds + ": " + string.Join(", ", without));
        w.WriteLine("informed: wins " + wins + "/" + Seeds + ", trials to spare " + (spare / Math.Max(1, wins)) + ", won through a combo-made ingredient " + byCombo);
        w.WriteLine("informed: lost at depth 1..5: " + string.Join(" ", lostAt.Skip(1)) + "; depth 1 cleared on the first trial " + firstTrial + "/" + Seeds);
        w.WriteLine("informed: reactions per run by size 2/3/4/5: " + string.Join(" / ", bySize.Skip(2).Select(n => (n / (double)Seeds).ToString("0.00")))
            + "; runs with a 5-item combo " + runsWith5 + "/" + Seeds + "; ignites per run " + (ignites / (double)Seeds).ToString("0.0")
            + "; longest chain avg " + chains.Average().ToString("0.0") + " max " + chains.Max() + "; sputters per run " + (sputters / (double)Seeds).ToString("0.00"));
        w.WriteLine("informed: trials used to clear depth 1..4 (avg): " + string.Join(" ", Enumerable.Range(1, 4).Select(d => (clearedTrials[d] / (double)Math.Max(1, cleared[d])).ToString("0.0"))));
        w.WriteLine("informed: combos, fired total (runs): " + string.Join("; ", combos.Select(c => c.Key + " " + c.Value + " (" + comboRuns[c.Key] + ")")));
        w.WriteLine("informed, no catalysts: wins " + bareWins + "/" + Seeds + ", lost at depth 1..5: " + string.Join(" ", bareLostAt.Skip(1)));
        w.WriteLine("uninformed: wins " + randomWins + "/" + Seeds + ", ended at depth 1..5: " + string.Join(" ", randomDepth.Skip(1)));
        w.WriteLine("lost at " + string.Join(" ", lost));
        int shown = 0;
        for (int seed = 1; seed <= Seeds && shown < 3; seed++)
        {
            var log = new List<string>();
            var r1 = Sim.Autoplay(bareDb, seed, log);
            if (r1.Phase == RunPhase.Won || r1.Depth != 3) continue;
            shown++;
            w.WriteLine("TRACE no catalysts, seed " + seed + ": " + r1.Phase + " at depth " + r1.Depth + "\n" + string.Join("\n", log));
        }
    }

    /// <summary>Prints one scripted run trial by trial: ATHANOR_SEED=17 dotnet run --project tools/CoreReports -- trace.</summary>
    static void TraceRun()
    {
        var db = new ContentDb(TestContent.Load());
        int seed = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_SEED"), out int n) ? n : 1;
        var log = new List<string>();
        var run = Sim.Autoplay(db, seed, log);
        Console.WriteLine("TRACE seed " + seed + ": " + run.Phase + " at depth " + run.Depth + ", catalysts "
            + string.Join(",", run.Catalysts.Select(c => c.id)) + "\n" + string.Join("\n", log));
    }
}
