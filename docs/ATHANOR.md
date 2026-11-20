# Athanor: the project document

The single project document. It describes Athanor **as it is now**; history is in git. If something here disagrees with the
code or `content.json`, the code wins: fix this file in the same change.

Not in this file, and kept where they are: every `CLAUDE.md`, `README.md` (game in two sentences plus commands), `CHANGELOG.md`,
`CREDITS.md` (font, effect and library licences), `docs/engine-reference/`, `.claude/` (agents, skills, rules), `vault/` (generated
structure notes plus the hand-written design notes).

Contents: 1 The game · 2 Architecture · 3 Data · 4 Decisions in force · 5 CI and rules · 6 Build, test, release · 7 Carry-overs ·
8 Lore and text voice

---

## 1. The game

**Premise.** A run-based alchemy puzzle for the browser. The player lays fantasy materials on a 3x3 bench, presses Ignite and sees
what reacts. Nothing is labelled: recipes are found by trying, and a failed mix still teaches something (a hint). Five depths, each
asking for more potency, end in one substance the game never names in advance (the Worldseed).

**Run loop.**

- A run descends through 5 depths. Depths 1 to 4 each have a potency target and **4 trials**; depth 5 has no target and **6 trials**
  (`RunState.BaseTrials`, `FinalTrials`). Targets: **300, 1,500, 7,500, 37,500** (ratio 5; `content.json` `depthTargets`).
- A trial: the hand is **5 materials** from the satchel (`BaseHand`), the player places any number on empty tiles, presses **Ignite**,
  the bench resolves, and what is spent is gone. **2 redraws** per depth swap the hand (`BaseRedraws`).
- Clearing a depth: descent cutscene, then pick 1 of 3 catalysts (permanent for the run), then the next depth.
- A run starts by picking 1 of 3 **aspects**, and picks another at depths 3 and 5. At the start of every depth an **event** may be
  drawn (after the aspect).
- Out of trials below the target ends the run. Making the ultimate wins the run **at any depth** (`RunState`, pinned by
  `RunStateTests`); see section 7 for the open question about depth 5 only.
- The Journal persists across runs; so does the profile (goals and unlocks). Nothing else does.

**The bench and Ignite.**

- 3x3 tiles (`Bench.Size = 3`, `TileCount = 9`, centre = tile 4), tiles numbered 0 to 8 in reading order. Neighbours are orthogonal.
  A hub on a corner has 2 neighbours, an edge 3, the centre 4, so a recipe has 2 to 5 items (`MinItems = 2`, `MaxItems = 5`), and a
  5-item recipe needs the hub on the centre tile. A recipe may use the same material up to twice (`MaxCopies = 2`).
- Every material has a **Reaction Index** (RI, 1 to 10), spread across tiers on purpose (a tier-0 starter can outrank a tier-4
  material). The Journal shows a material's RI once it is discovered.
- Ignite resolves in **rounds** on the bench itself. Each round the **hub** is the highest-RI tile that has a matching recipe
  around it (ties: earlier tile). The hub takes the **largest** recipe its neighbours allow; among equal sizes, the one whose
  consumed neighbours have the higher total RI. A recipe also needs its **required total RI** (`minReactivity`, "heat") from its
  items; if short, it behaves as if it did not exist, with no message.
- The reaction consumes its items and the result appears **in the hub's tile**, then takes part in the next round with its own RI.
  This repeats until nothing can react. A combo that stood ready but was spoiled by an earlier, hotter reaction shows a "sputter"
  beat (a line in Last Reaction; no score, no discovery).
- Pairs of neighbours left with no recipe **fizzle** and may give a hint; a hint never names a material.
- **What is spent.** Everything laid down is spent by Ignite. Starters (tier 0) go back to the satchel and return in later hands
  (2 copies of each at the start, `StarterCopies`); a discovery (tier 1+) that is placed is gone whether or not it reacted. Only
  what the chain **made** and left standing goes to the **rack** (12 places, `RackSize`; a full rack pushes out the weakest, then
  the oldest). Starting materials won through goals wait in a **pouch**: each hand draws one pouch card on top of the five
  (`PouchCards = 1`).

**Scoring** (`IgniteResolver.Score`). Potency of one reaction = base of the result's tier (`0, 20, 60, 180, 500, 1500` for tiers 0 to
5) x (items used - 1) x 2 if it is the first time this run x its place in the chain (the n-th reaction of one Ignite is multiplied
by n), then catalysts, then the hub tile's aspect multiplier, plus flat bonuses. A pair is x1, a 5-item combo x4. The first-ever
discovery also plays the full reaction cutscene; known reactions play a short one.

**Content kinds** (counts from `content.json`, 2026-11-19): 81 materials (13 starters at tier 0, 22 / 20 / 15 / 10 at tiers 1 to 4,
the ultimate `worldseed` at tier 5; 5 locked until won), 7 kinds (Potion, Crystal, Metal, Beast, Powder, Plant, Ore), 87 recipes
(62 pairs, 16 three-item, 7 four-item, 2 five-item), 26 catalysts (4 Common, 10 Uncommon, 8 Rare, 4 Legendary; 17 locked, 9 open on a
fresh profile), 16 aspects, 19 events, 22 goals.

**Aspects** (run-long bench modifiers, `scope` Tile / Link / Bench, `group` Cheap or Engine). Offer: 3 distinct candidates drawn
with the run's aspect RNG (`seed ^ 0x2545F491`); a tile-bound aspect shows its tile on the offer card and keeps it. The 16:
Doubled, Anchor, Heated (+3 RI), Echo, Attuned (a Kind), Seed, Fallow, Heavy, Cold\*, Volatile\*, Greedy Bench\*, Bridge, Open Bench,
Kiln, Crossroads, Pivot (\* = trade-off; Cold shuts a tile for +1 trial per depth, Volatile scores x3 but burns a starter, Greedy
trades a redraw for a card). Rules are in `BenchRules` and read by the one planner that both the game and the scripted player use.

**Events** (one per depth or none; RNG `seed ^ 0x7F4A7C15`, stepped once per depth; `noEventWeight` 3). Types in `content.json`:
Choice 4, Lore 4, Gamble 3, Hazard 3, Find 2, Shrine 2, Boon 1. Every event except a forced Hazard has a "Walk on" option. An option
that cannot be paid is not shown. An event lasts that depth only (cleared by `BeginDepth`). Lore events do nothing mechanical and
are not shown twice in a row. `BlockTile` never takes the centre and a corner is twice as likely as an edge.

**Goals** run across all runs and unlock the locked catalysts and starters. A goal is `{id, text, stat, target, rewardKind,
rewardRarity}` and pays a random locked item of its own bucket (kind and rarity), named only by that bucket until earned. Stats
kept: `runsStarted`, `runsWon`, `wonNoRedraw`, `depthReached`, `ignites`, `chainLongest`, `comboLargest`, `bestIgnite`, plus
`journalEntries` and `journalComplete` derived from the Journal. Failed runs count; a run's end is saved with the Ignite that ended
it, before the ending plays. The content does not load when a reward bucket has more goals than locked items.

