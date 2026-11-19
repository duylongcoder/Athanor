using System;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;

namespace Athanor.Tests
{
    /// <summary>
    /// The simulated players and shared helpers behind the balance tests and the console reports in tools/CoreReports.
    /// No NUnit here: it compiles in the Unity test assembly, in tools/CoreTests and in tools/CoreReports.
    /// </summary>
    public static class Sim
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


        // ---- the newcomer ---------------------------------------------------------------------------

        sealed class Newcomer
        {
            readonly ContentDb truth;
            readonly Journal journal;
            readonly HashSet<string> tried = new HashSet<string>();                    // pairs that did nothing with heat enough to trust it
            readonly Dictionary<string, int> cold = new Dictionary<string, int>();    // pairs that did nothing while the journal says one wants more heat
            ContentDb belief; AutoPlayer bot; int known = -1;

            public Newcomer(ContentDb truth, Journal journal) { this.truth = truth; this.journal = journal; }

            /// <summary>The content as this player believes it to be: every material, only the recipes in the journal.</summary>
            void Refresh()
            {
                if (journal.KnownPairs.Count == known) return;
                known = journal.KnownPairs.Count;
                belief = new ContentDb(new ContentData
                {
                    rules = truth.Rules,
                    materials = truth.Materials.ToArray(),
                    recipes = truth.Recipes.Where(r => journal.IsKnown(r.items)).ToArray(),
                    catalysts = truth.Catalysts.ToArray(),
                    depthTargets = Enumerable.Range(1, 4).Select(truth.TargetFor).ToArray(),
                    depthNames = new[] { "1", "2", "3", "4", "5" },
                    ultimate = truth.Ultimate,
                });
                bot = new AutoPlayer(belief) { HintValue = 1, RedrawWhenIdle = false, SeekFrontier = true };
            }

            const int ColdTries = 8;

            /// <summary>The most heat the journal says this material is waiting for, 0 when it says nothing.</summary>
            int HeatWanted(string m)
            {
                int most = 0;
                foreach (var h in journal.OpenHints(m, truth)) if (h.heat > most) most = h.heat;
                return most;
            }
            bool HeatHinted(string m) => HeatWanted(m) > 0;

            bool Untried(string a, string b)
            {
                if (journal.IsKnown(a, b)) return false;
                string key = ContentDb.PairKey(a, b);
                return !tried.Contains(key) && Tries(a, b) < ColdTries;
            }

            bool Hinted(string a, string b) =>
                journal.HasHint(a, truth.KindOf(b)) || journal.HasHint(b, truth.KindOf(a));

            long Believed(RunState run)
            {
                var ctx = run.NewContext();
                ctx.EmptyTiles = Bench.TileCount - run.Bench.Count;
                ctx.EmptyAtIgnite = run.Bench.Tiles.Select(t => t == null).ToArray();
                long gain = 0; int chain = 0;
                foreach (var step in ChainPlanner.Plan(run.Bench.Tiles, belief, run.Rules, false).Steps)
                    gain += IgniteResolver.Score(belief, step.Recipe, step.Tiles, !run.HasFired(step.Recipe), false, ++chain, run.Catalysts, ctx);
                return gain;
            }

            public readonly List<(string a, string b)> targets = new List<(string, string)>();
            public int TriedCount => tried.Count;
            public int ColdCount => cold.Count;
            public string Status(string a, string b) => tried.Contains(ContentDb.PairKey(a, b)) ? "crossed-off" : Tries(a, b) > 0 ? "cold x" + Tries(a, b) : "never";
            public bool CanMakeNow(string m) { Refresh(); return bot.CanMake(m); }
            const int TargetCount = 3, Patience = 40;
            readonly Dictionary<string, int> age = new Dictionary<string, int>(), givenUp = new Dictionary<string, int>();

