using System;
using System.Collections;
using System.Collections.Generic;
using Athanor.Gameplay;
using PrimeTween;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>What one card of the chooser shows.</summary>
    public sealed class ChoiceCard
    {
        public string id, sprite, name, text, kicker, cost;
        public string rarity;     // the rarity shown as stars and a tier strip; null for an aspect
        public int[] tiles;       // bench tiles to light on the little bench at the foot of the card; null: no bench
        public int[] dimTiles;    // tiles the lit ones are joined to
    }

    /// <summary>
    /// Three parchment cards, pick one: a catalyst after a depth is cleared, or an aspect at the start of a run
    /// and of depths 3 and 5. An aspect's card shows the tile it would land on.
    /// </summary>
    public sealed class CatalystChoiceView : MonoBehaviour
    {
        /// <summary>Stops what this component started so nothing runs on into the next Play (domain reload off).</summary>
        void OnDestroy() { StopAllCoroutines(); Motion.StopOn(this); }

        /// <summary>
        /// The development-build log line once the dealt cards take a click (tools/web/webtest.mjs and descents.mjs click a card after it).
        /// Not "[choice] ...": descents.mjs counts that one from GameManager.Descend.
        /// </summary>
        public const string OpenHook = "[cards] open";

        const int O = 440;
        /// <summary>One card's size, the gap between cards and the top of the row (art px), stored in CatalystChoice.prefab.</summary>
        [Serializable]
        public struct CardRow { public int width, height, gap, top; }
        [SerializeField] CardRow cardRow;
        /// <summary>The chooser's motion: the deal (base and per card, flight, stagger, the shade's fade divisor), the exit (length, the chosen card's hop as a share of it) and the flash on a pick.</summary>
        [Serializable]
        public struct Timing { public float dealBase, dealPerCard, dealFly, shadeDiv, stagger, away, hopShare; public Vector2 pickFlash; }
        /// <summary>The chooser's distances (px) and looks: the shade's alpha, how far below home cards start and fall to, the chosen card's hop, the hover lift and its speed, kicker colours, the pick burst.</summary>
        [Serializable]
        public struct Layout { public float dim, startDrop, fallDrop, hop, hoverLift, liftSpeed; public Color kicker, kickerCost; public BurstSpec pickBurst; }
        [SerializeField] Timing timing;
        [SerializeField] Layout layout;

        [Serializable]
        sealed class Card
        {
            public Transform root; public SpriteRenderer lit, icon, strip; public RarityStars cornerStars, stripStars; public Label name, text, kicker, tier;
            public Clickable click;
            public SpriteRenderer[] bench = new SpriteRenderer[Bench.TileCount];
            [NonSerialized] public Vector3 home; [NonSerialized] public string id; [NonSerialized] public float lift;
        }

        /// <summary>Card slots the prefab lays out. Layout, not a rule: rules.offerSize must not exceed it.</summary>
        const int Slots = 3;
        [SerializeField] Card[] cards = new Card[Slots];
        [SerializeField] SpriteRenderer shade;
        [SerializeField] Label heading, sub;
        int hovered = -1, count;
        string picked;
        bool open, wired;

        static Color Off => Art.Palette.pipOff;
        static Color On => Art.Palette.pipOn;
        static Color Joined => Art.Palette.faded;

        /// <summary>The name label of card i.</summary>
        public Label CardName(int i) => cards[i].name;
        /// <summary>The star row in the top-left corner of card i.</summary>
        public RarityStars CornerStars(int i) => cards[i].cornerStars;

        FxRefs fx;

        /// <summary>Hooks the clicks. GameManager calls it once at start; a test calls it after instantiating the prefab.</summary>
        public void Bind(FxRefs effects)
        {
            if (effects == null) throw new System.ArgumentNullException(nameof(effects));
            fx = effects;
            if (wired) return;
            wired = true;
            for (int i = 0; i < cards.Length; i++)
            {
                int index = i;
                var click = cards[i].click;
                click.onClick = () => PickCard(index);
                click.onEnter = () => hovered = index;
                click.onExit = () => { if (hovered == index) hovered = -1; };
            }
        }

        void PickCard(int index) { if (open) picked = cards[index].id; }

        /// <summary>The catalysts offered after a depth.</summary>
        public IEnumerator Choose(IReadOnlyList<CatalystData> offer, Action<string> done, float autoAfter = -1)
        {
            var list = new List<ChoiceCard>();
            foreach (var c in offer)
                list.Add(new ChoiceCard
                {
                    id = c.id, sprite = "cat_" + c.id, name = c.name, text = Loc.C("catalyst." + c.id + ".text", c.text), rarity = Rarities.Of(c.rarity),
                });
            return Choose(Loc.T("ui.choice.catalyst_title"), Loc.T("ui.choice.catalyst_sub"), list, done, autoAfter);
        }

        /// <summary>The aspects offered at the start of a run and of depths 3 and 5.</summary>
        public IEnumerator Choose(IReadOnlyList<AspectInstance> offer, Action<string> done, float autoAfter = -1)
        {
            var list = new List<ChoiceCard>();
            foreach (var a in offer)
                list.Add(new ChoiceCard
                {
                    id = a.Data.id, sprite = "asp_" + a.Data.id, name = a.Data.name, text = Wording.AspectRule(a, false), cost = Wording.AspectCost(a),
                    kicker = Loc.T(a.Data.tradeOff ? "ui.choice.tradeoff" : "ui.choice.aspect"), tiles = a.Tiles,
                    dimTiles = a.Data.effect == AspectEffects.OpenBench ? new[] { Bench.Centre } : null,
                });
            return Choose(Loc.T("ui.choice.aspect_title"), Loc.T("ui.choice.aspect_sub"), list, done, autoAfter);
        }

        /// <summary>Shows the cards and waits for a pick. autoAfter >= 0 takes the first card after that many seconds (scripted play).</summary>
        public IEnumerator Choose(string title, string line, IReadOnlyList<ChoiceCard> offer, Action<string> done, float autoAfter = -1)
        {
            picked = null; hovered = -1; open = false;
            count = Mathf.Min(offer.Count, cards.Length);
            if (count == 0) { done(null); yield break; }
            heading.Text = title; sub.Text = line;
            float start = (Px.W - (count * cardRow.width + (count - 1) * cardRow.gap)) / 2f;
            for (int i = 0; i < cards.Length; i++)
            {
                var c = cards[i];
                c.root.gameObject.SetActive(i < count);
                if (i >= count) continue;
                var o = offer[i];
                c.id = o.id; c.lift = 0; c.lit.enabled = false;
                c.icon.sprite = Art.Lib.Sprite(o.sprite);
                c.name.Text = o.name;
                c.text.Text = string.IsNullOrEmpty(o.cost) ? o.text : o.text + "\n<color=#9a3414>" + o.cost + "</color>";
                c.kicker.Color = string.IsNullOrEmpty(o.cost) ? layout.kicker : layout.kickerCost;
                bool catalyst = o.rarity != null;
                c.kicker.Text = o.kicker ?? "";
                c.strip.enabled = catalyst; c.tier.Text = catalyst ? Wording.RarityWord(o.rarity) : "";
                if (catalyst) { c.strip.color = Wording.RarityTone(o.rarity); c.cornerStars.Show(o.rarity); c.stripStars.Show(o.rarity); }
                else { c.cornerStars.Show(0, Color.white); c.stripStars.Show(0, Color.white); }
                bool bench = o.tiles != null && o.tiles.Length > 0;
                for (int k = 0; k < Bench.TileCount; k++)
                {
                    c.bench[k].enabled = bench;
                    c.bench[k].color = bench && Array.IndexOf(o.tiles, k) >= 0 ? On : o.dimTiles != null && Array.IndexOf(o.dimTiles, k) >= 0 ? Joined : Off;
                }
                c.home = Px.Snap(Px.Mid(start + i * (cardRow.width + cardRow.gap), cardRow.top, cardRow.width, cardRow.height));
                c.root.position = c.home + new Vector3(0, layout.startDrop, 0);
            }
            heading.Alpha = sub.Alpha = 0;
            gameObject.SetActive(true);

            float dealTime = timing.dealBase + timing.dealPerCard * count;
            // the shade and the heading come up first; the cards fly in one after another, 0.1 s apart
            var deal = Sequence.Create()
                .Group(Tween.Delay(dealTime))
                .Group(Motion.Fade(shade, 0, layout.dim, dealTime / timing.shadeDiv))
                .Group(Motion.Fade(heading, 0, 1, dealTime / 2))
                .Group(Motion.Fade(sub, 0, 1, dealTime / 2));
            for (int i = 0; i < count; i++)
                deal.Insert(i * timing.stagger, Motion.Move(cards[i].root, cards[i].home + new Vector3(0, layout.startDrop, 0), cards[i].home, timing.dealFly, Ease.OutBack));
            yield return deal.ToYieldInstruction();
            open = true;
            Dev.Hook(OpenHook);                // development builds only: from here a click on a card is taken
            for (float waited = 0; picked == null; waited += Time.deltaTime)
            {
                if (autoAfter >= 0 && waited > autoAfter) picked = cards[0].id;
                yield return null;
            }
            open = false;

            int chosen = 0;
            for (int i = 0; i < count; i++) if (cards[i].id == picked) chosen = i;
            cards[chosen].lit.enabled = true;
            fx.Screen.Flash(timing.pickFlash.x, timing.pickFlash.y);
            layout.pickBurst.Play(fx.Sparks, cards[chosen].root.position);
            // the others fall at once; the chosen card hops 10 px first, then falls after them
            float Away = timing.away, Hop = timing.hopShare * Away;
            var away = Sequence.Create()
                .Group(Motion.Fade(shade, layout.dim, 0, Away, Ease.InCubic))
                .Group(Motion.Fade(heading, 1, 0, Away))
                .Group(Motion.Fade(sub, 1, 0, Away));
            for (int i = 0; i < count; i++)
            {
                var c = cards[i];
                Vector3 at = c.home + new Vector3(0, c.lift, 0), gone = at + new Vector3(0, layout.fallDrop, 0);
                if (i != chosen) away.Insert(0, Motion.Move(c.root, at, gone, Away, Ease.InCubic));
                else away.Insert(0, Motion.Arc(c.root, at, at, layout.hop, Hop)).Insert(Hop, Motion.Move(c.root, at, gone, Away - Hop, Ease.InCubic));
            }
            yield return away.ToYieldInstruction();
            gameObject.SetActive(false);
            done(picked);
        }

        void Update()
        {
            if (!open) return;
            for (int i = 0; i < count; i++)
            {
                var c = cards[i];
                c.lift = Mathf.MoveTowards(c.lift, hovered == i ? layout.hoverLift : 0, Time.deltaTime * layout.liftSpeed);
                c.root.position = Px.Snap(c.home + new Vector3(0, c.lift, 0));
                c.lit.enabled = hovered == i;
            }
        }
    }
}
