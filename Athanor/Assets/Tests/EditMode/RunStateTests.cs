using System;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    public class RunStateTests
    {
        static ContentData Custom(int[] targets, string ultimate = "y")
        {
            var d = TestContent.Tiny();
            d.depthTargets = targets; d.ultimate = ultimate;
            d.catalysts = new[] { TestContent.Cat(Effects.ExtraHand), TestContent.Cat(Effects.ExtraTrial), TestContent.Cat(Effects.FirstTriple), TestContent.Cat(Effects.ChainPlus) };
            return d;
        }

        /// <summary>A run whose opening hand holds every wanted id (searches seeds).</summary>
        static RunState RunWith(ContentData d, int startDepth, params string[] want)
        {
            for (int seed = 1; seed < 500; seed++)
            {
                var r = new RunState(new ContentDb(d), new Journal(), seed, startDepth);
                var hand = r.Hand.ToList();
                if (want.All(w => hand.Remove(w))) return r;
            }
            throw new Exception("no seed gives that hand");
        }
        static bool PlaceId(RunState r, string id, int tile) => r.Place(r.Hand.ToList().IndexOf(id), tile);
        static int Starters(RunState r) => r.Hand.Count(h => h == "a" || h == "b" || h == "c" || h == "d");
        static readonly int[] Far = { 999999, 999999, 999999, 999999 };

        [Test] public void NewRun_Has5Cards_4Trials_2Redraws_Target300()
        {
            var r = new RunState(new ContentDb(TestContent.Load()), new Journal(), 1);
            Assert.AreEqual(5, r.Hand.Count); Assert.AreEqual(4, r.TrialsLeft); Assert.AreEqual(2, r.RedrawsLeft);
            Assert.AreEqual(300, r.Target); Assert.AreEqual(1, r.Depth); Assert.AreEqual(RunPhase.Playing, r.Phase);
            Assert.AreEqual(0, r.Potency);
        }

        [Test] public void Place_Then_Unplace_ReturnsCard()
        {
            var r = RunWith(Custom(new[] { 300, 900, 2500, 7000 }), 1, "a");
            Assert.IsTrue(PlaceId(r, "a", 4));
            Assert.AreEqual(4, r.Hand.Count); Assert.AreEqual("a", r.Bench.Tiles[4]);
            Assert.IsTrue(r.Unplace(4));
            Assert.AreEqual(5, r.Hand.Count); Assert.IsNull(r.Bench.Tiles[4]);
            Assert.IsFalse(r.Unplace(4));
        }

        [Test] public void Place_OnOccupiedTile_False()
        {
            var r = RunWith(Custom(new[] { 300, 900, 2500, 7000 }), 1, "a", "b");
            Assert.IsTrue(PlaceId(r, "a", 0));
            Assert.IsFalse(PlaceId(r, "b", 0));
            Assert.IsFalse(r.Place(99, 1)); Assert.IsFalse(r.Place(0, 9));
        }

        [Test] public void CanIgnite_False_WithoutAdjacentPair()
        {
            var r = RunWith(Custom(new[] { 300, 900, 2500, 7000 }), 1, "a", "b");
            Assert.IsFalse(r.CanIgnite);
            PlaceId(r, "a", 0); PlaceId(r, "b", 8);
            Assert.IsFalse(r.CanIgnite);
            Assert.Throws<InvalidOperationException>(() => r.Ignite());
            Assert.AreEqual(4, r.TrialsLeft);
        }

        [Test] public void Ignite_SpendsTrial_ClearsBench_RefillsHand_KeepsUnplayedCards()
        {
            var r = RunWith(Custom(new[] { 300, 900, 2500, 7000 }), 1, "a", "b");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1);
            var kept = r.Hand.ToList();
            var o = r.Ignite();
            Assert.AreEqual(40, o.Gained); Assert.AreEqual(40, r.Potency);
            Assert.AreEqual(3, r.TrialsLeft); Assert.AreEqual(0, r.Bench.Count);
            Assert.AreEqual(5, Starters(r));
            CollectionAssert.IsSubsetOf(kept, r.Hand.ToList());
            Assert.AreEqual(RunPhase.Playing, o.PhaseAfter);
            Assert.AreEqual(1, r.DiscoveriesThisRun);
        }

        [Test] public void ReachingTarget_GivesDepthCleared_WithThreeDistinctOffers()
        {
            var r = RunWith(Custom(new[] { 30, 900, 2500, 7000 }), 1, "a", "b");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1);
            Assert.AreEqual(RunPhase.DepthCleared, r.Ignite().PhaseAfter);
            Assert.AreEqual(3, r.CatalystOffer.Select(c => c.id).Distinct().Count());
            Assert.IsFalse(r.CanIgnite);
        }

        [Test] public void ChooseCatalyst_AdvancesDepth_ResetsPotencyAndTrials()
        {
            var r = RunWith(Custom(new[] { 30, 900, 2500, 7000 }), 1, "a", "b");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1); r.Ignite();
            r.Redraw();
            var pick = r.CatalystOffer[0];
            r.ChooseCatalyst(pick.id);
            Assert.AreEqual(2, r.Depth); Assert.AreEqual(0, r.Potency); Assert.AreEqual(900, r.Target);
            Assert.AreEqual(RunPhase.Playing, r.Phase);
            Assert.AreEqual(pick.effect == Effects.ExtraTrial ? 8 : 7, r.TrialsLeft); // 4 new + 3 unspent (+1 Hourglass)
            Assert.AreEqual(2, r.RedrawsLeft);
            Assert.AreEqual(pick.id, r.Catalysts.Single().id);
            Assert.AreEqual(r.HandSize, Starters(r));
            Assert.Throws<InvalidOperationException>(() => r.ChooseCatalyst(pick.id));
        }

        [Test] public void OutOfTrials_BelowTarget_IsLost()
        {
            var r = new RunState(new ContentDb(Custom(new[] { 999999, 1, 1, 1 })), new Journal(), 3);
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(RunPhase.Playing, r.Phase);
                r.Place(0, 0); r.Place(0, 1);
                r.Ignite();
            }
            Assert.AreEqual(RunPhase.Lost, r.Phase); Assert.AreEqual(0, r.TrialsLeft);
            Assert.IsFalse(r.Place(0, 0)); Assert.IsFalse(r.Redraw());
        }

        [Test] public void Depth5_HasSixTrials_NoTarget_AndMakingUltimate_IsWon()
        {
            var r = RunWith(Custom(new[] { 1, 1, 1, 1 }, "x"), 5, "a", "b");
            Assert.AreEqual(6, r.TrialsLeft); Assert.AreEqual(0, r.Target);
            PlaceId(r, "a", 0); PlaceId(r, "b", 1);
            Assert.AreEqual(RunPhase.Won, r.Ignite().PhaseAfter);
        }

        [Test] public void Depth5_PotencyAlone_DoesNotClear()
        {
            var r = RunWith(Custom(new[] { 1, 1, 1, 1 }), 5, "a", "b");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1);
            Assert.AreEqual(RunPhase.Playing, r.Ignite().PhaseAfter);
        }

        [Test] public void Redraw_ReplacesHand_AndDecrements_FalseAtZero()
        {
            var r = new RunState(new ContentDb(TestContent.Load()), new Journal(), 5);
            Assert.IsTrue(r.Redraw()); Assert.AreEqual(5, r.Hand.Count); Assert.AreEqual(1, r.RedrawsLeft);
            Assert.IsTrue(r.Redraw()); Assert.IsFalse(r.Redraw()); Assert.AreEqual(0, r.RedrawsLeft);
        }

        [Test] public void ExtraHand_Gives6Cards()
        {
            var d = Custom(new[] { 30, 900, 2500, 7000 }); d.catalysts = new[] { TestContent.Cat(Effects.ExtraHand) };
            var r = RunWith(d, 1, "a", "b");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1); r.Ignite();
            Assert.AreEqual(1, r.CatalystOffer.Count);
            r.ChooseCatalyst(Effects.ExtraHand);
            Assert.AreEqual(6, r.HandSize); Assert.AreEqual(6, Starters(r));
        }

        [Test] public void SmallSatchel_DrawsWhatExists()
        {
            var d = Custom(new[] { 999999, 1, 1, 1 });
            d.materials = new[] { TestContent.Mat("a", "Ore", 0), TestContent.Mat("b", "Ore", 0), TestContent.Mat("x", "Metal", 1) };
            d.recipes = new[] { TestContent.Rec("a", "b", "x") }; d.ultimate = "x"; d.depthTargets = new[] { 999999, 1, 1, 1 };
            var r = new RunState(new ContentDb(d), new Journal(), 1, 2);
            Assert.AreEqual(4, r.Hand.Count);
            r.Place(0, 0); r.Place(0, 1);
            Assert.DoesNotThrow(() => r.Ignite());
            Assert.That(r.Hand.Count, Is.LessThanOrEqualTo(5));
        }

        [Test] public void Discovery_StaysInHand_WithoutTakingAStarterSlot()
        {
            var r = RunWith(Custom(Far, ""), 1, "a", "b");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1); r.Ignite();
            Assert.AreEqual(1, r.Owned("x"));
            Assert.AreEqual(5, Starters(r)); Assert.AreEqual(6, r.Hand.Count);
        }

        [Test] public void Discovery_ThatReacts_IsUsedUp_ButStartersGoBackToTheSatchel()
        {
            var r = RunWith(Custom(Far, ""), 1, "a", "b", "c");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1); r.Ignite();
            PlaceId(r, "x", 0); PlaceId(r, "c", 1); r.Ignite();
            Assert.AreEqual(0, r.Owned("x")); Assert.AreEqual(1, r.Owned("y"));
            Assert.AreEqual(8, r.SatchelSize);
        }

        [Test] public void PlacedDiscovery_NeverReacting_IsLost()
        {
            var r = RunWith(Custom(Far, ""), 1, "a", "b", "d");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1); r.Ignite();
            PlaceId(r, "x", 0); PlaceId(r, "d", 1);
            var o = r.Ignite();
            Assert.IsNull(o.Events.Single().Result);
            Assert.AreEqual(0, r.Owned("x")); Assert.AreEqual(0, r.Bench.Count);
            Assert.AreEqual(5, Starters(r));   // the starter went back to the satchel and the hand refilled
        }

        [Test] public void Unplace_BeforeIgnite_KeepsTheDiscovery()
        {
            var r = RunWith(Custom(Far, ""), 1, "a", "b");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1); r.Ignite();
            PlaceId(r, "x", 4);
            Assert.IsTrue(r.Unplace(4));
            Assert.AreEqual(1, r.Owned("x")); Assert.AreEqual(1, r.Hand.Count(h => h == "x"));
        }

        [Test] public void PlacedStarters_AlwaysReturnToTheSatchel()
        {
            var r = RunWith(Custom(Far, ""), 1, "a", "b", "d");
            int before = r.SatchelSize;
            PlaceId(r, "a", 0); PlaceId(r, "b", 1); PlaceId(r, "d", 8);
            r.Ignite();
            Assert.AreEqual(before, r.SatchelSize);
            Assert.AreEqual(5, Starters(r));
        }

        [Test] public void ChainSurvivor_GoesToRack_ConsumedIntermediateDoesNot()
        {
            var r = RunWith(Custom(Far, ""), 1, "a", "b", "c");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1); PlaceId(r, "c", 3);   // a+b => x on tile 0, then x+c => y
            var o = r.Ignite();
            CollectionAssert.AreEqual(new[] { "x", "y" }, o.Events.Select(e => e.Result).ToList());
            Assert.AreEqual(0, r.Owned("x")); Assert.AreEqual(1, r.Owned("y"));
            Assert.AreEqual(2, r.DiscoveriesThisRun);
            Assert.AreEqual("y", o.Final[0]); Assert.IsTrue(o.Made[0]);   // where the survivor stood, for the screen
            Assert.AreEqual(1, o.Made.Count(m => m));
        }

        [Test] public void Ultimate_MadeMidChain_Wins()
        {
            var r = RunWith(Custom(Far, "y"), 1, "a", "b", "c");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1); PlaceId(r, "c", 3);
            Assert.AreEqual(RunPhase.Won, r.Ignite().PhaseAfter);
        }

        [Test] public void HasFired_ByRecipe()
        {
            var d = Custom(Far, "");
            var r = RunWith(d, 1, "a", "b");
            var db = new ContentDb(Custom(Far, ""));
            Assert.IsFalse(r.HasFired(db.Recipes[0]));
            PlaceId(r, "a", 0); PlaceId(r, "b", 1); r.Ignite();
            Assert.IsTrue(r.HasFired(db.Recipes[0])); Assert.IsTrue(r.HasFired("b", "a"));
        }

        [Test] public void Redraw_KeepsDiscoveries()
        {
            var r = RunWith(Custom(Far, ""), 1, "a", "b");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1); r.Ignite();
            Assert.IsTrue(r.Redraw());
            Assert.AreEqual(1, r.Owned("x")); Assert.AreEqual(5, Starters(r));
        }

        [Test] public void FullRack_SeveralResults_StaysAt12_AndDropsTheWeakest()
        {
            var d = new ContentData
            {
                materials = new[] { TestContent.Mat("a", "Ore", 0), TestContent.Mat("b", "Ore", 0), TestContent.Mat("x", "Metal", 1), TestContent.Mat("y", "Crystal", 2) },
                recipes = new[] { TestContent.Rec("a", "b", "x"), TestContent.Rec("x", "x", "y") },
                catalysts = new CatalystData[0], depthTargets = new[] { 1, 1, 1, 1 }, ultimate = "",
            };
            var r = new RunState(new ContentDb(d), new Journal(), 1);   // hand is always a,a,b,b
            IgniteOutcome o = null;
            int guard = 0;
            while (r.Owned("x") < RunState.RackSize && guard++ < 20)
            {
                if (r.Phase == RunPhase.DepthCleared) r.ChooseCatalyst(null);
                PlaceId(r, "a", 0); PlaceId(r, "b", 1); PlaceId(r, "b", 7); PlaceId(r, "a", 8);   // two x, on tiles 0 and 7
                o = r.Ignite();
                Assert.AreEqual(0, o.Dropped.Count);
            }
            Assert.AreEqual(RunState.RackSize, r.Owned("x"));
            Assert.AreEqual(RunPhase.Playing, r.Phase);
            PlaceId(r, "x", 3); PlaceId(r, "x", 4);                                               // x+x => y
            PlaceId(r, "a", 2); PlaceId(r, "b", 5); PlaceId(r, "b", 6); PlaceId(r, "a", 7);       // two more x
            o = r.Ignite();
            Assert.AreEqual(1, r.Owned("y"));
            Assert.AreEqual(RunState.RackSize - 1, r.Owned("x"));
            CollectionAssert.AreEqual(new[] { "x" }, o.Dropped);
            Assert.AreEqual(RunState.RackSize, r.Hand.Count(h => h == "x" || h == "y"));
        }

        [Test] public void MakingTheUltimate_WinsAtAnyDepth()
        {
            var r = RunWith(Custom(Far, "x"), 1, "a", "b");
            PlaceId(r, "a", 0); PlaceId(r, "b", 1);
            Assert.AreEqual(RunPhase.Won, r.Ignite().PhaseAfter);
        }

        static Journal FullJournal(ContentDb db)
        {
            var j = new Journal();
            foreach (var r in db.Recipes) j.Learn(r.items);
            return j;
        }

        /// <summary>What happened over one scripted run, for the balance report.</summary>
        sealed class RunStats
        {
            public readonly int[] BySize = new int[ContentDb.MaxItems + 1];   // reactions by number of materials
            public int Ignites, LongestChain, Sputters, FirstTrialClears;
            public bool UltimateByCombo;                                        // an ingredient of the ultimate was made by a combo this run
            public readonly HashSet<string> ComboMade = new HashSet<string>();
            public readonly Dictionary<string, int> Combos = new Dictionary<string, int>();   // combo recipe => times fired
            public readonly List<string> ClearedIn = new List<string>();        // trials used at each depth cleared
        }

        static RunState Autoplay(ContentDb db, int seed, List<string> log = null, RunStats stats = null)
        {
            var run = new RunState(db, FullJournal(db), seed);
            var bot = new AutoPlayer(db);
            int guard = 0, used = 0;
            while (run.Phase != RunPhase.Won && run.Phase != RunPhase.Lost && guard++ < 500)
            {
                int d = run.Depth; long before = run.Potency; int tr = run.TrialsLeft;
                if (bot.Decide(run) == AutoMove.ReadyToIgnite)
                {
                    string laid = log == null ? null : string.Join(" ", run.Bench.Tiles.Select(t => t ?? "."));
                    var o = run.Ignite();
                    used++;
                    if (log != null)
                        log.Add("   bench " + laid + " => " + string.Join(", ", o.Events.Where(e => e.Result != null).Select(e => e.Items.Length + ":" + e.Result + " " + e.Potency)) + (o.Dropped.Count > 0 ? " dropped " + string.Join(",", o.Dropped) : ""));
                    if (stats != null)
                    {
                        stats.Ignites++;
                        var reactions = o.Events.Where(e => e.Result != null).ToList();
                        foreach (var e in reactions)
                        {
                            stats.BySize[e.Items.Length]++;
                            if (e.Items.Length >= 3)
                            {
                                stats.ComboMade.Add(e.Result);
                                string key = e.Items.Length + " " + e.Result;
                                stats.Combos[key] = (stats.Combos.TryGetValue(key, out int n) ? n : 0) + 1;
                            }
                            if (e.Result == db.Ultimate && e.Items.Any(stats.ComboMade.Contains)) stats.UltimateByCombo = true;
                        }
                        stats.Sputters += o.Events.Count(e => e.Sputter);
                        stats.LongestChain = Math.Max(stats.LongestChain, reactions.Count);
                        if (o.PhaseAfter == RunPhase.DepthCleared)
                        {
                            stats.ClearedIn.Add(d + ":" + used);
                            if (d == 1 && used == 1) stats.FirstTrialClears++;
                        }
                    }
                    if (o.PhaseAfter == RunPhase.DepthCleared) used = 0;
                }
                if (log != null && run.Phase != RunPhase.DepthCleared && (run.TrialsLeft != tr || run.Depth != d))
                    log.Add("d" + d + " t" + tr + " +" + (run.Depth == d ? run.Potency - before : -1) + " deck " + run.SatchelSize + " hand " + string.Join(",", run.Hand));
            }
            return run;
        }

        /// <summary>A player who knows nothing: lays a random handful of cards on random tiles and takes a random catalyst.</summary>
        static RunState RandomPlay(ContentDb db, int seed)
        {
            var run = new RunState(db, new Journal(), seed);
            var rng = new Random(seed * 7919 + 13);
            int guard = 0;
            while (run.Phase != RunPhase.Won && run.Phase != RunPhase.Lost && guard++ < 500)
            {
                if (run.Phase == RunPhase.DepthCleared)
                {
                    run.ChooseCatalyst(run.CatalystOffer.Count > 0 ? run.CatalystOffer[rng.Next(run.CatalystOffer.Count)].id : null);
                    continue;
                }
                for (int t = 0; t < Bench.TileCount; t++) run.Unplace(t);
                int want = 2 + rng.Next(Math.Min(6, run.Hand.Count - 1));
                for (int tries = 0; tries < 60 && (run.Bench.Count < want || !run.CanIgnite) && run.Hand.Count > 0; tries++)
                    run.Place(rng.Next(run.Hand.Count), rng.Next(Bench.TileCount));
                if (run.CanIgnite) run.Ignite();
                else if (!run.Redraw()) { run.Place(0, 0); run.Place(0, 1); if (run.CanIgnite) run.Ignite(); else break; }
            }
            return run;
        }

        [Test] public void Bot_PlacesFiveItemCombo_WithTheLiveliestOnTheCentre()
        {
            var r = RunWith(TestContent.Combo(), 1, "s", "p", "q", "r", "t");
            var bot = new AutoPlayer(new ContentDb(TestContent.Combo()));
            Assert.AreEqual(AutoMove.ReadyToIgnite, bot.Decide(r));
            Assert.AreEqual("s", r.Bench.Tiles[Bench.Centre]);
            Assert.AreEqual(5, r.Bench.Count);
            Assert.AreEqual(RunPhase.Won, r.Ignite().PhaseAfter);   // "big" is that content's ultimate
        }

        [Test] public void Bot_GivesARecipeTheCompanyItNeeds()
        {
            // p(8) + q(5) is 13 of its own and 22 is asked: the second q(5) and t(4) must both touch the hub
            var d = TestContent.Combo();
            d.recipes = new[] { TestContent.Rec(new[] { "p", "q" }, 22, "x") };
            d.ultimate = "x";
            var r = RunWith(d, 1, "p", "q", "q", "t");
            var bot = new AutoPlayer(new ContentDb(d));
            Assert.AreEqual(AutoMove.ReadyToIgnite, bot.Decide(r));
            Assert.AreEqual(RunPhase.Won, r.Ignite().PhaseAfter, "bench " + string.Join(" ", r.Bench.Tiles.Select(t => t ?? ".")));
        }

        [Test] public void Bot_GivesADiscoveryTheCompanyItNeeds()
        {
            // x(6) + v(1) is 7 of its own and 20 is asked: the made x has to wait for two lively starters beside it
            var d = TestContent.Combo();
            d.recipes = new[] { TestContent.Rec(new[] { "p", "q" }, 0, "x"), TestContent.Rec(new[] { "x", "v" }, 20, "u") };
            d.ultimate = "u";
            d.depthTargets = Far;
            var db = new ContentDb(d);
            int wins = 0;
            for (int seed = 1; seed <= 30; seed++)
            {
                var r = new RunState(db, FullJournal(db), seed);
                var bot = new AutoPlayer(db);
                for (int guard = 0; guard < 40 && r.Phase == RunPhase.Playing; guard++) bot.Step(r);
                if (r.Phase == RunPhase.Won) wins++;
            }
            Assert.That(wins, Is.GreaterThanOrEqualTo(26), "wins " + wins + "/30");
        }

        [Test] public void Bot_DoesNotThrowAwayADiscovery_WhenForcedToIgnite()
        {
            var d = Custom(Far, "y");
            var r = RunWith(d, 1, "a", "b");
            var bot = new AutoPlayer(new ContentDb(d));
            for (int k = 0; k < 12 && r.Phase == RunPhase.Playing; k++)
            {
                int had = r.Owned("x");
                if (bot.Decide(r) != AutoMove.ReadyToIgnite) continue;
                bool usesX = r.Bench.Tiles.Contains("x");
                var o = r.Ignite();
                if (usesX) Assert.IsTrue(o.Events.Any(e => e.Result == "y"), "x was placed without making y");
            }
        }

        [Test] public void ScriptedRun_ReachesWon()
        {
            var db = new ContentDb(TestContent.Load());
            var run = Autoplay(db, 1);
            Assert.AreEqual(RunPhase.Won, run.Phase, "depth " + run.Depth + " potency " + run.Potency + "/" + run.Target);
        }

        [Test] public void InformedPlayer_WinsMostSeeds()
        {
            var db = new ContentDb(TestContent.Load());
            int wins = 0; var depths = new List<string>();
            for (int seed = 1; seed <= 40; seed++)
            {
                var run = Autoplay(db, seed);
                if (run.Phase == RunPhase.Won) wins++;
                depths.Add(run.Phase == RunPhase.Won ? "W" + run.TrialsLeft : run.Depth + ":" + run.Potency);
            }
            Assert.That(wins, Is.GreaterThanOrEqualTo(32), "wins " + wins + "/40: " + string.Join(" ", depths));
        }

        /// <summary>The balance pass left five-item combos as a rare peak, not the standard way to win.</summary>
        [Test] public void InformedPlayer_RarelyFiresAFiveItemCombo()
        {
            var db = new ContentDb(TestContent.Load());
            int runsWith5 = 0;
            for (int seed = 1; seed <= 40; seed++)
            {
                var st = new RunStats();
                Autoplay(db, seed, null, st);
                if (st.BySize[5] > 0) runsWith5++;
            }
            Assert.That(runsWith5, Is.LessThanOrEqualTo(8), "runs with a five-item combo: " + runsWith5 + "/40");
        }

        /// <summary>Prints one scripted run trial by trial: ATHANOR_SEED=17 dotnet test --filter TraceRun.</summary>
        [Test, Explicit] public void TraceRun()
        {
            var db = new ContentDb(TestContent.Load());
            int seed = int.TryParse(Environment.GetEnvironmentVariable("ATHANOR_SEED"), out int n) ? n : 1;
            var log = new List<string>();
            var run = Autoplay(db, seed, log);
            TestContext.Progress.WriteLine("TRACE seed " + seed + ": " + run.Phase + " at depth " + run.Depth + ", catalysts "
                + string.Join(",", run.Catalysts.Select(c => c.id)) + "\n" + string.Join("\n", log));
        }

        [Test, Explicit] public void BalanceReport()
        {
            var db = new ContentDb(TestContent.Load());
            const int Seeds = 200;
            int wins = 0, spare = 0, byCombo = 0, firstTrial = 0, ignites = 0, sputters = 0, runsWith5 = 0; var lost = new List<string>();
            var combos = new SortedDictionary<string, int>(); var comboRuns = new SortedDictionary<string, int>();
            var clearedTrials = new int[RunState.FinalDepth + 1]; var cleared = new int[RunState.FinalDepth + 1];
            var bySize = new int[ContentDb.MaxItems + 1]; var lostAt = new int[RunState.FinalDepth + 1]; var chains = new List<int>();
            for (int seed = 1; seed <= Seeds; seed++)
            {
                var st = new RunStats();
                var run = Autoplay(db, seed, null, st);
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
                var run = RandomPlay(db, seed);
                if (run.Phase == RunPhase.Won) randomWins++;
                randomDepth[run.Depth]++;
            }
            // the same player on the same content with no catalysts on offer: no depth may depend on one
            var bare = TestContent.Load(); bare.catalysts = new CatalystData[0];
            var bareDb = new ContentDb(bare);
            int bareWins = 0; var bareLostAt = new int[RunState.FinalDepth + 1];
            for (int seed = 1; seed <= Seeds; seed++)
            {
                var run = Autoplay(bareDb, seed);
                if (run.Phase == RunPhase.Won) bareWins++; else bareLostAt[run.Depth]++;
            }
            // and with each catalyst taken out of the game in turn: the win rate must not hang on any one of them
            var without = new List<string>();
            foreach (var c in db.Catalysts)
            {
                var less = TestContent.Load(); less.catalysts = less.catalysts.Where(x => x.id != c.id).ToArray();
                var lessDb = new ContentDb(less);
                int n = 0;
                for (int seed = 1; seed <= Seeds; seed++) if (Autoplay(lessDb, seed).Phase == RunPhase.Won) n++;
                without.Add(c.id + " " + n);
            }
            var w = TestContext.Progress;
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
                var r1 = Autoplay(bareDb, seed, log);
                if (r1.Phase == RunPhase.Won || r1.Depth != 3) continue;
                shown++;
                w.WriteLine("TRACE no catalysts, seed " + seed + ": " + r1.Phase + " at depth " + r1.Depth + "\n" + string.Join("\n", log));
            }
        }
    }
}
