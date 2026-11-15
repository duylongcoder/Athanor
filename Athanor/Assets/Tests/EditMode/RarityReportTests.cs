using System;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>
    /// Opt-in (Explicit) report: does each rarity pay? Same seeds, the informed bot, but at every catalyst offer it follows one
    /// policy: take the first offered catalyst of one rarity (else the bot's pick), a random one, or the bot's pick, or take the bot's usual pick.
    /// Also prints how often each rarity is offered and picked. Compares uniform draws with the content's rarityWeights
    /// (and ATHANOR_WEIGHTS="6,4,2,1" if set). ATHANOR_SEEDS sets the runs per cell (default 400).
    /// Opt-in by environment (not [Explicit], which the lint forbids): without ATHANOR_RARITY_REPORT=1 the test returns early as a pass (Assume would be inconclusive, which makes the Unity runner exit 2).
    /// ATHANOR_RARITY_REPORT=1 dotnet test tools/CoreTests --filter "FullyQualifiedName=Athanor.Tests.RarityReportTests.RarityReport_WhenOptedIn_PrintsWinRatesPerRarity" -l "console;verbosity=detailed"
    /// </summary>
    public class RarityReportTests
    {
        static RunState Play(ContentDb db, int seed, RunOptions options, string policy, int[] offered, int[] picked)
        {
            var run = new RunState(db, new Journal(), seed, 1, options);
            var bot = new AutoPlayer(db);
            for (int guard = 0; guard < 600 && run.Phase != RunPhase.Won && run.Phase != RunPhase.Lost; guard++)
            {
                if (run.Phase == RunPhase.DepthCleared)
                {
                    foreach (var c in run.CatalystOffer) offered[Rarities.Rank(c.rarity)]++;
                    if (policy == "bot") { bot.Decide(run); continue; }
                    var pick = policy == "random" ? run.CatalystOffer.ElementAtOrDefault(new Random(seed * 31 + run.Depth).Next(Math.Max(1, run.CatalystOffer.Count)))
                        : run.CatalystOffer.FirstOrDefault(c => Rarities.Of(c.rarity) == policy);
                    if (pick == null) { bot.Decide(run); continue; }   // none of that rarity on offer: the bot's usual pick
                    picked[Rarities.Rank(pick.rarity)]++;
                    run.ChooseCatalyst(pick.id);
                    continue;
                }
                if (bot.Decide(run) == AutoMove.ReadyToIgnite) run.Ignite();
            }
            return run;
        }

        [Test] public void RarityReport_WhenOptedIn_PrintsWinRatesPerRarity()
        {
            if (Environment.GetEnvironmentVariable("ATHANOR_RARITY_REPORT") != "1")
                Assert.Pass("opt-in report not run: set ATHANOR_RARITY_REPORT=1");   // not Assume/Ignore: an inconclusive result makes Unity exit 2
            int reported = 0;
            Action<string> w = TestContext.Progress.WriteLine;
            int seeds = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_SEEDS"), out int n) ? n : 400;
            var configs = new List<(string label, int[] weights)> { ("uniform", null) };
            var own = TestContent.Load().rarityWeights;
            if (own != null) configs.Add(("content " + string.Join(",", own), own));
            var env = Environment.GetEnvironmentVariable("ATHANOR_WEIGHTS");
            if (!string.IsNullOrEmpty(env)) configs.Add(("env " + env, env.Split(',').Select(int.Parse).ToArray()));
            var policies = new[] { "random" }.Concat(Rarities.All).Concat(new[] { "bot" }).ToArray();
            foreach (var (label, weights) in configs)
            {
                var data = TestContent.Load(); data.rarityWeights = weights;
                var db = new ContentDb(data);
                w("RARITY [" + label + "] hold one catalyst from depth 1 (all unlocked, first offer forced, then the bot), " + seeds + " runs each");
                var perRarity = new Dictionary<string, (int wins, int runs)>();
                foreach (var cat in db.Catalysts)
                {
                    int wins = 0;
                    for (int s = 1; s <= seeds; s++)
                    {
                        var run = Play(db, s, new RunOptions { Unlocks = BalanceMatrixTests.With(db, false, true), FirstOffer = new[] { cat.id } }, "bot", new int[4], new int[4]);
                        if (run.Phase == RunPhase.Won) wins++;
                    }
                    string r = Rarities.Of(cat.rarity); perRarity.TryGetValue(r, out var t); perRarity[r] = (t.wins + wins, t.runs + seeds);
                    w("RARITY   " + cat.id.PadRight(20) + r.PadRight(10) + wins + "/" + seeds + " (" + (100.0 * wins / seeds).ToString("0.0") + "%)");
                }
                foreach (var r in Rarities.All) if (perRarity.TryGetValue(r, out var t))
                { reported++; w("RARITY   BUCKET " + r.PadRight(10) + t.wins + "/" + t.runs + " (" + (100.0 * t.wins / t.runs).ToString("0.0") + "%)"); }
                foreach (bool fresh in new[] { true, false })
                {
                    string prof = fresh ? "fresh (starting pool)" : "all catalysts unlocked";
                    foreach (bool mods in new[] { false, true })
                    {
                        w("RARITY [" + label + "] " + prof + (mods ? " +aspects+events" : ""));
                        foreach (var policy in policies)
                        {
                            var offered = new int[4]; var picked = new int[4]; int wins = 0;
                            for (int s = 1; s <= seeds; s++)
                            {
                                var unlocks = fresh ? Unlocks.Fresh(db) : BalanceMatrixTests.With(db, false, true);
                                if (Play(db, s, new RunOptions { Unlocks = unlocks, Aspects = mods, Events = mods }, policy, offered, picked).Phase == RunPhase.Won) wins++;
                            }
                            int tot = Math.Max(1, offered.Sum());
                            w("RARITY   take " + (policy == "random" ? "a random one" : policy == "bot" ? "bot's pick" : "prefer " + policy).PadRight(14)
                              + " wins " + wins + "/" + seeds + " (" + (100.0 * wins / seeds).ToString("0.0") + "%)"
                              + " | offers C/U/R/L " + string.Join("/", offered.Select(o => (100 * o / tot) + "%"))
                              + " | picks " + string.Join("/", picked));
                        }
                    }
                }
            }
            Assert.That(reported, Is.GreaterThan(0), "the report covered at least one rarity bucket");
        }
    }
}
