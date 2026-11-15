using System;
using System.Collections;
using Athanor.Core;
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
        const int O = 420;
        const float CX = 320, Scroll = 400;

        GameObject room, workshop, bench;
        Transform root;
        SpriteRenderer shade, glow, ring, rune, icon, iconGlow;
        readonly SpriteRenderer[] bars = new SpriteRenderer[4];
        readonly Transform[] rays = new Transform[12];
        readonly Label[] lines = new Label[3];
        Label small, big, banner, prompt, intro;
        Light2D iconLight;
        Color tint = Color.white;
        float raysOn, runeOn, embers, streaks;      // strengths of the looping effects, driven by the scripts below

        public void Build(GameObject cutsceneRoom, GameObject workshopRoot, GameObject benchRoot)
        {
            room = cutsceneRoom; workshop = workshopRoot; bench = benchRoot;
            root = Make.Root("Story");
            var dark = Px.Hex("#0c0706");
            shade = Make.Rect(root, 0, 0, Px.W, Px.H, Color.clear, O);
            bars[0] = Make.Rect(root, 0, 0, Px.W, 30, Color.black, O + 9); bars[1] = Make.Rect(root, 0, Px.H - 30, Px.W, 30, Color.black, O + 9);
            bars[2] = Make.Rect(root, 0, 30, Px.W, 1, Px.Hex("#8c5a14"), O + 10); bars[3] = Make.Rect(root, 0, Px.H - 31, Px.W, 1, Px.Hex("#8c5a14"), O + 10);
            glow = Make.Glow(root, Px.At(CX, 170), 150, Color.clear, O + 1);
            rune = Make.Sprite(root, "rune_circle", Px.At(CX, 262), O + 2, Make.Lib.glow);
            ring = Make.Sprite(root, "floor_ring", Px.At(CX, 262), O + 2, Make.Lib.glow);
            for (int k = 0; k < rays.Length; k++)
            {
                rays[k] = Make.Root("ray", root);
                Make.Sprite(rays[k], "ray", Vector3.zero, O + 3, Make.Lib.glow).transform.localPosition = new Vector3(26 + 75, 0, 0);
            }
            iconGlow = Make.Glow(root, Vector3.zero, 80, Color.clear, O + 4);
            icon = Make.Sprite(root, "dot", Vector3.zero, O + 5);
            iconLight = Make.Light(root, Vector3.zero, 250, Color.white, 0);
            icon.transform.localScale = new Vector3(3, 3, 1);
            for (int i = 0; i < lines.Length; i++)
                lines[i] = Make.Text(root, "", CX, 38 + i * 16, FontKind.Body, 16, Px.Hex("#f0dcc0"), O + 12, Align.Centre, dark);
            small = Make.Text(root, "", CX, 104, FontKind.Tiny, 8, Px.Hex("#f8d250"), O + 12, Align.Centre, dark);
            big = Make.Text(root, "", CX, 128, FontKind.Title, 48, Px.Hex("#f0dcc0"), O + 12, Align.Centre, dark);
            big.LineBox = true;      // SPEC (f): the name sits in the mock's 48 px line box; Crux's short ascent would put VI ~11 px high
            intro = Make.Text(root, "", CX, 186, FontKind.Body, 16, Px.Hex("#f0dcc0"), O + 12, Align.Centre, dark, 540);
            banner = Make.Text(root, "", CX, 88, FontKind.Title, 32, Px.Hex("#f8d250"), O + 12, Align.Centre, Px.Hex("#3a1a08"));
            prompt = Make.Text(root, "", CX, Px.H - 19, FontKind.Tiny, 8, Px.Hex("#7a5a3a"), O + 12, Align.Centre);
            root.gameObject.SetActive(false);
        }

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

        /// <summary>The opening story, three lines a page (docs/devlogs/phase-4-lore.md). The last page is the original text.</summary>
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
            ScreenFx.I.SetBlack(1);
            Stage(true, true);
            embers = 1;
            yield return ScreenFx.I.FadeIn(0.9f);
            ClearSkip();
            for (int p = 0; p < IntroPages.Length; p++)
            {
                if (p > 0)
                {
                    // the page before fades out, then the lines are reused
                    yield return Anim.Over(0.35f, t => { foreach (var l in lines) l.Alpha = 1 - t; });
                    foreach (var l in lines) { l.Text = ""; l.Alpha = 1; }
                    ClearSkip();
                }
                var page = IntroPages[p];
                bool last = p == IntroPages.Length - 1;
                for (int i = 0; i < page.Length; i++)
                {
                    yield return Type(lines[i], Loc.T(page[i]));
                    yield return Hold(i < page.Length - 1 ? 0.8f : last ? 0.3f : 1.6f);
                }
            }
            prompt.Text = Loc.T("ui.common.click_to_begin");
            yield return Hold(8);
            End();
            yield return ScreenFx.I.FadeOut(0.4f);
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
            ScreenFx.I.Flash(0.3f, 0.5f); ScreenFx.I.Shake(2, 0.3f);
            Sparks.I.Burst(Px.At(CX, 170), 70, new Color(2.2f, 1.7f, 0.6f), 210, 1.1f, O + 11, 50, 0.6f);
            yield return Sequence.Create()
                .Group(Motion.Fade(banner, 0, 1, 0.3f))
                .Group(Motion.Move(banner.transform, Px.At(CX, 102), Px.At(CX, 88), 0.3f, Ease.OutBack))
                .ToYieldInstruction();
            yield return Hold(0.9f);
            // 2. down the stair: the room leaves upward
            bench.SetActive(false);
            streaks = 1;
            yield return Sequence.Create()
                .Group(Motion.Fade(banner, 1, 0, 0.3f))
                .Group(Motion.Move(workshop.transform, Vector3.zero, new Vector3(0, Scroll, 0), 0.9f, Ease.InCubic))
                .ToYieldInstruction();
            banner.Text = "";
            workshop.SetActive(false);
            midway?.Invoke();
            // 3. the name of the place
            small.Text = Loc.T("cutscene.depth", ("n", depth)); big.Text = depthName;
            string introKey = DepthLook.IntroKey(depth);       // docs/mockup/phase5-ui SPEC (f): the line under the name; none past depth 5
            intro.Text = introKey != null ? Loc.T(introKey) : "";
            intro.Alpha = 0;
            big.SetSize(depthName.Length > 20 ? 32 : 48);
            runeOn = 1;
            ClearSkip();
            yield return Sequence.Create()
                .Group(Motion.Fade(small, 0, 1, 0.45f))
                .Group(Motion.Fade(big, 0, 1, 0.45f))
                .Group(Motion.Move(big.transform, Px.At(CX, 136), Px.At(CX, 128), 0.45f, Ease.OutCubic))
                .ToYieldInstruction();
            if (introKey != null) yield return Sequence.Create().Group(Motion.Fade(intro, 0, 1, 0.3f)).ToYieldInstruction();
            yield return Hold(2.4f);
            // 4. arriving: the room rises into view
            workshop.SetActive(true);
            streaks = 0.4f;
            yield return Sequence.Create()
                .Group(Motion.Fade(small, 1, 0, 0.36f))
                .Group(Motion.Fade(big, 1, 0, 0.36f))
                .Group(Motion.Fade(intro, 1, 0, 0.36f))
                .Group(Tween.Custom(this, 1f, 0f, 0.45f, (s, v) => s.runeOn = v, Ease.Linear))
                .Group(Motion.Move(workshop.transform, new Vector3(0, -Scroll, 0), Vector3.zero, 0.9f, Ease.OutCubic))
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
            shade.color = new Color(0, 0, 0, 0.35f);
            intro.Text = Loc.T(key); intro.Alpha = 0;
            ClearSkip();
            yield return Sequence.Create().Group(Motion.Fade(intro, 0, 1, 0.3f)).ToYieldInstruction();
            yield return Hold(1.6f);
            yield return Sequence.Create().Group(Motion.Fade(intro, 1, 0, 0.3f)).ToYieldInstruction();
            intro.Text = "";
            End();
            root.gameObject.SetActive(false);
        }

        /// <summary>The ultimate substance over the cauldron. Stays on screen afterwards (the summary goes on top) until Close.</summary>
        public IEnumerator Ending(MaterialData ultimate)
        {
            Begin();
            yield return ScreenFx.I.FadeOut(0.5f);
            Stage(true, true);
            tint = Px.Hex(string.IsNullOrEmpty(ultimate.glow) ? "#fff6b8" : ultimate.glow);
            icon.sprite = Make.Lib.Sprite(ultimate.id); icon.enabled = true;
            raysOn = 1; embers = 1;
            yield return ScreenFx.I.FadeIn(1.1f);
            ClearSkip();
            string[] text = { Loc.T("story.ending.1"), Loc.T("story.ending.2"), Loc.T("story.ending.3") };
            for (int i = 0; i < text.Length; i++)
            {
                yield return Type(lines[i], text[i]);
                yield return Hold(0.9f);
            }
            ScreenFx.I.Flash(0.5f, 0.8f);
            Sparks.I.Burst(Px.At(CX, 150), 90, tint.Times(2.4f), 220, 1.4f, O + 6, 30);
            foreach (var l in lines) l.Text = "";
            banner.Text = Loc.T("cutscene.great_work"); banner.Alpha = 1; banner.transform.position = Px.At(CX, 44);
            prompt.Text = Loc.T("ui.common.click_to_continue");
            yield return Hold(10);
            prompt.Text = banner.Text = "";
            End();
        }

        // ---- pieces --------------------------------------------------------------------------------

        static void Visible(Label l, int characters)
        {
            l.main.maxVisibleCharacters = characters;
            if (l.shadow != null) l.shadow.maxVisibleCharacters = characters;
        }

        /// <summary>Writes a line out letter by letter; a click shows the rest at once.</summary>
        IEnumerator Type(Label label, string text)
        {
            label.Text = text;
            for (float n = 0; n < text.Length; n += Time.deltaTime * 42)
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
            var at = Px.Snap(Px.At(CX, 122 + Mathf.Sin(t * 2.2f) * 3));
            icon.transform.position = at; iconGlow.transform.position = at;
            iconGlow.color = tint.Alpha(0.45f * raysOn);
            iconLight.transform.position = at; iconLight.color = tint; iconLight.intensity = 1.2f * raysOn;
            for (int k = 0; k < rays.Length; k++)
            {
                rays[k].position = at;
                rays[k].rotation = Quaternion.Euler(0, 0, k * 30f + t * 14);
                rays[k].localScale = new Vector3((k % 2 == 0 ? 0.75f : 1) * raysOn, 1, 1);
                rays[k].GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 0.95f, 0.75f, 0.3f * raysOn);
            }
            // the rune circle and its widening ring under a depth's name
            rune.color = tint.Times(1.5f).Alpha(runeOn * (0.7f + 0.3f * Mathf.Sin(t * 4)));
            float r = t * 0.5f % 1;
            ring.transform.localScale = new Vector3((60 + r * 320) / 60f, (14 + r * 76) / 14f, 1);
            ring.color = tint.Times(1.5f).Alpha(runeOn * (1 - r) * 0.8f);
            glow.color = tint.Alpha(0.16f * runeOn);
            // the looping streams: embers off the fire, motes rushing upward while descending, motes off the rune
            if (emberLoop == null)
            {
                emberLoop = new Loop(root, "embers", 310, 22, 1.5f, CX - 56, CX + 56, 286, 286, -10, 10, 22, 60, -6, 0.25f);
                streakLoop = new Loop(root, "streaks", O + 1, 70, 1.1f, 0, Px.W, Px.H + 4, Px.H + 4, 0, 0, 380, 620, 0, 0);
                runeLoop = new Loop(root, "rune motes", O + 3, 26, 1.3f, CX - 140, CX + 140, 248, 276, 0, 0, 20, 60, -4, 0.3f);
            }
            emberLoop.Draw(t, embers, new Color(2.4f, 1.2f, 0.4f));
            streakLoop.Draw(t, streaks, tint.Times(1.6f).Alpha(0.75f));
            runeLoop.Draw(t, runeOn, tint.Times(2));
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

            public Loop(Transform parent, string name, int order, float rate, float life, float x0, float x1, float y0, float y1,
                float vx0, float vx1, float vy0, float vy1, float gravity, float sparkle)
            {
                int n = Mathf.CeilToInt(rate * life);
                cycle = new int[n]; on = new bool[n];
                for (int i = 0; i < n; i++) cycle[i] = int.MinValue;
                this.life = life; this.x0 = x0; this.x1 = x1; this.y0 = y0; this.y1 = y1;
                this.vx0 = vx0; this.vx1 = vx1; this.vy0 = vy0; this.vy1 = vy1; this.gravity = gravity; this.sparkle = sparkle;
                mesh = SparkMesh.Create(parent, name, order, Make.Lib.Sprite("dot"), Make.Lib.Sprite("sparkle"));
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
                    var p = Px.At(Mathf.Lerp(x0, x1, Hash(i, c, 1)), Mathf.Lerp(y0, y1, Hash(i, c, 2)));
                    float vx = Mathf.Lerp(vx0, vx1, Hash(i, c, 3)), vy = Mathf.Lerp(vy0, vy1, Hash(i, c, 4));
                    var pos = new Vector2(p.x + vx * a, p.y + vy * a - 0.5f * gravity * a * a);
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
