# Graph Report - Assets  (2026-11-26)

## Corpus Check
- 454 files · ~140,263 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 1244 file(s) not represented in the graph (top: .meta 889, .asset 278, .shader 24)
- WARNING: 1244 of 1698 files seen (73%) have no supported extractor and are missing from this graph entirely. It may look complete while representing only part of the codebase.

## Summary
- 2930 nodes · 10218 edges · 110 communities (78 shown, 32 thin omitted)
- Extraction: 82% EXTRACTED · 18% INFERRED · 0% AMBIGUOUS · INFERRED: 1837 edges (avg confidence: 0.85)
- Token cost: 0 input · 0 output

## Community Hubs (Navigation)
- ContentDb
- Phase3Tests
- Test
- ChainTests
- GameManager
- AutoplayTraceTests.Play()
- ScreenFlowTests
- PrefabFixture.Destroy()
- unityengine
- Journal
- LayoutPinTests
- RunState
- TutorialManager
- Athanor.Gameplay
- IgniteTests
- RunState
- Loc.T()
- TestCase
- Athanor.Gameplay
- IEnumerator
- Sparks
- AutoPlayer
- Flipbook
- PrefabFixture
- TutorialSequence
- Clickable
- BenchView
- LangTests
- HudView
- CatalystChoiceView
- SpriteRenderer
- Journal
- RunState.Ignite()
- unityeditor
- IgniteSequence
- DepthMood
- SparkMesh
- Color
- AutoPlayer
- TitleView
- Px.At()
- StaticResetTests
- Loc.Set()
- TutorialTests
- Label
- MenuItem
- JournalView
- CatalystEffect
- ManagerInspectors.cs
- GameObject
- Vector2
- Settings
- RarityStars
- Vector2
- ContentDb
- Loc
- Clickable
- List
- ContentDatabase
- GoalStat
- Sprite
- FontKind
- ContentEnumTests
- Lang
- Camera
- RunManager
- AudioMute
- AspectEffect
- AtlasBuilder
- PrefabBakeTests
- Px.At()
- GoalStat
- EventNeed
- SaveManager
- GameObject
- DepthRig
- CameraSway
- EventOp
- EffectOp
- List
- Settings.FromJson()
- RarityTests
- DepthTable
- PrefabFixture.Destroy()
- TutorialActionKind
- ContentDatabase
- FlipbookImporter
- ContentDatabase
- GameManager.DevKeys()
- AudioMute
- Save.SetTutorialDone()
- KindOrNone
- Lang
- ManagerSceneMigration.Apply()
- TipKind
- Kind
- EventType
- SpriteImportRules
- AudioMute
- LayoutData
- DepthLayerTests
- RunState.Ignite()
- EventNeedType
- Reader
- FontBuilder
- Dev.cs
- TutorialSequenceEditor
- Camera

## God Nodes (most connected - your core abstractions)
1. `ContentDb` - 258 edges
2. `RunState` - 195 edges
3. `Journal` - 136 edges
4. `Phase3Tests` - 129 edges
5. `Athanor.Game` - 120 edges
6. `GameManager` - 100 edges
7. `Athanor.Gameplay` - 92 edges
8. `BenchView` - 66 edges
9. `TitleView` - 65 edges
10. `Label` - 65 edges

## Surprising Connections (you probably didn't know these)
- `Newcomer` --references--> `ContentDb`  [EXTRACTED]
  Editor/Simulation/Sim.cs → Scripts/Gameplay/ContentDb.cs
- `ContentTests` --references--> `ContentDb`  [EXTRACTED]
  Tests/EditMode/ContentTests.cs → Scripts/Gameplay/ContentDb.cs
- `IgniteTests` --references--> `ContentDb`  [EXTRACTED]
  Tests/EditMode/IgniteTests.cs → Scripts/Gameplay/ContentDb.cs
- `TutorialTests` --references--> `ContentDb`  [EXTRACTED]
  Tests/EditModeGame/TutorialTests.cs → Scripts/Gameplay/ContentDb.cs
- `ContentTests` --references--> `ContentData`  [EXTRACTED]
  Tests/EditMode/ContentTests.cs → Scripts/Gameplay/ContentData.cs

## Import Cycles
- None detected.

## Communities (110 total, 32 thin omitted)

