using UnityEngine;

namespace Athanor.Game
{
    /// <summary>
    /// Dust drifting down through the moonbeam: a loop, each mote's position a pure function of time, all of them drawn
    /// as one SparkMesh (one renderer) at order 8.
    /// </summary>
    public sealed class Dust : MonoBehaviour
    {
        public int count = 46;
        /// <summary>Not used since the motes became one mesh (it has its own material); kept so the built scene still loads it.</summary>
        public Material material;
        public Sprite sprite;
        SparkMesh motes;

        static float Hash(int a, int b) => Mathf.Abs(Mathf.Sin(a * 127.1f + b * 311.7f) * 43758.5453f) % 1;

        void Start() => motes = SparkMesh.Create(transform, "motes", 8, sprite, null);

        void LateUpdate()
        {
            float t = Time.time;
            motes.Begin();
            for (int i = 0; i < count; i++)
            {
                float y = 30 + (Hash(i, 3) * 300 + t * (5 + (i % 5) * 2.5f)) % 220, tt = (y - 20) / 250;
                float x = 320 - 10 * tt + (Hash(i, 7) - 0.5f) * 2 * (30 + 60 * tt) + Mathf.Sin(t * 0.7f + i) * 4;
                float s = Mathf.Sin(t * 2 + i * 2.1f);
                motes.Add(Px.At(x, y), new Color(0.7f, 0.85f, 1, 0.3f + 0.4f * s * s), false);
            }
            motes.End();
        }
    }
}
