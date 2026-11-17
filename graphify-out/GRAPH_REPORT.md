# Graph Report - Crucible_Fable  (2026-11-17)

## Corpus Check
- Scoped run: Athanor/ and docs/ only (245 files · ~176,764 doc words). The full repo (3,166 files) was not built.

## Summary
- 3156 nodes · 9117 edges · 173 communities (96 shown, 77 thin omitted)
- Extraction: 85% EXTRACTED · 15% INFERRED · 0% AMBIGUOUS · INFERRED: 1330 edges (avg confidence: 0.85)
- Token cost: 0 input · 0 output

## Community Hubs (Navigation)
- Core data types
- Content catalogs and events
- Run rules and tests
- Depth test scene builder
- Score and tutorial
- Settings and JSON reader
- Bench and HUD views
- Auto-player bot
- Bench placement rules
- Content enums and hints
- Cutscene and mockup scripts
- Reaction events and goals
- Tutorial plan
- Mockup art generators
- Scene and bench view builders
- Catalyst and ignite resolver
- Profile and reward log
- Cutscene mockup art
- Flow states and choices
- Effects and event options
- Rack slots and screen FX
- Scene mockup scripts
- HD-2D screenshot tooling
- Package manifest entries
- Bot pairing and content lookup
- HUD anchors and aspects
- Mockup app scripts
- Sprite atlas builder
- Card view animations
- Depth evidence and bench drops
- Auto-player, rules and journal
- Run state ignite and catalyst
- Spark particles
- Ignite outcome and scoring
- Package manifest entries
- Dust particles and loop
- Package manifest entries
- Loop mockup charts
- Package lock entries
- Package manifest entries
- Phase 4 and 5 open decisions
- Localization tests
- Title and pause screens
- Depth math
- Label layout
- CI and release gates
- Package manifest entries
- Web and engine breaking changes
- Cutscene flow
- Label line-box metrics
- Package manifest entries
- Phase 2 balance and design
- Phase 5 prep and UI mockup
- Package manifest entries
- Camera sway
- Package manifest entries
- Depth test capture
- Package manifest entries
- Package manifest entries
- Package manifest entries
- Phase 2 implementation plan
- Depth rig viewport
- Package manifest entries
- Package manifest entries
- Mockup main script
- Lang and JSON parser
- Package manifest entries
- Package manifest entries
- Package manifest entries
- Unity package manifest
- Unity 2D package manifest
- Unreal 5.8 changes
- Unreal deprecated APIs
- Satchel
- Godot 4.7 changes
- Depth FX mockup and bench
- Aspect effects and materials
- Lang content keys
- Tilt-shift feature
- Unity 6.3 changes
- Depth table
- Rarity tests
- Auto-hook tests
- Package manifest entries
- JSON reader
- Package manifest entries
- Phase 4 depth design decision
- Flipbook importer
- Localization loader
- Lore event rules and balance
- Run-over depth tests
- Sprite atlas and VFX audit
- Unity 6.3 module reference
- HD-2D mockup and spec
- Tutorial and lore plan
- Unreal input migration
- Architecture and API decisions
- Auto-move and choices
- Run phase and offers
- Package manifest entries
- Package manifest entries
- Unreal ability system
- Hand and tile enums
- Requirement registry
- ADR and docs standards
- Phase 1 MVP devlog
- Fog and HD-2D depth plan
- Unreal Chaos physics
- Unreal rendering reference
- Kind colors
- Tween motion wrapper
- Migration guide v1.1
- PrimeTween package
- Unity 2D package
- Unity 2D package
- Unity 2D package
- Unity 2D package
- Unity package
- Unity package
- Unity package
- Unity package
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity module
- Unity URP package
- Unity test framework
- Unity UGUI package
- Unity visual scripting
- Font licence blocker
- Unreal navigation
- Gameplay camera system
- Mockup build scripts
- FX sheet data
- Depth build script
- HD-2D build script
- TMP line-break data
- TMP line-break data
- Unity project version
- Godot navigation reference
- Godot networking reference
- Unity audio reference
- Unity navigation reference
- Unity physics reference

## God Nodes (most connected - your core abstractions)
1. `ContentDb` - 232 edges
2. `RunState` - 174 edges
3. `Phase3Tests` - 129 edges
4. `Journal` - 107 edges
5. `GameFlow` - 90 edges
6. `Athanor.Game` - 77 edges
7. `Athanor.Core` - 58 edges
8. `AutoPlayer` - 52 edges
9. `Profile` - 49 edges
10. `ContentTests` - 48 edges