### Community 0 - "ContentDb"
Cohesion: 0.05
Nodes (26): AspectEffects, Effects, GoalData, Ops, ProfileData, RewardLogData, StatIds, ContentDb (+18 more)

### Community 1 - "Phase3Tests"
Cohesion: 0.09
Nodes (7): AspectData, ContentData, MaterialData, RulesData, Phase3Tests, WalkOn, TestContent

### Community 2 - "Test"
Cohesion: 0.06
Nodes (4): RecipeData, Target, ContentTests, RulesTests

### Community 3 - "ChainTests"
Cohesion: 0.08
Nodes (6): BenchRules, ChainPlan, ChainPlanner, ChainStep, LinkPair, ChainTests

### Community 4 - "GameManager"
Cohesion: 0.05
Nodes (20): GameManager, Bench, Db, Flow, Journal, JournalView, Profile, Progress (+12 more)

### Community 5 - "AutoplayTraceTests.Play()"
Cohesion: 0.07
Nodes (19): ContentManager, Database, Db, Singleton, Exists, Instance, Singletons, AutoHookTests (+11 more)

### Community 6 - "ScreenFlowTests"
Cohesion: 0.07
Nodes (21): FlowState, AspectChoice, Bench, Boot, CatalystChoice, Descent, Ending, Event (+13 more)

### Community 7 - "PrefabFixture.Destroy()"
Cohesion: 0.17
Nodes (4): JournalCell, Settings, PrefabBakeTests, PrefabFixture

### Community 9 - "Journal"
Cohesion: 0.10
Nodes (7): HintData, JournalData, Journal, Hints, KnownPairs, JournalTests, TutorialFlowTests

### Community 11 - "RunState"
Cohesion: 0.10
Nodes (5): RunOptions, Unlocks, Count, LoreTests, HudTests

### Community 12 - "TutorialManager"
Cohesion: 0.09
Nodes (11): Layout, TutorialManager, Finished, Sequence, Trial, TutorialAreaDef, TutorialRect, TutorialStepDef (+3 more)

### Community 13 - "Athanor.Gameplay"
Cohesion: 0.06
Nodes (3): Athanor.Gameplay, RewardKinds, ScoreParts

### Community 14 - "IgniteTests"
Cohesion: 0.12
Nodes (4): Bench, Count, IgniteResolver, IgniteTests

### Community 15 - "RunState"
Cohesion: 0.06
Nodes (29): RunState, AspectOffer, Aspects, CurrentEvent, DepthBlocked, DepthHeated, EventOptions, HandDelta (+21 more)

### Community 16 - "Loc.T()"
Cohesion: 0.10
Nodes (9): Wording, Marker, Warm, ChoiceCard, EffectData, EventData, EventOption, EventOutcome (+1 more)

### Community 17 - "TestCase"
Cohesion: 0.10
Nodes (10): Athanor.Game.Tests, DepthLook, EventNote, TutorialGate, TutorialPlan, AskSkip, None, Run (+2 more)

### Community 19 - "IEnumerator"
Cohesion: 0.08
Nodes (13): Cutscene, Playing, Skipped, DepthLine, DescentArrive, DescentBanner, DescentLeave, DescentName (+5 more)

### Community 20 - "Sparks"
Cohesion: 0.08
Nodes (11): Layout, SparkParticles, LiveCount, Lane, Particle, Sparks, LaneCount, LiveCount (+3 more)

### Community 21 - "AutoPlayer"
Cohesion: 0.12
Nodes (5): Career, Newcomer, ColdCount, TriedCount, Sim

### Community 22 - "Flipbook"
Cohesion: 0.09
Nodes (10): FogSheet, Flame, Flipbook, IsPlaying, Fx, ScreenFx, ShakeTiming, GoalRow (+2 more)

### Community 23 - "PrefabFixture"
Cohesion: 0.09
Nodes (7): CardView, Layout, LinkView, Layout, Size, Tooltip, HotPathTests

### Community 24 - "TutorialSequence"
Cohesion: 0.08
Nodes (24): TutorialSequence, AreaKind, Fixed, ScriptTile, View, CatalystTrialDef, ScriptTrial, Catalyst (+16 more)

### Community 25 - "Clickable"
Cohesion: 0.08
Nodes (15): Clickable, PointerInput, Bench, Down, Held, Hover, Rig, Up (+7 more)

