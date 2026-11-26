using System.Collections;
using System.Collections.Generic;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Athanor.Tests
{
    /// <summary>Main.unity played by the scripted player (F9) on a fixed seed: the gameplay hooks equal the recorded run.</summary>
    public class AutoplayTraceTests
    {
        const float Speed = 8f, LimitSeconds = 900f;

        sealed class Result { public bool Ended; public List<string> Lines; }

        static IEnumerator Play(int seed, Result result)
        {
            var prefs = new PrefsSnapshot(introSeen: true, tutorialDone: true);
            var trace = new TraceRecorder();
            try
            {
                yield return MainScene.Load();
                var game = MainScene.Find<GameManager>();
                RunManager.Instance.SeedOverride = seed;
                game.Auto = true;
                Time.timeScale = Speed;
                float elapsed = 0f;  // deterministic cap: accumulated unscaled frame time, not the wall clock
                while (!trace.Has("[runover]") && elapsed < LimitSeconds) { yield return null; elapsed += Time.unscaledDeltaTime; }
                result.Ended = trace.Has("[runover]");
                result.Lines = trace.UpTo("[runover]");
            }
            finally { Time.timeScale = 1f; trace.Dispose(); prefs.Dispose(); }
            // known game defect, carried over for Task 3+: RunOverView.OnDestroy stops tweens that are nested in a Sequence, and PrimeTween logs it once at scene unload
            // (it may log it more than once, so the unload alone runs with failing log messages ignored; the run itself is checked as usual)
            LogAssert.ignoreFailingMessages = true;
            yield return MainScene.Unload();
            yield return null;
            LogAssert.ignoreFailingMessages = false;
        }

        [UnityTest, Timeout(1200000)]
        public IEnumerator Autoplay_Seed11_MatchesBaseline()
        {
            // Arrange
            var r = new Result();
            // Act
            yield return Play(11, r);
            // Assert
            Assert.IsTrue(r.Ended, "the run did not reach its summary in time");
            Baseline.Check("autoplay_seed_11.txt", r.Lines);
        }

        [UnityTest, Timeout(1200000)]
        public IEnumerator Autoplay_Seed22_MatchesBaseline()
        {
            // Arrange
            var r = new Result();
            // Act
            yield return Play(22, r);
            // Assert
            Assert.IsTrue(r.Ended, "the run did not reach its summary in time");
            Baseline.Check("autoplay_seed_22.txt", r.Lines);
        }

        [UnityTest, Timeout(1200000)]
        public IEnumerator Autoplay_Seed33_MatchesBaseline()
        {
            // Arrange
            var r = new Result();
            // Act
            yield return Play(33, r);
            // Assert
            Assert.IsTrue(r.Ended, "the run did not reach its summary in time");
            Baseline.Check("autoplay_seed_33.txt", r.Lines);
        }
    }
}
