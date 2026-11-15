using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// Short vibration pulses for rare, meaningful moments (never clicks or Ignite). Only where the browser can vibrate
    /// (Android Chrome); <see cref="Supported"/> is false on desktop, iOS and in the editor, and the Settings row is hidden then.
    /// </summary>
    public static class Haptics
    {
        public const int Discovery = 20, DepthCleared = 40, RunEnded = 60;

        /// <summary>The player's setting.</summary>
        public static bool Enabled = true;
        /// <summary>Tests set this to pretend a device that can vibrate; null means ask the platform.</summary>
        public static bool? ForceSupported;
        /// <summary>Tests read pulses here instead of the browser.</summary>
        public static Action<int> Sink;

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")] static extern int AthanorCanVibrate();
        [DllImport("__Internal")] static extern void AthanorVibrate(int ms);
#endif

        public static bool Supported
        {
            get
            {
                if (ForceSupported.HasValue) return ForceSupported.Value;
#if UNITY_WEBGL && !UNITY_EDITOR
                return AthanorCanVibrate() == 1;
#else
                return false;
#endif
            }
        }

        /// <summary>How long to vibrate: 0 unless the player wants it, the device can and the pulse has a length.</summary>
        public static int Plan(bool enabled, bool supported, int ms) => enabled && supported && ms > 0 ? ms : 0;

        public static void Pulse(int ms)
        {
            int n = Plan(Enabled, Supported, ms);
            if (n == 0) return;
            Sink?.Invoke(n);
#if UNITY_WEBGL && !UNITY_EDITOR
            AthanorVibrate(n);
#endif
        }
    }
}
