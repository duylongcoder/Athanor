using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Athanor.Gameplay;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Athanor.Tests
{
    /// <summary>Main.unity loaded beside the test runner's scene.</summary>
    public static class MainScene
    {
        public const string Name = "Main";

        public static IEnumerator Load()
        {
            yield return SceneManager.LoadSceneAsync(Name, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Name));
            yield return null;
        }

        public static IEnumerator Unload()
        {
            var s = SceneManager.GetSceneByName(Name);
            if (s.isLoaded) yield return SceneManager.UnloadSceneAsync(s);
        }

        public static T Find<T>() where T : Object => Object.FindFirstObjectByType<T>(FindObjectsInactive.Include);
    }

    /// <summary>Takes the game's PlayerPrefs keys aside and gives the test a known start; Dispose puts the player's keys back.</summary>
    public sealed class PrefsSnapshot : IDisposable
    {
        static readonly string[] Strings = { Save.Key, Save.ProfileKey, Save.SettingsKey };
        static readonly string[] Ints = { Save.IntroKey, Save.TutorialKey };
        readonly Dictionary<string, string> strings = new Dictionary<string, string>();
        readonly Dictionary<string, int> ints = new Dictionary<string, int>();

        public PrefsSnapshot(bool introSeen, bool tutorialDone)
        {
            foreach (var k in Strings) if (PlayerPrefs.HasKey(k)) strings[k] = PlayerPrefs.GetString(k);
            foreach (var k in Ints) if (PlayerPrefs.HasKey(k)) ints[k] = PlayerPrefs.GetInt(k);
            foreach (var k in Strings.Concat(Ints)) PlayerPrefs.DeleteKey(k);
            PlayerPrefs.SetInt(Save.IntroKey, introSeen ? 1 : 0);
            PlayerPrefs.SetInt(Save.TutorialKey, tutorialDone ? 1 : 0);
            PlayerPrefs.SetString(Save.SettingsKey, new Settings { language = "en" }.ToJson());
            PlayerPrefs.Save();
        }

        public void Dispose()
        {
            foreach (var k in Strings.Concat(Ints)) PlayerPrefs.DeleteKey(k);
            foreach (var kv in strings) PlayerPrefs.SetString(kv.Key, kv.Value);
            foreach (var kv in ints) PlayerPrefs.SetInt(kv.Key, kv.Value);
            PlayerPrefs.Save();
        }
    }

    /// <summary>The gameplay lines of the dev hooks (Dev.Hook logs them), in order.</summary>
    public sealed class TraceRecorder : IDisposable
    {
        static readonly string[] Keep = { "[start]", "[intro]", "[descent]", "[choice]", "[runover]", "[ignite]", "[tut]", "[drive]", "[pause]" };
        public readonly List<string> Lines = new List<string>();

        public TraceRecorder() => Application.logMessageReceived += OnLog;
        void OnLog(string message, string stack, LogType type)
        {
            foreach (var k in Keep) if (message.StartsWith(k, StringComparison.Ordinal)) { Lines.Add(message); return; }
        }

        public bool Has(string prefix) => Lines.Any(l => l.StartsWith(prefix, StringComparison.Ordinal));

        /// <summary>The lines up to and including the first one that starts with prefix.</summary>
        public List<string> UpTo(string prefix)
        {
            int i = Lines.FindIndex(l => l.StartsWith(prefix, StringComparison.Ordinal));
            return i < 0 ? new List<string>(Lines) : Lines.Take(i + 1).ToList();
        }

        public void Dispose() => Application.logMessageReceived -= OnLog;
    }

    /// <summary>Text baselines under Tests/PlayMode/Baselines. ATHANOR_RECORD=1 writes them instead of comparing.</summary>
    public static class Baseline
    {
        static string Dir => Path.Combine(Application.dataPath, "Tests/PlayMode/Baselines");

        public static void Check(string file, IReadOnlyList<string> lines)
        {
            var path = Path.Combine(Dir, file);
            if (Environment.GetEnvironmentVariable("ATHANOR_RECORD") == "1")
            {
                Assert.Greater(lines.Count, 0, "nothing to record for " + file);
                Directory.CreateDirectory(Dir);
                File.WriteAllLines(path, lines);
                Assert.IsTrue(File.Exists(path), "recorded " + path);
                return;
            }
            Assert.IsTrue(File.Exists(path), "no baseline " + path + ": record it with ATHANOR_RECORD=1 on the commit before the change");
            var want = File.ReadAllLines(path);
            int n = Math.Min(want.Length, lines.Count);
            for (int i = 0; i < n; i++) Assert.AreEqual(want[i], lines[i], "line " + (i + 1) + " of " + file);
            Assert.AreEqual(want.Length, lines.Count, "line count of " + file);
        }
    }
}