            /// <summary>
            /// The experiments this player is working toward: pairs of things it can make and has not tried
            /// together. Pairs the journal hints at come first, then pairs waiting on more heat, then the highest.
            /// </summary>
            void ChooseTargets(RunState run)
            {
                // an experiment that has not come together after Patience trials is put aside for others
                foreach (var t in targets)
                {
                    string key = ContentDb.PairKey(t.a, t.b);
                    age[key] = (age.TryGetValue(key, out int n) ? n : 0) + 1;
                    if (age[key] > Patience) { givenUp[key] = (givenUp.TryGetValue(key, out int g) ? g : 0) + 1; age[key] = 0; }
                }
                targets.RemoveAll(t => !Untried(t.a, t.b) || !bot.CanMake(t.a) || !bot.CanMake(t.b) || age[ContentDb.PairKey(t.a, t.b)] == 0);
                if (targets.Count >= TargetCount || bot.KnowsTheWay) return;
                var makeable = truth.Materials.Where(m => m.id != truth.Ultimate && bot.CanMake(m.id) && (m.tier > 0 || run.IsOpen(m.id))).Select(m => m.id).ToList();
                int top = makeable.Max(m => truth.Material(m).tier);   // the highest thing this player can make
                var scored = new List<(double score, string a, string b)>();
                for (int i = 0; i < makeable.Count; i++)
                    for (int j = i; j < makeable.Count; j++)
                    {
                        string a = makeable[i], b = makeable[j];
                        if (!Untried(a, b) || targets.Any(t => (t.a == a && t.b == b))) continue;
                        int ta = truth.Material(a).tier, tb = truth.Material(b).tier;
                        if (ta == 0 && tb == 0) continue;   // starting materials meet often enough by themselves
                        double score = 10 * Math.Min(ta, tb) + 4 * Math.Max(ta, tb) - (bot.CostOf(a) + bot.CostOf(b));
                        // every result so far has been one tier above its highest ingredient, and nothing known makes anything
                        // higher than the top: the next step up must come from the top materials themselves
                        if (ta == top && tb == top) score += 50;
                        else if (ta == tb) score += 10;
                        if (Hinted(a, b)) score += 60;
                        if (Tries(a, b) > 0) score += 40;   // it fizzled and a heat hint keeps it open
                        if (givenUp.TryGetValue(ContentDb.PairKey(a, b), out int g)) score -= 30 * g;
                        scored.Add((score, a, b));
                    }
                foreach (var pick in scored.OrderByDescending(x => x.score).Take(TargetCount - targets.Count)) targets.Add((pick.a, pick.b));
            }

            /// <summary>One decision. Returns true when the bench is laid and ready to ignite.</summary>
            public bool Decide(RunState run)
            {
                Refresh();
                if (run.Phase == RunPhase.Playing)
                {
                    ChooseTargets(run);
                    var wanted = new Dictionary<string, int>();
                    // one experiment at a time is worked toward; the others are taken when they happen to be at hand
                    foreach (var (a, b) in targets.Take(1))
                    {
                        if (truth.Material(a).tier > 0) wanted[a] = Math.Max(wanted.TryGetValue(a, out int na) ? na : 0, a == b ? 2 : 1);
                        if (truth.Material(b).tier > 0) wanted[b] = Math.Max(wanted.TryGetValue(b, out int nb) ? nb : 0, a == b ? 2 : 1);
                    }
                    bot.Wanted = wanted;
                }
                var move = bot.Decide(run);
                if (run.Phase != RunPhase.Playing) return false;
                if (move == AutoMove.Redrew) return false;
                if (Experiment(run)) return run.CanIgnite;
                RetryHot(run);
                Explore(run);
                Warm(run);
                return run.CanIgnite;
            }