## Surprising Connections (you probably didn't know these)
- `docs/mockup/cutscene/index.html (opening cutscene mockup)` --semantically_similar_to--> `Reaction cutscene 7-second loop`  [INFERRED] [semantically similar]
  docs/devlogs/phase-4-lore.md → docs/mockup/mockup.html
- `Unreal Mass Entity` --semantically_similar_to--> `Unity 6.3 DOTS and Entities (ECS)`  [INFERRED] [semantically similar]
  docs/engine-reference/unreal/PLUGINS.md → docs/engine-reference/unity/plugins/dots-entities.md
- `Decision 9: ShouldRun moved to Core` --shares_data_with--> `TutorialScript as data`  [INFERRED]
  docs/superpowers/plans/2026-11-15-athanor-phase5-briefs.md → docs/superpowers/plans/2026-11-13-athanor-phase4-tutorial-lore.md
- `RunState` --defines--> `CanIgnite`  [EXTRACTED]
  Athanor/Assets/Scripts/Core/RunStart.cs → Athanor/Assets/Scripts/Core/RunState.cs
- `WebGL index.html template (rotate layer, canvas fit)` --references--> `tools/web/rotate_sync.mjs (rotate text sync)`  [EXTRACTED]
  Athanor/Assets/WebGLTemplates/Athanor/index.html → docs/CI.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **Phase 3 goal and reward loop** — docs_devlogs_phase_3_profile_goals, docs_devlogs_phase_3_goals_never_outnumber_locked, docs_devlogs_phase_3_catalysts, docs_devlogs_phase_3_starters, docs_devlogs_phase_3_starter_pouch [EXTRACTED 1.00]
- **Phase 4 depth variants and their building blocks** — docs_devlogs_phase_4_depth_variant_a_parallax, docs_devlogs_phase_4_depth_variant_b_perspective, docs_devlogs_phase_4_depth_variant_c_descent, docs_devlogs_phase_4_depth_projection_trick, docs_devlogs_phase_4_depth_fog [EXTRACTED 1.00]
- **CI merge gates** — docs_ci_ci_gate, docs_ci_release_gate, docs_ci_baseline_json, docs_ci_unity_lint, tools_ci_run_sh [EXTRACTED 1.00]
- **Phase 5 owner decisions and gates** — docs_devlogs_phase_5_prep_owner_decisions, docs_devlogs_phase_5_prep_gate_g2, docs_devlogs_phase_5_prep_size_ceiling, docs_devlogs_phase_5_prep_release_gate_ci, docs_devlogs_phase_5_prep_rarity_weights, docs_devlogs_phase_5_prep_fmod_dropped [INFERRED 0.75]
- **Godot 4.7 post-cutoff changes** — docs_engine_reference_godot_version, docs_engine_reference_godot_breaking_changes, docs_engine_reference_godot_deprecated_apis, docs_engine_reference_godot_jolt_default, docs_engine_reference_godot_area_mask_default, docs_engine_reference_godot_device_id [INFERRED 0.85]
- **Three worldseed roads and endings** — docs_devlogs_phase_5_endings_lore_worldseeds, docs_devlogs_phase_5_endings_lore_roots_of_worlds, docs_devlogs_phase_5_endings_lore_dead_end_materials, docs_devlogs_phase_5_endings_lore_any_depth_win [INFERRED 0.75]
- **Unity DOTS stack (Entities, Burst, Jobs API overhaul)** — docs_engine_reference_unity_plugins_dots_entities, docs_engine_reference_unity_current_best_practices_burst_jobs, docs_engine_reference_unity_breaking_changes_entities_overhaul [INFERRED 0.75]
- **Unreal 5.8 lighting and material stack (Substrate, MegaLights)** — docs_engine_reference_unreal_breaking_changes_substrate, docs_engine_reference_unreal_breaking_changes_megalights, docs_engine_reference_unreal_version [INFERRED 0.75]
- **UE 5.8 upgrade changes documented per module** — docs_engine_reference_unreal_modules_animation, docs_engine_reference_unreal_modules_audio, docs_engine_reference_unreal_modules_input, docs_engine_reference_unreal_modules_navigation, docs_engine_reference_unreal_modules_networking, docs_engine_reference_unreal_modules_physics [EXTRACTED 1.00]
- **Web toolchain major-version breaking changes** — docs_engine_reference_web_breaking_changes_vite8, docs_engine_reference_web_breaking_changes_typescript7, docs_engine_reference_web_breaking_changes_vitest5, docs_engine_reference_web_breaking_changes_playwright [EXTRACTED 1.00]
- **Phaser 4, PixiJS 8 and three.js API migrations** — docs_engine_reference_web_phaser_filters, docs_engine_reference_web_phaser_pipelines_removed, docs_engine_reference_web_pixijs_application_init, docs_engine_reference_web_threejs_timer, docs_engine_reference_web_threejs_pcfsoft_removed [EXTRACTED 1.00]
- **Phase 4 part 1: tutorial and lore** — docs_superpowers_plans_2026_11_13_athanor_phase4_tutorial_lore_tutorial_script, docs_superpowers_plans_2026_11_13_athanor_phase4_tutorial_lore_run_options_hooks, docs_superpowers_plans_2026_11_13_athanor_phase4_tutorial_lore_redraw_button, docs_superpowers_plans_2026_11_13_athanor_phase4_tutorial_lore_intro_pages, docs_superpowers_plans_2026_11_13_athanor_phase4_tutorial_lore_lore_events [INFERRED 0.75]
- **Phase 3 pillars** — docs_superpowers_specs_2026_11_12_athanor_phase3_design_profile_goals, docs_superpowers_specs_2026_11_12_athanor_phase3_design_aspects, docs_superpowers_specs_2026_11_12_athanor_phase3_design_bench_rules, docs_superpowers_specs_2026_11_12_athanor_phase3_design_events [INFERRED 0.85]
- **Phase 5 dispatch rules** — docs_superpowers_plans_2026_11_15_athanor_phase5_wave_structure, docs_superpowers_plans_2026_11_15_athanor_phase5_gates_5b, docs_superpowers_plans_2026_11_15_athanor_phase5_common_rules [INFERRED 0.85]

