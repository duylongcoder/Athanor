using System.Collections;
using Athanor.Gameplay;
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
        /// <summary>Colours and offsets of the tile, stored in Bench.prefab.</summary>
        [System.Serializable]
        public struct Layout
        {
            /// <summary>The lid over a shut tile.</summary>
            public Color shutLid;
            /// <summary>The face's glow while pointed at (alpha is the pulse).</summary>
            public Color hoverFace;
            /// <summary>The puff of Consume starts this far above the centre; the icon stands this far above it.</summary>
            public Vector3 consumeLift;
            public float iconLift;
            /// <summary>A material popping in starts this far below its place.</summary>
            public float popRise;
            /// <summary>The puff of Consume: count, speed, life, order, gravity (its colour is the tile's glow times consumeTint, so the spec's own colour is unused).</summary>
            public BurstSpec consumeBurst;
            public float consumeTint;
            /// <summary>The icon fades out and grows to consumeGrow times its size over consumeSeconds.</summary>
            public float consumeSeconds, consumeGrow;
        }
        [SerializeField] Layout layout;
        bool glows, faceSet;
        float flash, appear = 1, shownLight = float.NaN;
        Color shownFace;

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
            lid.color = shut ? layout.shutLid : Color.clear;
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
            glow = glows ? Px.Hex(m.glow) : Art.Palette.defaultGlow;
            icon.sprite = Art.Lib.Sprite(m.id);
            icon.enabled = shadow.enabled = ring.enabled = halo.enabled = true;
            icon.color = Color.white;
            glowLight.color = glow;
            halo.color = glow.Alpha(glows ? 0.3f : 0.07f);       // fixed by the material: set once here, not every frame
            appear = pop ? 0 : 1;
        }

        public void Clear()
        {
            Material = null;
            icon.enabled = shadow.enabled = ring.enabled = halo.enabled = false;
            glowLight.intensity = 0; shownLight = 0;
        }

        /// <summary>A quick bright pulse of the whole tile face.</summary>
        public void Flash(Color c) { flashColor = c; flash = 1; }

        /// <summary>The material sinks into the tile in a puff of its own colour.</summary>
        public IEnumerator Consume()
        {
            if (Material == null) yield break;
            var puff = layout.consumeBurst;
            fx.Sparks.Burst(centre + layout.consumeLift, puff.count, glow.Times(layout.consumeTint), puff.speed, puff.life, puff.order, puff.gravity);
            icon.color = Color.white;
            yield return Sequence.Create()
                .Group(Motion.Fade(icon, 1, 0, layout.consumeSeconds))
                .Group(Motion.Scale(icon.transform, 1, layout.consumeGrow, layout.consumeSeconds))
                .ToYieldInstruction();
            icon.transform.localScale = Vector3.one;
            Clear();
        }

        void Update() => Tick(Time.deltaTime);

        /// <summary>One frame of the tile. An empty tile nobody points at writes nothing; a material's ring pulse is the only thing written every frame.</summary>
        public void Tick(float deltaTime)
        {
            float t = Time.time;
            flash = Mathf.Max(0, flash - deltaTime * 2.6f);
            float hover = Hovered ? 0.45f + 0.3f * Mathf.Sin(t * 5) : 0;
            Color want = flash > 0 ? flashColor.Alpha(flash) : Hovered && !Shut ? layout.hoverFace.Alpha(hover)
                : Pinged ? markColor.Alpha(0.35f + 0.25f * Mathf.Sin(t * 6)) : Color.clear;
            if (!faceSet || want.r != shownFace.r || want.g != shownFace.g || want.b != shownFace.b || want.a != shownFace.a) { face.color = shownFace = want; faceSet = true; }
            if (Material == null) return;
            appear = Mathf.Min(1, appear + deltaTime * 5);
            float pop = Anim.OutBack(appear);
            int bob = Mathf.RoundToInt(Mathf.Sin(t * 2 + index * 1.7f) * 1.5f);
            var at = centre + new Vector3(0, layout.iconLift + bob + (1 - pop) * layout.popRise, 0);   // local: below a depth group the flat coordinates stay valid
            if (icon.transform.localPosition != at) icon.transform.localPosition = at;
            ring.color = glow.Times(1.6f).Alpha((0.55f + 0.25f * Mathf.Sin(t * 3 + index)) * appear);
            float light = (glows ? 0.95f : 0.3f) * appear + flash * 0.8f;
            if (light != shownLight) { shownLight = light; glowLight.intensity = light; }
        }
    }
}
