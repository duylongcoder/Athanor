---
generated: true
type: code
layer: Core
kind: class
namespace: Athanor.Core
file: Athanor/Assets/Scripts/Core/RunStart.cs
tags: [code/core, kind/class]
aliases: []
---

# RunState

`public sealed partial class RunState`
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/RunStart.cs:33-412` (+ partial: `Athanor/Assets/Scripts/Core/RunState.cs:20`)

> One run: descending through depths, spending trials, collecting catalysts.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public string LastLore` | 48 | The last Lore event seen: this run's latest, or the one the run started with. |
| property | `public IReadOnlyList<AspectInstance> Aspects` | 50 |  |
| property | `public IReadOnlyList<AspectInstance> AspectOffer` | 51 |  |
| property | `public EventData CurrentEvent` | 52 |  |
| property | `public IReadOnlyList<EventOption> EventOptions` | 54 | The options of the current event that can be taken now. |
| property | `public BenchRules Rules` | 55 |  |
| property | `public int RiShift` | 57 | This depth only: how much every material's Reaction Index is shifted, and whether the next first reaction triples. |
| property | `public bool TripleFirst` | 58 |  |
| property | `public int HandDelta` | 60 | This depth only: how many cards more or fewer each hand holds. |
| method | `public void ChooseAspect(string id)` | 146 | Takes one of the offered aspects for the rest of the run and moves on to the event or the bench. |
| method | `public EventResult ChooseEventOption(int index)` | 228 | Takes an option of the current event (an index into EventOptions), applies it and opens the bench. |
| property | `public IReadOnlyList<int> DepthBlocked` | 410 | Tiles shut or warmed by this depth's event, for the screen. |
| property | `public IEnumerable<int> DepthHeated` | 411 |  |
| ctor | `public RunState(ContentDb db, Journal journal, int seed, int startDepth = 1, RunOptions options = null)` | 34 |  |
| property | `public int Depth` | 51 |  |
| property | `public long Potency` | 52 |  |
| property | `public int Target` | 53 |  |
| property | `public int TrialsLeft` | 54 |  |
| property | `public int RedrawsLeft` | 55 |  |
| property | `public int DiscoveriesThisRun` | 56 |  |
| property | `public RunPhase Phase` | 57 |  |
| property | `public Bench Bench` | 58 |  |
| property | `public IReadOnlyList<string> Hand` | 59 |  |
| property | `public IReadOnlyList<CatalystData> Catalysts` | 60 |  |
| property | `public IReadOnlyList<CatalystData> CatalystOffer` | 61 |  |
| property | `public int HandSize` | 62 |  |
| property | `public bool Diagonals` | 63 |  |
| property | `public bool CanIgnite` | 64 |  |
| property | `public int RedrawsUsed` | 66 | How many times the hand was redrawn this run. |
| property | `public RunOptions Options` | 67 |  |
| method | `public bool IsOpen(string id)` | 68 |  |
| method | `public int Owned(string id)` | 72 | Copies of a material in the hand or on the bench. |
| method | `public bool HasFired(string a, string b)` | 78 |  |
| method | `public bool HasFired(RecipeData recipe)` | 79 |  |
| property | `public int SatchelSize` | 80 |  |
| method | `public bool Place(int handIndex, int tile)` | 137 |  |
| method | `public bool Unplace(int tile)` | 146 |  |
| method | `public bool Redraw()` | 155 | Throws away the starting materials in hand (not the bench, not discoveries) and draws again. |
| method | `public bool Deal(IReadOnlyList<string> ids)` | 171 | The tutorial: puts the starting materials in hand back and deals exactly these instead. Discoveries on the rack stay. Only while playing, on an empty bench. False, and nothing changed, when an id is not an open starting material or the satchel has too few copies of it. |
| method | `public ScoreContext NewContext()` | 184 | What a reaction's score depends on here and now (also used by the scripted player to plan). |
| method | `public List<ReactionEvent> Preview(string[] tiles)` | 190 | What this layout (9 tiles, null where empty) would do if ignited now. Nothing changes: the journal and the recipes fired this run are copied. The tutorial uses it to show a broken chain beside the real one. |
| method | `public IgniteOutcome Ignite()` | 198 |  |
| method | `public void ChooseCatalyst(string id)` | 279 | Takes one of the offered catalysts (or null when none were left to offer) and descends. |

## Uses
[[AspectEffects]], [[AspectInstance]], [[Bench]], [[BenchRules]], [[ContentDb]], [[Effects]], [[EventNote]], [[EventResult]], [[IgniteOutcome]], [[IgniteResolver]], [[Journal]], [[Kind]], [[Ops]], [[Rarities]], [[ReactionEvent]], [[Rng]], [[RunOptions]], [[RunPhase]], [[Satchel]], [[ScoreContext]], [[Unlocks]]

## Used by
[[AutoPlayer]], [[BenchView]], [[EventView]], [[GameFlow]], [[GoldenShots]], [[HudView]], [[IgniteSequence]], [[Profile]], [[RunOverView]]

## Tested by
[[HudTests]], [[LoreTests]], [[PauseTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RarityReportTests]], [[RarityTests]], [[RulesTests]], [[RunStateTests]], [[TutorialFlowTests]], [[TutorialTests]]
