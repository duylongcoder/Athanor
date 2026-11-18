using System;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>Loads the real language files (Resources/lang/en.json, vi.json), inside Unity or under plain dotnet.</summary>
    public static class TestLang
    {
        public static string Text(string code)
        {
#if UNITY_5_3_OR_NEWER
            return UnityEngine.Resources.Load<UnityEngine.TextAsset>("lang/" + code).text;
#else
            return System.IO.File.ReadAllText(System.IO.Path.Combine(System.AppContext.BaseDirectory, "lang", code + ".json"));
#endif
        }
    }

    public class LangTests
    {
        /// <summary>
        /// Characters the Vietnamese font draws (SVN-Coder's Crux only; Determination Sans was removed at Gate G1), measured
        /// from its cmap table (re-measured 2026-11-15, same set): printable ASCII, the 134 precomposed Vietnamese letters, and the punctuation below.
        /// Crux has no en dash (U+2013), so it is left out. See docs/devlogs/phase-4-settings-localization.md.
        /// </summary>
        const string VietLower = "àáâãèéêìíòóôõùúýăđĩũơưạảấầẩẫậắằẳẵặẹẻẽếềểễệỉịọỏốồổỗộớờởỡợụủứừửữựỳỵỷỹ";
        const string VietUpper = "ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝĂĐĨŨƠƯẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼẾỀỂỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪỬỮỰỲỴỶỸ";
        const string Punct = "“”‘’…—×·•«»°";

        static HashSet<char> FontSet()
        {
            var set = new HashSet<char>(VietLower + VietUpper + Punct);
            for (char c = ' '; c <= '~'; c++) set.Add(c);
            set.Add('\n');   // a line break, not a glyph
            return set;
        }

        LangFile enFile, viFile; Lang en, vi; Dictionary<string, string> contentEn;

        [SetUp] public void Load()
        {
            enFile = Lang.Parse(TestLang.Text("en"));
            viFile = Lang.Parse(TestLang.Text("vi"));
            en = new Lang(enFile);
            vi = new Lang(viFile, en);
            contentEn = Lang.ContentEnglish(TestContent.Load());
        }

        /// <summary>The current English of a key: the English file, or content.json for content keys.</summary>
        string English(string key) => en.Has(key) ? en.Template(key) : contentEn.TryGetValue(key, out var s) ? s : null;

        Dictionary<string, string> AllEnglish()
        {
            var all = new Dictionary<string, string>(contentEn);
            foreach (var k in en.Keys) all[k] = en.Template(k);
            return all;
        }

        // ---- the real files ----

        [Test] public void LangFiles_WhenParsed_HoldTheirLanguageAndName()
        {
            Assert.AreEqual("en", enFile.language); Assert.AreEqual("English", enFile.name);
            Assert.AreEqual("vi", viFile.language); Assert.AreEqual("Tiếng Việt", viFile.name);
            Assert.Greater(enFile.entries.Length, 200);
            Assert.Greater(viFile.entries.Length, 350);
        }

#if !UNITY_5_3_OR_NEWER
        [Test] public void SystemTextJson_WhenReadingBothFiles_ReadsTheSameEntries()
        {
            var opts = new System.Text.Json.JsonSerializerOptions { IncludeFields = true };
            foreach (var (code, mine) in new[] { ("en", enFile), ("vi", viFile) })
            {
                var other = System.Text.Json.JsonSerializer.Deserialize<LangFile>(TestLang.Text(code), opts);
                Assert.AreEqual(mine.language, other.language);
                Assert.AreEqual(mine.entries.Length, other.entries.Length, code);
                for (int i = 0; i < mine.entries.Length; i++)
                {
                    Assert.AreEqual(mine.entries[i].key, other.entries[i].key);
                    Assert.AreEqual(mine.entries[i].text, other.entries[i].text, mine.entries[i].key);
                    Assert.AreEqual(mine.entries[i].en, other.entries[i].en, mine.entries[i].key);
                }
            }
        }
#endif

        [Test] public void LangFiles_WhenGroupedByKey_HaveNoDuplicateKeys()
        {
            foreach (var f in new[] { enFile, viFile })
            {
                var dupes = f.entries.GroupBy(e => e.key).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
                CollectionAssert.IsEmpty(dupes, f.language);
            }
        }

        [Test] public void LangFiles_WhenCheckingEntries_HaveNoEmptyText()
        {
            foreach (var f in new[] { enFile, viFile })
                CollectionAssert.IsEmpty(f.entries.Where(e => string.IsNullOrWhiteSpace(e.text)).Select(e => e.key).ToList(), f.language);
        }

        [Test] public void EnglishFile_ComparedWithContentKeys_HoldsNoContentKeys()
        {
            CollectionAssert.IsEmpty(en.Keys.Where(contentEn.ContainsKey).ToList(), "content English stays in content.json");
        }

        [Test] public void VietnameseKeys_WhenLookedUpInEnglish_AreAnEnglishKeyOrAContentKey()
        {
            CollectionAssert.IsEmpty(vi.Keys.Where(k => English(k) == null).ToList());
        }

        [Test] public void Vietnamese_ComparedWithEnglishKeys_HasEveryUiKey()
        {
            CollectionAssert.IsEmpty(vi.Missing(en.Keys));
        }

        /// <summary>Owner, 2026-11-13: every content text is translated; names of materials, catalysts and aspects stay English for now.</summary>
        [Test] public void Vietnamese_HasEveryContentText_AndNoItemNames()
        {
            bool Name(string k) => k.EndsWith(".name") && (k.StartsWith("material.") || k.StartsWith("catalyst.") || k.StartsWith("aspect."));
            CollectionAssert.IsEmpty(vi.Missing(contentEn.Keys.Where(k => !Name(k))));
            CollectionAssert.IsEmpty(vi.Keys.Where(Name).ToList(), "item names fall back to content.json");
        }

        [Test] public void Vietnamese_ComparedWithCurrentEnglish_IsNotStale()
        {
            CollectionAssert.IsEmpty(vi.Stale(AllEnglish()), "the en field no longer matches the English: re-translate");
            CollectionAssert.IsEmpty(viFile.entries.Where(e => string.IsNullOrEmpty(e.en)).Select(e => e.key).ToList(), "every entry records its English");
        }

        [Test] public void Placeholders_InEveryVietnameseEntry_MatchTheEnglish()
        {
            var bad = new List<string>();
            foreach (var e in viFile.entries)
            {
                var want = Lang.Placeholders(English(e.key)).OrderBy(x => x, StringComparer.Ordinal);
                var got = Lang.Placeholders(e.text).OrderBy(x => x, StringComparer.Ordinal);
                if (!want.SequenceEqual(got)) bad.Add(e.key + ": " + e.text);
            }
            CollectionAssert.IsEmpty(bad);
        }

        [Test] public void VietnameseText_WhenCheckedAgainstTheFontSet_UsesOnlyCharactersInBothFonts()
        {
            var set = FontSet();
            var text = viFile.name + string.Concat(viFile.entries.Select(e => e.text));
            var missing = text.Where(c => !set.Contains(c)).Distinct().Select(c => c + " U+" + ((int)c).ToString("X4")).ToList();
            CollectionAssert.IsEmpty(missing);
        }

        [Test] public void Vietnamese_WhenNormalizedToFormC_IsUnchangedAndPrecomposed()
        {
            var bad = viFile.entries.Where(e => e.text != e.text.Normalize(System.Text.NormalizationForm.FormC)).Select(e => e.key).ToList();
            CollectionAssert.IsEmpty(bad, "type NFC (dựng sẵn), not letter + combining mark");
        }

        // The English limits from ContentTests.EventTexts_OfEveryEvent_FitTheEventWindow: the best bound until the Vietnamese layout is decided.
        [Test] public void VietnameseEventTexts_AgainstEnglishLimits_FitTheEventWindow()
        {
            int Limit(string k) => k.EndsWith(".name") ? 22 : k.EndsWith(".text") ? 56 : k.Contains(".out.") ? 52 : int.MaxValue;
            var bad = viFile.entries.Where(e => e.key.StartsWith("event.") && e.text.Length > Limit(e.key))
                .Select(e => e.key + " (" + e.text.Length + ")").ToList();
            CollectionAssert.IsEmpty(bad);
        }

        [Test] public void FontSet_WhenCounted_HasThe134VietnameseLetters()
        {
            Assert.AreEqual(67, VietLower.Length); Assert.AreEqual(67, VietUpper.Length);
            Assert.AreEqual(134, new HashSet<char>(VietLower + VietUpper).Count);
            Assert.AreEqual(VietUpper, VietLower.ToUpperInvariant());
        }

        [Test] public void RealLookups_InVietnameseAndEnglish_ReturnTheShippedText()
        {
            Assert.AreEqual("Bắt đầu", vi.Get("ui.title.begin"));
            Assert.AreEqual("+1 lượt", vi.Plural("note.trials", 1));
            Assert.AreEqual("+1 trial", en.Plural("note.trials", 1));
            Assert.AreEqual("-2 trials", en.Plural("note.trials", -2));
            Assert.AreEqual("Tầng 3", vi.Get("ui.hud.depth", ("n", 3)));
            Assert.AreEqual("Ember Coal", vi.Content("material.ember_coal.name", "Ember Coal"), "names stay English");
            Assert.AreEqual("Hầm Nông", vi.Content(Lang.DepthKey(1), "The Shallow Cellar"));
            Assert.AreEqual("Bạn để yên nó.", vi.Content(Lang.OutcomeKey("cracked_altar", 1, 0), "You leave it be."));
        }

        // ---- Lang itself ----

        static Lang Make(string code, Lang fallback, params (string key, string text)[] rows) =>
            new Lang(new LangFile { language = code, entries = rows.Select(r => new LangEntry { key = r.key, text = r.text }).ToArray() }, fallback);

        [Test] public void Fill_NamedPlaceholders_SignAndFormat()
        {
            Assert.AreEqual("Found Iron Ore", Lang.Fill("Found {item}", ("item", "Iron Ore")));
            Assert.AreEqual("+2 / -1 / 0", Lang.Fill("{a:+} / {b:+} / {c:+}", ("a", 2), ("b", -1), ("c", 0)));
            Assert.AreEqual("1,500", Lang.Fill("{n:N0}", ("n", 1500L)));
            Assert.AreEqual("{who} met 3", Lang.Fill("{who} met {n}", ("n", 3)), "an unknown name stays");
            Assert.AreEqual("no braces", Lang.Fill("no braces", ("n", 3)));
        }

        [Test] public void Fallback_WhenTextIsMissing_GoesLanguageThenEnglishThenSuppliedThenKey()
        {
            var english = Make("en", null, ("a", "A en"), ("b", "B en"));
            var other = Make("xx", english, ("a", "A xx"), ("empty", ""));
            Assert.AreEqual("A xx", other.Get("a"));
            Assert.AreEqual("B en", other.Get("b"));
            Assert.AreEqual("From content", other.Content("c", "From content"));
            Assert.AreEqual("d", other.Get("d"));
            Assert.AreEqual("empty", other.Get("empty"), "an empty text counts as missing");
            Assert.AreEqual("A xx", other.Content("a", "ignored"));
        }

        [Test] public void Plural_OneAndOther_AndOtherOnlyLanguages()
        {
            var english = Make("en", null, ("t.one", "{n:+} trial"), ("t.other", "{n:+} trials"));
            var single = Make("vi", english, ("t.other", "{n:+} lượt"));
            Assert.AreEqual("+1 trial", english.Plural("t", 1));
            Assert.AreEqual("-1 trial", english.Plural("t", -1));
            Assert.AreEqual("+3 trials", english.Plural("t", 3));
            Assert.AreEqual("0 trials", english.Plural("t", 0));
            Assert.AreEqual("+1 lượt", single.Plural("t", 1), "no .one: .other for every number");
            Assert.AreEqual("+3 lượt", single.Plural("t", 3));
            Assert.AreEqual("missing.other", single.Plural("missing", 2));
        }

        [Test] public void Missing_WithOtherFormPresent_CountsOtherAsCoveringOne()
        {
            var single = Make("vi", null, ("t.other", "x"), ("a", "y"));
            CollectionAssert.AreEqual(new[] { "b", "c.one" }, single.Missing(new[] { "a", "b", "t.one", "t.other", "c.one" }));
        }

        [Test] public void Stale_WhenTheEnglishChanged_FlagsTheEntry()
        {
            var file = new LangFile { language = "vi", entries = new[]
            {
                new LangEntry { key = "a", text = "một", en = "one" },
                new LangEntry { key = "b", text = "hai", en = "two" },
            } };
            var l = new Lang(file);
            CollectionAssert.AreEqual(new[] { "b" }, l.Stale(new Dictionary<string, string> { { "a", "one" }, { "b", "TWO" } }));
            Assert.AreEqual("one", l.SourceOf("a"));
        }

        [Test] public void Parse_WithEscapesUnknownFieldsAndBom_ReadsTheEntries()
        {
            var f = Lang.Parse("\uFEFF{ \"language\": \"xx\", \"extra\": [1, 2.5, true, null, {}],\n \"entries\": [ {\"key\": \"k\", \"text\": \"a\\nb \\\"q\\\" \\u1EC7\"} ] }");
            Assert.AreEqual("xx", f.language);
            Assert.AreEqual(1, f.entries.Length);
            Assert.AreEqual("a\nb \"q\" ệ", f.entries[0].text);
            Assert.IsNull(f.entries[0].en);
            Assert.Throws<FormatException>(() => Lang.Parse("{ \"entries\": [ {\"key\": \"k\" ] }"));
        }

        [Test] public void ContentEnglish_ForShippedContent_KeysFollowTheScheme()
        {
            Assert.AreEqual("A Keepers' Altar", contentEn["event.cracked_altar.name"]);
            Assert.AreEqual("Walk on", contentEn["event.cracked_altar.opt.1.label"]);
            Assert.AreEqual("You leave it be.", contentEn["event.cracked_altar.opt.1.out.0"]);
            Assert.AreEqual("Reach depth 2", contentEn["goal.g_depth2.text"]);
            Assert.AreEqual("The Shallow Cellar", contentEn["depth.1.name"]);
            Assert.AreEqual("Bellows", contentEn["catalyst.bellows.name"]);
        }
    }
}
