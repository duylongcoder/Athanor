using System;
using System.Collections.Generic;
using System.Linq;
using Athanor.Gameplay;
using UnityEngine;
using Hint = UnityEngine.TooltipAttribute;   // not "Tooltip": the game has a Tooltip view of its own

namespace Athanor.Game
{
    /// <summary>
    /// The whole tutorial as data: its run settings, the scripted trials, the steps (text and lit areas) and the lesson order.
    /// TutorialManager plays it; nothing in the manager names a step id.
    /// </summary>
    [CreateAssetMenu(menuName = "Athanor/Tutorial Sequence", fileName = "Tutorial")]
    public sealed class TutorialSequence : ScriptableObject
    {
        [Header("Run")]
        [Hint("The tutorial run's seed.")] public int seed;
        [Hint("No aspect offer and no event before this depth, so nothing touches the scripted hands.")] public int quietBefore;
        [Hint("The first catalyst offer.")] public CatalystDefinition[] firstOffer = new CatalystDefinition[0];

        [Header("Scripted trials (index = trial number)")]
        public TutorialTrialDef[] trials = new TutorialTrialDef[0];
        [Hint("The trial after the first catalyst choice, by the catalyst taken.")] public CatalystTrialDef[] catalystTrials = new CatalystTrialDef[0];

        [Header("The catalyst offer")]
        [Hint("Read before the cards open.")] public string[] offerLines = new string[0];
        [Hint("Shown while the player picks (taken down when the card is taken).")] public string offerHold = "";

        [Header("Steps and script")]
        public List<TutorialStepDef> steps = new List<TutorialStepDef>();
        public List<TutorialAction> script = new List<TutorialAction>();

        public TutorialStepDef Step(string id) => steps.FirstOrDefault(s => s.id == id) ?? throw new InvalidOperationException("No tutorial step " + id + " in " + name);

        public TutorialTrial Trial(int i) => ToTrial(trials[i]);

        /// <summary>The trial after choosing this catalyst; null for a catalyst not in the list.</summary>
        public TutorialTrial CatalystTrial(string catalystId) => ToTrial(CatalystTrialDef(catalystId));

        public TutorialTrialDef CatalystTrialDef(string catalystId) =>
            catalystTrials.FirstOrDefault(c => c.catalyst != null && c.catalyst.id == catalystId)?.trial;

        public string[] FirstOfferIds() => firstOffer.Select(c => c.id).ToArray();

        /// <summary>The run options of the tutorial run: the game's, with the quiet start and the fixed first offer.</summary>
        public RunOptions Options(Unlocks unlocks) => new RunOptions
        {
            Unlocks = unlocks, Aspects = true, Events = true, QuietBefore = quietBefore, FirstOffer = FirstOfferIds(),
        };

        public static TutorialTrial ToTrial(TutorialTrialDef d) => d == null ? null : new TutorialTrial
        {
            Hand = d.hand.Select(m => m.id).ToArray(),
            Lay = d.lay.Select(l => new TutorialLay(l.material.id, l.tile)).ToArray(),
            Makes = d.makes.Select(m => m.id).ToArray(),
        };

        /// <summary>The bench of a trial, 9 tiles, null where empty.</summary>
        public static string[] Layout(TutorialTrial t) => Layout(t.Lay);

        /// <summary>The trial without its last lay: the broken chain that Broken compares against.</summary>
        public static string[] BrokenLayout(TutorialTrial t) => Layout(t.Lay.Take(t.Lay.Length - 1).ToArray());

        static string[] Layout(TutorialLay[] lay)
        {
            var tiles = new string[Bench.TileCount];
            foreach (var l in lay) tiles[l.Tile] = l.Id;
            return tiles;
        }
    }
}
