using System.Reflection;
using Athanor.Core;
using Athanor.Game;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>
    /// The run-over depth row (owner, phase-5-prep 2026-11-16): a won run says "Planted at depth n" on one line with no value;
    /// a lost run says "Depth reached" with "n of 5". The view is reused, so each path must undo the other, and the bound key must
    /// follow a language change.
    /// </summary>
    public class RunOverDepthTests
    {
        ArtLibrary libBefore;
        GameObject root;
        RunOverView view;

        [SetUp] public void Build()
        {
            libBefore = Make.Lib;
            Make.Lib = AssetDatabase.LoadAssetAtPath<ArtLibrary>("Assets/Art/ArtLibrary.asset");
            Loc.Set(Loc.English);
            root = new GameObject("run over test");
            view = root.AddComponent<RunOverView>();
            view.Build();
        }

        [TearDown] public void Restore()
        {
            Object.DestroyImmediate(root);
            Make.Lib = libBefore;
            Loc.Set(Loc.English);
        }

        // labels under a hidden view do not hear Loc.Changed in edit mode; run the same rewrite the label runs on a language change
        static void Relabel(Label l) => typeof(Label).GetMethod("Relabel", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(l, null);

        static string Of(int n) => Loc.T("ui.common.of", ("a", n), ("b", RunState.FinalDepth));

        [Test]
        public void Won_ShowsPlantedAtDepth_OnOneLine_WithNoValue()
        {
            view.ShowDepth(true, 4);
            Assert.AreEqual("Planted at depth 4", view.DepthName.Text);
            Assert.AreEqual("", view.DepthValue.Text);
        }

        [Test]
        public void Lost_ShowsDepthReached_AndNOfFive()
        {
            view.ShowDepth(false, 3);
            Assert.AreEqual("Depth reached", view.DepthName.Text);
            Assert.AreEqual(Of(3), view.DepthValue.Text);
        }

        [Test]
        public void WonThenLost_OnTheSameView_RestoresTheLostRow()
        {
            view.ShowDepth(true, 4);
            view.ShowDepth(false, 2);
            Assert.AreEqual("Depth reached", view.DepthName.Text);
            Assert.AreEqual(Of(2), view.DepthValue.Text);
            view.ShowDepth(true, 5);
            Assert.AreEqual("Planted at depth 5", view.DepthName.Text);
            Assert.AreEqual("", view.DepthValue.Text);
        }

        [Test]
        public void Relabel_AfterLanguageChange_KeepsEachPathsText()
        {
            view.ShowDepth(true, 4);
            Loc.Set(Loc.Vietnamese);
            Relabel(view.DepthName);
            Assert.AreEqual(Loc.T("ui.runover.planted_at", ("n", 4)), view.DepthName.Text);
            Assert.AreNotEqual(Loc.T("ui.runover.depth_reached"), view.DepthName.Text);

            view.ShowDepth(false, 3);
            Loc.Set(Loc.English);
            Relabel(view.DepthName);
            Assert.AreEqual("Depth reached", view.DepthName.Text);
        }
    }
}
