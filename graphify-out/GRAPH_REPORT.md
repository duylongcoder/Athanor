# Graph Report - Assets  (2026-11-24)

## Corpus Check
- 165 files · ~140,937 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 714 file(s) not represented in the graph (top: .meta 612, .asset 25, .shader 24)
- WARNING: 714 of 1157 files seen (62%) have no supported extractor and are missing from this graph entirely. It may look complete while representing only part of the codebase.

## Summary
- 2614 nodes · 9552 edges · 98 communities (67 shown, 31 thin omitted)
- Extraction: 81% EXTRACTED · 19% INFERRED · 0% AMBIGUOUS · INFERRED: 1800 edges (avg confidence: 0.85)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `844c6937`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- ContentDb
- TutorialController
- Athanor.Gameplay
- IEnumerator
- LayoutPinTests
- Phase3Tests
- TestCase
- AutoplayTraceTests.Play
- IgniteTests
- Color
- RunState
- PrefabFixture
- Athanor.Game
- List
- ContentData
- Test
- Loc.T
- SparkMesh
- Label
- EventView
- Vector3
- BenchRules
- PrefabBakeTests
- ContentTests
- Vector2
- GameFlow
- SpriteRenderer
- AudioMute
- ContentDb.Material
- RunState.Ignite
- TutorialStepsMigration
- LangTests
- Profile
- IgniteSequence
- HudView
- Loc.Set
- SaveTests
- Px.At
- JournalView
- Sparks
- Satchel
- ScreenFlowTests
- MenuItem
- WindowView
- Lang
- BenchView
- ScreenFx
- DepthRig
- GameObject
- PauseMenu
- EffectOp
- TutorialTests
- HintData
- GameFlow.Awake
- AutoPlayer
- DepthMood
- Clickable
- RunState.RunState
- Camera
- ScreenFlow
- AtlasBuilder
- RarityStars
- Loc
- Texture2D
- Journal
- FontKind
- DepthLayerTests
- TextAsset
- DepthTable
- StaticResetTests
- FlowState
- RarityTests
- Unlocks.For
- CardView
- PointerInput
- FlipbookImporter
- GoldenShots.cs
- Json
- TipKind
- RunOverDepthTests
- Kind
- EventType
- RunPhase
- SpriteImportRules
- LayoutData
- SparkParticles
- BenchView.BuildTip
- AutoMove
- TutorialAreaEditor
- FontBuilder
- GoalData
- TitleChoice
- Pick
- Dev.cs
- KindColors.cs

## God Nodes (most connected - your core abstractions)
1. `ContentDb` - 252 edges
2. `RunState` - 194 edges
3. `Journal` - 135 edges
4. `Phase3Tests` - 129 edges
5. `Athanor.Game` - 110 edges
6. `GameFlow` - 100 edges
7. `Athanor.Gameplay` - 72 edges
8. `BenchView` - 66 edges
9. `Label` - 65 edges
10. `PrefabFixture` - 65 edges

## Surprising Connections (you probably didn't know these)
- `RunState` --defines--> `CanIgnite`  [EXTRACTED]
  Athanor/Assets/Scripts/Gameplay/RunStart.cs → Athanor/Assets/Scripts/Gameplay/RunState.cs
- `RunState` --defines--> `Target`  [EXTRACTED]
  Athanor/Assets/Scripts/Gameplay/RunStart.cs → Athanor/Assets/Scripts/Gameplay/RunState.cs
- `Newcomer` --references--> `AutoPlayer`  [EXTRACTED]
  Athanor/Assets/Editor/Simulation/Sim.cs → Athanor/Assets/Scripts/Gameplay/AutoPlayer.cs
- `Newcomer` --references--> `ContentDb`  [EXTRACTED]
  Athanor/Assets/Editor/Simulation/Sim.cs → Athanor/Assets/Scripts/Gameplay/ContentDb.cs
- `Newcomer` --references--> `Journal`  [EXTRACTED]
  Athanor/Assets/Editor/Simulation/Sim.cs → Athanor/Assets/Scripts/Gameplay/Journal.cs

## Import Cycles
- None detected.

## Communities (98 total, 31 thin omitted)

### Community 0 - "ContentDb"
Cohesion: 0.06
Nodes (16): Rarities, RecipeData, ContentDb, Aspects, Catalysts, Events, FinalDepth, Goals (+8 more)

### Community 1 - "TutorialController"
Cohesion: 0.07
Nodes (12): TutorialArea, TutorialRect, TutorialController, Finished, Trial, TutorialStep, TutorialLay, TutorialScript (+4 more)

