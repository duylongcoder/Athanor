using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// Silences audio while the browser tab is hidden, the app is paused or the window has lost focus. No sound ships in
    /// Phase 5, so this is a hook: it sets AudioListener.pause and is audible once sound lands.
    /// </summary>
    public static class AudioMute
    {
        static bool hidden, appPaused, focused = true;
        /// <summary>Tests read the result here instead of AudioListener.</summary>
        public static Action<bool> Sink;
        public static bool Muted { get; private set; }

        public static bool ShouldMute(bool tabHidden, bool paused, bool hasFocus) => tabHidden || paused || !hasFocus;

        public static void SetHidden(bool v) { hidden = v; Apply(); }
        public static void SetPaused(bool v) { appPaused = v; Apply(); }
        public static void SetFocus(bool v) { focused = v; Apply(); }
        public static void Reset() { hidden = appPaused = false; focused = true; Muted = false; }

        static void Apply()
        {
            Muted = ShouldMute(hidden, appPaused, focused);
            if (Sink != null) Sink(Muted); else AudioListener.pause = Muted;
        }
    }

    /// <summary>Feeds the browser's tab visibility and Unity's pause and focus into <see cref="AudioMute"/>.</summary>
    public sealed class AudioMuteHook : MonoBehaviour
    {
        delegate void VisibilityCallback(int hiddenTab);
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")] static extern void AthanorWatchVisibility(VisibilityCallback cb);

        [AOT.MonoPInvokeCallback(typeof(VisibilityCallback))]
        static void OnVisibility(int hiddenTab) => AudioMute.SetHidden(hiddenTab == 1);

        void Awake() => AthanorWatchVisibility(OnVisibility);
#endif
        void OnApplicationPause(bool paused) => AudioMute.SetPaused(paused);
        void OnApplicationFocus(bool focus) => AudioMute.SetFocus(focus);
    }
}
