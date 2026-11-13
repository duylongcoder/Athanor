# Phase 3: unlocks, aspects and depth events

Dates: 2026-11-12 to 2026-11-13. Spec: `docs/superpowers/specs/2026-11-12-athanor-phase3-design.md` (its
section 16, "As built", lists every place the game differs from the sections before it).
Product decisions: `phase-3-plan.md` (owner rounds 1 to 3). No task plan was written; the spec's section 15 was
the order of work.

## Goal

Phase 2 made one run deep. Phase 3 gives reasons to start another: a profile that grows (goals that unlock
catalysts and starting materials), runs that differ (aspects, depth events) and more to discover.

## What shipped

- **Profile and goals** (`Core/Profile.cs`, `Game/Boot/Save.cs`). Ten stats kept across runs, 22 goals, one per
  locked item. A goal pays a random item from its own bucket (kind and rarity) and names only that bucket until
  it is earned. Failed runs count. The content does not load when a bucket has more goals than locked items.
  Saved under `athanor.profile.v1`; a damaged save gives a fresh profile. Reset progress (title) erases the
  profile and keeps the journal.
- **Catalysts**: 26 (10 old, 16 new), in four rarities. 9 are open on a fresh profile, 17 are won.
- **Starting materials**: 13 (8 old, 5 new and locked). The ones a player has won wait in a **pouch**: every
  hand draws one of them on top of the usual five, so they never thin the satchel.
- **Recipes**: 24 new discoveries and 28 new recipes (81 materials, 87 recipes). Every new recipe needs a won
  starter, so a fresh profile plays the 59 recipes of Phase 2, which a test pins.
- **Aspects**: 16 run-long bench modifiers, pick one of three at the start of a run and of depths 3 and 5.
  All 16 were built, the five that change which tiles touch included. Three are trade-offs.
- **Depth events**: 16, one drawn (or none) at the start of every depth, after the aspect.
- **Engine**: `BenchRules` (shut tiles, warm tiles, joined tiles, pivot, kiln, crossroads) is read by the one
  planner that both the game and the scripted player use.
- **Screens**: the three-card chooser also offers aspects; event panel; Goals page; goal toast; title with Goals
  and Reset progress; run summary with the goals of the run; journal on two pages; rarity pips; tile gems, lids
  and joins; the aspects held and this depth's changes under the depth panel.
- **Art**: every new icon comes from `tools/art` (29 materials, 16 catalysts, 16 aspects, 7 event types and the
  new interface pieces).
- **Bot and reports**: the scripted player picks aspects and event options. `BalanceMatrix` (win rate by
  profile), `EventReport` (what each event costs, and the random player), `RoleReport` (whole careers of two
  simulated players).
- **Tools**: `WebBuild.BuildDev` makes a development build in `Build/web-dev`, where F9 hands the bench to the
  scripted player; `tools/web/autoplay.mjs` lets it play in headless Chrome and photographs the game.

## Balance

Informed bot (knows every recipe), 200 seeds, final rules. Owner targets: fresh 60 to 70%, full 80 to 90%,
random player near 0%.

| Profile | No aspects or events | With aspects and events |
|---|---|---|
| Fresh | 132 (66%) | 135 (67%) |
| Fresh and every catalyst | 164 (82%) | 170 (85%) |
| Fresh and every starter | 134 (67%) | 145 (72%) |
| Half unlocked | 164 (82%) | 163 (81%) |
| Full | 166 (83%) | 170 (85%) |

- Aspects alone: 144 (72%). Events alone: 128 (64%). Together they cancel, so a fresh profile sits where the
  owner asked without any number in the content being moved.
- The random player wins 0 of 200 on a fresh profile and 0 of 200 with everything unlocked; about 94% of those
  runs end at depth 1.
- A fresh profile loses mostly at depths 3 and 4 (28 and 31 of 200). With no catalyst at all the bot wins 83 of
  200, and depth 3 is where it stops (94 of the 117 losses).

### Events

The spec's rule: no event may make a depth impossible; met once, at any depth it may come to, it must leave at
least 70% of the wins a run with no event has. Baseline: 144 of 200 (fresh profile, aspects on, no events).

