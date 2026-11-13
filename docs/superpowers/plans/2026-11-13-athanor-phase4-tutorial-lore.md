# Athanor Phase 4 (part 1): Tutorial core and lore — Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Make the tutorial script in `docs/devlogs/phase-4-tutorial-script.md` (v2) playable and measured in Core, and ship
the lore pass in `docs/devlogs/phase-4-lore.md` (expanded opening cutscene, rewritten and new events).

**Architecture:** The tutorial is an ordinary `RunState` with three small hooks: `Deal` (a scripted hand), `RunOptions.QuietBefore`
(no aspects or events before a depth) and `RunOptions.FirstOffer` (a fixed first catalyst offer), plus two read-only helpers,
`RunState.Preview` (score a layout without playing it) and `ScoreParts` (a score split into its factors). The script itself
is data in `Core/Tutorial.cs`, and its tests replay it and pin every number in the script. Lore is content (`content.json`)
plus the intro text in `SimpleCutscene`. The Unity tutorial controller (highlights, click locks, held popups) is **not** in
this plan; it gets its own plan once these numbers are confirmed.

**Tech Stack:** C# 9 (Unity 6 / .NET 8 for Core tests), NUnit 3, `content.json` read by `JsonUtility` and `System.Text.Json`,
Python 3 for the one-off content edit.

---

## Before you start

- Repo root: `/Users/bunker/Desktop/New Folder`. Core code: `Athanor/Assets/Scripts/Core/`. Core tests:
  `Athanor/Assets/Tests/EditMode/` (compiled both by Unity and by `tools/CoreTests/CoreTests.csproj`, which globs
  `Core/**/*.cs` and `Tests/EditMode/**/*.cs`, so **new files there need no project change**).
- Run Core tests: `dotnet test tools/CoreTests` (baseline: 245 passed). Filter: `dotnet test tools/CoreTests --filter TutorialTests`.
- Run Unity tests: `unity test ./Athanor --mode EditMode` (baseline: 256 passed, 6 skipped).
- Never commit `tools/CoreTests/obj/` or `tools/CoreTests/bin/` (they show as modified; leave them). Always `git add` named files.
- Domain words: a **hand** is starting materials (tier 0, from the **satchel**) plus discoveries (the **rack**). A **trial**
  is one Ignite. The **bench** is 3x3, tiles 0 to 8 in reading order, 4 is the centre. A chain step's **hub** is the
  material with the highest Reaction Index that can react; its result stands on the hub's tile.
- Score of one reaction (`IgniteResolver.Score`): `Base(tier)` (0, 20, 60, 180, 500, 1500) x `(items - 1)` x 2 if first
  this run x chain step, then catalysts.

## File map

| File | Change | Responsibility |
|---|---|---|
| `Athanor/Assets/Scripts/Core/Satchel.cs` | modify | `Available(id)`, `Take(id)`: pull a named card |
| `Athanor/Assets/Scripts/Core/RunState.cs` | modify | `Deal`, `Preview`, fixed first offer, quiet depths |
| `Athanor/Assets/Scripts/Core/Profile.cs` | modify | `RunOptions.QuietBefore`, `RunOptions.FirstOffer`; `Profile.LastLore`, `RunOptions.LastLore` (Task 13) |
| `Athanor/Assets/Scripts/Core/ScoreParts.cs` | create | A reaction's score split into base, used, first, step, bonus |
| `Athanor/Assets/Scripts/Core/Tutorial.cs` | create | The tutorial script as data: hands, layouts, results, options |
| `Athanor/Assets/Tests/EditMode/TutorialTests.cs` | create | Hooks + the whole script replayed with its numbers |
| `Athanor/Assets/Scripts/Core/ContentData.cs` | modify | `ProfileData.lastLore` (Task 13) |
| `Athanor/Assets/Scripts/Core/RunStart.cs` | modify | `DrawEvent` leaves out the last Lore event; `RunState.LastLore` (Task 13) |
| `Athanor/Assets/Tests/EditMode/LoreTests.cs` | create | Lore events not shown twice in a row; balance-neutral check (Task 13) |
| `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs` | modify | Pass `Profile.LastLore` to the run, record each event shown (Task 13) |
| `Athanor/Assets/Tests/EditMode/ContentTests.cs` | modify | Event text limits, Lore events |
| `Athanor/Assets/Tests/EditMode/Phase3Tests.cs` | modify | Event count 16 → 19 |
| `Athanor/Assets/Resources/content.json` | modify | Lore event texts, 3 new Lore events |
| `Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs` | modify | Intro as three pages |
| `Athanor/Assets/Scripts/Game/Boot/Save.cs` | modify | `TutorialKey`, `TutorialDone` |
| `Athanor/Assets/Tests/EditModeGame/SaveTests.cs` | modify | `TutorialDone` round trip |
| `Athanor/Assets/Scripts/Game/Bench/HudView.cs` | modify | Redraw as its own button under its counter, greyed at 0 (`CanRedraw`, `RedrawAnchor`) |
| `Athanor/Assets/Scripts/Game/Bench/BenchView.cs` | modify | Redraw tooltip points at the button |
| `Athanor/Assets/Tests/EditModeGame/HudTests.cs` | create | `HudView.CanRedraw`: live until 0, off the bench never |
| `docs/devlogs/phase-4-tutorial-script.md`, `phase-4-prep.md`, `phase-4-lore.md`, `README.md` | modify | Measured numbers, log |

---

### Task 0: Branch

- [ ] **Step 1: Create the branch**

```bash
cd "/Users/bunker/Desktop/New Folder"
git checkout -b phase4-tutorial-lore
dotnet test tools/CoreTests
```
Expected: `Passed!  - Failed: 0, Passed: 245`.

---

### Task 1: Satchel can hand out a named card

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/Satchel.cs` (class `Satchel`, after `Discard`)
- Create: `Athanor/Assets/Tests/EditMode/TutorialTests.cs`

- [ ] **Step 1: Write the failing test** (creates the test file used by every later task)

```csharp
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>Phase 4: the hooks the tutorial needs, and the tutorial script replayed with every number it shows.</summary>
    public class TutorialTests
    {
        ContentDb db;
        [SetUp] public void Load() => db = new ContentDb(TestContent.Load());

        // ---- satchel ------------------------------------------------------------------------------

        [Test]
        public void Satchel_Take_PullsANamedCard_FromDrawOrDiscard()
        {
            var s = new Satchel(new[] { "a", "b", "b" }, 1);
            Assert.AreEqual(2, s.Available("b"));
            Assert.IsTrue(s.Take("b"));
            Assert.AreEqual(1, s.Available("b"));
            Assert.AreEqual(3, s.Total);                 // a card that is out still belongs to the satchel
            s.Discard(new[] { "b" });
            Assert.AreEqual(2, s.Available("b"));        // on the discard pile it can be taken again
            Assert.IsTrue(s.Take("b"));
            Assert.IsTrue(s.Take("b"));
            Assert.IsFalse(s.Take("b"));
            Assert.IsFalse(s.Take("zzz"));
        }
    }
}
```

- [ ] **Step 2: Run it, see it fail**

Run: `dotnet test tools/CoreTests --filter TutorialTests`
Expected: build error `'Satchel' does not contain a definition for 'Available'`.

- [ ] **Step 3: Implement** — add inside `Satchel`, after `Discard`:

```csharp
        /// <summary>Copies of a card in the draw and discard piles (not the ones out).</summary>
        public int Available(string id) => draw.Count(c => c == id) + discard.Count(c => c == id);

        /// <summary>
        /// Takes a named card out of the draw pile, or else the discard pile, as if it had been drawn (the tutorial's
        /// scripted hands). False, and nothing changed, when neither pile has a copy.
        /// </summary>
        public bool Take(string id)
        {
            var pile = draw.Contains(id) ? draw : discard.Contains(id) ? discard : null;
            if (pile == null) return false;
            pile.RemoveAt(pile.LastIndexOf(id));
            outCards.Add(id);
            return true;
        }
```
(`Satchel.cs` already has `using System.Linq;` — check the top of the file; add it if missing.)

- [ ] **Step 4: Run it, see it pass**

Run: `dotnet test tools/CoreTests --filter TutorialTests` → Expected: 1 passed.

- [ ] **Step 5: Commit**

```bash
git add Athanor/Assets/Scripts/Core/Satchel.cs Athanor/Assets/Tests/EditMode/TutorialTests.cs
git commit -m "Core: Satchel.Take and Available for scripted hands"
```

---

### Task 2: `RunState.Deal` — a scripted hand

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/RunState.cs` (after `Redraw()`)
- Test: `Athanor/Assets/Tests/EditMode/TutorialTests.cs`

- [ ] **Step 1: Write the failing tests** — add to `TutorialTests` (below the satchel test):

