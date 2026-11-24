using System;
using System.Collections;
using Athanor.Gameplay;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

namespace Athanor.Game
{
    /// <summary>
    /// The full discovery sequence: two materials spiral into the cauldron, the rune circle lights, a flash,
    /// and the new material rises on a column of light with its name and potency.
    /// Everything is a function of the clock T, so skipping is just moving the clock forward.
    /// </summary>
    public sealed class ReactionCutscene : Cutscene
    {
        const float CX = 320;
        const int O = 300;

        // ---- the numbers of the choreography, grouped by beat; Main.unity holds the values (see LayoutPinTests) ----
        // Everything is a function of the clock T (seconds since the room opened); the beats below name the moments on it.

        /// <summary>Before T = 0: the screen fades out of the bench and the room fades in.</summary>
        [Serializable]
        public struct Opening { public float fadeOut, fadeIn; }

        /// <summary>The gathering: the materials spiral in until end; the rune and the brew build up over ramp.</summary>
        [Serializable]
        public struct Gathering { public float end, ramp; }

        /// <summary>The burst at the end of the gathering: flash, shake, the sparks (their colour is multiplied by the result's glow) and the implosion sprite's sort order.</summary>
        [Serializable]
        public struct Bursting { public float flash, flashSeconds, shake, shakeSeconds; public BurstSpec burst; public int implosionOrder; }

        /// <summary>The result rises from riseAt (over rise seconds, fading in at resultFade a second); the words show from wordsAt, fading over wordsFade.</summary>
        [Serializable]
        public struct Revealing { public float riseAt, rise, resultFade, wordsAt, wordsFade; }

        /// <summary>The points count up from at, over seconds.</summary>
        [Serializable]
        public struct Tallying { public float at, seconds; }

        /// <summary>From settled the scene waits for a click; autoEnd ends it alone. Then the room fades out and the bench fades back.</summary>
        [Serializable]
        public struct Closing { public float settled, autoEnd, fadeOut, fadeIn; }

        /// <summary>The rune circle, the brew and the materials: colours, brightness and how they pulse.</summary>
        [Serializable]
        public struct Brew
        {
            /// <summary>Colours before the burst: the rune's; the brew's at the start and what it gains by the end of the gathering (rgb).</summary>
            public Color runeStart, brewStart, brewGain;
            /// <summary>Rune: brightness, pulse = base + amplitude * sin(speed * t), light strength.</summary>
            public float runeBright, runePulseBase, runePulseAmplitude, runePulseSpeed, runeLight;
            /// <summary>Brew: frames a second, brightness, alpha (base; added after the burst; added with the gathering), glow alpha (before; after; gained), light (gained; base; after).</summary>
            public float frameRate, brewBright, alphaBase, alphaAfter, alphaGather, glowBase, glowAfter, glowGain, lightGain, lightBase, lightAfter;
            /// <summary>Each flying material: light strength and glow alpha.</summary>
            public float matLight, matGlow;
        }

        /// <summary>The result, its pillar, rays and rings once the burst has gone off.</summary>
        [Serializable]
        public struct Prize
        {
            /// <summary>Glow alpha and light strength of the result; pillar brightness, alpha and pulse = base + amplitude * sin(speed * t).</summary>
            public float glowAlpha, lightStrength, pillarBright, pillarAlpha, pillarPulseBase, pillarPulseAmplitude, pillarPulseSpeed;
            /// <summary>The result bobs by bobAmplitude at bobSpeed.</summary>
            public float bobSpeed, bobAmplitude;
            /// <summary>Rays: spin speed, scale of every other ray, colour (alpha at full strength).</summary>
            public float raySpin, shortRay;
            public Color rayColor;
            /// <summary>Rings: cycles a second, width and height at birth and how much each grows, brightness, alpha.</summary>
            public float ringSpeed, ringWidth, ringGrowW, ringHeight, ringGrowH, ringBright, ringAlpha;
        }

        /// <summary>Where things are (art space, y down) and how the motion between them is shaped.</summary>
        [Serializable]
        public struct Layout
        {
            /// <summary>The cauldron's rim: the burst, the sparks and the result start here.</summary>
            public float rimY;
            /// <summary>The result floats resultLift above the rim and rises resultRise.</summary>
            public float resultLift, resultRise;
            /// <summary>The spiral of a material: radius, power of its shrinking, smallest radius, turns (radians), flattening, start y and how far it drops.</summary>
            public float spiralRadius, spiralPower, spiralMin, spiralSpin, spiralFlat, spiralY, spiralDrop;
            /// <summary>The banner and the title sit here and slide by wordSlide while they fade in.</summary>
            public float bannerY, titleY, wordSlide;
            /// <summary>The "+n" line at plusY; with a multiplier it moves plusShift left to make room.</summary>
            public float plusY, plusShift;
        }

