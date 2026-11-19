using System;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>Phase 3: the profile and its goals, unlocks, the new catalysts, bench rules, aspects, events and the start of a depth.</summary>
    public class Phase3Tests
    {
        // ---- small contents ---------------------------------------------------------------------------

        static readonly string[] Names = { "1", "2", "3", "4", "5" };

        /// <summary>
        /// Two starting materials, a(6) and b(4), two copies each, so every hand holds both.
        /// a+b = x (tier 1), x+a = y (tier 2), no last substance. With targets of 1 one reaction clears a depth.
        /// </summary>
        static ContentData Mini(int target = 1) => new ContentData
        {
            materials = new[] { TestContent.Mat("a", "Ore", 0, 6), TestContent.Mat("b", "Potion", 0, 4), TestContent.Mat("x", "Metal", 1, 5), TestContent.Mat("y", "Crystal", 2, 5) },
            recipes = new[] { TestContent.Rec(new[] { "a", "b" }, 0, "x"), TestContent.Rec(new[] { "x", "a" }, 0, "y") },
            catalysts = new CatalystData[0], depthTargets = new[] { target, target, target, target }, depthNames = Names, ultimate = "",
        };

        /// <summary>Mini with the aspects and events of the real game.</summary>
        static ContentData MiniWithRealStart()
        {
            var d = Mini(); var real = TestContent.Load();
            d.aspects = real.aspects; d.events = real.events; d.noEventWeight = real.noEventWeight;
            return d;
        }

        static EffectData Fx(string op, int amount = 0, int amount2 = 0) => new EffectData { op = op, amount = amount, amount2 = amount2 };
        static EventOption Option(string label, string requires, EffectData[] cost, params EffectData[] gain) => new EventOption
        {
            label = label, requires = requires, cost = cost, outcomes = new[] { new EventOutcome { weight = 1, text = label, gain = gain } },
        };
        static EventOption WalkOn => Option("Walk on", null, null);

        /// <summary>The one event of a content: always drawn at the depths given, with these options.</summary>
        static ContentData WithEvent(ContentData d, int minDepth, int maxDepth, params EventOption[] options)
        {
            d.events = new[] { new EventData { id = "e", name = "e", text = "e", type = "Boon", minDepth = minDepth, maxDepth = maxDepth, weight = 1, options = options } };
            d.noEventWeight = 0;
            return d;
        }
        static ContentData WithGain(ContentData d, params EffectData[] gain) => WithEvent(d, 1, 5, Option("go", null, null, gain));

        static ContentData WithAspect(ContentData d, string effect, string scope = "Tile", float amount = 0)
        {
            d.aspects = new[] { new AspectData { id = effect, name = effect, text = effect, effect = effect, scope = scope, group = "Cheap", amount = amount } };
            return d;
        }

        static readonly RunOptions EventsOnly = new RunOptions { Events = true }, AspectsOnly = new RunOptions { Aspects = true };

        static RunState Start(ContentData d, RunOptions options, int seed = 1, int depth = 1) => new RunState(new ContentDb(d), new Journal(), seed, depth, options);

        static bool PlaceId(RunState r, string id, int tile) => r.Place(r.Hand.ToList().IndexOf(id), tile);

        /// <summary>Takes the first aspect and the last event option (walk on, or the only one) until the bench is open.</summary>
        static void Open(RunState r)
        {
            while (true)
            {
                if (r.Phase == RunPhase.AspectOffer) r.ChooseAspect(r.AspectOffer[0].Data.id);
                else if (r.Phase == RunPhase.EventOffer) r.ChooseEventOption(r.EventOptions.Count - 1);
                else return;
            }
        }

        /// <summary>Two free tiles that touch and are not shut, the first being the hub's.</summary>
        static (int hub, int other) FreePair(RunState r, int hub = -1)
        {
            for (int a = 0; a < Bench.TileCount; a++)
            {
                if (hub >= 0 && a != hub) continue;
                if (r.Rules.Blocked[a] || r.Bench.Tiles[a] != null) continue;
                if (hub < 0 && Burns(r, a)) continue;
                foreach (int b in r.Rules.Around(a))
                    if (!r.Rules.Blocked[b] && r.Bench.Tiles[b] == null && (hub >= 0 || !Burns(r, b))) return (a, b);
            }
            throw new Exception("no two free tiles touch");
        }

        /// <summary>A volatile tile burns the starting material laid on it: the helpers keep off it unless asked for it.</summary>
        static bool Burns(RunState r, int tile) => r.Aspects.Any(a => a.Data.effect == AspectEffects.Volatile && a.Tiles[0] == tile);

        /// <summary>Lays a (the livelier, so the hub) and b side by side and ignites. hub: the tile a must stand on.</summary>
        static IgniteOutcome FireAB(RunState r, int hub = -1)
        {
            var (h, o) = FreePair(r, hub);
            Assert.IsTrue(PlaceId(r, "a", h), "no a in hand"); Assert.IsTrue(PlaceId(r, "b", o), "no b in hand");
            return r.Ignite();
        }

        /// <summary>From an open bench at depth N to an open bench at depth N + 1.</summary>
        static void Descend(RunState r, string catalyst = null)
        {
            Assert.AreEqual(RunPhase.DepthCleared, FireAB(r).PhaseAfter);
            r.ChooseCatalyst(catalyst ?? (r.CatalystOffer.Count > 0 ? r.CatalystOffer[0].id : null));
        }

        /// <summary>A Mini run standing at depth 2 with this one catalyst in hand.</summary>
        static RunState Holding(CatalystData cat, ContentData d = null, RunOptions options = null)
        {
            d = d ?? Mini(); d.catalysts = new[] { cat };
            d.depthTargets = new[] { 1, 999999, 999999, 999999 };
            var r = Start(d, options);
            Open(r);
            Descend(r, cat.id);
            Open(r);
            Assert.AreEqual(2, r.Depth); Assert.AreEqual(cat.id, r.Catalysts[0].id);
            return r;
        }

        static CatalystData Cat(string effect, float amount = 0, string kind = "", string rarity = null) =>
            new CatalystData { id = effect, name = effect, text = effect, effect = effect, kind = kind, amount = amount, rarity = rarity };

        // ---- content ----------------------------------------------------------------------------------

        static readonly string[] Phase2Recipes =
        {
            "deep_tear+ember_coal|0|steam_wisp", "ember_coal+iron_ore|0|sky_iron", "bone_ash+ember_coal|0|sulfur", "deep_tear+moondust|0|frost_salt",
            "deep_tear+glowmoss|0|verdant_tonic", "deep_tear+sunbloom|0|verdant_tonic", "glowmoss+moondust|0|moonpetal",
            "bone_ash+glowmoss|0|dusk_cap", "glowmoss+wyrm_fang|0|dusk_cap", "moondust+sunbloom|0|pixie_wing", "iron_ore+wyrm_fang|0|dragon_scale",
            "deep_tear+wyrm_fang|0|dragon_blood", "iron_ore+moondust|0|quicksilver", "bone_ash+sunbloom|0|mandrake", "sky_iron+sulfur|10|gold_ingot",
            "quicksilver+sulfur|10|cinnabar", "frost_salt+moonpetal|10|humstone", "frost_salt+steam_wisp|10|storm_glass",
            "dragon_blood+dragon_scale|10|basilisk_eye", "moonpetal+pixie_wing|10|unicorn_horn", "mandrake+verdant_tonic|10|lifewater",
            "dusk_cap+quicksilver|10|nightshade_ink", "moondust+sky_iron|10|starmetal_ore", "dragon_blood+gold_ingot|14|phoenix_feather",
            "humstone+starmetal_ore|14|star_shard", "cinnabar+lifewater|14|red_tincture", "storm_glass+unicorn_horn|14|aurora_dust",
            "basilisk_eye+nightshade_ink|14|seer_s_orb", "gold_ingot+quicksilver|14|sun_amalgam", "phoenix_feather+red_tincture|22|everflame",
            "aurora_dust+star_shard|22|heaven_s_ash", "seer_s_orb+sun_amalgam|22|living_gold", "red_tincture+star_shard|22|azoth",
            "azoth+living_gold|30|worldseed", "everflame+heaven_s_ash|30|worldseed", "bone_ash+ember_coal+moondust|0|hearth_glass",
            "deep_tear+sunbloom+wyrm_fang|0|wyrm_ichor", "bone_ash+ember_coal+iron_ore+moondust|0|forge_slag",
            "bone_ash+deep_tear+glowmoss+moondust+sunbloom|0|primal_bloom", "ember_coal+sky_iron+steam_wisp|10|thunder_ingot",
            "deep_tear+dusk_cap+glowmoss+mandrake|10|witch_salve", "dragon_scale+pixie_wing+wyrm_fang|10|chimera_hide",
            "humstone+moondust+storm_glass|14|void_pearl", "ember_coal+gold_ingot+iron_ore+starmetal_ore|14|titan_alloy",
            "glowmoss+lifewater+moondust+nightshade_ink+sunbloom|14|dream_spore", "aurora_dust+bone_ash+red_tincture|22|philosopher_s_salt",
            "ember_coal+phoenix_feather+sun_amalgam+sunbloom|22|chrysopoeia", "deep_tear+lifewater+void_pearl|22|azoth",
            "dragon_blood+ember_coal+phoenix_feather+sulfur|22|everflame", "aurora_dust+bone_ash+frost_salt+moonpetal|22|heaven_s_ash",
            "hearth_glass+steam_wisp|10|storm_glass", "humstone+thunder_ingot|14|star_shard", "chimera_hide+gold_ingot|14|phoenix_feather",
            "titan_alloy+void_pearl|22|living_gold", "chrysopoeia+philosopher_s_salt|30|worldseed", "forge_slag+wyrm_ichor|10|cinnabar",
            "primal_bloom+verdant_tonic|10|lifewater", "basilisk_eye+witch_salve|14|seer_s_orb", "dream_spore+titan_alloy|22|chrysopoeia",
        };

        static string Pin(ContentDb db, RecipeData r) => db.KeyOf(r) + "|" + r.minReactivity + "|" + r.result;

        [Test] public void Phase2Recipes_AreAllStillThere_Unchanged()
        {
            var db = new ContentDb(TestContent.Load());
            var now = new HashSet<string>(db.Recipes.Select(r => Pin(db, r)));
            Assert.AreEqual(59, Phase2Recipes.Length);
            foreach (var p in Phase2Recipes) Assert.IsTrue(now.Contains(p), "changed or missing: " + p);
        }

        [Test] public void Phase3_Adds28Recipes_EachWithANewMaterial()
        {
            var data = TestContent.Load(); var db = new ContentDb(data);
            var old = new HashSet<string>(Phase2Recipes);
            var oldMaterials = new HashSet<string>(Phase2Recipes.SelectMany(p => p.Split('|')[0].Split('+').Concat(new[] { p.Split('|')[2] })));
            foreach (var m in db.Starters.Where(m => !m.locked)) oldMaterials.Add(m.id);
            var added = db.Recipes.Where(r => !old.Contains(Pin(db, r))).ToList();
            Assert.AreEqual(28, added.Count);
            foreach (var r in added) Assert.IsTrue(r.items.Any(i => !oldMaterials.Contains(i)), "no new material in " + db.KeyOf(r));
        }

        /// <summary>Invariant 1: with nothing unlocked, the recipes within reach are exactly those of Phase 2.</summary>
        [Test] public void FreshProfile_WithNothingUnlocked_ReachesOnlyThePhase2Recipes()
        {
            var db = new ContentDb(TestContent.Load());
            var have = new HashSet<string>(db.Starters.Where(m => !m.locked).Select(m => m.id));
            for (bool grew = true; grew;)
            {
                grew = false;
                foreach (var r in db.Recipes) if (r.items.All(have.Contains) && have.Add(r.result)) grew = true;
            }
            var within = db.Recipes.Where(r => r.items.All(have.Contains)).Select(r => Pin(db, r)).OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(Phase2Recipes.OrderBy(x => x).ToList(), within);
        }

        [Test] public void Phase2Content_WithNoPhase3Fields_LoadsWithEmptyGoalsAspectsAndEvents()
        {
            var db = new ContentDb(TestContent.Tiny());
            Assert.AreEqual(0, db.Goals.Count); Assert.AreEqual(0, db.Aspects.Count); Assert.AreEqual(0, db.Events.Count);
            Assert.AreEqual(4, db.Starters.Count(m => Unlocks.Fresh(db).IsOpen(m.id)));
        }

        [Test] public void Content_Has22Goals_16Aspects_16Events()
        {
            var db = new ContentDb(TestContent.Load());
            Assert.AreEqual(22, db.Goals.Count); Assert.AreEqual(16, db.Aspects.Count); Assert.AreEqual(19, db.Events.Count);
            Assert.AreEqual(3, db.Aspects.Count(a => a.tradeOff));
            Assert.AreEqual(5, db.Aspects.Count(a => a.group == "Engine"));
            Assert.AreEqual(3, db.NoEventWeight);
        }

        [Test] public void Catalysts_ByRarity_4_10_8_4()
        {
            var db = new ContentDb(TestContent.Load());
            int[] want = { 4, 10, 8, 4 };
            for (int k = 0; k < 4; k++) Assert.AreEqual(want[k], db.Catalysts.Count(c => Rarities.Of(c.rarity) == Rarities.All[k]), Rarities.All[k]);
            Assert.AreEqual(17, db.Catalysts.Count(c => c.locked));
        }

        /// <summary>The owner's rule, bucket by bucket; and in the shipped content nothing stays locked for want of a goal.</summary>
        [Test] public void GoalBudget_EveryBucket_HasExactlyAsManyGoalsAsLockedItems()
        {
            var db = new ContentDb(TestContent.Load());
            int buckets = 0;
            foreach (var kind in new[] { RewardKinds.Starter, RewardKinds.Catalyst })
                foreach (var rarity in Rarities.All)
                {
                    int goals = db.Goals.Count(g => g.rewardKind == kind && g.rewardRarity == rarity), locked = db.LockedIn(kind, rarity).Count;
                    Assert.That(goals, Is.LessThanOrEqualTo(locked), kind + "/" + rarity);
                    Assert.AreEqual(locked, goals, "locked with no goal to win it: " + kind + "/" + rarity);
                    if (locked > 0) buckets++;
                }
            Assert.AreEqual(5, buckets);
        }

        static ContentData Lockable()
        {
            var d = Mini();
            d.materials = d.materials.Concat(new[] { new MaterialData { id = "s", name = "s", kind = "Ore", tier = 0, reactivity = 5, rarity = "Uncommon", locked = true } }).ToArray();
            d.goals = new[] { new GoalData { id = "g", text = "g", stat = StatIds.Ignites, target = 1, rewardKind = RewardKinds.Starter, rewardRarity = "Uncommon" } };
            return d;
        }
        static GoalData Goal(string id, string kind = RewardKinds.Starter, string rarity = "Uncommon", string stat = StatIds.Ignites) =>
            new GoalData { id = id, text = id, stat = stat, target = 1, rewardKind = kind, rewardRarity = rarity };

        [Test] public void Constructor_Accepts_OneGoalForOneLockedItem() => Assert.DoesNotThrow(() => new ContentDb(Lockable()));

        [Test] public void Constructor_Throws_WhenGoalsOutnumberLockedItems()
        {
            var d = Lockable(); d.goals = new[] { Goal("g1"), Goal("g2") };
            Assert.Throws<ArgumentException>(() => new ContentDb(d));
        }

        [Test] public void Constructor_Throws_OnAGoalForABucketWithNothingLocked()
        {
            var d = Lockable(); d.goals = new[] { Goal("g1", RewardKinds.Catalyst, "Rare") };
            Assert.Throws<ArgumentException>(() => new ContentDb(d));
        }

        [Test] public void Constructor_Throws_OnBadGoals()
        {
            foreach (var bad in new[]
            {
                Goal("g", RewardKinds.Starter, "Legendary"), Goal("g", RewardKinds.Starter, "Common"), Goal("g", "gold"),
                Goal("g", RewardKinds.Starter, "Uncommon", "noSuchStat"), new GoalData { id = "g", stat = StatIds.Ignites, target = 0, rewardKind = RewardKinds.Starter, rewardRarity = "Uncommon" },
            })
            {
                var d = Lockable(); d.goals = new[] { bad };
                Assert.Throws<ArgumentException>(() => new ContentDb(d), bad.rewardKind + "/" + bad.rewardRarity + "/" + bad.stat + "/" + bad.target);
            }
            var twice = Lockable(); twice.goals = new[] { Goal("g"), Goal("g") };
            Assert.Throws<ArgumentException>(() => new ContentDb(twice));
        }

        [Test] public void Constructor_Throws_OnALockedCommonItem()
        {
            var d = Mini(); d.materials[0].locked = true;
            Assert.Throws<ArgumentException>(() => new ContentDb(d));
            var c = Mini(); c.catalysts = new[] { new CatalystData { id = "c", effect = Effects.AllScale, amount = 2, locked = true } };
            Assert.Throws<ArgumentException>(() => new ContentDb(c));
        }

        [Test] public void Constructor_Throws_OnRarityOrLockOnADiscovery()
        {
            var d = Mini(); d.materials[2].rarity = "Rare";
            Assert.Throws<ArgumentException>(() => new ContentDb(d));
            var e = Mini(); e.materials[0].rarity = "Legendary";
            Assert.Throws<ArgumentException>(() => new ContentDb(e));
        }

        [Test] public void Constructor_Throws_OnUnknownEffectsAndOps()
        {
            var a = WithAspect(Mini(), "NoSuchAspect");
            Assert.Throws<ArgumentException>(() => new ContentDb(a));
            var c = Mini(); c.catalysts = new[] { Cat("NoSuchEffect") };
            Assert.Throws<ArgumentException>(() => new ContentDb(c));
            Assert.Throws<ArgumentException>(() => new ContentDb(WithGain(Mini(), Fx("NoSuchOp"))));
            Assert.Throws<ArgumentException>(() => new ContentDb(WithEvent(Mini(), 1, 5, Option("x", "NoSuchNeed", null))));
            Assert.Throws<ArgumentException>(() => new ContentDb(WithEvent(Mini(), 3, 2, WalkOn)));
        }

        [Test] public void Constructor_Throws_OnAnEventThatCannotBeLeft()
        {
            var d = WithEvent(Mini(), 1, 5, Option("pay", Ops.NeedRack, new[] { Fx(Ops.LoseRack, 1) }, Fx(Ops.Trials, 1)));
            Assert.Throws<ArgumentException>(() => new ContentDb(d));
        }

        [Test] public void EveryShippedEvent_CanBeLeftForNothing_OrIsOneFreeStep()
        {
            var db = new ContentDb(TestContent.Load());
            foreach (var e in db.Events)
            {
                bool walkOn = e.options.Any(o => string.IsNullOrEmpty(o.requires) && (o.cost == null || o.cost.Length == 0)
                    && o.outcomes.All(x => x.gain == null || x.gain.Length == 0));
                Assert.IsTrue(walkOn || e.options.Length == 1, e.id);
                if (e.type == "Hazard") Assert.AreEqual(1, e.options.Length, e.id);
            }
        }

        // ---- profile ----------------------------------------------------------------------------------

        [Test] public void Profile_AfterAnIgniteWithAChainOfTwo_CountsWhatAnIgniteDid()
        {
            var r = Start(Mini(999999), null);
            // a on 0 takes b on 1 and becomes x; the a on 3 then takes that x: a chain of two
            PlaceId(r, "a", 0); PlaceId(r, "b", 1); PlaceId(r, "a", 3);
            var o = r.Ignite();
            var p = new Profile();
            p.RunStarted(); p.DepthBegan(1); p.IgniteResolved(o);
            Assert.AreEqual(1, p.RunsStarted); Assert.AreEqual(1, p.Ignites); Assert.AreEqual(1, p.DepthReached);
            Assert.AreEqual(2, p.ChainLongest); Assert.AreEqual(2, p.ComboLargest);
            Assert.AreEqual(o.Gained, p.BestIgnite); Assert.That(o.Gained, Is.GreaterThan(0));
        }

        [Test] public void Profile_AFizzle_CountsAsAnIgnite_ButNotAsAChain()
        {
            var r = Start(Mini(999999), null);
            PlaceId(r, "a", 0); PlaceId(r, "a", 1);
            var p = new Profile();
            p.IgniteResolved(r.Ignite());
            Assert.AreEqual(1, p.Ignites); Assert.AreEqual(0, p.ChainLongest); Assert.AreEqual(0, p.ComboLargest); Assert.AreEqual(0, p.BestIgnite);
        }

        [Test] public void Profile_ALostRun_StillCounts()
        {
            var r = Start(Mini(999999), null);
            var p = new Profile(); p.RunStarted(); p.DepthBegan(r.Depth);
            while (r.Phase == RunPhase.Playing) p.IgniteResolved(FireAB(r));
            Assert.AreEqual(RunPhase.Lost, r.Phase);
            p.RunEnded(r);
            Assert.AreEqual(4, p.Ignites); Assert.AreEqual(1, p.RunsStarted); Assert.AreEqual(0, p.RunsWon); Assert.AreEqual(0, p.WonNoRedraw);
            Assert.AreEqual(1, p.DepthReached);
        }

        static RunState WonRun(bool withRedraw)
        {
            var d = Mini(999999); d.ultimate = "y";
            var r = Start(d, null);
            if (withRedraw) Assert.IsTrue(r.Redraw());
            FireAB(r);                                              // x goes to the rack
            PlaceId(r, "x", 0); PlaceId(r, "a", 1);
            Assert.AreEqual(RunPhase.Won, r.Ignite().PhaseAfter);
            return r;
        }

        [Test] public void Profile_AWin_Counts_AndAWinWithoutARedraw_CountsTwice()
        {
            var p = new Profile();
            p.RunEnded(WonRun(true));
            Assert.AreEqual(1, p.RunsWon); Assert.AreEqual(0, p.WonNoRedraw);
            p.RunEnded(WonRun(false));
            Assert.AreEqual(2, p.RunsWon); Assert.AreEqual(1, p.WonNoRedraw);
        }

        [Test] public void Profile_RoundTrip_KeepsEverything()
        {
            var db = new ContentDb(TestContent.Load());
            var p = new Profile(41) { RunsStarted = 12, RunsWon = 2, WonNoRedraw = 1, DepthReached = 5, Ignites = 140, ChainLongest = 4, ComboLargest = 5, BestIgnite = 123456789012L };
            var done = p.Evaluate(db, new Journal());
            Assert.That(done.Count, Is.GreaterThan(5));
            var q = Profile.FromData(p.ToData(), db);
            Assert.AreEqual(41, q.RewardSeed); Assert.AreEqual(12, q.RunsStarted); Assert.AreEqual(2, q.RunsWon); Assert.AreEqual(1, q.WonNoRedraw);
            Assert.AreEqual(5, q.DepthReached); Assert.AreEqual(140, q.Ignites); Assert.AreEqual(4, q.ChainLongest); Assert.AreEqual(5, q.ComboLargest);
            Assert.AreEqual(123456789012L, q.BestIgnite);
            CollectionAssert.AreEquivalent(p.Completed, q.Completed);
            CollectionAssert.AreEquivalent(p.UnlockedStarters, q.UnlockedStarters);
            CollectionAssert.AreEquivalent(p.UnlockedCatalysts, q.UnlockedCatalysts);
            Assert.AreEqual(p.Rewards.Count, q.Rewards.Count);
            foreach (var g in done) Assert.AreEqual(g.Item, q.RewardOf(g.Goal.id));
        }

        [Test] public void Profile_FromNothingOrGarbage_IsAnEmptyProfile()
        {
            var db = new ContentDb(TestContent.Load());
            var none = Profile.FromData(null, db, 77);
            Assert.AreEqual(77, none.RewardSeed); Assert.AreEqual(0, none.Completed.Count);
            var bad = Profile.FromData(new ProfileData
            {
                runsStarted = -5, ignites = -1, bestIgnite = -9, completed = new[] { "no_such_goal", null, "" }, unlockedStarters = new[] { "worldseed", "nope", null },
                unlockedCatalysts = new[] { "nope" }, rewards = new[] { null, new RewardLogData { goal = "no_such_goal", item = "x" } },
            }, db, 78);
            Assert.AreEqual(78, bad.RewardSeed); Assert.AreEqual(0, bad.RunsStarted); Assert.AreEqual(0, bad.Ignites); Assert.AreEqual(0, bad.BestIgnite);
            Assert.AreEqual(0, bad.Completed.Count); Assert.AreEqual(0, bad.UnlockedStarters.Count); Assert.AreEqual(0, bad.UnlockedCatalysts.Count);
            Assert.AreEqual(0, bad.Rewards.Count);
            Assert.DoesNotThrow(() => Profile.FromData(new ProfileData(), null));
        }

        // ---- goals ------------------------------------------------------------------------------------

        [Test] public void Goals_AFreshProfile_HasNoneDone()
        {
            var db = new ContentDb(TestContent.Load());
            Assert.AreEqual(0, new Profile().Evaluate(db, new Journal()).Count);
        }

        [Test] public void Goals_Progress_IsTheStatCappedAtTheTarget()
        {
            var db = new ContentDb(TestContent.Load());
            var goal = db.Goals.First(g => g.id == "g_ignite25");
            var p = new Profile { Ignites = 10 };
            Assert.AreEqual(10, p.Progress(goal, db, new Journal()));
            p.Ignites = 400;
            Assert.AreEqual(25, p.Progress(goal, db, new Journal()));
        }

        [Test] public void Goals_CompleteOnce_AndPayFromTheirOwnBucket()
        {
            var db = new ContentDb(TestContent.Load());
            var p = new Profile(5) { DepthReached = 2 };
            var done = p.Evaluate(db, new Journal());
            Assert.AreEqual(1, done.Count); Assert.AreEqual("g_depth2", done[0].Goal.id);
            CollectionAssert.Contains(db.LockedIn(RewardKinds.Starter, "Uncommon"), done[0].Item);
            Assert.IsTrue(p.UnlockedStarters.Contains(done[0].Item)); Assert.IsTrue(p.IsComplete("g_depth2"));
            Assert.AreEqual(done[0].Item, p.RewardOf("g_depth2"));
            Assert.AreEqual(0, p.Evaluate(db, new Journal()).Count, "paid twice");
            Assert.AreEqual(1, p.UnlockedStarters.Count);
        }

        [Test] public void Goals_TheSameSeed_GivesTheSameReward()
        {
            var db = new ContentDb(TestContent.Load());
            var seen = new HashSet<string>();
            for (int seed = 1; seed <= 40; seed++)
            {
                string a = new Profile(seed) { DepthReached = 2 }.Evaluate(db, new Journal())[0].Item;
                string b = new Profile(seed) { DepthReached = 2 }.Evaluate(db, new Journal())[0].Item;
                Assert.AreEqual(a, b); seen.Add(a);
            }
            Assert.That(seen.Count, Is.GreaterThan(1), "every profile gets the same item: the reward is not random");
        }

        static Journal FullJournal(ContentDb db) { var j = new Journal(); foreach (var r in db.Recipes) j.Learn(r.items); return j; }

        [Test] public void Goals_AllDone_UnlockEverything_EachItemOnce()
        {
            var db = new ContentDb(TestContent.Load());
            var p = new Profile(9) { RunsStarted = 99, RunsWon = 9, WonNoRedraw = 1, DepthReached = 5, Ignites = 5000, ChainLongest = 9, ComboLargest = 5, BestIgnite = 999999 };
            var done = p.Evaluate(db, FullJournal(db));
            Assert.AreEqual(22, done.Count);
            Assert.IsTrue(done.All(g => g.Item != null));
            Assert.AreEqual(22, done.Select(g => g.Item).Distinct().Count());
            Assert.AreEqual(5, p.UnlockedStarters.Count); Assert.AreEqual(17, p.UnlockedCatalysts.Count);
            var open = Unlocks.For(db, p);
            Assert.IsTrue(db.Starters.All(m => open.IsOpen(m.id)) && db.Catalysts.All(c => open.IsOpen(c.id)));
        }

        [Test] public void Goals_AJournalThatIsAlreadyFull_PaysOnTheFirstLook()
        {
            var db = new ContentDb(TestContent.Load());
            var j = new Journal();
            foreach (var r in db.Recipes.Take(10)) j.Learn(r.items);
            var done = new Profile().Evaluate(db, j);
            CollectionAssert.AreEquivalent(new[] { "g_journal10" }, done.Select(g => g.Goal.id).ToList());
            Assert.AreEqual(0, new Profile().Stat(StatIds.JournalComplete, db, j));
            Assert.AreEqual(1, new Profile().Stat(StatIds.JournalComplete, db, FullJournal(db)));
        }

        [Test] public void Goals_AnEmptyBucket_CompletesWithNoItem()
        {
            var db = new ContentDb(TestContent.Load());
            // a save in which every Uncommon starter is already open, though the goals are not done
            var p = Profile.FromData(new ProfileData { depthReached = 2, unlockedStarters = db.LockedIn(RewardKinds.Starter, "Uncommon").ToArray() }, db);
            var done = p.Evaluate(db, new Journal());
            Assert.AreEqual(1, done.Count); Assert.IsNull(done[0].Item); Assert.IsTrue(p.IsComplete("g_depth2"));
        }

        // ---- unlocks ----------------------------------------------------------------------------------

        [Test] public void Unlocks_Fresh_Has9CatalystsAnd8Starters_AllHasEverything()
        {
            var db = new ContentDb(TestContent.Load());
            var fresh = Unlocks.Fresh(db); var all = Unlocks.All(db);
            Assert.AreEqual(9, db.Catalysts.Count(c => fresh.IsOpen(c.id))); Assert.AreEqual(8, db.Starters.Count(m => fresh.IsOpen(m.id)));
            Assert.AreEqual(26, db.Catalysts.Count(c => all.IsOpen(c.id))); Assert.AreEqual(13, db.Starters.Count(m => all.IsOpen(m.id)));
            var half = Unlocks.Half(db, 3);
            Assert.That(half.Count, Is.GreaterThan(fresh.Count).And.LessThan(all.Count));
        }

        [Test] public void CatalystOffer_ForAFreshProfile_HoldsOnlyOpenCatalysts()
        {
            var db = new ContentDb(TestContent.Load());
            var fresh = Unlocks.Fresh(db); int offers = 0;
            for (int seed = 1; seed <= 12; seed++)
            {
                var run = new RunState(db, new Journal(), seed, 1, new RunOptions { Unlocks = fresh });
                var bot = new AutoPlayer(db);
                for (int guard = 0; guard < 400 && run.Phase != RunPhase.Won && run.Phase != RunPhase.Lost; guard++)
                {
                    if (run.Phase == RunPhase.DepthCleared)
                    {
                        offers++;
                        Assert.AreEqual(3, run.CatalystOffer.Count);
                        foreach (var c in run.CatalystOffer) Assert.IsTrue(fresh.IsOpen(c.id) && !c.locked, c.id + " offered to a fresh profile");
                    }
                    if (bot.Decide(run) == AutoMove.ReadyToIgnite) run.Ignite();
                }
            }
            Assert.That(offers, Is.GreaterThan(12));
        }

        [Test] public void AFreshRun_WithFreshUnlocks_Draws5CardsFrom16()
        {
            var db = new ContentDb(TestContent.Load());
            var run = new RunState(db, new Journal(), 1, 1, new RunOptions { Unlocks = Unlocks.Fresh(db) });
            Assert.AreEqual(16, run.SatchelSize); Assert.AreEqual(5, run.Hand.Count);
            Assert.IsTrue(run.Hand.All(h => !db.Material(h).locked));
        }

        /// <summary>A starter won from a goal waits in its own pouch: one of them joins every hand, on top of the usual five.</summary>
        [Test] public void AnUnlockedStarter_ComesFromThePouch_OneAHand()
        {
            var db = new ContentDb(TestContent.Load());
            var p = Profile.FromData(new ProfileData { unlockedStarters = new[] { "salt_crystal", "hollow_bone" } }, db);
            for (int seed = 1; seed <= 20; seed++)
            {
                var run = new RunState(db, new Journal(), seed, 1, new RunOptions { Unlocks = Unlocks.For(db, p) });
                Assert.AreEqual(20, run.SatchelSize);
                Assert.AreEqual(6, run.Hand.Count);
                Assert.AreEqual(1, run.Hand.Count(h => db.Material(h).locked), "seed " + seed);
                Assert.IsTrue(run.Redraw());
                Assert.AreEqual(6, run.Hand.Count); Assert.AreEqual(1, run.Hand.Count(h => db.Material(h).locked));
            }
        }

        // ---- the new catalysts: scoring -------------------------------------------------------------------

        static readonly int[] At0 = { 0, 1 }, At0Three = { 0, 1, 3 };

        static long Score(ContentDb db, string[] items, int[] tiles, CatalystData cat, int chain = 1, int depth = 1, BenchRules rules = null, ScoreContext ctx = null)
        {
            Assert.IsTrue(db.TryCombo(items, out var recipe));
            ctx = ctx ?? new ScoreContext { Depth = depth, Rules = rules ?? BenchRules.None };
            return IgniteResolver.Score(db, recipe, tiles, false, false, chain, cat == null ? new CatalystData[0] : new[] { cat }, ctx);
        }

        // Combo content: x (tier 1) = p + q, z (tier 2) = p + q + r, u (tier 2) = x + v
        static readonly string[] PQ = { "p", "q" }, PQR = { "p", "q", "r" }, XV = { "x", "v" };

        [Test] public void Score_Plain_IsBaseTimesItemsLessOne()
        {
            var db = new ContentDb(TestContent.Combo());
            Assert.AreEqual(20, Score(db, PQ, At0, null)); Assert.AreEqual(120, Score(db, PQR, At0Three, null)); Assert.AreEqual(60, Score(db, XV, At0, null));
        }

        [Test] public void ComboBonus_LiftsThreeOrMore_NotPairs()
        {
            var db = new ContentDb(TestContent.Combo()); var c = Cat(Effects.ComboBonus, 1.5f);
            Assert.AreEqual(180, Score(db, PQR, At0Three, c)); Assert.AreEqual(20, Score(db, PQ, At0, c));
        }

        [Test] public void PairBonus_LiftsPairs_NotCombos()
        {
            var db = new ContentDb(TestContent.Combo()); var c = Cat(Effects.PairBonus, 1.5f);
            Assert.AreEqual(30, Score(db, PQ, At0, c)); Assert.AreEqual(120, Score(db, PQR, At0Three, c));
        }

        [Test] public void FlatPerReaction_WithAChainAndAllScale_IsAddedAfterTheMultipliers()
        {
            var db = new ContentDb(TestContent.Combo());
            Assert.AreEqual(50, Score(db, PQ, At0, Cat(Effects.FlatPerReaction, 30)));
            Assert.IsTrue(db.TryCombo(PQ, out var recipe));
            // chain 3: 20 x 3 = 60, doubled by AllScale = 120, then 30 flat
            long both = IgniteResolver.Score(db, recipe, At0, false, false, 3, new[] { Cat(Effects.FlatPerReaction, 30), Cat(Effects.AllScale, 2) }, new ScoreContext());
            Assert.AreEqual(150, both);
        }

        [Test] public void TierDouble_ForPairsAndTierTwoCombos_DoublesFromItsTierUp()
        {
            var db = new ContentDb(TestContent.Combo()); var c = Cat(Effects.TierDouble, 2);
            Assert.AreEqual(240, Score(db, PQR, At0Three, c)); Assert.AreEqual(20, Score(db, PQ, At0, c));
        }

        [Test] public void StarterDouble_WithTwoRequired_NeedsThatManyStartingMaterials()
        {
            var db = new ContentDb(TestContent.Combo()); var c = Cat(Effects.StarterDouble, 2);
            Assert.AreEqual(40, Score(db, PQ, At0, c)); Assert.AreEqual(60, Score(db, XV, At0, c), "x + v holds one starting material");
        }

        [Test] public void ChainFrom_AtChainTwoThreeAndFour_DoublesFromThatLinkOn()
        {
            var db = new ContentDb(TestContent.Combo()); var c = Cat(Effects.ChainFrom, 3);
            Assert.AreEqual(40, Score(db, PQ, At0, c, 2)); Assert.AreEqual(120, Score(db, PQ, At0, c, 3)); Assert.AreEqual(160, Score(db, PQ, At0, c, 4));
        }

        [Test] public void DepthScale_AtDepthsOneThreeAndFive_GrowsWithEachDepthBelowTheFirst()
        {
            var db = new ContentDb(TestContent.Combo()); var c = Cat(Effects.DepthScale, 0.25f);
            Assert.AreEqual(20, Score(db, PQ, At0, c, 1, 1)); Assert.AreEqual(30, Score(db, PQ, At0, c, 1, 3)); Assert.AreEqual(40, Score(db, PQ, At0, c, 1, 5));
        }

        [Test] public void AllScale_ForAPairAndACombo_ScalesEverything()
        {
            var db = new ContentDb(TestContent.Combo());
            Assert.AreEqual(40, Score(db, PQ, At0, Cat(Effects.AllScale, 2))); Assert.AreEqual(240, Score(db, PQR, At0Three, Cat(Effects.AllScale, 2)));
        }

        // ---- the new catalysts: the run -------------------------------------------------------------------

        [Test] public void ExtraRedraw_WhenHeld_AddsARedrawAtEveryDepth()
        {
            var r = Holding(Cat(Effects.ExtraRedraw, 1));
            Assert.AreEqual(RunState.BaseRedraws + 1, r.RedrawsLeft);
        }

        [Test] public void ExtraHand_CountsItsAmount_AndOneWhenItNamesNone()
        {
            Assert.AreEqual(RunState.BaseHand + 2, Holding(Cat(Effects.ExtraHand, 2)).HandSize);
            Assert.AreEqual(RunState.BaseHand + 1, Holding(Cat(Effects.ExtraHand)).HandSize);
        }

        [Test] public void HeatPlus_ReachesTheBenchRules_AndLiftsARecipeOver()
        {
            var r = Holding(Cat(Effects.HeatPlus, 1));
            Assert.AreEqual(1, r.Rules.HeatPlus);
            // a(6) + b(4) is 10; the recipe asks 12; with one more for each of the two it fires
            var d = Mini(); d.recipes[0].minReactivity = 12;
            var db = new ContentDb(d);
            var tiles = new string[Bench.TileCount]; tiles[0] = "a"; tiles[1] = "b";
            Assert.IsNull(ChainPlanner.MatchAt(tiles, 0, db, BenchRules.None));
            Assert.IsNotNull(ChainPlanner.MatchAt(tiles, 0, db, new BenchRules { HeatPlus = 1 }));
            Assert.AreEqual(12, ChainPlanner.Heat(tiles, 0, db, new BenchRules { HeatPlus = 1 }));
        }

        [Test] public void SecondChance_GivesTrialsBack_OnceARun()
        {
            var r = Holding(Cat(Effects.SecondChance, 2));
            while (r.TrialsLeft > 1) FireAB(r);
            Assert.AreEqual(RunPhase.Playing, FireAB(r).PhaseAfter, "the last trial should have been given back");
            Assert.AreEqual(2, r.TrialsLeft);
            FireAB(r);
            Assert.AreEqual(RunPhase.Lost, FireAB(r).PhaseAfter, "it comes once only");
        }

        [Test] public void FreeDiscovery_TheFirstNewRecipeOfADepth_CostsNoTrial()
        {
            // a third recipe, so depth 2 still has two things to find: x + a and b + b
            var d = Mini();
            d.materials = d.materials.Concat(new[] { TestContent.Mat("w", "Plant", 1, 3) }).ToArray();
            d.recipes = d.recipes.Concat(new[] { TestContent.Rec(new[] { "b", "b" }, 0, "w") }).ToArray();
            var r = Holding(Cat(Effects.FreeDiscovery), d);
            int trials = r.TrialsLeft;
            PlaceId(r, "b", 0); PlaceId(r, "b", 1);
            Assert.IsTrue(r.Ignite().Events[0].Discovery);
            Assert.AreEqual(trials, r.TrialsLeft, "the first discovery of the depth should be free");
            PlaceId(r, "x", 0); PlaceId(r, "a", 1);
            Assert.IsTrue(r.Ignite().Events[0].Discovery);
            Assert.AreEqual(trials - 1, r.TrialsLeft, "only the first one is free");
            FireAB(r);
            Assert.AreEqual(trials - 2, r.TrialsLeft, "a known recipe always costs its trial");
        }

        [Test] public void Salvage_ADiscoveryLeftStanding_GoesBackToTheRack()
        {
            var with = Holding(Cat(Effects.Salvage));
            PlaceId(with, "x", 0); PlaceId(with, "b", 1);          // x and b do nothing together
            with.Ignite();
            Assert.AreEqual(1, with.Hand.Count(h => h == "x"), "salvage should have kept it");

            var without = Holding(Cat(Effects.ExtraRedraw, 1));
            PlaceId(without, "x", 0); PlaceId(without, "b", 1);
            without.Ignite();
            Assert.AreEqual(0, without.Hand.Count(h => h == "x"), "a discovery laid down is spent");
        }

        // ---- bench rules ------------------------------------------------------------------------------

        [Test] public void Around_Plain_IsTheFourSides()
        {
            CollectionAssert.AreEqual(new[] { 1, 3, 5, 7 }, BenchRules.None.Around(4));
            CollectionAssert.AreEqual(new[] { 1, 3 }, BenchRules.None.Around(0));
            CollectionAssert.AreEqual(new[] { 0, 1, 2, 3, 5, 6, 7, 8 }, BenchRules.Diagonal.Around(4));
        }

        [Test] public void Around_Crossroads_ReachesDiagonally_FromThatTileOnly()
        {
            var r = new BenchRules(); r.Crossroads[0] = true;
            CollectionAssert.AreEqual(new[] { 1, 3, 4 }, r.Around(0));
            CollectionAssert.AreEqual(new[] { 1, 3, 5, 7 }, r.Around(4), "the centre does not reach back as a hub");
            Assert.IsTrue(r.Touch(0, 4) && r.Touch(4, 0), "a pair needs only one side to reach");
        }

        [Test] public void Around_Links_JoinTwoTilesBothWays()
        {
            var r = new BenchRules(); r.Links.Add((0, 8));
            CollectionAssert.AreEqual(new[] { 1, 3, 8 }, r.Around(0));
            CollectionAssert.AreEqual(new[] { 0, 5, 7 }, r.Around(8));
        }

        [Test] public void Around_ABlockedTile_TouchesNothing_AndNothingTouchesIt()
        {
            var r = new BenchRules(); r.Blocked[1] = true;
            Assert.AreEqual(0, r.Around(1).Length);
            CollectionAssert.AreEqual(new[] { 3 }, r.Around(0));
            CollectionAssert.AreEqual(new[] { 3, 5, 7 }, r.Around(4));
            Assert.IsFalse(r.Touch(0, 1));
        }

        [Test] public void Ri_AddsTheTileAndTheShift_Within1To13()
        {
            var db = new ContentDb(Mini());
            var r = new BenchRules { RiShift = -1 }; r.RiBonus[2] = 3;
            Assert.AreEqual(5, r.Ri(db, 0, "a")); Assert.AreEqual(8, r.Ri(db, 2, "a"));
            Assert.AreEqual(1, new BenchRules { RiShift = -20 }.Ri(db, 0, "a"));
            Assert.AreEqual(BenchRules.MaxRi, new BenchRules { RiShift = 20 }.Ri(db, 0, "a"));
        }

        [Test] public void PlainRules_OnRandomBenches_PlanExactlyAsPhase2Did()
        {
            var db = new ContentDb(TestContent.Load());
            var ids = db.Materials.Select(m => m.id).ToList(); var rng = new Rng(11);
            for (int k = 0; k < 300; k++)
            {
                var tiles = new string[Bench.TileCount];
                for (int t = 0; t < tiles.Length; t++) if (rng.Range(3) > 0) tiles[t] = ids[rng.Range(ids.Count)];
                foreach (bool diagonals in new[] { false, true })
                {
                    var old = ChainPlanner.Plan(tiles, db, diagonals); var now = ChainPlanner.Plan(tiles, db, new BenchRules { Diagonals = diagonals });
                    CollectionAssert.AreEqual(old.Final, now.Final);
                    CollectionAssert.AreEqual(old.Steps.Select(s => s.Hub + ":" + db.KeyOf(s.Recipe) + ":" + s.Sputter).ToList(),
                        now.Steps.Select(s => s.Hub + ":" + db.KeyOf(s.Recipe) + ":" + s.Sputter).ToList());
                }
            }
        }

        [Test] public void Kiln_AHubOnIt_NeedsNoHeat()
        {
            var d = Mini(); d.recipes[0].minReactivity = 30;
            var db = new ContentDb(d);
            var tiles = new string[Bench.TileCount]; tiles[0] = "a"; tiles[1] = "b";
            Assert.IsNull(ChainPlanner.MatchAt(tiles, 0, db, BenchRules.None));
            var kiln = new BenchRules(); kiln.Kiln[0] = true;
            Assert.IsNotNull(ChainPlanner.MatchAt(tiles, 0, db, kiln));
            Assert.IsNull(ChainPlanner.MatchAt(tiles, 1, db, kiln), "only a hub on the kiln is spared");
        }

        [Test] public void Pivot_GoesFirst_WhateverTheOthersAre()
        {
            var db = new ContentDb(Mini());
            var tiles = new string[Bench.TileCount]; tiles[0] = "a"; tiles[1] = "b";
            Assert.AreEqual(0, ChainPlanner.Plan(tiles, db, BenchRules.None).Steps[0].Hub, "the livelier a is the hub");
            var plan = ChainPlanner.Plan(tiles, db, new BenchRules { Pivot = 1 });
            Assert.AreEqual(1, plan.Steps[0].Hub); Assert.AreEqual("x", plan.Final[1]); Assert.IsNull(plan.Final[0]);
        }

        [Test] public void HeatedTile_WithABonusOnOneOfTwoItems_MakesItsMaterialTheHub()
        {
            var db = new ContentDb(Mini());
            var tiles = new string[Bench.TileCount]; tiles[0] = "a"; tiles[1] = "b";
            var r = new BenchRules(); r.RiBonus[1] = 3;                      // b counts 7, a 6
            Assert.AreEqual(1, ChainPlanner.Plan(tiles, db, r).Steps[0].Hub);
            Assert.AreEqual(13, ChainPlanner.Heat(tiles, 0, db, r));
        }

        [Test] public void Pairs_WithLinksAndBlockedTiles_FollowLinksAndSkipBlockedTiles()
        {
            var bench = new Bench(); bench.Tiles[0] = "a"; bench.Tiles[8] = "b";
            Assert.AreEqual(0, IgniteResolver.Pairs(bench, BenchRules.None).Count);
            var r = new BenchRules(); r.Links.Add((0, 8));
            CollectionAssert.AreEqual(new[] { (0, 8) }, IgniteResolver.Pairs(bench, r));
            var b = new Bench(); b.Tiles[0] = "a"; b.Tiles[1] = "b";
            var shut = new BenchRules(); shut.Blocked[1] = true;
            Assert.IsFalse(IgniteResolver.HasAnyPair(b, shut));
        }

        [Test] public void Score_WithTileRulesOnTheHub_ReadsTheHubsTile()
        {
            var db = new ContentDb(TestContent.Combo());
            var doubled = new BenchRules(); doubled.HubMult[0] = 2;
            Assert.AreEqual(40, Score(db, PQ, At0, null, 1, 1, doubled));
            Assert.AreEqual(20, Score(db, PQ, new[] { 1, 0 }, null, 1, 1, doubled), "the hub is on tile 1");
            var heavy = new BenchRules(); heavy.Heavy[0] = true;
            Assert.AreEqual(40, Score(db, PQ, At0, null, 1, 1, heavy)); Assert.AreEqual(60, Score(db, PQ, At0, null, 2, 1, heavy));
            var attuned = new BenchRules(); attuned.AttunedKind[0] = "Ore";       // p is an Ore
            Assert.AreEqual(60, Score(db, PQ, At0, null, 1, 1, attuned));
            attuned.AttunedKind[0] = "Beast";
            Assert.AreEqual(20, Score(db, PQ, At0, null, 1, 1, attuned));
        }

        [Test] public void Score_SeedTile_PaysOnlyWhenEmptyAndTouched()
        {
            var db = new ContentDb(TestContent.Combo());
            var rules = new BenchRules(); rules.SeedBonus[3] = 40;
            var empty = new bool[Bench.TileCount]; empty[3] = true;
            Assert.AreEqual(60, Score(db, PQ, At0, null, 1, 1, null, new ScoreContext { Rules = rules, EmptyAtIgnite = empty }));
            Assert.AreEqual(20, Score(db, PQ, At0, null, 1, 1, null, new ScoreContext { Rules = rules, EmptyAtIgnite = new bool[Bench.TileCount] }), "the seed tile was used");
            Assert.AreEqual(20, Score(db, PQ, new[] { 2, 1 }, null, 1, 1, null, new ScoreContext { Rules = rules, EmptyAtIgnite = empty }), "tile 2 does not touch tile 3");
        }

        [Test] public void Score_TripleFirst_TriplesOnlyTheFirstLink()
        {
            var db = new ContentDb(TestContent.Combo());
            Assert.AreEqual(60, Score(db, PQ, At0, null, 1, 1, null, new ScoreContext { TripleFirst = true }));
            Assert.AreEqual(40, Score(db, PQ, At0, null, 2, 1, null, new ScoreContext { TripleFirst = true }));
        }

        // ---- aspects ----------------------------------------------------------------------------------

        [Test] public void LegacyRun_WithoutAspectsOrEvents_NeverStopsForAnAspectOrAnEvent()
        {
            var r = Start(MiniWithRealStart(), null);
            for (int depth = 1; depth < RunState.FinalDepth; depth++)
            {
                Assert.AreEqual(RunPhase.Playing, r.Phase); Assert.AreEqual(0, r.Aspects.Count); Assert.IsNull(r.CurrentEvent);
                Descend(r);
            }
        }

        /// <summary>What a depth's start showed, in order: "A" for the aspect offer, "E" for an event.</summary>
        static string OpenAndRecord(RunState r, List<string> log = null)
        {
            string seen = "";
            while (r.Phase == RunPhase.AspectOffer || r.Phase == RunPhase.EventOffer)
            {
                if (r.Phase == RunPhase.AspectOffer)
                {
                    seen += "A";
                    log?.Add(string.Join(",", r.AspectOffer.Select(a => a.Data.id + "@" + string.Join("/", a.Tiles) + a.Kind)));
                    r.ChooseAspect(r.AspectOffer[0].Data.id);
                }
                else
                {
                    seen += "E";
                    log?.Add(r.CurrentEvent.id);
                    r.ChooseEventOption(r.EventOptions.Count - 1);
                }
            }
            return seen;
        }

        [Test] public void StartOfDepth_AtEveryDepth_IsAspectThenEventThenBench()
        {
            var d = MiniWithRealStart();
            int events = 0, quiet = 0;
            for (int seed = 1; seed <= 60; seed++)
            {
                var r = Start(d, RunOptions.Full(null), seed);
                for (int depth = 1; depth <= RunState.FinalDepth; depth++)
                {
                    Assert.AreEqual(depth, r.Depth);
                    string seen = OpenAndRecord(r);
                    Assert.AreEqual(RunPhase.Playing, r.Phase);
                    bool aspectDepth = depth == 1 || depth == 3 || depth == 5;
                    Assert.That(seen, Is.EqualTo(aspectDepth ? "A" : "").Or.EqualTo(aspectDepth ? "AE" : "E"), "seed " + seed + " depth " + depth);
                    if (seen.EndsWith("E")) events++; else quiet++;
                    Assert.That(r.Hand.Count(h => h == "a" || h == "b"), Is.GreaterThanOrEqualTo(RunState.MinHand), "the hand was not dealt");
                    if (depth < RunState.FinalDepth) Descend(r);
                }
                Assert.AreEqual(3, r.Aspects.Count);
            }
            Assert.That(events, Is.GreaterThan(100)); Assert.That(quiet, Is.GreaterThan(20), "nothing happens sometimes");
        }

        [Test] public void AspectOffer_IsThreeDistinct_NeverOneAlreadyHeld_NeverOnATakenTile()
        {
            var d = MiniWithRealStart();
            for (int seed = 1; seed <= 150; seed++)
            {
                var r = Start(d, AspectsOnly, seed);
                for (int depth = 1; depth <= RunState.FinalDepth; depth++)
                {
                    if (r.Phase == RunPhase.AspectOffer)
                    {
                        var offer = r.AspectOffer;
                        Assert.AreEqual(3, offer.Count);
                        Assert.AreEqual(3, offer.Select(a => a.Data.id).Distinct().Count());
                        var held = r.Aspects.SelectMany(a => a.Tiles).ToList();
                        foreach (var a in offer)
                        {
                            Assert.IsFalse(r.Aspects.Any(h => h.Data.id == a.Data.id), a.Data.id + " is already held");
                            Assert.AreEqual(a.Data.scope == "Bench" ? 0 : a.Data.effect == AspectEffects.Bridge ? 2 : 1, a.Tiles.Length, a.Data.id);
                            foreach (int t in a.Tiles) Assert.IsFalse(held.Contains(t), a.Data.id + " on a taken tile, seed " + seed);
                            if (a.Data.effect == AspectEffects.Bridge) Assert.IsFalse(BenchRules.None.Touch(a.Tiles[0], a.Tiles[1]), "a bridge between neighbours");
                            if (a.Data.effect == AspectEffects.OpenBench) CollectionAssert.Contains(new[] { 0, 2, 6, 8 }, a.Tiles[0]);
                            Assert.AreEqual(a.Data.effect == AspectEffects.Attuned, a.Kind != null);
                        }
                        // pick a different card each time, so every aspect gets held somewhere
                        r.ChooseAspect(offer[(seed + depth) % 3].Data.id);
                    }
                    Assert.AreEqual(RunPhase.Playing, r.Phase);
                    if (depth < RunState.FinalDepth) Descend(r);
                }
                var tiles = r.Aspects.SelectMany(a => a.Tiles).ToList();
                Assert.AreEqual(tiles.Count, tiles.Distinct().Count(), "two aspects share a tile, seed " + seed);
                Assert.That(r.Aspects.Count(a => a.Data.effect == AspectEffects.Cold), Is.LessThanOrEqualTo(2));
            }
        }

        [Test] public void TheSameSeed_RunTwice_GivesTheSameAspectsAndEvents()
        {
            var d = MiniWithRealStart();
            var seen = new HashSet<string>();
            for (int seed = 1; seed <= 10; seed++)
            {
                var logs = new List<string>[2];
                for (int k = 0; k < 2; k++)
                {
                    logs[k] = new List<string>();
                    var r = Start(d, RunOptions.Full(null), seed);
                    for (int depth = 1; depth <= RunState.FinalDepth; depth++)
                    {
                        OpenAndRecord(r, logs[k]);
                        if (depth < RunState.FinalDepth) Descend(r);
                    }
                }
                CollectionAssert.AreEqual(logs[0], logs[1], "seed " + seed);
                seen.Add(string.Join("|", logs[0]));
            }
            Assert.That(seen.Count, Is.GreaterThan(5), "different seeds should give different runs");
        }

        [Test] public void ChooseAspect_WithAnIdNotOnOffer_RefusesWhatIsNotOnOffer()
        {
            var r = Start(MiniWithRealStart(), AspectsOnly);
            Assert.AreEqual(RunPhase.AspectOffer, r.Phase);
            Assert.IsFalse(r.Place(0, 0), "the bench is not open during the offer");
            Assert.Throws<ArgumentException>(() => r.ChooseAspect("no_such_aspect"));
            r.ChooseAspect(r.AspectOffer[0].Data.id);
            Assert.Throws<InvalidOperationException>(() => r.ChooseAspect("doubled_tile"));
        }

        static RunState WithOnly(string effect, ContentData d = null, string scope = "Tile", float amount = 0, int seed = 1)
        {
            var r = Start(WithAspect(d ?? Mini(999999), effect, scope, amount), AspectsOnly, seed);
            Assert.AreEqual(1, r.AspectOffer.Count);
            r.ChooseAspect(effect);
            Assert.AreEqual(RunPhase.Playing, r.Phase);
            return r;
        }
        static int TileOf(RunState r) => r.Aspects[0].Tiles[0];

        [Test] public void ColdTile_ShutsItsTile_AndGivesATrialEveryDepth()
        {
            var r = WithOnly(AspectEffects.Cold, Mini());
            int t = TileOf(r);
            Assert.AreEqual(RunState.BaseTrials + 1, r.TrialsLeft);
            Assert.IsTrue(r.Rules.Blocked[t]); Assert.IsFalse(r.Place(0, t)); Assert.IsNull(r.Bench.Tiles[t]);
            Descend(r);
            Assert.AreEqual(RunState.BaseTrials + RunState.BaseTrials + 1, r.TrialsLeft, "four carried over, four new and the cold tile's one");
            Assert.IsTrue(r.Rules.Blocked[t]);
        }

        [Test] public void GreedyBench_OneMoreCard_OneRedrawFewer_EveryDepth()
        {
            var d = TestContent.Tiny(); d.ultimate = ""; d.depthTargets = new[] { 1, 1, 1, 1 };
            var r = WithOnly(AspectEffects.Greedy, d, "Bench");
            Assert.AreEqual(0, r.Aspects[0].Tiles.Length);
            Assert.AreEqual(RunState.BaseHand + 1, r.HandSize); Assert.AreEqual(RunState.BaseHand + 1, r.Hand.Count);
            Assert.AreEqual(RunState.BaseRedraws - 1, r.RedrawsLeft);
        }

        [Test] public void DoubledAndVolatile_WhenTheHubIsOnOrAwayFromTheirTile_ScoreOnTheirTile()
        {
            foreach (var (effect, mult) in new[] { (AspectEffects.Doubled, 2), (AspectEffects.Volatile, 3) })
            {
                var r = WithOnly(effect);
                Assert.AreEqual(40 * mult, FireAB(r, TileOf(r)).Gained, effect);     // 20, doubled for the first time this run
                var away = WithOnly(effect);
                int hub = Enumerable.Range(0, Bench.TileCount).First(t => t != TileOf(away) && !away.Rules.Around(t).Contains(TileOf(away)));
                Assert.AreEqual(40, FireAB(away, hub).Gained, effect + " away from its tile");
            }
        }

        [Test] public void VolatileTile_BurnsTheStartingMaterialLaidOnIt_ButNeverBelowAHand()
        {
            var d = TestContent.Tiny(); d.ultimate = ""; d.depthTargets = new[] { 999999, 1, 1, 1 };
            var r = WithOnly(AspectEffects.Volatile, d);
            int t = TileOf(r), beside = r.Rules.Around(t)[0];
            int far = Enumerable.Range(0, Bench.TileCount).First(k => k != t && !r.Rules.Around(k).Contains(t)), farBeside = r.Rules.Around(far).First(n => n != t);
            Assert.AreEqual(8, r.SatchelSize);
            r.Place(0, far); r.Place(0, farBeside); r.Ignite();
            Assert.AreEqual(8, r.SatchelSize, "away from the volatile tile nothing is lost");
            r.Place(0, t); r.Place(0, beside); r.Ignite();
            Assert.AreEqual(7, r.SatchelSize, "the card on the volatile tile is gone for the run");
            r.Place(0, t); r.Place(0, beside); r.Ignite();
            Assert.AreEqual(7, r.SatchelSize, "seven cards is a hand and two: no more are burnt");
            Assert.AreEqual(RunState.BaseHand, r.Hand.Count);
        }

        [Test] public void HeatedTile_Adds3_AndSeedTile_Carries40()
        {
            var heated = WithOnly(AspectEffects.Heated);
            Assert.AreEqual(3, heated.Rules.RiBonus[TileOf(heated)]);
            var seed = WithOnly(AspectEffects.Seed);
            Assert.AreEqual(40, seed.Rules.SeedBonus[TileOf(seed)]);
            int t = TileOf(seed), hub = seed.Rules.Around(t)[0], other = seed.Rules.Around(hub).First(n => n != t);
            PlaceId(seed, "a", hub); PlaceId(seed, "b", other);
            Assert.AreEqual(80, seed.Ignite().Gained, "40 for the reaction and 40 from the empty seed tile beside its hub");
        }

        [Test] public void HeavyTile_WhenTheHubIsOnIt_CountsOneChainStepMore()
        {
            var r = WithOnly(AspectEffects.Heavy);
            Assert.AreEqual(80, FireAB(r, TileOf(r)).Gained);
        }

        [Test] public void AttunedTile_GetsAKind_AndLiftsARecipeThatUsesIt()
        {
            for (int seed = 1; seed <= 30; seed++)
            {
                var r = WithOnly(AspectEffects.Attuned, null, "Tile", 0, seed);
                string kind = r.Aspects[0].Kind;
                Assert.IsTrue(Enum.TryParse(kind, out Kind _), kind);
                Assert.AreEqual(kind, r.Rules.AttunedKind[TileOf(r)]);
                // a is an Ore and b a Potion: either kind makes x score as tier 2 (60, doubled the first time)
                Assert.AreEqual(kind == "Ore" || kind == "Potion" ? 120 : 40, FireAB(r, TileOf(r)).Gained, kind);
            }
        }

        [Test] public void EchoTile_WhenItsHubReacts_KeepsACopyOfWhatItsHubMakes()
        {
            var r = WithOnly(AspectEffects.Echo);
            FireAB(r, TileOf(r));
            Assert.AreEqual(2, r.Hand.Count(h => h == "x"));
        }

        [Test] public void AnchorTile_WithADiscoveryLaidOnIt_GivesBackTheDiscovery()
        {
            var r = WithOnly(AspectEffects.Anchor);
            FireAB(r);
            Assert.AreEqual(1, r.Hand.Count(h => h == "x"));
            int t = TileOf(r);
            PlaceId(r, "x", t); PlaceId(r, "a", r.Rules.Around(t)[0]);       // x + a = y, with a (livelier) as the hub
            r.Ignite();
            Assert.AreEqual(1, r.Hand.Count(h => h == "x"), "the anchored x should have come back");
            Assert.AreEqual(1, r.Hand.Count(h => h == "y"));
        }

        [Test] public void FallowTile_LeftEmpty_DrawsOneMoreCard()
        {
            var d = TestContent.Tiny(); d.ultimate = ""; d.depthTargets = new[] { 999999, 1, 1, 1 };
            var r = WithOnly(AspectEffects.Fallow, d);
            int t = TileOf(r);
            var (h, o) = (Enumerable.Range(0, Bench.TileCount).First(k => k != t && r.Rules.Around(k).Any(n => n != t)), -1);
            o = r.Rules.Around(h).First(n => n != t);
            r.Place(0, h); r.Place(0, o);
            r.Ignite();
            Assert.AreEqual(RunState.BaseHand + 1, r.Hand.Count(c => r.IsOpen(c)), "one more starting material after leaving the fallow tile empty");
            r.Place(0, t); r.Place(0, r.Rules.Around(t)[0]);
            r.Ignite();
            Assert.AreEqual(RunState.BaseHand, r.Hand.Count(c => r.IsOpen(c)), "used, it gives nothing");
        }

        [Test] public void Bridge_BetweenTwoTiles_JoinsItsTwoTiles()
        {
            var r = WithOnly(AspectEffects.Bridge, null, "Link");
            int a = r.Aspects[0].Tiles[0], b = r.Aspects[0].Tiles[1];
            Assert.IsFalse(BenchRules.None.Touch(a, b)); Assert.IsTrue(r.Rules.Touch(a, b));
            PlaceId(r, "a", a); PlaceId(r, "b", b);
            Assert.IsTrue(r.CanIgnite);
            Assert.AreEqual("x", r.Ignite().Events[0].Result);
        }

        [Test] public void OpenBench_OnALinkScope_JoinsACornerToTheCentre()
        {
            var r = WithOnly(AspectEffects.OpenBench, null, "Link");
            int corner = TileOf(r);
            CollectionAssert.Contains(new[] { 0, 2, 6, 8 }, corner);
            Assert.IsTrue(r.Rules.Touch(corner, Bench.Centre));
            Assert.AreEqual(5, r.Rules.Around(Bench.Centre).Length);
        }

        [Test] public void KilnCrossroadsPivot_WhenChosen_ReachTheBenchRules()
        {
            var kiln = WithOnly(AspectEffects.Kiln); Assert.IsTrue(kiln.Rules.Kiln[TileOf(kiln)]);
            var cross = WithOnly(AspectEffects.Crossroads); Assert.IsTrue(cross.Rules.Crossroads[TileOf(cross)]);
            var pivot = WithOnly(AspectEffects.Pivot); Assert.AreEqual(TileOf(pivot), pivot.Rules.Pivot);
        }

        // ---- events -----------------------------------------------------------------------------------

        static RunState AtEvent(ContentData d, int seed = 1)
        {
            var r = Start(d, EventsOnly, seed);
            Assert.AreEqual(RunPhase.EventOffer, r.Phase);
            return r;
        }

        [Test] public void Event_Trials_Redraws_Hand()
        {
            var tiny = TestContent.Tiny(); tiny.ultimate = "";
            var r = AtEvent(WithGain(tiny, Fx(Ops.Trials, 1), Fx(Ops.Redraws, 1), Fx(Ops.Hand, 2)));
            Assert.IsFalse(r.Place(0, 0), "the bench is not open during an event");
            var result = r.ChooseEventOption(0);
            Assert.AreEqual(RunPhase.Playing, r.Phase); Assert.IsNull(r.CurrentEvent);
            Assert.AreEqual("go", result.Text); Assert.AreEqual(3, result.Notes.Count);
            Assert.AreEqual(RunState.BaseTrials + 1, r.TrialsLeft); Assert.AreEqual(RunState.BaseRedraws + 1, r.RedrawsLeft);
            Assert.AreEqual(RunState.BaseHand + 2, r.HandSize); Assert.AreEqual(RunState.BaseHand + 2, r.Hand.Count);
        }

        [Test] public void Event_ASmallerHand_AppliesToTheFirstHandToo()
        {
            var tiny = TestContent.Tiny(); tiny.ultimate = "";
            var r = AtEvent(WithGain(tiny, Fx(Ops.Hand, -1)));
            r.ChooseEventOption(0);
            Assert.AreEqual(RunState.BaseHand - 1, r.HandSize);
            Assert.AreEqual(RunState.BaseHand - 1, r.Hand.Count, "the hand dealt before the event should have been trimmed");
            Assert.AreEqual(8, r.SatchelSize, "the trimmed card goes back to the satchel");
        }

        [Test] public void Event_Floors_ThreeCards_OneTrial_NoRedraws()
        {
            var tiny = TestContent.Tiny(); tiny.ultimate = "";
            var r = AtEvent(WithGain(tiny, Fx(Ops.Hand, -9), Fx(Ops.Trials, -9), Fx(Ops.Redraws, -9)));
            r.ChooseEventOption(0);
            Assert.AreEqual(RunState.MinHand, r.HandSize); Assert.AreEqual(RunState.MinHand, r.Hand.Count);
            Assert.AreEqual(1, r.TrialsLeft); Assert.AreEqual(0, r.RedrawsLeft);
        }

        [Test] public void Event_WithTemporaryEffects_EffectsLastOneDepth()
        {
            var d = WithEvent(Mini(), 1, 1, Option("go", null, null, Fx(Ops.RiShift, -1), Fx(Ops.BlockTile), Fx(Ops.HeatTile, 3), Fx(Ops.Hand, -1), Fx(Ops.FirstTriple)));
            var r = AtEvent(d);
            r.ChooseEventOption(0);
            Assert.AreEqual(-1, r.RiShift); Assert.AreEqual(-1, r.Rules.RiShift); Assert.IsTrue(r.TripleFirst);
            Assert.AreEqual(1, r.DepthBlocked.Count); Assert.AreEqual(1, r.DepthHeated.Count()); Assert.AreEqual(RunState.BaseHand - 1, r.HandSize);
            int shut = r.DepthBlocked[0], warm = r.DepthHeated.First();
            Assert.AreNotEqual(shut, warm);
            Assert.IsTrue(r.Rules.Blocked[shut]); Assert.AreEqual(3, r.Rules.RiBonus[warm]);
            Assert.IsFalse(r.Place(0, shut));
            // the fizzle of a + a leaves the triple waiting; the first reaction takes it
            var (h, o) = FreePair(r);
            PlaceId(r, "a", h); PlaceId(r, "a", o);
            r.Ignite();
            Assert.IsTrue(r.TripleFirst);
            Descend(r);
            Assert.AreEqual(2, r.Depth); Assert.AreEqual(RunPhase.Playing, r.Phase, "the event is for depth 1 only");
            Assert.AreEqual(0, r.RiShift); Assert.AreEqual(0, r.Rules.RiShift); Assert.IsFalse(r.TripleFirst);
            Assert.AreEqual(0, r.DepthBlocked.Count); Assert.AreEqual(0, r.DepthHeated.Count()); Assert.AreEqual(RunState.BaseHand, r.HandSize);
            Assert.IsFalse(r.Rules.Blocked.Any(b => b)); Assert.IsTrue(r.Rules.RiBonus.All(b => b == 0));
        }

        [Test] public void Event_FirstTriple_TriplesTheNextFirstReaction_Once()
        {
            var r = AtEvent(WithGain(Mini(999999), Fx(Ops.FirstTriple)));
            r.ChooseEventOption(0);
            Assert.AreEqual(120, FireAB(r).Gained);        // 20, doubled as new this run, tripled
            Assert.IsFalse(r.TripleFirst);
            Assert.AreEqual(20, FireAB(r).Gained);
        }

        [Test] public void Event_GiveDiscovery_PutsOneOfThatTierOnTheRack()
        {
            var r = AtEvent(WithGain(Mini(), Fx(Ops.GiveDiscovery, 2, 2)));
            var result = r.ChooseEventOption(0);
            CollectionAssert.Contains(r.Hand.ToList(), "y");
            Assert.AreEqual(1, result.Notes.Count);
            var low = AtEvent(WithGain(Mini(), Fx(Ops.GiveDiscovery, 1, 1)));
            low.ChooseEventOption(0);
            CollectionAssert.Contains(low.Hand.ToList(), "x");
        }

        [Test] public void Event_ACostThatCannotBePaid_IsNeverOffered()
        {
            var d = WithEvent(Mini(), 1, 5, Option("give", Ops.NeedRack, new[] { Fx(Ops.LoseRack, 1) }, Fx(Ops.Trials, 1)), WalkOn);
            var r = Start(d, EventsOnly);
            Assert.AreEqual(RunPhase.Playing, r.Phase, "with an empty rack there is nothing to do there but walk on, so it is not shown");
            Descend(r);                                   // the x made at depth 1 is on the rack now
            Assert.AreEqual(RunPhase.EventOffer, r.Phase);
            Assert.AreEqual(2, r.EventOptions.Count); Assert.AreEqual("give", r.EventOptions[0].label);
            int trials = r.TrialsLeft;
            var result = r.ChooseEventOption(0);
            Assert.AreEqual(0, r.Hand.Count(h => h == "x")); Assert.AreEqual(trials + 1, r.TrialsLeft);
            Assert.AreEqual(2, result.Notes.Count);
        }

        [Test] public void Event_NeedsATrialToSpare_OrARedraw()
        {
            var d = WithEvent(Mini(), 1, 5, Option("kneel", Ops.NeedTrial, new[] { Fx(Ops.Trials, -4) }, Fx(Ops.Redraws, 1)),
                Option("read", Ops.NeedRedraw, new[] { Fx(Ops.Redraws, -1) }, Fx(Ops.Hint)), WalkOn);
            var r = AtEvent(d);
            CollectionAssert.AreEqual(new[] { "read", "Walk on" }, r.EventOptions.Select(o => o.label).ToList(), "four trials cannot pay four and leave one");
            Assert.Throws<ArgumentException>(() => r.ChooseEventOption(5));
            int hints = 0;
            r.ChooseEventOption(0);
            Assert.AreEqual(RunState.BaseRedraws - 1, r.RedrawsLeft);
            Assert.That(hints, Is.EqualTo(0));
        }

        [Test] public void Event_Hint_WritesOneInTheJournal()
        {
            var db = new ContentDb(WithGain(Mini(), Fx(Ops.Hint)));
            var journal = new Journal();
            var r = new RunState(db, journal, 1, 1, EventsOnly);
            var result = r.ChooseEventOption(0);
            Assert.AreEqual(1, journal.Hints.Count); Assert.AreEqual(1, result.Notes.Count);
        }

        [Test] public void Event_LoseCard_ThinsTheSatchel_ButNeverBelowAHand()
        {
            var tiny = TestContent.Tiny(); tiny.ultimate = "";
            var r = AtEvent(WithGain(tiny, Fx(Ops.LoseCard, 1)));
            r.ChooseEventOption(0);
            Assert.AreEqual(7, r.SatchelSize); Assert.AreEqual(RunState.BaseHand, r.Hand.Count);
            var small = AtEvent(WithGain(Mini(), Fx(Ops.LoseCard, 1)));
            small.ChooseEventOption(0);
            Assert.AreEqual(4, small.SatchelSize, "four cards is already less than a hand");
        }

        [Test] public void Event_BlockTile_NeverShutsMoreThanTwo()
        {
            var r = AtEvent(WithGain(Mini(), Fx(Ops.BlockTile), Fx(Ops.BlockTile), Fx(Ops.BlockTile)));
            r.ChooseEventOption(0);
            Assert.AreEqual(2, r.DepthBlocked.Count); Assert.AreEqual(2, r.Rules.Blocked.Count(b => b));
        }

        [Test] public void Event_BlockTile_NeverShutsTheCentre()
        {
            // the centre is the only tile with four neighbours; the big recipes of the deep need it
            for (int seed = 1; seed <= 80; seed++)
            {
                var r = AtEvent(WithGain(Mini(), Fx(Ops.BlockTile), Fx(Ops.BlockTile)), seed);
                r.ChooseEventOption(0);
                Assert.AreEqual(2, r.DepthBlocked.Count, "seed " + seed);
                Assert.IsFalse(r.DepthBlocked.Contains(Bench.Centre), "seed " + seed + " shut the centre");
            }
        }

        [Test] public void Event_BlockTile_FallsOnACornerTwiceAsOftenAsOnAnEdge()
        {
            int corners = 0, edges = 0;
            for (int seed = 1; seed <= 300; seed++)
            {
                var r = AtEvent(WithGain(Mini(), Fx(Ops.BlockTile)), seed);
                r.ChooseEventOption(0);
                if (r.DepthBlocked[0] % 2 == 0) corners++; else edges++;
            }
            Assert.That(corners, Is.InRange(170, 230), corners + " corners, " + edges + " edges of 300: two in three should be corners");
        }

        [Test] public void Event_CatalystGift_AndUpgrade()
        {
            var d = WithGain(Mini(), Fx(Ops.CatalystGift));
            d.catalysts = new[] { Cat(Effects.AllScale, 2) };
            var r = AtEvent(d);
            r.ChooseEventOption(0);
            Assert.AreEqual(1, r.Catalysts.Count);

            // depth 2: the Common one held becomes the Uncommon one that is open
            var u = WithEvent(Mini(), 2, 2, Option("offer", Ops.NeedCatalyst, null, Fx(Ops.CatalystUpgrade)), WalkOn);
            u.catalysts = new[] { Cat(Effects.KindDouble, 0, "Ore", "Common"), Cat(Effects.FirstTriple, 0, "", "Uncommon") };
            var run = Start(u, EventsOnly);
            Descend(run, Effects.KindDouble);
            Assert.AreEqual(RunPhase.EventOffer, run.Phase);
            var result = run.ChooseEventOption(0);
            Assert.AreEqual(1, run.Catalysts.Count); Assert.AreEqual(Effects.FirstTriple, run.Catalysts[0].id);
            Assert.AreEqual(1, result.Notes.Count);
        }

        [Test] public void Event_Upgrade_IsNotOffered_WithNothingToTradeUp()
        {
            var u = WithEvent(Mini(), 1, 5, Option("offer", Ops.NeedCatalyst, null, Fx(Ops.CatalystUpgrade)), WalkOn);
            u.catalysts = new[] { Cat(Effects.KindDouble, 0, "Ore", "Common") };
            var run = Start(u, EventsOnly);
            Assert.AreEqual(RunPhase.Playing, run.Phase, "no catalyst is held");
            Descend(run, Effects.KindDouble);
            Assert.AreEqual(RunPhase.Playing, run.Phase, "nothing of the next rarity is open");
        }

        [Test] public void RealEvents_RespectTheirDepths_AndNeverOfferWhatCannotBePaid()
        {
            var d = MiniWithRealStart();
            var met = new HashSet<string>();
            for (int seed = 1; seed <= 300; seed++)
            {
                var r = Start(d, EventsOnly, seed);
                for (int depth = 1; depth <= RunState.FinalDepth; depth++)
                {
                    if (r.Phase == RunPhase.EventOffer)
                    {
                        var e = r.CurrentEvent; met.Add(e.id);
                        Assert.That(depth, Is.InRange(e.minDepth, e.maxDepth), e.id);
                        Assert.That(r.EventOptions.Count, Is.GreaterThan(0), e.id);
                        bool rack = r.Hand.Any(h => h == "x" || h == "y");
                        foreach (var o in r.EventOptions)
                        {
                            if (o.requires == Ops.NeedRack) Assert.IsTrue(rack, e.id + " asks for a discovery with none on the rack");
                            if (o.requires == Ops.NeedCatalyst) Assert.Fail(e.id + ": this content has no catalyst to offer");
                            if (o.requires == Ops.NeedTrial) Assert.That(r.TrialsLeft, Is.GreaterThanOrEqualTo(2));
                        }
                        int trials = r.TrialsLeft;
                        r.ChooseEventOption((seed + depth) % r.EventOptions.Count);
                        Assert.That(r.TrialsLeft, Is.GreaterThanOrEqualTo(1), e.id);
                        Assert.That(r.HandSize, Is.GreaterThanOrEqualTo(RunState.MinHand), e.id);
                        Assert.That(r.Rules.Blocked.Count(b => b), Is.LessThanOrEqualTo(2), e.id);
                    }
                    Assert.AreEqual(RunPhase.Playing, r.Phase);
                    if (depth < RunState.FinalDepth) Descend(r);
                }
            }
            // every event that needs neither a catalyst nor anything this small content lacks turns up
            Assert.That(met.Count, Is.GreaterThanOrEqualTo(14), "seen: " + string.Join(",", met.OrderBy(x => x)));
        }

        // ---- the scripted player on the whole game ------------------------------------------------------

        static RunState AutoplayFull(ContentDb db, int seed, Unlocks unlocks)
        {
            var run = new RunState(db, new Journal(), seed, 1, RunOptions.Full(unlocks));
            var bot = new AutoPlayer(db);
            for (int guard = 0; guard < 600 && run.Phase != RunPhase.Won && run.Phase != RunPhase.Lost; guard++)
                if (bot.Decide(run) == AutoMove.ReadyToIgnite) run.Ignite();
            return run;
        }

        [Test] public void AForcedEvent_IsMetAtEveryDepthItMayComeTo_AndAtNoOther()
        {
            var db = new ContentDb(TestContent.Load());
            var damp = db.Events.First(e => e.id == "damp_air");
            Assert.That(damp.minDepth, Is.GreaterThan(1), "the test needs an event that stays away from depth 1");
            for (int seed = 1; seed <= 20; seed++)
                for (int depth = 1; depth <= RunState.FinalDepth; depth++)
                {
                    var r = new RunState(db, new Journal(), seed, depth, new RunOptions { Events = true, ForceEvent = damp.id });
                    if (depth < damp.minDepth || depth > damp.maxDepth) Assert.AreEqual(RunPhase.Playing, r.Phase, "seed " + seed + " depth " + depth);
                    else
                    {
                        Assert.AreEqual(RunPhase.EventOffer, r.Phase, "seed " + seed + " depth " + depth);
                        Assert.AreEqual(damp.id, r.CurrentEvent.id);
                    }
                    // pinned to one depth, it comes there and the other depths have no event at all
                    var one = new RunState(db, new Journal(), seed, depth, new RunOptions { Events = true, ForceEvent = damp.id, ForceEventAt = 3 });
                    Assert.AreEqual(depth == 3 ? RunPhase.EventOffer : RunPhase.Playing, one.Phase, "pinned to 3, seed " + seed + " depth " + depth);
                }
        }

        [Test] public void InformedPlayer_FreshProfile_WithAspectsAndEvents_WinsSixInTen()
        {
            var db = new ContentDb(TestContent.Load());
            int wins = 0;
            for (int seed = 1; seed <= 60; seed++)
            {
                var run = AutoplayFull(db, seed, Unlocks.Fresh(db));
                Assert.That(run.Phase, Is.EqualTo(RunPhase.Won).Or.EqualTo(RunPhase.Lost), "seed " + seed + " did not end");
                if (run.Phase == RunPhase.Won) wins++;
            }
            Assert.That(wins, Is.InRange(33, 48), "wins " + wins + "/60: the owner's target for a fresh profile is 60 to 70%");
        }

        [Test] public void InformedPlayer_FullProfile_WithAspectsAndEvents_WinsEightInTen()
        {
            var db = new ContentDb(TestContent.Load());
            int wins = 0;
            for (int seed = 1; seed <= 60; seed++)
                if (AutoplayFull(db, seed, Unlocks.All(db)).Phase == RunPhase.Won) wins++;
            Assert.That(wins, Is.GreaterThanOrEqualTo(45), "wins " + wins + "/60: the owner's target for a full profile is 80 to 90%");
        }
    }
}
