using System.Collections;
using Athanor.Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Athanor.Game
{
    /// <summary>
    /// The full discovery sequence: two materials spiral into the cauldron, the rune circle lights, a flash,
    /// and the new material rises on a column of light with its name and potency.
    /// Everything is a function of the clock T, so skipping is just moving the clock forward.
    /// </summary>
    public sealed class ReactionCutscene : Cutscene
    {
        const float CX = 320, Rim = 161, FloorY = 292, GatherEnd = 2.05f, Burst = 2.3f, Reveal = 2.9f, Count = 3.4f, Settled = 4.5f, AutoEnd = 7.5f;
        const int O = 300;

        // the parts below are the Reaction Cutscene object of Main.unity (authored in the scene); others are given by Bind
        [SerializeField] GameObject room;
        GameObject[] others;
        [SerializeField] Transform stage;
        [SerializeField] SpriteRenderer rune, liquid, liquidGlow, pillar, result, resultGlow;
        const int MaxMats = ContentDb.MaxItems;
        [SerializeField] SpriteRenderer[] mats = new SpriteRenderer[MaxMats], matGlow = new SpriteRenderer[MaxMats], rings = new SpriteRenderer[3];
        [SerializeField] Transform[] rays = new Transform[14];
        [SerializeField] Light2D[] matLight = new Light2D[MaxMats];
        readonly Color[] matCol = new Color[MaxMats];       // filled by Setup from the materials of the reaction
        int matCount = 2;
        [SerializeField] Light2D runeLight, liquidLight, resultLight;
        [SerializeField] Sprite[] liquidFrames;
        [SerializeField] Label headerLeft, headerRight, skipHint, banner, title, plus, times, total;
        [SerializeField] Transform ui;

        ReactionEvent ev; ContentDb db;
        Color colRes;
        long basePotency; int multiplier;
        int experiment;
        float gatherSpeed = 1;

        FxRefs fx;

        /// <summary>others: everything that is switched off while the cutscene room is on screen (workshop, bench). Hides the stage.</summary>
        public void Bind(GameObject[] others, FxRefs fx)
        {
            if (fx == null) throw new System.ArgumentNullException(nameof(fx));
            this.fx = fx;
            this.others = others;
            Stage(false);
        }

        void Stage(bool on)
        {
            foreach (var o in others) o.SetActive(!on);
            room.SetActive(on); stage.gameObject.SetActive(on); ui.gameObject.SetActive(on);
        }

        /// <summary>hurry above 1 shortens the gathering, for the second and later discoveries of one Ignite.</summary>
        public void Setup(ReactionEvent e, ContentDb content, float hurry = 1)
        {
            ev = e; db = content; experiment++; gatherSpeed = Mathf.Max(1, hurry);
            var r = db.Material(e.Result);
            matCount = Mathf.Min(e.Items.Length, MaxMats);
            var names = new System.Collections.Generic.List<string>();
            for (int i = 0; i < matCount; i++)
            {
                var m = db.Material(e.Items[i]);
                matCol[i] = Px.Hex(string.IsNullOrEmpty(m.glow) ? "#f0dcc0" : m.glow);
                mats[i].sprite = Art.Lib.Sprite(m.id);
                names.Add(m.name);
            }
            colRes = Px.Hex(string.IsNullOrEmpty(r.glow) ? "#fff6b8" : r.glow);
            result.sprite = Art.Lib.Sprite(r.id);
            headerLeft.Text = Loc.T("cutscene.experiment", ("n", experiment));
            string all = string.Join(" + ", names);
            headerRight.Text = all.Length <= 46 ? all : Loc.T("cutscene.more", ("a", names[0]), ("b", names[1]), ("n", matCount - 2));
            title.Text = r.name;
            title.SetSize(r.name.Length > 14 ? 32 : 48);
            basePotency = db.BasePotency(r.tier);
            multiplier = basePotency > 0 && e.Potency % basePotency == 0 ? (int)(e.Potency / basePotency) : 1;
            plus.Text = "+" + HudView.Number(multiplier > 1 ? basePotency : e.Potency);
            times.Text = multiplier > 1 ? "x" + multiplier : "";
        }

        public override IEnumerator Play()
        {
            Begin();
            yield return fx.Screen.FadeOut(0.12f);
            Stage(true);
            fx.Sparks.Clear();
            float T = 0; bool burst = false;
            Draw(0);
            yield return fx.Screen.FadeIn(0.15f);
            ClearSkip();
            while (T < AutoEnd)
            {
                if (Skipped)
                {
                    ClearSkip();
                    if (T < Settled) { T = Settled; burst = true; } else break;
                }
                float before = T;
                T += Time.deltaTime * (T < GatherEnd ? gatherSpeed : 1);
                if (!burst && T >= GatherEnd)
                {
                    burst = true;
                    fx.Screen.Flash(1, 0.9f); fx.Screen.Shake(5, 0.9f);
                    fx.Sparks.Burst(Px.At(CX, Rim), 60, colRes.Times(2.5f), 190, 1.1f, O + 12, 40);
                    Fx.Spawn("implosion", Px.At(CX, Rim), room.transform, O + 12);
                    Fx.Spawn("holy-pillar", Px.At(CX, Rim), room.transform, O + 6);
                }
                Emit(before, T);
                Draw(T);
                yield return null;
            }
            End();
            yield return fx.Screen.FadeOut(0.15f);
            fx.Sparks.Clear();
            Stage(false);
            yield return fx.Screen.FadeIn(0.18f);
        }

        Vector3 SpiralPos(int i, float f)
        {
            f = Mathf.Max(0, f);
            float r = 150 * Mathf.Pow(1 - f, 1.4f) + 4, a = i * 2 * Mathf.PI / Mathf.Max(1, matCount) + f * 9;
            return Px.At(CX + Mathf.Cos(a) * r, 70 + f * f * 84 + Mathf.Sin(a) * r * 0.3f);
        }

        /// <summary>Particles for the stretch of time just played: trails behind the materials, then sparks off the brew.</summary>
        void Emit(float from, float to)
        {
            if (to < GatherEnd)
            {
                for (int i = 0; i < matCount; i++)
                {
                    var c = matCol[i].Times(2);
                    fx.Sparks.Trail(SpiralPos(i, to / GatherEnd) + (Vector3)Random.insideUnitCircle * 5, Random.insideUnitCircle * 10, c, 0.45f, O + 9, 0, Random.value < 0.25f);
                }
            }
            else if (to > Burst)
            {
                int n = Mathf.FloorToInt(to * 45) - Mathf.FloorToInt(from * 45);
                for (int k = 0; k < n; k++)
                {
                    var c = k % 3 == 0 ? new Color(2, 1.8f, 1.2f) : colRes.Times(2);
                    fx.Sparks.Emit(Px.At(CX + Random.Range(-60f, 60f), Rim), new Vector2(Random.Range(-45f, 45f), Random.Range(50f, 120f)), c, 1.6f, O + 8, -8, k % 4 == 0);
                }
            }
        }

        void Draw(float T)
        {
            float t = Time.time, gather = Mathf.Clamp01(T / 2f);
            bool after = T >= Burst;
            // rune circle
            float rk = after ? 1 : gather * gather;
            var rc = after ? colRes : new Color(0.75f, 0.5f, 1);
            rune.color = rc.Times(1.5f).Alpha(rk * (0.75f + 0.25f * Mathf.Sin(t * 4)));
            runeLight.color = rc; runeLight.intensity = rk * 0.6f;
            // the brew
            var lc = after ? colRes : new Color(0.35f + 0.4f * gather, 0.15f + 0.25f * gather, 0.6f);
            liquid.sprite = liquidFrames[(int)(t * 6) % liquidFrames.Length];
            liquid.color = lc.Times(1.4f).Alpha(0.35f + (after ? 0.5f : gather * 0.25f));
            liquidGlow.color = lc.Alpha(after ? 0.55f : 0.2f + gather * 0.25f);
            liquidLight.color = lc; liquidLight.intensity = after ? 1.3f : 0.5f * gather + 0.2f;
            // the two materials spiralling in
            bool flying = T < GatherEnd;
            for (int i = 0; i < MaxMats; i++)
            {
                bool on = flying && i < matCount;
                mats[i].enabled = matGlow[i].enabled = on;
                matLight[i].intensity = on ? 1.1f : 0;
                if (!on) continue;
                var p = Px.Snap(SpiralPos(i, T / GatherEnd)); var c = matCol[i];
                mats[i].transform.position = p; matGlow[i].transform.position = p; matLight[i].transform.position = p;
                matGlow[i].color = c.Alpha(0.75f); matLight[i].color = c;
            }
            // the result rising
            float a1 = after ? Mathf.Clamp01((T - Burst) * 1.6f) : 0, rise = after ? Mathf.Clamp01((T - Burst) / 1.2f) : 0;
            float iy = Rim - 20 - Anim.OutCubic(rise) * 62 + Mathf.Sin(t * 2.2f) * 3;
            var rp = Px.Snap(Px.At(CX, iy));
            result.enabled = after; result.transform.position = rp; result.color = Color.white.Alpha(a1);
            resultGlow.transform.position = rp; resultGlow.color = colRes.Alpha(0.4f * a1);
            resultLight.transform.position = rp; resultLight.color = colRes; resultLight.intensity = 1.25f * a1;
            pillar.color = colRes.Times(1.2f).Alpha(0.6f * a1 * (0.85f + 0.15f * Mathf.Sin(t * 9)));
            for (int k = 0; k < rays.Length; k++)
            {
                rays[k].position = rp;
                rays[k].rotation = Quaternion.Euler(0, 0, (k * 180f / 7) + t * 20);
                rays[k].localScale = new Vector3((k % 2 == 0 ? 0.7f : 1) * a1, 1, 1);
                rays[k].GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 0.95f, 0.75f, 0.28f * a1);
            }
            for (int k = 0; k < rings.Length; k++)
            {
                float r = after ? ((T - Burst) * 0.55f + k / 3f) % 1 : 0;
                rings[k].transform.localScale = new Vector3((60 + r * 300) / 60f, (14 + r * 70) / 14f, 1);
                rings[k].color = colRes.Times(1.5f).Alpha(after ? (1 - r) * 0.8f : 0);
            }
            // words
            float k1 = Mathf.Clamp01((T - Reveal) / 0.25f);
            banner.Alpha = k1; title.Alpha = k1;
            banner.transform.position = Px.At(CX, 38 - (1 - k1) * 6); title.transform.position = Px.At(CX, 180 + (1 - k1) * 6);
            bool counting = T > Count;
            float cf = Mathf.Clamp01((T - Count) / 0.9f);
            total.Text = multiplier > 1 ? "= " + HudView.Number((long)(ev.Potency * Anim.OutCubic(cf))) : "";
            total.Color = cf >= 1 ? Color.white : Px.Hex("#fff6b8");
            plus.Alpha = times.Alpha = total.Alpha = counting ? 1 : 0;
            if (multiplier <= 1) plus.transform.position = Px.At(CX, 230);
            else plus.transform.position = Px.At(CX - 84, 230);
            skipHint.Text = Loc.T(T >= Settled ? "ui.common.click_to_continue" : "ui.common.click_to_skip");
        }
    }
}
