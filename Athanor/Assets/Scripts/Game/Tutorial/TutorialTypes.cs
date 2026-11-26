using System;
using System.Collections.Generic;
using UnityEngine;
using Hint = UnityEngine.TooltipAttribute;   // not "Tooltip": the game has a Tooltip view of its own

namespace Athanor.Game
{
    /// <summary>Pure rectangle math for tutorial areas (art pixels, top-left origin). No scene needed, so it is tested alone.</summary>
    public static class TutorialRect
    {
        const float Half = 0.5f;

        /// <summary>Grows r by margin = (left, top, right, bottom) pixels.</summary>
        public static Rect Grow(Rect r, Vector4 m) =>
            new Rect(r.x - m.x, r.y - m.y, r.width + m.x + m.z, r.height + m.y + m.w);

        /// <summary>The art-pixel rect (top-left, size) of an area whose world centre is given and whose size is in pixels.</summary>
        public static Rect ArtRect(Vector2 worldCentre, Vector2 size)
        {
            float cx = worldCentre.x + Px.W * Half, cy = Px.H * Half - worldCentre.y;
            // to 1/100 px: a transform at 125.99999 must not floor to 124 in the hole snap
            float R(float v) => Mathf.Round(v * 100f) / 100f;
            return new Rect(R(cx - size.x * Half), R(cy - size.y * Half), size.x, size.y);
        }
    }

    /// <summary>What a tutorial area lights.</summary>
    public enum AreaKind { Fixed, ScriptTile, View }

    /// <summary>A named game view an area can follow (the rect comes from the view at run time).</summary>
    public enum ViewRef { Bench, Redraw, JournalCell, JournalRecipes, JournalIndex, JournalHints, EventHub }

    /// <summary>Which scripted hand's tiles a ScriptTile area follows.</summary>
    public enum ScriptTrial { Reaction, Miss, Chain, Catalyst }

    /// <summary>One lit window of a step. Fixed uses rect; ScriptTile and View follow the game at run time.</summary>
    [Serializable]
    public sealed class TutorialAreaDef
    {
        public string name = "";
        public AreaKind kind = AreaKind.Fixed;
        [Hint("Fixed: art pixels, top-left origin (x, y, width, height).")] public Rect rect;
        [Hint("ScriptTile: the scripted hand.")] public ScriptTrial trial = ScriptTrial.Reaction;
        [Hint("ScriptTile: index in the hand's lay; -1 = all of its tiles.")] public int slot;
        [Hint("View: the view to follow.")] public ViewRef view = ViewRef.Bench;
        [Hint("View JournalCell: the material shown in the Journal.")] public string cellId = "";
        [Hint("Pixels added on left, top, right, bottom.")] public Vector4 margin;
        [Hint("Clicks in this window reach the game. Off = highlight only.")] public bool clicksPass;
    }

    /// <summary>One tutorial line: its text, whether the screen darkens and locks, and what it lights.</summary>
    [Serializable]
    public sealed class TutorialStepDef
    {
        [Hint("The id the script names, e.g. 1.4 or 5.4i.")] public string id = "";
        [Hint("Lang key, e.g. tutorial.1_4. Two steps may share one.")] public string textKey = "";
        [Hint("Dim the screen outside the areas.")] public bool dark = true;
        [Hint("Clicks on the dark pulse the rim; clicks in pass-through areas reach the game.")] public bool lockPad = true;
        [Hint("Read steps: a click in a non-pass area advances the text. Off: it only pulses the rim.")] public bool clickAdvances = true;
        [Hint("Own text box position (art pixels, top-left). Off = TutorialStyle.boxPos.")] public bool useBoxPos;
        public Vector2 boxPos;
        [Hint("In order; the arrow sits over the last one.")] public List<TutorialAreaDef> areas = new List<TutorialAreaDef>();
    }

    /// <summary>What one entry of the lesson script does.</summary>
    public enum TutorialActionKind
    {
        /// <summary>Wait until the bench is the player's and the trial counter is <c>count</c>.</summary>
        WaitBench,
        /// <summary>Show <c>step</c> and wait for a click.</summary>
        Say,
        /// <summary>Show <c>step</c> until the first <c>count</c> lays of the current trial are on the bench (-1 = all).</summary>
        AwaitLay,
        /// <summary>Show <c>step</c> until the player Ignites, then wait for the Ignite to play out.</summary>
        Ignite,
        /// <summary>Show <c>step</c> until the Journal opens.</summary>
        AwaitJournal,
        /// <summary>Open the Journal page of the step's first JournalCell area, then Say.</summary>
        SayOnJournalPage,
        /// <summary>Open the page, show <c>step</c> and keep it up while the Journal stays open.</summary>
        HoldWhileJournal,
        /// <summary>Work out the score of the current trial without its last lay (shown later by Broken). Nothing is spent.</summary>
        MeasureBrokenChain,
        /// <summary>Show <c>step</c> over the current trial without its last lay, greyed, beside the measured score.</summary>
        Broken,
        /// <summary>Show <c>step</c> with one line per reaction of the last Ignite: base x first x chain = total.</summary>
        ChainSummary,
        /// <summary>Wait until the Ignite has played out and the bench is back.</summary>
        WaitResolved,
    }

    [Serializable]
    public struct TutorialAction
    {
        public TutorialActionKind kind;
        [Hint("The step this action shows (its id).")] public string step;
        [Hint("AwaitLay: lays needed (-1 = all). WaitBench: the trial index.")] public int count;
    }

    [Serializable]
    public struct TutorialLayDef
    {
        public Athanor.Gameplay.MaterialDefinition material;
        [Range(0, 8)] public int tile;
    }

    /// <summary>One scripted trial and the lines it plays.</summary>
    [Serializable]
    public sealed class TutorialTrialDef
    {
        public string name = "";
        [Hint("The hand dealt for this trial.")] public Athanor.Gameplay.MaterialDefinition[] hand = new Athanor.Gameplay.MaterialDefinition[0];
        [Hint("Where each material goes (this tile, not any lit tile: another order can break the chain).")] public TutorialLayDef[] lay = new TutorialLayDef[0];
        [Hint("What the Ignite makes, in chain order (empty for the miss). TutorialStepsBakeTests.EveryStepId_UsedByTheScript_ExistsInTheSequence checks it.")] public Athanor.Gameplay.MaterialDefinition[] makes = new Athanor.Gameplay.MaterialDefinition[0];
        [Hint("Shown with the score parts after each reaction (empty: none).")] public string reactionStep = "";
        [Hint("Read after reactionStep (empty: none).")] public string reactionFollowUp = "";
        [Hint("reactionStep uses the 'tutorial.step' text with the chain step number.")] public bool reactionChainText;
        [Hint("Shown when an Ignite of this trial fizzles (no reaction, not a sputter).")] public string fizzleStep = "";
        [Hint("Played after this trial's Ignite, before the next hand is dealt.")] public List<TutorialAction> afterIgnite = new List<TutorialAction>();
    }

    [Serializable]
    public sealed class CatalystTrialDef
    {
        public Athanor.Gameplay.CatalystDefinition catalyst;
        public TutorialTrialDef trial = new TutorialTrialDef();
    }
}