**Depths.** The Shallow Cellar, The Dripping Stair, The Humming Dark, The Root of Stone, The Quiet Below. Each opens with one intro
line under its name (`depth.<n>.intro`, max 56 chars) and a window view for that layer.

**Tutorial.** The first run itself: depth 1 and the first trial of depth 2, with scripted hands (`TutorialScript`, fixed seed
`TutorialScript.Seed`, fixed first catalyst offer Bellows / Heart Tile / First Spark, aspects and events held back until depth 3 by
`RunOptions.QuietBefore = 3`). It teaches reactions, the Journal and catalysts, plays once after the intro on a fresh profile, can
be skipped (asked once) when the save has a non-empty Journal, and "How to play" replays it on a throwaway journal and profile.

**Other player-facing parts.** Title (Begin, Journal, Goals, How to play, Watch intro, Settings), Settings (language, vibration,
camera sway; volume rows hidden until sound exists), pause menu in a run (button on the HUD, Esc), Journal in two pages with rarity
stars (list cell: one star, detail: all stars and the tier word, only for discovered starters), run summary ("Planted at depth N"
on a win), goal toasts. Sound is not included.

---

## 2. Architecture

```
Athanor/Assets/
  Scripts/Core/    rules, no UnityEngine (UL008 + CORE-PURITY), compiled by tools/CoreTests too
  Scripts/Game/    Boot, Bench, Ui, Cutscene, Depth, Fx, Support
  Scenes/Main.unity           the only scene; hand-edited source of truth
  Prefabs/Views, Prefabs/Parts
  Editor/          ArtLibraryBuilder, AtlasBuilder, FlipbookImporter, FontBuilder, SpriteImportRules, ViewCapture, WebBuild
  Resources/       content.json, lang/en.json, lang/vi.json
  Tests/EditMode (Core, Athanor.Core.Tests), Tests/EditModeGame (Game, Athanor.Game.Tests)
```

**Core / Game split.** `Athanor.Core` holds the rules and is deterministic (seeded `Rng`): `ContentDb`, `RunState` (+ `RunStart`,
the second half: aspects, events, start of a depth), `Satchel`, `Bench`/`BenchRules`, `IgniteResolver`/`ChainPlanner`, `Journal`,
`Profile` (+ `Unlocks`, `RunOptions`), `Settings`, `Lang`, `TutorialScript`, `AutoPlayer`, `ScoreParts`, `ScreenFlow` (below).
`Athanor.Game` draws and listens; it never decides a rule. A new rule means Core plus a test first. Core keeps no mutable `static`
(UL011); what an event option did is reported as `EventNote` (a `note.*` key plus `n`, `item`, `other`, `kind`, `plural`), and the
Game layer words it through `Loc`, so Core carries no English sentence and no `Regex`.

**Where a number goes (Phase 7).** One owner means a serialized field on that owner; two or more owners means a ScriptableObject; the
game's rules mean `content.json`. There are no code defaults: a field starts with today's value only long enough to be saved into the
prefab or asset, then the initializer is removed and a `LayoutPinTests` case pins the serialized value.

| Kind | Example | Goes to |
|---|---|---|
| Rule tuning | hand size, trials, redraws, potency table | `content.json` `rules`, read as `ContentDb.Rules` (missing block: `ContentDb` throws) |
| Shape of the board or data | `Bench.Size = 3`, nine tiles, 2 to 5 combo items | a named `const` in Core |
| Meaning of an effect | `KindDouble` is x2 | code (the name is the number) |
| Fixed position of a part | the Ignite label | the part's Transform in the prefab; no code line |
| Layout parameter used in a formula | catalyst pitch, bar origin, row pitch | the nested `[Serializable] struct Layout` on that view, values held by the prefab |
| Timing of one sequence | story fade, hold, `IgniteSequence.Timing`, `GameFlow.RunTiming` | the nested `Timing` struct (or `SwayTiming`, `ShakeTiming`) on the owner |
| Colour used by one view | Redraw up / over / pressed | a field on that view |
| Colour used by several files | rarity colours, gold, ink | `Assets/Data/Palette.asset` (`Palette`), published as `Art.Palette` |
| Depth look | ambient, moonbeam, bloom, fog | `DepthMood.presets` in `Main.unity` (5 entries); nothing in code |
| Dev hooks the web tools read | `"[auto] "`, `"[dev] mood "` | stays a `const` |
| Plain arithmetic | `/ 2f`, 0, 1, array sizes | stays |

UL009 (colours) and UL010 (positions and sizes) are errors in `Scripts/Game`. The lint cannot see a named `const` or a bare duration
argument, so "lint is clean" does not mean every number moved; the files the Phase 7 final review named were read by hand, the rest were
not. What stays in code is listed in section 7 item 11(e). No `Pacing` asset exists: the timings of the sequences that were split
out (`IgniteSequence`, `GameFlow.RunTiming`, the cutscenes, `GoalToast`, `BenchView`) have one owner each. Add a `Pacing` asset only
when a timing gets a second owner.

**Main.unity and prefabs.** `Main.unity` plus the prefabs are the source of truth; there is no scene generator and no code that
builds a view at run time. Edit a screen in the Inspector, not in C#.

- Views as prefabs (`Prefabs/Views`): Bench (nests the HUD prefab), CatalystChoice, Event, Goals, GoalToast, Journal, Pause, RunOver,
  Title. Parts (`Prefabs/Parts`): Card, GoalRow, Hud, JournalCell. Goal rows and Journal cells are data-driven repeats of a part.
- Scene objects (no prefab): Screen Fx, Window, Tutorial, Reaction Cutscene, Story Cutscene, the depth stage and rig.
- `GameFlow` (Boot) is the composition root and the bridge; it owns the run, the saves and the main coroutine loop. The screen state
  machine is Core `ScreenFlow` (`FlowState`: Boot, Title, Intro, Bench, Resolving, Journal, Descent, CatalystChoice, Ending, RunOver,
  AspectChoice, Event, Goals): current state, where Journal and Goals return to, when pause and abandon are allowed (`CanPause`,
  `CanAbandon`), the Ignite gate (`StartIgnite`), the sandbox replay, and a `Changed` event. `GameFlow` turns view events into
  `ScreenFlow` calls (`OpenJournal`, `CloseGoals`, ...) and `ScreenFlow` answers with a yes or no; `ScreenFlowTests` cover every
  guarded move, the illegal ones included (`Enter` is unconditional and GameFlow drives most plain moves through it; the title choices and the tutorial gate stay in GameFlow; `Changed` has no production subscriber yet). Every view is a `[SerializeField]` on `GameFlow`; `GameFlow.Awake` throws if one is missing.
  It publishes the art library as `Art.Lib` and the shared colours as `Art.Palette`, calls `Loc.Use(en, vi)` first thing, then
  `Bind(...)` once on each view.
