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
- Layer: **Core** · Source: `Athanor/Assets/Scripts/Core/RunStart.cs:23-397` (+ partial: `Athanor/Assets/Scripts/Core/RunState.cs:20`)

> One run: descending through depths, spending trials, collecting catalysts.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public string LastLore` | 38 | The last Lore event seen: this run's latest, or the one the run started with. |
| property | `public IReadOnlyList<AspectInstance> Aspects` | 40 |  |
| property | `public IReadOnlyList<AspectInstance> AspectOffer` | 41 |  |
| property | `public EventData CurrentEvent` | 42 |  |
| property | `public IReadOnlyList<EventOption> EventOptions` | 44 | The options of the current event that can be taken now. |
| property | `public BenchRules Rules` | 45 |  |
| property | `public int RiShift` | 47 | This depth only: how much every material's Reaction Index is shifted, and whether the next first reaction triples. |
| property | `public bool TripleFirst` | 48 |  |
| property | `public int HandDelta` | 50 | This depth only: how many cards more or fewer each hand holds. |
| method | `public void ChooseAspect(string id)` | 131 | Takes one of the offered aspects for the rest of the run and moves on to the event or the bench. |
| method | `public EventResult ChooseEventOption(int index)` | 213 | Takes an option of the current event (an index into EventOptions), applies it and opens the bench. |
| property | `public IReadOnlyList<int> DepthBlocked` | 395 | Tiles shut or warmed by this depth's event, for the screen. |
| property | `public IEnumerable<int> DepthHeated` | 396 |  |
| field | `public const int MinHand = 3, BaseHand = 5, BaseTrials = 4, BaseRedraws = 2, FinalDepth = 5, FinalTrials = 6, StarterCopies = 2, OfferSize = 3` | 22 |  |
| field | `public const int RackSize = 12` | 24 | How many discoveries the hand can keep; a new one pushes the weakest old one out. |
| ctor | `public RunState(ContentDb db, Journal journal, int seed, int startDepth = 1, RunOptions options = null)` | 38 |  |
| property | `public int Depth` | 55 |  |
| property | `public long Potency` | 56 |  |
| property | `public int Target` | 57 |  |
| property | `public int TrialsLeft` | 58 |  |
| property | `public int RedrawsLeft` | 59 |  |
| property | `public int DiscoveriesThisRun` | 60 |  |
| property | `public RunPhase Phase` | 61 |  |
| property | `public Bench Bench` | 62 |  |
| property | `public IReadOnlyList<string> Hand` | 63 |  |
| property | `public IReadOnlyList<CatalystData> Catalysts` | 64 |  |
| property | `public IReadOnlyList<CatalystData> CatalystOffer` | 65 |  |
| property | `public int HandSize` | 66 |  |
| property | `public bool Diagonals` | 67 |  |
| property | `public bool CanIgnite` | 68 |  |
| property | `public int RedrawsUsed` | 70 | How many times the hand was redrawn this run. |
| property | `public RunOptions Options` | 71 |  |
| method | `public bool IsOpen(string id)` | 72 |  |
| method | `public int Owned(string id)` | 76 | Copies of a material in the hand or on the bench. |
| field | `public static int PouchMode = 2` | 83 | How unlocked starters reach the hand. 2 (the game): from their own pouch, PouchCards a hand. 0: two copies in the satchel. 1: one copy in the satchel. |
| field | `public const int PouchCards = 1` | 84 |  |
| method | `public bool HasFired(string a, string b)` | 85 |  |
| method | `public bool HasFired(RecipeData recipe)` | 86 |  |
| property | `public int SatchelSize` | 87 |  |
| method | `public bool Place(int handIndex, int tile)` | 144 |  |
| method | `public bool Unplace(int tile)` | 153 |  |
| method | `public bool Redraw()` | 162 | Throws away the starting materials in hand (not the bench, not discoveries) and draws again. |
| method | `public bool Deal(IReadOnlyList<string> ids)` | 178 | The tutorial: puts the starting materials in hand back and deals exactly these instead. Discoveries on the rack stay. Only while playing, on an empty bench. False, and nothing changed, when an id is not an open starting material or the satchel has too few copies of it. |
| method | `public ScoreContext NewContext()` | 191 | What a reaction's score depends on here and now (also used by the scripted player to plan). |
| method | `public List<ReactionEvent> Preview(string[] tiles)` | 197 | What this layout (9 tiles, null where empty) would do if ignited now. Nothing changes: the journal and the recipes fired this run are copied. The tutorial uses it to show a broken chain beside the real one. |
| method | `public IgniteOutcome Ignite()` | 205 |  |
| method | `public void ChooseCatalyst(string id)` | 286 | Takes one of the offered catalysts (or null when none were left to offer) and descends. |

## Uses
[[AspectEffects]], [[AspectInstance]], [[Bench]], [[BenchRules]], [[ContentDb]], [[Effects]], [[EventResult]], [[IgniteOutcome]], [[IgniteResolver]], [[Journal]], [[Kind]], [[Ops]], [[Rarities]], [[ReactionEvent]], [[Rng]], [[RunOptions]], [[RunPhase]], [[Satchel]], [[ScoreContext]], [[Unlocks]]

## Used by
[[AutoPlayer]], [[BenchView]], [[CatalystChoiceView]], [[EventView]], [[GameFlow]], [[HudView]], [[Profile]], [[RunOverView]]

## Tested by
[[ContentTests]], [[HudTests]], [[LoreTests]], [[PauseTests]], [[Phase3Tests]], [[PrefabBakeTests]], [[RarityReportTests]], [[RarityTests]], [[RunOverDepthTests]], [[RunStateTests]], [[TutorialFlowTests]], [[TutorialTests]]
