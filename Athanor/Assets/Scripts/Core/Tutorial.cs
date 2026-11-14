using System.Linq;

namespace Athanor.Core
{
    /// <summary>
    /// One material laid on one tile. This material goes on this tile, not just any lit tile: another arrangement of the
    /// same materials can break the chain (trial 3 then scores 40 and does not clear the depth) or move the hint to the
    /// other material (trial 2).
    /// </summary>
    public readonly struct TutorialLay
    {
        public readonly string Id;
        public readonly int Tile;
        public TutorialLay(string id, int tile) { Id = id; Tile = tile; }
    }

    /// <summary>One scripted trial: the hand dealt, where each material goes, and what the Ignite makes, in chain order.</summary>
    public sealed class TutorialTrial
    {
        public string[] Hand;
        public TutorialLay[] Lay;
        public string[] Makes;   // empty for the miss
    }

    /// <summary>
    /// The tutorial (docs/devlogs/phase-4-tutorial-script.md, v2) as data. Every number the script shows is pinned by
    /// TutorialTests; change both together.
    /// </summary>
    public static class TutorialScript
    {
        public const int Seed = 20261113;
        /// <summary>No aspect offer and no event before this depth, so nothing touches the scripted hands.</summary>
        public const int QuietBefore = 3;
        public static readonly string[] Offer = { "bellows", "heart_tile", "first_spark" };

        /// <summary>Trial 1: a reaction. 40 potency.</summary>
        public static readonly TutorialTrial Reaction = new TutorialTrial
        {
            Hand = new[] { "ember_coal", "deep_tear", "iron_ore", "moondust", "bone_ash" },
            Lay = new[] { new TutorialLay("ember_coal", 4), new TutorialLay("deep_tear", 5) },
            Makes = new[] { "steam_wisp" },
        };

        /// <summary>Trial 2: a miss. Glowmoss is on the lower tile so the hint is written on its page.</summary>
        public static readonly TutorialTrial Miss = new TutorialTrial
        {
            Hand = new[] { "glowmoss", "iron_ore", "sunbloom", "moondust", "bone_ash" },
            Lay = new[] { new TutorialLay("glowmoss", 3), new TutorialLay("iron_ore", 4) },
            Makes = new string[0],
        };

        /// <summary>Trial 3: a chain, 40 then 240, which clears depth 1 (320 of 300).</summary>
        public static readonly TutorialTrial Chain = new TutorialTrial
        {
            Hand = new[] { "iron_ore", "ember_coal", "moondust", "bone_ash", "sunbloom" },
            Lay = new[] { new TutorialLay("iron_ore", 3), new TutorialLay("ember_coal", 4), new TutorialLay("moondust", 5) },
            Makes = new[] { "sky_iron", "starmetal_ore" },
        };

        public static readonly TutorialTrial[] Depth1 = { Reaction, Miss, Chain };

        /// <summary>The bench of a trial, 9 tiles, null where empty.</summary>
        public static string[] Layout(TutorialTrial trial) => Layout(trial.Lay);

        /// <summary>Trial 3 without its last material (Moondust): the broken chain that step 3.6 compares against.</summary>
        public static string[] BrokenChain() => Layout(Chain.Lay.Take(Chain.Lay.Length - 1).ToArray());

        static string[] Layout(TutorialLay[] lay)
        {
            var tiles = new string[Bench.TileCount];
            foreach (var l in lay) tiles[l.Tile] = l.Id;
            return tiles;
        }

        /// <summary>The first trial of depth 2, laid so the chosen catalyst shows. Null for a catalyst not on the offer.</summary>
        public static TutorialTrial CatalystTrial(string catalystId)
        {
            switch (catalystId)
            {
                case "bellows": return new TutorialTrial   // Ember Coal is an Ore: x2, 80
                {
                    Hand = new[] { "ember_coal", "bone_ash", "glowmoss", "sunbloom", "deep_tear" },
                    Lay = new[] { new TutorialLay("ember_coal", 4), new TutorialLay("bone_ash", 5) },
                    Makes = new[] { "sulfur" },
                };
                case "heart_tile": return new TutorialTrial   // touches the centre: x2, 80
                {
                    Hand = new[] { "deep_tear", "moondust", "glowmoss", "sunbloom", "bone_ash" },
                    Lay = new[] { new TutorialLay("deep_tear", 4), new TutorialLay("moondust", 5) },
                    Makes = new[] { "frost_salt" },
                };
                case "first_spark": return new TutorialTrial   // the first reaction: x3, 120
                {
                    Hand = new[] { "sunbloom", "moondust", "glowmoss", "deep_tear", "bone_ash" },
                    Lay = new[] { new TutorialLay("sunbloom", 4), new TutorialLay("moondust", 5) },
                    Makes = new[] { "pixie_wing" },
                };
                default: return null;
            }
        }

        /// <summary>The run options of the tutorial run: the game's, with the quiet start and the fixed first offer.</summary>
        public static RunOptions Options(Unlocks unlocks) => new RunOptions
        {
            Unlocks = unlocks, Aspects = true, Events = true, QuietBefore = QuietBefore, FirstOffer = Offer,
        };

        /// <summary>The tutorial plays on a save that has never finished it and whose journal is empty.</summary>
        public static bool ShouldRun(bool tutorialDone, int journalEntries) => !tutorialDone && journalEntries == 0;
    }
}
