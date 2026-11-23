using System;

namespace Athanor.Data
{
    /// <summary>A single recipe definition for the game.</summary>
    [Serializable]
    public class RecipeSO
    {
        /// <summary>Required materials in order (2–5 entries, unordered in game logic).</summary>
        public string[] items = Array.Empty<string>();

        /// <summary>Minimum combined reactivity needed to craft this recipe.</summary>
        public int minReactivity = 0;

        /// <summary>Result material ID produced by this recipe.</summary>
        public string result = "";
    }
}
