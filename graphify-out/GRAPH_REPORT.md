# Graph Report - Athanor  (2026-11-19)

## Corpus Check
- 358 files · ~116,289 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 587 file(s) not represented in the graph (top: .meta 494, .shader 24, .asset 19)
- WARNING: 587 of 945 files seen (62%) have no supported extractor and are missing from this graph entirely. It may look complete while representing only part of the codebase.

## Summary
- 2072 nodes · 7676 edges · 79 communities (53 shown, 26 thin omitted)
- Extraction: 81% EXTRACTED · 19% INFERRED · 0% AMBIGUOUS · INFERRED: 1481 edges (avg confidence: 0.85)
- Token cost: 0 input · 0 output

## Community Hubs (Navigation)
- Json Parser
- Game Source Files
- Phase 3 Rules Tests
- Dust and Spark Mesh
- Material and Reaction Events
- RunState Aspect Offers
- Settings JSON Reader
- Catalysts and Ignite Resolver
- ContentDb Tables
- Recipes and Reactivity
- Settings and View Binding
- BenchView Placement
- Loc and Aspect Instances
- Links and Rack Tiles
- Fonts and Kinds
- Core Source Files
- AutoPlayer Moves
- Score and Tutorial Controller
- Catalyst Choice View
- AutoPlayer Search
- Title Screen
- Profile and Rewards
- RunState Tests
- Bench Rules and Chains
- Depth Hit Testing
- Journal and Hints
- Run Options and Unlocks
- Chain Tests
- Depth Math
- Rng and Catalyst Draws
- HUD View
- Card View and Stars
- Reaction Cutscene
- Content Data Tests
- Pause Menu
- Save and Progress
- Cutscene Playback
- Test Setup and Loc Injection
- Journal View
- Depth Layers and Scene Fixture
- Depth Mood and Stage
- Clickable and Buttons
- Depth Look and Tutorial Gate
- Depth Rig
- Events and Lore Tests
- Catalyst Trial Tutorial Tests
- Art Library and Flame
- Atlas Builder
- Scoring Tests
- Window View Effects
- Run Over View
- Content Data Kinds
- Tutorial Plan Rules
- Flow States
- Camera Sway and ScreenFx
- Depth Table and Fog
- Tilt-Shift Feature
- Goal Toast
- Rarity Tests
- Flipbook Importer Data
- Tutorial Script
- Sprite Import Rules
- Layout Data
- View Capture
- Web Build
- Run Phases
- Flipbook Player
- Auto and Choice Hook Tests
- Main Depth Tests
- Font Builder
- Motion Tests
- Tile Origin
- Title Choices
- Dev Tools
- Kind Colors

## God Nodes (most connected - your core abstractions)
1. `ContentDb` - 230 edges
2. `RunState` - 180 edges
3. `Phase3Tests` - 129 edges
4. `Journal` - 119 edges
5. `GameFlow` - 100 edges
6. `Athanor.Game` - 88 edges
7. `Label` - 60 edges
8. `Athanor.Core` - 58 edges
9. `PrefabBakeTests` - 57 edges
10. `AutoPlayer` - 52 edges

## Surprising Connections (you probably didn't know these)
- `RunState` --defines--> `CanIgnite`  [EXTRACTED]
  Assets/Scripts/Core/RunStart.cs → Assets/Scripts/Core/RunState.cs
- `AutoPlayer` --references--> `BenchRules`  [EXTRACTED]
  Assets/Scripts/Core/AutoPlayer.cs → Assets/Scripts/Core/BenchRules.cs
- `AutoPlayer` --references--> `ScoreContext`  [EXTRACTED]
  Assets/Scripts/Core/AutoPlayer.cs → Assets/Scripts/Core/BenchRules.cs
- `AutoPlayer` --references--> `CatalystData`  [EXTRACTED]
  Assets/Scripts/Core/AutoPlayer.cs → Assets/Scripts/Core/ContentData.cs
- `AutoPlayer` --references--> `RecipeData`  [EXTRACTED]
  Assets/Scripts/Core/AutoPlayer.cs → Assets/Scripts/Core/ContentData.cs

## Import Cycles
- None detected.

## Communities (79 total, 26 thin omitted)

