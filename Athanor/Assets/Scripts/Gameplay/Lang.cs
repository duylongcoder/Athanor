using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Athanor.Gameplay
{
    /// <summary>One string of a language file. en is the English it was translated from (empty in the English file).</summary>
    [Serializable] public class LangEntry { public string key, text, en; }

    /// <summary>A language file (Resources/lang/&lt;code&gt;.json). A list, not a dictionary, so JsonUtility can read it too.</summary>
    [Serializable] public class LangFile { public string language, name; public LangEntry[] entries; }

    /// <summary>
    /// The player-facing text of one language, Unity-free. Lookup order: this language, then the fallback (English),
    /// then the text the caller supplies (the content assets' English for content keys), then the key itself.
    /// Placeholders are named: {item}, {n}, {n:+} (always signed), {n:N0}. Plurals are two keys, key.one (|n| = 1) and
    /// key.other; a language without .one uses .other for every number. See docs/ATHANOR.md §3.
    /// </summary>
    public sealed class Lang
    {
        public readonly string Language, Name;
        public readonly Lang Fallback;
        readonly Dictionary<string, string> text = new Dictionary<string, string>();
        readonly Dictionary<string, string> source = new Dictionary<string, string>();

        public Lang(LangFile file, Lang fallback = null)
        {
            Language = file?.language ?? "";
            Name = file?.name ?? Language;
            Fallback = fallback;
            foreach (var e in file?.entries ?? new LangEntry[0])
            {
                if (string.IsNullOrEmpty(e?.key)) continue;
                text[e.key] = e.text ?? "";
                if (!string.IsNullOrEmpty(e.en)) source[e.key] = e.en;
            }
        }

        public static Lang FromJson(string json, Lang fallback = null) => new Lang(Parse(json), fallback);

        public IEnumerable<string> Keys => text.Keys;
        public int Count => text.Count;

        /// <summary>True when this language itself (not its fallback) has the key.</summary>
        public bool Has(string key) => key != null && text.ContainsKey(key);

        /// <summary>The template for a key through the chain, or null when no language has it.</summary>
        public string Template(string key)
        {
            for (var l = this; l != null; l = l.Fallback)
                if (key != null && l.text.TryGetValue(key, out var t) && t.Length > 0) return t;
            return null;
        }

        /// <summary>The text for a key with its placeholders filled. Missing everywhere: the key itself.</summary>
        public string Get(string key, params (string name, object value)[] args) => Fill(Template(key) ?? key, args);

        /// <summary>Content text: this language, English UI, then the given English (from the content assets), then the key.</summary>
        public string Content(string key, string english, params (string name, object value)[] args) =>
            Fill(Template(key) ?? (string.IsNullOrEmpty(english) ? key : english), args);

        /// <summary>key.one when |n| = 1 and the language has it, else key.other. {n} is filled with n.</summary>
        public string Plural(string key, long n, params (string name, object value)[] args)
        {
            string t = null;
            for (var l = this; l != null && t == null; l = l.Fallback)
            {
                if (Math.Abs(n) == 1 && l.text.TryGetValue(key + ".one", out var one) && one.Length > 0) t = one;
                else if (l.text.TryGetValue(key + ".other", out var other) && other.Length > 0) t = other;
            }
            var all = new (string, object)[args.Length + 1];
            all[0] = ("n", n);
            Array.Copy(args, 0, all, 1, args.Length);
            return Fill(t ?? key + ".other", all);
        }

        /// <summary>Fills {name} and {name:format} from args. {n:+} writes the sign of a positive number. Unknown names stay.</summary>
        public static string Fill(string template, params (string name, object value)[] args)
        {
            if (string.IsNullOrEmpty(template) || args == null || args.Length == 0 || template.IndexOf('{') < 0) return template;
            var sb = new StringBuilder(template.Length + 16);
            int i = 0;
            while (i < template.Length)
            {
                char c = template[i];
                int close = c == '{' ? template.IndexOf('}', i + 1) : -1;
                if (close < 0) { sb.Append(c); i++; continue; }
                string inner = template.Substring(i + 1, close - i - 1);
                int colon = inner.IndexOf(':');
                string name = colon < 0 ? inner : inner.Substring(0, colon), format = colon < 0 ? null : inner.Substring(colon + 1);
                bool found = false;
                foreach (var a in args)
                {
                    if (a.name != name) continue;
                    sb.Append(Format(a.value, format)); found = true; break;
                }
                if (!found) sb.Append('{').Append(inner).Append('}');
                i = close + 1;
            }
            return sb.ToString();
        }

        static string Format(object value, string format)
        {
            if (format == "+")
            {
                string s = Convert.ToString(value, CultureInfo.InvariantCulture);
                return value is IConvertible && double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var d) && d > 0 ? "+" + s : s;
            }
            if (format != null && value is IFormattable f) return f.ToString(format, CultureInfo.InvariantCulture);
            return Convert.ToString(value, CultureInfo.InvariantCulture) ?? "";
        }

        /// <summary>The placeholders of a template, as written ("n:+", "item"), in order.</summary>
        public static List<string> Placeholders(string template)
        {
            var list = new List<string>();
            if (string.IsNullOrEmpty(template)) return list;
            for (int i = template.IndexOf('{'); i >= 0; i = template.IndexOf('{', i + 1))
            {
                int close = template.IndexOf('}', i + 1);
                if (close < 0) break;
                list.Add(template.Substring(i + 1, close - i - 1));
            }
            return list;
        }

        /// <summary>
        /// Keys of the given English set this language does not have. A plural .one counts as present when .other is,
        /// since a language with one form only writes .other.
        /// </summary>
        public List<string> Missing(IEnumerable<string> englishKeys)
        {
            var list = new List<string>();
            foreach (var k in englishKeys)
            {
                if (Has(k)) continue;
                if (k.EndsWith(".one", StringComparison.Ordinal) && Has(k.Substring(0, k.Length - 4) + ".other")) continue;
                list.Add(k);
            }
            return list;
        }

        /// <summary>Keys whose recorded English (the en field) differs from the current English: the translation is out of date.</summary>
        public List<string> Stale(IDictionary<string, string> english)
        {
            var list = new List<string>();
            foreach (var kv in source)
                if (english.TryGetValue(kv.Key, out var now) && now != kv.Value) list.Add(kv.Key);
            return list;
        }

        /// <summary>The English this language's entry says it was translated from, or null.</summary>
        public string SourceOf(string key) => key != null && source.TryGetValue(key, out var s) ? s : null;

        // ---- content keys ----

        /// <summary>
        /// Every the content assets string by key, in English: material/catalyst/aspect names and texts, goal texts, event names,
        /// texts, option labels and outcomes (by index), depth names (from 1).
        /// </summary>
        public static Dictionary<string, string> ContentEnglish(ContentData d)
        {
            var map = new Dictionary<string, string>();
            void Add(string key, string value) { if (!string.IsNullOrEmpty(value)) map[key] = value; }
            foreach (var m in d.materials ?? new MaterialData[0]) Add("material." + m.id + ".name", m.name);
            foreach (var c in d.catalysts ?? new CatalystData[0]) { Add("catalyst." + c.id + ".name", c.name); Add("catalyst." + c.id + ".text", c.text); }
            foreach (var a in d.aspects ?? new AspectData[0]) { Add("aspect." + a.id + ".name", a.name); Add("aspect." + a.id + ".text", a.text); }
            foreach (var g in d.goals ?? new GoalData[0]) Add("goal." + g.id + ".text", g.text);
            foreach (var e in d.events ?? new EventData[0])
            {
                Add(EventKey(e.id, "name"), e.name); Add(EventKey(e.id, "text"), e.text);
                for (int i = 0; i < (e.options?.Length ?? 0); i++)
                {
                    var o = e.options[i];
                    Add(OptionKey(e.id, i), o.label);
                    for (int j = 0; j < (o.outcomes?.Length ?? 0); j++) Add(OutcomeKey(e.id, i, j), o.outcomes[j].text);
                }
            }
            for (int i = 0; i < (d.depthNames?.Length ?? 0); i++) Add(DepthKey(i + 1), d.depthNames[i]);
            return map;
        }

        public static string EventKey(string id, string field) => "event." + id + "." + field;
        public static string OptionKey(string id, int option) => "event." + id + ".opt." + option + ".label";
        public static string OutcomeKey(string id, int option, int outcome) => "event." + id + ".opt." + option + ".out." + outcome;
        public static string DepthKey(int depth) => "depth." + depth + ".name";

        // ---- a small JSON reader, enough for language files (Core has no JSON library under Unity) ----

        /// <summary>Reads a language file. Unknown fields are ignored; a broken file throws FormatException.</summary>
        public static LangFile Parse(string json)
        {
            var root = new Json(json).Value() as Dictionary<string, object>;
            if (root == null) throw new FormatException("language file: not an object");
            var file = new LangFile { language = root.TryGetValue("language", out var l) ? l as string : null, name = root.TryGetValue("name", out var n) ? n as string : null };
            var list = new List<LangEntry>();
            if (root.TryGetValue("entries", out var es) && es is List<object> arr)
                foreach (var o in arr)
                    if (o is Dictionary<string, object> e)
                        list.Add(new LangEntry
                        {
                            key = e.TryGetValue("key", out var k) ? k as string : null,
                            text = e.TryGetValue("text", out var t) ? t as string : null,
                            en = e.TryGetValue("en", out var s) ? s as string : null,
                        });
            file.entries = list.ToArray();
            return file;
        }

        sealed class Json
        {
            readonly string s; int i;
            public Json(string text) { s = text ?? ""; if (s.Length > 0 && s[0] == '﻿') i = 1; }

            public object Value()
            {
                Skip();
                if (i >= s.Length) throw Error("unexpected end");
                char c = s[i];
                if (c == '{') return Obj();
                if (c == '[') return Arr();
                if (c == '"') return Str();
                if (Word("true")) return true;
                if (Word("false")) return false;
                if (Word("null")) return null;
                return Num();
            }

            Dictionary<string, object> Obj()
            {
                var d = new Dictionary<string, object>(); i++;
                Skip(); if (Peek('}')) { i++; return d; }
                while (true)
                {
                    Skip(); if (!Peek('"')) throw Error("expected a name");
                    string k = Str(); Skip();
                    if (!Peek(':')) throw Error("expected ':'"); i++;
                    d[k] = Value(); Skip();
                    if (Peek(',')) { i++; continue; }
                    if (Peek('}')) { i++; return d; }
                    throw Error("expected ',' or '}'");
                }
            }

            List<object> Arr()
            {
                var a = new List<object>(); i++;
                Skip(); if (Peek(']')) { i++; return a; }
                while (true)
                {
                    a.Add(Value()); Skip();
                    if (Peek(',')) { i++; continue; }
                    if (Peek(']')) { i++; return a; }
                    throw Error("expected ',' or ']'");
                }
            }

            string Str()
            {
                var sb = new StringBuilder(); i++;
                while (i < s.Length)
                {
                    char c = s[i++];
                    if (c == '"') return sb.ToString();
                    if (c != '\\') { sb.Append(c); continue; }
                    if (i >= s.Length) break;
                    char e = s[i++];
                    switch (e)
                    {
                        case 'n': sb.Append('\n'); break;
                        case 't': sb.Append('\t'); break;
                        case 'r': sb.Append('\r'); break;
                        case 'b': sb.Append('\b'); break;
                        case 'f': sb.Append('\f'); break;
                        case 'u':
                            if (i + 4 > s.Length) throw Error("bad \\u escape");
                            sb.Append((char)int.Parse(s.Substring(i, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture)); i += 4; break;
                        default: sb.Append(e); break;
                    }
                }
                throw Error("unterminated string");
            }

            object Num()
            {
                int start = i;
                while (i < s.Length && "+-0123456789.eE".IndexOf(s[i]) >= 0) i++;
                if (i == start) throw Error("unexpected '" + s[i] + "'");
                return double.Parse(s.Substring(start, i - start), NumberStyles.Float, CultureInfo.InvariantCulture);
            }

            bool Word(string w)
            {
                if (string.CompareOrdinal(s, i, w, 0, w.Length) != 0) return false;
                i += w.Length; return true;
            }

            bool Peek(char c) => i < s.Length && s[i] == c;
            void Skip() { while (i < s.Length && char.IsWhiteSpace(s[i])) i++; }
            FormatException Error(string what) => new FormatException("language file, char " + i + ": " + what);
        }
    }
}