```csharp
        // ---- scripted hands -----------------------------------------------------------------------

        static readonly string[] FirstHand = { "ember_coal", "deep_tear", "iron_ore", "moondust", "bone_ash" };

        /// <summary>Lays each (id, tile) from the hand and ignites.</summary>
        static IgniteOutcome LayAndIgnite(RunState run, params (string id, int tile)[] lay)
        {
            foreach (var (id, tile) in lay)
                Assert.IsTrue(run.Place(run.Hand.ToList().IndexOf(id), tile), "place " + id + " on " + tile);
            return run.Ignite();
        }

        [Test]
        public void Deal_ReplacesTheStartingHand_AndKeepsTheSatchelWhole()
        {
            var run = new RunState(db, new Journal(), 7);
            int size = run.SatchelSize;
            Assert.IsTrue(run.Deal(FirstHand));
            CollectionAssert.AreEquivalent(FirstHand, run.Hand);
            Assert.AreEqual(size, run.SatchelSize);
        }

        [Test]
        public void Deal_KeepsTheRack()
        {
            var run = new RunState(db, new Journal(), 7);
            run.Deal(FirstHand);
            LayAndIgnite(run, ("ember_coal", 4), ("deep_tear", 5));
            Assert.IsTrue(run.Deal(FirstHand));
            CollectionAssert.AreEquivalent(FirstHand.Concat(new[] { "steam_wisp" }), run.Hand);
        }

        [Test]
        public void Deal_Refuses_TooManyCopies_NonStarters_LockedStarters_AndABusyBench()
        {
            var run = new RunState(db, new Journal(), 7);
            var before = run.Hand.ToList();
            Assert.IsFalse(run.Deal(new[] { "moondust", "moondust", "moondust" }));   // the satchel holds two
            Assert.IsFalse(run.Deal(new[] { "steam_wisp" }));                         // a discovery
            Assert.IsFalse(run.Deal(new[] { "salt_crystal" }));                       // locked: not in a fresh satchel
            Assert.IsFalse(run.Deal(new[] { "no_such_thing" }));
            CollectionAssert.AreEqual(before, run.Hand);
            run.Place(0, 4);
            Assert.IsFalse(run.Deal(new[] { "moondust" }));
        }
```

- [ ] **Step 2: Run, see them fail**

Run: `dotnet test tools/CoreTests --filter TutorialTests`
Expected: build error `'RunState' does not contain a definition for 'Deal'`.

- [ ] **Step 3: Implement** — in `RunState.cs`, after `Redraw()`:

```csharp
        /// <summary>
        /// The tutorial: puts the starting materials in hand back and deals exactly these instead. Discoveries on the
        /// rack stay. Only while playing, on an empty bench. False, and nothing changed, when an id is not an open
        /// starting material or the satchel has too few copies of it.
        /// </summary>
        public bool Deal(IReadOnlyList<string> ids)
        {
            if (Phase != RunPhase.Playing || Bench.Count > 0 || ids == null) return false;
            foreach (var g in ids.GroupBy(i => i))
                if (!db.Has(g.Key) || !IsMain(g.Key) || satchel.Available(g.Key) + hand.Count(h => h == g.Key) < g.Count()) return false;
            satchel.Discard(hand.Where(IsMain));
            hand.RemoveAll(IsMain);
            foreach (var id in ids) satchel.Take(id);
            hand.AddRange(ids);
            return true;
        }
```

- [ ] **Step 4: Run, see them pass**

Run: `dotnet test tools/CoreTests --filter TutorialTests` → Expected: 4 passed.

- [ ] **Step 5: Commit**

```bash
git add Athanor/Assets/Scripts/Core/RunState.cs Athanor/Assets/Tests/EditMode/TutorialTests.cs
git commit -m "Core: RunState.Deal for the tutorial's scripted hands"
```

---

### Task 3: Quiet depths and a fixed first offer

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/Profile.cs` (class `RunOptions`)
- Modify: `Athanor/Assets/Scripts/Core/RunState.cs` (`BeginDepth`, `BuildOffer`, a new field)
- Test: `Athanor/Assets/Tests/EditMode/TutorialTests.cs`

- [ ] **Step 1: Write the failing tests** — add to `TutorialTests`:

```csharp
        // ---- run options --------------------------------------------------------------------------

        [Test]
        public void QuietBefore_HoldsAspectsAndEventsBack_UntilThatDepth()
        {
            var opts = new RunOptions { Unlocks = Unlocks.Fresh(db), Aspects = true, Events = true, QuietBefore = 3 };
            Assert.AreEqual(RunPhase.AspectOffer, new RunState(db, new Journal(), 7, 1, RunOptions.Full(Unlocks.Fresh(db))).Phase, "control");
            for (int seed = 1; seed <= 40; seed++)
            {
                foreach (int depth in new[] { 1, 2 })
                {
                    var run = new RunState(db, new Journal(), seed, depth, opts);
                    Assert.AreEqual(RunPhase.Playing, run.Phase, "seed " + seed + " depth " + depth);
                    Assert.IsNull(run.CurrentEvent);
                    Assert.AreEqual(0, run.Aspects.Count);
                }
            }
            Assert.AreEqual(RunPhase.AspectOffer, new RunState(db, new Journal(), 7, 3, opts).Phase);
        }

        /// <summary>Two starters, one recipe, targets of 1, four catalysts: every reaction clears a depth.</summary>
        static ContentDb MiniWithCatalysts() => new ContentDb(new ContentData
        {
            materials = new[] { TestContent.Mat("a", "Ore", 0, 6), TestContent.Mat("b", "Potion", 0, 4), TestContent.Mat("x", "Metal", 1, 5) },
            recipes = new[] { TestContent.Rec(new[] { "a", "b" }, 0, "x") },
            catalysts = new[] { TestContent.Cat(Effects.ChainPlus), TestContent.Cat(Effects.ExtraTrial), TestContent.Cat(Effects.ExtraRedraw), TestContent.Cat(Effects.PairBonus, "", 2) },
            depthTargets = new[] { 1, 1, 1, 1 }, depthNames = new[] { "1", "2", "3", "4", "5" }, ultimate = "",
        });

        [Test]
        public void FirstOffer_IsFixed_ForTheFirstOfferOnly()
        {
            var mini = MiniWithCatalysts();
            var run = new RunState(mini, new Journal(), 7, 1, new RunOptions { FirstOffer = new[] { Effects.ChainPlus, Effects.ExtraTrial, "unknown" } });
            LayAndIgnite(run, ("a", 4), ("b", 5));
            Assert.AreEqual(RunPhase.DepthCleared, run.Phase);
            CollectionAssert.AreEqual(new[] { Effects.ChainPlus, Effects.ExtraTrial }, run.CatalystOffer.Select(c => c.id).ToArray());
            run.ChooseCatalyst(Effects.ChainPlus);
            LayAndIgnite(run, ("a", 4), ("b", 5));
            Assert.AreEqual(3, run.CatalystOffer.Count, "the second offer is drawn as usual");
        }

        [Test]
        public void FirstOffer_LeavesOutLockedCatalysts()
        {
            var opts = new RunOptions { Unlocks = Unlocks.Fresh(db), FirstOffer = new[] { "crooked_mirror", "bellows" } };   // crooked_mirror is locked
            var run = new RunState(db, new Journal(), 7, 1, opts);
            run.Deal(FirstHand); LayAndIgnite(run, ("ember_coal", 4), ("deep_tear", 5));
            run.Deal(new[] { "iron_ore", "ember_coal", "moondust", "bone_ash", "sunbloom" });
            LayAndIgnite(run, ("iron_ore", 3), ("ember_coal", 4), ("moondust", 5));
            Assert.AreEqual(RunPhase.DepthCleared, run.Phase);
            CollectionAssert.AreEqual(new[] { "bellows" }, run.CatalystOffer.Select(c => c.id).ToArray());
        }
```

- [ ] **Step 2: Run, see them fail**

Run: `dotnet test tools/CoreTests --filter TutorialTests`
Expected: build error `'RunOptions' does not contain a definition for 'QuietBefore'`.

- [ ] **Step 3: Implement the options** — in `Profile.cs`, inside `RunOptions`, after `ForceEventAt`:

```csharp
        /// <summary>The tutorial: no aspect offer and no event at a depth below this one. 0: none held back.</summary>
        public int QuietBefore;
        /// <summary>The tutorial: the catalysts the run's first offer shows, in this order. Locked, held or unknown ids are left out.</summary>
        public string[] FirstOffer;
```

- [ ] **Step 4: Implement the run side** — in `RunState.cs`:

Add a field next to `int carried;`:
```csharp
        bool firstOfferMade;
```

In `BeginDepth()`, replace the two `...Pending` lines with:
```csharp
            bool quiet = Depth < options.QuietBefore;
            aspectPending = options.Aspects && !quiet && (Depth == 1 || Depth == 3 || Depth == 5);
            eventPending = options.Events && !quiet;