### Community 26 - "BenchView"
Cohesion: 0.09
Nodes (13): BenchView, Hud, InputEnabled, TileCount, From, Hand, None, Tile (+5 more)

### Community 27 - "LangTests"
Cohesion: 0.08
Nodes (3): LangEntry, LangFile, LangTests

### Community 28 - "HudView"
Cohesion: 0.07
Nodes (14): HudView, Gold, HoveredAspect, HoveredCatalyst, PauseAnchor, PauseEdgeColor, PauseHovered, PauseRect (+6 more)

### Community 29 - "CatalystChoiceView"
Cohesion: 0.08
Nodes (9): FxRefs, Card, CardRow, CatalystChoiceView, Joined, Off, On, Timing (+1 more)

### Community 30 - "SpriteRenderer"
Cohesion: 0.13
Nodes (4): ArcPath, Motion, Spawn, MotionTests

### Community 31 - "Journal"
Cohesion: 0.11
Nodes (10): Save, IntroSeen, TutorialDone, ProgressManager, Db, Journal, Profile, RunUnlocks (+2 more)

### Community 34 - "IgniteSequence"
Cohesion: 0.16
Nodes (6): IgniteSequence, Hurry, Timing, Anim, IgniteOutcomeView, ReactionEvent

### Community 35 - "DepthMood"
Cohesion: 0.10
Nodes (12): Look, Timing, WindowView, Soft, DepthMood, Preset, FogTint, DepthStage (+4 more)

### Community 36 - "SparkMesh"
Cohesion: 0.11
Nodes (8): Dust, Loop, Quad, SparkMesh, BaseMaterial, Count, Mesh, Vert

### Community 37 - "Color"
Cohesion: 0.08
Nodes (18): Layout, Layout, Look, Brew, Bursting, Closing, Gathering, Layout (+10 more)

### Community 38 - "AutoPlayer"
Cohesion: 0.14
Nodes (5): RunStats, AutoPlayer, KnowsTheWay, Move, RarityReportTests

### Community 39 - "TitleView"
Cohesion: 0.08
Nodes (11): Layout, PanelBox, Timing, TitleView, Asking, BeginPressed, CellMid, CreditY (+3 more)

### Community 42 - "StaticResetTests"
Cohesion: 0.10
Nodes (6): EditorGlobals, Art, Palette, RarityColors, GameTestSetup, StaticResetTests

### Community 45 - "Label"
Cohesion: 0.12
Nodes (7): Label, Alpha, Color, LineBox, Lines, Order, Width

### Community 46 - "MenuItem"
Cohesion: 0.15
Nodes (5): BalanceReports, CheckSprites, FixCandleImport, Reserialize, WebBuild

### Community 47 - "JournalView"
Cohesion: 0.10
Nodes (16): Grid, JournalView, CloseClick, Faded, HintsRect, IndexRect, Ink, IsOpen (+8 more)

### Community 48 - "CatalystEffect"
Cohesion: 0.09
Nodes (23): CatalystEffect, AllScale, CentreTwice, ChainFrom, ChainPlus, ComboBonus, DepthScale, Diagonals (+15 more)

### Community 49 - "ManagerInspectors.cs"
Cohesion: 0.11
Nodes (8): Athanor.EditorTools, ContentDatabaseEditor, MaterialDefinitionEditor, ProgressManagerEditor, RunManagerEditor, ContentSnapshot, FilePath, ContentSnapshotTests

### Community 52 - "Settings"
Cohesion: 0.17
Nodes (5): Layout, PauseMenu, Confirming, IsOpen, PauseTests

### Community 53 - "RarityStars"
Cohesion: 0.16
Nodes (4): RarityStars, GlyphCached, Root, Rarities

### Community 54 - "Vector2"
Cohesion: 0.14
Nodes (3): DepthMath, AnchorZ, DepthLayerTests

### Community 56 - "Loc"
Cohesion: 0.15
Nodes (7): LocalizationManager, Loc, Code, Current, NativeName, Vi, TestLang

### Community 57 - "Clickable"
Cohesion: 0.17
Nodes (6): RunOverView, Again, DepthName, DepthValue, Timing, RunOverDepthTests

