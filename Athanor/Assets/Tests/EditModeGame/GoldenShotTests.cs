using Athanor.EditorShared;
using Athanor.Game;
using NUnit.Framework;

namespace Athanor.Tests
{
    /// <summary>Every view, in both languages, still looks like the picture recorded in production/qa/evidence/phase7/baseline/golden.</summary>
    public class GoldenShotTests
    {
        [TestCaseSource(typeof(GoldenShots), nameof(GoldenShots.Names))]
        public void Render_Shot_MatchesTheGoldenImage(string name)
        {
            foreach (var lang in new[] { Loc.English, Loc.Vietnamese })
            {
                Loc.Set(lang);
                var golden = ViewCapture.Load(GoldenShots.PathOf(name, lang));
                Assert.IsNotNull(golden, "no golden image for " + name + "." + lang);
                Assert.AreEqual(0f, ViewCapture.Differ(GoldenShots.Render(name), golden), name + "." + lang);
            }
            Loc.Set(Loc.English);
        }
    }
}
