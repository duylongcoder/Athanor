# Graph Report - Assets  (2026-11-26)

## Corpus Check
- 470 files · ~140,354 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2932 nodes · 10233 edges · 116 communities (82 shown, 34 thin omitted)
- Extraction: 82% EXTRACTED · 18% INFERRED · 0% AMBIGUOUS · INFERRED: 1838 edges (avg confidence: 0.85)
- Token cost: 0 input · 0 output

## Community Hubs (Navigation)
- ContentDb
- Phase3Tests
- Journal
- RunState
- unityengine
- AutoPlayer
- Athanor.Gameplay
- LayoutPinTests
- Color
- GameObject
- RunState.Ignite()
- IEnumerator
- SpriteRenderer
- BenchRules
- TitleView
- Lang
- Px.At()
- AutoplayTraceTests.Play()
- Test
- HudView
- Loc.T()
- unityeditor
- ContentDatabase
- IgniteTests
- AudioMute
- Vector2
- GameManager
- TestCase
- BenchView
- TutorialManager
- List
- Settings
- SparkMesh
- CatalystChoiceView
- PrefabFixture
- PrefabFixture.Destroy()
- IgniteSequence
- Clickable
- JournalView
- ChainTests
- Label
- GoalStat
- Kind
- Sparks
- PrefabBakeTests
- LangTests
- EventView
- Rect
- DepthRig
- CatalystEffect
- ScreenFlowTests
- ManagerInspectors.cs
- RarityStars
- TutorialSequence
- MenuItem
- Loc.Set()
- Bench
- ScreenFx
- DepthMood
- Sprite
- AtlasBuilder
- StaticResetTests
- TearDown
- WindowView
- ScreenFlow
- TutorialTests
- AspectEffect
- EditorGlobals.Inject()
- Camera
- EventNeed
- EventOp
- EffectOp
- Settings.FromJson()
- TutorialManager.Step()
- PointerInput
- FlowState
- DepthLayerTests
- DepthTable
- SaveTests
- TutorialGate.Plan()
- RarityTests
- FontKind
- TutorialActionKind
- Loc
- FlipbookImporter
- GameManager.DevKeys()
- Save
- Save.SetTutorialDone()
- CameraSway
- ContentEnumTests
- SaveManager
- ContentFieldMigration.cs
- Singleton
- KindOrNone
- TipKind
- ViewRef
- ContentEnums.cs
- EventKind
- EventType
- SpriteImportRules
- LayoutData
- ManagerSceneMigration.Apply()
- FogSheet
- Flipbook
- RunManager
- EventNeedType
- RunPhase
- Reader
- FontBuilder
- Dev.cs
- TutorialSequenceEditor
- TitleChoice

## God Nodes (most connected - your core abstractions)
1. `ContentDb` - 258 edges
2. `RunState` - 195 edges
3. `Journal` - 136 edges
4. `Phase3Tests` - 129 edges
5. `Athanor.Game` - 121 edges
6. `GameManager` - 101 edges
7. `Athanor.Gameplay` - 92 edges
8. `BenchView` - 66 edges
9. `Label` - 65 edges
10. `TitleView` - 65 edges

## Surprising Connections (you probably didn't know these)
- `Newcomer` --references--> `ContentDb`  [EXTRACTED]
  Editor/Simulation/Sim.cs → Scripts/Gameplay/ContentDb.cs
- `Newcomer` --references--> `Journal`  [EXTRACTED]
  Editor/Simulation/Sim.cs → Scripts/Gameplay/Journal.cs
- `TutorialSequenceEditor` --references--> `TutorialSequence`  [EXTRACTED]
  Editor/TutorialSequenceEditor.cs → Scripts/Game/Tutorial/TutorialSequence.cs
- `DepthClickTests` --references--> `TileView`  [EXTRACTED]
  Tests/EditModeGame/DepthClickTests.cs → Scripts/Game/Bench/TileView.cs
- `MainDepthTests` --references--> `TileView`  [EXTRACTED]
  Tests/EditModeGame/MainDepthTests.cs → Scripts/Game/Bench/TileView.cs

## Import Cycles
- None detected.

## Communities (116 total, 34 thin omitted)

### Community 0 - "ContentDb"
Cohesion: 0.05
Nodes (18): ScoreContext, GoalData, ContentDb, Aspects, Catalysts, Events, FinalDepth, Goals (+10 more)

