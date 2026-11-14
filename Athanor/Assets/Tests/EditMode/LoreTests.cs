using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>Lore events: the last one seen is not drawn again until another Lore event has been seen.</summary>
    public class LoreTests
    {
        static EventData Ev(string id, string type, int weight = 1, int minDepth = 1, int maxDepth = 5) => new EventData
        {
            id = id, name = id, text = id, type = type, minDepth = minDepth, maxDepth = maxDepth, weight = weight,
            options = new[]
            {
                new EventOption
                {
                    label = "Go on", requires = "", cost = new EffectData[0],
                    outcomes = new[] { new EventOutcome { weight = 1, text = id, gain = new[] { new EffectData { op = Ops.Nothing } } } },
                },
            },
        };

        /// <summary>a+b = x; targets of 1, so one reaction clears a depth; no catalysts, so every offer is empty; no "no event" weight.</summary>
        static ContentDb Mini(params EventData[] events) => new ContentDb(new ContentData
        {
            materials = new[] { TestContent.Mat("a", "Ore", 0, 6), TestContent.Mat("b", "Potion", 0, 4), TestContent.Mat("x", "Metal", 1, 5) },
            recipes = new[] { TestContent.Rec(new[] { "a", "b" }, 0, "x") },
            catalysts = new CatalystData[0], depthTargets = new[] { 1, 1, 1, 1 }, depthNames = new[] { "1", "2", "3", "4", "5" }, ultimate = "",
            events = events, noEventWeight = 0,
        });

        static RunOptions Events(string lastLore = null) => new RunOptions { Events = true, LastLore = lastLore };

        static bool PlaceId(RunState r, string id, int tile) => r.Place(r.Hand.ToList().IndexOf(id), tile);

        /// <summary>Takes the open event (its only option), clears the depth with a+b and descends.</summary>
        static void NextDepth(RunState r)
        {
            if (r.Phase == RunPhase.EventOffer) r.ChooseEventOption(0);
            Assert.IsTrue(PlaceId(r, "a", 4), "no a in hand"); Assert.IsTrue(PlaceId(r, "b", 5), "no b in hand");
            r.Ignite();
            Assert.AreEqual(RunPhase.DepthCleared, r.Phase);
            r.ChooseCatalyst(null);
        }

        // ---- the draw -------------------------------------------------------------------------------

        [Test]
        public void TheLastLoreEvent_IsNotDrawn()
        {
            for (int seed = 1; seed <= 50; seed++)
            {
                var only = new RunState(Mini(Ev("l1", "Lore")), new Journal(), seed, 1, Events("l1"));
                Assert.IsNull(only.CurrentEvent, "seed " + seed);
                Assert.AreEqual(RunPhase.Playing, only.Phase, "seed " + seed);
                var two = new RunState(Mini(Ev("l1", "Lore"), Ev("l2", "Lore")), new Journal(), seed, 1, Events("l1"));
                Assert.AreNotEqual("l1", two.CurrentEvent?.id, "seed " + seed);
            }
        }

        [Test]
        public void WithoutALastLore_EveryLoreEventCanCome()
        {
            var seen = new HashSet<string>();
            for (int seed = 1; seed <= 50; seed++)
                seen.Add(new RunState(Mini(Ev("l1", "Lore"), Ev("l2", "Lore")), new Journal(), seed, 1, Events()).CurrentEvent?.id);
            CollectionAssert.IsSubsetOf(new[] { "l1", "l2" }, seen.ToList());
        }

        [Test]
        public void ALastLore_ThatIsNotALoreEvent_ChangesNothing()
        {
            for (int seed = 1; seed <= 50; seed++)
            {
                var a = new RunState(Mini(Ev("b1", "Boon"), Ev("l1", "Lore")), new Journal(), seed, 1, Events());
                var b = new RunState(Mini(Ev("b1", "Boon"), Ev("l1", "Lore")), new Journal(), seed, 1, Events("b1"));
                var c = new RunState(Mini(Ev("b1", "Boon"), Ev("l1", "Lore")), new Journal(), seed, 1, Events("gone"));
                Assert.AreEqual(a.CurrentEvent?.id, b.CurrentEvent?.id, "seed " + seed);
                Assert.AreEqual(a.CurrentEvent?.id, c.CurrentEvent?.id, "seed " + seed);
            }
        }

        [Test]
        public void InARun_ALoreEvent_IsNotShownTwiceInARow()
        {
            var r = new RunState(Mini(Ev("l1", "Lore")), new Journal(), 7, 1, Events());
            Assert.AreEqual("l1", r.CurrentEvent?.id, "depth 1");
            Assert.AreEqual("l1", r.LastLore);
            NextDepth(r);
            Assert.AreEqual(2, r.Depth);
            Assert.IsNull(r.CurrentEvent, "depth 2: l1 was the last Lore event");
            Assert.AreEqual(RunPhase.Playing, r.Phase);
            Assert.AreEqual("l1", r.LastLore);
        }

        [Test]
        public void AnotherLoreEvent_InBetween_LetsTheFirstComeBack()
        {
            var db = Mini(Ev("l1", "Lore"), Ev("l2", "Lore", 50, 2, 2));   // l2 only at depth 2, and likely there
            for (int seed = 1; seed <= 20; seed++)
            {
                var r = new RunState(db, new Journal(), seed, 1, Events());
                Assert.AreEqual("l1", r.CurrentEvent?.id, "depth 1 has only l1");
                NextDepth(r);
                if (r.CurrentEvent?.id != "l2") continue;
                Assert.AreEqual("l2", r.LastLore);
                NextDepth(r);
                Assert.AreEqual("l1", r.CurrentEvent?.id, "depth 3, after l2");
                return;
            }
            Assert.Fail("no seed drew l2 at depth 2");
        }

        [Test]
        public void ANonLoreEvent_InBetween_DoesNotLetItComeBack()
        {
            var db = Mini(Ev("l1", "Lore", 1, 1, 3), Ev("b1", "Boon", 50, 2, 2));   // l1 at depths 1 to 3; a heavy Boon at depth 2 only
            for (int seed = 1; seed <= 20; seed++)
            {
                var r = new RunState(db, new Journal(), seed, 1, Events());
                Assert.AreEqual("l1", r.CurrentEvent?.id, "depth 1 has only l1");
                NextDepth(r);
                if (r.CurrentEvent?.id != "b1") continue;
                Assert.AreEqual("l1", r.LastLore, "a Boon in between does not release l1");
                NextDepth(r);
                Assert.IsNull(r.CurrentEvent, "depth 3: l1 was still the last Lore event");
                Assert.AreEqual(RunPhase.Playing, r.Phase);
                Assert.AreEqual("l1", r.LastLore);
                return;
            }
            Assert.Fail("no seed drew b1 at depth 2");
        }

        // ---- balance --------------------------------------------------------------------------------

        static (RunState run, Dictionary<int, string> events) Play(ContentDb db, int seed, string lastLore)
        {
            var o = RunOptions.Full(Unlocks.Fresh(db)); o.LastLore = lastLore;
            var run = new RunState(db, new Journal(), seed, 1, o);
            var bot = new AutoPlayer(db); var seen = new Dictionary<int, string>();
            for (int guard = 0; guard < 600 && run.Phase != RunPhase.Won && run.Phase != RunPhase.Lost; guard++)
            {
                if (!seen.ContainsKey(run.Depth) && run.Phase != RunPhase.AspectOffer) seen[run.Depth] = run.CurrentEvent?.id;
                if (bot.Decide(run) == AutoMove.ReadyToIgnite) run.Ignite();
            }
            return (run, seen);
        }

        /// <summary>
        /// The same seed played with and without a remembered Lore event: the runs are the same move for move, and differ
        /// only where the remembered event would have come again (then no event comes).
        /// </summary>
        [Test]
        public void SkippingALoreEvent_IsBalanceNeutral()
        {
            var db = new ContentDb(TestContent.Load());
            int skipped = 0;
            for (int seed = 1; seed <= 80; seed++)
            {
                var (a, ea) = Play(db, seed, null);
                var (b, eb) = Play(db, seed, "quiet_room");
                Assert.AreEqual(a.Phase, b.Phase, "seed " + seed);
                Assert.AreEqual(a.Depth, b.Depth, "seed " + seed);
                Assert.AreEqual(a.Potency, b.Potency, "seed " + seed);
                Assert.AreEqual(a.TrialsLeft, b.TrialsLeft, "seed " + seed);
                Assert.AreEqual(a.RedrawsUsed, b.RedrawsUsed, "seed " + seed);
                CollectionAssert.AreEquivalent(ea.Keys, eb.Keys, "seed " + seed);
                foreach (var d in ea.Keys)
                {
                    if (ea[d] == eb[d]) continue;
                    Assert.AreEqual("quiet_room", ea[d], "seed " + seed + " depth " + d);
                    Assert.IsNull(eb[d], "seed " + seed + " depth " + d);
                    skipped++;
                }
            }
            Assert.Greater(skipped, 0, "no seed met quiet_room: the test did not exercise the skip");
        }

        // ---- the profile ----------------------------------------------------------------------------

        [Test]
        public void Profile_RemembersOnlyLoreEvents()
        {
            var p = new Profile();
            Assert.IsNull(p.LastLore);
            p.EventShown(Ev("l1", "Lore"));
            Assert.AreEqual("l1", p.LastLore);
            p.EventShown(Ev("b1", "Boon"));
            p.EventShown(null);
            Assert.AreEqual("l1", p.LastLore);
        }

        [Test]
        public void Profile_LastLore_IsSaved_AndDroppedWhenNoLongerALoreEvent()
        {
            var db = new ContentDb(TestContent.Load());
            var p = new Profile(); p.EventShown(db.Events.First(e => e.id == "quiet_room"));
            Assert.AreEqual("quiet_room", Profile.FromData(p.ToData(), db).LastLore);
            Assert.IsNull(Profile.FromData(new ProfileData { lastLore = "no_such_event" }, db).LastLore);
            Assert.IsNull(Profile.FromData(new ProfileData { lastLore = "warm_stone" }, db).LastLore, "warm_stone is a Boon");
            Assert.IsNull(Profile.FromData(new ProfileData { lastLore = "" }, db).LastLore, "JsonUtility writes a missing string as empty");
            Assert.AreEqual("anything", Profile.FromData(new ProfileData { lastLore = "anything" }).LastLore, "no db: nothing is checked");
        }
    }
}
