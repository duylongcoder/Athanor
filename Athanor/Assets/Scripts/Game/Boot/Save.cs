using System;
using Athanor.Gameplay;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Keeps the journal and the profile between sessions in PlayerPrefs (browser storage in a web build).</summary>
    public static class Save
    {
        public const string Key = "athanor.journal.v1", IntroKey = "athanor.intro.v1", ProfileKey = "athanor.profile.v1",
            TutorialKey = "athanor.tutorial.v1", SettingsKey = "athanor.settings.v1";

        /// <summary>The saved journal; an empty one when nothing is saved or what is saved cannot be read.</summary>
        public static Journal LoadJournal()
        {
            try
            {
                var text = PlayerPrefs.GetString(Key, "");
                return string.IsNullOrEmpty(text) ? new Journal() : Journal.FromData(JsonUtility.FromJson<JournalData>(text));
            }
            catch (Exception e) { Debug.LogWarning("[save] journal unreadable, starting empty: " + e.Message); return new Journal(); }
        }

        public static void StoreJournal(Journal journal)
        {
            PlayerPrefs.SetString(Key, JsonUtility.ToJson(journal.ToData()));
            PlayerPrefs.Save();
        }

        /// <summary>
        /// The saved profile (stats, goals done, items won); a new one with a reward seed of its own when nothing
        /// is saved or what is saved cannot be read. Anything the content no longer has is dropped.
        /// </summary>
        public static Profile LoadProfile(ContentDb db)
        {
            int seed = FreshSeed();
            try
            {
                var text = PlayerPrefs.GetString(ProfileKey, "");
                return string.IsNullOrEmpty(text) ? new Profile(seed) : Profile.FromData(JsonUtility.FromJson<ProfileData>(text), db, seed);
            }
            catch (Exception e) { Debug.LogWarning("[save] profile unreadable, starting fresh: " + e.Message); return new Profile(seed); }
        }

        public static void StoreProfile(Profile profile)
        {
            PlayerPrefs.SetString(ProfileKey, JsonUtility.ToJson(profile.ToData()));
            PlayerPrefs.Save();
        }

        /// <summary>Forgets goals, unlocks and stats. The journal is kept.</summary>
        public static void ResetProfile()
        {
            PlayerPrefs.DeleteKey(ProfileKey);
            PlayerPrefs.Save();
        }

        /// <summary>The saved settings; defaults when nothing is saved or what is saved cannot be read (Settings.FromJson never throws).</summary>
        public static Settings LoadSettings()
        {
            return Settings.FromJson(PlayerPrefs.GetString(SettingsKey, ""));
        }

        public static void StoreSettings(Settings settings)
        {
            PlayerPrefs.SetString(SettingsKey, settings.ToJson());
            PlayerPrefs.Save();
        }

        static int FreshSeed()
        {
            int seed = Environment.TickCount ^ (int)(DateTime.UtcNow.Ticks & 0x7fffffff);
            return seed == 0 ? 1 : seed;
        }

        public static bool IntroSeen => PlayerPrefs.GetInt(IntroKey, 0) == 1;
        public static void SetIntroSeen(bool v) { PlayerPrefs.SetInt(IntroKey, v ? 1 : 0); PlayerPrefs.Save(); }

        /// <summary>The tutorial was played to its end (or skipped). See TutorialScript.ShouldRun.</summary>
        public static bool TutorialDone => PlayerPrefs.GetInt(TutorialKey, 0) == 1;
        public static void SetTutorialDone(bool v) { PlayerPrefs.SetInt(TutorialKey, v ? 1 : 0); PlayerPrefs.Save(); }
    }
}
