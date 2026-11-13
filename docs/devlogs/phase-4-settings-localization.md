# Phase 4: Settings panel and localization (research)

Status: **Approved with changes (owner, 2026-11-13); first build done.** `Core/Lang.cs`, `Resources/lang/en.json`,
`Resources/lang/vi.json` and `Tests/EditMode/LangTests.cs` exist; no Game view uses them yet. Decisions, font results,
file format and glossary: [section 8](#8-decisions-and-first-build-2026-11-13). Sections 1 to 7 are the original research.
Owner's request: a Settings panel on the title screen with music volume, SFX volume, vibration on/off and Language
(Vietnamese and English for now). Text lives in separate files that are easy to track and edit. Audio will use FMOD
(studied separately; this doc only plans the buses and the volume calls).

Short answer:

- **Text:** about 165 strings in code (150 in Game, 15 in Core) plus 18 data ids shown as words, and 283 strings in
  `content.json` (1,089 words). Around 35 are built by concatenation and 9 need plural forms.
- **Fonts:** none of the four fonts can show Vietnamese. Alagard has no accented letters at all. The other three have 32
  to 40 of the 134 Vietnamese letters. Also, every font atlas is baked with ASCII only, so even those 32 to 40 are not
  in the game today.
- **Recommendation:** our own JSON files per language, read by a small Unity-free `Lang` class in Core. Not the Unity
  Localization package. Borrow its ideas (key names, named placeholders, plural forms, pseudo-locale).

---

## 1. Inventory of player-facing text

Method: grep of every string literal in `Athanor/Assets/Scripts/Game` and `Core`, sprite names, colours, object names and
ids removed by hand; a Python count over `Athanor/Assets/Resources/content.json`. Counts are "strings to give a key",
after merging obvious duplicates inside one file. A template with numbers counts as one string.

### 1.1 Code (Game and Core)

| Source | Strings | Examples | Notes |
|---|---:|---|---|
| `Game/Ui/TitleView.cs` | 13 | "Begin", "Journal", "Goals", "An alchemist's descent", "RESET PROGRESS", "Erase progress?", "Erase goals and unlocks? This cannot be undone." | Progress line is one concatenated template. Logo "Athanor" is a name (keep?). Font credit line. |
| `Game/Bench/HudView.cs` | 26 | "POTENCY", "TRIALS", "REDRAWS", "HINT", "Ignite", "NO TARGET", "Nothing happens" | Hints and depth notes are concatenated. "Something burned / hotter and spoiled / a mixture." is one sentence split over 3 labels. |
| `Game/Bench/BenchView.cs` | 10 | "RACK FULL: THE WEAKEST WILL FALL", "Warm Tile", "Shut Tile", "Swap the cards in your hand for new ones. Click to use one." | Material tooltip is a 6-part concatenation. |
| `Game/Ui/JournalView.cs` | 21 | "MATERIALS", "LATEST HINTS", "Locked", "Unknown", "Made from", "Reacts with nothing more", "... and {n} more" | Wraps by character count (`WrapAt = 38`). |
| `Game/Ui/RunOverView.cs` | 15 | "Depth reached", "Descend again", "The Great Work", "The Fire Dies", "You made the {name}." | Goals line fits by letter count (`Fits = 25`). |
| `Game/Ui/GoalsView.cs` | 6 | "Goals", "Close", "{a} OF {b} GOALS COMPLETE", "UNLOCKED: {item}" | Section heads use rarity ids as words. |
| `Game/Ui/CatalystChoiceView.cs` | 7 | "Choose a Catalyst", "It bends the rules for the rest of this descent.", "TRADE-OFF ASPECT" | "{RARITY} CATALYST" uses the rarity id. |
| `Game/Ui/GoalToast.cs` | 3 | "GOAL COMPLETE: {goal}", "Unlocked: {item}", "Nothing was left to unlock" | |
| `Game/Ui/EventView.cs` | 2 | "Continue", "{TYPE}  -  DEPTH {n}" | Event type id shown as a word. |
| `Game/Cutscene/SimpleCutscene.cs` | 11 | "Under the old workshop, the cellar keeps going down.", "CLICK TO BEGIN", "Depth Cleared" | Intro and ending lines. The tutorial/lore plan changes the intro. |
| `Game/Cutscene/ReactionCutscene.cs` | 6 | "Experiment {n}", "CLICK TO SKIP", "CLICK TO CONTINUE" | Header cut at 46 letters. |
| `Game/Support/Wording.cs` | 17 | "+1 trial", "a discovery", "lose a card", "a catalyst made rarer", "New {rarity} catalyst", "nothing" | Parses "COST:" out of aspect text; replaces "the attuned kind" with "a/an {kind}". |
| `Game/Ui/Tooltip.cs`, `UiButton.cs`, `Boot/GameFlow.cs` | 0 | (only "+{potency}", a number) | Tooltip and button only show text they are given. |
| **Game total** | **~137** | | |
| `Core/RunStart.cs` (event notes) | 14 | "+1 trial", "+2 cards in hand this depth", "Found {name}", "Gave up {name}", "Lost a {name} for this run", "{name} became {name}", "A tile is shut this depth" | Core is Unity-free. Tests count `Notes`, not their text. |
| `Core/ContentDb.cs` | 1 | "Depth {n}" (fallback depth name) | |
| **Core total** | **15** | | |
| Data ids shown as words | 18 | 7 kinds (`Ore`, `Plant`...), 4 rarities (`Common`...), 7 event types (`Choice`, `Gamble`...) | Shown with `ToUpperInvariant()`. They are ids, so they must stay English in data and get display keys. |

About 152 code strings plus 18 ids: **~170 keys**, a little under that after merging repeats ("Journal", "Close",
"CLICK TO CONTINUE", "The Great Work" appear in several files).

### 1.2 content.json

| Field | Strings | Words | Chars | Example |
|---|---:|---:|---:|---|
| `materials[].name` | 81 | 141 | 861 | "Ember Coal", "Philosopher's Salt" |
| `catalysts[].name` | 26 | 45 | 283 | "Bellows" |
| `catalysts[].text` | 26 | 202 | 1,196 | "Reactions involving an Ore score double." |
| `aspects[].name` | 16 | 30 | 164 | "Doubled Tile" |
| `aspects[].text` | 16 | 194 | 1,003 | "One more trial at every depth. COST: this tile is shut for the run." |
| `goals[].text` | 22 | 76 | 357 | "Reach depth 2", "Ignite 25 times" |
| `events[].name` | 16 | 46 | 230 | "A Cracked Altar" |
| `events[].text` | 16 | 123 | 639 | "A cracked altar asks for an offering." |
| `events[].options[].label` | 28 | 68 | 272 | "Offer a discovery", "Walk on" |
| `events[].options[].outcomes[].text` | 31 | 148 | 732 | "The altar drinks it in. Time slows." |
| `depthNames` | 5 | 16 | 84 | "The Shallow Cellar" |
| **Total** | **283** | **1,089** | **5,821** | |

Coming soon: the tutorial script (`phase-4-tutorial-script.md`, 2,251 words of doc) and the lore (`phase-4-lore.md`)
add tutorial popups, 3 Lore events and a new intro. They should be written with keys from day one.

### 1.3 Strings built by concatenation (hard to translate)

Word order in Vietnamese often differs ("Ember Coal needs heat 14" is "Than Hồng cần nhiệt 14", fine; but "New Rare
catalyst" is "Chất xúc tác Hiếm mới": the adjective moves). Every row below must become one template with named
placeholders.

| Where | Today | Problem |
|---|---|---|
| `TitleView.Progress` | `"JOURNAL: " + known + " OF " + total + " REACTIONS   -   GOALS: " + ...` | 6 pieces. |
| `HudView.HintLine` | `name + " stirs near " + ("an "/"a ") + kind + "."` | English article rule in code. Two variants (with and without name). |
| `HudView.Note` sputter | 3 labels: "Something burned" / "hotter and spoiled" / "a mixture." | One sentence split at English word breaks. Needs one string, wrapped by width. |
| `HudView` depth notes | `"THIS DEPTH: " + sign + n + " CARD A HAND"` | Sign, number, plural. |
| `BenchView` material tooltip | `kind + ", tier " + tier + " - " + known + " known\nReaction Index " + ri + ", heat here " + h` | 6 pieces, optional tail. |
| `BenchView` catalyst tooltip | `name + " (" + rarity + ")"` | Rarity id as word. |
| `JournalView` recipe lines | `"  + " + names + " = " + result + " (heat 22)"` | Mostly symbols, OK; "heat" needs a key. |
| `JournalView` sub line | `KIND + "  -  TIER " + n + "  -  REACTIVITY " + r` | 3 pieces. |
| `RunOverView` goals line | names joined with ", ", then `" AND " + n + " MORE"`, `"  -  VIEW GOALS"` | Fits by letter count; "1 GOALS" can show (no singular). |
| `RunOverView` sub | `"You made the " + name + "."`, `"Out of trials in " + depthName + "."` | Article. |
| `CatalystChoiceView` kicker | `RARITY + " CATALYST"` | Order. |
| `EventView` kind line | `TYPE + "  -  DEPTH " + n` | Type id as word. |
| `GoalsView` heads | `RARITY + "  -  " + done + " OF " + all` | |
| `Wording.OptionSummary` | `cost + ": " + gain`, `"50% " + gain`, `" / "` | Built from effect words; keep the glue as keys too. |
| `Wording.RewardLine` | `"New " + rarity + " starter"` | Order. |
| `Wording.AspectRule` | replaces "the attuned kind" with `A(kind)` inside data text | Needs a `{kind}` placeholder in the text. |
| `Wording.AspectCost` | splits data text at the literal "COST:" | Breaks in any other language. Needs its own field. |
| `Core/RunStart` notes | `"Found " + name`, `"Lost a " + name + " for this run"`, `name + " became " + name`, `name + " reacts with something " + kind` | Core builds English. |
| `ReactionCutscene` header | `a + " + " + b + " + " + n + " more"` | Plural, letter-count cut. |
| `CardView` | splits `m.name` at the first space into two lines | Assumes English word order and length. |

Count: about **35** concatenated strings.

Found while reading (English bugs today):

- `RunStart` LoseCard writes "Lost a " + name. "Ember Coal" and "Iron Ore" are starters, so it can say "Lost a Ember Coal".
- `RunOverView` can write "1 GOALS".

### 1.4 Plural forms

| Where | Forms |
|---|---|
| `Wording.Effect` | trial/trials, card each hand/cards each hand, redraw/redraws, "a discovery"/"{n} discoveries" |
| `HudView` depth note | CARD A HAND / CARDS A HAND |
| `RunStart` notes | trial/trials, card/cards in hand this depth, redraw/redraws |
| `RunOverView` | "{n} GOALS", "AND {n} MORE" (no singular today) |
| `JournalView`, `ReactionCutscene` | "... and {n} more", "+ {n} more" |

That is **9** places. Vietnamese has no plural (one form for every number) and no articles (a/an). English needs
`one`/`other`. So a simple two-form rule is enough for these two languages.

Other things that change by language:

- Numbers: `N0` with the invariant culture gives "1,500". Vietnamese writes "1.500". Owner question.
- Forced capitals: about 25 labels use `ToUpperInvariant()`. It works for Vietnamese (ư to Ư, đ to Đ), but capitals with
  two marks (Ấ, Ổ, Ữ) are taller. See fonts.
- Fixed fits: `JournalView.WrapAt = 38` letters, `RunOverView.Fits = 25` letters, `ReactionCutscene` 46 letters, the
  120 px `UiButton`, the 112 px hint box, card names on two lines. Vietnamese text is often longer than English. These
  should measure pixel width (`TextMeshPro.preferredWidth`) instead of letters.

---

## 2. Fonts and Vietnamese

### 2.1 Which font does what

`ArtLibrary.asset` maps the four `FontKind` values:

| FontKind | Font | Baked size | Used for |
|---|---|---|---|
| Title | Alagard | 16 | Logo, headings, buttons |
| Body | BitPotion | 16 | Sentences, notes, hints |
| Name | PixelOperator-Bold | 16 | Card names |
| Tiny | PixelOperator8 | 8 | Capital labels (TRIALS, HINT) |

### 2.2 Coverage (cmap read from the .ttf files)

fontTools is not installed, so a small Python script read the `cmap` table (formats 0, 4 and 12) of each TTF.
Vietnamese needs 134 letters outside ASCII: 67 lower case and 67 capitals (ă â đ ê ô ơ ư and every vowel with the five
tone marks). No font has combining marks (U+0300 to U+0309, U+0323), so letter + mark cannot be built either.

| Font | Glyphs | Latin-1 letters (U+00C0-00FF) | Vietnamese letters | Has | Missing (examples) |
|---|---:|---:|---:|---|---|
| Alagard | 107 | 1 of 64 (Û) | **0 of 134** | none | everything |
| BitPotion | 320 | 64 of 64 | **40 of 134** | À Á Â Ã È É Ê Ì Í Ò Ó Ô Õ Ù Ú Ý (and lower case), Ă ă Đ đ Ĩ ĩ Ũ ũ | ơ ư Ơ Ư, every hook above (ả), dot below (ạ), and the stacked ones (ấ ầ ẩ ẫ ậ, ắ ằ ...) |
| PixelOperator-Bold | 279 | 64 of 64 | **32 of 134** | À Á Â Ã È É Ê Ì Í Ò Ó Ô Õ Ù Ú Ý (and lower case) | ă đ ơ ư and their capitals, plus everything BitPotion misses |
| PixelOperator8 | 279 | 64 of 64 | **32 of 134** | same as PixelOperator-Bold | same |

Even the words for the language menu fail: "Tiếng Việt" needs ế and ệ, which no font has.

### 2.3 The atlas problem (even before the font)

`Editor/FontBuilder.cs` bakes each TMP font asset as **Static** with only ASCII plus `’×…`. So the game cannot draw
"é" today, even with BitPotion, which has it in the TTF. Any language work must change the baked character set.

### 2.4 Options for Vietnamese

| Option | Look | Work | Notes |
|---|---|---|---|
| A. Extend Pixel Operator (CC0) with the ~102 missing glyphs | Same as today | Medium: draw horn, hook above, dot below and stacked marks; most are base + mark | CC0 allows editing. Covers Name and Tiny. |
| B. Extend BitPotion and Alagard too | Same as today | Medium to high | Check both licences first. Alagard needs all 134 plus Latin-1. |
| C. Swap to a Vietnamese-capable pixel font family when language is VI | Different look in VI | Low | Candidates to check with the same cmap script: Ark Pixel Font and Fusion Pixel Font (OFL, 10/12/16 px), Silver (CC BY 4.0), VT323 (Google Fonts lists a Vietnamese subset). Verify each before choosing. |
| D. TMP fallback font (GNU Unifont, 16 px, whole BMP, OFL) | Mixed glyphs inside one word | Very low | Last-resort safety net only: no tofu boxes, but "Việt" would mix two fonts. |

Recommendation: **C for the first VI build, D as a safety net, A later if the owner wants the same look.** Keep the
"Athanor" logo in Alagard in every language (it is a name). Pick the VI font set per language, not glyph by glyph.

Rules for any choice:

- Text files must be in **NFC** (precomposed). Some Vietnamese keyboards (Unikey "Unicode tổ hợp") type letter + mark.
  The check script must reject U+0300 to U+036F.
- 8 px is too small for stacked marks. In VI the Tiny role needs a 10 to 12 px font. Rows at 10 px spacing in `HudView`
  (depth notes, reaction note) will need a layout check.
- Capitals with stacked marks need 3 to 4 extra pixels above cap height. Tight spots: `HudView` hint (`lineSpacing -30`),
  `EventView` outcome, 26 px buttons. Owner question: drop forced capitals in VI?
- `FontBuilder` should bake the set of characters actually used in the language files (plus ASCII), per font, and log
  what is missing. 134 extra glyphs at 16 px may need a 512x256 atlas instead of 256x256.
- The title credit line ("FONTS: ALAGARD, BITPOTION, PIXEL OPERATOR") must list any new font. OFL and CC BY need credit.

---

## 3. How to store the text: three options

### 3.1 Comparison

| | (a) Unity Localization package | (b) Own JSON per language | (c) ScriptableObject tables |
|---|---|---|---|
| Install | `com.unity.localization` plus Addressables (required dependency) | Nothing | Nothing |
| Core (Unity-free, dotnet tests) can read it | **No** | **Yes** (same way as `content.json`) | No |
| Edit outside Unity | Via CSV or Google Sheets extensions, round-trip | Any text editor, git diff per line | No (YAML assets) |
| Diff in git | YAML table assets, noisy | One line per string | YAML, noisy |
| Plurals / placeholders | Smart Strings (SmartFormat, CLDR plural rules, named args) | Own: `{name}` and `.one`/`.other` keys (~60 lines) | Own |
| Startup on web | Async init (`LocalizationSettings.InitializationOperation`); blocking `WaitForCompletion` is not supported on WebGL; Addressables catalog and bundles are extra requests | Synchronous `Resources.Load`, like `content.json` today | Synchronous |
| Build size | Adds Addressables + Localization + SmartFormat code; measure with a test build (the release build is 10 MB today) | A few KB of JSON | A few KB |
| Fits UI built in code | Partly: its `LocalizeStringEvent` components are for scene objects; we would call `GetLocalizedString` by hand | Yes | Yes |
| Per-language fonts | Asset Tables | `ArtLibrary` font set per language | Same |
| Extra tools | Table editor window, pseudo-localization, locale selectors | We write a small check script | Inspector only |

### 3.2 Recommendation: (b), our own JSON files

Reasons that fit this project:

1. **Core must stay Unity-free.** Core builds player text today (`RunStart` notes) and its tests run under `dotnet test`
   with `System.Text.Json`. A JSON file can be read by Core in both places; the package and ScriptableObjects cannot.
2. **content.json already works this way.** `GameFlow` loads it with `JsonUtility`, tests load it with `System.Text.Json`
   (`TestContent.cs`). Language files follow the same path.
3. **Easy to track and edit,** which the owner asked for: plain text, one line per string, readable diffs, and a translator
   needs no Unity.
4. **Web first.** No Addressables, no async start, no extra downloads.
5. **Two languages, simple grammar.** English one/other and Vietnamese one form need no CLDR engine.
6. **The UI is built in code** (`Make.Text`, `UiButton.Create`), so the package's components would not be used.

What we borrow from the package: dotted keys, named placeholders like Smart Strings, plural keys, a pseudo-locale for
testing, and the locale order (saved choice, then system language, then English). The keys map 1:1 to a String Table
Collection, so a later move to the package is a CSV import.

`JsonUtility` cannot read dictionaries. So the file is a list of entries, which both readers handle:

```json
{
  "language": "vi",
  "name": "Tiếng Việt",
  "entries": [
    { "key": "ui.title.begin", "text": "Bắt đầu", "en": "Begin" },
    { "key": "note.trials.other", "text": "{n:+} lượt thử", "en": "{n:+} trials" }
  ]
}
```

`en` in a Vietnamese entry is the English it was translated from. The check script flags it as **stale** when the English
changes. English files leave it out.

### 3.3 Files

> Superseded by the owner: **one file per language**, `Resources/lang/en.json` and `Resources/lang/vi.json`. See 8.3.

| File | Holds | Edited by |
|---|---|---|
| `Resources/lang/en/ui.json` | All UI, notes, effect words, cutscene lines, kind/rarity/event-type names | Developer |
| `Resources/lang/vi/ui.json` | Same keys in Vietnamese | Translator |
| `Resources/lang/vi/content.json` | Keys for `content.json` text in Vietnamese | Translator |
| `Resources/content.json` | Stays the **English source** for content text | Developer (as today) |
| `tools/lang/check.py` (new) | Reports missing, stale, unused keys, placeholder mismatches, non-NFC text, characters not in the font set | |

English content text is not copied into a second file. The lookup for content is "key in the current language, else the
English text in `content.json`". One source of truth, and Core tests and balance tools do not change.

### 3.4 Key names

Lower case, dots between levels, `snake_case` inside a level. Content keys come from the existing ids.

| Kind | Pattern | Example |
|---|---|---|
| UI | `ui.<screen>.<item>` | `ui.title.begin`, `ui.title.progress`, `ui.hud.trials`, `ui.journal.page_of`, `ui.common.close` |
| Settings | `ui.settings.<item>` | `ui.settings.music`, `ui.settings.vibration`, `ui.settings.on` |
| Cutscenes | `cutscene.<name>.<n>` | `cutscene.intro.1`, `cutscene.ending.3`, `cutscene.depth_cleared` |
| Event notes (Core) | `note.<op>[.one/.other]` | `note.trials.one` = "{n:+} trial", `note.found` = "Found {item}" |
| Effect words | `effect.<op>[.one/.other]` | `effect.lose_card` = "lose a card" |
| Material | `material.<id>.name` | `material.ember_coal.name` |
| Catalyst | `catalyst.<id>.name`, `.text` | `catalyst.bellows.text` |
| Aspect | `aspect.<id>.name`, `.text`, `.cost` | `aspect.doubled_tile.name` |
| Goal | `goal.<id>.text` | `goal.g_depth2.text` |
| Event | `event.<id>.name`, `.text` | `event.cracked_altar.text` |
| Event option | `event.<id>.opt.<option id>.label` | `event.cracked_altar.opt.offer.label` |
| Outcome | `event.<id>.opt.<option id>.out.<n>` | `event.cracked_altar.opt.offer.out.0` |
| Depth name | `depth.<n>.name` | `depth.1.name` = "The Shallow Cellar" |
| Ids shown as words | `kind.<id>`, `rarity.<id>`, `event_type.<id>` | `kind.ore`, `rarity.rare`, `event_type.gamble` |

Plurals: `.one` is used when |n| = 1, `.other` otherwise; a language with only `.other` uses it for every n. Placeholders:
`{n}`, `{n:+}` (always signed), `{item}`, `{kind}`, `{a}`, `{b}`. A missing key shows as `#key` so it is easy to spot.

Content changes needed for keys (small, in the implementation plan):

- Event options have no id; keys by index break when options are reordered. Add `id` to each option (28 options).
- Aspect text holds its cost after "COST:". Move it to a `cost` field (3 aspects) and stop parsing it in `Wording`.
- The attuned aspect says "the attuned kind" and code replaces it. Use a `{kind}` placeholder.
- Core notes: change `EventResult.Notes` from English strings to (key, args) pairs and render them in Game; or let Core
  call `Lang` directly (it is pure C#). Either keeps the note count the tests check.

---

## 4. Settings data

### 4.1 Fields

| Field | Type | Default | Range | Notes |
|---|---|---|---|---|
| `version` | int | 1 | | For later migrations |
| `music` | int | 7 | 0-10 | 0 = off |
| `sfx` | int | 8 | 0-10 | 0 = off |
| `ambience` | int | 7 | 0-10 | Only if the FMOD project has an Ambience bus |
| `haptics` | bool | true | | Ignored and hidden where unsupported |
| `language` | string | "" | "", "en", "vi" | "" = not chosen yet: use the system language |

Defaults are a guess; owner question.

### 4.2 Storage

- Key `athanor.settings.v1` in PlayerPrefs, JSON via `JsonUtility`, next to `athanor.journal.v1`, `athanor.intro.v1` and
  `athanor.profile.v1` in `Save.cs`. Add `Save.LoadSettings()` / `Save.StoreSettings()` in the same style: unreadable or
  missing data gives defaults.
- The data class and its clamping live in Core (`Core/Settings.cs`, Unity-free), so dotnet tests can cover them.
- Save when the panel closes (and on quit/pause), not on every click. On the web PlayerPrefs is browser storage and each
  `PlayerPrefs.Save()` writes it.
- "Reset progress" only deletes the profile key, so settings survive it. Keep it that way.

### 4.3 Applying at boot

1. Load settings first in `GameFlow.Start`.
2. Pick the language: saved choice, else `Application.systemLanguage == Vietnamese` gives "vi", else "en". Check on a web
   build that `systemLanguage` follows the browser language; if not, read `navigator.language` in the `.jslib`.
3. Load the language files and pick the font set, **before** any view's `Build()` (views write their labels in `Build()`).
4. Apply volumes when FMOD has loaded its banks (on the web banks load asynchronously). Until then keep the values and
   apply them as soon as the buses are valid.

### 4.4 FMOD buses and volume

| Bus | Holds | Slider |
|---|---|---|
| `bus:/Music` | Music | Music |
| `bus:/SFX` | Reactions, UI clicks, cutscene hits | Sound |
| `bus:/Ambience` (optional) | Candle crackle, cellar drips | Ambience, if the bus exists |
| `bus:/` (master) | Everything | none |

```csharp
var bus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
if (bus.isValid()) bus.setVolume(VolumeCurve.Linear(settings.music));
```

- `setVolume` takes linear gain (0 to 1). Map steps through decibels, not straight: step 0 is silence, steps 1 to 10 go
  from about -30 dB to 0 dB, gain = 10^(dB/20). The curve is a Core function with a unit test.
- FMOD also offers VCAs (`RuntimeManager.GetVCA("vca:/Music")`), which are meant for player-facing volume. The FMOD study
  should decide bus or VCA; the settings code does not care.
- Put the calls behind a small `IVolumes` interface in Game, so the panel works and is testable before FMOD lands.
- When the Sound slider changes, play one short click so the player hears the new level.

### 4.5 Haptics

| Platform | API | Support | Panel |
|---|---|---|---|
| Web, Android Chrome / Edge / Samsung | `navigator.vibrate(ms)` via a `.jslib` | Yes, after a user tap | Show |
| Web, iOS Safari (and every iOS browser, all use WebKit) | none | **No** | Hide |
| Web, desktop browsers | `navigator.vibrate` may exist but nothing vibrates; Firefox has removed it (check at build time) | No | Hide |
| Android app | `Handheld.Vibrate()` (one fixed buzz); `Vibrator` through `AndroidJavaObject` for short pulses | Yes | Show |
| iOS app | `Handheld.Vibrate()` (one long buzz); short taps need a native plugin (`UIImpactFeedbackGenerator`) | Yes | Show |
| Editor, desktop | none | No | Hide |

The `.jslib` exposes two functions: `Athanor_CanVibrate()` returns true only when `"vibrate" in navigator` **and** the
device has touch (`navigator.maxTouchPoints > 0` or `matchMedia("(pointer: coarse)")`), and `Athanor_Vibrate(ms)`.
A Game `Haptics` class wraps all platforms with `Supported` and `Pulse(ms)`.

Hidden when unsupported: the whole Vibration row; the rows below move up. The saved value stays, so the same profile on a
phone keeps its choice. When the player turns it on, the device pulses once.

The game does not vibrate anywhere yet. Owner question: which moments (Ignite press, a discovery, depth cleared, run lost).

---

## 5. Settings panel on the title (640x360)

### 5.1 Where it opens

Today the title has three 120x26 buttons at y = 214, 244, 274, a progress line at 306, "RESET PROGRESS" at 322 and the
font credit at 345. `tools/web/webtest.mjs` clicks Begin at (320, 227) and Journal at (320, 257), so those must not move.

Proposal:

- Add a fourth `UiButton` "Settings" at (260, 304).
- Move the progress line to y = 334 and the font credit to y = 348.
- Move "RESET PROGRESS" into the Settings panel. It is a rare, dangerous action and frees the row.

Alternative: a small gear icon in a corner and keep the title as it is. Owner question.

### 5.2 Layout

The panel reuses the 320x210 `dialog` sprite at (160, 75), the same as the "Erase progress?" dialog.

```
       x=160                                      x=480
y=75   +----------------------------------------------+
       |                   Settings                   |  Title 32, y+14
       |  ------------------------------------------  |  rule y+50
y+60   |  Music      <  ■■■■■■■□□□  >   7             |
y+80   |  Sound      <  ■■■■■■■■□□  >   8             |
y+100  |  Ambience   <  ■■■■■■■□□□  >   7             |  only if the bus exists
y+120  |  Vibration     [ On ]                        |  hidden if unsupported
y+140  |  Language   <   English    >                 |
       |  ------------------------------------------  |  rule y+158
y+164  |  RESET PROGRESS                              |  Tiny, opens the Erase dialog
y+178  |                   [ Back ]                   |  UiButton at x=260
y=285  +----------------------------------------------+
```

| Part | Size and place | Built with |
|---|---|---|
| Row label | Body 16, x = 188 | `Make.Text` |
| `<` and `>` | Title 16, 16x16 hit area, x = 290 and 422 | `Make.Text` + `Make.Click` |
| 10 level cells | 9x7 each, 1 px gap, x = 306 to 405 | `Make.Rect` tinted (lit `#f8d250`, empty `#3a2418`); no new art |
| Value | Title 16, right aligned at x = 452; "Off" at 0 | `Make.Text` |
| Vibration toggle | "On" / "Off" text in a 40x16 hit area | `Make.Text` + `Make.Click` |
| Language | `<` name `>`; the name in its own language ("English", "Tiếng Việt") | `Make.Text` + `Make.Click` |
| Back | 120x26 | `UiButton.Create` |
| Blocker | full screen, 0.7 black, catches clicks | `Make.Rect` + `Make.Click`, as in the Erase dialog |

Controls:

- Sliders are 11 pixel steps (0 to 10). Arrows step by one; clicking a cell sets that level; dragging across cells is a
  later nicety. At a phone's 2x to 3x scale a 16 px arrow is 32 to 48 CSS pixels, big enough to tap.
- Changes apply at once (volume heard, language relabels the panel itself). The file is written on Back.
- A click outside the dialog closes it, like the Journal.
- `TitleView.Asking` must be true while the panel is open, so the title buttons under it do nothing.
- Order: title `O = 420`, Settings at `O + 40`, the Erase dialog above it at `O + 60` when opened from the panel.
- Rows with a hidden control collapse; the panel keeps its size.

### 5.3 Changing language while the game runs

Labels are written once in `Build()`. Two ways to relabel:

1. Static labels remember their key: `Make.Text` and `UiButton.Create` get overloads that take a key, and a `Lang.Changed`
   event rewrites them and swaps their font. Dynamic text is rewritten anyway when a view opens.
2. Rebuild every view on change.

Recommend 1, and allow the change only on the title (no run in progress).

---

## 6. Task breakdown (for a later implementation plan)

Each task ends with a check.

| # | Task | Check |
|---:|---|---|
| 1 | `Core/Lang.cs`: load entry lists, `Get`, named placeholders, `{n:+}`, one/other plurals, fallback vi to en to `#key` | New Core tests under `dotnet test` |
| 2 | `Core/Settings.cs` data, defaults, clamping, volume curve; `Save.LoadSettings/StoreSettings` with `athanor.settings.v1` | Core tests: round trip, broken JSON gives defaults, clamp, curve |
| 3 | `lang/en/ui.json` with every code string; replace literals in Game views with keys | English web build looks the same: webtest screenshots match the current ones |
| 4 | Turn the ~35 concatenations into templates; fix "Lost a Ember Coal" and "1 GOALS" | Unit tests for each template in both plural forms |
| 5 | Core notes as key + args (or Core calls `Lang`) | Existing Phase 3 note-count tests still pass |
| 6 | content.json: option ids, aspect `cost` field, `{kind}` placeholder; content key lookup with English fallback | `ContentTests`: option ids unique per event; every key resolves |
| 7 | Pixel-width wrapping and fitting in Journal, RunOver, ReactionCutscene, CardView | Long pseudo strings stay inside their boxes |
| 8 | Pseudo-locale `xx`: English with marks and +35% length ("[Ẽmbêr Çôàl ~~~]") | Screenshot every screen in `xx`; no overflow, no `#key` |
| 9 | Fonts: choose and verify the VI font set (cmap script moved to `tools/`), `FontBuilder` bakes the characters used per language, per-language font set in `ArtLibrary`, Unifont fallback | EditMode test: every character in the VI files is in the VI atlases; TMP logs no missing glyphs |
| 10 | `tools/lang/check.py`: missing, stale (`en` field), unused, placeholder mismatch, non-NFC | Runs clean on the repo; fails on a planted bad entry |
| 11 | `IVolumes` + FMOD implementation, applied after banks load | Manual: sliders change music and SFX on web; 0 is silent |
| 12 | `Haptics` with `.jslib` and mobile paths, `Supported` flag | Manual: pulses on Android Chrome; row hidden on desktop and iOS Safari |
| 13 | Settings panel on the title, Settings button, Reset moved into the panel, relabel on language change | webtest: open Settings, switch to VI and back, screenshots, 0 console errors; Begin and Journal still at the same place |
| 14 | Vietnamese translation of `ui.json` and `content.json` | `check.py` shows 0 missing; owner reads every screen in VI |
| 15 | Release web build size and start time before and after | Size and first-frame time recorded in the devlog |

## 7. Open questions for the owner

1. Who writes the Vietnamese text? Which voice (plain modern, or old-fashioned alchemist)?
2. Translate material, catalyst and place names, or keep some in English for flavour? Keep the "Athanor" logo as is?
3. Is a different pixel font in Vietnamese acceptable for now (option C), or must it look exactly like English (option A/B, more art work)?
4. Keep forced capitals in Vietnamese, or use normal case there (stacked marks on capitals are tall)?
5. Default language: follow the device/browser, or always English until chosen?
6. Numbers in Vietnamese: "1.500" or keep "1,500"?
7. Settings as a fourth title button, or a gear icon? Is moving "RESET PROGRESS" into Settings fine?
8. Default volumes (Music 7, Sound 8)? Is there an Ambience bus?
9. When should the device vibrate?
10. Will Settings also be needed inside a run (a pause menu) later, for mobile?
11. Should the game mute when the browser tab is hidden?

---

## 8. Decisions and first build (2026-11-13)

### 8.1 Owner decisions

| # | Decision |
|---:|---|
| 1 | Own JSON files: approved, but **exactly one file per language**: `Athanor/Assets/Resources/lang/en.json` and `lang/vi.json`. No ui/content split. |
| 2 | Vietnamese written now for UI strings, event names/texts/options/outcomes, catalyst and aspect texts, goal texts, depth names, the intro (9 storyboard lines) and ending, and the tutorial lines. |
| 3 | **Names of materials, catalysts and aspects stay English for now.** vi.json has **no** entries for them (`material.*.name`, `catalyst.*.name`, `aspect.*.name`); the lookup falls back to content.json. A test keeps it that way. Event names are translated. |
| 4 | The last Keeper is called "The Keeper" (unnamed). |
| 5 | Fonts: the owner added `SVN-Determination Sans` and `SVN-Coder's Crux` (results in 8.2). |

### 8.2 Fonts: SVN-Determination Sans and SVN-Coder's Crux

Read from the TTF tables with a small Python reader (no fontTools): `cmap` (formats 0/4/6/12), `head`, `maxp`, `hhea`,
`OS/2`, `name`, and the `glyf` outlines to find the pixel grid.

| | SVN-Determination Sans | SVN-Coder's Crux |
|---|---|---|
| File | `Assets/Art/Fonts/SVN-Determination Sans.ttf` (101,748 B) | `Assets/Art/Fonts/SVN-Coder's Crux.ttf` (79,664 B) |
| Glyphs / mapped characters | 359 / 394 | 316 / 346 |
| Units per em | 1000 | 1024 |
| Pixel grid | 75 units = 1 px (every outline point is a multiple of 75) | 64 units = 1 px (a few glyphs sit on 1/8 px: `i j ' ‘ ’ “ ”`, `ã õ ĩ ũ`, the hook-above letters) |
| Em in pixels | 13⅓ px | 16 px |
| Cap height / x-height / descender | 9 / 7 / 3 px | 7 / 5 / 2 px |
| Tallest mark (Ấ, ẫ) | capitals 14 px, lower case 14 px above the baseline | capitals 10 px, lower case 8 px |
| Advance | proportional, mostly 7 px (3 to 9) | monospace 6.09 px (390 units; a few 4.1 px) |
| Line (hhea asc + desc + gap) | 675 + 150 + 500 = 17.7 px | 448 + 128 + 300 = 13.7 px |
| Vietnamese letters (134) | **134 of 134** | **134 of 134** |
| ASCII 32 to 126 | 95 of 95 | 95 of 95 |
| Punctuation “ ” ‘ ’ … — × · • « » ° | all | all |
| En dash – (U+2013) | yes | **no** |
| Combining marks U+0300 to U+036F | none (text must be NFC) | none (text must be NFC) |
| Width of "A cracked altar asks for an offering." | 231 px at 13⅓ (BitPotion 16: 172 px) | 226 px at 16 |
| Licence in the file | none. Copyright: "8-bit Operator (c) 2009 - 2014 Grand Chaos Productions. Based on spritesheet by Leafia_Barrett. Modified by theFIZZYnator. Undertale is copyright and intellectual property of Toby Fox." fsType 8 (editable embedding). Vietnamese version: "UKYO, 2022". | **CC BY 3.0** (name id 13/14), "Copyright NAL 2012", built with FontStruct (fontstruct.com/fontstructions/show/619715). Vietnamese version: "FSTR, 2020". Needs credit. |

Every character used in vi.json (and "Tiếng Việt") is in **both** fonts: 0 missing. `LangTests` checks this against the
measured set (ASCII + the 134 letters + `“”‘’…—×·•«»°`; the en dash is left out because Crux lacks it).

Findings to watch:

- **Licence risk, Determination Sans.** No licence string. Its base, 8-bit Operator, is a Grand Chaos font, and this one
  is an Undertale-style fan edit, then Vietnamised. Ask the owner to find the licence of the SVN release (and of
  theFIZZYnator's edit) before shipping. Crux is CC BY 3.0: credit "Coder's Crux by NAL" on the title (already in
  `ui.title.fonts` for vi).
- **Determination's em is 13⅓ px**, so no whole point size lands on its grid. Use `fontSize` 13.333 (1x), 26.667 (2x),
  40 (3x). TMP takes a float size. Bake the atlas at sampling size **40** (each font pixel = 3x3 atlas pixels, point
  filtered) so all three sizes are exact; baking at 13 or 14 rounds and distorts. A 40-pt atlas of about 230 characters
  needs 512x512.
- **Crux advances are 6.09 px**, not 6. Bake with `GlyphRenderMode.RASTER_HINTED` (FontBuilder uses `RASTER` today) so
  advances round to 6 px, and check spacing in a screenshot.
- **Crux stacked marks overhang** to the right: Ấ is 7 px wide in a 6 px cell, ẫ 9 px. Two stacked letters in a row may
  touch. Check "Tiếng Việt", "Bậc", "lượt" in a screenshot.
- **Both are wider than BitPotion** (Body today, 4.65 px a letter). With Crux as Body, 18 boxed texts gain a line
  (3 event texts in the 288 px event box; 15 catalyst/aspect texts in the 124 px card box). Layout task 7 must allow it.

Proposed roles in Vietnamese (`ArtLibrary` font set per language; "Athanor" logo stays Alagard in every language):

| FontKind | English today | Vietnamese | Sizes (on the grid) | Why |
|---|---|---|---|---|
| Title | Alagard 16 / 32 / 48 | SVN-Determination Sans | 13.333 / 26.667 / 40 (for 16 / 32 / 48) | Biggest, cap 9 px; stacked capitals still readable. Buttons: "Xuống lần nữa" is 84 px of 120. |
| Name | PixelOperator-Bold 16 | SVN-Determination Sans | 13.333 | Same 8-bit Operator family as Pixel Operator, so card names look close to today. |
| Body | BitPotion 16 | SVN-Coder's Crux | 16 | Narrowest of the two (6 px); lower-case stacked marks stay within 8 px, so 16 px line pitch holds. |
| Tiny | PixelOperator8 8 | SVN-Coder's Crux | 16 | 8 px cannot carry stacked marks (2.3 of section 2.4). Crux at 16 is cap 7 px; marks need 3 more px above caps, so the 10 px rows in `HudView` need 12 to 13 px. |

FontBuilder change (not done; Editor is held by other agents): bake per language, adding the characters of vi.json to
the ASCII set, and log what is missing.

### 8.3 File format and lookup

```json
{
  "language": "vi",
  "name": "Tiếng Việt",
  "entries": [
    {"key": "ui.title.begin", "text": "Bắt đầu", "en": "Begin"},
    {"key": "note.trials.other", "text": "{n:+} lượt", "en": "{n:+} trials"}
  ]
}
```

- One entry per line, so git diffs are per string. Read by `JsonUtility` (`LangFile`/`LangEntry` are `[Serializable]`
  with public fields), by `System.Text.Json` with `IncludeFields`, and by `Lang.Parse` (a small JSON reader in Core,
  since Core has no JSON library under Unity). A test checks all three agree.
- `en.json` holds UI strings only (234 entries, no `en` field). Content English stays in `content.json`.
- `vi.json` holds UI + content (384 entries: 224 UI, 160 content). Every entry records its English in `en`.
- Lookup (`Lang`): this language, then English, then the text the caller supplies (`Content(key, english)`, for
  content.json fields), then the key itself. An empty text counts as missing.
- Placeholders `{name}`, `{n:+}` (sign on positives), `{n:N0}` (any .NET format, invariant culture).
- Plurals: `key.one` / `key.other`; `Plural(key, n)` fills `{n}`. vi has only `.other`; `Missing()` counts `.other` as
  covering `.one`.
- `Missing(keys)` and `Stale(english)` (the `en` field differs from the current English) are for the check script/tests.
- `Lang.ContentEnglish(ContentData)` builds the content keys: `material.<id>.name`, `catalyst.<id>.name/.text`,
  `aspect.<id>.name/.text`, `goal.<id>.text`, `event.<id>.name/.text`, `event.<id>.opt.<n>.label`,
  `event.<id>.opt.<n>.out.<m>` (indexes, 0-based, as the owner's brief; option ids can come later), `depth.<n>.name`
  (from 1).
- Other key groups: `ui.<screen>.<item>`, `ui.settings.*`, `cutscene.*`, `story.intro.1..9`, `story.ending.1..3`,
  `tutorial.<step>` (`tutorial.1_6b`; plus `tutorial.step`, `tutorial.part.*` for the score popups), `effect.*`,
  `note.*` (Core event notes), `kind.<id>` and `kind.<id>.indef` (for the hint line), `rarity.<id>`, `event_type.<id>`.
- New templates fix two English bugs from 1.3: `note.lost` is "Lost {item} for this run" (no "a"), and
  `ui.runover.goal_count.one` is "{n} GOAL".
- `ui.aspect.cost_marker` ("COST:" / "GIÁ:") lets `Wording.AspectCost` split Vietnamese aspect text until the `cost`
  field of task 6 exists.

Tests (`Tests/EditMode/LangTests.cs`, 21 tests, run by `dotnet test tools/CoreTests`; the csproj copies `lang/*.json`
to `bin/.../lang/`): both files parse (and agree with System.Text.Json); no duplicate keys; no empty text; en.json has no
content keys; every vi key is an English or content key; vi has every UI key and every content text but no item names;
nothing stale; placeholders match the English; every vi character is in both fonts; text is NFC; plus unit tests of
`Fill`, fallback, plurals, `Missing`, `Stale`, the parser and the content key scheme.

### 8.4 Glossary (English to Vietnamese)

| English | Vietnamese | Notes |
|---|---|---|
| Trial (HUD "TRIALS") | lượt ("LƯỢT") | Short; "lượt thử" is clearer but long. "Out of trials" = "Hết lượt". |
| Potency | dược lực | Noun on the HUD and in rules. The verb "score" is "được điểm" ("score double" = "được điểm gấp đôi"). |
| Reaction | phản ứng | |
| Reaction Index | Chỉ số Phản ứng | Journal's "REACTIVITY" (same number) is "CHỈ SỐ". |
| Chain / chain step | chuỗi / bậc chuỗi | "CHAIN x2" = "CHUỖI x2". Tutorial "Step 2" = "Bước 2". |
| Combo | combo | Kept: "COMBO 4", "Tạo combo 4". |
| Catalyst | xúc tác | "Chất xúc tác" is too long. Catalyst **names stay English**. |
| Aspect / trade-off aspect | đặc tính / đặc tính đánh đổi | Aspect names stay English. COST: = "GIÁ:". |
| Ignite | Đốt lửa | Button at Title 32: 94 px. Alternative "Đốt" if it feels long. |
| Redraw | đổi bài | HUD "ĐỔI BÀI"; "+1 đổi bài". |
| Card (material in hand) | lá | "lose a card" = "mất một lá". |
| Material / starter | nguyên liệu / khởi đầu | "STARTER" = "KHỞI ĐẦU". |
| Discovery | phát hiện | "New Discovery" = "Phát Hiện Mới". |
| Hint | gợi ý | |
| Rack | giá | "RACK FULL" = "GIÁ ĐẦY". |
| Bench / tile / hub | bàn chế / ô / tâm | "centre tile" = "ô giữa"; "diagonal" = "kề chéo". |
| Heat / tier | nhiệt / bậc | |
| Satchel | túi đồ | |
| Depth | tầng | "Depth Cleared" = "Qua Tầng". |
| Run / descent | chuyến | "Start 3 runs" = "Bắt đầu 3 chuyến"; "this descent" = "chuyến này". |
| Goal / target (score) | mục tiêu / chỉ tiêu | Kept apart: "NO TARGET" = "KHÔNG CHỈ TIÊU". |
| Journal (UI) | Sổ tay | |
| the Book (lore) | Cuốn Sổ | Ties to "Sổ tay". "An Old Ledger" became "Bản Ghi Cũ" so "Sổ" means only the Book. |
| Keeper / the last Keeper | Thủ Hầm / Thủ Hầm cuối cùng | "Keeper of the cellar". Alternatives: "Người Giữ Hầm", "Người Giữ". |
| Worldseed | Worldseed | A material name: stays English. The ending says "hạt mầm" (a seed). |
| The Great Work / The Fire Dies | Đại Công Trình / Lửa Tàn | |
| Experiment {n} | Thí nghiệm {n} | |
| Kinds | Quặng, Thảo mộc, Dược thủy, Thú, Tinh thể, Kim loại, Bột | Ore, Plant, Potion, Beast, Crystal, Metal, Powder |
| Rarities | Thường, Ít gặp, Hiếm, Huyền thoại | Common, Uncommon, Rare, Legendary |
| Event types | Lựa chọn, Canh bạc, Nhặt được, Miếu thờ, Hiểm họa, Phúc lành, Chuyện xưa | Choice, Gamble, Find, Shrine, Hazard, Boon, Lore |
| Depth names | Hầm Nông, Bậc Thang Rỉ Nước, Bóng Tối Rì Rầm, Cội Đá, Đáy Lặng | The Shallow Cellar ... The Quiet Below |
| Click | bấm | "CLICK TO BEGIN" = "BẤM ĐỂ BẮT ĐẦU". |

Voice: plain modern Vietnamese, "bạn" for the player, short clauses. Forced capitals kept as in English.

### 8.5 Length check

All 16 event texts are at most 56 letters (longest: `sealed_shrine` 55). Lines more than 20% longer than the English
(and 3+ letters longer) are mostly one-word labels: "Quay lại" (Back), "Mục tiêu" (Goals), "Âm thanh" (Sound),
"KHÔNG CHỈ TIÊU" (NO TARGET, 14 vs 9), "Trang chủ" (Title), event types ("Chuyện xưa" for Lore), kinds ("Dược thủy").
Longer sentences: `ui.hud.hint_kind` +44% ("{item} xao động gần một loại {kind}."), `story.intro.9` +22%,
`event.old_ledger.text` +24%, `event.damp_air` outcome +26%, `goal.g_big1500` +24%, `goal.g_win1` +56%.
The bigger change is the font width (8.2): 18 boxed texts gain a line in the proposed fonts.

### 8.6 Lore rewrites, translated ahead (not in vi.json yet)

vi.json translates the **current** content.json, so nothing is stale today. When the lore pass of
`phase-4-lore.md` lands in content.json, `Vietnamese_IsNotStale` will list exactly the changed keys; replace them with
these (all within the lore limits: name 22, text 56, outcome 52). Unchanged rows keep the current translation.

| id | Name | Text | Outcomes |
|---|---|---|---|
| cracked_altar | Bệ Thờ Thủ Hầm | Một bệ thờ nứt dâng các Thủ Hầm đòi một lễ vật. | (same) |
| tilted_workbench | Bàn Chế Cũ | Bàn chế của một Thủ Hầm khác. Nghiêng, nhưng rộng. | (same) |
| old_ledger | Bản Ghi Thủ Hầm | Bản ghi ẩm chép những gì một Thủ Hầm khác đã thử. | (same) |
| hungry_furnace | Lò Của Thủ Hầm | Một lò cũ há miệng, sau ngần ấy năm vẫn còn ấm. | (same) |
| shifting_bench | Bàn Chế Xê Dịch | Rễ cựa dưới sàn. Bàn chế nghiến kèn kẹt. | Một ô yên vị trên rễ ấm. / (same) |
| smugglers_crate | Thùng Bị Vùi | Một thùng hàng, bị rễ nuốt mất một nửa. | (same) |
| forgotten_satchel | Túi Đồ Bỏ Quên | Một túi đồ như của bạn, bỏ lại trên bậc thang. | (same) |
| glinting_seam | Mạch Đá Lấp Lánh | Một mẻ thuốc cũ đã đông trong vách như quặng. | (same) |
| sealed_shrine | Miếu Niêm Phong | Miếu thờ Thủ Hầm đầu tiên ngân lên trước xúc tác. | (same) |
| damp_air | Hơi Ẩm | Nước rỉ xuống từ bậc thang. Mọi thứ nguội đi. | (same) |
| falling_grit | Sạn Rơi | Sạn rơi từ đám rễ xuống bàn chế. | (same) |
| cold_draft | Gió Lùa Lạnh | Gió lạnh lùa lên bậc thang, qua túi áo bạn. | (same) |
| warm_stone | Hòn Đá Ấm | Một hòn đá trong vách đang ấm. Có gì lớn sau nó. | (same) |
| quiet_room | Phòng Của Thủ Hầm | Giường, cốc, một cái tên bị gạch. Bụi xoay chậm. | (same) |
| names_in_wall (new) | Tên Trên Vách | Những cái tên khắc vào vách, tên này dưới tên kia. | Tên cuối cùng còn mới. Bên dưới vẫn còn chỗ. / Go on = Đi tiếp |
| warm_roots (new) | Rễ Ấm | Rễ đâm xuyên trần. Chúng ấm, và ngân nga. | Bạn nghe tiếng ngân trong chân răng. |
| pinned_note (new) | Mảnh Giấy Ghim | Giấy ghim trên bàn: 'Gieo nó. Đừng leo lên lại.' | Bạn gấp nó cất đi. |

### 8.7 Translation choices for the owner to review

1. **Keeper = "Thủ Hầm"** (keeper of the cellar). Or "Người Giữ Hầm" (plainer, longer) / "Người Giữ".
2. **Journal = "Sổ tay"** in the UI and **the Book = "Cuốn Sổ"** in the story. Or "Cuốn Sách" for the Book (loses the link to the Journal button).
3. **Potency = "dược lực"** and "score" = "được điểm". Or "hiệu lực" / "uy lực" / plain "điểm".
4. **Trial = "lượt"** (not "lượt thử"). Redraw = "đổi bài", card = "lá".
5. **Ignite = "Đốt lửa"** (94 px at Title 32). Or "Nhóm lửa" (wider) / "Đốt".
6. **Catalyst = "xúc tác"**, aspect = "đặc tính".
7. **Run = "chuyến"** ("chuyến này" = this descent).
8. Rarity "Uncommon" = **"Ít gặp"**; event type "Find" = **"Nhặt được"**, "Lore" = **"Chuyện xưa"**.
9. Numbers: goal texts use **"1.500"** and **"1.000"** (Vietnamese style). Code-formatted numbers (`Wording.Number`) still print "1,500"; owner question 6.
10. Title subtitle "An alchemist's descent" = **"Nhà giả kim xuống hầm sâu"**. "The Great Work" = **"Đại Công Trình"**.
11. Tutorial and story lines keep material names in English inside the text ("Bấm Ember Coal..."). If names are translated later, these lines change too.
12. The attuned aspect text says "loại đã hòa hợp" (the attuned kind) literally: `Wording.AspectRule` replaces only the English phrase. Task 6 (`{kind}` placeholder) fixes both languages.
13. The hint line becomes "{item} xao động gần một loại {kind}." with `kind.<id>.indef` = the lower-case kind ("quặng"), replacing the English a/an code.
14. Forced capitals are kept in Vietnamese ("BẤM ĐỂ BẮT ĐẦU"); Crux capitals with stacked marks reach 10 px (owner question 4).
15. The credit line for vi lists "SVN-DETERMINATION SANS, SVN-CODER'S CRUX (NAL)"; check the wording the licences require.