            /// <summary>
            /// Both halves of an experiment are in hand: give it a trial of its own. The livelier one goes where it
            /// has the most room, the other beside it, and the liveliest starting materials all round, because the
            /// deeper recipes have always wanted heat. Not on a depth's last trial while the target is still short.
            /// </summary>
            bool Experiment(RunState run)
            {
                if (run.TrialsLeft <= 1 && run.Depth < RunState.FinalDepth && run.Potency < run.Target) return false;
                foreach (var (a, b) in targets)
                {
                    var hand = run.Hand.ToList();
                    foreach (int t in Enumerable.Range(0, Bench.TileCount)) if (run.Bench.Tiles[t] != null) hand.Add(run.Bench.Tiles[t]);
                    if (!hand.Contains(a) || !hand.Contains(b) || (a == b && hand.Count(h => h == a) < 2)) continue;
                    for (int t = 0; t < Bench.TileCount; t++) run.Unplace(t);
                    var rules = run.Rules;
                    int hub = -1, room = 0;
                    for (int t = 0; t < Bench.TileCount; t++)
                        if (!rules.Blocked[t] && rules.Around(t).Length > room) { room = rules.Around(t).Length; hub = t; }
                    if (hub < 0) return false;
                    string hubItem = truth.Reactivity(a) >= truth.Reactivity(b) ? a : b, other = hubItem == a ? b : a;
                    var around = rules.Around(hub);
                    run.Place(run.Hand.ToList().IndexOf(hubItem), hub);
                    run.Place(run.Hand.ToList().IndexOf(other), around[0]);
                    for (int k = 1; k < around.Length; k++)
                    {
                        int pick = -1;
                        for (int i = 0; i < run.Hand.Count; i++)
                        {
                            string c = run.Hand[i];
                            if (truth.Material(c).tier != 0) continue;
                            // company that is known to react with either of the two would fire first and spoil the experiment;
                            // anything else may be as lively as it likes: with no recipe of its own it never becomes the hub
                            if (journal.IsKnown(c, hubItem) || journal.IsKnown(c, other)) continue;
                            if (pick < 0 || truth.Reactivity(c) > truth.Reactivity(run.Hand[pick])) pick = i;
                        }
                        if (pick < 0) break;
                        run.Place(pick, around[k]);
                    }
                    return true;
                }
                return false;
            }

            /// <summary>Fills free tiles with cards that touch something they have not been tried with, never at the plan's cost.</summary>
            void Explore(RunState run)
            {
                var rules = run.Rules;
                var refused = new HashSet<string>();   // placements that would spoil the plan
                while (true)
                {
                    long before = Believed(run);
                    int bestTile = -1, bestCard = -1; double bestScore = 0;
                    var seen = new HashSet<string>();
                    for (int i = 0; i < run.Hand.Count; i++)
                    {
                        string c = run.Hand[i];
                        if (!seen.Add(c)) continue;
                        bool kept = targets.Any(x => x.a == c || x.b == c);   // made for an experiment: only spent on that experiment
                        // a discovery the plan still needs (as an end or an ingredient) is not for idle trying
                        if (!kept && truth.Material(c).tier > 0 && bot.Demand(c) >= run.Hand.Count(h => h == c)) continue;
                        for (int t = 0; t < Bench.TileCount; t++)
                        {
                            if (run.Bench.Tiles[t] != null || rules.Blocked[t] || refused.Contains(c + '@' + t.ToString())) continue;
                            double score = 0;
                            for (int n = 0; n < Bench.TileCount; n++)
                            {
                                string m = run.Bench.Tiles[n];
                                if (m == null || !rules.Touch(t, n) || !Untried(c, m)) continue;
                                if (kept && !targets.Any(x => (x.a == c && x.b == m) || (x.a == m && x.b == c))) continue;
                                score += 10 + (Hinted(c, m) ? 15 : 0) + (targets.Any(x => (x.a == c && x.b == m) || (x.a == m && x.b == c)) ? 40 : 0);
                            }
                            if (score <= 0 && run.Bench.Count > 0) continue;
                            if (run.Bench.Count == 0) score = kept ? 0 : PartnersInHand(run, c) + (t == Bench.Centre ? 1 : 0);   // open with the card that has most to try
                            if (score <= 0) continue;
                            score += 2 * truth.Material(c).tier;
                            if (score > bestScore) { bestScore = score; bestTile = t; bestCard = i; }
                        }
                    }
                    if (bestTile < 0) break;
                    string card = run.Hand[bestCard];
                    run.Place(bestCard, bestTile);
                    if (Believed(run) < before) { run.Unplace(bestTile); refused.Add(card + '@' + bestTile.ToString()); }
                }
            }

            int Tries(string a, string b) => cold.TryGetValue(ContentDb.PairKey(a, b), out int n) ? n : 0;