### Community 0 - "Json Parser"
Cohesion: 0.05
Nodes (10): Json, Lang, Count, Keys, LangEntry, LangFile, Code, Current (+2 more)

### Community 3 - "Dust and Spark Mesh"
Cohesion: 0.05
Nodes (15): Dust, Quad, SparkMesh, BaseMaterial, Count, Mesh, SparkParticles, LiveCount (+7 more)

### Community 4 - "Material and Reaction Events"
Cohesion: 0.08
Nodes (13): MaterialData, ReactionEvent, GoalReward, GameFlow, Bench, Db, Journal, JournalView (+5 more)

### Community 5 - "RunState Aspect Offers"
Cohesion: 0.06
Nodes (30): EventResult, RunState, AspectOffer, Aspects, CurrentEvent, DepthBlocked, DepthHeated, EventOptions (+22 more)

### Community 6 - "Settings JSON Reader"
Cohesion: 0.06
Nodes (9): Reader, AudioMute, Muted, AudioMuteHook, VisibilityCallback, Haptics, Supported, SettingsTests (+1 more)

### Community 7 - "Catalysts and Ignite Resolver"
Cohesion: 0.10
Nodes (6): CatalystData, Bench, Count, IgniteResolver, CanIgnite, IgniteTests

### Community 8 - "ContentDb Tables"
Cohesion: 0.09
Nodes (12): ContentDb, Aspects, Catalysts, Events, Goals, Materials, NoEventWeight, Recipes (+4 more)

### Community 10 - "Settings and View Binding"
Cohesion: 0.18
Nodes (4): Settings, ChoiceHookTests, PrefabBakeTests, PrefabFixture

### Community 11 - "BenchView Placement"
Cohesion: 0.06
Nodes (16): BenchView, Hud, InputEnabled, TileCount, Pick, Is, Slot, TileView (+8 more)

### Community 12 - "Loc and Aspect Instances"
Cohesion: 0.09
Nodes (7): AspectInstance, Loc, NativeName, Vi, Wording, Marker, ChoiceCard

### Community 13 - "Links and Rack Tiles"
Cohesion: 0.11
Nodes (9): LinkView, Vert, ArcPath, Motion, Align, Centre, Left, Right (+1 more)

### Community 14 - "Fonts and Kinds"
Cohesion: 0.08
Nodes (17): FontKind, Body, Logo, Name, Numeral, Tiny, Title, Fonts (+9 more)

### Community 15 - "Core Source Files"
Cohesion: 0.14
Nodes (4): RewardKinds, RarityReportTests, Athanor.Core, Athanor.Tests

### Community 16 - "AutoPlayer Moves"
Cohesion: 0.11
Nodes (10): AutoMove, ChoseAspect, ChoseCatalyst, ChoseEvent, None, ReadyToIgnite, Redrew, Newcomer (+2 more)

### Community 17 - "Score and Tutorial Controller"
Cohesion: 0.13
Nodes (9): ScoreParts, TutorialController, Finished, HandRect, IgniteRect, JournalRect, RackRect, RedrawRect (+1 more)

### Community 18 - "Catalyst Choice View"
Cohesion: 0.08
Nodes (10): Card, CatalystChoiceView, EventView, Outcome, Row, GoalRow, GoalsView, CloseClick (+2 more)

### Community 19 - "AutoPlayer Search"
Cohesion: 0.11
Nodes (7): AutoPlayer, KnowsTheWay, Move, ResolveResult, Career, RunStats, Sim

### Community 20 - "Title Screen"
Cohesion: 0.08
Nodes (7): TitleView, Asking, BeginPressed, CreditY, HowToPressed, IntroPressed, ProgressY

### Community 21 - "Profile and Rewards"
Cohesion: 0.15
Nodes (7): ProfileData, RewardLogData, Profile, Completed, Rewards, UnlockedCatalysts, UnlockedStarters

### Community 23 - "Bench Rules and Chains"
Cohesion: 0.15
Nodes (4): BenchRules, ChainPlan, ChainPlanner, ChainStep

### Community 24 - "Depth Hit Testing"
Cohesion: 0.14
Nodes (8): PointerInput, Bench, Down, Held, Hover, Up, World, DepthClickTests

### Community 25 - "Journal and Hints"
Cohesion: 0.18
Nodes (6): HintData, JournalData, Journal, Hints, KnownPairs, JournalTests

