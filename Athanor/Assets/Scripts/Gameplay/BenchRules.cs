using System;
using System.Collections.Generic;

namespace Athanor.Gameplay
{
    /// <summary>
    /// What catalysts, aspects and events change about the bench itself: which tiles touch, how lively a
    /// tile counts, which tiles are shut. The planner, the resolver and the scripted player all read this,
    /// so they cannot disagree. Build one, then leave it alone: the neighbour table is worked out once.
    /// </summary>
    public sealed class BenchRules
    {

        public bool Diagonals;                                   // Crooked Mirror
        public int RiShift, HeatPlus;                            // events; Warm Bench
        public int Pivot = -1;
        public readonly bool[] Blocked = new bool[Bench.TileCount];
        public readonly int[] RiBonus = new int[Bench.TileCount];
        public readonly bool[] Kiln = new bool[Bench.TileCount], Crossroads = new bool[Bench.TileCount];
        public readonly List<LinkPair> Links = new List<LinkPair>();
        // scoring, read from the hub's tile
        public readonly int[] HubMult = { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public readonly bool[] Heavy = new bool[Bench.TileCount];
        public readonly string[] AttunedKind = new string[Bench.TileCount];
        public readonly int[] SeedBonus = new int[Bench.TileCount];

        /// <summary>The Phase 2 bench, with or without diagonal neighbours.</summary>
        public static readonly BenchRules None = new BenchRules(), Diagonal = new BenchRules { Diagonals = true };
        public static BenchRules Plain(bool diagonals) => diagonals ? Diagonal : None;

        int[][] around;

        /// <summary>The tiles a hub on this tile reaches, ascending.</summary>
        public int[] Around(int tile)
        {
            if (around == null) around = Build();
            return around[tile];
        }

        int[][] Build()
        {
            var all = new int[Bench.TileCount][];
            for (int t = 0; t < Bench.TileCount; t++)
            {
                var list = new List<int>();
                if (!Blocked[t])
                {
                    bool diag = Diagonals || Crossroads[t];
                    int r = t / Bench.Size, c = t % Bench.Size;
                    for (int dr = -1; dr <= 1; dr++)
                        for (int dc = -1; dc <= 1; dc++)
                        {
                            if ((dr == 0 && dc == 0) || (!diag && dr != 0 && dc != 0)) continue;
                            int rr = r + dr, cc = c + dc;
                            if (rr >= 0 && rr < Bench.Size && cc >= 0 && cc < Bench.Size) list.Add(rr * Bench.Size + cc);
                        }
                    foreach (var (a, b) in Links)
                    {
                        if (a == t && !list.Contains(b)) list.Add(b);
                        if (b == t && !list.Contains(a)) list.Add(a);
                    }
                    list.RemoveAll(n => Blocked[n]);
                    list.Sort();
                }
                all[t] = list.ToArray();
            }
            return all;
        }

        /// <summary>The Reaction Index a material counts for on this tile.</summary>
        public int Ri(ContentDb db, int tile, string id)
        {
            int ri = db.Reactivity(id) + RiBonus[tile] + RiShift;
            return ri < 1 ? 1 : ri > db.Rules.maxRi ? db.Rules.maxRi : ri;
        }

        /// <summary>Either tile reaches the other (Crossroads makes this one-sided for a hub; a pair needs only one side).</summary>
        public bool Touch(int a, int b) => Array.IndexOf(Around(a), b) >= 0 || Array.IndexOf(Around(b), a) >= 0;
    }

    /// <summary>What one reaction's score depends on beyond the recipe and the catalysts.</summary>
    public sealed class ScoreContext
    {
        public int Depth = 1, EmptyTiles;
        public BenchRules Rules = BenchRules.None;
        public bool[] EmptyAtIgnite;        // for Seed tiles; null: none empty
        public bool TripleFirst;            // an event: the first reaction of this Ignite scores triple
        public Func<string, bool> IsStarter;
    }
}
