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
| AutoPlayer | AutoPlayer | Core | class | Athanor/Assets/Scripts/Core/AutoPlayer.cs | 13 |  |
| AutoMove | AutoMove | Core | enum | Athanor/Assets/Scripts/Core/AutoPlayer.cs | 11 |  |
| BenchRules | BenchRules | Core | class | Athanor/Assets/Scripts/Core/BenchRules.cs | 11 | What catalysts, aspects and events change about the bench itself: which tiles touch, how lively a tile counts, which tiles are shut. The planner, the resolver and the scripted player all read this, so |
| ScoreContext | ScoreContext | Core | class | Athanor/Assets/Scripts/Core/BenchRules.cs | 82 | What one reaction's score depends on beyond the recipe and the catalysts. |
| ChainStep | ChainStep | Core | class | Athanor/Assets/Scripts/Core/Chain.cs | 11 | One thing the bench does during an Ignite. A reaction: the hub and the neighbours it consumed (Tiles[0] is the hub, the rest ascending). A sputter: a combo of three or more whose layout was standing r |
| ChainPlan | ChainPlan | Core | class | Athanor/Assets/Scripts/Core/Chain.cs | 20 |  |
| ChainPlanner | ChainPlanner | Core | class | Athanor/Assets/Scripts/Core/Chain.cs | 32 | Works out what a bench does, with no side effects. Each round the liveliest tile (highest Reaction Index, earlier tile on a tie) that has a recipe around it becomes the hub; the largest recipe it can  |
| RulesData | RulesData | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 29 | The run's tuning numbers, read from the "rules" block of content.json. Required: ContentDb throws without it. |
| ContentData | ContentData | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 56 |  |
| ProfileData | ProfileData | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 78 |  |
| Rarities | Rarities | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 88 |  |
| Effects | Effects | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 97 | The catalyst effect names used in content.json. |
| AspectEffects | AspectEffects | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 116 | The aspect effect names used in content.json. |
| Ops | Ops | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 128 | The event effect names (EffectData.op) and option requirements used in content.json. |
| StatIds | StatIds | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 144 | The stats a goal can watch. |
| Kind | Kind | Core | enum | Athanor/Assets/Scripts/Core/ContentData.cs | 5 |  |
| ContentDb | ContentDb | Core | class | Athanor/Assets/Scripts/Core/ContentDb.cs | 10 | Validated, indexed view of the game's content. |
| Bench | Bench | Core | class | Athanor/Assets/Scripts/Core/Ignite.cs | 7 | The 3x3 bench: tile index = row * 3 + column, null where empty. |
| ReactionEvent | ReactionEvent | Core | class | Athanor/Assets/Scripts/Core/Ignite.cs | 21 | One thing that happened during an Ignite, in the order it should be shown. A reaction: Tiles[0] is the hub, where the result now stands; the other tiles were consumed. A sputter: a combo that stood re |
| ResolveResult | ResolveResult | Core | class | Athanor/Assets/Scripts/Core/Ignite.cs | 36 |  |
| IgniteResolver | IgniteResolver | Core | class | Athanor/Assets/Scripts/Core/Ignite.cs | 43 |  |
| Journal | Journal | Core | class | Athanor/Assets/Scripts/Core/Journal.cs | 8 | What the player has learned across runs: recipes that worked, and hints from ones that did not. |
| Lang | Lang | Core | class | Athanor/Assets/Scripts/Core/Lang.cs | 20 | The player-facing text of one language, Unity-free. Lookup order: this language, then the fallback (English), then the text the caller supplies (content.json's English for content keys), then the key  |
| Json | Json | Core | class | Athanor/Assets/Scripts/Core/Lang.cs | 212 |  |
| GoalReward | GoalReward | Core | class | Athanor/Assets/Scripts/Core/Profile.cs | 8 | A goal that has just been completed, and the locked item it gave (null when its pool was empty). |
| Profile | Profile | Core | class | Athanor/Assets/Scripts/Core/Profile.cs | 18 | What the player keeps between runs besides the journal: counters, completed goals and the locked starters and catalysts won from them. |
| Unlocks | Unlocks | Core | class | Athanor/Assets/Scripts/Core/Profile.cs | 154 | Which starting materials and catalysts a run may use. |
| RunOptions | RunOptions | Core | class | Athanor/Assets/Scripts/Core/Profile.cs | 206 | Switches for a run. The default is a Phase 2 run: no aspects, no events, and (with no Unlocks) only what the content leaves open. |
| AspectInstance | AspectInstance | Core | class | Athanor/Assets/Scripts/Core/RunStart.cs | 8 | An aspect as held by a run (or as offered): where it sits and, for an attuned tile, which kind it favours. |
| EventResult | EventResult | Core | class | Athanor/Assets/Scripts/Core/RunStart.cs | 16 | What taking an event option did, for the screen. |
| RunState | RunState | Core | class | Athanor/Assets/Scripts/Core/RunStart.cs | 23 | One run: descending through depths, spending trials, collecting catalysts. |
| IgniteOutcome | IgniteOutcome | Core | class | Athanor/Assets/Scripts/Core/RunState.cs | 9 |  |
| RunPhase | RunPhase | Core | enum | Athanor/Assets/Scripts/Core/RunState.cs | 7 |  |
| Rng | Rng | Core | class | Athanor/Assets/Scripts/Core/Satchel.cs | 7 | Small deterministic RNG so shuffles match between Unity and the dotnet test run. |
| Satchel | Satchel | Core | class | Athanor/Assets/Scripts/Core/Satchel.cs | 20 | The player's deck of starting materials: a draw pile, a discard pile, and the cards currently out. |
| ScoreParts | ScoreParts | Core | class | Athanor/Assets/Scripts/Core/ScoreParts.cs | 7 | One reaction's potency split into the factors the tutorial names: Total = Base x Used x First x Step x Bonus. Bonus is everything else (catalysts, the hub's tile, events); 1 when nothing else applied. |
| Settings | Settings | Core | class | Athanor/Assets/Scripts/Core/Settings.cs | 12 | Player settings: volumes, haptics and language. Unity-free; Game stores the JSON (PlayerPrefs, key athanor.settings.v1). Public fields so JsonUtility can read it too; ToJson/FromJson are hand-written  |
| Reader | Reader | Core | class | Athanor/Assets/Scripts/Core/Settings.cs | 62 |  |
| TutorialLay | TutorialLay | Core | struct | Athanor/Assets/Scripts/Core/Tutorial.cs | 13 | One material laid on one tile. This material goes on this tile, not just any lit tile: another arrangement of the same materials can break the chain (trial 3 then scores 40 and does not clear the dept |
| TutorialTrial | TutorialTrial | Core | class | Athanor/Assets/Scripts/Core/Tutorial.cs | 21 | One scripted trial: the hand dealt, where each material goes, and what the Ignite makes, in chain order. |
| TutorialScript | TutorialScript | Core | class | Athanor/Assets/Scripts/Core/Tutorial.cs | 32 | The tutorial (docs/ATHANOR.md §8, v2) as data. Every number the script shows is pinned by TutorialTests; change both together. |
| TutorialPlan | TutorialPlan | Core | enum | Athanor/Assets/Scripts/Core/Tutorial.cs | 6 |  |
| BenchView | BenchView | Game | class | Athanor/Assets/Scripts/Game/Bench/BenchView.cs | 14 | The main screen: nine tiles, the hand of cards, the rack of discoveries and the HUD. Materials are placed by dragging them onto a tile, or by clicking one and then a tile; clicking a placed one takes  |
| From | From | Game | enum | Athanor/Assets/Scripts/Game/Bench/BenchView.cs | 24 |  |
| CardView | CardView | Game | class | Athanor/Assets/Scripts/Game/Bench/CardView.cs | 7 | A starting material in the hand, drawn as a parchment card. |
| Dust | Dust | Game | class | Athanor/Assets/Scripts/Game/Bench/Dust.cs | 9 | Dust drifting down through the moonbeam: a loop, each mote's position a pure function of time, all of them drawn as one SparkMesh (one renderer) at order 8. |
| HudView | HudView | Game | class | Athanor/Assets/Scripts/Game/Bench/HudView.cs | 8 | The panels round the bench: depth and potency, catalysts, trials and redraws, the last-reaction note, journal and Ignite. |
| LinkView | LinkView | Game | class | Athanor/Assets/Scripts/Game/Bench/LinkView.cs | 7 | Motes of light travelling between neighbouring tiles whose reaction is already in the journal. |
| TileView | TileView | Game | class | Athanor/Assets/Scripts/Game/Bench/TileView.cs | 10 | One of the nine bench tiles: its hover highlight and, when occupied, the material standing on it. |
| GameFlow | GameFlow | Game | class | Athanor/Assets/Scripts/Game/Boot/GameFlow.cs | 15 | Owns the run and moves the game between its screens. |
| FlowState | FlowState | Game | enum | Athanor/Assets/Scripts/Game/Boot/GameFlow.cs | 12 |  |
| TitleChoice | TitleChoice | Game | enum | Athanor/Assets/Scripts/Game/Boot/GameFlow.cs | 131 |  |
| Save | Save | Game | class | Athanor/Assets/Scripts/Game/Boot/Save.cs | 8 | Keeps the journal and the profile between sessions in PlayerPrefs (browser storage in a web build). |
| Cutscene | Cutscene | Game | class | Athanor/Assets/Scripts/Game/Cutscene/Cutscene.cs | 7 | A sequence that plays to its end unless the player clicks to skip ahead. |
| DepthLook | DepthLook | Game | class | Athanor/Assets/Scripts/Game/Cutscene/DepthLook.cs | 9 | What a depth looks like from the bench: the intro line under its name and the view through the window. Pure choices, no scene; the pictures and effects are drawn by WindowView. |
| ReactionCutscene | ReactionCutscene | Game | class | Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs | 13 | The full discovery sequence: two materials spiral into the cauldron, the rune circle lights, a flash, and the new material rises on a column of light with its name and potency. Everything is a functio |
| SimpleCutscene | SimpleCutscene | Game | class | Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs | 14 | The short story beats built from text and a few effects: the intro, the descent between depths, and the ending. A click finishes the line being written, then moves on. |
| Loop | Loop | Game | class | Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs | 281 | A stream of glowing particles played as a loop instead of emitted: rate x life fixed slots, each reborn every life seconds at a hashed place and speed, so its position is a function of the clock. A sl |
| TutorialRules | TutorialRules | Game | class | Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs | 12 | The decisions around the tutorial that need no scene, so they can be tested. What Begin does is the Core rule TutorialScript.Plan. |
| TutorialController | TutorialController | Game | class | Athanor/Assets/Scripts/Game/Cutscene/TutorialController.cs | 33 | Plays the scripted tutorial over the bench: a dark screen with lit holes and a pulsing gold rim, an arrow, a text box at the top, click locks, and popups held until read. It reads the game's state; it |
| TutorialHole | TutorialHole | Game | class | Athanor/Assets/Scripts/Game/Cutscene/TutorialHole.cs | 10 | One lit window of the tutorial overlay: a SpriteMask that cuts a hole in the dim (the dim draws "visible outside mask"), and the four gold rim pieces round it. TutorialController keeps a few of these  |
| TutorialTargets | TutorialTargets | Game | class | Athanor/Assets/Scripts/Game/Cutscene/TutorialSettings.cs | 12 | Where the tutorial points. Art space: 640x360, x to the right, y down from the top-left corner. Every rectangle a step lights comes from here (or from the view it talks about: the Journal's pages, the |
| TutorialStyle | TutorialStyle | Game | class | Athanor/Assets/Scripts/Game/Cutscene/TutorialSettings.cs | 39 | How the tutorial overlay looks and feels: the text box, the popup lines, the rim and arrow, and their timings. |
| WindowView | WindowView | Game | class | Athanor/Assets/Scripts/Game/Cutscene/WindowView.cs | 11 | The workshop window shows what the current depth "sees" and plays one tiny looping effect (docs/ATHANOR.md §8): 1 dust motes up a shaft, 2 drips down the glass, 3 a breathing amber glow, 4 roots creep |
| CameraSway | CameraSway | Game | class | Athanor/Assets/Scripts/Game/Depth/CameraSway.cs | 11 | The idle sway and the world-only shake of the HD-2D bench. The sway moves the world camera (x = A sin(2 pi t / 6), y = A / 2 sin(2 pi t / 9), A = 4 art px) and re-aims it so the middle tile row (art y |
| DepthLayer | DepthLayer | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthLayer.cs | 10 | One flat thing of the bench placed in depth: it is moved to its depth and enlarged by (D - z) / D, so that from the camera at rest it covers exactly the art pixels it covers in the flat game. Sorting  |
| DepthMath | DepthMath | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthMath.cs | 10 | The projection maths of the HD-2D bench (HD-2D mockup SPEC, "Layer stack and projection"; removed in P6-22, see git history). Pure functions, no scene. Depth z follows the SPEC: 0 is the plane where o |
| DepthMood | DepthMood | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthMood.cs | 13 | The five depth presets of the HD-2D test scene (HD-2D mockup SPEC, "Depth presets", with the URP conversions of "Mapping mockup values to Unity"): ambient light, moonbeam, bloom, vignette, the fog she |
| Preset | Preset | Game | struct | Athanor/Assets/Scripts/Game/Depth/DepthMood.cs | 17 | One depth's look. |
| DepthRig | DepthRig | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthRig.cs | 12 | The camera split of the HD-2D bench. The world camera is a perspective camera at distance D from the art plane; it renders the layers under this object into a RenderTexture of 640K x 360K pixels (K =  |
| DepthStage | DepthStage | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthStage.cs | 10 | The HD-2D bench of the main scene as the game sees it: the rig (world camera, UI camera), the sway, the per-depth moods and the effects switch, all objects of Main.unity. GameFlow talks to this and no |
| DepthSwitch | DepthSwitch | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthSwitch.cs | 11 | The "all effects off" switch of the depth test scene. Off: the far layers show the original sharp sprites and the fog sheets and the foreground are hidden, so the world at rest is today's bench. On: t |
| SpriteSwap | SpriteSwap | Game | struct | Athanor/Assets/Scripts/Game/Depth/DepthSwitch.cs | 15 | A sprite that has a sharp picture and a pre-blurred one of the same centre. |
| FlameSwap | FlameSwap | Game | struct | Athanor/Assets/Scripts/Game/Depth/DepthSwitch.cs | 23 | A flame whose frames have a sharp set and a pre-blurred set. |
| DepthTable | DepthTable | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthTable.cs | 13 | The table as a floor: one mesh lying on the tilted plane of DepthMath.TableZ (plus the upright front face below the front edge), cut along every art row. A plain tilted quad would foreshorten the art, |
| DepthTestControls | DepthTestControls | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthTestControls.cs | 11 | Play-mode controls of the depth test scene only: the pointer hovers the nine tiles through the world camera (DepthRig.BenchPoint), 1..5 pick a depth preset (0 today's look), E toggles the effects, S t |
| DepthTiltShiftFeature | DepthTiltShiftFeature | Game | class | Athanor/Assets/Scripts/Game/Depth/DepthTiltShiftFeature.cs | 9 | A full-screen pass of the tilt-shift (one blur direction per feature) that runs only for cameras with an enabled TiltShift, so the HUD camera and the main game's camera are never blurred. |
| FogSheet | FogSheet | Game | class | Athanor/Assets/Scripts/Game/Depth/FogSheet.cs | 10 | A tiling fog sheet (a tiled SpriteRenderer with the "Athanor/Fog Lit" material) that drifts sideways in whole texels: the picture moves left by trunc(t * speed) texels (back sheet +4, front sheet -6 a |
| TiltShift | TiltShift | Game | class | Athanor/Assets/Scripts/Game/Depth/TiltShift.cs | 12 | The tilt-shift of the world camera: sharp from art y 145 to 235, then a blur that rises over 110 rows (smoothstep) to sigma 1.5 art px (owner gate G2 #9, #10). Drawn by DepthTiltShiftFeature before bl |
| Flame | Flame | Game | class | Athanor/Assets/Scripts/Game/Fx/Flame.cs | 7 | A flickering flame: cycles its frames and wavers the light and halo that go with it. |
| Flipbook | Flipbook | Game | class | Athanor/Assets/Scripts/Game/Fx/Flipbook.cs | 8 | Plays a FlipbookSheet on a SpriteRenderer. |
| FlipbookSheet | FlipbookSheet | Game | class | Athanor/Assets/Scripts/Game/Fx/FlipbookSheet.cs | 6 | One exported effect: its frames in order and how long each is shown. |
| Fx | Fx | Game | class | Athanor/Assets/Scripts/Game/Fx/Fx.cs | 6 | Spawns exported pixelfx effects by name. |
| ScreenFx | ScreenFx | Game | class | Athanor/Assets/Scripts/Game/Fx/ScreenFx.cs | 7 | Whole-screen effects: a white flash, camera shake, and fading to and from black. |
| SparkMesh | SparkMesh | Game | class | Athanor/Assets/Scripts/Game/Fx/SparkMesh.cs | 13 | Many 1 px dots and 5x5 sparkles drawn as one mesh at one sorting order: one renderer and at most two draws (one per texture) instead of a SpriteRenderer each. Fill it every frame with Begin, Add, End. |
| SparkParticles | SparkParticles | Game | class | Athanor/Assets/Scripts/Game/Fx/SparkParticles.cs | 13 | Sparks drawn by ParticleSystems (docs/ATHANOR.md §2), only compiled with ATHANOR_SPARK_PARTICLES. Not shipped: it pulls the ParticleSystem engine module into the wasm, which was the thing measured. On |
| Sparks | Sparks | Game | class | Athanor/Assets/Scripts/Game/Fx/Sparks.cs | 13 | Glowing pixel particles: bursts, trails and rising embers. Particles are plain structs integrated here; each sorting order is one lane drawn by one SparkMesh (one renderer, at most two draws), so a bu |
| Particle | Particle | Game | struct | Athanor/Assets/Scripts/Game/Fx/Sparks.cs | 20 |  |
| Lane | Lane | Game | class | Athanor/Assets/Scripts/Game/Fx/Sparks.cs | 25 |  |
| Anim | Anim | Game | class | Athanor/Assets/Scripts/Game/Support/Anim.cs | 11 | Beats (Wait), easing curves, and Over for per-frame scripts that are not a move or a fade (the spark trails in GameFlow). Moves, fades and scales of objects go through Motion (PrimeTween, snapped to p |
| Art | Art | Game | class | Athanor/Assets/Scripts/Game/Support/Art.cs | 4 | The art library the running game uses. GameFlow sets it from its serialized field; the Game tests set it in GameTestSetup. |
| ArtLibrary | ArtLibrary | Game | class | Athanor/Assets/Scripts/Game/Support/ArtLibrary.cs | 8 | Every sprite, effect sheet, font and material the game looks up by name. Built by Athanor > Refresh Art Library. |
| AudioMute | AudioMute | Game | class | Athanor/Assets/Scripts/Game/Support/AudioMute.cs | 11 | Silences audio while the browser tab is hidden, the app is paused or the window has lost focus. No sound ships in Phase 5, so this is a hook: it sets AudioListener.pause and is audible once sound land |
| AudioMuteHook | AudioMuteHook | Game | class | Athanor/Assets/Scripts/Game/Support/AudioMute.cs | 33 | Feeds the browser's tab visibility and Unity's pause and focus into AudioMute. |
| Clickable | Clickable | Game | class | Athanor/Assets/Scripts/Game/Support/Clickable.cs | 9 | A rectangle around this object that the pointer can hover and click. The highest order under the pointer wins. Holes (world-space rectangles) are cut out of it: the pointer there is not on this object |
| Dev | Dev | Game | class | Athanor/Assets/Scripts/Game/Support/Dev.cs | 9 | Log lines that exist only as the hook of tools/web/webtest.mjs ([tut], [drive], [loc]). The calls, and the strings they build, are compiled out of a release build; a development build and the editor k |
| Fonts | Fonts | Game | class | Athanor/Assets/Scripts/Game/Support/Fonts.cs | 10 | Which font a FontKind uses in the language showing, and the size and line box that go with it. |
| FontKind | FontKind | Game | enum | Athanor/Assets/Scripts/Game/Support/Fonts.cs | 7 |  |
| Haptics | Haptics | Game | class | Athanor/Assets/Scripts/Game/Support/Haptics.cs | 11 | Short vibration pulses for rare, meaningful moments (never clicks or Ignite). Only where the browser can vibrate (Android Chrome); Supported is false on desktop, iOS and in the editor, and the Setting |
| KindColors | KindColors | Game | class | Athanor/Assets/Scripts/Game/Support/KindColors.cs | 4 | The label colour for each kind of material. |
| Label | Label | Game | class | Athanor/Assets/Scripts/Game/Support/Label.cs | 11 | A line of pixel text with an optional one-pixel drop shadow. A label bound to a Lang key (Bind) rewrites itself when the language changes; every label swaps its font to the language's. |
| Loc | Loc | Game | class | Athanor/Assets/Scripts/Game/Support/Loc.cs | 12 | The language the game is showing: the loaded Lang (Vietnamese falls back to English), the font set that goes with it and helpers for the words views share. Views never hold text; they ask for a key. E |
| Motion | Motion | Game | class | Athanor/Assets/Scripts/Game/Support/Motion.cs | 16 | One-shot tweens on PrimeTween, kept in one place so every move is snapped to whole pixels (Px.Snap). Each call takes its start value explicitly and returns the Tween: group several with Sequence.Creat |
| PointerInput | PointerInput | Game | class | Athanor/Assets/Scripts/Game/Support/PointerInput.cs | 8 | Mouse or touch position in world space, press state for this frame, and hover/click dispatch to Clickables. |
| Px | Px | Game | class | Athanor/Assets/Scripts/Game/Support/Px.cs | 9 | The art is laid out on a 640x360 canvas with y running down from the top-left corner. The world uses one unit per pixel with the origin at the screen centre and y up. |
| RarityStars | RarityStars | Game | class | Athanor/Assets/Scripts/Game/Support/RarityStars.cs | 13 | The rarity glyph (SPEC changes 21-24): up to four 7x7 stars on an 8 px pitch, tinted, each with a dark outline made of four offset dark copies. The root sits at the top-left of the row. Built once and |
| Spawn | Spawn | Game | class | Athanor/Assets/Scripts/Game/Support/Spawn.cs | 13 | Run-time spawners: the objects the game makes while it plays (float numbers, fly-in icons, tutorial rects, link motes, rarity stars). Everything that is on screen from the start is a scene object or a |
| Align | Align | Game | enum | Athanor/Assets/Scripts/Game/Support/Spawn.cs | 7 |  |
| Wording | Wording | Game | class | Athanor/Assets/Scripts/Game/Support/Wording.cs | 9 | The words and colours the Phase 3 screens share: rarities, aspects, event effects and goal rewards. |
| ChoiceCard | ChoiceCard | Game | class | Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs | 11 | What one card of the chooser shows. |
| CatalystChoiceView | CatalystChoiceView | Game | class | Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs | 23 | Three parchment cards, pick one: a catalyst after a depth is cleared, or an aspect at the start of a run and of depths 3 and 5. An aspect's card shows the tile it would land on. |
| Card | Card | Game | class | Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs | 35 |  |
| EventView | EventView | Game | class | Athanor/Assets/Scripts/Game/Ui/EventView.cs | 13 | The event at the start of a depth: what was found, the options that can be taken (each with what it costs and may give), then what came of the one chosen. |
| Row | Row | Game | class | Athanor/Assets/Scripts/Game/Ui/EventView.cs | 19 |  |
| GoalRow | GoalRow | Game | class | Athanor/Assets/Scripts/Game/Ui/GoalRow.cs | 6 | One row of the Goals page: the goal and its count, then the reward and its progress bar. |
| GoalToast | GoalToast | Game | class | Athanor/Assets/Scripts/Game/Ui/GoalToast.cs | 11 | A small panel at the top of the screen that says a goal was completed and what it unlocked. Toasts queue; each stays 2.5 seconds or until clicked. It never blocks anything under it but itself. |
| GoalsView | GoalsView | Game | class | Athanor/Assets/Scripts/Game/Ui/GoalsView.cs | 12 | The Goals page: every long-term goal with its progress, grouped by the rarity of what it unlocks. Before a goal is done its reward is named only by kind and rarity; afterwards the item it gave is show |
| JournalCell | JournalCell | Game | class | Athanor/Assets/Scripts/Game/Ui/JournalCell.cs | 10 | One cell of the journal's grid: the material's icon with its click. An Uncommon or better starting material also gets a star at the cell's corner; that row is made when the cell is set up (5 of the 81 |
| JournalView | JournalView | Game | class | Athanor/Assets/Scripts/Game/Ui/JournalView.cs | 12 | The journal: every material as an icon (a silhouette until it has been made), and for the selected one the reactions found so far, how many are still unknown, and any hints collected. |
| PauseMenu | PauseMenu | Game | class | Athanor/Assets/Scripts/Game/Ui/PauseMenu.cs | 13 | The in-run pause: Resume, Vibration (only where the device can), How to play (greyed: title only) and Quit to title with a confirm. Time.timeScale is 0 while it is open; it is put back on every way ou |
| RunOverView | RunOverView | Game | class | Athanor/Assets/Scripts/Game/Ui/RunOverView.cs | 9 | The summary at the end of a run, won or lost. |
| TitleView | TitleView | Game | class | Athanor/Assets/Scripts/Game/Ui/TitleView.cs | 8 | The title, shown over the candlelit workshop. |
| Tooltip | Tooltip | Game | class | Athanor/Assets/Scripts/Game/Ui/Tooltip.cs | 9 | A small panel with a title and two lines of text that appears beside whatever the pointer rests on. A hand card's tip adds a row under the name with its stars and tier word, and the panel grows by tha |
| UiButton | UiButton | Game | class | Athanor/Assets/Scripts/Game/Ui/UiButton.cs | 7 | A 120x26 red button with a label: brightens under the pointer, sinks when pressed. |
| ArtLibraryBuilder | ArtLibraryBuilder | Editor | class | Athanor/Assets/Editor/ArtLibraryBuilder.cs | 12 | Collects the sprites, effect sheets, fonts and materials into Assets/Art/ArtLibrary.asset, the one lookup the game uses by name. Run it after new art or effect sheets are imported (the sprite and flip |
| AtlasBuilder | AtlasBuilder | Editor | class | Athanor/Assets/Editor/AtlasBuilder.cs | 27 | Two art steps that run after tools/art/export.mjs, both idempotent: 1. Sprite sheets: the numbered frames of each animation (Background/flame_small_0..7.png ...) are merged into one Multiple-mode shee |
| FlipbookImporter | FlipbookImporter | Editor | class | Athanor/Assets/Editor/FlipbookImporter.cs | 14 | Turns each pixelfx sheet in Assets/Art/Fx/Sheets (name.png + name.json, exported from pixelfx) into sliced sprites and a FlipbookSheet asset, then adds them to the art library so Fx.Spawn(name) can pl |
| FontBuilder | FontBuilder | Editor | class | Athanor/Assets/Editor/FontBuilder.cs | 10 | Bakes the pixel fonts into TextMeshPro bitmap font assets at their native size (crisp, point-filtered). |
| LayoutData | LayoutData | Editor | class | Athanor/Assets/Editor/LayoutData.cs | 11 |  |
| EditorGlobals | EditorGlobals | Editor | class | Athanor/Assets/Editor/Shared/EditorGlobals.cs | 10 | Sets the globals GameFlow and Sparks would set from their serialized fields at runtime (Art.Lib, Loc.Use, SparkMesh.BaseMaterial), loading the assets through the AssetDatabase. The batch tools (-execu |
| GoldenShots | GoldenShots | Editor | class | Athanor/Assets/Editor/Shared/GoldenShots.cs | 17 | The golden shots: one picture of every view in a fixed, seeded state, in English and Vietnamese. Each view is built the way its prefab test builds it (instantiate, Bind, Show / Open), never through Up |
| Reserialize | Reserialize | Editor | class | Athanor/Assets/Editor/Shared/Reserialize.cs | 11 | Rewrites every view prefab and the data assets in the current serializer format, so a field added to a component is stored with its value. Phase 7 uses it after a number moves from code to a prefab. M |
| ViewCapture | ViewCapture | Editor | class | Athanor/Assets/Editor/Shared/ViewCapture.cs | 8 | Renders a GameObject alone (in an otherwise empty scene) to a 640x360 image, the way the UI camera draws it. |
| SpriteImportRules | SpriteImportRules | Editor | class | Athanor/Assets/Editor/SpriteImportRules.cs | 10 | Every texture under Assets/Art is pixel art: a point-filtered, uncompressed sprite at 1 pixel per unit. The one exception is Assets/Art/Depth (the HD-2D layers): its pre-blurred pictures (*_far, *_nea |
| WebBuild | WebBuild | Editor | class | Athanor/Assets/Editor/WebBuild.cs | 10 | Builds the game for the browser into Build/web. Also callable from the command line: unity build Athanor --target WebGL --execute-method WebBuild.Build |
| ChainTests | ChainTests | Tests | class | Athanor/Assets/Tests/EditMode/ChainTests.cs | 8 | The chain planner on TestContent.Combo(): p 8, q 5, r 2, s 9, t 4, v 1 (Reaction Index). |
| ContentTests | ContentTests | Tests | class | Athanor/Assets/Tests/EditMode/ContentTests.cs | 9 |  |
| IgniteTests | IgniteTests | Tests | class | Athanor/Assets/Tests/EditMode/IgniteTests.cs | 8 |  |
| TestLang | TestLang | Tests | class | Athanor/Assets/Tests/EditMode/LangTests.cs | 10 | Loads the real language files (Resources/lang/en.json, vi.json), inside Unity or under plain dotnet. |
| LangTests | LangTests | Tests | class | Athanor/Assets/Tests/EditMode/LangTests.cs | 22 |  |
| LoreTests | LoreTests | Tests | class | Athanor/Assets/Tests/EditMode/LoreTests.cs | 9 | Lore events: the last one seen is not drawn again until another Lore event has been seen. |
| Phase3Tests | Phase3Tests | Tests | class | Athanor/Assets/Tests/EditMode/Phase3Tests.cs | 10 | Phase 3: the profile and its goals, unlocks, the new catalysts, bench rules, aspects, events and the start of a depth. |
| RarityReportTests | RarityReportTests | Tests | class | Athanor/Assets/Tests/EditMode/RarityReportTests.cs | 17 | Opt-in (Explicit) report: does each rarity pay? Same seeds, the informed bot, but at every catalyst offer it follows one policy: take the first offered catalyst of one rarity (else the bot's pick), a  |
| RarityTests | RarityTests | Tests | class | Athanor/Assets/Tests/EditMode/RarityTests.cs | 9 | Phase 5a-04: catalyst offers and event gifts draw by the optional content.json "rarityWeights" (Common..Legendary). |
| RoleSimTests | RoleSimTests | Tests | class | Athanor/Assets/Tests/EditMode/RoleSimTests.cs | 17 | Two simulated players on the full Phase 3 game (unlocks, aspects, events), each starting from an empty journal and a fresh profile and keeping both from run to run. Admin: knows every recipe and how a |
| RulesTests | RulesTests | Tests | class | Athanor/Assets/Tests/EditMode/RulesTests.cs | 8 |  |
| RunStateTests | RunStateTests | Tests | class | Athanor/Assets/Tests/EditMode/RunStateTests.cs | 9 |  |
| SatchelTests | SatchelTests | Tests | class | Athanor/Assets/Tests/EditMode/SatchelTests.cs | 8 |  |
| JournalTests | JournalTests | Tests | class | Athanor/Assets/Tests/EditMode/SatchelTests.cs | 45 |  |
| SettingsTests | SettingsTests | Tests | class | Athanor/Assets/Tests/EditMode/SettingsTests.cs | 6 |  |
| Sim | Sim | Tests | class | Athanor/Assets/Tests/EditMode/Sim.cs | 12 | The simulated players and shared helpers behind the balance tests and the console reports in tools/CoreReports. No NUnit here: it compiles in the Unity test assembly, in tools/CoreTests and in tools/C |
| Newcomer | Newcomer | Tests | class | Athanor/Assets/Tests/EditMode/Sim.cs | 73 |  |
| Career | Career | Tests | class | Athanor/Assets/Tests/EditMode/Sim.cs | 388 |  |
| RunStats | RunStats | Tests | class | Athanor/Assets/Tests/EditMode/Sim.cs | 486 | What happened over one scripted run, for the balance report. |
| TestContent | TestContent | Tests | class | Athanor/Assets/Tests/EditMode/TestContent.cs | 6 | Loads the real content.json, inside Unity or under plain dotnet. |
| TutorialTests | TutorialTests | Tests | class | Athanor/Assets/Tests/EditMode/TutorialTests.cs | 9 | Phase 4: the hooks the tutorial needs, and the tutorial script replayed with every number it shows. |
| AutoHookTests | AutoHookTests | Tests | class | Athanor/Assets/Tests/EditModeGame/AutoHookTests.cs | 12 | F9 (the scripted player) is a toggle, so tools/web/descents.mjs cannot know its state from its own key presses: a press at the wrong moment left the scripted player off after depth 2 and the run stood |
| ChoiceHookTests | ChoiceHookTests | Tests | class | Athanor/Assets/Tests/EditModeGame/ChoiceHookTests.cs | 14 | The card chooser logs its development hook only once the cards take a click (the deal is over). tools/web/webtest.mjs and descents.mjs click a card after this line; a click before it was lost and the  |
| DepthClickTests | DepthClickTests | Tests | class | Athanor/Assets/Tests/EditModeGame/DepthClickTests.cs | 13 | Clicks on the HD-2D bench: a point of the UI camera's world goes through the world texture and the perspective world camera to the table's tilted top, and the flat point found there is what TileView.C |
| DepthLayerTests | DepthLayerTests | Tests | class | Athanor/Assets/Tests/EditModeGame/DepthLayerTests.cs | 12 | The HD-2D projection maths (HD-2D mockup SPEC, "Layer stack and projection"; removed in P6-22, see git history) and the rules that keep the depth art out of the shipped game. |
| DepthLookTests | DepthLookTests | Tests | class | Athanor/Assets/Tests/EditModeGame/DepthLookTests.cs | 5 |  |
| GameTestSetup | GameTestSetup | Tests | class | Athanor/Assets/Tests/EditModeGame/GameTestSetup.cs | 7 | Once per run of the Game tests: the language files, art library and particle material the game would get from GameFlow and Sparks. |
| GoldenShotTests | GoldenShotTests | Tests | class | Athanor/Assets/Tests/EditModeGame/GoldenShotTests.cs | 7 | Every view, in both languages, still looks like the picture recorded in production/qa/evidence/phase7/baseline/golden. |
| HudTests | HudTests | Tests | class | Athanor/Assets/Tests/EditModeGame/HudTests.cs | 9 | The Redraw button is live only while a redraw is left and the bench is in play. |
| LabelLineBoxTests | LabelLineBoxTests | Tests | class | Athanor/Assets/Tests/EditModeGame/LabelLineBoxTests.cs | 15 | Line-box text (the Descent's depth name, SPEC (f)): the mockup sets it in a CSS line box as tall as the size, so the glyphs sit a half-leading below the box top; TMP puts the baseline one ascender dow |
| LocTests | LocTests | Tests | class | Athanor/Assets/Tests/EditModeGame/LocTests.cs | 12 | The Game layer's language: Vietnamese draws from SVN-Coder's Crux, every key resolves, and the core's English notes are translated. |
| MainDepthTests | MainDepthTests | Tests | class | Athanor/Assets/Tests/EditModeGame/MainDepthTests.cs | 11 | The HD-2D bench in the main scene (5b-07a): layers, the tiles' depth group, clicks through the world ray, the shake. |
| MainDevHookTests | MainDevHookTests | Tests | class | Athanor/Assets/Tests/EditModeGame/MainDevHookTests.cs | 11 | The dev-only hooks tools/web/mainhd2d.mjs drives (5b-07b): F6 mood steps, F10 tile faces in UI units, the bench's place / unplace lines. The key handling is guarded by Application.isEditor \|\| Debug.is |
| MainSceneFixture | MainSceneFixture | Tests | class | Athanor/Assets/Tests/EditModeGame/MainSceneFixture.cs | 14 | Main.unity opened beside the test scene, for the views that are scene objects and not prefabs. Dispose closes it without saving. |
| MotionTests | MotionTests | Tests | class | Athanor/Assets/Tests/EditModeGame/MotionTests.cs | 13 | Motion's helpers make live PrimeTween tweens. PrimeTween refuses a payload over 16 bytes: it logs an error and returns a dead tween, which a Sequence then refuses too (the rack flight and the catalyst |
| PauseTests | PauseTests | Tests | class | Athanor/Assets/Tests/EditModeGame/PauseTests.cs | 10 | The pause menu: time stops and comes back on every exit, Esc toggles, nothing opens in the tutorial, Quit leaves the stored run data alone. |
| PrefabBakeTests | PrefabBakeTests | Tests | class | Athanor/Assets/Tests/EditModeGame/PrefabBakeTests.cs | 13 | The shipped prefabs keep what the views need: a label keeps its key, a button still clicks, the tooltip makes its panel. |
| PrefabFixture | PrefabFixture | Tests | class | Athanor/Assets/Tests/EditModeGame/PrefabFixture.cs | 9 | A copy of a prefab for one test. Call Destroy in [TearDown] (or a finally). |
| RunOverDepthTests | RunOverDepthTests | Tests | class | Athanor/Assets/Tests/EditModeGame/RunOverDepthTests.cs | 15 | The run-over depth row (owner, phase-5-prep 2026-11-16): a won run says "Planted at depth n" on one line with no value; a lost run says "Depth reached" with "n of 5". The view is reused, so each path  |
| SaveTests | SaveTests | Tests | class | Athanor/Assets/Tests/EditModeGame/SaveTests.cs | 11 | The journal and the profile survive a round trip through PlayerPrefs, and a damaged save never stops the game. |
| SceneBakeTests | SceneBakeTests | Tests | class | Athanor/Assets/Tests/EditModeGame/SceneBakeTests.cs | 11 | The scene objects of Main.unity that are not prefabs (Task 15): their serialized parts are all there, and Bind finishes them. |
| SettingsHapticsTests | SettingsHapticsTests | Tests | class | Athanor/Assets/Tests/EditModeGame/SettingsHapticsTests.cs | 9 | Saved settings (athanor.settings.v1), the haptics plan and the mute-on-hidden-tab hook. |
| SparksTests | SparksTests | Tests | class | Athanor/Assets/Tests/EditModeGame/SparksTests.cs | 9 | Sparks on batched meshes: same motion and fade as the old SpriteRenderer pool, whole-pixel centres, HDR kept, no garbage. |
| TutorialFlowTests | TutorialFlowTests | Tests | class | Athanor/Assets/Tests/EditModeGame/TutorialFlowTests.cs | 11 | The tutorial's decisions, its sandbox and the scores it shows (the numbers TutorialTests pins in Core). |

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
| BenchRules | method | List | 20 | public readonly List<(int a, int b)> Links = new List<(int, int)>() |
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
| Rarities | field | Common | 90 | public const string Common = "Common", Uncommon = "Uncommon", Rare = "Rare", Legendary = |
| Rarities | property | All | 91 | public static readonly string[] All = |
| Rarities | method | Rank | 92 | public static int Rank(string rarity) |
| Rarities | method | Of | 93 | public static string Of(string rarity) |
| Effects | field | KindDouble | 99 | public const string KindDouble = "KindDouble", CentreTwice = "CentreTwice", FirstTriple = "FirstTriple", Diagonals = "Diagonals", ExtraTrial = "ExtraTrial", ExtraHand = "ExtraHand", DiscoveryTriple =  |
| Effects | property | All | 107 | public static readonly string[] All = |
| AspectEffects | field | Doubled | 118 | public const string Doubled = "Doubled", Anchor = "Anchor", Heated = "Heated", Echo = "Echo", Attuned = "Attuned", Seed = "Seed", Fallow = "Fallow", Heavy = "Heavy", Cold = "Cold", Volatile = "Volatil |
| AspectEffects | property | All | 121 | public static readonly string[] All = |
| Ops | field | Trials | 130 | public const string Trials = "Trials", Hand = "Hand", Redraws = "Redraws", RiShift = "RiShift", GiveDiscovery = "GiveDiscovery", LoseRack = "LoseRack", LoseCard = "LoseCard", FirstTriple = "FirstTripl |
| Ops | property | All | 133 | public static readonly string[] All = |
| Ops | field | NeedRack | 138 | public const string NeedRack = "Rack", NeedCatalyst = "Catalyst", NeedTrial = "Trial", NeedRedraw = |
| Ops | property | Needs | 139 | public static readonly string[] Needs = |
| Ops | property | Types | 140 | public static readonly string[] Types = |
| StatIds | field | RunsStarted | 146 | public const string RunsStarted = "runsStarted", RunsWon = "runsWon", WonNoRedraw = "wonNoRedraw", DepthReached = "depthReached", Ignites = "ignites", ChainLongest = "chainLongest", ComboLargest = "co |
| StatIds | property | All | 149 | public static readonly string[] All = |
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
| EventResult | field | Notes | 19 | public readonly List<string> Notes = new List<string>() |
| RunState | property | LastLore | 38 | public string LastLore |
| RunState | property | Aspects | 40 | public IReadOnlyList<AspectInstance> Aspects |
| RunState | property | AspectOffer | 41 | public IReadOnlyList<AspectInstance> AspectOffer |
| RunState | property | CurrentEvent | 42 | public EventData CurrentEvent |
| RunState | property | EventOptions | 44 | public IReadOnlyList<EventOption> EventOptions |
| RunState | property | Rules | 45 | public BenchRules Rules |
| RunState | property | RiShift | 47 | public int RiShift |
| RunState | property | TripleFirst | 48 | public bool TripleFirst |
| RunState | property | HandDelta | 50 | public int HandDelta |
| RunState | method | ChooseAspect | 136 | public void ChooseAspect(string id) |
| RunState | method | ChooseEventOption | 218 | public EventResult ChooseEventOption(int index) |
| RunState | property | DepthBlocked | 400 | public IReadOnlyList<int> DepthBlocked |
| RunState | property | DepthHeated | 401 | public IEnumerable<int> DepthHeated |
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
| BenchView | field | JournalRequested | 16 | public event Action IgniteRequested, RedrawRequested, JournalRequested |
| BenchView | property | InputEnabled | 17 | public bool InputEnabled |
| BenchView | property | Hud | 18 | public HudView Hud |
| BenchView | property | TileCount | 20 | public int TileCount |
| BenchView | property | Pick | 25 | struct Pick |
| BenchView | method | Bind | 50 | public void Bind(ContentDb content) |
| BenchView | method | Bind | 62 | public void Bind(RunState state, Journal book) |
| BenchView | method | TileCentre | 70 | public Vector3 TileCentre(int i) |
| BenchView | method | RackSlot | 72 | public Vector3 RackSlot(int i) |
| BenchView | method | PreviewOnRack | 74 | public void PreviewOnRack(int i, string id) |
| BenchView | method | Tile | 79 | public TileView Tile(int i) |
| BenchView | method | Refresh | 82 | public void Refresh(bool deal = false) |
| BenchView | method | ClearLinks | 162 | public void ClearLinks() |
| BenchView | method | FloatText | 165 | public IEnumerator FloatText(string text, Vector3 from, Color color, float seconds = 0.8f, bool small = false) |
| BenchView | method | PlaceLine | 214 | public static string PlaceLine(int tile) |
| BenchView | method | UnplaceLine | 215 | public static string UnplaceLine(int tile) |
| From | value | None | 24 | None |
| From | value | Hand | 24 | Hand |
| From | value | Tile | 24 | Tile |
| CardView | field | W | 9 | public const int W = 74, H = 70 |
| CardView | field | handIndex | 10 | public int handIndex |
| CardView | field | id | 11 | public string id |
| CardView | field | home | 12 | public Vector3 home |
| CardView | field | Dimmed | 13 | public bool Lifted, Selected, Dimmed |
| CardView | method | Show | 24 | public void Show(MaterialData m, int order) |
| CardView | method | Fan | 45 | public void Fan(float visible, int front) |
| CardView | method | DealIn | 70 | public void DealIn(float wait) |
| CardView | method | Contains | 72 | public bool Contains(Vector2 world) |
| Dust | field | count | 11 | public int count = 46 |
| Dust | field | material | 13 | public Material material |
| Dust | field | sprite | 14 | public Sprite sprite |
| HudView | field | PauseClicked | 10 | public event Action IgniteClicked, JournalClicked, RedrawClicked, PauseClicked |
| HudView | property | HoveredCatalyst | 12 | public int HoveredCatalyst |
| HudView | property | RedrawHovered | 13 | public bool RedrawHovered |
| HudView | property | PauseHovered | 15 | public bool PauseHovered |
| HudView | field | PauseX | 19 | public const int PauseX = 12, PauseY = 152, PauseW = 28, PauseH = 20, PauseOrder = 650 |
| HudView | property | HoveredAspect | 23 | public int HoveredAspect |
| HudView | field | MaxCatalysts | 25 | public const int MaxCatalysts = 6, MaxAspects = 3 |
| HudView | field | RedrawX | 27 | public const int RedrawX = 556, RedrawY = 88, RedrawW = 76, RedrawH = 20 |
| HudView | method | SetPauseShown | 55 | public void SetPauseShown(bool shown) |
| HudView | property | PauseShown | 61 | public bool PauseShown |
| HudView | property | PauseAnchor | 62 | public Vector3 PauseAnchor |
| HudView | property | PotencyPosition | 64 | public Vector3 PotencyPosition |
| HudView | method | CatalystAnchor | 66 | public Vector3 CatalystAnchor(int i) |
| HudView | method | AspectAnchor | 67 | public Vector3 AspectAnchor(int i) |
| HudView | property | RedrawAnchor | 69 | public Vector3 RedrawAnchor |
| HudView | method | CanRedraw | 71 | public static bool CanRedraw(RunState run) |
| HudView | method | Number | 72 | public static string Number(long n) |
| HudView | method | Bind | 77 | public void Bind() |
| HudView | method | Show | 117 | public void Show(RunState run, ContentDb db, Journal journal, bool inputEnabled, bool keepRedraw = false) |
| HudView | method | SetPotency | 165 | public void SetPotency(long value, bool instant = false) |
| HudView | method | Note | 168 | public void Note(ReactionEvent e, ContentDb db) |
| HudView | method | Hint | 192 | public void Hint(HintData h, ContentDb db) |
| HudView | method | HintLine | 199 | public static string HintLine(HintData h, ContentDb db, bool withName = true) |
| HudView | method | Bump | 207 | public void Bump() |
| HudView | method | ClearNote | 209 | public void ClearNote() |
| LinkView | field | color | 11 | public Color color = new Color(2.2f, 1.7f, 0.8f) |
| LinkView | field | speed | 12 | public float speed = 0.35f, arc = 7, alpha = 0.9f |
| LinkView | method | Set | 16 | public void Set(List<(Vector3 a, Vector3 b)> pairs) |
| TileView | field | index | 12 | public int index |
| TileView | field | centre | 13 | public Vector3 centre |
| TileView | property | Material | 22 | public string Material |
| TileView | property | Hovered | 23 | public bool Hovered |
| TileView | property | Pinged | 25 | public bool Pinged |
| TileView | property | Shut | 27 | public bool Shut |
| TileView | property | Marked | 28 | public bool Marked |
| TileView | property | Icon | 29 | public SpriteRenderer Icon |
| TileView | property | FaceCentre | 32 | public Vector2 FaceCentre |
| TileView | property | FaceSize | 33 | public Vector2 FaceSize |
| TileView | method | Contains | 35 | public bool Contains(Vector2 world) |
| TileView | method | SetState | 39 | public void SetState(Color? marker, bool shut) |
| TileView | method | Show | 49 | public void Show(MaterialData m, bool pop) |
| TileView | method | Clear | 62 | public void Clear() |
| TileView | method | Flash | 70 | public void Flash(Color c) |
| TileView | method | Consume | 73 | public IEnumerator Consume() |
| GameFlow | field | library | 18 | public ArtLibrary library |
| GameFlow | field | cutsceneRoom | 21 | public GameObject workshop, cutsceneRoom |
| GameFlow | field | mainCamera | 22 | public Camera mainCamera |
| GameFlow | field | depthStage | 24 | public DepthStage depthStage |
| GameFlow | property | Db | 26 | public ContentDb Db |
| GameFlow | property | Journal | 27 | public Journal Journal |
| GameFlow | property | Profile | 29 | public Profile Profile |
| GameFlow | property | Run | 30 | public RunState Run |
| GameFlow | property | Bench | 32 | public BenchView Bench |
| GameFlow | property | State | 33 | public FlowState State |
| GameFlow | property | RunOver | 36 | public RunOverView RunOver |
| GameFlow | field | Auto | 38 | public bool Auto |
| GameFlow | field | AutoHook | 41 | public const string AutoHook = |
| GameFlow | method | ToggleAuto | 44 | public void ToggleAuto() |
| GameFlow | method | NextMood | 53 | public static int NextMood(int mood) |
| GameFlow | method | MoodLine | 54 | public static string MoodLine(int mood) |
| GameFlow | method | EffectsLine | 55 | public static string EffectsLine(bool on) |
| GameFlow | property | JournalView | 117 | public JournalView JournalView |
| GameFlow | method | JournalAfterQuit | 455 | public static Journal JournalAfterQuit(bool sandbox, Journal current) |
| GameFlow | method | ProfileAfterQuit | 457 | public static Profile ProfileAfterQuit(bool sandbox, Profile current, ContentDb db) |
| GameFlow | method | Ignite | 498 | public void Ignite() |
| GameFlow | method | SettleIgnite | 506 | public static IgniteOutcome SettleIgnite(RunState run, Journal journal, bool sandbox) |
| FlowState | value | Boot | 12 | Boot |
| FlowState | value | Title | 12 | Title |
| FlowState | value | Intro | 12 | Intro |
| FlowState | value | Bench | 12 | Bench |
| FlowState | value | Resolving | 12 | Resolving |
| FlowState | value | Journal | 12 | Journal |
| FlowState | value | Descent | 12 | Descent |
| FlowState | value | CatalystChoice | 12 | CatalystChoice |
| FlowState | value | Ending | 12 | Ending |
| FlowState | value | RunOver | 12 | RunOver |
| FlowState | value | AspectChoice | 12 | AspectChoice |
| FlowState | value | Event | 12 | Event |
| FlowState | value | Goals | 12 | Goals |
| TitleChoice | value | Begin | 131 | Begin |
| TitleChoice | value | HowToPlay | 131 | HowToPlay |
| TitleChoice | value | Intro | 131 | Intro |
| Save | field | Key | 10 | public const string Key = "athanor.journal.v1", IntroKey = "athanor.intro.v1", ProfileKey = "athanor.profile.v1", TutorialKey = "athanor.tutorial.v1", SettingsKey = |
| Save | method | LoadJournal | 14 | public static Journal LoadJournal() |
| Save | method | StoreJournal | 24 | public static void StoreJournal(Journal journal) |
| Save | method | LoadProfile | 34 | public static Profile LoadProfile(ContentDb db) |
| Save | method | StoreProfile | 45 | public static void StoreProfile(Profile profile) |
| Save | method | ResetProfile | 52 | public static void ResetProfile() |
| Save | method | LoadSettings | 59 | public static Settings LoadSettings() |
| Save | method | StoreSettings | 64 | public static void StoreSettings(Settings settings) |
| Save | property | IntroSeen | 76 | public static bool IntroSeen |
| Save | property | TutorialDone | 83 | public static bool TutorialDone |
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
| ReactionCutscene | method | Bind | 41 | public void Bind(GameObject[] others) |
| ReactionCutscene | method | Setup | 54 | public void Setup(ReactionEvent e, ContentDb content, float hurry = 1) |
| ReactionCutscene | method | Play | 80 | public override IEnumerator Play() |
| SimpleCutscene | method | Play | 33 | public override IEnumerator Play() |
| SimpleCutscene | method | Close | 50 | public void Close() |
| SimpleCutscene | method | Intro | 69 | public IEnumerator Intro() |
| SimpleCutscene | method | Descent | 105 | public IEnumerator Descent(int depth, string depthName, Color colour, Action midway) |
| SimpleCutscene | method | DepthIntro | 164 | public IEnumerator DepthIntro(string key) |
| SimpleCutscene | method | Ending | 180 | public IEnumerator Ending(MaterialData ultimate) |
| Loop | ctor | Loop | 288 | public Loop(Transform parent, string name, int order, float rate, float life, float x0, float x1, float y0, float y1, float vx0, float vx1, float vy0, float vy1, float gravity, float sparkle) |
| Loop | method | Draw | 299 | public void Draw(float t, float strength, Color color) |
| TutorialRules | method | MarkDone | 15 | public static void MarkDone() |
| TutorialRules | method | AnswerSkipQuestion | 21 | public static bool AnswerSkipQuestion(bool playIt) |
| TutorialController | property | Trial | 55 | public int Trial |
| TutorialController | property | Finished | 57 | public bool Finished |
| TutorialController | field | Shown | 59 | public readonly List<long> Shown = new List<long>() |
| TutorialController | method | Bind | 62 | public void Bind(GameFlow flow) |
| TutorialController | method | Union | 74 | public static Rect Union(Rect[] r) |
| TutorialController | method | Abort | 146 | public void Abort() |
| TutorialController | method | AfterEvent | 236 | public IEnumerator AfterEvent(ReactionEvent e, int index) |
| TutorialController | method | AfterIgnite | 276 | public IEnumerator AfterIgnite(IgniteOutcomeView o) |
| TutorialController | method | AfterChoose | 301 | public void AfterChoose(string catalystId, string name) |
| TutorialController | method | OnOffer | 311 | public IEnumerator OnOffer() |
| TutorialController | method | OfferTaken | 318 | public void OfferTaken() |
| TutorialController | method | Drive | 329 | public IEnumerator Drive() |
| TutorialController | property | IgniteOutcomeView | 379 | public sealed class IgniteOutcomeView |
| TutorialHole | method | Open | 18 | public void Open(Rect r, bool showRim) |
| TutorialHole | method | Close | 30 | public void Close() |
| TutorialHole | method | SetRimAlpha | 33 | public void SetRimAlpha(float a) |
| TutorialTargets | field | hand | 16 | public Rect hand = new Rect(126, 284, 392, 80) |
| TutorialTargets | field | ignite | 18 | public Rect ignite = new Rect(515, 304, 122, 42) |
| TutorialTargets | field | journal | 20 | public Rect journal = new Rect(516, 284, 122, 24) |
| TutorialTargets | field | rack | 22 | public Rect rack = new Rect(126, 252, 392, 32) |
| TutorialTargets | field | offer | 24 | public Rect offer = new Rect(0, 60, 640, 240) |
| TutorialTargets | field | tileSize | 28 | public Vector2 tileSize = new Vector2(72, 44) |
| TutorialTargets | field | tileShift | 30 | public Vector2 tileShift = new Vector2(36, 24) |
| TutorialTargets | field | redrawMargin | 34 | public float redrawMargin = 2 |
| TutorialStyle | field | baseOrder | 43 | public int baseOrder = 600 |
| TutorialStyle | field | popupOrderOffset | 44 | public int popupOrderOffset = 12 |
| TutorialStyle | field | brokenOrderOffset | 45 | public int brokenOrderOffset = 6 |
| TutorialStyle | field | boxPos | 49 | public Vector2 boxPos = new Vector2(168, 14) |
| TutorialStyle | field | boxWidth | 50 | public float boxWidth = 304 |
| TutorialStyle | field | boxBorder | 52 | public float boxBorder = 1 |
| TutorialStyle | field | textInset | 54 | public Vector2 textInset = new Vector2(8, 7) |
| TutorialStyle | field | textWrapWidth | 56 | public float textWrapWidth = 288 |
| TutorialStyle | field | boxPadding | 58 | public float boxPadding = 12 |
| TutorialStyle | field | readExtra | 59 | public float readExtra = 12 |
| TutorialStyle | field | boxMaxHeight | 61 | public float boxMaxHeight = 86 |
| TutorialStyle | field | linePitch | 63 | public int linePitch = 12, maxLines = 4 |
| TutorialStyle | field | linePitchVi | 65 | public int linePitchVi = 16, maxLinesVi = 3 |
| TutorialStyle | field | journalBoxDrop | 67 | public int journalBoxDrop = 248 |
| TutorialStyle | field | popupPos | 71 | public Vector2 popupPos = new Vector2(320, 70) |
| TutorialStyle | field | popupPitch | 72 | public float popupPitch = 20 |
| TutorialStyle | field | popupFontSize | 73 | public int popupFontSize = 16 |
| TutorialStyle | field | popupColor | 74 | public Color popupColor = new Color32(0xf0, 0xdc, 0xc0, 255) |
| TutorialStyle | field | popupTotalColor | 76 | public Color popupTotalColor = new Color32(0xff, 0xf6, 0xb8, 255) |
| TutorialStyle | field | popupShadow | 77 | public Color popupShadow = new Color32(0x0c, 0x07, 0x06, 255) |
| TutorialStyle | field | brokenGhost | 81 | public Color brokenGhost = new Color(0.45f, 0.45f, 0.45f, 0.35f) |
| TutorialStyle | field | clickGuard | 89 | public float clickGuard = 0.25f |
| TutorialStyle | field | rimSpeed | 94 | public float rimSpeed = 5 |
| TutorialStyle | field | bumpDecay | 96 | public float bumpDecay = 3 |
| TutorialStyle | field | arrowAbove | 100 | public float arrowAbove = 10, arrowBelow = 8 |
| TutorialStyle | field | arrowFlipY | 102 | public float arrowFlipY = 80 |
| TutorialStyle | field | arrowBobSpeed | 103 | public float arrowBobSpeed = 6, arrowBobAmplitude = 2 |
| WindowView | method | Bind | 28 | public void Bind() |
| WindowView | property | Soft | 37 | public bool Soft |
| WindowView | method | Show | 50 | public void Show(int depth) |
| WindowView | method | ShowNight | 64 | public void ShowNight() |
| CameraSway | field | swayOn | 14 | public bool swayOn = true |
| CameraSway | field | amplitude | 16 | public float amplitude = DepthMath.SwayAmplitude |
| CameraSway | field | external | 20 | public Vector2 external |
| CameraSway | method | Shake | 25 | public void Shake(int size, float seconds) |
| CameraSway | method | ShakeOffset | 33 | public static Vector2Int ShakeOffset(float t, int size) |
| CameraSway | method | Pose | 37 | public void Pose(float t) |
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
| DepthMath | field | RestDistance | 13 | public const float RestDistance = 600f |
| DepthMath | field | TableFront | 15 | public const float TableFront = 660f |
| DepthMath | field | TableBack | 17 | public const float TableBack = 1.27f * TableFront |
| DepthMath | field | TableTopY | 19 | public const float TableTopY = 132f, TableRows = 122f |
| DepthMath | field | AnchorY | 21 | public const float AnchorY = 188f |
| DepthMath | method | Scale | 24 | public static float Scale(float z, float d = RestDistance) |
| DepthMath | method | FovDegrees | 27 | public static float FovDegrees(float d = RestDistance, float halfHeight = Px.H / 2f) |
| DepthMath | method | Place | 30 | public static Vector3 Place(Vector2 rest, float z, float d = RestDistance) |
| DepthMath | method | Seen | 37 | public static Vector2 Seen(Vector3 world, float d = RestDistance) |
| DepthMath | method | Project | 45 | public static Vector2 Project(Vector3 world, Vector2 cam, Vector2 shift, float d = RestDistance) |
| DepthMath | method | DrawnFace | 55 | public static Vector2[] DrawnFace(Vector2 faceCentre, Vector2 faceSize, float artY, Vector2 cam, Vector2 shift, float inset, float d = RestDistance) |
| DepthMath | method | TableZ | 72 | public static float TableZ(float artY, float d = RestDistance, float front = TableFront, float back = TableBack, float topY = TableTopY, float rows = TableRows) |
| DepthMath | method | Parallax | 83 | public static float Parallax(float z, float zAnchor, float d = RestDistance) |
| DepthMath | field | SwayAmplitude | 86 | public const float SwayAmplitude = 4f, SwayPeriodX = 6f, SwayPeriodY = 9f |
| DepthMath | method | Sway | 89 | public static Vector2 Sway(float t, float amplitude = SwayAmplitude) |
| DepthMath | method | AnchorShift | 96 | public static Vector2 AnchorShift(Vector2 cam, float zAnchor, float d = RestDistance) |
| DepthMath | method | Projection | 102 | public static Matrix4x4 Projection(float d, Vector2 shift, float near, float far) |
| DepthMath | method | ViewportRay | 114 | public static Ray ViewportRay(Vector2 viewport, Vector2 cam, Vector2 shift, float d = RestDistance) |
| DepthMath | method | HitTable | 124 | public static bool HitTable(Ray ray, out Vector2 flat, float d = RestDistance, float front = TableFront, float back = TableBack, float topY = TableTopY, float rows = TableRows) |
| DepthMood | field | presets | 36 | public Preset[] presets = Defaults() |
| DepthMood | field | depth | 38 | public int depth |
| DepthMood | field | moon | 39 | public Light2D ambient, moon |
| DepthMood | field | volume | 40 | public Volume volume |
| DepthMood | field | frontFog | 41 | public SpriteRenderer backFog, frontFog |
| DepthMood | field | effects | 43 | public DepthSwitch effects |
| DepthMood | field | windowSwap | 44 | public int windowSwap |
| DepthMood | field | windowNight | 46 | public Sprite windowNight |
| DepthMood | field | window | 48 | public WindowView window |
| DepthMood | field | windowSharp | 49 | public Sprite[] windowSharp = new Sprite[0], windowSoft = new Sprite[0] |
| DepthMood | method | Defaults | 53 | public static Preset[] Defaults() |
| DepthMood | method | Set | 73 | public void Set(int d) |
| DepthMood | method | Set | 76 | public void Set(int d, bool themedWindow) |
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
| DepthRig | field | distance | 23 | public float distance = DepthMath.RestDistance |
| DepthRig | field | worldCamera | 27 | public Camera worldCamera |
| DepthRig | field | uiCamera | 29 | public Camera uiCamera |
| DepthRig | field | worldDisplay | 31 | public MeshRenderer worldDisplay |
| DepthRig | field | autoScale | 33 | public bool autoScale = true |
| DepthRig | field | near | 35 | public float near = 50f, far = 2000f |
| DepthRig | property | Flat | 45 | public bool Flat |
| DepthRig | method | ScreenScale | 59 | public static int ScreenScale() |
| DepthRig | property | WorldTexture | 62 | public RenderTexture WorldTexture |
| DepthRig | property | RestPosition | 65 | public Vector3 RestPosition |
| DepthRig | property | CamOffset | 68 | public Vector2 CamOffset |
| DepthRig | property | ImageShift | 70 | public Vector2 ImageShift |
| DepthRig | property | AnchorZ | 72 | public float AnchorZ |
| DepthRig | method | SetView | 78 | public void SetView(Vector2 cam, Vector2 shake) |
| DepthRig | method | ViewportOf | 86 | public static Vector2 ViewportOf(Vector2 ui) |
| DepthRig | method | WorldRay | 89 | public Ray WorldRay(Vector2 ui) |
| DepthRig | method | BenchPoint | 92 | public bool BenchPoint(Vector2 ui, out Vector2 flat) |
| DepthRig | method | Apply | 123 | public void Apply() |
| DepthStage | field | rig | 12 | public DepthRig rig |
| DepthStage | field | sway | 13 | public CameraSway sway |
| DepthStage | field | mood | 14 | public DepthMood mood |
| DepthStage | field | effects | 15 | public DepthSwitch effects |
| DepthStage | field | window | 16 | public WindowView window |
| DepthStage | method | PlaceTiles | 19 | public void PlaceTiles(BenchView bench, int count) |
| DepthStage | method | SetFlat | 31 | public void SetFlat(bool flat) |
| DepthStage | method | ShowMood | 34 | public void ShowMood(int depth, bool themedWindow) |
| DepthStage | property | SwayOn | 40 | public bool SwayOn |
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
| DepthTable | field | distance | 22 | public float distance = DepthMath.RestDistance |
| DepthTable | field | front | 24 | public float front = DepthMath.TableFront, back = DepthMath.TableBack |
| DepthTable | field | topY | 26 | public float topY = DepthMath.TableTopY, rows = DepthMath.TableRows |
| DepthTable | method | RowZ | 40 | public float RowZ(float y) |
| DepthTable | method | Point | 43 | public Vector3 Point(float x, float y) |
| DepthTable | method | Rebuild | 46 | public void Rebuild(float d) |
| DepthTestControls | field | rig | 13 | public DepthRig rig |
| DepthTestControls | field | sway | 14 | public CameraSway sway |
| DepthTestControls | field | effects | 15 | public DepthSwitch effects |
| DepthTestControls | field | mood | 16 | public DepthMood mood |
| DepthTestControls | field | tiles | 17 | public TileView[] tiles = new TileView[0] |
| DepthTestControls | property | Hovered | 19 | public int Hovered |
| DepthTiltShiftFeature | method | AddRenderPasses | 12 | public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) |
| FogSheet | field | speed | 15 | public float speed = 4f |
| FogSheet | field | width | 17 | public int width = 320 |
| FogSheet | method | Offset | 25 | public static int Offset(float t, float speed, int width) |
| FogSheet | method | Set | 28 | public void Set(float t) |
| TiltShift | field | material | 18 | public Material material |
| TiltShift | field | bandTop | 20 | public float bandTop = 145f, bandBottom = 235f |
| TiltShift | field | feather | 22 | public float feather = 110f |
| TiltShift | field | maxSigma | 24 | public float maxSigma = 1.5f |
| TiltShift | method | IsOn | 27 | public static bool IsOn(Camera camera) |
| TiltShift | method | Sigma | 30 | public static float Sigma(float artY, float top, float bottom, float feather, float maxSigma) |
| TiltShift | method | Push | 42 | public void Push() |
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
| ScreenFx | property | I | 9 | public static ScreenFx I |
| ScreenFx | method | Bind | 19 | public void Bind(Camera camera, CameraSway worldSway = null) |
| ScreenFx | method | Flash | 27 | public void Flash(float peak, float seconds) |
| ScreenFx | method | Shake | 28 | public void Shake(float pixels, float seconds) |
| ScreenFx | method | FadeOut | 30 | public IEnumerator FadeOut(float seconds) |
| ScreenFx | method | FadeIn | 31 | public IEnumerator FadeIn(float seconds) |
| ScreenFx | method | SetBlack | 32 | public void SetBlack(float a) |
| SparkMesh | property | Vert | 15 | struct Vert |
| SparkMesh | property | Quad | 16 | struct Quad |
| SparkMesh | field | DotScale | 19 | public const float DotScale = 0.25f |
| SparkMesh | property | BaseMaterial | 32 | public static Material BaseMaterial |
| SparkMesh | property | Count | 48 | public int Count |
| SparkMesh | method | Create | 51 | public static SparkMesh Create(Transform parent, string name, int order, Sprite dot, Sprite sparkle) |
| SparkMesh | method | MaterialFor | 62 | public static Material MaterialFor(Texture texture) |
| SparkMesh | method | Begin | 97 | public void Begin() |
| SparkMesh | method | Add | 99 | public void Add(Vector2 pos, Color color, bool sparkle) |
| SparkMesh | method | End | 106 | public void End() |
| SparkMesh | property | Mesh | 175 | public Mesh Mesh |
| SparkParticles | field | K | 15 | public const float K = 3 |
| SparkParticles | property | LiveCount | 24 | public int LiveCount |
| SparkParticles | method | Clear | 26 | public void Clear() |
| SparkParticles | method | Emit | 28 | public void Emit(Sparks owner, Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity, bool sparkle, float drag) |
| Sparks | property | I | 15 | public static Sparks I |
| Sparks | field | TrailRate | 18 | public const float TrailRate = 60 |
| Sparks | property | LiveCount | 42 | public int LiveCount |
| Sparks | property | LaneCount | 57 | public int LaneCount |
| Sparks | method | LaneMesh | 60 | public SparkMesh LaneMesh(int order) |
| Sparks | method | Emit | 63 | public void Emit(Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity = 0, bool sparkle = false, float drag = 0) |
| Sparks | method | Trail | 80 | public void Trail(Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity = 0, bool sparkle = false, float drag = 0) |
| Sparks | method | TrailCount | 92 | public static int TrailCount(float dt, float r) |
| Sparks | method | Burst | 95 | public void Burst(Vector2 pos, int count, Color color, float speed, float life, int order, float gravity = 0, float squash = 1) |
| Sparks | method | Clear | 104 | public void Clear() |
| Sparks | method | Step | 113 | public static bool Step(ref Particle p, float dt) |
| Sparks | method | Shade | 124 | public static Color Shade(in Particle p) |
| Sparks | method | Advance | 141 | public void Advance(float dt) |
| Sparks | method | Draw | 152 | public void Draw() |
| Particle | field | vel | 22 | public Vector2 pos, vel |
| Lane | field | order | 27 | public int order |
| Anim | method | Over | 14 | public static IEnumerator Over(float seconds, Action<float> step) |
| Anim | method | Wait | 21 | public static IEnumerator Wait(float seconds, Func<bool> skip = null) |
| Anim | method | OutCubic | 26 | public static float OutCubic(float t) |
| Anim | method | InCubic | 27 | public static float InCubic(float t) |
| Anim | method | InOut | 28 | public static float InOut(float t) |
| Anim | method | OutBack | 29 | public static float OutBack(float t) |
| Anim | method | Hump | 31 | public static float Hump(float t) |
| Art | field | Lib | 7 | public static ArtLibrary Lib |
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
| AudioMute | method | Reset | 23 | public static void Reset() |
| Clickable | field | offset | 11 | public Vector2 size, offset |
| Clickable | field | order | 12 | public int order |
| Clickable | field | holes | 14 | public readonly List<Rect> holes = new List<Rect>() |
| Clickable | field | onUp | 15 | public Action onClick, onEnter, onExit, onDown, onUp |
| Clickable | method | Contains | 17 | public bool Contains(Vector2 world) |
| Dev | method | Hook | 12 | public static void Hook(string line) |
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
| Haptics | property | Supported | 27 | public static bool Supported |
| Haptics | method | Plan | 41 | public static int Plan(bool enabled, bool supported, int ms) |
| Haptics | method | Pulse | 43 | public static void Pulse(int ms) |
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
| Loc | field | Changed | 19 | public static event Action Changed |
| Loc | property | Code | 22 | public static string Code |
| Loc | property | Vi | 23 | public static bool Vi |
| Loc | property | Current | 24 | public static Lang Current |
| Loc | property | NativeName | 26 | public static string NativeName |
| Loc | method | Use | 34 | public static void Use(TextAsset english, TextAsset vietnamese) |
| Loc | method | InitFromPlayer | 59 | public static void InitFromPlayer(string saved) |
| Loc | method | FromUrl | 68 | public static string FromUrl(string url) |
| Loc | method | Set | 77 | public static void Set(string c) |
| Loc | method | T | 90 | public static string T(string key, params (string name, object value)[] args) |
| Loc | method | P | 93 | public static string P(string key, long n, params (string name, object value)[] args) |
| Loc | method | C | 96 | public static string C(string key, string english, params (string name, object value)[] args) |
| Loc | method | Has | 98 | public static bool Has(string key) |
| Loc | method | Kind | 101 | public static string Kind(string kind) |
| Loc | method | Indef | 105 | public static string Indef(string kind) |
| Loc | method | Rarity | 109 | public static string Rarity(string rarity) |
| Loc | method | Upper | 112 | public static string Upper(string s) |
| Loc | method | DepthName | 115 | public static string DepthName(ContentDb db, int depth) |
| Loc | method | Number | 118 | public static string Number(long n) |
| Motion | method | Move | 19 | public static Tween Move(Transform t, Vector3 from, Vector3 to, float seconds, Ease ease = Ease.Linear) |
| Motion | method | MoveLocal | 23 | public static Tween MoveLocal(Transform t, Vector3 from, Vector3 to, float seconds, Ease ease = Ease.Linear) |
| Motion | property | ArcPath | 30 | sealed class ArcPath |
| Motion | method | Arc | 36 | public static Tween Arc(Transform t, Vector3 from, Vector3 to, float height, float seconds, Ease ease = Ease.Linear) |
| Motion | method | Scale | 42 | public static Tween Scale(Transform t, float from, float to, float seconds, Ease ease = Ease.Linear) |
| Motion | method | Fade | 46 | public static Tween Fade(SpriteRenderer r, float from, float to, float seconds, Ease ease = Ease.Linear) |
| Motion | method | Fade | 50 | public static Tween Fade(Label l, float from, float to, float seconds, Ease ease = Ease.Linear) |
| Motion | method | Wait | 54 | public static IEnumerator Wait(Tween tween, Func<bool> hurry = null) |
| Motion | method | Wait | 60 | public static IEnumerator Wait(Sequence sequence, Func<bool> hurry = null) |
| PointerInput | property | World | 10 | public static Vector2 World |
| PointerInput | field | Rig | 12 | public static DepthRig Rig |
| PointerInput | property | Bench | 15 | public static Vector2 Bench |
| PointerInput | property | Down | 16 | public static bool Down |
| PointerInput | property | Up | 17 | public static bool Up |
| PointerInput | property | Held | 18 | public static bool Held |
| PointerInput | property | Hover | 19 | public static Clickable Hover |
| PointerInput | field | AnyPress | 21 | public static event Action AnyPress |
| PointerInput | method | Register | 26 | public static void Register(Clickable c) |
| PointerInput | method | Unregister | 27 | public static void Unregister(Clickable c) |
| PointerInput | method | ScreenToWorld | 30 | public static Vector2 ScreenToWorld(Vector2 screen) |
| PointerInput | method | Resolve | 39 | public static void Resolve(Vector2 world) |
| Px | field | W | 11 | public const int W = 640, H = 360 |
| Px | method | At | 14 | public static Vector3 At(float x, float y) |
| Px | method | Mid | 17 | public static Vector3 Mid(float x, float y, float w, float h) |
| Px | method | Fit | 20 | public static void Fit(Transform t, float x, float y, float w, float h) |
| Px | method | Snap | 26 | public static Vector3 Snap(Vector3 p) |
| Px | method | Hex | 29 | public static Color Hex(string hex, float a = 1, float k = 1) |
| Px | method | Alpha | 35 | public static Color Alpha(this Color c, float a) |
| Px | method | Times | 36 | public static Color Times(this Color c, float k) |
| RarityStars | field | Size | 15 | public const int Size = 7, Pitch = 8, Max = 4 |
| RarityStars | property | Root | 24 | public Transform Root |
| RarityStars | ctor | RarityStars | 27 | public RarityStars() |
| RarityStars | ctor | RarityStars | 29 | public RarityStars(Transform parent, int order) |
| RarityStars | method | Count | 65 | public static int Count(string rarity) |
| RarityStars | method | WidthOf | 68 | public static int WidthOf(int n) |
| RarityStars | method | Show | 71 | public void Show(string rarity) |
| RarityStars | method | Show | 74 | public void Show(int n, Color color) |
| RarityStars | method | SetAlpha | 81 | public void SetAlpha(float a) |
| RarityStars | method | SetOrder | 88 | public void SetOrder(int order) |
| RarityStars | method | SetActive | 94 | public void SetActive(bool on) |
| Spawn | method | Root | 16 | public static Transform Root(string name, Transform parent = null) |
| Spawn | method | Sprite | 25 | public static SpriteRenderer Sprite(Transform parent, string sprite, Vector3 worldPos, int order, Material material = null) |
| Spawn | method | Rect | 39 | public static SpriteRenderer Rect(Transform parent, float x, float y, float w, float h, Color color, int order) |
| Spawn | method | Glow | 49 | public static SpriteRenderer Glow(Transform parent, Vector3 worldPos, float radius, Color color, int order) |
| Spawn | method | Text | 58 | public static Label Text(Transform parent, string text, float x, float y, FontKind font, int size, Color color, int order, Align align = Align.Left, Color? shadow = null, float width = 0) |
| Spawn | method | Click | 100 | public static Clickable Click(GameObject go, Vector2 size, int order, System.Action onClick = null) |
| Align | value | Left | 7 | Left |
| Align | value | Centre | 7 | Centre |
| Align | value | Right | 7 | Right |
| Wording | method | RarityColor | 11 | public static Color RarityColor(string rarity) |
| Wording | method | RarityTone | 23 | public static Color RarityTone(string rarity) |
| Wording | method | RarityWord | 35 | public static string RarityWord(string rarity) |
| Wording | method | AspectColor | 38 | public static Color AspectColor(string effect) |
| Wording | field | Warm | 58 | public static readonly Color Warm = Px.Hex("#ffa63a") |
| Wording | method | AspectRule | 61 | public static string AspectRule(AspectInstance a, bool cost = true) |
| Wording | method | AspectCost | 83 | public static string AspectCost(AspectInstance a) |
| Wording | method | Effect | 91 | public static string Effect(EffectData fx) |
| Wording | method | OptionSummary | 121 | public static string OptionSummary(EventOption o) |
| Wording | method | GoalText | 140 | public static string GoalText(GoalData g) |
| Wording | method | OptionLabel | 143 | public static string OptionLabel(EventData e, EventOption o) |
| Wording | method | OutcomeText | 147 | public static string OutcomeText(EventData e, EventOption o, string english) |
| Wording | method | Note | 175 | public static string Note(string english) |
| Wording | method | RewardLine | 187 | public static string RewardLine(GoalData g) |
| Wording | method | ItemName | 191 | public static string ItemName(ContentDb db, string id) |
| Wording | method | ItemSprite | 200 | public static string ItemSprite(ContentDb db, string id) |
| Wording | method | Number | 202 | public static string Number(long n) |
| ChoiceCard | field | cost | 13 | public string id, sprite, name, text, kicker, cost |
| ChoiceCard | field | rarity | 14 | public string rarity |
| ChoiceCard | field | tiles | 15 | public int[] tiles |
| ChoiceCard | field | dimTiles | 16 | public int[] dimTiles |
| CatalystChoiceView | field | OpenHook | 29 | public const string OpenHook = |
| CatalystChoiceView | method | CardName | 55 | public Label CardName(int i) |
| CatalystChoiceView | method | CornerStars | 57 | public RarityStars CornerStars(int i) |
| CatalystChoiceView | method | Bind | 60 | public void Bind() |
| CatalystChoiceView | method | Choose | 77 | public IEnumerator Choose(IReadOnlyList<CatalystData> offer, Action<string> done, float autoAfter = -1) |
| CatalystChoiceView | method | Choose | 89 | public IEnumerator Choose(IReadOnlyList<AspectInstance> offer, Action<string> done, float autoAfter = -1) |
| CatalystChoiceView | method | Choose | 103 | public IEnumerator Choose(string title, string line, IReadOnlyList<ChoiceCard> offer, Action<string> done, float autoAfter = -1) |
| Card | field | root | 37 | public Transform root |
| Card | field | click | 38 | public Clickable click |
| Card | field | bench | 39 | public SpriteRenderer[] bench = new SpriteRenderer[Bench.TileCount] |
| EventView | method | OptionLabel | 35 | public Label OptionLabel(int i) |
| EventView | property | Outcome | 37 | public Label Outcome |
| EventView | method | Bind | 40 | public void Bind() |
| EventView | method | Show | 67 | public IEnumerator Show(RunState run, Action changed) |
| Row | field | root | 21 | public Transform root |
| GoalRow | field | reward | 8 | public Label text, count, reward |
| GoalRow | field | icon | 9 | public SpriteRenderer track, fill, icon |
| GoalToast | field | Stay | 15 | public const float Stay = 2.5f |
| GoalToast | field | Slide | 17 | public const float Slide = 0.25f |
| GoalToast | property | Item | 19 | struct Item |
| GoalToast | property | Title | 29 | public Label Title |
| GoalToast | property | Line | 31 | public Label Line |
| GoalToast | method | Bind | 34 | public void Bind() |
| GoalToast | method | Add | 40 | public void Add(IEnumerable<GoalReward> rewards, ContentDb db) |
| GoalToast | method | Clear | 52 | public void Clear() |
| GoalsView | field | Closed | 17 | public event Action Closed |
| GoalsView | property | IsOpen | 18 | public bool IsOpen |
| GoalsView | property | CloseClick | 33 | public Clickable CloseClick |
| GoalsView | method | Bind | 39 | public void Bind(ContentDb content) |
| GoalsView | method | Open | 62 | public void Open(Profile profile, Journal journal) |
| GoalsView | method | Close | 105 | public void Close() |
| JournalCell | field | icon | 12 | public SpriteRenderer icon |
| JournalCell | field | click | 13 | public Clickable click |
| JournalView | field | Closed | 19 | public event Action Closed |
| JournalView | property | IsOpen | 20 | public bool IsOpen |
| JournalView | field | PerPage | 23 | public const int PerPage = Cols * Rows |
| JournalView | property | Title | 44 | public Label Title |
| JournalView | property | CloseClick | 46 | public Clickable CloseClick |
| JournalView | property | PageClick | 48 | public Clickable PageClick |
| JournalView | method | Bind | 54 | public void Bind(ContentDb content) |
| JournalView | method | Open | 112 | public void Open(Journal book, Func<string, bool> openStarters = null) |
| JournalView | method | Close | 163 | public void Close() |
| JournalView | method | ShowMaterial | 174 | public Rect ShowMaterial(string id) |
| JournalView | property | IndexRect | 181 | public static Rect IndexRect |
| JournalView | property | RecipesRect | 183 | public static Rect RecipesRect |
| JournalView | property | HintsRect | 185 | public static Rect HintsRect |
| PauseMenu | field | QuitConfirmed | 17 | public event Action QuitConfirmed |
| PauseMenu | field | VibrationChanged | 19 | public event Action VibrationChanged |
| PauseMenu | field | Closed | 21 | public event Action Opened, Closed |
| PauseMenu | field | CanOpen | 23 | public Func<bool> CanOpen |
| PauseMenu | property | IsOpen | 25 | public bool IsOpen |
| PauseMenu | property | Confirming | 26 | public bool Confirming |
| PauseMenu | method | Allowed | 36 | public static bool Allowed(FlowState state, bool benchInput, bool inCutscene) |
| PauseMenu | method | Bind | 40 | public void Bind(Settings playerSettings) |
| PauseMenu | method | Open | 67 | public bool Open() |
| PauseMenu | method | Close | 78 | public void Close() |
| PauseMenu | method | Toggle | 88 | public void Toggle() |
| RunOverView | property | Again | 12 | public bool Again |
| RunOverView | field | GoalsClicked | 14 | public event System.Action GoalsClicked |
| RunOverView | method | Bind | 25 | public void Bind() |
| RunOverView | method | Pick | 47 | public void Pick(bool again) |
| RunOverView | property | DepthName | 50 | public Label DepthName |
| RunOverView | property | DepthValue | 52 | public Label DepthValue |
| RunOverView | method | ShowDepth | 58 | public void ShowDepth(bool won, int depth, int finalDepth) |
| RunOverView | method | Show | 66 | public IEnumerator Show(bool won, RunState run, Journal journal, ContentDb db, System.Collections.Generic.IReadOnlyList<GoalReward> goals) |
| TitleView | property | BeginPressed | 11 | public bool BeginPressed |
| TitleView | property | HowToPressed | 13 | public bool HowToPressed |
| TitleView | property | IntroPressed | 14 | public bool IntroPressed |
| TitleView | method | ClearBegin | 16 | public void ClearBegin() |
| TitleView | field | SettingsClosed | 17 | public event Action JournalClicked, GoalsClicked, ResetConfirmed, SettingsClosed |
| TitleView | property | Asking | 19 | public bool Asking |
| TitleView | method | Bind | 44 | public void Bind(Settings playerSettings) |
| TitleView | method | AskSkip | 108 | public void AskSkip(Action<bool> answer) |
| TitleView | method | Open | 206 | public void Open(int known, int total, int goalsDone, int goals) |
| TitleView | method | Progress | 217 | public void Progress(int known, int total, int goalsDone, int goals) |
| TitleView | method | Close | 224 | public void Close() |
| Tooltip | method | Bind | 22 | public void Bind() |
| Tooltip | method | Show | 32 | public void Show(string heading, string text, Vector2 anchor, float clearance = 20, string rarity = null) |
| Tooltip | method | Hide | 58 | public void Hide() |
| UiButton | field | W | 9 | public const int W = 120, H = 26 |
| UiButton | field | Clicked | 15 | public event Action Clicked |
| UiButton | property | Text | 17 | public Label Text |
| UiButton | property | Disabled | 19 | public bool Disabled |
| UiButton | method | Bind | 24 | public void Bind() |
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
| AtlasBuilder | method | BuildAtlases | 309 | public static string BuildAtlases() |
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
| EditorGlobals | method | Inject | 13 | public static void Inject() |
| GoldenShots | method | Names | 27 | public static IEnumerable<string> Names() |
| GoldenShots | method | PathOf | 30 | public static string PathOf(string name, string lang) |
| GoldenShots | method | WriteAll | 37 | public static void WriteAll() |
| GoldenShots | method | Render | 53 | public static Texture2D Render(string name) |
| Reserialize | method | All | 14 | public static void All() |
| ViewCapture | method | Render | 15 | public static Texture2D Render(GameObject root, params string[] keepHidden) |
| ViewCapture | method | RenderAsShown | 64 | public static Texture2D RenderAsShown(GameObject root) |
| ViewCapture | method | Save | 73 | public static void Save(Texture2D tex, string path) |
| ViewCapture | method | Load | 80 | public static Texture2D Load(string path) |
| ViewCapture | method | Differ | 89 | public static float Differ(Texture2D a, Texture2D b) |
| SpriteImportRules | method | GetVersion | 14 | public override uint GetVersion() |
| SpriteImportRules | method | IsSoftDepthArt | 17 | public static bool IsSoftDepthArt(string assetPath) |
| SpriteImportRules | method | IsDepthFog | 25 | public static bool IsDepthFog(string assetPath) |
| WebBuild | field | Output | 12 | public const string Output = "Build/web", DevOutput = |
| WebBuild | method | Build | 15 | public static void Build() |
| WebBuild | method | BuildQuick | 19 | public static void BuildQuick() |
| WebBuild | method | BuildDev | 26 | public static void BuildDev() |
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
| RulesTests | method | Rules_ShippedContent_HasTodaysValues | 11 | public void Rules_ShippedContent_HasTodaysValues() |
| RulesTests | method | FinalDepth_ShippedContent_IsFive | 30 | public void FinalDepth_ShippedContent_IsFive() |
| RulesTests | method | Phase_AspectDepthsTwo_OffersAspectsAtDepthTwoOnly | 79 | public void Phase_AspectDepthsTwo_OffersAspectsAtDepthTwoOnly() |
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
| Sim | method | With | 14 | public static Unlocks With(ContentDb db, bool starters, bool catalysts) |
| Sim | method | Autoplay | 24 | public static RunState Autoplay(ContentDb db, int seed, RunOptions options) |
| Sim | method | Line | 33 | public static string Line(ContentDb db, string label, Func<int, RunOptions> options, int seeds) |
| Sim | method | RandomPlay | 47 | public static RunState RandomPlay(ContentDb db, int seed, RunOptions options) |
| Sim | method | Play | 397 | public static Career Play(ContentDb db, bool admin, int player, int runs, bool aspects = true, bool events = true, List<string> log = null, int logFrom = 0, int taughtTier = 0) |
| Sim | method | Pct | 450 | public static string Pct(double x) |
| Sim | method | Describe | 452 | public static void Describe(string role, List<Career> careers, int runs, Action<string> w, int finalDepth) |
| Sim | method | Autoplay | 496 | public static RunState Autoplay(ContentDb db, int seed, List<string> log = null, RunStats stats = null) |
| Sim | method | RandomPlay | 543 | public static RunState RandomPlay(ContentDb db, int seed) |
| Sim | method | FullJournal | 565 | public static Journal FullJournal(ContentDb db) |
| Newcomer | ctor | Newcomer | 81 | public Newcomer(ContentDb truth, Journal journal) |
| Newcomer | method | List | 133 | public readonly List<(string a, string b)> targets = new List<(string, string)>() |
| Newcomer | property | TriedCount | 134 | public int TriedCount |
| Newcomer | property | ColdCount | 135 | public int ColdCount |
| Newcomer | method | Status | 136 | public string Status(string a, string b) |
| Newcomer | method | CanMakeNow | 137 | public bool CanMakeNow(string m) |
| Newcomer | method | Decide | 180 | public bool Decide(RunState run) |
| Newcomer | method | Learn | 372 | public void Learn(IgniteOutcome outcome, BenchRules rules) |
| Career | field | FirstWin | 390 | public int FirstWin = -1 |
| Career | field | Won | 391 | public readonly List<bool> Won = new List<bool>() |
| Career | field | Depth | 392 | public readonly List<int> Depth = new List<int>(), Journal = new List<int>(), Goals = new List<int>(), Unlocked = new List<int>() |
| Career | field | Stuck | 393 | public int Stuck |
| Career | field | Knows | 394 | public string Knows = |
| RunStats | field | BySize | 488 | public readonly int[] BySize = new int[ContentDb.MaxItems + 1] |
| RunStats | field | FirstTrialClears | 489 | public int Ignites, LongestChain, Sputters, FirstTrialClears |
| RunStats | field | UltimateByCombo | 490 | public bool UltimateByCombo |
| RunStats | field | ComboMade | 491 | public readonly HashSet<string> ComboMade = new HashSet<string>() |
| RunStats | field | Combos | 492 | public readonly Dictionary<string, int> Combos = new Dictionary<string, int>() |
| RunStats | field | ClearedIn | 493 | public readonly List<string> ClearedIn = new List<string>() |
| TestContent | method | Load | 8 | public static ContentData Load() |
| TestContent | method | Tiny | 21 | public static ContentData Tiny() |
| TestContent | method | Combo | 43 | public static ContentData Combo(int zMin = 15) |
| TestContent | field | Today | 69 | public static readonly RulesData Today = Rules() |
| TestContent | field | FinalDepth | 71 | public const int FinalDepth = 5 |
| TestContent | method | Rules | 74 | public static RulesData Rules() |
| TestContent | method | Mat | 81 | public static MaterialData Mat(string id, string kind, int tier, int reactivity = 5) |
| TestContent | method | Rec | 83 | public static RecipeData Rec(string a, string b, string r) |
| TestContent | method | Rec | 84 | public static RecipeData Rec(string[] items, int min, string r) |
| TestContent | method | Cat | 85 | public static CatalystData Cat(string effect, string kind = "", float amount = 0) |
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
| DepthClickTests | method | TearDown | 37 | public void TearDown() |
| DepthClickTests | method | ClickOnTileCentre_AtRestAndUnderMaximumSway_HitsThatTile | 53 | public void ClickOnTileCentre_AtRestAndUnderMaximumSway_HitsThatTile() |
| DepthClickTests | method | ClickWhereTheCameraShowsTheTileCentre_UnderSwayAndShake_FindsThatPoint | 68 | public void ClickWhereTheCameraShowsTheTileCentre_UnderSwayAndShake_FindsThatPoint() |
| DepthClickTests | method | DrawnFace_UnderShakeUpToThreePx_IsHitByTheRayAtCentreAndInsetCorners | 85 | public void DrawnFace_UnderShakeUpToThreePx_IsHitByTheRayAtCentreAndInsetCorners() |
| DepthClickTests | method | WorldRay_UnderSwayAndShake_IsTheWorldCamerasOwnRay | 109 | public void WorldRay_UnderSwayAndShake_IsTheWorldCamerasOwnRay() |
| DepthClickTests | method | Sway_KeepsTheAnchorRowStill_AndMovesTheWallLessThanAnArtPixel | 121 | public void Sway_KeepsTheAnchorRowStill_AndMovesTheWallLessThanAnArtPixel() |
| DepthClickTests | method | SetView_ShakeOnly_MovesEveryDepthByTheSameWholeArtPixels | 142 | public void SetView_ShakeOnly_MovesEveryDepthByTheSameWholeArtPixels() |
| DepthClickTests | method | TiltShiftSigma_IsZeroInTheBand_AndRisesToTheMaximum | 154 | public void TiltShiftSigma_IsZeroInTheBand_AndRisesToTheMaximum() |
| DepthClickTests | method | Show_TileReloadedFromAScene_ShowsTheMaterialAndTakesClicks | 164 | public void Show_TileReloadedFromAScene_ShowsTheMaterialAndTakesClicks() |
| DepthClickTests | method | FogOffset_StepsInWholeTexels_AndWraps | 180 | public void FogOffset_StepsInWholeTexels_AndWraps() |
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
| GoldenShotTests | method | Render_Shot_MatchesTheGoldenImage | 10 | public void Render_Shot_MatchesTheGoldenImage(string name) |
| HudTests | method | CanRedraw_WhileRedrawsRemain_IsTrueUntilTheLastRedrawIsUsed | 14 | public void CanRedraw_WhileRedrawsRemain_IsTrueUntilTheLastRedrawIsUsed() |
| HudTests | method | CanRedraw_IsFalse_OffTheBench_EvenWithRedrawsLeft | 29 | public void CanRedraw_IsFalse_OffTheBench_EvenWithRedrawsLeft() |
| HudTests | method | CanRedraw_IsFalse_WithNoRun | 40 | public void CanRedraw_IsFalse_WithNoRun() |
| LabelLineBoxTests | method | HalfLeading_Crux48_IsElevenPixels | 43 | public void HalfLeading_Crux48_IsElevenPixels() |
| LabelLineBoxTests | method | LineBoxTitle_InVietnamese_MovesFaceAndShadowDownByCruxHalfLeading | 52 | public void LineBoxTitle_InVietnamese_MovesFaceAndShadowDownByCruxHalfLeading() |
| LabelLineBoxTests | method | LineBoxTitle_SizeSwitchTo32_FollowsTheNewSize | 62 | public void LineBoxTitle_SizeSwitchTo32_FollowsTheNewSize() |
| LabelLineBoxTests | method | LineBoxTitle_InEnglish_DoesNotMove | 72 | public void LineBoxTitle_InEnglish_DoesNotMove() |
| LabelLineBoxTests | method | PlainTitle_InVietnamese_DoesNotMove | 81 | public void PlainTitle_InVietnamese_DoesNotMove() |
| LocTests | method | Use_WithInjectedFiles_ServesTheirText | 19 | public void Use_WithInjectedFiles_ServesTheirText() |
| LocTests | method | Use_NullFile_Throws | 32 | public void Use_NullFile_Throws() |
| LocTests | method | ViJsonCharacters_AllInCruxAtlas_NoneMissing | 40 | public void ViJsonCharacters_AllInCruxAtlas_NoneMissing() |
| LocTests | method | UppercasedViStrings_AreInTheCruxAtlas_BecauseScreensWriteCapitalsThroughLocUpper | 51 | public void UppercasedViStrings_AreInTheCruxAtlas_BecauseScreensWriteCapitalsThroughLocUpper() |
| LocTests | method | EveryCharacterInEnJson_IsInTheCruxAtlas_SoALanguageSwitchNeverDrawsABlank | 64 | public void EveryCharacterInEnJson_IsInTheCruxAtlas_SoALanguageSwitchNeverDrawsABlank() |
| LocTests | method | EveryUiKey_ResolvesInBothLanguages_NeverToTheKey | 73 | public void EveryUiKey_ResolvesInBothLanguages_NeverToTheKey() |
| LocTests | method | Vietnamese_ChangesTheWords_AndEnglishComesBack | 85 | public void Vietnamese_ChangesTheWords_AndEnglishComesBack() |
| LocTests | method | ItemNames_StayEnglish_InVietnamese | 96 | public void ItemNames_StayEnglish_InVietnamese() |
| LocTests | method | FormatNumber_ByLanguage_UsesLocaleSeparators | 103 | public void FormatNumber_ByLanguage_UsesLocaleSeparators() |
| LocTests | method | CoreNotes_AreTranslated_InVietnamese_AndLeftAloneInEnglish | 111 | public void CoreNotes_AreTranslated_InVietnamese_AndLeftAloneInEnglish() |
| LocTests | method | ReadLangFromUrl_WithLangParam_ReturnsCode | 123 | public void ReadLangFromUrl_WithLangParam_ReturnsCode() |
| MainDepthTests | method | SetUp | 18 | public void SetUp() |
| MainDepthTests | method | TearDown | 37 | public void TearDown() |
| MainDepthTests | method | SpawnChildren_OfAWorldLayerParent_DrawOnTheWorldLayer | 40 | public void SpawnChildren_OfAWorldLayerParent_DrawOnTheWorldLayer() |
| MainDepthTests | method | TileGroup_AtRest_KeepsTheFaceOnItsArtPixels | 53 | public void TileGroup_AtRest_KeepsTheFaceOnItsArtPixels() |
| MainDepthTests | method | TileIcon_MovedByLocalPosition_StaysOnTheTile | 71 | public void TileIcon_MovedByLocalPosition_StaysOnTheTile() |
| MainDepthTests | method | ShakeFromScreenFx_MovesTheWorldPicture_AndFlatModeIgnoresIt | 80 | public void ShakeFromScreenFx_MovesTheWorldPicture_AndFlatModeIgnoresIt() |
| MainDepthTests | method | PointerBench_AllNineTiles_HitAtRestAtSwayExtremesAndUnderShake | 95 | public void PointerBench_AllNineTiles_HitAtRestAtSwayExtremesAndUnderShake() |
| MainDepthTests | method | MainScene_HasTheRigWorldAndUiCameras_AndTheStageIsWired | 121 | public void MainScene_HasTheRigWorldAndUiCameras_AndTheStageIsWired() |
| MainDevHookTests | method | NextMood_StepsOneToFive_AndWraps | 14 | public void NextMood_StepsOneToFive_AndWraps() |
| MainDevHookTests | method | HookLines_ForEachHook_MatchTheScriptPatterns | 23 | public void HookLines_ForEachHook_MatchTheScriptPatterns() |
| MainDevHookTests | method | DrawnFace_AtRestView_IsTheInsetFaceInUiUnits | 33 | public void DrawnFace_AtRestView_IsTheInsetFaceInUiUnits() |
| MainDevHookTests | method | DrawnFace_WithSway_MovesTheFaceButNotAtTheAnchorRow | 42 | public void DrawnFace_WithSway_MovesTheFaceButNotAtTheAnchorRow() |
| MainSceneFixture | field | Flow | 20 | public readonly GameFlow Flow |
| MainSceneFixture | ctor | MainSceneFixture | 23 | public MainSceneFixture() |
| MainSceneFixture | method | Field | 35 | public static T Field<T>(object owner, string name) |
| MainSceneFixture | method | FlowField | 43 | public T FlowField<T>(string name) where T : Component |
| MainSceneFixture | method | Dispose | 46 | public void Dispose() |
| MotionTests | method | Clean | 20 | public void Clean() |
| MotionTests | method | Arc_IsAlive_AndCanJoinASequence | 27 | public void Arc_IsAlive_AndCanJoinASequence() |
| MotionTests | method | Arc_EndsOnItsTarget_OnWholePixels | 36 | public void Arc_EndsOnItsTarget_OnWholePixels() |
| MotionTests | method | EveryHelper_WhenStarted_IsAlive | 45 | public void EveryHelper_WhenStarted_IsAlive() |
| PauseTests | method | Open_StopsTime_AndEveryExitRestoresIt | 38 | public void Open_StopsTime_AndEveryExitRestoresIt() |
| PauseTests | method | Quit_RestoresTime_BeforeTheOwnerHearsOfIt | 53 | public void Quit_RestoresTime_BeforeTheOwnerHearsOfIt() |
| PauseTests | method | Esc_Toggles_AndBacksOutOfTheQuitQuestion | 67 | public void Esc_Toggles_AndBacksOutOfTheQuitQuestion() |
| PauseTests | method | Abort_TutorialRun_DoesNotMarkItDone | 78 | public void Abort_TutorialRun_DoesNotMarkItDone() |
| PauseTests | method | Open_NotAllowed_IsRefused | 90 | public void Open_NotAllowed_IsRefused() |
| PauseTests | method | Allowed_OnlyOnTheBenchWithInput_NotInCutscene_TutorialIsPausable | 98 | public void Allowed_OnlyOnTheBenchWithInput_NotInCutscene_TutorialIsPausable() |
| PauseTests | method | VibrationButton_NoHaptics_IsHidden | 108 | public void VibrationButton_NoHaptics_IsHidden() |
| PauseTests | method | AfterQuit_ProfileAndJournalAreWhatIsStored_AndStorageIsNotWritten | 116 | public void AfterQuit_ProfileAndJournalAreWhatIsStored_AndStorageIsNotWritten() |
| PauseTests | method | Quit_AfterADiscovery_JournalInStorageHasIt | 133 | public void Quit_AfterADiscovery_JournalInStorageHasIt() |
| PauseTests | method | SettleIgnite_Sandbox_StoresNothing | 152 | public void SettleIgnite_Sandbox_StoresNothing() |
| PrefabBakeTests | method | Relabel_KeyedLabelInPrefab_FollowsTheLanguage | 34 | public void Relabel_KeyedLabelInPrefab_FollowsTheLanguage() |
| PrefabBakeTests | method | Relabel_PlainLabelInPrefab_KeepsItsText | 44 | public void Relabel_PlainLabelInPrefab_KeepsItsText() |
| PrefabBakeTests | method | Click_ButtonInPrefab_RaisesClicked | 55 | public void Click_ButtonInPrefab_RaisesClicked() |
| PrefabBakeTests | method | Click_DisabledButtonInPrefab_DoesNothing | 66 | public void Click_DisabledButtonInPrefab_DoesNothing() |
| PrefabBakeTests | method | Text_ButtonInPrefab_HasLabelAndBody | 78 | public void Text_ButtonInPrefab_HasLabelAndBody() |
| PrefabBakeTests | method | Disabled_SetOnAButton_IsKeptInTheSerializedState | 87 | public void Disabled_SetOnAButton_IsKeptInTheSerializedState() |
| PrefabBakeTests | method | ShowDepth_RunOverPrefab_WritesTheDepthRow | 96 | public void ShowDepth_RunOverPrefab_WritesTheDepthRow() |
| PrefabBakeTests | method | Click_RunOverPrefabButton_PicksItsChoice | 112 | public void Click_RunOverPrefabButton_PicksItsChoice(int index, bool again) |
| PrefabBakeTests | method | Show_EventPrefab_ListsTheOptions | 146 | public void Show_EventPrefab_ListsTheOptions() |
| PrefabBakeTests | method | Click_EventPrefabLastOptionThenContinue_ShowsTheOutcomeAndCloses | 164 | public void Click_EventPrefabLastOptionThenContinue_ShowsTheOutcomeAndCloses() |
| PrefabBakeTests | method | Add_GoalToastPrefab_ShowsTheGoalName | 195 | public void Add_GoalToastPrefab_ShowsTheGoalName() |
| PrefabBakeTests | method | Click_ShowingGoalToastPrefab_SkipsToTheSlideOut | 213 | public void Click_ShowingGoalToastPrefab_SkipsToTheSlideOut() |
| PrefabBakeTests | method | Show_CatalystChoicePrefab_FillsThreeCards | 242 | public void Show_CatalystChoicePrefab_FillsThreeCards() |
| PrefabBakeTests | method | Show_CatalystChoicePrefabRareCard_DrawsThreeStars | 260 | public void Show_CatalystChoicePrefabRareCard_DrawsThreeStars() |
| PrefabBakeTests | method | Click_CatalystChoicePrefabCardAfterTheDeal_PicksItsId | 285 | public void Click_CatalystChoicePrefabCardAfterTheDeal_PicksItsId() |
| PrefabBakeTests | method | Open_PausePrefab_ShowsTheMenu | 308 | public void Open_PausePrefab_ShowsTheMenu() |
| PrefabBakeTests | method | Click_PausePrefabResume_ClosesAndRestoresTime | 326 | public void Click_PausePrefabResume_ClosesAndRestoresTime() |
| PrefabBakeTests | method | Click_PausePrefabCancelOnTheQuitQuestion_BacksOutToTheMenu | 344 | public void Click_PausePrefabCancelOnTheQuitQuestion_BacksOutToTheMenu() |
| PrefabBakeTests | method | Click_PausePrefabOutsideTheDialog_Resumes | 364 | public void Click_PausePrefabOutsideTheDialog_Resumes() |
| PrefabBakeTests | method | Bind_GoalsPrefab_MakesOneRowPerGoal | 388 | public void Bind_GoalsPrefab_MakesOneRowPerGoal() |
| PrefabBakeTests | method | Open_GoalsPrefabFreshProfile_WritesEveryGoal | 404 | public void Open_GoalsPrefabFreshProfile_WritesEveryGoal() |
| PrefabBakeTests | method | Click_GoalsPrefabClose_ClosesAndTellsTheOwner | 425 | public void Click_GoalsPrefabClose_ClosesAndTellsTheOwner() |
| PrefabBakeTests | method | Bind_JournalPrefab_MakesOneCellPerMaterial | 447 | public void Bind_JournalPrefab_MakesOneCellPerMaterial() |
| PrefabBakeTests | method | Click_JournalPrefabCell_ShowsThatMaterial | 463 | public void Click_JournalPrefabCell_ShowsThatMaterial() |
| PrefabBakeTests | method | Click_JournalPrefabPageLabel_TurnsTheGridPage | 485 | public void Click_JournalPrefabPageLabel_TurnsTheGridPage() |
| PrefabBakeTests | method | Click_JournalPrefabClose_ClosesAndTellsTheOwner | 507 | public void Click_JournalPrefabClose_ClosesAndTellsTheOwner() |
| PrefabBakeTests | method | Show_TooltipInBenchPrefab_HasItsSlicedPanel | 528 | public void Show_TooltipInBenchPrefab_HasItsSlicedPanel() |
| PrefabBakeTests | method | Bind_TitlePrefab_ShowsTheLogoText | 550 | public void Bind_TitlePrefab_ShowsTheLogoText() |
| PrefabBakeTests | method | Click_TitlePrefabBegin_SetsBeginPressed | 567 | public void Click_TitlePrefabBegin_SetsBeginPressed() |
| PrefabBakeTests | method | Click_TitlePrefabSettingsThenLanguage_SwitchesTheLanguageAndBackCloses | 584 | public void Click_TitlePrefabSettingsThenLanguage_SwitchesTheLanguageAndBackCloses() |
| PrefabBakeTests | method | Click_TitlePrefabEraseLink_AsksThenConfirmingRaisesResetConfirmed | 612 | public void Click_TitlePrefabEraseLink_AsksThenConfirmingRaisesResetConfirmed() |
| PrefabBakeTests | method | AskSkip_TitlePrefab_AnswersPlayFromTheSkipDialog | 635 | public void AskSkip_TitlePrefab_AnswersPlayFromTheSkipDialog() |
| PrefabBakeTests | method | Bind_HudPrefab_ShowsTheDepthNumber | 662 | public void Bind_HudPrefab_ShowsTheDepthNumber() |
| PrefabBakeTests | method | Hover_HudPrefabSocketAndAspect_SetsAndClearsTheHoveredIndex | 681 | public void Hover_HudPrefabSocketAndAspect_SetsAndClearsTheHoveredIndex() |
| PrefabBakeTests | method | Click_HudPrefabRedraw_RaisesRedrawClickedOnlyWhileARedrawIsLeft | 707 | public void Click_HudPrefabRedraw_RaisesRedrawClickedOnlyWhileARedrawIsLeft() |
| PrefabBakeTests | method | Click_HudPrefabPauseJournalAndIgnite_RaiseTheirEvents | 735 | public void Click_HudPrefabPauseJournalAndIgnite_RaiseTheirEvents() |
| PrefabBakeTests | method | Refresh_BenchPrefab_HasNineTiles | 770 | public void Refresh_BenchPrefab_HasNineTiles() |
| PrefabBakeTests | method | Refresh_BenchPrefab_DealsOneCardPerStartingMaterialFromTheCardPrefab | 796 | public void Refresh_BenchPrefab_DealsOneCardPerStartingMaterialFromTheCardPrefab() |
| PrefabBakeTests | method | Show_CardPrefab_OneWordNameHidesTheSecondLine | 822 | public void Show_CardPrefab_OneWordNameHidesTheSecondLine() |
| PrefabFixture | field | Root | 12 | public readonly GameObject Root |
| PrefabFixture | ctor | PrefabFixture | 15 | public PrefabFixture(string prefabPath) |
| PrefabFixture | method | Get | 23 | public T Get<T>() where T : Component |
| PrefabFixture | method | Destroy | 29 | public void Destroy() |
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
| SceneBakeTests | method | ScreenFx_Baked_HasFlashAndFadeSprites_AndBindMakesItTheSingleton | 21 | public void ScreenFx_Baked_HasFlashAndFadeSprites_AndBindMakesItTheSingleton() |
| SceneBakeTests | method | Window_Baked_HasPaneAndEveryEffectSprite_AndShowStartsAnEffect | 42 | public void Window_Baked_HasPaneAndEveryEffectSprite_AndShowStartsAnEffect() |
| SceneBakeTests | method | Tutorial_Baked_HasItsBoxArrowAndCellRoot_ClosedUntilAStepShows | 63 | public void Tutorial_Baked_HasItsBoxArrowAndCellRoot_ClosedUntilAStepShows() |
| SceneBakeTests | method | Reaction_Baked_HasEveryPart_AndBindHidesTheStageAndSwitchesTheOthers | 92 | public void Reaction_Baked_HasEveryPart_AndBindHidesTheStageAndSwitchesTheOthers() |
| SceneBakeTests | method | Story_Baked_HasEveryPart_AndTheRayFacesAreTheRaySprites | 125 | public void Story_Baked_HasEveryPart_AndTheRayFacesAreTheRaySprites() |
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