### Community 26 - "Run Options and Unlocks"
Cohesion: 0.11
Nodes (6): RunOptions, Unlocks, Count, IgniteOutcome, HudTests, TutorialFlowTests

### Community 28 - "Depth Math"
Cohesion: 0.12
Nodes (4): DepthMath, AnchorZ, DepthLayerTests, MainDevHookTests

### Community 29 - "Rng and Catalyst Draws"
Cohesion: 0.17
Nodes (4): Rng, Satchel, Total, SatchelTests

### Community 30 - "HUD View"
Cohesion: 0.08
Nodes (10): HudView, HoveredAspect, HoveredCatalyst, PauseAnchor, PauseHovered, PauseShown, PotencyPosition, RedrawAnchor (+2 more)

### Community 31 - "Card View and Stars"
Cohesion: 0.15
Nodes (3): CardView, RarityStars, Root

### Community 32 - "Reaction Cutscene"
Cohesion: 0.16
Nodes (4): ReactionCutscene, Loop, Anim, Px

### Community 33 - "Content Data Tests"
Cohesion: 0.17
Nodes (4): AspectData, ContentData, GoalData, TestContent

### Community 35 - "Pause Menu"
Cohesion: 0.15
Nodes (4): PauseMenu, Confirming, IsOpen, PauseTests

### Community 36 - "Save and Progress"
Cohesion: 0.15
Nodes (4): Save, IntroSeen, TutorialDone, SaveTests

### Community 37 - "Cutscene Playback"
Cohesion: 0.20
Nodes (4): Cutscene, Playing, Skipped, SimpleCutscene

### Community 38 - "Test Setup and Loc Injection"
Cohesion: 0.11
Nodes (3): EditorGlobals, GameTestSetup, LocTests

### Community 39 - "Journal View"
Cohesion: 0.13
Nodes (10): JournalCell, JournalView, CloseClick, HintsRect, IndexRect, IsOpen, PageClick, Pages (+2 more)

### Community 40 - "Depth Layers and Scene Fixture"
Cohesion: 0.17
Nodes (3): DepthLayer, MainSceneFixture, SceneBakeTests

### Community 41 - "Depth Mood and Stage"
Cohesion: 0.13
Nodes (9): DepthMood, Preset, FogTint, DepthStage, SwayOn, DepthSwitch, SpriteSwap, DepthTestControls (+1 more)

### Community 42 - "Clickable and Buttons"
Cohesion: 0.13
Nodes (4): Clickable, UiButton, Disabled, Text

### Community 43 - "Depth Look and Tutorial Gate"
Cohesion: 0.16
Nodes (3): DepthLook, DepthLookTests, Athanor.Game.Tests

### Community 44 - "Depth Rig"
Cohesion: 0.14
Nodes (6): DepthRig, CamOffset, Flat, ImageShift, RestPosition, WorldTexture

### Community 45 - "Events and Lore Tests"
Cohesion: 0.28
Nodes (5): EffectData, EventData, EventOption, EventOutcome, LoreTests

### Community 47 - "Art Library and Flame"
Cohesion: 0.18
Nodes (6): ArtLibraryBuilder, FlameSwap, Flame, FlipbookSheet, Art, ArtLibrary

### Community 51 - "Run Over View"
Cohesion: 0.18
Nodes (5): RunOverView, Again, DepthName, DepthValue, RunOverDepthTests

### Community 52 - "Content Data Kinds"
Cohesion: 0.14
Nodes (13): AspectEffects, Effects, Kind, Beast, Crystal, Metal, Ore, Plant (+5 more)

### Community 54 - "Tutorial Plan Rules"
Cohesion: 0.18
Nodes (5): TutorialPlan, AskSkip, None, Run, TutorialRules

### Community 55 - "Flow States"
Cohesion: 0.14
Nodes (14): FlowState, AspectChoice, Bench, Boot, CatalystChoice, Descent, Ending, Event (+6 more)

### Community 56 - "Camera Sway and ScreenFx"
Cohesion: 0.20
Nodes (3): CameraSway, ScreenFx, I

### Community 59 - "Goal Toast"
Cohesion: 0.24
Nodes (4): GoalToast, Line, Title, Item

