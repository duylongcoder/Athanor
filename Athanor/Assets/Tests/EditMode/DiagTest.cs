using System;
using System.Linq;
using Athanor.Core;
using Athanor.Tests;
using NUnit.Framework;

public class DiagTest
{
    [Test]
    public void DebugDampAir()
    {
        var db = new ContentDb(TestContent.Load());
        foreach (var e in db.Events)
            Console.WriteLine("EVENT id=" + e.id + " type=" + e.type + " min=" + e.minDepth + " max=" + e.maxDepth + " weight=" + e.weight + " options=" + e.options.Length);
        var damp = db.Events.First(e => e.id == "damp_air");
        Console.WriteLine("damp min=" + damp.minDepth + " max=" + damp.maxDepth + " options=" + damp.options.Length);
        for (int depth = 1; depth <= 10; depth++)
        {
            var r = new RunState(db, new Journal(), 1, depth, new RunOptions { Events = true, ForceEvent = damp.id });
            Console.WriteLine("depth=" + depth + " phase=" + r.Phase + " event=" + (r.CurrentEvent == null ? "none" : r.CurrentEvent.id));
        }
    }
}