using System;
using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The workshop window shows what the current depth "sees" and plays one tiny looping effect (docs/ATHANOR.md §8):
    /// 1 dust motes up a shaft, 2 drips down the glass, 3 a breathing amber glow, 4 roots creeping a row at a time,
    /// 5 a faint gold point that flickers. Effects are a fixed set of sprites moved from Time.time, so they stop with the
    /// pause and allocate nothing per frame.
    /// </summary>
    public sealed class WindowView : MonoBehaviour
    {
        const int Order = 2, Motes = 6, Drips = 2, RootRows = 89, RootsFrom = 20;

        /// <summary>The periods of the effects, in seconds; Main.unity holds the values (see LayoutPinTests).</summary>
        [Serializable]
        public struct Timing
        {
            /// <summary>One mote rises in dustPeriod; a drip's whole cycle is dripPeriod and it falls for the first dripFall of it (a fraction).</summary>
            public float dustPeriod, dripPeriod, dripFall;
            /// <summary>The glow swells and fades once in breathePeriod.</summary>
            public float breathePeriod;
            /// <summary>The point dips every flickerPeriod, for the first flickerDip of it, at flickerRate.</summary>
            public float flickerPeriod, flickerDip, flickerRate;
            /// <summary>The roots give back one more row every rootStep.</summary>
            public float rootStep;
        }

        /// <summary>Where the effects sit, as offsets from the pane's centre (art pixels, y up), and the paths they follow.</summary>
        [Serializable]
        public struct Layout
        {
            /// <summary>Motes: x range, y range (bottom to top). Drips: x range, y at the top and how far one falls.</summary>
            public Vector2 moteX, moteY, dripX;
            public float dripTop, dripDrop;
            /// <summary>The amber glow and the point of light.</summary>
            public Vector2 glowAt, pointAt;
        }

        /// <summary>Colours and strengths of the effects.</summary>
        [Serializable]
        public struct Look
        {
            /// <summary>Mote colour (alpha is the most it shows), glow colour (alpha is the least; it swells by glowSwing), point colour (alpha is its steady strength).</summary>
            public Color mote, glow, point;
            public float glowSwing;
            /// <summary>The point's dip: its least alpha and the swing added to it.</summary>
            public float dipBase, dipSwing;
            /// <summary>What the roots view's roots are painted over before they creep back (its background).</summary>
            public Color rootBackground;
        }

        // the pane is the workshop's window sprite; the rest are the effect sprites under the WindowFx object (all in Main.unity)
        [SerializeField] SpriteRenderer pane, glow, point;
        [SerializeField] SpriteRenderer[] motes = new SpriteRenderer[Motes], drips = new SpriteRenderer[Drips];
        [SerializeField] Timing timing;
        [SerializeField] Layout layout;
        [SerializeField] Look look;
        int view, shownDepth;
        bool soft;
        Vector3 centre;       // the pane's centre in the flat game: the effects are placed in flat coordinates, below a group at the pane's depth
        Sprite night;         // the original night-sky window, kept for the title and the tutorial

        // depth 4: a copy of the roots view with the root pixels painted out, then given back a row at a time from the top
        Texture2D rootTex; Sprite rootSprite; Color32[] rootSrc, rootNow; int rootRevealed; float rootClock;

        /// <summary>Remembers the night sky and the pane's flat centre; the pane and the effect sprites are serialized (no pane: the view does nothing).</summary>
        public void Bind()
        {
            if (pane == null) return;
            night = pane.sprite;
            var layer = pane.GetComponent<DepthLayer>();
            centre = layer != null ? (Vector3)layer.rest : pane.transform.position;
        }

        /// <summary>The pre-blurred window pictures (depth effects on) instead of the sharp ones (off). Redraws the pane.</summary>
        public bool Soft
        {
            get => soft;
            set
            {
                if (soft == value) return;
                soft = value;
                if (pane == null) return;
                if (shownDepth > 0) Show(shownDepth); else ShowNight();
            }
        }

        /// <summary>Swaps the view and starts its effect for a depth (1..5; deeper keeps 5).</summary>
        public void Show(int depth)
        {
            if (pane == null) return;
            view = DepthLook.View(depth);
            shownDepth = depth;
            ReleaseRoots();
            HideEffects();
            string name = DepthLook.WindowSprite(depth);
            var art = Art.Lib.Sprite(soft ? name + "_far" : name) ?? Art.Lib.Sprite(name);
            pane.sprite = art;
            if (view == 4) StartRoots(art);
        }

        /// <summary>Back to the original night sky with no effect (title screen, first-run tutorial, How to play).</summary>
        public void ShowNight()
        {
            if (pane == null) return;
            view = 0; shownDepth = 0;
            ReleaseRoots();
            HideEffects();
            if (night != null) pane.sprite = night;
        }

        void HideEffects()
        {
            foreach (var m in motes) m.enabled = false;
            foreach (var d in drips) d.enabled = false;
            glow.enabled = point.enabled = false;
        }

        void Update()
        {
            if (pane == null || !pane.gameObject.activeInHierarchy) return;
            float t = Time.time;
            var c = centre;
            switch (view)
            {
                case 1: Dust(t, c); break;
                case 2: Drip(t, c); break;
                case 3: Breathe(t, c); break;
                case 4: Creep(); break;
                case 5: Flicker(t, c); break;
            }
        }

        // ---- effects --------------------------------------------------------------------------------

        /// <summary>Pale motes rise slowly up the shaft at the pane's right of centre.</summary>
        void Dust(float t, Vector3 c)
        {
            for (int i = 0; i < Motes; i++)
            {
                float clock = t / timing.dustPeriod + i / (float)Motes;
                int n = Mathf.FloorToInt(clock); float p = clock - n;
                motes[i].enabled = true;
                motes[i].color = look.mote.Alpha(Mathf.Sin(p * Mathf.PI) * look.mote.a);
                float dx = Mathf.Lerp(layout.moteX.x, layout.moteX.y, Hash(i, n)), dy = Mathf.Lerp(layout.moteY.x, layout.moteY.y, p);
                motes[i].transform.localPosition = Px.Snap(c + new Vector3(dx, dy, 0));
            }
        }

        /// <summary>A drip runs down the glass about every 1.6 s (two sprites taking turns).</summary>
        void Drip(float t, Vector3 c)
        {
            for (int i = 0; i < Drips; i++)
            {
                float clock = t / timing.dripPeriod + i * 0.5f;
                int n = Mathf.FloorToInt(clock); float p = (clock - n) / timing.dripFall;      // falls for the first part of the cycle
                drips[i].enabled = p < 1;
                if (p >= 1) continue;
                float dx = Mathf.Lerp(layout.dripX.x, layout.dripX.y, Hash(i + 7, n)), dy = layout.dripTop - p * p * layout.dripDrop;
                drips[i].transform.localPosition = Px.Snap(c + new Vector3(dx, dy, 0));
            }
        }

        /// <summary>The far amber glow (drawn off-centre right in the view) swells and fades like a breath, 5 s a cycle.</summary>
        void Breathe(float t, Vector3 c)
        {
            glow.enabled = true;
            glow.transform.localPosition = Px.Snap(c + (Vector3)layout.glowAt);
            glow.color = look.glow.Alpha(look.glow.a + look.glowSwing * (0.5f + 0.5f * Mathf.Sin(t * Mathf.PI * 2 / timing.breathePeriod)));
        }

        /// <summary>The point of light low in the glass, steady but for a quick dip every 7 s.</summary>
        void Flicker(float t, Vector3 c)
        {
            point.enabled = true;
            point.transform.localPosition = Px.Snap(c + (Vector3)layout.pointAt);
            float k = t % timing.flickerPeriod;
            float a = k < timing.flickerDip ? look.dipBase + look.dipSwing * Mathf.Abs(Mathf.Sin(k * timing.flickerRate)) * 0.5f : look.point.a;
            point.color = look.point.Alpha(a);
        }

        // ---- roots ----------------------------------------------------------------------------------

        void StartRoots(Sprite art)
        {
            var src = art.texture;
            if (src == null || !src.isReadable) return;                // not readable: the static roots stay as drawn
            rootSrc = src.GetPixels32();
            rootNow = new Color32[rootSrc.Length];
            Color32 bg = look.rootBackground;
            for (int i = 0; i < rootSrc.Length; i++) rootNow[i] = rootSrc[i].r >= 150 && rootSrc[i].a > 0 ? bg : rootSrc[i];
            rootTex = new Texture2D(src.width, src.height, TextureFormat.RGBA32, false) { filterMode = FilterMode.Point, wrapMode = TextureWrapMode.Clamp };
            rootSprite = Sprite.Create(rootTex, new Rect(0, 0, src.width, src.height), Vector2.one * 0.5f, art.pixelsPerUnit);
            rootRevealed = 0; rootClock = 0;
            RevealTo(RootsFrom);
            pane.sprite = rootSprite;
        }

        void Creep()
        {
            if (rootTex == null || rootRevealed >= RootRows) return;
            rootClock += Time.deltaTime;
            if (rootClock < timing.rootStep) return;
            rootClock -= timing.rootStep;
            RevealTo(rootRevealed + 1);
        }

        /// <summary>Gives the roots back for the top n rows of the view.</summary>
        void RevealTo(int rows)
        {
            int w = rootTex.width;
            for (int r = rootRevealed; r < Mathf.Min(rows, RootRows); r++)
            {
                int y = rootTex.height - 1 - r;
                System.Array.Copy(rootSrc, y * w, rootNow, y * w, w);
            }
            rootRevealed = Mathf.Min(rows, RootRows);
            rootTex.SetPixels32(rootNow); rootTex.Apply(false);
        }

        void ReleaseRoots()
        {
            if (rootSprite != null) Destroy(rootSprite);
            if (rootTex != null) Destroy(rootTex);
            rootSprite = null; rootTex = null; rootSrc = rootNow = null;
        }

        static float Hash(int i, int c)
        {
            unchecked
            {
                uint h = (uint)i * 0x9E3779B1u ^ (uint)c * 0x85EBCA77u;
                h ^= h >> 15; h *= 0x2C1B3C6Du; h ^= h >> 12; h *= 0x297A2D39u; h ^= h >> 15;
                return (h & 0xFFFFFF) / 16777216f;
            }
        }
    }
}