```

Replace `BuildOffer()` with:
```csharp
        void BuildOffer()
        {
            if (options.FirstOffer != null && !firstOfferMade)
            {
                firstOfferMade = true;
                offer = options.FirstOffer.Select(id => db.Catalysts.FirstOrDefault(c => c.id == id))
                    .Where(c => c != null && IsOpen(c.id) && catalysts.All(o => o.id != c.id)).ToList();
                if (offer.Count > 0) return;
            }
            var pool = db.Catalysts.Where(c => IsOpen(c.id) && catalysts.All(o => o.id != c.id)).ToList();
            rng.Shuffle(pool);
            offer = pool.Take(OfferSize).ToList();
        }
```

- [ ] **Step 5: Run, see them pass; run everything**

Run: `dotnet test tools/CoreTests --filter TutorialTests` → Expected: 7 passed.
Run: `dotnet test tools/CoreTests` → Expected: 252 passed (245 + 7). The old runs pass `QuietBefore = 0`, so nothing else changes.

- [ ] **Step 6: Commit**

```bash
git add Athanor/Assets/Scripts/Core/Profile.cs Athanor/Assets/Scripts/Core/RunState.cs Athanor/Assets/Tests/EditMode/TutorialTests.cs
git commit -m "Core: RunOptions.QuietBefore and FirstOffer for the tutorial"
```

---

### Task 4: `RunState.Preview` and `ScoreParts`

**Files:**
- Create: `Athanor/Assets/Scripts/Core/ScoreParts.cs`
- Modify: `Athanor/Assets/Scripts/Core/RunState.cs` (after `NewContext()`)
- Test: `Athanor/Assets/Tests/EditMode/TutorialTests.cs`

- [ ] **Step 1: Write the failing tests** — add to `TutorialTests`:

```csharp
        // ---- preview and score parts --------------------------------------------------------------

        static string[] Row(string left, string centre, string right)
        {
            var t = new string[Bench.TileCount]; t[3] = left; t[4] = centre; t[5] = right; return t;
        }

        [Test]
        public void Preview_ScoresALayout_AndChangesNothing()
        {
            var journal = new Journal();
            var run = new RunState(db, journal, 7);
            var hand = run.Hand.ToList();
            var events = run.Preview(Row("iron_ore", "ember_coal", "moondust")).Where(e => e.Result != null).ToList();
            CollectionAssert.AreEqual(new[] { "sky_iron", "starmetal_ore" }, events.Select(e => e.Result).ToArray());
            CollectionAssert.AreEqual(new long[] { 40, 240 }, events.Select(e => e.Potency).ToArray());
            CollectionAssert.AreEqual(new[] { 4, 5 }, events.Select(e => e.Hub).ToArray());   // step 2's hub is Moondust
            Assert.AreEqual(0, journal.KnownPairs.Count);
            Assert.IsFalse(run.HasFired("ember_coal", "iron_ore"));
            Assert.AreEqual(0, run.Potency);
            CollectionAssert.AreEqual(hand, run.Hand);
        }

        [Test]
        public void ScoreParts_SplitsAScore_IntoItsFactors()
        {
            var run = new RunState(db, new Journal(), 7);
            var step2 = run.Preview(Row("iron_ore", "ember_coal", "moondust")).Where(e => e.Result != null).ElementAt(1);
            var p = ScoreParts.Of(db, step2);
            Assert.AreEqual(60, p.Base); Assert.AreEqual(1, p.Used); Assert.AreEqual(2, p.First); Assert.AreEqual(2, p.Step);
            Assert.AreEqual(1.0, p.Bonus, 1e-9); Assert.AreEqual(240, p.Total);
            Assert.IsNull(ScoreParts.Of(db, new ReactionEvent()));   // a fizzle has no score
        }
```

- [ ] **Step 2: Run, see them fail**

Run: `dotnet test tools/CoreTests --filter TutorialTests`
Expected: build error `'RunState' does not contain a definition for 'Preview'`.

- [ ] **Step 3: Implement `ScoreParts`** — create `Athanor/Assets/Scripts/Core/ScoreParts.cs`:

```csharp
namespace Athanor.Core
{
    /// <summary>
    /// One reaction's potency split into the factors the tutorial names: Total = Base x Used x First x Step x Bonus.
    /// Bonus is everything else (catalysts, the hub's tile, events); 1 when nothing else applied.
    /// </summary>
    public sealed class ScoreParts
    {
        public long Base, Total;
        public int Used, First, Step;
        public double Bonus;

        /// <summary>Null for a fizzle or a sputter.</summary>
        public static ScoreParts Of(ContentDb db, ReactionEvent e)
        {
            if (e == null || e.Recipe == null || e.Result == null) return null;
            var p = new ScoreParts
            {
                Base = ContentDb.BasePotency(db.Material(e.Result).tier),
                Used = e.Recipe.items.Length - 1,
                First = e.FirstThisRun ? 2 : 1,
                Step = e.Chain,
                Total = e.Potency,
            };
            double plain = (double)p.Base * p.Used * p.First * p.Step;
            p.Bonus = plain > 0 ? e.Potency / plain : 1;
            return p;
        }
    }
}
```

- [ ] **Step 4: Implement `Preview`** — in `RunState.cs`, after `NewContext()`:

```csharp
        /// <summary>
        /// What this layout (9 tiles, null where empty) would do if ignited now. Nothing changes: the journal and the
        /// recipes fired this run are copied. The tutorial uses it to show a broken chain beside the real one.
        /// </summary>
        public List<ReactionEvent> Preview(string[] tiles)
        {
            var bench = new Bench();
            for (int t = 0; t < Bench.TileCount && t < tiles.Length; t++) bench.Tiles[t] = tiles[t];
            var copy = Journal.FromData(journal.ToData());
            return IgniteResolver.Resolve(bench, db, copy, new HashSet<string>(fired), catalysts, NewContext()).Events;
        }