| Event | Once, by depth (wins of 200) | At every depth it may come to |
|---|---|---|
| Damp Air (hazard) | d2 153, d3 144, d4 130, d5 143 | 120 (83%) |
| Falling Grit (hazard) | d2 150, d3 152, d4 120, d5 139 | 114 (79%) |
| Cold Draft (hazard) | d3 117, d4 113, d5 142 | 80 (55%) |
| Dripping Well (gamble) | d1 137, d2 148, d3 149, d4 141, d5 144 | 119 (82%) |
| Shifting Bench (gamble) | 144 at every depth (the bot walks on) | 144 (100%) |
| Smuggler's Crate (gamble) | d3 158, d4 142, d5 143 | 147 (102%) |

- The lowest single meeting is Cold Draft at depth 4: 113 of 144, 78%. Every event passes.
- **Falling Grit failed this rule when first measured**: at depth 4 it left 95 of 144 (65%). A shut edge takes a
  neighbour from the centre tile, and a shut centre takes the big recipes away. An event now never shuts the
  centre, and a corner is twice as likely as an edge. Depth 4 is 120 (83%).
- Cold Draft three depths in a row (55%) is the worst a run can draw. The draw makes that all but impossible
  (about 1 chance in 30 at each depth), and the rule is about one meeting, so it was left alone. It is the first
  thing to soften if depths 3 and 4 feel unfair.
- The other ten events, at every depth they may come to: between 139 and 157, except Hungry Furnace at 120. That
  one is the bot's doing (it feeds the furnace a discovery it needed), not a forced loss; a player can walk on.

### Two players, whole careers (`RoleReport`)

The owner's question: how far apart are a player who knows every recipe (the admin) and one who learns them by
trying (the newcomer), and how many runs does each need to make the last substance once? Twelve simulated
players of each kind, every one with a profile and a journal that persist from run to run, on the whole game
(unlocks, aspects, events). The newcomer experiments, reads the journal's hints and repeats what it has found.

| | Admin (60 runs each) | Newcomer (600 runs each) | Newcomer told every recipe up to tier 4 |
|---|---|---|---|
| First Ultimate, of those who made it | run 1 (median); slowest run 4 | run 371 (median); fastest 175, slowest 474 | run 184 (median); fastest 23, slowest 466 |
| Never made it | 0 of 12 | 6 of 12 in 600 runs | 4 of 12 in 600 runs |
| Win rate, all runs | 79% | 9% | 23% |
| Win rate over time | 73% in runs 1 to 5, about 80% after | 0% until run 160, 3% by run 200, 19% in runs 501 to 600 | 0% until run 20, 10% by run 80, about 31% from run 400 |
| Average depth reached | 3.94 | 3.26 (1.8 in the first five runs) | 3.57 |
| Where runs are lost (depth 1 to 5) | 4 / 7 / 31 / 56 / 2% | 1 / 10 / 59 / 29 / 0% | 1 / 5 / 48 / 44 / 1% |
| Journal entries of 87 | 37 after 5 runs, 55 after 40 | 19 after 5, 36 after 40, 50 after 200, 57 after 600 | 84 from the start |
| Goals done of 22 | 13.5 after 5 runs, 19.1 after 40 | 5.7 after 5, 14.3 after 40, 18.7 after 600 | 10.8 after 5, 20.0 after 600 |

What it says:

- **The gap is knowledge, and it is very wide.** The admin wins four runs in five from the first evening. The
  newcomer does not win at all for about 160 runs and is still under one win in five after 600.
- **The wall is depth 3.** Six newcomer losses in ten are there. The newcomer finds the simple recipes fast (23
  of 26 that make tier 1, 22 of 25 for tier 2) and then stalls: about 9 of 18 for tier 3 and 4 of 15 for tier 4
  after 600 runs. Depth 3 asks for 7,500, which needs those.
- **Knowing the recipes is not enough either.** A newcomer handed every recipe except the last substance's still
  takes 184 runs (median) to make it, and four of twelve never do: each attempt at the Ultimate costs two
  tier-4 materials, which must be built first, in the same run.
