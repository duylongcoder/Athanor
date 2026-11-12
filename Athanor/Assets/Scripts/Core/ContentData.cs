using System;

namespace Athanor.Core
{
    public enum Kind { Ore, Metal, Crystal, Powder, Potion, Plant, Beast }

    // Plain serializable shapes: read by JsonUtility in Unity and System.Text.Json in the dotnet tests.
    /// <summary>rarity and locked are for starting materials only: "Common" | "Uncommon" | "Rare"; a locked one must be won from a goal.</summary>
    [Serializable] public class MaterialData { public string id, name, kind, glow; public int tier, reactivity; public string rarity; public bool locked; }
    /// <summary>items: 2 to 5 materials, unordered. The older two-material form (a, b) is still read.</summary>
    [Serializable] public class RecipeData { public string[] items; public int minReactivity; public string result; public string a, b; }
    /// <summary>rarity: "Common" | "Uncommon" | "Rare" | "Legendary" (missing reads as Common). A locked one must be won from a goal.</summary>
    [Serializable] public class CatalystData { public string id, name, text, effect, kind; public float amount; public string rarity; public bool locked; }

    /// <summary>A long-term goal: reach target on a stat, win one random locked item of rewardKind ("starter" | "catalyst") and rewardRarity.</summary>
    [Serializable] public class GoalData { public string id, text, stat, rewardKind, rewardRarity; public int target; }

    /// <summary>A run-long bench modifier. scope: "Tile" | "Link" | "Bench". group: "Cheap" | "Engine".</summary>
    [Serializable] public class AspectData { public string id, name, text, effect, scope, group; public float amount; public bool tradeOff; }

    [Serializable] public class EffectData { public string op, arg; public int amount, amount2; }
    [Serializable] public class EventOutcome { public int weight; public string text; public EffectData[] gain; }
    /// <summary>requires: "" | "Rack" | "Catalyst" | "Trial" | "Redraw". One of outcomes is drawn by weight when the option is taken.</summary>
    [Serializable] public class EventOption { public string label, requires; public EffectData[] cost; public EventOutcome[] outcomes; }
    [Serializable] public class EventData { public string id, name, text, type; public int minDepth, maxDepth, weight; public EventOption[] options; }

    [Serializable]
    public class ContentData
    {
        public MaterialData[] materials;
        public RecipeData[] recipes;
        public CatalystData[] catalysts;
        public int[] depthTargets;
        public string[] depthNames;
        public string ultimate;
        public GoalData[] goals;
        public AspectData[] aspects;
        public EventData[] events;
        public int noEventWeight;
    }

    [Serializable] public class HintData { public string material, partnerKind; public int heat; }   // heat > 0: "needs this much heat", and no partnerKind
    [Serializable] public class JournalData { public string[] known; public HintData[] hints; }

    [Serializable] public class RewardLogData { public string goal, item; }
    [Serializable]
    public class ProfileData
    {
        public int version = 1, rewardSeed;
        public int runsStarted, runsWon, wonNoRedraw, depthReached, ignites, chainLongest, comboLargest;
        public long bestIgnite;
        public string[] completed, unlockedStarters, unlockedCatalysts;
        public RewardLogData[] rewards;
    }

    public static class Rarities
    {
        public const string Common = "Common", Uncommon = "Uncommon", Rare = "Rare", Legendary = "Legendary";
        public static readonly string[] All = { Common, Uncommon, Rare, Legendary };
        public static int Rank(string rarity) => Math.Max(0, Array.IndexOf(All, string.IsNullOrEmpty(rarity) ? Common : rarity));
        public static string Of(string rarity) => string.IsNullOrEmpty(rarity) ? Common : rarity;
    }

