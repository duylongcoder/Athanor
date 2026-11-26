using System;
using System.Linq;
using System.Reflection;
using Athanor.Game;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Athanor.Tests
{
    /// <summary>Main.unity opened beside the test scene, for the views that are scene objects and not prefabs. Dispose closes it without saving.</summary>
    public sealed class MainSceneFixture : IDisposable
    {
        const string Path = "Assets/Scenes/Main.unity";
        readonly Scene scene;

        /// <summary>The GameManager of the scene.</summary>
        public readonly GameManager Flow;

        /// <summary>Opens Main.unity additively; fails the test when it has no GameManager.</summary>
        public MainSceneFixture()
        {
            scene = EditorSceneManager.OpenScene(Path, OpenSceneMode.Additive);
            Flow = scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<GameManager>(true)).SingleOrDefault();
            if (Flow == null)
            {
                EditorSceneManager.CloseScene(scene, true);
                Assert.Fail("no GameManager in " + Path);
            }
        }

        /// <summary>The serialized (private) field of an object, e.g. a view's references to its parts.</summary>
        public static T Field<T>(object owner, string name)
        {
            var f = owner.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            Assert.IsNotNull(f, owner.GetType().Name + " has no field " + name);
            return (T)f.GetValue(owner);
        }

        /// <summary>The component GameManager holds in its serialized field.</summary>
        public T FlowField<T>(string name) where T : Component => Field<T>(Flow, name);

        /// <summary>Every component of type T in the scene (inactive ones too).</summary>
        public T[] All<T>() where T : Component => scene.GetRootGameObjects().SelectMany(g => g.GetComponentsInChildren<T>(true)).ToArray();

        /// <summary>Closes the scene without saving, so a test's changes never reach Main.unity.</summary>
        public void Dispose() => EditorSceneManager.CloseScene(scene, true);
    }
}
