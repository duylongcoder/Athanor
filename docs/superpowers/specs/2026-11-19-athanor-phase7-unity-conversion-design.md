# Athanor Phase 7: Unity conversion — design

**Status:** draft for owner approval, 2026-11-19. **Branch:** `feature/phase7-unity-conversion`, cut from the Phase 6 head (§8).
**Approach chosen by the owner:** A ("every kind of number in its own place"), see §5.
When this phase closes, the parts of this file still true move into `docs/ATHANOR.md` and the file is deleted (2026-11-19 rule).

---

## 1. Intent

The owner reviewed several classes and found too many values hard-coded in C#: pixel positions, colours, timings and rule numbers.
The goal is a project laid out the Unity way, so it is easier to maintain, extend and debug:

- plain C# (no `MonoBehaviour`, no `UnityEngine.Object`) holds rules, state machines and calculations, and is unit-tested;
- `MonoBehaviour` is only the bridge: lifecycle events, wiring to Transform / sprite / tween / effect, pushing input down to the logic;
- tuning lives in data the owner can edit (Inspector for presentation, `content.json` for rules), not in code;
- UI is driven by events and state, not by logic reaching into components;
- lifecycle and cleanup are explicit, including Play / Stop / Play in the Editor with domain reload off;
- per-frame code does no avoidable work or allocation.

The phase changes structure only. **The game must look, play and score exactly as it does today.**

## 2. Where the project stands (recon, 2026-11-19)

Most of the owner's checklist is already in place:

| Checklist item | State today | Evidence |
|---|---|---|
| Plain C# core, separate from MonoBehaviour | done for rules | `Scripts/Core` has no `UnityEngine` (lint UL008, repo check CORE-PURITY); compiled alone by `tools/CoreTests` |
| No `new` on MonoBehaviour, views from scene/prefab | done | views are prefabs + `Bind`; "no code builds a view at run time" (ATHANOR.md §2) |
| Main-thread rule, no `.Result` / `.Wait()` | done | no `Task`, `Thread` or `async` anywhere; coroutines + PrimeTween only; `async void` is lint UL006 |
| Unsubscribe on destroy/disable | mostly done | `GameFlow.OnDestroy`, `Label`, `TitleView`, `Cutscene` unsubscribe; lint UL005 |
| NuGet / platform limits | done | no NuGet packages; WebGL only |
| `UnityEngine.Object` null comparison | done | lint UL004 bans `?.` / `??` on Unity objects |
| Config as ScriptableObject / data | **partial** | content in `content.json`; `ArtLibrary`, `FlipbookSheet` are SOs; Tutorial numbers became Inspector fields today (`TutorialTargets`, `TutorialStyle`) |
| Avoid singletons / statics | **partial** | carry-over 7 lists `ScreenFx.I`, `Sparks.I` |
| Hot path free of allocation | **partial** | lint UL002 catches LINQ / `new` / string concat in `Update`, but not method calls that build strings |

The real gaps:

1. **Presentation numbers in view code.** 58 `Px.Hex("#…")` colour literals in `Scripts/Game` (22 in `Wording`, 6 in `GameFlow`, 4
   each in `TitleView` and `HudView`). Art-pixel positions written into method bodies, e.g. `Px.At(575, 306)` in `HudView.Update`
   (`HudView.cs:237`), bar width 132 and origin 18,70 (`HudView.cs:227-231`), catalyst sockets at 488 + i × pitch (`HudView.cs:141`).
   Cutscene choreography is mostly numbers (about 150 numeric literals in `SimpleCutscene`, 130 in `ReactionCutscene`, 70 in
   `WindowView`). The per-depth ambient / moon / accent arrays are written twice: `GameFlow.cs:80-85` and `DepthMood.Defaults()`.
   Layout rectangles are public statics that other classes read (`HudView.RedrawX/Y/W/H`, `JournalView.IndexRect/RecipesRect/
   HintsRect`, read by `TutorialController`).
2. **Rule tuning numbers in Core, not in `content.json`.** `RunState.cs:22-24` (hand 3/5, trials 4 and 6 at the last depth, redraws 2,
   final depth 5, starter copies 2, offer size 3, rack 12), `PouchCards = 1`, aspects offered at depths 1/3/5 (`RunState.cs:109`),
   the potency table `{0,20,60,180,500,1500}` (`ContentDb.cs:230`), first-reaction ×2 (`Ignite.cs:128`), a fallback of 3 for Heated
   and 40 for Seed when an aspect's `amount` is 0 (`RunStart.cs:65,67,368`), thresholds 2 in `RunStart.cs:165,351`.
