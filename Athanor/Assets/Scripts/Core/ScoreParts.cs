namespace Athanor.Core
{
    /// <summary>
    /// One reaction's potency split into the factors the tutorial names: Total = Base x Used x First x Step x Bonus.
    /// Bonus is everything else (catalysts, the hub's tile, events); 1 when nothing else applied.
    /// </summary>
    public sealed class ScoreParts
    {
        public long Base, Total;
        public int Used, First, Step;
        public double Bonus;

        /// <summary>Null for a fizzle or a sputter.</summary>
        public static ScoreParts Of(ContentDb db, ReactionEvent e)
        {
            if (e == null || e.Recipe == null || e.Result == null) return null;
            var p = new ScoreParts
            {
                Base = db.BasePotency(db.Material(e.Result).tier),
                Used = e.Recipe.items.Length - 1,
                First = e.FirstThisRun ? db.Rules.firstReactionMult : 1,
                Step = e.Chain,
                Total = e.Potency,
            };
            double plain = (double)p.Base * p.Used * p.First * p.Step;
            p.Bonus = plain > 0 ? e.Potency / plain : 1;
            return p;
        }
    }
}
