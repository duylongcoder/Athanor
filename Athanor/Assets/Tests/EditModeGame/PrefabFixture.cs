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

        /// <summary>Destroys the instance (safe to call twice).</summary>
        public void Destroy() { if (Root != null) Object.DestroyImmediate(Root); }
    }
}
