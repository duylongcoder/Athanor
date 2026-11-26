namespace Athanor.Gameplay
{
    /// <summary>What Begin does about the tutorial: nothing, play it, or ask once whether to skip it.</summary>
    public enum TutorialPlan { None, Run, AskSkip }

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

    /// <summary>When the first run is the tutorial: only the TutorialDone flag decides, plus the skip question for an old save.</summary>
    public static class TutorialGate
    {
        /// <summary>The tutorial is offered until the save has finished (or skipped) it.</summary>
        public static bool ShouldRun(bool tutorialDone) => !tutorialDone;

        /// <summary>Done: None. Not done with an empty journal: Run. Not done with journal entries (a save from before the tutorial): AskSkip, asked once.</summary>
        public static TutorialPlan Plan(bool tutorialDone, int journalEntries)
        {
            if (!ShouldRun(tutorialDone)) return TutorialPlan.None;
            return journalEntries == 0 ? TutorialPlan.Run : TutorialPlan.AskSkip;
        }
    }
}