```

- [ ] **Step 5: Run, see them pass**

Run: `dotnet test tools/CoreTests --filter TutorialTests` → Expected: 9 passed.

- [ ] **Step 6: Commit**

```bash
git add Athanor/Assets/Scripts/Core/ScoreParts.cs Athanor/Assets/Scripts/Core/RunState.cs Athanor/Assets/Tests/EditMode/TutorialTests.cs
git commit -m "Core: RunState.Preview and ScoreParts"
```

---

### Task 5: The tutorial script as data, replayed with its numbers

**Files:**
- Create: `Athanor/Assets/Scripts/Core/Tutorial.cs`
- Test: `Athanor/Assets/Tests/EditMode/TutorialTests.cs`

The expected numbers below were worked out by hand from `IgniteResolver.Score` and `ChainPlanner`. **If a test
disagrees, do not change the test to match without telling the owner**: the script text shows these numbers, so a
mismatch means the script (`phase-4-tutorial-script.md`) must change too. Report it.

- [ ] **Step 1: Write the failing tests** — add to `TutorialTests`:

```csharp
        // ---- the script ---------------------------------------------------------------------------

        RunState StartTutorial(int seed = TutorialScript.Seed) =>
            new RunState(db, new Journal(), seed, 1, TutorialScript.Options(Unlocks.Fresh(db)));

        static IgniteOutcome Play(RunState run, TutorialTrial trial)
        {
            Assert.IsTrue(run.Deal(trial.Hand), "deal");
            foreach (var lay in trial.Lay)
                Assert.IsTrue(run.Place(run.Hand.ToList().IndexOf(lay.Id), lay.Tile), "place " + lay.Id + " on " + lay.Tile);
            return run.Ignite();
        }

        static List<ReactionEvent> Reactions(IEnumerable<ReactionEvent> events) => events.Where(e => e.Result != null).ToList();

        [Test]
        public void Trial1_EmberCoalAndDeepTear_MakeSteamWisp_For40()
        {
            var run = StartTutorial();
            Assert.AreEqual(RunPhase.Playing, run.Phase);
            Assert.AreEqual(4, run.TrialsLeft);
            var r = Reactions(Play(run, TutorialScript.Reaction).Events);
            CollectionAssert.AreEqual(TutorialScript.Reaction.Makes, r.Select(e => e.Result).ToArray());
            var p = ScoreParts.Of(db, r[0]);
            Assert.AreEqual(20, p.Base); Assert.AreEqual(1, p.Used); Assert.AreEqual(2, p.First); Assert.AreEqual(1, p.Step);
            Assert.AreEqual(40, p.Total);
            Assert.AreEqual(3, run.TrialsLeft);
            Assert.Contains("steam_wisp", run.Hand.ToList());   // kept on the rack
        }

        [Test]
        public void Trial2_GlowmossAndIronOre_Fizzle_SpendATrial_AndHintPotionOnGlowmoss()
        {
            var run = StartTutorial();
            Play(run, TutorialScript.Reaction);
            var o = Play(run, TutorialScript.Miss);
            Assert.IsEmpty(Reactions(o.Events));
            Assert.AreEqual(2, run.TrialsLeft);
            Assert.AreEqual(40, run.Potency);
            var hint = o.Events.Single(e => e.Hint != null).Hint;
            Assert.AreEqual("glowmoss", hint.material);
            Assert.AreEqual("Potion", hint.partnerKind);
        }

        [Test]
        public void Trial3_Chain_SkyIronThenStarmetalOre_40And240_ClearsDepth1()
        {
            var run = StartTutorial();
            Play(run, TutorialScript.Reaction);
            Play(run, TutorialScript.Miss);
            var broken = Reactions(run.Preview(TutorialScript.BrokenChain()));
            CollectionAssert.AreEqual(new long[] { 40 }, broken.Select(e => e.Potency).ToArray());
            var r = Reactions(Play(run, TutorialScript.Chain).Events);
            CollectionAssert.AreEqual(TutorialScript.Chain.Makes, r.Select(e => e.Result).ToArray());
            CollectionAssert.AreEqual(new long[] { 40, 240 }, r.Select(e => e.Potency).ToArray());
            CollectionAssert.AreEqual(new[] { 4, 5 }, r.Select(e => e.Hub).ToArray());
            CollectionAssert.AreEqual(new[] { 1, 2 }, r.Select(e => e.Chain).ToArray());
            Assert.AreEqual(320, run.Potency);
            Assert.AreEqual(RunPhase.DepthCleared, run.Phase);
            Assert.AreEqual(1, run.TrialsLeft);
            CollectionAssert.AreEqual(TutorialScript.Offer, run.CatalystOffer.Select(c => c.id).ToArray());
        }

        [TestCase("bellows", 80, 2.0)]
        [TestCase("heart_tile", 80, 2.0)]
        [TestCase("first_spark", 120, 3.0)]
        public void Depth2_CatalystTrial_ShowsTheCatalyst(string catalyst, long score, double bonus)
        {
            var run = StartTutorial();
            foreach (var t in TutorialScript.Depth1) Play(run, t);
            run.ChooseCatalyst(catalyst);
            Assert.AreEqual(2, run.Depth);
            Assert.AreEqual(RunPhase.Playing, run.Phase, "no aspect or event before depth 3");
            Assert.AreEqual(5, run.TrialsLeft, "4 plus the 1 left at depth 1");
            var trial = TutorialScript.CatalystTrial(catalyst);
            var r = Reactions(Play(run, trial).Events);
            CollectionAssert.AreEqual(trial.Makes, r.Select(e => e.Result).ToArray());
            Assert.AreEqual(score, r[0].Potency);
            Assert.AreEqual(bonus, ScoreParts.Of(db, r[0]).Bonus, 1e-9);
        }

        [Test]
        public void Script_IsTheSame_WhateverTheSeed()
        {
            for (int seed = 1; seed <= 25; seed++)
            {
                var run = StartTutorial(seed);
                foreach (var t in TutorialScript.Depth1) Play(run, t);
                Assert.AreEqual(320, run.Potency, "seed " + seed);
            }
        }

        [Test]
        public void CatalystTrial_CoversTheOffer_AndNothingElse()
        {
            foreach (var id in TutorialScript.Offer) Assert.IsNotNull(TutorialScript.CatalystTrial(id), id);
            Assert.IsNull(TutorialScript.CatalystTrial("hourglass"));
        }

        [TestCase(false, 0, true)]
        [TestCase(true, 0, false)]
        [TestCase(false, 3, false)]
        public void ShouldRun_OnlyOnAFreshSave(bool done, int journalEntries, bool expected) =>
            Assert.AreEqual(expected, TutorialScript.ShouldRun(done, journalEntries));
```

- [ ] **Step 2: Run, see them fail**

Run: `dotnet test tools/CoreTests --filter TutorialTests`
Expected: build error `The type or namespace name 'TutorialScript' could not be found`.

- [ ] **Step 3: Implement** — create `Athanor/Assets/Scripts/Core/Tutorial.cs`:

```csharp
using System.Linq;

namespace Athanor.Core
{
    /// <summary>One material laid on one tile.</summary>
    public readonly struct TutorialLay
    {
        public readonly string Id;
        public readonly int Tile;
        public TutorialLay(string id, int tile) { Id = id; Tile = tile; }
    }

    /// <summary>One scripted trial: the hand dealt, where each material goes, and what the Ignite makes, in chain order.</summary>
    public sealed class TutorialTrial
    {
        public string[] Hand;
        public TutorialLay[] Lay;
        public string[] Makes;   // empty for the miss
    }

    /// <summary>
    /// The tutorial (docs/devlogs/phase-4-tutorial-script.md, v2) as data. Every number the script shows is pinned by
    /// TutorialTests; change both together.
    /// </summary>
    public static class TutorialScript
    {
        public const int Seed = 20261113;
        /// <summary>No aspect offer and no event before this depth, so nothing touches the scripted hands.</summary>
        public const int QuietBefore = 3;
        public static readonly string[] Offer = { "bellows", "heart_tile", "first_spark" };

        /// <summary>Trial 1: a reaction. 40 potency.</summary>
        public static readonly TutorialTrial Reaction = new TutorialTrial
        {
            Hand = new[] { "ember_coal", "deep_tear", "iron_ore", "moondust", "bone_ash" },
            Lay = new[] { new TutorialLay("ember_coal", 4), new TutorialLay("deep_tear", 5) },
            Makes = new[] { "steam_wisp" },
        };

        /// <summary>Trial 2: a miss. Glowmoss is on the lower tile so the hint is written on its page.</summary>
        public static readonly TutorialTrial Miss = new TutorialTrial
        {
            Hand = new[] { "glowmoss", "iron_ore", "sunbloom", "moondust", "bone_ash" },
            Lay = new[] { new TutorialLay("glowmoss", 3), new TutorialLay("iron_ore", 4) },
            Makes = new string[0],
        };

        /// <summary>Trial 3: a chain, 40 then 240, which clears depth 1 (320 of 300).</summary>
        public static readonly TutorialTrial Chain = new TutorialTrial
        {
            Hand = new[] { "iron_ore", "ember_coal", "moondust", "bone_ash", "sunbloom" },
            Lay = new[] { new TutorialLay("iron_ore", 3), new TutorialLay("ember_coal", 4), new TutorialLay("moondust", 5) },
            Makes = new[] { "sky_iron", "starmetal_ore" },
        };

        public static readonly TutorialTrial[] Depth1 = { Reaction, Miss, Chain };

        /// <summary>The bench of a trial, 9 tiles, null where empty.</summary>
        public static string[] Layout(TutorialTrial trial) => Layout(trial.Lay);

        /// <summary>Trial 3 without its last material (Moondust): the broken chain that step 3.6 compares against.</summary>
        public static string[] BrokenChain() => Layout(Chain.Lay.Take(Chain.Lay.Length - 1).ToArray());

        static string[] Layout(TutorialLay[] lay)
        {
            var tiles = new string[Bench.TileCount];
            foreach (var l in lay) tiles[l.Tile] = l.Id;
            return tiles;
        }

        /// <summary>The first trial of depth 2, laid so the chosen catalyst shows. Null for a catalyst not on the offer.</summary>
        public static TutorialTrial CatalystTrial(string catalystId)
        {
            switch (catalystId)
            {
                case "bellows": return new TutorialTrial   // Ember Coal is an Ore: x2, 80
                {
                    Hand = new[] { "ember_coal", "bone_ash", "glowmoss", "sunbloom", "deep_tear" },
                    Lay = new[] { new TutorialLay("ember_coal", 4), new TutorialLay("bone_ash", 5) },
                    Makes = new[] { "sulfur" },
                };
                case "heart_tile": return new TutorialTrial   // touches the centre: x2, 80
                {
                    Hand = new[] { "deep_tear", "moondust", "glowmoss", "sunbloom", "bone_ash" },
                    Lay = new[] { new TutorialLay("deep_tear", 4), new TutorialLay("moondust", 5) },
                    Makes = new[] { "frost_salt" },
                };
                case "first_spark": return new TutorialTrial   // the first reaction: x3, 120
                {
                    Hand = new[] { "sunbloom", "moondust", "glowmoss", "deep_tear", "bone_ash" },
                    Lay = new[] { new TutorialLay("sunbloom", 4), new TutorialLay("moondust", 5) },
                    Makes = new[] { "pixie_wing" },
                };
                default: return null;
            }
        }

        /// <summary>The run options of the tutorial run: the game's, with the quiet start and the fixed first offer.</summary>
        public static RunOptions Options(Unlocks unlocks) => new RunOptions
        {
            Unlocks = unlocks, Aspects = true, Events = true, QuietBefore = QuietBefore, FirstOffer = Offer,
        };

