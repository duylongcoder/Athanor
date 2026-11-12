namespace Athanor.Game
{
    /// <summary>The label colour for each kind of material.</summary>
    public static class KindColors
    {
        public static string Of(string kind)
        {
            switch (kind)
            {
                case "Ore": return "#c08a5c";
                case "Metal": return "#a8b8c4";
                case "Crystal": return "#c486f4";
                case "Powder": return "#e4d2aa";
                case "Potion": return "#6ee6c8";
                case "Plant": return "#8ad04a";
                case "Beast": return "#ee6a3c";
                default: return "#f0dcc0";
            }
        }
    }
}
