using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The art library the running game uses. GameFlow sets it from its serialized field; the Game tests set it in GameTestSetup.</summary>
    public static class Art
    {
        /// <summary>The library; null until GameFlow.Awake or a test/editor setup assigns it.</summary>
        public static ArtLibrary Lib;

        /// <summary>The colours files share (Assets/Data/Palette.asset); null until GameFlow.Awake or a test/editor setup assigns it.</summary>
        public static Palette Palette;

        /// <summary>Forgets the library and the palette before the first scene (domain reload off).</summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void ResetStatics() { Lib = null; Palette = null; }
    }
}
