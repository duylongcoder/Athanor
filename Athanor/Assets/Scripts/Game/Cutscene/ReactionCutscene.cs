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

        GameObject room; GameObject[] others;
        Transform stage;
        SpriteRenderer rune, liquid, liquidGlow, pillar, result, resultGlow;
        const int MaxMats = ContentDb.MaxItems;
        readonly SpriteRenderer[] mats = new SpriteRenderer[MaxMats], matGlow = new SpriteRenderer[MaxMats], rings = new SpriteRenderer[3];
        readonly Transform[] rays = new Transform[14];
        readonly Light2D[] matLight = new Light2D[MaxMats];
        readonly Color[] matCol = new Color[MaxMats];
        int matCount = 2;
        Light2D runeLight, liquidLight, resultLight;
        Sprite[] liquidFrames;
        Label headerLeft, headerRight, skipHint, banner, title, plus, times, total;
        Transform ui;

        ReactionEvent ev; ContentDb db;
        Color colRes;
        long basePotency; int multiplier;
        int experiment;
        float gatherSpeed = 1;

        /// <summary>others: everything that is switched off while the cutscene room is on screen (workshop, bench).</summary>
        public void Build(GameObject cutsceneRoom, params GameObject[] others)
        {
            room = cutsceneRoom; this.others = others;
            var t = stage = Make.Root("Reaction", room.transform);
            rune = Make.Sprite(t, "rune_circle", Px.At(CX, FloorY), O + 2, Make.Lib.glow);
            runeLight = Make.Light(t, Px.At(CX, FloorY), 230, Color.white, 0, 0.4f);
            liquidFrames = new[] { Make.Lib.Sprite("liquid_0"), Make.Lib.Sprite("liquid_1"), Make.Lib.Sprite("liquid_2"), Make.Lib.Sprite("liquid_3") };
            liquid = Make.Sprite(t, "liquid_0", Px.At(CX, Rim), O + 5, Make.Lib.glow);
            liquidGlow = Make.Glow(t, Px.At(CX, Rim), 100, Color.clear, O + 5);
            liquidGlow.transform.localScale = new Vector3(100 / 32f, 45 / 32f, 1);
            liquidLight = Make.Light(t, Px.At(CX, Rim - 20), 200, Color.white, 0, 0.85f);
            pillar = Make.Sprite(t, "pillar", Px.Mid(CX - 44.5f, 0, 89, 161), O + 6, Make.Lib.glow);
            for (int k = 0; k < rings.Length; k++) rings[k] = Make.Sprite(t, "floor_ring", Px.At(CX, FloorY), O + 2, Make.Lib.glow);
            for (int k = 0; k < rays.Length; k++)
            {
                rays[k] = Make.Root("ray", t);
                var r = Make.Sprite(rays[k], "ray", Vector3.zero, O + 7, Make.Lib.glow);
                r.transform.localPosition = new Vector3(22 + 75, 0, 0);
            }
            for (int i = 0; i < MaxMats; i++)
            {
                matGlow[i] = Make.Glow(t, Vector3.zero, 34, Color.clear, O + 9);
                mats[i] = Make.Sprite(t, "dot", Vector3.zero, O + 10);
                mats[i].transform.localScale = new Vector3(2, 2, 1);
                matLight[i] = Make.Light(t, Vector3.zero, 90, Color.white, 0);
            }
            resultGlow = Make.Glow(t, Vector3.zero, 70, Color.clear, O + 8);
            result = Make.Sprite(t, "dot", Vector3.zero, O + 11);
            result.transform.localScale = new Vector3(3, 3, 1);
            resultLight = Make.Light(t, Vector3.zero, 260, Color.white, 0);

            ui = Make.Root("Reaction UI", room.transform);
            Make.Rect(ui, 0, 0, Px.W, 30, Color.black, O + 40); Make.Rect(ui, 0, Px.H - 30, Px.W, 30, Color.black, O + 40);
            Make.Rect(ui, 0, 30, Px.W, 1, Px.Hex("#8c5a14"), O + 41); Make.Rect(ui, 0, Px.H - 31, Px.W, 1, Px.Hex("#8c5a14"), O + 41);
            var faint = Px.Hex("#7a5a3a");
            headerLeft = Make.Text(ui, "", 12, 9, FontKind.Body, 16, faint, O + 45);
            headerRight = Make.Text(ui, "", Px.W - 12, 9, FontKind.Body, 16, faint, O + 45, Align.Right);
            skipHint = Make.Text(ui, "CLICK TO SKIP", CX, Px.H - 19, FontKind.Tiny, 8, Px.Hex("#5a4028"), O + 45, Align.Centre);
            banner = Make.Text(ui, "New Discovery", CX, 38, FontKind.Title, 32, Px.Hex("#f8d250"), O + 45, Align.Centre, Px.Hex("#3a1a08"));
            title = Make.Text(ui, "", CX, 180, FontKind.Title, 48, Color.white, O + 45, Align.Centre, Px.Hex("#0b2b3a"));
            plus = Make.Text(ui, "", CX - 84, 230, FontKind.Title, 32, Px.Hex("#f8d250"), O + 45, Align.Centre, Px.Hex("#3a1a08"));
            times = Make.Text(ui, "", CX - 16, 230, FontKind.Title, 32, Px.Hex("#ee6a3c"), O + 45, Align.Centre, Px.Hex("#3b0d1e"));
            total = Make.Text(ui, "", CX + 66, 230, FontKind.Title, 32, Px.Hex("#fff6b8"), O + 45, Align.Centre, Px.Hex("#3a1a08"));
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
                mats[i].sprite = Make.Lib.Sprite(m.id);
                names.Add(m.name);
            }
            colRes = Px.Hex(string.IsNullOrEmpty(r.glow) ? "#fff6b8" : r.glow);
            result.sprite = Make.Lib.Sprite(r.id);
            headerLeft.Text = "Experiment " + experiment;
            string all = string.Join(" + ", names);
            headerRight.Text = all.Length <= 46 ? all : names[0] + " + " + names[1] + " + " + (matCount - 2) + " more";
            title.Text = r.name;
            title.main.fontSize = title.shadow.fontSize = r.name.Length > 14 ? 32 : 48;
            basePotency = ContentDb.BasePotency(r.tier);
            multiplier = basePotency > 0 && e.Potency % basePotency == 0 ? (int)(e.Potency / basePotency) : 1;
            plus.Text = "+" + HudView.Number(multiplier > 1 ? basePotency : e.Potency);
            times.Text = multiplier > 1 ? "x" + multiplier : "";
        }

        public override IEnumerator Play()
        {
            Begin();
            yield return ScreenFx.I.FadeOut(0.12f);
            Stage(true);
            Sparks.I.Clear();
            float T = 0; bool burst = false;
            Draw(0);
            yield return ScreenFx.I.FadeIn(0.15f);
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
                    ScreenFx.I.Flash(1, 0.9f); ScreenFx.I.Shake(5, 0.9f);
                    Sparks.I.Burst(Px.At(CX, Rim), 60, colRes.Times(2.5f), 190, 1.1f, O + 12, 40);
                    Fx.Spawn("implosion", Px.At(CX, Rim), room.transform, O + 12);
                    Fx.Spawn("holy-pillar", Px.At(CX, Rim), room.transform, O + 6);
                }
                Emit(before, T);
                Draw(T);
                yield return null;
            }
            End();
            yield return ScreenFx.I.FadeOut(0.15f);
            Sparks.I.Clear();
            Stage(false);
            yield return ScreenFx.I.FadeIn(0.18f);
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
                    Sparks.I.Trail(SpiralPos(i, to / GatherEnd) + (Vector3)Random.insideUnitCircle * 5, Random.insideUnitCircle * 10, c, 0.45f, O + 9, 0, Random.value < 0.25f);
                }
            }
            else if (to > Burst)
            {
                int n = Mathf.FloorToInt(to * 45) - Mathf.FloorToInt(from * 45);
                for (int k = 0; k < n; k++)
                {
                    var c = k % 3 == 0 ? new Color(2, 1.8f, 1.2f) : colRes.Times(2);
                    Sparks.I.Emit(Px.At(CX + Random.Range(-60f, 60f), Rim), new Vector2(Random.Range(-45f, 45f), Random.Range(50f, 120f)), c, 1.6f, O + 8, -8, k % 4 == 0);
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
            skipHint.Text = T >= Settled ? "CLICK TO CONTINUE" : "CLICK TO SKIP";
        }
    }
}