## Communities (173 total, 77 thin omitted)

### Community 0 - "Core data types"
Cohesion: 0.05
Nodes (17): ScoreContext, GoalData, RecipeData, ContentDb, Aspects, Catalysts, Events, Goals (+9 more)

### Community 1 - "Content catalogs and events"
Cohesion: 0.05
Nodes (34): AspectData, EventData, EventResult, RunState, AspectOffer, Aspects, CurrentEvent, DepthBlocked (+26 more)

### Community 3 - "Depth test scene builder"
Cohesion: 0.05
Nodes (21): DepthTestBuilder, SceneBuilder, DepthLayer, DepthMood, Preset, FogTint, DepthSwitch, FlameSwap (+13 more)

### Community 4 - "Score and tutorial"
Cohesion: 0.08
Nodes (15): ScoreParts, TutorialLay, TutorialScript, TutorialTrial, TutorialController, Finished, HandRect, IgniteRect (+7 more)

### Community 5 - "Settings and JSON reader"
Cohesion: 0.06
Nodes (13): Settings, Reader, Save, IntroSeen, TutorialDone, AudioMute, Muted, AudioMuteHook (+5 more)

### Community 7 - "Auto-player bot"
Cohesion: 0.08
Nodes (8): AutoPlayer, KnowsTheWay, Move, RunOptions, Unlocks, Count, BalanceMatrixTests, HudTests

### Community 8 - "Bench placement rules"
Cohesion: 0.09
Nodes (5): BenchRules, ChainPlan, ChainPlanner, ChainStep, ChainTests

### Community 9 - "Content enums and hints"
Cohesion: 0.08
Nodes (14): HintData, JournalData, Kind, Beast, Crystal, Metal, Ore, Plant (+6 more)

### Community 10 - "Cutscene and mockup scripts"
Cohesion: 0.06
Nodes (45): bars(), buildGallery(), buildPanel(), buildScenes(), CANDLES, choiceRow(), CHOICES, click() (+37 more)

### Community 11 - "Reaction events and goals"
Cohesion: 0.07
Nodes (17): ReactionEvent, GoalReward, GameFlow, Bench, Db, I, Journal, JournalView (+9 more)

### Community 12 - "Tutorial plan"
Cohesion: 0.06
Nodes (10): TutorialPlan, AskSkip, None, Run, DepthLook, TutorialRules, WindowView, DepthLookTests (+2 more)

### Community 13 - "Mockup art generators"
Cohesion: 0.09
Nodes (40): ashPile(), bowlPowder(), cork(), crystals(), cubes(), emberCoal(), eye(), fang() (+32 more)

