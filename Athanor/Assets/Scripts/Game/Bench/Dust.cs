using UnityEngine;

namespace Athanor.Game
{
    /// <summary>Dust drifting down through the moonbeam.</summary>
    public sealed class Dust : MonoBehaviour
    {
        public int count = 46;
        public Material material;
        public Sprite sprite;
        SpriteRenderer[] motes;

        static float Hash(int a, int b) => Mathf.Abs(Mathf.Sin(a * 127.1f + b * 311.7f) * 43758.5453f) % 1;

        void Start()
        {
            motes = new SpriteRenderer[count];
            for (int i = 0; i < count; i++)
            {
                var go = new GameObject("mote");
                go.transform.SetParent(transform, false);
                go.transform.localScale = new Vector3(0.25f, 0.25f, 1);
                var r = go.AddComponent<SpriteRenderer>();
                r.sprite = sprite; r.sharedMaterial = material; r.sortingOrder = 8;
                motes[i] = r;
            }
        }

        void Update()
        {
            float t = Time.time;
            for (int i = 0; i < count; i++)
            {
                float y = 30 + (Hash(i, 3) * 300 + t * (5 + (i % 5) * 2.5f)) % 220, tt = (y - 20) / 250;
                float x = 320 - 10 * tt + (Hash(i, 7) - 0.5f) * 2 * (30 + 60 * tt) + Mathf.Sin(t * 0.7f + i) * 4;
                float s = Mathf.Sin(t * 2 + i * 2.1f);
                motes[i].transform.localPosition = Px.Snap(Px.At(x, y));
                motes[i].color = new Color(0.7f, 0.85f, 1, 0.3f + 0.4f * s * s);
            }
        }
    }
}
