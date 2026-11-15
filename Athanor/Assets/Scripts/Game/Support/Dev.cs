using System.Diagnostics;

namespace Athanor.Game
{
    /// <summary>
    /// Log lines that exist only as the hook of tools/web/webtest.mjs ([tut], [drive], [loc]). The calls, and the strings they build,
    /// are compiled out of a release build; a development build and the editor keep them.
    /// </summary>
    public static class Dev
    {
        [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
        public static void Hook(string line) => UnityEngine.Debug.Log(line);
    }
}
