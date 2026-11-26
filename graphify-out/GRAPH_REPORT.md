# Graph Report - Assets  (2026-11-26)

## Corpus Check
- 454 files · ~140,356 words
- Verdict: corpus is large enough that graph structure adds value.

## Summary
- 2947 nodes · 10151 edges · 120 communities (81 shown, 39 thin omitted)
- Extraction: 82% EXTRACTED · 18% INFERRED · 0% AMBIGUOUS · INFERRED: 1800 edges (avg confidence: 0.85)
- Token cost: 0 input · 0 output

## Community Hubs (Navigation)
- IgniteTests
- Loc.T()
- ContentDb
- Sparks
- Phase3Tests
- ChainTests
- RunState
- unityengine
- RunState
- IEnumerator
- PrefabFixture.Destroy()
- ContentDb
- Athanor.Gameplay
- Test
- Label
- AutoplayTraceTests.Play()
- TestCase
- Clickable
- AutoPlayer
- BenchView
- Athanor.Gameplay
- TutorialManager
- SpriteRenderer
- HudView
- unityeditor
- TitleView
- ContentDb
- Color
- StaticResetTests
- RunState.Ignite()
- LayoutPinTests
- GameManager
- IgniteSequence
- List
- DepthRig
- Vector2
- GoalStat
- PrefabFixture
- AutoPlayer
- MenuItem
- CatalystChoiceView
- Flipbook
- JournalView
- Journal
- Sprite
- Px.At()
- ScreenFlowTests
- TutorialSequence
- ManagerInspectors.cs
- Lang
- Px.At()
- CatalystEffect
- Settings.FromJson()
- GameObject
- Settings
- TutorialTests
- LangTests
- ContentDatabase
- Clickable
- RarityTests
- FontKind
- LangTests
- TutorialManager
- Loc.Set()
- DepthMood
- DepthMood
- Journal
- RunManager
- AtlasBuilder
- RarityStars
- AspectEffect
- Camera
- Vector2
- ScreenFlowTests
- EventNeed
- GoalStat
- LayoutPinTests
- Loc
- GameObject
- EventOp
- EffectOp
- ScreenFlowTests
- TutorialManager
- DepthTable
- AudioMute
- Flipbook
- Test
- TutorialActionKind
- ContentDatabase
- GameManager.DevKeys()
- Loc
- FlipbookImporter
- SaveManager
- ContentDatabase
- AudioMute
- CameraSway
- AutoplayTraceTests.Play()
- AudioMute
- ContentEnumTests
- AudioMute
- KindOrNone
- TutorialSequence
- Kind
- EventType
- SpriteImportRules
- LayoutData
- ManagerSceneMigration.Apply()
- Journal
- RunState.Ignite()
- ContentDb
- EventNeedType
- Px.At()
- TutorialSequenceEditor
- CameraSway
- Flipbook
- GameManager
- StaticResetTests
- Dev.cs
- Dev.cs

## God Nodes (most connected - your core abstractions)
1. `ContentDb` - 255 edges
2. `RunState` - 193 edges
3. `Journal` - 132 edges
4. `Phase3Tests` - 129 edges
5. `Athanor.Game` - 120 edges
6. `GameManager` - 99 edges
7. `Athanor.Gameplay` - 90 edges
8. `BenchView` - 66 edges
9. `TitleView` - 65 edges
10. `PrefabFixture` - 65 edges

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

## Communities (120 total, 39 thin omitted)

### Community 0 - "IgniteTests"
Cohesion: 0.07
Nodes (8): HintData, JournalData, Bench, Count, IgniteResolver, CanIgnite, IgniteTests, JournalTests

### Community 1 - "Loc.T()"
Cohesion: 0.07
Nodes (6): Wording, Marker, Warm, ChoiceCard, GoalReward, AspectInstance

### Community 2 - "ContentDb"
Cohesion: 0.07
Nodes (14): ContentDb, Aspects, Catalysts, Events, FinalDepth, Goals, Materials, NoEventWeight (+6 more)

### Community 3 - "Sparks"
Cohesion: 0.05
Nodes (17): Layout, Quad, SparkMesh, BaseMaterial, Count, Mesh, Vert, SparkParticles (+9 more)