- **`IgniteSequence`** (Boot, on the GameFlow object) is the choreography of one Ignite: the chain of reactions (flash, draw-in, burst,
  points), the discovery beat and cutscene, the flight to the rack, the spent tiles and the final potency. The Ignite is settled before
  it plays (`GameFlow.SettleIgnite`); nothing in it decides a rule. Its paces and waits are the serialized `Timing` and `Look` structs
  in `Main.unity`; `GameFlow` keeps its own in `RunTiming`. `Descend` and `EndRun` stay in `GameFlow`.
- **`FxRefs`** (Fx) holds the scene's two effect players, `ScreenFx` and `Sparks`; `GameFlow` builds it from its `screenFx` and
  `sparks` fields and passes it through `Bind` to every view that flashes, shakes or throws sparks. There is no `ScreenFx.I` or
  `Sparks.I`.
- **`Palette`** (Support, `Assets/Data/Palette.asset`) is the ScriptableObject of colours that two or more files share, by role
  (rarity text and tone, aspect colours, gold, ink / faded / teal, pips, button text, star outline). A colour one view uses stays a
  field on that view.
- **The `Bind` contract.** A view's references are serialized. `Bind(...)` does the one-time wiring that cannot be serialized: it
  hooks clicks (lambdas), takes the `ContentDb` or settings, and makes run-time-only objects (sprites from `Sprite.Create`, render
  textures). A view has no `Build`. Run-time spawns that remain (the tutorial's popup lines, the Journal's corner stars, float text, motes)
  go through `Spawn` (Root, Sprite, Rect, Glow, Text, Click); font choice is `Fonts`.
- The `Build` methods of `TileView`, `CardView`, `BenchView` and the cutscenes were bake sources and are gone with the builders. A
  prefab is re-baked by editing it, not by code.
- Prefab serialization rules that bit: Unity does not serialize `Action`, tuples, dictionaries, queues or plain classes without
  `[Serializable]`; a serialized `string` is `""`; a `Sprite.Create` sprite is never kept by a prefab (clear it before saving, make
  it again in `Bind`); renaming a serialized field needs `[FormerlySerializedAs]` (UL301).

**Support.** `Label` (TextMeshPro wrapper with `Lines`/`LineBox`, font per role, Vietnamese vertical bias), `UiButton`, `Loc`
(`Use`, `InitFromPlayer`, `Changed`), `Art`/`ArtLibrary` (fonts, sprites by content id, built by `Athanor > Refresh Art Library`),
`PointerInput`, `Motion` (PrimeTween), `Haptics`, `AudioMute` (a hook; no sound ships), `Dev` (compiled out of release).

**HD-2D rig** (`Scripts/Game/Depth`). The bench is rendered in depth: a perspective **world camera** at distance D from the art
plane renders the layers under `DepthRig` into a RenderTexture of 640K x 360K px (K = whole display scale; never a fractional
stretch); an orthographic **UI camera** with the Pixel Perfect Camera draws that texture on a full-screen quad beneath the HUD, so
the HUD never moves and stays 640x360. `DepthLayer` moves a flat object to its depth and enlarges it by (D - z) / D so at rest it
covers exactly the art pixels it covers in the flat game. `DepthTable` is the table as a tilted floor mesh cut along every art row.
`DepthMood` holds the five per-depth presets (ambient, moonbeam, bloom, vignette, fog sheets, window view); `DepthSwitch` is the
effects-off switch; the tilt-shift is a URP renderer feature (`DepthTiltShiftFeature`, band sharp from art y 145 to 235).
`DepthStage` is the one handle `GameFlow` uses (rig, sway, moods, effects, `PlaceTiles`, `SetFlat`). **`CameraSway`**: idle sway
x = A sin(2 pi t / 6), y = A/2 sin(2 pi t / 9), A = 4 art px, re-aimed so the middle tile row (art y 188) stays still; the Settings
toggle turns it off. **Shake moves the world only.** Cutscenes stay flat at z = 0 (`SetFlat`). The depth groups are placed by
`GameFlow` at run time (`DepthStage.PlaceTiles`), not baked into the Bench prefab.

**Save** (`Save`, PlayerPrefs; browser storage in a web build): `athanor.journal.v1`, `athanor.profile.v1`, `athanor.intro.v1`,
`athanor.tutorial.v1`, `athanor.settings.v1`. A damaged journal or profile loads empty / fresh (`SaveTests`). Changing a shape
means bumping the key version and keeping a reader for the old one. "Reset progress" (inside Settings) erases the profile and keeps
the Journal.

**Localization** (`Loc` over `Core/Lang`). `lang/en.json` and `lang/vi.json` are `{language, name, entries:[{key, text}]}`
(258 and 420 entries). Lookup: chosen language, then English, then the key. Start language: `?lang=` in the page address, else the
saved choice, else the device language (Vietnamese if the system says so, English otherwise). `Loc.Use` must run before any `Label`
exists; the Game tests do it once in `GameTestSetup`. Key prefixes: `ui.*`, `story.*`, `tutorial.*`, `cutscene.*`, `effect.*`,
`note.*`, `kind.*`, `depth.<n>.intro`. Number format: "1,500" in English, "1.500" in Vietnamese (`Wording.Number`).

**Allowed statics.** Presentation code keeps no writable static, except in eight classes: `Loc`, `PointerInput`, `Art`, `Haptics`,
`AudioMute`, `SparkMesh`, `TiltShift`, `RarityStars` (`static_allowlist` in `tools/lint/lint.config.json`; UL012 is an error). Domain
reload is off in the Editor, so statics survive Stop and Play. Each of the eight has a
`[RuntimeInitializeOnLoadMethod(SubsystemRegistration)] ResetStatics()` that restores every static to its declared default, drops event
handlers and destroys what the class created (`StaticResetTests`). A ninth class needs a line here, a reset and a test. A component that
starts coroutines or tweens stops them in `OnDestroy` (`StopAllCoroutines()` and `Motion.StopOn(this)`).

**Hot-path rule.** An `Update` (or a `Tick` it calls) returns early when nothing changed: no string building, no sprite lookup, no
`GetComponent`, no closure and no LINQ per frame. Text, sprites and layout are written when their input changes (compare, then
write), and a cached result is dropped by the one place that changes its input. UL002 does not catch a method call that builds a
string, so this is a review rule; `HotPathTests` hold the idle views at 0 B and the positive tests beside them
(`Tick_PotencyChanged_RewritesText`, `Tick_TooltipUp_RebuildsAfterRedrawsChange`, ...) hold that a change is still drawn. A new cache
needs both kinds of test.