### Community 2 - "Athanor.Gameplay"
Cohesion: 0.08
Nodes (4): Athanor.Gameplay, Athanor.Tests, RewardKinds, RarityReportTests

### Community 3 - "IEnumerator"
Cohesion: 0.07
Nodes (12): Cutscene, Playing, Skipped, DepthLine, DescentArrive, DescentLeave, DescentName, IntroPage (+4 more)

### Community 4 - "LayoutPinTests"
Cohesion: 0.09
Nodes (6): TutorialHole, Palette, RarityColors, LayoutPinTests, MainSceneFixture, SceneBakeTests

### Community 5 - "Phase3Tests"
Cohesion: 0.10
Nodes (3): ScoreContext, CatalystData, Phase3Tests

### Community 6 - "TestCase"
Cohesion: 0.07
Nodes (11): Athanor.Game.Tests, DepthLook, TutorialRules, EventNote, TutorialPlan, AskSkip, None, Run (+3 more)

### Community 7 - "AutoplayTraceTests.Play"
Cohesion: 0.07
Nodes (11): Reader, SettingsTests, AutoHookTests, AutoplayTraceTests, Result, Baseline, Dir, MainScene (+3 more)

### Community 8 - "IgniteTests"
Cohesion: 0.10
Nodes (5): Bench, Count, IgniteResolver, CanIgnite, IgniteTests

### Community 9 - "Color"
Cohesion: 0.05
Nodes (34): Layout, Layout, Layout, TileView, FaceCentre, FaceSize, Hovered, Icon (+26 more)

### Community 10 - "RunState"
Cohesion: 0.06
Nodes (29): EventResult, RunState, AspectOffer, Aspects, CurrentEvent, DepthBlocked, DepthHeated, EventOptions (+21 more)

### Community 11 - "PrefabFixture"
Cohesion: 0.13
Nodes (3): ChoiceHookTests, HotPathTests, PrefabFixture

### Community 12 - "Athanor.Game"
Cohesion: 0.09
Nodes (3): Athanor.Game, Athanor.EditorTools, Fx

### Community 13 - "List"
Cohesion: 0.09
Nodes (8): Career, RunStats, Sim, ResolveResult, RunOptions, Unlocks, Count, Baseline

### Community 14 - "ContentData"
Cohesion: 0.12
Nodes (14): AspectData, AspectEffects, ContentData, EffectData, Effects, EventData, EventOption, EventOutcome (+6 more)

### Community 16 - "Loc.T"
Cohesion: 0.10
Nodes (6): Wording, Marker, Warm, ChoiceCard, Item, AspectInstance

### Community 17 - "SparkMesh"
Cohesion: 0.08
Nodes (10): ArtLibraryBuilder, Dust, FlipbookSheet, Quad, SparkMesh, BaseMaterial, Count, Mesh (+2 more)

### Community 18 - "Label"
Cohesion: 0.07
Nodes (16): Label, Alpha, Color, LineBox, Lines, Order, Width, Layout (+8 more)

### Community 19 - "EventView"
Cohesion: 0.07
Nodes (11): EventView, Outcome, Row, RunOverView, Again, DepthName, DepthValue, Timing (+3 more)

### Community 20 - "Vector3"
Cohesion: 0.11
Nodes (8): ArcPath, Motion, Align, Centre, Left, Right, Spawn, MotionTests

### Community 21 - "BenchRules"
Cohesion: 0.11
Nodes (5): BenchRules, ChainPlan, ChainPlanner, ChainStep, LinkPair

### Community 22 - "PrefabBakeTests"
Cohesion: 0.12
Nodes (3): Settings, PrefabBakeTests, TestFx

### Community 24 - "Vector2"
Cohesion: 0.13
Nodes (5): DepthMath, Timing, Timing, Layout, DepthClickTests

### Community 25 - "GameFlow"
Cohesion: 0.07
Nodes (14): GameFlow, Bench, Db, Flow, Journal, JournalView, Profile, Run (+6 more)

### Community 26 - "SpriteRenderer"
Cohesion: 0.09
Nodes (13): LinkView, FogSheet, Flame, Flipbook, IsPlaying, GoalRow, GoalsView, CloseClick (+5 more)

### Community 27 - "AudioMute"
Cohesion: 0.09
Nodes (7): AudioMute, Muted, AudioMuteHook, VisibilityCallback, Haptics, Supported, SettingsHapticsTests

### Community 28 - "ContentDb.Material"
Cohesion: 0.15
Nodes (3): Newcomer, ColdCount, TriedCount

