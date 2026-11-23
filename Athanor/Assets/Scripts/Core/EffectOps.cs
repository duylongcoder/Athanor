using System;

namespace Athanor.Core
{
    // ── Ops (effects inside events) ──────────────────────────────────────────
    public enum EffectOp
    {
        Trials,       Hand,             Redraws,         RiShift,
        GiveDiscovery, LoseRack,        LoseCard,        FirstTriple,
        Hint,         BlockTile,        HeatTile,        CatalystUpgrade,
        CatalystGift, Nothing
    }

    // ── Need type for event options ───────────────────────────────────────────
    public enum EventNeedType
    {
        None = 0,          // empty string → no requirement
        Rack,
        Catalyst,
        Trial,
        Redraw,
    }

    // ── Event types ───────────────────────────────────────────────────────────
    public enum EventType
    {
        Choice, Gamble, Find, Shrine, Hazard, Boon, Lore
    }

    // ── Rarity strings already defined in ContentData.cs as Rarities.* constants
    //    kept for backward compatibility. New code uses string where appropriate.

    // ── Kind already defined above ────────────────────────────────────────────
}