### Community 5 - "ChainTests"
Cohesion: 0.08
Nodes (6): BenchRules, ChainPlan, ChainPlanner, ChainStep, LinkPair, ChainTests

### Community 6 - "RunState"
Cohesion: 0.06
Nodes (32): EventData, EventOption, EventResult, RunState, AspectOffer, Aspects, CurrentEvent, DepthBlocked (+24 more)

### Community 8 - "RunState"
Cohesion: 0.09
Nodes (8): Journal, Hints, KnownPairs, RunOptions, Unlocks, Count, LoreTests, HudTests

### Community 9 - "IEnumerator"
Cohesion: 0.08
Nodes (11): Cutscene, Playing, Skipped, DepthLine, DescentArrive, DescentLeave, DescentName, IntroPage (+3 more)

### Community 10 - "PrefabFixture.Destroy()"
Cohesion: 0.16
Nodes (3): JournalCell, PrefabBakeTests, PrefabFixture

### Community 11 - "ContentDb"
Cohesion: 0.08
Nodes (14): AspectData, AspectEffects, Effects, Ops, ProfileData, Rarities, RewardLogData, StatIds (+6 more)

### Community 12 - "Athanor.Gameplay"
Cohesion: 0.07
Nodes (3): Athanor.Gameplay, RewardKinds, ScoreParts

### Community 14 - "Label"
Cohesion: 0.05
Nodes (26): Label, Alpha, Color, Order, Width, Card, Row, GoalRow (+18 more)

### Community 15 - "AutoplayTraceTests.Play()"
Cohesion: 0.10
Nodes (10): AutoHookTests, AutoplayTraceTests, Result, ManagerBootTests, MainScene, PrefsSnapshot, TraceRecorder, Probe (+2 more)

### Community 16 - "TestCase"
Cohesion: 0.10
Nodes (10): Athanor.Game.Tests, DepthLook, EventNote, TutorialGate, TutorialPlan, AskSkip, None, Run (+2 more)

### Community 17 - "Clickable"
Cohesion: 0.07
Nodes (17): Clickable, PointerInput, Bench, Down, Held, Hover, Rig, Up (+9 more)

### Community 18 - "AutoPlayer"
Cohesion: 0.12
Nodes (6): Career, Newcomer, ColdCount, TriedCount, Sim, RarityReportTests

### Community 19 - "BenchView"
Cohesion: 0.07
Nodes (22): BenchView, Hud, InputEnabled, TileCount, From, Hand, None, Tile (+14 more)

### Community 21 - "TutorialManager"
Cohesion: 0.10
Nodes (6): IgniteOutcomeView, TutorialManager, Finished, Sequence, Trial, TutorialStepDef

### Community 22 - "SpriteRenderer"
Cohesion: 0.11
Nodes (9): LinkView, ArcPath, Motion, Align, Centre, Left, Right, Spawn (+1 more)

### Community 23 - "HudView"
Cohesion: 0.07
Nodes (14): HudView, Gold, HoveredAspect, HoveredCatalyst, PauseAnchor, PauseEdgeColor, PauseHovered, PauseRect (+6 more)

### Community 24 - "unityeditor"
Cohesion: 0.07
Nodes (3): Athanor.EditorShared, FixCandleImport, Reserialize

### Community 25 - "TitleView"
Cohesion: 0.08
Nodes (10): Layout, PanelBox, TitleView, Asking, BeginPressed, CellMid, CreditY, HowToPressed (+2 more)

### Community 26 - "ContentDb"
Cohesion: 0.14
Nodes (3): ScoreContext, CatalystData, RecipeData

### Community 27 - "Color"
Cohesion: 0.08
Nodes (18): Layout, Look, Brew, Bursting, Closing, Gathering, Layout, Opening (+10 more)

### Community 28 - "StaticResetTests"
Cohesion: 0.08
Nodes (5): EditorGlobals, Singletons, Art, GameTestSetup, StaticResetTests

### Community 31 - "GameManager"
Cohesion: 0.07
Nodes (15): GameManager, Bench, Db, Flow, Journal, JournalView, Profile, Progress (+7 more)

