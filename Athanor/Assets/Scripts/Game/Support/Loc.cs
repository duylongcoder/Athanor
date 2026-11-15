using System;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The language the game is showing: the loaded <see cref="Lang"/> (Vietnamese falls back to English), the font set that goes
    /// with it and helpers for the words views share. Views never hold text; they ask for a key. English until a language is chosen,
    /// so tests and the editor are deterministic; <see cref="InitFromPlayer"/> picks the player's language at boot.
    /// </summary>
    public static class Loc
    {
        public const string English = "en", Vietnamese = "vi";
        static Lang en, current;
        static string code = English;

        /// <summary>Raised after the language changed, so labels can rewrite themselves and swap fonts.</summary>
        public static event Action Changed;

        /// <summary>"en" or "vi".</summary>
        public static string Code { get { Ensure(); return code; } }
        public static bool Vi => Code == Vietnamese;
        public static Lang Current { get { Ensure(); return current; } }
        /// <summary>The language's own name for the Settings panel ("English", "Tiếng Việt").</summary>
        public static string NativeName => Current.Name;

        static Lang Load(string c, Lang fallback)
        {
            var asset = Resources.Load<TextAsset>("lang/" + c);
            if (asset == null) { Debug.LogError("Loc: no language file " + c); return new Lang(new LangFile { language = c, name = c, entries = new LangEntry[0] }, fallback); }
            return Lang.FromJson(asset.text, fallback);
        }

        static void Ensure()
        {
            if (current != null) return;
            en = Load(English, null);
            current = en; code = English;
        }

        /// <summary>
        /// The language to start in: a "?lang=" in the page address (for checking), else the saved choice, else the device language.
        /// </summary>
        public static void InitFromPlayer(string saved)
        {
            string chosen = FromUrl(Application.absoluteURL);
            if (chosen == null && (saved == English || saved == Vietnamese)) chosen = saved;
            if (chosen == null) chosen = Application.systemLanguage == SystemLanguage.Vietnamese ? Vietnamese : English;
            Set(chosen);
        }

        /// <summary>The value of "lang=" in an address when it names a language we have, else null.</summary>
        public static string FromUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;
            int at = url.IndexOf("lang=", StringComparison.Ordinal);
            if (at < 0) return null;
            string v = url.Substring(at + 5);
            return v.StartsWith(Vietnamese, StringComparison.Ordinal) ? Vietnamese : v.StartsWith(English, StringComparison.Ordinal) ? English : null;
        }

        public static void Set(string c)
        {
            Ensure();
            if (c != English && c != Vietnamese) c = English;
            if (c == code) return;
            code = c;
            current = c == English ? en : Load(c, en);
            Changed?.Invoke();
        }

        // ---- lookups ----

        /// <summary>The text of a key with named placeholders filled; the key itself when no language has it.</summary>
        public static string T(string key, params (string name, object value)[] args) => Current.Get(key, args);

        /// <summary>key.one / key.other by the number; {n} is filled.</summary>
        public static string P(string key, long n, params (string name, object value)[] args) => Current.Plural(key, n, args);

        /// <summary>Content text (events, goals, depth names, catalyst and aspect texts): this language, else the content.json English.</summary>
        public static string C(string key, string english, params (string name, object value)[] args) => Current.Content(key, english, args);

        public static bool Has(string key) => Current.Template(key) != null;

        /// <summary>"Ore" or "Quặng" for a material kind from content.json ("Ore", "ore").</summary>
        public static string Kind(string kind) =>
            string.IsNullOrEmpty(kind) ? "" : Current.Content("kind." + kind.ToLowerInvariant(), kind);

        /// <summary>"an ore" or "quặng": the kind inside a sentence.</summary>
        public static string Indef(string kind) =>
            string.IsNullOrEmpty(kind) ? "" : Current.Content("kind." + kind.ToLowerInvariant() + ".indef", kind.ToLowerInvariant());

        /// <summary>"Rare" or "Hiếm" for a rarity from content.json.</summary>
        public static string Rarity(string rarity) => Current.Content("rarity." + Rarities.Of(rarity).ToLowerInvariant(), Rarities.Of(rarity));

        /// <summary>Capitals as the screens write them. Vietnamese keeps its marks (ế becomes Ế).</summary>
        public static string Upper(string s) => s == null ? null : s.ToUpperInvariant();

        /// <summary>The depth's name in this language.</summary>
        public static string DepthName(ContentDb db, int depth) => C(Lang.DepthKey(depth), db.DepthName(depth));

        /// <summary>1,500 in English, 1.500 in Vietnamese.</summary>
        public static string Number(long n) =>
            Vi ? n.ToString("N0", System.Globalization.CultureInfo.InvariantCulture).Replace(',', '.') : n.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
    }
}
