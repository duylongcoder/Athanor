using System.Collections;
using Athanor.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Athanor.Tests
{
    public class SingletonTests
    {
        sealed class Probe : Singleton<Probe> { public int awakes; protected override void OnAwake() => awakes++; }

        [TearDown]
        public void DestroyLeftoverProbes()
        {
            foreach (var p in Object.FindObjectsByType<Probe>(FindObjectsInactive.Include)) Object.DestroyImmediate(p.gameObject);
        }

        [UnityTest]
        public IEnumerator Instance_AfterAwake_IsThatComponent()
        {
            // Arrange
            var go = new GameObject("probe");
            // Act
            var p = go.AddComponent<Probe>();
            yield return null;
            // Assert
            Assert.AreSame(p, Probe.Instance);
            Assert.AreEqual(1, p.awakes);
            Object.Destroy(go);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Awake_SecondInstance_IsRemovedAndTheFirstStays()
        {
            // Arrange
            var a = new GameObject("a").AddComponent<Probe>();
            LogAssert.Expect(LogType.Error, new System.Text.RegularExpressions.Regex("second Probe"));
            // Act
            var b = new GameObject("b").AddComponent<Probe>();
            yield return null;
            // Assert
            Assert.AreSame(a, Probe.Instance);
            Assert.IsTrue(b == null, "the second component is destroyed");
            Object.Destroy(a.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator OnDestroy_TheInstance_ClearsIt()
        {
            // Arrange
            var go = new GameObject("probe"); go.AddComponent<Probe>();
            yield return null;
            // Act
            Object.Destroy(go);
            yield return null;
            // Assert
            Assert.IsFalse(Probe.Exists);
            Assert.Throws<System.InvalidOperationException>(() => { var _ = Probe.Instance; });
        }

        [UnityTest]
        public IEnumerator ResetStatics_AfterAStop_ForgetsEveryInstance()
        {
            // Arrange
            var go = new GameObject("probe"); go.AddComponent<Probe>();
            yield return null;
            // Act
            Singletons.ResetStatics();
            // Assert
            Assert.IsFalse(Probe.Exists);
            Object.Destroy(go);
            yield return null;
        }
    }
}
