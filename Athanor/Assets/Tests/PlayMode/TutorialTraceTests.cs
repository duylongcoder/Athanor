using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Athanor.Gameplay;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Athanor.Tests
{
    /// <summary>The first-run tutorial auto-driven from Begin to line 5.5: its lines and popup scores equal the recorded run.</summary>
    public class TutorialTraceTests
    {
        const float Speed = 8f, LimitSeconds = 900f;

        [UnityTest, Timeout(1200000)]
        public IEnumerator Tutorial_AutoDriven_MatchesBaseline()
        {
            // Arrange
            var prefs = new PrefsSnapshot(introSeen: true, tutorialDone: false);
            var trace = new TraceRecorder();
            bool finished = false; List<string> lines = null;
            try
            {
                yield return MainScene.Load();
                var game = MainScene.Find<GameManager>();
                var tutorial = MainScene.Find<TutorialManager>();
                tutorial.AutoDrive = true;
                Time.timeScale = Speed;
                while (game.State != FlowState.Title) yield return null;
                // Act
                MainScene.Find<TitleView>().RequestBegin();
                float elapsed = 0f;  // deterministic cap: accumulated unscaled frame time, not the wall clock
                while (!tutorial.Finished && elapsed < LimitSeconds) { yield return null; elapsed += Time.unscaledDeltaTime; }
                finished = tutorial.Finished;
                lines = trace.Lines.Concat(new[] { "[shown] " + string.Join(" ", tutorial.Shown) }).ToList();
            }
            finally { Time.timeScale = 1f; trace.Dispose(); prefs.Dispose(); }
            // known game defect (RunOverView.OnDestroy, see AutoplayTraceTests): the unload alone ignores failing log messages
            LogAssert.ignoreFailingMessages = true;
            try { yield return MainScene.Unload(); yield return null; }
            finally { LogAssert.ignoreFailingMessages = false; }
            // Assert
            Assert.IsTrue(finished, "the tutorial did not reach line 5.5 in time");
            Baseline.Check("tutorial_trace.txt", lines);
        }
    }
}
