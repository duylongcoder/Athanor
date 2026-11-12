using System;
using System.Collections;
using System.Collections.Generic;
using Athanor.Core;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Three parchment cards after a depth is cleared: pick one catalyst for the rest of the run.</summary>
    public sealed class CatalystChoiceView : MonoBehaviour
    {
        const int O = 440, CardW = 150, CardH = 180, Gap = 18, Top = 112;
        const float Dim = 0.8f;

        sealed class Card
        {
            public Transform root; public SpriteRenderer lit, icon; public Label name, text; public Vector3 home; public string id; public float lift;
        }

        readonly Card[] cards = new Card[RunState.OfferSize];
        SpriteRenderer shade;
        Label heading, sub;
        int hovered = -1, count;
        string picked;
        bool open;

        public void Build()
        {
            var t = transform;
            shade = Make.Rect(t, 0, 0, Px.W, Px.H, Color.clear, O - 1);
            Make.Click(shade.gameObject, new Vector2(Px.W, Px.H), O - 1);
            heading = Make.Text(t, "Choose a Catalyst", 320, 36, FontKind.Title, 32, Px.Hex("#f8d250"), O + 6, Align.Centre, Px.Hex("#3a1a08"));
            sub = Make.Text(t, "It bends the rules for the rest of this descent.", 320, 74, FontKind.Body, 16, Px.Hex("#f0dcc0"), O + 6, Align.Centre, Px.Hex("#0c0706"));
            for (int i = 0; i < cards.Length; i++)
            {
                int index = i;
                var c = cards[i] = new Card { root = Make.Root("Choice " + i, t) };
                c.lit = Make.Sprite(c.root, "choice_lit", Vector3.zero, O); c.lit.transform.localPosition = new Vector3(1, -1, 0);
                Make.Sprite(c.root, "choice", Vector3.zero, O + 1).transform.localPosition = new Vector3(1, -2, 0);
                c.icon = Make.Sprite(c.root, "dot", Vector3.zero, O + 2); c.icon.transform.localPosition = new Vector3(0, 61, 0);
                c.name = Make.Text(c.root, "", 0, 0, FontKind.Title, 16, Px.Hex("#3a2212"), O + 3, Align.Centre);
                c.name.transform.localPosition = new Vector3(0, 40, 0);
                c.text = Make.Text(c.root, "", 0, 0, FontKind.Body, 16, Px.Hex("#3a2212"), O + 3, Align.Left, null, 124);
                c.text.transform.localPosition = new Vector3(-61, 8, 0);
                c.text.main.lineSpacing = -30;
                Make.Text(c.root, "CATALYST", 0, 0, FontKind.Tiny, 8, Px.Hex("#a8854e"), O + 3, Align.Centre).transform.localPosition = new Vector3(0, -72, 0);
                var click = Make.Click(c.root.gameObject, new Vector2(CardW, CardH), O, () => { if (open) picked = cards[index].id; });
                click.onEnter = () => hovered = index;
                click.onExit = () => { if (hovered == index) hovered = -1; };
            }
            gameObject.SetActive(false);
        }

        /// <summary>Shows the offer and waits for a pick. autoAfter >= 0 takes the first card after that many seconds (scripted play).</summary>
        public IEnumerator Choose(IReadOnlyList<CatalystData> offer, Action<string> done, float autoAfter = -1)
        {
            picked = null; hovered = -1; open = false;
            count = Mathf.Min(offer.Count, cards.Length);
            if (count == 0) { done(null); yield break; }
            float start = (Px.W - (count * CardW + (count - 1) * Gap)) / 2f;
            for (int i = 0; i < cards.Length; i++)
            {
                var c = cards[i];
                c.root.gameObject.SetActive(i < count);
                if (i >= count) continue;
                c.id = offer[i].id; c.lift = 0; c.lit.enabled = false;
                c.icon.sprite = Make.Lib.Sprite("cat_" + offer[i].id);
                c.name.Text = offer[i].name; c.text.Text = offer[i].text;
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
