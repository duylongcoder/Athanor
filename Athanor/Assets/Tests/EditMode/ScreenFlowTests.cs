using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    public class ScreenFlowTests
    {
        static ScreenFlow At(FlowState s) { var f = new ScreenFlow(); f.Enter(s); return f; }

        [Test] public void OpenJournal_FromBench_ClosesBackToBench()
        {
            // Arrange
            var f = At(FlowState.Bench);

            // Act
            Assert.IsTrue(f.OpenJournal(false));
            Assert.AreEqual(FlowState.Journal, f.State);
            Assert.IsTrue(f.CloseJournal());

            // Assert
            Assert.AreEqual(FlowState.Bench, f.State);
        }
        [Test] public void OpenJournal_FromTitle_ClosesBackToTitle()
        {
            // Arrange
            var f = At(FlowState.Title);

            // Act
            Assert.IsTrue(f.OpenJournal(false));
            Assert.AreEqual(FlowState.Title, f.JournalFrom);
            f.CloseJournal();

            // Assert
            Assert.AreEqual(FlowState.Title, f.State);
        }
        [Test] public void OpenJournal_WhileTitleAsks_IsRefused()
        {
            // Arrange
            var f = At(FlowState.Title);

            // Act
            Assert.IsFalse(f.OpenJournal(true));

            // Assert
            Assert.AreEqual(FlowState.Title, f.State);
        }
        [Test] public void OpenJournal_FromResolving_IsRefused()
        {
            // Arrange
            var f = At(FlowState.Resolving);

            // Act
            Assert.IsFalse(f.OpenJournal(false));

            // Assert
            Assert.AreEqual(FlowState.Resolving, f.State);
        }
        [Test] public void CloseJournal_NotInJournal_ChangesNothing()
        {
            // Arrange
            var f = At(FlowState.Bench);

            // Act
            Assert.IsFalse(f.CloseJournal());

            // Assert
            Assert.AreEqual(FlowState.Bench, f.State);
        }
        [Test] public void OpenGoals_FromRunOver_ClosesBackToRunOver()
        {
            // Arrange
            var f = At(FlowState.RunOver);

            // Act
            Assert.IsTrue(f.OpenGoals(false));
            Assert.AreEqual(FlowState.Goals, f.State);
            Assert.IsTrue(f.CloseGoals());

            // Assert
            Assert.AreEqual(FlowState.RunOver, f.State);
        }
        [Test] public void OpenGoals_FromBench_IsRefused()
        {
            // Arrange
            var f = At(FlowState.Bench);

            // Act
            Assert.IsFalse(f.OpenGoals(false));

            // Assert
            Assert.AreEqual(FlowState.Bench, f.State);
        }
        [Test] public void StartIgnite_BenchAndCanIgnite_GoesToResolving()
        {
            // Arrange
            var f = At(FlowState.Bench);

            // Act
            Assert.IsTrue(f.StartIgnite(true));

            // Assert
            Assert.AreEqual(FlowState.Resolving, f.State);
        }
        [Test] public void StartIgnite_CannotIgnite_StaysOnBench()
        {
            // Arrange
            var f = At(FlowState.Bench);

            // Act
            Assert.IsFalse(f.StartIgnite(false));

            // Assert
            Assert.AreEqual(FlowState.Bench, f.State);
        }
        [Test] public void StartIgnite_FromJournal_IsRefused()
        {
            // Arrange
            var f = At(FlowState.Journal);

            // Act
            Assert.IsFalse(f.StartIgnite(true));

            // Assert
            Assert.AreEqual(FlowState.Journal, f.State);
        }
        [Test] public void CanPause_BenchInputNoCutscenePlaying_IsTrue()
        {
            // Arrange
            var f = At(FlowState.Bench);

            // Act
            var result = f.CanPause(true, true);

            // Assert
            Assert.IsTrue(result);
        }
        [Test] public void CanPause_InCutscene_IsFalse()
        {
            // Arrange
            var f = At(FlowState.Bench);
            f.InCutscene = true;

            // Act
            var result = f.CanPause(true, true);

            // Assert
            Assert.IsFalse(result);
        }
        [Test] public void CanPause_InputOff_IsFalse()
        {
            // Arrange
            var f = At(FlowState.Bench);

            // Act
            var result = f.CanPause(false, true);

            // Assert
            Assert.IsFalse(result);
        }
        [Test] public void CanPause_RunNotPlaying_IsFalse()
        {
            // Arrange
            var f = At(FlowState.Bench);

            // Act
            var result = f.CanPause(true, false);

            // Assert
            Assert.IsFalse(result);
        }
        [Test] public void CanPause_NotOnBench_IsFalse()
        {
            // Arrange
            var f = At(FlowState.Title);

            // Act
            var result = f.CanPause(true, true);

            // Assert
            Assert.IsFalse(result);
        }
        [Test] public void CanPause_AnyScreenButBench_IsFalse()
        {
            // Arrange
            // Act
            foreach (FlowState s in System.Enum.GetValues(typeof(FlowState)))
                if (s != FlowState.Bench)
                    Assert.IsFalse(At(s).CanPause(true, true), s.ToString());

            // Assert (implicit in loop assertions)
        }
        [Test] public void CanAbandon_RunEnded_IsFalse()
        {
            // Arrange
            var f = At(FlowState.Bench);

            // Act
            var result = f.CanAbandon(true);

            // Assert
            Assert.IsFalse(result);
        }
        [Test] public void CanResetProgress_OnTitle_IsTrue()
        {
            // Arrange
            // Act
            var titleResult = At(FlowState.Title).CanResetProgress;
            var benchResult = At(FlowState.Bench).CanResetProgress;

            // Assert
            Assert.IsTrue(titleResult);
            Assert.IsFalse(benchResult);
        }
        [Test] public void Changed_Enter_ReportsFromAndTo()
        {
            // Arrange
            var f = new ScreenFlow();
            FlowState a = FlowState.Bench, b = FlowState.Bench;
            int n = 0;
            f.Changed += (x, y) => { a = x; b = y; n++; };

            // Act
            f.Enter(FlowState.Title);

            // Assert
            Assert.AreEqual(1, n);
            Assert.AreEqual(FlowState.Boot, a);
            Assert.AreEqual(FlowState.Title, b);
        }
        [Test] public void Changed_EnterSameState_IsNotRaised()
        {
            // Arrange
            var f = At(FlowState.Title);
            int n = 0;
            f.Changed += (x, y) => n++;

            // Act
            f.Enter(FlowState.Title);

            // Assert
            Assert.AreEqual(0, n);
        }
        [Test] public void Sandbox_BeginThenEnd_IsFalseAgain()
        {
            // Arrange
            var f = new ScreenFlow();

            // Act
            f.BeginSandbox();
            var afterBegin = f.Sandbox;
            f.EndSandbox();
            var afterEnd = f.Sandbox;

            // Assert
            Assert.IsTrue(afterBegin);
            Assert.IsFalse(afterEnd);
        }
    }
}
