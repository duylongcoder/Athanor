using Athanor.Gameplay;
using NUnit.Framework;

namespace Athanor.Tests
{
    public class SettingsTests
    {
        [Test]
        public void New_NoArguments_HasDefaultValues()
        {
            var s = new Settings();
            Assert.AreEqual(1, s.version);
            Assert.AreEqual(7, s.music);
            Assert.AreEqual(8, s.sfx);
            Assert.IsTrue(s.haptics);
            Assert.AreEqual("", s.language);
        }

        [Test]
        public void New_NoArguments_SwayIsOn()
        {
            Assert.IsTrue(new Settings().sway);
        }

        [Test]
        public void ToJson_ThenFromJson_RoundTripsSwayOff()
        {
            var back = Settings.FromJson(new Settings { sway = false }.ToJson());
            Assert.IsFalse(back.sway);
            Assert.IsTrue(Settings.FromJson(new Settings { sway = true }.ToJson()).sway);
        }

        [Test]
        public void FromJson_OldSettingsWithoutSway_KeepsSwayOn()
        {
            var back = Settings.FromJson("{\"version\":1,\"music\":5,\"sfx\":5,\"haptics\":false,\"language\":\"en\"}");
            Assert.IsTrue(back.sway);
            Assert.IsFalse(back.haptics);
        }

        [Test]
        public void FromJson_SwayAsNumber_GivesDefaults()
        {
            Assert.IsTrue(Settings.FromJson("{\"sway\": 0}").sway);
        }

        [Test]
        public void ToJson_ThenFromJson_RoundTripsValues()
        {
            var s = new Settings { music = 3, sfx = 0, haptics = false, language = "vi" };
            var back = Settings.FromJson(s.ToJson());
            Assert.AreEqual(1, back.version);
            Assert.AreEqual(3, back.music);
            Assert.AreEqual(0, back.sfx);
            Assert.IsFalse(back.haptics);
            Assert.AreEqual("vi", back.language);
        }

        [Test]
        public void FromJson_JsonUtilityAndSystemTextJsonShape_ReadsFields()
        {
            var back = Settings.FromJson("{ \"version\": 1, \"music\": 4, \"sfx\": 9,\n \"haptics\": false, \"language\": \"en\" }");
            Assert.AreEqual(4, back.music);
            Assert.AreEqual(9, back.sfx);
            Assert.IsFalse(back.haptics);
            Assert.AreEqual("en", back.language);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("not json")]
        [TestCase("{\"music\": ")]
        [TestCase("[1,2,3]")]
        [TestCase("{\"music\": \"loud\"}")]
        public void FromJson_BrokenJson_GivesDefaults(string json)
        {
            var s = Settings.FromJson(json);
            Assert.AreEqual(7, s.music);
            Assert.AreEqual(8, s.sfx);
            Assert.IsTrue(s.haptics);
            Assert.AreEqual("", s.language);
        }

        [Test]
        public void FromJson_MissingFields_KeepDefaults()
        {
            var s = Settings.FromJson("{\"music\": 2}");
            Assert.AreEqual(2, s.music);
            Assert.AreEqual(8, s.sfx);
            Assert.IsTrue(s.haptics);
        }

        [Test]
        public void FromJson_OutOfRangeValues_ClampsVolumesAndLanguage()
        {
            var s = Settings.FromJson("{\"music\": 99, \"sfx\": -4, \"language\": \"fr\"}");
            Assert.AreEqual(10, s.music);
            Assert.AreEqual(0, s.sfx);
            Assert.AreEqual("", s.language);

            var t = new Settings { music = 50, sfx = -1, language = "xx" };
            t.Clamp();
            Assert.AreEqual(10, t.music);
            Assert.AreEqual(0, t.sfx);
            Assert.AreEqual("", t.language);
        }

        [Test]
        public void RoundTrip_LanguageWithEscapes_Survives()
        {
            var s = Settings.FromJson("{\"language\": \"v\\u0069\"}");
            Assert.AreEqual("vi", s.language);
        }
    }
}