3. **A mutable static in Core:** `RunState.PouchMode` (`RunState.cs:83`), set by `tools/CoreReports` from `ATHANOR_POUCH`.
4. **Core writes English sentences** ("+1 trial", "Found Iron Ore", `RunStart.cs:281-301`). English shows them as they are;
   Vietnamese turns them back into `note.*` keys with eleven regexes (`Wording.NoteForms`, `Wording.cs:154`). `Wording` is the only
   `Regex` user in the game.
5. **Statics survive Play / Stop / Play.** `EditorSettings.asset` has Enter Play Mode Options on with domain reload **off**
   (`m_EnterPlayModeOptions: 1`), and no class has a `RuntimeInitializeOnLoadMethod` reset. `Loc` (code, current, `Changed`),
   `PointerInput` (`Rig`, `Down/Up/Held/Hover`, `AnyPress`), `Art.Lib`, `Haptics.Enabled/Sink`, `AudioMute.Sink/Muted`,
   `ScreenFx.I`, `Sparks.I` keep the last session's values and handlers.
6. **`GameFlow` is 798 lines.** It is the composition root, the screen state machine (`FlowState`, return states, pause rule,
   tutorial and sandbox handling) and the Ignite / descent / ending choreography in one MonoBehaviour. The screen flow cannot be
   tested in dotnet.
7. **Per-frame work.** `HudView.Update` formats the potency string, looks up the Ignite sprite and moves five objects every frame
   whether anything changed or not. 27 files have an `Update`.

## 3. Goals, non-goals, anti-goals

**Goals**

- G1. Every rule tuning number is in a `rules` block of `content.json`. Core keeps only named structural constants (the 3x3 bench,
  validation bounds) and the numbers that are an effect's meaning (a "Double" doubles).
- G2. Every presentation number (position, size, colour, timing, easing) is in a prefab or scene serialized field, or in a shared
  ScriptableObject. View method bodies hold no layout, colour or timing literals (§6.1 lists what may stay).
- G3. `GameFlow` is split: the screen state machine is plain C# in Core with dotnet tests; `GameFlow` is the composition root and
  bridge; the Ignite choreography is its own component.
- G4. No mutable static in Core. Statics in Game only from a short allowlist, each with an explicit reset that runs at the start of
  every Play session. `ScreenFx.I` and `Sparks.I` are gone (closes carry-over 7).
- G5. Core reports what happened as keys with arguments; `Wording.NoteForms` and its regexes are gone.
- G6. Idle per-frame code does nothing: `Update` returns early when nothing changed, and does not format strings or swap sprites
  per frame.
- G7. CI keeps it this way: new lint rules for each kind of regression, with the baseline still empty.
- G8. Nothing the player sees or scores changes (§6.10 is the proof).

**Non-goals**

- A 1920x1080 logical canvas. It needs every sprite redrawn and every coordinate re-based; the owner dropped it (2026-11-19). The
  canvas stays 640x360 with whole-number scaling; full screen on a 1080p display is already 3x = 1920x1080.
- PC / Steam. Later, not in this phase. The only thing this phase does for it: platform-specific code lives in the bridge layer only
  (§6.8), so adding a platform later does not touch Core.
- New features, content, balance or text changes.
- A UI framework change (uGUI, UI Toolkit), a DI framework (Zenject, VContainer), Addressables, or turning coroutines into
  `async`/`Awaitable`.
- Sound.

**Anti-goals** (failure even if it works)

- A moved number changes how the game looks, plays or scores.
- One number with two sources of truth (a code default *and* a prefab value both believed; a SO *and* a JSON export).
- Rule numbers in ScriptableObjects, where Core, the dotnet tests and the balance reports cannot read them.
- A weakened CI check, a lint baseline entry, or a skipped test to get green.
- A single "Config" god-object holding every number in the game.
- Views built or laid out from code again.

## 4. Constraints

- **Core:** no `UnityEngine`; compiled alone by `tools/CoreTests` and `tools/CoreReports`. `content.json` shapes are read by
  `JsonUtility` (Unity) and `System.Text.Json` (dotnet): public fields, no properties, no dictionaries.
- **Unity serialization:** no `Action`, tuples, dictionaries, queues, or plain classes without `[Serializable]`; a serialized string
  is `""`; renaming a serialized field needs `[FormerlySerializedAs]` (lint UL301).
