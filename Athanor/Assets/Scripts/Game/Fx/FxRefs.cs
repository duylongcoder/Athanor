using System;

namespace Athanor.Game
{
    /// <summary>The scene's two effect players, handed to every view that flashes, shakes or throws sparks (no statics).</summary>
    public sealed class FxRefs
    {
        /// <summary>Flash, shake and fades.</summary>
        public readonly ScreenFx Screen;
        /// <summary>Bursts, trails and embers.</summary>
        public readonly Sparks Sparks;

        public FxRefs(ScreenFx screen, Sparks sparks)
        {
            if (screen == null) throw new ArgumentNullException(nameof(screen));
            if (sparks == null) throw new ArgumentNullException(nameof(sparks));
            Screen = screen; Sparks = sparks;
        }
    }
}