            /// <summary>
            /// A pair that did nothing before, where the journal now says one of the two wants more heat: lay it
            /// out again with the livelier one where it has the most room, and the liveliest spare starting
            /// materials round it until the heat the hint named is reached.
            /// </summary>
            void RetryHot(RunState run)
            {
                var rules = run.Rules;
                var hand = run.Hand.Distinct().ToList();
                foreach (var a in hand)
                {
                    if (!HeatHinted(a)) continue;
                    foreach (var b in hand)
                    {
                        if (b == a ? run.Hand.Count(h => h == a) < 2 : false) continue;
                        if (Tries(a, b) < 1 || !Untried(a, b)) continue;
                        // the livelier of the two will be the hub
                        string hubItem = truth.Reactivity(a) >= truth.Reactivity(b) ? a : b, other = hubItem == a ? b : a;
                        int hub = -1, room = 0;
                        for (int t = 0; t < Bench.TileCount; t++)
                        {
                            if (run.Bench.Tiles[t] != null || rules.Blocked[t]) continue;
                            int free = rules.Around(t).Count(n => run.Bench.Tiles[n] == null);
                            if (free > room) { room = free; hub = t; }
                        }
                        if (hub < 0 || room < 1) return;
                        long before = Believed(run);
                        var around = rules.Around(hub).Where(n => run.Bench.Tiles[n] == null).ToList();
                        run.Place(run.Hand.ToList().IndexOf(hubItem), hub);
                        run.Place(run.Hand.ToList().IndexOf(other), around[0]);
                        int heat = truth.Reactivity(hubItem) + truth.Reactivity(other);
                        int wanted = Math.Max(HeatWanted(a), HeatWanted(b));
                        for (int k = 1; k < around.Count && heat < wanted; k++)
                        {
                            int pick = -1;
                            for (int i = 0; i < run.Hand.Count; i++)
                                if (truth.Material(run.Hand[i]).tier == 0 && (pick < 0 || truth.Reactivity(run.Hand[i]) > truth.Reactivity(run.Hand[pick]))) pick = i;
                            if (pick < 0) break;
                            heat += truth.Reactivity(run.Hand[pick]);
                            run.Place(pick, around[k]);
                        }
                        if (Believed(run) < before) foreach (int n in around.Concat(new[] { hub })) run.Unplace(n);
                        return;   // one such retry a trial
                    }
                }
            }

            /// <summary>The journal said a material wants more heat: put the liveliest spare starting materials beside it.</summary>
            void Warm(RunState run)
            {
                var rules = run.Rules;
                for (int t = 0; t < Bench.TileCount; t++)
                {
                    string m = run.Bench.Tiles[t];
                    if (m == null || !HeatHinted(m)) continue;
                    foreach (int n in rules.Around(t))
                    {
                        if (run.Bench.Tiles[n] != null) continue;
                        int pick = -1;
                        for (int i = 0; i < run.Hand.Count; i++)
                            if (truth.Material(run.Hand[i]).tier == 0 && (pick < 0 || truth.Reactivity(run.Hand[i]) > truth.Reactivity(run.Hand[pick]))) pick = i;
                        if (pick < 0) return;
                        long before = Believed(run);
                        run.Place(pick, n);
                        if (Believed(run) < before) run.Unplace(n);
                    }
                }
            }

            int PartnersInHand(RunState run, string c)
            {
                int n = 0;
                foreach (var other in run.Hand.Distinct()) if (other != c && Untried(c, other)) n++;
                return n;
            }

            /// <summary>
            /// A pair that did nothing is crossed off, unless the journal says one of the two is waiting for
            /// more heat than there was round them: then it stays open, to be tried again in livelier company.
            /// </summary>
            public void Learn(IgniteOutcome outcome, BenchRules rules)
            {
                foreach (var e in outcome.Events)
                {
                    if (e.Result != null || e.Sputter || e.Items.Length != 2) continue;
                    string key = ContentDb.PairKey(e.Items[0], e.Items[1]);
                    int heat = Math.Max(ChainPlanner.Heat(outcome.Final, e.Tiles[0], truth, rules), ChainPlanner.Heat(outcome.Final, e.Tiles[1], truth, rules));
                    int wanted = Math.Max(HeatWanted(e.Items[0]), HeatWanted(e.Items[1]));
                    if (heat >= wanted) tried.Add(key);
                    else cold[key] = (cold.TryGetValue(key, out int n) ? n : 0) + 1;
                }
            }
        }

        // ---- a career -------------------------------------------------------------------------------

