using System.Collections.Generic;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>
    /// F9 (the scripted player) is a toggle, so tools/web/descents.mjs cannot know its state from its own key presses: a press
    /// at the wrong moment left the scripted player off after depth 2 and the run stood still. The toggle logs its new state.
    /// </summary>
    public class AutoHookTests
    {
        GameObject go;
        readonly List<string> heard = new List<string>();

        void Hear(string line, string stack, LogType type) => heard.Add(line);

        [SetUp] public void Build()
        {
            go = new GameObject("auto test");
            heard.Clear();
            Application.logMessageReceived += Hear;
        }

        [TearDown] public void Restore()
        {
            Application.logMessageReceived -= Hear;
            Object.DestroyImmediate(go);
        }

        [Test]
        public void ToggleAuto_FlipsTheScriptedPlayer_AndLogsTheNewState()
        {
            var flow = go.AddComponent<GameManager>();      // edit mode: Awake does not run, only the toggle is exercised
            Assert.IsFalse(flow.Auto);

            flow.ToggleAuto();
            Assert.IsTrue(flow.Auto);
            CollectionAssert.AreEqual(new[] { GameManager.AutoHook + "on" }, heard);

            flow.ToggleAuto();
            Assert.IsFalse(flow.Auto);
            CollectionAssert.AreEqual(new[] { GameManager.AutoHook + "on", GameManager.AutoHook + "off" }, heard);
        }
    }
}
