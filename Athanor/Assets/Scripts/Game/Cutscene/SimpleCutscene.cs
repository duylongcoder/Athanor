using System;
using System.Collections;
using Athanor.Gameplay;
using PrimeTween;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Athanor.Game
{
    /// <summary>
    /// The short story beats built from text and a few effects: the intro, the descent between depths, and the ending.
    /// A click finishes the line being written, then moves on.
    /// </summary>
    public sealed class SimpleCutscene : Cutscene
    {
        /// <summary>Stops what this component started so nothing runs on into the next Play (domain reload off).</summary>
        void OnDestroy() { StopAllCoroutines(); Motion.StopOn(this); }

        const float CX = 320;

        // ---- the numbers of the choreography, grouped by beat; Main.unity holds the values (see LayoutPinTests) ----

        /// <summary>The opening story: the fades round it and the holds between its lines, in seconds.</summary>
        [Serializable]
        public struct IntroPage
        {
            /// <summary>Fade in from black; a page's fade out between pages; the fade out to black at the end.</summary>
            public float fadeIn, lineFade, fadeOut;
            /// <summary>Hold after a line; after the last line of the last page; after the last line of an earlier page.</summary>
            public float hold, lastHold, finalHold;
            /// <summary>How long "click to begin" waits for a click.</summary>
            public float promptHold;
        }

        /// <summary>Descent beat 1, the win over the bench: "depth cleared" rises into place, with a flash, a shake and a burst.</summary>
        [Serializable]
        public struct DescentBanner
        {
            public float flash, flashSeconds, shake, shakeSeconds;
            /// <summary>The banner fades in and rises from art y fromY to toY in this long, then holds.</summary>
            public float rise, fromY, toY, hold;
            /// <summary>The burst over the bench: where (art y), and its flattening (see Sparks.Burst).</summary>
            public BurstSpec burst;
            public float burstY, burstSquash;
        }

        /// <summary>Descent beat 2, down the stair: the banner fades while the room leaves upward by scroll pixels.</summary>
        [Serializable]
        public struct DescentLeave { public float fade, seconds, scroll; }

        /// <summary>Descent beat 3, the name of the place: fades in while the name rises from art y fromY to toY, then the line under it, then a hold.</summary>
        [Serializable]
        public struct DescentName { public float fade, rise, fromY, toY, introFade, hold; }

        /// <summary>Descent beat 4, arriving: the texts and the rune fade while the room rises back; streaks is the motes' strength meanwhile.</summary>
        [Serializable]
        public struct DescentArrive { public float streaks, fade, runeFade, seconds; }

        /// <summary>The depth's line at the start of a run: fade in, hold, fade out; the bench is dimmed by shade.</summary>
        [Serializable]
        public struct DepthLine { public float fade, hold; public Color shade; }

        /// <summary>The ending: the fades round the story, the hold after a line, the final flash and burst, the banner's place.</summary>
        [Serializable]
        public struct EndingBeat
        {
            public float fadeOut, fadeIn, hold, flash, flashSeconds, promptHold;
            /// <summary>The burst over the prize: where (art y); its colour is multiplied by the prize's glow.</summary>
            public BurstSpec burst;
            public float burstY, bannerY;
        }

        /// <summary>What Update draws each frame: the floating prize and its rays, the rune circle and its ring, the colours of the streams.</summary>
        [Serializable]
        public struct Look
        {
            /// <summary>The prize floats about art y prizeY (bobbing by bobAmplitude at bobSpeed); its glow and light at full strength.</summary>
            public float prizeY, bobSpeed, bobAmplitude, glowAlpha, lightIntensity;
            /// <summary>Rays: angle step, spin speed, scale of every other ray, colour (alpha at full strength).</summary>
            public float rayStep, raySpin, shortRay;
            public Color rayColor;
            /// <summary>Rune: brightness (also the ring's), pulse = base + amplitude * sin(speed * t), glow alpha.</summary>
            public float runeBright, pulseBase, pulseAmplitude, pulseSpeed, runeGlow;
            /// <summary>Ring: cycles per second; width and height at birth and how much each grows; alpha.</summary>
            public float ringSpeed, ringWidth, ringGrowW, ringHeight, ringGrowH, ringAlpha;
            /// <summary>Streams: the embers' colour; the streaks' and motes' brightness (the depth's colour times it) and the streaks' alpha.</summary>
            public Color emberColor;
            public float streakBright, streakAlpha, moteBright;
        }

        /// <summary>One looping stream: sort order, particles a second, life, where they are born (art x, y), their speed, gravity and chance to sparkle.</summary>
        [Serializable]
        public struct LoopSpec
        {
            public int order;
            public float rate, life, x0, x1, y0, y1, vx0, vx1, vy0, vy1, gravity, sparkle;
        }

        [SerializeField] IntroPage introPage;
        [SerializeField] DescentBanner descentBanner;
        [SerializeField] DescentLeave descentLeave;
        [SerializeField] DescentName descentName;
        [SerializeField] DescentArrive descentArrive;
        [SerializeField] DepthLine depthLine;
        [SerializeField] EndingBeat ending;
        [SerializeField] Look look;
        [SerializeField] LoopSpec emberSpec, streakSpec, moteSpec;
        /// <summary>Letters a second when a line is written out.</summary>
        [SerializeField] float typeRate;

        // the parts below are the Story Cutscene object of Main.unity (authored in the scene); the three loops are made at run time
        [SerializeField] GameObject room, workshop, bench;
        [SerializeField] Transform root;
        [SerializeField] SpriteRenderer shade, glow, ring, rune, icon, iconGlow;
        [SerializeField] SpriteRenderer[] bars = new SpriteRenderer[4];
        [SerializeField] Transform[] rays = new Transform[12];
        /// <summary>The sprite under each ray, found once, in the scene, so that Update does not look for it every frame.</summary>
        [SerializeField] SpriteRenderer[] rayFaces = new SpriteRenderer[12];
        [SerializeField] Label[] lines = new Label[3];
        [SerializeField] Label small, big, banner, prompt, intro;
        [SerializeField] Light2D iconLight;
        Color tint = Color.white;
        FxRefs fx;

        /// <summary>Takes the effect players. GameManager calls it once at start.</summary>
        public void Bind(FxRefs effects)
        {
            if (effects == null) throw new System.ArgumentNullException(nameof(effects));
            fx = effects;
        }
        float raysOn, runeOn, embers, streaks;      // strengths of the looping effects, driven by the scripts below

        public override IEnumerator Play() => Intro();

        /// <summary>Clears the stage. inRoom swaps the workshop for the cauldron room; letterbox adds the black bars.</summary>
        void Stage(bool inRoom, bool letterbox)
        {
            root.gameObject.SetActive(true);
            if (inRoom) { workshop.SetActive(false); bench.SetActive(false); room.SetActive(true); }
            foreach (var b in bars) b.enabled = letterbox;
            shade.color = glow.color = rune.color = ring.color = iconGlow.color = Color.clear;
            icon.enabled = false;
            foreach (var l in lines) { l.Text = ""; l.Alpha = 1; Visible(l, 99999); }
            small.Text = big.Text = banner.Text = prompt.Text = intro.Text = "";
            raysOn = runeOn = embers = streaks = 0;
            Update();
        }

        /// <summary>Puts the workshop back and hides everything this cutscene drew.</summary>
        public void Close()
        {
            root.gameObject.SetActive(false);
            room.SetActive(false); workshop.SetActive(true);
            workshop.transform.position = Vector3.zero;
            raysOn = runeOn = embers = streaks = 0;
        }

        // ---- the three stories ---------------------------------------------------------------------

        /// <summary>The opening story, three lines a page (docs/ATHANOR.md §8). The last page is the original text.</summary>
        static readonly string[][] IntroPages =
        {
            new[] { "story.intro.1", "story.intro.2", "story.intro.3" },
            new[] { "story.intro.4", "story.intro.5", "story.intro.6" },
            new[] { "story.intro.7", "story.intro.8", "story.intro.9" },
        };

        /// <summary>Starts and ends on a black screen.</summary>
        public IEnumerator Intro()
        {
            Begin();
            fx.Screen.SetBlack(1);
            Stage(true, true);
            embers = 1;
            yield return fx.Screen.FadeIn(introPage.fadeIn);
            ClearSkip();
            for (int p = 0; p < IntroPages.Length; p++)
            {
                if (p > 0)
                {
                    // the page before fades out, then the lines are reused
                    yield return Anim.Over(introPage.lineFade, t => { foreach (var l in lines) l.Alpha = 1 - t; });
                    foreach (var l in lines) { l.Text = ""; l.Alpha = 1; }
                    ClearSkip();
                }
                var page = IntroPages[p];
                bool last = p == IntroPages.Length - 1;
                for (int i = 0; i < page.Length; i++)
                {
                    yield return Type(lines[i], Loc.T(page[i]));
                    yield return Hold(i < page.Length - 1 ? introPage.hold : last ? introPage.lastHold : introPage.finalHold);
                }
            }
            prompt.Text = Loc.T("ui.common.click_to_begin");
            yield return Hold(introPage.promptHold);
            End();
            yield return fx.Screen.FadeOut(introPage.fadeOut);
            Close();
        }

        /// <summary>
        /// "Depth cleared" over the bench, the room slides away upward, the next depth is named, and the room rises back.
        /// midway runs while nothing is on screen (the flow uses it to change the lighting). Leaves the bench hidden.
        /// </summary>
        public IEnumerator Descent(int depth, string depthName, Color colour, Action midway)
        {
            Begin();
            Stage(false, false);
            tint = colour;
            // 1. the win, over the bench
            banner.Text = Loc.T("cutscene.depth_cleared");
            var win = descentBanner;
            fx.Screen.Flash(win.flash, win.flashSeconds); fx.Screen.Shake(win.shake, win.shakeSeconds);
            fx.Sparks.Burst(Px.At(CX, win.burstY), win.burst.count, win.burst.color, win.burst.speed, win.burst.life, win.burst.order, win.burst.gravity, win.burstSquash);
            yield return Sequence.Create()
                .Group(Motion.Fade(banner, 0, 1, win.rise))
                .Group(Motion.Move(banner.transform, Px.At(CX, win.fromY), Px.At(CX, win.toY), win.rise, Ease.OutBack))
                .ToYieldInstruction();
            yield return Hold(win.hold);
            // 2. down the stair: the room leaves upward
            bench.SetActive(false);
            streaks = 1;
            yield return Sequence.Create()
                .Group(Motion.Fade(banner, 1, 0, descentLeave.fade))
                .Group(Motion.Move(workshop.transform, Vector3.zero, new Vector3(0, descentLeave.scroll, 0), descentLeave.seconds, Ease.InCubic))
                .ToYieldInstruction();
            banner.Text = "";
            workshop.SetActive(false);
            midway?.Invoke();
            // 3. the name of the place
            small.Text = Loc.T("cutscene.depth", ("n", depth)); big.Text = depthName;
            string introKey = DepthLook.IntroKey(depth);       // Phase 5 UI mockup SPEC (f): the line under the name; none past depth 5
            intro.Text = introKey != null ? Loc.T(introKey) : "";
            intro.Alpha = 0;
            big.SetSize(depthName.Length > 20 ? 32 : 48);
            runeOn = 1;
            ClearSkip();
            yield return Sequence.Create()
                .Group(Motion.Fade(small, 0, 1, descentName.fade))
                .Group(Motion.Fade(big, 0, 1, descentName.fade))
                .Group(Motion.Move(big.transform, Px.At(CX, descentName.fromY), Px.At(CX, descentName.toY), descentName.rise, Ease.OutCubic))
                .ToYieldInstruction();
            if (introKey != null) yield return Sequence.Create().Group(Motion.Fade(intro, 0, 1, descentName.introFade)).ToYieldInstruction();
            yield return Hold(descentName.hold);
            // 4. arriving: the room rises into view
            workshop.SetActive(true);
            streaks = descentArrive.streaks;
            yield return Sequence.Create()
                .Group(Motion.Fade(small, 1, 0, descentArrive.fade))
                .Group(Motion.Fade(big, 1, 0, descentArrive.fade))
                .Group(Motion.Fade(intro, 1, 0, descentArrive.fade))
                .Group(Tween.Custom(this, 1f, 0f, descentArrive.runeFade, (s, v) => s.runeOn = v, Ease.Linear))
                .Group(Motion.Move(workshop.transform, new Vector3(0, -descentLeave.scroll, 0), Vector3.zero, descentArrive.seconds, Ease.OutCubic))
                .ToYieldInstruction();
            End();
            workshop.transform.position = Vector3.zero;
            root.gameObject.SetActive(false);
            streaks = runeOn = 0;
        }

        /// <summary>
        /// The depth's line over the bench at the start of a run (depth 1 has no descent): fades in, holds, fades out, about 2.2 s
        /// at most; a click shortens the hold. Leaves the bench as it was; the caller keeps its input off meanwhile.
        /// </summary>
        public IEnumerator DepthIntro(string key)
        {
            Begin();
            Stage(false, false);
            shade.color = depthLine.shade;
            intro.Text = Loc.T(key); intro.Alpha = 0;
            ClearSkip();
            yield return Sequence.Create().Group(Motion.Fade(intro, 0, 1, depthLine.fade)).ToYieldInstruction();
            yield return Hold(depthLine.hold);
            yield return Sequence.Create().Group(Motion.Fade(intro, 1, 0, depthLine.fade)).ToYieldInstruction();
            intro.Text = "";
            End();
            root.gameObject.SetActive(false);
        }

        /// <summary>The ultimate substance over the cauldron. Stays on screen afterwards (the summary goes on top) until Close.</summary>
        public IEnumerator Ending(MaterialData ultimate)
        {
            Begin();
            yield return fx.Screen.FadeOut(ending.fadeOut);
            Stage(true, true);
            tint = Px.Hex(string.IsNullOrEmpty(ultimate.glow) ? "#fff6b8" : ultimate.glow);
            icon.sprite = Art.Lib.Sprite(ultimate.id); icon.enabled = true;
            raysOn = 1; embers = 1;
            yield return fx.Screen.FadeIn(ending.fadeIn);
            ClearSkip();
            string[] text = { Loc.T("story.ending.1"), Loc.T("story.ending.2"), Loc.T("story.ending.3") };
            for (int i = 0; i < text.Length; i++)
            {
                yield return Type(lines[i], text[i]);
                yield return Hold(ending.hold);
            }
            fx.Screen.Flash(ending.flash, ending.flashSeconds);
            var finale = ending.burst; finale.color = Mul(tint, finale.color);
            finale.Play(fx.Sparks, Px.At(CX, ending.burstY));
            foreach (var l in lines) l.Text = "";
            banner.Text = Loc.T("cutscene.great_work"); banner.Alpha = 1; banner.transform.position = Px.At(CX, ending.bannerY);
            prompt.Text = Loc.T("ui.common.click_to_continue");
            yield return Hold(ending.promptHold);
            prompt.Text = banner.Text = "";
            End();
        }

        // ---- pieces --------------------------------------------------------------------------------

        /// <summary>The colour times a glow, channel by channel; the alpha is the colour's.</summary>
        static Color Mul(Color a, Color b) => new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a);

        static void Visible(Label l, int characters)
        {
            l.main.maxVisibleCharacters = characters;
            if (l.shadow != null) l.shadow.maxVisibleCharacters = characters;
        }

        /// <summary>Writes a line out letter by letter; a click shows the rest at once.</summary>
        IEnumerator Type(Label label, string text)
        {
            label.Text = text;
            for (float n = 0; n < text.Length; n += Time.deltaTime * typeRate)
            {
                if (Skipped) { ClearSkip(); break; }
                Visible(label, (int)n);
                yield return null;
            }
            Visible(label, 99999);
        }

        /// <summary>Waits; a click ends the wait.</summary>
        IEnumerator Hold(float seconds)
        {
            for (float t = 0; t < seconds; t += Time.deltaTime)
            {
                if (Skipped) { ClearSkip(); yield break; }
                yield return null;
            }
        }

        void Update()
        {
            if (root == null || !root.gameObject.activeSelf) return;
            float t = Time.time;
            // the floating prize and its rays
            var at = Px.Snap(Px.At(CX, look.prizeY + Mathf.Sin(t * look.bobSpeed) * look.bobAmplitude));
            icon.transform.position = at; iconGlow.transform.position = at;
            iconGlow.color = tint.Alpha(look.glowAlpha * raysOn);
            iconLight.transform.position = at; iconLight.color = tint; iconLight.intensity = look.lightIntensity * raysOn;
            for (int k = 0; k < rays.Length; k++)
            {
                rays[k].position = at;
                rays[k].rotation = Quaternion.Euler(0, 0, k * look.rayStep + t * look.raySpin);
                float spread = (k % 2 == 0 ? look.shortRay : 1) * raysOn;
                rays[k].localScale = new Vector3(spread, 1, 1);
                rayFaces[k].color = look.rayColor.Alpha(look.rayColor.a * raysOn);
            }
            // the rune circle and its widening ring under a depth's name
            rune.color = tint.Times(look.runeBright).Alpha(runeOn * (look.pulseBase + look.pulseAmplitude * Mathf.Sin(t * look.pulseSpeed)));
            float r = t * look.ringSpeed % 1;
            float ringX = (look.ringWidth + r * look.ringGrowW) / look.ringWidth, ringY = (look.ringHeight + r * look.ringGrowH) / look.ringHeight;
            ring.transform.localScale = new Vector3(ringX, ringY, 1);
            ring.color = tint.Times(look.runeBright).Alpha(runeOn * (1 - r) * look.ringAlpha);
            glow.color = tint.Alpha(look.runeGlow * runeOn);
            // the looping streams: embers off the fire, motes rushing upward while descending, motes off the rune
            if (emberLoop == null)
            {
                emberLoop = new Loop(root, "embers", emberSpec);
                streakLoop = new Loop(root, "streaks", streakSpec);
                runeLoop = new Loop(root, "rune motes", moteSpec);
            }
            emberLoop.Draw(t, embers, look.emberColor);
            streakLoop.Draw(t, streaks, tint.Times(look.streakBright).Alpha(look.streakAlpha));
            runeLoop.Draw(t, runeOn, tint.Times(look.moteBright));
        }

        // ---- looping particle streams (drawn by Update) ---------------------------------------------------

        Loop emberLoop, streakLoop, runeLoop;

        /// <summary>
        /// A stream of glowing particles played as a loop instead of emitted: rate x life fixed slots, each reborn every
        /// life seconds at a hashed place and speed, so its position is a function of the clock. A slot shows for a
        /// life when it is reborn while the stream is on (with probability strength), so turning a stream on fills it
        /// from its source and turning it off lets the particles in flight finish, as an emitter would. One SparkMesh.
        /// </summary>
        sealed class Loop
        {
            readonly SparkMesh mesh;
            readonly int[] cycle;
            readonly bool[] on;
            readonly float life, x0, x1, y0, y1, vx0, vx1, vy0, vy1, gravity, sparkle;

            public Loop(Transform parent, string name, LoopSpec s)
            {
                int n = Mathf.CeilToInt(s.rate * s.life);
                cycle = new int[n]; on = new bool[n];
                for (int i = 0; i < n; i++) cycle[i] = int.MinValue;
                life = s.life; x0 = s.x0; x1 = s.x1; y0 = s.y0; y1 = s.y1;
                vx0 = s.vx0; vx1 = s.vx1; vy0 = s.vy0; vy1 = s.vy1; gravity = s.gravity; sparkle = s.sparkle;
                mesh = SparkMesh.Create(parent, name, s.order, Art.Lib.Sprite("dot"), Art.Lib.Sprite("sparkle"));
            }

            public void Draw(float t, float strength, Color color)
            {
                int n = cycle.Length;
                mesh.Begin();
                for (int i = 0; i < n; i++)
                {
                    float clock = t / life + (i + 0.5f + (Hash(i, 0, 9) - 0.5f) * 0.9f) / n;
                    int c = Mathf.FloorToInt(clock);
                    // reborn this frame: shown for this life if the stream is on now (a missed birth stays dark)
                    if (cycle[i] != c) { on[i] = cycle[i] == c - 1 && Hash(i, c, 0) < strength; cycle[i] = c; }
                    if (!on[i]) continue;
                    float a = (clock - c) * life;
                    float bornX = Mathf.Lerp(x0, x1, Hash(i, c, 1)), bornY = Mathf.Lerp(y0, y1, Hash(i, c, 2));
                    var p = Px.At(bornX, bornY);
                    float vx = Mathf.Lerp(vx0, vx1, Hash(i, c, 3)), vy = Mathf.Lerp(vy0, vy1, Hash(i, c, 4));
                    float fall = 0.5f * gravity * a * a;
                    var pos = new Vector2(p.x + vx * a, p.y + vy * a - fall);
                    mesh.Add(pos, color.Alpha(color.a * Mathf.Clamp01((life - a) / life * 2)), Hash(i, c, 5) < sparkle);
                }
                mesh.End();
            }

            static float Hash(int i, int c, int k)
            {
                unchecked
                {
                    uint h = (uint)i * 0x9E3779B1u ^ (uint)c * 0x85EBCA77u ^ (uint)k * 0xC2B2AE3Du;
                    h ^= h >> 15; h *= 0x2C1B3C6Du; h ^= h >> 12; h *= 0x297A2D39u; h ^= h >> 15;
                    return (h & 0xFFFFFF) / 16777216f;
                }
            }
        }
    }
}