        public sealed class Career
        {
            public int FirstWin = -1;                       // 1-based run index of the first Ultimate, -1: never
            public readonly List<bool> Won = new List<bool>();
            public readonly List<int> Depth = new List<int>(), Journal = new List<int>(), Goals = new List<int>(), Unlocked = new List<int>();
            public int Stuck;
            public string Knows = "";
        }

        public static Career Play(ContentDb db, bool admin, int player, int runs, bool aspects = true, bool events = true, List<string> log = null, int logFrom = 0, int taughtTier = 0)
        {
            var career = new Career();
            var journal = new Journal();
            // taughtTier: a player who has been told every recipe up to that tier of result (0: told nothing)
            foreach (var r in db.Recipes) if (db.Material(r.result).tier <= taughtTier) journal.Learn(r.items);
            var profile = new Profile(player * 977 + 13);
            var adminBot = new AutoPlayer(db);
            var newcomer = new Newcomer(db, journal);
            for (int k = 1; k <= runs; k++)
            {
                profile.RunStarted();
                var run = new RunState(db, journal, player * 100003 + k * 7919, 1,
                    new RunOptions { Unlocks = Unlocks.For(db, profile), Aspects = aspects, Events = events });
                int depth = 0, idle = 0;
                for (int guard = 0; guard < 600 && run.Phase != RunPhase.Won && run.Phase != RunPhase.Lost; guard++)
                {
                    if (run.Depth != depth) { depth = run.Depth; profile.DepthBegan(depth); profile.Evaluate(db, journal); }
                    bool ready = admin ? adminBot.Decide(run) == AutoMove.ReadyToIgnite : newcomer.Decide(run);
                    if (!ready)
                    {
                        if (run.Phase == RunPhase.Playing && ++idle > 6) { career.Stuck++; break; }   // cannot lay anything that ignites
                        continue;
                    }
                    idle = 0;
                    string laid = string.Join(" ", run.Bench.Tiles.Select(t => t ?? "."));
                    string handNow = string.Join(",", run.Hand);
                    var rulesNow = run.Rules;
                    var outcome = run.Ignite();
                    if (log != null && k >= logFrom)
                        log.Add("run " + k + " d" + run.Depth + " targets [" + string.Join("; ", newcomer.targets.Select(t => t.a + "+" + t.b)) + "] bench " + laid + " | hand " + handNow
                            + " => " + string.Join(", ", outcome.Events.Select(e => e.Result != null ? (e.Discovery ? "NEW " : "") + e.Result + " " + e.Potency : e.Sputter ? "sputter" : "fizz " + string.Join("+", e.Items) + (e.Hint != null ? " hint(" + e.Hint.material + (e.Hint.heat > 0 ? " heat " + e.Hint.heat : " " + e.Hint.partnerKind) + ")" : "")))
                            + " pot " + run.Potency + "/" + run.Target + " tried " + newcomer.TriedCount + " cold " + newcomer.ColdCount);
                    profile.IgniteResolved(outcome);
                    if (!admin) newcomer.Learn(outcome, rulesNow);
                    profile.Evaluate(db, journal);
                }
                profile.RunEnded(run);
                profile.Evaluate(db, journal);
                bool won = run.Phase == RunPhase.Won;
                if (won && career.FirstWin < 0) career.FirstWin = k;
                career.Won.Add(won); career.Depth.Add(run.Depth); career.Journal.Add(journal.KnownPairs.Count);
                career.Goals.Add(profile.Completed.Count); career.Unlocked.Add(profile.UnlockedStarters.Count + profile.UnlockedCatalysts.Count);
            }
            if (log != null)
                foreach (var r in db.Recipes.Where(r => !journal.IsKnown(r.items) && db.Material(r.result).tier >= 2 && db.Material(r.result).tier <= 3))
                    log.Add("MISSING t" + db.Material(r.result).tier + " " + string.Join("+", r.items) + " -> " + r.result + " min " + r.minReactivity
                        + (r.items.Length == 2 ? " [" + newcomer.Status(r.items[0], r.items[1]) + "]" : "") + " makeable: " + string.Join(",", r.items.Select(i => newcomer.CanMakeNow(i) ? "y" : "n")));
            career.Knows = string.Join(" ", Enumerable.Range(1, 5).Select(t => "t" + t + ":" + db.Recipes.Count(r => db.Material(r.result).tier == t && journal.IsKnown(r.items))
                + "/" + db.Recipes.Count(r => db.Material(r.result).tier == t)));
            return career;
        }

