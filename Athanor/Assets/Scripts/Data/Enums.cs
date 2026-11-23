using System;

namespace Athanor.Data
{
    // ── Rarity ────────────────────────────────────────────────────────────────
    public enum Rarity
    {
        Common = 0,
        Uncommon = 1,
        Rare = 2,
        Legendary = 3,
    }

    // ── Reward kind for goal rewards ──────────────────────────────────────────
    public enum RewardKind
    {
        Starter = 0,
        Catalyst = 1,
    }

    // ── Aspect scope ──────────────────────────────────────────────────────────
    public enum AspectScope
    {
        Tile = 0,
        Link = 1,
        Bench = 2,
    }

    // ── Aspect group (flavour label) ──────────────────────────────────────────
    public enum AspectGroup
    {
        Cheap = 0,
        Engine = 1,
    }

    // ── Stat IDs a goal can watch ─────────────────────────────────────────────
    public enum GoalStatId
    {
        RunsStarted = 0,
        RunsWon = 1,
        WonNoRedraw = 2,
        DepthReached = 3,
        Ignites = 4,
        ChainLongest = 5,
        ComboLargest = 6,
        BestIgnite = 7,
        JournalEntries = 8,
        JournalComplete = 9,
    }
}
