using System.Collections;
using Athanor.Core;
using PrimeTween;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Athanor.Game
{
    /// <summary>One of the nine bench tiles: its hover highlight and, when occupied, the material standing on it.</summary>
    public sealed class TileView : MonoBehaviour
    {
        /// <summary>Stops what this component started so nothing runs on into the next Play (domain reload off).</summary>
        void OnDestroy() { StopAllCoroutines(); Motion.StopOn(this); }

        FxRefs fx;
        public int index;

        /// <summary>Takes the effect players (BenchView calls it).</summary>
        public void Bind(FxRefs effects)
        {
            if (effects == null) throw new System.ArgumentNullException(nameof(effects));
            fx = effects;
        }

        public Vector3 centre;            // world position of the middle of the top face
        // serialized: the tile lives in Prefabs/Views/Bench.prefab, so what it needs is kept in the prefab
        [SerializeField, HideInInspector] Vector2 faceCentre, faceSize;
        [SerializeField, HideInInspector] SpriteRenderer face, ring, shadow, icon, halo, lid, cross, mark;
        [SerializeField, HideInInspector] Color markColor, glow, flashColor;
        [SerializeField, HideInInspector] Light2D glowLight;
        bool glows;
        float flash, appear = 1;

        public string Material { get; private set; }
        public bool Hovered { get; set; }
        /// <summary>Lit softly in its marker's colour, to show which tile an aspect in the HUD sits on.</summary>
        public bool Pinged { get; set; }
        /// <summary>Nothing can be placed here (a cold tile, or grit from an event).</summary>
        public bool Shut { get; private set; }
        public bool Marked => mark.enabled;
        public SpriteRenderer Icon => icon;

        /// <summary>The face the tile is hit-tested on: centre and size in flat world units.</summary>
        public Vector2 FaceCentre => faceCentre;
        public Vector2 FaceSize => faceSize;

        public bool Contains(Vector2 world) =>
            Mathf.Abs(world.x - faceCentre.x) <= faceSize.x / 2 && Mathf.Abs(world.y - faceCentre.y) <= faceSize.y / 2;

        /// <summary>The marker's colour (null: no marker) and whether the tile is shut.</summary>
        public void SetState(Color? marker, bool shut)
        {
            Shut = shut;
            lid.color = shut ? new Color(0.03f, 0.02f, 0.05f, 0.78f) : Color.clear;
            cross.enabled = shut;
            mark.enabled = marker.HasValue && !shut;
            markColor = marker ?? Color.white;
            mark.color = markColor;
        }

        public void Show(MaterialData m, bool pop)
        {
            if (Material == m.id) return;
            Material = m.id;
            glows = !string.IsNullOrEmpty(m.glow);
            glow = Px.Hex(glows ? m.glow : "#f0dcc0");
            icon.sprite = Art.Lib.Sprite(m.id);
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
            fx.Sparks.Burst(centre + new Vector3(0, 8, 0), 14, glow.Times(2), 40, 0.5f, 30, -20);
            icon.color = Color.white;
            yield return Sequence.Create()
                .Group(Motion.Fade(icon, 1, 0, 0.18f))
                .Group(Motion.Scale(icon.transform, 1, 1.4f, 0.18f))
                .ToYieldInstruction();
            icon.transform.localScale = Vector3.one;
            Clear();
        }

        void Update()
        {
            float t = Time.time;
            flash = Mathf.Max(0, flash - Time.deltaTime * 2.6f);
            float hover = Hovered ? 0.45f + 0.3f * Mathf.Sin(t * 5) : 0;
            face.color = flash > 0 ? flashColor.Alpha(flash) : Hovered && !Shut ? new Color(1, 0.85f, 0.45f, hover)
                : Pinged ? markColor.Alpha(0.35f + 0.25f * Mathf.Sin(t * 6)) : Color.clear;
            if (Material == null) return;
            appear = Mathf.Min(1, appear + Time.deltaTime * 5);
            float pop = Anim.OutBack(appear);
            int bob = Mathf.RoundToInt(Mathf.Sin(t * 2 + index * 1.7f) * 1.5f);
            icon.transform.localPosition = centre + new Vector3(0, 10 + bob + (1 - pop) * 8, 0);   // local: below a depth group the flat coordinates stay valid
            ring.color = glow.Times(1.6f).Alpha((0.55f + 0.25f * Mathf.Sin(t * 3 + index)) * appear);
            halo.color = glow.Alpha(glows ? 0.3f : 0.07f);
            glowLight.intensity = (glows ? 0.95f : 0.3f) * appear + flash * 0.8f;
        }
    }
}