**Flat path.** Without a `DepthStage` (no scene builds one without it today) the descent banner accent is white and ambient light and
moonbeam are not set; `Main.unity` always has a stage.

**Platform and threading rules** (enforced by lint, UL013 and UL014).

- WebGL only, one thread. No `Task.Run`, `new Thread`, `System.Threading.Timer`, `.Result` or `.Wait()` anywhere in `Scripts/`. Time
  is coroutines and PrimeTween; Unity's `Awaitable` only with `destroyCancellationToken`.
- A platform `#if` (`UNITY_WEBGL`, `UNITY_STANDALONE*`, `UNITY_ANDROID`, `UNITY_IOS`) and any jslib call live only in the bridge files
  of `platform_define_allowlist` in `tools/lint/lint.config.json`: `Save`, `Haptics`, `AudioMute`, `GameFlow` (frame pacing), `Dev`.
- No NuGet packages. A new package needs a WebGL / IL2CPP check and a line in section 4. Resolution stays 640x360; PC and Steam
  later means platform code only in bridge files (section 4, 2026-11-19).

**Dev hooks and keys** (editor and development builds only; `Dev.Hook` log lines are what `tools/web/*.mjs` read):

| Key | Does |
|---|---|
| F6 | cycle the depth mood 1 to 5 |
| F7 | shake the world |
| F8 | HD-2D effects on / off |
| F9 | hand the bench to the scripted player (`AutoPlayer`); `WebBuild.BuildDev` makes the build that has it |
| F10 | log the nine tile faces as drawn |
| Esc | pause menu in a run; closes Journal and Goals |

---

## 3. Data

**`content.json`** (`Resources/content.json`; plain `[Serializable]` classes in `ContentData.cs`, read by `JsonUtility` in Unity and
`System.Text.Json` in dotnet, so fields are public and there are no properties). Full field tables, validation and the "where does a
change go" table: `vault/Data/Data Design.md`. Summary:

| Key | Shape |
|---|---|
| `materials[]` | `{id, name, kind, tier, reactivity 1-10, glow, rarity?, locked?}`; tier 0 = starter |
| `recipes[]` | `{items[2..5], minReactivity, result}`, unordered |
| `catalysts[]` | `{id, name, text, effect, kind, amount, rarity, locked}` |
| `aspects[]` | `{id, name, text, effect, scope, group, amount, tradeOff}` |
| `events[]` | `{id, name, text, type, minDepth, maxDepth, weight, options[{label, requires, cost[], outcomes[{weight, text, gain[]}]}]}` |
| `goals[]` | `{id, text, stat, target, rewardKind, rewardRarity}` |
| `rules` | flat object (`RulesData`): `handMin, handBase, trials, trialsFinalDepth, redraws, starterCopies, offerSize, rackSize, pouchCards, firstReactionMult, maxRi, aspectDepths[], potencyByTier[]`; required (`ContentDb` throws without it and validates ranges and array lengths; no hidden defaults in code); read as `db.Rules` by Core, the Game views, the tests and `tools/CoreReports`; table in `vault/Data/Data Design.md`. `amount` must be above 0 on the Heated and Seed aspects and on a HeatTile event effect (no code fallback). The pouch mode is `RunOptions.PouchMode` (default 2, `ATHANOR_POUCH` in the reports) |
| `depthTargets[]`, `depthNames[]`, `ultimate`, `noEventWeight` | 4 targets for 5 depths; `ultimate` = `worldseed` |

Text limits for events: name 22 characters, text 56, outcome 52. Item names (materials, catalysts, aspects) stay English in Vietnamese;
event and depth names are translated.

**`lang/*.json`**: every `en` key exists in `vi`; `{x}` placeholders match (CI check LANG). Player-visible strings are `Lang` keys.
Content texts are localized per the settings rule above.

**Icons.** A new material, catalyst or aspect needs an icon with the same id in `tools/art/icons.mjs` (aspects `asp_<id>`, event
types in `EVENT_ICONS`); CI check ICONS fails otherwise.

**Art pipeline** (`tools/art`: `export.mjs`, `export-window.mjs`, `export-depth.mjs`, `icons.mjs`, `painter.mjs`, `scene.mjs`,
`ui.mjs`, `blur.mjs`, `png.mjs`). Every sprite is generated by code and written into `Athanor/Assets/Art`. `export.mjs` also writes
`Athanor/Assets/Editor/layout.json`, which is read only by editor tooling through `LayoutData`; the game build ships no layout file.
After a sprite change, run `Athanor > Refresh Art Library`. **Drift warning: do not run `node tools/art/export.mjs` as part of
routine work.** The generator's output no longer matches the committed art, so a run rewrites assets. The note that recorded the
details lived in an untracked session file and is gone; before relying on the generator, run it into a scratch copy and diff.

**Atlases and effects.** Sprite atlases (`Assets/Art/Atlases/*.spriteatlasv2`, built by `AtlasBuilder`); no further consolidation.
Effect sheets are committed in `Assets/Art/Fx/Sheets` and imported by `Athanor > Import Effect Sheets`. To re-export them, clone
pixelfx (`github.com/patch-gg/pixelfx`, MIT; the companion `git.patch.gg/patch/pixel-effects`) and use its render tool; the export
scripts and both clones were removed from this repo in Phase 6. Spark particles are `SparkMesh` (ParticleSystem was measured at
about 264 KB of wasm and rejected).

**Fonts and credits.** Alagard (headings, numbers), BitPotion (reading text), Pixel Operator (tiny labels), Coder's Crux (Vietnamese
roles and Phase 5 UI text). Attribution lives in `CREDITS.md`; keep the credit line on the title screen. PrimeTween 1.5.2 is kept as
`Athanor/Packages/com.kyrylokuzyk.primetween.tgz`: fine while the repo is private, re-decide before any public release.

---

## 4. Decisions in force

One line each. A decision is written here before it is relied on. Superseded and rejected ones are left out.

**Product and rules**

- 2026-11-11: Unity 6000.6.7f1 Universal 2D, WebGL, 640x360 with integer scaling and point filtering; art generated by code
  (`tools/art`); rules in plain C# with content as data; mouse first, touch partial. Candlelit fantasy workshop look.
- 2026-11-12: Reaction Index is spread across tiers and sets firing order; combos 2 to 5 items; chain in rounds with the result
  appearing in the hub's tile; discoveries placed are used up; hints live on the Journal entry and hide once the material is fully
  known; depth targets 300 / 1,500 / 7,500 / 37,500.
- 2026-11-13: balance targets, measured by the informed bot over 200 seeds: fresh profile 60 to 70%, full 80 to 90%, a random player
  near 0%; a hazard or gamble met once must leave at least 70% of the event-free wins; `InformedPlayer_WinsMostSeeds` bounds 24 to 31
  of 40.