### Community 30 - "TutorialStepsMigration"
Cohesion: 0.10
Nodes (20): A, S, TutorialStepsMigration, AreaKind, Fixed, ScriptTile, View, ScriptTrial (+12 more)

### Community 31 - "LangTests"
Cohesion: 0.09
Nodes (3): LangEntry, LangFile, LangTests

### Community 32 - "Profile"
Cohesion: 0.15
Nodes (5): Profile, Completed, Rewards, UnlockedCatalysts, UnlockedStarters

### Community 33 - "IgniteSequence"
Cohesion: 0.13
Nodes (7): IgniteSequence, Hurry, Timing, IgniteOutcomeView, ReactionEvent, IgniteOutcome, ScoreParts

### Community 34 - "HudView"
Cohesion: 0.07
Nodes (12): HudView, Gold, HoveredAspect, HoveredCatalyst, PauseEdgeColor, PauseHovered, PauseRect, PauseShown (+4 more)

### Community 35 - "Loc.Set"
Cohesion: 0.10
Nodes (3): EditorGlobals, GameTestSetup, LabelLineBoxTests

### Community 36 - "SaveTests"
Cohesion: 0.13
Nodes (4): Save, IntroSeen, TutorialDone, SaveTests

### Community 37 - "Px.At"
Cohesion: 0.13
Nodes (5): PauseAnchor, RedrawAnchor, Loop, Anim, Px

### Community 38 - "JournalView"
Cohesion: 0.13
Nodes (13): JournalCell, JournalView, CloseClick, Faded, HintsRect, IndexRect, Ink, IsOpen (+5 more)

### Community 39 - "Sparks"
Cohesion: 0.13
Nodes (5): Lane, Sparks, LaneCount, LiveCount, SparksTests

### Community 40 - "Satchel"
Cohesion: 0.20
Nodes (4): Rng, Satchel, Total, SatchelTests

### Community 42 - "MenuItem"
Cohesion: 0.15
Nodes (5): BalanceReports, CheckSprites, FixCandleImport, Reserialize, WebBuild

### Community 43 - "WindowView"
Cohesion: 0.17
Nodes (5): Layout, Look, Timing, WindowView, Soft

### Community 44 - "Lang"
Cohesion: 0.17
Nodes (3): Lang, Count, Keys

### Community 45 - "BenchView"
Cohesion: 0.11
Nodes (11): BenchView, Hud, InputEnabled, TileCount, From, Hand, None, Tile (+3 more)

### Community 47 - "ScreenFx"
Cohesion: 0.11
Nodes (5): CameraSway, SwayTiming, ScreenFx, ShakeTiming, MainDepthTests

### Community 48 - "DepthRig"
Cohesion: 0.13
Nodes (7): DepthRig, AnchorZ, CamOffset, Flat, ImageShift, RestPosition, WorldTexture

### Community 50 - "PauseMenu"
Cohesion: 0.17
Nodes (5): Layout, PauseMenu, Confirming, IsOpen, PauseTests

### Community 51 - "EffectOp"
Cohesion: 0.09
Nodes (21): EffectOp, BlockTile, CatalystGift, CatalystUpgrade, FirstTriple, GiveDiscovery, Hand, HeatTile (+13 more)

### Community 54 - "HintData"
Cohesion: 0.22
Nodes (3): HintData, JournalData, JournalTests

### Community 55 - "GameFlow.Awake"
Cohesion: 0.16
Nodes (6): FxRefs, GoalToast, Line, SlideOutAt, Title, Timing

### Community 56 - "AutoPlayer"
Cohesion: 0.22
Nodes (3): AutoPlayer, KnowsTheWay, Move

### Community 57 - "DepthMood"
Cohesion: 0.13
Nodes (8): DepthMood, Preset, FogTint, DepthStage, SwayOn, DepthSwitch, FlameSwap, SpriteSwap

### Community 58 - "Clickable"
Cohesion: 0.18
Nodes (6): Clickable, Card, CatalystChoiceView, Joined, Off, On

### Community 61 - "ScreenFlow"
Cohesion: 0.16
Nodes (6): ScreenFlow, CanResetProgress, InCutscene, JournalFrom, Sandbox, State

### Community 63 - "RarityStars"
Cohesion: 0.22
Nodes (3): RarityStars, GlyphCached, Root

### Community 64 - "Loc"
Cohesion: 0.16
Nodes (5): Loc, Code, Current, NativeName, Vi

### Community 67 - "Journal"
Cohesion: 0.27
Nodes (3): Journal, Hints, KnownPairs

### Community 68 - "FontKind"
Cohesion: 0.15
Nodes (9): FontKind, Body, Logo, Name, Numeral, Tiny, Title, Fonts (+1 more)

