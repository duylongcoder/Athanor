---
generated: true
type: code
layer: Gameplay
kind: class
namespace: Athanor.Gameplay
file: Athanor/Assets/Scripts/Gameplay/Lang.cs
tags: [code/gameplay, kind/class]
aliases: []
---

# Lang

`public sealed class Lang`
- Layer: **Gameplay** · Source: `Athanor/Assets/Scripts/Gameplay/Lang.cs:20-304`

> The player-facing text of one language, Unity-free. Lookup order: this language, then the fallback (English), then the text the caller supplies (the content assets' English for content keys), then the key itself. Placeholders are named: {item}, {n}, {n:+} (always signed), {n:N0}. Plurals are two keys, key.one (|n| = 1) and key.other; a language without .one uses .other for every number. See docs/ATHANOR.md §3.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public readonly string Language, Name` | 22 |  |
| field | `public readonly Lang Fallback` | 23 |  |
| ctor | `public Lang(LangFile file, Lang fallback = null)` | 27 |  |
| method | `public static Lang FromJson(string json, Lang fallback = null)` | 40 |  |
| property | `public IEnumerable<string> Keys` | 42 |  |
| property | `public int Count` | 43 |  |
| method | `public bool Has(string key)` | 46 | True when this language itself (not its fallback) has the key. |
| method | `public string Template(string key)` | 49 | The template for a key through the chain, or null when no language has it. |
| method | `public string Get(string key, params (string name, object value)[] args)` | 57 | The text for a key with its placeholders filled. Missing everywhere: the key itself. |
| method | `public string Content(string key, string english, params (string name, object value)[] args)` | 60 | Content text: this language, English UI, then the given English (from the content assets), then the key. |
| method | `public string Plural(string key, long n, params (string name, object value)[] args)` | 64 | key.one when /n/ = 1 and the language has it, else key.other. {n} is filled with n. |
| method | `public static string Fill(string template, params (string name, object value)[] args)` | 79 | Fills {name} and {name:format} from args. {n:+} writes the sign of a positive number. Unknown names stay. |
| method | `public static List<string> Placeholders(string template)` | 116 | The placeholders of a template, as written ("n:+", "item"), in order. |
| method | `public List<string> Missing(IEnumerable<string> englishKeys)` | 133 | Keys of the given English set this language does not have. A plural .one counts as present when .other is, since a language with one form only writes .other. |
| method | `public List<string> Stale(IDictionary<string, string> english)` | 146 | Keys whose recorded English (the en field) differs from the current English: the translation is out of date. |
| method | `public string SourceOf(string key)` | 155 | The English this language's entry says it was translated from, or null. |
| method | `public static Dictionary<string, string> ContentEnglish(ContentData d)` | 163 | Every the content assets string by key, in English: material/catalyst/aspect names and texts, goal texts, event names, texts, option labels and outcomes (by index), depth names (from 1). |
| method | `public static string EventKey(string id, string field)` | 185 |  |
| method | `public static string OptionKey(string id, int option)` | 186 |  |
| method | `public static string OutcomeKey(string id, int option, int outcome)` | 187 |  |
| method | `public static string DepthKey(int depth)` | 188 |  |
| method | `public static LangFile Parse(string json)` | 193 | Reads a language file. Unknown fields are ignored; a broken file throws FormatException. |

## Uses
[[ContentData]]

## Used by
[[EventView]], [[FontBuilder]], [[Loc]], [[Wording]]

## Tested by
[[LangTests]], [[LocTests]]
