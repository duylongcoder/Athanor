using System;
using System.Collections;
using System.Collections.Generic;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>What one card of the chooser shows.</summary>
    public sealed class ChoiceCard
    {
        public string id, sprite, name, text, kicker, cost;
        public Color? pip;        // the rarity pip in the corner; none for an aspect
        public int[] tiles;       // bench tiles to light on the little bench at the foot of the card; null: no bench
        public int[] dimTiles;    // tiles the lit ones are joined to
    }

    /// <summary>
    /// Three parchment cards, pick one: a catalyst after a depth is cleared, or an aspect at the start of a run
    /// and of depths 3 and 5. An aspect's card shows the tile it would land on.
    /// </summary>
    public sealed class CatalystChoiceView : MonoBehaviour
    {
        const int O = 440, CardW = 150, CardH = 180, Gap = 18, Top = 112;
        const float Dim = 0.8f;

        sealed class Card
        {
            public Transform root; public SpriteRenderer lit, icon, pip; public Label name, text, kicker; public Vector3 home; public string id; public float lift;
            public readonly SpriteRenderer[] bench = new SpriteRenderer[Bench.TileCount];
        }

        readonly Card[] cards = new Card[RunState.OfferSize];
        SpriteRenderer shade;
        Label heading, sub;
        int hovered = -1, count;
        string picked;
        bool open;

        static readonly Color Ink = Px.Hex("#3a2212"), Off = Px.Hex("#b89860"), On = Px.Hex("#c8501e"), Joined = Px.Hex("#7a5430");

        public void Build()
        {
            var t = transform;
            shade = Make.Rect(t, 0, 0, Px.W, Px.H, Color.clear, O - 1);
            Make.Click(shade.gameObject, new Vector2(Px.W, Px.H), O - 1);
            heading = Make.Text(t, "", 320, 36, FontKind.Title, 32, Px.Hex("#f8d250"), O + 6, Align.Centre, Px.Hex("#3a1a08"));
            sub = Make.Text(t, "", 320, 74, FontKind.Body, 16, Px.Hex("#f0dcc0"), O + 6, Align.Centre, Px.Hex("#0c0706"));
            for (int i = 0; i < cards.Length; i++)
            {
                int index = i;
                var c = cards[i] = new Card { root = Make.Root("Choice " + i, t) };
                c.lit = Make.Sprite(c.root, "choice_lit", Vector3.zero, O); c.lit.transform.localPosition = new Vector3(1, -1, 0);
                Make.Sprite(c.root, "choice", Vector3.zero, O + 1).transform.localPosition = new Vector3(1, -2, 0);
                c.icon = Make.Sprite(c.root, "dot", Vector3.zero, O + 2); c.icon.transform.localPosition = new Vector3(0, 61, 0);
                c.pip = Make.Sprite(c.root, "pip", Vector3.zero, O + 3); c.pip.transform.localPosition = new Vector3(-66, 81, 0);
                c.name = Make.Text(c.root, "", 0, 0, FontKind.Title, 16, Ink, O + 3, Align.Centre);
                c.name.transform.localPosition = new Vector3(0, 40, 0);
                c.text = Make.Text(c.root, "", 0, 0, FontKind.Body, 16, Ink, O + 3, Align.Left, null, 124);
                c.text.transform.localPosition = new Vector3(-61, 8, 0);
                c.text.main.lineSpacing = -30;
                // the little bench: three rows of three, the back row narrower, as the real one is drawn
                for (int k = 0; k < Bench.TileCount; k++)
                {
                    c.bench[k] = Make.Sprite(c.root, "mini_tile", Vector3.zero, O + 3);
                    c.bench[k].transform.localPosition = new Vector3((k % 3 - 1) * 8, -46 - (k / 3) * 7, 0);
                }
                c.kicker = Make.Text(c.root, "", 0, 0, FontKind.Tiny, 8, Px.Hex("#a8854e"), O + 3, Align.Centre);
                c.kicker.transform.localPosition = new Vector3(0, -72, 0);
                var click = Make.Click(c.root.gameObject, new Vector2(CardW, CardH), O, () => { if (open) picked = cards[index].id; });
                click.onEnter = () => hovered = index;
                click.onExit = () => { if (hovered == index) hovered = -1; };
            }
            gameObject.SetActive(false);
        }

        /// <summary>The catalysts offered after a depth.</summary>
        public IEnumerator Choose(IReadOnlyList<CatalystData> offer, Action<string> done, float autoAfter = -1)
        {
            var list = new List<ChoiceCard>();
            foreach (var c in offer)
                list.Add(new ChoiceCard
                {
                    id = c.id, sprite = "cat_" + c.id, name = c.name, text = c.text, pip = Wording.RarityColor(c.rarity),
                    kicker = Rarities.Of(c.rarity).ToUpperInvariant() + " CATALYST",
                });
            return Choose("Choose a Catalyst", "It bends the rules for the rest of this descent.", list, done, autoAfter);
        }

        /// <summary>The aspects offered at the start of a run and of depths 3 and 5.</summary>
        public IEnumerator Choose(IReadOnlyList<AspectInstance> offer, Action<string> done, float autoAfter = -1)
        {
            var list = new List<ChoiceCard>();
            foreach (var a in offer)
                list.Add(new ChoiceCard
                {
                    id = a.Data.id, sprite = "asp_" + a.Data.id, name = a.Data.name, text = Wording.AspectRule(a, false), cost = Wording.AspectCost(a),
                    kicker = a.Data.tradeOff ? "TRADE-OFF ASPECT" : "ASPECT", tiles = a.Tiles,
                    dimTiles = a.Data.effect == AspectEffects.OpenBench ? new[] { Bench.Centre } : null,
                });
            return Choose("Choose an Aspect", "It changes the bench until this descent ends.", list, done, autoAfter);
        }

        /// <summary>Shows the cards and waits for a pick. autoAfter >= 0 takes the first card after that many seconds (scripted play).</summary>
        public IEnumerator Choose(string title, string line, IReadOnlyList<ChoiceCard> offer, Action<string> done, float autoAfter = -1)
        {
            picked = null; hovered = -1; open = false;
            count = Mathf.Min(offer.Count, cards.Length);
            if (count == 0) { done(null); yield break; }
            heading.Text = title; sub.Text = line;
            float start = (Px.W - (count * CardW + (count - 1) * Gap)) / 2f;
            for (int i = 0; i < cards.Length; i++)
            {
                var c = cards[i];
                c.root.gameObject.SetActive(i < count);
                if (i >= count) continue;
                var o = offer[i];
                c.id = o.id; c.lift = 0; c.lit.enabled = false;
                c.icon.sprite = Make.Lib.Sprite(o.sprite);
                c.name.Text = o.name;
                c.text.Text = string.IsNullOrEmpty(o.cost) ? o.text : o.text + "\n<color=#9a3414>" + o.cost + "</color>";
                c.kicker.Text = o.kicker; c.kicker.Color = Px.Hex(string.IsNullOrEmpty(o.cost) ? "#a8854e" : "#9a3414");
                c.pip.enabled = o.pip.HasValue;
                if (o.pip.HasValue) c.pip.color = o.pip.Value;
                bool bench = o.tiles != null && o.tiles.Length > 0;
                for (int k = 0; k < Bench.TileCount; k++)
                {
                    c.bench[k].enabled = bench;
                    c.bench[k].color = bench && Array.IndexOf(o.tiles, k) >= 0 ? On : o.dimTiles != null && Array.IndexOf(o.dimTiles, k) >= 0 ? Joined : Off;
                }
                c.home = Px.Snap(Px.Mid(start + i * (CardW + Gap), Top, CardW, CardH));
                c.root.position = c.home + new Vector3(0, -300, 0);
            }
            heading.Alpha = sub.Alpha = 0;
            gameObject.SetActive(true);

            float dealTime = 0.5f + 0.1f * count;
            yield return Anim.Over(dealTime, t =>
            {
                shade.color = new Color(0, 0, 0, Dim * Mathf.Clamp01(t * 2.5f));
                heading.Alpha = sub.Alpha = Mathf.Clamp01(t * 2);
                for (int i = 0; i < count; i++)
                {
                    float k = Mathf.Clamp01((t * dealTime - i * 0.1f) / 0.5f);
                    cards[i].root.position = Px.Snap(cards[i].home + new Vector3(0, (1 - Anim.OutBack(k)) * -300, 0));
                }
            });
            open = true;
            for (float waited = 0; picked == null; waited += Time.deltaTime)
            {
                if (autoAfter >= 0 && waited > autoAfter) picked = cards[0].id;
                yield return null;
            }
            open = false;

            int chosen = 0;
            for (int i = 0; i < count; i++) if (cards[i].id == picked) chosen = i;
            cards[chosen].lit.enabled = true;
            ScreenFx.I.Flash(0.22f, 0.35f);
            Sparks.I.Burst(cards[chosen].root.position, 40, new Color(2.2f, 1.7f, 0.6f), 150, 0.8f, O + 8, 60);
            yield return Anim.Over(0.55f, t =>
            {
                shade.color = new Color(0, 0, 0, Dim * (1 - Anim.InCubic(t)));
                heading.Alpha = sub.Alpha = 1 - t;
                for (int i = 0; i < count; i++)
                {
                    var c = cards[i];
                    float drop = i == chosen ? Anim.InCubic(Mathf.Clamp01((t - 0.45f) / 0.55f)) * 330 - Anim.Hump(Mathf.Clamp01(t / 0.45f)) * 10 : Anim.InCubic(t) * 330;
                    c.root.position = Px.Snap(c.home + new Vector3(0, -drop + c.lift, 0));
                }
            });
            gameObject.SetActive(false);
            done(picked);
        }

        void Update()
        {
            if (!open) return;
            for (int i = 0; i < count; i++)
            {
                var c = cards[i];
                c.lift = Mathf.MoveTowards(c.lift, hovered == i ? 8 : 0, Time.deltaTime * 80);
                c.root.position = Px.Snap(c.home + new Vector3(0, c.lift, 0));
                c.lit.enabled = hovered == i;
            }
        }
    }
}