### Community 32 - "IgniteSequence"
Cohesion: 0.17
Nodes (5): IgniteSequence, Hurry, Timing, MaterialData, ReactionEvent

### Community 33 - "List"
Cohesion: 0.17
Nodes (4): Rng, Satchel, Total, SatchelTests

### Community 34 - "DepthRig"
Cohesion: 0.10
Nodes (7): DepthRig, AnchorZ, CamOffset, Flat, ImageShift, RestPosition, WorldTexture

### Community 36 - "GoalStat"
Cohesion: 0.10
Nodes (15): ContentNames, GoalStat, BestIgnite, ChainLongest, ComboLargest, DepthReached, Ignites, JournalComplete (+7 more)

### Community 37 - "PrefabFixture"
Cohesion: 0.15
Nodes (3): CardView, Layout, HotPathTests

### Community 38 - "AutoPlayer"
Cohesion: 0.16
Nodes (4): AutoPlayer, KnowsTheWay, Move, ResolveResult

### Community 39 - "MenuItem"
Cohesion: 0.17
Nodes (3): BalanceReports, RunStats, WebBuild

### Community 40 - "CatalystChoiceView"
Cohesion: 0.12
Nodes (8): FxRefs, CardRow, CatalystChoiceView, Joined, Off, On, Timing, ChoiceHookTests

### Community 41 - "Flipbook"
Cohesion: 0.13
Nodes (5): FogSheet, Flipbook, IsPlaying, ScreenFx, ShakeTiming

### Community 42 - "JournalView"
Cohesion: 0.10
Nodes (16): Grid, JournalView, CloseClick, Faded, HintsRect, IndexRect, Ink, IsOpen (+8 more)

### Community 44 - "Sprite"
Cohesion: 0.14
Nodes (7): ArtLibraryBuilder, CheckSprites, Dust, FlameSwap, Flame, FlipbookSheet, ArtLibrary

### Community 46 - "Px.At()"
Cohesion: 0.09
Nodes (9): TileView, FaceCentre, FaceSize, Hovered, Icon, Marked, Material, Pinged (+1 more)

### Community 48 - "TutorialSequence"
Cohesion: 0.16
Nodes (11): TutorialSequence, AreaKind, Fixed, ScriptTile, View, CatalystTrialDef, TutorialAction, TutorialLayDef (+3 more)

### Community 49 - "ManagerInspectors.cs"
Cohesion: 0.11
Nodes (8): Athanor.EditorTools, ContentDatabaseEditor, MaterialDefinitionEditor, ProgressManagerEditor, RunManagerEditor, ContentSnapshot, FilePath, ContentSnapshotTests

### Community 50 - "Lang"
Cohesion: 0.17
Nodes (3): Lang, Count, Keys

### Community 51 - "Px.At()"
Cohesion: 0.15
Nodes (3): Loop, Anim, Px

### Community 52 - "CatalystEffect"
Cohesion: 0.09
Nodes (23): CatalystEffect, AllScale, CentreTwice, ChainFrom, ChainPlus, ComboBonus, DepthScale, Diagonals (+15 more)

### Community 53 - "Settings.FromJson()"
Cohesion: 0.21
Nodes (3): Reader, Settings, SettingsTests

### Community 55 - "Settings"
Cohesion: 0.17
Nodes (5): Layout, PauseMenu, Confirming, IsOpen, PauseTests

### Community 58 - "ContentDatabase"
Cohesion: 0.15
Nodes (17): AspectDefinition, CatalystDefinition, ContentDatabase, RarityId, Common, Epic, Legendary, Mythic (+9 more)

### Community 59 - "Clickable"
Cohesion: 0.15
Nodes (6): RunOverView, Again, DepthName, DepthValue, Timing, RunOverDepthTests

### Community 60 - "RarityTests"
Cohesion: 0.21
Nodes (4): ContentData, RulesData, RarityTests, TestContent

### Community 61 - "FontKind"
Cohesion: 0.12
Nodes (11): FontKind, Body, Logo, Name, Numeral, Tiny, Title, Fonts (+3 more)