### Community 61 - "Flipbook Importer Data"
Cohesion: 0.20
Nodes (6): Box, FlipbookImporter, Frame, Info, Meta, Sheet

### Community 62 - "Tutorial Script"
Cohesion: 0.38
Nodes (3): TutorialLay, TutorialScript, TutorialTrial

### Community 64 - "Layout Data"
Cohesion: 0.48
Nodes (4): LayoutData, PointData, RectData, TileData

### Community 67 - "Run Phases"
Cohesion: 0.29
Nodes (7): RunPhase, AspectOffer, DepthCleared, EventOffer, Lost, Playing, Won

### Community 74 - "Tile Origin"
Cohesion: 0.50
Nodes (4): From, Hand, None, Tile

### Community 75 - "Title Choices"
Cohesion: 0.50
Nodes (4): TitleChoice, Begin, HowToPlay, Intro

## Knowledge Gaps
- **190 isolated node(s):** `None`, `ChoseCatalyst`, `Redrew`, `ReadyToIgnite`, `ChoseAspect` (+185 more)
  These have ≤1 connection - possible missing edges. (Counts symbols only; 313 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **26 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `ContentDb` connect `ContentDb Tables` to `Phase 3 Rules Tests`, `Material and Reaction Events`, `RunState Aspect Offers`, `Catalysts and Ignite Resolver`, `Recipes and Reactivity`, `Settings and View Binding`, `BenchView Placement`, `Loc and Aspect Instances`, `Core Source Files`, `AutoPlayer Moves`, `Score and Tutorial Controller`, `Catalyst Choice View`, `AutoPlayer Search`, `Profile and Rewards`, `RunState Tests`, `Bench Rules and Chains`, `Journal and Hints`, `Run Options and Unlocks`, `Chain Tests`, `Rng and Catalyst Draws`, `HUD View`, `Reaction Cutscene`, `Content Data Tests`, `Save and Progress`, `Journal View`, `Events and Lore Tests`, `Catalyst Trial Tutorial Tests`, `Scoring Tests`, `Content Data Kinds`, `Journal Completeness`, `Rarity Tests`?**
  _High betweenness centrality (0.128) - this node is a cross-community bridge._
- **Are the 108 inferred relationships involving `ContentDb` (e.g. with `GameFlow.Awake()` and `ChainTests.Company_ConsumedEarlierInTheChain_NoLongerCounts()`) actually correct?**
  _`ContentDb` has 108 INFERRED edges - model-reasoned connections that need verification._
- **What connects `None`, `ChoseCatalyst`, `Redrew` to the rest of the system?**
  _190 weakly-connected nodes found - possible missing edges._
- **Should `Json Parser` be split into smaller, more focused modules?**
  _Cohesion score 0.05289193302891933 - nodes in this community are weakly interconnected._
- **Why does `GameFlow` connect `Material and Reaction Events` to `Game Source Files`, `RunState Aspect Offers`, `Settings JSON Reader`, `ContentDb Tables`, `Settings and View Binding`, `BenchView Placement`, `Loc and Aspect Instances`, `Links and Rack Tiles`, `Score and Tutorial Controller`, `Catalyst Choice View`, `AutoPlayer Search`, `Title Screen`, `Profile and Rewards`, `Journal and Hints`, `Run Options and Unlocks`, `Reaction Cutscene`, `Pause Menu`, `Save and Progress`, `Cutscene Playback`, `Test Setup and Loc Injection`, `Journal View`, `Depth Layers and Scene Fixture`, `Depth Mood and Stage`, `Art Library and Flame`, `Window View Effects`, `Run Over View`, `Flow States`, `Camera Sway and ScreenFx`, `Tilt-Shift Feature`, `Goal Toast`, `Auto and Choice Hook Tests`, `Main Depth Tests`, `Title Choices`?**
  _High betweenness centrality (0.126) - this node is a cross-community bridge._
- **Are the 40 inferred relationships involving `RunState` (e.g. with `GameFlow.PlayRun()` and `LoreTests.ALastLore_ThatIsNotALoreEvent_ChangesNothing()`) actually correct?**
  _`RunState` has 40 INFERRED edges - model-reasoned connections that need verification._
- **Should `Game Source Files` be split into smaller, more focused modules?**
  _Cohesion score 0.07484909456740442 - nodes in this community are weakly interconnected._