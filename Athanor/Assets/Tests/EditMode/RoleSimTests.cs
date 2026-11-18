using System;
using System.Collections.Generic;
using System.Linq;
using Athanor.Core;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>
    /// Two simulated players on the full Phase 3 game (unlocks, aspects, events), each starting from an
    /// empty journal and a fresh profile and keeping both from run to run.
    ///   Admin: knows every recipe and how a bench resolves (the scripted player on the real content).
    ///   Newcomer: knows only what its own journal holds, plans with that, and spends spare tiles trying
    ///   pairs it has not tried, following the journal's hints.
    /// Run the report: dotnet run --project tools/CoreReports -- roles (the simulation itself is in Sim.cs)
    /// </summary>
    public class RoleSimTests
    {
        [Test] public void Newcomer_OverFortyRuns_LearnsAndReachesTheDeep()
        {
            // a smoke test of the simulated newcomer: it must fill its journal and reach the deep
            var db = new ContentDb(TestContent.Load());
            var career = Sim.Play(db, false, 1, 40);
            Assert.That(career.Journal.Last(), Is.GreaterThan(career.Journal.First()), "the journal never grew");
            Assert.That(career.Journal.Last(), Is.GreaterThanOrEqualTo(20), "journal " + career.Journal.Last());
            Assert.That(career.Depth.Max(), Is.GreaterThanOrEqualTo(3), "deepest " + career.Depth.Max());
        }

        [Test] public void Admin_WithinADozenRuns_WinsFromAFreshProfile()
        {
            var db = new ContentDb(TestContent.Load());
            var career = Sim.Play(db, true, 1, 12);
            Assert.That(career.FirstWin, Is.InRange(1, 12), "an informed player should make the Ultimate within a dozen runs");
        }
    }
}