### Community 1 - "Phase3Tests"
Cohesion: 0.08
Nodes (12): AspectData, AspectEffects, ContentData, Effects, MaterialData, Ops, Rarities, RulesData (+4 more)

### Community 2 - "Journal"
Cohesion: 0.06
Nodes (20): ProgressManager, Db, Journal, Profile, RunUnlocks, Sandbox, ProfileData, RewardLogData (+12 more)

### Community 3 - "RunState"
Cohesion: 0.05
Nodes (30): EventResult, RunState, AspectOffer, Aspects, CurrentEvent, DepthBlocked, DepthHeated, EventOptions (+22 more)

### Community 5 - "AutoPlayer"
Cohesion: 0.07
Nodes (10): Career, Newcomer, ColdCount, TriedCount, RunStats, Sim, AutoPlayer, KnowsTheWay (+2 more)

### Community 6 - "Athanor.Gameplay"
Cohesion: 0.07
Nodes (3): Athanor.Gameplay, Athanor.Tests, RewardKinds

### Community 7 - "LayoutPinTests"
Cohesion: 0.10
Nodes (3): LayoutPinTests, MainSceneFixture, SceneBakeTests

### Community 8 - "Color"
Cohesion: 0.05
Nodes (27): Layout, Layout, Layout, Look, Brew, Bursting, Closing, Gathering (+19 more)

### Community 9 - "GameObject"
Cohesion: 0.09
Nodes (6): GoldenShots, ViewCapture, Singletons, AutoHookTests, GoldenShotTests, TestFx

### Community 10 - "RunState.Ignite()"
Cohesion: 0.13
Nodes (8): AutoMove, ChoseAspect, ChoseCatalyst, ChoseEvent, None, ReadyToIgnite, Redrew, RunStateTests

### Community 11 - "IEnumerator"
Cohesion: 0.09
Nodes (10): Cutscene, Playing, Skipped, DescentArrive, DescentLeave, DescentName, IntroPage, Look (+2 more)

### Community 12 - "SpriteRenderer"
Cohesion: 0.09
Nodes (11): Slot, LinkView, ArcPath, Motion, Align, Centre, Left, Right (+3 more)

### Community 13 - "BenchRules"
Cohesion: 0.10
Nodes (8): BenchRules, ChainPlan, ChainPlanner, ChainStep, CatalystData, IgniteResolver, ResolveResult, Diagonals

### Community 14 - "TitleView"
Cohesion: 0.08
Nodes (11): Layout, PanelBox, Timing, TitleView, Asking, BeginPressed, CellMid, CreditY (+3 more)

### Community 15 - "Lang"
Cohesion: 0.10
Nodes (6): Json, Lang, Count, Keys, LangEntry, LangFile

### Community 16 - "Px.At()"
Cohesion: 0.08
Nodes (15): PauseAnchor, PotencyPosition, RedrawAnchor, TileView, FaceCentre, FaceSize, Hovered, Icon (+7 more)

### Community 17 - "AutoplayTraceTests.Play()"
Cohesion: 0.14
Nodes (9): AutoplayTraceTests, Result, ManagerBootTests, MainScene, PrefsSnapshot, TraceRecorder, Probe, SingletonTests (+1 more)

### Community 18 - "Test"
Cohesion: 0.10
Nodes (3): Target, ContentTests, RulesTests

### Community 19 - "HudView"
Cohesion: 0.07
Nodes (11): HudView, Gold, HoveredAspect, HoveredCatalyst, PauseEdgeColor, PauseHovered, PauseRect, PauseShown (+3 more)

### Community 20 - "Loc.T()"
Cohesion: 0.13
Nodes (8): Wording, Marker, Warm, EffectData, EventData, EventOption, EventOutcome, AspectInstance

### Community 22 - "ContentDatabase"
Cohesion: 0.10
Nodes (19): AspectDefinition, CatalystDefinition, ContentDatabase, RarityId, Common, Epic, Legendary, Mythic (+11 more)

### Community 24 - "AudioMute"
Cohesion: 0.09
Nodes (7): AudioMute, Muted, AudioMuteHook, VisibilityCallback, Haptics, Supported, SettingsHapticsTests

### Community 25 - "Vector2"
Cohesion: 0.14
Nodes (5): DepthMath, TutorialStyle, Timing, Timing, DepthClickTests

### Community 26 - "GameManager"
Cohesion: 0.07
Nodes (16): GameManager, Bench, Db, Flow, Journal, JournalView, Profile, Progress (+8 more)

