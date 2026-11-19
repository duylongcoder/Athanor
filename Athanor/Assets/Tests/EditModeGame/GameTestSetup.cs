using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>Once per run of the Game tests: the language files, art library and particle material the game would get from GameFlow and Sparks.</summary>
    [SetUpFixture]
    public sealed class GameTestSetup
    {
        [OneTimeSetUp]
        public void Inject() => EditorGlobals.Inject();
    }
}
