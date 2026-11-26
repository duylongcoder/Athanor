using System.Collections;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Athanor.Tests
{
    /// <summary>Main.unity boots with its managers in place and in order.</summary>
    public class ManagerBootTests
    {
        [UnityTest]
        public IEnumerator Load_MainScene_EveryDataManagerIsUp()
        {
            // Arrange
            var prefs = new PrefsSnapshot(introSeen: true, tutorialDone: true);
            try
            {
                // Act
                yield return MainScene.Load();
                // Assert
                Assert.IsTrue(ContentManager.Exists && SaveManager.Exists && LocalizationManager.Exists, "a manager is missing from Main.unity › Managers");
                Assert.AreEqual(81, ContentManager.Instance.Db.Materials.Count);
                Assert.AreEqual("en", SaveManager.Instance.Settings.language);
                Assert.AreSame(ContentManager.Instance.Db, GameManager.Instance.Db);
            }
            finally { prefs.Dispose(); }
            yield return MainScene.Unload();
        }

        [UnityTest]
        public IEnumerator Sandbox_BeginThenEnd_RestoresTheRealJournalAndProfile()
        {
            // Arrange
            var prefs = new PrefsSnapshot(introSeen: true, tutorialDone: true);
            try
            {
                yield return MainScene.Load();
                var p = ProgressManager.Instance;
                var journal = p.Journal; var profile = p.Profile;
                // Act
                p.BeginSandbox();
                bool swapped = p.Sandbox && p.Journal != journal && p.Profile != profile;
                p.EndSandbox();
                // Assert
                Assert.IsTrue(swapped, "a sandbox plays on a throwaway journal and profile");
                Assert.IsFalse(p.Sandbox);
                Assert.AreSame(journal, p.Journal);
                Assert.AreSame(profile, p.Profile);
            }
            finally { prefs.Dispose(); }
            yield return MainScene.Unload();
        }

        [UnityTest]
        public IEnumerator Load_TwiceInOnePlaySession_ManagersAreTheSecondScenes()
        {
            // Arrange
            var prefs = new PrefsSnapshot(introSeen: true, tutorialDone: true);
            try
            {
                yield return MainScene.Load();
                var first = RunManager.Instance;
                yield return MainScene.Unload();
                bool clearedOnUnload = !RunManager.Exists;
                // Act
                yield return MainScene.Load();
                // Assert
                Assert.IsTrue(clearedOnUnload, "unloading Main forgets its managers");
                Assert.IsTrue(RunManager.Exists);
                Assert.AreNotSame(first, RunManager.Instance);
            }
            finally { prefs.Dispose(); }
            yield return MainScene.Unload();
        }

        [UnityTest]
        public IEnumerator Load_MainScene_LogsNoErrorAndReachesTheTitle()
        {
            // Arrange
            var prefs = new PrefsSnapshot(introSeen: true, tutorialDone: true);
            try
            {
                // Act
                yield return MainScene.Load();
                for (int i = 0; i < 120 && GameManager.Instance.State != Athanor.Gameplay.FlowState.Title; i++) yield return null;
                // Assert
                Assert.AreEqual(Athanor.Gameplay.FlowState.Title, GameManager.Instance.State);
                LogAssert.NoUnexpectedReceived();
            }
            finally { prefs.Dispose(); }
            yield return MainScene.Unload();
        }
    }
}