### Community 14 - "Scene and bench view builders"
Cohesion: 0.17
Nodes (6): Align, Centre, Left, Right, Make, Row

### Community 15 - "Catalyst and ignite resolver"
Cohesion: 0.11
Nodes (7): CatalystData, Bench, Count, IgniteResolver, ResolveResult, CanIgnite, IgniteTests

### Community 16 - "Profile and reward log"
Cohesion: 0.11
Nodes (8): ProfileData, RewardLogData, Profile, Completed, Rewards, UnlockedCatalysts, UnlockedStarters, SaveTests

### Community 17 - "Cutscene mockup art"
Cohesion: 0.09
Nodes (38): ART, BAY, Buf, buildArt(), clamp01(), COL, crisp(), ditherCache (+30 more)

### Community 18 - "Flow states and choices"
Cohesion: 0.06
Nodes (25): FlowState, AspectChoice, Bench, Boot, CatalystChoice, Descent, Ending, Event (+17 more)

### Community 19 - "Effects and event options"
Cohesion: 0.08
Nodes (10): EffectData, EventOption, EventOutcome, AspectInstance, Wording, Marker, GoalsView, IsOpen (+2 more)

### Community 20 - "Rack slots and screen FX"
Cohesion: 0.08
Nodes (16): Slot, ScreenFx, I, Motion, ArcPath, Card, EventView, Row (+8 more)

### Community 21 - "Scene mockup scripts"
Cohesion: 0.08
Nodes (34): BAY, benchBase(), benchFrame(), benchStatic(), boot(), candleSpr, card(), CAUL (+26 more)

### Community 22 - "HD-2D screenshot tooling"
Cohesion: 0.05
Nodes (25): all, CAMERA, CASES, chrome, DEPTHS, errors, fails, FX (+17 more)

### Community 23 - "Package manifest entries"
Cohesion: 0.05
Nodes (42): dependencies, depth, source, url, version, depth, source, url (+34 more)

### Community 24 - "Bot pairing and content lookup"
Cohesion: 0.13
Nodes (5): RoleSimTests, Career, Newcomer, ColdCount, TriedCount

### Community 25 - "HUD anchors and aspects"
Cohesion: 0.09
Nodes (13): HudView, HoveredAspect, HoveredCatalyst, PauseAnchor, PauseHovered, PauseShown, PotencyPosition, RedrawAnchor (+5 more)

### Community 26 - "Mockup app scripts"
Cohesion: 0.13
Nodes (37): benchBg(), box(), button(), descent(), dialog(), F, FONT, groups (+29 more)

### Community 27 - "Sprite atlas builder"
Cohesion: 0.09
Nodes (4): AtlasBuilder, FontBuilder, SpriteImportRules, WebBuild

### Community 28 - "Card view animations"
Cohesion: 0.09
Nodes (9): CardView, RarityStars, Root, JournalView, HintsRect, IndexRect, IsOpen, Pages (+1 more)

### Community 30 - "Depth evidence and bench drops"
Cohesion: 0.10
Nodes (14): BenchView, Hud, InputEnabled, Pick, Is, LinkView, TileView, Hovered (+6 more)

### Community 31 - "Auto-player, rules and journal"
Cohesion: 0.17
Nodes (3): RewardKinds, Athanor.Core, Athanor.Tests

### Community 33 - "Spark particles"
Cohesion: 0.09
Nodes (9): SparkParticles, LiveCount, Sparks, I, Lane, LaneCount, LiveCount, Particle (+1 more)

### Community 34 - "Ignite outcome and scoring"
Cohesion: 0.13
Nodes (3): ContentData, IgniteOutcome, TutorialTests

### Community 35 - "Package manifest entries"
Cohesion: 0.06
Nodes (34): dependencies, depth, source, version, dependencies, depth, source, version (+26 more)

### Community 36 - "Dust particles and loop"
Cohesion: 0.10
Nodes (7): Dust, Loop, SparkMesh, Count, Mesh, Quad, Vert

### Community 37 - "Package manifest entries"
Cohesion: 0.06
Nodes (32): dependencies, depth, source, version, dependencies, depth, source, url (+24 more)

### Community 38 - "Loop mockup charts"
Cohesion: 0.14
Nodes (23): bar(), buildLive(), canvas640(), chart(), dustCheck(), dustXY(), emitBefore(), hzCheck() (+15 more)

### Community 39 - "Package lock entries"
Cohesion: 0.08
Nodes (29): dependencies, depth, source, version, dependencies, depth, source, version (+21 more)