        /// <summary>The tutorial plays on a save that has never finished it and whose journal is empty.</summary>
        public static bool ShouldRun(bool tutorialDone, int journalEntries) => !tutorialDone && journalEntries == 0;
    }
}
```

- [ ] **Step 4: Run, see them pass**

Run: `dotnet test tools/CoreTests --filter TutorialTests` → Expected: 20 passed (9 + 11; each `TestCase` counts once).
If any number assertion fails, stop and report the actual value (see the note at the top of this task).

- [ ] **Step 5: Run everything**

Run: `dotnet test tools/CoreTests` → Expected: 265 passed, 0 failed.

- [ ] **Step 6: Commit**

```bash
git add Athanor/Assets/Scripts/Core/Tutorial.cs Athanor/Assets/Tests/EditMode/TutorialTests.cs
git commit -m "Core: the tutorial script as data, replayed by TutorialTests"
```

---

### Task 6: Newcomer baseline and measured numbers in the script

**Files:**
- Modify: `docs/devlogs/phase-4-tutorial-script.md` (section "Numbers")
- Modify: `docs/devlogs/phase-4-prep.md` (section "Log"; Act step 3)

- [ ] **Step 1: Run the baseline reports** (prep, Act step 3; before the lore changes the event pool)

```bash
cd tools/CoreTests
dotnet test --filter BalanceMatrix -l "console;verbosity=detailed" > /tmp/balance-before.txt
ATHANOR_TAUGHT=4 ATHANOR_PLAYERS=12 ATHANOR_RUNS=600 dotnet test --filter RoleReport -l "console;verbosity=detailed" > /tmp/role-before.txt
cd ../..
```
Expected: both end with `Passed!`. RoleReport takes several minutes.

- [ ] **Step 2: Write the numbers into the script doc** — replace the body of "## Numbers" in
`docs/devlogs/phase-4-tutorial-script.md` with:

```markdown
Measured by `TutorialTests` (2026-11-13), all as the script states: trial 1 = 40; trial 2 = 0 with the hint
"Glowmoss reacts with something Potion"; trial 3 = 40 + 240, hubs on tiles 4 then 5, depth 1 cleared at 320 with 1 trial
left; broken chain = 40; depth 2 first trial: Bellows 80, Heart Tile 80, First Spark 120. The same for 25 seeds.
```
(If Task 5 reported a different number, write the measured one and fix the line that shows it.)

- [ ] **Step 3: Save the baseline in the prep devlog** — in `docs/devlogs/phase-4-prep.md`, under "## Log", add:

```markdown
- 2026-11-13: Newcomer baseline before the lore pass. BalanceMatrix: <paste the summary table from /tmp/balance-before.txt>.
  RoleReport (taught 4, 12 players, 600 runs): <paste the summary lines from /tmp/role-before.txt>.
```
Replace the two `<paste ...>` with the actual summary lines from the files (the table rows printed at the end of each report).

- [ ] **Step 4: Commit**

```bash
git add docs/devlogs/phase-4-tutorial-script.md docs/devlogs/phase-4-prep.md
git commit -m "Docs: tutorial numbers measured, newcomer baseline"
```

---

### Task 7: Lore pass on the events

**Files:**
- Modify: `Athanor/Assets/Tests/EditMode/ContentTests.cs` (end of class)
- Modify: `Athanor/Assets/Tests/EditMode/Phase3Tests.cs:194`
- Modify: `Athanor/Assets/Resources/content.json`

- [ ] **Step 1: Write the failing tests** — add at the end of `ContentTests` (class body):

```csharp
        [Test]
        public void EventTexts_FitTheEventWindow()
        {
            foreach (var e in data.events)
            {
                Assert.LessOrEqual(e.name.Length, 22, e.id + " name");
                Assert.LessOrEqual(e.text.Length, 56, e.id + " text");
                foreach (var o in e.options)
                    foreach (var x in o.outcomes) Assert.LessOrEqual(x.text.Length, 52, e.id + " outcome");
            }
        }

        [Test]
        public void LoreEvents_DoNothing_AndTellTheStoryInOrder()
        {
            var lore = data.events.Where(e => e.type == "Lore").ToList();
            CollectionAssert.AreEquivalent(new[] { "quiet_room", "names_in_wall", "warm_roots", "pinned_note" }, lore.Select(e => e.id));
            foreach (var e in lore)
            {
                Assert.AreEqual(1, e.options.Length, e.id);
                Assert.IsTrue(e.options[0].outcomes.All(x => x.gain.All(g => g.op == Ops.Nothing)), e.id);
            }
            var byId = lore.ToDictionary(e => e.id);
            Assert.Less(byId["names_in_wall"].maxDepth, byId["warm_roots"].minDepth);
            Assert.Less(byId["warm_roots"].maxDepth, byId["pinned_note"].minDepth);
        }
```

And in `Phase3Tests.cs` line 194 change `Assert.AreEqual(16, db.Events.Count);` to `Assert.AreEqual(19, db.Events.Count);`.

- [ ] **Step 2: Run, see them fail**

Run: `dotnet test tools/CoreTests --filter "ContentTests|Phase3Tests"`
Expected: `LoreEvents_DoNothing_AndTellTheStoryInOrder` fails (only `quiet_room`) and the count test fails (16, expected 19).
`EventTexts_FitTheEventWindow` already passes: today's texts fit, and the test guards the new ones.

- [ ] **Step 3: Apply the lore texts** — run from the repo root (keeps the file's 1-space JSON style, no trailing newline):

```bash
python3 - <<'EOF'
import json, pathlib
p = pathlib.Path("Athanor/Assets/Resources/content.json")
d = json.loads(p.read_text(encoding="utf-8"))
E = {e["id"]: e for e in d["events"]}

def edit(id, name=None, text=None, outcomes=None):
    e = E[id]
    if name: e["name"] = name
    if text: e["text"] = text
    for (opt, out), t in (outcomes or {}).items(): e["options"][opt]["outcomes"][out]["text"] = t

edit("cracked_altar", "A Keepers' Altar", "A cracked altar to the Keepers asks for an offering.")
edit("tilted_workbench", "An Old Workbench", "Another Keeper's bench. Tilted, but roomy.")
edit("old_ledger", "A Keeper's Ledger", "A damp ledger lists what another Keeper tried.")
edit("hungry_furnace", "A Keeper's Furnace", "An old furnace gapes, still warm after all this time.")
edit("shifting_bench", text="Roots shift under the floor. The bench grinds.", outcomes={(0, 0): "A tile settles over a warm root."})
edit("smugglers_crate", "A Buried Crate", "A crate, half swallowed by the roots.")
edit("forgotten_satchel", text="A satchel like yours, left on the stair.")
edit("glinting_seam", text="An old brew has set into the wall like ore.")
edit("sealed_shrine", text="A shrine to the first Keeper hums at your catalysts.")
edit("damp_air", text="Water seeps down from the stair. Everything cools.")
edit("falling_grit", text="Grit falls from the roots onto the bench.")
edit("cold_draft", text="A cold draft comes up the stair, through your pockets.")
edit("warm_stone", text="A stone in the wall is warm. Something grows behind it.")
edit("quiet_room", "A Keeper's Room", "A cot, a cup, a name scratched out. Dust turns slowly.")

def lore(id, lo, hi, name, text, outcome):
    return {"id": id, "name": name, "text": text, "type": "Lore", "minDepth": lo, "maxDepth": hi, "weight": 1,
            "options": [{"label": "Go on", "requires": "", "cost": [],
                         "outcomes": [{"weight": 1, "text": outcome,
                                       "gain": [{"op": "Nothing", "arg": "", "amount": 0, "amount2": 0}]}]}]}

for ev in [
    lore("names_in_wall", 1, 2, "Names in the Wall", "Names cut into the wall, one under another.", "The last one is fresh. There is room below it."),
    lore("warm_roots", 3, 4, "Warm Roots", "Roots push through the ceiling. They are warm, and hum.", "You feel the hum in your teeth."),
    lore("pinned_note", 5, 5, "A Pinned Note", "A note pinned to a bench: 'Plant it. Do not climb back.'", "You fold it away."),
]:
    if ev["id"] not in E: d["events"].append(ev)