### Community 27 - "TestCase"
Cohesion: 0.14
Nodes (5): Athanor.Game.Tests, DepthLook, EventNote, DepthLookTests, NoteTextTests

### Community 28 - "BenchView"
Cohesion: 0.10
Nodes (12): BenchView, Hud, InputEnabled, TileCount, From, Hand, None, Tile (+4 more)

### Community 29 - "TutorialManager"
Cohesion: 0.13
Nodes (5): TutorialHole, TutorialManager, Finished, Sequence, Trial

### Community 30 - "List"
Cohesion: 0.15
Nodes (4): Rng, Satchel, Total, SatchelTests

### Community 31 - "Settings"
Cohesion: 0.15
Nodes (6): Layout, PauseMenu, Confirming, IsOpen, Settings, PauseTests

### Community 32 - "SparkMesh"
Cohesion: 0.11
Nodes (7): Dust, Quad, SparkMesh, BaseMaterial, Count, Mesh, Vert

### Community 33 - "CatalystChoiceView"
Cohesion: 0.13
Nodes (7): CardRow, CatalystChoiceView, Joined, Off, On, ChoiceCard, ChoiceHookTests

### Community 35 - "PrefabFixture.Destroy()"
Cohesion: 0.13
Nodes (11): GoalRow, GoalsView, BarLeft, CloseClick, Faded, Ink, IsOpen, Teal (+3 more)

### Community 36 - "IgniteSequence"
Cohesion: 0.17
Nodes (6): IgniteSequence, Hurry, Timing, Fx, IgniteOutcomeView, ReactionEvent

### Community 37 - "Clickable"
Cohesion: 0.11
Nodes (9): Clickable, RunOverView, Again, DepthName, DepthValue, Timing, UiButton, Disabled (+1 more)

### Community 38 - "JournalView"
Cohesion: 0.10
Nodes (16): Grid, JournalView, CloseClick, Faded, HintsRect, IndexRect, Ink, IsOpen (+8 more)

### Community 40 - "Label"
Cohesion: 0.13
Nodes (9): CardView, Layout, Label, Alpha, Color, LineBox, Lines, Order (+1 more)

### Community 41 - "GoalStat"
Cohesion: 0.10
Nodes (15): ContentNames, GoalStat, BestIgnite, ChainLongest, ComboLargest, DepthReached, Ignites, JournalComplete (+7 more)

### Community 42 - "Kind"
Cohesion: 0.14
Nodes (11): HintData, JournalData, Kind, Beast, Crystal, Metal, Ore, Plant (+3 more)

### Community 43 - "Sparks"
Cohesion: 0.13
Nodes (6): Lane, Particle, Sparks, LaneCount, LiveCount, SparksTests

### Community 44 - "PrefabBakeTests"
Cohesion: 0.14
Nodes (7): GoalToast, Line, SlideOutAt, Title, Item, Timing, PrefabBakeTests

### Community 46 - "EventView"
Cohesion: 0.12
Nodes (7): FxRefs, EventView, Outcome, Row, Layout, Size, Tooltip

### Community 47 - "Rect"
Cohesion: 0.14
Nodes (9): ScriptTrial, Catalyst, Chain, Miss, Reaction, TutorialAreaDef, TutorialRect, TutorialStepDef (+1 more)

### Community 48 - "DepthRig"
Cohesion: 0.12
Nodes (8): DepthRig, AnchorZ, CamOffset, Flat, ImageShift, RestPosition, WorldTexture, MainDepthTests

### Community 50 - "CatalystEffect"
Cohesion: 0.09
Nodes (23): CatalystEffect, AllScale, CentreTwice, ChainFrom, ChainPlus, ComboBonus, DepthScale, Diagonals (+15 more)

### Community 52 - "ManagerInspectors.cs"
Cohesion: 0.11
Nodes (8): Athanor.EditorTools, ContentDatabaseEditor, MaterialDefinitionEditor, ProgressManagerEditor, RunManagerEditor, ContentSnapshot, FilePath, ContentSnapshotTests

### Community 53 - "RarityStars"
Cohesion: 0.17
Nodes (3): RarityStars, GlyphCached, Root

### Community 54 - "TutorialSequence"
Cohesion: 0.16
Nodes (11): TutorialSequence, AreaKind, Fixed, ScriptTile, View, CatalystTrialDef, TutorialAction, TutorialLayDef (+3 more)

