using System.Reflection;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>A copy of a prefab for one test. Call Destroy in [TearDown] (or a finally).</summary>
    public sealed class PrefabFixture
    {
        /// <summary>The scene instance of the prefab.</summary>
        public readonly GameObject Root;

        /// <summary>Instantiates the prefab at prefabPath; fails the test when it does not exist.</summary>
        public PrefabFixture(string prefabPath)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            Assert.IsNotNull(prefab, "no prefab at " + prefabPath);
            Root = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        }

        /// <summary>The component of type T on the root, or null.</summary>
        public T Get<T>() where T : Component => Root.GetComponent<T>();

        /// <summary>
        /// Destroys the instance (safe to call twice). Unity does not run OnDestroy in Edit mode, so it is called by hand: a view that
        /// subscribed to a static event (Loc.Changed) in Bind must let go of it, or it fires on the dead view in a later test.
        /// </summary>
        public void Destroy()
        {
            if (Root == null) return;
            foreach (var m in Root.GetComponentsInChildren<MonoBehaviour>(true))
                m.GetType().GetMethod("OnDestroy", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)?.Invoke(m, null);
            Object.DestroyImmediate(Root);
        }
    }
}