p.write_text(json.dumps(d, indent=1, ensure_ascii=False), encoding="utf-8")
EOF
git diff --stat Athanor/Assets/Resources/content.json
```
Expected: one file changed; the diff shows only event text lines and the three new events (no reformatting elsewhere).

- [ ] **Step 4: Run, see them pass; run everything**

Run: `dotnet test tools/CoreTests` → Expected: 267 passed, 0 failed.

- [ ] **Step 5: Commit**

```bash
git add Athanor/Assets/Resources/content.json Athanor/Assets/Tests/EditMode/ContentTests.cs Athanor/Assets/Tests/EditMode/Phase3Tests.cs
git commit -m "Content: lore pass on the events, three new Lore events"
```

---

### Task 8: Balance check after the lore

**Files:**
- Modify: `docs/devlogs/phase-4-lore.md` (end), `docs/devlogs/phase-4-prep.md` (Log)

- [ ] **Step 1: Rerun the balance matrix**

```bash
cd tools/CoreTests
dotnet test --filter BalanceMatrix -l "console;verbosity=detailed" > /tmp/balance-after.txt
cd ../..
diff <(grep -i "win" /tmp/balance-before.txt) <(grep -i "win" /tmp/balance-after.txt)
```
Expected: every win rate within 3 points of the baseline (the lore events only thin the event draw).
If a row moves more than 3 points: stop, and report the row to the owner; do not retune in this plan.

- [ ] **Step 2: Record it** — append to `docs/devlogs/phase-4-lore.md`:

```markdown
## Log

- 2026-11-13: Event texts and three Lore events shipped in content.json. BalanceMatrix after: <rows>; baseline in phase-4-prep.md.
```
Replace `<rows>` with the win-rate rows from `/tmp/balance-after.txt`.

- [ ] **Step 3: Commit**

```bash
git add docs/devlogs/phase-4-lore.md
git commit -m "Docs: balance check after the lore events"
```

---

### Task 9: The opening cutscene in three pages (Unity)

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs` (method `Intro()`)

No automated test covers cutscene text; it is checked by playing it (Step 3).

- [ ] **Step 1: Replace `Intro()`** with:

```csharp
        /// <summary>The opening story, three lines a page (docs/devlogs/phase-4-lore.md). The last page is the original text.</summary>
        static readonly string[][] IntroPages =
        {
            new[]
            {
                "Under the old workshop, the cellar keeps going down.",
                "No one dug it. It grew, one Keeper at a time.",
                "Each went down with a satchel, a bench, and a Book.",
            },
            new[]
            {
                "At the bottom they made something, and planted it.",
                "The last Keeper went down and did not come back up.",
                "The bench is yours now. The Book is empty.",
            },
            new[]
            {
                "Each depth asks for a stronger brew. Nothing down here is labelled.",
                "The Book will remember what your hands forget.",
                "So mix. Watch what happens. Remember.",
            },
        };

        /// <summary>Starts and ends on a black screen.</summary>
        public IEnumerator Intro()
        {
            Begin();
            ScreenFx.I.SetBlack(1);
            Stage(true, true);
            embers = 1;
            yield return ScreenFx.I.FadeIn(0.9f);
            ClearSkip();
            for (int p = 0; p < IntroPages.Length; p++)
            {
                if (p > 0)
                {
                    // the page before fades out, then the lines are reused
                    yield return Anim.Over(0.35f, t => { foreach (var l in lines) l.Alpha = 1 - t; });
                    foreach (var l in lines) { l.Text = ""; l.Alpha = 1; }
                }
                var page = IntroPages[p];
                bool last = p == IntroPages.Length - 1;
                for (int i = 0; i < page.Length; i++)
                {
                    yield return Type(lines[i], page[i]);
                    yield return Hold(i < page.Length - 1 ? 0.8f : last ? 0.3f : 1.6f);
                }
            }
            prompt.Text = "CLICK TO BEGIN";
            yield return Hold(8);
            End();
            yield return ScreenFx.I.FadeOut(0.4f);
            Close();
        }
```

- [ ] **Step 2: Compile and run Unity tests**

Run: `unity test ./Athanor --mode EditMode`
Expected: no compile errors; 256 + the new Core tests (the EditMode assembly includes `Tests/EditMode`) passed, 6 skipped.

- [ ] **Step 3: Play it**

In the Editor: delete the key `athanor.intro.v1` (Edit > Clear All PlayerPrefs, or `PlayerPrefs.DeleteKey`), press Play,
press Begin. Expected: three pages, each typed line by line; a click finishes a line, then ends a wait; no line is cut or
wraps at 640x360; "CLICK TO BEGIN" after page 3. Take a screenshot of each page into `docs/devlogs/phase-4-shots/`.

- [ ] **Step 4: Commit**

```bash
git add Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs docs/devlogs/phase-4-shots
git commit -m "Game: opening cutscene in three pages (lore)"
```

---

### Task 10: The tutorial save key (Unity)

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Boot/Save.cs`
- Test: `Athanor/Assets/Tests/EditModeGame/SaveTests.cs`

- [ ] **Step 1: Write the failing test** — add to `SaveTests`:

```csharp
        [Test]
        public void TutorialDone_DefaultsToFalse_AndRoundTrips()
        {
            bool had = PlayerPrefs.HasKey(Save.TutorialKey);
            int kept = PlayerPrefs.GetInt(Save.TutorialKey, 0);
            try
            {
                PlayerPrefs.DeleteKey(Save.TutorialKey);
                Assert.IsFalse(Save.TutorialDone);
                Save.TutorialDone = true;
                Assert.IsTrue(Save.TutorialDone);
            }
            finally
            {
                if (had) PlayerPrefs.SetInt(Save.TutorialKey, kept); else PlayerPrefs.DeleteKey(Save.TutorialKey);
            }
        }
```

- [ ] **Step 2: Run, see it fail**

Run: `unity test ./Athanor --mode EditMode`
Expected: compile error `'Save' does not contain a definition for 'TutorialKey'`.

- [ ] **Step 3: Implement** — in `Save.cs`, extend the key line and add a property after `IntroSeen`:

```csharp
        public const string Key = "athanor.journal.v1", IntroKey = "athanor.intro.v1", ProfileKey = "athanor.profile.v1",
            TutorialKey = "athanor.tutorial.v1";
```
```csharp
        /// <summary>The tutorial was played to its end (or skipped). See TutorialScript.ShouldRun.</summary>
        public static bool TutorialDone
        {
            get => PlayerPrefs.GetInt(TutorialKey, 0) == 1;
            set { PlayerPrefs.SetInt(TutorialKey, value ? 1 : 0); PlayerPrefs.Save(); }
        }
```

- [ ] **Step 4: Run, see it pass**

Run: `unity test ./Athanor --mode EditMode` → Expected: all passed, 6 skipped.

- [ ] **Step 5: Commit**

```bash
git add Athanor/Assets/Scripts/Game/Boot/Save.cs Athanor/Assets/Tests/EditModeGame/SaveTests.cs
git commit -m "Game: tutorial save key"
```

---

### Task 11: Wrap up

**Files:**
- Modify: `docs/devlogs/README.md`, `docs/devlogs/phase-4-prep.md`

- [ ] **Step 1: Full test runs**

Run: `dotnet test tools/CoreTests` → Expected: 267 passed, 0 failed.
Run: `unity test ./Athanor --mode EditMode` → Expected: all passed, 6 skipped.

- [ ] **Step 2: Devlog index** — in `docs/devlogs/README.md`, change the tutorial row's status and add the lore row:

```markdown
| 4 | [phase-4-tutorial-script.md](phase-4-tutorial-script.md) | Script v2; Core hooks and numbers built, Unity controller next |
| 4 | [phase-4-lore.md](phase-4-lore.md) | Lore draft; events and opening cutscene built |
```

- [ ] **Step 3: Prep log** — add under "## Log" in `docs/devlogs/phase-4-prep.md`:

```markdown
- 2026-11-13: Plan `2026-11-13-athanor-phase4-tutorial-lore.md` done: Deal, QuietBefore, FirstOffer, Preview, ScoreParts,
  TutorialScript with TutorialTests; event lore pass and 3 Lore events; intro in three pages; tutorial save key.
  Next: the Unity tutorial controller plan.
