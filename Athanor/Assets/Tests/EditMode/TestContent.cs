using Athanor.Core;

namespace Athanor.Tests
{
    /// <summary>Loads the real content.json, inside Unity or under plain dotnet.</summary>
    public static class TestContent
    {
        public static ContentData Load()
        {
#if UNITY_5_3_OR_NEWER
            var text = UnityEngine.Resources.Load<UnityEngine.TextAsset>("content").text;
            return UnityEngine.JsonUtility.FromJson<ContentData>(text);
#else
            var path = System.IO.Path.Combine(System.AppContext.BaseDirectory, "content.json");
            var opts = new System.Text.Json.JsonSerializerOptions { IncludeFields = true };
            return System.Text.Json.JsonSerializer.Deserialize<ContentData>(System.IO.File.ReadAllText(path), opts);
#endif
        }

        /// <summary>a+b=>x (tier 1), x+c=>y (tier 2); a,b Ore; c Potion; d Plant reacts with nothing.</summary>
        public static ContentData Tiny()
        {
            return new ContentData
            {
                materials = new[]
                {
                    Mat("a", "Ore", 0), Mat("b", "Ore", 0), Mat("c", "Potion", 0), Mat("d", "Plant", 0),
                    Mat("x", "Metal", 1), Mat("y", "Crystal", 2),
                },
                recipes = new[] { Rec("a", "b", "x"), Rec("x", "c", "y") },
                catalysts = new CatalystData[0],
                depthTargets = new[] { 300, 900, 2500, 7000 },
                depthNames = new[] { "1", "2", "3", "4", "5" },
                ultimate = "y",
            };
        }

        /// <summary>
        /// Combo content. Starters (Reaction Index): p 8, q 5, r 2, s 9, t 4, v 1.
        /// x(6) = p+q; u(3) = x+v; z(7) = p+q+r (needs zMin); m(4) = s+q; big(5) = s+p+q+r+t; quad(5) = s+p+q+t.
        /// </summary>
        public static ContentData Combo(int zMin = 15)
        {
            return new ContentData
            {
                materials = new[]
                {
                    Mat("p", "Ore", 0, 8), Mat("q", "Potion", 0, 5), Mat("r", "Plant", 0, 2), Mat("s", "Beast", 0, 9),
                    Mat("t", "Powder", 0, 4), Mat("v", "Crystal", 0, 1),
                    Mat("x", "Metal", 1, 6), Mat("m", "Beast", 1, 4), Mat("u", "Crystal", 2, 3), Mat("z", "Potion", 2, 7),
                    Mat("big", "Crystal", 3, 5), Mat("quad", "Metal", 3, 5),
                },
                recipes = new[]
                {
                    Rec(new[] { "p", "q" }, 10, "x"), Rec(new[] { "x", "v" }, 6, "u"), Rec(new[] { "p", "q", "r" }, zMin, "z"),
                    Rec(new[] { "s", "q" }, 10, "m"), Rec(new[] { "s", "p", "q", "r", "t" }, 28, "big"),
                    Rec(new[] { "s", "p", "q", "t" }, 24, "quad"),
                },
                catalysts = new CatalystData[0],
                depthTargets = new[] { 300, 1500, 7500, 37500 },
                depthNames = new[] { "1", "2", "3", "4", "5" },
                ultimate = "big",
            };
        }

        public static MaterialData Mat(string id, string kind, int tier, int reactivity = 5) =>
            new MaterialData { id = id, name = id, kind = kind, tier = tier, glow = "", reactivity = reactivity };
        public static RecipeData Rec(string a, string b, string r) => new RecipeData { a = a, b = b, result = r };
        public static RecipeData Rec(string[] items, int min, string r) => new RecipeData { items = items, minReactivity = min, result = r };
        public static CatalystData Cat(string effect, string kind = "", float amount = 0) =>
            new CatalystData { id = effect, name = effect, text = effect, effect = effect, kind = kind, amount = amount };
    }
}