        /// <summary>The trail behind a flying material and the sparks off the brew.</summary>
        [Serializable]
        public struct Sparkle
        {
            /// <summary>Trail: jitter, speed, life, brightness (times the material's glow), chance of a twinkle.</summary>
            public float trailJitter, trailSpeed, trailLife, trailBright, trailTwinkle;
            /// <summary>Sparks: a second; spread of x; speed in x; speed in y (min, max); life; gravity; brightness (times the result's glow); the white one in three.</summary>
            public float rate, spread, speedX, speedYMin, speedYMax, life, gravity, bright;
            public Color white;
        }

        [SerializeField] Opening opening;
        [SerializeField] Gathering gathering;
        [SerializeField] Bursting bursting;
        [SerializeField] Revealing revealing;
        [SerializeField] Tallying tallying;
        [SerializeField] Closing closing;
        [SerializeField] Brew brew;
        [SerializeField] Prize prize;
        [SerializeField] Layout layout;
        [SerializeField] Sparkle sparkle;

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
                matCol[i] = string.IsNullOrEmpty(m.glow) ? Art.Palette.defaultGlow : Px.Hex(m.glow);
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
            yield return fx.Screen.FadeOut(opening.fadeOut);
            Stage(true);
            fx.Sparks.Clear();
            float T = 0; bool burst = false;
            Draw(0);
            yield return fx.Screen.FadeIn(opening.fadeIn);
            ClearSkip();
            while (T < closing.autoEnd)
            {
                if (Skipped)
                {
                    ClearSkip();
                    if (T < closing.settled) { T = closing.settled; burst = true; } else break;
                }
                float before = T;
                T += Time.deltaTime * (T < gathering.end ? gatherSpeed : 1);
                if (!burst && T >= gathering.end)
                {
                    burst = true;
                    fx.Screen.Flash(bursting.flash, bursting.flashSeconds); fx.Screen.Shake(bursting.shake, bursting.shakeSeconds);
                    var rim = Px.At(CX, layout.rimY);
                    var spark = bursting.burst; spark.color = Mul(colRes, spark.color);
                    spark.Play(fx.Sparks, rim);
                    Fx.Spawn("implosion", rim, room.transform, bursting.implosionOrder);
                    Fx.Spawn("holy-pillar", rim, room.transform, O + 6);
                }
                Emit(before, T);
                Draw(T);
                yield return null;
            }
            End();
            yield return fx.Screen.FadeOut(closing.fadeOut);
            fx.Sparks.Clear();
            Stage(false);
            yield return fx.Screen.FadeIn(closing.fadeIn);
        }

