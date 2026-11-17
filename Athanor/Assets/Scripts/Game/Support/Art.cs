namespace Athanor.Game
{
    /// <summary>The art library the running game uses. GameFlow sets it from its serialized field; the Game tests set it in GameTestSetup.</summary>
    public static class Art
    {
        /// <summary>The library; null until GameFlow.Awake or a test/editor setup assigns it.</summary>
        public static ArtLibrary Lib;
    }
}
