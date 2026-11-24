using System;

namespace Athanor.Gameplay
{
    // The Inspector shows these; the rules read the strings ContentNames writes. ContentEnumTests pin every list
    // to the name tables in ContentData.cs, so a new effect means a new line in both places.

    /// <summary>Unset (no rarity written) then Rarities.All in order.</summary>
    public enum RarityId { Unset, Common, Uncommon, Rare, Epic, Legendary, Mythic, WorldSeed }

    /// <summary>None (no kind written) then Kind in order. A catalyst's kind filter.</summary>
    public enum KindOrNone { None, Ore, Metal, Crystal, Powder, Potion, Plant, Beast }

    /// <summary>Effects.All in order.</summary>
    public enum CatalystEffect
    {
        KindDouble, CentreTwice, FirstTriple, Diagonals, ExtraTrial, ExtraHand, DiscoveryTriple, EmptyTileBonus, ChainPlus,
        ComboBonus, PairBonus, FlatPerReaction, TierDouble, StarterDouble, ExtraRedraw, HeatPlus, Salvage, ChainFrom,
        DepthScale, FreeDiscovery, AllScale, SecondChance,
    }

    /// <summary>AspectEffects.All in order.</summary>
    public enum AspectEffect { Doubled, Anchor, Heated, Echo, Attuned, Seed, Fallow, Heavy, Cold, Volatile, Greedy, Bridge, OpenBench, Kiln, Crossroads, Pivot }

    /// <summary>Where an aspect acts (AspectData.scope).</summary>
    public enum AspectScope { Tile, Link, Bench }

    /// <summary>The aspect's pool (AspectData.group).</summary>
    public enum AspectGroup { Cheap, Engine }

    /// <summary>Ops.All in order (EffectData.op).</summary>
    public enum EventOp { Trials, Hand, Redraws, RiShift, GiveDiscovery, LoseRack, LoseCard, FirstTriple, Hint, BlockTile, HeatTile, CatalystUpgrade, CatalystGift, Nothing }

    /// <summary>Ops.Types in order. Not called EventType: UnityEngine has one.</summary>
    public enum EventKind { Choice, Gamble, Find, Shrine, Hazard, Boon, Lore }

    /// <summary>None (no requirement, written "") then Ops.Needs in order.</summary>
    public enum EventNeed { None, Rack, Catalyst, Trial, Redraw }

    /// <summary>StatIds.All in order (the ids are camelCase strings; ContentNames maps them).</summary>
    public enum GoalStat { RunsStarted, RunsWon, WonNoRedraw, DepthReached, Ignites, ChainLongest, ComboLargest, BestIgnite, JournalEntries, JournalComplete }

    /// <summary>What a goal pays out.</summary>
    public enum RewardKind { Starter, Catalyst }

    /// <summary>Enum values to and from the strings of ContentData.</summary>
    public static class ContentNames
    {
        /// <summary>The rarity string; "" for Unset.</summary>
        public static string Rarity(RarityId r) => r == RarityId.Unset ? "" : r.ToString();
        /// <summary>The kind string; "" for None.</summary>
        public static string Kind(KindOrNone k) => k == KindOrNone.None ? "" : k.ToString();
        /// <summary>The requirement string; "" for None.</summary>
        public static string Need(EventNeed n) => n == EventNeed.None ? "" : n.ToString();
        /// <summary>The camelCase stat id.</summary>
        public static string Stat(GoalStat s) => StatIds.All[(int)s];
        /// <summary>The reward kind string.</summary>
        public static string Reward(RewardKind k) => k == RewardKind.Starter ? RewardKinds.Starter : RewardKinds.Catalyst;

        /// <summary>The enum value named s; none for "" or null. Throws on an unknown name, so a bad import stops loudly.</summary>
        public static T Parse<T>(string s, T none) where T : struct, Enum => string.IsNullOrEmpty(s) ? none : (T)Enum.Parse(typeof(T), s);

        /// <summary>The GoalStat for a stat id; throws on an unknown id.</summary>
        public static GoalStat ParseStat(string s)
        {
            int i = Array.IndexOf(StatIds.All, s);
            if (i < 0) throw new ArgumentException("unknown stat " + s);
            return (GoalStat)i;
        }

        /// <summary>The RewardKind for a reward string; throws on an unknown string.</summary>
        public static RewardKind ParseReward(string s) => s == RewardKinds.Starter ? RewardKind.Starter
            : s == RewardKinds.Catalyst ? RewardKind.Catalyst : throw new ArgumentException("unknown reward kind " + s);
    }
}