        /// <summary>The colour times a glow, channel by channel; the alpha is the colour's.</summary>
        static Color Mul(Color a, Color b) => new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a);

        Vector3 SpiralPos(int i, float f)
        {
            f = Mathf.Max(0, f);
            float r = layout.spiralRadius * Mathf.Pow(1 - f, layout.spiralPower) + layout.spiralMin, a = i * 2 * Mathf.PI / Mathf.Max(1, matCount) + f * layout.spiralSpin;
            float x = CX + Mathf.Cos(a) * r, y = layout.spiralY + f * f * layout.spiralDrop + Mathf.Sin(a) * r * layout.spiralFlat;
            return Px.At(x, y);
        }

        /// <summary>Particles for the stretch of time just played: trails behind the materials, then sparks off the brew.</summary>
        void Emit(float from, float to)
        {
            if (to < gathering.end)
            {
                for (int i = 0; i < matCount; i++)
                {
                    var c = matCol[i].Times(sparkle.trailBright);
                    fx.Sparks.Trail(SpiralPos(i, to / gathering.end) + (Vector3)Random.insideUnitCircle * sparkle.trailJitter, Random.insideUnitCircle * sparkle.trailSpeed, c, sparkle.trailLife, O + 9, 0, Random.value < sparkle.trailTwinkle);
                }
            }
            else if (to > revealing.riseAt)
            {
                int n = Mathf.FloorToInt(to * sparkle.rate) - Mathf.FloorToInt(from * sparkle.rate);
                for (int k = 0; k < n; k++)
                {
                    var c = k % 3 == 0 ? sparkle.white : colRes.Times(sparkle.bright);
                    float x = CX + Random.Range(-sparkle.spread, sparkle.spread);
                    var at = Px.At(x, layout.rimY);
                    var velocity = new Vector2(Random.Range(-sparkle.speedX, sparkle.speedX), Random.Range(sparkle.speedYMin, sparkle.speedYMax));
                    fx.Sparks.Emit(at, velocity, c, sparkle.life, O + 8, sparkle.gravity, k % 4 == 0);
                }
            }
        }

        void Draw(float T)
        {
            float t = Time.time, gather = Mathf.Clamp01(T / gathering.ramp);
            bool after = T >= revealing.riseAt;
            // rune circle
            float rk = after ? 1 : gather * gather;
            var rc = after ? colRes : brew.runeStart;
            rune.color = rc.Times(brew.runeBright).Alpha(rk * (brew.runePulseBase + brew.runePulseAmplitude * Mathf.Sin(t * brew.runePulseSpeed)));
            runeLight.color = rc; runeLight.intensity = rk * brew.runeLight;
            // the brew
            var lc = after ? colRes : new Color(brew.brewStart.r + brew.brewGain.r * gather, brew.brewStart.g + brew.brewGain.g * gather, brew.brewStart.b);
            liquid.sprite = liquidFrames[(int)(t * brew.frameRate) % liquidFrames.Length];
            liquid.color = lc.Times(brew.brewBright).Alpha(brew.alphaBase + (after ? brew.alphaAfter : gather * brew.alphaGather));
            liquidGlow.color = lc.Alpha(after ? brew.glowAfter : brew.glowBase + gather * brew.glowGain);
            liquidLight.color = lc; liquidLight.intensity = after ? brew.lightAfter : brew.lightGain * gather + brew.lightBase;
            // the two materials spiralling in
            bool flying = T < gathering.end;
            for (int i = 0; i < MaxMats; i++)
            {
                bool on = flying && i < matCount;
                mats[i].enabled = matGlow[i].enabled = on;
                matLight[i].intensity = on ? brew.matLight : 0;
                if (!on) continue;
                var p = Px.Snap(SpiralPos(i, T / gathering.end)); var c = matCol[i];
                mats[i].transform.position = p; matGlow[i].transform.position = p; matLight[i].transform.position = p;
                matGlow[i].color = c.Alpha(brew.matGlow); matLight[i].color = c;
            }
            // the result rising
            float a1 = after ? Mathf.Clamp01((T - revealing.riseAt) * revealing.resultFade) : 0, rise = after ? Mathf.Clamp01((T - revealing.riseAt) / revealing.rise) : 0;
            float iy = layout.rimY - layout.resultLift - Anim.OutCubic(rise) * layout.resultRise + Mathf.Sin(t * prize.bobSpeed) * prize.bobAmplitude;
            var rp = Px.Snap(Px.At(CX, iy));
            result.enabled = after; result.transform.position = rp; result.color = Color.white.Alpha(a1);
            resultGlow.transform.position = rp; resultGlow.color = colRes.Alpha(prize.glowAlpha * a1);
            resultLight.transform.position = rp; resultLight.color = colRes; resultLight.intensity = prize.lightStrength * a1;
            pillar.color = colRes.Times(prize.pillarBright).Alpha(prize.pillarAlpha * a1 * (prize.pillarPulseBase + prize.pillarPulseAmplitude * Mathf.Sin(t * prize.pillarPulseSpeed)));
            for (int k = 0; k < rays.Length; k++)
            {
                rays[k].position = rp;
                rays[k].rotation = Quaternion.Euler(0, 0, (k * 180f / 7) + t * prize.raySpin);
                float spread = (k % 2 == 0 ? prize.shortRay : 1) * a1;
                rays[k].localScale = new Vector3(spread, 1, 1);
                rays[k].GetChild(0).GetComponent<SpriteRenderer>().color = prize.rayColor.Alpha(prize.rayColor.a * a1);
            }
            for (int k = 0; k < rings.Length; k++)
            {
                float r = after ? ((T - revealing.riseAt) * prize.ringSpeed + k / 3f) % 1 : 0;
                float ringX = (prize.ringWidth + r * prize.ringGrowW) / prize.ringWidth, ringY = (prize.ringHeight + r * prize.ringGrowH) / prize.ringHeight;
                rings[k].transform.localScale = new Vector3(ringX, ringY, 1);
                rings[k].color = colRes.Times(prize.ringBright).Alpha(after ? (1 - r) * prize.ringAlpha : 0);
            }
            // words
            float k1 = Mathf.Clamp01((T - revealing.wordsAt) / revealing.wordsFade);
            banner.Alpha = k1; title.Alpha = k1;
            float slide = (1 - k1) * layout.wordSlide;
            banner.transform.position = Px.At(CX, layout.bannerY - slide); title.transform.position = Px.At(CX, layout.titleY + slide);
            bool counting = T > tallying.at;
            float cf = Mathf.Clamp01((T - tallying.at) / tallying.seconds);
            total.Text = multiplier > 1 ? "= " + HudView.Number((long)(ev.Potency * Anim.OutCubic(cf))) : "";
            total.Color = cf >= 1 ? Color.white : Art.Palette.buttonText;
            plus.Alpha = times.Alpha = total.Alpha = counting ? 1 : 0;
            if (multiplier <= 1) plus.transform.position = Px.At(CX, layout.plusY);
            else plus.transform.position = Px.At(CX - layout.plusShift, layout.plusY);
            skipHint.Text = Loc.T(T >= closing.settled ? "ui.common.click_to_continue" : "ui.common.click_to_skip");
        }
    }
}