    /// <summary>The catalyst effect names used in content.json.</summary>
    public static class Effects
    {
        public const string KindDouble = "KindDouble", CentreTwice = "CentreTwice", FirstTriple = "FirstTriple",
            Diagonals = "Diagonals", ExtraTrial = "ExtraTrial", ExtraHand = "ExtraHand",
            DiscoveryTriple = "DiscoveryTriple", EmptyTileBonus = "EmptyTileBonus", ChainPlus = "ChainPlus",
            // Phase 3
            ComboBonus = "ComboBonus", PairBonus = "PairBonus", FlatPerReaction = "FlatPerReaction", TierDouble = "TierDouble",
            StarterDouble = "StarterDouble", ExtraRedraw = "ExtraRedraw", HeatPlus = "HeatPlus", Salvage = "Salvage",
            ChainFrom = "ChainFrom", DepthScale = "DepthScale", FreeDiscovery = "FreeDiscovery", AllScale = "AllScale",
            SecondChance = "SecondChance";
        public static readonly string[] All =
        {
            KindDouble, CentreTwice, FirstTriple, Diagonals, ExtraTrial, ExtraHand, DiscoveryTriple, EmptyTileBonus, ChainPlus,
            ComboBonus, PairBonus, FlatPerReaction, TierDouble, StarterDouble, ExtraRedraw, HeatPlus, Salvage, ChainFrom,
            DepthScale, FreeDiscovery, AllScale, SecondChance,
        };
    }

    /// <summary>The aspect effect names used in content.json.</summary>
    public static class AspectEffects
    {
        public const string Doubled = "Doubled", Anchor = "Anchor", Heated = "Heated", Echo = "Echo", Attuned = "Attuned", Seed = "Seed",
            Fallow = "Fallow", Heavy = "Heavy", Cold = "Cold", Volatile = "Volatile", Greedy = "Greedy",
            Bridge = "Bridge", OpenBench = "OpenBench", Kiln = "Kiln", Crossroads = "Crossroads", Pivot = "Pivot";
        public static readonly string[] All =
        {
            Doubled, Anchor, Heated, Echo, Attuned, Seed, Fallow, Heavy, Cold, Volatile, Greedy, Bridge, OpenBench, Kiln, Crossroads, Pivot,
        };
    }

    /// <summary>The event effect names (EffectData.op) and option requirements used in content.json.</summary>
    public static class Ops
    {
        public const string Trials = "Trials", Hand = "Hand", Redraws = "Redraws", RiShift = "RiShift", GiveDiscovery = "GiveDiscovery",
            LoseRack = "LoseRack", LoseCard = "LoseCard", FirstTriple = "FirstTriple", Hint = "Hint", BlockTile = "BlockTile",
            HeatTile = "HeatTile", CatalystUpgrade = "CatalystUpgrade", CatalystGift = "CatalystGift", Nothing = "Nothing";
        public static readonly string[] All =
        {
            Trials, Hand, Redraws, RiShift, GiveDiscovery, LoseRack, LoseCard, FirstTriple, Hint, BlockTile, HeatTile,
            CatalystUpgrade, CatalystGift, Nothing,
        };
        public const string NeedRack = "Rack", NeedCatalyst = "Catalyst", NeedTrial = "Trial", NeedRedraw = "Redraw";
        public static readonly string[] Needs = { NeedRack, NeedCatalyst, NeedTrial, NeedRedraw };
        public static readonly string[] Types = { "Choice", "Gamble", "Find", "Shrine", "Hazard", "Boon", "Lore" };
    }

    /// <summary>The stats a goal can watch.</summary>
    public static class StatIds
    {
        public const string RunsStarted = "runsStarted", RunsWon = "runsWon", WonNoRedraw = "wonNoRedraw", DepthReached = "depthReached",
            Ignites = "ignites", ChainLongest = "chainLongest", ComboLargest = "comboLargest", BestIgnite = "bestIgnite",
            JournalEntries = "journalEntries", JournalComplete = "journalComplete";
        public static readonly string[] All =
        {
            RunsStarted, RunsWon, WonNoRedraw, DepthReached, Ignites, ChainLongest, ComboLargest, BestIgnite, JournalEntries, JournalComplete,
        };
    }
}
