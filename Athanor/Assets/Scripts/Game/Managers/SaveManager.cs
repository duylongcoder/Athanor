using Athanor.Gameplay;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The player's settings and the small saved flags. Storage itself stays in Save (PlayerPrefs; browser storage on the web).</summary>
    [DefaultExecutionOrder(-190)]
    public sealed class SaveManager : Singleton<SaveManager>
    {
        public Settings Settings { get; private set; }

        protected override void OnAwake()
        {
            Settings = Save.LoadSettings();
            Haptics.Enabled = Settings.haptics;
        }

        public void StoreSettings() => Save.StoreSettings(Settings);

        public bool IntroSeen => Save.IntroSeen;
        public void SetIntroSeen(bool seen) => Save.SetIntroSeen(seen);
        public bool TutorialDone => Save.TutorialDone;
        public void SetTutorialDone(bool done) => Save.SetTutorialDone(done);

        [ContextMenu("Erase journal, profile, settings, intro and tutorial")]
        void EraseAll()
        {
            foreach (var k in new[] { Save.Key, Save.ProfileKey, Save.SettingsKey, Save.IntroKey, Save.TutorialKey }) PlayerPrefs.DeleteKey(k);
            PlayerPrefs.Save();
            Debug.Log("[save] erased; restart Play to start fresh");
        }

        [ContextMenu("Show the tutorial again")]
        void ForgetTutorial() => Save.SetTutorialDone(false);
    }
}