### Community 62 - "LangTests"
Cohesion: 0.19
Nodes (3): Json, LangEntry, LangFile

### Community 63 - "TutorialManager"
Cohesion: 0.17
Nodes (8): ScriptTrial, Catalyst, Chain, Miss, Reaction, TutorialAreaDef, TutorialRect, TutorialAreaTests

### Community 65 - "DepthMood"
Cohesion: 0.13
Nodes (7): DepthMood, Preset, FogTint, DepthStage, SwayOn, DepthSwitch, SpriteSwap

### Community 66 - "DepthMood"
Cohesion: 0.20
Nodes (5): Layout, Look, Timing, WindowView, Soft

### Community 67 - "Journal"
Cohesion: 0.14
Nodes (6): ProgressManager, Db, Journal, Profile, RunUnlocks, Sandbox

### Community 68 - "RunManager"
Cohesion: 0.12
Nodes (13): RunManager, AutoPlayer, LastSeed, Run, SeedOverride, IgniteOutcome, RunPhase, AspectOffer (+5 more)

### Community 70 - "RarityStars"
Cohesion: 0.21
Nodes (3): RarityStars, GlyphCached, Root

### Community 71 - "AspectEffect"
Cohesion: 0.12
Nodes (17): AspectEffect, Anchor, Attuned, Bridge, Cold, Crossroads, Doubled, Echo (+9 more)

### Community 74 - "ScreenFlowTests"
Cohesion: 0.17
Nodes (6): ScreenFlow, CanResetProgress, InCutscene, JournalFrom, Sandbox, State

### Community 75 - "EventNeed"
Cohesion: 0.15
Nodes (11): EventNeed, Catalyst, None, Rack, Redraw, Trial, EventEffectDef, EventOptionDef (+3 more)

### Community 76 - "GoalStat"
Cohesion: 0.12
Nodes (15): AspectGroup, Cheap, Engine, AspectScope, Bench, Link, Tile, EventKind (+7 more)

### Community 78 - "Loc"
Cohesion: 0.20
Nodes (3): FontBuilder, LocalizationManager, TestLang

### Community 80 - "EventOp"
Cohesion: 0.13
Nodes (15): EventOp, BlockTile, CatalystGift, CatalystUpgrade, FirstTriple, GiveDiscovery, Hand, HeatTile (+7 more)

### Community 81 - "EffectOp"
Cohesion: 0.13
Nodes (15): EffectOp, BlockTile, CatalystGift, CatalystUpgrade, FirstTriple, GiveDiscovery, Hand, HeatTile (+7 more)

### Community 82 - "ScreenFlowTests"
Cohesion: 0.14
Nodes (14): FlowState, AspectChoice, Bench, Boot, CatalystChoice, Descent, Ending, Event (+6 more)

### Community 83 - "TutorialManager"
Cohesion: 0.21
Nodes (5): Baseline, StepRects, TutorialStepsBakeTests, Baseline, Dir

### Community 84 - "DepthTable"
Cohesion: 0.24
Nodes (5): TableShape, DepthTable, Shape, DepthFixture, Table

### Community 88 - "TutorialActionKind"
Cohesion: 0.17
Nodes (12): TutorialActionKind, AwaitJournal, AwaitLay, Broken, ChainSummary, HoldWhileJournal, Ignite, MeasureBrokenChain (+4 more)

### Community 91 - "Loc"
Cohesion: 0.25
Nodes (5): Loc, Code, Current, NativeName, Vi

### Community 92 - "FlipbookImporter"
Cohesion: 0.20
Nodes (6): Box, FlipbookImporter, Frame, Info, Meta, Sheet

### Community 93 - "SaveManager"
Cohesion: 0.24
Nodes (4): SaveManager, IntroSeen, Settings, TutorialDone

### Community 94 - "ContentDatabase"
Cohesion: 0.22
Nodes (4): ContentAssets, ContentValidation, ContentAssetTests, ContentValidationTests

### Community 97 - "AutoplayTraceTests.Play()"
Cohesion: 0.22
Nodes (6): ContentManager, Database, Db, Singleton, Exists, Instance

### Community 101 - "KindOrNone"
Cohesion: 0.22
Nodes (9): KindOrNone, Beast, Crystal, Metal, None, Ore, Plant, Potion (+1 more)

