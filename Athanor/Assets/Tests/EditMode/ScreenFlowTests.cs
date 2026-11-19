using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    public class ScreenFlowTests
    {
        static ScreenFlow At(FlowState s) { var f = new ScreenFlow(); f.Enter(s); return f; }

        [Test] public void OpenJournal_FromBench_ClosesBackToBench()
        {
            var f = At(FlowState.Bench);
            Assert.IsTrue(f.OpenJournal(false));
            Assert.AreEqual(FlowState.Journal, f.State);
            Assert.IsTrue(f.CloseJournal());
            Assert.AreEqual(FlowState.Bench, f.State);
        }
        [Test] public void OpenJournal_FromTitle_ClosesBackToTitle()
        {
            var f = At(FlowState.Title);
            Assert.IsTrue(f.OpenJournal(false));
            Assert.AreEqual(FlowState.Title, f.JournalFrom);
            f.CloseJournal();
            Assert.AreEqual(FlowState.Title, f.State);
        }
        [Test] public void OpenJournal_WhileTitleAsks_IsRefused()
        {
            var f = At(FlowState.Title);
            Assert.IsFalse(f.OpenJournal(true));
            Assert.AreEqual(FlowState.Title, f.State);
        }
        [Test] public void OpenJournal_FromResolving_IsRefused()
        {
            var f = At(FlowState.Resolving);
            Assert.IsFalse(f.OpenJournal(false));
            Assert.AreEqual(FlowState.Resolving, f.State);
        }
        [Test] public void CloseJournal_NotInJournal_ChangesNothing()
        {
            var f = At(FlowState.Bench);
            Assert.IsFalse(f.CloseJournal());
            Assert.AreEqual(FlowState.Bench, f.State);
        }
        [Test] public void OpenGoals_FromRunOver_ClosesBackToRunOver()
        {
            var f = At(FlowState.RunOver);
            Assert.IsTrue(f.OpenGoals(false));
            Assert.AreEqual(FlowState.Goals, f.State);
            Assert.IsTrue(f.CloseGoals());
            Assert.AreEqual(FlowState.RunOver, f.State);
        }
        [Test] public void OpenGoals_FromBench_IsRefused()
        {
            var f = At(FlowState.Bench);
            Assert.IsFalse(f.OpenGoals(false));
            Assert.AreEqual(FlowState.Bench, f.State);
        }
        [Test] public void StartIgnite_BenchAndCanIgnite_GoesToResolving()
        {
            var f = At(FlowState.Bench);
            Assert.IsTrue(f.StartIgnite(true));
            Assert.AreEqual(FlowState.Resolving, f.State);
        }
        [Test] public void StartIgnite_CannotIgnite_StaysOnBench()
        {
            var f = At(FlowState.Bench);
            Assert.IsFalse(f.StartIgnite(false));
            Assert.AreEqual(FlowState.Bench, f.State);
        }
        [Test] public void StartIgnite_FromJournal_IsRefused()
        {
            var f = At(FlowState.Journal);
            Assert.IsFalse(f.StartIgnite(true));
            Assert.AreEqual(FlowState.Journal, f.State);
        }
        [Test] public void CanPause_BenchInputNoCutscenePlaying_IsTrue()
            => Assert.IsTrue(At(FlowState.Bench).CanPause(true, true));
        [Test] public void CanPause_InCutscene_IsFalse()
        {
            var f = At(FlowState.Bench); f.InCutscene = true;
            Assert.IsFalse(f.CanPause(true, true));
        }
        [Test] public void CanPause_InputOff_IsFalse() => Assert.IsFalse(At(FlowState.Bench).CanPause(false, true));
        [Test] public void CanPause_RunNotPlaying_IsFalse() => Assert.IsFalse(At(FlowState.Bench).CanPause(true, false));
        [Test] public void CanPause_NotOnBench_IsFalse() => Assert.IsFalse(At(FlowState.Title).CanPause(true, true));
        [Test] public void CanAbandon_RunEnded_IsFalse() => Assert.IsFalse(At(FlowState.Bench).CanAbandon(true));
        [Test] public void CanResetProgress_OnTitle_IsTrue()
        {
            Assert.IsTrue(At(FlowState.Title).CanResetProgress);
            Assert.IsFalse(At(FlowState.Bench).CanResetProgress);
        }
        [Test] public void Changed_Enter_ReportsFromAndTo()
        {
            var f = new ScreenFlow(); FlowState a = FlowState.Bench, b = FlowState.Bench; int n = 0;
            f.Changed += (x, y) => { a = x; b = y; n++; };
            f.Enter(FlowState.Title);
            Assert.AreEqual(1, n);
            Assert.AreEqual(FlowState.Boot, a);
            Assert.AreEqual(FlowState.Title, b);
        }
        [Test] public void Changed_EnterSameState_IsNotRaised()
        {
            var f = At(FlowState.Title); int n = 0;
            f.Changed += (x, y) => n++;
            f.Enter(FlowState.Title);
            Assert.AreEqual(0, n);
        }
        [Test] public void Sandbox_BeginThenEnd_IsFalseAgain()
        {
            var f = new ScreenFlow();
            f.BeginSandbox(); Assert.IsTrue(f.Sandbox);
            f.EndSandbox(); Assert.IsFalse(f.Sandbox);
        }
    }
}