### Community 58 - "List"
Cohesion: 0.19
Nodes (4): ResolveResult, EventResult, Rng, Baseline

### Community 59 - "ContentDatabase"
Cohesion: 0.16
Nodes (16): AspectDefinition, CatalystDefinition, ContentDatabase, RarityId, Common, Epic, Legendary, Mythic (+8 more)

### Community 60 - "GoalStat"
Cohesion: 0.11
Nodes (18): AspectGroup, Cheap, Engine, AspectScope, Bench, Link, Tile, GoalStat (+10 more)

### Community 61 - "Sprite"
Cohesion: 0.20
Nodes (3): ArtLibraryBuilder, FlipbookSheet, ArtLibrary

### Community 62 - "FontKind"
Cohesion: 0.14
Nodes (13): FontKind, Body, Logo, Name, Numeral, Tiny, Title, Fonts (+5 more)

### Community 63 - "ContentEnumTests"
Cohesion: 0.16
Nodes (9): EventKind, Boon, Choice, Find, Gamble, Hazard, Lore, Shrine (+1 more)

### Community 64 - "Lang"
Cohesion: 0.22
Nodes (3): Lang, Count, Keys

### Community 66 - "RunManager"
Cohesion: 0.13
Nodes (13): RunManager, AutoPlayer, LastSeed, Run, SeedOverride, IgniteOutcome, RunPhase, AspectOffer (+5 more)

### Community 67 - "AudioMute"
Cohesion: 0.21
Nodes (3): AudioMute, Muted, SettingsHapticsTests

### Community 68 - "AspectEffect"
Cohesion: 0.12
Nodes (17): AspectEffect, Anchor, Attuned, Bridge, Cold, Crossroads, Doubled, Echo (+9 more)

### Community 70 - "PrefabBakeTests"
Cohesion: 0.17
Nodes (7): GoalToast, Line, SlideOutAt, Title, Item, Layout, Timing

### Community 71 - "Px.At()"
Cohesion: 0.15
Nodes (9): TileView, FaceCentre, FaceSize, Hovered, Icon, Marked, Material, Pinged (+1 more)

### Community 72 - "GoalStat"
Cohesion: 0.20
Nodes (4): ContentNames, RewardKind, Catalyst, Starter

### Community 73 - "EventNeed"
Cohesion: 0.17
Nodes (10): EventNeed, Catalyst, None, Rack, Redraw, Trial, EventDefinition, EventEffectDef (+2 more)

### Community 74 - "SaveManager"
Cohesion: 0.15
Nodes (4): SaveManager, IntroSeen, Settings, TutorialDone

### Community 76 - "DepthRig"
Cohesion: 0.19
Nodes (6): DepthRig, CamOffset, Flat, ImageShift, RestPosition, WorldTexture

### Community 77 - "CameraSway"
Cohesion: 0.16
Nodes (3): CameraSway, SwayTiming, MainDepthTests

### Community 78 - "EventOp"
Cohesion: 0.13
Nodes (15): EventOp, BlockTile, CatalystGift, CatalystUpgrade, FirstTriple, GiveDiscovery, Hand, HeatTile (+7 more)

### Community 79 - "EffectOp"
Cohesion: 0.13
Nodes (15): EffectOp, BlockTile, CatalystGift, CatalystUpgrade, FirstTriple, GiveDiscovery, Hand, HeatTile (+7 more)

### Community 80 - "List"
Cohesion: 0.31
Nodes (3): Satchel, Total, SatchelTests

### Community 83 - "DepthTable"
Cohesion: 0.22
Nodes (5): TableShape, DepthTable, Shape, DepthFixture, Table

### Community 84 - "PrefabFixture.Destroy()"
Cohesion: 0.17
Nodes (9): GoalsView, BarLeft, CloseClick, Faded, Ink, IsOpen, Teal, Grid (+1 more)

### Community 85 - "TutorialActionKind"
Cohesion: 0.17
Nodes (12): TutorialActionKind, AwaitJournal, AwaitLay, Broken, ChainSummary, HoldWhileJournal, Ignite, MeasureBrokenChain (+4 more)

### Community 87 - "FlipbookImporter"
Cohesion: 0.20
Nodes (6): Box, FlipbookImporter, Frame, Info, Meta, Sheet

