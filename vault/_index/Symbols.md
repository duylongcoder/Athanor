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
| ScoreContext | ScoreContext | Core | class | Athanor/Assets/Scripts/Core/BenchRules.cs | 83 | What one reaction's score depends on beyond the recipe and the catalysts. |
| ChainStep | ChainStep | Core | class | Athanor/Assets/Scripts/Core/Chain.cs | 11 | One thing the bench does during an Ignite. A reaction: the hub and the neighbours it consumed (Tiles[0] is the hub, the rest ascending). A sputter: a combo of three or more whose layout was standing r |
| ChainPlan | ChainPlan | Core | class | Athanor/Assets/Scripts/Core/Chain.cs | 20 |  |
| ChainPlanner | ChainPlanner | Core | class | Athanor/Assets/Scripts/Core/Chain.cs | 32 | Works out what a bench does, with no side effects. Each round the liveliest tile (highest Reaction Index, earlier tile on a tie) that has a recipe around it becomes the hub; the largest recipe it can  |
| ContentData | ContentData | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 28 |  |
| ProfileData | ProfileData | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 47 |  |
| Rarities | Rarities | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 57 |  |
| Effects | Effects | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 66 | The catalyst effect names used in content.json. |
| AspectEffects | AspectEffects | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 85 | The aspect effect names used in content.json. |
| Ops | Ops | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 97 | The event effect names (EffectData.op) and option requirements used in content.json. |
| StatIds | StatIds | Core | class | Athanor/Assets/Scripts/Core/ContentData.cs | 113 | The stats a goal can watch. |
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
| Unlocks | Unlocks | Core | class | Athanor/Assets/Scripts/Core/Profile.cs | 152 | Which starting materials and catalysts a run may use. |
| RunOptions | RunOptions | Core | class | Athanor/Assets/Scripts/Core/Profile.cs | 204 | Switches for a run. The default is a Phase 2 run: no aspects, no events, and (with no Unlocks) only what the content leaves open. |
| AspectInstance | AspectInstance | Core | class | Athanor/Assets/Scripts/Core/RunStart.cs | 8 | An aspect as held by a run (or as offered): where it sits and, for an attuned tile, which kind it favours. |
| EventResult | EventResult | Core | class | Athanor/Assets/Scripts/Core/RunStart.cs | 16 | What taking an event option did, for the screen. |
| RunState | RunState | Core | class | Athanor/Assets/Scripts/Core/RunStart.cs | 23 | One run: descending through depths, spending trials, collecting catalysts. |
| IgniteOutcome | IgniteOutcome | Core | class | Athanor/Assets/Scripts/Core/RunState.cs | 9 |  |
| RunPhase | RunPhase | Core | enum | Athanor/Assets/Scripts/Core/RunState.cs | 7 |  |
| Rng | Rng | Core | class | Athanor/Assets/Scripts/Core/Satchel.cs | 7 | Small deterministic RNG so shuffles match between Unity and the dotnet test run. |
| Satchel | Satchel | Core | class | Athanor/Assets/Scripts/Core/Satchel.cs | 20 | The player's deck of starting materials: a draw pile, a discard pile, and the cards currently out. |
| ScoreParts | ScoreParts | Core | class | Athanor/Assets/Scripts/Core/ScoreParts.cs | 7 | One reaction's potency split into the factors the tutorial names: Total = Base x Used x First x Step x Bonus. Bonus is everything else (catalysts, the hub's tile, events); 1 when nothing else applied. |
| TutorialLay | TutorialLay | Core | struct | Athanor/Assets/Scripts/Core/Tutorial.cs | 10 | One material laid on one tile. This material goes on this tile, not just any lit tile: another arrangement of the same materials can break the chain (trial 3 then scores 40 and does not clear the dept |
| TutorialTrial | TutorialTrial | Core | class | Athanor/Assets/Scripts/Core/Tutorial.cs | 18 | One scripted trial: the hand dealt, where each material goes, and what the Ignite makes, in chain order. |
| TutorialScript | TutorialScript | Core | class | Athanor/Assets/Scripts/Core/Tutorial.cs | 29 | The tutorial (docs/devlogs/phase-4-tutorial-script.md, v2) as data. Every number the script shows is pinned by TutorialTests; change both together. |
| BenchView | BenchView | Game | class | Athanor/Assets/Scripts/Game/Bench/BenchView.cs | 14 | The main screen: nine tiles, the hand of cards, the rack of discoveries and the HUD. Materials are placed by dragging them onto a tile, or by clicking one and then a tile; clicking a placed one takes  |
| From | From | Game | enum | Athanor/Assets/Scripts/Game/Bench/BenchView.cs | 22 |  |
| CardView | CardView | Game | class | Athanor/Assets/Scripts/Game/Bench/CardView.cs | 7 | A starting material in the hand, drawn as a parchment card. |
| Dust | Dust | Game | class | Athanor/Assets/Scripts/Game/Bench/Dust.cs | 9 | Dust drifting down through the moonbeam: a loop, each mote's position a pure function of time, all of them drawn as one SparkMesh (one renderer) at order 8. |
| HudView | HudView | Game | class | Athanor/Assets/Scripts/Game/Bench/HudView.cs | 9 | The panels round the bench: depth and potency, catalysts, trials and redraws, the last-reaction note, journal and Ignite. |
| LinkView | LinkView | Game | class | Athanor/Assets/Scripts/Game/Bench/LinkView.cs | 7 | Motes of light travelling between neighbouring tiles whose reaction is already in the journal. |
| TileView | TileView | Game | class | Athanor/Assets/Scripts/Game/Bench/TileView.cs | 10 | One of the nine bench tiles: its hover highlight and, when occupied, the material standing on it. |
| GameFlow | GameFlow | Game | class | Athanor/Assets/Scripts/Game/Boot/GameFlow.cs | 15 | Owns the run and moves the game between its screens. |
| FlowState | FlowState | Game | enum | Athanor/Assets/Scripts/Game/Boot/GameFlow.cs | 12 |  |
| Save | Save | Game | class | Athanor/Assets/Scripts/Game/Boot/Save.cs | 8 | Keeps the journal and the profile between sessions in PlayerPrefs (browser storage in a web build). |
| Cutscene | Cutscene | Game | class | Athanor/Assets/Scripts/Game/Cutscene/Cutscene.cs | 7 | A sequence that plays to its end unless the player clicks to skip ahead. |
| ReactionCutscene | ReactionCutscene | Game | class | Athanor/Assets/Scripts/Game/Cutscene/ReactionCutscene.cs | 13 | The full discovery sequence: two materials spiral into the cauldron, the rune circle lights, a flash, and the new material rises on a column of light with its name and potency. Everything is a functio |
| SimpleCutscene | SimpleCutscene | Game | class | Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs | 14 | The short story beats built from text and a few effects: the intro, the descent between depths, and the ending. A click finishes the line being written, then moves on. |
| Loop | Loop | Game | class | Athanor/Assets/Scripts/Game/Cutscene/SimpleCutscene.cs | 303 | A stream of glowing particles played as a loop instead of emitted: rate x life fixed slots, each reborn every life seconds at a hashed place and speed, so its position is a function of the clock. A sl |
| Flame | Flame | Game | class | Athanor/Assets/Scripts/Game/Fx/Flame.cs | 7 | A flickering flame: cycles its frames and wavers the light and halo that go with it. |
| Flipbook | Flipbook | Game | class | Athanor/Assets/Scripts/Game/Fx/Flipbook.cs | 8 | Plays a FlipbookSheet on a SpriteRenderer. |
| FlipbookSheet | FlipbookSheet | Game | class | Athanor/Assets/Scripts/Game/Fx/FlipbookSheet.cs | 6 | One exported effect: its frames in order and how long each is shown. |
| Fx | Fx | Game | class | Athanor/Assets/Scripts/Game/Fx/Fx.cs | 6 | Spawns exported pixelfx effects by name. |
| ScreenFx | ScreenFx | Game | class | Athanor/Assets/Scripts/Game/Fx/ScreenFx.cs | 7 | Whole-screen effects: a white flash, camera shake, and fading to and from black. |
| SparkMesh | SparkMesh | Game | class | Athanor/Assets/Scripts/Game/Fx/SparkMesh.cs | 13 | Many 1 px dots and 5x5 sparkles drawn as one mesh at one sorting order: one renderer and at most two draws (one per texture) instead of a SpriteRenderer each. Fill it every frame with Begin, Add, End. |
| SparkParticles | SparkParticles | Game | class | Athanor/Assets/Scripts/Game/Fx/SparkParticles.cs | 13 | Sparks drawn by ParticleSystems (the 3.3 design in docs/devlogs/phase-4-assets.md), only compiled with ATHANOR_SPARK_PARTICLES. Not shipped: it pulls the ParticleSystem engine module into the wasm, wh |
| Sparks | Sparks | Game | class | Athanor/Assets/Scripts/Game/Fx/Sparks.cs | 13 | Glowing pixel particles: bursts, trails and rising embers. Particles are plain structs integrated here; each sorting order is one lane drawn by one SparkMesh (one renderer, at most two draws), so a bu |
| Particle | Particle | Game | struct | Athanor/Assets/Scripts/Game/Fx/Sparks.cs | 20 |  |
| Lane | Lane | Game | class | Athanor/Assets/Scripts/Game/Fx/Sparks.cs | 25 |  |
| Anim | Anim | Game | class | Athanor/Assets/Scripts/Game/Support/Anim.cs | 11 | Beats (Wait), easing curves, and Over for per-frame scripts that are not a move or a fade (the spark trails in GameFlow). Moves, fades and scales of objects go through Motion (PrimeTween, snapped to p |
| ArtLibrary | ArtLibrary | Game | class | Athanor/Assets/Scripts/Game/Support/ArtLibrary.cs | 8 | Every sprite, effect sheet, font and material the game looks up by name. Built by Athanor > Build Scene. |
| Clickable | Clickable | Game | class | Athanor/Assets/Scripts/Game/Support/Clickable.cs | 7 | A rectangle around this object that the pointer can hover and click. The highest order under the pointer wins. |
| KindColors | KindColors | Game | class | Athanor/Assets/Scripts/Game/Support/KindColors.cs | 4 | The label colour for each kind of material. |
| Label | Label | Game | class | Athanor/Assets/Scripts/Game/Support/Label.cs | 7 | A line of pixel text with an optional one-pixel drop shadow. |
| LayoutData | LayoutData | Game | class | Athanor/Assets/Scripts/Game/Support/LayoutData.cs | 11 |  |
| Make | Make | Game | class | Athanor/Assets/Scripts/Game/Support/Make.cs | 11 | Factories for the sprites, text and lights the views are built from. |
| FontKind | FontKind | Game | enum | Athanor/Assets/Scripts/Game/Support/Make.cs | 7 |  |
| Align | Align | Game | enum | Athanor/Assets/Scripts/Game/Support/Make.cs | 8 |  |
| Motion | Motion | Game | class | Athanor/Assets/Scripts/Game/Support/Motion.cs | 16 | One-shot tweens on PrimeTween, kept in one place so every move is snapped to whole pixels (Px.Snap). Each call takes its start value explicitly and returns the Tween: group several with Sequence.Creat |
| PointerInput | PointerInput | Game | class | Athanor/Assets/Scripts/Game/Support/PointerInput.cs | 8 | Mouse or touch position in world space, press state for this frame, and hover/click dispatch to Clickables. |
| Px | Px | Game | class | Athanor/Assets/Scripts/Game/Support/Px.cs | 9 | The art is laid out on a 640x360 canvas with y running down from the top-left corner. The world uses one unit per pixel with the origin at the screen centre and y up. |
| Wording | Wording | Game | class | Athanor/Assets/Scripts/Game/Support/Wording.cs | 9 | The words and colours the Phase 3 screens share: rarities, aspects, event effects and goal rewards. |
| ChoiceCard | ChoiceCard | Game | class | Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs | 11 | What one card of the chooser shows. |
| CatalystChoiceView | CatalystChoiceView | Game | class | Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs | 23 | Three parchment cards, pick one: a catalyst after a depth is cleared, or an aspect at the start of a run and of depths 3 and 5. An aspect's card shows the tile it would land on. |
| Card | Card | Game | class | Athanor/Assets/Scripts/Game/Ui/CatalystChoiceView.cs | 28 |  |
| EventView | EventView | Game | class | Athanor/Assets/Scripts/Game/Ui/EventView.cs | 13 | The event at the start of a depth: what was found, the options that can be taken (each with what it costs and may give), then what came of the one chosen. |
| EventView.Row | EventView.Row | Game | class | Athanor/Assets/Scripts/Game/Ui/EventView.cs | 18 |  |
| GoalToast | GoalToast | Game | class | Athanor/Assets/Scripts/Game/Ui/GoalToast.cs | 11 | A small panel at the top of the screen that says a goal was completed and what it unlocked. Toasts queue; each stays 2.5 seconds or until clicked. It never blocks anything under it but itself. |
| GoalsView | GoalsView | Game | class | Athanor/Assets/Scripts/Game/Ui/GoalsView.cs | 12 | The Goals page: every long-term goal with its progress, grouped by the rarity of what it unlocks. Before a goal is done its reward is named only by kind and rarity; afterwards the item it gave is show |
| GoalsView.Row | GoalsView.Row | Game | class | Athanor/Assets/Scripts/Game/Ui/GoalsView.cs | 20 |  |
| JournalView | JournalView | Game | class | Athanor/Assets/Scripts/Game/Ui/JournalView.cs | 12 | The journal: every material as an icon (a silhouette until it has been made), and for the selected one the reactions found so far, how many are still unknown, and any hints collected. |
| RunOverView | RunOverView | Game | class | Athanor/Assets/Scripts/Game/Ui/RunOverView.cs | 9 | The summary at the end of a run, won or lost. |
| TitleView | TitleView | Game | class | Athanor/Assets/Scripts/Game/Ui/TitleView.cs | 7 | The title, shown over the candlelit workshop. |
| Tooltip | Tooltip | Game | class | Athanor/Assets/Scripts/Game/Ui/Tooltip.cs | 6 | A small panel with a title and two lines of text that appears beside whatever the pointer rests on. |
| UiButton | UiButton | Game | class | Athanor/Assets/Scripts/Game/Ui/UiButton.cs | 7 | A 120x26 red button with a label: brightens under the pointer, sinks when pressed. |
| AtlasBuilder | AtlasBuilder | Editor | class | Athanor/Assets/Editor/AtlasBuilder.cs | 27 | Two art steps that run after tools/art/export.mjs, both idempotent: 1. Sprite sheets: the numbered frames of each animation (Background/flame_small_0..7.png ...) are merged into one Multiple-mode shee |
| FlipbookImporter | FlipbookImporter | Editor | class | Athanor/Assets/Editor/FlipbookImporter.cs | 14 | Turns each pixelfx sheet in Assets/Art/Fx/Sheets (name.png + name.json, written by tools/fx/export.sh) into sliced sprites and a FlipbookSheet asset, then adds them to the art library so Fx.Spawn(name |
| FontBuilder | FontBuilder | Editor | class | Athanor/Assets/Editor/FontBuilder.cs | 8 | Bakes the pixel fonts into TextMeshPro bitmap font assets at their native size (crisp, point-filtered). |
| SceneBuilder | SceneBuilder | Editor | class | Athanor/Assets/Editor/SceneBuilder.cs | 16 | Builds Assets/Scenes/Main.unity and the art library from the exported sprites. Run it again after regenerating art (node tools/art/export.mjs) or exporting effect sheets. Hand edits to the scene are o |
| SpriteImportRules | SpriteImportRules | Editor | class | Athanor/Assets/Editor/SpriteImportRules.cs | 6 | Every texture under Assets/Art is pixel art: a point-filtered, uncompressed sprite at 1 pixel per unit. |
| WebBuild | WebBuild | Editor | class | Athanor/Assets/Editor/WebBuild.cs | 10 | Builds the game for the browser into Build/web. Also callable from the command line: unity build Athanor --target WebGL --execute-method WebBuild.Build |
| BalanceMatrixTests | BalanceMatrixTests | Tests | class | Athanor/Assets/Tests/EditMode/BalanceMatrixTests.cs | 13 | The informed scripted player on fixed profiles, to see what each part of Phase 3 does to the win rate. dotnet test tools/CoreTests --filter BalanceMatrix -l "console;verbosity=detailed" |
| ChainTests | ChainTests | Tests | class | Athanor/Assets/Tests/EditMode/ChainTests.cs | 8 | The chain planner on TestContent.Combo(): p 8, q 5, r 2, s 9, t 4, v 1 (Reaction Index). |
| ContentTests | ContentTests | Tests | class | Athanor/Assets/Tests/EditMode/ContentTests.cs | 9 |  |
| IgniteTests | IgniteTests | Tests | class | Athanor/Assets/Tests/EditMode/IgniteTests.cs | 8 |  |
| TestLang | TestLang | Tests | class | Athanor/Assets/Tests/EditMode/LangTests.cs | 10 | Loads the real language files (Resources/lang/en.json, vi.json), inside Unity or under plain dotnet. |
| LangTests | LangTests | Tests | class | Athanor/Assets/Tests/EditMode/LangTests.cs | 22 |  |
| LoreTests | LoreTests | Tests | class | Athanor/Assets/Tests/EditMode/LoreTests.cs | 9 | Lore events: the last one seen is not drawn again until another Lore event has been seen. |
| Phase3Tests | Phase3Tests | Tests | class | Athanor/Assets/Tests/EditMode/Phase3Tests.cs | 10 | Phase 3: the profile and its goals, unlocks, the new catalysts, bench rules, aspects, events and the start of a depth. |
| RoleSimTests | RoleSimTests | Tests | class | Athanor/Assets/Tests/EditMode/RoleSimTests.cs | 17 | Two simulated players on the full Phase 3 game (unlocks, aspects, events), each starting from an empty journal and a fresh profile and keeping both from run to run. Admin: knows every recipe and how a |
| Newcomer | Newcomer | Tests | class | Athanor/Assets/Tests/EditMode/RoleSimTests.cs | 21 |  |
| Career | Career | Tests | class | Athanor/Assets/Tests/EditMode/RoleSimTests.cs | 334 |  |
| RunStateTests | RunStateTests | Tests | class | Athanor/Assets/Tests/EditMode/RunStateTests.cs | 9 |  |
| RunStats | RunStats | Tests | class | Athanor/Assets/Tests/EditMode/RunStateTests.cs | 294 | What happened over one scripted run, for the balance report. |
| SatchelTests | SatchelTests | Tests | class | Athanor/Assets/Tests/EditMode/SatchelTests.cs | 8 |  |
| JournalTests | JournalTests | Tests | class | Athanor/Assets/Tests/EditMode/SatchelTests.cs | 45 |  |
| TestContent | TestContent | Tests | class | Athanor/Assets/Tests/EditMode/TestContent.cs | 6 | Loads the real content.json, inside Unity or under plain dotnet. |
| TutorialTests | TutorialTests | Tests | class | Athanor/Assets/Tests/EditMode/TutorialTests.cs | 9 | Phase 4: the hooks the tutorial needs, and the tutorial script replayed with every number it shows. |
| HudTests | HudTests | Tests | class | Athanor/Assets/Tests/EditModeGame/HudTests.cs | 9 | The Redraw button is live only while a redraw is left and the bench is in play. |
| MotionTests | MotionTests | Tests | class | Athanor/Assets/Tests/EditModeGame/MotionTests.cs | 13 | Motion's helpers make live PrimeTween tweens. PrimeTween refuses a payload over 16 bytes: it logs an error and returns a dead tween, which a Sequence then refuses too (the rack flight and the catalyst |
| SaveTests | SaveTests | Tests | class | Athanor/Assets/Tests/EditModeGame/SaveTests.cs | 9 | The journal and the profile survive a round trip through PlayerPrefs, and a damaged save never stops the game. |
| SparksTests | SparksTests | Tests | class | Athanor/Assets/Tests/EditModeGame/SparksTests.cs | 9 | Sparks on batched meshes: same motion and fade as the old SpriteRenderer pool, whole-pixel centres, HDR kept, no garbage. |

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
| BenchRules | field | MaxRi | 13 | public const int MaxRi = 13 |
| BenchRules | field | Diagonals | 15 | public bool Diagonals |
| BenchRules | field | HeatPlus | 16 | public int RiShift, HeatPlus |
| BenchRules | field | Pivot | 17 | public int Pivot = -1 |
| BenchRules | field | Blocked | 18 | public readonly bool[] Blocked = new bool[Bench.TileCount] |
| BenchRules | field | RiBonus | 19 | public readonly int[] RiBonus = new int[Bench.TileCount] |
| BenchRules | field | Kiln | 20 | public readonly bool[] Kiln = new bool[Bench.TileCount], Crossroads = new bool[Bench.TileCount] |
| BenchRules | method | List | 21 | public readonly List<(int a, int b)> Links = new List<(int, int)>() |
| BenchRules | property | HubMult | 23 | public readonly int[] HubMult = |
| BenchRules | field | Heavy | 24 | public readonly bool[] Heavy = new bool[Bench.TileCount] |
| BenchRules | field | AttunedKind | 25 | public readonly string[] AttunedKind = new string[Bench.TileCount] |
| BenchRules | field | SeedBonus | 26 | public readonly int[] SeedBonus = new int[Bench.TileCount] |
| BenchRules | property | None | 29 | public static readonly BenchRules None = new BenchRules(), Diagonal = new BenchRules |
| BenchRules | method | Plain | 30 | public static BenchRules Plain(bool diagonals) |
| BenchRules | method | Around | 35 | public int[] Around(int tile) |
| BenchRules | method | Ri | 72 | public int Ri(ContentDb db, int tile, string id) |
| BenchRules | method | Touch | 79 | public bool Touch(int a, int b) |
| ScoreContext | field | Depth | 85 | public int Depth = 1, EmptyTiles |
| ScoreContext | field | Rules | 86 | public BenchRules Rules = BenchRules.None |
| ScoreContext | field | EmptyAtIgnite | 87 | public bool[] EmptyAtIgnite |
| ScoreContext | field | TripleFirst | 88 | public bool TripleFirst |
| ScoreContext | field | IsStarter | 89 | public Func<string, bool> IsStarter |
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
| ContentData | field | materials | 30 | public MaterialData[] materials |
| ContentData | field | recipes | 31 | public RecipeData[] recipes |
| ContentData | field | catalysts | 32 | public CatalystData[] catalysts |
| ContentData | field | depthTargets | 33 | public int[] depthTargets |
| ContentData | field | depthNames | 34 | public string[] depthNames |
| ContentData | field | ultimate | 35 | public string ultimate |
| ContentData | field | goals | 36 | public GoalData[] goals |
| ContentData | field | aspects | 37 | public AspectData[] aspects |
| ContentData | field | events | 38 | public EventData[] events |
| ContentData | field | noEventWeight | 39 | public int noEventWeight |
| ProfileData | field | version | 49 | public int version = 1, rewardSeed |
| ProfileData | field | comboLargest | 50 | public int runsStarted, runsWon, wonNoRedraw, depthReached, ignites, chainLongest, comboLargest |
| ProfileData | field | bestIgnite | 51 | public long bestIgnite |
| ProfileData | field | unlockedCatalysts | 52 | public string[] completed, unlockedStarters, unlockedCatalysts |
| ProfileData | field | rewards | 53 | public RewardLogData[] rewards |
| ProfileData | field | lastLore | 54 | public string lastLore |
| Rarities | field | Common | 59 | public const string Common = "Common", Uncommon = "Uncommon", Rare = "Rare", Legendary = |
| Rarities | property | All | 60 | public static readonly string[] All = |
| Rarities | method | Rank | 61 | public static int Rank(string rarity) |
| Rarities | method | Of | 62 | public static string Of(string rarity) |
| Effects | field | KindDouble | 68 | public const string KindDouble = "KindDouble", CentreTwice = "CentreTwice", FirstTriple = "FirstTriple", Diagonals = "Diagonals", ExtraTrial = "ExtraTrial", ExtraHand = "ExtraHand", DiscoveryTriple =  |
| Effects | property | All | 76 | public static readonly string[] All = |
| AspectEffects | field | Doubled | 87 | public const string Doubled = "Doubled", Anchor = "Anchor", Heated = "Heated", Echo = "Echo", Attuned = "Attuned", Seed = "Seed", Fallow = "Fallow", Heavy = "Heavy", Cold = "Cold", Volatile = "Volatil |
| AspectEffects | property | All | 90 | public static readonly string[] All = |
| Ops | field | Trials | 99 | public const string Trials = "Trials", Hand = "Hand", Redraws = "Redraws", RiShift = "RiShift", GiveDiscovery = "GiveDiscovery", LoseRack = "LoseRack", LoseCard = "LoseCard", FirstTriple = "FirstTripl |
| Ops | property | All | 102 | public static readonly string[] All = |
| Ops | field | NeedRack | 107 | public const string NeedRack = "Rack", NeedCatalyst = "Catalyst", NeedTrial = "Trial", NeedRedraw = |
| Ops | property | Needs | 108 | public static readonly string[] Needs = |
| Ops | property | Types | 109 | public static readonly string[] Types = |
| StatIds | field | RunsStarted | 115 | public const string RunsStarted = "runsStarted", RunsWon = "runsWon", WonNoRedraw = "wonNoRedraw", DepthReached = "depthReached", Ignites = "ignites", ChainLongest = "chainLongest", ComboLargest = "co |
| StatIds | property | All | 118 | public static readonly string[] All = |
| Kind | value | Ore | 5 | Ore |
| Kind | value | Metal | 5 | Metal |
| Kind | value | Crystal | 5 | Crystal |
| Kind | value | Powder | 5 | Powder |
| Kind | value | Potion | 5 | Potion |
| Kind | value | Plant | 5 | Plant |
| Kind | value | Beast | 5 | Beast |
| ContentDb | field | MinItems | 19 | public const int MinItems = 2, MaxItems = 5, MaxCopies = 2, MinReactivity = 1, MaxReactivity = 10 |
| ContentDb | ctor | ContentDb | 23 | public ContentDb(ContentData data) |
| ContentDb | method | LockedIn | 141 | public List<string> LockedIn(string rewardKind, string rarity) |
| ContentDb | property | Starters | 149 | public IReadOnlyList<MaterialData> Starters |
| ContentDb | property | Materials | 150 | public IReadOnlyList<MaterialData> Materials |
| ContentDb | property | Recipes | 151 | public IReadOnlyList<RecipeData> Recipes |
| ContentDb | property | Catalysts | 152 | public IReadOnlyList<CatalystData> Catalysts |
| ContentDb | property | Goals | 153 | public IReadOnlyList<GoalData> Goals |
| ContentDb | property | Aspects | 154 | public IReadOnlyList<AspectData> Aspects |
| ContentDb | property | Events | 155 | public IReadOnlyList<EventData> Events |
| ContentDb | property | NoEventWeight | 156 | public int NoEventWeight |
| ContentDb | method | Catalyst | 157 | public CatalystData Catalyst(string id) |
| ContentDb | property | Ultimate | 158 | public string Ultimate |
| ContentDb | method | Material | 160 | public MaterialData Material(string id) |
| ContentDb | method | Has | 161 | public bool Has(string id) |
| ContentDb | method | KindOf | 162 | public Kind KindOf(string id) |
| ContentDb | method | PartnersOf | 163 | public IEnumerable<string> PartnersOf(string id) |
| ContentDb | method | Reactivity | 165 | public int Reactivity(string id) |
| ContentDb | method | TotalReactivity | 166 | public int TotalReactivity(RecipeData r) |
| ContentDb | method | KeyOf | 167 | public string KeyOf(RecipeData r) |
| ContentDb | method | Company | 170 | public int Company(RecipeData r) |
| ContentDb | method | RecipesContaining | 173 | public IReadOnlyList<RecipeData> RecipesContaining(string id) |
| ContentDb | method | TryCombo | 176 | public bool TryCombo(IEnumerable<string> items, out RecipeData recipe) |
| ContentDb | method | TryReact | 178 | public bool TryReact(string a, string b, out string result) |
| ContentDb | method | TargetFor | 186 | public int TargetFor(int depth) |
| ContentDb | method | DepthName | 192 | public string DepthName(int depth) |
| ContentDb | method | BasePotency | 199 | public static long BasePotency(int tier) |
| ContentDb | method | PairKey | 201 | public static string PairKey(string a, string b) |
| ContentDb | method | ComboKey | 204 | public static string ComboKey(IEnumerable<string> items) |
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
| Profile | field | RewardSeed | 22 | public int RewardSeed |
| Profile | field | LastLore | 24 | public string LastLore |
| Profile | ctor | Profile | 29 | public Profile(int rewardSeed = 1) |
| Profile | property | Completed | 31 | public IReadOnlyCollection<string> Completed |
| Profile | property | UnlockedStarters | 32 | public IReadOnlyCollection<string> UnlockedStarters |
| Profile | property | UnlockedCatalysts | 33 | public IReadOnlyCollection<string> UnlockedCatalysts |
| Profile | property | Rewards | 34 | public IReadOnlyList<RewardLogData> Rewards |
| Profile | method | IsComplete | 35 | public bool IsComplete(string goalId) |
| Profile | method | HasUnlocked | 36 | public bool HasUnlocked(string id) |
| Profile | method | RewardOf | 38 | public string RewardOf(string goalId) |
| Profile | method | RunStarted | 42 | public void RunStarted() |
| Profile | method | EventShown | 45 | public void EventShown(EventData e) |
| Profile | method | DepthBegan | 47 | public void DepthBegan(int depth) |
| Profile | method | IgniteResolved | 49 | public void IgniteResolved(IgniteOutcome outcome) |
| Profile | method | RunEnded | 62 | public void RunEnded(RunState run) |
| Profile | method | Stat | 72 | public long Stat(string id, ContentDb db, Journal journal) |
| Profile | method | Progress | 91 | public long Progress(GoalData goal, ContentDb db, Journal journal) |
| Profile | method | Evaluate | 98 | public List<GoalReward> Evaluate(ContentDb db, Journal journal) |
| Profile | method | ToData | 120 | public ProfileData ToData() |
| Profile | method | FromData | 129 | public static Profile FromData(ProfileData d, ContentDb db = null, int freshSeed = 1) |
| Unlocks | method | IsOpen | 157 | public bool IsOpen(string id) |
| Unlocks | property | Count | 158 | public int Count |
| Unlocks | method | Fresh | 161 | public static Unlocks Fresh(ContentDb db) |
| Unlocks | method | All | 169 | public static Unlocks All(ContentDb db) |
| Unlocks | method | For | 178 | public static Unlocks For(ContentDb db, Profile profile) |
| Unlocks | method | Half | 188 | public static Unlocks Half(ContentDb db, int seed) |
| RunOptions | field | Unlocks | 206 | public Unlocks Unlocks |
| RunOptions | field | Events | 207 | public bool Aspects, Events |
| RunOptions | field | ForceEvent | 209 | public string ForceEvent |
| RunOptions | field | ForceEventAt | 211 | public int ForceEventAt |
| RunOptions | field | QuietBefore | 213 | public int QuietBefore |
| RunOptions | field | FirstOffer | 215 | public string[] FirstOffer |
| RunOptions | field | LastLore | 217 | public string LastLore |
| RunOptions | field | Legacy | 218 | public static readonly RunOptions Legacy = new RunOptions() |
| RunOptions | method | Full | 219 | public static RunOptions Full(Unlocks unlocks) |
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
| RunState | method | ChooseAspect | 131 | public void ChooseAspect(string id) |
| RunState | method | ChooseEventOption | 213 | public EventResult ChooseEventOption(int index) |
| RunState | property | DepthBlocked | 395 | public IReadOnlyList<int> DepthBlocked |
| RunState | property | DepthHeated | 396 | public IEnumerable<int> DepthHeated |
| RunState | field | MinHand | 22 | public const int MinHand = 3, BaseHand = 5, BaseTrials = 4, BaseRedraws = 2, FinalDepth = 5, FinalTrials = 6, StarterCopies = 2, OfferSize = 3 |
| RunState | field | RackSize | 24 | public const int RackSize = 12 |
| RunState | ctor | RunState | 38 | public RunState(ContentDb db, Journal journal, int seed, int startDepth = 1, RunOptions options = null) |
| RunState | property | Depth | 55 | public int Depth |
| RunState | property | Potency | 56 | public long Potency |
| RunState | property | Target | 57 | public int Target |
| RunState | property | TrialsLeft | 58 | public int TrialsLeft |
| RunState | property | RedrawsLeft | 59 | public int RedrawsLeft |
| RunState | property | DiscoveriesThisRun | 60 | public int DiscoveriesThisRun |
| RunState | property | Phase | 61 | public RunPhase Phase |
| RunState | property | Bench | 62 | public Bench Bench |
| RunState | property | Hand | 63 | public IReadOnlyList<string> Hand |
| RunState | property | Catalysts | 64 | public IReadOnlyList<CatalystData> Catalysts |
| RunState | property | CatalystOffer | 65 | public IReadOnlyList<CatalystData> CatalystOffer |
| RunState | property | HandSize | 66 | public int HandSize |
| RunState | property | Diagonals | 67 | public bool Diagonals |
| RunState | property | CanIgnite | 68 | public bool CanIgnite |
| RunState | property | RedrawsUsed | 70 | public int RedrawsUsed |
| RunState | property | Options | 71 | public RunOptions Options |
| RunState | method | IsOpen | 72 | public bool IsOpen(string id) |
| RunState | method | Owned | 76 | public int Owned(string id) |
| RunState | field | PouchMode | 83 | public static int PouchMode = 2 |
| RunState | field | PouchCards | 84 | public const int PouchCards = 1 |
| RunState | method | HasFired | 85 | public bool HasFired(string a, string b) |
| RunState | method | HasFired | 86 | public bool HasFired(RecipeData recipe) |
| RunState | property | SatchelSize | 87 | public int SatchelSize |
| RunState | method | Place | 144 | public bool Place(int handIndex, int tile) |
| RunState | method | Unplace | 153 | public bool Unplace(int tile) |
| RunState | method | Redraw | 162 | public bool Redraw() |
| RunState | method | Deal | 178 | public bool Deal(IReadOnlyList<string> ids) |
| RunState | method | NewContext | 191 | public ScoreContext NewContext() |
| RunState | method | Preview | 197 | public List<ReactionEvent> Preview(string[] tiles) |
| RunState | method | Ignite | 205 | public IgniteOutcome Ignite() |
| RunState | method | ChooseCatalyst | 287 | public void ChooseCatalyst(string id) |
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
| TutorialLay | field | Id | 12 | public readonly string Id |
| TutorialLay | field | Tile | 13 | public readonly int Tile |
| TutorialLay | ctor | TutorialLay | 14 | public TutorialLay(string id, int tile) |
| TutorialTrial | field | Hand | 20 | public string[] Hand |
| TutorialTrial | field | Lay | 21 | public TutorialLay[] Lay |
| TutorialTrial | field | Makes | 22 | public string[] Makes |
| TutorialScript | field | Seed | 31 | public const int Seed = 20261113 |
| TutorialScript | field | QuietBefore | 33 | public const int QuietBefore = 3 |
| TutorialScript | property | Offer | 34 | public static readonly string[] Offer = |
| TutorialScript | property | Reaction | 37 | public static readonly TutorialTrial Reaction = new TutorialTrial |
| TutorialScript | property | Miss | 45 | public static readonly TutorialTrial Miss = new TutorialTrial |
| TutorialScript | property | Chain | 53 | public static readonly TutorialTrial Chain = new TutorialTrial |
| TutorialScript | property | Depth1 | 60 | public static readonly TutorialTrial[] Depth1 = |
| TutorialScript | method | Layout | 63 | public static string[] Layout(TutorialTrial trial) |
| TutorialScript | method | BrokenChain | 66 | public static string[] BrokenChain() |
| TutorialScript | method | CatalystTrial | 76 | public static TutorialTrial CatalystTrial(string catalystId) |
| TutorialScript | method | Options | 103 | public static RunOptions Options(Unlocks unlocks) |
| TutorialScript | method | ShouldRun | 109 | public static bool ShouldRun(bool tutorialDone, int journalEntries) |
| BenchView | field | JournalRequested | 16 | public event Action IgniteRequested, RedrawRequested, JournalRequested |
| BenchView | property | InputEnabled | 17 | public bool InputEnabled |
| BenchView | property | Hud | 18 | public HudView Hud |
| BenchView | property | Pick | 23 | struct Pick |
| BenchView | property | Slot | 24 | sealed class Slot |
| BenchView | method | Build | 36 | public void Build(LayoutData layout, ContentDb content, Transform parent) |
| BenchView | method | Bind | 74 | public void Bind(RunState state, Journal book) |
| BenchView | method | TileCentre | 82 | public Vector3 TileCentre(int i) |
| BenchView | method | RackSlot | 84 | public Vector3 RackSlot(int i) |
| BenchView | method | PreviewOnRack | 86 | public void PreviewOnRack(int i, string id) |
| BenchView | method | Tile | 91 | public TileView Tile(int i) |
| BenchView | method | Refresh | 94 | public void Refresh(bool deal = false) |
| BenchView | method | ClearLinks | 173 | public void ClearLinks() |
| BenchView | method | FloatText | 176 | public IEnumerator FloatText(string text, Vector3 from, Color color, float seconds = 0.8f, bool small = false) |
| From | value | None | 22 | None |
| From | value | Hand | 22 | Hand |
| From | value | Tile | 22 | Tile |
| CardView | field | W | 9 | public const int W = 74, H = 70 |
| CardView | field | handIndex | 10 | public int handIndex |
| CardView | field | id | 11 | public string id |
| CardView | field | home | 12 | public Vector3 home |
| CardView | field | Dimmed | 13 | public bool Lifted, Selected, Dimmed |
| CardView | method | Build | 24 | public void Build(MaterialData m, Transform parent, int order) |
| CardView | method | Fan | 56 | public void Fan(float visible, int front) |
| CardView | method | DealIn | 88 | public void DealIn(float wait) |
| CardView | method | Contains | 90 | public bool Contains(Vector2 world) |
| Dust | field | count | 11 | public int count = 46 |
| Dust | field | material | 13 | public Material material |
| Dust | field | sprite | 14 | public Sprite sprite |
| HudView | field | RedrawClicked | 12 | public event Action IgniteClicked, JournalClicked, RedrawClicked |
| HudView | property | HoveredCatalyst | 14 | public int HoveredCatalyst |
| HudView | property | RedrawHovered | 15 | public bool RedrawHovered |
| HudView | property | HoveredAspect | 17 | public int HoveredAspect |
| HudView | field | MaxCatalysts | 19 | public const int MaxCatalysts = 6, MaxAspects = 3 |
| HudView | field | RedrawX | 21 | public const int RedrawX = 556, RedrawY = 88, RedrawW = 76, RedrawH = 20 |
| HudView | property | PotencyPosition | 46 | public Vector3 PotencyPosition |
| HudView | method | CatalystAnchor | 48 | public Vector3 CatalystAnchor(int i) |
| HudView | method | AspectAnchor | 49 | public Vector3 AspectAnchor(int i) |
| HudView | property | RedrawAnchor | 51 | public Vector3 RedrawAnchor |
| HudView | method | CanRedraw | 53 | public static bool CanRedraw(RunState run) |
| HudView | method | Number | 54 | public static string Number(long n) |
| HudView | method | Build | 56 | public void Build(Transform parent) |
| HudView | method | Show | 130 | public void Show(RunState run, ContentDb db, Journal journal, bool inputEnabled, bool keepRedraw = false) |
| HudView | method | SetPotency | 178 | public void SetPotency(long value, bool instant = false) |
| HudView | method | Note | 181 | public void Note(ReactionEvent e, ContentDb db) |
| HudView | method | Hint | 198 | public void Hint(HintData h, ContentDb db) |
| HudView | method | HintLine | 205 | public static string HintLine(HintData h, ContentDb db, bool withName = true) |
| HudView | method | Bump | 213 | public void Bump() |
| HudView | method | ClearNote | 215 | public void ClearNote() |
| LinkView | field | color | 11 | public Color color = new Color(2.2f, 1.7f, 0.8f) |
| LinkView | field | speed | 12 | public float speed = 0.35f, arc = 7, alpha = 0.9f |
| LinkView | method | Set | 16 | public void Set(List<(Vector3 a, Vector3 b)> pairs) |
| TileView | field | index | 12 | public int index |
| TileView | field | centre | 13 | public Vector3 centre |
| TileView | property | Material | 23 | public string Material |
| TileView | property | Hovered | 24 | public bool Hovered |
| TileView | property | Pinged | 26 | public bool Pinged |
| TileView | property | Shut | 28 | public bool Shut |
| TileView | property | Marked | 29 | public bool Marked |
| TileView | property | Icon | 30 | public SpriteRenderer Icon |
| TileView | method | Build | 32 | public void Build(int i, TileData data, Transform parent) |
| TileView | method | Contains | 57 | public bool Contains(Vector2 world) |
| TileView | method | SetState | 61 | public void SetState(Color? marker, bool shut) |
| TileView | method | Show | 71 | public void Show(MaterialData m, bool pop) |
| TileView | method | Clear | 84 | public void Clear() |
| TileView | method | Flash | 92 | public void Flash(Color c) |
| TileView | method | Consume | 95 | public IEnumerator Consume() |
| GameFlow | field | library | 17 | public ArtLibrary library |
| GameFlow | field | cutsceneRoom | 18 | public GameObject workshop, cutsceneRoom |
| GameFlow | field | mainCamera | 19 | public Camera mainCamera |
| GameFlow | property | I | 21 | public static GameFlow I |
| GameFlow | property | Db | 22 | public ContentDb Db |
| GameFlow | property | Journal | 23 | public Journal Journal |
| GameFlow | property | Profile | 25 | public Profile Profile |
| GameFlow | property | Run | 26 | public RunState Run |
| GameFlow | property | Bench | 27 | public BenchView Bench |
| GameFlow | property | State | 28 | public FlowState State |
| GameFlow | property | RunOver | 29 | public RunOverView RunOver |
| GameFlow | field | Auto | 31 | public bool Auto |
| GameFlow | method | Ignite | 263 | public void Ignite() |
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
| Save | field | Key | 10 | public const string Key = "athanor.journal.v1", IntroKey = "athanor.intro.v1", ProfileKey = "athanor.profile.v1", TutorialKey = |
| Save | method | LoadJournal | 14 | public static Journal LoadJournal() |
| Save | method | StoreJournal | 24 | public static void StoreJournal(Journal journal) |
| Save | method | LoadProfile | 34 | public static Profile LoadProfile(ContentDb db) |
| Save | method | StoreProfile | 45 | public static void StoreProfile(Profile profile) |
| Save | method | ResetProfile | 52 | public static void ResetProfile() |
| Save | property | IntroSeen | 64 | public static bool IntroSeen |
| Save | property | TutorialDone | 71 | public static bool TutorialDone |
| Cutscene | property | Skipped | 9 | protected bool Skipped |
| Cutscene | property | Playing | 10 | protected bool Playing |
| Cutscene | method | Play | 12 | public abstract IEnumerator Play() |
| Cutscene | method | Skip | 14 | public void Skip() |
| Cutscene | method | Begin | 17 | protected void Begin() |
| Cutscene | method | ClearSkip | 18 | protected void ClearSkip() |
| Cutscene | method | End | 19 | protected void End() |
| Cutscene | method | OnEnable | 21 | protected virtual void OnEnable() |
| Cutscene | method | OnDisable | 22 | protected virtual void OnDisable() |
| ReactionCutscene | method | Build | 39 | public void Build(GameObject cutsceneRoom, params GameObject[] others) |
| ReactionCutscene | method | Setup | 92 | public void Setup(ReactionEvent e, ContentDb content, float hurry = 1) |
| ReactionCutscene | method | Play | 118 | public override IEnumerator Play() |
| SimpleCutscene | method | Build | 30 | public void Build(GameObject cutsceneRoom, GameObject workshopRoot, GameObject benchRoot) |
| SimpleCutscene | method | Play | 59 | public override IEnumerator Play() |
| SimpleCutscene | method | Close | 76 | public void Close() |
| SimpleCutscene | method | Intro | 110 | public IEnumerator Intro() |
| SimpleCutscene | method | Descent | 146 | public IEnumerator Descent(int depth, string depthName, Color colour, Action midway) |
| SimpleCutscene | method | Ending | 197 | public IEnumerator Ending(MaterialData ultimate) |
| Loop | ctor | Loop | 310 | public Loop(Transform parent, string name, int order, float rate, float life, float x0, float x1, float y0, float y1, float vx0, float vx1, float vy0, float vy1, float gravity, float sparkle) |
| Loop | method | Draw | 321 | public void Draw(float t, float strength, Color color) |
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
| ScreenFx | method | Build | 15 | public void Build(Camera camera) |
| ScreenFx | method | Flash | 25 | public void Flash(float peak, float seconds) |
| ScreenFx | method | Shake | 26 | public void Shake(float pixels, float seconds) |
| ScreenFx | method | FadeOut | 28 | public IEnumerator FadeOut(float seconds) |
| ScreenFx | method | FadeIn | 29 | public IEnumerator FadeIn(float seconds) |
| ScreenFx | method | SetBlack | 30 | public void SetBlack(float a) |
| SparkMesh | property | Vert | 15 | struct Vert |
| SparkMesh | property | Quad | 16 | struct Quad |
| SparkMesh | field | DotScale | 19 | public const float DotScale = 0.25f |
| SparkMesh | property | Count | 41 | public int Count |
| SparkMesh | method | Create | 44 | public static SparkMesh Create(Transform parent, string name, int order, Sprite dot, Sprite sparkle) |
| SparkMesh | method | MaterialFor | 54 | public static Material MaterialFor(Texture texture) |
| SparkMesh | method | Begin | 90 | public void Begin() |
| SparkMesh | method | Add | 92 | public void Add(Vector2 pos, Color color, bool sparkle) |
| SparkMesh | method | End | 99 | public void End() |
| SparkMesh | property | Mesh | 164 | public Mesh Mesh |
| SparkParticles | field | K | 15 | public const float K = 3 |
| SparkParticles | property | LiveCount | 24 | public int LiveCount |
| SparkParticles | method | Clear | 26 | public void Clear() |
| SparkParticles | method | Emit | 28 | public void Emit(Sparks owner, Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity, bool sparkle, float drag) |
| Sparks | property | I | 15 | public static Sparks I |
| Sparks | field | TrailRate | 18 | public const float TrailRate = 60 |
| Sparks | property | LiveCount | 36 | public int LiveCount |
| Sparks | property | LaneCount | 51 | public int LaneCount |
| Sparks | method | LaneMesh | 54 | public SparkMesh LaneMesh(int order) |
| Sparks | method | Emit | 57 | public void Emit(Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity = 0, bool sparkle = false, float drag = 0) |
| Sparks | method | Trail | 74 | public void Trail(Vector2 pos, Vector2 vel, Color color, float life, int order, float gravity = 0, bool sparkle = false, float drag = 0) |
| Sparks | method | TrailCount | 86 | public static int TrailCount(float dt, float r) |
| Sparks | method | Burst | 89 | public void Burst(Vector2 pos, int count, Color color, float speed, float life, int order, float gravity = 0, float squash = 1) |
| Sparks | method | Clear | 98 | public void Clear() |
| Sparks | method | Step | 107 | public static bool Step(ref Particle p, float dt) |
| Sparks | method | Shade | 118 | public static Color Shade(in Particle p) |
| Sparks | method | Advance | 135 | public void Advance(float dt) |
| Sparks | method | Draw | 146 | public void Draw() |
| Particle | field | vel | 22 | public Vector2 pos, vel |
| Lane | field | order | 27 | public int order |
| Anim | method | Over | 14 | public static IEnumerator Over(float seconds, Action<float> step) |
| Anim | method | Wait | 21 | public static IEnumerator Wait(float seconds, Func<bool> skip = null) |
| Anim | method | OutCubic | 26 | public static float OutCubic(float t) |
| Anim | method | InCubic | 27 | public static float InCubic(float t) |
| Anim | method | InOut | 28 | public static float InOut(float t) |
| Anim | method | OutBack | 29 | public static float OutBack(float t) |
| Anim | method | Hump | 31 | public static float Hump(float t) |
| ArtLibrary | field | sprites | 10 | public Sprite[] sprites |
| ArtLibrary | field | sheets | 11 | public FlipbookSheet[] sheets |
| ArtLibrary | field | tiny | 12 | public TMP_FontAsset title, body, label, tiny |
| ArtLibrary | field | glow | 13 | public Material lit, unlit, glow |
| ArtLibrary | method | Sprite | 18 | public Sprite Sprite(string name) |
| ArtLibrary | method | Sheet | 31 | public FlipbookSheet Sheet(string name) |
| Clickable | field | offset | 9 | public Vector2 size, offset |
| Clickable | field | order | 10 | public int order |
| Clickable | field | onUp | 11 | public Action onClick, onEnter, onExit, onDown, onUp |
| Clickable | method | Contains | 13 | public bool Contains(Vector2 world) |
| KindColors | method | Of | 6 | public static string Of(string kind) |
| Label | field | shadow | 9 | public TextMeshPro main, shadow |
| Label | property | Text | 11 | public string Text |
| Label | property | Color | 17 | public Color Color |
| Label | property | Alpha | 19 | public float Alpha |
| Label | property | Order | 28 | public int Order |
| Label | property | Width | 33 | public float Width |
| LayoutData | field | h | 13 | public int w, h |
| LayoutData | field | tiles | 14 | public TileData[] tiles |
| LayoutData | field | placed | 15 | public RectData[] placed |
| LayoutData | field | candles | 16 | public PointData[] candles |
| LayoutData | method | Placed | 19 | public RectData Placed(string name) |
| Make | field | Lib | 13 | public static ArtLibrary Lib |
| Make | method | Root | 15 | public static Transform Root(string name, Transform parent = null) |
| Make | method | Sprite | 23 | public static SpriteRenderer Sprite(Transform parent, string sprite, Vector3 worldPos, int order, Material material = null) |
| Make | method | SpriteAt | 36 | public static SpriteRenderer SpriteAt(Transform parent, string sprite, float x, float y, int order, Material material = null) |
| Make | method | Rect | 43 | public static SpriteRenderer Rect(Transform parent, float x, float y, float w, float h, Color color, int order) |
| Make | method | Glow | 53 | public static SpriteRenderer Glow(Transform parent, Vector3 worldPos, float radius, Color color, int order) |
| Make | method | Text | 62 | public static Label Text(Transform parent, string text, float x, float y, FontKind font, int size, Color color, int order, Align align = Align.Left, Color? shadow = null, float width = 0) |
| Make | method | Light | 101 | public static Light2D Light(Transform parent, Vector3 worldPos, float radius, Color color, float intensity, float squash = 1) |
| Make | method | Click | 117 | public static Clickable Click(GameObject go, Vector2 size, int order, System.Action onClick = null) |
| FontKind | value | Title | 7 | Title |
| FontKind | value | Body | 7 | Body |
| FontKind | value | Name | 7 | Name |
| FontKind | value | Tiny | 7 | Tiny |
| Align | value | Left | 8 | Left |
| Align | value | Centre | 8 | Centre |
| Align | value | Right | 8 | Right |
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
| PointerInput | property | Down | 11 | public static bool Down |
| PointerInput | property | Up | 12 | public static bool Up |
| PointerInput | property | Held | 13 | public static bool Held |
| PointerInput | property | Hover | 14 | public static Clickable Hover |
| PointerInput | field | AnyPress | 16 | public static event Action AnyPress |
| PointerInput | method | Register | 21 | public static void Register(Clickable c) |
| PointerInput | method | Unregister | 22 | public static void Unregister(Clickable c) |
| PointerInput | method | ScreenToWorld | 25 | public static Vector2 ScreenToWorld(Vector2 screen) |
| Px | field | W | 11 | public const int W = 640, H = 360 |
| Px | method | At | 14 | public static Vector3 At(float x, float y) |
| Px | method | Mid | 17 | public static Vector3 Mid(float x, float y, float w, float h) |
| Px | method | Snap | 19 | public static Vector3 Snap(Vector3 p) |
| Px | method | Hex | 22 | public static Color Hex(string hex, float a = 1, float k = 1) |
| Px | method | Alpha | 28 | public static Color Alpha(this Color c, float a) |
| Px | method | Times | 29 | public static Color Times(this Color c, float k) |
| Wording | method | RarityColor | 11 | public static Color RarityColor(string rarity) |
| Wording | method | AspectColor | 23 | public static Color AspectColor(string effect) |
| Wording | field | Warm | 43 | public static readonly Color Warm = Px.Hex("#ffa63a") |
| Wording | method | AspectRule | 48 | public static string AspectRule(AspectInstance a, bool cost = true) |
| Wording | method | AspectCost | 57 | public static string AspectCost(AspectInstance a) |
| Wording | method | Effect | 67 | public static string Effect(EffectData fx) |
| Wording | method | OptionSummary | 97 | public static string OptionSummary(EventOption o) |
| Wording | method | RewardLine | 115 | public static string RewardLine(GoalData g) |
| Wording | method | ItemName | 119 | public static string ItemName(ContentDb db, string id) |
| Wording | method | ItemSprite | 128 | public static string ItemSprite(ContentDb db, string id) |
| Wording | method | Number | 130 | public static string Number(long n) |
| ChoiceCard | field | cost | 13 | public string id, sprite, name, text, kicker, cost |
| ChoiceCard | field | pip | 14 | public Color? pip |
| ChoiceCard | field | tiles | 15 | public int[] tiles |
| ChoiceCard | field | dimTiles | 16 | public int[] dimTiles |
| CatalystChoiceView | method | Build | 43 | public void Build() |
| CatalystChoiceView | method | Choose | 79 | public IEnumerator Choose(IReadOnlyList<CatalystData> offer, Action<string> done, float autoAfter = -1) |
| CatalystChoiceView | method | Choose | 92 | public IEnumerator Choose(IReadOnlyList<AspectInstance> offer, Action<string> done, float autoAfter = -1) |
| CatalystChoiceView | method | Choose | 106 | public IEnumerator Choose(string title, string line, IReadOnlyList<ChoiceCard> offer, Action<string> done, float autoAfter = -1) |
| Card | field | root | 30 | public Transform root |
| Card | field | bench | 31 | public readonly SpriteRenderer[] bench = new SpriteRenderer[Bench.TileCount] |
| EventView | method | Build | 32 | public void Build() |
| EventView | method | Show | 68 | public IEnumerator Show(RunState run, Action changed) |
| EventView.Row | field | root | 20 | public Transform root |
| GoalToast | property | Item | 16 | struct Item |
| GoalToast | method | Build | 24 | public void Build() |
| GoalToast | method | Add | 40 | public void Add(IEnumerable<GoalReward> rewards, ContentDb db) |
| GoalToast | method | Clear | 52 | public void Clear() |
| GoalsView | field | Closed | 17 | public event Action Closed |
| GoalsView | property | IsOpen | 18 | public bool IsOpen |
| GoalsView | method | Build | 34 | public void Build(ContentDb content) |
| GoalsView | method | Open | 81 | public void Open(Profile profile, Journal journal) |
| GoalsView | method | Close | 124 | public void Close() |
| GoalsView.Row | field | root | 22 | public Transform root |
| JournalView | field | Closed | 19 | public event Action Closed |
| JournalView | property | IsOpen | 20 | public bool IsOpen |
| JournalView | method | Build | 39 | public void Build(ContentDb content) |
| JournalView | method | Open | 96 | public void Open(Journal book, Func<string, bool> openStarters = null) |
| JournalView | method | Close | 146 | public void Close() |
| RunOverView | property | Again | 13 | public bool Again |
| RunOverView | field | GoalsClicked | 15 | public event System.Action GoalsClicked |
| RunOverView | method | Build | 22 | public void Build() |
| RunOverView | method | Pick | 48 | public void Pick(bool again) |
| RunOverView | method | Show | 50 | public IEnumerator Show(bool won, RunState run, Journal journal, ContentDb db, System.Collections.Generic.IReadOnlyList<GoalReward> goals) |
| TitleView | property | BeginPressed | 10 | public bool BeginPressed |
| TitleView | field | ResetConfirmed | 11 | public event Action JournalClicked, GoalsClicked, ResetConfirmed |
| TitleView | property | Asking | 13 | public bool Asking |
| TitleView | method | Build | 22 | public void Build() |
| TitleView | method | Open | 67 | public void Open(int known, int total, int goalsDone, int goals) |
| TitleView | method | Progress | 77 | public void Progress(int known, int total, int goalsDone, int goals) |
| TitleView | method | Close | 83 | public void Close() |
| Tooltip | method | Build | 11 | public void Build(Transform parent) |
| Tooltip | method | Show | 23 | public void Show(string heading, string text, Vector2 anchor, float clearance = 20) |
| Tooltip | method | Hide | 34 | public void Hide() |
| UiButton | field | W | 9 | public const int W = 120, H = 26 |
| UiButton | method | Create | 15 | public static UiButton Create(Transform parent, string text, float x, float y, int order, Action onClick) |
| AtlasBuilder | field | ArtDir | 29 | public const string ArtDir = "Assets/Art", AtlasDir = "Assets/Art/Atlases", LibraryPath = |
| AtlasBuilder | field | Padding | 30 | public const int Padding = 2, MaxSize = 2048, SheetGap = 2 |
| AtlasBuilder | property | Animations | 34 | public static readonly string[] Animations = |
| AtlasBuilder | method | IsSpriteSheet | 37 | public static bool IsSpriteSheet(string assetPath) |
| AtlasBuilder | property | Standalone | 40 | public static readonly Dictionary<string, string> Standalone = new Dictionary<string, string> |
| AtlasBuilder | method | readonly | 49 | public static readonly (string name, string rule, Func<string, string, bool> match)[] Groups = |
| AtlasBuilder | method | BuildAll | 66 | public static string BuildAll() |
| AtlasBuilder | method | BuildBatch | 78 | public static void BuildBatch() |
| AtlasBuilder | method | PackAndReport | 86 | public static string PackAndReport() |
| AtlasBuilder | method | PackBatch | 107 | public static void PackBatch() |
| AtlasBuilder | method | BuildSpriteSheets | 124 | public static string BuildSpriteSheets() |
| AtlasBuilder | method | Assign | 283 | public static Dictionary<string, List<string>> Assign() |
| AtlasBuilder | method | BuildAtlases | 301 | public static string BuildAtlases() |
| FlipbookImporter | field | Dir | 16 | public const string Dir = |
| FlipbookImporter | method | Import | 26 | public static string Import() |
| FontBuilder | method | Build | 13 | public static string Build() |
| SceneBuilder | method | Build | 23 | public static string Build() |
| SceneBuilder | method | BuildBatch | 66 | public static void BuildBatch() |
| SceneBuilder | method | RefreshLibrary | 69 | public static ArtLibrary RefreshLibrary() |
| SpriteImportRules | method | GetVersion | 9 | public override uint GetVersion() |
| WebBuild | field | Output | 12 | public const string Output = "Build/web", DevOutput = |
| WebBuild | method | Build | 15 | public static void Build() |
| WebBuild | method | BuildQuick | 19 | public static void BuildQuick() |
| WebBuild | method | BuildDev | 26 | public static void BuildDev() |
| BalanceMatrixTests | method | With | 15 | public static Unlocks With(ContentDb db, bool starters, bool catalysts) |
| BalanceMatrixTests | method | Autoplay | 25 | public static RunState Autoplay(ContentDb db, int seed, RunOptions options) |
| BalanceMatrixTests | method | Line | 34 | public static string Line(ContentDb db, string label, Func<int, RunOptions> options, int seeds) |
| BalanceMatrixTests | method | RandomPlay | 68 | public static RunState RandomPlay(ContentDb db, int seed, RunOptions options) |
| ContentTests | method | EventTexts_FitTheEventWindow | 316 | public void EventTexts_FitTheEventWindow() |
| ContentTests | method | LoreEvents_DoNothing_AndTellTheStoryInOrder | 328 | public void LoreEvents_DoNothing_AndTellTheStoryInOrder() |
| TestLang | method | Text | 12 | public static string Text(string code) |
| LoreTests | method | TheLastLoreEvent_IsNotDrawn | 50 | public void TheLastLoreEvent_IsNotDrawn() |
| LoreTests | method | WithoutALastLore_EveryLoreEventCanCome | 63 | public void WithoutALastLore_EveryLoreEventCanCome() |
| LoreTests | method | ALastLore_ThatIsNotALoreEvent_ChangesNothing | 72 | public void ALastLore_ThatIsNotALoreEvent_ChangesNothing() |
| LoreTests | method | InARun_ALoreEvent_IsNotShownTwiceInARow | 85 | public void InARun_ALoreEvent_IsNotShownTwiceInARow() |
| LoreTests | method | AnotherLoreEvent_InBetween_LetsTheFirstComeBack | 98 | public void AnotherLoreEvent_InBetween_LetsTheFirstComeBack() |
| LoreTests | method | ANonLoreEvent_InBetween_DoesNotLetItComeBack | 116 | public void ANonLoreEvent_InBetween_DoesNotLetItComeBack() |
| LoreTests | method | SkippingALoreEvent_IsBalanceNeutral | 155 | public void SkippingALoreEvent_IsBalanceNeutral() |
| LoreTests | method | Profile_RemembersOnlyLoreEvents | 183 | public void Profile_RemembersOnlyLoreEvents() |
| LoreTests | method | Profile_LastLore_IsSaved_AndDroppedWhenNoLongerALoreEvent | 195 | public void Profile_LastLore_IsSaved_AndDroppedWhenNoLongerALoreEvent() |
| Newcomer | ctor | Newcomer | 29 | public Newcomer(ContentDb truth, Journal journal) |
| Newcomer | method | List | 79 | public readonly List<(string a, string b)> targets = new List<(string, string)>() |
| Newcomer | property | TriedCount | 80 | public int TriedCount |
| Newcomer | property | ColdCount | 81 | public int ColdCount |
| Newcomer | method | Status | 82 | public string Status(string a, string b) |
| Newcomer | method | CanMakeNow | 83 | public bool CanMakeNow(string m) |
| Newcomer | method | Decide | 126 | public bool Decide(RunState run) |
| Newcomer | method | Learn | 318 | public void Learn(IgniteOutcome outcome, BenchRules rules) |
| Career | field | FirstWin | 336 | public int FirstWin = -1 |
| Career | field | Won | 337 | public readonly List<bool> Won = new List<bool>() |
| Career | field | Depth | 338 | public readonly List<int> Depth = new List<int>(), Journal = new List<int>(), Goals = new List<int>(), Unlocked = new List<int>() |
| Career | field | Stuck | 339 | public int Stuck |
| Career | field | Knows | 340 | public string Knows = |
| RunStats | field | BySize | 296 | public readonly int[] BySize = new int[ContentDb.MaxItems + 1] |
| RunStats | field | FirstTrialClears | 297 | public int Ignites, LongestChain, Sputters, FirstTrialClears |
| RunStats | field | UltimateByCombo | 298 | public bool UltimateByCombo |
| RunStats | field | ComboMade | 299 | public readonly HashSet<string> ComboMade = new HashSet<string>() |
| RunStats | field | Combos | 300 | public readonly Dictionary<string, int> Combos = new Dictionary<string, int>() |
| RunStats | field | ClearedIn | 301 | public readonly List<string> ClearedIn = new List<string>() |
| TestContent | method | Load | 8 | public static ContentData Load() |
| TestContent | method | Tiny | 21 | public static ContentData Tiny() |
| TestContent | method | Combo | 42 | public static ContentData Combo(int zMin = 15) |
| TestContent | method | Mat | 66 | public static MaterialData Mat(string id, string kind, int tier, int reactivity = 5) |
| TestContent | method | Rec | 68 | public static RecipeData Rec(string a, string b, string r) |
| TestContent | method | Rec | 69 | public static RecipeData Rec(string[] items, int min, string r) |
| TestContent | method | Cat | 70 | public static CatalystData Cat(string effect, string kind = "", float amount = 0) |
| TutorialTests | method | Satchel_Take_PullsANamedCard_FromDrawOrDiscard | 17 | public void Satchel_Take_PullsANamedCard_FromDrawOrDiscard() |
| TutorialTests | method | Deal_ReplacesTheStartingHand_AndKeepsTheSatchelWhole | 45 | public void Deal_ReplacesTheStartingHand_AndKeepsTheSatchelWhole() |
| TutorialTests | method | Deal_KeepsTheRack | 55 | public void Deal_KeepsTheRack() |
| TutorialTests | method | Deal_Refuses_TooManyCopies_NonStarters_LockedStarters_AndABusyBench | 65 | public void Deal_Refuses_TooManyCopies_NonStarters_LockedStarters_AndABusyBench() |
| TutorialTests | method | QuietBefore_HoldsAspectsAndEventsBack_UntilThatDepth | 81 | public void QuietBefore_HoldsAspectsAndEventsBack_UntilThatDepth() |
| TutorialTests | method | FirstOffer_IsFixed_ForTheFirstOfferOnly | 108 | public void FirstOffer_IsFixed_ForTheFirstOfferOnly() |
| TutorialTests | method | FirstOffer_LeavesOutLockedCatalysts | 121 | public void FirstOffer_LeavesOutLockedCatalysts() |
| TutorialTests | method | Preview_ScoresALayout_AndChangesNothing | 140 | public void Preview_ScoresALayout_AndChangesNothing() |
| TutorialTests | method | ScoreParts_SplitsAScore_IntoItsFactors | 156 | public void ScoreParts_SplitsAScore_IntoItsFactors() |
| TutorialTests | method | Trial1_EmberCoalAndDeepTear_MakeSteamWisp_For40 | 182 | public void Trial1_EmberCoalAndDeepTear_MakeSteamWisp_For40() |
| TutorialTests | method | Trial2_GlowmossAndIronOre_Fizzle_SpendATrial_AndHintPotionOnGlowmoss | 197 | public void Trial2_GlowmossAndIronOre_Fizzle_SpendATrial_AndHintPotionOnGlowmoss() |
| TutorialTests | method | Trial3_Chain_SkyIronThenStarmetalOre_40And240_ClearsDepth1 | 211 | public void Trial3_Chain_SkyIronThenStarmetalOre_40And240_ClearsDepth1() |
| TutorialTests | method | Depth2_CatalystTrial_ShowsTheCatalyst | 232 | public void Depth2_CatalystTrial_ShowsTheCatalyst(string catalyst, long score, double bonus) |
| TutorialTests | method | Script_IsTheSame_WhateverTheSeed | 248 | public void Script_IsTheSame_WhateverTheSeed() |
| TutorialTests | method | CatalystTrial_CoversTheOffer_AndNothingElse | 259 | public void CatalystTrial_CoversTheOffer_AndNothingElse() |
| TutorialTests | method | ShouldRun_OnlyOnAFreshSave | 268 | public void ShouldRun_OnlyOnAFreshSave(bool done, int journalEntries, bool expected) |
| HudTests | method | CanRedraw_UntilTheLastRedrawIsUsed | 14 | public void CanRedraw_UntilTheLastRedrawIsUsed() |
| HudTests | method | CanRedraw_IsFalse_OffTheBench_EvenWithRedrawsLeft | 28 | public void CanRedraw_IsFalse_OffTheBench_EvenWithRedrawsLeft() |
| HudTests | method | CanRedraw_IsFalse_WithNoRun | 39 | public void CanRedraw_IsFalse_WithNoRun() |
| MotionTests | method | Clean | 20 | public void Clean() |
| MotionTests | method | Arc_IsAlive_AndCanJoinASequence | 27 | public void Arc_IsAlive_AndCanJoinASequence() |
| MotionTests | method | Arc_EndsOnItsTarget_OnWholePixels | 36 | public void Arc_EndsOnItsTarget_OnWholePixels() |
| MotionTests | method | EveryHelper_IsAlive | 45 | public void EveryHelper_IsAlive() |
| SaveTests | method | Restore | 20 | public void Restore() |
| SaveTests | method | LoadProfile_WithNothingSaved_IsFresh_WithASeedOfItsOwn | 29 | public void LoadProfile_WithNothingSaved_IsFresh_WithASeedOfItsOwn() |
| SaveTests | method | LoadProfile_WithGarbage_IsFresh | 38 | public void LoadProfile_WithGarbage_IsFresh() |
| SaveTests | method | StoreThenLoadProfile_KeepsStatsGoalsAndUnlocks | 49 | public void StoreThenLoadProfile_KeepsStatsGoalsAndUnlocks() |
| SaveTests | method | LoadProfile_DropsWhatTheContentNoLongerHas | 66 | public void LoadProfile_DropsWhatTheContentNoLongerHas() |
| SaveTests | method | ResetProfile_ErasesTheProfile_AndKeepsTheJournal | 80 | public void ResetProfile_ErasesTheProfile_AndKeepsTheJournal() |
| SaveTests | method | LoadJournal_WithGarbageString_ReturnsEmpty | 92 | public void LoadJournal_WithGarbageString_ReturnsEmpty() |
| SaveTests | method | LoadJournal_WithOlderShape_ReturnsEmpty | 101 | public void LoadJournal_WithOlderShape_ReturnsEmpty() |
| SaveTests | method | LoadJournal_WithNothingSaved_ReturnsEmpty | 110 | public void LoadJournal_WithNothingSaved_ReturnsEmpty() |
| SaveTests | method | StoreThenLoad_KeepsRecipesAndHints | 117 | public void StoreThenLoad_KeepsRecipesAndHints() |
| SaveTests | method | StoreThenLoad_KeepsComboRecipes | 129 | public void StoreThenLoad_KeepsComboRecipes() |
| SaveTests | method | LoadJournal_Phase1PairKeys_StillLoad | 138 | public void LoadJournal_Phase1PairKeys_StillLoad() |
| SaveTests | method | TutorialDone_DefaultsToFalse_AndRoundTrips | 145 | public void TutorialDone_DefaultsToFalse_AndRoundTrips() |
| SparksTests | method | Step_MatchesTheOldPool_PositionAndFade | 36 | public void Step_MatchesTheOldPool_PositionAndFade() |
| SparksTests | method | Step_Dies_WhenItsLifeIsOver | 53 | public void Step_Dies_WhenItsLifeIsOver() |
| SparksTests | method | Trail_EmitsTheSameDensity_AtAnyFrameRate | 61 | public void Trail_EmitsTheSameDensity_AtAnyFrameRate(int hz) |
| SparksTests | method | Trail_IsOnePerFrame_At60Hz | 69 | public void Trail_IsOnePerFrame_At60Hz() |
| SparksTests | method | SparkMesh_SnapsCentres_ToWholePixels_AndKeepsHdrColours | 75 | public void SparkMesh_SnapsCentres_ToWholePixels_AndKeepsHdrColours() |
| SparksTests | method | Sparks_UsesOneLanePerSortingOrder_AndClearEmptiesThem | 101 | public void Sparks_UsesOneLanePerSortingOrder_AndClearEmptiesThem() |
| SparksTests | method | Sparks_SteadyState_AllocatesNothing | 119 | public void Sparks_SteadyState_AllocatesNothing() |

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