- **`Main.unity` and the prefabs are the source of truth**; no scene generator (decision 2026-11-18).
- **Editor:** Enter Play Mode Options on, domain reload off (keep it; this phase makes the code safe for it).
- **WebGL:** one thread; build ceiling 11,824,672 B (10,961,269 B measured, 863,403 B headroom).
- **CI gate:** `tools/ci/run.sh all` green; lint baseline empty; `MIN_TESTS` 250 in Core.
- **Process:** a decision goes in ATHANOR.md §4 before it is relied on; the owner commits, agents never do; one owner per file per
  wave; content and rules first, UI next, QA last; `python3 tools/vault/build_vault.py` after C# or content changes.

## 5. Approaches

| | Approach | Trade-off | |
|---|---|---|---|
| **A** | **Each kind of number in its own place.** Rules → `content.json` `rules`. Positions → the prefab itself. One-owner presentation values → serialized fields. Shared presentation values → a few ScriptableObjects. | Two homes (JSON for rules, Inspector for looks), but each number has exactly one, and both are visible to the tools that need them. | **chosen** |
| B | ScriptableObject for everything, rules included. | Everything in the Inspector, but Core cannot read an asset: needs an Editor export to JSON for the dotnet tests and reports, i.e. two sources and a sync check; content-designer edits Unity YAML. | rejected |
| C | One `tuning.json` for rules and layout. | Text, easy for agents and the vault, but positions live outside the prefab (no drag in the Scene view, two sources per position); contradicts the 2026-11-18 decision. | rejected |

The owner chose A on 2026-11-19: it is the only option that keeps the Inspector for presentation *and* a single text source for
rules that CI and the balance reports read.

## 6. Design

### 6.1 Where each kind of number goes

| Kind | Example today | Goes to |
|---|---|---|
| Rule tuning | `BaseHand = 5`, potency table | `content.json` → `rules`, read as `ContentDb.Rules` |
| Shape of the board or data | `Bench.Size = 3`, `TileCount = 9`, `ContentDb.MinItems..MaxItems`, the corner tiles `{0,2,6,8}` | stays a named `const` in Core, or derived (e.g. final depth from `depthNames.Length` if it always matches) |
| An effect's meaning | `KindDouble` → ×2, `FirstTriple` → ×3 | stays in code (the name is the number) |
| Fixed position of a part | Ignite label at art 575,306 | the part's Transform in the prefab; the code line is deleted |
| Layout parameter used in code | catalyst pitch 35 / 105, potency bar origin 18,70 and width 132, row pitch 10 / 12 | a `[Serializable]` layout struct on that view, values held by the prefab |
| Colour used by one view | Redraw up / over / pressed / off | serialized field on that view |
| Colour used by several | rarity colours, gold `#f8d250`, note colours `#11606a` / `#7a5430` | the `Palette` ScriptableObject |
| Timing of one sequence | story fade 0.9 s, hold 2.4 s | serialized timing struct on that component |
| Timing shared across components | reaction pace, hurry factor, float-text speed | the `Pacing` ScriptableObject |
| Depth look | ambient, moon, accent, bloom, vignette, fog | `DepthMood.presets`, already serialized in `Main.unity` (5 entries); `GameFlow`'s arrays and `DepthMood.Defaults()` go |
| Dev hooks the web tools read | `"[auto] "`, `"[dev] mood "` | stays `const` (tools/web parse them) |
| Plain arithmetic | `0.5f + 0.5f * sin`, `/ 2f`, 0, 1 | stays |

The rule of thumb: **one owner → serialized field on the owner; two or more owners, or values swapped as a set → ScriptableObject;
the game's rules → `content.json`.**

**No code defaults after migration.** A field starts with today's value as its initializer only long enough to be saved into the
prefab or asset. Once the prefab or asset holds the value, the initializer is removed and a bake test pins the serialized value against today's value
(recorded in the test, not "non-zero", since some values are 0), so the code holds no second copy. This also applies to `TutorialTargets` and `TutorialStyle` (today they keep their defaults).
*(My call; see §8.)*

### 6.2 Rules in `content.json`

- New top-level `rules` object, a `[Serializable] RulesData` class in `ContentData.cs` with public fields, read the same way by both
  JSON readers. Shape (field names are the builder's; values are today's):

  ```json
  "rules": {
    "hand": { "min": 3, "base": 5 }, "trials": { "base": 4, "finalDepth": 6 }, "redraws": 2,
    "starterCopies": 2, "offerSize": 3, "rackSize": 12, "pouchCards": 1,
    "aspectDepths": [1, 3, 5], "potencyByTier": [0, 20, 60, 180, 500, 1500], "firstReactionMult": 2
  }
  ```