```

- [ ] **Step 4: Commit**

```bash
git add docs/devlogs/README.md docs/devlogs/phase-4-prep.md
git commit -m "Docs: phase 4 part 1 done"
```

---

### Task 12: Redraw as its own button (Unity)

Owner decision 8 (`phase-4-tutorial-script.md`): Redraw is its own button, the redraws-left counter sits directly above it,
and the button is greyed and cannot be clicked at 0. Today there is no button: `HudView.Build` puts an invisible 78x32
click zone (`Make.Root("redraw")` + `Make.Click`, `HudView.cs` lines 67 to 69) over the counter (`redraws` at 611,48 and
`redrawsLabel` at 556,63, lines 65 and 66), `HudView.Show` only dims the counter at 0 (lines 111 and 112), and the
tooltip in `BenchView.cs` (lines 307 to 311) points at `Px.At(591, 46)`. Script step 3.0 highlights this button.

The counter stays where it is, in the right half of `hud_right` (480,8, 152x76). The button goes right under the panel,
under the counter: art-space 556,88, 76x20 (556 + 76 = 632, the panel's right edge; the workshop table starts at x 102 and
ends at 538, so nothing else is there). `UiButton` is 120x26 and has no disabled look, so it is not reused; the button is
drawn from `Make.Rect` (no new sprite, so `ArtLibrary.sprites` and `SceneBuilder` are untouched) and handled in `HudView`
the way Ignite is (`canIgnite` / `igniteDown`). The greyed look follows `CanRedraw(run)` only, not `inputEnabled`, so the
button does not flash grey during every reaction, and stays lit while step 3.0 points at it. Clicks are still gated by
`inputEnabled` in `BenchView` (line 53) and by `GameFlow` (line 75), as today. `layout.json` has no HUD keys (the HUD is
placed by constants in `HudView`), so it is not changed.

**Files:**
- Modify: `Athanor/Assets/Scripts/Game/Bench/HudView.cs`
- Modify: `Athanor/Assets/Scripts/Game/Bench/BenchView.cs` (Redraw tooltip anchor)
- Create: `Athanor/Assets/Tests/EditModeGame/HudTests.cs`

- [ ] **Step 1: Write the failing test** — create `Athanor/Assets/Tests/EditModeGame/HudTests.cs`:

```csharp
using Athanor.Core;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>The Redraw button is live only while a redraw is left and the bench is in play.</summary>
    public class HudTests
    {
        static ContentDb Db() => new ContentDb(JsonUtility.FromJson<ContentData>(Resources.Load<TextAsset>("content").text));

        [Test]
        public void CanRedraw_UntilTheLastRedrawIsUsed()
        {
            var r = new RunState(Db(), new Journal(), 5);
            Assert.AreEqual(RunPhase.Playing, r.Phase);
            for (int i = RunState.BaseRedraws; i > 0; i--)
            {
                Assert.IsTrue(HudView.CanRedraw(r));
                Assert.IsTrue(r.Redraw());
            }
            Assert.AreEqual(0, r.RedrawsLeft);
            Assert.IsFalse(HudView.CanRedraw(r));
        }

        [Test]
        public void CanRedraw_IsFalse_OffTheBench_EvenWithRedrawsLeft()
        {
            var db = Db();
            // a full run opens depth 1 on an aspect offer (QuietBefore is 0 unless set)
            var r = new RunState(db, new Journal(), 5, 1, RunOptions.Full(Unlocks.Fresh(db)));
            Assert.AreEqual(RunPhase.AspectOffer, r.Phase);
            Assert.Greater(r.RedrawsLeft, 0);
            Assert.IsFalse(HudView.CanRedraw(r));
        }

        [Test]
        public void CanRedraw_IsFalse_WithNoRun() => Assert.IsFalse(HudView.CanRedraw(null));
    }
}
```

- [ ] **Step 2: Run, see it fail**

Run: `unity test ./Athanor --mode EditMode`
Expected: compile error `'HudView' does not contain a definition for 'CanRedraw'`.

- [ ] **Step 3: Implement in `HudView.cs`**

3a. Under `public const int MaxCatalysts = 6, MaxAspects = 3;` add:

```csharp
        /// <summary>The Redraw button in art space: under the right panel, right below the redraws-left counter.</summary>
        public const int RedrawX = 556, RedrawY = 88, RedrawW = 76, RedrawH = 20;
```

3b. Extend the field lines. Replace

```csharp
        float shown, bump; long goal, targetValue; bool canIgnite, igniteDown;
```
with
```csharp
        float shown, bump; long goal, targetValue; bool canIgnite, igniteDown, canRedraw, redrawDown;
        SpriteRenderer redrawFill;
        Label redrawText;
```

and replace

```csharp
        static readonly Color Dim = Px.Hex("#a88a6a"), Pale = Px.Hex("#f0dcc0"), Gold = Px.Hex("#f8d250"), Ink = Px.Hex("#3a2212");
```
with
```csharp
        static readonly Color Dim = Px.Hex("#a88a6a"), Pale = Px.Hex("#f0dcc0"), Gold = Px.Hex("#f8d250"), Ink = Px.Hex("#3a2212");
        static readonly Color RedrawUp = Px.Hex("#1d5a52"), RedrawOver = Px.Hex("#26706a"), RedrawPressed = Px.Hex("#143f3a"),
            RedrawOff = Px.Hex("#3a2e28");
```

3c. Under `public Vector3 AspectAnchor(int i) => ...;` add:

```csharp
        /// <summary>Where the Redraw tooltip (and the tutorial's highlight) points: the button's centre.</summary>
        public Vector3 RedrawAnchor => Px.Mid(RedrawX, RedrawY, RedrawW, RedrawH);
        /// <summary>The Redraw button is live: a redraw is left and the bench is being played (not an offer, not a cleared depth).</summary>
        public static bool CanRedraw(RunState run) => run != null && run.Phase == RunPhase.Playing && run.RedrawsLeft > 0;
```

3d. In `Build`, replace the five redraw lines

```csharp
            redrawsLabel = Make.Text(t, "REDRAWS", 556, 63, FontKind.Tiny, 8, Dim, O + 3);
            redraws = Make.Text(t, "", 611, 48, FontKind.Title, 32, Px.Hex("#6ee6c8"), O + 3, Align.Left, dark);
            var redrawZone = Make.Root("redraw", t); redrawZone.position = Px.Mid(552, 46, 78, 32);
            var rc = Make.Click(redrawZone.gameObject, new Vector2(78, 32), O, () => RedrawClicked?.Invoke());
            rc.onEnter = () => RedrawHovered = true; rc.onExit = () => RedrawHovered = false;
```
with
```csharp
            redrawsLabel = Make.Text(t, "REDRAWS", 556, 63, FontKind.Tiny, 8, Dim, O + 3);
            redraws = Make.Text(t, "", 611, 48, FontKind.Title, 32, Px.Hex("#6ee6c8"), O + 3, Align.Left, dark);
            // the Redraw button, right under the counter: a dark one-pixel edge round a fill that greys out at zero
            Make.Rect(t, RedrawX, RedrawY, RedrawW, RedrawH, dark, O + 1);
            redrawFill = Make.Rect(t, RedrawX + 1, RedrawY + 1, RedrawW - 2, RedrawH - 2, RedrawUp, O + 2);
            redrawText = Make.Text(t, "Redraw", RedrawX + RedrawW / 2f, RedrawY, FontKind.Title, 16, Pale, O + 4, Align.Centre, dark);
            var redrawZone = Make.Root("redraw", t); redrawZone.position = RedrawAnchor;
            var rc = Make.Click(redrawZone.gameObject, new Vector2(RedrawW, RedrawH), O, () => { if (canRedraw) RedrawClicked?.Invoke(); });
            rc.onEnter = () => RedrawHovered = true;
            rc.onExit = () => { RedrawHovered = false; redrawDown = false; };
            rc.onDown = () => redrawDown = true; rc.onUp = () => redrawDown = false;
```

3e. In `Show`, replace

```csharp
            redraws.Alpha = redrawsLabel.Alpha = run.RedrawsLeft > 0 ? 1 : 0.35f;
```
with
```csharp
            // the button follows the run, not inputEnabled: it stays lit while a reaction plays (clicks are gated in BenchView)
            canRedraw = CanRedraw(run);
            redraws.Alpha = redrawsLabel.Alpha = canRedraw ? 1 : 0.35f;
```

3f. In `Update`, after the last Ignite line (`igniteText.transform.position = ...;`) add:

```csharp
            redrawFill.color = !canRedraw ? RedrawOff : redrawDown ? RedrawPressed : RedrawHovered ? RedrawOver : RedrawUp;
            redrawText.Alpha = canRedraw ? 1 : 0.45f;
            redrawText.transform.position = Px.At(RedrawX + RedrawW / 2f, RedrawY + (canRedraw && redrawDown ? 2 : 0));
```

- [ ] **Step 4: Point the tooltip at the button** — in `BenchView.cs`, in the `Hud.RedrawHovered` branch, replace

```csharp
                key = "redraw"; title = "Redraw"; anchor = Px.At(591, 46);
```
with
```csharp
                key = "redraw"; title = "Redraw"; anchor = Hud.RedrawAnchor;
```
The body text stays ("Swap the cards in your hand for new ones. Click to use one." / "None left at this depth.").

- [ ] **Step 5: Run, see it pass**

Run: `unity test ./Athanor --mode EditMode`
Expected: no compile errors; all passed (3 more than before), 6 skipped.

- [ ] **Step 6: Look at it** — in the Editor, press Play and start a run:
  - The button reads "Redraw", sits under the right panel with the counter (2) right above it, and no text is cut at 640x360.
  - It brightens under the pointer and the label sinks 2 px when pressed; a click spends one redraw and deals a new hand.
  - After the second redraw the counter shows 0 and the button is grey; clicking it does nothing (the hand stays).
  - It is grey on an aspect or event offer and after "Depth Cleared"; the next depth lights it again with 2.
  - Hovering it shows the Redraw tooltip near the button, inside the screen.
  - If "Redraw" sits too high or low in the box, change only the `RedrawY` offset in `redrawText` (Build and Update).
  Take a screenshot (lit and grey) into `docs/devlogs/phase-4-shots/`.

- [ ] **Step 7: Commit**

```bash
git add Athanor/Assets/Scripts/Game/Bench/HudView.cs Athanor/Assets/Scripts/Game/Bench/BenchView.cs \
  Athanor/Assets/Tests/EditModeGame/HudTests.cs Athanor/Assets/Tests/EditModeGame/HudTests.cs.meta docs/devlogs/phase-4-shots