### Community 102 - "TutorialSequence"
Cohesion: 0.25
Nodes (8): ViewRef, Bench, EventHub, JournalCell, JournalHints, JournalIndex, JournalRecipes, Redraw

### Community 103 - "Kind"
Cohesion: 0.25
Nodes (8): Kind, Beast, Crystal, Metal, Ore, Plant, Potion, Powder

### Community 104 - "EventType"
Cohesion: 0.25
Nodes (8): EventType, Boon, Choice, Find, Gamble, Hazard, Lore, Shrine

### Community 106 - "LayoutData"
Cohesion: 0.48
Nodes (4): LayoutData, PointData, RectData, TileData

### Community 108 - "Journal"
Cohesion: 0.29
Nodes (3): Save, IntroSeen, TutorialDone

### Community 109 - "RunState.Ignite()"
Cohesion: 0.29
Nodes (7): AutoMove, ChoseAspect, ChoseCatalyst, ChoseEvent, None, ReadyToIgnite, Redrew

### Community 111 - "EventNeedType"
Cohesion: 0.29
Nodes (6): EventNeedType, Catalyst, None, Rack, Redraw, Trial

### Community 116 - "GameManager"
Cohesion: 0.50
Nodes (4): TitleChoice, Begin, HowToPlay, Intro

## Knowledge Gaps
- **392 isolated node(s):** `Aspects`, `Catalysts`, `Events`, `FinalDepth`, `Goals` (+387 more)
  These have ≤1 connection - possible missing edges. (Counts symbols only; 583 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **39 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ContentDb` connect `ContentDb` to `IgniteTests`, `Loc.T()`, `Phase3Tests`, `ChainTests`, `RunState`, `RunState`, `PrefabFixture.Destroy()`, `ContentDb`, `Athanor.Gameplay`, `Test`, `Label`, `AutoPlayer`, `BenchView`, `ContentDb`, `Color`, `RunState.Ignite()`, `GameManager`, `IgniteSequence`, `List`, `PrefabFixture`, `AutoPlayer`, `MenuItem`, `CatalystChoiceView`, `JournalView`, `Lang`, `GameObject`, `TutorialTests`, `RarityTests`, `Journal`, `Flipbook`, `Test`, `ContentDatabase`, `ContentDatabase`, `AutoplayTraceTests.Play()`, `Kind`, `ContentDb`?**
  _High betweenness centrality (0.109) - this node is a cross-community bridge._
- **Are the 123 inferred relationships involving `ContentDb` (e.g. with `BalanceReports.BalanceMatrix()` and `BalanceReports.BalanceReport()`) actually correct?**
  _`ContentDb` has 123 INFERRED edges - model-reasoned connections that need verification._
- **What connects `Aspects`, `Catalysts`, `Events` to the rest of the system?**
  _392 weakly-connected nodes found - possible missing edges._
- **Should `IgniteTests` be split into smaller, more focused modules?**
  _Cohesion score 0.07368421052631578 - nodes in this community are weakly interconnected._
- **Why does `RunState` connect `RunState` to `IgniteTests`, `Loc.T()`, `ContentDb`, `Phase3Tests`, `ChainTests`, `RunState`, `PrefabFixture.Destroy()`, `ContentDb`, `Athanor.Gameplay`, `AutoPlayer`, `BenchView`, `HudView`, `ContentDb`, `RunState.Ignite()`, `GameManager`, `IgniteSequence`, `List`, `PrefabFixture`, `AutoPlayer`, `MenuItem`, `GameObject`, `TutorialTests`, `RarityTests`, `RunManager`?**
  _High betweenness centrality (0.093) - this node is a cross-community bridge._
- **Are the 48 inferred relationships involving `RunState` (e.g. with `GoldenShots.BenchCrowded()` and `GoldenShots.BenchTutorial()`) actually correct?**
  _`RunState` has 48 INFERRED edges - model-reasoned connections that need verification._
- **Should `Loc.T()` be split into smaller, more focused modules?**
  _Cohesion score 0.06702702702702702 - nodes in this community are weakly interconnected._