### Community 40 - "Package manifest entries"
Cohesion: 0.07
Nodes (27): dependencies, depth, source, version, dependencies, depth, source, version (+19 more)

### Community 41 - "Phase 4 and 5 open decisions"
Cohesion: 0.07
Nodes (17): FMOD for Unity 2.03 (later dropped), Newcomer baseline BalanceMatrix numbers, OODA re-check of Phase 4 content, English to Vietnamese glossary, Haptics per platform, Lang class in Core, Title Settings panel, Fixed catalyst offer (Bellows, Heart Tile, First Spark) (+9 more)

### Community 43 - "Title and pause screens"
Cohesion: 0.11
Nodes (7): TitleView, Asking, BeginPressed, CreditY, HowToPressed, IntroPressed, ProgressY

### Community 44 - "Depth math"
Cohesion: 0.15
Nodes (3): DepthMath, AnchorZ, DepthLayerTests

### Community 45 - "Label layout"
Cohesion: 0.13
Nodes (15): Label, Alpha, Color, LineBox, Lines, Order, Text, Width (+7 more)

### Community 46 - "CI and release gates"
Cohesion: 0.11
Nodes (21): WebGL index.html template (rotate layer, canvas fit), CI merge rule doc, Lint baseline frozen at 158 findings, only shrinks, ci-gate job (needs every job success), release-gate job (tags, size budget 11,824,672 B, secrets), Unity lint and repo guard rules table, Collaborative Design Principle, AskUserQuestion explain then capture pattern (+13 more)

### Community 47 - "Package manifest entries"
Cohesion: 0.09
Nodes (24): dependencies, depth, source, url, version, dependencies, depth, source (+16 more)

### Community 48 - "Web and engine breaking changes"
Cohesion: 0.11
Nodes (22): Web Toolchain Breaking Changes, Playwright 1.53 to 1.64 Changes, TypeScript 7 Native Compiler, Vite 8 Rolldown and Oxc Migration, Vitest 5 Breaking Changes, Web Current Best Practices, Frame Delta Clamp After Hidden Tab, Opening Builds from file:// Protocol (+14 more)

### Community 49 - "Cutscene flow"
Cohesion: 0.20
Nodes (4): Cutscene, Playing, Skipped, SimpleCutscene

### Community 51 - "Package manifest entries"
Cohesion: 0.09
Nodes (23): dependencies, depth, source, version, dependencies, depth, source, version (+15 more)

### Community 52 - "Phase 2 balance and design"
Cohesion: 0.14
Nodes (19): Phase 2 balance: informed bot 85%, random player 0%, Combos of 2 to 5 items and chain reactions, Phase 2 handoff, Open for owner: difficulty at top of range (85%), Reaction Index and hub tile selection, Phase 2 devlog: Reaction Index, combos and chains, 16 run-long bench aspects, pick one of three, Phase 3 balance: fresh 67%, full 85%, random 0% (+11 more)

### Community 53 - "Phase 5 prep and UI mockup"
Cohesion: 0.11
Nodes (15): Phase 5 prep devlog, 5b-02 loops before/after page, Phase 5a UI mockup page, 5a-03 UI spec and change list, Rarity star glyph and tiers, Rotate-device layer for portrait phones, Vietnamese cards and HUD spec, Phase 5 dispatch plan (+7 more)

### Community 54 - "Package manifest entries"
Cohesion: 0.10
Nodes (21): dependencies, depth, source, version, dependencies, depth, source, version (+13 more)

### Community 55 - "Camera sway"
Cohesion: 0.15
Nodes (6): CameraSway, DepthRig, CamOffset, ImageShift, RestPosition, WorldTexture

### Community 56 - "Package manifest entries"
Cohesion: 0.10
Nodes (20): dependencies, depth, source, url, version, dependencies, depth, source (+12 more)

### Community 58 - "Package manifest entries"
Cohesion: 0.11
Nodes (19): dependencies, dependencies, depth, source, version, dependencies, depth, source (+11 more)

### Community 59 - "Package manifest entries"
Cohesion: 0.11
Nodes (19): dependencies, depth, source, version, dependencies, depth, source, url (+11 more)

### Community 60 - "Package manifest entries"
Cohesion: 0.11
Nodes (19): dependencies, depth, source, version, dependencies, depth, source, url (+11 more)