- 2026-11-13: won starters wait in a pouch (one card per hand) instead of joining the satchel; a lost run still counts toward goals;
  the Engine aspects were built, the three trade-offs (Cold, Volatile, Greedy) are the only costly ones.
- 2026-11-13: the tutorial is the first run itself (no training room), no extra first-seen notes for aspects, events, goals or
  redraws, Redraw is its own button, trial 4 of the old draft is cut, the guide's voice is the plain neutral label.
- 2026-11-15: `ShouldRun` relies only on the `TutorialDone` flag, plus a Skip prompt for saves with a non-empty Journal; "Skip" starts
  the run at once; "Play it" (How to play) uses the throwaway sandbox.
- 2026-11-15: rarity is display only (stars, tier word); draw weights stay all 1 (`rarityWeights` absent). Set `[4,3,2,1]` was
  measured at 35 of 40 against the 24 to 31 target and rejected. Revisit only with a new measurement; the target range is not widened.
- 2026-11-15: a Lore event is remembered in the profile so it is not shown twice in a row (so `quiet_room` appears about a quarter as often).
- Win at any depth stays the rule until the owner decides the endings question (section 7).

**Presentation and platform**

- 2026-11-15: sound is out of scope (FMOD dropped for good; music and cue lines in the lore are notes); the hooks stay (`AudioMute`,
  `GameFlow` reactions, descent and endings, `ScreenFx`).
- 2026-11-15: Vietnamese is written by `localizer-vi` in plain modern Vietnamese, "bạn" for the player; item names stay English; "The
  Keeper" stays unnamed in Vietnamese lines; forced capitals are kept, HUD rows grow to 12 to 13 px, and a string that still
  overflows switches to normal case; SVN-Determination Sans is deleted (no licence), Coder's Crux serves all four Vietnamese roles.
- 2026-11-15: Settings is a fourth title button; vibration pulses only on a discovery (20 ms), a cleared depth (40 ms) and a run's end
  (60 ms), only where `Haptics.Supported`; the pause menu is on all platforms with `Time.timeScale = 0`, Quit to title discards the
  run uncounted; audio mutes when the tab is hidden; the rotate-device layer is HTML/CSS in the WebGL template, shown on
  `(orientation: portrait) and (max-width: 639px) and (pointer: coarse)`, in the template's system monospace.
- 2026-11-15: size ceiling for the Phase 5 build: **11,824,672 B** (10,776,096 + 1 MiB), measured on the release web build.
- 2026-11-15: the 7-scene intro storyboard (Keepers shown as silhouettes, 3 Keepers, no names under them, the last Keeper looks back
  once, the Worldseed never shown, one cue that drops out in scene 5) is the intended intro; its new art is made later (section 7).
- 2026-11-15: Descent shows one intro line per depth and the window shows a layer-specific view with one effect; bench and slots do
  not change.
- 2026-11-16 (gate G2, 11 SPEC answers as recommended): foreground strip is two corner pieces; front fog over the tiles kept at alpha
  0.14 (0.13 to 0.16 per depth); the 640x360 pixel-mist fog grain kept; sway amplitude 4 art px; sway has a Settings off switch;
  shake moves the world only; the HUD stays 640x360 at whole multiples; the five depth presets are starting values and may be
  widened; window view softened less (far pre-blur radius 1 px at 2x, tilt-shift strength 3 px at 2x); tilt-shift band sharp from
  art y 145 to 235; the world renders at the whole display scale (2x, 3x).
- 2026-11-17: sway is kept (no measurable shimmer); if 3x is too slow the answer is a reduced-effects switch (tilt-shift and fog off),
  not a 2x cap and not the parallax fallback; evidence PNG strips stay in git, shimmer GIFs do not.

**Process and architecture**

- 2026-11-18: **`Main.unity` and prefabs are the source of truth; no scene generator.** Expensive to reverse, so recorded here; the
  owner can ask for an ADR through `/architecture-decision` instead.
- 2026-11-18: `layout.json` moved to `Assets/Editor` rather than deleted (the art exporter writes it and editor tools read it); the
  game never loads it.
