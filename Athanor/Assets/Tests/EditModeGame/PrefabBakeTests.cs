using System.Reflection;
using Athanor.Core;
using Athanor.Game;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>Baked objects must behave like the ones Build made: a label keeps its key, a button still clicks.</summary>
    public class PrefabBakeTests
    {
        const string Temp = "Assets/Tests/EditModeGame/Temp_Bake.prefab";
        GameObject made, copy;

        [TearDown] public void Clean()
        {
            if (made != null) Object.DestroyImmediate(made);
            if (copy != null) Object.DestroyImmediate(copy);
            AssetDatabase.DeleteAsset(Temp);
            Loc.Set(Loc.English);
        }

        T RoundTrip<T>(GameObject go) where T : Component
        {
            made = go;
            var prefab = PrefabUtility.SaveAsPrefabAsset(go, Temp);
            copy = Object.Instantiate(prefab);
            return copy.GetComponent<T>();
        }

        static void Relabel(Label l) => typeof(Label).GetMethod("Relabel", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(l, null);

        [Test]
        public void Relabel_KeyedLabelAfterPrefabRoundTrip_FollowsTheLanguage()
        {
            var label = RoundTrip<Label>(Make.TextKey(null, "ui.runover.again", 0, 0, FontKind.Title, 16, Color.white, 0).gameObject);
            Loc.Set(Loc.Vietnamese);
            Relabel(label);
            Assert.AreEqual(Loc.T("ui.runover.again"), label.Text);
        }

        [Test]
        public void Relabel_PlainLabelAfterPrefabRoundTrip_KeepsItsText()
        {
            var label = RoundTrip<Label>(Make.Text(null, "abc", 0, 0, FontKind.Body, 16, Color.white, 0).gameObject);
            Relabel(label);
            Assert.AreEqual("abc", label.Text);
        }

        [Test]
        public void Click_ButtonAfterPrefabRoundTrip_RaisesClicked()
        {
            var button = RoundTrip<UiButton>(UiButton.Create(null, "ui.runover.again", 0, 0, 0).gameObject);
            button.Bind();
            int clicks = 0;
            button.Clicked += () => clicks++;
            button.GetComponent<Clickable>().onClick();
            Assert.AreEqual(1, clicks);
        }

        [Test]
        public void Click_DisabledButtonAfterPrefabRoundTrip_DoesNothing()
        {
            var button = RoundTrip<UiButton>(UiButton.Create(null, "ui.runover.again", 0, 0, 0).gameObject);
            button.Bind();
            button.Disabled = true;
            int clicks = 0;
            button.Clicked += () => clicks++;
            button.GetComponent<Clickable>().onClick();
            Assert.AreEqual(0, clicks);
        }

        [Test]
        public void Text_ButtonAfterPrefabRoundTrip_KeepsLabelAndBody()
        {
            var button = RoundTrip<UiButton>(UiButton.Create(null, "ui.runover.again", 0, 0, 0).gameObject);
            Assert.IsNotNull(button.Text, "label reference lost in the prefab");
            var body = typeof(UiButton).GetField("body", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(button) as SpriteRenderer;
            Assert.IsTrue(body != null, "body reference lost in the prefab");
        }

        [Test]
        public void Disabled_SetBeforePrefabRoundTrip_StaysDisabled()
        {
            var original = UiButton.Create(null, "ui.runover.again", 0, 0, 0);
            original.Disabled = true;
            var button = RoundTrip<UiButton>(original.gameObject);
            Assert.IsTrue(button.Disabled);
        }

        [Test]
        public void ShowDepth_RunOverPrefab_WritesTheDepthRow()
        {
            var f = new PrefabFixture("Assets/Prefabs/Views/RunOver.prefab");
            try
            {
                var view = f.Get<RunOverView>();
                view.Bind();
                view.ShowDepth(true, 4);
                Assert.AreEqual("Planted at depth 4", view.DepthName.Text);
            }
            finally { f.Destroy(); }
        }

        // the two buttons in hierarchy order: "Descend again" (index 0), then "Title" (index 1)
        [TestCase(0, true)]
        [TestCase(1, false)]
        public void Click_RunOverPrefabButton_PicksItsChoice(int index, bool again)
        {
            var f = new PrefabFixture("Assets/Prefabs/Views/RunOver.prefab");
            try
            {
                var view = f.Get<RunOverView>();
                view.Bind();
                view.gameObject.SetActive(true);            // Show opens it; Pick ignores a hidden summary
                var buttons = view.GetComponentsInChildren<UiButton>(true);
                Assert.AreEqual(2, buttons.Length);

                buttons[index].GetComponent<Clickable>().onClick();
                view.Pick(!again);                          // ignored once the click has chosen; counts if the click did nothing

                Assert.AreEqual(again, view.Again);
            }
            finally { f.Destroy(); }
        }

        // a goal with no reward item: the toast needs no content database for it
        static GoalReward[] OneGoal() => new[] { new GoalReward { Goal = new GoalData { id = "g_depth2", text = "Reach depth 2" } } };

        // edit mode runs no Update; the toast takes the next item off its queue in Update
        static void Tick(GoalToast t) => typeof(GoalToast).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(t, null);

        [Test]
        public void Add_GoalToastPrefab_ShowsTheGoalName()
        {
            var f = new PrefabFixture("Assets/Prefabs/Views/GoalToast.prefab");
            try
            {
                var toast = f.Get<GoalToast>();
                toast.Bind();

                toast.Add(OneGoal(), null);
                Tick(toast);

                Assert.AreEqual("GOAL COMPLETE: REACH DEPTH 2", toast.Title.Text);
                Assert.AreEqual("Nothing was left to unlock", toast.Line.Text);
            }
            finally { f.Destroy(); }
        }

        [Test]
        public void Click_ShowingGoalToastPrefab_SkipsToTheSlideOut()
        {
            var f = new PrefabFixture("Assets/Prefabs/Views/GoalToast.prefab");
            try
            {
                var toast = f.Get<GoalToast>();
                toast.Bind();
                toast.Add(OneGoal(), null);
                Tick(toast);

                toast.Title.GetComponentInParent<Clickable>(true).onClick();

                float age = (float)typeof(GoalToast).GetField("age", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(toast);
                Assert.GreaterOrEqual(age, GoalToast.Stay + GoalToast.Slide);
            }
            finally { f.Destroy(); }
        }
    }
}
