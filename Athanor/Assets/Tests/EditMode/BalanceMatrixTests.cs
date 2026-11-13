using System;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>
    /// The informed scripted player on fixed profiles, to see what each part of Phase 3 does to the win rate.
    /// dotnet test tools/CoreTests --filter BalanceMatrix -l "console;verbosity=detailed"
    /// </summary>
    public class BalanceMatrixTests
    {
        public static Unlocks With(ContentDb db, bool starters, bool catalysts)
        {
            var data = new ProfileData
            {
                unlockedStarters = starters ? db.Starters.Where(m => m.locked).Select(m => m.id).ToArray() : new string[0],
                unlockedCatalysts = catalysts ? db.Catalysts.Where(c => c.locked).Select(c => c.id).ToArray() : new string[0],
            };
            return Unlocks.For(db, Profile.FromData(data, db));
        }

        public static RunState Autoplay(ContentDb db, int seed, RunOptions options)
        {
            var run = new RunState(db, new Journal(), seed, 1, options);
            var bot = new AutoPlayer(db);
            for (int guard = 0; guard < 600 && run.Phase != RunPhase.Won && run.Phase != RunPhase.Lost; guard++)
                if (bot.Decide(run) == AutoMove.ReadyToIgnite) run.Ignite();
            return run;
        }

        public static string Line(ContentDb db, string label, Func<int, RunOptions> options, int seeds)
        {
            int wins = 0; var lostAt = new int[RunState.FinalDepth + 1]; double depth = 0;
            for (int seed = 1; seed <= seeds; seed++)
            {
                var run = Autoplay(db, seed, options(seed));
                if (run.Phase == RunPhase.Won) wins++; else lostAt[run.Depth]++;
                depth += run.Depth;
            }
            return label.PadRight(44) + " wins " + wins + "/" + seeds + " (" + (100 * wins / seeds) + "%), avg depth " + (depth / seeds).ToString("0.00")
                + ", lost at 1..5: " + string.Join(" ", lostAt.Skip(1));
        }

        [Test, Explicit] public void BalanceMatrix()
        {
            var db = new ContentDb(TestContent.Load());
            Action<string> w = TestContext.Progress.WriteLine;
            int seeds = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_SEEDS"), out int n) ? n : 200;
            RunState.PouchMode = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_POUCH"), out int mode) ? mode : 2;
            w("MATRIX pouch mode " + RunState.PouchMode);
            foreach (bool mods in new[] { false, true })
            {
                string tag = mods ? " +aspects+events" : "";
                w("MATRIX " + Line(db, "fresh" + tag, s => new RunOptions { Unlocks = Unlocks.Fresh(db), Aspects = mods, Events = mods }, seeds));
                w("MATRIX " + Line(db, "fresh + all catalysts" + tag, s => new RunOptions { Unlocks = With(db, false, true), Aspects = mods, Events = mods }, seeds));
                w("MATRIX " + Line(db, "fresh + all starters" + tag, s => new RunOptions { Unlocks = With(db, true, false), Aspects = mods, Events = mods }, seeds));
                w("MATRIX " + Line(db, "half" + tag, s => new RunOptions { Unlocks = Unlocks.Half(db, s), Aspects = mods, Events = mods }, seeds));
                w("MATRIX " + Line(db, "full" + tag, s => new RunOptions { Unlocks = Unlocks.All(db), Aspects = mods, Events = mods }, seeds));
            }
            w("MATRIX " + Line(db, "fresh, aspects only", s => new RunOptions { Unlocks = Unlocks.Fresh(db), Aspects = true }, seeds));
            w("MATRIX " + Line(db, "fresh, events only", s => new RunOptions { Unlocks = Unlocks.Fresh(db), Events = true }, seeds));
        }

        /// <summary>A player who knows nothing, on the whole game: a random aspect, a random option, a random handful of cards on random tiles.</summary>
        public static RunState RandomPlay(ContentDb db, int seed, RunOptions options)
        {
            var run = new RunState(db, new Journal(), seed, 1, options);
            var rng = new Random(seed * 7919 + 13);
            for (int guard = 0; guard < 500 && run.Phase != RunPhase.Won && run.Phase != RunPhase.Lost; guard++)
            {
                if (run.Phase == RunPhase.AspectOffer) { run.ChooseAspect(run.AspectOffer[rng.Next(run.AspectOffer.Count)].Data.id); continue; }
                if (run.Phase == RunPhase.EventOffer) { run.ChooseEventOption(rng.Next(run.EventOptions.Count)); continue; }
                if (run.Phase == RunPhase.DepthCleared)
                {
                    run.ChooseCatalyst(run.CatalystOffer.Count > 0 ? run.CatalystOffer[rng.Next(run.CatalystOffer.Count)].id : null);
                    continue;
                }
                for (int t = 0; t < Bench.TileCount; t++) run.Unplace(t);
                int want = 2 + rng.Next(Math.Min(6, Math.Max(1, run.Hand.Count - 1)));
                for (int tries = 0; tries < 60 && (run.Bench.Count < want || !run.CanIgnite) && run.Hand.Count > 0; tries++)
                    run.Place(rng.Next(run.Hand.Count), rng.Next(Bench.TileCount));
                if (run.CanIgnite) run.Ignite();
                else if (!run.Redraw()) break;
            }
            return run;
        }

        /// <summary>
        /// The spec's rule for events (8.4): none may make a depth impossible. Each event in turn is put in the informed
        /// player's way and the wins are set against the same runs with no event at all (the percentage): a hazard or a
        /// gamble once at each depth it may come to, which must keep 70%, and every event at all its depths in one run.
        /// Then the player who knows nothing, who must stay near no wins whatever is unlocked.
        /// dotnet test tools/CoreTests --filter EventReport -l "console;verbosity=detailed"
        /// </summary>
        [Test, Explicit] public void EventReport()
        {
            var db = new ContentDb(TestContent.Load());
            Action<string> w = TestContext.Progress.WriteLine;
            int seeds = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_SEEDS"), out int n) ? n : 200;
            int Wins(string force, int at = 0)
            {
                int wins = 0;
                for (int seed = 1; seed <= seeds; seed++)
                    if (Autoplay(db, seed, new RunOptions { Unlocks = Unlocks.Fresh(db), Aspects = true, Events = force != null, ForceEvent = force, ForceEventAt = at }).Phase == RunPhase.Won) wins++;
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
                    var run = RandomPlay(db, seed, RunOptions.Full(full ? Unlocks.All(db) : Unlocks.Fresh(db)));
                    if (run.Phase == RunPhase.Won) wins++;
                    endedAt[run.Depth]++;
                }
                w("RANDOM " + (full ? "full" : "fresh").PadRight(6) + " wins " + wins + "/" + seeds + ", ended at depth 1..5: " + string.Join(" ", endedAt.Skip(1)));
            }
        }
    }
}