### Community 55 - "MenuItem"
Cohesion: 0.17
Nodes (4): BalanceReports, FixCandleImport, Reserialize, WebBuild

### Community 57 - "Bench"
Cohesion: 0.15
Nodes (4): Bench, Count, LinkPair, CanIgnite

### Community 59 - "DepthMood"
Cohesion: 0.13
Nodes (7): DepthMood, Preset, FogTint, DepthStage, SwayOn, DepthSwitch, SpriteSwap

### Community 60 - "Sprite"
Cohesion: 0.18
Nodes (6): ArtLibraryBuilder, CheckSprites, FlameSwap, Flame, FlipbookSheet, ArtLibrary

### Community 64 - "WindowView"
Cohesion: 0.21
Nodes (5): Layout, Look, Timing, WindowView, Soft

### Community 65 - "ScreenFlow"
Cohesion: 0.17
Nodes (6): ScreenFlow, CanResetProgress, InCutscene, JournalFrom, Sandbox, State

### Community 67 - "AspectEffect"
Cohesion: 0.12
Nodes (17): AspectEffect, Anchor, Attuned, Bridge, Cold, Crossroads, Doubled, Echo (+9 more)

### Community 68 - "EditorGlobals.Inject()"
Cohesion: 0.16
Nodes (4): EditorGlobals, LocalizationManager, TestLang, GameTestSetup

### Community 70 - "EventNeed"
Cohesion: 0.17
Nodes (10): EventNeed, Catalyst, None, Rack, Redraw, Trial, EventDefinition, EventEffectDef (+2 more)

### Community 71 - "EventOp"
Cohesion: 0.13
Nodes (15): EventOp, BlockTile, CatalystGift, CatalystUpgrade, FirstTriple, GiveDiscovery, Hand, HeatTile (+7 more)

### Community 72 - "EffectOp"
Cohesion: 0.13
Nodes (15): EffectOp, BlockTile, CatalystGift, CatalystUpgrade, FirstTriple, GiveDiscovery, Hand, HeatTile (+7 more)

### Community 74 - "TutorialManager.Step()"
Cohesion: 0.19
Nodes (5): Baseline, StepRects, TutorialStepsBakeTests, Baseline, Dir

### Community 75 - "PointerInput"
Cohesion: 0.15
Nodes (8): PointerInput, Bench, Down, Held, Hover, Rig, Up, World

### Community 76 - "FlowState"
Cohesion: 0.14
Nodes (14): FlowState, AspectChoice, Bench, Boot, CatalystChoice, Descent, Ending, Event (+6 more)

### Community 78 - "DepthTable"
Cohesion: 0.24
Nodes (5): TableShape, DepthTable, Shape, DepthFixture, Table

### Community 80 - "TutorialGate.Plan()"
Cohesion: 0.21
Nodes (5): TutorialGate, TutorialPlan, AskSkip, None, Run

### Community 82 - "FontKind"
Cohesion: 0.20
Nodes (9): FontKind, Body, Logo, Name, Numeral, Tiny, Title, Fonts (+1 more)

### Community 83 - "TutorialActionKind"
Cohesion: 0.17
Nodes (12): TutorialActionKind, AwaitJournal, AwaitLay, Broken, ChainSummary, HoldWhileJournal, Ignite, MeasureBrokenChain (+4 more)

### Community 84 - "Loc"
Cohesion: 0.25
Nodes (5): Loc, Code, Current, NativeName, Vi

### Community 85 - "FlipbookImporter"
Cohesion: 0.20
Nodes (6): Box, FlipbookImporter, Frame, Info, Meta, Sheet

### Community 87 - "Save"
Cohesion: 0.22
Nodes (3): Save, IntroSeen, TutorialDone

### Community 91 - "SaveManager"
Cohesion: 0.25
Nodes (4): SaveManager, IntroSeen, Settings, TutorialDone

### Community 92 - "ContentFieldMigration.cs"
Cohesion: 0.25
Nodes (3): ContentFieldMigration, ContentAssets, ContentAssetTests

### Community 93 - "Singleton"
Cohesion: 0.25
Nodes (6): ContentManager, Database, Db, Singleton, Exists, Instance

### Community 94 - "KindOrNone"
Cohesion: 0.22
Nodes (9): KindOrNone, Beast, Crystal, Metal, None, Ore, Plant, Potion (+1 more)

