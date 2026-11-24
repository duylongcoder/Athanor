using Athanor.Gameplay;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>Saved settings (athanor.settings.v1), the haptics plan and the mute-on-hidden-tab hook.</summary>
    public class SettingsHapticsTests
    {
        string before;

        [SetUp] public void Keep()
        {
            before = PlayerPrefs.GetString(Save.SettingsKey, null);
            Haptics.Enabled = true; Haptics.ForceSupported = null; Haptics.Sink = null;
            AudioMute.Reset(); AudioMute.Sink = null;
        }

        [TearDown] public void Restore()
        {
            if (before == null) PlayerPrefs.DeleteKey(Save.SettingsKey); else PlayerPrefs.SetString(Save.SettingsKey, before);
            Haptics.Enabled = true; Haptics.ForceSupported = null; Haptics.Sink = null;
            AudioMute.Reset(); AudioMute.Sink = null;
        }

        [Test]
        public void Settings_StoredThenLoaded_RoundTrip()
        {
            Assert.AreEqual("athanor.settings.v1", Save.SettingsKey);
            Save.StoreSettings(new Settings { music = 3, sfx = 0, haptics = false, language = "vi" });
            var s = Save.LoadSettings();
            Assert.AreEqual(3, s.music); Assert.AreEqual(0, s.sfx); Assert.IsFalse(s.haptics); Assert.AreEqual("vi", s.language);
        }

        [Test]
        public void Settings_NothingOrGarbageSaved_GivesDefaults()
        {
            PlayerPrefs.DeleteKey(Save.SettingsKey);
            Assert.AreEqual("", Save.LoadSettings().language);
            foreach (var bad in new[] { "{{ nope", "[]", "{\"music\":\"loud\"}", "null" })
            {
                PlayerPrefs.SetString(Save.SettingsKey, bad);
                var s = Save.LoadSettings();
                Assert.AreEqual(7, s.music, bad); Assert.IsTrue(s.haptics, bad); Assert.AreEqual("", s.language, bad);
            }
        }

        [Test]
        public void Haptics_Plan_OnlyWhenEnabledAndSupported()
        {
            Assert.AreEqual(20, Haptics.Plan(true, true, Haptics.Discovery));
            Assert.AreEqual(40, Haptics.Plan(true, true, Haptics.DepthCleared));
            Assert.AreEqual(60, Haptics.Plan(true, true, Haptics.RunEnded));
            Assert.AreEqual(0, Haptics.Plan(false, true, 20));
            Assert.AreEqual(0, Haptics.Plan(true, false, 20));
        }

        [Test]
        public void Haptics_Pulse_ReachesTheSinkOnlyWhenAllowed()
        {
            int got = 0; Haptics.Sink = ms => got = ms;
            Haptics.ForceSupported = false; Haptics.Pulse(20); Assert.AreEqual(0, got, "desktop and iOS: no pulse");
            Haptics.ForceSupported = true; Haptics.Enabled = false; Haptics.Pulse(20); Assert.AreEqual(0, got, "turned off");
            Haptics.Enabled = true; Haptics.Pulse(Haptics.RunEnded); Assert.AreEqual(60, got);
        }

        [Test]
        public void Haptics_InTheEditor_IsNotSupported() => Assert.IsFalse(Haptics.Supported);

        [Test]
        public void AudioMute_TabHiddenThenShown_PausesThenResumes()
        {
            bool last = false; int calls = 0; AudioMute.Sink = m => { last = m; calls++; };
            AudioMute.SetHidden(true); Assert.IsTrue(last);
            AudioMute.SetHidden(false); Assert.IsFalse(last);
            AudioMute.SetPaused(true); Assert.IsTrue(last);
            AudioMute.SetFocus(false); AudioMute.SetPaused(false); Assert.IsTrue(last, "still unfocused");
            AudioMute.SetFocus(true); Assert.IsFalse(last);
            Assert.AreEqual(6, calls);
        }

        [Test]
        public void AudioMute_AnyReason_Mutes()
        {
            Assert.IsFalse(AudioMute.ShouldMute(false, false, true));
            Assert.IsTrue(AudioMute.ShouldMute(true, false, true));
            Assert.IsTrue(AudioMute.ShouldMute(false, true, true));
            Assert.IsTrue(AudioMute.ShouldMute(false, false, false));
        }
    }
}
