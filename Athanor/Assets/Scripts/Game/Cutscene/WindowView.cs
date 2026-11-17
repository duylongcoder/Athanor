using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// The workshop window shows what the current depth "sees" and plays one tiny looping effect (docs/devlogs/phase-5-lore.md):
    /// 1 dust motes up a shaft, 2 drips down the glass, 3 a breathing amber glow, 4 roots creeping a row at a time,
    /// 5 a faint gold point that flickers. Effects are a fixed set of sprites moved from Time.time, so they stop with the
    /// pause and allocate nothing per frame.
    /// </summary>
    public sealed class WindowView : MonoBehaviour
    {
        const int Order = 2, Motes = 6, Drips = 2, RootRows = 89, RootsFrom = 20;
        const float RootStep = 0.45f;

        SpriteRenderer pane, glow, point;
        readonly SpriteRenderer[] motes = new SpriteRenderer[Motes], drips = new SpriteRenderer[Drips];
        int view, shownDepth;
        bool soft;
        Vector3 centre;       // the pane's centre in the flat game: the effects are placed in flat coordinates, below a group at the pane's depth
        Sprite night;         // the original night-sky window, kept for the title and the tutorial

        // depth 4: a copy of the roots view with the root pixels painted out, then given back a row at a time from the top
        Texture2D rootTex; Sprite rootSprite; Color32[] rootSrc, rootNow; int rootRevealed; float rootClock;

        /// <summary>Finds the pane in the workshop and makes the effect sprites (hidden).</summary>
        public void Build(Transform workshop)
        {
            var found = workshop.Find("window");
            pane = found != null ? found.GetComponent<SpriteRenderer>() : null;
            if (pane == null) return;
            night = pane.sprite;
            var layer = pane.GetComponent<DepthLayer>();
            var at = centre = layer != null ? (Vector3)layer.rest : pane.transform.position;
            var fx = Make.Root("WindowFx", workshop);
            for (int i = 0; i < Motes; i++) { motes[i] = Make.Sprite(fx, "dot", at, Order); motes[i].transform.localScale = new Vector3(0.25f, 0.25f, 1); }
            for (int i = 0; i < Drips; i++) drips[i] = Make.Sprite(fx, "window_drip", at, Order);
            glow = Make.Glow(fx, at, 18, Color.clear, Order);
            point = Make.Sprite(fx, "dot", at, Order); point.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            HideEffects();
            // in the HD-2D bench the effects sit just in front of the pane, in its depth
            if (layer != null) DepthLayer.AddGroup(fx.gameObject, layer.z + 0.5f).Apply(DepthMath.RestDistance);
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
            var art = Make.Lib.Sprite(soft ? name + "_far" : name) ?? Make.Lib.Sprite(name);
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
                float clock = t / 9f + i / (float)Motes;
                int n = Mathf.FloorToInt(clock); float p = clock - n;
                motes[i].enabled = true;
                motes[i].color = new Color(0.85f, 0.85f, 0.8f, Mathf.Sin(p * Mathf.PI) * 0.55f);
                motes[i].transform.localPosition = Px.Snap(c + new Vector3(Mathf.Lerp(4, 14, Hash(i, n)), Mathf.Lerp(-22, 40, p), 0));
            }
        }

        /// <summary>A drip runs down the glass about every 1.6 s (two sprites taking turns).</summary>
        void Drip(float t, Vector3 c)
        {
            for (int i = 0; i < Drips; i++)
            {
                float clock = t / 3.2f + i * 0.5f;
                int n = Mathf.FloorToInt(clock); float p = (clock - n) / 0.3f;      // falls for the first 0.3 of the cycle
                drips[i].enabled = p < 1;
                if (p >= 1) continue;
                drips[i].transform.localPosition = Px.Snap(c + new Vector3(Mathf.Lerp(-16, 16, Hash(i + 7, n)), 34 - p * p * 66, 0));
            }
        }

        /// <summary>The far amber glow (drawn off-centre right in the view) swells and fades like a breath, 5 s a cycle.</summary>
        void Breathe(float t, Vector3 c)
        {
            glow.enabled = true;
            glow.transform.localPosition = Px.Snap(c + new Vector3(22, -12, 0));
            glow.color = new Color(1f, 0.68f, 0.28f, 0.08f + 0.2f * (0.5f + 0.5f * Mathf.Sin(t * Mathf.PI * 2 / 5f)));
        }

        /// <summary>The point of light low in the glass, steady but for a quick dip every 7 s.</summary>
        void Flicker(float t, Vector3 c)
        {
            point.enabled = true;
            point.transform.localPosition = Px.Snap(c + new Vector3(12, -30, 0));
            float k = t % 7f;
            float a = k < 0.28f ? 0.1f + 0.5f * Mathf.Abs(Mathf.Sin(k * 22f)) * 0.5f : 0.5f;
            point.color = new Color(1f, 0.82f, 0.4f, a);
        }

        // ---- roots ----------------------------------------------------------------------------------

        void StartRoots(Sprite art)
        {
            var src = art.texture;
            if (src == null || !src.isReadable) return;                // not readable: the static roots stay as drawn
            rootSrc = src.GetPixels32();
            rootNow = new Color32[rootSrc.Length];
            var bg = new Color32(23, 19, 28, 255);
            for (int i = 0; i < rootSrc.Length; i++) rootNow[i] = rootSrc[i].r >= 150 && rootSrc[i].a > 0 ? bg : rootSrc[i];
            rootTex = new Texture2D(src.width, src.height, TextureFormat.RGBA32, false) { filterMode = FilterMode.Point, wrapMode = TextureWrapMode.Clamp };
            rootSprite = Sprite.Create(rootTex, new Rect(0, 0, src.width, src.height), new Vector2(0.5f, 0.5f), art.pixelsPerUnit);
            rootRevealed = 0; rootClock = 0;
            RevealTo(RootsFrom);
            pane.sprite = rootSprite;
        }

        void Creep()
        {
            if (rootTex == null || rootRevealed >= RootRows) return;
            rootClock += Time.deltaTime;
            if (rootClock < RootStep) return;
            rootClock -= RootStep;
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