### Community 95 - "TipKind"
Cohesion: 0.25
Nodes (8): TipKind, Aspect, Catalyst, Material, None, Pause, Redraw, TileNote

### Community 96 - "ViewRef"
Cohesion: 0.25
Nodes (8): ViewRef, Bench, EventHub, JournalCell, JournalHints, JournalIndex, JournalRecipes, Redraw

### Community 97 - "ContentEnums.cs"
Cohesion: 0.25
Nodes (7): AspectGroup, Cheap, Engine, AspectScope, Bench, Link, Tile

### Community 98 - "EventKind"
Cohesion: 0.25
Nodes (8): EventKind, Boon, Choice, Find, Gamble, Hazard, Lore, Shrine

### Community 99 - "EventType"
Cohesion: 0.25
Nodes (8): EventType, Boon, Choice, Find, Gamble, Hazard, Lore, Shrine

### Community 101 - "LayoutData"
Cohesion: 0.48
Nodes (4): LayoutData, PointData, RectData, TileData

### Community 105 - "RunManager"
Cohesion: 0.29
Nodes (5): RunManager, AutoPlayer, LastSeed, Run, SeedOverride

### Community 106 - "EventNeedType"
Cohesion: 0.29
Nodes (6): EventNeedType, Catalyst, None, Rack, Redraw, Trial

### Community 107 - "RunPhase"
Cohesion: 0.29
Nodes (7): RunPhase, AspectOffer, DepthCleared, EventOffer, Lost, Playing, Won

### Community 114 - "TitleChoice"
Cohesion: 0.50
Nodes (4): TitleChoice, Begin, HowToPlay, Intro

## Knowledge Gaps
- **392 isolated node(s):** `FilePath`, `TriedCount`, `ColdCount`, `Seq`, `Hud` (+387 more)
  These have ≤1 connection - possible missing edges. (Counts symbols only; 573 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **34 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `RunState` connect `RunState` to `ContentDb`, `Phase3Tests`, `Journal`, `AutoPlayer`, `Athanor.Gameplay`, `GameObject`, `RunState.Ignite()`, `BenchRules`, `Test`, `HudView`, `Loc.T()`, `GameManager`, `BenchView`, `List`, `PrefabFixture`, `IgniteSequence`, `Label`, `Kind`, `Bench`, `TutorialTests`, `RarityTests`, `RunManager`, `RunPhase`?**
  _High betweenness centrality (0.094) - this node is a cross-community bridge._
- **Are the 124 inferred relationships involving `ContentDb` (e.g. with `BalanceReports.BalanceMatrix()` and `BalanceReports.BalanceReport()`) actually correct?**
  _`ContentDb` has 124 INFERRED edges - model-reasoned connections that need verification._
- **What connects `FilePath`, `TriedCount`, `ColdCount` to the rest of the system?**
  _392 weakly-connected nodes found - possible missing edges._
- **Should `ContentDb` be split into smaller, more focused modules?**
  _Cohesion score 0.04749015748031496 - nodes in this community are weakly interconnected._
- **Why does `GameManager` connect `GameManager` to `ContentDb`, `Journal`, `RunState`, `LayoutPinTests`, `Color`, `GameObject`, `IEnumerator`, `TitleView`, `AutoplayTraceTests.Play()`, `BenchView`, `TutorialManager`, `List`, `Settings`, `CatalystChoiceView`, `PrefabFixture.Destroy()`, `IgniteSequence`, `Clickable`, `JournalView`, `Sparks`, `PrefabBakeTests`, `EventView`, `DepthRig`, `system_collections`, `ScreenFx`, `DepthMood`, `Sprite`, `WindowView`, `ScreenFlow`, `Camera`, `PointerInput`, `FlowState`, `GameManager.DevKeys()`, `ContentFieldMigration.cs`, `Singleton`, `RunManager`, `GameManager.Update()`, `TitleChoice`?**
  _High betweenness centrality (0.083) - this node is a cross-community bridge._
- **Are the 49 inferred relationships involving `RunState` (e.g. with `GoldenShots.BenchCrowded()` and `GoldenShots.BenchTutorial()`) actually correct?**
  _`RunState` has 49 INFERRED edges - model-reasoned connections that need verification._
- **Should `Phase3Tests` be split into smaller, more focused modules?**
  _Cohesion score 0.07644529383659819 - nodes in this community are weakly interconnected._