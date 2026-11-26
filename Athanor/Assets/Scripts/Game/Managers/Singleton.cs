using System;
using System.Collections.Generic;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// Forgets every manager instance when Play starts. Domain reload is off (EditorSettings), so statics survive Stop and Play;
    /// without this a second session would find the destroyed managers of the first.
    /// </summary>
    public static class Singletons
    {
        static readonly List<Action> clears = new List<Action>();

        internal static void Track(Action clear) { if (!clears.Contains(clear)) clears.Add(clear); }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void ResetStatics()
        {
            foreach (var c in clears) c();
            clears.Clear();
        }
    }

    /// <summary>
    /// A manager that exists once, in Main.unity › Managers. There is no lazy creation and no DontDestroyOnLoad: the scene owns it.
    /// Instance throws when the manager is not in the scene, so a missing manager fails at once rather than as a null later.
    /// Derived classes put their start-up in OnAwake and call base.OnDestroy.
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        static T instance;

        /// <summary>The one manager; throws when none is in the scene.</summary>
        public static T Instance => instance != null ? instance
            : throw new InvalidOperationException(typeof(T).Name + " is not in the scene (Main.unity)");

        /// <summary>True while a manager of this type is alive.</summary>
        public static bool Exists => instance != null;

        static void Clear() => instance = null;

        protected void Awake()
        {
            if (instance != null && instance != this)
            {
                Debug.LogError("A second " + typeof(T).Name + " on " + name + " was removed; " + instance.name + " is the one in use.", this);
                Destroy(this);
                return;
            }
            instance = (T)this;
            Singletons.Track(Clear);
            OnAwake();
        }

        protected virtual void OnAwake() { }

        protected virtual void OnDestroy()
        {
            if (instance == this) instance = null;
        }
    }
}
