using System;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Keeps the journal between sessions in PlayerPrefs (browser storage in a web build).</summary>
    public static class Save
    {
        public const string Key = "athanor.journal.v1", IntroKey = "athanor.intro.v1";

        /// <summary>The saved journal; an empty one when nothing is saved or what is saved cannot be read.</summary>
        public static Journal LoadJournal()
        {
            try
            {
                var text = PlayerPrefs.GetString(Key, "");
                return string.IsNullOrEmpty(text) ? new Journal() : Journal.FromData(JsonUtility.FromJson<JournalData>(text));
            }
            catch (Exception) { return new Journal(); }
        }

        public static void StoreJournal(Journal journal)
        {
            PlayerPrefs.SetString(Key, JsonUtility.ToJson(journal.ToData()));
            PlayerPrefs.Save();
        }

        public static bool IntroSeen
        {
            get => PlayerPrefs.GetInt(IntroKey, 0) == 1;
            set { PlayerPrefs.SetInt(IntroKey, value ? 1 : 0); PlayerPrefs.Save(); }
        }
    }
}