### Community 61 - "Phase 2 implementation plan"
Cohesion: 0.15
Nodes (17): Phase 2 implementation plan, Phase 2 balance pass, ChainPlanner hub match and sputter, Reaction Index data model, Phase 1 design spec, Fizzle hints, Run: 5 depths, trials, targets, Satchel with starters and discards (+9 more)

### Community 63 - "Package manifest entries"
Cohesion: 0.11
Nodes (18): dependencies, depth, source, version, dependencies, depth, source, url (+10 more)

### Community 64 - "Package manifest entries"
Cohesion: 0.11
Nodes (18): dependencies, depth, source, version, dependencies, depth, source, version (+10 more)

### Community 65 - "Mockup main script"
Cohesion: 0.16
Nodes (11): add(), buildPanel(), drawHud(), frame(), parallax(), placeArt(), quadGeo(), rowG() (+3 more)

### Community 66 - "Lang and JSON parser"
Cohesion: 0.16
Nodes (3): LangEntry, LangFile, TestLang

### Community 67 - "Package manifest entries"
Cohesion: 0.12
Nodes (17): dependencies, depth, source, url, version, dependencies, depth, source (+9 more)

### Community 68 - "Package manifest entries"
Cohesion: 0.12
Nodes (17): dependencies, depth, dependencies, depth, source, url, version, source (+9 more)

### Community 69 - "Package manifest entries"
Cohesion: 0.12
Nodes (17): dependencies, depth, source, version, dependencies, depth, source, version (+9 more)

### Community 70 - "Unity package manifest"
Cohesion: 0.12
Nodes (15): com.unity.2d.sprite@1.0.0, com.unity.ide.visualstudio@2.0.26, com.unity.modules.jsonserialize@1.0.0, com.unity.modules.screencapture@1.0.0, com.unity.modules.tilemap@1.0.0, com.unity.modules.vehicles@1.0.0, com.unity.timeline@6.6.0, com.unity.2d.sprite (+7 more)

### Community 71 - "Unity 2D package manifest"
Cohesion: 0.12
Nodes (16): dependencies, com.unity.2d.animation@16.0.1, com.unity.2d.aseprite@6.0.0, com.unity.2d.tilemap.extras@9.0.1, com.unity.modules.adaptiveperformance@1.0.0, com.unity.modules.director@1.0.0, com.unity.modules.imageconversion@1.0.0, com.unity.modules.ui@1.0.0 (+8 more)

### Community 72 - "Unreal 5.8 changes"
Cohesion: 0.22
Nodes (16): Unreal Engine 5.8 Breaking Changes, Unreal C++20 Minimum Standard, Unreal Enhanced Input System, Unreal MegaLights, Unreal Substrate Material System, Unreal Engine 5.8 Current Best Practices, Unreal Automation Tests Command Line, Unreal Niagara VFX (+8 more)

### Community 73 - "Unreal deprecated APIs"
Cohesion: 0.13
Nodes (16): Unreal Deprecated APIs, Cascade to Niagara, Raw Input Plugin to Game Input for Windows, FSmartName Removed in UE 5.8, Sound Cue to MetaSounds, UE_LOG to UE_LOGF, Unreal Animation Module Reference, Control Rig Procedural Animation (+8 more)

### Community 74 - "Satchel"
Cohesion: 0.31
Nodes (3): Satchel, Total, SatchelTests

### Community 75 - "Godot 4.7 changes"
Cohesion: 0.13
Nodes (15): AudioStreamPlayer area_mask default change in 4.7, Godot current best practices, Godot 4.6 to 4.7 breaking changes, Godot deprecated APIs table, InputEvent DEVICE_ID constants in 4.7, gdUnit4 headless command and exit codes, Jolt default 3D physics since 4.6, Godot animation module reference (+7 more)

### Community 76 - "Depth FX mockup and bench"
Cohesion: 0.17
Nodes (13): pixelfx flipbook effects, Depth moods 5 presets, Alchemy bench mockup v2, Alchemy bench scene, Lit pixel stage with bloom, Material icon set, Pixel painter (Spr class), Reaction cutscene 7-second loop (+5 more)

### Community 77 - "Aspect effects and materials"
Cohesion: 0.19
Nodes (7): AspectEffects, Effects, MaterialData, Ops, Rarities, StatIds, RarityReportTests

### Community 78 - "Lang content keys"
Cohesion: 0.26
Nodes (3): Lang, Count, Keys

