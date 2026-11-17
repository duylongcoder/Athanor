using System.Linq;
using Athanor.Core;
using Athanor.Game;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>The Game layer's language: Vietnamese draws from SVN-Coder's Crux, every key resolves, and the core's English notes are translated.</summary>
    public class LocTests
    {
        [TearDown] public void BackToEnglish() { EditorGlobals.Inject(); Loc.Set(Loc.English); }

        static LangFile File(string code) => Lang.Parse(Resources.Load<TextAsset>("lang/" + code).text);

        [Test]
        public void Use_WithInjectedFiles_ServesTheirText()
        {
            var en = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/en.json");
            var vi = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/vi.json");
            const string entry = "\"text\": \"Xuống lần nữa\"";
            StringAssert.Contains(entry, vi.text, "vi.json changed: update the entry this test replaces");
            var marked = new TextAsset(vi.text.Replace(entry, "\"text\": \"P6MARKER\""));
            Loc.Use(en, marked);
            Loc.Set(Loc.Vietnamese);
            Assert.AreEqual("P6MARKER", Loc.T("ui.runover.again"));
        }

        [Test]
        public void Use_NullFile_Throws()
        {
            var en = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Resources/lang/en.json");
            Assert.Throws<System.ArgumentNullException>(() => Loc.Use(null, en));
            Assert.Throws<System.ArgumentNullException>(() => Loc.Use(en, null));
        }

        [Test]
        public void ViJsonCharacters_AllInCruxAtlas_NoneMissing()
        {
            var crux = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Fonts/Crux.asset");
            Assert.IsNotNull(crux, "run Athanor > Build Fonts");
            var vi = File("vi");
            var text = vi.name + string.Concat(vi.entries.Select(e => e.text));
            var missing = text.Where(c => c != '\n' && !crux.HasCharacter(c, false)).Distinct().Select(c => c + " U+" + ((int)c).ToString("X4")).ToList();
            CollectionAssert.IsEmpty(missing);
        }

        [Test]
        public void UppercasedViStrings_AreInTheCruxAtlas_BecauseScreensWriteCapitalsThroughLocUpper()
        {
            var crux = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Fonts/Crux.asset");
            var vi = File("vi");
            var text = Loc.Upper(vi.name + string.Concat(vi.entries.Select(e => e.text)));
            var missing = text.Where(c => c != '\n' && !crux.HasCharacter(c, false)).Distinct().Select(c => c + " U+" + ((int)c).ToString("X4")).ToList();
            CollectionAssert.IsEmpty(missing, "bake them: Athanor > Build Fonts");
            const string letters = "àáâãèéêìíòóôõùúýăđĩũơưạảấầẩẫậắằẳẵặẹẻẽếềểễệỉịọỏốồổỗộớờởỡợụủứừửữựỳỵỷỹ";
            var gaps = (letters + Loc.Upper(letters)).Where(c => !crux.HasCharacter(c, false)).ToList();
            CollectionAssert.IsEmpty(gaps, "all 134 Vietnamese letters, either case");
        }

        [Test]
        public void EveryCharacterInEnJson_IsInTheCruxAtlas_SoALanguageSwitchNeverDrawsABlank()
        {
            var crux = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/Art/Fonts/Crux.asset");
            var en = File("en");
            var missing = (en.name + string.Concat(en.entries.Select(e => e.text))).Where(c => c != '\n' && c != '–' && !crux.HasCharacter(c, false)).Distinct().ToList();
            CollectionAssert.IsEmpty(missing);
        }

        [Test]
        public void EveryUiKey_ResolvesInBothLanguages_NeverToTheKey()
        {
            var keys = File("en").entries.Select(e => e.key).Where(k => k.StartsWith("ui.") || k.StartsWith("cutscene.") || k.StartsWith("story.")).ToList();
            foreach (var code in new[] { Loc.English, Loc.Vietnamese })
            {
                Loc.Set(code);
                var bad = keys.Where(k => !Loc.Has(k) && !Loc.Has(k + ".other")).ToList();
                CollectionAssert.IsEmpty(bad, code);
            }
        }

        [Test]
        public void Vietnamese_ChangesTheWords_AndEnglishComesBack()
        {
            Assert.AreEqual("Begin", Loc.T("ui.title.begin"));
            Loc.Set(Loc.Vietnamese);
            Assert.AreEqual("Bắt đầu", Loc.T("ui.title.begin"));
            Assert.AreEqual("Athanor", Loc.T("ui.title.logo"));
            Loc.Set(Loc.English);
            Assert.AreEqual("Begin", Loc.T("ui.title.begin"));
        }

        [Test]
        public void ItemNames_StayEnglish_InVietnamese()
        {
            Loc.Set(Loc.Vietnamese);
            Assert.AreEqual("Ember Coal", Loc.C("material.ember_coal.name", "Ember Coal"));
        }

        [Test]
        public void FormatNumber_ByLanguage_UsesLocaleSeparators()
        {
            Assert.AreEqual("1,500", Loc.Number(1500));
            Loc.Set(Loc.Vietnamese);
            Assert.AreEqual("1.500", Loc.Number(1500));
        }

        [Test]
        public void CoreNotes_AreTranslated_InVietnamese_AndLeftAloneInEnglish()
        {
            Assert.AreEqual("Found Ember Coal", Wording.Note("Found Ember Coal"));
            Loc.Set(Loc.Vietnamese);
            Assert.AreEqual("Tìm thấy Ember Coal", Wording.Note("Found Ember Coal"));
            Assert.AreEqual("Mất Ember Coal trong chuyến này", Wording.Note("Lost a Ember Coal for this run"));
            Assert.AreEqual("+2 lượt", Wording.Note("+2 trials"));
            Assert.AreEqual("Ember Coal hóa thành Sky Iron", Wording.Note("Ember Coal became Sky Iron"));
            Assert.AreEqual("A sentence nobody knows", Wording.Note("A sentence nobody knows"));
        }

        [Test]
        public void ReadLangFromUrl_WithLangParam_ReturnsCode()
        {
            Assert.AreEqual("vi", Loc.FromUrl("http://localhost:8080/index.html?lang=vi"));
            Assert.AreEqual("en", Loc.FromUrl("http://localhost/?x=1&lang=en"));
            Assert.IsNull(Loc.FromUrl("http://localhost/?lang=fr"));
            Assert.IsNull(Loc.FromUrl(""));
        }
    }
}
