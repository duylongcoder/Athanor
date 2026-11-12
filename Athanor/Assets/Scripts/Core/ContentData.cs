using System;

namespace Athanor.Core
{
    public enum Kind { Ore, Metal, Crystal, Powder, Potion, Plant, Beast }

    // Plain serializable shapes: read by JsonUtility in Unity and System.Text.Json in the dotnet tests.
    [Serializable] public class MaterialData { public string id, name, kind, glow; public int tier, reactivity; }
    /// <summary>items: 2 to 5 materials, unordered. The older two-material form (a, b) is still read.</summary>
    [Serializable] public class RecipeData { public string[] items; public int minReactivity; public string result; public string a, b; }
    [Serializable] public class CatalystData { public string id, name, text, effect, kind; public float amount; }

    [Serializable]
    public class ContentData
    {
        public MaterialData[] materials;
        public RecipeData[] recipes;
        public CatalystData[] catalysts;
        public int[] depthTargets;
        public string[] depthNames;
        public string ultimate;
    }

    [Serializable] public class HintData { public string material, partnerKind; public int heat; }   // heat > 0: "needs this much heat", and no partnerKind
    [Serializable] public class JournalData { public string[] known; public HintData[] hints; }

    /// <summary>The catalyst effect names used in content.json.</summary>
    public static class Effects
    {
        public const string KindDouble = "KindDouble", CentreTwice = "CentreTwice", FirstTriple = "FirstTriple",
            Diagonals = "Diagonals", ExtraTrial = "ExtraTrial", ExtraHand = "ExtraHand",
            DiscoveryTriple = "DiscoveryTriple", EmptyTileBonus = "EmptyTileBonus", ChainPlus = "ChainPlus";
    }
}