- `ContentDb` validates the block (ranges, array lengths against tiers and depths) and **throws if it is missing**; no hidden
  defaults in code.
- `RunState`, `RunStart`, `ContentDb.BasePotency`, `IgniteResolver` read `db.Rules`. Callers outside Core that use the old constants
  (`BenchView`, `CatalystChoiceView`, `RunOverView`, Core and Game tests, `tools/CoreReports`) read `db.Rules` too.
- Aspect `amount` fallbacks (Heated 3, Seed 40): `amount` becomes required for those effects in `content.json`, validation enforces
  it, the fallback code goes. The values in `content.json` are set so play is unchanged.
- `RunState.PouchMode` becomes a field of `RunOptions` (default 2, the shipped mode); `tools/CoreReports` sets it from
  `ATHANOR_POUCH` through the options.
- Which remaining Core numbers are tuning and which are shape (`BenchRules.MaxRi = 13`, `Chain.SputterSize = 3`, the thresholds of 2
  in `RunStart`, `TutorialScript.Seed`, `QuietBefore`) is the builder's classification, written as a table in the brief and checked by
  content-designer. Tutorial seed and script stay in Core (pinned by tests, decision 2026-11-19).
- `vault/Data/Data Design.md` gets the `rules` table; content-designer owns `content.json`, rules-engineer owns Core.

### 6.3 Presentation numbers

- **Per view, in this order:** (1) add the serialized field with today's value; (2) code reads the field; (3) open and save the
  prefab or scene so the value is serialized; (4) capture before and after (§6.10), identical; (5) remove the initializer and add
  the bake-test assertion.
- **Fixed positions** move into the prefab Transform and the positioning line is deleted. **Moving positions** (counters, bars, the
  potency number that shifts when long) keep their formula in code with the anchor and sizes from the layout struct.
- **Cross-view rectangles** (`HudView.Redraw*`, `JournalView.*Rect`) stop being statics. Each view exposes its rectangle from its
  own layout as an instance property, and `TutorialController` reads it from the live view it already has through `GameFlow`.
- **Shared ScriptableObjects** (names and folder are the builder's; my proposal is `Assets/Data/`):
  - `Palette`: named colours used by more than one view, including the 22 in `Wording` (rarity and kind colours).
  - `Pacing`: shared timings of the Ignite / reaction sequence.
- **Depth looks need no new asset.** `DepthMood.presets` is already serialized in `Main.unity` and `DepthStage.ShowMood` applies it
  after `GameFlow.Mood` sets the ambient light and moonbeam from its own arrays. `GameFlow.Mood` reads the preset of the
  scene's `DepthMood` instead, and the arrays at `GameFlow.cs:80-85` and `DepthMood.Defaults()` are deleted. Where the two copies
  differ, the scene's values are what the player sees today; the captures decide. What happens without a `DepthStage` (the flat
  path) is the builder's call.
- **Cutscenes:** their numbers go into serialized timing structs grouped by beat (e.g. intro page: fade in, hold, fade out; banner:
  rise, hold), not one flat list per class. Unity Timeline was considered and is out of scope: it would rewrite the cutscenes,
  which risks the identical-look rule, and the package adds wasm size.
- **How views get them:** a serialized reference on the view or scene object, checked in `Bind` the way `GameFlow.Awake` checks its
  views today. `Palette` is also published as `Art.Palette` next to `Art.Lib` for the static helpers (`Wording`, `RarityStars`);
  it joins the allowlist in §6.5.

### 6.4 Splitting `GameFlow`