- 2026-11-18: `Settings/DepthRenderer2D.asset` is kept (`UniversalRP.asset` lists it as renderer 1 and Main's camera uses index 1);
  the four depth materials live in `Assets/Art/Depth/Materials/`.
- 2026-11-18: the owner commits; agents never commit. A task ends at a checkpoint with a suggested Conventional Commit message
  and `Story: P6-<n>` in the body. The balance reports are a console program (`tools/CoreReports`), not tests.
- 2026-11-19: this file replaces the devlogs, plans, specs, phase sign-offs and the old CI doc.
- 2026-11-19: the tutorial's dark screen is one `Dim` sprite drawn "visible outside mask"; each lit window is a `SpriteMask` in a pooled `TutorialHole` (with its gold rim), and the click lock is one `Clickable` with the holes cut out of it. Nothing is built or destroyed per step except popup lines. Replaces the per-step grid of dim rectangles. The tutorial's numbers (rectangles each step lights, text box, popup lines, rim and arrow, timings) are Inspector fields of the Tutorial object (`TutorialTargets`, `TutorialStyle`); the scripted hands and lays stay in Core `TutorialScript`, pinned by tests.
- 2026-11-19: Phase 7 (Unity conversion, spec 2026-11-19): rule numbers live in content.json "rules"; presentation numbers in the prefab or scene that owns them, shared colours in the Palette asset; GameFlow splits into Core ScreenFlow, the GameFlow bridge and IgniteSequence; Game statics only from the lint allowlist, each reset at SubsystemRegistration; Core reports note keys, not sentences. Nothing visible or scored changes (golden shots, byte-identical reports).
- 2026-11-19: resolution stays 640x360 (a 1920x1080 canvas needs every sprite redrawn; dropped); PC / Steam later, platform code only in bridge files.
- 2026-11-20: Phase 7 close. No `Pacing` asset (after the split every timing has one owner; add one only when a timing gets a second owner). `PointerInput.Rig` stays a static of the allowlisted `PointerInput`, set by `PointerInput.Use(rig)` and cleared by its reset. UL009 to UL014 are all errors and the lint baseline is empty. The number-placement rule, the allowed statics and the platform and threading rules are in section 2. The Phase 7 spec and plan are not kept: their lasting content is in sections 2 to 7 (their deletion follows the final-review fix wave). Not every presentation number moved: the list that stays in code on purpose is in section 7 item 11(e); the claim "every timing has one owner" holds for the split-out sequences only.
- 2026-11-20: Phase 7 non-goals, so the question "why not X" has an answer: no dependency-injection framework (`Bind`/`FxRefs` pass what a view needs); no uGUI or UI Toolkit (the UI is hand-built sprites on the 640x360 canvas); no Addressables; coroutines are not converted to `async`/`Awaitable`; Unity Timeline is not used for the cutscenes (wasm size, and the look must stay identical).
- 2026-11-20: the one visible text change of Phase 7 (owner ruling 2026-11-19): the English lost-item note reads "Lost Iron Ore for this run", not "Lost a Iron Ore for this run" (key `note.lost`, no article, so no "a"/"an" error). Vietnamese is unchanged; `NoteTextTests` pin it.

---

## 5. CI and rules

**If `ci-gate` is not green, the change is not merged.** Branch protection requires it, including for admins
(`APPLY=1 tools/setup/protect-branch.sh <owner>/<repo> main`). Reproduce any failure locally with the command CI runs:
`tools/ci/run.sh all` (or `lint`, `repo`, `core`, `rotate`, `commits <range>`).

| Failure in practice | Caught by | Job |
|---|---|---|
| Hard-rule code bugs: `Find*` in `Update`, swallowed `catch`, `?.` on a Unity object, event never unsubscribed, hard-coded secret | `unity_lint` UL001-UL007 | lint |
| `UnityEngine` inside `Scripts/Core` | UL008 and `check_repo` CORE-PURITY | lint, repo-guard |
| Hard-coded colour in `Scripts/Game` (`Px.Hex("#..")`, `new Color(<numbers>)`) | UL009 (error) | lint |
| Hard-coded position or size in `Scripts/Game` (`Px.At`, `Px.Mid`, `new Rect`, `new Vector2/3` with a number other than 0 or 1) | UL010 (error) | lint |
| Mutable `static` (field, event, property with a setter) in `Scripts/Core` | UL011 (error) | lint |
| Mutable or collection `static` in `Scripts/Game` outside `static_allowlist`, or an allowlisted class without a `SubsystemRegistration` reset | UL012 (error) | lint |
| `Task.Run`, `new Thread`, `.Result`, `.Wait()`, `System.Threading.Timer` in `Scripts/` (WebGL is single-threaded) | UL013 (error) | lint |
| `#if UNITY_WEBGL/STANDALONE*/ANDROID/IOS` outside `platform_define_allowlist` | UL014 (error) | lint |
| Bad tests: no assertion, `[Ignore]`/`[Explicit]`/`[Retry]`, `Thread.Sleep`, unseeded random or clock, name not `Method_Condition_Result` | UL100-UL105 | lint |
| Missing or duplicate `.meta`, wrong `ProjectSettings` | UL200-UL203 | lint |
| Build output staged (`bin`, `obj`, `Athanor/Build`, `test-results.xml`) | `check_repo` BUILD-OUTPUT, UL204 | repo-guard |
| Renamed serialized field loses scene/prefab data | UL301 (`FormerlySerializedAs`) | lint |
| Bad PR title (becomes the squash commit) | UL300 | lint |
| Broken or duplicate ids in `content.json` | CONTENT | repo-guard |
| New material, catalyst or aspect without an icon in `tools/art/icons.mjs` | ICONS | repo-guard |
| String missing in `vi.json`, empty text, placeholder that differs between en and vi | LANG | repo-guard |
| Rules regress or a test is silently dropped (fewer than `MIN_TESTS` = 250 pass) | `dotnet test tools/CoreTests` | core-tests |
| `vault/` stale after a code or content change | `build_vault.py` then no diff | repo-guard |
| Rotate-device text in the WebGL template drifts from `ui.rotate.title` / `ui.rotate.body` | `tools/web/rotate_sync.mjs` (`run.sh rotate`) | repo-guard |
| Art/web scripts do not parse | `node --check` | repo-guard |
| A presentation number drifts from the value pinned when it moved into a prefab or the scene | `LayoutPinTests` (EditMode) | release-gate (tags, manual), local |
| A view or scene object looks different (golden shots, 0 % difference, English and Vietnamese) | `GoldenShotTests` (EditMode; `Athanor > Write Golden Shots` re-records) | release-gate (tags, manual), local |
| An allowlisted static is not back at its default after `ResetStatics`, or an idle frame allocates | `StaticResetTests`, `HotPathTests` (EditMode) | release-gate (tags, manual), local |
| A job fails, is cancelled or is skipped | `ci-gate` needs every job `success` | ci-gate |
| Release build broken, over its size budget (Build/ + index.html > 11,824,672 B), console error while loading | EditMode, `WebBuild.Build`, byte budget, `loadsmoke.mjs` | release-gate (tags, manual) |
| Game flow broken in the browser, console error in play | `WebBuild.BuildDev` then `webtest.mjs` | release-gate (tags, manual) |

**Rules for everyone.**

- Never weaken a check to get green. Fix the code, or change the rule in `tools/lint/unity_lint.py` or `tools/ci/check_repo.py` with
  a test and a row in the table above.
- **The lint baseline is empty** (`tools/lint/baseline.json` has `entries: []`). Every finding fails CI; do not add entries.
- There is no `DepthTest` scene, builder or evidence tool any more; depth is checked on `Main.unity` (`DepthClickTests`,
  `MainDepthTests`, `tools/web/mainhd2d.mjs`).
- The six balance reports are `dotnet run --project tools/CoreReports -- balance-matrix | events | roles | newcomer | balance | trace`
  (add `DOTNET_ROLL_FORWARD=Major` on a machine with only a newer .NET). They print numbers and assert nothing, so they are not
  tests. After a content or scoring change quote them.
- `release-gate` needs the secrets `UNITY_LICENSE`, `UNITY_EMAIL`, `UNITY_PASSWORD`; without them it fails on purpose.
- Hard rules also in `CLAUDE.md`: no game rules in the Game layer; no `UnityEngine` in Core; new rule means a test first; every
  player-visible string a `Lang` key in both languages; new content needs an icon; build output is never edited or staged.
- Not covered by CI (on a human): visual and UI review (screenshots under `production/qa/evidence/`), balance reading.

---

## 6. Build, test, release

| Name | Command | Pass when |
|---|---|---|
| CI | `tools/ci/run.sh all` | exit 0 |
| Core tests | `dotnet test tools/CoreTests` (`DOTNET_ROLL_FORWARD=Major` if only a newer runtime) | 0 failed |
| EditMode | `unity test Athanor --mode EditMode` (editor closed) | 0 failed |
| Release build | `unity build Athanor --target WebGL --execute-method WebBuild.Build --allow-dirty-build` | exit 0; writes `Athanor/Build/web` |
| Quick build | same with `WebBuild.BuildQuick` | exit 0 |
| Dev build | same with `WebBuild.BuildDev` | exit 0; writes `Athanor/Build/web-dev`, F9 autoplay |
| Serve | `python3 -m http.server 8080 -d Athanor/Build/web-dev` | |
| WebTest | `node tools/web/webtest.mjs http://localhost:8080/ <outDir>` | exit 0 |
| HD2D | `node tools/web/mainhd2d.mjs http://localhost:8080/ <outDir> 640x360 hud,tiles` | 0 console errors and every non-shake check passes (sway 27/27, rest, hud) |
| Autoplay | `node tools/web/autoplay.mjs http://127.0.0.1:8081/ <outDir> 120 3` | exit 0 |
| Descents | `node tools/web/descents.mjs <url> <outDir> <seconds> en` | exit 0 |
| Load smoke | `node tools/web/loadsmoke.mjs <url> <outDir>` | exit 0 |
| Vault | `python3 tools/vault/build_vault.py` | no diff afterwards |

Unity batch commands need the Editor closed (project lock). Headless checks need `google-chrome` and Node 22+. Shake in HD2D is
recorded, not gated: the harness has click latency (proven by `DepthClickTests` in EditMode) and the count varies run to run on the
same code (20/27, 18/27, 27/27 and 16/27 across four Phase 6 runs). If `unity` says the project is not a Unity project, give it the
path (`./Athanor`).

**Size.** Ceiling **11,824,672 B** for Build/ + `index.html` of the release web build. Measured 2026-11-20 at the end of Phase 7
(Task 18): **10,919,503 B** (headroom 905,169 B), 41,766 B under the Phase 6 Task 16 figure of 10,961,269 B (Phase 5 close:
10,917,289 B). `Regex` left `Scripts/` in Phase 7, but the built wasm still carries the `RegularExpressions` strings, so the drop is
not credited to it (no isolated build was made). The build is Gzip-compressed with Unity's built-in fallback, so any static host
works; the page keeps the 640x360 picture at a whole-number scale, centred on black.

**Figures at this writing (2026-11-20, end of Phase 7).** Core 367 passed, 0 failed, 0 skipped. EditMode 641 total, 641 passed, 0
failed, 0 skipped (18 `GoldenShotTests` cases at 0 % difference). The six `tools/CoreReports` outputs are byte-identical to the
Phase 7 baseline (`production/qa/evidence/phase7/baseline/`). BalanceMatrix (informed bot, 200 seeds): fresh 132 (66%); fresh + all
catalysts 164 (82%); fresh + all starters 134 (67%); half 164 (82%); full 166 (83%); with aspects and events: fresh 129 (64%),
fresh + all starters 140 (70%), half 163 (81%), full 169 (84%); aspects alone 144 (72%), events alone 129 (64%). Web checks on the
final builds, all exit 0 with 0 console errors (evidence `production/qa/evidence/phase7/task18/`): `loadsmoke` (release),
`webtest` (129 screenshots), `autoplay` 120 s, `descents` 240 s, `mainhd2d` 640x360 `hud,tiles` (54 of 54 checks, sway 27/27,
shake 27/27). `tools/ci/run.sh all` exit 0 (lint clean with an empty baseline, repo check 0 problems, rotate OK, Core 367 passed).

**Idle bench views (Phase 7 Task 17).** GC allocation of the idle bench views (bench, HUD, tiles, cards, links, dust, sparks, flame): **0 B** over 600 frames (10 s at 60 fps). This is not the whole idle game. Measured in EditMode, not in the
Editor Profiler: `HotPathTests.IdleBench_TenSecondsOfEveryUpdate_AllocatesZeroBytes` ticks the Bench prefab's BenchView, HudView, nine
TileViews, the CardViews and the LinkViews 600 times inside one `Is.Not.AllocatingGCMemory()` (Unity's GC.Alloc counter, the one the
Profiler's GC Alloc column reads); `Continuous_DustSparksAndFlame_...` does the same for Dust, Sparks (40 live particles) and Flame.
PointerInput, GameFlow, TitleView, DepthRig and the cutscene views were read, not measured (no input device or run in a batch test).
Owner to confirm in the Editor Profiler (Play, reach the bench, idle 10 s: script updates 0 B GC Alloc).

**Release steps.** Version is `project.yaml` `project.version` (0.4.0); the owner sets it. Player-facing notes go in `CHANGELOG.md`
(the Phase 5a entry is still a DRAFT). Tag, then `release-gate` builds, measures and web-tests.

---

## 7. Carry-overs

Undecided or not done. None is decided by being listed.

1. **Endings and the Worldseed frame** (the draft is marked *undecided*). Owner answers so far: several Ultimates, each its own
   ending, no true / good / bad ending, endings equal in worth. The main session read the owner's G2 answer as covering world frame
   Option 1 "Roots of worlds" and three seeds from the three existing recipe paths (flowing / burning / keeping), **to be
   confirmed**. Undecided: **win at any depth, or depth 5 only** (it decides most of the ending text, needs a BalanceMatrix check, and
   changes `MakingTheUltimate_WinsAtAnyDepth`); whether `story.ending.1` and `.3` stay shared with only line 2 per seed; whether
   the Journal gets a "Planted" page (a profile shape change: bump the key and keep an old reader); the four dead-end tier-4
   materials (`thunder_heart`, `ossuary_crown`, `storm_philter`, `copper_sun`). No content, rule or text changes until answered.
2. **Loops mockup** (dust / ember loops, old 5b-02): the owner's review, keep or revert the shipped loops.
3. **G3 carry-overs:** the 7-scene intro storyboard assets (6 new, 30 frames; shares `cellar_stair` with the Descent), wider `LineBox`
   use, a Vietnamese text pass, the reduced-effects switch (not built). The owner's laptop frame-time figures were never given to the
   record; the only numbers are the Intel UHD 630 ones (Main 16.7 ms at 2x, 20.5 ms at 3x).
4. **Version string and release notes** for the Phase 5 build are not done; `project.yaml` stays 0.4.0 and the changelog entry is a draft.
5. **Remote branch** `origin/feature/2.5D_test-phase-4` is merged and can go; only on the owner's command.
6. **Accepted test gaps:** shake click 25/45 in the harness (latency, not a bug); 1x HUD garble seen only in software rendering
   (SwiftShader) and not on release with a GPU; depth 5 descent frames not photographed; Vietnamese half-leading offsets of 2 to 7 px
   on some banners, tight Vietnamese catalyst lines and tall buttons; fog grain judged by eye only; no Legendary material exists so a
   Legendary Journal frame was never shot (do not add one for a screenshot); the lost-run summary is covered by tests only.
7. ~~**Singletons left:** `ScreenFx.I` and `Sparks.I`~~ closed in Phase 7 Task 7: both are passed through `Bind` as an `FxRefs` (GameFlow builds it from its `screenFx` and `sparks` fields).
8. **Leftover DepthTest tools:** `tools/web/depthmatrix.sh`, `depthprobe.mjs`, `depthshimmer.py`, `depthshimmer.sh` still exist
   although the prototype they measured is gone; the Phase 6 spec called for deleting them.
9. **studio-tests were removed** in Phase 6. The framework skills `skill-test` and `skill-improve` are left in place, but their
   fixtures are gone.
10. ~~**Remaining Phase 6 work:** graphify of `Athanor/` (Task 30); EditMode ends at 0 failed and 0 skipped~~ closed in Phase 7: the graphify map is committed and EditMode ends at 0 failed, 0 skipped (section 6).
11. **Phase 7 open items.** (a) Owner: confirm idle-bench GC allocation in the Editor Profiler (Play, reach the bench, idle 10 s, script
    updates 0 B); only the bench views, Dust, Sparks and Flame were measured, in EditMode (section 6). (b) Owner: watch the intro,
    descent, discovery and ending cutscenes in the Editor once; the web checks and golden shots cover the descents and the title, no
    check plays the reaction cutscene or an ending. (c) `tools/web/webtest.mjs` flake: `waitCards` takes `offerFrom` at step 5_2, but
    the cards can open before that, so the run sometimes stops at `vi_5_3` with "the cards never took a click" (seen in the Task 1 baseline run
    and in a Task 17 run, never with a console error; the rerun passed). Fix: take `offerFrom` at step 5_1. Still open; the fix belongs to the QA
    tooling, not to the game. (d) The Phase 7 spec and plan files are deleted after the final-review fix wave (their lasting content is in
    sections 2 to 7). (e) Presentation numbers that stay in code, on purpose or not yet moved: `Flame` flicker and `Flipbook` 1/30 s,
    font sizes and sort orders, the `Dev` hook strings, `Tooltip` `W`/`BaseH`/`RowH` (layout used in a formula),
    `CX = 320` / `O = 300` in `ReactionCutscene` and `SimpleCutscene` (half-canvas), the sizes in `WindowView` and `RunOverView`
    (`Fits = 25`), and similar sizes of baked art in views the final review did not read line by line, the float-text fade split in `BenchView.FloatText` (0.7 / 0.3 of its own
    length, arithmetic), and the page-grid constants of `GoalsView`, `JournalView`, the `TitleView` settings panel and
    `CatalystChoiceView` (`X`, `Y`, `Top`, `ColW`, `Cell`, `GridX`, `P`, `PX`, `CellX`, `CardW`, `Gap`, ...): they mirror
    `tools/art/ui.mjs` and sit inside many formulas, so moving them is a per-view refactor with golden-shot risk; not done in Phase 7.
    Moved in the final-review wave: `GameFlow.RunTiming` fades and autoplay pace, `TileView` consume burst, `BenchView` hand strip,
    hit boxes, deal stagger and tooltip delay, `GoalToast` box and `Timing`, `PauseMenu.dialogTop`.
    (f) Owner: the real second session. In the Editor press Play, then Stop, then Play again, and go Title, Bench, Journal, Title:
    no console error and no doubled handler (a click acts once). `StaticResetTests` check each reset alone, not a second session.
12. Notes, not tasks: endless mode after depth 5 (the owner redesigns it), a currency (no purpose yet), sound (other plan).

---

## 8. Lore and text voice

**The world in five lines.** (1) Under the old workshop is a cellar that **grows**. Nobody dug it. (2) It grows because of the
**Keepers**: each one brewed down to the bottom, made the Worldseed and planted it; each seed added a depth, and the rooms, tools and
shrines on the way down are what earlier Keepers left. (3) The **last Keeper** went down and did not come back up; the player takes
over their bench. (4) The **Book** (the Journal) is the one thing that comes back up every time, which is why discoveries are kept
across runs and nothing else is. (5) A failed run is a climb back up the stair at dawn; a won run plants a seed and the cellar gets a
little deeper.

| Game thing | In the story |
|---|---|
| Run | one descent |
| Journal | the Book, passed from Keeper to Keeper |
| Catalysts | tools of earlier Keepers, found on the way |
| Aspects | the bench settling into the depth: tiles warm, crack, take root |
| Events | rooms and traces of earlier Keepers along the stair |
| Depths | layers grown by earlier seeds, older the deeper you go |
| Worldseed | what each Keeper makes at the bottom and plants |

**Binding rules for any line of text.**

- The world is a stack of dungeons (cellar layers grown by earlier seeds). The ultimate is the Worldseed. There is no true, good or
  bad ending.
- **"The Keeper" is the name of the last Keeper and is never named in depth intros or event lines.** The "first Keeper" in
  `sealed_shrine` is a different person. Keepers are silhouettes; their faces are never shown.
- **The final substance is never named in advance**, and no recipe is ever explained in story text. Line 5's intro says "seeds"
  and "planted" and never names the substance. The Worldseed is never shown before the ending.
- Everything the player reads that is not content is a `Lang` key; item names stay English in Vietnamese; the "Athanor" logo stays in
  Alagard.
- Limits: depth intro 56 characters; event name 22, text 56, outcome 52 (counted by hand; the longest, `pinned_note`, is exactly 56).
  The two "Walk on" outcomes read "You leave it be."
- Tutorial voice is plain and neutral on purpose (a label, not the story's voice). Each step highlights the thing it names and locks
  every other click until it is done; a click on the text box skips to the next line. Lines are one or two short lines and are
  worded so no lone word wraps in the 288 px box.
- Intro text is written out in the top slots (BitPotion 16 px, cream `#f0dcc0` with a dark shadow); the letterbox bars are y 0 to 30
  and 330 to 360. The nine approved intro lines are word for word; the last three sit together in scene 7, as the intro does now.
  Opening line of the world: "Nothing down here is labelled." Closing line: "Plant it, and the cellar will grow a little deeper."

**The depth intro lines** (`depth.<n>.intro`) are canon: 1 "The air smells of cold stone and old candle." · 2 "Water finds its
way down. So do you." · 3 "Something below keeps a slow, warm note." · 4 "The roots here are thicker than the walls." ·
5 "No hum now. Only the place where seeds are planted."

**Window view per depth** (art notes): 1 a pale slice of the workshop floor far above, a shaft of grey light (dust motes drift up);
2 wet black stone and a stair step edge (drips down the glass); 3 deep blue-black with a warm amber glow far off (the glow pulses
like a breath); 4 pale root tips pressing on the glass (roots creep a pixel at a time, then hold); 5 near black with one faint point
of light low in the glass (still; the point flickers once in a long while). Palette stays inside the game palette.

**Lore events** (`quiet_room`, `names_in_wall` depths 1 to 2, `warm_roots` depths 3 to 4, `pinned_note` depth 5) say the story in
order: the Keepers before you, the cellar is alive, what to do at the bottom (the pinned note reads "Plant it. Do not climb back.").
They have one "Go on" option and no effect.

**Draft, not binding:** the ending lines, seed names and the "Planted" Book line from the endings proposal. They are undecided
(section 7, item 1); do not write them into `en.json` before the owner answers.