### Community 80 - "Unity 6.3 changes"
Cohesion: 0.24
Nodes (14): Unity 6.3 Breaking Changes, Entities DOTS API Overhaul, Unity WebGPU Default for WebGL, Unity 6.3 Current Best Practices, Unity Test Runner Command Line, Unity 6.3 Deprecated APIs, Unity 6.3 Input Module Reference, Unity Input System Package (+6 more)

### Community 84 - "Package manifest entries"
Cohesion: 0.15
Nodes (13): dependencies, depth, source, version, dependencies, depth, source, version (+5 more)

### Community 86 - "Package manifest entries"
Cohesion: 0.18
Nodes (11): dependencies, depth, source, version, dependencies, depth, source, version (+3 more)

### Community 87 - "Phase 4 depth design decision"
Cohesion: 0.40
Nodes (8): Phase 4 devlog: depth layers and 2.5D feasibility, Lit pixel mist sheets and per-layer depth fog, Option 3: Universal 3D renderer (rejected), Option 2: perspective camera into 640x360 RenderTexture, Variant A: layered parallax 2D, orthographic, Variant B: perspective 2.5D with per-layer depth fog, Variant C: descent stair with 3D geometry, docs/mockup/depth/index.html (depth mockup)

### Community 88 - "Flipbook importer"
Cohesion: 0.20
Nodes (6): FlipbookImporter, Box, Frame, Info, Meta, Sheet

### Community 89 - "Localization loader"
Cohesion: 0.29
Nodes (5): Loc, Code, Current, NativeName, Vi

### Community 90 - "Lore event rules and balance"
Cohesion: 0.29
Nodes (8): 16 depth events drawn after the aspect, Phase 4 devlog: lore (rough draft), Lore events balance check, -3.0 points at cut-off, Opening cutscene storyboard, seven scenes about 44 s, Three new Lore events: names_in_wall, warm_roots, pinned_note, docs/mockup/cutscene/index.html (opening cutscene mockup), Opening storyboard 7 scenes, Opening cutscene in three pages

### Community 92 - "Sprite atlas and VFX audit"
Cohesion: 0.33
Nodes (7): AtlasBuilder.cs (Editor sprite atlas builder), Phase 4 audit: sprite atlases and particle VFX, Sprite atlases: 209 textures into 9 pages plus 1 standalone, pixelfx flipbooks never exported, Fx.Spawn returns null, Athanor/Particle Glow shader (unlit additive), SparkMesh: Sparks drawn as one mesh per sorting order, Unused TMP default font, about 0.5 MB compressed

### Community 93 - "Unity 6.3 module reference"
Cohesion: 0.25
Nodes (9): Unity Burst Compiler and Jobs System, Unity 6.3 Animation Module Reference, Unity Timeline, Unity 6.3 Networking Module Reference, Netcode for GameObjects, Unity 6.3 Optional Packages Index, Unity 6.3 Cinemachine, Unity 6.3 DOTS and Entities (ECS) (+1 more)

### Community 94 - "HD-2D mockup and spec"
Cohesion: 0.36
Nodes (6): 5b-01 HD-2D mockup page, 5b-01 HD-2D spec, Pixel mist fog sheets, Foreground strip stand-in art, Camera sway and world shake, Tilt-shift band along screen Y

### Community 95 - "Tutorial and lore plan"
Cohesion: 0.39
Nodes (7): Phase 4 tutorial script devlog, Phase 4 part 1 plan: tutorial and lore, Lore events, Redraw as its own button, RunOptions QuietBefore and FirstOffer hooks, TutorialScript as data, 16 depth events

### Community 96 - "Unreal input migration"
Cohesion: 0.29
Nodes (8): Legacy Input Bindings to Enhanced Input, Unreal Input Module Reference, Enhanced Input and Common Input Unification, Enhanced Input System, Unreal UI Module Reference (UMG), UMG Widgets and Bindings, Unreal CommonUI Plugin, CommonActivatableWidget Screen Stack

### Community 97 - "Architecture and API decisions"
Cohesion: 0.25
Nodes (7): api_decisions, forbidden_patterns, interfaces, last_updated, performance_budgets, state_ownership, version

### Community 99 - "Auto-move and choices"
Cohesion: 0.29
Nodes (7): AutoMove, ChoseAspect, ChoseCatalyst, ChoseEvent, None, ReadyToIgnite, Redrew

### Community 100 - "Run phase and offers"
Cohesion: 0.29
Nodes (7): RunPhase, AspectOffer, DepthCleared, EventOffer, Lost, Playing, Won

### Community 101 - "Package manifest entries"
Cohesion: 0.33
Nodes (6): dependencies, depth, source, url, version, com.unity.collab-proxy