- **Core `ScreenFlow`** (plain C#): `FlowState` moves to Core. It holds the current state and the rules of moving between screens:
  where Journal and Goals return to, when pause is allowed (`PauseMenu.Allowed` moves here), the title choices, the tutorial gate
  (`TutorialScript.ShouldRun` is already Core), the sandbox replay. It exposes methods per player intent and a `StateChanged` event.
  dotnet tests cover every transition, including the illegal ones.
- **`GameFlow`** (MonoBehaviour): composition root (the `Awake` wiring and checks), saves, the main coroutine loop, routing view
  events into `ScreenFlow` and `ScreenFlow` changes out to views.
- **Ignite sequence** (its own MonoBehaviour, on the GameFlow object or a child): `Resolve`, `Fizzle`, `Sputter`, `Discover`,
  `React`, `Drawn`, `Points`, `ToRack`, `FlashTiles`, with its timings from `Pacing`.
- `Descend` and `EndRun` stay in `GameFlow` or move to a story sequence: the builder's call.
- Coroutines stay coroutines. `GameFlow` should end near 350 lines; that is a guide, not a gate.

### 6.5 Statics and lifecycle

| Static today | Fate |
|---|---|
| `ScreenFx.I`, `Sparks.I` | removed; a serialized reference passed in `Bind` to each user (cutscenes, `EventView`, `GameFlow`, ignite sequence) |
| `PointerInput.Rig` | removed; set on the instance through `Bind` |
| `PointerInput.Down/Up/Held/Hover`, `AnyPress` | allowlisted (one pointer per app, read by many small parts) |
| `Loc` (`Code`, `Current`, `Changed`) | allowlisted (every `Label` needs it in `OnEnable`) |
| `Art.Lib`, `Art.Palette` | allowlisted |
| `Haptics.Enabled/Sink`, `AudioMute.Sink/Muted` | allowlisted |
| `RunState.PouchMode` (Core) | removed (§6.2) |

- Every allowlisted class has a
  `[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]` method that clears its values and its event
  handlers. The allowlist lives in `tools/lint/lint.config.json` and in ATHANOR.md §2.
- Coroutines and tweens a component starts are stopped by that component when it is disabled or destroyed.
- An EditMode test calls each reset and asserts it is clean. QA adds a manual check: Play, Stop, Play, then Title → Bench → Journal →
  Title with no console errors and no doubled handlers.

### 6.6 Core reports keys, not sentences

- `RunStart` notes become a small `[Serializable]` note type: a `note.*` key plus its number and item. The keys already exist in
  both language files (`note.trials`, `note.found`, `note.ri_shift`, …).
- `EventView` renders them through `Loc`. `Wording.NoteForms`, `Wording.Note` and the regexes are deleted. Measure the build: if
  `System.Text.RegularExpressions` is stripped, the build gets smaller.
- Core tests that compared English sentences compare keys and arguments; `LangTests` keep the wording tests.
- localizer-vi checks that no key is missing (it should not be).

### 6.7 Hot paths

- `HudView.Update`: format the potency text only when the rounded value changes; swap the Ignite sprite and move the label only when
  `canIgnite` or `igniteDown` changes; lay out the bar only when its width changes.
- The same pass over the other 26 `Update` methods: return early when idle; no string building, sprite lookup or `GetComponent`
  per frame.
- Proof: on a development build, the idle bench allocates 0 B per frame in the Profiler ("GC Alloc"). The figure is written in
  ATHANOR.md §6.

### 6.8 Platform and threading rules

Written into ATHANOR.md §2 and enforced by lint; little code changes.

- WebGL only. No `Task.Run`, `Thread`, `System.Threading.Timer`, `.Result` or `.Wait()` in `Scripts/`. Coroutines and PrimeTween;
  Unity's `Awaitable` only with `destroyCancellationToken`.
- Platform-specific code (`#if UNITY_WEBGL`, `UNITY_STANDALONE`, …, jslib calls) only in an allowlist of bridge files (`Save`,
  `Haptics`, `AudioMute`, `GameFlow.Awake` frame pacing, `Dev`).
- No NuGet packages. Any new package needs a WebGL / IL2CPP check and a line in ATHANOR.md §4.

### 6.9 CI enforcement

New lint rules in `tools/lint/unity_lint.py`, each with tests in `tools/lint/tests` and a row in the ATHANOR.md §5 table. **A rule
is switched on in the same task that fixes its last finding**, so the baseline stays empty.

| Rule | Catches |
|---|---|
| UL009 | a colour literal (`Px.Hex("#…")`, `new Color(` with number literals) in `Scripts/Game` |
| UL010 | `Px.At` / `Px.Mid` / `new Rect` / `new Vector2/3` with number literals in `Scripts/Game` (0 and 1 allowed) |
| UL011 | a non-readonly static field in `Scripts/Core` |
| UL012 | a mutable static in `Scripts/Game` (non-readonly field, static event, static property with a setter) outside the allowlist, or an allowlisted class without a `SubsystemRegistration` reset |
| UL013 | `Task.Run`, `new Thread`, `.Result`, `.Wait()`, `System.Threading.Timer` in `Scripts/` |
| UL014 | a platform `#if` outside the bridge allowlist |

UL009 and UL010 apply to method bodies and field initializers of `Scripts/Game`, not to `Editor/` or tests. Numbering is
provisional: the builder takes the next free numbers.

### 6.10 Proof that nothing changed

**Before the first change** (task W0), record a baseline under `production/qa/evidence/phase7/baseline/`:

- the output of all six `tools/CoreReports` reports;
- a `ViewCapture.Render` PNG of every view prefab and scene object in fixed states, in English and in Vietnamese (the builder adds a
  capture-all Editor command if `PrefabBakeTests` does not already cover each one);
- `webtest.mjs`, `autoplay.mjs` and `descents.mjs` results on a dev build.

**After every task:**

- reports byte-identical to the baseline;
- captures differ by 0 pixels (`ViewCapture.Differ`);
- `dotnet test tools/CoreTests` and `unity test Athanor --mode EditMode`: 0 failed, 0 skipped;
- `tools/ci/run.sh all` exit 0;
- `build_vault.py` leaves no diff.

**At the end of the phase:** a release build under the size ceiling, `loadsmoke`, `webtest` and `autoplay` green, and the GC figure
from §6.7.

### 6.11 Workstreams and order

Structure first, data next, guards last, so each number moves once, together with the code that uses it.

| # | Work | Owner (agent) | Depends on |
|---|---|---|---|
| W0 | Baseline (§6.10); write the decisions of this phase in ATHANOR.md §4 | main session, qa-engineer | — |
| W1 | Rules block: `RulesData`, validation, Core reads `db.Rules`, `PouchMode` into `RunOptions`, amount fallbacks out; UL011 on | rules-engineer (Core, tests), content-designer (`content.json`, Data Design note) | W0 |
| W2 | Notes as keys; `Wording.NoteForms` out | rules-engineer (Core), unity-gameplay-engineer (`EventView`, `Wording`), localizer-vi (check) | W1 |
| W3 | Statics: `ScreenFx.I`, `Sparks.I`, `PointerInput.Rig` out; allowlist and resets; UL012 and UL013 on | unity-gameplay-engineer | W0 |
| W4 | `GameFlow` split: Core `ScreenFlow` + tests; bridge; ignite sequence | rules-engineer (Core), then unity-gameplay-engineer (Game) | W1–W3 |
| W5 | Shared assets `Palette` and `Pacing`; depth looks read from `DepthMood` | unity-gameplay-engineer | W4 |
| W6 | Per-view numbers, in waves of disjoint files: (a) Hud, Bench, Tile, Card, Link; (b) Journal, Goals, Choice, Event, RunOver, Pause, Title, Toast, Tooltip; (c) Simple, Reaction, Window and Tutorial cutscenes; (d) Depth and Fx. UL009, UL010, UL014 on at the end | unity-gameplay-engineer; ui-ux-designer reviews the layout structs | W5 |
| W7 | Hot paths (§6.7) and GC figure | unity-gameplay-engineer, performance check by qa-engineer | W6 |
| W8 | ATHANOR.md §2/§4/§5/§6 brought up to date, vault rebuilt, sign-off | vault-librarian, qa-engineer | W7 |

Each task ends at a checkpoint with a suggested Conventional Commit message and `Story: P7-<n>`; the owner commits.

## 7. Left to the builder

Names of new types, files and the asset folder; the exact field names of `rules`; the tuning-vs-shape classification of the leftover
Core numbers (checked by content-designer); how layout structs are split per view; where `Descend` / `EndRun` go; how the
capture-all command is started; the final lint rule numbers.

## 8. Calls made without the owner (check these)

1. The phase is called **Phase 7**, stories are `P7-<n>`, and the work goes on a new branch `feature/phase7-unity-conversion` cut
   from the current Phase 6 head (rebased when Phase 6 merges).
2. Code defaults are removed once a value lives in the prefab or asset, **including `TutorialTargets` / `TutorialStyle`**, which keep
   theirs today.
3. `Loc`, `PointerInput`, `Art`, `Haptics`, `AudioMute` stay static (allowlisted, with resets) instead of being injected everywhere.
4. Shared ScriptableObjects go in a new `Assets/Data/` folder.
5. A missing `rules` block is an error, not a fallback to built-in values.
6. Structure moves before numbers (W4 before W5–W6), so `GameFlow`'s numbers move once, together with the code that uses them.
7. No `DepthLooks` asset: the scene's `DepthMood` already holds the five presets, so the duplicate in `GameFlow` is deleted instead.
8. Cutscene numbers become timing structs grouped by beat; Unity Timeline is out of scope.
9. "Nothing changed" is proven in both languages (captures in English and Vietnamese).