        public static string Pct(double x) => (100 * x).ToString("0") + "%";

        public static void Describe(string role, List<Career> careers, int runs, Action<string> w)
        {
            var first = careers.Where(c => c.FirstWin > 0).Select(c => c.FirstWin).OrderBy(x => x).ToList();
            int never = careers.Count - first.Count;
            w(role + ": players " + careers.Count + ", runs each " + runs);
            if (first.Count > 0)
                w(role + ": first Ultimate at run: median " + first[first.Count / 2] + ", mean " + first.Average().ToString("0.0")
                    + ", fastest " + first[0] + ", slowest " + first[first.Count - 1] + ", quartiles " + first[first.Count / 4] + " / " + first[first.Count * 3 / 4]
                    + "; never within " + runs + " runs: " + never + "/" + careers.Count);
            else w(role + ": no player made the Ultimate within " + runs + " runs");
            int[] edges = { 1, 6, 11, 21, 41, 81, 121, 161, 201, 301, 401, 501, 601, 801, 1001 };
            var parts = new List<string>();
            for (int e = 0; e + 1 < edges.Length && edges[e] <= runs; e++)
            {
                int lo = edges[e], hi = Math.Min(runs, edges[e + 1] - 1);
                var slice = careers.SelectMany(c => c.Won.Skip(lo - 1).Take(hi - lo + 1)).ToList();
                var depth = careers.SelectMany(c => c.Depth.Skip(lo - 1).Take(hi - lo + 1)).ToList();
                parts.Add("runs " + lo + "-" + hi + ": win " + Pct(slice.Count(x => x) / (double)slice.Count) + ", depth " + depth.Average().ToString("0.0"));
            }
            w(role + ": " + string.Join(" | ", parts));
            var lostAt = new int[RunState.FinalDepth + 1]; int lost = 0;
            foreach (var c in careers) for (int k = 0; k < c.Won.Count; k++) if (!c.Won[k]) { lostAt[c.Depth[k]]++; lost++; }
            w(role + ": all runs: win " + Pct(careers.Sum(c => c.Won.Count(x => x)) / (double)(careers.Count * runs))
                + ", average depth reached " + careers.SelectMany(c => c.Depth).Average().ToString("0.00")
                + ", losses by depth 1..5: " + string.Join(" ", lostAt.Skip(1).Select(n => Pct(n / (double)Math.Max(1, lost))))
                + ", stuck runs " + careers.Sum(c => c.Stuck));
            int[] marks = { 5, 10, 20, 40, 80, 120, 160, 200, 300, 400, 500, 600, 800, 1000 };
            w(role + ": journal entries (of 87) after N runs: " + string.Join(", ", marks.Where(m => m <= runs).Select(m => m + ": " + careers.Average(c => c.Journal[m - 1]).ToString("0"))));
            w(role + ": recipes known by result tier, first three players: " + string.Join(" || ", careers.Take(3).Select(c => c.Knows)));
            w(role + ": goals done (of 22) after N runs: " + string.Join(", ", marks.Where(m => m <= runs).Select(m => m + ": " + careers.Average(c => c.Goals[m - 1]).ToString("0.0"))));
        }


        /// <summary>What happened over one scripted run, for the balance report.</summary>
        public sealed class RunStats
        {
            public readonly int[] BySize = new int[ContentDb.MaxItems + 1];   // reactions by number of materials
            public int Ignites, LongestChain, Sputters, FirstTrialClears;
            public bool UltimateByCombo;                                        // an ingredient of the ultimate was made by a combo this run
            public readonly HashSet<string> ComboMade = new HashSet<string>();
            public readonly Dictionary<string, int> Combos = new Dictionary<string, int>();   // combo recipe => times fired
            public readonly List<string> ClearedIn = new List<string>();        // trials used at each depth cleared
        }

        public static RunState Autoplay(ContentDb db, int seed, List<string> log = null, RunStats stats = null)
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
        public static RunState RandomPlay(ContentDb db, int seed)
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

        public static Journal FullJournal(ContentDb db)
        {
            var j = new Journal();
            foreach (var r in db.Recipes) j.Learn(r.items);
            return j;
        }
    }
}