### Community 102 - "Package manifest entries"
Cohesion: 0.40
Nodes (5): dependencies, depth, source, version, com.unity.modules.umbra

### Community 103 - "Unreal ability system"
Cohesion: 0.40
Nodes (4): FActiveGameplayEffectHandle(int32) Constructor, UGameplayAbility AbilityTags Direct Access, Unreal Gameplay Ability System, Ability Asset Tags (SetAssetTags)

### Community 104 - "Hand and tile enums"
Cohesion: 0.50
Nodes (4): From, Hand, None, Tile

### Community 105 - "Requirement registry"
Cohesion: 0.50
Nodes (3): last_updated, requirements, version

### Community 106 - "ADR and docs standards"
Cohesion: 0.50
Nodes (4): docs/CLAUDE.md (ADR and docs standards), ADR status lifecycle Proposed to Accepted to Superseded, Engine reference docs (version-pinned API snapshots), TR registry: stable requirement IDs

### Community 108 - "Fog and HD-2D depth plan"
Cohesion: 0.50
Nodes (4): AERO volumetric fog (rejected), Sprite fog layer lit by Light2D, Gate G2 approval of HD-2D mockup, HD-2D depth layers plan

### Community 109 - "Unreal Chaos physics"
Cohesion: 0.50
Nodes (4): PhysicsMover Replaced by ChaosMover, Unreal Physics Module Reference, Chaos Physics, ChaosMover Plugin

### Community 110 - "Unreal rendering reference"
Cohesion: 0.67
Nodes (4): Unreal Rendering Module Reference, Lumen Global Illumination, MegaLights Production Status, Substrate Materials

### Community 113 - "Migration guide v1.1"
Cohesion: 1.00
Nodes (3): Migration Guide v1.0 to v1.1 project.yaml, migrate-v1-config.sh (dry-run, migrate, finalize), project.yaml as single source of config

## Knowledge Gaps
- **260 isolated node(s):** `None`, `ChoseCatalyst`, `Redrew`, `ReadyToIgnite`, `ChoseAspect` (+255 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 786 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **77 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `RunState` connect `Content catalogs and events` to `Core data types`, `Run rules and tests`, `Auto-player bot`, `Bench placement rules`, `Content enums and hints`, `Reaction events and goals`, `Catalyst and ignite resolver`, `Profile and reward log`, `Effects and event options`, `Rack slots and screen FX`, `Bot pairing and content lookup`, `Depth evidence and bench drops`, `Auto-player, rules and journal`, `Run state ignite and catalyst`, `Ignite outcome and scoring`, `Satchel`, `Aspect effects and materials`, `Rarity tests`, `Run phase and offers`?**
  _High betweenness centrality (0.058) - this node is a cross-community bridge._
- **Are the 113 inferred relationships involving `ContentDb` (e.g. with `GameFlow.Awake()` and `BalanceMatrixTests.BalanceMatrix()`) actually correct?**
  _`ContentDb` has 113 INFERRED edges - model-reasoned connections that need verification._
- **What connects `None`, `ChoseCatalyst`, `Redrew` to the rest of the system?**
  _260 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Core data types` be split into smaller, more focused modules?**
  _Cohesion score 0.052976658798846055 - nodes in this community are weakly interconnected._
- **Why does `ContentDb` connect `Core data types` to `Content catalogs and events`, `Run rules and tests`, `Auto-player bot`, `Bench placement rules`, `Content enums and hints`, `Reaction events and goals`, `Scene and bench view builders`, `Catalyst and ignite resolver`, `Profile and reward log`, `Effects and event options`, `Rack slots and screen FX`, `Bot pairing and content lookup`, `HUD anchors and aspects`, `Card view animations`, `Depth evidence and bench drops`, `Auto-player, rules and journal`, `Run state ignite and catalyst`, `Ignite outcome and scoring`, `Aspect effects and materials`, `Rarity tests`?**
  _High betweenness centrality (0.055) - this node is a cross-community bridge._
- **Are the 35 inferred relationships involving `RunState` (e.g. with `GameFlow.PlayRun()` and `LoreTests.ALastLore_ThatIsNotALoreEvent_ChangesNothing()`) actually correct?**
  _`RunState` has 35 INFERRED edges - model-reasoned connections that need verification._
- **Should `Content catalogs and events` be split into smaller, more focused modules?**
  _Cohesion score 0.04878048780487805 - nodes in this community are weakly interconnected._