using System;

namespace Athanor.Core
{
    /// <summary>The screens the game moves between.</summary>
    public enum FlowState { Boot, Title, Intro, Bench, Resolving, Journal, Descent, CatalystChoice, Ending, RunOver, AspectChoice, Event, Goals }

    /// <summary>The screen state machine: which screen is up and which moves are allowed. No Unity types.</summary>
    public sealed class ScreenFlow
    {
        FlowState goalsFrom = FlowState.RunOver;

        /// <summary>The current screen; Boot at construction.</summary>
        public FlowState State { get; private set; } = FlowState.Boot;

        /// <summary>Where the open Journal returns to (Bench or Title).</summary>
        public FlowState JournalFrom { get; private set; } = FlowState.Bench;

        /// <summary>True while a cutscene plays.</summary>
        public bool InCutscene { get; set; }

        /// <summary>True while the sandbox is on.</summary>
        public bool Sandbox { get; private set; }

        /// <summary>Raised as (from, to) on a real change; not raised when the state stays the same.</summary>
        public event Action<FlowState, FlowState> Changed;

        /// <summary>Moves to a screen unconditionally; raises Changed if it differs.</summary>
        public void Enter(FlowState state)
        {
            if (state == State) return;
            var from = State;
            State = state;
            Changed?.Invoke(from, state);
        }

        /// <summary>Opens the Journal from the Bench, or from the Title when it is not asking a question.</summary>
        public bool OpenJournal(bool titleAsking)
        {
            if (State == FlowState.Bench || (State == FlowState.Title && !titleAsking))
            {
                JournalFrom = State;
                Enter(FlowState.Journal);
                return true;
            }
            return false;
        }

        /// <summary>Leaves the Journal for JournalFrom.</summary>
        public bool CloseJournal()
        {
            if (State != FlowState.Journal) return false;
            Enter(JournalFrom);
            return true;
        }

        /// <summary>Opens Goals from the Title (when not asking) or from RunOver.</summary>
        public bool OpenGoals(bool titleAsking)
        {
            if (State == FlowState.RunOver || (State == FlowState.Title && !titleAsking))
            {
                goalsFrom = State;
                Enter(FlowState.Goals);
                return true;
            }
            return false;
        }

        /// <summary>Leaves Goals for the screen it was opened from.</summary>
        public bool CloseGoals()
        {
            if (State != FlowState.Goals) return false;
            Enter(goalsFrom);
            return true;
        }

        /// <summary>Bench and canIgnite moves to Resolving.</summary>
        public bool StartIgnite(bool canIgnite)
        {
            if (State != FlowState.Bench || !canIgnite) return false;
            Enter(FlowState.Resolving);
            return true;
        }

        /// <summary>Pausing needs the Bench, bench input, no cutscene and a run in play.</summary>
        public bool CanPause(bool benchInput, bool runPlaying)
            => State == FlowState.Bench && benchInput && !InCutscene && runPlaying;

        /// <summary>Abandoning needs the Bench and a run that has not ended.</summary>
        public bool CanAbandon(bool runEnded) => State == FlowState.Bench && !runEnded;

        /// <summary>Resetting progress is offered on the Title only.</summary>
        public bool CanResetProgress => State == FlowState.Title;

        /// <summary>Turns the sandbox on.</summary>
        public void BeginSandbox() { Sandbox = true; }

        /// <summary>Turns the sandbox off.</summary>
        public void EndSandbox() { Sandbox = false; }
    }
}
