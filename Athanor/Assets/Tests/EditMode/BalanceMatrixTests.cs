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
    }
}
