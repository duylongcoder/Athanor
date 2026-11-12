using System.Collections;
using Athanor.Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Athanor.Game
{
    /// <summary>One of the nine bench tiles: its hover highlight and, when occupied, the material standing on it.</summary>
    public sealed class TileView : MonoBehaviour
    {
        public int index;
        public Vector3 centre;            // world position of the middle of the top face
        Vector2 faceCentre, faceSize;
        SpriteRenderer face, ring, shadow, icon, halo;
        Light2D glowLight;
        Color glow;
        bool glows;
        float flash, appear = 1;
        Color flashColor;

        public string Material { get; private set; }
        public bool Hovered { get; set; }
        public SpriteRenderer Icon => icon;

        public void Build(int i, TileData data, Transform parent)
        {
            index = i;
            transform.SetParent(parent, false);
            centre = Px.At(data.x, data.y);
            var f = data.face;
            faceCentre = Px.Mid(f.x, f.y, f.w, f.h); faceSize = new Vector2(f.w, f.h);
            int row = i / 3;
            face = Make.SpriteAt(transform, "tile_face_" + i, f.x, f.y, 10, Make.Lib.glow);
            ring = Make.Sprite(transform, "tile_ring_" + row, centre, 11, Make.Lib.glow);
            shadow = Make.Glow(transform, centre + new Vector3(0, -3, 0), 9, new Color(0, 0, 0, 0.6f), 12);
            shadow.sharedMaterial = Make.Lib.unlit; shadow.transform.localScale = new Vector3(18 / 64f, 7 / 64f, 1);
            halo = Make.Glow(transform, centre + new Vector3(0, 8, 0), 24, Color.clear, 12);
            icon = Make.Sprite(transform, "dot", centre, 14 + row * 2, Make.Lib.lit);
            glowLight = Make.Light(transform, centre + new Vector3(0, 6, 0), 46, Color.white, 0, 0.77f);
            face.color = Color.clear;
            Clear();
        }

        public bool Contains(Vector2 world) =>
            Mathf.Abs(world.x - faceCentre.x) <= faceSize.x / 2 && Mathf.Abs(world.y - faceCentre.y) <= faceSize.y / 2;

        public void Show(MaterialData m, bool pop)
        {
            if (Material == m.id) return;
            Material = m.id;
            glows = !string.IsNullOrEmpty(m.glow);
            glow = Px.Hex(glows ? m.glow : "#f0dcc0");
            icon.sprite = Make.Lib.Sprite(m.id);
            icon.enabled = shadow.enabled = ring.enabled = halo.enabled = true;
            icon.color = Color.white;
            glowLight.color = glow;
            appear = pop ? 0 : 1;
        }

        public void Clear()
        {
            Material = null;
            icon.enabled = shadow.enabled = ring.enabled = halo.enabled = false;
            glowLight.intensity = 0;
        }

        /// <summary>A quick bright pulse of the whole tile face.</summary>
        public void Flash(Color c) { flashColor = c; flash = 1; }

        /// <summary>The material sinks into the tile in a puff of its own colour.</summary>
        public IEnumerator Consume()
        {
            if (Material == null) yield break;
            Sparks.I.Burst(centre + new Vector3(0, 8, 0), 14, glow.Times(2), 40, 0.5f, 30, -20);
            yield return Anim.Over(0.18f, t => { icon.color = Color.white.Alpha(1 - t); icon.transform.localScale = Vector3.one * (1 + t * 0.4f); });
            icon.transform.localScale = Vector3.one;
            Clear();
        }

        void Update()
        {
            float t = Time.time;
            flash = Mathf.Max(0, flash - Time.deltaTime * 2.6f);
            float hover = Hovered ? 0.45f + 0.3f * Mathf.Sin(t * 5) : 0;
            face.color = flash > 0 ? flashColor.Alpha(flash) : Hovered ? new Color(1, 0.85f, 0.45f, hover) : Color.clear;
            if (Material == null) return;
            appear = Mathf.Min(1, appear + Time.deltaTime * 5);
            float pop = Anim.OutBack(appear);
            int bob = Mathf.RoundToInt(Mathf.Sin(t * 2 + index * 1.7f) * 1.5f);
            icon.transform.position = centre + new Vector3(0, 10 + bob + (1 - pop) * 8, 0);
            ring.color = glow.Times(1.6f).Alpha((0.55f + 0.25f * Mathf.Sin(t * 3 + index)) * appear);
            halo.color = glow.Alpha(glows ? 0.3f : 0.07f);
            glowLight.intensity = (glows ? 0.95f : 0.3f) * appear + flash * 0.8f;
        }
    }
}
