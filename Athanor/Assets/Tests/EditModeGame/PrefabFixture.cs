using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Athanor.Tests
{
    /// <summary>A copy of a prefab for one test. Call Dispose (or Destroy) in [TearDown].</summary>
    public sealed class PrefabFixture
    {
        public readonly GameObject Root;

        public PrefabFixture(string prefabPath)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            Assert.IsNotNull(prefab, "no prefab at " + prefabPath);
            Root = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        }

        public T Get<T>() where T : Component => Root.GetComponent<T>();

        public void Destroy() { if (Root != null) Object.DestroyImmediate(Root); }
    }
}
