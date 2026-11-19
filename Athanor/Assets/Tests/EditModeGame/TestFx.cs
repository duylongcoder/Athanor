using Athanor.Game;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>Effect players for a test that binds a view: a ScreenFx with its flash and fade sprites, and a Sparks.</summary>
    [SetUpFixture]
    public sealed class TestFx
    {
        static readonly System.Collections.Generic.List<GameObject> made = new System.Collections.Generic.List<GameObject>();

        /// <summary>Destroys whatever Make built and no parent took with it, once the tests are done.</summary>
        [OneTimeTearDown]
        public void DestroyLeftovers()
        {
            foreach (var g in made) if (g != null) Object.DestroyImmediate(g);
            made.Clear();
        }

        /// <summary>Builds both under parent (they go when the parent does); parent may be null when the test destroys them itself.</summary>
        public static FxRefs Make(Transform parent)
        {
            var go = new GameObject("test fx");
            made.Add(go);
            if (parent != null) go.transform.SetParent(parent, false);
            var screen = go.AddComponent<ScreenFx>();
            var so = new SerializedObject(screen);
            so.FindProperty("flash").objectReferenceValue = Sprite(go, "flash");
            so.FindProperty("black").objectReferenceValue = Sprite(go, "black");
            so.ApplyModifiedPropertiesWithoutUndo();
            return new FxRefs(screen, go.AddComponent<Sparks>());
        }

        static SpriteRenderer Sprite(GameObject parent, string name)
        {
            var g = new GameObject(name);
            g.transform.SetParent(parent.transform, false);
            return g.AddComponent<SpriteRenderer>();
        }
    }
}
