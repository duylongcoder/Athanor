using System.Collections.Generic;
using Athanor.Game;
using NUnit.Framework;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>
    /// The card chooser logs its development hook only once the cards take a click (the deal is over). tools/web/webtest.mjs and
    /// descents.mjs click a card after this line; a click before it was lost and the run stopped (webtest flake e08/e10).
    /// </summary>
    public class ChoiceHookTests
    {
        PrefabFixture fixture;
        CatalystChoiceView view;
        readonly List<string> heard = new List<string>();

        void Hear(string line, string stack, LogType type) => heard.Add(line);

        [SetUp] public void Build()
        {
            fixture = new PrefabFixture("Assets/Prefabs/Views/CatalystChoice.prefab");
            view = fixture.Get<CatalystChoiceView>();
            view.Bind(TestFx.Make(fixture.Root.transform));
            heard.Clear();
            Application.logMessageReceived += Hear;
        }

        [TearDown] public void Restore()
        {
            Application.logMessageReceived -= Hear;
            DOTween.KillAll();
            fixture.Destroy();
        }

        [Test]
        public void Choose_LogsOpenHook_OnlyAfterTheDeal()
        {
            var offer = new List<ChoiceCard>();
            foreach (var id in new[] { "a", "b", "c" }) offer.Add(new ChoiceCard { id = id, sprite = "dot", name = id, text = id });

            var choose = view.Choose("title", "line", offer, _ => { });
            Assert.IsTrue(choose.MoveNext());                     // the cards are being dealt: no click is taken yet
            CollectionAssert.DoesNotContain(heard, CatalystChoiceView.OpenHook);

            Assert.IsTrue(choose.MoveNext());                     // the deal is over: the cards take a click, and the hook says so
            Assert.AreEqual(1, heard.FindAll(l => l == CatalystChoiceView.OpenHook).Count);

            Assert.IsTrue(choose.MoveNext());                     // waiting for a pick does not log it again
            Assert.AreEqual(1, heard.FindAll(l => l == CatalystChoiceView.OpenHook).Count);
        }
    }
}
