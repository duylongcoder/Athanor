using System.Collections.Generic;
using System.Globalization;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>The words and colours the Phase 3 screens share: rarities, aspects, event effects and goal rewards.</summary>
    public static class Wording
    {
        public static Color RarityColor(string rarity)
        {
            switch (Rarities.Of(rarity))
            {
                case Rarities.Uncommon: return Px.Hex("#8ad04a");
                case Rarities.Rare: return Px.Hex("#7cc0f8");
                case Rarities.Legendary: return Px.Hex("#ffa63a");
                default: return Px.Hex("#a8a0a8");
            }
        }

        /// <summary>The colour of an aspect's marker on its tile and of its link.</summary>
        public static Color AspectColor(string effect)
        {
            switch (effect)
            {
                case AspectEffects.Doubled: case AspectEffects.Bridge: case AspectEffects.OpenBench: return Px.Hex("#f8d250");
                case AspectEffects.Anchor: return Px.Hex("#a8b8c4");
                case AspectEffects.Heated: return Px.Hex("#ffa63a");
                case AspectEffects.Echo: case AspectEffects.Crossroads: return Px.Hex("#6ee6c8");
                case AspectEffects.Attuned: return Px.Hex("#c486f4");
                case AspectEffects.Seed: return Px.Hex("#8ad04a");
                case AspectEffects.Fallow: return Px.Hex("#d49a5c");
                case AspectEffects.Heavy: return Px.Hex("#cfc8c4");
                case AspectEffects.Cold: return Px.Hex("#7cc0f8");
                case AspectEffects.Volatile: return Px.Hex("#ee6a3c");
                case AspectEffects.Kiln: return Px.Hex("#e06a1c");
                case AspectEffects.Pivot: return Px.Hex("#f890b8");
                default: return Px.Hex("#f0dcc0");
            }
        }

        public static readonly Color Warm = Px.Hex("#ffa63a");

        static string A(string word) => ("AEIOU".IndexOf(char.ToUpperInvariant(word[0])) >= 0 ? "an " : "a ") + word;

        /// <summary>An aspect's rule, with the kind an attuned tile drew written in. cost: false leaves the trade-off's price out.</summary>
        public static string AspectRule(AspectInstance a, bool cost = true)
        {
            string text = a.Data.text ?? "";
            if (!string.IsNullOrEmpty(a.Kind)) text = text.Replace("the attuned kind", A(a.Kind));
            int at = text.IndexOf("COST:");
            return at < 0 || cost ? text : text.Substring(0, at).TrimEnd();
        }

        /// <summary>"COST: this tile is shut for the run.", or null for an aspect that costs nothing.</summary>
        public static string AspectCost(AspectInstance a)
        {
            string text = a.Data.text ?? "";
            int at = text.IndexOf("COST:");
            return at < 0 ? null : text.Substring(at);
        }

        static string Signed(int n, string one, string many) => (n > 0 ? "+" : "") + n + " " + (Mathf.Abs(n) == 1 ? one : many);

        /// <summary>A few words for one event effect, as it is shown under an option before it is taken.</summary>
        public static string Effect(EffectData fx)
        {
            int n = fx.amount;
            switch (fx.op)
            {
                case Ops.Trials: return Signed(n, "trial", "trials");
                case Ops.Hand: return Signed(n, "card each hand", "cards each hand");
                case Ops.Redraws: return Signed(n, "redraw", "redraws");
                case Ops.RiShift: return "Reaction Index " + (n > 0 ? "+" : "") + n;
                case Ops.GiveDiscovery: return "a discovery";
                case Ops.LoseRack: return "give up " + (n > 1 ? n + " discoveries" : "a discovery");
                case Ops.LoseCard: return "lose a card";
                case Ops.FirstTriple: return "first reaction x3";
                case Ops.Hint: return "a hint";
                case Ops.BlockTile: return "a tile shut";
                case Ops.HeatTile: return "a warm tile";
                case Ops.CatalystUpgrade: return "a catalyst made rarer";
                case Ops.CatalystGift: return "a catalyst";
                default: return null;
            }
        }

        static string Join(EffectData[] list)
        {
            var parts = new List<string>();
            foreach (var fx in list ?? new EffectData[0]) { var w = Effect(fx); if (w != null) parts.Add(w); }
            return string.Join(", ", parts);
        }

        /// <summary>What an option costs and what it may give: "give up a discovery: +1 trial", "50% +1 trial / 50% lose a card".</summary>
        public static string OptionSummary(EventOption o)
        {
            string cost = Join(o.cost);
            int total = 0;
            foreach (var x in o.outcomes) total += x.weight;
            var gains = new List<string>();
            foreach (var x in o.outcomes)
            {
                string g = Join(x.gain);
                if (g.Length == 0) g = "nothing";
                gains.Add(o.outcomes.Length > 1 ? Mathf.RoundToInt(100f * x.weight / total) + "% " + g : g);
            }
            string gain = string.Join(" / ", gains);
            if (cost.Length == 0) return gain == "nothing" ? "" : gain;
            return cost + ": " + gain;
        }

        /// <summary>"New Rare catalyst": the kind and rarity of a goal's reward, never the item. Short, so it ends before the progress bar.</summary>
        public static string RewardLine(GoalData g) =>
            "New " + g.rewardRarity + (g.rewardKind == RewardKinds.Starter ? " starter" : " catalyst");

        /// <summary>The name of a starting material or catalyst won from a goal.</summary>
        public static string ItemName(ContentDb db, string id)
        {
            if (string.IsNullOrEmpty(id)) return "nothing";
            if (db.Has(id)) return db.Material(id).name;
            var c = db.Catalyst(id);
            return c != null ? c.name : id;
        }

        /// <summary>The sprite of an item won from a goal: a material's icon, or a catalyst's.</summary>
        public static string ItemSprite(ContentDb db, string id) => db.Has(id) ? id : "cat_" + id;

        public static string Number(long n) => n.ToString("N0", CultureInfo.InvariantCulture);
    }
}
