using System.Collections.Generic;
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

        /// <summary>The dark tone of a rarity: the catalyst strip, the tooltip's tier word and the Journal page's tier word (SPEC 22).</summary>
        public static Color RarityTone(string rarity)
        {
            switch (Rarities.Of(rarity))
            {
                case Rarities.Uncommon: return Px.Hex("#3a6a14");
                case Rarities.Rare: return Px.Hex("#1c5a96");
                case Rarities.Legendary: return Px.Hex("#8a4a08");
                default: return Px.Hex("#5a5058");
            }
        }

        /// <summary>The tier word as the strip, tooltip and Journal show it: "RARE" / "HIẾM".</summary>
        public static string RarityWord(string rarity) => Loc.Upper(Loc.Rarity(rarity));

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

        /// <summary>An aspect's rule, with the kind an attuned tile drew written in. cost: false leaves the trade-off's price out.</summary>
        public static string AspectRule(AspectInstance a, bool cost = true)
        {
            string text = RuleText(a);
            if (!string.IsNullOrEmpty(a.Kind))
            {
                string kind = Loc.Indef(a.Kind);
                text = text.Replace("the attuned kind", kind).Replace("loại đã hòa hợp", "loại " + kind);
            }
            int at = text.IndexOf(Marker, System.StringComparison.Ordinal);
            return at < 0 || cost ? text : text.Substring(0, at).TrimEnd();
        }

        static string Marker => Loc.T("ui.aspect.cost_marker");

        static string RuleText(AspectInstance a)
        {
            string text = Loc.C("aspect." + a.Data.id + ".text", a.Data.text ?? "");
            // content.json writes the price after "COST:"; the language writes it after its own marker (vi.json already does)
            return text.Replace("COST:", Marker);
        }

        /// <summary>"COST: this tile is shut for the run.", or null for an aspect that costs nothing.</summary>
        public static string AspectCost(AspectInstance a)
        {
            string text = RuleText(a);
            int at = text.IndexOf(Marker, System.StringComparison.Ordinal);
            return at < 0 ? null : text.Substring(at);
        }

        /// <summary>A few words for one event effect, as it is shown under an option before it is taken.</summary>
        public static string Effect(EffectData fx)
        {
            int n = fx.amount;
            switch (fx.op)
            {
                case Ops.Trials: return Loc.P("effect.trials", n);
                case Ops.Hand: return Loc.P("effect.hand", n);
                case Ops.Redraws: return Loc.P("effect.redraws", n);
                case Ops.RiShift: return Loc.T("effect.ri_shift", ("n", n));
                case Ops.GiveDiscovery: return Loc.T("effect.give_discovery");
                case Ops.LoseRack: return Loc.P("effect.lose_rack", n > 1 ? n : 1);
                case Ops.LoseCard: return Loc.T("effect.lose_card");
                case Ops.FirstTriple: return Loc.T("effect.first_triple");
                case Ops.Hint: return Loc.T("effect.hint");
                case Ops.BlockTile: return Loc.T("effect.block_tile");
                case Ops.HeatTile: return Loc.T("effect.heat_tile");
                case Ops.CatalystUpgrade: return Loc.T("effect.catalyst_upgrade");
                case Ops.CatalystGift: return Loc.T("effect.catalyst_gift");
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
            string nothing = Loc.T("effect.nothing");
            var gains = new List<string>();
            foreach (var x in o.outcomes)
            {
                string g = Join(x.gain);
                if (g.Length == 0) g = nothing;
                gains.Add(o.outcomes.Length > 1 ? Loc.T("effect.chance", ("pct", Mathf.RoundToInt(100f * x.weight / total)), ("gain", g)) : g);
            }
            string gain = string.Join(" / ", gains);
            if (cost.Length == 0) return gain == nothing ? "" : gain;
            return Loc.T("effect.cost_gain", ("cost", cost), ("gain", gain));
        }

        /// <summary>A goal's text in this language.</summary>
        public static string GoalText(GoalData g) => Loc.C("goal." + g.id + ".text", g.text);

        /// <summary>The label of an event option in this language.</summary>
        public static string OptionLabel(EventData e, EventOption o) =>
            Loc.C(Lang.OptionKey(e.id, System.Array.IndexOf(e.options, o)), o.label);

        /// <summary>What came of an option, in this language: the outcome the core drew (English text) found among the option's outcomes.</summary>
        public static string OutcomeText(EventData e, EventOption o, string english)
        {
            if (string.IsNullOrEmpty(english)) return "";
            int i = System.Array.IndexOf(e.options, o), j = System.Array.FindIndex(o.outcomes, x => x.text == english);
            return i < 0 || j < 0 ? english : Loc.C(Lang.OutcomeKey(e.id, i, j), english);
        }

        /// <summary>An event note (a key and its arguments from the core) in this language.</summary>
        public static string NoteText(EventNote note) =>
            note.plural
                ? Loc.P(note.key, note.n)
                : Loc.T(note.key, ("n", note.n), ("item", note.item), ("a", note.item), ("b", note.other),
                    ("kind", Loc.Vi ? Loc.Indef(note.kind) : note.kind));

        /// <summary>"New Rare catalyst": the kind and rarity of a goal's reward, never the item. Short, so it ends before the progress bar.</summary>
        public static string RewardLine(GoalData g) =>
            Loc.T(g.rewardKind == RewardKinds.Starter ? "ui.goals.reward_starter" : "ui.goals.reward_catalyst", ("rarity", Loc.Rarity(g.rewardRarity)));

        /// <summary>The name of a starting material or catalyst won from a goal.</summary>
        public static string ItemName(ContentDb db, string id)
        {
            if (string.IsNullOrEmpty(id)) return Loc.T("effect.nothing");
            if (db.Has(id)) return db.Material(id).name;
            var c = db.Catalyst(id);
            return c != null ? c.name : id;
        }

        /// <summary>The sprite of an item won from a goal: a material's icon, or a catalyst's.</summary>
        public static string ItemSprite(ContentDb db, string id) => db.Has(id) ? id : "cat_" + id;

        public static string Number(long n) => Loc.Number(n);
    }
}
