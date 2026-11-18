---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Support/Loc.cs
tags: [code/game, kind/class]
aliases: []
---

# Loc

`public static class Loc`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Support/Loc.cs:12-120`

> The language the game is showing: the loaded Lang (Vietnamese falls back to English), the font set that goes with it and helpers for the words views share. Views never hold text; they ask for a key. English until a language is chosen, so tests and the editor are deterministic; InitFromPlayer picks the player's language at boot.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string English = "en", Vietnamese =` | 14 |  |
| field | `public static event Action Changed` | 19 | Raised after the language changed, so labels can rewrite themselves and swap fonts. |
| property | `public static string Code` | 22 | "en" or "vi". |
| property | `public static bool Vi` | 23 |  |
| property | `public static Lang Current` | 24 |  |
| property | `public static string NativeName` | 26 | The language's own name for the Settings panel ("English", "Tiếng Việt"). |
| method | `public static void Use(TextAsset english, TextAsset vietnamese)` | 34 | Gives Loc its language files. GameFlow calls it first thing; the Game tests and editor tools call it through EditorGlobals. Call it before any Label exists: it does not raise Changed, so existing labels keep their old text. |
| method | `public static void InitFromPlayer(string saved)` | 59 | The language to start in: a "?lang=" in the page address (for checking), else the saved choice, else the device language. |
| method | `public static string FromUrl(string url)` | 68 | The value of "lang=" in an address when it names a language we have, else null. |
| method | `public static void Set(string c)` | 77 |  |
| method | `public static string T(string key, params (string name, object value)[] args)` | 90 | The text of a key with named placeholders filled; the key itself when no language has it. |
| method | `public static string P(string key, long n, params (string name, object value)[] args)` | 93 | key.one / key.other by the number; {n} is filled. |
| method | `public static string C(string key, string english, params (string name, object value)[] args)` | 96 | Content text (events, goals, depth names, catalyst and aspect texts): this language, else the content.json English. |
| method | `public static bool Has(string key)` | 98 |  |
| method | `public static string Kind(string kind)` | 101 | "Ore" or "Quặng" for a material kind from content.json ("Ore", "ore"). |
| method | `public static string Indef(string kind)` | 105 | "an ore" or "quặng": the kind inside a sentence. |
| method | `public static string Rarity(string rarity)` | 109 | "Rare" or "Hiếm" for a rarity from content.json. |
| method | `public static string Upper(string s)` | 112 | Capitals as the screens write them. Vietnamese keeps its marks (ế becomes Ế). |
| method | `public static string DepthName(ContentDb db, int depth)` | 115 | The depth's name in this language. |
| method | `public static string Number(long n)` | 118 | 1,500 in English, 1.500 in Vietnamese. |

## Uses
[[ContentDb]], [[Kind]], [[Lang]], [[Rarities]]

## Used by
[[BenchView]], [[CardView]], [[CatalystChoiceView]], [[EditorGlobals]], [[EventView]], [[GameFlow]], [[GoalToast]], [[GoalsView]], [[HudView]], [[JournalView]], [[Label]], [[Make]], [[PauseMenu]], [[ReactionCutscene]], [[RunOverView]], [[SimpleCutscene]], [[TitleView]], [[TutorialController]], [[ViewBaker]], [[Wording]]

## Tested by
[[LabelLineBoxTests]], [[LocTests]], [[PrefabBakeTests]], [[RunOverDepthTests]]
