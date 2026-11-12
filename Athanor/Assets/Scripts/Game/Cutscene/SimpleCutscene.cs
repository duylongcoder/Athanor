using System;
using System.Collections;
using Athanor.Core;
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
        Label small, big, banner, prompt;
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
            small = Make.Text(root, "", CX, 128, FontKind.Tiny, 8, Px.Hex("#f8d250"), O + 12, Align.Centre, dark);
            big = Make.Text(root, "", CX, 140, FontKind.Title, 48, Px.Hex("#f0dcc0"), O + 12, Align.Centre, dark);
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
            small.Text = big.Text = banner.Text = prompt.Text = "";
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

        /// <summary>Starts and ends on a black screen.</summary>
        public IEnumerator Intro()
        {
            Begin();
            ScreenFx.I.SetBlack(1);
            Stage(true, true);
            embers = 1;
            yield return ScreenFx.I.FadeIn(0.9f);
            ClearSkip();
            string[] text =
            {
                "Under the old workshop, the cellar keeps going down.",
                "Each depth asks for a stronger brew. Nothing down here is labelled.",
                "So mix. Watch what happens. Remember.",
            };
            for (int i = 0; i < text.Length; i++)
            {
                yield return Type(lines[i], text[i]);
                yield return Hold(i < text.Length - 1 ? 0.8f : 0.3f);
            }
            prompt.Text = "CLICK TO BEGIN";
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
            banner.Text = "Depth Cleared";
            ScreenFx.I.Flash(0.3f, 0.5f); ScreenFx.I.Shake(2, 0.3f);
            Sparks.I.Burst(Px.At(CX, 170), 70, new Color(2.2f, 1.7f, 0.6f), 210, 1.1f, O + 11, 50, 0.6f);
            yield return Anim.Over(0.3f, t => { banner.Alpha = t; banner.transform.position = Px.At(CX, 88 + Mathf.Round((1 - Anim.OutBack(t)) * 14)); });
            yield return Hold(0.9f);
            // 2. down the stair: the room leaves upward
            bench.SetActive(false);
            streaks = 1;
            yield return Anim.Over(0.9f, t =>
            {
                banner.Alpha = 1 - Mathf.Clamp01(t * 3);
                workshop.transform.position = new Vector3(0, Mathf.Round(Anim.InCubic(t) * Scroll), 0);
            });
            banner.Text = "";
            workshop.SetActive(false);
            midway?.Invoke();
            // 3. the name of the place
            small.Text = "DEPTH " + depth; big.Text = depthName;
            big.main.fontSize = big.shadow.fontSize = depthName.Length > 20 ? 32 : 48;
            runeOn = 1;
            ClearSkip();
            yield return Anim.Over(0.45f, t =>
            {
                small.Alpha = big.Alpha = t;
                big.transform.position = Px.At(CX, 140 + Mathf.Round((1 - Anim.OutCubic(t)) * 8));
            });
            yield return Hold(1.7f);
            // 4. arriving: the room rises into view
            workshop.SetActive(true);
            streaks = 0.4f;
            yield return Anim.Over(0.9f, t =>
            {
                small.Alpha = big.Alpha = 1 - Mathf.Clamp01(t * 2.5f);
                runeOn = 1 - Mathf.Clamp01(t * 2);
                workshop.transform.position = new Vector3(0, Mathf.Round((Anim.OutCubic(t) - 1) * Scroll), 0);
            });
            End();
            workshop.transform.position = Vector3.zero;
            root.gameObject.SetActive(false);
            streaks = runeOn = 0;
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
            string[] text =
            {
                "At the bottom of everything: a seed.",
                "It hums with every reaction that came before it.",
                "Plant it, and the cellar will grow a little deeper.",
            };
            for (int i = 0; i < text.Length; i++)
            {
                yield return Type(lines[i], text[i]);
                yield return Hold(0.9f);
            }
            ScreenFx.I.Flash(0.5f, 0.8f);
            Sparks.I.Burst(Px.At(CX, 150), 90, tint.Times(2.4f), 220, 1.4f, O + 6, 30);
            foreach (var l in lines) l.Text = "";
            banner.Text = "The Great Work"; banner.Alpha = 1; banner.transform.position = Px.At(CX, 44);
            prompt.Text = "CLICK TO CONTINUE";
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
            float t = Time.time, dt = Time.deltaTime;
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
            if (Sparks.I == null) return;
            // embers off the fire
            if (embers > 0 && UnityEngine.Random.value < 22 * dt)
                Sparks.I.Emit(Px.At(CX + UnityEngine.Random.Range(-56f, 56f), 286), new Vector2(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(22f, 60f)),
                    new Color(2.4f, 1.2f, 0.4f), 1.5f, 310, -6, UnityEngine.Random.value < 0.25f);
            // motes rushing upward while descending
            for (int n = Mathf.FloorToInt(t * 70 * streaks) - Mathf.FloorToInt((t - dt) * 70 * streaks); n > 0; n--)
                Sparks.I.Emit(Px.At(UnityEngine.Random.Range(0f, Px.W), Px.H + 4), new Vector2(0, UnityEngine.Random.Range(380f, 620f)),
                    tint.Times(1.6f).Alpha(0.75f), 1.1f, O + 1);
            if (runeOn > 0 && UnityEngine.Random.value < 26 * dt * runeOn)
                Sparks.I.Emit(Px.At(CX + UnityEngine.Random.Range(-140f, 140f), 262 + UnityEngine.Random.Range(-14f, 14f)),
                    new Vector2(0, UnityEngine.Random.Range(20f, 60f)), tint.Times(2), 1.3f, O + 3, -4, UnityEngine.Random.value < 0.3f);
        }
    }
}