git commit -m "Game: Redraw as its own button, greyed at zero"
```

---

### Task 13: Lore events are not shown twice in a row (Core)

Owner decision (2026-11-13, `phase-4-lore.md`): a Lore event is marked as seen, so it is not shown twice in a row.
The profile remembers the id of the last Lore event the player was shown (`Profile.LastLore`, saved). A run starts from it
(`RunOptions.LastLore`) and keeps it up to date itself (`RunState.LastLore`), so a run also never shows the same Lore event
at two depths in a row. `DrawEvent` leaves that event out: when the roll lands on its slot, no event comes, and the event
dice are rolled once more exactly as walking past it would have rolled them. Every other event keeps its exact chance and
the run's later draws are the same, so this is **balance-neutral by construction**: `BalanceMatrix` must print the same
rows as before, and `SkippingALoreEvent_IsBalanceNeutral` checks it run by run.
"In a row" means: the last Lore event seen comes back only after another Lore event has been seen.

**Files:**
- Modify: `Athanor/Assets/Scripts/Core/ContentData.cs` (class `ProfileData`)
- Modify: `Athanor/Assets/Scripts/Core/Profile.cs` (class `Profile`, class `RunOptions`)
- Modify: `Athanor/Assets/Scripts/Core/RunStart.cs` (`DrawEvent`, a field and a property)
- Modify: `Athanor/Assets/Scripts/Game/Boot/GameFlow.cs` (`PlayRun`, `StartOfDepth`; Step 6)
- Create: `Athanor/Assets/Tests/EditMode/LoreTests.cs`

- [ ] **Step 0: Baseline the balance report** (unless `/tmp/balance-after.txt` from Task 8 is still there)

```bash
cd tools/CoreTests
dotnet test --filter BalanceMatrix -l "console;verbosity=detailed" > /tmp/balance-before-13.txt
cd ../..
```

- [ ] **Step 1: Write the failing tests** — create `Athanor/Assets/Tests/EditMode/LoreTests.cs`:

```csharp
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
```

- [ ] **Step 2: Run, see them fail**

Run: `dotnet test tools/CoreTests --filter LoreTests`
Expected: build error `'RunOptions' does not contain a definition for 'LastLore'`.

- [ ] **Step 3: The profile and the options** — in `ContentData.cs`, inside `ProfileData`, after `public RewardLogData[] rewards;`:

```csharp
        public string lastLore;
```

In `Profile.cs`, inside `Profile`, after `public int RewardSeed;`:

```csharp
        /// <summary>The id of the last Lore event the player was shown (kept across runs), or null. A run leaves it out of its next draw.</summary>
        public string LastLore;
```

After `RunStarted()`:

```csharp
        /// <summary>Call when an event is shown. Only Lore events are remembered.</summary>
        public void EventShown(EventData e) { if (e != null && e.type == "Lore") LastLore = e.id; }
```

In `ToData()`, after `rewards = rewards.ToArray(),` add `lastLore = LastLore,`. In `FromData`, after the object initialiser
(`var p = new Profile(...) { ... };`):

```csharp
            if (!string.IsNullOrEmpty(d.lastLore) && (db == null || db.Events.Any(e => e.id == d.lastLore && e.type == "Lore"))) p.LastLore = d.lastLore;
```

In `RunOptions`, after `FirstOffer`:

```csharp
        /// <summary>The Lore event seen last (Profile.LastLore): not drawn again until another Lore event has been seen. Null: none.</summary>
        public string LastLore;
```

- [ ] **Step 4: The draw** — in `RunStart.cs`, add next to `List<string> reachable;`:

```csharp
        string lastLore;

        /// <summary>The last Lore event seen: this run's latest, or the one the run started with.</summary>
        public string LastLore => lastLore ?? options.LastLore;
```

In `DrawEvent()`, replace everything from `int total = ...` to the end of the method with:

```csharp
            int total = Math.Max(0, db.NoEventWeight) + pool.Sum(e => e.weight);
            if (total <= 0) return;
            int roll = eventRng.Range(total) - Math.Max(0, db.NoEventWeight);
            if (roll < 0) return;
            foreach (var e in pool)
            {
                if (roll < e.weight) { CurrentEvent = e; break; }
                roll -= e.weight;
            }
            if (CurrentEvent == null) return;
            if (CurrentEvent.type == "Lore" && CurrentEvent.id == LastLore)
            {
                // seen last time: no event. Roll the outcome dice as walking past it would have, so every later draw is
                // the same as if it had been shown (balance-neutral; a Lore event does nothing).
                var walk = CurrentEvent.options.FirstOrDefault(CanTake);
                if (walk != null) eventRng.Range(walk.outcomes.Sum(x => x.weight));
                CurrentEvent = null;
                return;
            }
            if (CurrentEvent.type == "Lore") lastLore = CurrentEvent.id;
            eventOptions = CurrentEvent.options.Where(CanTake).ToList();
```

The `ForceEvent` branch above stays as it is (balance checks that force an event are not affected).

- [ ] **Step 5: Run, see them pass; run everything; the balance report is unchanged**

Run: `dotnet test tools/CoreTests --filter LoreTests` → Expected: 8 passed.
Run: `dotnet test tools/CoreTests` → Expected: 0 failed; 8 more passed than before this task (275 after Task 7's 267).

```bash
cd tools/CoreTests
dotnet test --filter BalanceMatrix -l "console;verbosity=detailed" > /tmp/balance-after-13.txt
cd ../..
diff <(grep -i "win" /tmp/balance-before-13.txt) <(grep -i "win" /tmp/balance-after-13.txt) && echo "balance unchanged"
```
Expected: `balance unchanged`. If any row differs: stop and report it; the skip path is meant to leave every draw the same.

- [ ] **Step 6: Hook it up in the game** — in `GameFlow.PlayRun`, replace the `Run = new RunState(...)` line with:

```csharp
            var options = RunOptions.Full(unlocks);
            options.LastLore = Profile.LastLore;
            Run = new RunState(Db, Journal, System.Environment.TickCount * 31 + runs, 1, options);
```

In `StartOfDepth`, in the event branch, before `yield return eventView.Show(...)`:

```csharp
                    Profile.EventShown(Run.CurrentEvent);
                    Save.StoreProfile(Profile);
```

Run: `unity test ./Athanor --mode EditMode` → Expected: no compile errors, all passed, 6 skipped.
Look at it (Editor, Play): start a run, take "Go on" at A Keeper's Room; quit to the title; start again: A Keeper's Room
does not come back at depth 1 or 2 until another Lore event has been shown (F9 autoplay with the console open shows the
events per depth quickly).

- [ ] **Step 7: Record it and commit** — in `docs/devlogs/phase-4-lore.md`, under "## Log" (create it if Task 8 has not):

```markdown
- 2026-11-13: Lore events are not shown twice in a row (Profile.LastLore, RunOptions.LastLore, DrawEvent). BalanceMatrix unchanged.
```

```bash
git add Athanor/Assets/Scripts/Core/ContentData.cs Athanor/Assets/Scripts/Core/Profile.cs Athanor/Assets/Scripts/Core/RunStart.cs \
  Athanor/Assets/Tests/EditMode/LoreTests.cs Athanor/Assets/Scripts/Game/Boot/GameFlow.cs docs/devlogs/phase-4-lore.md
git commit -m "Core: a Lore event is not shown twice in a row"
```
(Unity creates `LoreTests.cs.meta` on its first import; add it too if it is there.)

---

## Not in this plan (next plan: Unity tutorial controller)

Written once the owner has approved script v2 and Task 5's numbers hold:
a `TutorialController` in `Game/Boot` that `GameFlow.PlayRun` uses when `TutorialScript.ShouldRun(Save.TutorialDone,
Journal.KnownPairs.Count)`; it deals each `TutorialTrial`, lights the tiles in `Lay`, locks every other click, holds the
score popup split by `ScoreParts`, plays the chain one step per click, shows `Preview(BrokenChain())` beside the real
chain, points at the Redraw button (`HudView.RedrawAnchor`, Task 12) at 3.0 without unlocking it, holds goal toasts
until 5.5, sets `Save.TutorialDone`; plus the title's "Skip tutorial" question and the "How to play" replay on a
throwaway journal and profile.