### Community 71 - "DepthTable"
Cohesion: 0.22
Nodes (5): TableShape, DepthTable, Shape, DepthFixture, Table

### Community 73 - "FlowState"
Cohesion: 0.14
Nodes (14): FlowState, AspectChoice, Bench, Boot, CatalystChoice, Descent, Ending, Event (+6 more)

### Community 77 - "PointerInput"
Cohesion: 0.20
Nodes (8): PointerInput, Bench, Down, Held, Hover, Rig, Up, World

### Community 78 - "FlipbookImporter"
Cohesion: 0.20
Nodes (6): Box, FlipbookImporter, Frame, Info, Meta, Sheet

### Community 81 - "TipKind"
Cohesion: 0.25
Nodes (8): TipKind, Aspect, Catalyst, Material, None, Pause, Redraw, TileNote

### Community 83 - "Kind"
Cohesion: 0.25
Nodes (8): Kind, Beast, Crystal, Metal, Ore, Plant, Potion, Powder

### Community 84 - "EventType"
Cohesion: 0.25
Nodes (8): EventType, Boon, Choice, Find, Gamble, Hazard, Lore, Shrine

### Community 85 - "RunPhase"
Cohesion: 0.25
Nodes (7): RunPhase, AspectOffer, DepthCleared, EventOffer, Lost, Playing, Won

### Community 87 - "LayoutData"
Cohesion: 0.48
Nodes (4): LayoutData, PointData, RectData, TileData

### Community 90 - "AutoMove"
Cohesion: 0.29
Nodes (7): AutoMove, ChoseAspect, ChoseCatalyst, ChoseEvent, None, ReadyToIgnite, Redrew

### Community 94 - "TitleChoice"
Cohesion: 0.50
Nodes (4): TitleChoice, Begin, HowToPlay, Intro

## Knowledge Gaps
- **259 isolated node(s):** `TriedCount`, `ColdCount`, `Hud`, `TileCount`, `None` (+254 more)
  These have ≤1 connection - possible missing edges. (Counts symbols only; 431 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **31 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `GameFlow` connect `GameFlow` to `ContentDb`, `TutorialController`, `IEnumerator`, `LayoutPinTests`, `AutoplayTraceTests.Play`, `Color`, `RunState`, `List`, `SparkMesh`, `Label`, `EventView`, `PrefabBakeTests`, `SpriteRenderer`, `Profile`, `IgniteSequence`, `SaveTests`, `JournalView`, `Sparks`, `WindowView`, `BenchView`, `system_collections`, `ScreenFx`, `GameObject`, `PauseMenu`, `GameFlow.Awake`, `AutoPlayer`, `DepthMood`, `Clickable`, `Camera`, `ScreenFlow`, `Journal`, `TextAsset`, `FlowState`, `TitleChoice`?**
  _High betweenness centrality (0.137) - this node is a cross-community bridge._
- **Are the 120 inferred relationships involving `ContentDb` (e.g. with `BalanceReports.BalanceMatrix()` and `BalanceReports.BalanceReport()`) actually correct?**
  _`ContentDb` has 120 INFERRED edges - model-reasoned connections that need verification._
- **What connects `TriedCount`, `ColdCount`, `Hud` to the rest of the system?**
  _259 weakly-connected nodes found - possible missing edges._
- **Should `ContentDb` be split into smaller, more focused modules?**
  _Cohesion score 0.06044303797468355 - nodes in this community are weakly interconnected._
- **Why does `RunState` connect `RunState` to `ContentDb`, `Athanor.Gameplay`, `IEnumerator`, `Phase3Tests`, `IgniteTests`, `PrefabFixture`, `List`, `ContentData`, `Test`, `Loc.T`, `BenchRules`, `ContentTests`, `GameFlow`, `ContentDb.Material`, `RunState.Ignite`, `Profile`, `IgniteSequence`, `HudView`, `Satchel`, `BenchView`, `GameObject`, `TutorialTests`, `AutoPlayer`, `RunState.RunState`, `Journal`, `RarityTests`, `Unlocks.For`, `CardView`, `RunPhase`?**
  _High betweenness centrality (0.108) - this node is a cross-community bridge._
- **Are the 50 inferred relationships involving `RunState` (e.g. with `GoldenShots.BenchCrowded()` and `GoldenShots.BenchTutorial()`) actually correct?**
  _`RunState` has 50 INFERRED edges - model-reasoned connections that need verification._
- **Should `TutorialController` be split into smaller, more focused modules?**
  _Cohesion score 0.06519114688128773 - nodes in this community are weakly interconnected._