### Community 88 - "ContentDatabase"
Cohesion: 0.22
Nodes (4): ContentAssets, ContentValidation, ContentAssetTests, ContentValidationTests

### Community 93 - "KindOrNone"
Cohesion: 0.22
Nodes (9): KindOrNone, Beast, Crystal, Metal, None, Ore, Plant, Potion (+1 more)

### Community 96 - "TipKind"
Cohesion: 0.25
Nodes (8): TipKind, Aspect, Catalyst, Material, None, Pause, Redraw, TileNote

### Community 97 - "Kind"
Cohesion: 0.25
Nodes (8): Kind, Beast, Crystal, Metal, Ore, Plant, Potion, Powder

### Community 98 - "EventType"
Cohesion: 0.25
Nodes (8): EventType, Boon, Choice, Find, Gamble, Hazard, Lore, Shrine

### Community 101 - "LayoutData"
Cohesion: 0.48
Nodes (4): LayoutData, PointData, RectData, TileData

### Community 103 - "RunState.Ignite()"
Cohesion: 0.29
Nodes (7): AutoMove, ChoseAspect, ChoseCatalyst, ChoseEvent, None, ReadyToIgnite, Redrew

### Community 104 - "EventNeedType"
Cohesion: 0.29
Nodes (6): EventNeedType, Catalyst, None, Rack, Redraw, Trial

## Knowledge Gaps
- **392 isolated node(s):** `Aspects`, `Catalysts`, `Events`, `FinalDepth`, `Goals` (+387 more)
  These have ≤1 connection - possible missing edges. (Counts symbols only; 574 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **32 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ContentDb` connect `ContentDb` to `Phase3Tests`, `Test`, `ChainTests`, `GameManager`, `AutoplayTraceTests.Play()`, `PrefabFixture.Destroy()`, `Journal`, `LayoutPinTests`, `RunState`, `TutorialManager`, `Athanor.Gameplay`, `IgniteTests`, `RunState`, `Loc.T()`, `AutoPlayer`, `PrefabFixture`, `BenchView`, `CatalystChoiceView`, `Journal`, `RunState.Ignite()`, `IgniteSequence`, `Color`, `AutoPlayer`, `TutorialTests`, `JournalView`, `GameObject`, `ContentDb`, `List`, `RarityTests`, `PrefabFixture.Destroy()`, `ContentDatabase`, `ContentDatabase`, `Kind`?**
  _High betweenness centrality (0.119) - this node is a cross-community bridge._
- **Are the 124 inferred relationships involving `ContentDb` (e.g. with `BalanceReports.BalanceMatrix()` and `BalanceReports.BalanceReport()`) actually correct?**
  _`ContentDb` has 124 INFERRED edges - model-reasoned connections that need verification._
- **What connects `Aspects`, `Catalysts`, `Events` to the rest of the system?**
  _392 weakly-connected nodes found - possible missing edges._
- **Should `ContentDb` be split into smaller, more focused modules?**
  _Cohesion score 0.053584764749813295 - nodes in this community are weakly interconnected._
- **Why does `GameManager` connect `GameManager` to `ContentDb`, `AutoplayTraceTests.Play()`, `ScreenFlowTests`, `PrefabFixture.Destroy()`, `Journal`, `LayoutPinTests`, `TutorialManager`, `RunState`, `IEnumerator`, `Sparks`, `Flipbook`, `Clickable`, `BenchView`, `CatalystChoiceView`, `Journal`, `IgniteSequence`, `DepthMood`, `Color`, `TitleView`, `system_collections`, `StaticResetTests`, `JournalView`, `GameObject`, `Settings`, `Clickable`, `List`, `Sprite`, `Camera`, `RunManager`, `PrefabBakeTests`, `CameraSway`, `PrefabFixture.Destroy()`, `GameManager.DevKeys()`?**
  _High betweenness centrality (0.105) - this node is a cross-community bridge._
- **Are the 49 inferred relationships involving `RunState` (e.g. with `GoldenShots.BenchCrowded()` and `GoldenShots.BenchTutorial()`) actually correct?**
  _`RunState` has 49 INFERRED edges - model-reasoned connections that need verification._
- **Should `Phase3Tests` be split into smaller, more focused modules?**
  _Cohesion score 0.08683473389355742 - nodes in this community are weakly interconnected._