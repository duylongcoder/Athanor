---
generated: true
type: index
tags: [index]
---

# Symbol index

Flat tables behind `tools/vault/q.py`. For browsing use the per-type notes in `Code/` and `Content/`.

## Stats
| key | count |
|---|---|
| materials | 81 |
| recipes | 87 |
| catalysts | 26 |
| aspects | 16 |
| events | 19 |
| goals | 22 |
| depths | 5 |

## Types
| name | note | layer | kind | file | line | doc |
|---|---|---|---|---|---|---|
| AutoPlayer | AutoPlayer | Gameplay | class | Athanor/Assets/Scripts/Gameplay/AutoPlayer.cs | 13 |  |
| AutoMove | AutoMove | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/AutoPlayer.cs | 11 |  |
| BenchRules | BenchRules | Gameplay | class | Athanor/Assets/Scripts/Gameplay/BenchRules.cs | 11 | What catalysts, aspects and events change about the bench itself: which tiles touch, how lively a tile counts, which tiles are shut. The planner, the resolver and the scripted player all read this, so |
| ScoreContext | ScoreContext | Gameplay | class | Athanor/Assets/Scripts/Gameplay/BenchRules.cs | 82 | What one reaction's score depends on beyond the recipe and the catalysts. |
| ChainStep | ChainStep | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Chain.cs | 11 | One thing the bench does during an Ignite. A reaction: the hub and the neighbours it consumed (Tiles[0] is the hub, the rest ascending). A sputter: a combo of three or more whose layout was standing r |
| ChainPlan | ChainPlan | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Chain.cs | 20 |  |
| ChainPlanner | ChainPlanner | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Chain.cs | 32 | Works out what a bench does, with no side effects. Each round the liveliest tile (highest Reaction Index, earlier tile on a tie) that has a recipe around it becomes the hub; the largest recipe it can  |
| AspectDefinition | AspectDefinition | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Content/AspectDefinition.cs | 7 | A run-long bench modifier offered at GameRules.aspectDepths. |
| CatalystDefinition | CatalystDefinition | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Content/CatalystDefinition.cs | 7 | A catalyst card: a run-long scoring effect chosen after a depth. |
| ContentDatabase | ContentDatabase | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Content/ContentDatabase.cs | 12 | The game's content: every definition asset in the order the rules see it (the order matters: offers, draws and "the order of the file" tie-breaks follow it). ToContentData builds the plain data Conten |
| CatalystEffect | CatalystEffect | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs | 15 | Effects.All in order. |
| ContentNames | ContentNames | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs | 47 | Enum values to and from the strings of ContentData. |
| RarityId | RarityId | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs | 9 |  |
| KindOrNone | KindOrNone | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs | 12 |  |
| AspectEffect | AspectEffect | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs | 23 |  |
| AspectScope | AspectScope | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs | 26 |  |
| AspectGroup | AspectGroup | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs | 29 |  |
| EventOp | EventOp | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs | 32 |  |
| EventKind | EventKind | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs | 35 |  |
| EventNeed | EventNeed | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs | 38 |  |
| GoalStat | GoalStat | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs | 41 |  |
| RewardKind | RewardKind | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/Content/ContentEnums.cs | 44 |  |
| EventEffectDef | EventEffectDef | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Content/EventDefinition.cs | 9 | One effect of an event option's cost or outcome. |
| EventOutcomeDef | EventOutcomeDef | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Content/EventDefinition.cs | 20 | One weighted outcome of an event option. |
| EventOptionDef | EventOptionDef | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Content/EventDefinition.cs | 31 | One choice of an event: its requirement, cost and weighted outcomes. |
| EventDefinition | EventDefinition | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Content/EventDefinition.cs | 47 | An event drawn at the start of a depth. |
| GameRules | GameRules | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Content/GameRules.cs | 7 | The run's tuning numbers (was the "rules" block of the content assets) and the depth table. |
| GoalDefinition | GoalDefinition | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Content/GoalDefinition.cs | 7 | A long-term goal: reach target on a stat, win one random locked item of rewardKind and rewardRarity. |
| MaterialDefinition | MaterialDefinition | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Content/MaterialDefinition.cs | 7 | A material. The asset's file name is its id; the id is what saves store, so never change it after release. |
| RecipeDefinition | RecipeDefinition | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Content/RecipeDefinition.cs | 8 | A combo of 2 to 5 materials (unordered, at most 2 of one) that makes result. |
| RulesData | RulesData | Gameplay | class | Athanor/Assets/Scripts/Gameplay/ContentData.cs | 29 | The run's tuning numbers, read from the "rules" block of the content assets. Required: ContentDb throws without it. |
| ContentData | ContentData | Gameplay | class | Athanor/Assets/Scripts/Gameplay/ContentData.cs | 56 |  |
| ProfileData | ProfileData | Gameplay | class | Athanor/Assets/Scripts/Gameplay/ContentData.cs | 78 |  |
| Rarities | Rarities | Gameplay | class | Athanor/Assets/Scripts/Gameplay/ContentData.cs | 88 |  |
| Effects | Effects | Gameplay | class | Athanor/Assets/Scripts/Gameplay/ContentData.cs | 98 | The catalyst effect names used in the content assets. |
| AspectEffects | AspectEffects | Gameplay | class | Athanor/Assets/Scripts/Gameplay/ContentData.cs | 117 | The aspect effect names used in the content assets. |
| Ops | Ops | Gameplay | class | Athanor/Assets/Scripts/Gameplay/ContentData.cs | 129 | The event effect names (EffectData.op) and option requirements used in the content assets. |
| StatIds | StatIds | Gameplay | class | Athanor/Assets/Scripts/Gameplay/ContentData.cs | 145 | The stats a goal can watch. |
| Kind | Kind | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/ContentData.cs | 5 |  |
| ContentDb | ContentDb | Gameplay | class | Athanor/Assets/Scripts/Gameplay/ContentDb.cs | 10 | Validated, indexed view of the game's content. |
| EffectOp | EffectOp | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/EffectOps.cs | 6 |  |
| EventNeedType | EventNeedType | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/EffectOps.cs | 15 |  |
| EventType | EventType | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/EffectOps.cs | 25 |  |
| Bench | Bench | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Ignite.cs | 7 | The 3x3 bench: tile index = row * 3 + column, null where empty. |
| ReactionEvent | ReactionEvent | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Ignite.cs | 21 | One thing that happened during an Ignite, in the order it should be shown. A reaction: Tiles[0] is the hub, where the result now stands; the other tiles were consumed. A sputter: a combo that stood re |
| ResolveResult | ResolveResult | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Ignite.cs | 36 |  |
| IgniteResolver | IgniteResolver | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Ignite.cs | 43 |  |
| Journal | Journal | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Journal.cs | 8 | What the player has learned across runs: recipes that worked, and hints from ones that did not. |
| Lang | Lang | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Lang.cs | 20 | The player-facing text of one language, Unity-free. Lookup order: this language, then the fallback (English), then the text the caller supplies (the content assets' English for content keys), then the |
| Json | Json | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Lang.cs | 212 |  |
| LinkPair | LinkPair | Gameplay | struct | Athanor/Assets/Scripts/Gameplay/LinkPair.cs | 7 | Named pair for bench link coordinates — used as an alternative to value tuples so Unity serialization is reliable. |
| GoalReward | GoalReward | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Profile.cs | 8 | A goal that has just been completed, and the locked item it gave (null when its pool was empty). |
| Profile | Profile | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Profile.cs | 18 | What the player keeps between runs besides the journal: counters, completed goals and the locked starters and catalysts won from them. |
| Unlocks | Unlocks | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Profile.cs | 154 | Which starting materials and catalysts a run may use. |
| RunOptions | RunOptions | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Profile.cs | 206 | Switches for a run. The default is a Phase 2 run: no aspects, no events, and (with no Unlocks) only what the content leaves open. |
| AspectInstance | AspectInstance | Gameplay | class | Athanor/Assets/Scripts/Gameplay/RunStart.cs | 8 | An aspect as held by a run (or as offered): where it sits and, for an attuned tile, which kind it favours. |
| EventResult | EventResult | Gameplay | class | Athanor/Assets/Scripts/Gameplay/RunStart.cs | 16 | What taking an event option did, for the screen. |
| EventNote | EventNote | Gameplay | class | Athanor/Assets/Scripts/Gameplay/RunStart.cs | 24 | One thing an event option did, as a lang key (note.*) and its arguments; the Game layer words it. |
| RunState | RunState | Gameplay | class | Athanor/Assets/Scripts/Gameplay/RunStart.cs | 33 | One run: descending through depths, spending trials, collecting catalysts. |
| IgniteOutcome | IgniteOutcome | Gameplay | class | Athanor/Assets/Scripts/Gameplay/RunState.cs | 9 |  |
| RunPhase | RunPhase | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/RunState.cs | 7 |  |
| Rng | Rng | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Satchel.cs | 7 | Small deterministic RNG so shuffles match between Unity and the dotnet test run. |
| Satchel | Satchel | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Satchel.cs | 20 | The player's deck of starting materials: a draw pile, a discard pile, and the cards currently out. |
| ScoreParts | ScoreParts | Gameplay | class | Athanor/Assets/Scripts/Gameplay/ScoreParts.cs | 7 | One reaction's potency split into the factors the tutorial names: Total = Base x Used x First x Step x Bonus. Bonus is everything else (catalysts, the hub's tile, events); 1 when nothing else applied. |
| ScreenFlow | ScreenFlow | Gameplay | class | Athanor/Assets/Scripts/Gameplay/ScreenFlow.cs | 9 | The screen state machine: which screen is up and which moves are allowed. No Unity types. |
| FlowState | FlowState | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/ScreenFlow.cs | 6 |  |
| Settings | Settings | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Settings.cs | 12 | Player settings: volumes, haptics and language. Unity-free; Game stores the JSON (PlayerPrefs, key athanor.settings.v1). Public fields so JsonUtility can read it too; ToJson/FromJson are hand-written  |
| Reader | Reader | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Settings.cs | 62 |  |
| TutorialLay | TutorialLay | Gameplay | struct | Athanor/Assets/Scripts/Gameplay/Tutorial.cs | 13 | One material laid on one tile. This material goes on this tile, not just any lit tile: another arrangement of the same materials can break the chain (trial 3 then scores 40 and does not clear the dept |
| TutorialTrial | TutorialTrial | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Tutorial.cs | 21 | One scripted trial: the hand dealt, where each material goes, and what the Ignite makes, in chain order. |
| TutorialScript | TutorialScript | Gameplay | class | Athanor/Assets/Scripts/Gameplay/Tutorial.cs | 32 | The tutorial (docs/ATHANOR.md §8, v2) as data. Every number the script shows is pinned by TutorialTests; change both together. |
| TutorialPlan | TutorialPlan | Gameplay | enum | Athanor/Assets/Scripts/Gameplay/Tutorial.cs | 6 |  |
| BenchView | BenchView | Game | class | Athanor/Assets/Scripts/Game/Bench/BenchView.cs | 14 | The main screen: nine tiles, the hand of cards, the rack of discoveries and the HUD. Materials are placed by dragging them onto a tile, or by clicking one and then a tile; clicking a placed one takes  |
| BenchView.Layout | BenchView.Layout | Game | struct | Athanor/Assets/Scripts/Game/Bench/BenchView.cs | 30 | Offsets and a colour of the bench, stored in Bench.prefab. |
| TipKey | TipKey | Game | struct | Athanor/Assets/Scripts/Game/Bench/BenchView.cs | 76 |  |
| From | From | Game | enum | Athanor/Assets/Scripts/Game/Bench/BenchView.cs | 52 |  |
| TipKind | TipKind | Game | enum | Athanor/Assets/Scripts/Game/Bench/BenchView.cs | 75 |  |
| CardView | CardView | Game | class | Athanor/Assets/Scripts/Game/Bench/CardView.cs | 7 | A starting material in the hand, drawn as a parchment card. |
| CardView.Layout | CardView.Layout | Game | struct | Athanor/Assets/Scripts/Game/Bench/CardView.cs | 22 | Offsets and speeds of the card, stored in Card.prefab. |
| Dust | Dust | Game | class | Athanor/Assets/Scripts/Game/Bench/Dust.cs | 9 | Dust drifting down through the moonbeam: a loop, each mote's position a pure function of time, all of them drawn as one SparkMesh (one renderer) at order 8. |
| HudView | HudView | Game | class | Athanor/Assets/Scripts/Game/Bench/HudView.cs | 8 | The panels round the bench: depth and potency, catalysts, trials and redraws, the last-reaction note, journal and Ignite. |
| HudView.Layout | HudView.Layout | Game | struct | Athanor/Assets/Scripts/Game/Bench/HudView.cs | 29 | The HUD's layout numbers (art pixels, y down), stored in Hud.prefab. |
| LinkView | LinkView | Game | class | Athanor/Assets/Scripts/Game/Bench/LinkView.cs | 7 | Motes of light travelling between neighbouring tiles whose reaction is already in the journal. |
| TileView | TileView | Game | class | Athanor/Assets/Scripts/Game/Bench/TileView.cs | 10 | One of the nine bench tiles: its hover highlight and, when occupied, the material standing on it. |
| TileView.Layout | TileView.Layout | Game | struct | Athanor/Assets/Scripts/Game/Bench/TileView.cs | 33 | Colours and offsets of the tile, stored in Bench.prefab. |
| GameFlow | GameFlow | Game | class | Athanor/Assets/Scripts/Game/Boot/GameFlow.cs | 13 | Owns the run and moves the game between its screens. |
| RunTiming | RunTiming | Game | struct | Athanor/Assets/Scripts/Game/Boot/GameFlow.cs | 91 | Waits and shake of the descent and the end of a run, in seconds. |
| TitleChoice | TitleChoice | Game | enum | Athanor/Assets/Scripts/Game/Boot/GameFlow.cs | 148 |  |
| IgniteSequence | IgniteSequence | Game | class | Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs | 15 | The choreography of one Ignite: the chain of reactions (flash, draw-in, burst, points), the discovery beat and cutscene, the flight of what was made to the rack, the spent tiles and the final potency. |
| IgniteSequence.Timing | IgniteSequence.Timing | Game | struct | Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs | 22 | Paces, waits and durations, in seconds unless the name says otherwise. |
| IgniteSequence.Look | IgniteSequence.Look | Game | struct | Athanor/Assets/Scripts/Game/Boot/IgniteSequence.cs | 55 | Bursts, offsets, sizes and colours. |
| Save | Save | Game | class | Athanor/Assets/Scripts/Game/Boot/Save.cs | 8 | Keeps the journal and the profile between sessions in PlayerPrefs (browser storage in a web build). |
| Cutscene | Cutscene | Game | class | Athanor/Assets/Scripts/Game/Cutscene/Cutscene.cs | 7 | A sequence that plays to its end unless the player clicks to skip ahead. |
| DepthLook | DepthLook | Game | class | Athanor/Assets/Scripts/Game/Cutscene/DepthLook.cs | 9 | What a depth looks like from the bench: the intro line under its name and the view through the window. Pure choices, no scene; the pictures and effects are drawn by WindowView. |
| ReactionCutscene | ReactionCutscene | Game | class | Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs | 15 | The full discovery sequence: two materials spiral into the cauldron, the rune circle lights, a flash, and the new material rises on a column of light with its name and potency. Everything is a functio |
| Brew | Brew | Game | struct | Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs | 49 | The rune circle, the brew and the materials: colours, brightness and how they pulse. |
| Prize | Prize | Game | struct | Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs | 63 | The result, its pillar, rays and rings once the burst has gone off. |
| ReactionCutscene.Layout | ReactionCutscene.Layout | Game | struct | Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs | 78 | Where things are (art space, y down) and how the motion between them is shaped. |
| Sparkle | Sparkle | Game | struct | Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs | 94 | The trail behind a flying material and the sparks off the brew. |
| SimpleCutscene | SimpleCutscene | Game | class | Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs | 14 | The short story beats built from text and a few effects: the intro, the descent between depths, and the ending. A click finishes the line being written, then moves on. |
| IntroPage | IntroPage | Game | struct | Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs | 25 | The opening story: the fades round it and the holds between its lines, in seconds. |
| DescentBanner | DescentBanner | Game | struct | Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs | 37 | Descent beat 1, the win over the bench: "depth cleared" rises into place, with a flash, a shake and a burst. |
| EndingBeat | EndingBeat | Game | struct | Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs | 65 | The ending: the fades round the story, the hold after a line, the final flash and burst, the banner's place. |
| SimpleCutscene.Look | SimpleCutscene.Look | Game | struct | Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs | 75 | What Update draws each frame: the floating prize and its rays, the rune circle and its ring, the colours of the streams. |
| LoopSpec | LoopSpec | Game | struct | Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs | 93 | One looping stream: sort order, particles a second, life, where they are born (art x, y), their speed, gravity and chance to sparkle. |
| Loop | Loop | Game | class | Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs | 388 | A stream of glowing particles played as a loop instead of emitted: rate x life fixed slots, each reborn every life seconds at a hashed place and speed, so its position is a function of the clock. A sl |
| TutorialRect | TutorialRect | Game | class | Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs | 6 | Pure rectangle math for tutorial areas (art pixels, top-left origin). No scene needed, so it is tested alone. |
| TutorialArea | TutorialArea | Game | class | Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs | 37 | One lit window of a step. Fixed: the object's world position is the centre and size (pixels) its size. ScriptTile and View follow the game at run time. clicksPass lets clicks in the window reach the g |
| AreaKind | AreaKind | Game | enum | Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs | 25 |  |
| ViewRef | ViewRef | Game | enum | Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs | 28 |  |
| ScriptTrial | ScriptTrial | Game | enum | Athanor/Assets/Scripts/Game/Cutscene/TutorialArea.cs | 31 |  |
| TutorialRules | TutorialRules | Game | class | Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs | 12 | The decisions around the tutorial that need no scene, so they can be tested. What Begin does is the Core rule TutorialScript.Plan. |
| TutorialController | TutorialController | Game | class | Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs | 33 | Plays the scripted tutorial over the bench: a dark screen with lit holes and a pulsing gold rim, an arrow, a text box at the top, click locks, and popups held until read. It reads the game's state; it |
| TutorialHole | TutorialHole | Game | class | Athanor/Assets/Scripts/Game/Cutscene/TutorialHole.cs | 10 | One lit window of the tutorial overlay: a SpriteMask that cuts a hole in the dim (the dim draws "visible outside mask"), and the four gold rim pieces round it. TutorialController keeps a few of these  |
| TutorialStyle | TutorialStyle | Game | class | Athanor/Assets/Scripts/Game/Cutscene/TutorialSettings.cs | 9 | How the tutorial overlay looks and feels: the text box, the popup lines, the rim and arrow, and their timings. |
| TutorialStep | TutorialStep | Game | class | Athanor/Assets/Scripts/Game/Cutscene/TutorialStep.cs | 10 | One tutorial step, a child of the Tutorial object. Holds the text key, the dim and lock options, and its areas (child objects). The step id is the object's name (e.g. "1.4", "5.4i"). |
| WindowView | WindowView | Game | class | Athanor/Assets/Scripts/Game/Cutscene/WindowView.cs | 12 | The workshop window shows what the current depth "sees" and plays one tiny looping effect (docs/ATHANOR.md §8): 1 dust motes up a shaft, 2 drips down the glass, 3 a breathing amber glow, 4 roots creep |
| WindowView.Timing | WindowView.Timing | Game | struct | Athanor/Assets/Scripts/Game/Cutscene/WindowView.cs | 18 | The periods of the effects, in seconds; Main.unity holds the values (see LayoutPinTests). |
| WindowView.Layout | WindowView.Layout | Game | struct | Athanor/Assets/Scripts/Game/Cutscene/WindowView.cs | 32 | Where the effects sit, as offsets from the pane's centre (art pixels, y up), and the paths they follow. |
| WindowView.Look | WindowView.Look | Game | struct | Athanor/Assets/Scripts/Game/Cutscene/WindowView.cs | 43 | Colours and strengths of the effects. |
| CameraSway | CameraSway | Game | class | Athanor/Assets/Scripts/Game/Depth/CameraSway.cs | 12 | The idle sway and the world-only shake of the HD-2D bench. The sway moves the world camera (x = A sin(2 pi t / 6), y = A / 2 sin(2 pi t / 9), A = 4 art px) and re-aims it so the middle tile row (art y |
| SwayTiming | SwayTiming | Game | struct | Athanor/Assets/Scripts/Game/Depth/CameraSway.cs | 21 | The sway's two periods (seconds) and the world shake's two frequencies (radians per second); held by Main.unity. |
| DepthLayer | DepthLayer | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthLayer.cs | 10 | One flat thing of the bench placed in depth: it is moved to its depth and enlarged by (D - z) / D, so that from the camera at rest it covers exactly the art pixels it covers in the flat game. Sorting  |
| DepthMath | DepthMath | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthMath.cs | 10 | The projection maths of the HD-2D bench (HD-2D mockup SPEC, "Layer stack and projection"; removed in P6-22, see git history). Pure functions, no scene. Depth z follows the SPEC: 0 is the plane where o |
| TableShape | TableShape | Game | struct | Athanor/Assets/Scripts/Game/Depth/DepthMath.cs | 133 | The table's shape as the maths needs it: the distance from the camera of its front and back edge at rest, the art y of its back edge and the number of art rows of the tilted top. DepthTable holds the  |
| DepthMood | DepthMood | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthMood.cs | 13 | The five depth presets of the HD-2D test scene (HD-2D mockup SPEC, "Depth presets", with the URP conversions of "Mapping mockup values to Unity"): ambient light, moonbeam, bloom, vignette, the fog she |
| Preset | Preset | Game | struct | Athanor/Assets/Scripts/Game/Depth/DepthMood.cs | 17 | One depth's look. |
| DepthRig | DepthRig | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthRig.cs | 12 | The camera split of the HD-2D bench. The world camera is a perspective camera at distance D from the art plane; it renders the layers under this object into a RenderTexture of 640K x 360K pixels (K =  |
| DepthStage | DepthStage | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthStage.cs | 10 | The HD-2D bench of the main scene as the game sees it: the rig (world camera, UI camera), the sway, the per-depth moods and the effects switch, all objects of Main.unity. GameFlow talks to this and no |
| DepthSwitch | DepthSwitch | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthSwitch.cs | 11 | The "all effects off" switch of the depth test scene. Off: the far layers show the original sharp sprites and the fog sheets and the foreground are hidden, so the world at rest is today's bench. On: t |
| SpriteSwap | SpriteSwap | Game | struct | Athanor/Assets/Scripts/Game/Depth/DepthSwitch.cs | 15 | A sprite that has a sharp picture and a pre-blurred one of the same centre. |
| FlameSwap | FlameSwap | Game | struct | Athanor/Assets/Scripts/Game/Depth/DepthSwitch.cs | 23 | A flame whose frames have a sharp set and a pre-blurred set. |
| DepthTable | DepthTable | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthTable.cs | 13 | The table as a floor: one mesh lying on the tilted plane of DepthMath.TableZ (plus the upright front face below the front edge), cut along every art row. A plain tilted quad would foreshorten the art, |
| DepthTiltShiftFeature | DepthTiltShiftFeature | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthTiltShiftFeature.cs | 9 | A full-screen pass of the tilt-shift (one blur direction per feature) that runs only for cameras with an enabled TiltShift, so the HUD camera and the main game's camera are never blurred. |
| FogSheet | FogSheet | Game | class | Athanor/Assets/Scripts/Game/Depth/FogSheet.cs | 10 | A tiling fog sheet (a tiled SpriteRenderer with the "Athanor/Fog Lit" material) that drifts sideways in whole texels: the picture moves left by trunc(t * speed) texels (back sheet +4, front sheet -6 a |
| TiltShift | TiltShift | Game | class | Athanor/Assets/Scripts/Game/Depth/TiltShift.cs | 12 | The tilt-shift of the world camera: sharp from art y 145 to 235, then a blur that rises over 110 rows (smoothstep) to sigma 1.5 art px (owner gate G2 #9, #10). Drawn by DepthTiltShiftFeature before bl |
| BurstSpec | BurstSpec | Game | struct | Athanor/Assets/Scripts/Game/Fx/BurstSpec.cs | 8 | One serialized spark burst: what Sparks.Burst takes besides the position. |
| Flame | Flame | Game | class | Athanor/Assets/Scripts/Game/Fx/Flame.cs | 7 | A flickering flame: cycles its frames and wavers the light and halo that go with it. |
| Flipbook | Flipbook | Game | class | Athanor/Assets/Scripts/Game/Fx/Flipbook.cs | 8 | Plays a FlipbookSheet on a SpriteRenderer. |
| FlipbookSheet | FlipbookSheet | Game | class | Athanor/Assets/Scripts/Game/Fx/FlipbookSheet.cs | 6 | One exported effect: its frames in order and how long each is shown. |
| Fx | Fx | Game | class | Athanor/Assets/Scripts/Game/Fx/Fx.cs | 6 | Spawns exported pixelfx effects by name. |
| FxRefs | FxRefs | Game | class | Athanor/Assets/Scripts/Game/Fx/FxRefs.cs | 6 | The scene's two effect players, handed to every view that flashes, shakes or throws sparks (no statics). |
| ScreenFx | ScreenFx | Game | class | Athanor/Assets/Scripts/Game/Fx/ScreenFx.cs | 8 | Whole-screen effects: a white flash, camera shake, and fading to and from black. |
| ShakeTiming | ShakeTiming | Game | struct | Athanor/Assets/Scripts/Game/Fx/ScreenFx.cs | 17 | The camera shake's two frequencies (radians per second) and how much of the x travel the y axis gets; held by Main.unity. |
| SparkMesh | SparkMesh | Game | class | Athanor/Assets/Scripts/Game/Fx/SparkMesh.cs | 13 | Many 1 px dots and 5x5 sparkles drawn as one mesh at one sorting order: one renderer and at most two draws (one per texture) instead of a SpriteRenderer each. Fill it every frame with Begin, Add, End. |
| SparkParticles | SparkParticles | Game | class | Athanor/Assets/Scripts/Game/Fx/SparkParticles.cs | 13 | Sparks drawn by ParticleSystems (docs/ATHANOR.md §2), only compiled with ATHANOR_SPARK_PARTICLES. Not shipped: it pulls the ParticleSystem engine module into the wasm, which was the thing measured. On |
| Sparks | Sparks | Game | class | Athanor/Assets/Scripts/Game/Fx/Sparks.cs | 13 | Glowing pixel particles: bursts, trails and rising embers. Particles are plain structs integrated here; each sorting order is one lane drawn by one SparkMesh (one renderer, at most two draws), so a bu |
| Particle | Particle | Game | struct | Athanor/Assets/Scripts/Game/Fx/Sparks.cs | 18 |  |
| Lane | Lane | Game | class | Athanor/Assets/Scripts/Game/Fx/Sparks.cs | 23 |  |
| Anim | Anim | Game | class | Athanor/Assets/Scripts/Game/Support/Anim.cs | 11 | Beats (Wait), easing curves, and Over for per-frame scripts that are not a move or a fade (the spark trails in GameFlow). Moves, fades and scales of objects go through Motion (PrimeTween, snapped to p |
| Art | Art | Game | class | Athanor/Assets/Scripts/Game/Support/Art.cs | 6 | The art library the running game uses. GameFlow sets it from its serialized field; the Game tests set it in GameTestSetup. |
| ArtLibrary | ArtLibrary | Game | class | Athanor/Assets/Scripts/Game/Support/ArtLibrary.cs | 8 | Every sprite, effect sheet, font and material the game looks up by name. Built by Athanor > Refresh Art Library. |
| AudioMute | AudioMute | Game | class | Athanor/Assets/Scripts/Game/Support/AudioMute.cs | 11 | Silences audio while the browser tab is hidden, the app is paused or the window has lost focus. No sound ships in Phase 5, so this is a hook: it sets AudioListener.pause and is audible once sound land |
| AudioMuteHook | AudioMuteHook | Game | class | Athanor/Assets/Scripts/Game/Support/AudioMute.cs | 36 | Feeds the browser's tab visibility and Unity's pause and focus into AudioMute. |
| Clickable | Clickable | Game | class | Athanor/Assets/Scripts/Game/Support/Clickable.cs | 9 | A rectangle around this object that the pointer can hover and click. The highest order under the pointer wins. Holes (world-space rectangles) are cut out of it: the pointer there is not on this object |
| Dev | Dev | Game | class | Athanor/Assets/Scripts/Game/Support/Dev.cs | 9 | Log lines that exist only as the hook of tools/web/webtest.mjs ([tut], [drive], [loc]). The calls, and the strings they build, are compiled out of a release build; a development build and the editor k |
| Fonts | Fonts | Game | class | Athanor/Assets/Scripts/Game/Support/Fonts.cs | 10 | Which font a FontKind uses in the language showing, and the size and line box that go with it. |
| FontKind | FontKind | Game | enum | Athanor/Assets/Scripts/Game/Support/Fonts.cs | 7 |  |
| Haptics | Haptics | Game | class | Athanor/Assets/Scripts/Game/Support/Haptics.cs | 11 | Short vibration pulses for rare, meaningful moments (never clicks or Ignite). Only where the browser can vibrate (Android Chrome); Supported is false on desktop, iOS and in the editor, and the Setting |
| KindColors | KindColors | Game | class | Athanor/Assets/Scripts/Game/Support/KindColors.cs | 4 | The label colour for each kind of material. |
| Label | Label | Game | class | Athanor/Assets/Scripts/Game/Support/Label.cs | 11 | A line of pixel text with an optional one-pixel drop shadow. A label bound to a Lang key (Bind) rewrites itself when the language changes; every label swaps its font to the language's. |
| Loc | Loc | Game | class | Athanor/Assets/Scripts/Game/Support/Loc.cs | 12 | The language the game is showing: the loaded Lang (Vietnamese falls back to English), the font set that goes with it and helpers for the words views share. Views never hold text; they ask for a key. E |
| Motion | Motion | Game | class | Athanor/Assets/Scripts/Game/Support/Motion.cs | 16 | One-shot tweens on PrimeTween, kept in one place so every move is snapped to whole pixels (Px.Snap). Each call takes its start value explicitly and returns the Tween: group several with Sequence.Creat |
| RarityColors | RarityColors | Game | struct | Athanor/Assets/Scripts/Game/Support/Palette.cs | 8 | One colour per rarity, for code that switches on the rarity. |
| Palette | Palette | Game | class | Athanor/Assets/Scripts/Game/Support/Palette.cs | 18 | The colours two or more files share, by role (Assets/Data/Palette.asset). A colour only one file uses stays on that file's view. GameFlow publishes it as Art.Palette. |
| PointerInput | PointerInput | Game | class | Athanor/Assets/Scripts/Game/Support/PointerInput.cs | 8 | Mouse or touch position in world space, press state for this frame, and hover/click dispatch to Clickables. |
| Px | Px | Game | class | Athanor/Assets/Scripts/Game/Support/Px.cs | 9 | The art is laid out on a 640x360 canvas with y running down from the top-left corner. The world uses one unit per pixel with the origin at the screen centre and y up. |
| RarityStars | RarityStars | Game | class | Athanor/Assets/Scripts/Game/Support/RarityStars.cs | 13 | The rarity glyph (SPEC changes 21-24): up to four 7x7 stars on an 8 px pitch, tinted, each with a dark outline made of four offset dark copies. The root sits at the top-left of the row. Built once and |
| Spawn | Spawn | Game | class | Athanor/Assets/Scripts/Game/Support/Spawn.cs | 13 | Run-time spawners: the objects the game makes while it plays (float numbers, fly-in icons, tutorial rects, link motes, rarity stars). Everything that is on screen from the start is a scene object or a |
| Align | Align | Game | enum | Athanor/Assets/Scripts/Game/Support/Spawn.cs | 7 |  |
| Wording | Wording | Game | class | Athanor/Assets/Scripts/Game/Support/Wording.cs | 8 | The words and colours the Phase 3 screens share: rarities, aspects, event effects and goal rewards. |
| ChoiceCard | ChoiceCard | Game | class | Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs | 11 | What one card of the chooser shows. |
| CatalystChoiceView | CatalystChoiceView | Game | class | Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs | 23 | Three parchment cards, pick one: a catalyst after a depth is cleared, or an aspect at the start of a run and of depths 3 and 5. An aspect's card shows the tile it would land on. |
| Card | Card | Game | class | Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs | 45 |  |
| EventView | EventView | Game | class | Athanor/Assets/Scripts/Game/Ui/EventView.cs | 13 | The event at the start of a depth: what was found, the options that can be taken (each with what it costs and may give), then what came of the one chosen. |
| Row | Row | Game | class | Athanor/Assets/Scripts/Game/Ui/EventView.cs | 19 |  |
| GoalRow | GoalRow | Game | class | Athanor/Assets/Scripts/Game/Ui/GoalRow.cs | 6 | One row of the Goals page: the goal and its count, then the reward and its progress bar. |
| GoalToast | GoalToast | Game | class | Athanor/Assets/Scripts/Game/Ui/GoalToast.cs | 11 | A small panel at the top of the screen that says a goal was completed and what it unlocked. Toasts queue; each stays 2.5 seconds or until clicked. It never blocks anything under it but itself. |
| GoalsView | GoalsView | Game | class | Athanor/Assets/Scripts/Game/Ui/GoalsView.cs | 12 | The Goals page: every long-term goal with its progress, grouped by the rarity of what it unlocks. Before a goal is done its reward is named only by kind and rarity; afterwards the item it gave is show |
| JournalCell | JournalCell | Game | class | Athanor/Assets/Scripts/Game/Ui/JournalCell.cs | 10 | One cell of the journal's grid: the material's icon with its click. An Uncommon or better starting material also gets a star at the cell's corner; that row is made when the cell is set up (5 of the 81 |
| JournalView | JournalView | Game | class | Athanor/Assets/Scripts/Game/Ui/JournalView.cs | 12 | The journal: every material as an icon (a silhouette until it has been made), and for the selected one the reactions found so far, how many are still unknown, and any hints collected. |
| JournalView.Layout | JournalView.Layout | Game | struct | Athanor/Assets/Scripts/Game/Ui/JournalView.cs | 22 | The numbers of the book that are not its grid (art px, y down), stored in Journal.prefab. |
| PauseMenu | PauseMenu | Game | class | Athanor/Assets/Scripts/Game/Ui/PauseMenu.cs | 13 | The in-run pause: Resume, Vibration (only where the device can), How to play (greyed: title only) and Quit to title with a confirm. Time.timeScale is 0 while it is open; it is put back on every way ou |
| RunOverView | RunOverView | Game | class | Athanor/Assets/Scripts/Game/Ui/RunOverView.cs | 9 | The summary at the end of a run, won or lost. |
| TitleView | TitleView | Game | class | Athanor/Assets/Scripts/Game/Ui/TitleView.cs | 8 | The title, shown over the candlelit workshop. |
| TitleView.Layout | TitleView.Layout | Game | struct | Athanor/Assets/Scripts/Game/Ui/TitleView.cs | 42 | The title's positions and colours (art px, y down), stored in Title.prefab. |
| TitleView.Timing | TitleView.Timing | Game | struct | Athanor/Assets/Scripts/Game/Ui/TitleView.cs | 57 | The title's motion: logo fade-in, the sub-title's delay and fade, the glow's colour, base, swing and speed. |
| Tooltip | Tooltip | Game | class | Athanor/Assets/Scripts/Game/Ui/Tooltip.cs | 10 | A small panel with a title and two lines of text that appears beside whatever the pointer rests on. A hand card's tip adds a row under the name with its stars and tier word, and the panel grows by tha |
| Tooltip.Layout | Tooltip.Layout | Game | struct | Athanor/Assets/Scripts/Game/Ui/Tooltip.cs | 18 | The tooltip's inner offsets (px), stored in the Bench prefab. |
| UiButton | UiButton | Game | class | Athanor/Assets/Scripts/Game/Ui/UiButton.cs | 7 | A 120x26 red button with a label: brightens under the pointer, sinks when pressed. |
| UiButton.Look | UiButton.Look | Game | struct | Athanor/Assets/Scripts/Game/Ui/UiButton.cs | 15 | The button's colours and label offsets, stored in each prefab that holds one. |
| ArtLibraryBuilder | ArtLibraryBuilder | Editor | class | Athanor/Assets/Editor/ArtLibraryBuilder.cs | 12 | Collects the sprites, effect sheets, fonts and materials into Assets/Art/ArtLibrary.asset, the one lookup the game uses by name. Run it after new art or effect sheets are imported (the sprite and flip |
| AtlasBuilder | AtlasBuilder | Editor | class | Athanor/Assets/Editor/AtlasBuilder.cs | 27 | Two art steps that run after tools/art/export.mjs, both idempotent: 1. Sprite sheets: the numbered frames of each animation (Background/flame_small_0..7.png ...) are merged into one Multiple-mode shee |
| BalanceReports | BalanceReports | Editor | class | Athanor/Assets/Editor/Balance/BalanceReports.cs | 16 | The balance reports (Athanor > Balance, or BalanceReports.RunBatch). They print numbers and assert nothing. dotnet run --project tools/CoreReports -- balance-matrix \| events \| roles \| newcomer \| balan |
| CheckSprites | CheckSprites | Editor | class | Athanor/Assets/Editor/CheckSprites.cs | 6 |  |
| FixCandleImport | FixCandleImport | Editor | class | Athanor/Assets/Editor/FixCandleImport.cs | 4 |  |
| FlipbookImporter | FlipbookImporter | Editor | class | Athanor/Assets/Editor/FlipbookImporter.cs | 14 | Turns each pixelfx sheet in Assets/Art/Fx/Sheets (name.png + name.json, exported from pixelfx) into sliced sprites and a FlipbookSheet asset, then adds them to the art library so Fx.Spawn(name) can pl |
| FontBuilder | FontBuilder | Editor | class | Athanor/Assets/Editor/FontBuilder.cs | 10 | Bakes the pixel fonts into TextMeshPro bitmap font assets at their native size (crisp, point-filtered). |
| LayoutData | LayoutData | Editor | class | Athanor/Assets/Editor/LayoutData.cs | 11 |  |
| ContentFieldMigration | ContentFieldMigration | Editor | class | Athanor/Assets/Editor/Migration/ContentFieldMigration.cs | 11 | Sets GameFlow.content in Main.unity to Assets/Content/ContentDatabase.asset and clears contentJson. |
| ContentAssets | ContentAssets | Editor | class | Athanor/Assets/Editor/Shared/ContentAssets.cs | 5 | The content assets for Editor tools and tests (no scene, no Resources). |
| ContentSnapshot | ContentSnapshot | Editor | class | Athanor/Assets/Editor/Shared/ContentSnapshot.cs | 9 | Writes the content assets as tools/content/snapshot.json (the shape the content assets had) for the tools that are not Unity: tools/ci/check_repo.py, tools/vault/build_vault.py, tools/art/export.mjs.  |
| EditorGlobals | EditorGlobals | Editor | class | Athanor/Assets/Editor/Shared/EditorGlobals.cs | 10 | Sets the globals GameFlow and Sparks would set from their serialized fields at runtime (Art.Lib, Loc.Use, SparkMesh.BaseMaterial), loading the assets through the AssetDatabase. The batch tools (-execu |
| GoldenShots | GoldenShots | Editor | class | Athanor/Assets/Editor/Shared/GoldenShots.cs | 19 | The golden shots: one picture of every view in a fixed, seeded state, in English and Vietnamese. Each view is built the way its prefab test builds it (instantiate, Bind, Show / Open), never through Up |
| Reserialize | Reserialize | Editor | class | Athanor/Assets/Editor/Shared/Reserialize.cs | 11 | Rewrites every view prefab and the data assets in the current serializer format, so a field added to a component is stored with its value. Phase 7 uses it after a number moves from code to a prefab. M |
| ViewCapture | ViewCapture | Editor | class | Athanor/Assets/Editor/Shared/ViewCapture.cs | 8 | Renders a GameObject alone (in an otherwise empty scene) to a 640x360 image, the way the UI camera draws it. |
| Sim | Sim | Editor | class | Athanor/Assets/Editor/Simulation/Sim.cs | 9 | The simulated players behind the balance tests and the Editor balance reports. |
| Newcomer | Newcomer | Editor | class | Athanor/Assets/Editor/Simulation/Sim.cs | 70 |  |
| Career | Career | Editor | class | Athanor/Assets/Editor/Simulation/Sim.cs | 385 |  |
| RunStats | RunStats | Editor | class | Athanor/Assets/Editor/Simulation/Sim.cs | 483 | What happened over one scripted run, for the balance report. |
| SpriteImportRules | SpriteImportRules | Editor | class | Athanor/Assets/Editor/SpriteImportRules.cs | 10 | Every texture under Assets/Art is pixel art: a point-filtered, uncompressed sprite at 1 pixel per unit. The one exception is Assets/Art/Depth (the HD-2D layers): its pre-blurred pictures (*_far, *_nea |
| TutorialAreaEditor | TutorialAreaEditor | Editor | class | Athanor/Assets/Editor/TutorialStepGizmo.cs | 11 | Scene-view preview of the tutorial steps in Main.unity (Edit Mode): every Fixed area is drawn as an outline, green when clicks pass through it and amber when it is highlight only, with the step name a |
| TutorialStepsMigration | TutorialStepsMigration | Editor | class | Athanor/Assets/Editor/TutorialStepsMigration.cs | 16 | Creates (or updates, by name) the tutorial step objects under the Tutorial object of Main.unity, from the table below. The table reproduces what Drive lit and which clicks reached the game before the  |
| A | A | Editor | class | Athanor/Assets/Editor/TutorialStepsMigration.cs | 24 |  |
| S | S | Editor | class | Athanor/Assets/Editor/TutorialStepsMigration.cs | 32 |  |
| WebBuild | WebBuild | Editor | class | Athanor/Assets/Editor/WebBuild.cs | 10 | Builds the game for the browser into Build/web. Also callable from the command line: unity build Athanor --target WebGL --execute-method WebBuild.Build |
| ChainTests | ChainTests | Tests | class | Athanor/Assets/Tests/EditMode/ChainTests.cs | 8 | The chain planner on TestContent.Combo(): p 8, q 5, r 2, s 9, t 4, v 1 (Reaction Index). |
| ContentAssetTests | ContentAssetTests | Tests | class | Athanor/Assets/Tests/EditMode/ContentAssetTests.cs | 7 | Rules about the content assets themselves. |
| ContentDatabaseTests | ContentDatabaseTests | Tests | class | Athanor/Assets/Tests/EditMode/ContentDatabaseTests.cs | 9 | ContentDatabase built in memory hands the rules the plain data they read. |
| ContentEnumTests | ContentEnumTests | Tests | class | Athanor/Assets/Tests/EditMode/ContentEnumTests.cs | 9 | The Inspector enums of the content assets spell exactly the names the rules read. |
| ContentSnapshotTests | ContentSnapshotTests | Tests | class | Athanor/Assets/Tests/EditMode/ContentSnapshotTests.cs | 7 | tools/content/snapshot.json is the assets written out; a content change without a new snapshot fails here. |
| ContentTests | ContentTests | Tests | class | Athanor/Assets/Tests/EditMode/ContentTests.cs | 9 |  |
| IgniteTests | IgniteTests | Tests | class | Athanor/Assets/Tests/EditMode/IgniteTests.cs | 8 |  |
| TestLang | TestLang | Tests | class | Athanor/Assets/Tests/EditMode/LangTests.cs | 10 | Loads the real language files (Resources/lang/en.json, vi.json), inside Unity. |
| LangTests | LangTests | Tests | class | Athanor/Assets/Tests/EditMode/LangTests.cs | 18 |  |
| LoreTests | LoreTests | Tests | class | Athanor/Assets/Tests/EditMode/LoreTests.cs | 9 | Lore events: the last one seen is not drawn again until another Lore event has been seen. |
| Phase3Tests | Phase3Tests | Tests | class | Athanor/Assets/Tests/EditMode/Phase3Tests.cs | 10 | Phase 3: the profile and its goals, unlocks, the new catalysts, bench rules, aspects, events and the start of a depth. |
| RarityReportTests | RarityReportTests | Tests | class | Athanor/Assets/Tests/EditMode/RarityReportTests.cs | 17 | Opt-in (Explicit) report: does each rarity pay? Same seeds, the informed bot, but at every catalyst offer it follows one policy: take the first offered catalyst of one rarity (else the bot's pick), a  |
| RarityTests | RarityTests | Tests | class | Athanor/Assets/Tests/EditMode/RarityTests.cs | 9 | Phase 5a-04: catalyst offers and event gifts draw by the optional the content assets "rarityWeights" (Common..WorldSeed). |
| RoleSimTests | RoleSimTests | Tests | class | Athanor/Assets/Tests/EditMode/RoleSimTests.cs | 17 | Two simulated players on the full Phase 3 game (unlocks, aspects, events), each starting from an empty journal and a fresh profile and keeping both from run to run. Admin: knows every recipe and how a |
| RulesTests | RulesTests | Tests | class | Athanor/Assets/Tests/EditMode/RulesTests.cs | 8 |  |
| RunStateTests | RunStateTests | Tests | class | Athanor/Assets/Tests/EditMode/RunStateTests.cs | 9 |  |
| SatchelTests | SatchelTests | Tests | class | Athanor/Assets/Tests/EditMode/SatchelTests.cs | 8 |  |
| JournalTests | JournalTests | Tests | class | Athanor/Assets/Tests/EditMode/SatchelTests.cs | 45 |  |
| ScreenFlowTests | ScreenFlowTests | Tests | class | Athanor/Assets/Tests/EditMode/ScreenFlowTests.cs | 6 |  |
| SettingsTests | SettingsTests | Tests | class | Athanor/Assets/Tests/EditMode/SettingsTests.cs | 6 |  |
| TestContent | TestContent | Tests | class | Athanor/Assets/Tests/EditMode/TestContent.cs | 6 | The real content, from Assets/Content. |
| TutorialTests | TutorialTests | Tests | class | Athanor/Assets/Tests/EditMode/TutorialTests.cs | 9 | Phase 4: the hooks the tutorial needs, and the tutorial script replayed with every number it shows. |
| AutoHookTests | AutoHookTests | Tests | class | Athanor/Assets/Tests/EditModeGame/AutoHookTests.cs | 12 | F9 (the scripted player) is a toggle, so tools/web/descents.mjs cannot know its state from its own key presses: a press at the wrong moment left the scripted player off after depth 2 and the run stood |
| ChoiceHookTests | ChoiceHookTests | Tests | class | Athanor/Assets/Tests/EditModeGame/ChoiceHookTests.cs | 14 | The card chooser logs its development hook only once the cards take a click (the deal is over). tools/web/webtest.mjs and descents.mjs click a card after this line; a click before it was lost and the  |
| DepthClickTests | DepthClickTests | Tests | class | Athanor/Assets/Tests/EditModeGame/DepthClickTests.cs | 13 | Clicks on the HD-2D bench: a point of the UI camera's world goes through the world texture and the perspective world camera to the table's tilted top, and the flat point found there is what TileView.C |
| DepthFixture | DepthFixture | Tests | class | Athanor/Assets/Tests/EditModeGame/DepthFixture.cs | 10 | The HD-2D rig of the main scene as test data: the numbers Main.unity holds (LayoutPinTests.Depth_MainScene_KeepsTodaysRigSwayAndTable pins the scene against the same literals). The game code has no co |
| DepthLayerTests | DepthLayerTests | Tests | class | Athanor/Assets/Tests/EditModeGame/DepthLayerTests.cs | 12 | The HD-2D projection maths (HD-2D mockup SPEC, "Layer stack and projection"; removed in P6-22, see git history) and the rules that keep the depth art out of the shipped game. |
| DepthLookTests | DepthLookTests | Tests | class | Athanor/Assets/Tests/EditModeGame/DepthLookTests.cs | 5 |  |
| GameTestSetup | GameTestSetup | Tests | class | Athanor/Assets/Tests/EditModeGame/GameTestSetup.cs | 7 | Once per run of the Game tests: the language files, art library and particle material the game would get from GameFlow and Sparks. |
| GoldenShotTests | GoldenShotTests | Tests | class | Athanor/Assets/Tests/EditModeGame/GoldenShotTests.cs | 8 | Every view, in both languages, still looks like the picture recorded in production/qa/evidence/phase7/baseline/golden. |
| HotPathTests | HotPathTests | Tests | class | Athanor/Assets/Tests/EditModeGame/HotPathTests.cs | 12 | Idle frames allocate nothing: a component that has nothing to change this frame builds no string, looks up no sprite and makes no closure. |
| HudTests | HudTests | Tests | class | Athanor/Assets/Tests/EditModeGame/HudTests.cs | 9 | The Redraw button is live only while a redraw is left and the bench is in play. |
| LabelLineBoxTests | LabelLineBoxTests | Tests | class | Athanor/Assets/Tests/EditModeGame/LabelLineBoxTests.cs | 15 | Line-box text (the Descent's depth name, SPEC (f)): the mockup sets it in a CSS line box as tall as the size, so the glyphs sit a half-leading below the box top; TMP puts the baseline one ascender dow |
| LayoutPinTests | LayoutPinTests | Tests | class | Athanor/Assets/Tests/EditModeGame/LayoutPinTests.cs | 11 | Phase 7: numbers that moved from code into Main.unity or a prefab equal the literals the code had. |
| LocTests | LocTests | Tests | class | Athanor/Assets/Tests/EditModeGame/LocTests.cs | 12 | The Game layer's language: Vietnamese draws from SVN-Coder's Crux, every key resolves, and the core's English notes are translated. |
| MainDepthTests | MainDepthTests | Tests | class | Athanor/Assets/Tests/EditModeGame/MainDepthTests.cs | 11 | The HD-2D bench in the main scene (5b-07a): layers, the tiles' depth group, clicks through the world ray, the shake. |
| MainDevHookTests | MainDevHookTests | Tests | class | Athanor/Assets/Tests/EditModeGame/MainDevHookTests.cs | 11 | The dev-only hooks tools/web/mainhd2d.mjs drives (5b-07b): F6 mood steps, F10 tile faces in UI units, the bench's place / unplace lines. The key handling is guarded by Application.isEditor \|\| Debug.is |
| MainSceneFixture | MainSceneFixture | Tests | class | Athanor/Assets/Tests/EditModeGame/MainSceneFixture.cs | 14 | Main.unity opened beside the test scene, for the views that are scene objects and not prefabs. Dispose closes it without saving. |
| MotionTests | MotionTests | Tests | class | Athanor/Assets/Tests/EditModeGame/MotionTests.cs | 13 | Motion's helpers make live PrimeTween tweens. PrimeTween refuses a payload over 16 bytes: it logs an error and returns a dead tween, which a Sequence then refuses too (the rack flight and the catalyst |
| NoteTextTests | NoteTextTests | Tests | class | Athanor/Assets/Tests/EditModeGame/NoteTextTests.cs | 8 | An event note (a key and its arguments from the core) reads as the English or Vietnamese sentence the regex-matched wording used to give. |
| PauseTests | PauseTests | Tests | class | Athanor/Assets/Tests/EditModeGame/PauseTests.cs | 10 | The pause menu: time stops and comes back on every exit, Esc toggles, nothing opens in the tutorial, Quit leaves the stored run data alone. |
| PrefabBakeTests | PrefabBakeTests | Tests | class | Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs | 13 | The shipped prefabs keep what the views need: a label keeps its key, a button still clicks, the tooltip makes its panel. |
| PrefabFixture | PrefabFixture | Tests | class | Athanor/Assets/Tests/EditModeGame/PrefabFixture.cs | 10 | A copy of a prefab for one test. Dispose calls Destroy automatically. |
| RunOverDepthTests | RunOverDepthTests | Tests | class | Athanor/Assets/Tests/EditModeGame/RunOverDepthTests.cs | 15 | The run-over depth row (owner, phase-5-prep 2026-11-16): a won run says "Planted at depth n" on one line with no value; a lost run says "Depth reached" with "n of 5". The view is reused, so each path  |
| SaveTests | SaveTests | Tests | class | Athanor/Assets/Tests/EditModeGame/SaveTests.cs | 11 | The journal and the profile survive a round trip through PlayerPrefs, and a damaged save never stops the game. |
| SceneBakeTests | SceneBakeTests | Tests | class | Athanor/Assets/Tests/EditModeGame/SceneBakeTests.cs | 12 | The scene objects of Main.unity that are not prefabs (Task 15): their serialized parts are all there, and Bind finishes them. |
| SettingsHapticsTests | SettingsHapticsTests | Tests | class | Athanor/Assets/Tests/EditModeGame/SettingsHapticsTests.cs | 9 | Saved settings (athanor.settings.v1), the haptics plan and the mute-on-hidden-tab hook. |
| SparksTests | SparksTests | Tests | class | Athanor/Assets/Tests/EditModeGame/SparksTests.cs | 9 | Sparks on batched meshes: same motion and fade as the old SpriteRenderer pool, whole-pixel centres, HDR kept, no garbage. |
| StaticResetTests | StaticResetTests | Tests | class | Athanor/Assets/Tests/EditModeGame/StaticResetTests.cs | 13 | With domain reload off, statics survive Stop and Play. Every allowlisted class has a ResetStatics that [RuntimeInitializeOnLoadMethod(SubsystemRegistration)] runs before the first scene loads; these t |
| TestFx | TestFx | Tests | class | Athanor/Assets/Tests/EditModeGame/TestFx.cs | 10 | Effect players for a test that binds a view: a ScreenFx with its flash and fade sprites, and a Sparks. |
| TutorialAreaTests | TutorialAreaTests | Tests | class | Athanor/Assets/Tests/EditModeGame/TutorialAreaTests.cs | 7 |  |
| TutorialFlowTests | TutorialFlowTests | Tests | class | Athanor/Assets/Tests/EditModeGame/TutorialFlowTests.cs | 11 | The tutorial's decisions, its sandbox and the scores it shows (the numbers TutorialTests pins in Core). |
| TutorialStepsBakeTests | TutorialStepsBakeTests | Tests | class | Athanor/Assets/Tests/EditModeGame/TutorialStepsBakeTests.cs | 17 | The tutorial steps are scene objects of Main.unity. The baseline (tutorial_lit_baseline.json) is what each step lit when the rects were numbers in TutorialTargets and lists in Drive (art pixels, befor |
| AutoplayTraceTests | AutoplayTraceTests | Tests | class | Athanor/Assets/Tests/PlayMode/AutoplayTraceTests.cs | 11 | Main.unity played by the scripted player (F9) on a fixed seed: the gameplay hooks equal the recorded run. |
| MainScene | MainScene | Tests | class | Athanor/Assets/Tests/PlayMode/PlayModeKit.cs | 16 | Main.unity loaded beside the test runner's scene. |
| PrefsSnapshot | PrefsSnapshot | Tests | class | Athanor/Assets/Tests/PlayMode/PlayModeKit.cs | 37 | Takes the game's PlayerPrefs keys aside and gives the test a known start; Dispose puts the player's keys back. |
| TraceRecorder | TraceRecorder | Tests | class | Athanor/Assets/Tests/PlayMode/PlayModeKit.cs | 65 | The gameplay lines of the dev hooks (Dev.Hook logs them), in order. |
| Baseline | Baseline | Tests | class | Athanor/Assets/Tests/PlayMode/PlayModeKit.cs | 89 | Text baselines under Tests/PlayMode/Baselines. ATHANOR_RECORD=1 writes them instead of comparing. |
| TutorialTraceTests | TutorialTraceTests | Tests | class | Athanor/Assets/Tests/PlayMode/TutorialTraceTests.cs | 13 | The first-run tutorial auto-driven from Begin to line 5.5: its lines and popup scores equal the recorded run. |

## Members
| type | kind | name | line | sig |
|---|---|---|---|---|
| AutoPlayer | field | HintValue | 29 | public double HintValue = 0 |
| AutoPlayer | field | RedrawWhenIdle | 31 | public bool RedrawWhenIdle = true |
| AutoPlayer | field | SeekFrontier | 33 | public bool SeekFrontier = false |
| AutoPlayer | field | Wanted | 35 | public Dictionary<string, int> Wanted |
| AutoPlayer | method | CanMake | 37 | public bool CanMake(string id) |
| AutoPlayer | method | CostOf | 39 | public int CostOf(string id) |
| AutoPlayer | property | KnowsTheWay | 40 | public bool KnowsTheWay |
| AutoPlayer | method | Demand | 42 | public int Demand(string id) |
| AutoPlayer | ctor | AutoPlayer | 50 | public AutoPlayer(ContentDb db) |
| AutoPlayer | method | Step | 66 | public void Step(RunState run) |
| AutoPlayer | method | Decide | 72 | public AutoMove Decide(RunState run) |
| AutoPlayer | property | Move | 223 | struct Move |
| AutoMove | value | None | 11 | None |
| AutoMove | value | ChoseCatalyst | 11 | ChoseCatalyst |
| AutoMove | value | Redrew | 11 | Redrew |
| AutoMove | value | ReadyToIgnite | 11 | ReadyToIgnite |
| AutoMove | value | ChoseAspect | 11 | ChoseAspect |
| AutoMove | value | ChoseEvent | 11 | ChoseEvent |
| BenchRules | field | Diagonals | 14 | public bool Diagonals |
| BenchRules | field | HeatPlus | 15 | public int RiShift, HeatPlus |
| BenchRules | field | Pivot | 16 | public int Pivot = -1 |
| BenchRules | field | Blocked | 17 | public readonly bool[] Blocked = new bool[Bench.TileCount] |
| BenchRules | field | RiBonus | 18 | public readonly int[] RiBonus = new int[Bench.TileCount] |
| BenchRules | field | Kiln | 19 | public readonly bool[] Kiln = new bool[Bench.TileCount], Crossroads = new bool[Bench.TileCount] |
| BenchRules | field | Links | 20 | public readonly List<LinkPair> Links = new List<LinkPair>() |
| BenchRules | property | HubMult | 22 | public readonly int[] HubMult = |
| BenchRules | field | Heavy | 23 | public readonly bool[] Heavy = new bool[Bench.TileCount] |
| BenchRules | field | AttunedKind | 24 | public readonly string[] AttunedKind = new string[Bench.TileCount] |
| BenchRules | field | SeedBonus | 25 | public readonly int[] SeedBonus = new int[Bench.TileCount] |
| BenchRules | property | None | 28 | public static readonly BenchRules None = new BenchRules(), Diagonal = new BenchRules |
| BenchRules | method | Plain | 29 | public static BenchRules Plain(bool diagonals) |
| BenchRules | method | Around | 34 | public int[] Around(int tile) |
| BenchRules | method | Ri | 71 | public int Ri(ContentDb db, int tile, string id) |
| BenchRules | method | Touch | 78 | public bool Touch(int a, int b) |
| ScoreContext | field | Depth | 84 | public int Depth = 1, EmptyTiles |
| ScoreContext | field | Rules | 85 | public BenchRules Rules = BenchRules.None |
| ScoreContext | field | EmptyAtIgnite | 86 | public bool[] EmptyAtIgnite |
| ScoreContext | field | TripleFirst | 87 | public bool TripleFirst |
| ScoreContext | field | IsStarter | 88 | public Func<string, bool> IsStarter |
| ChainStep | field | Hub | 13 | public int Hub |
| ChainStep | field | Tiles | 14 | public int[] Tiles |
| ChainStep | field | Items | 15 | public string[] Items |
| ChainStep | field | Recipe | 16 | public RecipeData Recipe |
| ChainStep | field | Sputter | 17 | public bool Sputter |
| ChainPlan | field | Steps | 22 | public readonly List<ChainStep> Steps = new List<ChainStep>() |
| ChainPlan | field | Final | 23 | public string[] Final |
| ChainPlan | field | Made | 24 | public bool[] Made |
| ChainPlanner | field | SputterSize | 35 | public const int SputterSize = 3 |
| ChainPlanner | method | Neighbours | 37 | public static IEnumerable<int> Neighbours(int tile, bool diagonals) |
| ChainPlanner | method | Neighbours | 38 | public static int[] Neighbours(int tile, BenchRules rules) |
| ChainPlanner | method | Heat | 45 | public static int Heat(string[] tiles, int hub, ContentDb db, bool diagonals) |
| ChainPlanner | method | Heat | 47 | public static int Heat(string[] tiles, int hub, ContentDb db, BenchRules rules) |
| ChainPlanner | method | MatchAt | 56 | public static ChainStep MatchAt(string[] tiles, int hub, ContentDb db, bool diagonals) |
| ChainPlanner | method | MatchAt | 57 | public static ChainStep MatchAt(string[] tiles, int hub, ContentDb db, BenchRules rules) |
| ChainPlanner | method | ShortAt | 63 | public static RecipeData ShortAt(string[] tiles, int hub, ContentDb db, bool diagonals) |
| ChainPlanner | method | ShortAt | 64 | public static RecipeData ShortAt(string[] tiles, int hub, ContentDb db, BenchRules rules) |
| ChainPlanner | method | Plan | 109 | public static ChainPlan Plan(string[] tiles, ContentDb db, bool diagonals, bool sputters = true) |
| ChainPlanner | method | Plan | 112 | public static ChainPlan Plan(string[] tiles, ContentDb db, BenchRules rules, bool sputters = true) |
| AspectDefinition | field | displayName | 10 | public string displayName = |
| AspectDefinition | field | effect | 12 | public AspectEffect effect |
| AspectDefinition | field | scope | 13 | public AspectScope scope |
| AspectDefinition | field | group | 14 | public AspectGroup group |
| AspectDefinition | method | ToData | 19 | public AspectData ToData() |
| CatalystDefinition | field | displayName | 10 | public string displayName = |
| CatalystDefinition | field | effect | 12 | public CatalystEffect effect |
| CatalystDefinition | field | amount | 14 | public float amount |
| CatalystDefinition | field | rarity | 15 | public RarityId rarity = RarityId.Common |
| CatalystDefinition | method | ToData | 19 | public CatalystData ToData() |
| ContentDatabase | field | rules | 14 | public GameRules rules |
| ContentDatabase | field | materials | 15 | public List<MaterialDefinition> materials = new List<MaterialDefinition>() |
| ContentDatabase | field | recipes | 16 | public List<RecipeDefinition> recipes = new List<RecipeDefinition>() |
| ContentDatabase | field | catalysts | 17 | public List<CatalystDefinition> catalysts = new List<CatalystDefinition>() |
| ContentDatabase | field | aspects | 18 | public List<AspectDefinition> aspects = new List<AspectDefinition>() |
| ContentDatabase | field | events | 19 | public List<EventDefinition> events = new List<EventDefinition>() |
| ContentDatabase | field | goals | 20 | public List<GoalDefinition> goals = new List<GoalDefinition>() |
| ContentDatabase | method | ToContentData | 23 | public ContentData ToContentData() |
| CatalystEffect | value | KindDouble | 17 | KindDouble |
| CatalystEffect | value | CentreTwice | 17 | CentreTwice |
| CatalystEffect | value | FirstTriple | 17 | FirstTriple |
| CatalystEffect | value | Diagonals | 17 | Diagonals |
| CatalystEffect | value | ExtraTrial | 17 | ExtraTrial |
| CatalystEffect | value | ExtraHand | 17 | ExtraHand |
| CatalystEffect | value | DiscoveryTriple | 17 | DiscoveryTriple |
| CatalystEffect | value | EmptyTileBonus | 17 | EmptyTileBonus |
| CatalystEffect | value | ChainPlus | 17 | ChainPlus |
| CatalystEffect | value | ComboBonus | 18 | ComboBonus |
| CatalystEffect | value | PairBonus | 18 | PairBonus |
| CatalystEffect | value | FlatPerReaction | 18 | FlatPerReaction |
| CatalystEffect | value | TierDouble | 18 | TierDouble |
| CatalystEffect | value | StarterDouble | 18 | StarterDouble |
| CatalystEffect | value | ExtraRedraw | 18 | ExtraRedraw |
| CatalystEffect | value | HeatPlus | 18 | HeatPlus |
| CatalystEffect | value | Salvage | 18 | Salvage |
| CatalystEffect | value | ChainFrom | 18 | ChainFrom |
| CatalystEffect | value | DepthScale | 19 | DepthScale |
| CatalystEffect | value | FreeDiscovery | 19 | FreeDiscovery |
| CatalystEffect | value | AllScale | 19 | AllScale |
| CatalystEffect | value | SecondChance | 19 | SecondChance |
| ContentNames | method | Rarity | 50 | public static string Rarity(RarityId r) |
| ContentNames | method | Kind | 52 | public static string Kind(KindOrNone k) |
| ContentNames | method | Need | 54 | public static string Need(EventNeed n) |
| ContentNames | method | Stat | 56 | public static string Stat(GoalStat s) |
| ContentNames | method | Reward | 58 | public static string Reward(RewardKind k) |
| ContentNames | method | Parse | 61 | public static T Parse<T>(string s, T none) where T : struct, Enum |
| ContentNames | method | ParseStat | 64 | public static GoalStat ParseStat(string s) |
| ContentNames | method | ParseReward | 72 | public static RewardKind ParseReward(string s) |
| RarityId | value | Unset | 9 | Unset |
| RarityId | value | Common | 9 | Common |
| RarityId | value | Uncommon | 9 | Uncommon |
| RarityId | value | Rare | 9 | Rare |
| RarityId | value | Epic | 9 | Epic |
| RarityId | value | Legendary | 9 | Legendary |
| RarityId | value | Mythic | 9 | Mythic |
| RarityId | value | WorldSeed | 9 | WorldSeed |
| KindOrNone | value | None | 12 | None |
| KindOrNone | value | Ore | 12 | Ore |
| KindOrNone | value | Metal | 12 | Metal |
| KindOrNone | value | Crystal | 12 | Crystal |
| KindOrNone | value | Powder | 12 | Powder |
| KindOrNone | value | Potion | 12 | Potion |
| KindOrNone | value | Plant | 12 | Plant |
| KindOrNone | value | Beast | 12 | Beast |
| AspectEffect | value | Doubled | 23 | Doubled |
| AspectEffect | value | Anchor | 23 | Anchor |
| AspectEffect | value | Heated | 23 | Heated |
| AspectEffect | value | Echo | 23 | Echo |
| AspectEffect | value | Attuned | 23 | Attuned |
| AspectEffect | value | Seed | 23 | Seed |
| AspectEffect | value | Fallow | 23 | Fallow |
| AspectEffect | value | Heavy | 23 | Heavy |
| AspectEffect | value | Cold | 23 | Cold |
| AspectEffect | value | Volatile | 23 | Volatile |
| AspectEffect | value | Greedy | 23 | Greedy |
| AspectEffect | value | Bridge | 23 | Bridge |
| AspectEffect | value | OpenBench | 23 | OpenBench |
| AspectEffect | value | Kiln | 23 | Kiln |
| AspectEffect | value | Crossroads | 23 | Crossroads |
| AspectEffect | value | Pivot | 23 | Pivot |
| AspectScope | value | Tile | 26 | Tile |
| AspectScope | value | Link | 26 | Link |
| AspectScope | value | Bench | 26 | Bench |
| AspectGroup | value | Cheap | 29 | Cheap |
| AspectGroup | value | Engine | 29 | Engine |
| EventOp | value | Trials | 32 | Trials |
| EventOp | value | Hand | 32 | Hand |
| EventOp | value | Redraws | 32 | Redraws |
| EventOp | value | RiShift | 32 | RiShift |
| EventOp | value | GiveDiscovery | 32 | GiveDiscovery |
| EventOp | value | LoseRack | 32 | LoseRack |
| EventOp | value | LoseCard | 32 | LoseCard |
| EventOp | value | FirstTriple | 32 | FirstTriple |
| EventOp | value | Hint | 32 | Hint |
| EventOp | value | BlockTile | 32 | BlockTile |
| EventOp | value | HeatTile | 32 | HeatTile |
| EventOp | value | CatalystUpgrade | 32 | CatalystUpgrade |
| EventOp | value | CatalystGift | 32 | CatalystGift |
| EventOp | value | Nothing | 32 | Nothing |
| EventKind | value | Choice | 35 | Choice |
| EventKind | value | Gamble | 35 | Gamble |
| EventKind | value | Find | 35 | Find |
| EventKind | value | Shrine | 35 | Shrine |
| EventKind | value | Hazard | 35 | Hazard |
| EventKind | value | Boon | 35 | Boon |
| EventKind | value | Lore | 35 | Lore |
| EventNeed | value | None | 38 | None |
| EventNeed | value | Rack | 38 | Rack |
| EventNeed | value | Catalyst | 38 | Catalyst |
| EventNeed | value | Trial | 38 | Trial |
| EventNeed | value | Redraw | 38 | Redraw |
| GoalStat | value | RunsStarted | 41 | RunsStarted |
| GoalStat | value | RunsWon | 41 | RunsWon |
| GoalStat | value | WonNoRedraw | 41 | WonNoRedraw |
| GoalStat | value | DepthReached | 41 | DepthReached |
| GoalStat | value | Ignites | 41 | Ignites |
| GoalStat | value | ChainLongest | 41 | ChainLongest |
| GoalStat | value | ComboLargest | 41 | ComboLargest |
| GoalStat | value | BestIgnite | 41 | BestIgnite |
| GoalStat | value | JournalEntries | 41 | JournalEntries |
| GoalStat | value | JournalComplete | 41 | JournalComplete |
| RewardKind | value | Starter | 44 | Starter |
| RewardKind | value | Catalyst | 44 | Catalyst |
| EventEffectDef | field | op | 11 | public EventOp op |
| EventEffectDef | field | amount2 | 13 | public int amount, amount2 |
| EventEffectDef | method | ToData | 15 | public EffectData ToData() |
| EventOutcomeDef | field | gain | 24 | public EventEffectDef[] gain = new EventEffectDef[0] |
| EventOutcomeDef | method | ToData | 26 | public EventOutcome ToData() |
| EventOptionDef | field | label | 33 | public string label = |
| EventOptionDef | field | requires | 34 | public EventNeed requires |
| EventOptionDef | field | cost | 35 | public EventEffectDef[] cost = new EventEffectDef[0] |
| EventOptionDef | field | outcomes | 36 | public EventOutcomeDef[] outcomes = new EventOutcomeDef[0] |
| EventOptionDef | method | ToData | 38 | public EventOption ToData() |
| EventDefinition | field | id | 49 | public string id = |
| EventDefinition | field | type | 52 | public EventKind type |
| EventDefinition | field | options | 56 | public EventOptionDef[] options = new EventOptionDef[0] |
| EventDefinition | method | ToData | 59 | public EventData ToData() |
| GameRules | field | rarityWeights | 32 | public int[] rarityWeights = new int[0] |
| GameRules | method | ToData | 35 | public RulesData ToData() |
| GoalDefinition | field | id | 9 | public string id = |
| GoalDefinition | field | stat | 11 | public GoalStat stat |
| GoalDefinition | field | rewardKind | 13 | public RewardKind rewardKind |
| GoalDefinition | method | ToData | 17 | public GoalData ToData() |
| MaterialDefinition | field | id | 10 | public string id = |
| MaterialDefinition | field | displayName | 12 | public string displayName = |
| MaterialDefinition | field | kind | 13 | public Kind kind |
| MaterialDefinition | field | glow | 18 | public Color glow = Color.white |
| MaterialDefinition | field | hasGlow | 20 | public bool hasGlow = true |
| MaterialDefinition | field | rarity | 22 | public RarityId rarity |
| MaterialDefinition | field | locked | 24 | public bool locked |
| MaterialDefinition | method | ToData | 27 | public MaterialData ToData() |
| RecipeDefinition | field | items | 10 | public MaterialDefinition[] items = new MaterialDefinition[0] |
| RecipeDefinition | field | result | 13 | public MaterialDefinition result |
| RecipeDefinition | method | ToData | 16 | public RecipeData ToData() |
| RulesData | field | handBase | 32 | public int handMin, handBase |
| RulesData | field | trialsFinalDepth | 34 | public int trials, trialsFinalDepth |
| RulesData | field | redraws | 36 | public int redraws |
| RulesData | field | starterCopies | 38 | public int starterCopies |
| RulesData | field | offerSize | 40 | public int offerSize |
| RulesData | field | rackSize | 42 | public int rackSize |
| RulesData | field | pouchCards | 44 | public int pouchCards |
| RulesData | field | maxRi | 46 | public int maxRi |
| RulesData | field | firstReactionMult | 48 | public int firstReactionMult |
| RulesData | field | aspectDepths | 50 | public int[] aspectDepths |
| RulesData | field | potencyByTier | 52 | public long[] potencyByTier |
| ContentData | field | rules | 58 | public RulesData rules |
| ContentData | field | materials | 59 | public MaterialData[] materials |
| ContentData | field | recipes | 60 | public RecipeData[] recipes |
| ContentData | field | catalysts | 61 | public CatalystData[] catalysts |
| ContentData | field | depthTargets | 62 | public int[] depthTargets |
| ContentData | field | depthNames | 63 | public string[] depthNames |
| ContentData | field | ultimate | 64 | public string ultimate |
| ContentData | field | goals | 65 | public GoalData[] goals |
| ContentData | field | aspects | 66 | public AspectData[] aspects |
| ContentData | field | events | 67 | public EventData[] events |
| ContentData | field | noEventWeight | 68 | public int noEventWeight |
| ContentData | field | rarityWeights | 70 | public int[] rarityWeights |
| ProfileData | field | version | 80 | public int version = 1, rewardSeed |
| ProfileData | field | comboLargest | 81 | public int runsStarted, runsWon, wonNoRedraw, depthReached, ignites, chainLongest, comboLargest |
| ProfileData | field | bestIgnite | 82 | public long bestIgnite |
| ProfileData | field | unlockedCatalysts | 83 | public string[] completed, unlockedStarters, unlockedCatalysts |
| ProfileData | field | rewards | 84 | public RewardLogData[] rewards |
| ProfileData | field | lastLore | 85 | public string lastLore |
| Rarities | field | Common | 90 | public const string Common = "Common", Uncommon = "Uncommon", Rare = "Rare", Epic = "Epic", Legendary = "Legendary", Mythic = "Mythic", WorldSeed = |
| Rarities | property | All | 92 | public static readonly string[] All = |
| Rarities | method | Rank | 93 | public static int Rank(string rarity) |
| Rarities | method | Of | 94 | public static string Of(string rarity) |
| Effects | field | KindDouble | 100 | public const string KindDouble = "KindDouble", CentreTwice = "CentreTwice", FirstTriple = "FirstTriple", Diagonals = "Diagonals", ExtraTrial = "ExtraTrial", ExtraHand = "ExtraHand", DiscoveryTriple =  |
| Effects | property | All | 108 | public static readonly string[] All = |
| AspectEffects | field | Doubled | 119 | public const string Doubled = "Doubled", Anchor = "Anchor", Heated = "Heated", Echo = "Echo", Attuned = "Attuned", Seed = "Seed", Fallow = "Fallow", Heavy = "Heavy", Cold = "Cold", Volatile = "Volatil |
| AspectEffects | property | All | 122 | public static readonly string[] All = |
| Ops | field | Trials | 131 | public const string Trials = "Trials", Hand = "Hand", Redraws = "Redraws", RiShift = "RiShift", GiveDiscovery = "GiveDiscovery", LoseRack = "LoseRack", LoseCard = "LoseCard", FirstTriple = "FirstTripl |
| Ops | property | All | 134 | public static readonly string[] All = |
| Ops | field | NeedRack | 139 | public const string NeedRack = "Rack", NeedCatalyst = "Catalyst", NeedTrial = "Trial", NeedRedraw = |
| Ops | property | Needs | 140 | public static readonly string[] Needs = |
| Ops | property | Types | 141 | public static readonly string[] Types = |
| StatIds | field | RunsStarted | 147 | public const string RunsStarted = "runsStarted", RunsWon = "runsWon", WonNoRedraw = "wonNoRedraw", DepthReached = "depthReached", Ignites = "ignites", ChainLongest = "chainLongest", ComboLargest = "co |
| StatIds | property | All | 150 | public static readonly string[] All = |
| Kind | value | Ore | 5 | Ore |
| Kind | value | Metal | 5 | Metal |
| Kind | value | Crystal | 5 | Crystal |
| Kind | value | Powder | 5 | Powder |
| Kind | value | Potion | 5 | Potion |
| Kind | value | Plant | 5 | Plant |
| Kind | value | Beast | 5 | Beast |
| ContentDb | field | MinItems | 19 | public const int MinItems = 2, MaxItems = 5, MaxCopies = 2, MinReactivity = 1, MaxReactivity = 10 |
| ContentDb | ctor | ContentDb | 23 | public ContentDb(ContentData data) |
| ContentDb | property | Rules | 151 | public RulesData Rules |
| ContentDb | property | FinalDepth | 154 | public int FinalDepth |
| ContentDb | method | LockedIn | 180 | public List<string> LockedIn(string rewardKind, string rarity) |
| ContentDb | property | Starters | 188 | public IReadOnlyList<MaterialData> Starters |
| ContentDb | property | Materials | 189 | public IReadOnlyList<MaterialData> Materials |
| ContentDb | property | Recipes | 190 | public IReadOnlyList<RecipeData> Recipes |
| ContentDb | property | Catalysts | 191 | public IReadOnlyList<CatalystData> Catalysts |
| ContentDb | property | Goals | 192 | public IReadOnlyList<GoalData> Goals |
| ContentDb | property | Aspects | 193 | public IReadOnlyList<AspectData> Aspects |
| ContentDb | property | Events | 194 | public IReadOnlyList<EventData> Events |
| ContentDb | property | NoEventWeight | 195 | public int NoEventWeight |
| ContentDb | method | RarityWeight | 198 | public int RarityWeight(string rarity) |
| ContentDb | property | UniformRarity | 201 | public bool UniformRarity |
| ContentDb | method | DrawCatalysts | 204 | public List<CatalystData> DrawCatalysts(List<CatalystData> pool, int n, Rng rng) |
| ContentDb | method | PickCatalyst | 213 | public CatalystData PickCatalyst(IList<CatalystData> pool, Rng rng) |
| ContentDb | method | Catalyst | 222 | public CatalystData Catalyst(string id) |
| ContentDb | property | Ultimate | 223 | public string Ultimate |
| ContentDb | method | Material | 225 | public MaterialData Material(string id) |
| ContentDb | method | Has | 226 | public bool Has(string id) |
| ContentDb | method | KindOf | 227 | public Kind KindOf(string id) |
| ContentDb | method | PartnersOf | 228 | public IEnumerable<string> PartnersOf(string id) |
| ContentDb | method | Reactivity | 230 | public int Reactivity(string id) |
| ContentDb | method | TotalReactivity | 231 | public int TotalReactivity(RecipeData r) |
| ContentDb | method | KeyOf | 232 | public string KeyOf(RecipeData r) |
| ContentDb | method | Company | 235 | public int Company(RecipeData r) |
| ContentDb | method | RecipesContaining | 238 | public IReadOnlyList<RecipeData> RecipesContaining(string id) |
| ContentDb | method | TryCombo | 241 | public bool TryCombo(IEnumerable<string> items, out RecipeData recipe) |
| ContentDb | method | TryReact | 243 | public bool TryReact(string a, string b, out string result) |
| ContentDb | method | TargetFor | 251 | public int TargetFor(int depth) |
| ContentDb | method | DepthName | 257 | public string DepthName(int depth) |
| ContentDb | method | BasePotency | 264 | public long BasePotency(int tier) |
| ContentDb | method | PairKey | 266 | public static string PairKey(string a, string b) |
| ContentDb | method | ComboKey | 269 | public static string ComboKey(IEnumerable<string> items) |
| EffectOp | value | Trials | 8 | Trials |
| EffectOp | value | Hand | 8 | Hand |
| EffectOp | value | Redraws | 8 | Redraws |
| EffectOp | value | RiShift | 8 | RiShift |
| EffectOp | value | GiveDiscovery | 9 | GiveDiscovery |
| EffectOp | value | LoseRack | 9 | LoseRack |
| EffectOp | value | LoseCard | 9 | LoseCard |
| EffectOp | value | FirstTriple | 9 | FirstTriple |
| EffectOp | value | Hint | 10 | Hint |
| EffectOp | value | BlockTile | 10 | BlockTile |
| EffectOp | value | HeatTile | 10 | HeatTile |
| EffectOp | value | CatalystUpgrade | 10 | CatalystUpgrade |
| EffectOp | value | CatalystGift | 11 | CatalystGift |
| EffectOp | value | Nothing | 11 | Nothing |
| EventNeedType | value | None | 17 | None |
| EventNeedType | value | Rack | 18 | Rack |
| EventNeedType | value | Catalyst | 19 | Catalyst |
| EventNeedType | value | Trial | 20 | Trial |
| EventNeedType | value | Redraw | 21 | Redraw |
| EventType | value | Choice | 27 | Choice |
| EventType | value | Gamble | 27 | Gamble |
| EventType | value | Find | 27 | Find |
| EventType | value | Shrine | 27 | Shrine |
| EventType | value | Hazard | 27 | Hazard |
| EventType | value | Boon | 27 | Boon |
| EventType | value | Lore | 27 | Lore |
| Bench | field | Size | 9 | public const int Size = 3, TileCount = 9, Centre = 4 |
| Bench | field | Tiles | 10 | public readonly string[] Tiles = new string[TileCount] |
| Bench | property | Count | 11 | public int Count |
| Bench | method | Clear | 12 | public void Clear() |
| ReactionEvent | field | Hub | 23 | public int Hub |
| ReactionEvent | field | Tiles | 24 | public int[] Tiles |
| ReactionEvent | field | Items | 25 | public string[] Items |
| ReactionEvent | field | Result | 26 | public string Result |
| ReactionEvent | field | Recipe | 27 | public RecipeData Recipe |
| ReactionEvent | field | Discovery | 28 | public bool Discovery |
| ReactionEvent | field | FirstThisRun | 29 | public bool FirstThisRun |
| ReactionEvent | field | Sputter | 30 | public bool Sputter |
| ReactionEvent | field | Chain | 31 | public int Chain |
| ReactionEvent | field | Potency | 32 | public long Potency |
| ReactionEvent | field | Hint | 33 | public HintData Hint |
| ResolveResult | field | Events | 38 | public List<ReactionEvent> Events = new List<ReactionEvent>() |
| ResolveResult | field | Final | 39 | public string[] Final |
| ResolveResult | field | Made | 40 | public bool[] Made |
| IgniteResolver | method | Pairs | 46 | public static List<(int a, int b)> Pairs(Bench bench, bool diagonals) |
| IgniteResolver | method | Pairs | 48 | public static List<(int a, int b)> Pairs(Bench bench, BenchRules rules) |
| IgniteResolver | method | HasAnyPair | 61 | public static bool HasAnyPair(Bench bench, bool diagonals) |
| IgniteResolver | method | HasAnyPair | 62 | public static bool HasAnyPair(Bench bench, BenchRules rules) |
| IgniteResolver | method | Has | 64 | public static bool Has(IReadOnlyList<CatalystData> catalysts, string effect) |
| IgniteResolver | method | Resolve | 71 | public static ResolveResult Resolve(Bench bench, ContentDb db, Journal journal, ISet<string> firedThisRun, IReadOnlyList<CatalystData> catalysts, ScoreContext context = null) |
| IgniteResolver | method | Score | 116 | public static long Score(ContentDb db, RecipeData recipe, int[] tiles, bool firstThisRun, bool discovery, int chain, int emptyTiles, IReadOnlyList<CatalystData> catalysts) |
| IgniteResolver | method | Score | 120 | public static long Score(ContentDb db, RecipeData recipe, int[] tiles, bool firstThisRun, bool discovery, int chain, IReadOnlyList<CatalystData> catalysts, ScoreContext ctx) |
| Journal | property | KnownPairs | 13 | public IReadOnlyCollection<string> KnownPairs |
| Journal | property | Hints | 14 | public IReadOnlyList<HintData> Hints |
| Journal | method | IsKnown | 16 | public bool IsKnown(string a, string b) |
| Journal | method | Learn | 17 | public bool Learn(string a, string b) |
| Journal | method | IsKnown | 19 | public bool IsKnown(IEnumerable<string> items) |
| Journal | method | Learn | 20 | public bool Learn(IEnumerable<string> items) |
| Journal | method | IsComplete | 23 | public bool IsComplete(string material, ContentDb db) |
| Journal | method | OpenHints | 27 | public IEnumerable<HintData> OpenHints(string material, ContentDb db) |
| Journal | method | HasHint | 30 | public bool HasHint(string material, Kind partnerKind) |
| Journal | method | AddHint | 33 | public bool AddHint(string material, Kind partnerKind) |
| Journal | method | AddHeatHint | 41 | public bool AddHeatHint(string material, int heat) |
| Journal | method | ToData | 48 | public JournalData ToData() |
| Journal | method | FromData | 51 | public static Journal FromData(JournalData d) |
| Lang | field | Name | 22 | public readonly string Language, Name |
| Lang | field | Fallback | 23 | public readonly Lang Fallback |
| Lang | ctor | Lang | 27 | public Lang(LangFile file, Lang fallback = null) |
| Lang | method | FromJson | 40 | public static Lang FromJson(string json, Lang fallback = null) |
| Lang | property | Keys | 42 | public IEnumerable<string> Keys |
| Lang | property | Count | 43 | public int Count |
| Lang | method | Has | 46 | public bool Has(string key) |
| Lang | method | Template | 49 | public string Template(string key) |
| Lang | method | Get | 57 | public string Get(string key, params (string name, object value)[] args) |
| Lang | method | Content | 60 | public string Content(string key, string english, params (string name, object value)[] args) |
| Lang | method | Plural | 64 | public string Plural(string key, long n, params (string name, object value)[] args) |
| Lang | method | Fill | 79 | public static string Fill(string template, params (string name, object value)[] args) |
| Lang | method | Placeholders | 116 | public static List<string> Placeholders(string template) |
| Lang | method | Missing | 133 | public List<string> Missing(IEnumerable<string> englishKeys) |
| Lang | method | Stale | 146 | public List<string> Stale(IDictionary<string, string> english) |
| Lang | method | SourceOf | 155 | public string SourceOf(string key) |
| Lang | method | ContentEnglish | 163 | public static Dictionary<string, string> ContentEnglish(ContentData d) |
| Lang | method | EventKey | 185 | public static string EventKey(string id, string field) |
| Lang | method | OptionKey | 186 | public static string OptionKey(string id, int option) |
| Lang | method | OutcomeKey | 187 | public static string OutcomeKey(string id, int option, int outcome) |
| Lang | method | DepthKey | 188 | public static string DepthKey(int depth) |
| Lang | method | Parse | 193 | public static LangFile Parse(string json) |
| Json | ctor | Json | 215 | public Json(string text) |
| Json | method | Value | 217 | public object Value() |
| LinkPair | field | a | 9 | public int a |
| LinkPair | field | b | 10 | public int b |
| LinkPair | ctor | LinkPair | 12 | public LinkPair(int a, int b) |
| LinkPair | method | Equals | 14 | public bool Equals(LinkPair other) |
| LinkPair | method | Equals | 16 | public override bool Equals(object obj) |
| LinkPair | method | GetHashCode | 18 | public override int GetHashCode() |
| LinkPair | property | operator | 20 | public static bool operator ==(LinkPair left, LinkPair right) |
| LinkPair | property | public static bool operator ! | 21 | public static bool operator !=(LinkPair left, LinkPair right) |
| LinkPair | method | Deconstruct | 24 | public void Deconstruct(out int a, out int b) |
| GoalReward | field | Goal | 10 | public GoalData Goal |
| GoalReward | field | Item | 11 | public string Item |
| Profile | field | ComboLargest | 20 | public int RunsStarted, RunsWon, WonNoRedraw, DepthReached, Ignites, ChainLongest, ComboLargest |
| Profile | field | BestIgnite | 21 | public long BestIgnite |
| Profile | field | RewardSeed | 24 | public int RewardSeed |
| Profile | field | LastLore | 26 | public string LastLore |
| Profile | ctor | Profile | 31 | public Profile(int rewardSeed = 1) |
| Profile | property | Completed | 33 | public IReadOnlyCollection<string> Completed |
| Profile | property | UnlockedStarters | 34 | public IReadOnlyCollection<string> UnlockedStarters |
| Profile | property | UnlockedCatalysts | 35 | public IReadOnlyCollection<string> UnlockedCatalysts |
| Profile | property | Rewards | 36 | public IReadOnlyList<RewardLogData> Rewards |
| Profile | method | IsComplete | 37 | public bool IsComplete(string goalId) |
| Profile | method | HasUnlocked | 38 | public bool HasUnlocked(string id) |
| Profile | method | RewardOf | 40 | public string RewardOf(string goalId) |
| Profile | method | RunStarted | 44 | public void RunStarted() |
| Profile | method | EventShown | 47 | public void EventShown(EventData e) |
| Profile | method | DepthBegan | 49 | public void DepthBegan(int depth) |
| Profile | method | IgniteResolved | 51 | public void IgniteResolved(IgniteOutcome outcome) |
| Profile | method | RunEnded | 64 | public void RunEnded(RunState run) |
| Profile | method | Stat | 74 | public long Stat(string id, ContentDb db, Journal journal) |
| Profile | method | Progress | 93 | public long Progress(GoalData goal, ContentDb db, Journal journal) |
| Profile | method | Evaluate | 100 | public List<GoalReward> Evaluate(ContentDb db, Journal journal) |
| Profile | method | ToData | 122 | public ProfileData ToData() |
| Profile | method | FromData | 131 | public static Profile FromData(ProfileData d, ContentDb db = null, int freshSeed = 1) |
| Unlocks | method | IsOpen | 159 | public bool IsOpen(string id) |
| Unlocks | property | Count | 160 | public int Count |
| Unlocks | method | Fresh | 163 | public static Unlocks Fresh(ContentDb db) |
| Unlocks | method | All | 171 | public static Unlocks All(ContentDb db) |
| Unlocks | method | For | 180 | public static Unlocks For(ContentDb db, Profile profile) |
| Unlocks | method | Half | 190 | public static Unlocks Half(ContentDb db, int seed) |
| RunOptions | field | Unlocks | 208 | public Unlocks Unlocks |
| RunOptions | field | Events | 209 | public bool Aspects, Events |
| RunOptions | field | ForceEvent | 211 | public string ForceEvent |
| RunOptions | field | ForceEventAt | 213 | public int ForceEventAt |
| RunOptions | field | QuietBefore | 215 | public int QuietBefore |
| RunOptions | field | FirstOffer | 217 | public string[] FirstOffer |
| RunOptions | field | LastLore | 219 | public string LastLore |
| RunOptions | field | PouchMode | 221 | public int PouchMode = 2 |
| RunOptions | field | Legacy | 222 | public static readonly RunOptions Legacy = new RunOptions() |
| RunOptions | method | Full | 223 | public static RunOptions Full(Unlocks unlocks) |
| AspectInstance | field | Data | 10 | public AspectData Data |
| AspectInstance | field | Tiles | 11 | public int[] Tiles = new int[0] |
| AspectInstance | field | Kind | 12 | public string Kind |
| EventResult | field | Text | 18 | public string Text |
| EventResult | field | Notes | 19 | public readonly List<EventNote> Notes = new List<EventNote>() |
| EventNote | field | key | 26 | public string key |
| EventNote | field | n | 27 | public int n |
| EventNote | field | kind | 28 | public string item, other, kind |
| EventNote | field | plural | 29 | public bool plural |
| RunState | property | LastLore | 48 | public string LastLore |
| RunState | property | Aspects | 50 | public IReadOnlyList<AspectInstance> Aspects |
| RunState | property | AspectOffer | 51 | public IReadOnlyList<AspectInstance> AspectOffer |
| RunState | property | CurrentEvent | 52 | public EventData CurrentEvent |
| RunState | property | EventOptions | 54 | public IReadOnlyList<EventOption> EventOptions |
| RunState | property | Rules | 55 | public BenchRules Rules |
| RunState | property | RiShift | 57 | public int RiShift |
| RunState | property | TripleFirst | 58 | public bool TripleFirst |
| RunState | property | HandDelta | 60 | public int HandDelta |
| RunState | method | ChooseAspect | 146 | public void ChooseAspect(string id) |
| RunState | method | ChooseEventOption | 228 | public EventResult ChooseEventOption(int index) |
| RunState | property | DepthBlocked | 410 | public IReadOnlyList<int> DepthBlocked |
| RunState | property | DepthHeated | 411 | public IEnumerable<int> DepthHeated |
| RunState | ctor | RunState | 34 | public RunState(ContentDb db, Journal journal, int seed, int startDepth = 1, RunOptions options = null) |
| RunState | property | Depth | 51 | public int Depth |
| RunState | property | Potency | 52 | public long Potency |
| RunState | property | Target | 53 | public int Target |
| RunState | property | TrialsLeft | 54 | public int TrialsLeft |
| RunState | property | RedrawsLeft | 55 | public int RedrawsLeft |
| RunState | property | DiscoveriesThisRun | 56 | public int DiscoveriesThisRun |
| RunState | property | Phase | 57 | public RunPhase Phase |
| RunState | property | Bench | 58 | public Bench Bench |
| RunState | property | Hand | 59 | public IReadOnlyList<string> Hand |
| RunState | property | Catalysts | 60 | public IReadOnlyList<CatalystData> Catalysts |
| RunState | property | CatalystOffer | 61 | public IReadOnlyList<CatalystData> CatalystOffer |
| RunState | property | HandSize | 62 | public int HandSize |
| RunState | property | Diagonals | 63 | public bool Diagonals |
| RunState | property | CanIgnite | 64 | public bool CanIgnite |
| RunState | property | RedrawsUsed | 66 | public int RedrawsUsed |
| RunState | property | Options | 67 | public RunOptions Options |
| RunState | method | IsOpen | 68 | public bool IsOpen(string id) |
| RunState | method | Owned | 72 | public int Owned(string id) |
| RunState | method | HasFired | 78 | public bool HasFired(string a, string b) |
| RunState | method | HasFired | 79 | public bool HasFired(RecipeData recipe) |
| RunState | property | SatchelSize | 80 | public int SatchelSize |
| RunState | method | Place | 137 | public bool Place(int handIndex, int tile) |
| RunState | method | Unplace | 146 | public bool Unplace(int tile) |
| RunState | method | Redraw | 155 | public bool Redraw() |
| RunState | method | Deal | 171 | public bool Deal(IReadOnlyList<string> ids) |
| RunState | method | NewContext | 184 | public ScoreContext NewContext() |
| RunState | method | Preview | 190 | public List<ReactionEvent> Preview(string[] tiles) |
| RunState | method | Ignite | 198 | public IgniteOutcome Ignite() |
| RunState | method | ChooseCatalyst | 279 | public void ChooseCatalyst(string id) |
| IgniteOutcome | field | Events | 11 | public List<ReactionEvent> Events |
| IgniteOutcome | field | Gained | 12 | public long Gained |
| IgniteOutcome | field | Dropped | 13 | public List<string> Dropped = new List<string>() |
| IgniteOutcome | field | Final | 14 | public string[] Final |
| IgniteOutcome | field | Made | 15 | public bool[] Made |
| IgniteOutcome | field | PhaseAfter | 16 | public RunPhase PhaseAfter |
| RunPhase | value | Playing | 7 | Playing |
| RunPhase | value | DepthCleared | 7 | DepthCleared |
| RunPhase | value | Lost | 7 | Lost |
| RunPhase | value | Won | 7 | Won |
| RunPhase | value | AspectOffer | 7 | AspectOffer |
| RunPhase | value | EventOffer | 7 | EventOffer |
| Rng | ctor | Rng | 10 | public Rng(int seed) |
| Rng | method | Next | 11 | public uint Next() |
| Rng | method | Range | 12 | public int Range(int n) |
| Rng | method | Shuffle | 13 | public void Shuffle<T>(IList<T> list) |
| Satchel | ctor | Satchel | 27 | public Satchel(IEnumerable<string> cards, int seed) |
| Satchel | property | Total | 34 | public int Total |
| Satchel | method | Draw | 36 | public List<string> Draw(int n) |
| Satchel | method | Lose | 53 | public void Lose(string id) |
| Satchel | method | Discard | 55 | public void Discard(IEnumerable<string> ids) |
| Satchel | method | Available | 61 | public int Available(string id) |
| Satchel | method | Take | 67 | public bool Take(string id) |
| ScoreParts | field | Total | 9 | public long Base, Total |
| ScoreParts | field | Step | 10 | public int Used, First, Step |
| ScoreParts | field | Bonus | 11 | public double Bonus |
| ScoreParts | method | Of | 14 | public static ScoreParts Of(ContentDb db, ReactionEvent e) |
| ScreenFlow | property | State | 14 | public FlowState State |
| ScreenFlow | property | JournalFrom | 17 | public FlowState JournalFrom |
| ScreenFlow | property | InCutscene | 20 | public bool InCutscene |
| ScreenFlow | property | Sandbox | 23 | public bool Sandbox |
| ScreenFlow | field | Changed | 26 | public event Action<FlowState, FlowState> Changed |
| ScreenFlow | method | Enter | 29 | public void Enter(FlowState state) |
| ScreenFlow | method | OpenJournal | 38 | public bool OpenJournal(bool titleAsking) |
| ScreenFlow | method | CloseJournal | 50 | public bool CloseJournal() |
| ScreenFlow | method | OpenGoals | 58 | public bool OpenGoals(bool titleAsking) |
| ScreenFlow | method | CloseGoals | 70 | public bool CloseGoals() |
| ScreenFlow | method | StartIgnite | 78 | public bool StartIgnite(bool canIgnite) |
| ScreenFlow | method | CanPause | 86 | public bool CanPause(bool benchInput, bool runPlaying) |
| ScreenFlow | method | CanAbandon | 90 | public bool CanAbandon(bool runEnded) |
| ScreenFlow | property | CanResetProgress | 93 | public bool CanResetProgress |
| ScreenFlow | method | BeginSandbox | 96 | public void BeginSandbox() |
| ScreenFlow | method | EndSandbox | 99 | public void EndSandbox() |
| FlowState | value | Boot | 6 | Boot |
| FlowState | value | Title | 6 | Title |
| FlowState | value | Intro | 6 | Intro |
| FlowState | value | Bench | 6 | Bench |
| FlowState | value | Resolving | 6 | Resolving |
| FlowState | value | Journal | 6 | Journal |
| FlowState | value | Descent | 6 | Descent |
| FlowState | value | CatalystChoice | 6 | CatalystChoice |
| FlowState | value | Ending | 6 | Ending |
| FlowState | value | RunOver | 6 | RunOver |
| FlowState | value | AspectChoice | 6 | AspectChoice |
| FlowState | value | Event | 6 | Event |
| FlowState | value | Goals | 6 | Goals |
| Settings | field | MaxVolume | 14 | public const int MaxVolume = 10 |
| Settings | field | version | 17 | public int version = 1 |
| Settings | field | music | 19 | public int music = 7 |
| Settings | field | sfx | 21 | public int sfx = 8 |
| Settings | field | haptics | 22 | public bool haptics = true |
| Settings | field | sway | 24 | public bool sway = true |
| Settings | field | language | 26 | public string language = |
| Settings | method | Clamp | 29 | public Settings Clamp() |
| Settings | method | ToJson | 38 | public string ToJson() |
| Settings | method | FromJson | 52 | public static Settings FromJson(string json) |
| Reader | ctor | Reader | 65 | public Reader(string text) |
| Reader | method | ReadObject | 70 | public bool ReadObject(Settings s) |
| TutorialLay | field | Id | 15 | public readonly string Id |
| TutorialLay | field | Tile | 16 | public readonly int Tile |
| TutorialLay | ctor | TutorialLay | 17 | public TutorialLay(string id, int tile) |
| TutorialTrial | field | Hand | 23 | public string[] Hand |
| TutorialTrial | field | Lay | 24 | public TutorialLay[] Lay |
| TutorialTrial | field | Makes | 25 | public string[] Makes |
| TutorialScript | field | Seed | 34 | public const int Seed = 20261113 |
| TutorialScript | field | QuietBefore | 36 | public const int QuietBefore = 3 |
| TutorialScript | property | Offer | 37 | public static readonly string[] Offer = |
| TutorialScript | property | Reaction | 40 | public static readonly TutorialTrial Reaction = new TutorialTrial |
| TutorialScript | property | Miss | 48 | public static readonly TutorialTrial Miss = new TutorialTrial |
| TutorialScript | property | Chain | 56 | public static readonly TutorialTrial Chain = new TutorialTrial |
| TutorialScript | property | Depth1 | 63 | public static readonly TutorialTrial[] Depth1 = |
| TutorialScript | method | Layout | 66 | public static string[] Layout(TutorialTrial trial) |
| TutorialScript | method | BrokenChain | 69 | public static string[] BrokenChain() |
| TutorialScript | method | CatalystTrial | 79 | public static TutorialTrial CatalystTrial(string catalystId) |
| TutorialScript | method | Options | 106 | public static RunOptions Options(Unlocks unlocks) |
| TutorialScript | method | ShouldRun | 112 | public static bool ShouldRun(bool tutorialDone) |
| TutorialScript | method | Plan | 118 | public static TutorialPlan Plan(bool tutorialDone, int journalEntries) |
| TutorialPlan | value | None | 6 | None |
| TutorialPlan | value | Run | 6 | Run |
| TutorialPlan | value | AskSkip | 6 | AskSkip |
| BenchView | field | JournalRequested | 19 | public event Action IgniteRequested, RedrawRequested, JournalRequested |
| BenchView | property | InputEnabled | 20 | public bool InputEnabled |
| BenchView | property | Hud | 21 | public HudView Hud |
| BenchView | property | TileCount | 23 | public int TileCount |
| BenchView | property | Timing | 49 | public struct Timing |
| BenchView | property | Pick | 53 | struct Pick |
| BenchView | method | Bind | 89 | public void Bind(ContentDb content, FxRefs fx) |
| BenchView | method | Bind | 103 | public void Bind(RunState state, Journal book) |
| BenchView | method | TileCentre | 111 | public Vector3 TileCentre(int i) |
| BenchView | method | RackSlot | 113 | public Vector3 RackSlot(int i) |
| BenchView | method | PreviewOnRack | 115 | public void PreviewOnRack(int i, string id) |
| BenchView | method | Tile | 120 | public TileView Tile(int i) |
| BenchView | method | Refresh | 123 | public void Refresh(bool deal = false) |
| BenchView | method | ClearLinks | 204 | public void ClearLinks() |
| BenchView | method | FloatText | 207 | public IEnumerator FloatText(string text, Vector3 from, Color color, float seconds, bool small = false) |
| BenchView | method | PlaceLine | 257 | public static string PlaceLine(int tile) |
| BenchView | method | UnplaceLine | 258 | public static string UnplaceLine(int tile) |
| BenchView | method | Tick | 289 | public void Tick(float deltaTime) |
| BenchView.Layout | field | joinLift | 33 | public Vector3 joinLift |
| BenchView.Layout | field | floatLiftTo | 35 | public Vector3 floatLiftFrom, floatLiftTo |
| BenchView.Layout | field | floatShadow | 37 | public Color floatShadow |
| BenchView.Layout | field | ghostLift | 39 | public float ghostLift |
| BenchView.Layout | field | cardTipLift | 41 | public Vector3 tileTipLift, cardTipLift |
| BenchView.Layout | field | handMaxPitch | 43 | public float handX, handW, cardY, handMaxPitch |
| BenchView.Layout | field | tileHitAbove | 45 | public float slotHit, tileHitHalfWidth, tileHitBelow, tileHitAbove |
| TipKey | field | kind | 78 | public TipKind kind |
| TipKey | method | Equals | 79 | public bool Equals(TipKey o) |
| From | value | None | 52 | None |
| From | value | Hand | 52 | Hand |
| From | value | Tile | 52 | Tile |
| TipKind | value | None | 75 | None |
| TipKind | value | Catalyst | 75 | Catalyst |
| TipKind | value | Aspect | 75 | Aspect |
| TipKind | value | Pause | 75 | Pause |
| TipKind | value | Redraw | 75 | Redraw |
| TipKind | value | Material | 75 | Material |
| TipKind | value | TileNote | 75 | TileNote |
| CardView | field | W | 9 | public const int W = 74, H = 70 |
| CardView | field | handIndex | 10 | public int handIndex |
| CardView | field | id | 11 | public string id |
| CardView | field | home | 12 | public Vector3 home |
| CardView | field | Dimmed | 13 | public bool Lifted, Selected, Dimmed |
| CardView | method | Show | 42 | public void Show(MaterialData m, int order) |
| CardView | method | Fan | 64 | public void Fan(float visible, int front) |
| CardView | method | DealIn | 89 | public void DealIn(float wait) |
| CardView | method | Contains | 91 | public bool Contains(Vector2 world) |
| CardView | method | Tick | 100 | public void Tick(float deltaTime) |
| CardView.Layout | field | nameTwoLines | 25 | public float nameOneLine, nameTwoLines |
| CardView.Layout | field | pictureSlideMax | 27 | public float pictureSlideMax |
| CardView.Layout | field | dealDrop | 29 | public float dealDrop |
| CardView.Layout | field | bobSpeed | 31 | public float selectedLift, liftedLift, liftSpeed, bobAmp, bobSpeed |
| Dust | field | count | 11 | public int count = 46 |
| Dust | field | material | 13 | public Material material |
| Dust | field | sprite | 14 | public Sprite sprite |
| Dust | field | tint | 16 | public Color tint |
| Dust | field | alphaSwing | 17 | public float alphaBase, alphaSwing |
| HudView | field | PauseClicked | 10 | public event Action IgniteClicked, JournalClicked, RedrawClicked, PauseClicked |
| HudView | property | HoveredCatalyst | 12 | public int HoveredCatalyst |
| HudView | property | RedrawHovered | 13 | public bool RedrawHovered |
| HudView | property | PauseHovered | 15 | public bool PauseHovered |
| HudView | field | PauseOrder | 17 | public const int PauseOrder = 650 |
| HudView | property | HoveredAspect | 23 | public int HoveredAspect |
| HudView | field | MaxCatalysts | 25 | public const int MaxCatalysts = 6, MaxAspects = 3 |
| HudView | method | SetPauseShown | 99 | public void SetPauseShown(bool shown) |
| HudView | property | PauseShown | 105 | public bool PauseShown |
| HudView | property | PauseAnchor | 106 | public Vector3 PauseAnchor |
| HudView | property | RedrawRect | 109 | public Rect RedrawRect |
| HudView | property | PauseRect | 111 | public Rect PauseRect |
| HudView | property | PotencyPosition | 112 | public Vector3 PotencyPosition |
| HudView | method | CatalystAnchor | 114 | public Vector3 CatalystAnchor(int i) |
| HudView | method | AspectAnchor | 115 | public Vector3 AspectAnchor(int i) |
| HudView | property | RedrawAnchor | 117 | public Vector3 RedrawAnchor |
| HudView | method | CanRedraw | 119 | public static bool CanRedraw(RunState run) |
| HudView | method | Number | 120 | public static string Number(long n) |
| HudView | method | Bind | 125 | public void Bind() |
| HudView | method | Show | 165 | public void Show(RunState run, ContentDb db, Journal journal, bool inputEnabled, bool keepRedraw = false) |
| HudView | method | SetPotency | 213 | public void SetPotency(long value, bool instant = false) |
| HudView | method | Note | 216 | public void Note(ReactionEvent e, ContentDb db) |
| HudView | method | Hint | 240 | public void Hint(HintData h, ContentDb db) |
| HudView | method | HintLine | 247 | public static string HintLine(HintData h, ContentDb db, bool withName = true) |
| HudView | method | Bump | 255 | public void Bump() |
| HudView | method | ClearNote | 257 | public void ClearNote() |
| HudView | method | Tick | 266 | public void Tick(float deltaTime) |
| HudView.Layout | field | rowVi | 32 | public int rowEn, rowVi |
| HudView.Layout | field | noteCharsVi | 34 | public int noteCharsEn, noteCharsVi |
| HudView.Layout | field | redrawRect | 36 | public Rect redrawRect |
| HudView.Layout | field | pauseRect | 38 | public Rect pauseRect |
| HudView.Layout | field | redrawLabelY | 40 | public float redrawLabelY |
| HudView.Layout | field | pressedDy | 42 | public float pressedDy |
| HudView.Layout | field | catalystSpread | 44 | public float catalystPitch, catalystSpread |
| HudView.Layout | field | catalystSideBySide | 45 | public int catalystSideBySide |
| HudView.Layout | field | iconSize | 46 | public Vector2 socketOrigin, socketSize, iconOffset, iconSize |
| HudView.Layout | field | potencyAnchor | 48 | public Vector2 aspectAnchor, potencyAnchor |
| HudView.Layout | field | aspectPitch | 49 | public float aspectPitch |
| HudView.Layout | field | noteTop | 51 | public float noteX, noteTopAspects, noteTop |
| HudView.Layout | field | potencyY | 53 | public float potencyX, potencyTightX, potencyY |
| HudView.Layout | field | barOrigin | 54 | public Vector2 barOrigin |
| HudView.Layout | field | barHeight | 55 | public float barWidth, barHeight |
| HudView.Layout | field | tightTo | 57 | public float tightFrom, tightGap, tightTo |
| HudView.Layout | field | pulseColor | 59 | public Color pulseColor |
| HudView.Layout | field | pulseAlpha | 60 | public Vector2 pulseAlpha |
| HudView.Layout | field | barTipScale | 62 | public Vector3 barTipScale |
| LinkView | field | color | 11 | public Color color |
| LinkView | field | speed | 12 | public float speed = 0.35f, arc = 7, alpha = 0.9f |
| LinkView | field | moteScale | 14 | public Vector2 moteScale |
| LinkView | field | lift | 15 | public float lift |
| LinkView | method | Set | 19 | public void Set(List<(Vector3 a, Vector3 b)> pairs) |
| TileView | field | index | 16 | public int index |
| TileView | method | Bind | 19 | public void Bind(FxRefs effects) |
| TileView | field | centre | 25 | public Vector3 centre |
| TileView | property | Material | 55 | public string Material |
| TileView | property | Hovered | 56 | public bool Hovered |
| TileView | property | Pinged | 58 | public bool Pinged |
| TileView | property | Shut | 60 | public bool Shut |
| TileView | property | Marked | 61 | public bool Marked |
| TileView | property | Icon | 62 | public SpriteRenderer Icon |
| TileView | property | FaceCentre | 65 | public Vector2 FaceCentre |
| TileView | property | FaceSize | 66 | public Vector2 FaceSize |
| TileView | method | Contains | 68 | public bool Contains(Vector2 world) |
| TileView | method | SetState | 72 | public void SetState(Color? marker, bool shut) |
| TileView | method | Show | 82 | public void Show(MaterialData m, bool pop) |
| TileView | method | Clear | 96 | public void Clear() |
| TileView | method | Flash | 104 | public void Flash(Color c) |
| TileView | method | Consume | 107 | public IEnumerator Consume() |
| TileView | method | Tick | 124 | public void Tick(float deltaTime) |
| TileView.Layout | field | shutLid | 36 | public Color shutLid |
| TileView.Layout | field | hoverFace | 38 | public Color hoverFace |
| TileView.Layout | field | consumeLift | 40 | public Vector3 consumeLift |
| TileView.Layout | field | iconLift | 41 | public float iconLift |
| TileView.Layout | field | popRise | 43 | public float popRise |
| TileView.Layout | field | consumeBurst | 45 | public BurstSpec consumeBurst |
| TileView.Layout | field | consumeTint | 46 | public float consumeTint |
| TileView.Layout | field | consumeGrow | 48 | public float consumeSeconds, consumeGrow |
| GameFlow | field | library | 16 | public ArtLibrary library |
| GameFlow | field | cutsceneRoom | 23 | public GameObject workshop, cutsceneRoom |
| GameFlow | field | mainCamera | 24 | public Camera mainCamera |
| GameFlow | field | depthStage | 26 | public DepthStage depthStage |
| GameFlow | property | Db | 28 | public ContentDb Db |
| GameFlow | property | Journal | 29 | public Journal Journal |
| GameFlow | property | Profile | 31 | public Profile Profile |
| GameFlow | property | Run | 32 | public RunState Run |
| GameFlow | property | Bench | 34 | public BenchView Bench |
| GameFlow | property | State | 36 | public FlowState State |
| GameFlow | property | Flow | 38 | public ScreenFlow Flow |
| GameFlow | property | RunOver | 42 | public RunOverView RunOver |
| GameFlow | field | Auto | 44 | public bool Auto |
| GameFlow | property | SeedOverride | 48 | public int SeedOverride |
| GameFlow | field | AutoHook | 51 | public const string AutoHook = |
| GameFlow | method | ToggleAuto | 54 | public void ToggleAuto() |
| GameFlow | method | NextMood | 63 | public static int NextMood(int mood) |
| GameFlow | method | MoodLine | 64 | public static string MoodLine(int mood) |
| GameFlow | method | EffectsLine | 65 | public static string EffectsLine(bool on) |
| GameFlow | property | JournalView | 135 | public JournalView JournalView |
| GameFlow | method | RequestJournal | 254 | public void RequestJournal() |
| GameFlow | method | JournalAfterQuit | 478 | public static Journal JournalAfterQuit(bool sandbox, Journal current) |
| GameFlow | method | ProfileAfterQuit | 480 | public static Profile ProfileAfterQuit(bool sandbox, Profile current, ContentDb db) |
| GameFlow | method | Ignite | 517 | public void Ignite() |
| GameFlow | method | SettleIgnite | 524 | public static IgniteOutcome SettleIgnite(RunState run, Journal journal, bool sandbox) |
| RunTiming | field | endingWait | 94 | public float descendWait, endingWait |
| RunTiming | field | lossWait | 96 | public float lossShake, lossShakeSeconds, lossWait |
| RunTiming | field | runFadeOut | 98 | public float titleFadeIn, titleFadeOut, runFadeIn, runFadeOut |
| RunTiming | field | autoPace | 100 | public float autoChoiceWait, autoPace |
| TitleChoice | value | Begin | 148 | Begin |
| TitleChoice | value | HowToPlay | 148 | HowToPlay |
| TitleChoice | value | Intro | 148 | Intro |
| IgniteSequence | property | Hurry | 99 | public bool Hurry |
| IgniteSequence | method | Bind | 102 | public void Bind(BenchView bench, ReactionCutscene reaction, FxRefs fx, ScreenFlow flow) |
| IgniteSequence | method | Play | 111 | public IEnumerator Play(IgniteOutcome outcome, RunState run, ContentDb db, long potencyBefore, TutorialController tutorial, bool auto) |
| IgniteSequence.Timing | field | paceDecay | 25 | public float paceHurry, paceFloor, paceDecay |
| IgniteSequence.Timing | field | endPace | 27 | public float endPaceHurry, endPace |
| IgniteSequence.Timing | field | discoverHurryStep | 29 | public float discoverHurryBase, discoverHurryStep |
| IgniteSequence.Timing | field | discoverHurryCap | 30 | public int discoverHurryCap |
| IgniteSequence.Timing | field | discoverBeat | 32 | public float discoverBeat |
| IgniteSequence.Timing | field | fizzleHintText | 34 | public float fizzleHintWait, fizzleWait, fizzleHintText |
| IgniteSequence.Timing | field | sputterText | 36 | public float sputterWait, sputterText |
| IgniteSequence.Timing | field | reactFlash | 38 | public float drawSeconds, drawWait, reactWait, pointsSeconds, reactShake, reactFlash |
| IgniteSequence.Timing | field | comboText | 40 | public float pointsText, pointsTextPace, comboText |
| IgniteSequence.Timing | field | discoverShake | 42 | public float discoverShake |
| IgniteSequence.Timing | field | rackStagger | 44 | public float rackPop, rackFly, rackHold, rackStagger |
| IgniteSequence.Timing | field | rackWaitNone | 46 | public float rackWaitKept, rackWaitNone |
| IgniteSequence.Timing | field | lostText | 48 | public float lostText |
| IgniteSequence.Timing | field | rackTrailChance | 50 | public float rackTrailChance |
| IgniteSequence.Look | field | rackLand | 57 | public BurstSpec fizzleBurst, sputterBurst, discoverBurst, reactBurst, pointsLand, rackLand |
| IgniteSequence.Look | field | sputterFlash | 59 | public Color fizzleFlash, sputterFlash |
| IgniteSequence.Look | field | pointsTrailTint | 61 | public Color drawTint, rackTrailTint, pointsTint, pointsTrailTint |
| IgniteSequence.Look | field | defaultGlow | 63 | public Color defaultGlow |
| IgniteSequence.Look | field | lostText | 65 | public Color pointsText, comboText, chainText, hintText, spoiledText, lostText |
| IgniteSequence.Look | field | rackUp | 67 | public Vector3 hubLift, hintTextLift, spoilTileLift, spoilTextLift, drawLift, pointsTextLift, comboTextLift, rackLift, rackUp |
| IgniteSequence.Look | field | lostAt | 69 | public Vector2 lostAt |
| IgniteSequence.Look | field | burstPerExtra | 71 | public int burstPerChain, burstChainCap, burstPerExtra |
| IgniteSequence.Look | field | speedPerChain | 72 | public float speedPerChain |
| IgniteSequence.Look | field | discoverShake | 74 | public float shakeBase, shakePerChain, shakePerExtra, shakeMax, discoverShake |
| IgniteSequence.Look | field | flashSize | 76 | public int flashChain, flashSize |
| IgniteSequence.Look | field | flashMax | 77 | public float flashPerStep, flashMax |
| IgniteSequence.Look | field | rackLife | 79 | public float drawJitter, drawLife, pointsJitter, pointsJitter2, pointsLife, pointsLife2, pointsHump, rackJitter, rackLife |
| IgniteSequence.Look | field | rackTrailOrder | 80 | public int drawOrder, pointsOrder, pointsOrder2, rackTrailOrder |
| IgniteSequence.Look | field | arcHeight | 82 | public float haloSize, haloAlpha, popScale, arcHeight |
| IgniteSequence.Look | field | iconOrder | 83 | public int haloOrder, iconOrder |
| Save | field | Key | 10 | public const string Key = "athanor.journal.v1", IntroKey = "athanor.intro.v1", ProfileKey = "athanor.profile.v1", TutorialKey = "athanor.tutorial.v1", SettingsKey = |
| Save | method | LoadJournal | 14 | public static Journal LoadJournal() |
| Save | method | StoreJournal | 24 | public static void StoreJournal(Journal journal) |
| Save | method | LoadProfile | 34 | public static Profile LoadProfile(ContentDb db) |
| Save | method | StoreProfile | 45 | public static void StoreProfile(Profile profile) |
| Save | method | ResetProfile | 52 | public static void ResetProfile() |
| Save | method | LoadSettings | 59 | public static Settings LoadSettings() |
| Save | method | StoreSettings | 64 | public static void StoreSettings(Settings settings) |
| Save | property | IntroSeen | 76 | public static bool IntroSeen |
| Save | method | SetIntroSeen | 77 | public static void SetIntroSeen(bool v) |
| Save | property | TutorialDone | 80 | public static bool TutorialDone |
| Save | method | SetTutorialDone | 81 | public static void SetTutorialDone(bool v) |
| Cutscene | property | Skipped | 9 | protected bool Skipped |
| Cutscene | property | Playing | 10 | protected bool Playing |
| Cutscene | method | Play | 12 | public abstract IEnumerator Play() |
| Cutscene | method | RequestSkip | 14 | public void RequestSkip() |
| Cutscene | method | Begin | 17 | protected void Begin() |
| Cutscene | method | ClearSkip | 18 | protected void ClearSkip() |
| Cutscene | method | End | 19 | protected void End() |
| Cutscene | method | OnEnable | 21 | protected virtual void OnEnable() |
| Cutscene | method | OnDisable | 22 | protected virtual void OnDisable() |
| DepthLook | field | Authored | 12 | public const int Authored = 5 |
| DepthLook | method | IntroKey | 15 | public static string IntroKey(int depth) |
| DepthLook | method | View | 18 | public static int View(int depth) |
| DepthLook | method | ShowsRunIntro | 24 | public static bool ShowsRunIntro(int depth, bool tutorial, bool sandbox) |
| DepthLook | method | ThemedWindow | 27 | public static bool ThemedWindow(bool inRun, bool tutorial, bool sandbox) |
| DepthLook | method | WindowSprite | 30 | public static string WindowSprite(int depth) |
| ReactionCutscene | property | Opening | 25 | public struct Opening |
| ReactionCutscene | property | Gathering | 29 | public struct Gathering |
| ReactionCutscene | property | Bursting | 33 | public struct Bursting |
| ReactionCutscene | property | Revealing | 37 | public struct Revealing |
| ReactionCutscene | property | Tallying | 41 | public struct Tallying |
| ReactionCutscene | property | Closing | 45 | public struct Closing |
| ReactionCutscene | method | Bind | 139 | public void Bind(GameObject[] others, FxRefs fx) |
| ReactionCutscene | method | Setup | 154 | public void Setup(ReactionEvent e, ContentDb content, float hurry = 1) |
| ReactionCutscene | method | Play | 180 | public override IEnumerator Play() |
| Brew | field | brewGain | 52 | public Color runeStart, brewStart, brewGain |
| Brew | field | runeLight | 54 | public float runeBright, runePulseBase, runePulseAmplitude, runePulseSpeed, runeLight |
| Brew | field | lightAfter | 56 | public float frameRate, brewBright, alphaBase, alphaAfter, alphaGather, glowBase, glowAfter, glowGain, lightGain, lightBase, lightAfter |
| Brew | field | matGlow | 58 | public float matLight, matGlow |
| Prize | field | pillarPulseSpeed | 66 | public float glowAlpha, lightStrength, pillarBright, pillarAlpha, pillarPulseBase, pillarPulseAmplitude, pillarPulseSpeed |
| Prize | field | bobAmplitude | 68 | public float bobSpeed, bobAmplitude |
| Prize | field | shortRay | 70 | public float raySpin, shortRay |
| Prize | field | rayColor | 71 | public Color rayColor |
| Prize | field | ringAlpha | 73 | public float ringSpeed, ringWidth, ringGrowW, ringHeight, ringGrowH, ringBright, ringAlpha |
| ReactionCutscene.Layout | field | rimY | 81 | public float rimY |
| ReactionCutscene.Layout | field | resultRise | 83 | public float resultLift, resultRise |
| ReactionCutscene.Layout | field | spiralDrop | 85 | public float spiralRadius, spiralPower, spiralMin, spiralSpin, spiralFlat, spiralY, spiralDrop |
| ReactionCutscene.Layout | field | wordSlide | 87 | public float bannerY, titleY, wordSlide |
| ReactionCutscene.Layout | field | plusShift | 89 | public float plusY, plusShift |
| Sparkle | field | trailTwinkle | 97 | public float trailJitter, trailSpeed, trailLife, trailBright, trailTwinkle |
| Sparkle | field | bright | 99 | public float rate, spread, speedX, speedYMin, speedYMax, life, gravity, bright |
| Sparkle | field | white | 100 | public Color white |
| SimpleCutscene | property | DescentLeave | 49 | public struct DescentLeave |
| SimpleCutscene | property | DescentName | 53 | public struct DescentName |
| SimpleCutscene | property | DescentArrive | 57 | public struct DescentArrive |
| SimpleCutscene | property | DepthLine | 61 | public struct DepthLine |
| SimpleCutscene | method | Bind | 126 | public void Bind(FxRefs effects) |
| SimpleCutscene | method | Play | 133 | public override IEnumerator Play() |
| SimpleCutscene | method | Close | 150 | public void Close() |
| SimpleCutscene | method | Intro | 169 | public IEnumerator Intro() |
| SimpleCutscene | method | Descent | 205 | public IEnumerator Descent(int depth, string depthName, Color colour, Action midway) |
| SimpleCutscene | method | DepthIntro | 265 | public IEnumerator DepthIntro(string key) |
| SimpleCutscene | method | Ending | 281 | public IEnumerator Ending(MaterialData ultimate) |
| IntroPage | field | fadeOut | 28 | public float fadeIn, lineFade, fadeOut |
| IntroPage | field | finalHold | 30 | public float hold, lastHold, finalHold |
| IntroPage | field | promptHold | 32 | public float promptHold |
| DescentBanner | field | shakeSeconds | 39 | public float flash, flashSeconds, shake, shakeSeconds |
| DescentBanner | field | hold | 41 | public float rise, fromY, toY, hold |
| DescentBanner | field | burst | 43 | public BurstSpec burst |
| DescentBanner | field | burstSquash | 44 | public float burstY, burstSquash |
| EndingBeat | field | promptHold | 67 | public float fadeOut, fadeIn, hold, flash, flashSeconds, promptHold |
| EndingBeat | field | burst | 69 | public BurstSpec burst |
| EndingBeat | field | bannerY | 70 | public float burstY, bannerY |
| SimpleCutscene.Look | field | lightIntensity | 78 | public float prizeY, bobSpeed, bobAmplitude, glowAlpha, lightIntensity |
| SimpleCutscene.Look | field | shortRay | 80 | public float rayStep, raySpin, shortRay |
| SimpleCutscene.Look | field | rayColor | 81 | public Color rayColor |
| SimpleCutscene.Look | field | runeGlow | 83 | public float runeBright, pulseBase, pulseAmplitude, pulseSpeed, runeGlow |
| SimpleCutscene.Look | field | ringAlpha | 85 | public float ringSpeed, ringWidth, ringGrowW, ringHeight, ringGrowH, ringAlpha |
| SimpleCutscene.Look | field | emberColor | 87 | public Color emberColor |
| SimpleCutscene.Look | field | moteBright | 88 | public float streakBright, streakAlpha, moteBright |
| LoopSpec | field | order | 95 | public int order |
| LoopSpec | field | sparkle | 96 | public float rate, life, x0, x1, y0, y1, vx0, vx1, vy0, vy1, gravity, sparkle |
| Loop | ctor | Loop | 395 | public Loop(Transform parent, string name, LoopSpec s) |
| Loop | method | Draw | 405 | public void Draw(float t, float strength, Color color) |
| TutorialRect | method | Grow | 11 | public static Rect Grow(Rect r, Vector4 m) |
| TutorialRect | method | ArtRect | 15 | public static Rect ArtRect(Vector2 worldCentre, Vector2 size) |
| TutorialArea | field | kind | 39 | public AreaKind kind = AreaKind.Fixed |
| TutorialArea | method | FixedArtRect | 49 | public Rect FixedArtRect() |
| AreaKind | value | Fixed | 25 | Fixed |
| AreaKind | value | ScriptTile | 25 | ScriptTile |
| AreaKind | value | View | 25 | View |
| ViewRef | value | Bench | 28 | Bench |
| ViewRef | value | Redraw | 28 | Redraw |
| ViewRef | value | JournalCell | 28 | JournalCell |
| ViewRef | value | JournalRecipes | 28 | JournalRecipes |
| ViewRef | value | JournalIndex | 28 | JournalIndex |
| ViewRef | value | JournalHints | 28 | JournalHints |
| ViewRef | value | EventHub | 28 | EventHub |
| ScriptTrial | value | Reaction | 31 | Reaction |
| ScriptTrial | value | Miss | 31 | Miss |
| ScriptTrial | value | Chain | 31 | Chain |
| ScriptTrial | value | Catalyst | 31 | Catalyst |
| TutorialRules | method | MarkDone | 15 | public static void MarkDone() |
| TutorialRules | method | AnswerSkipQuestion | 21 | public static bool AnswerSkipQuestion(bool playIt) |
| TutorialController | property | Trial | 55 | public int Trial |
| TutorialController | property | Finished | 57 | public bool Finished |
| TutorialController | field | Shown | 59 | public readonly List<long> Shown = new List<long>() |
| TutorialController | field | AutoDrive | 62 | public bool AutoDrive |
| TutorialController | method | Bind | 65 | public void Bind(GameFlow flow) |
| TutorialController | method | Union | 77 | public static Rect Union(Rect[] r) |
| TutorialController | method | Abort | 157 | public void Abort() |
| TutorialController | method | Step | 164 | public TutorialStep Step(string id) |
| TutorialController | method | Holes | 313 | public Rect[] Holes(TutorialStep step, int hub = -1) |
| TutorialController | method | AfterEvent | 318 | public IEnumerator AfterEvent(ReactionEvent e, int index) |
| TutorialController | method | AfterIgnite | 357 | public IEnumerator AfterIgnite(IgniteOutcomeView o) |
| TutorialController | method | AfterChoose | 382 | public void AfterChoose(string catalystId, string name) |
| TutorialController | method | OnOffer | 392 | public IEnumerator OnOffer() |
| TutorialController | method | OfferTaken | 400 | public void OfferTaken() |
| TutorialController | method | Drive | 411 | public IEnumerator Drive() |
| TutorialController | property | IgniteOutcomeView | 465 | public sealed class IgniteOutcomeView |
| TutorialHole | method | Open | 18 | public void Open(Rect r, bool showRim) |
| TutorialHole | method | Close | 30 | public void Close() |
| TutorialHole | method | SetRimAlpha | 33 | public void SetRimAlpha(float a) |
| TutorialStyle | field | baseOrder | 13 | public int baseOrder |
| TutorialStyle | field | popupOrderOffset | 14 | public int popupOrderOffset |
| TutorialStyle | field | brokenOrderOffset | 15 | public int brokenOrderOffset |
| TutorialStyle | field | boxPos | 19 | public Vector2 boxPos |
| TutorialStyle | field | boxWidth | 20 | public float boxWidth |
| TutorialStyle | field | boxBorder | 22 | public float boxBorder |
| TutorialStyle | field | textInset | 24 | public Vector2 textInset |
| TutorialStyle | field | textWrapWidth | 26 | public float textWrapWidth |
| TutorialStyle | field | boxPadding | 28 | public float boxPadding |
| TutorialStyle | field | readExtra | 29 | public float readExtra |
| TutorialStyle | field | boxMaxHeight | 31 | public float boxMaxHeight |
| TutorialStyle | field | maxLines | 33 | public int linePitch, maxLines |
| TutorialStyle | field | maxLinesVi | 35 | public int linePitchVi, maxLinesVi |
| TutorialStyle | field | popupPos | 39 | public Vector2 popupPos |
| TutorialStyle | field | popupPitch | 40 | public float popupPitch |
| TutorialStyle | field | popupFontSize | 41 | public int popupFontSize |
| TutorialStyle | field | popupColor | 42 | public Color popupColor |
| TutorialStyle | field | popupTotalColor | 44 | public Color popupTotalColor |
| TutorialStyle | field | popupShadow | 45 | public Color popupShadow |
| TutorialStyle | field | brokenGhost | 49 | public Color brokenGhost |
| TutorialStyle | field | tileSize | 57 | public Vector2 tileSize |
| TutorialStyle | field | tileShift | 59 | public Vector2 tileShift |
| TutorialStyle | field | clickGuard | 63 | public float clickGuard |
| TutorialStyle | field | rimSpeed | 68 | public float rimSpeed |
| TutorialStyle | field | bumpDecay | 70 | public float bumpDecay |
| TutorialStyle | field | arrowBelow | 74 | public float arrowAbove, arrowBelow |
| TutorialStyle | field | arrowFlipY | 76 | public float arrowFlipY |
| TutorialStyle | field | arrowBobAmplitude | 77 | public float arrowBobSpeed, arrowBobAmplitude |
| TutorialStep | field | textKey | 13 | public string textKey = |
| TutorialStep | field | boxPos | 18 | public Vector2 boxPos |
| TutorialStep | method | Areas | 21 | public List<TutorialArea> Areas() |
| WindowView | method | Bind | 69 | public void Bind() |
| WindowView | property | Soft | 78 | public bool Soft |
| WindowView | method | Show | 91 | public void Show(int depth) |
| WindowView | method | ShowNight | 105 | public void ShowNight() |
| WindowView.Timing | field | dripFall | 21 | public float dustPeriod, dripPeriod, dripFall |
| WindowView.Timing | field | breathePeriod | 23 | public float breathePeriod |
| WindowView.Timing | field | flickerRate | 25 | public float flickerPeriod, flickerDip, flickerRate |
| WindowView.Timing | field | rootStep | 27 | public float rootStep |
| WindowView.Layout | field | dripX | 35 | public Vector2 moteX, moteY, dripX |
| WindowView.Layout | field | dripDrop | 36 | public float dripTop, dripDrop |
| WindowView.Layout | field | pointAt | 38 | public Vector2 glowAt, pointAt |
| WindowView.Look | field | point | 46 | public Color mote, glow, point |
| WindowView.Look | field | glowSwing | 47 | public float glowSwing |
| WindowView.Look | field | dipSwing | 49 | public float dipBase, dipSwing |
| WindowView.Look | field | rootBackground | 51 | public Color rootBackground |
| CameraSway | field | swayOn | 15 | public bool swayOn = true |
| CameraSway | field | amplitude | 17 | public float amplitude |
| CameraSway | field | timing | 26 | public SwayTiming timing |
| CameraSway | field | external | 30 | public Vector2 external |
| CameraSway | method | Shake | 35 | public void Shake(int size, float seconds) |
| CameraSway | method | ShakeOffset | 43 | public static Vector2Int ShakeOffset(float t, int size, float rateX, float rateY) |
| CameraSway | method | Pose | 47 | public void Pose(float t) |
| SwayTiming | field | periodY | 23 | public float periodX, periodY |
| SwayTiming | field | shakeRateY | 24 | public float shakeRateX, shakeRateY |
| DepthLayer | field | z | 13 | public float z |
| DepthLayer | field | rest | 15 | public Vector2 rest |
| DepthLayer | field | restScale | 17 | public Vector3 restScale = Vector3.one |
| DepthLayer | field | pointLight | 20 | public Light2D pointLight |
| DepthLayer | field | restOuter | 22 | public float restInner, restOuter |
| DepthLayer | method | Add | 25 | public static DepthLayer Add(GameObject go, float z) |
| DepthLayer | field | groupLights | 47 | public Light2D[] groupLights = new Light2D[0] |
| DepthLayer | field | groupInner | 48 | public float[] groupInner = new float[0], groupOuter = new float[0] |
| DepthLayer | method | AddGroup | 55 | public static DepthLayer AddGroup(GameObject go, float z) |
| DepthLayer | method | Apply | 67 | public void Apply(float d) |
| DepthMath | method | Scale | 13 | public static float Scale(float z, float d) |
| DepthMath | method | FovDegrees | 16 | public static float FovDegrees(float d, float halfHeight = Px.H / 2f) |
| DepthMath | method | Place | 19 | public static Vector3 Place(Vector2 rest, float z, float d) |
| DepthMath | method | Seen | 26 | public static Vector2 Seen(Vector3 world, float d) |
| DepthMath | method | Project | 34 | public static Vector2 Project(Vector3 world, Vector2 cam, Vector2 shift, float d) |
| DepthMath | method | DrawnFace | 44 | public static Vector2[] DrawnFace(Vector2 faceCentre, Vector2 faceSize, float artY, Vector2 cam, Vector2 shift, float inset, float d, TableShape table) |
| DepthMath | method | TableZ | 61 | public static float TableZ(float artY, float d, TableShape table) |
| DepthMath | method | Parallax | 71 | public static float Parallax(float z, float zAnchor, float d) |
| DepthMath | method | Sway | 75 | public static Vector2 Sway(float t, float amplitude, float periodX, float periodY) |
| DepthMath | method | AnchorShift | 86 | public static Vector2 AnchorShift(Vector2 cam, float zAnchor, float d) |
| DepthMath | method | Projection | 92 | public static Matrix4x4 Projection(float d, Vector2 shift, float near, float far) |
| DepthMath | method | ViewportRay | 104 | public static Ray ViewportRay(Vector2 viewport, Vector2 cam, Vector2 shift, float d) |
| DepthMath | method | HitTable | 114 | public static bool HitTable(Ray ray, out Vector2 flat, float d, TableShape table) |
| TableShape | field | rows | 135 | public readonly float front, back, topY, rows |
| TableShape | ctor | TableShape | 137 | public TableShape(float front, float back, float topY, float rows) |
| DepthMood | field | presets | 36 | public Preset[] presets |
| DepthMood | field | depth | 38 | public int depth |
| DepthMood | field | moon | 39 | public Light2D ambient, moon |
| DepthMood | field | volume | 40 | public Volume volume |
| DepthMood | field | frontFog | 41 | public SpriteRenderer backFog, frontFog |
| DepthMood | field | effects | 43 | public DepthSwitch effects |
| DepthMood | field | windowSwap | 44 | public int windowSwap |
| DepthMood | field | windowNight | 46 | public Sprite windowNight |
| DepthMood | field | window | 48 | public WindowView window |
| DepthMood | field | windowSharp | 49 | public Sprite[] windowSharp = new Sprite[0], windowSoft = new Sprite[0] |
| DepthMood | method | Set | 52 | public void Set(int d) |
| DepthMood | method | Set | 55 | public void Set(int d, bool themedWindow) |
| Preset | field | label | 20 | public string label |
| Preset | field | ambient | 21 | public Color ambient |
| Preset | field | moon | 22 | public float moon |
| Preset | field | accent | 23 | public Color accent |
| Preset | field | bloomIntensity | 25 | public float bloomThreshold, bloomIntensity |
| Preset | field | vignette | 27 | public float vignette |
| Preset | field | fogMix | 29 | public float backFog, frontFog, fogMix |
| Preset | property | FogTint | 32 | public Color FogTint |
| DepthRig | field | UiLayer | 15 | public const int UiLayer = 5 |
| DepthRig | field | WorldLayer | 19 | public const int WorldLayer = 6 |
| DepthRig | field | distance | 23 | public float distance |
| DepthRig | field | worldCamera | 27 | public Camera worldCamera |
| DepthRig | field | uiCamera | 29 | public Camera uiCamera |
| DepthRig | field | worldDisplay | 31 | public MeshRenderer worldDisplay |
| DepthRig | field | autoScale | 33 | public bool autoScale = true |
| DepthRig | field | far | 35 | public float near, far |
| DepthRig | field | table | 37 | public DepthTable table |
| DepthRig | field | anchorY | 39 | public float anchorY |
| DepthRig | property | Flat | 49 | public bool Flat |
| DepthRig | method | ScreenScale | 63 | public static int ScreenScale() |
| DepthRig | property | WorldTexture | 66 | public RenderTexture WorldTexture |
| DepthRig | property | RestPosition | 69 | public Vector3 RestPosition |
| DepthRig | property | CamOffset | 72 | public Vector2 CamOffset |
| DepthRig | property | ImageShift | 74 | public Vector2 ImageShift |
| DepthRig | property | AnchorZ | 76 | public float AnchorZ |
| DepthRig | method | SetView | 82 | public void SetView(Vector2 cam, Vector2 shake) |
| DepthRig | method | ViewportOf | 90 | public static Vector2 ViewportOf(Vector2 ui) |
| DepthRig | method | WorldRay | 97 | public Ray WorldRay(Vector2 ui) |
| DepthRig | method | BenchPoint | 100 | public bool BenchPoint(Vector2 ui, out Vector2 flat) |
| DepthRig | method | Apply | 131 | public void Apply() |
| DepthStage | field | rig | 12 | public DepthRig rig |
| DepthStage | field | sway | 13 | public CameraSway sway |
| DepthStage | field | mood | 14 | public DepthMood mood |
| DepthStage | field | effects | 15 | public DepthSwitch effects |
| DepthStage | field | window | 16 | public WindowView window |
| DepthStage | method | PlaceTiles | 19 | public void PlaceTiles(BenchView bench, int count) |
| DepthStage | method | SetFlat | 31 | public void SetFlat(bool flat) |
| DepthStage | method | ShowMood | 34 | public void ShowMood(int depth, bool themedWindow) |
| DepthStage | method | Look | 40 | public DepthMood.Preset Look(int depth) |
| DepthStage | property | SwayOn | 43 | public bool SwayOn |
| DepthSwitch | field | effectsOn | 30 | public bool effectsOn = true |
| DepthSwitch | field | sprites | 31 | public SpriteSwap[] sprites = new SpriteSwap[0] |
| DepthSwitch | field | flames | 32 | public FlameSwap[] flames = new FlameSwap[0] |
| DepthSwitch | field | window | 34 | public WindowView window |
| DepthSwitch | field | effectObjects | 36 | public GameObject[] effectObjects = new GameObject[0] |
| DepthSwitch | field | effectBehaviours | 38 | public Behaviour[] effectBehaviours = new Behaviour[0] |
| DepthSwitch | method | Set | 41 | public void Set(bool on) |
| SpriteSwap | field | target | 17 | public SpriteRenderer target |
| SpriteSwap | field | soft | 18 | public Sprite sharp, soft |
| FlameSwap | field | flame | 25 | public Flame flame |
| FlameSwap | field | soft | 26 | public Sprite[] sharp, soft |
| DepthTable | field | sprite | 18 | public Sprite sprite |
| DepthTable | field | artY | 20 | public float artX, artY |
| DepthTable | field | distance | 22 | public float distance |
| DepthTable | field | back | 24 | public float front, back |
| DepthTable | field | rows | 26 | public float topY, rows |
| DepthTable | method | RowZ | 40 | public float RowZ(float y) |
| DepthTable | property | Shape | 43 | public TableShape Shape |
| DepthTable | method | Point | 46 | public Vector3 Point(float x, float y) |
| DepthTable | method | Rebuild | 49 | public void Rebuild(float d) |
| DepthTiltShiftFeature | method | AddRenderPasses | 12 | public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) |
| FogSheet | field | speed | 15 | public float speed |
| FogSheet | field | width | 17 | public int width |
| FogSheet | method | Offset | 27 | public static int Offset(float t, float speed, int width) |
| FogSheet | method | Set | 30 | public void Set(float t) |
| FogSheet | method | Tick | 47 | public void Tick(float time) |
| TiltShift | field | material | 18 | public Material material |
| TiltShift | field | bandBottom | 20 | public float bandTop, bandBottom |
| TiltShift | field | feather | 22 | public float feather |
| TiltShift | field | maxSigma | 24 | public float maxSigma |
| TiltShift | method | ResetStatics | 28 | public static void ResetStatics() |
| TiltShift | method | IsOn | 31 | public static bool IsOn(Camera camera) |
| TiltShift | method | Sigma | 34 | public static float Sigma(float artY, float top, float bottom, float feather, float maxSigma) |
| TiltShift | method | Push | 46 | public void Push() |
| BurstSpec | field | count | 10 | public int count |
| BurstSpec | field | color | 11 | public Color color |
| BurstSpec | field | life | 12 | public float speed, life |
| BurstSpec | field | order | 13 | public int order |
| BurstSpec | field | gravity | 14 | public float gravity |
| BurstSpec | ctor | BurstSpec | 16 | public BurstSpec(int count, Color color, float speed, float life, int order, float gravity = 0) |
| BurstSpec | method | Play | 22 | public void Play(Sparks sparks, Vector2 pos) |
| Flame | field | frames | 9 | public Sprite[] frames |
| Flame | field | halo | 10 | public SpriteRenderer body, halo |
| Flame | field | glowLight | 11 | public Light2D glowLight |
| Flame | field | lightIntensity | 12 | public float lightIntensity = 1, haloAlpha = 0.5f, framesPerSecond = 11, phase |
| Flipbook | field | sheet | 10 | public FlipbookSheet sheet |
| Flipbook | field | playOnEnable | 11 | public bool playOnEnable = true, destroyOnEnd |
| Flipbook | field | Finished | 12 | public event Action Finished |
| Flipbook | property | IsPlaying | 17 | public bool IsPlaying |
| Flipbook | method | Play | 22 | public void Play(FlipbookSheet s = null) |
| FlipbookSheet | field | frames | 8 | public Sprite[] frames |
| FlipbookSheet | field | durations | 9 | public float[] durations |
| FlipbookSheet | field | loop | 10 | public bool loop |
| Fx | method | Spawn | 9 | public static Flipbook Spawn(string name, Vector2 pos, Transform parent = null, int sortingOrder = 50) |
| FxRefs | field | Screen | 9 | public readonly ScreenFx Screen |
| FxRefs | field | Sparks | 11 | public readonly Sparks Sparks |
| FxRefs | ctor | FxRefs | 13 | public FxRefs(ScreenFx screen, Sparks sparks) |
| ScreenFx | method | Bind | 31 | public void Bind(Camera camera, CameraSway worldSway = null) |
| ScreenFx | method | Flash | 38 | public void Flash(float peak, float seconds) |
| ScreenFx | method | Shake | 39 | public void Shake(float pixels, float seconds) |
| ScreenFx | method | FadeOut | 41 | public IEnumerator FadeOut(float seconds) |
| ScreenFx | method | FadeIn | 42 | public IEnumerator FadeIn(float seconds) |
| ScreenFx | method | SetBlack | 43 | public void SetBlack(float a) |
| ScreenFx | method | Tick | 48 | public void Tick(float deltaTime) |
| ShakeTiming | field | yScale | 19 | public float rateX, rateY, yScale |
| SparkMesh | property | Vert | 15 | struct Vert |
| SparkMesh | property | Quad | 16 | struct Quad |
| SparkMesh | field | DotScale | 19 | public const float DotScale = 0.25f |
| SparkMesh | property | BaseMaterial | 34 | public static Material BaseMaterial |
| SparkMesh | method | ResetStatics | 43 | public static void ResetStatics() |
| SparkMesh | property | Count | 59 | public int Count |
| SparkMesh | method | Create | 62 | public static SparkMesh Create(Transform parent, string name, int order, Sprite dot, Sprite sparkle) |
| SparkMesh | method | MaterialFor | 73 | public static Material MaterialFor(Texture texture) |
| SparkMesh | method | Begin | 108 | public void Begin() |
| SparkMesh | method | Add | 110 | public void Add(Vector2 pos, Color color, bool sparkle) |
| SparkMesh | method | End | 117 | public void End() |
| SparkMesh | property | Mesh | 186 | public Mesh Mesh |
| SparkParticles | field | K | 15 | public const float K = 3 |
| SparkParticles | property | LiveCount | 24 | public int LiveCount |
| SparkParticles | method | Clear | 26 | public void Clear() |
| SparkParticles | method | Emit | 28 | public void Emit(Sparks owner, Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity, bool sparkle, float drag) |
| Sparks | field | TrailRate | 16 | public const float TrailRate = 60 |
| Sparks | property | LiveCount | 40 | public int LiveCount |
| Sparks | property | LaneCount | 55 | public int LaneCount |
| Sparks | method | LaneMesh | 58 | public SparkMesh LaneMesh(int order) |
| Sparks | method | Emit | 61 | public void Emit(Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity = 0, bool sparkle = false, float drag = 0) |
| Sparks | method | Trail | 78 | public void Trail(Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity = 0, bool sparkle = false, float drag = 0) |
| Sparks | method | TrailCount | 90 | public static int TrailCount(float dt, float r) |
| Sparks | method | Burst | 93 | public void Burst(Vector2 pos, int count, Color color, float speed, float life, int order, float gravity = 0, float squash = 1) |
| Sparks | method | Clear | 102 | public void Clear() |
| Sparks | method | Step | 111 | public static bool Step(ref Particle p, float dt) |
| Sparks | method | Shade | 122 | public static Color Shade(in Particle p) |
| Sparks | method | Advance | 139 | public void Advance(float dt) |
| Sparks | method | Draw | 150 | public void Draw() |
| Particle | field | vel | 20 | public Vector2 pos, vel |
| Lane | field | order | 25 | public int order |
| Anim | method | Over | 14 | public static IEnumerator Over(float seconds, Action<float> step) |
| Anim | method | Wait | 21 | public static IEnumerator Wait(float seconds, Func<bool> skip = null) |
| Anim | method | OutCubic | 26 | public static float OutCubic(float t) |
| Anim | method | InCubic | 27 | public static float InCubic(float t) |
| Anim | method | InOut | 28 | public static float InOut(float t) |
| Anim | method | OutBack | 29 | public static float OutBack(float t) |
| Anim | method | Hump | 31 | public static float Hump(float t) |
| Art | field | Lib | 9 | public static ArtLibrary Lib |
| Art | field | Palette | 12 | public static Palette Palette |
| Art | method | ResetStatics | 16 | public static void ResetStatics() |
| ArtLibrary | field | sprites | 10 | public Sprite[] sprites |
| ArtLibrary | field | sheets | 11 | public FlipbookSheet[] sheets |
| ArtLibrary | field | tiny | 12 | public TMP_FontAsset title, body, label, tiny |
| ArtLibrary | field | crux | 14 | public TMP_FontAsset crux |
| ArtLibrary | field | glow | 15 | public Material lit, unlit, glow |
| ArtLibrary | method | Sprite | 20 | public Sprite Sprite(string name) |
| ArtLibrary | method | Sheet | 33 | public FlipbookSheet Sheet(string name) |
| AudioMute | field | Sink | 15 | public static Action<bool> Sink |
| AudioMute | property | Muted | 16 | public static bool Muted |
| AudioMute | method | ShouldMute | 18 | public static bool ShouldMute(bool tabHidden, bool paused, bool hasFocus) |
| AudioMute | method | SetHidden | 20 | public static void SetHidden(bool v) |
| AudioMute | method | SetPaused | 21 | public static void SetPaused(bool v) |
| AudioMute | method | SetFocus | 22 | public static void SetFocus(bool v) |
| AudioMute | method | ResetStatics | 25 | public static void ResetStatics() |
| AudioMute | method | Reset | 26 | public static void Reset() |
| Clickable | field | offset | 11 | public Vector2 size, offset |
| Clickable | field | order | 12 | public int order |
| Clickable | field | holes | 14 | public readonly List<Rect> holes = new List<Rect>() |
| Clickable | field | onUp | 15 | public Action onClick, onEnter, onExit, onDown, onUp |
| Clickable | method | Contains | 17 | public bool Contains(Vector2 world) |
| Dev | method | Hook | 11 | public static void Hook(string line) |
| Fonts | method | FontFor | 13 | public static TMP_FontAsset FontFor(FontKind kind) |
| Fonts | method | SizeFor | 21 | public static float SizeFor(FontKind kind, int size) |
| Fonts | method | HalfLeading | 28 | public static int HalfLeading(UnityEngine.TextCore.FaceInfo face, float size) |
| FontKind | value | Title | 7 | Title |
| FontKind | value | Body | 7 | Body |
| FontKind | value | Name | 7 | Name |
| FontKind | value | Tiny | 7 | Tiny |
| FontKind | value | Logo | 7 | Logo |
| FontKind | value | Numeral | 7 | Numeral |
| Haptics | field | Discovery | 13 | public const int Discovery = 20, DepthCleared = 40, RunEnded = 60 |
| Haptics | field | Enabled | 16 | public static bool Enabled = true |
| Haptics | field | ForceSupported | 18 | public static bool? ForceSupported |
| Haptics | field | Sink | 20 | public static Action<int> Sink |
| Haptics | method | ResetStatics | 29 | public static void ResetStatics() |
| Haptics | property | Supported | 31 | public static bool Supported |
| Haptics | method | Plan | 45 | public static int Plan(bool enabled, bool supported, int ms) |
| Haptics | method | Pulse | 47 | public static void Pulse(int ms) |
| KindColors | method | Of | 6 | public static string Of(string kind) |
| Label | field | shadow | 13 | public TextMeshPro main, shadow |
| Label | field | kind | 15 | public FontKind kind |
| Label | field | size | 16 | public int size |
| Label | property | Lines | 23 | public int Lines |
| Label | property | LineBox | 32 | public bool LineBox |
| Label | method | ApplyBias | 35 | public void ApplyBias() |
| Label | method | Bind | 59 | public void Bind(string textKey, params (string name, object value)[] textArgs) |
| Label | method | SetKey | 62 | public void SetKey(string textKey, params (string name, object value)[] textArgs) |
| Label | method | SetSize | 65 | public void SetSize(int points) |
| Label | property | Text | 93 | public string Text |
| Label | property | Color | 99 | public Color Color |
| Label | property | Alpha | 101 | public float Alpha |
| Label | property | Order | 110 | public int Order |
| Label | property | Width | 115 | public float Width |
| Loc | field | English | 14 | public const string English = "en", Vietnamese = |
| Loc | method | ResetStatics | 20 | public static void ResetStatics() |
| Loc | field | Changed | 26 | public static event Action Changed |
| Loc | property | Code | 29 | public static string Code |
| Loc | property | Vi | 30 | public static bool Vi |
| Loc | property | Current | 31 | public static Lang Current |
| Loc | property | NativeName | 33 | public static string NativeName |
| Loc | method | Use | 41 | public static void Use(TextAsset english, TextAsset vietnamese) |
| Loc | method | InitFromPlayer | 66 | public static void InitFromPlayer(string saved) |
| Loc | method | FromUrl | 75 | public static string FromUrl(string url) |
| Loc | method | Set | 84 | public static void Set(string c) |
| Loc | method | T | 97 | public static string T(string key, params (string name, object value)[] args) |
| Loc | method | P | 100 | public static string P(string key, long n, params (string name, object value)[] args) |
| Loc | method | C | 103 | public static string C(string key, string english, params (string name, object value)[] args) |
| Loc | method | Has | 105 | public static bool Has(string key) |
| Loc | method | Kind | 108 | public static string Kind(string kind) |
| Loc | method | Indef | 112 | public static string Indef(string kind) |
| Loc | method | Rarity | 116 | public static string Rarity(string rarity) |
| Loc | method | Upper | 119 | public static string Upper(string s) |
| Loc | method | DepthName | 122 | public static string DepthName(ContentDb db, int depth) |
| Loc | method | Number | 125 | public static string Number(long n) |
| Motion | method | Move | 19 | public static Tween Move(Transform t, Vector3 from, Vector3 to, float seconds, Ease ease = Ease.Linear) |
| Motion | method | MoveLocal | 23 | public static Tween MoveLocal(Transform t, Vector3 from, Vector3 to, float seconds, Ease ease = Ease.Linear) |
| Motion | property | ArcPath | 30 | sealed class ArcPath |
| Motion | method | Arc | 36 | public static Tween Arc(Transform t, Vector3 from, Vector3 to, float height, float seconds, Ease ease = Ease.Linear) |
| Motion | method | Scale | 42 | public static Tween Scale(Transform t, float from, float to, float seconds, Ease ease = Ease.Linear) |
| Motion | method | Fade | 46 | public static Tween Fade(SpriteRenderer r, float from, float to, float seconds, Ease ease = Ease.Linear) |
| Motion | method | Fade | 50 | public static Tween Fade(Label l, float from, float to, float seconds, Ease ease = Ease.Linear) |
| Motion | method | StopOn | 57 | public static void StopOn(Component owner) |
| Motion | method | Wait | 67 | public static IEnumerator Wait(Tween tween, Func<bool> hurry = null) |
| Motion | method | Wait | 73 | public static IEnumerator Wait(Sequence sequence, Func<bool> hurry = null) |
| RarityColors | field | legendary | 10 | public Color common, uncommon, rare, legendary |
| Palette | field | rarityText | 22 | public RarityColors rarityText |
| Palette | field | rarityTone | 24 | public RarityColors rarityTone |
| Palette | field | gold | 27 | public Color gold |
| Palette | field | pivot | 28 | public Color anchor, warm, echo, attuned, seed, fallow, heavy, cold, volatileTile, kiln, pivot |
| Palette | field | defaultGlow | 32 | public Color defaultGlow |
| Palette | field | teal | 34 | public Color ink, faded, teal |
| Palette | field | edge | 36 | public Color edge |
| Palette | field | pipOff | 38 | public Color pipOn, pipOff |
| Palette | field | buttonText | 40 | public Color buttonText |
| Palette | field | starOutline | 42 | public Color starOutline |
| PointerInput | property | World | 10 | public static Vector2 World |
| PointerInput | property | Rig | 12 | public static DepthRig Rig |
| PointerInput | property | Bench | 15 | public static Vector2 Bench |
| PointerInput | property | Down | 16 | public static bool Down |
| PointerInput | property | Up | 17 | public static bool Up |
| PointerInput | property | Held | 18 | public static bool Held |
| PointerInput | property | Hover | 19 | public static Clickable Hover |
| PointerInput | field | AnyPress | 21 | public static event Action AnyPress |
| PointerInput | method | Use | 27 | public static void Use(DepthRig rig) |
| PointerInput | method | ResetStatics | 31 | public static void ResetStatics() |
| PointerInput | method | Register | 37 | public static void Register(Clickable c) |
| PointerInput | method | Unregister | 38 | public static void Unregister(Clickable c) |
| PointerInput | method | ScreenToWorld | 41 | public static Vector2 ScreenToWorld(Vector2 screen) |
| PointerInput | method | Resolve | 50 | public static void Resolve(Vector2 world) |
| Px | field | W | 11 | public const int W = 640, H = 360 |
| Px | field | DotSize | 14 | public const float DotSize = 4f |
| Px | method | At | 17 | public static Vector3 At(float x, float y) |
| Px | method | Mid | 24 | public static Vector3 Mid(float x, float y, float w, float h) |
| Px | method | Fit | 27 | public static void Fit(Transform t, float x, float y, float w, float h) |
| Px | method | Snap | 33 | public static Vector3 Snap(Vector3 p) |
| Px | method | Hex | 36 | public static Color Hex(string hex, float a = 1, float k = 1) |
| Px | method | Alpha | 42 | public static Color Alpha(this Color c, float a) |
| Px | method | Times | 43 | public static Color Times(this Color c, float k) |
| RarityStars | field | Size | 15 | public const int Size = 7, Pitch = 8, Max = 4 |
| RarityStars | property | Root | 23 | public Transform Root |
| RarityStars | ctor | RarityStars | 26 | public RarityStars() |
| RarityStars | ctor | RarityStars | 28 | public RarityStars(Transform parent, int order) |
| RarityStars | property | GlyphCached | 48 | public static bool GlyphCached |
| RarityStars | method | ResetStatics | 52 | public static void ResetStatics() |
| RarityStars | method | Count | 75 | public static int Count(string rarity) |
| RarityStars | method | WidthOf | 78 | public static int WidthOf(int n) |
| RarityStars | method | Show | 81 | public void Show(string rarity) |
| RarityStars | method | Show | 84 | public void Show(int n, Color color) |
| RarityStars | method | SetAlpha | 91 | public void SetAlpha(float a) |
| RarityStars | method | SetOrder | 98 | public void SetOrder(int order) |
| RarityStars | method | SetActive | 104 | public void SetActive(bool on) |
| Spawn | method | Root | 21 | public static Transform Root(string name, Transform parent = null) |
| Spawn | method | Sprite | 30 | public static SpriteRenderer Sprite(Transform parent, string sprite, Vector3 worldPos, int order, Material material = null) |
| Spawn | method | Rect | 44 | public static SpriteRenderer Rect(Transform parent, float x, float y, float w, float h, Color color, int order) |
| Spawn | method | Glow | 54 | public static SpriteRenderer Glow(Transform parent, Vector3 worldPos, float radius, Color color, int order) |
| Spawn | method | Text | 64 | public static Label Text(Transform parent, string text, float x, float y, FontKind font, int size, Color color, int order, Align align = Align.Left, Color? shadow = null, float width = 0) |
| Spawn | method | Click | 107 | public static Clickable Click(GameObject go, Vector2 size, int order, System.Action onClick = null) |
| Align | value | Left | 7 | Left |
| Align | value | Centre | 7 | Centre |
| Align | value | Right | 7 | Right |
| Wording | method | RarityColor | 10 | public static Color RarityColor(string rarity) |
| Wording | method | RarityTone | 22 | public static Color RarityTone(string rarity) |
| Wording | method | RarityWord | 34 | public static string RarityWord(string rarity) |
| Wording | method | AspectColor | 37 | public static Color AspectColor(string effect) |
| Wording | property | Warm | 57 | public static Color Warm |
| Wording | method | AspectRule | 60 | public static string AspectRule(AspectInstance a, bool cost = true) |
| Wording | method | AspectCost | 82 | public static string AspectCost(AspectInstance a) |
| Wording | method | Effect | 90 | public static string Effect(EffectData fx) |
| Wording | method | OptionSummary | 120 | public static string OptionSummary(EventOption o) |
| Wording | method | GoalText | 139 | public static string GoalText(GoalData g) |
| Wording | method | OptionLabel | 142 | public static string OptionLabel(EventData e, EventOption o) |
| Wording | method | OutcomeText | 146 | public static string OutcomeText(EventData e, EventOption o, string english) |
| Wording | method | NoteText | 154 | public static string NoteText(EventNote note) |
| Wording | method | RewardLine | 161 | public static string RewardLine(GoalData g) |
| Wording | method | ItemName | 165 | public static string ItemName(ContentDb db, string id) |
| Wording | method | ItemSprite | 174 | public static string ItemSprite(ContentDb db, string id) |
| Wording | method | Number | 176 | public static string Number(long n) |
| ChoiceCard | field | cost | 13 | public string id, sprite, name, text, kicker, cost |
| ChoiceCard | field | rarity | 14 | public string rarity |
| ChoiceCard | field | tiles | 15 | public int[] tiles |
| ChoiceCard | field | dimTiles | 16 | public int[] dimTiles |
| CatalystChoiceView | field | OpenHook | 32 | public const string OpenHook = |
| CatalystChoiceView | property | Timing | 37 | public struct Timing |
| CatalystChoiceView | property | Layout | 40 | public struct Layout |
| CatalystChoiceView | method | CardName | 67 | public Label CardName(int i) |
| CatalystChoiceView | method | CornerStars | 69 | public RarityStars CornerStars(int i) |
| CatalystChoiceView | method | Bind | 74 | public void Bind(FxRefs effects) |
| CatalystChoiceView | method | Choose | 93 | public IEnumerator Choose(IReadOnlyList<CatalystData> offer, Action<string> done, float autoAfter = -1) |
| CatalystChoiceView | method | Choose | 105 | public IEnumerator Choose(IReadOnlyList<AspectInstance> offer, Action<string> done, float autoAfter = -1) |
| CatalystChoiceView | method | Choose | 119 | public IEnumerator Choose(string title, string line, IReadOnlyList<ChoiceCard> offer, Action<string> done, float autoAfter = -1) |
| Card | field | root | 47 | public Transform root |
| Card | field | click | 48 | public Clickable click |
| Card | field | bench | 49 | public SpriteRenderer[] bench = new SpriteRenderer[Bench.TileCount] |
| EventView | property | Timing | 29 | public struct Timing |
| EventView | property | Layout | 32 | public struct Layout |
| EventView | method | OptionLabel | 45 | public Label OptionLabel(int i) |
| EventView | property | Outcome | 47 | public Label Outcome |
| EventView | method | Bind | 52 | public void Bind(FxRefs effects) |
| EventView | method | Show | 82 | public IEnumerator Show(RunState run, Action changed) |
| EventView | method | Tick | 127 | public void Tick(float deltaTime) |
| Row | field | root | 21 | public Transform root |
| GoalRow | field | reward | 8 | public Label text, count, reward |
| GoalRow | field | icon | 9 | public SpriteRenderer track, fill, icon |
| GoalToast | property | Layout | 15 | public struct Layout |
| GoalToast | property | Timing | 19 | public struct Timing |
| GoalToast | property | SlideOutAt | 23 | public float SlideOutAt |
| GoalToast | property | Item | 25 | struct Item |
| GoalToast | property | Title | 35 | public Label Title |
| GoalToast | property | Line | 37 | public Label Line |
| GoalToast | method | Bind | 40 | public void Bind(FxRefs effects) |
| GoalToast | method | Add | 53 | public void Add(IEnumerable<GoalReward> rewards, ContentDb db) |
| GoalToast | method | Clear | 65 | public void Clear() |
| GoalsView | field | Closed | 17 | public event Action Closed |
| GoalsView | property | IsOpen | 18 | public bool IsOpen |
| GoalsView | property | Layout | 22 | public struct Layout |
| GoalsView | property | CloseClick | 44 | public Clickable CloseClick |
| GoalsView | method | Bind | 50 | public void Bind(ContentDb content) |
| GoalsView | method | Open | 74 | public void Open(Profile profile, Journal journal) |
| GoalsView | method | Close | 117 | public void Close() |
| JournalCell | field | icon | 12 | public SpriteRenderer icon |
| JournalCell | field | click | 13 | public Clickable click |
| JournalView | field | Closed | 33 | public event Action Closed |
| JournalView | property | IsOpen | 34 | public bool IsOpen |
| JournalView | field | PerPage | 37 | public const int PerPage = Cols * Rows |
| JournalView | property | Title | 60 | public Label Title |
| JournalView | property | CloseClick | 62 | public Clickable CloseClick |
| JournalView | property | PageClick | 64 | public Clickable PageClick |
| JournalView | method | Bind | 70 | public void Bind(ContentDb content) |
| JournalView | method | Open | 128 | public void Open(Journal book, Func<string, bool> openStarters = null) |
| JournalView | method | Close | 179 | public void Close() |
| JournalView | method | ShowMaterial | 190 | public Rect ShowMaterial(string id) |
| JournalView | method | CellRect | 197 | public static Rect CellRect(int i) |
| JournalView | method | CellRect | 199 | public Rect CellRect(string id) |
| JournalView | property | IndexRect | 201 | public Rect IndexRect |
| JournalView | property | RecipesRect | 203 | public Rect RecipesRect |
| JournalView | property | HintsRect | 205 | public Rect HintsRect |
| JournalView.Layout | field | hintsRect | 25 | public Rect indexRect, recipesRect, hintsRect |
| JournalView.Layout | field | titleTopLong | 27 | public float titleTop, titleTopLong |
| JournalView.Layout | field | unknown | 29 | public Color hint, unknown |
| PauseMenu | field | QuitConfirmed | 16 | public event Action QuitConfirmed |
| PauseMenu | field | VibrationChanged | 18 | public event Action VibrationChanged |
| PauseMenu | field | Closed | 20 | public event Action Opened, Closed |
| PauseMenu | field | CanOpen | 22 | public Func<bool> CanOpen |
| PauseMenu | property | IsOpen | 24 | public bool IsOpen |
| PauseMenu | property | Confirming | 25 | public bool Confirming |
| PauseMenu | property | Layout | 29 | public struct Layout |
| PauseMenu | method | Bind | 39 | public void Bind(Settings playerSettings) |
| PauseMenu | method | Open | 66 | public bool Open() |
| PauseMenu | method | Close | 77 | public void Close() |
| PauseMenu | method | Toggle | 87 | public void Toggle() |
| RunOverView | property | Again | 12 | public bool Again |
| RunOverView | field | GoalsClicked | 14 | public event System.Action GoalsClicked |
| RunOverView | property | Timing | 18 | public struct Timing |
| RunOverView | method | Bind | 29 | public void Bind() |
| RunOverView | method | Pick | 52 | public void Pick(bool again) |
| RunOverView | property | DepthName | 55 | public Label DepthName |
| RunOverView | property | DepthValue | 57 | public Label DepthValue |
| RunOverView | method | ShowDepth | 63 | public void ShowDepth(bool won, int depth, int finalDepth) |
| RunOverView | method | Show | 71 | public IEnumerator Show(bool won, RunState run, Journal journal, ContentDb db, System.Collections.Generic.IReadOnlyList<GoalReward> goals) |
| TitleView | property | BeginPressed | 11 | public bool BeginPressed |
| TitleView | property | HowToPressed | 13 | public bool HowToPressed |
| TitleView | property | IntroPressed | 14 | public bool IntroPressed |
| TitleView | method | ClearBegin | 16 | public void ClearBegin() |
| TitleView | method | RequestBegin | 18 | public void RequestBegin() |
| TitleView | field | SettingsClosed | 19 | public event Action JournalClicked, GoalsClicked, ResetConfirmed, SettingsClosed |
| TitleView | property | Asking | 21 | public bool Asking |
| TitleView | method | Bind | 74 | public void Bind(Settings playerSettings, FxRefs effects) |
| TitleView | method | AskSkip | 140 | public void AskSkip(Action<bool> answer) |
| TitleView | method | Open | 240 | public void Open(int known, int total, int goalsDone, int goals) |
| TitleView | method | Progress | 251 | public void Progress(int known, int total, int goalsDone, int goals) |
| TitleView | method | Close | 258 | public void Close() |
| TitleView.Layout | field | outlineSpread | 45 | public float centerX, logoY, logoRise, outlineSpread |
| TitleView.Layout | field | creditYVi | 47 | public float progressY, progressYVi, creditY, creditYVi |
| TitleView.Layout | field | cellOver | 49 | public Color resetRest, resetOver, cellRest, cellOver |
| TitleView.Layout | field | resetYNoVib | 51 | public float pitch, pitchNoVib, firstRow, firstRowNoVib, dividerX, dividerW, dividerH, dividerY, dividerYNoVib, resetY, resetYNoVib |
| TitleView.Layout | field | fillSize | 53 | public float labelX, labelDy, valueDy, valueDx, boxInset, boxSize, fillInset, fillSize |
| TitleView.Timing | field | glowSpeed | 59 | public float fadeIn, subDelay, subFade, glowBase, glowSwing, glowSpeed |
| TitleView.Timing | field | glow | 60 | public Color glow |
| TitleView.Timing | field | emberFall | 62 | public float emberRate, emberY, emberSize, emberFall |
| Tooltip | method | Bind | 37 | public void Bind() |
| Tooltip | method | Show | 47 | public void Show(string heading, string text, Vector2 anchor, float clearance = 20, string rarity = null) |
| Tooltip | method | Hide | 74 | public void Hide() |
| Tooltip.Layout | field | tierDy | 21 | public float pad, titleDy, bodyDy, starsDy, tierDy |
| Tooltip.Layout | field | edgeMargin | 23 | public float tierGap, edgeMargin |
| Tooltip.Layout | field | border | 25 | public float border |
| Tooltip.Layout | field | longText | 27 | public int longText |
| UiButton | field | W | 9 | public const int W = 120, H = 26 |
| UiButton | field | Clicked | 26 | public event Action Clicked |
| UiButton | property | Text | 28 | public Label Text |
| UiButton | property | Disabled | 30 | public bool Disabled |
| UiButton | method | Bind | 35 | public void Bind() |
| UiButton | method | Tick | 50 | public void Tick(float deltaTime) |
| UiButton.Look | field | disabledText | 18 | public Color disabledTint, overTint, disabledText |
| UiButton.Look | field | labelDown | 20 | public float labelUp, labelDown |
| ArtLibraryBuilder | method | Refresh | 19 | public static void Refresh() |
| ArtLibraryBuilder | method | RefreshBatch | 22 | public static void RefreshBatch() |
| ArtLibraryBuilder | method | RefreshLibrary | 25 | public static ArtLibrary RefreshLibrary() |
| AtlasBuilder | field | ArtDir | 29 | public const string ArtDir = "Assets/Art", AtlasDir = "Assets/Art/Atlases", LibraryPath = |
| AtlasBuilder | field | Padding | 30 | public const int Padding = 2, MaxSize = 2048, SheetGap = 2 |
| AtlasBuilder | field | DepthDir | 34 | public const string DepthDir = |
| AtlasBuilder | method | IsDepthArt | 37 | public static bool IsDepthArt(string assetPath) |
| AtlasBuilder | property | Animations | 41 | public static readonly string[] Animations = |
| AtlasBuilder | method | IsSpriteSheet | 44 | public static bool IsSpriteSheet(string assetPath) |
| AtlasBuilder | property | Standalone | 47 | public static readonly Dictionary<string, string> Standalone = new Dictionary<string, string> |
| AtlasBuilder | method | readonly | 56 | public static readonly (string name, string rule, Func<string, string, bool> match)[] Groups = |
| AtlasBuilder | method | BuildAll | 73 | public static string BuildAll() |
| AtlasBuilder | method | BuildBatch | 85 | public static void BuildBatch() |
| AtlasBuilder | method | PackAndReport | 93 | public static string PackAndReport() |
| AtlasBuilder | method | PackBatch | 114 | public static void PackBatch() |
| AtlasBuilder | method | BuildSpriteSheets | 131 | public static string BuildSpriteSheets() |
| AtlasBuilder | method | Assign | 290 | public static Dictionary<string, List<string>> Assign() |
| AtlasBuilder | method | BuildAtlases | 312 | public static string BuildAtlases() |
| BalanceReports | method | Run | 23 | public static int Run(string report, TextWriter output) |
| BalanceReports | method | RunBatch | 45 | public static void RunBatch() |
| CheckSprites | method | Check | 9 | public static void Check() |
| FixCandleImport | method | Fix | 7 | public static void Fix() |
| FlipbookImporter | field | Dir | 16 | public const string Dir = |
| FlipbookImporter | method | Import | 26 | public static string Import() |
| FontBuilder | method | BuildBatch | 15 | public static void BuildBatch() |
| FontBuilder | method | Build | 18 | public static string Build() |
| LayoutData | field | h | 13 | public int w, h |
| LayoutData | field | tiles | 14 | public TileData[] tiles |
| LayoutData | field | placed | 15 | public RectData[] placed |
| LayoutData | field | candles | 16 | public PointData[] candles |
| LayoutData | method | Placed | 19 | public RectData Placed(string name) |
| LayoutData | field | Path | 25 | public const string Path = |
| ContentFieldMigration | method | Run | 16 | public static void Run() |
| ContentFieldMigration | method | RunBatch | 28 | public static void RunBatch() |
| ContentAssets | field | Root | 7 | public const string Root = "Assets/Content", DatabasePath = Root + "/ContentDatabase.asset", RulesPath = Root + |
| ContentAssets | method | Database | 10 | public static ContentDatabase Database() |
| ContentAssets | method | Data | 18 | public static ContentData Data() |
| ContentSnapshot | property | FilePath | 12 | public static string FilePath |
| ContentSnapshot | method | Text | 15 | public static string Text() |
| ContentSnapshot | method | Write | 18 | public static void Write() |
| ContentSnapshot | method | RunBatch | 26 | public static void RunBatch() |
| EditorGlobals | method | Inject | 13 | public static void Inject() |
| Reserialize | method | All | 14 | public static void All() |
| ViewCapture | method | Render | 15 | public static Texture2D Render(GameObject root, params string[] keepHidden) |
| ViewCapture | method | RenderAsShown | 64 | public static Texture2D RenderAsShown(GameObject root) |
| ViewCapture | method | Save | 73 | public static void Save(Texture2D tex, string path) |
| ViewCapture | method | Load | 80 | public static Texture2D Load(string path) |
| ViewCapture | method | Differ | 89 | public static float Differ(Texture2D a, Texture2D b) |
| Sim | method | With | 11 | public static Unlocks With(ContentDb db, bool starters, bool catalysts) |
| Sim | method | Autoplay | 21 | public static RunState Autoplay(ContentDb db, int seed, RunOptions options) |
| Sim | method | Line | 30 | public static string Line(ContentDb db, string label, Func<int, RunOptions> options, int seeds) |
| Sim | method | RandomPlay | 44 | public static RunState RandomPlay(ContentDb db, int seed, RunOptions options) |
| Sim | method | Play | 394 | public static Career Play(ContentDb db, bool admin, int player, int runs, bool aspects = true, bool events = true, List<string> log = null, int logFrom = 0, int taughtTier = 0) |
| Sim | method | Pct | 447 | public static string Pct(double x) |
| Sim | method | Describe | 449 | public static void Describe(string role, List<Career> careers, int runs, Action<string> w, int finalDepth) |
| Sim | method | Autoplay | 493 | public static RunState Autoplay(ContentDb db, int seed, List<string> log = null, RunStats stats = null) |
| Sim | method | RandomPlay | 540 | public static RunState RandomPlay(ContentDb db, int seed) |
| Sim | method | FullJournal | 562 | public static Journal FullJournal(ContentDb db) |
| Newcomer | ctor | Newcomer | 78 | public Newcomer(ContentDb truth, Journal journal) |
| Newcomer | method | List | 130 | public readonly List<(string a, string b)> targets = new List<(string, string)>() |
| Newcomer | property | TriedCount | 131 | public int TriedCount |
| Newcomer | property | ColdCount | 132 | public int ColdCount |
| Newcomer | method | Status | 133 | public string Status(string a, string b) |
| Newcomer | method | CanMakeNow | 134 | public bool CanMakeNow(string m) |
| Newcomer | method | Decide | 177 | public bool Decide(RunState run) |
| Newcomer | method | Learn | 369 | public void Learn(IgniteOutcome outcome, BenchRules rules) |
| Career | field | FirstWin | 387 | public int FirstWin = -1 |
| Career | field | Won | 388 | public readonly List<bool> Won = new List<bool>() |
| Career | field | Depth | 389 | public readonly List<int> Depth = new List<int>(), Journal = new List<int>(), Goals = new List<int>(), Unlocked = new List<int>() |
| Career | field | Stuck | 390 | public int Stuck |
| Career | field | Knows | 391 | public string Knows = |
| RunStats | field | BySize | 485 | public readonly int[] BySize = new int[ContentDb.MaxItems + 1] |
| RunStats | field | FirstTrialClears | 486 | public int Ignites, LongestChain, Sputters, FirstTrialClears |
| RunStats | field | UltimateByCombo | 487 | public bool UltimateByCombo |
| RunStats | field | ComboMade | 488 | public readonly HashSet<string> ComboMade = new HashSet<string>() |
| RunStats | field | Combos | 489 | public readonly Dictionary<string, int> Combos = new Dictionary<string, int>() |
| RunStats | field | ClearedIn | 490 | public readonly List<string> ClearedIn = new List<string>() |
| SpriteImportRules | method | GetVersion | 14 | public override uint GetVersion() |
| SpriteImportRules | method | IsSoftDepthArt | 17 | public static bool IsSoftDepthArt(string assetPath) |
| SpriteImportRules | method | IsDepthFog | 25 | public static bool IsDepthFog(string assetPath) |
| TutorialStepsMigration | method | Run | 79 | public static void Run() |
| TutorialStepsMigration | method | RunBatch | 88 | public static void RunBatch() |
| A | field | name | 26 | public string name |
| S | field | key | 34 | public string id, key |
| WebBuild | field | Output | 12 | public const string Output = "Build/web", DevOutput = |
| WebBuild | method | Build | 15 | public static void Build() |
| WebBuild | method | BuildQuick | 19 | public static void BuildQuick() |
| WebBuild | method | BuildDev | 26 | public static void BuildDev() |
| ContentAssetTests | method | Definitions_EveryAsset_IsNamedAfterItsId | 10 | public void Definitions_EveryAsset_IsNamedAfterItsId() |
| ContentDatabaseTests | method | ToContentData_TinyDatabase_BuildsAValidContentDb | 37 | public void ToContentData_TinyDatabase_BuildsAValidContentDb() |
| ContentDatabaseTests | method | ToContentData_Material_WritesTheStringsTheRulesRead | 53 | public void ToContentData_Material_WritesTheStringsTheRulesRead() |
| ContentDatabaseTests | method | ToContentData_MaterialWithoutGlow_WritesAnEmptyGlowString | 65 | public void ToContentData_MaterialWithoutGlow_WritesAnEmptyGlowString() |
| ContentDatabaseTests | method | ToContentData_NoRarityWeights_IsNullSoTheDrawStaysUniform | 78 | public void ToContentData_NoRarityWeights_IsNullSoTheDrawStaysUniform() |
| ContentDatabaseTests | method | ToContentData_RecipeWithAMissingItem_IsRejectedByContentDb | 90 | public void ToContentData_RecipeWithAMissingItem_IsRejectedByContentDb() |
| ContentDatabaseTests | method | ToContentData_CalledTwice_ReturnsIndependentCopies | 102 | public void ToContentData_CalledTwice_ReturnsIndependentCopies() |
| ContentEnumTests | method | CatalystEffect_Names_EqualEffectsAll | 14 | public void CatalystEffect_Names_EqualEffectsAll() |
| ContentEnumTests | method | AspectEffect_Names_EqualAspectEffectsAll | 25 | public void AspectEffect_Names_EqualAspectEffectsAll() |
| ContentEnumTests | method | EventOp_Names_EqualOpsAll | 36 | public void EventOp_Names_EqualOpsAll() |
| ContentEnumTests | method | EventKind_Names_EqualOpsTypes | 47 | public void EventKind_Names_EqualOpsTypes() |
| ContentEnumTests | method | EventNeed_NamesAfterNone_EqualOpsNeeds | 58 | public void EventNeed_NamesAfterNone_EqualOpsNeeds() |
| ContentEnumTests | method | RarityId_NamesAfterUnset_EqualRaritiesAll | 69 | public void RarityId_NamesAfterUnset_EqualRaritiesAll() |
| ContentEnumTests | method | KindOrNone_NamesAfterNone_EqualKind | 80 | public void KindOrNone_NamesAfterNone_EqualKind() |
| ContentEnumTests | method | AspectScopeAndGroup_Names_AreTheStringsTheRulesCompare | 91 | public void AspectScopeAndGroup_Names_AreTheStringsTheRulesCompare() |
| ContentEnumTests | method | GoalStat_EveryValue_MapsToItsStatId | 105 | public void GoalStat_EveryValue_MapsToItsStatId() |
| ContentEnumTests | method | ContentNames_NoneValues_WriteEmptyStrings | 117 | public void ContentNames_NoneValues_WriteEmptyStrings() |
| ContentSnapshotTests | method | Snapshot_File_EqualsTheAssets | 10 | public void Snapshot_File_EqualsTheAssets() |
| ContentTests | method | EventTexts_OfEveryEvent_FitTheEventWindow | 316 | public void EventTexts_OfEveryEvent_FitTheEventWindow() |
| ContentTests | method | LoreEvents_DoNothing_AndTellTheStoryInOrder | 328 | public void LoreEvents_DoNothing_AndTellTheStoryInOrder() |
| TestLang | method | Text | 12 | public static string Text(string code) |
| LoreTests | method | TheLastLoreEvent_WhenRemembered_IsNotDrawn | 51 | public void TheLastLoreEvent_WhenRemembered_IsNotDrawn() |
| LoreTests | method | WithoutALastLore_OverManySeeds_EveryLoreEventCanCome | 64 | public void WithoutALastLore_OverManySeeds_EveryLoreEventCanCome() |
| LoreTests | method | ALastLore_ThatIsNotALoreEvent_ChangesNothing | 73 | public void ALastLore_ThatIsNotALoreEvent_ChangesNothing() |
| LoreTests | method | InARun_ALoreEvent_IsNotShownTwiceInARow | 86 | public void InARun_ALoreEvent_IsNotShownTwiceInARow() |
| LoreTests | method | AnotherLoreEvent_InBetween_LetsTheFirstComeBack | 99 | public void AnotherLoreEvent_InBetween_LetsTheFirstComeBack() |
| LoreTests | method | ANonLoreEvent_InBetween_DoesNotLetItComeBack | 117 | public void ANonLoreEvent_InBetween_DoesNotLetItComeBack() |
| LoreTests | method | SkippingALoreEvent_WhenPlayedWithAndWithoutIt_IsBalanceNeutral | 156 | public void SkippingALoreEvent_WhenPlayedWithAndWithoutIt_IsBalanceNeutral() |
| LoreTests | method | Profile_WhenShownLoreBoonAndNullEvents_RemembersOnlyLoreEvents | 184 | public void Profile_WhenShownLoreBoonAndNullEvents_RemembersOnlyLoreEvents() |
| LoreTests | method | Profile_LastLore_IsSaved_AndDroppedWhenNoLongerALoreEvent | 196 | public void Profile_LastLore_IsSaved_AndDroppedWhenNoLongerALoreEvent() |
| RarityTests | method | NoField_Equals_AllEqualWeights_Equals_Today | 47 | public void NoField_Equals_AllEqualWeights_Equals_Today() |
| RarityTests | method | Offer_IsDeterministicPerSeed_AndStaysDistinct | 63 | public void Offer_IsDeterministicPerSeed_AndStaysDistinct() |
| RarityTests | method | Offer_HeavierRarityWeight_FavoursThatRarity | 78 | public void Offer_HeavierRarityWeight_FavoursThatRarity() |
| RarityTests | method | Offer_ZeroWeights_ExcludeARarityWhileOthersRemain | 98 | public void Offer_ZeroWeights_ExcludeARarityWhileOthersRemain() |
| RarityTests | method | FirstOffer_AnyWeights_IsUntouched | 113 | public void FirstOffer_AnyWeights_IsUntouched() |
| RarityTests | method | Gift_NoField_Equals_Uniform_Today | 155 | public void Gift_NoField_Equals_Uniform_Today() |
| RarityTests | method | Gift_FollowsTheWeights_AndIsDeterministic | 166 | public void Gift_FollowsTheWeights_AndIsDeterministic() |
| RarityTests | method | Validation_BadWeights_AreRejected | 185 | public void Validation_BadWeights_AreRejected() |
| RarityTests | method | RealContent_RarityWeights_AbsentIsUniformOrPresentIsValid | 203 | public void RealContent_RarityWeights_AbsentIsUniformOrPresentIsValid() |
| RarityTests | method | Rarities_All_HasSevenEntries | 226 | public void Rarities_All_HasSevenEntries() |
| RulesTests | method | Rules_ShippedContent_HasTodaysValues | 11 | public void Rules_ShippedContent_HasTodaysValues() |
| RulesTests | method | FinalDepth_ShippedContent_IsFive | 36 | public void FinalDepth_ShippedContent_IsFive() |
| RulesTests | method | Constructor_SeedAspectWithoutAmount_Throws | 63 | public void Constructor_SeedAspectWithoutAmount_Throws() |
| RulesTests | method | Constructor_HeatTileEffectWithoutAmount_Throws | 71 | public void Constructor_HeatTileEffectWithoutAmount_Throws() |
| RulesTests | method | Constructor_AspectDepthOutsideTheRun_Throws | 116 | public void Constructor_AspectDepthOutsideTheRun_Throws() |
| RulesTests | method | Constructor_HandBaseBelowHandMin_Throws | 124 | public void Constructor_HandBaseBelowHandMin_Throws() |
| RulesTests | method | Constructor_DepthTargetsNotOneFewerThanDepths_Throws | 132 | public void Constructor_DepthTargetsNotOneFewerThanDepths_Throws() |
| RulesTests | method | TrialsLeft_TrialsSix_StartsDepthOneWithSix | 159 | public void TrialsLeft_TrialsSix_StartsDepthOneWithSix() |
| RulesTests | method | BasePotency_TableDoubled_IsDoubled | 172 | public void BasePotency_TableDoubled_IsDoubled() |
| RulesTests | method | Phase_AspectDepthsTwo_OffersAspectsAtDepthTwoOnly | 185 | public void Phase_AspectDepthsTwo_OffersAspectsAtDepthTwoOnly() |
| SettingsTests | method | New_NoArguments_HasDefaultValues | 9 | public void New_NoArguments_HasDefaultValues() |
| SettingsTests | method | New_NoArguments_SwayIsOn | 20 | public void New_NoArguments_SwayIsOn() |
| SettingsTests | method | ToJson_ThenFromJson_RoundTripsSwayOff | 26 | public void ToJson_ThenFromJson_RoundTripsSwayOff() |
| SettingsTests | method | FromJson_OldSettingsWithoutSway_KeepsSwayOn | 34 | public void FromJson_OldSettingsWithoutSway_KeepsSwayOn() |
| SettingsTests | method | FromJson_SwayAsNumber_GivesDefaults | 42 | public void FromJson_SwayAsNumber_GivesDefaults() |
| SettingsTests | method | ToJson_ThenFromJson_RoundTripsValues | 48 | public void ToJson_ThenFromJson_RoundTripsValues() |
| SettingsTests | method | FromJson_JsonUtilityAndSystemTextJsonShape_ReadsFields | 60 | public void FromJson_JsonUtilityAndSystemTextJsonShape_ReadsFields() |
| SettingsTests | method | FromJson_BrokenJson_GivesDefaults | 76 | public void FromJson_BrokenJson_GivesDefaults(string json) |
| SettingsTests | method | FromJson_MissingFields_KeepDefaults | 86 | public void FromJson_MissingFields_KeepDefaults() |
| SettingsTests | method | FromJson_OutOfRangeValues_ClampsVolumesAndLanguage | 95 | public void FromJson_OutOfRangeValues_ClampsVolumesAndLanguage() |
| SettingsTests | method | RoundTrip_LanguageWithEscapes_Survives | 110 | public void RoundTrip_LanguageWithEscapes_Survives() |
| TestContent | method | Load | 8 | public static ContentData Load() |
| TestContent | method | Tiny | 11 | public static ContentData Tiny() |
| TestContent | method | Combo | 33 | public static ContentData Combo(int zMin = 15) |
| TestContent | field | Today | 59 | public static readonly RulesData Today = Rules() |
| TestContent | field | FinalDepth | 61 | public const int FinalDepth = 5 |
| TestContent | method | Rules | 64 | public static RulesData Rules() |
| TestContent | method | Mat | 71 | public static MaterialData Mat(string id, string kind, int tier, int reactivity = 5) |
| TestContent | method | Rec | 73 | public static RecipeData Rec(string a, string b, string r) |
| TestContent | method | Rec | 74 | public static RecipeData Rec(string[] items, int min, string r) |
| TestContent | method | Cat | 75 | public static CatalystData Cat(string effect, string kind = "", float amount = 0) |
| TutorialTests | method | Satchel_Take_PullsANamedCard_FromDrawOrDiscard | 17 | public void Satchel_Take_PullsANamedCard_FromDrawOrDiscard() |
| TutorialTests | method | Deal_ReplacesTheStartingHand_AndKeepsTheSatchelWhole | 45 | public void Deal_ReplacesTheStartingHand_AndKeepsTheSatchelWhole() |
| TutorialTests | method | Deal_AfterMakingADiscovery_KeepsTheRack | 55 | public void Deal_AfterMakingADiscovery_KeepsTheRack() |
| TutorialTests | method | Deal_Refuses_TooManyCopies_NonStarters_LockedStarters_AndABusyBench | 65 | public void Deal_Refuses_TooManyCopies_NonStarters_LockedStarters_AndABusyBench() |
| TutorialTests | method | QuietBefore_HoldsAspectsAndEventsBack_UntilThatDepth | 81 | public void QuietBefore_HoldsAspectsAndEventsBack_UntilThatDepth() |
| TutorialTests | method | FirstOffer_IsFixed_ForTheFirstOfferOnly | 109 | public void FirstOffer_IsFixed_ForTheFirstOfferOnly() |
| TutorialTests | method | FirstOffer_WithALockedCatalystNamed_LeavesOutLockedCatalysts | 122 | public void FirstOffer_WithALockedCatalystNamed_LeavesOutLockedCatalysts() |
| TutorialTests | method | Preview_ScoresALayout_AndChangesNothing | 141 | public void Preview_ScoresALayout_AndChangesNothing() |
| TutorialTests | method | ScoreParts_SplitsAScore_IntoItsFactors | 157 | public void ScoreParts_SplitsAScore_IntoItsFactors() |
| TutorialTests | method | Trial1_EmberCoalAndDeepTear_MakeSteamWisp_For40 | 183 | public void Trial1_EmberCoalAndDeepTear_MakeSteamWisp_For40() |
| TutorialTests | method | Trial2_GlowmossAndIronOre_Fizzle_SpendATrial_AndHintPotionOnGlowmoss | 198 | public void Trial2_GlowmossAndIronOre_Fizzle_SpendATrial_AndHintPotionOnGlowmoss() |
| TutorialTests | method | Trial3_Chain_SkyIronThenStarmetalOre_40And240_ClearsDepth1 | 212 | public void Trial3_Chain_SkyIronThenStarmetalOre_40And240_ClearsDepth1() |
| TutorialTests | method | Depth2_CatalystTrial_ShowsTheCatalyst | 233 | public void Depth2_CatalystTrial_ShowsTheCatalyst(string catalyst, long score, double bonus) |
| TutorialTests | method | Script_IsTheSame_WhateverTheSeed | 249 | public void Script_IsTheSame_WhateverTheSeed() |
| TutorialTests | method | CatalystTrial_CoversTheOffer_AndNothingElse | 260 | public void CatalystTrial_CoversTheOffer_AndNothingElse() |
| TutorialTests | method | ShouldRun_TutorialDoneFlag_RunsOnlyUntilDone | 268 | public void ShouldRun_TutorialDoneFlag_RunsOnlyUntilDone(bool done, bool expected) |
| TutorialTests | method | Plan_DoneFlagAndJournal_PicksRunAskSkipOrNone | 276 | public void Plan_DoneFlagAndJournal_PicksRunAskSkipOrNone(bool done, int journalEntries, TutorialPlan expected) |
| TutorialTests | method | Plan_NotDone_RunsExactlyWhenShouldRunAndTheJournalIsEmpty | 280 | public void Plan_NotDone_RunsExactlyWhenShouldRunAndTheJournalIsEmpty(int journalEntries) |
| AutoHookTests | method | ToggleAuto_FlipsTheScriptedPlayer_AndLogsTheNewState | 33 | public void ToggleAuto_FlipsTheScriptedPlayer_AndLogsTheNewState() |
| ChoiceHookTests | method | Choose_LogsOpenHook_OnlyAfterTheDeal | 39 | public void Choose_LogsOpenHook_OnlyAfterTheDeal() |
| DepthClickTests | method | SetUp | 23 | public void SetUp() |
| DepthClickTests | method | TearDown | 36 | public void TearDown() |
| DepthClickTests | method | ClickOnTileCentre_AtRestAndUnderMaximumSway_HitsThatTile | 52 | public void ClickOnTileCentre_AtRestAndUnderMaximumSway_HitsThatTile() |
| DepthClickTests | method | ClickWhereTheCameraShowsTheTileCentre_UnderSwayAndShake_FindsThatPoint | 67 | public void ClickWhereTheCameraShowsTheTileCentre_UnderSwayAndShake_FindsThatPoint() |
| DepthClickTests | method | DrawnFace_UnderShakeUpToThreePx_IsHitByTheRayAtCentreAndInsetCorners | 84 | public void DrawnFace_UnderShakeUpToThreePx_IsHitByTheRayAtCentreAndInsetCorners() |
| DepthClickTests | method | WorldRay_UnderSwayAndShake_IsTheWorldCamerasOwnRay | 108 | public void WorldRay_UnderSwayAndShake_IsTheWorldCamerasOwnRay() |
| DepthClickTests | method | Sway_KeepsTheAnchorRowStill_AndMovesTheWallLessThanAnArtPixel | 120 | public void Sway_KeepsTheAnchorRowStill_AndMovesTheWallLessThanAnArtPixel() |
| DepthClickTests | method | SetView_ShakeOnly_MovesEveryDepthByTheSameWholeArtPixels | 141 | public void SetView_ShakeOnly_MovesEveryDepthByTheSameWholeArtPixels() |
| DepthClickTests | method | TiltShiftSigma_IsZeroInTheBand_AndRisesToTheMaximum | 153 | public void TiltShiftSigma_IsZeroInTheBand_AndRisesToTheMaximum() |
| DepthClickTests | method | Show_TileReloadedFromAScene_ShowsTheMaterialAndTakesClicks | 163 | public void Show_TileReloadedFromAScene_ShowsTheMaterialAndTakesClicks() |
| DepthClickTests | method | FogOffset_StepsInWholeTexels_AndWraps | 179 | public void FogOffset_StepsInWholeTexels_AndWraps() |
| DepthFixture | field | D | 12 | public const float D = 600f, Front = 660f, Back = 838.2f, TopY = 132f, Rows = 122f, AnchorY = 188f, Near = 50f, Far = 2000f |
| DepthFixture | property | Table | 14 | public static TableShape Table |
| DepthFixture | method | AddRig | 17 | public static DepthRig AddRig(GameObject root, Camera worldCamera) |
| DepthLayerTests | method | Fov_AtRest_Shows180UnitsAboveAndBelowTheCentre | 35 | public void Fov_AtRest_Shows180UnitsAboveAndBelowTheCentre() |
| DepthLayerTests | method | Scale_SpecDepths_MatchTheSpecTable | 42 | public void Scale_SpecDepths_MatchTheSpecTable() |
| DepthLayerTests | method | Place_AtAnyDepth_IsSeenWhereItWasDrawn | 49 | public void Place_AtAnyDepth_IsSeenWhereItWasDrawn() |
| DepthLayerTests | method | TableZ_MatchesTheSpecRows_AndClampsOutsideTheTop | 64 | public void TableZ_MatchesTheSpecRows_AndClampsOutsideTheTop() |
| DepthLayerTests | method | TableZ_EveryArtRow_LiesOnOnePlane | 73 | public void TableZ_EveryArtRow_LiesOnOnePlane() |
| DepthLayerTests | method | Parallax_MatchesTheSpecTable_AndIsZeroAtTheAnchor | 86 | public void Parallax_MatchesTheSpecTable_AndIsZeroAtTheAnchor() |
| DepthLayerTests | method | TearDown | 96 | public void TearDown() |
| DepthLayerTests | method | DepthLayer_Apply_PlacesAndScalesTheObjectAndItsPointLight | 99 | public void DepthLayer_Apply_PlacesAndScalesTheObjectAndItsPointLight() |
| DepthLayerTests | method | ArtLibrary_MainScene_HasEveryDepthArtSpriteOnce | 119 | public void ArtLibrary_MainScene_HasEveryDepthArtSpriteOnce() |
| DepthLayerTests | method | DepthArt_ImportSettings_KeepTheBlurSmoothAndTheFogTiling | 132 | public void DepthArt_ImportSettings_KeepTheBlurSmoothAndTheFogTiling() |
| DepthLookTests | method | IntroKey_IsTheDepthsKey | 10 | public void IntroKey_IsTheDepthsKey(int depth, string key) |
| DepthLookTests | method | IntroKey_NoneOutsideTheAuthoredDepths | 15 | public void IntroKey_NoneOutsideTheAuthoredDepths(int depth) |
| DepthLookTests | method | View_EachDepthHasItsOwn | 18 | public void View_EachDepthHasItsOwn(int depth, int view) |
| DepthLookTests | method | View_DeeperThanFive_KeepsTheLast | 21 | public void View_DeeperThanFive_KeepsTheLast(int depth) |
| DepthLookTests | method | View_BelowOne_IsTheFirst | 28 | public void View_BelowOne_IsTheFirst(int depth) |
| DepthLookTests | method | WindowSprite_AnyDepth_NamesTheArtSprite | 31 | public void WindowSprite_AnyDepth_NamesTheArtSprite() |
| DepthLookTests | method | ShowsRunIntro_OnlyDepthOneOfANormalRun_IsTrue | 37 | public void ShowsRunIntro_OnlyDepthOneOfANormalRun_IsTrue() |
| DepthLookTests | method | ThemedWindow_OutsideANormalRun_IsFalse | 48 | public void ThemedWindow_OutsideANormalRun_IsFalse() |
| GameTestSetup | method | Inject | 10 | public void Inject() |
| GoldenShotTests | method | Render_Shot_MatchesTheGoldenImage | 11 | public void Render_Shot_MatchesTheGoldenImage(string name) |
| HotPathTests | method | DestroyMade | 19 | public void DestroyMade() |
| HotPathTests | method | Tick_IdleHud_AllocatesNothing | 43 | public void Tick_IdleHud_AllocatesNothing() |
| HotPathTests | method | Tick_PotencyUnchanged_KeepsTheSameText | 52 | public void Tick_PotencyUnchanged_KeepsTheSameText() |
| HotPathTests | method | Tick_PotencyChanged_RewritesText | 64 | public void Tick_PotencyChanged_RewritesText() |
| HotPathTests | method | Tick_PotencyRunsIntoTarget_SwapsTheWordAwayAndBack | 80 | public void Tick_PotencyRunsIntoTarget_SwapsTheWordAwayAndBack() |
| HotPathTests | method | Tick_IdleBench_AllocatesNothing | 119 | public void Tick_IdleBench_AllocatesNothing() |
| HotPathTests | method | Tick_BenchWithTooltipUp_AllocatesNothing | 128 | public void Tick_BenchWithTooltipUp_AllocatesNothing() |
| HotPathTests | method | Tick_TooltipUp_RebuildsAfterRedrawsChange | 142 | public void Tick_TooltipUp_RebuildsAfterRedrawsChange() |
| HotPathTests | method | Tick_TileHoverStartsAndStops_DrawsAndClearsTheFace | 167 | public void Tick_TileHoverStartsAndStops_DrawsAndClearsTheFace() |
| HotPathTests | method | Tick_TileAndTileWithMaterial_AllocateNothing | 184 | public void Tick_TileAndTileWithMaterial_AllocateNothing() |
| HotPathTests | method | Tick_IdleCard_AllocatesNothing | 197 | public void Tick_IdleCard_AllocatesNothing() |
| HotPathTests | method | Tick_IdleEventView_AllocatesNothing | 210 | public void Tick_IdleEventView_AllocatesNothing() |
| HotPathTests | method | Tick_IdleUiButton_AllocatesNothing | 220 | public void Tick_IdleUiButton_AllocatesNothing() |
| HotPathTests | method | Tick_FogSheet_AllocatesNothingWithinATexelAndWhenOneIsCrossed | 229 | public void Tick_FogSheet_AllocatesNothingWithinATexelAndWhenOneIsCrossed() |
| HotPathTests | method | Tick_FogSheet_WritesTheNewTexelOffsetAndOnlyThen | 240 | public void Tick_FogSheet_WritesTheNewTexelOffsetAndOnlyThen() |
| HotPathTests | method | Tick_ScreenFxFlashEnds_ClearsTheFlashSprite | 257 | public void Tick_ScreenFxFlashEnds_ClearsTheFlashSprite() |
| HotPathTests | method | Tick_IdleScreenFx_AllocatesNothing | 274 | public void Tick_IdleScreenFx_AllocatesNothing() |
| HotPathTests | method | IdleBench_TenSecondsOfEveryUpdate_AllocatesZeroBytes | 291 | public void IdleBench_TenSecondsOfEveryUpdate_AllocatesZeroBytes() |
| HotPathTests | method | Continuous_DustSparksAndFlame_AllocateNothingWhileRunning | 326 | public void Continuous_DustSparksAndFlame_AllocateNothingWhileRunning() |
| HudTests | method | CanRedraw_WhileRedrawsRemain_IsTrueUntilTheLastRedrawIsUsed | 14 | public void CanRedraw_WhileRedrawsRemain_IsTrueUntilTheLastRedrawIsUsed() |
| HudTests | method | CanRedraw_IsFalse_OffTheBench_EvenWithRedrawsLeft | 29 | public void CanRedraw_IsFalse_OffTheBench_EvenWithRedrawsLeft() |
| HudTests | method | CanRedraw_IsFalse_WithNoRun | 40 | public void CanRedraw_IsFalse_WithNoRun() |
| LabelLineBoxTests | method | HalfLeading_Crux48_IsElevenPixels | 43 | public void HalfLeading_Crux48_IsElevenPixels() |
| LabelLineBoxTests | method | LineBoxTitle_InVietnamese_MovesFaceAndShadowDownByCruxHalfLeading | 52 | public void LineBoxTitle_InVietnamese_MovesFaceAndShadowDownByCruxHalfLeading() |
| LabelLineBoxTests | method | LineBoxTitle_SizeSwitchTo32_FollowsTheNewSize | 62 | public void LineBoxTitle_SizeSwitchTo32_FollowsTheNewSize() |
| LabelLineBoxTests | method | LineBoxTitle_InEnglish_DoesNotMove | 72 | public void LineBoxTitle_InEnglish_DoesNotMove() |
| LabelLineBoxTests | method | PlainTitle_InVietnamese_DoesNotMove | 81 | public void PlainTitle_InVietnamese_DoesNotMove() |
| LayoutPinTests | method | Palette_Asset_HasTodaysColours | 39 | public void Palette_Asset_HasTodaysColours() |
| LayoutPinTests | method | IgniteSequence_MainScene_KeepsTodaysTiming | 59 | public void IgniteSequence_MainScene_KeepsTodaysTiming() |
| LayoutPinTests | method | IgniteSequence_MainScene_KeepsTodaysLook | 81 | public void IgniteSequence_MainScene_KeepsTodaysLook() |
| LayoutPinTests | method | HudView_Prefab_KeepsTodaysLayout | 122 | public void HudView_Prefab_KeepsTodaysLayout() |
| LayoutPinTests | method | HudView_IgniteLabel_SitsWhereTheCodePutIt | 143 | public void HudView_IgniteLabel_SitsWhereTheCodePutIt() |
| LayoutPinTests | method | BenchParts_Prefabs_KeepTodaysLayout | 151 | public void BenchParts_Prefabs_KeepTodaysLayout() |
| LayoutPinTests | method | JournalView_Prefab_KeepsTodaysLayout | 215 | public void JournalView_Prefab_KeepsTodaysLayout() |
| LayoutPinTests | method | GoalsView_Prefab_KeepsTodaysLayout | 231 | public void GoalsView_Prefab_KeepsTodaysLayout() |
| LayoutPinTests | method | CatalystChoiceView_Prefab_KeepsTodaysLayout | 238 | public void CatalystChoiceView_Prefab_KeepsTodaysLayout() |
| LayoutPinTests | method | EventView_Prefab_KeepsTodaysLayout | 255 | public void EventView_Prefab_KeepsTodaysLayout() |
| LayoutPinTests | method | RunOverView_Prefab_KeepsTodaysTiming | 265 | public void RunOverView_Prefab_KeepsTodaysTiming() |
| LayoutPinTests | method | PauseMenu_Prefab_KeepsTodaysLayout | 272 | public void PauseMenu_Prefab_KeepsTodaysLayout() |
| LayoutPinTests | method | TitleView_Prefab_KeepsTodaysLayout | 282 | public void TitleView_Prefab_KeepsTodaysLayout() |
| LayoutPinTests | method | GoalToast_Prefab_KeepsTodaysLayout | 308 | public void GoalToast_Prefab_KeepsTodaysLayout() |
| LayoutPinTests | method | Tooltip_BenchPrefab_KeepsTodaysLayout | 317 | public void Tooltip_BenchPrefab_KeepsTodaysLayout() |
| LayoutPinTests | method | UiButton_EveryPrefabInstance_KeepsTodaysLook | 329 | public void UiButton_EveryPrefabInstance_KeepsTodaysLook() |
| LayoutPinTests | method | Palette_Asset_KeepsTheStarOutline | 353 | public void Palette_Asset_KeepsTheStarOutline() |
| LayoutPinTests | method | Dust_MainScene_KeepsTodaysLook | 361 | public void Dust_MainScene_KeepsTodaysLook() |
| LayoutPinTests | method | SimpleCutscene_MainScene_KeepsTodaysBeats | 381 | public void SimpleCutscene_MainScene_KeepsTodaysBeats() |
| LayoutPinTests | method | ReactionCutscene_MainScene_KeepsTodaysBeats | 409 | public void ReactionCutscene_MainScene_KeepsTodaysBeats() |
| LayoutPinTests | method | WindowView_MainScene_KeepsTodaysEffects | 437 | public void WindowView_MainScene_KeepsTodaysEffects() |
| LayoutPinTests | method | Tutorial_MainScene_KeepsTodaysStyle | 455 | public void Tutorial_MainScene_KeepsTodaysStyle() |
| LayoutPinTests | method | Depth_MainScene_KeepsTodaysRigSwayAndTable | 476 | public void Depth_MainScene_KeepsTodaysRigSwayAndTable() |
| LayoutPinTests | method | Effects_MainScene_KeepsTodaysTiltShiftFogAndShake | 496 | public void Effects_MainScene_KeepsTodaysTiltShiftFogAndShake() |
| LocTests | method | Use_WithInjectedFiles_ServesTheirText | 19 | public void Use_WithInjectedFiles_ServesTheirText() |
| LocTests | method | Use_NullFile_Throws | 32 | public void Use_NullFile_Throws() |
| LocTests | method | ViJsonCharacters_AllInCruxAtlas_NoneMissing | 40 | public void ViJsonCharacters_AllInCruxAtlas_NoneMissing() |
| LocTests | method | UppercasedViStrings_AreInTheCruxAtlas_BecauseScreensWriteCapitalsThroughLocUpper | 51 | public void UppercasedViStrings_AreInTheCruxAtlas_BecauseScreensWriteCapitalsThroughLocUpper() |
| LocTests | method | EveryCharacterInEnJson_IsInTheCruxAtlas_SoALanguageSwitchNeverDrawsABlank | 64 | public void EveryCharacterInEnJson_IsInTheCruxAtlas_SoALanguageSwitchNeverDrawsABlank() |
| LocTests | method | EveryUiKey_ResolvesInBothLanguages_NeverToTheKey | 73 | public void EveryUiKey_ResolvesInBothLanguages_NeverToTheKey() |
| LocTests | method | Vietnamese_ChangesTheWords_AndEnglishComesBack | 85 | public void Vietnamese_ChangesTheWords_AndEnglishComesBack() |
| LocTests | method | ItemNames_StayEnglish_InVietnamese | 96 | public void ItemNames_StayEnglish_InVietnamese() |
| LocTests | method | FormatNumber_ByLanguage_UsesLocaleSeparators | 103 | public void FormatNumber_ByLanguage_UsesLocaleSeparators() |
| LocTests | method | ReadLangFromUrl_WithLangParam_ReturnsCode | 111 | public void ReadLangFromUrl_WithLangParam_ReturnsCode() |
| MainDepthTests | method | SetUp | 18 | public void SetUp() |
| MainDepthTests | method | TearDown | 36 | public void TearDown() |
| MainDepthTests | method | SpawnChildren_OfAWorldLayerParent_DrawOnTheWorldLayer | 39 | public void SpawnChildren_OfAWorldLayerParent_DrawOnTheWorldLayer() |
| MainDepthTests | method | TileGroup_AtRest_KeepsTheFaceOnItsArtPixels | 52 | public void TileGroup_AtRest_KeepsTheFaceOnItsArtPixels() |
| MainDepthTests | method | TileIcon_MovedByLocalPosition_StaysOnTheTile | 70 | public void TileIcon_MovedByLocalPosition_StaysOnTheTile() |
| MainDepthTests | method | ShakeFromScreenFx_MovesTheWorldPicture_AndFlatModeIgnoresIt | 79 | public void ShakeFromScreenFx_MovesTheWorldPicture_AndFlatModeIgnoresIt() |
| MainDepthTests | method | PointerBench_AllNineTiles_HitAtRestAtSwayExtremesAndUnderShake | 95 | public void PointerBench_AllNineTiles_HitAtRestAtSwayExtremesAndUnderShake() |
| MainDepthTests | method | MainScene_HasTheRigWorldAndUiCameras_AndTheStageIsWired | 121 | public void MainScene_HasTheRigWorldAndUiCameras_AndTheStageIsWired() |
| MainDepthTests | method | DepthMoodPresets_MainScene_MatchTheOldGameFlowLooks | 140 | public void DepthMoodPresets_MainScene_MatchTheOldGameFlowLooks() |
| MainDevHookTests | method | NextMood_StepsOneToFive_AndWraps | 14 | public void NextMood_StepsOneToFive_AndWraps() |
| MainDevHookTests | method | HookLines_ForEachHook_MatchTheScriptPatterns | 23 | public void HookLines_ForEachHook_MatchTheScriptPatterns() |
| MainDevHookTests | method | DrawnFace_AtRestView_IsTheInsetFaceInUiUnits | 33 | public void DrawnFace_AtRestView_IsTheInsetFaceInUiUnits() |
| MainDevHookTests | method | DrawnFace_WithSway_MovesTheFaceButNotAtTheAnchorRow | 42 | public void DrawnFace_WithSway_MovesTheFaceButNotAtTheAnchorRow() |
| MainSceneFixture | field | Flow | 20 | public readonly GameFlow Flow |
| MainSceneFixture | ctor | MainSceneFixture | 23 | public MainSceneFixture() |
| MainSceneFixture | method | Field | 35 | public static T Field<T>(object owner, string name) |
| MainSceneFixture | method | FlowField | 43 | public T FlowField<T>(string name) where T : Component |
| MainSceneFixture | method | All | 46 | public T[] All<T>() where T : Component |
| MainSceneFixture | method | Dispose | 49 | public void Dispose() |
| MotionTests | method | Clean | 20 | public void Clean() |
| MotionTests | method | Arc_IsAlive_AndCanJoinASequence | 27 | public void Arc_IsAlive_AndCanJoinASequence() |
| MotionTests | method | Arc_EndsOnItsTarget_OnWholePixels | 36 | public void Arc_EndsOnItsTarget_OnWholePixels() |
| MotionTests | method | EveryHelper_WhenStarted_IsAlive | 45 | public void EveryHelper_WhenStarted_IsAlive() |
| NoteTextTests | method | NoteText_Trials_MatchesTheOldWording | 29 | public void NoteText_Trials_MatchesTheOldWording(string lang, int n, string expected) |
| NoteTextTests | method | NoteText_Hand_MatchesTheOldWording | 38 | public void NoteText_Hand_MatchesTheOldWording(string lang, int n, string expected) |
| NoteTextTests | method | NoteText_Redraws_MatchesTheOldWording | 47 | public void NoteText_Redraws_MatchesTheOldWording(string lang, int n, string expected) |
| NoteTextTests | method | NoteText_RiShift_MatchesTheOldWording | 56 | public void NoteText_RiShift_MatchesTheOldWording(string lang, int n, string expected) |
| NoteTextTests | method | NoteText_HeatTile_MatchesTheOldWording | 61 | public void NoteText_HeatTile_MatchesTheOldWording(string lang, int n, string expected) |
| NoteTextTests | method | NoteText_Found_MatchesTheOldWording | 66 | public void NoteText_Found_MatchesTheOldWording(string lang, string expected) |
| NoteTextTests | method | NoteText_GaveUp_MatchesTheOldWording | 71 | public void NoteText_GaveUp_MatchesTheOldWording(string lang, string expected) |
| NoteTextTests | method | NoteText_Lost_MatchesTheOldWording | 77 | public void NoteText_Lost_MatchesTheOldWording(string lang, string expected) |
| NoteTextTests | method | NoteText_Received_MatchesTheOldWording | 82 | public void NoteText_Received_MatchesTheOldWording(string lang, string expected) |
| NoteTextTests | method | NoteText_Hint_MatchesTheOldWording | 87 | public void NoteText_Hint_MatchesTheOldWording(string lang, string expected) |
| NoteTextTests | method | NoteText_Became_MatchesTheOldWording | 92 | public void NoteText_Became_MatchesTheOldWording(string lang, string expected) |
| NoteTextTests | method | NoteText_FirstTriple_MatchesTheOldWording | 97 | public void NoteText_FirstTriple_MatchesTheOldWording(string lang, string expected) |
| NoteTextTests | method | NoteText_BlockTile_MatchesTheOldWording | 102 | public void NoteText_BlockTile_MatchesTheOldWording(string lang, string expected) |
| PauseTests | method | Open_StopsTime_AndEveryExitRestoresIt | 38 | public void Open_StopsTime_AndEveryExitRestoresIt() |
| PauseTests | method | Quit_RestoresTime_BeforeTheOwnerHearsOfIt | 53 | public void Quit_RestoresTime_BeforeTheOwnerHearsOfIt() |
| PauseTests | method | Esc_Toggles_AndBacksOutOfTheQuitQuestion | 67 | public void Esc_Toggles_AndBacksOutOfTheQuitQuestion() |
| PauseTests | method | Abort_TutorialRun_DoesNotMarkItDone | 78 | public void Abort_TutorialRun_DoesNotMarkItDone() |
| PauseTests | method | Open_NotAllowed_IsRefused | 90 | public void Open_NotAllowed_IsRefused() |
| PauseTests | method | VibrationButton_NoHaptics_IsHidden | 98 | public void VibrationButton_NoHaptics_IsHidden() |
| PauseTests | method | AfterQuit_ProfileAndJournalAreWhatIsStored_AndStorageIsNotWritten | 106 | public void AfterQuit_ProfileAndJournalAreWhatIsStored_AndStorageIsNotWritten() |
| PauseTests | method | Quit_AfterADiscovery_JournalInStorageHasIt | 123 | public void Quit_AfterADiscovery_JournalInStorageHasIt() |
| PauseTests | method | SettleIgnite_Sandbox_StoresNothing | 142 | public void SettleIgnite_Sandbox_StoresNothing() |
| PrefabBakeTests | method | Relabel_KeyedLabelInPrefab_FollowsTheLanguage | 34 | public void Relabel_KeyedLabelInPrefab_FollowsTheLanguage() |
| PrefabBakeTests | method | Relabel_PlainLabelInPrefab_KeepsItsText | 44 | public void Relabel_PlainLabelInPrefab_KeepsItsText() |
| PrefabBakeTests | method | Click_ButtonInPrefab_RaisesClicked | 55 | public void Click_ButtonInPrefab_RaisesClicked() |
| PrefabBakeTests | method | Click_DisabledButtonInPrefab_DoesNothing | 66 | public void Click_DisabledButtonInPrefab_DoesNothing() |
| PrefabBakeTests | method | Text_ButtonInPrefab_HasLabelAndBody | 78 | public void Text_ButtonInPrefab_HasLabelAndBody() |
| PrefabBakeTests | method | Disabled_SetOnAButton_IsKeptInTheSerializedState | 87 | public void Disabled_SetOnAButton_IsKeptInTheSerializedState() |
| PrefabBakeTests | method | ShowDepth_RunOverPrefab_WritesTheDepthRow | 96 | public void ShowDepth_RunOverPrefab_WritesTheDepthRow() |
| PrefabBakeTests | method | Click_RunOverPrefabButton_PicksItsChoice | 112 | public void Click_RunOverPrefabButton_PicksItsChoice(int index, bool again) |
| PrefabBakeTests | method | Bind_EventPrefabWithoutFx_Throws | 146 | public void Bind_EventPrefabWithoutFx_Throws() |
| PrefabBakeTests | method | Show_EventPrefab_ListsTheOptions | 158 | public void Show_EventPrefab_ListsTheOptions() |
| PrefabBakeTests | method | Click_EventPrefabLastOptionThenContinue_ShowsTheOutcomeAndCloses | 176 | public void Click_EventPrefabLastOptionThenContinue_ShowsTheOutcomeAndCloses() |
| PrefabBakeTests | method | Add_GoalToastPrefab_ShowsTheGoalName | 207 | public void Add_GoalToastPrefab_ShowsTheGoalName() |
| PrefabBakeTests | method | Click_ShowingGoalToastPrefab_SkipsToTheSlideOut | 225 | public void Click_ShowingGoalToastPrefab_SkipsToTheSlideOut() |
| PrefabBakeTests | method | Show_CatalystChoicePrefab_FillsThreeCards | 254 | public void Show_CatalystChoicePrefab_FillsThreeCards() |
| PrefabBakeTests | method | Show_CatalystChoicePrefabRareCard_DrawsThreeStars | 272 | public void Show_CatalystChoicePrefabRareCard_DrawsThreeStars() |
| PrefabBakeTests | method | Click_CatalystChoicePrefabCardAfterTheDeal_PicksItsId | 297 | public void Click_CatalystChoicePrefabCardAfterTheDeal_PicksItsId() |
| PrefabBakeTests | method | Open_PausePrefab_ShowsTheMenu | 320 | public void Open_PausePrefab_ShowsTheMenu() |
| PrefabBakeTests | method | Click_PausePrefabResume_ClosesAndRestoresTime | 338 | public void Click_PausePrefabResume_ClosesAndRestoresTime() |
| PrefabBakeTests | method | Click_PausePrefabCancelOnTheQuitQuestion_BacksOutToTheMenu | 356 | public void Click_PausePrefabCancelOnTheQuitQuestion_BacksOutToTheMenu() |
| PrefabBakeTests | method | Click_PausePrefabOutsideTheDialog_Resumes | 376 | public void Click_PausePrefabOutsideTheDialog_Resumes() |
| PrefabBakeTests | method | Bind_GoalsPrefab_MakesOneRowPerGoal | 400 | public void Bind_GoalsPrefab_MakesOneRowPerGoal() |
| PrefabBakeTests | method | Open_GoalsPrefabFreshProfile_WritesEveryGoal | 416 | public void Open_GoalsPrefabFreshProfile_WritesEveryGoal() |
| PrefabBakeTests | method | Click_GoalsPrefabClose_ClosesAndTellsTheOwner | 437 | public void Click_GoalsPrefabClose_ClosesAndTellsTheOwner() |
| PrefabBakeTests | method | Bind_JournalPrefab_MakesOneCellPerMaterial | 459 | public void Bind_JournalPrefab_MakesOneCellPerMaterial() |
| PrefabBakeTests | method | Click_JournalPrefabCell_ShowsThatMaterial | 475 | public void Click_JournalPrefabCell_ShowsThatMaterial() |
| PrefabBakeTests | method | Click_JournalPrefabPageLabel_TurnsTheGridPage | 497 | public void Click_JournalPrefabPageLabel_TurnsTheGridPage() |
| PrefabBakeTests | method | Click_JournalPrefabClose_ClosesAndTellsTheOwner | 519 | public void Click_JournalPrefabClose_ClosesAndTellsTheOwner() |
| PrefabBakeTests | method | Show_TooltipInBenchPrefab_HasItsSlicedPanel | 540 | public void Show_TooltipInBenchPrefab_HasItsSlicedPanel() |
| PrefabBakeTests | method | Bind_TitlePrefab_ShowsTheLogoText | 562 | public void Bind_TitlePrefab_ShowsTheLogoText() |
| PrefabBakeTests | method | Click_TitlePrefabBegin_SetsBeginPressed | 579 | public void Click_TitlePrefabBegin_SetsBeginPressed() |
| PrefabBakeTests | method | Click_TitlePrefabSettingsThenLanguage_SwitchesTheLanguageAndBackCloses | 596 | public void Click_TitlePrefabSettingsThenLanguage_SwitchesTheLanguageAndBackCloses() |
| PrefabBakeTests | method | Click_TitlePrefabEraseLink_AsksThenConfirmingRaisesResetConfirmed | 624 | public void Click_TitlePrefabEraseLink_AsksThenConfirmingRaisesResetConfirmed() |
| PrefabBakeTests | method | AskSkip_TitlePrefab_AnswersPlayFromTheSkipDialog | 647 | public void AskSkip_TitlePrefab_AnswersPlayFromTheSkipDialog() |
| PrefabBakeTests | method | Bind_HudPrefab_ShowsTheDepthNumber | 674 | public void Bind_HudPrefab_ShowsTheDepthNumber() |
| PrefabBakeTests | method | Hover_HudPrefabSocketAndAspect_SetsAndClearsTheHoveredIndex | 693 | public void Hover_HudPrefabSocketAndAspect_SetsAndClearsTheHoveredIndex() |
| PrefabBakeTests | method | Click_HudPrefabRedraw_RaisesRedrawClickedOnlyWhileARedrawIsLeft | 719 | public void Click_HudPrefabRedraw_RaisesRedrawClickedOnlyWhileARedrawIsLeft() |
| PrefabBakeTests | method | Click_HudPrefabPauseJournalAndIgnite_RaiseTheirEvents | 747 | public void Click_HudPrefabPauseJournalAndIgnite_RaiseTheirEvents() |
| PrefabBakeTests | method | Refresh_BenchPrefab_HasNineTiles | 782 | public void Refresh_BenchPrefab_HasNineTiles() |
| PrefabBakeTests | method | Refresh_BenchPrefab_DealsOneCardPerStartingMaterialFromTheCardPrefab | 808 | public void Refresh_BenchPrefab_DealsOneCardPerStartingMaterialFromTheCardPrefab() |
| PrefabBakeTests | method | Show_CardPrefab_OneWordNameHidesTheSecondLine | 834 | public void Show_CardPrefab_OneWordNameHidesTheSecondLine() |
| PrefabFixture | field | Root | 13 | public readonly GameObject Root |
| PrefabFixture | ctor | PrefabFixture | 16 | public PrefabFixture(string prefabPath) |
| PrefabFixture | method | Get | 24 | public T Get<T>() where T : Component |
| PrefabFixture | method | Destroy | 30 | public void Destroy() |
| PrefabFixture | method | Dispose | 38 | public void Dispose() |
| RunOverDepthTests | method | Won_ShowsPlantedAtDepth_OnOneLine_WithNoValue | 42 | public void Won_ShowsPlantedAtDepth_OnOneLine_WithNoValue() |
| RunOverDepthTests | method | Lost_ShowsDepthReached_AndNOfFive | 50 | public void Lost_ShowsDepthReached_AndNOfFive() |
| RunOverDepthTests | method | WonThenLost_OnTheSameView_RestoresTheLostRow | 58 | public void WonThenLost_OnTheSameView_RestoresTheLostRow() |
| RunOverDepthTests | method | Relabel_AfterLanguageChange_KeepsEachPathsText | 70 | public void Relabel_AfterLanguageChange_KeepsEachPathsText() |
| SaveTests | method | Restore | 22 | public void Restore() |
| SaveTests | method | LoadProfile_WithNothingSaved_IsFresh_WithASeedOfItsOwn | 31 | public void LoadProfile_WithNothingSaved_IsFresh_WithASeedOfItsOwn() |
| SaveTests | method | LoadProfile_WithGarbage_IsFresh | 40 | public void LoadProfile_WithGarbage_IsFresh() |
| SaveTests | method | StoreThenLoadProfile_WithStatsAndCompletedGoals_KeepsStatsGoalsAndUnlocks | 51 | public void StoreThenLoadProfile_WithStatsAndCompletedGoals_KeepsStatsGoalsAndUnlocks() |
| SaveTests | method | LoadProfile_WithIdsTheContentNoLongerHas_DropsThem | 68 | public void LoadProfile_WithIdsTheContentNoLongerHas_DropsThem() |
| SaveTests | method | ResetProfile_ErasesTheProfile_AndKeepsTheJournal | 82 | public void ResetProfile_ErasesTheProfile_AndKeepsTheJournal() |
| SaveTests | method | LoadJournal_WithGarbageString_ReturnsEmpty | 94 | public void LoadJournal_WithGarbageString_ReturnsEmpty() |
| SaveTests | method | LoadJournal_CorruptData_ReturnsEmptyJournalAndWarns | 103 | public void LoadJournal_CorruptData_ReturnsEmptyJournalAndWarns() |
| SaveTests | method | LoadProfile_CorruptData_ReturnsFreshProfileAndWarns | 112 | public void LoadProfile_CorruptData_ReturnsFreshProfileAndWarns() |
| SaveTests | method | LoadJournal_WithOlderShape_ReturnsEmpty | 121 | public void LoadJournal_WithOlderShape_ReturnsEmpty() |
| SaveTests | method | LoadJournal_WithNothingSaved_ReturnsEmpty | 130 | public void LoadJournal_WithNothingSaved_ReturnsEmpty() |
| SaveTests | method | StoreThenLoadJournal_WithAPairAndAHint_KeepsRecipesAndHints | 137 | public void StoreThenLoadJournal_WithAPairAndAHint_KeepsRecipesAndHints() |
| SaveTests | method | StoreThenLoadJournal_WithAThreeItemCombo_KeepsComboRecipes | 149 | public void StoreThenLoadJournal_WithAThreeItemCombo_KeepsComboRecipes() |
| SaveTests | method | LoadJournal_Phase1PairKeys_StillLoad | 158 | public void LoadJournal_Phase1PairKeys_StillLoad() |
| SaveTests | method | TutorialDone_DefaultsToFalse_AndRoundTrips | 165 | public void TutorialDone_DefaultsToFalse_AndRoundTrips() |
| SceneBakeTests | method | TutorialSteps_RedrawAndJournalSteps_MatchTheViews | 22 | public void TutorialSteps_RedrawAndJournalSteps_MatchTheViews() |
| SceneBakeTests | method | GameFlow_Baked_HasSparksField | 40 | public void GameFlow_Baked_HasSparksField() |
| SceneBakeTests | method | ScreenFx_Baked_HasFlashAndFadeSprites | 46 | public void ScreenFx_Baked_HasFlashAndFadeSprites() |
| SceneBakeTests | method | Window_Baked_HasPaneAndEveryEffectSprite_AndShowStartsAnEffect | 66 | public void Window_Baked_HasPaneAndEveryEffectSprite_AndShowStartsAnEffect() |
| SceneBakeTests | method | Tutorial_Baked_HasItsBoxArrowAndCellRoot_ClosedUntilAStepShows | 87 | public void Tutorial_Baked_HasItsBoxArrowAndCellRoot_ClosedUntilAStepShows() |
| SceneBakeTests | method | Reaction_Baked_HasEveryPart_AndBindHidesTheStageAndSwitchesTheOthers | 116 | public void Reaction_Baked_HasEveryPart_AndBindHidesTheStageAndSwitchesTheOthers() |
| SceneBakeTests | method | Story_Baked_HasEveryPart_AndTheRayFacesAreTheRaySprites | 150 | public void Story_Baked_HasEveryPart_AndTheRayFacesAreTheRaySprites() |
| SettingsHapticsTests | method | Settings_StoredThenLoaded_RoundTrip | 28 | public void Settings_StoredThenLoaded_RoundTrip() |
| SettingsHapticsTests | method | Settings_NothingOrGarbageSaved_GivesDefaults | 37 | public void Settings_NothingOrGarbageSaved_GivesDefaults() |
| SettingsHapticsTests | method | Haptics_Plan_OnlyWhenEnabledAndSupported | 50 | public void Haptics_Plan_OnlyWhenEnabledAndSupported() |
| SettingsHapticsTests | method | Haptics_Pulse_ReachesTheSinkOnlyWhenAllowed | 60 | public void Haptics_Pulse_ReachesTheSinkOnlyWhenAllowed() |
| SettingsHapticsTests | method | Haptics_InTheEditor_IsNotSupported | 69 | public void Haptics_InTheEditor_IsNotSupported() |
| SettingsHapticsTests | method | AudioMute_TabHiddenThenShown_PausesThenResumes | 72 | public void AudioMute_TabHiddenThenShown_PausesThenResumes() |
| SettingsHapticsTests | method | AudioMute_AnyReason_Mutes | 84 | public void AudioMute_AnyReason_Mutes() |
| SparksTests | method | Step_MatchesTheOldPool_PositionAndFade | 32 | public void Step_MatchesTheOldPool_PositionAndFade() |
| SparksTests | method | Step_Dies_WhenItsLifeIsOver | 49 | public void Step_Dies_WhenItsLifeIsOver() |
| SparksTests | method | Trail_EmitsTheSameDensity_AtAnyFrameRate | 57 | public void Trail_EmitsTheSameDensity_AtAnyFrameRate(int hz) |
| SparksTests | method | Trail_IsOnePerFrame_At60Hz | 65 | public void Trail_IsOnePerFrame_At60Hz() |
| SparksTests | method | SparkMesh_SnapsCentres_ToWholePixels_AndKeepsHdrColours | 71 | public void SparkMesh_SnapsCentres_ToWholePixels_AndKeepsHdrColours() |
| SparksTests | method | Sparks_UsesOneLanePerSortingOrder_AndClearEmptiesThem | 97 | public void Sparks_UsesOneLanePerSortingOrder_AndClearEmptiesThem() |
| SparksTests | method | Sparks_SteadyState_AllocatesNothing | 115 | public void Sparks_SteadyState_AllocatesNothing() |
| StaticResetTests | method | ResetStatics_Loc_DropsChangedHandlersAndGoesBackToEnglish | 21 | public void ResetStatics_Loc_DropsChangedHandlersAndGoesBackToEnglish() |
| StaticResetTests | method | ResetStatics_PointerInput_ClearsRigHoverAndAnyPress | 36 | public void ResetStatics_PointerInput_ClearsRigHoverAndAnyPress() |
| StaticResetTests | method | ResetStatics_Haptics_RestoresEnabledAndClearsSink | 57 | public void ResetStatics_Haptics_RestoresEnabledAndClearsSink() |
| StaticResetTests | method | ResetStatics_AudioMute_RestoresUnmuted | 67 | public void ResetStatics_AudioMute_RestoresUnmuted() |
| StaticResetTests | method | ResetStatics_SparkMesh_EmptiesTheMaterialCache | 82 | public void ResetStatics_SparkMesh_EmptiesTheMaterialCache() |
| StaticResetTests | method | ResetStatics_TiltShift_ForgetsCameras | 100 | public void ResetStatics_TiltShift_ForgetsCameras() |
| StaticResetTests | method | ResetStatics_RarityStars_DropsTheCachedGlyph | 115 | public void ResetStatics_RarityStars_DropsTheCachedGlyph() |
| StaticResetTests | method | ResetStatics_Art_ClearsTheLibrary | 129 | public void ResetStatics_Art_ClearsTheLibrary() |
| TestFx | method | DestroyLeftovers | 16 | public void DestroyLeftovers() |
| TestFx | method | Make | 23 | public static FxRefs Make(Transform parent) |
| TutorialAreaTests | method | Grow_EachMarginSide_AddsSeparately | 14 | public void Grow_EachMarginSide_AddsSeparately() |
| TutorialAreaTests | method | ArtRect_OfWorldCentre_IsTheCentredArtRect | 21 | public void ArtRect_OfWorldCentre_IsTheCentredArtRect() |
| TutorialAreaTests | method | Fixed_Area_ReadsItsTransform | 28 | public void Fixed_Area_ReadsItsTransform() |
| TutorialAreaTests | method | Step_Lists_Its_Areas_In_Child_Order | 43 | public void Step_Lists_Its_Areas_In_Child_Order() |
| TutorialFlowTests | method | Union_WithNoHoles_IsTheWholeCanvas_SoATextOnlyStepDimsEverythingAndDrawsNoRim | 33 | public void Union_WithNoHoles_IsTheWholeCanvas_SoATextOnlyStepDimsEverythingAndDrawsNoRim() |
| TutorialFlowTests | method | Union_OfHoles_IsTheirBoundingBox | 40 | public void Union_OfHoles_IsTheirBoundingBox() |
| TutorialFlowTests | method | Plan_FreshSave_RunsTheTutorial | 47 | public void Plan_FreshSave_RunsTheTutorial() |
| TutorialFlowTests | method | Plan_JournalWithEntries_AsksOnce | 53 | public void Plan_JournalWithEntries_AsksOnce() |
| TutorialFlowTests | method | MarkDone_AfterSkip_SetsTutorialDone | 61 | public void MarkDone_AfterSkip_SetsTutorialDone() |
| TutorialFlowTests | method | AnswerSkipQuestion_Skip_MarksDoneAndStartsTheRunWithoutAReplay | 70 | public void AnswerSkipQuestion_Skip_MarksDoneAndStartsTheRunWithoutAReplay() |
| TutorialFlowTests | method | AnswerSkipQuestion_PlayIt_ReplaysInTheSandboxAndLeavesTheSaveAlone | 80 | public void AnswerSkipQuestion_PlayIt_ReplaysInTheSandboxAndLeavesTheSaveAlone() |
| TutorialFlowTests | method | ScriptedTrials_Shown_Equal40_0_40_240 | 104 | public void ScriptedTrials_Shown_Equal40_0_40_240() |
| TutorialFlowTests | method | HowToPlay_ThrowawayJournalAndProfile_LeavesStorageByteIdentical | 113 | public void HowToPlay_ThrowawayJournalAndProfile_LeavesStorageByteIdentical() |
| TutorialFlowTests | method | ChooseEventOption_EmptiesEventOptions_SoViewsMustKeepTheChosenOption | 133 | public void ChooseEventOption_EmptiesEventOptions_SoViewsMustKeepTheChosenOption() |
| TutorialStepsBakeTests | field | BaselinePath | 19 | public const string BaselinePath = |
| TutorialStepsBakeTests | property | AllStepIds | 22 | public static readonly string[] AllStepIds = |
| TutorialStepsBakeTests | method | EveryStepId_UsedByDrive_ExistsInTheScene | 40 | public void EveryStepId_UsedByDrive_ExistsInTheScene() |
| TutorialStepsBakeTests | method | EveryStep_TextKey_HasEnglishAndVietnamese | 46 | public void EveryStep_TextKey_HasEnglishAndVietnamese() |
| TutorialStepsBakeTests | method | FixedAreas_OfEveryStep_HavePositiveSize | 59 | public void FixedAreas_OfEveryStep_HavePositiveSize() |
| TutorialStepsBakeTests | method | EveryStep_Rects_EqualTheBaseline | 67 | public void EveryStep_Rects_EqualTheBaseline() |
| TutorialStepsBakeTests | method | ClickRules_PassFlags_MatchTheOldBehaviour | 85 | public void ClickRules_PassFlags_MatchTheOldBehaviour() |
| AutoplayTraceTests | property | Result | 15 | sealed class Result |
| AutoplayTraceTests | method | Autoplay_Seed11_MatchesBaseline | 43 | public IEnumerator Autoplay_Seed11_MatchesBaseline() |
| AutoplayTraceTests | method | Autoplay_Seed22_MatchesBaseline | 55 | public IEnumerator Autoplay_Seed22_MatchesBaseline() |
| AutoplayTraceTests | method | Autoplay_Seed33_MatchesBaseline | 67 | public IEnumerator Autoplay_Seed33_MatchesBaseline() |
| MainScene | field | Name | 18 | public const string Name = |
| MainScene | method | Load | 20 | public static IEnumerator Load() |
| MainScene | method | Unload | 27 | public static IEnumerator Unload() |
| MainScene | method | Find | 33 | public static T Find<T>() where T : Object |
| PrefsSnapshot | ctor | PrefsSnapshot | 44 | public PrefsSnapshot(bool introSeen, bool tutorialDone) |
| PrefsSnapshot | method | Dispose | 55 | public void Dispose() |
| TraceRecorder | field | Lines | 68 | public readonly List<string> Lines = new List<string>() |
| TraceRecorder | ctor | TraceRecorder | 70 | public TraceRecorder() |
| TraceRecorder | method | Has | 76 | public bool Has(string prefix) |
| TraceRecorder | method | UpTo | 79 | public List<string> UpTo(string prefix) |
| TraceRecorder | method | Dispose | 85 | public void Dispose() |
| Baseline | method | Check | 93 | public static void Check(string file, IReadOnlyList<string> lines) |
| TutorialTraceTests | method | Tutorial_AutoDriven_MatchesBaseline | 18 | public IEnumerator Tutorial_AutoDriven_MatchesBaseline() |

## Materials
| id | name | kind | tier | reactivity |
|---|---|---|---|---|
| ember_coal | Ember Coal | Ore | 0 | 9 |
| deep_tear | Deep Tear | Potion | 0 | 4 |
| glowmoss | Glowmoss | Plant | 0 | 2 |
| moondust | Moondust | Powder | 0 | 7 |
| iron_ore | Iron Ore | Ore | 0 | 3 |
| bone_ash | Bone Ash | Powder | 0 | 5 |
| sunbloom | Sunbloom | Plant | 0 | 6 |
| wyrm_fang | Wyrm Fang | Beast | 0 | 8 |
| steam_wisp | Steam Wisp | Beast | 1 | 8 |
| sky_iron | Sky Iron | Metal | 1 | 3 |
| sulfur | Sulfur | Powder | 1 | 9 |
| frost_salt | Frost Salt | Crystal | 1 | 2 |
| verdant_tonic | Verdant Tonic | Potion | 1 | 5 |
| moonpetal | Moonpetal | Plant | 1 | 4 |
| dusk_cap | Dusk Cap | Plant | 1 | 1 |
| pixie_wing | Pixie Wing | Beast | 1 | 7 |
| dragon_scale | Dragon Scale | Beast | 1 | 2 |
| dragon_blood | Dragon Blood | Potion | 1 | 10 |
| quicksilver | Quicksilver | Metal | 1 | 6 |
| mandrake | Mandrake | Plant | 1 | 3 |
| hearth_glass | Hearth Glass | Crystal | 1 | 6 |
| wyrm_ichor | Wyrm Ichor | Potion | 1 | 4 |
| forge_slag | Forge Slag | Ore | 1 | 1 |
| primal_bloom | Primal Bloom | Plant | 1 | 10 |
| gold_ingot | Gold Ingot | Metal | 2 | 2 |
| cinnabar | Cinnabar | Crystal | 2 | 7 |
| humstone | Humstone | Crystal | 2 | 5 |
| storm_glass | Storm Glass | Crystal | 2 | 9 |
| basilisk_eye | Basilisk Eye | Beast | 2 | 6 |
| unicorn_horn | Unicorn Horn | Beast | 2 | 3 |
| lifewater | Lifewater | Potion | 2 | 1 |
| nightshade_ink | Nightshade Ink | Potion | 2 | 8 |
| starmetal_ore | Starmetal Ore | Ore | 2 | 4 |
| thunder_ingot | Thunder Ingot | Metal | 2 | 10 |
| witch_salve | Witch Salve | Potion | 2 | 3 |
| chimera_hide | Chimera Hide | Beast | 2 | 7 |
| phoenix_feather | Phoenix Feather | Beast | 3 | 10 |
| star_shard | Star Shard | Crystal | 3 | 6 |
| red_tincture | Red Tincture | Potion | 3 | 3 |
| aurora_dust | Aurora Dust | Powder | 3 | 8 |
| seer_s_orb | Seer's Orb | Crystal | 3 | 2 |
| sun_amalgam | Sun Amalgam | Metal | 3 | 5 |
| void_pearl | Void Pearl | Crystal | 3 | 1 |
| titan_alloy | Titan Alloy | Metal | 3 | 9 |
| dream_spore | Dream Spore | Plant | 3 | 7 |
| everflame | Everflame | Potion | 4 | 9 |
| heaven_s_ash | Heaven's Ash | Powder | 4 | 1 |
| living_gold | Living Gold | Metal | 4 | 4 |
| azoth | Azoth | Potion | 4 | 7 |
| philosopher_s_salt | Philosopher's Salt | Powder | 4 | 6 |
| chrysopoeia | Chrysopoeia | Metal | 4 | 2 |
| worldseed | Worldseed | Crystal | 5 | 10 |
| salt_crystal | Salt Crystal | Crystal | 0 | 5 |
| copper_filings | Copper Filings | Metal | 0 | 5 |
| grave_beetle | Grave Beetle | Beast | 0 | 4 |
| lightning_sand | Lightning Sand | Powder | 0 | 10 |
| hollow_bone | Hollow Bone | Beast | 0 | 1 |
| brine | Brine | Potion | 1 | 3 |
| verdigris | Verdigris | Powder | 1 | 6 |
| carapace | Carapace | Beast | 1 | 2 |
| static_dust | Static Dust | Powder | 1 | 9 |
| marrow | Marrow | Potion | 1 | 3 |
| spark_quartz | Spark Quartz | Crystal | 1 | 8 |
| bronze | Bronze | Metal | 2 | 4 |
| lye | Lye | Potion | 2 | 7 |
| blue_vitriol | Blue Vitriol | Crystal | 2 | 5 |
| tempest_glass | Tempest Glass | Crystal | 2 | 10 |
| scarab_amulet | Scarab Amulet | Metal | 2 | 3 |
| ossuary_dust | Ossuary Dust | Powder | 2 | 6 |
| thunderstone | Thunderstone | Ore | 2 | 9 |
| quickfire | Quickfire | Potion | 2 | 8 |
| stormcaller_rod | Stormcaller Rod | Metal | 3 | 9 |
| reliquary_bone | Reliquary Bone | Beast | 3 | 3 |
| salt_of_ages | Salt of Ages | Powder | 3 | 5 |
| ball_lightning | Ball Lightning | Potion | 3 | 10 |
| grave_lantern | Grave Lantern | Crystal | 3 | 4 |
| green_bronze | Green Bronze | Metal | 3 | 7 |
| thunder_heart | Thunder Heart | Beast | 4 | 10 |
| ossuary_crown | Ossuary Crown | Metal | 4 | 2 |
| storm_philter | Storm Philter | Potion | 4 | 8 |
| copper_sun | Copper Sun | Crystal | 4 | 6 |

## Recipes
| items | minReactivity | result |
|---|---|---|
| ember_coal, deep_tear | 0 | steam_wisp |
| ember_coal, iron_ore | 0 | sky_iron |
| ember_coal, bone_ash | 0 | sulfur |
| deep_tear, moondust | 0 | frost_salt |
| deep_tear, glowmoss | 0 | verdant_tonic |
| sunbloom, deep_tear | 0 | verdant_tonic |
| glowmoss, moondust | 0 | moonpetal |
| glowmoss, bone_ash | 0 | dusk_cap |
| wyrm_fang, glowmoss | 0 | dusk_cap |
| sunbloom, moondust | 0 | pixie_wing |
| wyrm_fang, iron_ore | 0 | dragon_scale |
| wyrm_fang, deep_tear | 0 | dragon_blood |
| iron_ore, moondust | 0 | quicksilver |
| sunbloom, bone_ash | 0 | mandrake |
| sky_iron, sulfur | 10 | gold_ingot |
| quicksilver, sulfur | 10 | cinnabar |
| frost_salt, moonpetal | 10 | humstone |
| steam_wisp, frost_salt | 10 | storm_glass |
| dragon_blood, dragon_scale | 10 | basilisk_eye |
| pixie_wing, moonpetal | 10 | unicorn_horn |
| mandrake, verdant_tonic | 10 | lifewater |
| dusk_cap, quicksilver | 10 | nightshade_ink |
| sky_iron, moondust | 10 | starmetal_ore |
| gold_ingot, dragon_blood | 14 | phoenix_feather |
| starmetal_ore, humstone | 14 | star_shard |
| cinnabar, lifewater | 14 | red_tincture |
| unicorn_horn, storm_glass | 14 | aurora_dust |
| basilisk_eye, nightshade_ink | 14 | seer_s_orb |
| gold_ingot, quicksilver | 14 | sun_amalgam |
| phoenix_feather, red_tincture | 22 | everflame |
| star_shard, aurora_dust | 22 | heaven_s_ash |
| seer_s_orb, sun_amalgam | 22 | living_gold |
| star_shard, red_tincture | 22 | azoth |
| living_gold, azoth | 30 | worldseed |
| everflame, heaven_s_ash | 30 | worldseed |
| ember_coal, moondust, bone_ash | 0 | hearth_glass |
| wyrm_fang, deep_tear, sunbloom | 0 | wyrm_ichor |
| ember_coal, iron_ore, bone_ash, moondust | 0 | forge_slag |
| glowmoss, sunbloom, deep_tear, moondust, bone_ash | 0 | primal_bloom |
| sky_iron, steam_wisp, ember_coal | 10 | thunder_ingot |
| mandrake, dusk_cap, deep_tear, glowmoss | 10 | witch_salve |
| dragon_scale, pixie_wing, wyrm_fang | 10 | chimera_hide |
| storm_glass, humstone, moondust | 14 | void_pearl |
| gold_ingot, starmetal_ore, ember_coal, iron_ore | 14 | titan_alloy |
| lifewater, nightshade_ink, glowmoss, sunbloom, moondust | 14 | dream_spore |
| red_tincture, aurora_dust, bone_ash | 22 | philosopher_s_salt |
| sun_amalgam, phoenix_feather, ember_coal, sunbloom | 22 | chrysopoeia |
| void_pearl, lifewater, deep_tear | 22 | azoth |
| phoenix_feather, ember_coal, sulfur, dragon_blood | 22 | everflame |
| aurora_dust, bone_ash, frost_salt, moonpetal | 22 | heaven_s_ash |
| hearth_glass, steam_wisp | 10 | storm_glass |
| thunder_ingot, humstone | 14 | star_shard |
| chimera_hide, gold_ingot | 14 | phoenix_feather |
| void_pearl, titan_alloy | 22 | living_gold |
| philosopher_s_salt, chrysopoeia | 30 | worldseed |
| wyrm_ichor, forge_slag | 10 | cinnabar |
| primal_bloom, verdant_tonic | 10 | lifewater |
| witch_salve, basilisk_eye | 14 | seer_s_orb |
| dream_spore, titan_alloy | 22 | chrysopoeia |
| salt_crystal, deep_tear | 0 | brine |
| copper_filings, bone_ash | 0 | verdigris |
| grave_beetle, iron_ore | 0 | carapace |
| lightning_sand, moondust | 0 | static_dust |
| hollow_bone, sunbloom | 0 | marrow |
| lightning_sand, salt_crystal | 0 | spark_quartz |
| hollow_bone, grave_beetle | 0 | carapace |
| grave_beetle, bone_ash, deep_tear | 0 | marrow |
| copper_filings, sky_iron | 10 | bronze |
| brine, bone_ash | 10 | lye |
| verdigris, brine | 10 | blue_vitriol |
| spark_quartz, static_dust | 10 | tempest_glass |
| carapace, copper_filings | 10 | scarab_amulet |
| marrow, bone_ash, carapace | 10 | ossuary_dust |
| static_dust, forge_slag | 10 | thunderstone |
| spark_quartz, ember_coal | 10 | quickfire |
| salt_crystal, verdigris | 10 | blue_vitriol |
| grave_beetle, moonpetal, hollow_bone | 10 | ossuary_dust |
| tempest_glass, bronze | 14 | stormcaller_rod |
| scarab_amulet, marrow, hollow_bone | 14 | reliquary_bone |
| lye, blue_vitriol, moonpetal | 14 | salt_of_ages |
| quickfire, thunderstone | 14 | ball_lightning |
| ossuary_dust, scarab_amulet, glowmoss | 14 | grave_lantern |
| bronze, primal_bloom | 14 | green_bronze |
| stormcaller_rod, ball_lightning, phoenix_feather | 22 | thunder_heart |
| reliquary_bone, grave_lantern, void_pearl | 22 | ossuary_crown |
| ball_lightning, salt_of_ages, aurora_dust, lightning_sand | 22 | storm_philter |
| green_bronze, sun_amalgam, copper_filings | 22 | copper_sun |

## Lists
- catalysts: bellows, bestiary, heart_tile, first_spark, crooked_mirror, hourglass, deep_satchel, eureka_lens, resonant_slab, quiet_bench, herbarium, alembic, wide_crucible, quick_hands, grain_of_gold, tier_seeker, hearth_habit, second_wind, warm_bench, salvage, cascade, descent, tinkers_eye, bottomless_satchel, philosophers_fire, last_breath
- aspects: doubled_tile, anchor_tile, heated_tile, echo_tile, attuned_tile, seed_tile, fallow_tile, heavy_tile, cold_tile, volatile_tile, greedy_bench, bridge, open_bench, kiln_tile, crossroads, pivot_tile
- events: cracked_altar, tilted_workbench, old_ledger, hungry_furnace, dripping_well, shifting_bench, smugglers_crate, forgotten_satchel, glinting_seam, sealed_shrine, quiet_shrine, damp_air, falling_grit, cold_draft, warm_stone, quiet_room, names_in_wall, warm_roots, pinned_note
- goals: g_depth2, g_runs3, g_ignite25, g_combo4, g_journal20, g_chain3, g_journal10, g_depth3, g_runs10, g_ignite100, g_depth4, g_runs25, g_ignite300, g_chain4, g_combo5, g_journal30, g_big1500, g_win1, g_win5, g_noredraw, g_ignite1000, g_journalfull
