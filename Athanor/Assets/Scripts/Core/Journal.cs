using System;
using System.Collections.Generic;
using System.Linq;

namespace Athanor.Core
{
    /// <summary>What the player has learned across runs: recipes that worked, and hints from ones that did not.</summary>
    public sealed class Journal
    {
        readonly HashSet<string> known = new HashSet<string>();
        readonly List<HintData> hints = new List<HintData>();

        public IReadOnlyCollection<string> KnownPairs => known;
        public IReadOnlyList<HintData> Hints => hints;

        public bool IsKnown(string a, string b) => known.Contains(ContentDb.PairKey(a, b));
        public bool Learn(string a, string b) => known.Add(ContentDb.PairKey(a, b));

        public bool IsKnown(IEnumerable<string> items) => known.Contains(ContentDb.ComboKey(items));
        public bool Learn(IEnumerable<string> items) => known.Add(ContentDb.ComboKey(items));

        /// <summary>Every recipe this material is an ingredient of has been found.</summary>
        public bool IsComplete(string material, ContentDb db) =>
            db.Has(material) && db.RecipesContaining(material).All(r => IsKnown(r.items));

        /// <summary>The hints about a material, for as long as it still has something unfound.</summary>
        public IEnumerable<HintData> OpenHints(string material, ContentDb db) =>
            IsComplete(material, db) || !db.Has(material) ? Enumerable.Empty<HintData>() : hints.Where(h => h.material == material);

        public bool HasHint(string material, Kind partnerKind) =>
            hints.Any(h => h.material == material && h.partnerKind == partnerKind.ToString());

        public bool AddHint(string material, Kind partnerKind)
        {
            if (HasHint(material, partnerKind)) return false;
            hints.Add(new HintData { material = material, partnerKind = partnerKind.ToString() });
            return true;
        }

        /// <summary>A hint that this material has a recipe waiting on more heat round its hub.</summary>
        public bool AddHeatHint(string material, int heat)
        {
            if (heat <= 0 || hints.Any(h => h.material == material && h.heat == heat)) return false;
            hints.Add(new HintData { material = material, heat = heat });
            return true;
        }

        public JournalData ToData() => new JournalData { known = known.ToArray(), hints = hints.ToArray() };

        /// <summary>Never throws: anything malformed is skipped.</summary>
        public static Journal FromData(JournalData d)
        {
            var j = new Journal();
            if (d == null) return j;
            if (d.known != null)
                foreach (var k in d.known) if (!string.IsNullOrEmpty(k) && k.Contains("+")) j.known.Add(k);
            if (d.hints != null)
                foreach (var h in d.hints)
                {
                    if (h == null || string.IsNullOrEmpty(h.material)) continue;
                    if (h.heat > 0) j.AddHeatHint(h.material, h.heat);
                    else if (Enum.TryParse(h.partnerKind, out Kind kind)) j.AddHint(h.material, kind);
                }
            return j;
        }
    }
}