- **Aspects and events do not move the admin**: 79% with them, 76% without.
- Twelve players is a small sample: an earlier run of the same report gave the newcomer a median of 447 with 7
  of 12 never making it. Read "hundreds of runs, and about half never" rather than the exact figure.
- The newcomer is a model, and a cautious one: it does not reason about Kinds or tiers the way a person does.
  A real player will do better. The shape should hold: quick early finds, then a long plateau before tier 3.

## Found and fixed in the last pass (2026-11-13)

Found by looking at the web build in headless Chrome and by running the reports, after the tests were green:

- **Goals page**: the goal, its reward and the progress bar were drawn on top of each other. A row is now two
  clear lines, and the reward line is short enough to end before the bar.
- **Hand of more than five cards** (the usual hand once a starter is won): names were cut by the next card. The
  name and icon now sit in the part of the card that shows, and the card pointed at comes to the front.
- **Run summary**: the line naming the goals of the run ran out of the dialog. It now fits, and says "10 GOALS"
  when not even one name fits beside the count.
- **A win could be lost**: the end of a run was counted after the ending cutscene, so closing the page during
  it lost the win and its goals. It is now counted and saved with the Ignite that ended the run.
- **Balance report**: `BalanceReport` (Phase 2) no longer ran, because it removes catalysts and the content then
  has goals with nothing to pay. It drops the goals too.
- **Falling Grit** at depth 4, above.
- **Potency over 10,000** was written across the target beside it (so at depth 4, always; this was already so in
  Phase 2). A number that long now takes the place of the word "POTENCY".

## Verification

- Rules: `dotnet test tools/CoreTests` 245 of 245. In Unity (edit mode): 256 pass, 6 skipped (the reports, which
  run only when asked for).
- Release web build: 10 MB. `tools/web/webtest.mjs` in headless Chrome: 0 console errors; an
  aspect taken, an event taken, a first trial played with real mouse events, the journal and goals kept across a
  reload, Reset progress asked and done, a damaged journal and profile start clean.
- Autoplay build, two runs by the scripted player: 0 console errors. Seen in its pictures: goal toasts, tile
  gems, the run summary (lost and won), the Goals page with finished goals, a six-card hand.
- Pictures in `phase-3-shots/` (the ones marked "Development Build" in a corner are from the autoplay build):
  [the aspect choice](phase-3-shots/aspect-choice.png), [an event](phase-3-shots/event-panel.png),
  [a goal toast](phase-3-shots/goal-toast.png), [the Goals page](phase-3-shots/goals-page.png),
  [the run summary](phase-3-shots/run-summary.png), [a hand of eight with one card pointed
  at](phase-3-shots/big-hand-card-pointed-at.png), [Reset progress asking](phase-3-shots/reset-asks.png).
- Not done: nobody but the author read the code. Nothing is committed to git. Sound is still not in the game.
  The game was not played by hand in the editor on this machine, only in the browser by script.

## Open for the owner

1. **A newcomer needs a very long time to reach the last substance** (the table above). The informed player
   makes it in the first run or two. This is the largest gap Phase 3 leaves, and Phase 4 plans more than one
   Ultimate.
2. **Goal numbers are as drafted.** The informed player has about 19 of 22 goals after 40 runs. "Complete the
   journal" asks for all 87 recipes, the ones behind every locked starter included.
3. **Cold Draft** drawn at depths 3, 4 and 5 of one run halves the bot's wins.
4. **Seven or more cards in hand** (Greedy Bench, Tilted Workbench, the satchel catalysts together) cut long
   names until the card is pointed at.
5. The spec's "each rarity pays" check (win rate rising with the rarity of the catalysts held) was not measured
   bucket by bucket; only "every catalyst" against "none won" was (85% against 67%).

## Log

- 2026-11-12: brainstorm, plan (three owner rounds), spec.
- 2026-11-13: Core (profile, goals, unlocks, bench rules, aspects, events), content, icons, bot, the reports;
  the pouch replaced the two-copy satchel after the bot fell to 1%; screens; web build.
- 2026-11-13, last pass: browser checks, the fixes above, final measurements, release build.
