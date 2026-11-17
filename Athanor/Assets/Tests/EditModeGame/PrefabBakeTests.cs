using System.Reflection;
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
    }
}
